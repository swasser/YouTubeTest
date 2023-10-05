using NLog;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace YouTubeTest.Pages
{
    internal class VideoPage : BasePage
    {
        By adPlayerOverlayBy = By.ClassName("ytp-ad-player-overlay");
        By artistNameBy = By.XPath("//*[@id='info-rows']//*[@id='title'][contains(text(),'ARTIST')]/..//a[1]");
        By noThanksButtonBy = By.CssSelector("button[aria-label='No thanks']");
        By promoOverlayBy = By.Id("main");
        By showMoreBy = By.Id("expand");
        By skipAdBy = By.XPath("//*[@class='ytp-ad-text ytp-ad-skip-button-text'][contains(text(),'Skip Ad')]");

        public VideoPage(IWebDriver driver, ILogger logger) : base(driver, logger) { }

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
            ClosePromo();

            var expand = ShortWait.Until(ExpectedConditions.ElementToBeClickable(showMoreBy));

            expand.Click();

            var artist = ShortWait.Until(ExpectedConditions.ElementIsVisible(artistNameBy));

            return artist.Text;
        }

        private void ClosePromo()
        {
            try
            {
                if (ShortWait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(promoOverlayBy)).Count > 0)
                {
                    var noThanks = ShortWait.Until(ExpectedConditions.ElementIsVisible(noThanksButtonBy));
                    noThanks.Click();
                };
            }
            catch (Exception)
            {
                //Do nothing
            }
        }
    }
}
