using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetTopologySuite.Geometries;
using TravelAgency.Domain.Helpers;

namespace TravelAgency.Infrastructure.Helpers
{
    public static class EfCoreExtensions
    {
        public static PropertyBuilder<GeoCoordinates> UseGeoCoordinateConversion(
            this PropertyBuilder<GeoCoordinates> propertyBuilder)
        {
            return propertyBuilder.HasConversion(
                geoCoordinate => new Point(geoCoordinate.Longitude, geoCoordinate.Latitude) { SRID = 4326 },
                point => new GeoCoordinates(point.Y, point.X));
        }

        public static PropertyBuilder<GeoCoordinates?> UseGeoCoordinateConversionNullable(
            this PropertyBuilder<GeoCoordinates?> propertyBuilder)
        {
            return propertyBuilder.HasConversion(
                geoCoordinate => geoCoordinate == null ? null : new Point(geoCoordinate.Longitude, geoCoordinate.Latitude) { SRID = 4326 },
                point => point == null ? null : new GeoCoordinates(point.Y, point.X));
        }
    }
}
