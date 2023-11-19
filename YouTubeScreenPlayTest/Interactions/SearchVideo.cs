using Boa.Constrictor.Screenplay;
using Boa.Constrictor.Selenium;
using YouTubeScreenPlayTest.Pages;

namespace YouTubeScreenPlayTest.Interactions
{
    internal class SearchVideo : ITask
    {
        public string SearchPhrase { get; }
        
        private SearchVideo(string searchPhrase) => SearchPhrase = searchPhrase;

        public static SearchVideo For(string searchPhrase) => new SearchVideo(searchPhrase);

        public void PerformAs(IActor actor)
        {
            actor.AttemptsTo(SendKeys.To(YouTubeMainPage.SearchInput, SearchPhrase));
            actor.AttemptsTo(Submit.On(YouTubeMainPage.SearchInput));
        }
    }
}
