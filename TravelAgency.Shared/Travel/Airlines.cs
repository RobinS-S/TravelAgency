namespace TravelAgency.Shared.Travel
{
    public class Airlines
    {
        public static string GenerateRandomFlightNumber(string? airlineName = null)
        {
            airlineName ??= AirlineNames[Random.Shared.Next(0, AirlineNames.Count - 1)];

            char airlineLetter = airlineName.ToUpper()[0];
            string randomNumbers = Random.Shared.Next(1000, 10000).ToString();

            return $"{airlineLetter}{randomNumbers}";
        }

        public static string GetUniqueCapitalLetters(string airlineName)
        {
            List<char> capitalLetters = new();

            foreach (char c in airlineName)
            {
                if (char.IsUpper(c))
                {
                    capitalLetters.Add(c);
                }
            }

            capitalLetters = capitalLetters.ToList();
            return new string(capitalLetters.ToArray());
        }

        public static List<string> AirlineNames = new()
        {
            "AeroSwift",
            "SkyLink",
            "FlyJet",
            "CloudAir",
            "AeroWing",
            "SkyExpress",
            "AirFly",
            "AeroNautic",
            "WingLine",
            "FlySky",
            "CloudWing",
            "AeroJet",
            "SkyAir",
            "AirWing",
            "FlyExpress",
            "WingAir",
            "AeroLink",
            "SkyFly",
            "AirJet",
            "WingExpress",
            "AeroExpress",
            "SkyWing",
            "FlyAir",
            "WingJet",
            "CloudFly",
            "AeroAir",
            "SkyNautic",
            "AirExpress",
            "FlyNautic",
            "WingSky",
            "AeroFly",
            "SkyExpress",
            "AirSky",
            "FlyCloud",
            "WingNautic",
            "AeroExpress",
            "SkyWing",
            "AirFly",
            "WingAir",
            "AeroLink",
            "SkyFly",
            "AirJet",
            "WingExpress",
            "AeroExpress",
            "SkyAir",
            "AirWing",
            "FlyExpress",
            "WingJet",
            "AeroAir",
            "SkyNautic",
            "FlyAir",
            "WingSky"
        };
    }
}
