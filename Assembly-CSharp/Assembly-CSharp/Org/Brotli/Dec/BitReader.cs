using System;
using System.IO;

namespace Org.Brotli.Dec
{
	// Token: 0x02000074 RID: 116
	internal sealed class BitReader
	{
		// Token: 0x0600022A RID: 554 RVA: 0x00006F84 File Offset: 0x00005184
		internal static void ReadMoreInput(BitReader br)
		{
			if (br.intOffset <= 1015)
			{
				return;
			}
			if (!br.endOfStreamReached)
			{
				int readOffset = br.intOffset << 2;
				int bytesRead = 4096 - readOffset;
				Array.Copy(br.byteBuffer, readOffset, br.byteBuffer, 0, bytesRead);
				br.intOffset = 0;
				try
				{
					while (bytesRead < 4096)
					{
						int len = br.input.Read(br.byteBuffer, bytesRead, 4096 - bytesRead);
						if (len <= 0)
						{
							br.endOfStreamReached = true;
							br.tailBytes = bytesRead;
							bytesRead += 3;
							break;
						}
						bytesRead += len;
					}
				}
				catch (IOException e)
				{
					throw new BrotliRuntimeException("Failed to read input", e);
				}
				IntReader.Convert(br.intReader, bytesRead >> 2);
				return;
			}
			if (BitReader.IntAvailable(br) >= -2)
			{
				return;
			}
			throw new BrotliRuntimeException("No more input");
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00007058 File Offset: 0x00005258
		internal static void CheckHealth(BitReader br, bool endOfStream)
		{
			if (!br.endOfStreamReached)
			{
				return;
			}
			int byteOffset = (br.intOffset << 2) + (br.bitOffset + 7 >> 3) - 8;
			if (byteOffset > br.tailBytes)
			{
				throw new BrotliRuntimeException("Read after end");
			}
			if (endOfStream && byteOffset != br.tailBytes)
			{
				throw new BrotliRuntimeException("Unused bytes after end");
			}
		}

		// Token: 0x0600022C RID: 556 RVA: 0x000070B0 File Offset: 0x000052B0
		internal static void FillBitWindow(BitReader br)
		{
			if (br.bitOffset >= 32)
			{
				int[] array = br.intBuffer;
				int num = br.intOffset;
				br.intOffset = num + 1;
				br.accumulator = (array[num] << 32) | (long)((ulong)br.accumulator >> 32);
				br.bitOffset -= 32;
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00007102 File Offset: 0x00005302
		internal static int ReadBits(BitReader br, int n)
		{
			BitReader.FillBitWindow(br);
			int num = (int)((ulong)br.accumulator >> br.bitOffset) & ((1 << n) - 1);
			br.bitOffset += n;
			return num;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00007134 File Offset: 0x00005334
		internal static void Init(BitReader br, Stream input)
		{
			if (br.input != null)
			{
				throw new InvalidOperationException("Bit reader already has associated input stream");
			}
			IntReader.Init(br.intReader, br.byteBuffer, br.intBuffer);
			br.input = input;
			br.accumulator = 0L;
			br.bitOffset = 64;
			br.intOffset = 1024;
			br.endOfStreamReached = false;
			BitReader.Prepare(br);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000719A File Offset: 0x0000539A
		private static void Prepare(BitReader br)
		{
			BitReader.ReadMoreInput(br);
			BitReader.CheckHealth(br, false);
			BitReader.FillBitWindow(br);
			BitReader.FillBitWindow(br);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x000071B5 File Offset: 0x000053B5
		internal static void Reload(BitReader br)
		{
			if (br.bitOffset == 64)
			{
				BitReader.Prepare(br);
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x000071C8 File Offset: 0x000053C8
		internal static void Close(BitReader br)
		{
			Stream @is = br.input;
			br.input = null;
			if (@is != null)
			{
				@is.Close();
			}
		}

		// Token: 0x06000232 RID: 562 RVA: 0x000071EC File Offset: 0x000053EC
		internal static void JumpToByteBoundary(BitReader br)
		{
			int padding = (64 - br.bitOffset) & 7;
			if (padding != 0 && BitReader.ReadBits(br, padding) != 0)
			{
				throw new BrotliRuntimeException("Corrupted padding bits");
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000721C File Offset: 0x0000541C
		internal static int IntAvailable(BitReader br)
		{
			int limit = 1024;
			if (br.endOfStreamReached)
			{
				limit = br.tailBytes + 3 >> 2;
			}
			return limit - br.intOffset;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000724C File Offset: 0x0000544C
		internal static void CopyBytes(BitReader br, byte[] data, int offset, int length)
		{
			if ((br.bitOffset & 7) != 0)
			{
				throw new BrotliRuntimeException("Unaligned copyBytes");
			}
			while (br.bitOffset != 64 && length != 0)
			{
				data[offset++] = (byte)((ulong)br.accumulator >> br.bitOffset);
				br.bitOffset += 8;
				length--;
			}
			if (length == 0)
			{
				return;
			}
			int copyInts = Math.Min(BitReader.IntAvailable(br), length >> 2);
			if (copyInts > 0)
			{
				int readOffset = br.intOffset << 2;
				Array.Copy(br.byteBuffer, readOffset, data, offset, copyInts << 2);
				offset += copyInts << 2;
				length -= copyInts << 2;
				br.intOffset += copyInts;
			}
			if (length == 0)
			{
				return;
			}
			if (BitReader.IntAvailable(br) > 0)
			{
				BitReader.FillBitWindow(br);
				while (length != 0)
				{
					data[offset++] = (byte)((ulong)br.accumulator >> br.bitOffset);
					br.bitOffset += 8;
					length--;
				}
				BitReader.CheckHealth(br, false);
				return;
			}
			try
			{
				while (length > 0)
				{
					int len = br.input.Read(data, offset, length);
					if (len == -1)
					{
						throw new BrotliRuntimeException("Unexpected end of input");
					}
					offset += len;
					length -= len;
				}
			}
			catch (IOException e)
			{
				throw new BrotliRuntimeException("Failed to read input", e);
			}
		}

		// Token: 0x040002C0 RID: 704
		private const int Capacity = 1024;

		// Token: 0x040002C1 RID: 705
		private const int Slack = 16;

		// Token: 0x040002C2 RID: 706
		private const int IntBufferSize = 1040;

		// Token: 0x040002C3 RID: 707
		private const int ByteReadSize = 4096;

		// Token: 0x040002C4 RID: 708
		private const int ByteBufferSize = 4160;

		// Token: 0x040002C5 RID: 709
		private readonly byte[] byteBuffer = new byte[4160];

		// Token: 0x040002C6 RID: 710
		private readonly int[] intBuffer = new int[1040];

		// Token: 0x040002C7 RID: 711
		private readonly IntReader intReader = new IntReader();

		// Token: 0x040002C8 RID: 712
		private Stream input;

		// Token: 0x040002C9 RID: 713
		private bool endOfStreamReached;

		// Token: 0x040002CA RID: 714
		internal long accumulator;

		// Token: 0x040002CB RID: 715
		internal int bitOffset;

		// Token: 0x040002CC RID: 716
		private int intOffset;

		// Token: 0x040002CD RID: 717
		private int tailBytes;
	}
}
