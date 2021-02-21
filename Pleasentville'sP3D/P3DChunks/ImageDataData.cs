using System;
using System.Drawing;
using System.IO;
namespace PleasentvillesP3D
{
	public partial class P3D
	{

        public static byte[] GetImage(P3D.Chunk image)
        {
            int offset = 0;
            int imageSize = Utility.Byte4ToInt(Utility.GetRange(image.Data, offset, 4), 0);
            offset += 4;
            byte[] imageB = Utility.GetRange(image.Data, offset, imageSize);
            return imageB;
        }

        public static byte[] GenerateImageDataData(Bitmap image)
        {
            MemoryStream imageStream = new MemoryStream();
            image.Save(imageStream, System.Drawing.Imaging.ImageFormat.Png);
            byte[] imageBytes = imageStream.ToArray();
            int imageSize = imageBytes.Length;
            byte[] imageChunk = new byte[imageSize + 4];
            Array.Copy(BitConverter.GetBytes(imageSize), 0, imageChunk, 0, 4);
            Array.Copy(imageBytes, 0, imageChunk, 4, imageSize);
            return imageChunk;
        }
    }
}
