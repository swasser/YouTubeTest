using Microsoft.Playwright;

namespace YouTubePlaywrightTests.Pages
{
    internal class SearchResultsPage
    {
        private readonly IPage _page;

        public SearchResultsPage(IPage page)
        {
            _page = page;
        }

        public async Task FilterByVideo() 
        {
            await Filter();

            await _page.GetByRole(AriaRole.Link, new() { Name = "Video", Exact = true }).ClickAsync();
        }

        public async Task FilterByCount()
        {
            await Filter();

            await _page.GetByRole(AriaRole.Link, new() { Name = "View count" }).ClickAsync();
        }

        public async Task<VideoPage> SearchByVideoUrl(string url)
        {
            await _page.Locator($"#video-title[href*='{url}']").ClickAsync();

            return new VideoPage(_page);
        }

        private async Task Filter()
        {
            await _page.GetByLabel("Search filters").ClickAsync();
        }
    }
}
