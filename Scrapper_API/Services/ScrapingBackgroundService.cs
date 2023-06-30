using Newtonsoft.Json;
using Producer;
using Scrapper_API.Services.Contracts;
using System.Runtime.CompilerServices;

namespace Scrapper_API.Services;

public class ScrapingBackgroundService : BackgroundService
{
    private readonly IScrapingService _scrapingService;
    private readonly ILogger<ScrapingBackgroundService> _logger;
    private CancellationTokenSource _cancellationTokenSource;

    private RabbitMqProducer _producer;
    private readonly RedisCache _cache;
    public ScrapingBackgroundService(IScrapingService scrapingService, ILogger<ScrapingBackgroundService> logger, RabbitMqProducer producer, RedisCache cache)
    {
        _scrapingService = scrapingService;
        _logger = logger;
        _producer = producer;
        _cache = cache;
        _cancellationTokenSource = new CancellationTokenSource();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested && !_cancellationTokenSource.Token.IsCancellationRequested)
        {
            var newResults =  await _scrapingService.ScrapSite();
            var cachedResults = GetCachedResults();

            var newItems = newResults?.Except(cachedResults).ToList();
            _logger.LogWarning("Set result: {result}", newItems?.Count());
            if (newItems != null && newItems.Any())
            {
                foreach (var item in newItems)
                {
                    _producer.PublishMessage(item);
                }
                _logger.LogWarning("Set result: {result}", newItems?.Count());
                SetCachedResults(newResults);
                _logger.LogInformation("Scraping result: {result}", newItems?.Count());

            }
            else
            {
                _logger.LogInformation("Scraping result: no new items");

            }

            await Task.Delay(TimeSpan.FromSeconds(30), _cancellationTokenSource.Token);
        }
    }

    private IEnumerable<string> GetCachedResults()
    {
        var cachedResultsJson = _cache.Get("ScrapingResults");
        return string.IsNullOrEmpty(cachedResultsJson)
            ? new List<string>()
            : JsonConvert.DeserializeObject<List<string>>(cachedResultsJson);
    }

    private void SetCachedResults(IEnumerable<string> results)
    {
        var resultsJson = JsonConvert.SerializeObject(results);
        _cache.Set("ScrapingResults", resultsJson);
    }

    public void Stop()
    {
        _cancellationTokenSource.Cancel();
    }
    public void Start()
    {
        // Check if the previous token was cancelled, and if so, create a new one
        if (_cancellationTokenSource.IsCancellationRequested)
        {
            _cancellationTokenSource = new CancellationTokenSource();
        }
    }
}
