 using TravelAgency.Shared.Dto;

namespace TravelAgency.Client.Pages.Residences
{
    public partial class ResidencesPage : ContentPage
    {
        private readonly ResidencesPageViewModel _viewModel;

        public ResidencesPage(ResidencesPageViewModel viewModel)
        {
            _viewModel = viewModel;
            BindingContext = _viewModel;

            SizeChanged += CountriesPage_SizeChanged;

            InitializeComponent();
        }

        protected override async void OnParentSet()
        {
            base.OnAppearing();

            await _viewModel.LoadDataCommand.ExecuteAsync(null);
        }

        private void CountriesPage_SizeChanged(object? sender, EventArgs e)
        {
            bool isPortrait = Height > Width;
            if (isPortrait)
            {
                LocationCollection.ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Vertical);
            }
            else
            {
                LocationCollection.ItemsLayout = new GridItemsLayout(2, ItemsLayoutOrientation.Horizontal);
            }
        }

        private async void OnItemTapped(object sender, EventArgs e)
        {
            var frame = (Frame)sender;
            var item = (ResidenceDto)frame.BindingContext;
            if(item?.Id != null)
            {
                await _viewModel.ViewDetails(item.Id.Value);
            }
        }
    }
}