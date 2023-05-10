namespace TravelAgency.Client.Pages.Main
{
    public partial class MainPage : ContentPage
    {
        private readonly MainPageViewModel _viewModel;

        public MainPage()
        {
            _viewModel = ServiceProviderHelper.GetService<MainPageViewModel>()!;
            BindingContext = _viewModel;

            InitializeComponent();
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}