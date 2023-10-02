using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace YouTubeTest.Pages
{
    internal class SearchResultsPage : BasePage
    {
        By filterButtonBy = By.Id("filter-button");
        By videoFilterBy = By.XPath("//*[@id='label'][@title='Search for Video']");
        By countFilterBy = By.XPath("//*[@id='label'][@title='Sort by view count']");
        By videoTitleBy = By.Id("video-title");
        public SearchResultsPage(IWebDriver driver) : base(driver) { }

        public SearchResultsPage FilterByVideo()
        {
            Filter();

            var videoFilter = ShortWait.Until(ExpectedConditions.ElementIsVisible(videoFilterBy));
            videoFilter.Click();

            return this;
        }

        public SearchResultsPage FilterByCount()
        {
            Filter();

            var countFilter = ShortWait.Until(ExpectedConditions.ElementIsVisible(countFilterBy));
            countFilter.Click();

            return this;
        }

        public VideoPage SearchVideoByUrl(string url)
        {
            var videoTitles = Driver.FindElements(videoTitleBy);

            var selectedTitle = videoTitles.Where(x =>
                !string.IsNullOrEmpty(x.GetAttribute("href")) &&
                x.GetAttribute("href").Contains($"{url}"))
                    .First();

            selectedTitle.Click();

            return new VideoPage(Driver);
        }

        private void Filter()
        {
            var filterButton = ShortWait.Until(ExpectedConditions.ElementIsVisible(filterButtonBy));
            filterButton.Click();
        }
    }
}
