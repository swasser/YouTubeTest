using Microsoft.Playwright;

namespace YouTubePlaywrightTests.Pages
{
    internal class VideoPage
    {
        private readonly IPage _page;
        private ILocator adCountDown => _page.Locator("[id=\"ad-image\\:5\"]");
        private ILocator skipAd => _page.GetByRole(AriaRole.Button, new() { Name = "Skip Ad" });
        private ILocator videoDescriptionTableView => _page.Locator("#structured-description #info-rows");
        private ILocator comments => _page.Locator("#comments");
        private ILocator moreButton => _page.GetByRole(AriaRole.Button, new() { Name = "...more" });
        private ILocator artistTitle => _page.Locator("#structured-description").GetByText("ARTIST");
        private ILocator artistName => _page.Locator("#structured-description").GetByText("ARTIST").Locator("+*").Locator("#default-metadata");
        private ILocator artistNameEngagmentView => _page.Locator("#primary ytd-watch-metadata .yt-video-attribute-view-model__subtitle");
        private ILocator videoDescriptionEngagementView => _page.Locator("#primary ytd-watch-metadata [card-list-style='HORIZONTAL_CARD_LIST_STYLE_TYPE_ENGAGEMENT_PANEL_SECTION']");

        public VideoPage(IPage page)
        {
            _page = page;
        }

        public void SkipAds()
        {
            if(adCountDown != null)
            {
                skipAd.ClickAsync();
            }
        }

        public async Task<string> GetArtistName()
        {
            comments.IsVisibleAsync().Wait();

            await moreButton.ClickAsync();

            try
            {
                await Assertions.Expect(videoDescriptionTableView).ToBeVisibleAsync();
                return await artistName.TextContentAsync();
            }
            catch (PlaywrightException)
            {
                await Assertions.Expect(videoDescriptionEngagementView).ToBeVisibleAsync();
                return await artistNameEngagmentView.TextContentAsync();
            }
        }
    }
}
