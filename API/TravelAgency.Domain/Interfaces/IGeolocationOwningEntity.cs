﻿namespace TravelAgency.Domain.Interfaces
{
    public interface IGeolocationOwningEntity
    {
        public GeoCoordinates Coordinates { get; set; }
    }
}
