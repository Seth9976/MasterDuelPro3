using System;
using System.IO;

namespace SevenZip.Compression.LZMA
{
	// Token: 0x02000023 RID: 35
	public static class SevenZipHelper
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x00007DB4 File Offset: 0x00005FB4
		public static byte[] Compress(byte[] inputBytes, ICodeProgress progress = null)
		{
			MemoryStream memoryStream = new MemoryStream(inputBytes);
			MemoryStream memoryStream2 = new MemoryStream();
			SevenZipHelper.Compress(memoryStream, memoryStream2, progress);
			return memoryStream2.ToArray();
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00007DE4 File Offset: 0x00005FE4
		public static void Compress(Stream inStream, Stream outStream, ICodeProgress progress = null)
		{
			Encoder encoder = new Encoder();
			encoder.SetCoderProperties(SevenZipHelper.propIDs, SevenZipHelper.properties);
			encoder.WriteCoderProperties(outStream);
			encoder.Code(inStream, outStream, -1L, -1L, progress);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00007E20 File Offset: 0x00006020
		public static byte[] Decompress(byte[] inputBytes)
		{
			MemoryStream memoryStream = new MemoryStream(inputBytes);
			Decoder decoder = new Decoder();
			memoryStream.Seek(0L, SeekOrigin.Begin);
			MemoryStream memoryStream2 = new MemoryStream();
			byte[] array = new byte[5];
			bool flag = memoryStream.Read(array, 0, 5) != 5;
			if (flag)
			{
				throw new Exception("input .lzma is too short");
			}
			long num = 0L;
			for (int i = 0; i < 8; i++)
			{
				int num2 = memoryStream.ReadByte();
				bool flag2 = num2 < 0;
				if (flag2)
				{
					throw new Exception("Can't Read 1");
				}
				num |= (long)((long)((ulong)((byte)num2)) << 8 * i);
			}
			decoder.SetDecoderProperties(array);
			long num3 = memoryStream.Length - memoryStream.Position;
			decoder.Code(memoryStream, memoryStream2, num3, num, null);
			return memoryStream2.ToArray();
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00007EF4 File Offset: 0x000060F4
		public static MemoryStream StreamDecompress(MemoryStream newInStream)
		{
			Decoder decoder = new Decoder();
			newInStream.Seek(0L, SeekOrigin.Begin);
			MemoryStream memoryStream = new MemoryStream();
			byte[] array = new byte[5];
			bool flag = newInStream.Read(array, 0, 5) != 5;
			if (flag)
			{
				throw new Exception("input .lzma is too short");
			}
			long num = 0L;
			for (int i = 0; i < 8; i++)
			{
				int num2 = newInStream.ReadByte();
				bool flag2 = num2 < 0;
				if (flag2)
				{
					throw new Exception("Can't Read 1");
				}
				num |= (long)((long)((ulong)((byte)num2)) << 8 * i);
			}
			decoder.SetDecoderProperties(array);
			long num3 = newInStream.Length - newInStream.Position;
			decoder.Code(newInStream, memoryStream, num3, num, null);
			memoryStream.Position = 0L;
			return memoryStream;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00007FBC File Offset: 0x000061BC
		public static MemoryStream StreamDecompress(MemoryStream newInStream, long outSize)
		{
			Decoder decoder = new Decoder();
			newInStream.Seek(0L, SeekOrigin.Begin);
			MemoryStream memoryStream = new MemoryStream();
			byte[] array = new byte[5];
			bool flag = newInStream.Read(array, 0, 5) != 5;
			if (flag)
			{
				throw new Exception("input .lzma is too short");
			}
			decoder.SetDecoderProperties(array);
			long num = newInStream.Length - newInStream.Position;
			decoder.Code(newInStream, memoryStream, num, outSize, null);
			memoryStream.Position = 0L;
			return memoryStream;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000803C File Offset: 0x0000623C
		public static void StreamDecompress(Stream compressedStream, Stream decompressedStream, long compressedSize, long decompressedSize)
		{
			long position = compressedStream.Position;
			Decoder decoder = new Decoder();
			byte[] array = new byte[5];
			bool flag = compressedStream.Read(array, 0, 5) != 5;
			if (flag)
			{
				throw new Exception("input .lzma is too short");
			}
			decoder.SetDecoderProperties(array);
			decoder.Code(compressedStream, decompressedStream, compressedSize - 5L, decompressedSize, null);
			compressedStream.Position = position + compressedSize;
		}

		// Token: 0x040000E0 RID: 224
		private static CoderPropID[] propIDs = new CoderPropID[]
		{
			CoderPropID.DictionarySize,
			CoderPropID.PosStateBits,
			CoderPropID.LitContextBits,
			CoderPropID.LitPosBits,
			CoderPropID.Algorithm,
			CoderPropID.NumFastBytes,
			CoderPropID.MatchFinder,
			CoderPropID.EndMarker
		};

		// Token: 0x040000E1 RID: 225
		private static object[] properties = new object[] { 2097152, 2, 3, 0, 2, 32, "bt4", false };
	}
}
