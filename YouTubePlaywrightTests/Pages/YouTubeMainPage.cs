using Microsoft.Playwright;

namespace YouTubePlaywrightTests.Pages
{
    internal class YouTubeMainPage
    {
        private readonly IPage _page;

        private ILocator _searchLocator => _page.Locator("input[aria-label='Search']");
        private ILocator _searchLocatorCombobox => _page.GetByRole(AriaRole.Textbox, new() { Name = "search_query" });

        internal YouTubeMainPage(IPage page)
        {
            _page = page;
        }

        public async Task GotoAsync()
        {
            await _page.GotoAsync("http://www.youtube.com");
        }

        public async Task<SearchResultsPage> SearchAsync(string text)
        {
            //await _searchLocatorCombobox.WaitForAsync();
            //await _searchLocatorCombobox.HighlightAsync();
            //await _searchLocatorCombobox.FillAsync(text);
            //await _searchLocatorCombobox.PressAsync("Enter");

            await _searchLocator.ClickAsync();
            await _searchLocator.FillAsync(text);
            await _searchLocator.PressAsync("Enter");

            return new SearchResultsPage(_page);
        }
    }
}
