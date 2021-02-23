using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;

namespace P3DColourKey
{
	public partial class P3D
	{
        public struct ImageDataStruct //Structure to hold basic universal chunk data
        {
            public string Name;
            public int version;
            public int width;
            public int height;
            public int bitDepth;
            public int palaetised;
            public int hasAlpha;
            public int format;
        }

        public static ImageDataStruct GetImageData(P3D.Chunk imageDataChunk)
        {
            ImageDataStruct imageData = new ImageDataStruct();
            int offset = 0;
            int nameLength = imageDataChunk.Data[offset++];
            imageData.Name = System.Text.Encoding.UTF8.GetString(Utility.GetRange(imageDataChunk.Data, offset, nameLength));
            offset += nameLength;
            imageData.version = Utility.Byte4ToInt(imageDataChunk.Data, offset); offset += 4;
            imageData.width = Utility.Byte4ToInt(imageDataChunk.Data, offset); offset += 4;
            imageData.height = Utility.Byte4ToInt(imageDataChunk.Data, offset); offset += 4;
            imageData.bitDepth = Utility.Byte4ToInt(imageDataChunk.Data, offset); offset += 4;
            imageData.palaetised = Utility.Byte4ToInt(imageDataChunk.Data, offset); offset += 4;
            imageData.hasAlpha = Utility.Byte4ToInt(imageDataChunk.Data, offset); offset += 4;
            imageData.format = Utility.Byte4ToInt(imageDataChunk.Data, offset);
            return imageData;
        }

        public static byte[] GenerateImageData(ImageDataStruct imgDat)
        {
            int offset = 0;
            byte[] imageDataChunk = new byte[1 + imgDat.Name.Length + (7 * 4)];
            Array.Copy(BitConverter.GetBytes(imgDat.Name.Length), 0, imageDataChunk, offset++, 1);
            Array.Copy(Encoding.ASCII.GetBytes(imgDat.Name), 0, imageDataChunk, offset, imgDat.Name.Length); offset += imgDat.Name.Length;
            Array.Copy(BitConverter.GetBytes(imgDat.version), 0, imageDataChunk, offset, 4); offset += 4;
            Array.Copy(BitConverter.GetBytes(imgDat.width), 0, imageDataChunk, offset, 4); offset += 4;
            Array.Copy(BitConverter.GetBytes(imgDat.height), 0, imageDataChunk, offset, 4); offset += 4;
            Array.Copy(BitConverter.GetBytes(imgDat.bitDepth), 0, imageDataChunk, offset, 4); offset += 4;
            Array.Copy(BitConverter.GetBytes(imgDat.palaetised), 0, imageDataChunk, offset, 4); offset += 4;
            Array.Copy(BitConverter.GetBytes(imgDat.hasAlpha), 0, imageDataChunk, offset, 4); offset += 4;
            Array.Copy(BitConverter.GetBytes(imgDat.format), 0, imageDataChunk, offset, 4);
            return imageDataChunk;
        }
    }
}
