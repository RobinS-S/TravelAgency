namespace TravelAgency.Client.Pages.Countries
{
    public partial class CountriesPage : ContentPage
    {
        private readonly CountriesPageViewModel _viewModel;

        public CountriesPage(CountriesPageViewModel viewModel)
        {

            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await _viewModel.LoadDataCommand.ExecuteAsync(null);
        }
    }
}