using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace YouTubeTest.Pages
{
    internal abstract class BasePage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _longWait;
        private readonly WebDriverWait _shortWait;

        internal IWebDriver Driver { get { return _driver; } }
        internal WebDriverWait LongWait { get { return _longWait; } }
        internal WebDriverWait ShortWait { get { return _shortWait; } }

        public BasePage(IWebDriver driver)
        {
            _driver = driver;
            _shortWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(2));
            _longWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
        }
    }
}
