using Boa.Constrictor.Screenplay;
using Boa.Constrictor.Selenium;
using YouTubeScreenPlayTest.Pages;

namespace YouTubeScreenPlayTest.Interactions
{
    internal class ViewVideoDetails : IQuestion<string>
    {
        private ViewVideoDetails() { }

        public static ViewVideoDetails ArtistName() => new ViewVideoDetails();

        public string RequestAs(IActor actor)
        {
            string artistName;

            actor.WaitsUntil(Appearance.Of(VideoPage.comments), IsEqualTo.True(), timeout: 10);

            try
            {
                if (actor.WaitsUntil(Appearance.Of(VideoPage.adCountdown), IsEqualTo.True(), timeout: 2))
                {
                    actor.AttemptsTo(Click.On(VideoPage.skipAd));
                }
            }
            catch (WaitingException)
            {
                //Do nothing
            }

            actor.AttemptsTo(Click.On(VideoPage.moreButton));

            if(actor.WaitsUntil(Existence.Of(VideoPage.videoDescriptionEngagementView), IsEqualTo.True(), timeout: 2))
            {
                artistName = actor.AsksFor<string>(Text.Of(VideoPage.artistNameEngagmentView));
            }
            else
            {
                artistName = actor.AsksFor<string>(Text.Of(VideoPage.artistName));
            }

            return artistName;
        }


    }
}
