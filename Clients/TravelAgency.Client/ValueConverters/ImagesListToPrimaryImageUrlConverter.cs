﻿using System.Globalization;
using TravelAgency.Shared.Dto;

namespace TravelAgency.Client.ValueConverters
{
    public class ImagesListToPrimaryImageUrlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ICollection<EntityImageDto> collection)
            {
                var primaryObject = collection
                    .FirstOrDefault(item => item.IsPrimary == true);

                return primaryObject != null ? $"{ApiConfig.ApiUrl}/api/Images/{primaryObject.ImageId}" : string.Empty;
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
