using Boa.Constrictor.Screenplay;
using Boa.Constrictor.Selenium;
using OpenQA.Selenium;
using YouTubeScreenPlayTest.Pages;

namespace YouTubeScreenPlayTest.Helpers
{
    internal class GetChannelName : IQuestion<string>
    {
        private string Url;
        private GetChannelName(string url) 
        {
            Url = url;
        }

        public static GetChannelName For(string url) => new GetChannelName(url);
        
        public string RequestAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;

            var videos = GetVideos(actor, driver);

            var resultKey = videos.Keys.First(x => x.Contains(Url));

            var resultValue = videos[resultKey];

            return resultValue;
        }

        private Dictionary<string,string> GetVideos(IActor actor, IWebDriver driver)
        {
            var list = actor.AskingFor(Appearance.Of(SearchResultsPage.containerVideoItem));
            var videoItems = SearchResultsPage.containerVideoItem.FindElements(driver);

            //bool temp;

            //Check for video itmes list refresh
            //try
            //{
            //    new WebDriverWait(driver, TimeSpan.FromSeconds(2)).Until(d => SearchResultsPage.videosContainer.FindElements(driver));
            //    Action act = () => videoItems.ToList().ForEach(item => _ = item.Displayed);
            //    var t = act.Should().NotThrowAfter(10.Seconds(), 100.Microseconds());
            //}
            //catch (WebDriverTimeoutException)
            //{
            //    //videoItems = SearchResultsPage.containerVideoItem.FindElements(driver);
            //}
            //catch (StaleElementReferenceException)
            //{
            //    videoItems = SearchResultsPage.containerVideoItem.FindElements(driver);
            //}

            var dict = new Dictionary<string, string>();

            try
            {
                foreach (var item in videoItems)
                {
                    WebLocator linkLocator = (WebLocator)SearchResultsPage.containerVideoLink;
                    By linkQuery = linkLocator.Query;

                    WebLocator channelLocator = (WebLocator)SearchResultsPage.containerChannelName;
                    By channelQuery = channelLocator.Query;

                    var link = item.FindElement(linkQuery);
                    var channel = item.FindElement(channelQuery);

                    var linkText = link.GetAttribute("href");
                    var channelText = channel.GetDomProperty("innerText");

                    dict.Add(linkText, channelText);
                }
            }
            catch (ArgumentException)
            {
                dict = GetVideos(actor, driver);
            }
            catch (StaleElementReferenceException)
            {
                dict = GetVideos(actor, driver);
            }

            return dict;
        }
    }
}
