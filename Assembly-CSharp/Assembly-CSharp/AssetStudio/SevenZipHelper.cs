using System;
using System.IO;
using SevenZip.Compression.LZMA;

namespace AssetStudio
{
	// Token: 0x0200017D RID: 381
	public static class SevenZipHelper
	{
		// Token: 0x06000575 RID: 1397 RVA: 0x00019308 File Offset: 0x00017508
		public static MemoryStream StreamDecompress(MemoryStream inStream)
		{
			SevenZip.Compression.LZMA.Decoder decoder = new SevenZip.Compression.LZMA.Decoder();
			inStream.Seek(0L, SeekOrigin.Begin);
			MemoryStream newOutStream = new MemoryStream();
			byte[] properties = new byte[5];
			if (inStream.Read(properties, 0, 5) != 5)
			{
				throw new Exception("input .lzma is too short");
			}
			long outSize = 0L;
			for (int i = 0; i < 8; i++)
			{
				int v = inStream.ReadByte();
				if (v < 0)
				{
					throw new Exception("Can't Read 1");
				}
				outSize |= (long)((long)((ulong)((byte)v)) << 8 * i);
			}
			decoder.SetDecoderProperties(properties);
			long compressedSize = inStream.Length - inStream.Position;
			decoder.Code(inStream, newOutStream, compressedSize, outSize, null);
			newOutStream.Position = 0L;
			return newOutStream;
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x000193B0 File Offset: 0x000175B0
		public static void StreamDecompress(Stream compressedStream, Stream decompressedStream, long compressedSize, long decompressedSize)
		{
			long basePosition = compressedStream.Position;
			SevenZip.Compression.LZMA.Decoder decoder = new SevenZip.Compression.LZMA.Decoder();
			byte[] properties = new byte[5];
			if (compressedStream.Read(properties, 0, 5) != 5)
			{
				throw new Exception("input .lzma is too short");
			}
			decoder.SetDecoderProperties(properties);
			decoder.Code(compressedStream, decompressedStream, compressedSize - 5L, decompressedSize, null);
			compressedStream.Position = basePosition + compressedSize;
		}
	}
}
