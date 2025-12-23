using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace HealthPetApp.Extensions.Converters
{
    public class SelectedToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isSelected)
            {
                // Retorna uma cor de destaque se selecionado, senão uma cor neutra
                return isSelected ? Color.FromArgb("#F5B7B1") : Color.FromArgb("#FFFFFF");
            }
            return Color.FromArgb("#EEE");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
