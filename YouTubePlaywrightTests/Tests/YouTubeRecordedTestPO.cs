using YouTubePlaywrightTests.Framework;
using YouTubePlaywrightTests.Pages;

namespace YouTubePlaywrightTests.Tests
{
    internal class YouTubeRecordedTestPO : PwTestBase
    {
        [Test]
        public async Task YouTube_Test_PageObjects()
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
