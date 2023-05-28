 using TravelAgency.Shared.Dto;

namespace TravelAgency.Client.Pages.Locations
{
    public partial class LocationsPage : ContentPage
    {
        private readonly LocationsPageViewModel _viewModel;

        public LocationsPage(LocationsPageViewModel viewModel)
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