using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace P3DColourKey
{
	public partial class P3D
    {
        public int uncompressedLength = 0;
        public string originalHash;

        public Chunk Root = new Chunk();
        public  struct Chunk //Structure to hold basic universal chunk data
        {
            public byte[] ChunkType;
            public int ChunkSize;
            public int FullChunkSize;
            public byte[] Data;
            public List<Chunk> subChunks;
        }

        public static byte[][] chunkOrder = new byte[5][]
        {
            new byte[4] { 0, 144, 1, 0},    //TEXTURE_CHUNK 13
            new byte[4] { 1, 144, 1, 0},    //IMAGE CHUNK
            new byte[4] { 2, 144, 1, 0}, 
            new byte[4] { 5, 144, 1, 0 },   //SPRITE_CHUNK 11
            new byte[4] { 1, 16, 0, 3 }     //INSTPARTICLESYSTEM_CHUNK 56
     
        };

        const int TEXTURE_CHUNK = 0;
        const int IMAGE_CHUNK = 1;
        const int IMAGEDATA_CHUNK = 2;
        const int SPRITE_CHUNK = 3;
        const int INSTPARTICLESYSTEM_CHUNK = 4;


        //Takes a byte array of 12 bytes representing a chunk header and returns ChunkType, The size of data inside the chunk and The entire size of the chunk itself and its child chunks 
        private (byte[], int, int) ReadChunkHeader(byte[] header)
        {
            byte[] ChunkType = new byte[4];
            Array.Copy(header, 0, ChunkType, 0, 4);
            return (ChunkType, Utility.Byte4ToInt(header, 4), Utility.Byte4ToInt(header, 8));
        }

        //Will take a block from a compressed p3d and decompress it
        private byte[] DecompressBlock(byte[] source, int sourceIndex, byte[] destination, int destinationIndex, int destinationLength)
        {
            int written = 0;
            while (written < destinationLength)
            {
                int unknown = source[sourceIndex++];
                if (unknown <= 15)
                {
                    if (unknown == 0)
                    {
                        if (source[sourceIndex] == 0)
                        {
                            int unknown2 = 0;
                            do
                            {
                                unknown2 = source[++sourceIndex];
                                unknown += 255;
                            } while (unknown2 == 0);
                        }
                        unknown += source[sourceIndex++];
                        Array.Copy(source, sourceIndex, destination, destinationIndex, 15);
                        destinationIndex += 15;
                        sourceIndex += 15;
                        written += 15;
                    }
                    do
                    {
                        destination[destinationIndex++] = source[sourceIndex++];
                        ++written;
                        --unknown;
                    } while (unknown > 0);
                }
                else
                {
                    int unknown2 = unknown % 16;
                    if (unknown2 == 0)
                    {
                        int unknown3 = 15;
                        if (source[sourceIndex] == 0)
                        {
                            int unknown4;
                            do
                            {
                                unknown4 = source[++sourceIndex];
                                unknown3 += 255;
                            } while (unknown4 == 0);
                        }
                        unknown2 += source[sourceIndex++] + unknown3;
                    }
                    int unknown5 = Convert.ToInt32(Math.Floor(Convert.ToDecimal(unknown2 / 4)));
                    int unknown6 = destinationIndex - (Convert.ToInt32(Math.Floor(Convert.ToDecimal(unknown / 16))) | 16 * source[sourceIndex++]);
                    do
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            destination[destinationIndex++] = destination[unknown6++];
                        }
                        --unknown5;
                    } while (unknown5 > 0);
                    int unknown7 = unknown2 % 4;
                    while (unknown7 > 0)
                    {
                        destination[destinationIndex++] = destination[unknown6++];
                        --unknown7;
                    }
                    written += unknown2;
                }
            }
            return destination;
        }

        //Will take a compressed P3D and decompress it
        private byte[] DecompressP3D(FileStream Reader)
        {
            byte[] magic = Utility.ReadByte4(Reader);
            byte[] decompressedData;
            if (magic[3] == 90) //If magic word ends in "Z" then it is compressed and need decompressing
            {
                uncompressedLength = Utility.ReadUint32(Reader);
                decompressedData = new byte[uncompressedLength];
                int decompressedLength = 0;
                Reader.Position = 8;
                while (decompressedLength < uncompressedLength)
                {
                    int compressedLength = Utility.ReadUint32(Reader);
                    int uncompressedBlockLength = Utility.ReadUint32(Reader);
                    byte[] Data = new byte[compressedLength];
                    Reader.Read(Data, 0, compressedLength);
                    decompressedData = DecompressBlock(Data, 0, decompressedData, decompressedLength, uncompressedBlockLength);
                    decompressedLength += uncompressedBlockLength;
                }
            }
            else
            {
                Reader.Position = 8;
                uncompressedLength = Utility.ReadUint32(Reader);
                decompressedData = new byte[uncompressedLength];
                Reader.Position = 0;
                Reader.Read(decompressedData, 0, uncompressedLength);

            }
            return decompressedData;
        }
        
        //Will take a chunk with data to update and recalculate header
        private Chunk RecalculateChunk(Chunk currentChunk, byte[] newData)
		{
            int fullChunkSize = 0;
            foreach (Chunk subChunk in currentChunk.subChunks)
			{
                fullChunkSize += subChunk.FullChunkSize;
			}
            currentChunk.ChunkSize = newData.Length+12;
            currentChunk.Data = newData;
            currentChunk.FullChunkSize = fullChunkSize + currentChunk.ChunkSize;
            return currentChunk;
		}

        //Will return a Chunk from a byte array of data from index
        private Chunk GetChunk ( byte[]data, int offset)
		{
            Chunk chunk = new Chunk();
            chunk.subChunks = new List<Chunk>();
            byte[] header = new byte[12];
            Array.Copy(data, offset, header, 0, 12);
            var headerInfo = ReadChunkHeader(header);
            chunk.ChunkType = headerInfo.Item1;
            chunk.ChunkSize = headerInfo.Item2;
            chunk.FullChunkSize = headerInfo.Item3;
            chunk.Data = new byte[chunk.ChunkSize-12];
            offset += 12;
            Array.Copy(data, offset, chunk.Data, 0, chunk.ChunkSize-12);
            offset += chunk.ChunkSize-12;
            int currentSize = chunk.ChunkSize;
            while (currentSize < chunk.FullChunkSize)
			{
                var subHeader = ReadChunkHeader(Utility.GetRange(data, offset, 12));
                Chunk subChunk = GetChunk(data, offset);
                offset += subHeader.Item3;
                currentSize += subHeader.Item3;
                chunk.subChunks.Add(subChunk);
            }
      
            return chunk;
        }

        //Will read a P3D and store its Chunks
        public void ReadP3D(string path)
        {
            FileStream Reader;
            Reader = File.OpenRead(path);
            byte[] uncompressedData = DecompressP3D(Reader);
            originalHash = Hash.GetHashSHA1(uncompressedData);
            Reader.Close();
            Root = GetChunk(uncompressedData, 0);
        }

        //Will take a Chunk structure and convert it to bytes so it can be written back to p3d format
        private static byte[] PackChunk(Chunk chunk)
        {
            byte[] fullChunk = new byte[chunk.FullChunkSize];
            int outputIndex = 0;
            Array.Copy(chunk.ChunkType, 0, fullChunk, outputIndex, 4); outputIndex += 4;
            Array.Copy(BitConverter.GetBytes(chunk.ChunkSize), 0, fullChunk, outputIndex, 4); outputIndex += 4;
            Array.Copy(BitConverter.GetBytes(chunk.FullChunkSize), 0, fullChunk, outputIndex, 4); outputIndex += 4;
            Array.Copy(chunk.Data, 0, fullChunk, outputIndex, chunk.ChunkSize-12);
            outputIndex = chunk.ChunkSize;
            foreach (Chunk subChunk in chunk.subChunks)
			{
                Array.Copy(PackChunk(subChunk), 0, fullChunk, outputIndex, subChunk.FullChunkSize);
                outputIndex += subChunk.FullChunkSize;
            }
            return fullChunk;
        }

        //Will write chunks back into a P3D
        public void WriteP3D(string path)
        {
            FileInfo fi = new FileInfo(path);
            DialogResult dr = DialogResult.Retry;
            while (fi.IsReadOnly)
            {
                dr = MessageBox.Show(string.Format("The following file is read only\n\"{0}\"\n\nPlease make it writeable and then retry. Cancelling will make no changes to file.", path), "", MessageBoxButtons.RetryCancel);
                if (dr == DialogResult.Retry)
                {
                    fi = new FileInfo(path);
                }
                else if (dr == DialogResult.Cancel)
                {
                    return;
                }
            }

            if (Root.subChunks.Count == 0)
			{
                return;
			}

            byte[] output = new byte[uncompressedLength];
            Root.FullChunkSize = uncompressedLength;
            Array.Copy(PackChunk(Root), 0, output, 0, uncompressedLength);

            if (originalHash != Hash.GetHashSHA1(output)) 
            {
                BinaryWriter outputWriter = new BinaryWriter(File.Open(path, FileMode.Create, FileAccess.Write));
                outputWriter.Write(output);
                outputWriter.Close();
            }
        }
    }
}