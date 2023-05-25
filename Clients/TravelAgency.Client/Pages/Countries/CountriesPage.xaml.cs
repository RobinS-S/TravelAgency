using TravelAgency.Shared.Dto;

namespace TravelAgency.Client.Pages.Countries
{
    public partial class CountriesPage : ContentPage
    {
        private readonly CountriesPageViewModel _viewModel;

        public CountriesPage(CountriesPageViewModel viewModel)
        {
            _viewModel = viewModel;
            BindingContext = _viewModel;

            SizeChanged += CountriesPage_SizeChanged;

            InitializeComponent();
        }

        private void CountriesPage_SizeChanged(object? sender, EventArgs e)
        {
            bool isPortrait = Height > Width;
            if (isPortrait)
            {
                CountryCollection.ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Vertical);
            }
            else
            {
                CountryCollection.ItemsLayout = new GridItemsLayout(2, ItemsLayoutOrientation.Horizontal);
            }
        }

        private async void OnItemTapped(object sender, EventArgs e)
        {
            var frame = (Frame)sender;
            var item = (CountryDto)frame.BindingContext;
            await _viewModel.ViewDetails(item.Id);
        }
    }
}