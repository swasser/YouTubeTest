using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using OpenQA.Selenium;

namespace YouTubeTest.Framework
{
    [TestFixture]
    public class WebTestBase
    {
        internal IWebDriver? Driver { get; set; }
        internal ILogger Logger { get; set; }
        internal IHost TestHost { get; set; }

        internal IConfiguration Configuration { get; set; }
        
        [SetUp]
        public void Setup()
        {
            TestHost = WebTestHost.TestHostStart();

            Configuration = TestHost.Services.GetRequiredService<IConfiguration>();

            Driver = WebTestHost.GetDriver(TestHost);

            Logger = LogManager.GetCurrentClassLogger();

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
