using Scrapper.Models;
using Scrapper.Services.Contracts;

namespace Scrapper.Services;

public class SiteScrapper : IScrapper
{
    public IEnumerable<ScrappedElement> GetElementFromSpecifiedSite(string SiteConfig)
    {
        throw new NotImplementedException();
    }
}
