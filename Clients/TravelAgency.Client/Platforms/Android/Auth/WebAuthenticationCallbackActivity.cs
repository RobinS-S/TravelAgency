using AndroidLibrary = Android;
using Android.App;
using Android.Content.PM;

namespace TravelAgency.Client.Platforms.Android.Auth
{
    [Activity(NoHistory = true, LaunchMode = LaunchMode.SingleTop, Exported = true)]
    [IntentFilter(new[] { AndroidLibrary.Content.Intent.ActionView },
        Categories = new[] { AndroidLibrary.Content.Intent.CategoryDefault, AndroidLibrary.Content.Intent.CategoryBrowsable, AndroidLibrary.Content.Intent.ActionView },
        DataScheme = CallbackScheme)]
    public class WebAuthenticationCallbackActivity : WebAuthenticatorCallbackActivity
    {
        private const string CallbackScheme = "travelagency";
    }
}
