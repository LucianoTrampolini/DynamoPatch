using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Dynamo.Boekingssysteem.Controls
{
    public class BoolToFontWeightConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                if (((bool)value))
                    return FontWeights.Bold;
                return FontWeights.Normal;
            }

            return FontWeights.Normal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}