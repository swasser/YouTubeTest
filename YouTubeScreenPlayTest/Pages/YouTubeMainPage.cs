using Boa.Constrictor.Selenium;
using OpenQA.Selenium;

namespace YouTubeScreenPlayTest.Pages
{
    internal class YouTubeMainPage
    {
        public const string Url = "https://www.youtube.com";

        public static IWebLocator SearchInput => WebLocator.L(
            "YouTube Search Input",
            By.CssSelector("input[aria-label='Search']"));

    }
}
