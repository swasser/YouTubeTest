using Microsoft.Playwright.NUnit;
using YouTubePlaywrightTests.Pages;

namespace YouTubePlaywrightTests.Tests
{
    public class YouTubePwTests : PageTest
    {
        [Test, Repeat(1)]
        public async Task YouTube_Test_PageTest_PageObjects()
        {
            var youTubePage = new YouTubeMainPage(Page);

            await youTubePage.GotoAsync();

            var searchResultsPage = await youTubePage.SearchAsync("I Will Survive - Alien song");

            await searchResultsPage.FilterByVideo();

            await searchResultsPage.FilterByCount();

            var videoPage = await searchResultsPage.SearchByVideoUrl("watch?v=ybXrrTX3LuI");

            videoPage.SkipAds();

            var artistName = await videoPage.GetArtistName();

            TestContext.WriteLine($"Artist name is : {artistName}");
        }
    }
}