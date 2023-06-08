namespace TravelAgency.Client.Pages.Countries
{
    public partial class CountriesPage : ContentPage
    {
        private readonly CountriesPageViewModel _viewModel;

        public CountriesPage(CountriesPageViewModel viewModel)
        {
            _viewModel = viewModel;
            BindingContext = _viewModel;

            InitializeComponent();
        }

        protected override async void OnParentSet()
        {
            base.OnAppearing();

            await _viewModel.LoadDataCommand.ExecuteAsync(null);
        }
    }
}