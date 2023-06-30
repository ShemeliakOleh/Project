using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace Scrapper_API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ScrapperController : ControllerBase
{



    [HttpGet("Some")]
    public async Task<IEnumerable<string>> Get()
    {
        return new List<string>();
    }


    [HttpGet("GetElements")]
    public async Task<IEnumerable<string>> GetList()
    {


        var sites = new List<string>();

        IWebDriver driver = new ChromeDriver();

        driver.Url = @"https://jobs.dou.ua/vacancies/?category=.NET";






        IWebElement moreBtn = null;
        var IsMoreBtn = true;


        while (IsMoreBtn)
        {
            try
            {
                moreBtn = driver.FindElement(By.XPath("//a[@href='javascript:;'][contains(.,'Більше вакансій')]"));
                await Task.Delay(1000);
            }
            catch (Exception)
            {
                await Task.Delay(1000);
            }

            if (moreBtn != null)
            {
                Console.WriteLine(moreBtn.Text);
                var button = moreBtn.FindElement(By.XPath("//a[@href='javascript:;'][contains(.,'Більше вакансій')]"));
                if (button != null)
                {
                    try
                    {
                        button.Click();
                    }
                    catch (Exception)
                    {
                        IsMoreBtn = false;
                    }
                }

            }
        }




        for (int i = 0; i < 10; i++)
        {
            try
            {
                driver.FindElement(By.XPath("(//a[@class='vt'])[1]"));
                await Task.Delay(1000);
            }
            catch (Exception)
            {
                await Task.Delay(1000);
            }

            if (driver.FindElement(By.XPath($"(//a[@class='vt'])[{1}]")).Text != string.Empty)
            {
                var jobString = string.Empty;
                var j = 1;
                do
                {
                    try
                    {
                        jobString = driver.FindElement(By.XPath($"(//a[@class='vt'])[{j}]")).Text;
                        Console.WriteLine(jobString);
                        sites.Add(jobString);
                        j++;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("There are no more jobs");
                        jobString = string.Empty;
                    }


                } while (jobString.Length > 0);

                break;

            }

        }
        return sites;

    }
}
