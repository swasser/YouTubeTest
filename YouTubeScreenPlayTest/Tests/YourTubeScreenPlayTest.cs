using Boa.Constrictor.Screenplay;
using Boa.Constrictor.Selenium;
using FluentAssertions;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium.Chrome;
using YouTubeScreenPlayTest.Helpers;
using YouTubeScreenPlayTest.Interactions;
using YouTubeScreenPlayTest.Pages;

namespace YouTubeScreenPlayTest.Tests
{
    internal class YourTubeScreenPlayTest
    {
        IActor actor { get; set; }

        [SetUp]
        public void Setup()
        {
            actor = new Actor(name: "Bob", logger: new ConsoleLogger());

            actor.Can(BrowseTheWeb.With(new ChromeDriver()));

            actor.AttemptsTo(MaximizeWindow.ForBrowser());
        }
        
        [Test]
        public void YouTubeSearch()
        {
            var videoUrl = "watch?v=ybXrrTX3LuI";
            var songName = "I Will Survive - Alien song";

            actor.AttemptsTo(Navigate.ToUrl(YouTubeMainPage.Url));
            actor.AttemptsTo(SearchVideo.For(songName));
            actor.AttemptsTo(FilterVideos.By(Filter.Type_Video));
            actor.AttemptsTo(FilterVideos.By(Filter.SortBy_Count));
            actor.AskingFor(GetChannelName.For(videoUrl)).Should().Be("nikki7993");
            actor.AttemptsTo(SelectVideo.ForUrl(videoUrl));
            actor.AskingFor(ViewVideoDetails.ArtistName()).Should().Be("Gloria Gaynor");
            actor.AskingFor(Text.Of(VideoPage.channelName)).Should().Be("nikki7993");
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Equals(TestStatus.Failed))
            {
                //Placeholder to take screenshot
            }

            actor.AttemptsTo(QuitWebDriver.ForBrowser()); 
        }
    }
}
