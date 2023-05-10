using TravelAgency.Shared.Dto;

namespace TravelAgency.Client.Pages.Countries
{
    public partial class CountriesPage : ContentPage
    {
        private readonly CountriesPageViewModel _viewModel;

        public CountriesPage()
        {
            _viewModel = ServiceProviderHelper.GetService<CountriesPageViewModel>()!;
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

        private async void OnItemTapped(object sender, EventArgs e)
        {
            var frame = (Frame)sender;
            var item = (CountryDto)frame.BindingContext;
            await _viewModel.ViewDetails(item.Id);
        }
    }
}