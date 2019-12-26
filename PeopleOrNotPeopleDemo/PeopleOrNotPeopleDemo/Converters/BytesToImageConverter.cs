using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace PeopleOrNotPeopleDemo.Converters
{
    public class BytesToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value == null)
            {
                return null;
            }

            var bytes = (byte[])value;
            var stream = new MemoryStream(bytes);

            return ImageSource.FromStream(() => stream);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        //public static ImageSource ByteToImage(byte[] imageData)
        //{
        //    BitMapImage biImg = new BitMapImage();
        //    MemoryStream ms = new MemoryStream(imageData);
        //    biImg.BeginInit();
        //    biImg.StreamSource = ms;
        //    biImg.EndInit();

        //    ImageSource imgSrc = biImg as ImageSource;

        //    return imgSrc;
        //}
    }
}
