namespace CopyHtmlWebSite.MainApp.ValueConverter
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class InvertBooleanConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                bool.TryParse(value.ToString(), out bool result);
                return !result;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}