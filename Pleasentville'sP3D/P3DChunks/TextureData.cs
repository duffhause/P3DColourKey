using System;
using System.Collections.Generic;
using System.Text;

namespace PleasentvillesP3D
{
	public partial class P3D
	{
        public struct TextureDataStruct
        {
            public string Name;
            public int version;
            public int width;
            public int height;
            public int bitDepth;
            public int alphaDepth;
            public int mipMapCount;
            public int textureType;
            public int usage;
            public int priority;
        } //Structure to hold univeral texture data

        public static byte[] GenerateTextureDataData(TextureDataStruct texDat)
        {
            int offset = 0;
            byte[] textureDataChunk = new byte[1 + texDat.Name.Length + (9 * 4)];
            Array.Copy(BitConverter.GetBytes(texDat.Name.Length), 0, textureDataChunk, offset++, 1);
            Array.Copy(Encoding.ASCII.GetBytes(texDat.Name), 0, textureDataChunk, offset, texDat.Name.Length); offset += texDat.Name.Length;
            Array.Copy(BitConverter.GetBytes(texDat.version), 0, textureDataChunk, offset, 4); offset += 4;
            Array.Copy(BitConverter.GetBytes(texDat.width), 0, textureDataChunk, offset, 4); offset += 4;
            Array.Copy(BitConverter.GetBytes(texDat.height), 0, textureDataChunk, offset, 4); offset += 4;
            Array.Copy(BitConverter.GetBytes(texDat.bitDepth), 0, textureDataChunk, offset, 4); offset += 4;
            Array.Copy(BitConverter.GetBytes(texDat.alphaDepth), 0, textureDataChunk, offset, 4); offset += 4;
            Array.Copy(BitConverter.GetBytes(texDat.mipMapCount), 0, textureDataChunk, offset, 4); offset += 4;
            Array.Copy(BitConverter.GetBytes(texDat.textureType), 0, textureDataChunk, offset, 4); offset += 4;
            Array.Copy(BitConverter.GetBytes(texDat.usage), 0, textureDataChunk, offset, 4); offset += 4;
            Array.Copy(BitConverter.GetBytes(texDat.priority), 0, textureDataChunk, offset, 4);
            return textureDataChunk;
        }

        public static TextureDataStruct GetTextureData(P3D.Chunk texture)
        {
            TextureDataStruct textureData = new TextureDataStruct();
            int offset = 0;
            int nameLength = texture.Data[offset++];
            textureData.Name = System.Text.Encoding.UTF8.GetString(Utility.GetRange(texture.Data, offset, nameLength));
            offset += nameLength;
            textureData.version = Utility.Byte4ToInt(texture.Data, offset); offset += 4;
            textureData.width = Utility.Byte4ToInt(texture.Data, offset); offset += 4;
            textureData.height = Utility.Byte4ToInt(texture.Data, offset); offset += 4;
            textureData.bitDepth = Utility.Byte4ToInt(texture.Data, offset); offset += 4;
            textureData.alphaDepth = Utility.Byte4ToInt(texture.Data, offset); offset += 4;
            textureData.mipMapCount = Utility.Byte4ToInt(texture.Data, offset); offset += 4;
            textureData.textureType = Utility.Byte4ToInt(texture.Data, offset); offset += 4;
            textureData.usage = Utility.Byte4ToInt(texture.Data, offset); offset += 4;
            textureData.priority = Utility.Byte4ToInt(texture.Data, offset);
            return textureData;
        }

    }
}
