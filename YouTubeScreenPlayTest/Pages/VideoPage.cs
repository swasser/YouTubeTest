using Boa.Constrictor.Selenium;
using OpenQA.Selenium;

namespace YouTubeScreenPlayTest.Pages
{
    internal class VideoPage
    {
        public static IWebLocator adCountdown => WebLocator.L(
            "Countdown until it will be possible to skip an ad",
            By.CssSelector("[id=\"ad-image\\:5\"]")
            );

        public static IWebLocator skipAd => WebLocator.L(
            "Skip an ad",
            By.CssSelector("button.ytp-ad-skip-button-modern")
            );

        public static IWebLocator comments => WebLocator.L(
            "Viewers comments",
            By.CssSelector("#comments")
            );

        public static IWebLocator moreButton => WebLocator.L(
            "More details button",
            By.CssSelector("tp-yt-paper-button#expand")
            );

        public static IWebLocator artistName => WebLocator.L(
            "Artist name (table view)",
            By.XPath("//*[@id='info-rows']//*[@id='title'][contains(text(),'ARTIST')]/..//a[1]")
            );

        public static IWebLocator artistNameEngagmentView => WebLocator.L(
            "Artist name (engagment view)",
            By.CssSelector("#primary ytd-watch-metadata .yt-video-attribute-view-model__subtitle")
            );

        public static IWebLocator videoDescriptionEngagementView => WebLocator.L(
            "Artist name (engagment view)",
            By.CssSelector("#primary ytd-watch-metadata [card-list-style='HORIZONTAL_CARD_LIST_STYLE_TYPE_ENGAGEMENT_PANEL_SECTION']")
            );

        public static IWebLocator channelName => WebLocator.L(
            "Channel name",
            By.CssSelector("#owner ytd-channel-name#channel-name #text")
            );
        
    }
}
