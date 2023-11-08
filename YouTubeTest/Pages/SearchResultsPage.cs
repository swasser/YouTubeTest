using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace YouTubeTest.Pages
{
    internal class SearchResultsPage : BasePage
    {
        By channelNameBy = By.XPath(".//ytd-channel-name[@id='channel-name'][@class='long-byline style-scope ytd-video-renderer']");
        By countFilterBy = By.XPath("//*[@id='label'][@title='Sort by view count']");
        By filterButtonBy = By.Id("filter-button");
        By videoFilterBy = By.XPath("//*[@id='label'][@title='Search for Video']");
        By videoItemBy = By.XPath("//ytd-video-renderer//a[@id='thumbnail']");
 
        public SearchResultsPage(IWebDriver driver, ILogger<BasePage> logger) : base(driver, logger){ }

        public SearchResultsPage FilterByVideo()
        {
            Filter();

            var videoFilter = ShortWait.Until(ExpectedConditions.ElementIsVisible(videoFilterBy));
            videoFilter.Click();

            WaitToReload();

            return this;
        }

        public SearchResultsPage FilterByCount()
        {
            Filter();

            var countFilter = ShortWait.Until(ExpectedConditions.ElementIsVisible(countFilterBy));
            countFilter.Click();

            WaitToReload();

            return this;
        }

        public VideoPage SearchVideoByUrl(string url)
        {
            var videoTitles = Driver.FindElements(videoItemBy);

            var selectedTitle = videoTitles.Where(x =>
                !string.IsNullOrEmpty(x.GetAttribute("href")) &&
                x.GetAttribute("href").Contains($"{url}")).ToList().FirstOrDefault();
 
            var video = selectedTitle?.FindElement(By.XPath(".//../../.."));

            var channel = video?.FindElement(channelNameBy).Text;

            Logger.Log(Microsoft.Extensions.Logging.LogLevel.Information, "{channel}", channel);

            video?.Click();

            return new VideoPage(Driver, Logger);
        }

        private void WaitToReload()
        {
            try
            {
                ShortWait.Until(ExpectedConditions.StalenessOf(Driver.FindElements(videoItemBy).First()));
            }
            catch (WebDriverTimeoutException)
            {
                //Do nothing
            }
        }

        private void Filter()
        {
            var filterButton = ShortWait.Until(ExpectedConditions.ElementIsVisible(filterButtonBy));
            filterButton.Click();
        }
    }
}
