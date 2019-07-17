using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FileStasher.Utilities
{
    public static class ImageUtils
    {
        /// <summary>
        /// Converts the bitmap to a bitmapimage
        /// </summary>
        /// <param name="bitmap">The bitmap</param>
        /// <returns>The original image in bitmapimage format</returns>
        public static BitmapImage BitmapToBitmapImage(Bitmap bitmap)
        {
            if (bitmap == null)
            {
                return null;
            }

            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    ms.Position = 0;
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = ms;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();

                    return bitmapImage;
                }

            }
            catch (Exception)
            {
                // TODO log maybe?
                //throw;
            }

            return null;
        }
    }
}
