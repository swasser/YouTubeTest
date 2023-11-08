using Microsoft.Playwright;

namespace YouTubePlaywrightTests.Tests
{
    public class YouTubeRecordedTest
    {
        [Test]
        public static async Task YouTube_Recorded_Test()
        {
            //Setup
            using var playwright = await Playwright.CreateAsync();

            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
            });
            var context = await browser.NewContextAsync();

            var page = await context.NewPageAsync();

            await page.GotoAsync("https://www.youtube.com/");

            //Search page

            await page.GetByPlaceholder("Search").ClickAsync();

            await page.GetByPlaceholder("Search").FillAsync("I Will Survive - Alien song");

            await page.GetByRole(AriaRole.Button, new() { Name = "Search", Exact = true }).ClickAsync();

            //Search results page

            await page.GetByLabel("Search filters").ClickAsync();

            await page.GetByRole(AriaRole.Link, new() { Name = "Video", Exact = true }).ClickAsync();

            await page.GetByLabel("Search filters").ClickAsync();

            await page.GetByRole(AriaRole.Link, new() { Name = "View count" }).ClickAsync();

            await page.GetByTitle("I Will Survive - Aliensong").ClickAsync();

            //Video page

            //await page.GetByTitle("I Will Survive - Aliensong", new() { Name = }).ClickAsync();

            await page.GetByRole(AriaRole.Button, new() { Name = "...more" }).ClickAsync();

            await page.Locator("#structured-description ytd-info-row-renderer").Filter(new() { HasText = "ARTIST Gloria Gaynor" }).Locator("#default-metadata-section").ClickAsync();
        }
    }
}

