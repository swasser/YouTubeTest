using Boa.Constrictor.Selenium;
using OpenQA.Selenium;

namespace YouTubeScreenPlayTest.Pages
{
    internal class SearchResultsPage
    {
        public static IWebLocator ByCount => WebLocator.L(
            "Filter by count option",
            By.XPath("//*[@id='label'][@title='Sort by view count']")
            );

        public static IWebLocator ByVideo => WebLocator.L(
            "Filter by video option",
            By.XPath("//*[@id='label'][@title='Search for Video']")
            );

        public static IWebLocator Filter => WebLocator.L(
            "Filter button",
            By.Id("filter-button")
            );

        public static IWebLocator VideoItem => WebLocator.L(
            "Video item",
            By.XPath("//ytd-video-renderer//a[@id='thumbnail']")
            );

        public static IWebLocator VideoItemWithUrl(string url) => WebLocator.L(
            "Video item with a give URL",
            By.CssSelector($"#video-title[href*='{url}']")
            );

        public static IWebLocator containerChannelName => WebLocator.L(
            "Channel name (within video item container)",
            By.CssSelector("#channel-info")
            );

        public static IWebLocator containerVideoLink => WebLocator.L(
            "Video item link (within video item container)",
            By.CssSelector("#video-title")
            );

        public static IWebLocator containerVideoItem => WebLocator.L(
            "Video item container",
            By.CssSelector("#dismissible.style-scope.ytd-video-renderer")
            );

        public static IWebLocator videosContainer => WebLocator.L(
            "Video item container",
            By.CssSelector("#contents.style-scope.ytd-section-list-renderer")
            );
    }
}
