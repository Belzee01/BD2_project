using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using static SQLAccess.Utils;

namespace SQLAccess
{
    /// <summary>
    /// Converts a full path to image to specific image type present in Images folder
    /// </summary>
    [ValueConversion(typeof(string), typeof(BitmapImage))]
    public class HeaderToImageConverter : IValueConverter
    {
        public static HeaderToImageConverter Instance = new HeaderToImageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dbType = (DB_TYPE)value;

            // Default value for image
            var image = "drive";

            // Check what image should be displayed
            if (dbType == DB_TYPE.DATABASE)
                image = "drive";
            if (dbType == DB_TYPE.TABLE)
                image = "folder-closed";

            return new BitmapImage(new Uri($"pack://application:,,,/Images/{image}.png"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
