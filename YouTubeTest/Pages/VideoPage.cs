using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace YouTubeTest.Pages
{
    internal class VideoPage : BasePage
    {
        By adPlayerOverlayBy = By.ClassName("ytp-ad-player-overlay");
        By artistNameBy = By.XPath("//*[@id='info-rows']//*[@id='title'][contains(text(),'ARTIST')]/..//a[1]");
        By artistNameEngagmentViewBy = By.CssSelector("#primary ytd-watch-metadata .yt-video-attribute-view-model__subtitle");
        By commentsBy = By.CssSelector("#comments");
        By noThanksButtonBy = By.CssSelector("button[aria-label='No thanks']");
        By promoOverlayBy = By.Id("main");
        By showMoreBy = By.CssSelector("#expand.button");
        By skipAdBy = By.XPath("//*[@class='ytp-ad-text ytp-ad-skip-button-text'][contains(text(),'Skip Ad')]");
        By videoDescriptionEngagementViewBy = By.CssSelector("#primary ytd-watch-metadata [card-list-style='HORIZONTAL_CARD_LIST_STYLE_TYPE_ENGAGEMENT_PANEL_SECTION']");

        public VideoPage(IWebDriver driver, ILogger<BasePage> logger) : base(driver, logger) { }

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
            var waitForCommentsToLoad = ShortWait.Until(ExpectedConditions.ElementIsVisible(commentsBy));

            ClosePromo();

            var expand = ShortWait.Until(ExpectedConditions.ElementIsVisible(showMoreBy));

            expand.Click();

            IWebElement artist = null!;

            try
            {
                artist = ShortWait.Until(ExpectedConditions.ElementIsVisible(artistNameBy));
            }
            catch (WebDriverTimeoutException)
            {
                ShortWait.Until(ExpectedConditions.ElementIsVisible(videoDescriptionEngagementViewBy));
                artist = ShortWait.Until(ExpectedConditions.ElementIsVisible(artistNameEngagmentViewBy));
            }

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
