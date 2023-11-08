using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace YouTubeTest.Pages
{
    internal class YouTubeMainPage : BasePage
    {
        By searchBoxBy = By.XPath("//input[@id='search']");

        public YouTubeMainPage(IWebDriver driver, ILogger<BasePage> logger) : base(driver, logger) {}
                 
        public SearchResultsPage Search(string searchText)
        {
            var searchBox = ShortWait.Until(ExpectedConditions.ElementIsVisible(searchBoxBy));

            searchBox.Click();

            searchBox.SendKeys(searchText);

            searchBox.Submit();

            return new SearchResultsPage(Driver, Logger);
        }
    }
}
