using Mapsui;
using Mapsui.Projections;
using Mapsui.UI.Maui;

namespace TravelAgency.Client.Controls
{
    // TODO: find a fix for https://github.com/dotnet/maui/issues/13628
    public class CrossPlatformMapControl : MapView
    {
        private MapControl _mapControl = new();

        public CrossPlatformMapControl() : base()
        {
            _mapControl.Map?.Layers.Add(Mapsui.Tiling.OpenStreetMap.CreateTileLayer());
            Map = _mapControl.Map!;
            this.PinClicked += CrossPlatformMapControl_PinClicked;
        }

        private void CrossPlatformMapControl_PinClicked(object? sender, PinClickedEventArgs e)
        {
            e.Handled = true;
            var googleMapsUrl =
                $"https://www.google.com/maps/search/?api=1&query={e.Pin.Position.Latitude.ToString(System.Globalization.CultureInfo.InvariantCulture)},{e.Pin.Position.Longitude.ToString(System.Globalization.CultureInfo.InvariantCulture)}";

            Task.Run(async () =>
            {
                try
                {
                    if (await Launcher.CanOpenAsync(googleMapsUrl))
                    {
                        await Launcher.OpenAsync(googleMapsUrl);
                    }
                }
                catch (Exception)
                {
                    // ignored
                }
            });
        }

        public Pin AddPin(double latitude, double longitude, Color c, float scale = 0.7F, string label = "Pin", string address = "", bool focus = true, int resolution = 6)
        {
            var myPin = new Pin(this)
            {
                Position = new Position(latitude, longitude),
                Type = PinType.Pin,
                Label = label,
                Address = address,
                Scale = scale,
                Color = c
            };
            Pins.Add(myPin);

            if (focus)
            {
                var (x, y) = SphericalMercator.FromLonLat(longitude, latitude);
                Map.Home = (nav) => nav.CenterOnAndZoomTo(new MPoint(x, y), nav.Resolutions[resolution]);
            }

            return myPin;
        }
    }
}

