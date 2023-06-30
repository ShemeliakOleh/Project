namespace Scrapper_API.Services.Contracts;

public interface IScrapingService
{
    public Task<IEnumerable<string>> ScrapSite();
}
