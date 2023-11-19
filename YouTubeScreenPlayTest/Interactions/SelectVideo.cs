using Boa.Constrictor.Screenplay;
using Boa.Constrictor.Selenium;
using YouTubeScreenPlayTest.Pages;

namespace YouTubeScreenPlayTest.Interactions
{
    internal class SelectVideo : ITask
    {
        private string VideoUrl { get; }

        private SelectVideo(string videoUrl) => VideoUrl = videoUrl;

        public static SelectVideo ForUrl(string videoUrl) => new SelectVideo(videoUrl);

        public void PerformAs(IActor actor)
        {
            actor.AsksFor(Text.Of(SearchResultsPage.VideoItemWithUrl(VideoUrl)));
            actor.AttemptsTo(Click.On(SearchResultsPage.VideoItemWithUrl(VideoUrl)));
            actor.WaitsUntil(Appearance.Of(SearchResultsPage.VideoItem), IsEqualTo.False());
        }
    }
}
