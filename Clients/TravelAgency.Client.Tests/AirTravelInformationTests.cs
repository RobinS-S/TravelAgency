using TravelAgency.Shared.Airports;

namespace TravelAgency.Client.Tests
{
    public class AirTravelInformationTests
    {
        [Fact]
        public void AirportsListContainsSchiphol()
        {
            // Arrange
            var expectedAirportName = "Schiphol";
            var inputCode = "ams";

            // Act
            var airport = AirTravelInformation.GetAirportByIATACode(inputCode);

            // Assert
            Assert.NotNull(airport);
            Assert.Equal(expectedAirportName, airport.Name);
        }
    }
}
