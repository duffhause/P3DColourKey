using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.ComponentModel;
using System.IO;
//using System.Reflection.Metadata;
using Pfim;
using Pfim.dds;
using System.Runtime.InteropServices;

namespace P3DColourKey
{
	public class Images
	{

        private static Bitmap CreateNonIndexedImage(Image src)
        {
            Bitmap newBmp = new Bitmap(src.Width, src.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            using (Graphics gfx = Graphics.FromImage(newBmp))
            {
                gfx.DrawImage(src, 0, 0);
            }
            return newBmp;
        }

        // Method to detrmine if 1 colour is close to another
        static bool ColorsAreClose(Color a, Color z, int threshhold) //thersh was 50, expertiment
        {
            // work out difference between 3 channels
            int r = (int)a.R - z.R,
                g = (int)a.G - z.G,
                b = (int)a.B - z.B;

            // return if the differneces are within the threshold
            // they are squared so they are positive
            return (r * r + g * g + b * b) < threshhold * threshhold;
        }

        // Method to apply filter to a Bitmap
        public static Bitmap ShindlerBitmap(Bitmap image, Color baseColour, int threshhold)
        {
            image = CreateNonIndexedImage(image);
            for (int x=0; x<image.Width; x++)
			{
                for ( int y = 0; y<image.Height; y++){
                    Color pixelColour = image.GetPixel(x, y);
                    if (!ColorsAreClose(pixelColour, baseColour, threshhold))
                    {
                        int a = pixelColour.A,
                        r = pixelColour.R,
                        g = pixelColour.G,
                        b = pixelColour.B;
                        //find average
                        int avg = (r + g + b) / 3;
                        //set new pixel value
                        image.SetPixel(x, y, Color.FromArgb(a, avg, avg, avg));
                    }
			    }
			}
            return image;
        }

        public static Bitmap BytesToBitmap (byte[] img, int fileFormat, int w, int h)
		{
            MemoryStream mStream = new MemoryStream(img);
            if (fileFormat == 10) //DXT5 Use Pfim for this
            {
                using (var image = Pfim.Pfim.FromStream(mStream))
                {
                    PixelFormat format = PixelFormat.Format32bppArgb;
                    var data = Marshal.UnsafeAddrOfPinnedArrayElement(image.Data, 0);
                    var bitmap = new Bitmap(w, h, image.Stride, format, data);
                    bitmap.RotateFlip(RotateFlipType.Rotate180FlipX); //For some reason Pfim will return the image flipped so we need to change it back
                    mStream.Close();
                    return bitmap;
                }
            }
            else if (fileFormat == 6 || fileFormat == 8) //DXT1 or DXT3
            {
                img = DDS.LoadDDS(img); //Decompress
                //LoadDDS will return raw argb, so we need to convert it into a Bitmap
                Bitmap createdBitmap = new Bitmap(w, h);
                int offset = 0;
                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        int r = img[offset++];
                        int g = img[offset++];
                        int b = img[offset++];
                        int a = img[offset++];
                        Color col = Color.FromArgb(a, r, g, b);
                        createdBitmap.SetPixel(x, y, col);
                    }
                }
                return createdBitmap;

            }
            else if (fileFormat == 1)
            {
                Bitmap bmp = (Bitmap)Bitmap.FromStream(mStream);
                return bmp;
            }
            else
            {
                return null;
            }
        }
    }
}
