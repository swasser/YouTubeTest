using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace YouTubeTest.Pages
{
    internal abstract class BasePage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _longWait;
        private readonly WebDriverWait _shortWait;
        private readonly ILogger<BasePage> _logger;

        internal IWebDriver Driver { get { return _driver; } }
        internal ILogger<BasePage> Logger { get { return _logger; } }
        internal WebDriverWait LongWait { get { return _longWait; } }
        internal WebDriverWait ShortWait { get { return _shortWait; } }

        public BasePage(IWebDriver driver, ILogger<BasePage> logger)
        {
            _driver = driver;
            _logger = logger;
            _shortWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(3));
            _longWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
        }
    }
}
