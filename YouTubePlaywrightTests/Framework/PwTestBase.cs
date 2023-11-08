using Microsoft.Playwright;
using NUnit.Framework.Interfaces;

namespace YouTubePlaywrightTests.Framework
{
    public abstract class PwTestBase
    {
        //For enabling SelectorsHub Chrome extension when headless==false
        //const string SelectorsHubExtensionPath = @"C:\Install\Chrome Extensions\SelectorsHub_5_1_3_0.crx";
        //const string SelectorsHubChromeExtensionPath = @"%LOCALAPPDATA%\Google\Chrome\User Data\Profile 1\Extensions\ndgimibanhlabgdgjcpbbndiehljcpfh\5.1.3_0\";
        //const string ChromeUserDataDirPath = @"%LOCALAPPDATA%\Google\Chrome\User Data\Profile 1";
        public IPlaywright PlaywrightInstance { get; set; }
        public IBrowser BrowserInstance { get; set; }
        public IBrowserContext BrowserContext { get; set; }
        public IPage Page { get; set; }

        [SetUp]
        public async Task SetUp()
        {
            PlaywrightInstance = await Playwright.CreateAsync();

            BrowserInstance = await PlaywrightInstance.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
             });

            var context = await BrowserInstance.NewContextAsync();

            Page = await context.NewPageAsync();

            //For enabling SelectorsHub Chrome extension when headless==false
            //BrowserContext = await PlaywrightInstance.Chromium.LaunchPersistentContextAsync(
            //    $"",
            //    new BrowserTypeLaunchPersistentContextOptions
            //    {
            //        Headless = false,
            //        Args = new[] 
            //        { 
            //            "--start-maximized",
            //            //"--profile-directory=Profile 1",
            //            //$"--disable-extensions-except={SelectorsHubChromeExtensionPath}",
            //            //$"--load-extension={SelectorsHubChromeExtensionPath}" 
            //        },
            //        SlowMo = 500,
            //    }
            //);

            //Page = BrowserContext.Pages[0];
        }

        [TearDown]
        public async Task TearDown()
        {
            var path = $".\\screenshots\\screenshot{DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss.FFF")}.png";

            if(TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed||
            TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Inconclusive||
            TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Warning)
            {
                await Page.ScreenshotAsync(new()
                {
                    Path = path,
                    OmitBackground = true,
                    FullPage = true,
                });

                TestContext.AddTestAttachment(path);
            }

            if (Page != null)
            {
                await Page.CloseAsync();
            }

            if (BrowserContext != null)
            {
                await BrowserContext.CloseAsync();
            }
        }
    }
}
