using Mapsui.UI.Maui;

namespace TravelAgency.Client.Pages.Residences.Detail
{
    public partial class ResidenceDetailPage : ContentPage
    {
        private readonly ResidenceDetailPageViewModel _viewModel;
        private Pin? _residencePin;

        public ResidenceDetailPage(ResidenceDetailPageViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += ViewModel_PropertyChanged;
            BindingContext = _viewModel;

            InitializeComponent();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _viewModel.PropertyChanged -= ViewModel_PropertyChanged;
        }

        private void ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Residence" && _viewModel.Residence != null)
            {
                if (_residencePin != null)
                {
                    map.Pins.Remove(_residencePin);
                    _residencePin.Dispose();
                }

                _residencePin = map.AddPin(_viewModel.Residence.Coordinates.Latitude, _viewModel.Residence.Coordinates.Longitude, Colors.Red, 0.7F, "Residence", "", true, 19);
            }
        }
    }
}