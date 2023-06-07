﻿namespace TravelAgency.Infrastructure.Helpers
{
    public static class RandomExtensions
    {
        public static decimal NextDecimal(this Random random, decimal minValue, decimal maxValue)
        {
            decimal randomValue = (decimal)random.NextDouble() * (maxValue - minValue) + minValue;
            return randomValue;
        }
    }
}
