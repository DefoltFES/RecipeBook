using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace RecipeBook.service
{
    class PathToImageConvertor:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Uri fileUri;
            if (value == null)
            {
                return null;
            }

            if (value is Uri uri)
            {
                fileUri = uri;
            }
            if (Path.IsPathFullyQualified(value.ToString()))
            {
                 fileUri = new Uri(value.ToString(), UriKind.Absolute);
              
            }
            else
            {
                 fileUri = new Uri(System.AppDomain.CurrentDomain.BaseDirectory+value.ToString(), UriKind.Absolute);
                
            }
            if (!File.Exists(fileUri.OriginalString))
            {
                return null;
            }
            
            var b1 = new BitmapImage();
            b1.BeginInit();
            b1.UriSource = fileUri;
            b1.CacheOption = BitmapCacheOption.OnLoad;
            b1.EndInit();
            return b1;


        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new Uri(value.ToString()).LocalPath;
        }
    }
}
