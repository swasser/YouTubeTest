using NUnit.Framework.Internal;
using YouTubeTest.Framework;
using YouTubeTest.Pages;

namespace YouTubeTest
{
    public class YouTubeTests : WebTestBase
    {
        [Test]
        public void Search_filter_play_PO()
        {
            YouTubeMainPage Start = new YouTubeMainPage(Driver);

            SearchResultsPage Results = Start.Search("I Will Survive - Alien song");

            Results.FilterByVideo();

            Results.FilterByCount();

            VideoPage VideoPage = Results.SearchVideoByUrl("watch?v=ybXrrTX3LuI");

            VideoPage.SkipAds();

            var artistName = VideoPage.GetArtistName();

            Logger.Info($"Artist name is: {artistName}. ");
        }
    }
}