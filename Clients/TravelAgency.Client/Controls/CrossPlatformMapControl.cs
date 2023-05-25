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
        }

        public Pin AddPin(double latitude, double longitude, Color c, float scale = 0.7F, string label = "Pin", string address = "", bool focus = true)
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
                Map.Home = (nav) => nav.CenterOnAndZoomTo(new MPoint(x, y), nav.Resolutions[6]);
            }

            return myPin;
        }
    }
}

