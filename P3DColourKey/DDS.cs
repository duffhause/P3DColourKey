using System;
using System.Collections.Generic;
using System.IO;
//using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace P3DColourKey
{
	class DDS
	{

		public static int[] DDSPixelFormatFlags = new int[6]
		{
			0x1, 0x2, 0x4, 0x40, 0x200, 0x20000
		};
		public static int DDPF_FOURCC = DDSPixelFormatFlags[2];

		public static string ReadFourCC (byte[] Source, int offset)
		{
			byte[] bytes = Utility.GetRange(Source, offset, 4);
			string str = System.Text.Encoding.UTF8.GetString(bytes);
			return str;

		}

		public static byte[] LoadDXT (byte[] Source, int Width, int Height, string PixelFormatFourCC, int offset)
		{
			byte[] Data = new byte[Width * Height * 4];
			int BlockCountX = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(Width/4)));
			int BlockCountY = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(Height/4)));
			int PaletteIndicies;
			for (int BlockY = 0; BlockY < BlockCountY; ++BlockY)
			{
				for (int BlockX = 0; BlockX < BlockCountX; ++BlockX)
				{
					int[] Alphas = new int[2];
					if (PixelFormatFourCC != "DXT1")
					{
						int tmp = Utility.Byte4ToInt(Source, offset); offset += 4;
						int tmp1 = Utility.Byte4ToInt(Source, offset); offset += 4;
						Alphas = new int[2] {tmp, tmp1};
					}
					int X = BlockX * 4;
					int Y = BlockY * 4;
					int[] Colour = new int[2];
					byte[] A = new byte[4];
					byte[] R = new byte[4];
					byte[] G = new byte[4];
					byte[] B = new byte[4];
					for (int i = 0; i < Colour.Length; ++i)
					{
						Colour[i] = Utility.Byte2ToInt(Source, offset); offset += 2;
						A[i] = 255;
						R[i] = Convert.ToByte(Math.Floor(Convert.ToDecimal(((Colour[i] >> 11) & 0x1F) * 255 / 0x1F)));
						G[i] = Convert.ToByte(Math.Floor(Convert.ToDecimal(((Colour[i] >> 5) & 0x3F) * 255 / 0x3F)));
						B[i] = Convert.ToByte(Math.Floor(Convert.ToDecimal(((Colour[i] & 0x1F) * 255 / 0x1F))));
					}
					if (PixelFormatFourCC != "DXT1" || Colour[0] > Colour[1])
					{
						A[2] = 255;
						R[2] = Convert.ToByte(Math.Floor(Convert.ToDecimal((2 * R[0] + R[1]) / 3)));
						G[2] = Convert.ToByte(Math.Floor(Convert.ToDecimal((2 * G[0] + G[1]) / 3)));
						B[2] = Convert.ToByte(Math.Floor(Convert.ToDecimal((2 * B[0] + B[1]) / 3)));
						A[3] = 255;
						R[3] = Convert.ToByte(Math.Floor(Convert.ToDecimal((R[0] + 2 * R[1]) / 3)));
						G[3] = Convert.ToByte(Math.Floor(Convert.ToDecimal((G[0] + 2 * G[1]) / 3)));
						B[3] = Convert.ToByte(Math.Floor(Convert.ToDecimal((B[0] + 2 * B[1]) / 3)));
					} else
					{
						A[2] = 255;
						R[2] = Convert.ToByte(Math.Floor(Convert.ToDecimal((R[0] + R[1]) / 2)));
						G[2] = Convert.ToByte(Math.Floor(Convert.ToDecimal((G[0] + G[1]) / 2)));
						B[2] = Convert.ToByte(Math.Floor(Convert.ToDecimal((B[0] + B[1]) / 2)));
						A[3] = 0;
						R[3] = 0;
						G[3] = 0;
						B[3] = 0;
					}
					PaletteIndicies = Utility.Byte4ToInt(Source, offset); offset += 4;
					for (int Y2 = 0; Y2 < 4; ++Y2)
					{
						for (int X2 = 0; X2 < 4; ++X2)
						{
							int PaletteIndex = (PaletteIndicies >> (2 * (4 * Y2 + X2))) & 3;
							if (X+X2 < Width)
							{
								byte[] Pixel = new byte[4];
								if (PixelFormatFourCC == "DXT3")
								{
									int AlphaIndex = 4 * (4 * Y2 + X2);
									int alpha = (((Alphas[Convert.ToInt32(Math.Floor(Convert.ToDecimal(AlphaIndex / 32)))] >> (AlphaIndex % 32)) & 15) * 255 / 0xF);
									Pixel = new byte[4] { B[PaletteIndex], G[PaletteIndex], R[PaletteIndex], Convert.ToByte(alpha) };
								} else
								{
									Pixel = new byte[4] { B[PaletteIndex], G[PaletteIndex], R[PaletteIndex], A[PaletteIndex]};
								}
								Data[(X + X2 + (Height - 1 - (Y + Y2)) * Width) * 4 + 0] = Pixel[2];
								Data[(X + X2 + (Height - 1 - (Y + Y2)) * Width) * 4 + 1] = Pixel[1];
								Data[(X + X2 + (Height - 1 - (Y + Y2)) * Width) * 4 + 2] = Pixel[0];
								Data[(X + X2 + (Height - 1 - (Y + Y2)) * Width) * 4 + 3] = Pixel[3];
							}
						}
					}
				}
			}
			return Data;
		}

		public static byte[] LoadDDS (byte[] Source)
		{
			int offset = 0;
			int Signature = Utility.Byte4ToInt(Source, offset); offset += 4;
			if (Signature != 0x20534444)
			{
				//
			}
			int Size = Utility.Byte4ToInt(Source, offset); offset += 4;
			int Flags = Utility.Byte4ToInt(Source, offset); offset += 4;
			int Height = Utility.Byte4ToInt(Source, offset); offset += 4;
			int Width = Utility.Byte4ToInt(Source, offset); offset += 4;
			int PitchOrLinearSize = Utility.Byte4ToInt(Source, offset); offset += 4;
			int SignDepthature = Utility.Byte4ToInt(Source, offset); offset += 4;
			int MipMapCount = Utility.Byte4ToInt(Source, offset); offset += 4;
			int[] Reserved1 = new int[11];
			for (int i = 0; i < Reserved1.Length; ++i)
			{
				Reserved1[i] = Utility.Byte4ToInt(Source, offset); offset += 4;
			}
			int PixelFormatSize = Utility.Byte4ToInt(Source, offset); offset += 4;
			int PixelFormatFlags = Utility.Byte4ToInt(Source, offset); offset += 4;
			string PixelFormatFourCC = ReadFourCC(Source, offset); offset += 4;
			int PixelFormatRGBBitCount = Utility.Byte4ToInt(Source, offset); offset += 4;
			int PixelFormatRBitMask = Utility.Byte4ToInt(Source, offset); offset += 4;
			int PixelFormatGBitMask = Utility.Byte4ToInt(Source, offset); offset += 4;
			int PixelFormatBBitMask = Utility.Byte4ToInt(Source, offset); offset += 4;
			int PixelFormatABitMask = Utility.Byte4ToInt(Source, offset); offset += 4;
			int Caps = Utility.Byte4ToInt(Source, offset); offset += 4;
			int Caps2 = Utility.Byte4ToInt(Source, offset); offset += 4;
			int Caps3 = Utility.Byte4ToInt(Source, offset); offset += 4;
			int Caps4 = Utility.Byte4ToInt(Source, offset); offset += 4;
			int Reserved2 = Utility.Byte4ToInt(Source, offset); offset += 4;
			if ((PixelFormatFlags&DDPF_FOURCC) == 0)
			{
				//
			}
			
			if (PixelFormatFourCC != "DXT1" && PixelFormatFourCC != "DXT3")
			{
				return Source;
			}

			return (LoadDXT(Source, Width, Height, PixelFormatFourCC, offset));

		}

	}
}
