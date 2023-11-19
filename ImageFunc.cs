using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ChanceryStore
{
    public class ImageFunc
    {
        /// <summary>
        /// ковертирование из byte[]  в Bitmap
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        static public BitmapImage ConvertByteToBitmap(byte[] image)
        {
            MemoryStream byteStream = new MemoryStream(image);
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = byteStream;
            bitmapImage.EndInit();
            return bitmapImage;
        }

        /// <summary>
        /// ковертирование из imageSource в byte[] 
        /// </summary>
        /// <param name="imgPath"></param>
        /// <returns></returns>
        static public byte[] ConvertStringToByte(string imgPath)
        {
            byte[] imgData = null; // изображение в байтах  
            // открыть из закрыть поток чтения файла
            using (FileStream fs = new FileStream(imgPath, FileMode.Open))
            {
                imgData = new byte[fs.Length];
                fs.Read(imgData, 0, (int)fs.Length);
            }
            return imgData;
        }

        static public BitmapImage ConvertStringToBitmap(string imgPath)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imgPath);
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            return bitmap;
        }

    }
}
