using Scrapper.Models;

namespace Scrapper.Services.Contracts;

public interface ISiteScrapper
{
    public IEnumerable<ScrappedElement> GetElementFromSpecifiedSite(string SiteConfig);
}
