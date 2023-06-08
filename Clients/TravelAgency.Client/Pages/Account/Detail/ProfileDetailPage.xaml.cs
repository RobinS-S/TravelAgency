namespace TravelAgency.Client.Pages.Account.Detail
{
    public partial class ProfileDetailPage : ContentPage
    {
        private readonly ProfileDetailPageViewModel _viewModel;

        public ProfileDetailPage(ProfileDetailPageViewModel viewModel)
        {
            _viewModel = viewModel;
            BindingContext = _viewModel;

            InitializeComponent();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}