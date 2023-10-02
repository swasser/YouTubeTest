using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace YouTubeTest.Framework
{
    internal static class WebTestHost
    {
        internal static IHost TestHostStart()
        {
            Logger logger = NLog.LogManager.Setup().LoadConfigurationFromFile().GetCurrentClassLogger();

            var builder = new HostBuilder().ConfigureServices(services =>
            {
                services.AddTransient<IWebDriver, ChromeDriver>().
                AddLogging(logbuilder =>
                {
                    logbuilder.ClearProviders();
                    logbuilder.AddNLog();
                });
            }
            );

            builder.ConfigureAppConfiguration(config =>
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                );

            return builder.Build();
        }
        
        internal static IWebDriver GetDriver(IHost host)
        {
            return host.Services.GetRequiredService<IWebDriver>();
        }
    }
}
