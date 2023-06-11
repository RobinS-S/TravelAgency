using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using TravelAgency.Client.Repositories;
using TravelAgency.Client.Resources.Localization;
using TravelAgency.Shared.Dto;

namespace TravelAgency.Client.Pages.Account.Detail
{
    [QueryProperty("Id", "id")]
    public partial class ProfileDetailPageViewModel : ObservableObject
    {
        private readonly AccountRepository _accountRepository;

        [ObservableProperty]
        private ProfileDto? _profile;

        [ObservableProperty]
        private string _id = null!;

        [ObservableProperty]
        private bool _isRefreshing;

        [ObservableProperty]
        private bool _errorStateEnabled;

        public ProfileDetailPageViewModel(AccountRepository accountRepository)
        {
            this._accountRepository = accountRepository;
        }

        [RelayCommand]
        private async Task LoadData()
        {
            IsRefreshing = true;
            var profile = await _accountRepository.GetByIdAsync(Id);
            if (profile != null)
            {
                Profile = profile;
            }
            ErrorStateEnabled = profile == null;
            IsRefreshing = false;
        }

        async partial void OnIdChanged(string value)
        {
            await LoadData();
        }

        [RelayCommand]
        private async Task Call()
        {
            if (PhoneDialer.Default.IsSupported && !string.IsNullOrEmpty(Profile!.PhoneNumber))
            {
                PhoneDialer.Default.Open($"+{Profile.PhoneNumber}");
            }
            else
            {
                var toast = Toast.Make(Translations.CallingNotSupported, ToastDuration.Long, 15);
                await toast.Show();
            }
        }

        [RelayCommand]
        private async Task SendEmail()
        {
            if (Email.Default.IsComposeSupported)
            {
                var message = new EmailMessage
                {
                    To = new List<string> { Profile!.Email }
                };

                await Email.Default.ComposeAsync(message);
            }
            else
            {
                var toast = Toast.Make(Translations.EmailNotSupported, ToastDuration.Long, 15);
                await toast.Show();
            }
        }

        [RelayCommand]
        private async Task SendWhatsApp()
        {
            var url = $"whatsapp://send?phone=+{Profile!.PhoneNumber}";
            var supportsUri = await Launcher.Default.CanOpenAsync(url);

            if (supportsUri)
            {
                await Launcher.Default.OpenAsync(url);
            }
            else
            {
                var toast = Toast.Make(Translations.WhatsAppNotInstalled, ToastDuration.Long, 15);
                await toast.Show();
            }
        }
    }
}