using IdentityModel.OidcClient.Browser;

namespace TravelAgency.Client.Platforms.Windows.Auth
{
    public class WebViewBrowserAuthenticatorBrowser : IdentityModel.OidcClient.Browser.IBrowser
    {
        private readonly WebView _webView;

        public WebViewBrowserAuthenticatorBrowser(WebView webView)
        {
            _webView = webView;
        }

        public async Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
        {
            var tcs = new TaskCompletionSource<BrowserResult>();

            _webView.Navigating += (sender, e) =>
            {
                if (e.Url.StartsWith(options.EndUrl))
                {
                    _webView.WidthRequest = 0;
                    _webView.HeightRequest = 0;
                    if (tcs.Task.Status != TaskStatus.RanToCompletion)
                    {
                        tcs.SetResult(new BrowserResult
                        {
                            ResultType = BrowserResultType.Success,
                            Response = e.Url.ToString()
                        });
                    }
                }
            };

            _webView.WidthRequest = 800;
            _webView.HeightRequest = 800;
            _webView.Source = new UrlWebViewSource { Url = options.StartUrl };

            return await tcs.Task;
        }
    }
}
