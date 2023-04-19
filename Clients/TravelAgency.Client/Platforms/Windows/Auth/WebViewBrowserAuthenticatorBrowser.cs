using IdentityModel.OidcClient.Browser;

namespace TravelAgency.Client.Platforms.Windows.Auth
{
    public class WebViewBrowserAuthenticatorBrowser : IdentityModel.OidcClient.Browser.IBrowser
    {
        private readonly WebView _webView;
        private TaskCompletionSource<BrowserResult> _tcs;
        private string _endUrl;

        public WebViewBrowserAuthenticatorBrowser(WebView webView)
        {
            _webView = webView;
        }

        public async Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
        {
            _tcs = new TaskCompletionSource<BrowserResult>();
            _endUrl = options.EndUrl;

            _webView.Navigating += WebViewOnNavigating;

            _webView.WidthRequest = 800;
            _webView.HeightRequest = 800;
            _webView.Source = new UrlWebViewSource { Url = options.StartUrl };

            return await _tcs.Task;
        }

        private void WebViewOnNavigating(object sender, WebNavigatingEventArgs e)
        {
            if (!e.Url.StartsWith(_endUrl)) return;

            _webView.WidthRequest = 0;
            _webView.HeightRequest = 0;
            if (_tcs.Task.Status != TaskStatus.RanToCompletion)
            {
                _tcs.SetResult(new BrowserResult
                {
                    ResultType = BrowserResultType.Success,
                    Response = e.Url
                });
            }
        }

        public void Unsubscribe()
        {
            _webView.Navigating -= WebViewOnNavigating;
        }
    }
}
