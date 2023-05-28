namespace TravelAgency.Client.Pages.Residences
{
    public partial class ResidencesPage : ContentPage
    {
        private readonly ResidencesPageViewModel _viewModel;

        public ResidencesPage(ResidencesPageViewModel viewModel)
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