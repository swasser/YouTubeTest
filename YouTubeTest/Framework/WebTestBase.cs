using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using YouTubeTest.Pages;

namespace YouTubeTest.Framework
{
    [TestFixture]
    public class WebTestBase
    {
        internal IWebDriver Driver { get; set; }
        internal ILogger<BasePage> Logger { get; set; }
        internal IHost TestHost { get; set; }
        internal IConfiguration Configuration { get; set; }
        
        [SetUp]
        public void Setup()
        {
            Configuration = WebTestHost.TestServiceProvider().GetRequiredService<IConfiguration>();

            Logger = WebTestHost.TestServiceProvider().GetRequiredService<ILogger<BasePage>>();

            Driver = WebTestHost.TestServiceProvider().GetRequiredService<IWebDriver>();

            Driver.Navigate().GoToUrl($"{Configuration.GetValue<string>("appUrl")}");

            Driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TearDown()
        {
            Driver?.Dispose();
            TestHost?.Dispose();
        }
    }
}
