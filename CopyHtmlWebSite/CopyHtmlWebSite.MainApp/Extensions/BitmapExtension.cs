using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CopyHtmlWebSite.MainApp.Extensions
{
    public static class BitmapExtension
    {
        public static BitmapSource GetBitmapSource(this Bitmap image)
        {
            var rect = new Rectangle(0, 0, image.Width, image.Height);
            var bitmapData = image.LockBits(rect, ImageLockMode.ReadOnly, image.PixelFormat);

            try
            {
                BitmapPalette palette = null;

                if (image.Palette.Entries.Length > 0)
                {
                    var paletteColors = image.Palette.Entries.Select(entry => System.Windows.Media.Color.FromArgb(entry.A, entry.R, entry.G, entry.B)).ToList();
                    palette = new BitmapPalette(paletteColors);
                }

                return BitmapSource.Create(
                    image.Width,
                    image.Height,
                    image.HorizontalResolution,
                    image.VerticalResolution,
                    ConvertPixelFormat(image.PixelFormat),
                    palette,
                    bitmapData.Scan0,
                    bitmapData.Stride * image.Height,
                    bitmapData.Stride
                );
            }
            finally
            {
                image.UnlockBits(bitmapData);
            }
        }

        private static System.Windows.Media.PixelFormat ConvertPixelFormat(System.Drawing.Imaging.PixelFormat sourceFormat)
        {
            switch (sourceFormat)
            {
                case System.Drawing.Imaging.PixelFormat.Format24bppRgb:
                    return PixelFormats.Bgr24;

                case System.Drawing.Imaging.PixelFormat.Format32bppArgb:
                    return PixelFormats.Bgra32;

                case System.Drawing.Imaging.PixelFormat.Format32bppRgb:
                    return PixelFormats.Bgr32;
            }

            return new System.Windows.Media.PixelFormat();
        }
    }
}
