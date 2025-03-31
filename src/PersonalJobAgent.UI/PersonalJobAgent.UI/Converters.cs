using System;
using System.Windows;

namespace PersonalJobAgent.UI
{
    /// <summary>
    /// Converter that transforms null to Visibility.Collapsed and non-null to Visibility.Visible
    /// </summary>
    public class NullToVisibilityConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value == null ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Converter that compares a string value to a parameter
    /// </summary>
    public class StringEqualsConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null && parameter == null)
                return true;
            
            if (value == null || parameter == null)
                return false;
            
            return value.ToString() == parameter.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((bool)value)
                return parameter;
            
            return null;
        }
    }

    /// <summary>
    /// Converter that transforms application status to a brush color
    /// </summary>
    public class StatusToBrushConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Gray);
            
            string status = value.ToString();
            
            switch (status.ToLower())
            {
                case "applied":
                    return new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(33, 150, 243)); // Blue
                case "screening":
                    return new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(156, 39, 176)); // Purple
                case "interview":
                    return new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 152, 0)); // Orange
                case "technicaltest":
                case "technical test":
                    return new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 188, 212)); // Cyan
                case "offer":
                    return new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(76, 175, 80)); // Green
                case "rejected":
                    return new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(244, 67, 54)); // Red
                case "withdrawn":
                    return new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(158, 158, 158)); // Gray
                default:
                    return new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Gray);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
