namespace TravelAgency.Client.Services
{
    public class GeolocationService
    {
        private IGeolocation _geolocation;
        public Location? Location = new();

        public GeolocationService(IGeolocation geolocation)
        {
            _geolocation = geolocation;
        }

        public async Task GetLocation()
        {
            try
            {
                Location = await _geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Best,
                    Timeout = TimeSpan.FromSeconds(30)
                });
            }
            catch (Exception)
            {
            }
        }
    }
}
