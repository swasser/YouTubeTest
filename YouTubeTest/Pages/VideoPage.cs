using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace YouTubeTest.Pages
{
    internal class VideoPage : BasePage
    {
        By adPlayerOverlayBy = By.ClassName("ytp-ad-player-overlay");
        By skipAdBy = By.XPath("//*[@class='ytp-ad-text ytp-ad-skip-button-text'][contains(text(),'Skip Ad')]");
        By showMoreBy = By.Id("expand");
        By artistNameBy = By.XPath("//*[@id='info-rows']//*[@id='title'][contains(text(),'ARTIST')]/..//a[1]");

        public VideoPage(IWebDriver driver) : base(driver) { }

        public VideoPage SkipAds()
        {
            try
            {
                if (ShortWait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(adPlayerOverlayBy)).Count > 0)
                {
                    var skip = LongWait.Until(ExpectedConditions.ElementIsVisible(skipAdBy));
                    skip.Click();
                };
            }
            catch (Exception)
            {
                //Do nothing
            }

            return this; 
        }

        public string GetArtistName()
        {
            var expand = ShortWait.Until(ExpectedConditions.ElementToBeClickable(showMoreBy));

            expand.Click();

            var artist = ShortWait.Until(ExpectedConditions.ElementIsVisible(artistNameBy));

            return artist.Text;
        }
    }
}
