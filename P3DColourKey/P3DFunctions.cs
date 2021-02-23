using System.Drawing;
using System.Linq;

namespace P3DColourKey
{
	public partial class P3D
    {
        //Will take a List of chunks and edit images in Texture chunks
        public Chunk ShindlerTextures(Chunk currentChunks, Color baseColour, int threshhold)
        {
            (ImageDataStruct, Chunk, Bitmap) ProcessImageChunk(Chunk subChunk)
            {
                ImageDataStruct imageData = GetImageData(subChunk);
                Chunk image = subChunk.subChunks[0];

                if (!image.ChunkType.SequenceEqual(chunkOrder[IMAGEDATA_CHUNK]))
                {
                    //TODO
                }

                Bitmap imageBitmap = Images.BytesToBitmap(GetImage(image), imageData.format, imageData.width, imageData.height);
                return (imageData, image, imageBitmap);
            }

            for (int i = 0; i < currentChunks.subChunks.Count; i++)
            {
                Chunk chunk = currentChunks.subChunks[i];
                if (chunk.ChunkType.SequenceEqual(chunkOrder[TEXTURE_CHUNK]))
                {
                    TextureDataStruct textureData = GetTextureData(chunk);
                    for (int j = 0; j < chunk.subChunks.Count; j++)
                    {
                        Chunk subChunk = chunk.subChunks[j];
                        if (subChunk.ChunkType.SequenceEqual(chunkOrder[IMAGE_CHUNK]))
                        {
                            var imageChunk = ProcessImageChunk(subChunk);
                            ImageDataStruct imageData = imageChunk.Item1;
                            Chunk image = imageChunk.Item2;
                            Bitmap imageBitmap = imageChunk.Item3;
                            imageBitmap = Images.ShindlerBitmap(imageBitmap, baseColour, threshhold);

                            //EditImageData
                            imageData.format = 1;
                            int bitDepth = Image.GetPixelFormatSize(imageBitmap.PixelFormat);
                            textureData.bitDepth = bitDepth;
                            imageData.bitDepth = bitDepth;

                            //Generate new chunk
                            byte[] imageDataData = GenerateImageDataData(imageBitmap);
                            subChunk.subChunks[0] = RecalculateChunk(subChunk.subChunks[0], imageDataData);

                            byte[] imageDataBytes = GenerateImageData(imageData);
                            chunk.subChunks[j] = RecalculateChunk(chunk.subChunks[j], imageDataBytes);
                        }
                    }
                    byte[] textureDataBytes = GenerateTextureDataData(textureData);
                    uncompressedLength -= currentChunks.subChunks[i].FullChunkSize;
                    currentChunks.subChunks[i] = RecalculateChunk(currentChunks.subChunks[i], textureDataBytes);
                    uncompressedLength += currentChunks.subChunks[i].FullChunkSize;
                }
                else if (chunk.ChunkType.SequenceEqual(chunkOrder[SPRITE_CHUNK]))
                {
                    for (int j = 0; j < chunk.subChunks.Count; j++)
                    {
                        Chunk subChunk = chunk.subChunks[j];
                        if (subChunk.ChunkType.SequenceEqual(chunkOrder[IMAGE_CHUNK]))
                        {
                            var imageChunk = ProcessImageChunk(subChunk);
                            ImageDataStruct imageData = imageChunk.Item1;
                            Chunk image = imageChunk.Item2;
                            Bitmap imageBitmap = imageChunk.Item3;
                            imageBitmap = Images.ShindlerBitmap(imageBitmap, baseColour, threshhold);

                            //EditImageData
                            imageData.format = 1;
                            int bitDepth = Image.GetPixelFormatSize(imageBitmap.PixelFormat);
                            imageData.bitDepth = bitDepth;

                            //Generate new chunk
                            byte[] imageDataData = GenerateImageDataData(imageBitmap);
                            subChunk.subChunks[0] = RecalculateChunk(subChunk.subChunks[0], imageDataData);

                            byte[] imageDataBytes = GenerateImageData(imageData);
                            chunk.subChunks[j] = RecalculateChunk(chunk.subChunks[j], imageDataBytes);
                        }
                    }
                    uncompressedLength -= currentChunks.subChunks[i].FullChunkSize;
                    currentChunks.subChunks[i] = RecalculateChunk(currentChunks.subChunks[i], currentChunks.subChunks[i].Data);
                    uncompressedLength += currentChunks.subChunks[i].FullChunkSize;
                }
                else if (chunk.subChunks.Count > 0)
                {
                    chunk = ShindlerTextures(chunk, baseColour, threshhold);
                    chunk = RecalculateChunk(chunk, chunk.Data);
                    currentChunks.subChunks[i] = chunk;
                }
            }
            return currentChunks;
        }
    }
}