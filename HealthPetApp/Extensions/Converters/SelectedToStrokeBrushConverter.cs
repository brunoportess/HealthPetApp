using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace HealthPetApp.Extensions.Converters
{
    public class SelectedToStrokeBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isSelected = (bool)value;

            return isSelected
                ? new SolidColorBrush(Color.FromArgb("#FF7F00")) // cor do stroke quando selecionado
                : new SolidColorBrush(Colors.Transparent);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
