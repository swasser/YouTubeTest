using Boa.Constrictor.Screenplay;
using Boa.Constrictor.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using YouTubeScreenPlayTest.Pages;

namespace YouTubeScreenPlayTest.Interactions
{
    internal class FilterVideos : ITask
    {
        public Filter Filter { get; }
        private FilterVideos(Filter filter) => Filter = filter;
        public static FilterVideos By(Filter filter) => new FilterVideos(filter);

        public void PerformAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;
            var locator = ((WebLocator)SearchResultsPage.videosContainer).Query;
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
            wait.IgnoreExceptionTypes(typeof(WebDriverTimeoutException));

            switch (Filter)
            {
                case Filter.Type_Video:
                    actor.AttemptsTo(Click.On(SearchResultsPage.Filter));
                    actor.AttemptsTo(Click.On(SearchResultsPage.ByVideo));
                    try
                    {
                        wait.Until(StalenessOf(driver.FindElements(locator).First()));
                    }
                    catch (WebDriverTimeoutException)
                    {
                       //Do nothing
                    }
                        
                    break;
                case Filter.SortBy_Count:
                    actor.AttemptsTo(Click.On(SearchResultsPage.Filter));
                    actor.AttemptsTo(Click.On(SearchResultsPage.ByCount));
                    try
                    {
                        wait.Until(StalenessOf(driver.FindElements(locator).First()));
                    }
                    catch (WebDriverTimeoutException)
                    {
                        //Do nothing
                    }
                    break;
            }
        }

        public static Func<IWebDriver, bool> StalenessOf(IWebElement element)
        {
            return delegate
            {
                try
                {
                    return element == null || !element.Enabled;
                }
                catch (StaleElementReferenceException)
                {
                    return true;
                }
                catch (WebDriverTimeoutException)
                {
                    return true;
                }
            };
        }
    }

    enum Filter
    {
        Type_Video = 10,
        SortBy_Count = 50
    }
}
