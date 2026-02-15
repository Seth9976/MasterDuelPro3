using System;
using System.Diagnostics;

namespace LZ4ps
{
	// Token: 0x0200002C RID: 44
	public static class LZ4Codec
	{
		// Token: 0x060000E4 RID: 228 RVA: 0x000089C4 File Offset: 0x00006BC4
		public static int MaximumOutputLength(int inputLength)
		{
			return inputLength + inputLength / 255 + 16;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000089E4 File Offset: 0x00006BE4
		internal static void CheckArguments(byte[] input, int inputOffset, ref int inputLength, byte[] output, int outputOffset, ref int outputLength)
		{
			bool flag = inputLength < 0;
			if (flag)
			{
				inputLength = input.Length - inputOffset;
			}
			bool flag2 = inputLength == 0;
			if (flag2)
			{
				outputLength = 0;
			}
			else
			{
				bool flag3 = input == null;
				if (flag3)
				{
					throw new ArgumentNullException("input");
				}
				bool flag4 = inputOffset < 0 || inputOffset + inputLength > input.Length;
				if (flag4)
				{
					throw new ArgumentException("inputOffset and inputLength are invalid for given input");
				}
				bool flag5 = outputLength < 0;
				if (flag5)
				{
					outputLength = output.Length - outputOffset;
				}
				bool flag6 = output == null;
				if (flag6)
				{
					throw new ArgumentNullException("output");
				}
				bool flag7 = outputOffset < 0 || outputOffset + outputLength > output.Length;
				if (flag7)
				{
					throw new ArgumentException("outputOffset and outputLength are invalid for given output");
				}
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00008A98 File Offset: 0x00006C98
		[Conditional("DEBUG")]
		private static void Assert(bool condition, string errorMessage)
		{
			bool flag = !condition;
			if (flag)
			{
				throw new ArgumentException(errorMessage);
			}
			Debug.Assert(condition, errorMessage);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00008ABD File Offset: 0x00006CBD
		internal static void Poke2(byte[] buffer, int offset, ushort value)
		{
			buffer[offset] = (byte)value;
			buffer[offset + 1] = (byte)(value >> 8);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00008AD0 File Offset: 0x00006CD0
		internal static ushort Peek2(byte[] buffer, int offset)
		{
			return (ushort)((int)buffer[offset] | ((int)buffer[offset + 1] << 8));
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00008AF0 File Offset: 0x00006CF0
		internal static uint Peek4(byte[] buffer, int offset)
		{
			return (uint)((int)buffer[offset] | ((int)buffer[offset + 1] << 8) | ((int)buffer[offset + 2] << 16) | ((int)buffer[offset + 3] << 24));
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00008B20 File Offset: 0x00006D20
		private static uint Xor4(byte[] buffer, int offset1, int offset2)
		{
			uint num = (uint)((int)buffer[offset1] | ((int)buffer[offset1 + 1] << 8) | ((int)buffer[offset1 + 2] << 16) | ((int)buffer[offset1 + 3] << 24));
			uint num2 = (uint)((int)buffer[offset2] | ((int)buffer[offset2 + 1] << 8) | ((int)buffer[offset2 + 2] << 16) | ((int)buffer[offset2 + 3] << 24));
			return num ^ num2;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00008B74 File Offset: 0x00006D74
		private static ulong Xor8(byte[] buffer, int offset1, int offset2)
		{
			ulong num = (ulong)buffer[offset1] | ((ulong)buffer[offset1 + 1] << 8) | ((ulong)buffer[offset1 + 2] << 16) | ((ulong)buffer[offset1 + 3] << 24) | ((ulong)buffer[offset1 + 4] << 32) | ((ulong)buffer[offset1 + 5] << 40) | ((ulong)buffer[offset1 + 6] << 48) | ((ulong)buffer[offset1 + 7] << 56);
			ulong num2 = (ulong)buffer[offset2] | ((ulong)buffer[offset2 + 1] << 8) | ((ulong)buffer[offset2 + 2] << 16) | ((ulong)buffer[offset2 + 3] << 24) | ((ulong)buffer[offset2 + 4] << 32) | ((ulong)buffer[offset2 + 5] << 40) | ((ulong)buffer[offset2 + 6] << 48) | ((ulong)buffer[offset2 + 7] << 56);
			return num ^ num2;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00008C20 File Offset: 0x00006E20
		private static bool Equal2(byte[] buffer, int offset1, int offset2)
		{
			bool flag = buffer[offset1] != buffer[offset2];
			return !flag && buffer[offset1 + 1] == buffer[offset2 + 1];
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00008C54 File Offset: 0x00006E54
		private static bool Equal4(byte[] buffer, int offset1, int offset2)
		{
			bool flag = buffer[offset1] != buffer[offset2];
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = buffer[offset1 + 1] != buffer[offset2 + 1];
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					bool flag4 = buffer[offset1 + 2] != buffer[offset2 + 2];
					flag2 = !flag4 && buffer[offset1 + 3] == buffer[offset2 + 3];
				}
			}
			return flag2;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00008CB3 File Offset: 0x00006EB3
		private static void Copy4(byte[] buf, int src, int dst)
		{
			LZ4Codec.Assert(dst > src, "Copying backwards is not implemented");
			buf[dst + 3] = buf[src + 3];
			buf[dst + 2] = buf[src + 2];
			buf[dst + 1] = buf[src + 1];
			buf[dst] = buf[src];
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00008CEC File Offset: 0x00006EEC
		private static void Copy8(byte[] buf, int src, int dst)
		{
			LZ4Codec.Assert(dst > src, "Copying backwards is not implemented");
			buf[dst + 7] = buf[src + 7];
			buf[dst + 6] = buf[src + 6];
			buf[dst + 5] = buf[src + 5];
			buf[dst + 4] = buf[src + 4];
			buf[dst + 3] = buf[src + 3];
			buf[dst + 2] = buf[src + 2];
			buf[dst + 1] = buf[src + 1];
			buf[dst] = buf[src];
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00008D58 File Offset: 0x00006F58
		private static void BlockCopy(byte[] src, int src_0, byte[] dst, int dst_0, int len)
		{
			LZ4Codec.Assert(src != dst, "BlockCopy does not handle copying to the same buffer");
			bool flag = len >= 16;
			if (flag)
			{
				Buffer.BlockCopy(src, src_0, dst, dst_0, len);
			}
			else
			{
				while (len >= 8)
				{
					dst[dst_0] = src[src_0];
					dst[dst_0 + 1] = src[src_0 + 1];
					dst[dst_0 + 2] = src[src_0 + 2];
					dst[dst_0 + 3] = src[src_0 + 3];
					dst[dst_0 + 4] = src[src_0 + 4];
					dst[dst_0 + 5] = src[src_0 + 5];
					dst[dst_0 + 6] = src[src_0 + 6];
					dst[dst_0 + 7] = src[src_0 + 7];
					len -= 8;
					src_0 += 8;
					dst_0 += 8;
				}
				while (len >= 4)
				{
					dst[dst_0] = src[src_0];
					dst[dst_0 + 1] = src[src_0 + 1];
					dst[dst_0 + 2] = src[src_0 + 2];
					dst[dst_0 + 3] = src[src_0 + 3];
					len -= 4;
					src_0 += 4;
					dst_0 += 4;
				}
				while (len-- > 0)
				{
					dst[dst_0++] = src[src_0++];
				}
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00008E6C File Offset: 0x0000706C
		private static int WildCopy(byte[] src, int src_0, byte[] dst, int dst_0, int dst_end)
		{
			int i = dst_end - dst_0;
			LZ4Codec.Assert(src != dst, "BlockCopy does not handle copying to the same buffer");
			LZ4Codec.Assert(i > 0, "Length have to be greater than 0");
			bool flag = i >= 16;
			if (flag)
			{
				Buffer.BlockCopy(src, src_0, dst, dst_0, i);
			}
			else
			{
				while (i >= 4)
				{
					dst[dst_0] = src[src_0];
					dst[dst_0 + 1] = src[src_0 + 1];
					dst[dst_0 + 2] = src[src_0 + 2];
					dst[dst_0 + 3] = src[src_0 + 3];
					i -= 4;
					src_0 += 4;
					dst_0 += 4;
				}
				while (i-- > 0)
				{
					dst[dst_0++] = src[src_0++];
				}
			}
			return i;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00008F28 File Offset: 0x00007128
		private static int SecureCopy(byte[] buffer, int src, int dst, int dst_end)
		{
			int num = dst - src;
			int num2 = dst_end - dst;
			int i = num2;
			LZ4Codec.Assert(num >= 4, "Target must be at least 4 bytes further than source");
			LZ4Codec.Assert(true, "This method requires BLOCK_COPY_LIMIT > 4");
			LZ4Codec.Assert(i > 0, "Length have to be greater than 0");
			bool flag = num >= 16;
			if (flag)
			{
				bool flag2 = num >= num2;
				if (flag2)
				{
					Buffer.BlockCopy(buffer, src, buffer, dst, num2);
					return num2;
				}
				do
				{
					Buffer.BlockCopy(buffer, src, buffer, dst, num);
					src += num;
					dst += num;
					i -= num;
				}
				while (i >= num);
			}
			while (i >= 4)
			{
				buffer[dst] = buffer[src];
				buffer[dst + 1] = buffer[src + 1];
				buffer[dst + 2] = buffer[src + 2];
				buffer[dst + 3] = buffer[src + 3];
				dst += 4;
				src += 4;
				i -= 4;
			}
			while (i-- > 0)
			{
				buffer[dst++] = buffer[src++];
			}
			return num2;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00009030 File Offset: 0x00007230
		public static int Encode32(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength)
		{
			LZ4Codec.CheckArguments(input, inputOffset, ref inputLength, output, outputOffset, ref outputLength);
			bool flag = outputLength == 0;
			int num;
			if (flag)
			{
				num = 0;
			}
			else
			{
				bool flag2 = inputLength < 65547;
				if (flag2)
				{
					ushort[] array = new ushort[8192];
					num = LZ4Codec.LZ4_compress64kCtx_safe32(array, input, output, inputOffset, outputOffset, inputLength, outputLength);
				}
				else
				{
					int[] array2 = new int[4096];
					num = LZ4Codec.LZ4_compressCtx_safe32(array2, input, output, inputOffset, outputOffset, inputLength, outputLength);
				}
			}
			return num;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000090A4 File Offset: 0x000072A4
		public static byte[] Encode32(byte[] input, int inputOffset, int inputLength)
		{
			bool flag = inputLength < 0;
			if (flag)
			{
				inputLength = input.Length - inputOffset;
			}
			bool flag2 = input == null;
			if (flag2)
			{
				throw new ArgumentNullException("input");
			}
			bool flag3 = inputOffset < 0 || inputOffset + inputLength > input.Length;
			if (flag3)
			{
				throw new ArgumentException("inputOffset and inputLength are invalid for given input");
			}
			byte[] array = new byte[LZ4Codec.MaximumOutputLength(inputLength)];
			int num = LZ4Codec.Encode32(input, inputOffset, inputLength, array, 0, array.Length);
			bool flag4 = num != array.Length;
			byte[] array3;
			if (flag4)
			{
				bool flag5 = num < 0;
				if (flag5)
				{
					throw new InvalidOperationException("Compression has been corrupted");
				}
				byte[] array2 = new byte[num];
				Buffer.BlockCopy(array, 0, array2, 0, num);
				array3 = array2;
			}
			else
			{
				array3 = array;
			}
			return array3;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00009154 File Offset: 0x00007354
		public static int Encode64(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength)
		{
			LZ4Codec.CheckArguments(input, inputOffset, ref inputLength, output, outputOffset, ref outputLength);
			bool flag = outputLength == 0;
			int num;
			if (flag)
			{
				num = 0;
			}
			else
			{
				bool flag2 = inputLength < 65547;
				if (flag2)
				{
					ushort[] array = new ushort[8192];
					num = LZ4Codec.LZ4_compress64kCtx_safe64(array, input, output, inputOffset, outputOffset, inputLength, outputLength);
				}
				else
				{
					int[] array2 = new int[4096];
					num = LZ4Codec.LZ4_compressCtx_safe64(array2, input, output, inputOffset, outputOffset, inputLength, outputLength);
				}
			}
			return num;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000091C8 File Offset: 0x000073C8
		public static byte[] Encode64(byte[] input, int inputOffset, int inputLength)
		{
			bool flag = inputLength < 0;
			if (flag)
			{
				inputLength = input.Length - inputOffset;
			}
			bool flag2 = input == null;
			if (flag2)
			{
				throw new ArgumentNullException("input");
			}
			bool flag3 = inputOffset < 0 || inputOffset + inputLength > input.Length;
			if (flag3)
			{
				throw new ArgumentException("inputOffset and inputLength are invalid for given input");
			}
			byte[] array = new byte[LZ4Codec.MaximumOutputLength(inputLength)];
			int num = LZ4Codec.Encode64(input, inputOffset, inputLength, array, 0, array.Length);
			bool flag4 = num != array.Length;
			byte[] array3;
			if (flag4)
			{
				bool flag5 = num < 0;
				if (flag5)
				{
					throw new InvalidOperationException("Compression has been corrupted");
				}
				byte[] array2 = new byte[num];
				Buffer.BlockCopy(array, 0, array2, 0, num);
				array3 = array2;
			}
			else
			{
				array3 = array;
			}
			return array3;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00009278 File Offset: 0x00007478
		public static int Decode32(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength, bool knownOutputLength)
		{
			LZ4Codec.CheckArguments(input, inputOffset, ref inputLength, output, outputOffset, ref outputLength);
			bool flag = outputLength == 0;
			int num;
			if (flag)
			{
				num = 0;
			}
			else if (knownOutputLength)
			{
				int num2 = LZ4Codec.LZ4_uncompress_safe32(input, output, inputOffset, outputOffset, outputLength);
				bool flag2 = num2 != inputLength;
				if (flag2)
				{
					throw new ArgumentException("LZ4 block is corrupted, or invalid length has been given.");
				}
				num = outputLength;
			}
			else
			{
				int num3 = LZ4Codec.LZ4_uncompress_unknownOutputSize_safe32(input, output, inputOffset, outputOffset, inputLength, outputLength);
				bool flag3 = num3 < 0;
				if (flag3)
				{
					throw new ArgumentException("LZ4 block is corrupted, or invalid length has been given.");
				}
				num = num3;
			}
			return num;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00009300 File Offset: 0x00007500
		public static byte[] Decode32(byte[] input, int inputOffset, int inputLength, int outputLength)
		{
			bool flag = inputLength < 0;
			if (flag)
			{
				inputLength = input.Length - inputOffset;
			}
			bool flag2 = input == null;
			if (flag2)
			{
				throw new ArgumentNullException("input");
			}
			bool flag3 = inputOffset < 0 || inputOffset + inputLength > input.Length;
			if (flag3)
			{
				throw new ArgumentException("inputOffset and inputLength are invalid for given input");
			}
			byte[] array = new byte[outputLength];
			int num = LZ4Codec.Decode32(input, inputOffset, inputLength, array, 0, outputLength, true);
			bool flag4 = num != outputLength;
			if (flag4)
			{
				throw new ArgumentException("outputLength is not valid");
			}
			return array;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00009384 File Offset: 0x00007584
		public static int Decode64(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength, bool knownOutputLength)
		{
			LZ4Codec.CheckArguments(input, inputOffset, ref inputLength, output, outputOffset, ref outputLength);
			bool flag = outputLength == 0;
			int num;
			if (flag)
			{
				num = 0;
			}
			else if (knownOutputLength)
			{
				int num2 = LZ4Codec.LZ4_uncompress_safe64(input, output, inputOffset, outputOffset, outputLength);
				bool flag2 = num2 != inputLength;
				if (flag2)
				{
					throw new ArgumentException("LZ4 block is corrupted, or invalid length has been given.");
				}
				num = outputLength;
			}
			else
			{
				int num3 = LZ4Codec.LZ4_uncompress_unknownOutputSize_safe64(input, output, inputOffset, outputOffset, inputLength, outputLength);
				bool flag3 = num3 < 0;
				if (flag3)
				{
					throw new ArgumentException("LZ4 block is corrupted, or invalid length has been given.");
				}
				num = num3;
			}
			return num;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000940C File Offset: 0x0000760C
		public static byte[] Decode64(byte[] input, int inputOffset, int inputLength, int outputLength)
		{
			bool flag = inputLength < 0;
			if (flag)
			{
				inputLength = input.Length - inputOffset;
			}
			bool flag2 = input == null;
			if (flag2)
			{
				throw new ArgumentNullException("input");
			}
			bool flag3 = inputOffset < 0 || inputOffset + inputLength > input.Length;
			if (flag3)
			{
				throw new ArgumentException("inputOffset and inputLength are invalid for given input");
			}
			byte[] array = new byte[outputLength];
			int num = LZ4Codec.Decode64(input, inputOffset, inputLength, array, 0, outputLength, true);
			bool flag4 = num != outputLength;
			if (flag4)
			{
				throw new ArgumentException("outputLength is not valid");
			}
			return array;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00009490 File Offset: 0x00007690
		private static LZ4Codec.LZ4HC_Data_Structure LZ4HC_Create(byte[] src, int src_0, int src_len, byte[] dst, int dst_0, int dst_len)
		{
			LZ4Codec.LZ4HC_Data_Structure lz4HC_Data_Structure = new LZ4Codec.LZ4HC_Data_Structure
			{
				src = src,
				src_base = src_0,
				src_end = src_0 + src_len,
				src_LASTLITERALS = src_0 + src_len - 5,
				dst = dst,
				dst_base = dst_0,
				dst_len = dst_len,
				dst_end = dst_0 + dst_len,
				hashTable = new int[32768],
				chainTable = new ushort[65536],
				nextToUpdate = src_0 + 1
			};
			ushort[] chainTable = lz4HC_Data_Structure.chainTable;
			for (int i = chainTable.Length - 1; i >= 0; i--)
			{
				chainTable[i] = ushort.MaxValue;
			}
			return lz4HC_Data_Structure;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00009544 File Offset: 0x00007744
		private static int LZ4_compressHC_32(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength)
		{
			return LZ4Codec.LZ4_compressHCCtx_32(LZ4Codec.LZ4HC_Create(input, inputOffset, inputLength, output, outputOffset, outputLength));
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00009568 File Offset: 0x00007768
		public static int Encode32HC(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength)
		{
			bool flag = inputLength == 0;
			int num;
			if (flag)
			{
				num = 0;
			}
			else
			{
				LZ4Codec.CheckArguments(input, inputOffset, ref inputLength, output, outputOffset, ref outputLength);
				int num2 = LZ4Codec.LZ4_compressHC_32(input, inputOffset, inputLength, output, outputOffset, outputLength);
				num = ((num2 <= 0) ? (-1) : num2);
			}
			return num;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000095AC File Offset: 0x000077AC
		public static byte[] Encode32HC(byte[] input, int inputOffset, int inputLength)
		{
			bool flag = inputLength == 0;
			byte[] array;
			if (flag)
			{
				array = new byte[0];
			}
			else
			{
				int num = LZ4Codec.MaximumOutputLength(inputLength);
				byte[] array2 = new byte[num];
				int num2 = LZ4Codec.Encode32HC(input, inputOffset, inputLength, array2, 0, num);
				bool flag2 = num2 < 0;
				if (flag2)
				{
					throw new ArgumentException("Provided data seems to be corrupted.");
				}
				bool flag3 = num2 != num;
				if (flag3)
				{
					byte[] array3 = new byte[num2];
					Buffer.BlockCopy(array2, 0, array3, 0, num2);
					array2 = array3;
				}
				array = array2;
			}
			return array;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00009628 File Offset: 0x00007828
		private static int LZ4_compressHC_64(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength)
		{
			return LZ4Codec.LZ4_compressHCCtx_64(LZ4Codec.LZ4HC_Create(input, inputOffset, inputLength, output, outputOffset, outputLength));
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000964C File Offset: 0x0000784C
		public static int Encode64HC(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength)
		{
			bool flag = inputLength == 0;
			int num;
			if (flag)
			{
				num = 0;
			}
			else
			{
				LZ4Codec.CheckArguments(input, inputOffset, ref inputLength, output, outputOffset, ref outputLength);
				int num2 = LZ4Codec.LZ4_compressHC_64(input, inputOffset, inputLength, output, outputOffset, outputLength);
				num = ((num2 <= 0) ? (-1) : num2);
			}
			return num;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00009690 File Offset: 0x00007890
		public static byte[] Encode64HC(byte[] input, int inputOffset, int inputLength)
		{
			bool flag = inputLength == 0;
			byte[] array;
			if (flag)
			{
				array = new byte[0];
			}
			else
			{
				int num = LZ4Codec.MaximumOutputLength(inputLength);
				byte[] array2 = new byte[num];
				int num2 = LZ4Codec.Encode64HC(input, inputOffset, inputLength, array2, 0, num);
				bool flag2 = num2 < 0;
				if (flag2)
				{
					throw new ArgumentException("Provided data seems to be corrupted.");
				}
				bool flag3 = num2 != num;
				if (flag3)
				{
					byte[] array3 = new byte[num2];
					Buffer.BlockCopy(array2, 0, array3, 0, num2);
					array2 = array3;
				}
				array = array2;
			}
			return array;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000970C File Offset: 0x0000790C
		private static int LZ4_compressCtx_safe32(int[] hash_table, byte[] src, byte[] dst, int src_0, int dst_0, int src_len, int dst_maxlen)
		{
			int[] debruijn_TABLE_ = LZ4Codec.DEBRUIJN_TABLE_32;
			int num = src_0;
			int num2 = src_0 + src_len;
			int num3 = num2 - 12;
			int num4 = dst_0;
			int num5 = num4 + dst_maxlen;
			int num6 = num2 - 5;
			int num7 = num6 - 1;
			int num8 = num6 - 3;
			int num9 = num5 - 6;
			int num10 = num5 - 8;
			bool flag = src_len < 13;
			if (!flag)
			{
				hash_table[(int)(LZ4Codec.Peek4(src, src_0) * 2654435761U >> 20)] = src_0 - src_0;
				int i = src_0 + 1;
				uint num11 = LZ4Codec.Peek4(src, i) * 2654435761U >> 20;
				for (;;)
				{
					int num12 = 67;
					int num13 = i;
					int num16;
					do
					{
						uint num14 = num11;
						int num15 = num12++ >> 6;
						i = num13;
						num13 = i + num15;
						bool flag2 = num13 > num3;
						if (flag2)
						{
							goto Block_2;
						}
						num11 = LZ4Codec.Peek4(src, num13) * 2654435761U >> 20;
						num16 = src_0 + hash_table[(int)num14];
						hash_table[(int)num14] = i - src_0;
					}
					while (num16 < i - 65535 || !LZ4Codec.Equal4(src, num16, i));
					while (i > num && num16 > src_0 && src[i - 1] == src[num16 - 1])
					{
						i--;
						num16--;
					}
					int j = i - num;
					int num17 = num4++;
					bool flag3 = num4 + j + (j >> 8) > num10;
					if (flag3)
					{
						goto Block_8;
					}
					bool flag4 = j >= 15;
					if (!flag4)
					{
						dst[num17] = (byte)(j << 4);
						goto IL_01E9;
					}
					int num18 = j - 15;
					dst[num17] = 240;
					bool flag5 = num18 > 254;
					if (!flag5)
					{
						dst[num4++] = (byte)num18;
						goto IL_01E9;
					}
					do
					{
						dst[num4++] = byte.MaxValue;
						num18 -= 255;
					}
					while (num18 > 254);
					dst[num4++] = (byte)num18;
					LZ4Codec.BlockCopy(src, num, dst, num4, j);
					num4 += j;
					for (;;)
					{
						IL_020C:
						LZ4Codec.Poke2(dst, num4, (ushort)(i - num16));
						num4 += 2;
						i += 4;
						num16 += 4;
						num = i;
						while (i < num8)
						{
							int num19 = (int)LZ4Codec.Xor4(src, num16, i);
							bool flag6 = num19 == 0;
							if (flag6)
							{
								i += 4;
								num16 += 4;
							}
							else
							{
								i += debruijn_TABLE_[(int)((uint)((num19 & -num19) * 125613361) >> 27)];
								IL_02B3:
								j = i - num;
								bool flag7 = num4 + (j >> 8) > num9;
								if (flag7)
								{
									goto Block_18;
								}
								bool flag8 = j >= 15;
								if (flag8)
								{
									int num20 = num17;
									dst[num20] += 15;
									for (j -= 15; j > 509; j -= 510)
									{
										dst[num4++] = byte.MaxValue;
										dst[num4++] = byte.MaxValue;
									}
									bool flag9 = j > 254;
									if (flag9)
									{
										j -= 255;
										dst[num4++] = byte.MaxValue;
									}
									dst[num4++] = (byte)j;
								}
								else
								{
									int num21 = num17;
									dst[num21] += (byte)j;
								}
								bool flag10 = i > num3;
								if (flag10)
								{
									goto Block_22;
								}
								hash_table[(int)(LZ4Codec.Peek4(src, i - 2) * 2654435761U >> 20)] = i - 2 - src_0;
								uint num14 = LZ4Codec.Peek4(src, i) * 2654435761U >> 20;
								num16 = src_0 + hash_table[(int)num14];
								hash_table[(int)num14] = i - src_0;
								bool flag11 = num16 > i - 65536 && LZ4Codec.Equal4(src, num16, i);
								if (flag11)
								{
									num17 = num4++;
									dst[num17] = 0;
									goto IL_020C;
								}
								goto IL_03FE;
							}
						}
						bool flag12 = i < num7 && LZ4Codec.Equal2(src, num16, i);
						if (flag12)
						{
							i += 2;
							num16 += 2;
						}
						bool flag13 = i < num6 && src[num16] == src[i];
						if (flag13)
						{
							i++;
							goto IL_02B3;
						}
						goto IL_02B3;
					}
					IL_03FE:
					num = i++;
					num11 = LZ4Codec.Peek4(src, i) * 2654435761U >> 20;
					continue;
					IL_01E9:
					bool flag14 = j > 0;
					if (flag14)
					{
						int num22 = num4 + j;
						LZ4Codec.WildCopy(src, num, dst, num4, num22);
						num4 = num22;
						goto IL_020C;
					}
					goto IL_020C;
				}
				Block_2:
				goto IL_0420;
				Block_8:
				return 0;
				Block_18:
				return 0;
				Block_22:
				num = i;
			}
			IL_0420:
			int k = num2 - num;
			bool flag15 = num4 + k + 1 + (k + 255 - 15) / 255 > num5;
			int num23;
			if (flag15)
			{
				num23 = 0;
			}
			else
			{
				bool flag16 = k >= 15;
				if (flag16)
				{
					dst[num4++] = 240;
					for (k -= 15; k > 254; k -= 255)
					{
						dst[num4++] = byte.MaxValue;
					}
					dst[num4++] = (byte)k;
				}
				else
				{
					dst[num4++] = (byte)(k << 4);
				}
				LZ4Codec.BlockCopy(src, num, dst, num4, num2 - num);
				num4 += num2 - num;
				num23 = num4 - dst_0;
			}
			return num23;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00009C00 File Offset: 0x00007E00
		private static int LZ4_compress64kCtx_safe32(ushort[] hash_table, byte[] src, byte[] dst, int src_0, int dst_0, int src_len, int dst_maxlen)
		{
			int[] debruijn_TABLE_ = LZ4Codec.DEBRUIJN_TABLE_32;
			int num = src_0;
			int num2 = src_0 + src_len;
			int num3 = num2 - 12;
			int num4 = dst_0;
			int num5 = num4 + dst_maxlen;
			int num6 = num2 - 5;
			int num7 = num6 - 1;
			int num8 = num6 - 3;
			int num9 = num5 - 6;
			int num10 = num5 - 8;
			bool flag = src_len < 13;
			if (!flag)
			{
				int i = src_0 + 1;
				uint num11 = LZ4Codec.Peek4(src, i) * 2654435761U >> 19;
				for (;;)
				{
					int num12 = 67;
					int num13 = i;
					int num16;
					do
					{
						uint num14 = num11;
						int num15 = num12++ >> 6;
						i = num13;
						num13 = i + num15;
						bool flag2 = num13 > num3;
						if (flag2)
						{
							goto Block_2;
						}
						num11 = LZ4Codec.Peek4(src, num13) * 2654435761U >> 19;
						num16 = src_0 + (int)hash_table[(int)num14];
						hash_table[(int)num14] = (ushort)(i - src_0);
					}
					while (!LZ4Codec.Equal4(src, num16, i));
					while (i > num && num16 > src_0 && src[i - 1] == src[num16 - 1])
					{
						i--;
						num16--;
					}
					int num17 = i - num;
					int num18 = num4++;
					bool flag3 = num4 + num17 + (num17 >> 8) > num10;
					if (flag3)
					{
						goto Block_7;
					}
					bool flag4 = num17 >= 15;
					if (!flag4)
					{
						dst[num18] = (byte)(num17 << 4);
						goto IL_01C8;
					}
					int j = num17 - 15;
					dst[num18] = 240;
					bool flag5 = j > 254;
					if (!flag5)
					{
						dst[num4++] = (byte)j;
						goto IL_01C8;
					}
					do
					{
						dst[num4++] = byte.MaxValue;
						j -= 255;
					}
					while (j > 254);
					dst[num4++] = (byte)j;
					LZ4Codec.BlockCopy(src, num, dst, num4, num17);
					num4 += num17;
					for (;;)
					{
						IL_01EA:
						LZ4Codec.Poke2(dst, num4, (ushort)(i - num16));
						num4 += 2;
						i += 4;
						num16 += 4;
						num = i;
						while (i < num8)
						{
							int num19 = (int)LZ4Codec.Xor4(src, num16, i);
							bool flag6 = num19 == 0;
							if (flag6)
							{
								i += 4;
								num16 += 4;
							}
							else
							{
								i += debruijn_TABLE_[(int)((uint)((num19 & -num19) * 125613361) >> 27)];
								IL_0290:
								j = i - num;
								bool flag7 = num4 + (j >> 8) > num9;
								if (flag7)
								{
									goto Block_17;
								}
								bool flag8 = j >= 15;
								if (flag8)
								{
									int num20 = num18;
									dst[num20] += 15;
									for (j -= 15; j > 509; j -= 510)
									{
										dst[num4++] = byte.MaxValue;
										dst[num4++] = byte.MaxValue;
									}
									bool flag9 = j > 254;
									if (flag9)
									{
										j -= 255;
										dst[num4++] = byte.MaxValue;
									}
									dst[num4++] = (byte)j;
								}
								else
								{
									int num21 = num18;
									dst[num21] += (byte)j;
								}
								bool flag10 = i > num3;
								if (flag10)
								{
									goto Block_21;
								}
								hash_table[(int)(LZ4Codec.Peek4(src, i - 2) * 2654435761U >> 19)] = (ushort)(i - 2 - src_0);
								uint num14 = LZ4Codec.Peek4(src, i) * 2654435761U >> 19;
								num16 = src_0 + (int)hash_table[(int)num14];
								hash_table[(int)num14] = (ushort)(i - src_0);
								bool flag11 = LZ4Codec.Equal4(src, num16, i);
								if (flag11)
								{
									num18 = num4++;
									dst[num18] = 0;
									goto IL_01EA;
								}
								goto IL_03D0;
							}
						}
						bool flag12 = i < num7 && LZ4Codec.Equal2(src, num16, i);
						if (flag12)
						{
							i += 2;
							num16 += 2;
						}
						bool flag13 = i < num6 && src[num16] == src[i];
						if (flag13)
						{
							i++;
							goto IL_0290;
						}
						goto IL_0290;
					}
					IL_03D0:
					num = i++;
					num11 = LZ4Codec.Peek4(src, i) * 2654435761U >> 19;
					continue;
					IL_01C8:
					bool flag14 = num17 > 0;
					if (flag14)
					{
						int num22 = num4 + num17;
						LZ4Codec.WildCopy(src, num, dst, num4, num22);
						num4 = num22;
						goto IL_01EA;
					}
					goto IL_01EA;
				}
				Block_2:
				goto IL_03F1;
				Block_7:
				return 0;
				Block_17:
				return 0;
				Block_21:
				num = i;
			}
			IL_03F1:
			int k = num2 - num;
			bool flag15 = num4 + k + 1 + (k - 15 + 255) / 255 > num5;
			int num23;
			if (flag15)
			{
				num23 = 0;
			}
			else
			{
				bool flag16 = k >= 15;
				if (flag16)
				{
					dst[num4++] = 240;
					for (k -= 15; k > 254; k -= 255)
					{
						dst[num4++] = byte.MaxValue;
					}
					dst[num4++] = (byte)k;
				}
				else
				{
					dst[num4++] = (byte)(k << 4);
				}
				LZ4Codec.BlockCopy(src, num, dst, num4, num2 - num);
				num4 += num2 - num;
				num23 = num4 - dst_0;
			}
			return num23;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000A0C0 File Offset: 0x000082C0
		private static int LZ4_uncompress_safe32(byte[] src, byte[] dst, int src_0, int dst_0, int dst_len)
		{
			int[] decoder_TABLE_ = LZ4Codec.DECODER_TABLE_32;
			int num = src_0;
			int i = dst_0;
			int num2 = i + dst_len;
			int num3 = num2 - 5;
			int num4 = num2 - 8;
			int num5 = num2 - 8;
			int num6;
			int num8;
			for (;;)
			{
				byte b = src[num++];
				bool flag = (num6 = b >> 4) == 15;
				if (flag)
				{
					int num7;
					while ((num7 = (int)src[num++]) == 255)
					{
						num6 += 255;
					}
					num6 += num7;
				}
				num8 = i + num6;
				bool flag2 = num8 > num4;
				if (flag2)
				{
					break;
				}
				bool flag3 = i < num8;
				if (flag3)
				{
					int num9 = LZ4Codec.WildCopy(src, num, dst, i, num8);
					num += num9;
					i += num9;
				}
				num -= i - num8;
				i = num8;
				int num10 = num8 - (int)LZ4Codec.Peek2(src, num);
				num += 2;
				bool flag4 = num10 < dst_0;
				if (flag4)
				{
					goto Block_6;
				}
				bool flag5 = (num6 = (int)(b & 15)) == 15;
				if (flag5)
				{
					while (src[num] == 255)
					{
						num++;
						num6 += 255;
					}
					num6 += (int)src[num++];
				}
				bool flag6 = i - num10 < 4;
				if (flag6)
				{
					dst[i] = dst[num10];
					dst[i + 1] = dst[num10 + 1];
					dst[i + 2] = dst[num10 + 2];
					dst[i + 3] = dst[num10 + 3];
					i += 4;
					num10 += 4;
					num10 -= decoder_TABLE_[i - num10];
					LZ4Codec.Copy4(dst, num10, i);
					i = i;
					num10 = num10;
				}
				else
				{
					LZ4Codec.Copy4(dst, num10, i);
					i += 4;
					num10 += 4;
				}
				num8 = i + num6;
				bool flag7 = num8 > num5;
				if (flag7)
				{
					bool flag8 = num8 > num3;
					if (flag8)
					{
						goto Block_11;
					}
					bool flag9 = i < num4;
					if (flag9)
					{
						int num9 = LZ4Codec.SecureCopy(dst, num10, i, num4);
						num10 += num9;
						i += num9;
					}
					while (i < num8)
					{
						dst[i++] = dst[num10++];
					}
					i = num8;
				}
				else
				{
					bool flag10 = i < num8;
					if (flag10)
					{
						LZ4Codec.SecureCopy(dst, num10, i, num8);
					}
					i = num8;
				}
			}
			bool flag11 = num8 != num2;
			if (!flag11)
			{
				LZ4Codec.BlockCopy(src, num, dst, i, num6);
				num += num6;
				return num - src_0;
			}
			Block_6:
			Block_11:
			return -(num - src_0);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x0000A324 File Offset: 0x00008524
		private static int LZ4_uncompress_unknownOutputSize_safe32(byte[] src, byte[] dst, int src_0, int dst_0, int src_len, int dst_maxlen)
		{
			int[] decoder_TABLE_ = LZ4Codec.DECODER_TABLE_32;
			int i = src_0;
			int num = i + src_len;
			int j = dst_0;
			int num2 = j + dst_maxlen;
			int num3 = num - 8;
			int num4 = num - 6;
			int num5 = num2 - 8;
			int num6 = num2 - 8;
			int num7 = num2 - 5;
			int num8 = num2 - 12;
			bool flag = i == num;
			if (!flag)
			{
				int num9;
				int num11;
				for (;;)
				{
					byte b = src[i++];
					bool flag2 = (num9 = b >> 4) == 15;
					if (flag2)
					{
						int num10 = 255;
						while (i < num && num10 == 255)
						{
							num9 += (num10 = (int)src[i++]);
						}
					}
					num11 = j + num9;
					bool flag3 = num11 > num8 || i + num9 > num3;
					if (flag3)
					{
						break;
					}
					bool flag4 = j < num11;
					if (flag4)
					{
						int num12 = LZ4Codec.WildCopy(src, i, dst, j, num11);
						i += num12;
						j += num12;
					}
					i -= j - num11;
					j = num11;
					int num13 = num11 - (int)LZ4Codec.Peek2(src, i);
					i += 2;
					bool flag5 = num13 < dst_0;
					if (flag5)
					{
						goto Block_9;
					}
					bool flag6 = (num9 = (int)(b & 15)) == 15;
					if (flag6)
					{
						while (i < num4)
						{
							int num14 = (int)src[i++];
							num9 += num14;
							bool flag7 = num14 == 255;
							if (!flag7)
							{
								break;
							}
						}
					}
					bool flag8 = j - num13 < 4;
					if (flag8)
					{
						dst[j] = dst[num13];
						dst[j + 1] = dst[num13 + 1];
						dst[j + 2] = dst[num13 + 2];
						dst[j + 3] = dst[num13 + 3];
						j += 4;
						num13 += 4;
						num13 -= decoder_TABLE_[j - num13];
						LZ4Codec.Copy4(dst, num13, j);
						j = j;
						num13 = num13;
					}
					else
					{
						LZ4Codec.Copy4(dst, num13, j);
						j += 4;
						num13 += 4;
					}
					num11 = j + num9;
					bool flag9 = num11 > num6;
					if (flag9)
					{
						bool flag10 = num11 > num7;
						if (flag10)
						{
							goto Block_14;
						}
						bool flag11 = j < num5;
						if (flag11)
						{
							int num12 = LZ4Codec.SecureCopy(dst, num13, j, num5);
							num13 += num12;
							j += num12;
						}
						while (j < num11)
						{
							dst[j++] = dst[num13++];
						}
						j = num11;
					}
					else
					{
						bool flag12 = j < num11;
						if (flag12)
						{
							LZ4Codec.SecureCopy(dst, num13, j, num11);
						}
						j = num11;
					}
				}
				bool flag13 = num11 > num2;
				if (!flag13)
				{
					bool flag14 = i + num9 != num;
					if (!flag14)
					{
						LZ4Codec.BlockCopy(src, i, dst, j, num9);
						j += num9;
						return j - dst_0;
					}
				}
				Block_9:
				Block_14:;
			}
			return -(i - src_0);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000A5EC File Offset: 0x000087EC
		private static void LZ4HC_Insert_32(LZ4Codec.LZ4HC_Data_Structure ctx, int src_p)
		{
			ushort[] chainTable = ctx.chainTable;
			int[] hashTable = ctx.hashTable;
			int i = ctx.nextToUpdate;
			byte[] src = ctx.src;
			int src_base = ctx.src_base;
			while (i < src_p)
			{
				int num = i;
				int num2 = num - (hashTable[(int)(LZ4Codec.Peek4(src, num) * 2654435761U >> 17)] + src_base);
				bool flag = num2 > 65535;
				if (flag)
				{
					num2 = 65535;
				}
				chainTable[num & 65535] = (ushort)num2;
				hashTable[(int)(LZ4Codec.Peek4(src, num) * 2654435761U >> 17)] = num - src_base;
				i++;
			}
			ctx.nextToUpdate = i;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000A690 File Offset: 0x00008890
		private static int LZ4HC_CommonLength_32(LZ4Codec.LZ4HC_Data_Structure ctx, int p1, int p2)
		{
			int[] debruijn_TABLE_ = LZ4Codec.DEBRUIJN_TABLE_32;
			byte[] src = ctx.src;
			int src_LASTLITERALS = ctx.src_LASTLITERALS;
			int i = p1;
			while (i < src_LASTLITERALS - 3)
			{
				int num = (int)LZ4Codec.Xor4(src, p2, i);
				bool flag = num == 0;
				if (!flag)
				{
					i += debruijn_TABLE_[(int)((uint)((num & -num) * 125613361) >> 27)];
					return i - p1;
				}
				i += 4;
				p2 += 4;
			}
			bool flag2 = i < src_LASTLITERALS - 1 && LZ4Codec.Equal2(src, p2, i);
			if (flag2)
			{
				i += 2;
				p2 += 2;
			}
			bool flag3 = i < src_LASTLITERALS && src[p2] == src[i];
			if (flag3)
			{
				i++;
			}
			return i - p1;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x0000A744 File Offset: 0x00008944
		private static int LZ4HC_InsertAndFindBestMatch_32(LZ4Codec.LZ4HC_Data_Structure ctx, int src_p, ref int src_match)
		{
			ushort[] chainTable = ctx.chainTable;
			int[] hashTable = ctx.hashTable;
			byte[] src = ctx.src;
			int src_base = ctx.src_base;
			int num = 256;
			int num2 = 0;
			int num3 = 0;
			ushort num4 = 0;
			LZ4Codec.LZ4HC_Insert_32(ctx, src_p);
			int num5 = hashTable[(int)(LZ4Codec.Peek4(src, src_p) * 2654435761U >> 17)] + src_base;
			bool flag = num5 >= src_p - 4;
			if (flag)
			{
				bool flag2 = LZ4Codec.Equal4(src, num5, src_p);
				if (flag2)
				{
					num4 = (ushort)(src_p - num5);
					num3 = (num2 = LZ4Codec.LZ4HC_CommonLength_32(ctx, src_p + 4, num5 + 4) + 4);
					src_match = num5;
				}
				num5 -= (int)chainTable[num5 & 65535];
			}
			while (num5 >= src_p - 65535 && num != 0)
			{
				num--;
				bool flag3 = src[num5 + num3] == src[src_p + num3];
				if (flag3)
				{
					bool flag4 = LZ4Codec.Equal4(src, num5, src_p);
					if (flag4)
					{
						int num6 = LZ4Codec.LZ4HC_CommonLength_32(ctx, src_p + 4, num5 + 4) + 4;
						bool flag5 = num6 > num3;
						if (flag5)
						{
							num3 = num6;
							src_match = num5;
						}
					}
				}
				num5 -= (int)chainTable[num5 & 65535];
			}
			bool flag6 = num2 != 0;
			if (flag6)
			{
				int i = src_p;
				int num7 = src_p + num2 - 3;
				while (i < num7 - (int)num4)
				{
					chainTable[i & 65535] = num4;
					i++;
				}
				do
				{
					chainTable[i & 65535] = num4;
					hashTable[(int)(LZ4Codec.Peek4(src, i) * 2654435761U >> 17)] = i - src_base;
					i++;
				}
				while (i < num7);
				ctx.nextToUpdate = num7;
			}
			return num3;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000A8F4 File Offset: 0x00008AF4
		private static int LZ4HC_InsertAndGetWiderMatch_32(LZ4Codec.LZ4HC_Data_Structure ctx, int src_p, int startLimit, int longest, ref int matchpos, ref int startpos)
		{
			ushort[] chainTable = ctx.chainTable;
			int[] hashTable = ctx.hashTable;
			byte[] src = ctx.src;
			int src_base = ctx.src_base;
			int src_LASTLITERALS = ctx.src_LASTLITERALS;
			int[] debruijn_TABLE_ = LZ4Codec.DEBRUIJN_TABLE_32;
			int num = 256;
			int num2 = src_p - startLimit;
			LZ4Codec.LZ4HC_Insert_32(ctx, src_p);
			int num3 = hashTable[(int)(LZ4Codec.Peek4(src, src_p) * 2654435761U >> 17)] + src_base;
			while (num3 >= src_p - 65535 && num != 0)
			{
				num--;
				bool flag = src[startLimit + longest] == src[num3 - num2 + longest];
				if (flag)
				{
					bool flag2 = LZ4Codec.Equal4(src, num3, src_p);
					if (flag2)
					{
						int num4 = num3 + 4;
						int i = src_p + 4;
						int num5 = src_p;
						while (i < src_LASTLITERALS - 3)
						{
							int num6 = (int)LZ4Codec.Xor4(src, num4, i);
							bool flag3 = num6 == 0;
							if (!flag3)
							{
								i += debruijn_TABLE_[(int)((uint)((num6 & -num6) * 125613361) >> 27)];
								IL_0135:
								num4 = num3;
								while (num5 > startLimit && num4 > src_base && src[num5 - 1] == src[num4 - 1])
								{
									num5--;
									num4--;
								}
								bool flag4 = i - num5 > longest;
								if (flag4)
								{
									longest = i - num5;
									matchpos = num4;
									startpos = num5;
								}
								goto IL_018D;
							}
							i += 4;
							num4 += 4;
						}
						bool flag5 = i < src_LASTLITERALS - 1 && LZ4Codec.Equal2(src, num4, i);
						if (flag5)
						{
							i += 2;
							num4 += 2;
						}
						bool flag6 = i < src_LASTLITERALS && src[num4] == src[i];
						if (flag6)
						{
							i++;
							goto IL_0135;
						}
						goto IL_0135;
					}
					IL_018D:;
				}
				num3 -= (int)chainTable[num3 & 65535];
			}
			return longest;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000AAC4 File Offset: 0x00008CC4
		private static int LZ4_encodeSequence_32(LZ4Codec.LZ4HC_Data_Structure ctx, ref int src_p, ref int dst_p, ref int src_anchor, int matchLength, int src_ref, int dst_end)
		{
			byte[] src = ctx.src;
			byte[] dst = ctx.dst;
			int num = src_p - src_anchor;
			int num2 = dst_p;
			dst_p = num2 + 1;
			int num3 = num2;
			bool flag = dst_p + num + 8 + (num >> 8) > dst_end;
			int num4;
			if (flag)
			{
				num4 = 1;
			}
			else
			{
				bool flag2 = num >= 15;
				int i;
				if (flag2)
				{
					dst[num3] = 240;
					for (i = num - 15; i > 254; i -= 255)
					{
						byte[] array = dst;
						num2 = dst_p;
						dst_p = num2 + 1;
						array[num2] = 255;
					}
					byte[] array2 = dst;
					num2 = dst_p;
					dst_p = num2 + 1;
					array2[num2] = (byte)i;
				}
				else
				{
					dst[num3] = (byte)(num << 4);
				}
				bool flag3 = num > 0;
				if (flag3)
				{
					int num5 = dst_p + num;
					src_anchor += LZ4Codec.WildCopy(src, src_anchor, dst, dst_p, num5);
					dst_p = num5;
				}
				LZ4Codec.Poke2(dst, dst_p, (ushort)(src_p - src_ref));
				dst_p += 2;
				i = matchLength - 4;
				bool flag4 = dst_p + 6 + (i >> 8) > dst_end;
				if (flag4)
				{
					num4 = 1;
				}
				else
				{
					bool flag5 = i >= 15;
					if (flag5)
					{
						byte[] array3 = dst;
						int num6 = num3;
						array3[num6] += 15;
						for (i -= 15; i > 509; i -= 510)
						{
							byte[] array4 = dst;
							num2 = dst_p;
							dst_p = num2 + 1;
							array4[num2] = 255;
							byte[] array5 = dst;
							num2 = dst_p;
							dst_p = num2 + 1;
							array5[num2] = 255;
						}
						bool flag6 = i > 254;
						if (flag6)
						{
							i -= 255;
							byte[] array6 = dst;
							num2 = dst_p;
							dst_p = num2 + 1;
							array6[num2] = 255;
						}
						byte[] array7 = dst;
						num2 = dst_p;
						dst_p = num2 + 1;
						array7[num2] = (byte)i;
					}
					else
					{
						byte[] array8 = dst;
						int num7 = num3;
						array8[num7] += (byte)i;
					}
					src_p += matchLength;
					src_anchor = src_p;
					num4 = 0;
				}
			}
			return num4;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000AC98 File Offset: 0x00008E98
		private static int LZ4_compressHCCtx_32(LZ4Codec.LZ4HC_Data_Structure ctx)
		{
			byte[] src = ctx.src;
			byte[] dst = ctx.dst;
			int src_base = ctx.src_base;
			int src_end = ctx.src_end;
			int dst_base = ctx.dst_base;
			int dst_len = ctx.dst_len;
			int dst_end = ctx.dst_end;
			int i = src_base;
			int num = i;
			int num2 = src_end - 12;
			int num3 = dst_base;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			int num7 = 0;
			int num8 = 0;
			for (i++; i < num2; i++)
			{
				int num9 = LZ4Codec.LZ4HC_InsertAndFindBestMatch_32(ctx, i, ref num4);
				bool flag = num9 == 0;
				if (!flag)
				{
					int num10 = i;
					int num11 = num4;
					int num12 = num9;
					int num13;
					for (;;)
					{
						num13 = ((i + num9 < num2) ? LZ4Codec.LZ4HC_InsertAndGetWiderMatch_32(ctx, i + num9 - 2, i + 1, num9, ref num6, ref num5) : num9);
						bool flag2 = num13 == num9;
						if (flag2)
						{
							break;
						}
						bool flag3 = num10 < i;
						if (flag3)
						{
							bool flag4 = num5 < i + num12;
							if (flag4)
							{
								i = num10;
								num4 = num11;
								num9 = num12;
							}
						}
						bool flag5 = num5 - i < 3;
						if (flag5)
						{
							num9 = num13;
							i = num5;
							num4 = num6;
						}
						else
						{
							int num16;
							for (;;)
							{
								bool flag6 = num5 - i < 18;
								if (flag6)
								{
									int num14 = num9;
									bool flag7 = num14 > 18;
									if (flag7)
									{
										num14 = 18;
									}
									bool flag8 = i + num14 > num5 + num13 - 4;
									if (flag8)
									{
										num14 = num5 - i + num13 - 4;
									}
									int num15 = num14 - (num5 - i);
									bool flag9 = num15 > 0;
									if (flag9)
									{
										num5 += num15;
										num6 += num15;
										num13 -= num15;
									}
								}
								num16 = ((num5 + num13 < num2) ? LZ4Codec.LZ4HC_InsertAndGetWiderMatch_32(ctx, num5 + num13 - 3, num5, num13, ref num8, ref num7) : num13);
								bool flag10 = num16 == num13;
								if (flag10)
								{
									goto Block_13;
								}
								bool flag11 = num7 < i + num9 + 3;
								if (flag11)
								{
									bool flag12 = num7 >= i + num9;
									if (flag12)
									{
										break;
									}
									num5 = num7;
									num6 = num8;
									num13 = num16;
								}
								else
								{
									bool flag13 = num5 < i + num9;
									if (flag13)
									{
										bool flag14 = num5 - i < 15;
										if (flag14)
										{
											bool flag15 = num9 > 18;
											if (flag15)
											{
												num9 = 18;
											}
											bool flag16 = i + num9 > num5 + num13 - 4;
											if (flag16)
											{
												num9 = num5 - i + num13 - 4;
											}
											int num17 = num9 - (num5 - i);
											bool flag17 = num17 > 0;
											if (flag17)
											{
												num5 += num17;
												num6 += num17;
												num13 -= num17;
											}
										}
										else
										{
											num9 = num5 - i;
										}
									}
									bool flag18 = LZ4Codec.LZ4_encodeSequence_32(ctx, ref i, ref num3, ref num, num9, num4, dst_end) != 0;
									if (flag18)
									{
										goto Block_27;
									}
									i = num5;
									num4 = num6;
									num9 = num13;
									num5 = num7;
									num6 = num8;
									num13 = num16;
								}
							}
							bool flag19 = num5 < i + num9;
							if (flag19)
							{
								int num18 = i + num9 - num5;
								num5 += num18;
								num6 += num18;
								num13 -= num18;
								bool flag20 = num13 < 4;
								if (flag20)
								{
									num5 = num7;
									num6 = num8;
									num13 = num16;
								}
							}
							bool flag21 = LZ4Codec.LZ4_encodeSequence_32(ctx, ref i, ref num3, ref num, num9, num4, dst_end) != 0;
							if (flag21)
							{
								goto Block_21;
							}
							i = num7;
							num4 = num8;
							num9 = num16;
							num10 = num5;
							num11 = num6;
							num12 = num13;
						}
					}
					bool flag22 = LZ4Codec.LZ4_encodeSequence_32(ctx, ref i, ref num3, ref num, num9, num4, dst_end) != 0;
					if (flag22)
					{
						return 0;
					}
					continue;
					Block_13:
					bool flag23 = num5 < i + num9;
					if (flag23)
					{
						num9 = num5 - i;
					}
					bool flag24 = LZ4Codec.LZ4_encodeSequence_32(ctx, ref i, ref num3, ref num, num9, num4, dst_end) != 0;
					if (flag24)
					{
						return 0;
					}
					i = num5;
					bool flag25 = LZ4Codec.LZ4_encodeSequence_32(ctx, ref i, ref num3, ref num, num13, num6, dst_end) != 0;
					if (flag25)
					{
						return 0;
					}
					continue;
					Block_21:
					return 0;
					Block_27:
					return 0;
				}
			}
			int j = src_end - num;
			bool flag26 = (long)(num3 - dst_base + j + 1 + (j + 255 - 15) / 255) > (long)((ulong)dst_len);
			if (flag26)
			{
				return 0;
			}
			bool flag27 = j >= 15;
			if (flag27)
			{
				dst[num3++] = 240;
				for (j -= 15; j > 254; j -= 255)
				{
					dst[num3++] = byte.MaxValue;
				}
				dst[num3++] = (byte)j;
			}
			else
			{
				dst[num3++] = (byte)(j << 4);
			}
			LZ4Codec.BlockCopy(src, num, dst, num3, src_end - num);
			num3 += src_end - num;
			return num3 - dst_base;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000B14C File Offset: 0x0000934C
		private static int LZ4_compressCtx_safe64(int[] hash_table, byte[] src, byte[] dst, int src_0, int dst_0, int src_len, int dst_maxlen)
		{
			int[] debruijn_TABLE_ = LZ4Codec.DEBRUIJN_TABLE_64;
			int num = src_0;
			int num2 = src_0 + src_len;
			int num3 = num2 - 12;
			int num4 = dst_0;
			int num5 = num4 + dst_maxlen;
			int num6 = num2 - 5;
			int num7 = num6 - 1;
			int num8 = num6 - 3;
			int num9 = num6 - 7;
			int num10 = num5 - 6;
			int num11 = num5 - 8;
			bool flag = src_len < 13;
			if (!flag)
			{
				hash_table[(int)(LZ4Codec.Peek4(src, src_0) * 2654435761U >> 20)] = src_0 - src_0;
				int i = src_0 + 1;
				uint num12 = LZ4Codec.Peek4(src, i) * 2654435761U >> 20;
				for (;;)
				{
					int num13 = 67;
					int num14 = i;
					int num17;
					do
					{
						uint num15 = num12;
						int num16 = num13++ >> 6;
						i = num14;
						num14 = i + num16;
						bool flag2 = num14 > num3;
						if (flag2)
						{
							goto Block_2;
						}
						num12 = LZ4Codec.Peek4(src, num14) * 2654435761U >> 20;
						num17 = src_0 + hash_table[(int)num15];
						hash_table[(int)num15] = i - src_0;
					}
					while (num17 < i - 65535 || !LZ4Codec.Equal4(src, num17, i));
					while (i > num && num17 > src_0 && src[i - 1] == src[num17 - 1])
					{
						i--;
						num17--;
					}
					int j = i - num;
					int num18 = num4++;
					bool flag3 = num4 + j + (j >> 8) > num11;
					if (flag3)
					{
						goto Block_8;
					}
					bool flag4 = j >= 15;
					if (!flag4)
					{
						dst[num18] = (byte)(j << 4);
						goto IL_01EF;
					}
					int num19 = j - 15;
					dst[num18] = 240;
					bool flag5 = num19 > 254;
					if (!flag5)
					{
						dst[num4++] = (byte)num19;
						goto IL_01EF;
					}
					do
					{
						dst[num4++] = byte.MaxValue;
						num19 -= 255;
					}
					while (num19 > 254);
					dst[num4++] = (byte)num19;
					LZ4Codec.BlockCopy(src, num, dst, num4, j);
					num4 += j;
					for (;;)
					{
						IL_0212:
						LZ4Codec.Poke2(dst, num4, (ushort)(i - num17));
						num4 += 2;
						i += 4;
						num17 += 4;
						num = i;
						while (i < num9)
						{
							long num20 = (long)LZ4Codec.Xor8(src, num17, i);
							bool flag6 = num20 == 0L;
							if (flag6)
							{
								i += 8;
								num17 += 8;
							}
							else
							{
								i += debruijn_TABLE_[(int)(checked((IntPtr)((ulong)(unchecked((num20 & -num20) * 151050438428048703L)) >> 58)))];
								IL_02E2:
								j = i - num;
								bool flag7 = num4 + (j >> 8) > num10;
								if (flag7)
								{
									goto Block_20;
								}
								bool flag8 = j >= 15;
								if (flag8)
								{
									int num21 = num18;
									dst[num21] += 15;
									for (j -= 15; j > 509; j -= 510)
									{
										dst[num4++] = byte.MaxValue;
										dst[num4++] = byte.MaxValue;
									}
									bool flag9 = j > 254;
									if (flag9)
									{
										j -= 255;
										dst[num4++] = byte.MaxValue;
									}
									dst[num4++] = (byte)j;
								}
								else
								{
									int num22 = num18;
									dst[num22] += (byte)j;
								}
								bool flag10 = i > num3;
								if (flag10)
								{
									goto Block_24;
								}
								hash_table[(int)(LZ4Codec.Peek4(src, i - 2) * 2654435761U >> 20)] = i - 2 - src_0;
								uint num15 = LZ4Codec.Peek4(src, i) * 2654435761U >> 20;
								num17 = src_0 + hash_table[(int)num15];
								hash_table[(int)num15] = i - src_0;
								bool flag11 = num17 > i - 65536 && LZ4Codec.Equal4(src, num17, i);
								if (flag11)
								{
									num18 = num4++;
									dst[num18] = 0;
									goto IL_0212;
								}
								goto IL_042D;
							}
						}
						bool flag12 = i < num8 && LZ4Codec.Equal4(src, num17, i);
						if (flag12)
						{
							i += 4;
							num17 += 4;
						}
						bool flag13 = i < num7 && LZ4Codec.Equal2(src, num17, i);
						if (flag13)
						{
							i += 2;
							num17 += 2;
						}
						bool flag14 = i < num6 && src[num17] == src[i];
						if (flag14)
						{
							i++;
							goto IL_02E2;
						}
						goto IL_02E2;
					}
					IL_042D:
					num = i++;
					num12 = LZ4Codec.Peek4(src, i) * 2654435761U >> 20;
					continue;
					IL_01EF:
					bool flag15 = j > 0;
					if (flag15)
					{
						int num23 = num4 + j;
						LZ4Codec.WildCopy(src, num, dst, num4, num23);
						num4 = num23;
						goto IL_0212;
					}
					goto IL_0212;
				}
				Block_2:
				goto IL_044F;
				Block_8:
				return 0;
				Block_20:
				return 0;
				Block_24:
				num = i;
			}
			IL_044F:
			int k = num2 - num;
			bool flag16 = num4 + k + 1 + (k + 255 - 15) / 255 > num5;
			int num24;
			if (flag16)
			{
				num24 = 0;
			}
			else
			{
				bool flag17 = k >= 15;
				if (flag17)
				{
					dst[num4++] = 240;
					for (k -= 15; k > 254; k -= 255)
					{
						dst[num4++] = byte.MaxValue;
					}
					dst[num4++] = (byte)k;
				}
				else
				{
					dst[num4++] = (byte)(k << 4);
				}
				LZ4Codec.BlockCopy(src, num, dst, num4, num2 - num);
				num4 += num2 - num;
				num24 = num4 - dst_0;
			}
			return num24;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000B670 File Offset: 0x00009870
		private static int LZ4_compress64kCtx_safe64(ushort[] hash_table, byte[] src, byte[] dst, int src_0, int dst_0, int src_len, int dst_maxlen)
		{
			int[] debruijn_TABLE_ = LZ4Codec.DEBRUIJN_TABLE_64;
			int num = src_0;
			int num2 = src_0 + src_len;
			int num3 = num2 - 12;
			int num4 = dst_0;
			int num5 = num4 + dst_maxlen;
			int num6 = num2 - 5;
			int num7 = num6 - 1;
			int num8 = num6 - 3;
			int num9 = num6 - 7;
			int num10 = num5 - 6;
			int num11 = num5 - 8;
			bool flag = src_len < 13;
			if (!flag)
			{
				int i = src_0 + 1;
				uint num12 = LZ4Codec.Peek4(src, i) * 2654435761U >> 19;
				for (;;)
				{
					int num13 = 67;
					int num14 = i;
					int num17;
					do
					{
						uint num15 = num12;
						int num16 = num13++ >> 6;
						i = num14;
						num14 = i + num16;
						bool flag2 = num14 > num3;
						if (flag2)
						{
							goto Block_2;
						}
						num12 = LZ4Codec.Peek4(src, num14) * 2654435761U >> 19;
						num17 = src_0 + (int)hash_table[(int)num15];
						hash_table[(int)num15] = (ushort)(i - src_0);
					}
					while (!LZ4Codec.Equal4(src, num17, i));
					while (i > num && num17 > src_0 && src[i - 1] == src[num17 - 1])
					{
						i--;
						num17--;
					}
					int num18 = i - num;
					int num19 = num4++;
					bool flag3 = num4 + num18 + (num18 >> 8) > num11;
					if (flag3)
					{
						goto Block_7;
					}
					bool flag4 = num18 >= 15;
					if (!flag4)
					{
						dst[num19] = (byte)(num18 << 4);
						goto IL_01CC;
					}
					int j = num18 - 15;
					dst[num19] = 240;
					bool flag5 = j > 254;
					if (!flag5)
					{
						dst[num4++] = (byte)j;
						goto IL_01CC;
					}
					do
					{
						dst[num4++] = byte.MaxValue;
						j -= 255;
					}
					while (j > 254);
					dst[num4++] = (byte)j;
					LZ4Codec.BlockCopy(src, num, dst, num4, num18);
					num4 += num18;
					for (;;)
					{
						IL_01EE:
						LZ4Codec.Poke2(dst, num4, (ushort)(i - num17));
						num4 += 2;
						i += 4;
						num17 += 4;
						num = i;
						while (i < num9)
						{
							long num20 = (long)LZ4Codec.Xor8(src, num17, i);
							bool flag6 = num20 == 0L;
							if (flag6)
							{
								i += 8;
								num17 += 8;
							}
							else
							{
								i += debruijn_TABLE_[(int)(checked((IntPtr)((ulong)(unchecked((num20 & -num20) * 151050438428048703L)) >> 58)))];
								IL_02BD:
								j = i - num;
								bool flag7 = num4 + (j >> 8) > num10;
								if (flag7)
								{
									goto Block_19;
								}
								bool flag8 = j >= 15;
								if (flag8)
								{
									int num21 = num19;
									dst[num21] += 15;
									for (j -= 15; j > 509; j -= 510)
									{
										dst[num4++] = byte.MaxValue;
										dst[num4++] = byte.MaxValue;
									}
									bool flag9 = j > 254;
									if (flag9)
									{
										j -= 255;
										dst[num4++] = byte.MaxValue;
									}
									dst[num4++] = (byte)j;
								}
								else
								{
									int num22 = num19;
									dst[num22] += (byte)j;
								}
								bool flag10 = i > num3;
								if (flag10)
								{
									goto Block_23;
								}
								hash_table[(int)(LZ4Codec.Peek4(src, i - 2) * 2654435761U >> 19)] = (ushort)(i - 2 - src_0);
								uint num15 = LZ4Codec.Peek4(src, i) * 2654435761U >> 19;
								num17 = src_0 + (int)hash_table[(int)num15];
								hash_table[(int)num15] = (ushort)(i - src_0);
								bool flag11 = LZ4Codec.Equal4(src, num17, i);
								if (flag11)
								{
									num19 = num4++;
									dst[num19] = 0;
									goto IL_01EE;
								}
								goto IL_03FD;
							}
						}
						bool flag12 = i < num8 && LZ4Codec.Equal4(src, num17, i);
						if (flag12)
						{
							i += 4;
							num17 += 4;
						}
						bool flag13 = i < num7 && LZ4Codec.Equal2(src, num17, i);
						if (flag13)
						{
							i += 2;
							num17 += 2;
						}
						bool flag14 = i < num6 && src[num17] == src[i];
						if (flag14)
						{
							i++;
							goto IL_02BD;
						}
						goto IL_02BD;
					}
					IL_03FD:
					num = i++;
					num12 = LZ4Codec.Peek4(src, i) * 2654435761U >> 19;
					continue;
					IL_01CC:
					bool flag15 = num18 > 0;
					if (flag15)
					{
						int num23 = num4 + num18;
						LZ4Codec.WildCopy(src, num, dst, num4, num23);
						num4 = num23;
						goto IL_01EE;
					}
					goto IL_01EE;
				}
				Block_2:
				goto IL_041E;
				Block_7:
				return 0;
				Block_19:
				return 0;
				Block_23:
				num = i;
			}
			IL_041E:
			int k = num2 - num;
			bool flag16 = num4 + k + 1 + (k - 15 + 255) / 255 > num5;
			int num24;
			if (flag16)
			{
				num24 = 0;
			}
			else
			{
				bool flag17 = k >= 15;
				if (flag17)
				{
					dst[num4++] = 240;
					for (k -= 15; k > 254; k -= 255)
					{
						dst[num4++] = byte.MaxValue;
					}
					dst[num4++] = (byte)k;
				}
				else
				{
					dst[num4++] = (byte)(k << 4);
				}
				LZ4Codec.BlockCopy(src, num, dst, num4, num2 - num);
				num4 += num2 - num;
				num24 = num4 - dst_0;
			}
			return num24;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000BB60 File Offset: 0x00009D60
		private static int LZ4_uncompress_safe64(byte[] src, byte[] dst, int src_0, int dst_0, int dst_len)
		{
			int[] decoder_TABLE_ = LZ4Codec.DECODER_TABLE_32;
			int[] decoder_TABLE_2 = LZ4Codec.DECODER_TABLE_64;
			int num = src_0;
			int i = dst_0;
			int num2 = i + dst_len;
			int num3 = num2 - 5;
			int num4 = num2 - 8;
			int num5 = num2 - 8 - 4;
			int num7;
			int num9;
			for (;;)
			{
				uint num6 = (uint)src[num++];
				bool flag = (num7 = (int)((byte)(num6 >> 4))) == 15;
				if (flag)
				{
					int num8;
					while ((num8 = (int)src[num++]) == 255)
					{
						num7 += 255;
					}
					num7 += num8;
				}
				num9 = i + num7;
				bool flag2 = num9 > num4;
				if (flag2)
				{
					break;
				}
				bool flag3 = i < num9;
				if (flag3)
				{
					int num10 = LZ4Codec.WildCopy(src, num, dst, i, num9);
					num += num10;
					i += num10;
				}
				num -= i - num9;
				i = num9;
				int num11 = num9 - (int)LZ4Codec.Peek2(src, num);
				num += 2;
				bool flag4 = num11 < dst_0;
				if (flag4)
				{
					goto Block_6;
				}
				bool flag5 = (num7 = (int)((byte)(num6 & 15U))) == 15;
				if (flag5)
				{
					while (src[num] == 255)
					{
						num++;
						num7 += 255;
					}
					num7 += (int)src[num++];
				}
				bool flag6 = i - num11 < 8;
				if (flag6)
				{
					int num12 = decoder_TABLE_2[i - num11];
					dst[i] = dst[num11];
					dst[i + 1] = dst[num11 + 1];
					dst[i + 2] = dst[num11 + 2];
					dst[i + 3] = dst[num11 + 3];
					i += 4;
					num11 += 4;
					num11 -= decoder_TABLE_[i - num11];
					LZ4Codec.Copy4(dst, num11, i);
					i += 4;
					num11 -= num12;
				}
				else
				{
					LZ4Codec.Copy8(dst, num11, i);
					i += 8;
					num11 += 8;
				}
				num9 = i + num7 - 4;
				bool flag7 = num9 > num5;
				if (flag7)
				{
					bool flag8 = num9 > num3;
					if (flag8)
					{
						goto Block_11;
					}
					bool flag9 = i < num4;
					if (flag9)
					{
						int num10 = LZ4Codec.SecureCopy(dst, num11, i, num4);
						num11 += num10;
						i += num10;
					}
					while (i < num9)
					{
						dst[i++] = dst[num11++];
					}
					i = num9;
				}
				else
				{
					bool flag10 = i < num9;
					if (flag10)
					{
						LZ4Codec.SecureCopy(dst, num11, i, num9);
					}
					i = num9;
				}
			}
			bool flag11 = num9 != num2;
			if (!flag11)
			{
				LZ4Codec.BlockCopy(src, num, dst, i, num7);
				num += num7;
				return num - src_0;
			}
			Block_6:
			Block_11:
			return -(num - src_0);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000BDF8 File Offset: 0x00009FF8
		private static int LZ4_uncompress_unknownOutputSize_safe64(byte[] src, byte[] dst, int src_0, int dst_0, int src_len, int dst_maxlen)
		{
			int[] decoder_TABLE_ = LZ4Codec.DECODER_TABLE_32;
			int[] decoder_TABLE_2 = LZ4Codec.DECODER_TABLE_64;
			int i = src_0;
			int num = i + src_len;
			int j = dst_0;
			int num2 = j + dst_maxlen;
			int num3 = num - 8;
			int num4 = num - 6;
			int num5 = num2 - 8;
			int num6 = num2 - 12;
			int num7 = num2 - 5;
			int num8 = num2 - 12;
			bool flag = i == num;
			if (!flag)
			{
				int num9;
				int num11;
				for (;;)
				{
					byte b = src[i++];
					bool flag2 = (num9 = b >> 4) == 15;
					if (flag2)
					{
						int num10 = 255;
						while (i < num && num10 == 255)
						{
							num9 += (num10 = (int)src[i++]);
						}
					}
					num11 = j + num9;
					bool flag3 = num11 > num8 || i + num9 > num3;
					if (flag3)
					{
						break;
					}
					bool flag4 = j < num11;
					if (flag4)
					{
						int num12 = LZ4Codec.WildCopy(src, i, dst, j, num11);
						i += num12;
						j += num12;
					}
					i -= j - num11;
					j = num11;
					int num13 = num11 - (int)LZ4Codec.Peek2(src, i);
					i += 2;
					bool flag5 = num13 < dst_0;
					if (flag5)
					{
						goto Block_9;
					}
					bool flag6 = (num9 = (int)(b & 15)) == 15;
					if (flag6)
					{
						while (i < num4)
						{
							int num14 = (int)src[i++];
							num9 += num14;
							bool flag7 = num14 == 255;
							if (!flag7)
							{
								break;
							}
						}
					}
					bool flag8 = j - num13 < 8;
					if (flag8)
					{
						int num15 = decoder_TABLE_2[j - num13];
						dst[j] = dst[num13];
						dst[j + 1] = dst[num13 + 1];
						dst[j + 2] = dst[num13 + 2];
						dst[j + 3] = dst[num13 + 3];
						j += 4;
						num13 += 4;
						num13 -= decoder_TABLE_[j - num13];
						LZ4Codec.Copy4(dst, num13, j);
						j += 4;
						num13 -= num15;
					}
					else
					{
						LZ4Codec.Copy8(dst, num13, j);
						j += 8;
						num13 += 8;
					}
					num11 = j + num9 - 4;
					bool flag9 = num11 > num6;
					if (flag9)
					{
						bool flag10 = num11 > num7;
						if (flag10)
						{
							goto Block_14;
						}
						bool flag11 = j < num5;
						if (flag11)
						{
							int num12 = LZ4Codec.SecureCopy(dst, num13, j, num5);
							num13 += num12;
							j += num12;
						}
						while (j < num11)
						{
							dst[j++] = dst[num13++];
						}
						j = num11;
					}
					else
					{
						bool flag12 = j < num11;
						if (flag12)
						{
							LZ4Codec.SecureCopy(dst, num13, j, num11);
						}
						j = num11;
					}
				}
				bool flag13 = num11 > num2;
				if (!flag13)
				{
					bool flag14 = i + num9 != num;
					if (!flag14)
					{
						LZ4Codec.BlockCopy(src, i, dst, j, num9);
						j += num9;
						return j - dst_0;
					}
				}
				Block_9:
				Block_14:;
			}
			return -(i - src_0);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000C0DC File Offset: 0x0000A2DC
		private static void LZ4HC_Insert_64(LZ4Codec.LZ4HC_Data_Structure ctx, int src_p)
		{
			ushort[] chainTable = ctx.chainTable;
			int[] hashTable = ctx.hashTable;
			byte[] src = ctx.src;
			int src_base = ctx.src_base;
			int i;
			for (i = ctx.nextToUpdate; i < src_p; i++)
			{
				int num = i;
				int num2 = num - (hashTable[(int)(LZ4Codec.Peek4(src, num) * 2654435761U >> 17)] + src_base);
				bool flag = num2 > 65535;
				if (flag)
				{
					num2 = 65535;
				}
				chainTable[num & 65535] = (ushort)num2;
				hashTable[(int)(LZ4Codec.Peek4(src, num) * 2654435761U >> 17)] = num - src_base;
			}
			ctx.nextToUpdate = i;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000C184 File Offset: 0x0000A384
		private static int LZ4HC_CommonLength_64(LZ4Codec.LZ4HC_Data_Structure ctx, int p1, int p2)
		{
			int[] debruijn_TABLE_ = LZ4Codec.DEBRUIJN_TABLE_64;
			byte[] src = ctx.src;
			int src_LASTLITERALS = ctx.src_LASTLITERALS;
			int i = p1;
			while (i < src_LASTLITERALS - 7)
			{
				long num = (long)LZ4Codec.Xor8(src, p2, i);
				bool flag = num == 0L;
				if (!flag)
				{
					i += debruijn_TABLE_[(int)(checked((IntPtr)((ulong)(unchecked((num & -num) * 151050438428048703L)) >> 58)))];
					return i - p1;
				}
				i += 8;
				p2 += 8;
			}
			bool flag2 = i < src_LASTLITERALS - 3 && LZ4Codec.Equal4(src, p2, i);
			if (flag2)
			{
				i += 4;
				p2 += 4;
			}
			bool flag3 = i < src_LASTLITERALS - 1 && LZ4Codec.Equal2(src, p2, i);
			if (flag3)
			{
				i += 2;
				p2 += 2;
			}
			bool flag4 = i < src_LASTLITERALS && src[p2] == src[i];
			if (flag4)
			{
				i++;
			}
			return i - p1;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000C260 File Offset: 0x0000A460
		private static int LZ4HC_InsertAndFindBestMatch_64(LZ4Codec.LZ4HC_Data_Structure ctx, int src_p, ref int matchpos)
		{
			ushort[] chainTable = ctx.chainTable;
			int[] hashTable = ctx.hashTable;
			byte[] src = ctx.src;
			int src_base = ctx.src_base;
			int num = 256;
			int num2 = 0;
			int num3 = 0;
			ushort num4 = 0;
			LZ4Codec.LZ4HC_Insert_64(ctx, src_p);
			int num5 = hashTable[(int)(LZ4Codec.Peek4(src, src_p) * 2654435761U >> 17)] + src_base;
			bool flag = num5 >= src_p - 4;
			if (flag)
			{
				bool flag2 = LZ4Codec.Equal4(src, num5, src_p);
				if (flag2)
				{
					num4 = (ushort)(src_p - num5);
					num3 = (num2 = LZ4Codec.LZ4HC_CommonLength_64(ctx, src_p + 4, num5 + 4) + 4);
					matchpos = num5;
				}
				num5 -= (int)chainTable[num5 & 65535];
			}
			while (num5 >= src_p - 65535 && num != 0)
			{
				num--;
				bool flag3 = src[num5 + num3] == src[src_p + num3];
				if (flag3)
				{
					bool flag4 = LZ4Codec.Equal4(src, num5, src_p);
					if (flag4)
					{
						int num6 = LZ4Codec.LZ4HC_CommonLength_64(ctx, src_p + 4, num5 + 4) + 4;
						bool flag5 = num6 > num3;
						if (flag5)
						{
							num3 = num6;
							matchpos = num5;
						}
					}
				}
				num5 -= (int)chainTable[num5 & 65535];
			}
			bool flag6 = num2 != 0;
			if (flag6)
			{
				int i = src_p;
				int num7 = src_p + num2 - 3;
				while (i < num7 - (int)num4)
				{
					chainTable[i & 65535] = num4;
					i++;
				}
				do
				{
					chainTable[i & 65535] = num4;
					hashTable[(int)(LZ4Codec.Peek4(src, i) * 2654435761U >> 17)] = i - src_base;
					i++;
				}
				while (i < num7);
				ctx.nextToUpdate = num7;
			}
			return num3;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000C410 File Offset: 0x0000A610
		private static int LZ4HC_InsertAndGetWiderMatch_64(LZ4Codec.LZ4HC_Data_Structure ctx, int src_p, int startLimit, int longest, ref int matchpos, ref int startpos)
		{
			int[] debruijn_TABLE_ = LZ4Codec.DEBRUIJN_TABLE_64;
			ushort[] chainTable = ctx.chainTable;
			int[] hashTable = ctx.hashTable;
			byte[] src = ctx.src;
			int src_base = ctx.src_base;
			int src_LASTLITERALS = ctx.src_LASTLITERALS;
			int num = 256;
			int num2 = src_p - startLimit;
			LZ4Codec.LZ4HC_Insert_64(ctx, src_p);
			int num3 = hashTable[(int)(LZ4Codec.Peek4(src, src_p) * 2654435761U >> 17)] + src_base;
			while (num3 >= src_p - 65535 && num != 0)
			{
				num--;
				bool flag = src[startLimit + longest] == src[num3 - num2 + longest];
				if (flag)
				{
					bool flag2 = LZ4Codec.Equal4(src, num3, src_p);
					if (flag2)
					{
						int num4 = num3 + 4;
						int i = src_p + 4;
						int num5 = src_p;
						while (i < src_LASTLITERALS - 7)
						{
							long num6 = (long)LZ4Codec.Xor8(src, num4, i);
							bool flag3 = num6 == 0L;
							if (!flag3)
							{
								i += debruijn_TABLE_[(int)(checked((IntPtr)((ulong)(unchecked((num6 & -num6) * 151050438428048703L)) >> 58)))];
								IL_0164:
								num4 = num3;
								while (num5 > startLimit && num4 > src_base && src[num5 - 1] == src[num4 - 1])
								{
									num5--;
									num4--;
								}
								bool flag4 = i - num5 > longest;
								if (flag4)
								{
									longest = i - num5;
									matchpos = num4;
									startpos = num5;
								}
								goto IL_01BD;
							}
							i += 8;
							num4 += 8;
						}
						bool flag5 = i < src_LASTLITERALS - 3 && LZ4Codec.Equal4(src, num4, i);
						if (flag5)
						{
							i += 4;
							num4 += 4;
						}
						bool flag6 = i < src_LASTLITERALS - 1 && LZ4Codec.Equal2(src, num4, i);
						if (flag6)
						{
							i += 2;
							num4 += 2;
						}
						bool flag7 = i < src_LASTLITERALS && src[num4] == src[i];
						if (flag7)
						{
							i++;
							goto IL_0164;
						}
						goto IL_0164;
					}
					IL_01BD:;
				}
				num3 -= (int)chainTable[num3 & 65535];
			}
			return longest;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000C610 File Offset: 0x0000A810
		private static int LZ4_encodeSequence_64(LZ4Codec.LZ4HC_Data_Structure ctx, ref int src_p, ref int dst_p, ref int src_anchor, int matchLength, int src_ref)
		{
			byte[] src = ctx.src;
			byte[] dst = ctx.dst;
			int dst_end = ctx.dst_end;
			int num = src_p - src_anchor;
			int num2 = dst_p;
			dst_p = num2 + 1;
			int num3 = num2;
			bool flag = dst_p + num + 8 + (num >> 8) > dst_end;
			int num4;
			if (flag)
			{
				num4 = 1;
			}
			else
			{
				bool flag2 = num >= 15;
				int i;
				if (flag2)
				{
					dst[num3] = 240;
					for (i = num - 15; i > 254; i -= 255)
					{
						byte[] array = dst;
						num2 = dst_p;
						dst_p = num2 + 1;
						array[num2] = 255;
					}
					byte[] array2 = dst;
					num2 = dst_p;
					dst_p = num2 + 1;
					array2[num2] = (byte)i;
				}
				else
				{
					dst[num3] = (byte)(num << 4);
				}
				bool flag3 = num > 0;
				if (flag3)
				{
					int num5 = dst_p + num;
					src_anchor += LZ4Codec.WildCopy(src, src_anchor, dst, dst_p, num5);
					dst_p = num5;
				}
				LZ4Codec.Poke2(dst, dst_p, (ushort)(src_p - src_ref));
				dst_p += 2;
				i = matchLength - 4;
				bool flag4 = dst_p + 6 + (i >> 8) > dst_end;
				if (flag4)
				{
					num4 = 1;
				}
				else
				{
					bool flag5 = i >= 15;
					if (flag5)
					{
						byte[] array3 = dst;
						int num6 = num3;
						array3[num6] += 15;
						for (i -= 15; i > 509; i -= 510)
						{
							byte[] array4 = dst;
							num2 = dst_p;
							dst_p = num2 + 1;
							array4[num2] = 255;
							byte[] array5 = dst;
							num2 = dst_p;
							dst_p = num2 + 1;
							array5[num2] = 255;
						}
						bool flag6 = i > 254;
						if (flag6)
						{
							i -= 255;
							byte[] array6 = dst;
							num2 = dst_p;
							dst_p = num2 + 1;
							array6[num2] = 255;
						}
						byte[] array7 = dst;
						num2 = dst_p;
						dst_p = num2 + 1;
						array7[num2] = (byte)i;
					}
					else
					{
						byte[] array8 = dst;
						int num7 = num3;
						array8[num7] += (byte)i;
					}
					src_p += matchLength;
					src_anchor = src_p;
					num4 = 0;
				}
			}
			return num4;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000C7F4 File Offset: 0x0000A9F4
		private static int LZ4_compressHCCtx_64(LZ4Codec.LZ4HC_Data_Structure ctx)
		{
			byte[] src = ctx.src;
			int i = ctx.src_base;
			int src_end = ctx.src_end;
			int dst_base = ctx.dst_base;
			int num = i;
			int num2 = src_end - 12;
			byte[] dst = ctx.dst;
			int dst_len = ctx.dst_len;
			int num3 = ctx.dst_base;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			int num7 = 0;
			int num8 = 0;
			for (i++; i < num2; i++)
			{
				int num9 = LZ4Codec.LZ4HC_InsertAndFindBestMatch_64(ctx, i, ref num4);
				bool flag = num9 == 0;
				if (!flag)
				{
					int num10 = i;
					int num11 = num4;
					int num12 = num9;
					int num13;
					for (;;)
					{
						num13 = ((i + num9 < num2) ? LZ4Codec.LZ4HC_InsertAndGetWiderMatch_64(ctx, i + num9 - 2, i + 1, num9, ref num6, ref num5) : num9);
						bool flag2 = num13 == num9;
						if (flag2)
						{
							break;
						}
						bool flag3 = num10 < i;
						if (flag3)
						{
							bool flag4 = num5 < i + num12;
							if (flag4)
							{
								i = num10;
								num4 = num11;
								num9 = num12;
							}
						}
						bool flag5 = num5 - i < 3;
						if (flag5)
						{
							num9 = num13;
							i = num5;
							num4 = num6;
						}
						else
						{
							int num16;
							for (;;)
							{
								bool flag6 = num5 - i < 18;
								if (flag6)
								{
									int num14 = num9;
									bool flag7 = num14 > 18;
									if (flag7)
									{
										num14 = 18;
									}
									bool flag8 = i + num14 > num5 + num13 - 4;
									if (flag8)
									{
										num14 = num5 - i + num13 - 4;
									}
									int num15 = num14 - (num5 - i);
									bool flag9 = num15 > 0;
									if (flag9)
									{
										num5 += num15;
										num6 += num15;
										num13 -= num15;
									}
								}
								num16 = ((num5 + num13 < num2) ? LZ4Codec.LZ4HC_InsertAndGetWiderMatch_64(ctx, num5 + num13 - 3, num5, num13, ref num8, ref num7) : num13);
								bool flag10 = num16 == num13;
								if (flag10)
								{
									goto Block_13;
								}
								bool flag11 = num7 < i + num9 + 3;
								if (flag11)
								{
									bool flag12 = num7 >= i + num9;
									if (flag12)
									{
										break;
									}
									num5 = num7;
									num6 = num8;
									num13 = num16;
								}
								else
								{
									bool flag13 = num5 < i + num9;
									if (flag13)
									{
										bool flag14 = num5 - i < 15;
										if (flag14)
										{
											bool flag15 = num9 > 18;
											if (flag15)
											{
												num9 = 18;
											}
											bool flag16 = i + num9 > num5 + num13 - 4;
											if (flag16)
											{
												num9 = num5 - i + num13 - 4;
											}
											int num17 = num9 - (num5 - i);
											bool flag17 = num17 > 0;
											if (flag17)
											{
												num5 += num17;
												num6 += num17;
												num13 -= num17;
											}
										}
										else
										{
											num9 = num5 - i;
										}
									}
									bool flag18 = LZ4Codec.LZ4_encodeSequence_64(ctx, ref i, ref num3, ref num, num9, num4) != 0;
									if (flag18)
									{
										goto Block_27;
									}
									i = num5;
									num4 = num6;
									num9 = num13;
									num5 = num7;
									num6 = num8;
									num13 = num16;
								}
							}
							bool flag19 = num5 < i + num9;
							if (flag19)
							{
								int num18 = i + num9 - num5;
								num5 += num18;
								num6 += num18;
								num13 -= num18;
								bool flag20 = num13 < 4;
								if (flag20)
								{
									num5 = num7;
									num6 = num8;
									num13 = num16;
								}
							}
							bool flag21 = LZ4Codec.LZ4_encodeSequence_64(ctx, ref i, ref num3, ref num, num9, num4) != 0;
							if (flag21)
							{
								goto Block_21;
							}
							i = num7;
							num4 = num8;
							num9 = num16;
							num10 = num5;
							num11 = num6;
							num12 = num13;
						}
					}
					bool flag22 = LZ4Codec.LZ4_encodeSequence_64(ctx, ref i, ref num3, ref num, num9, num4) != 0;
					if (flag22)
					{
						return 0;
					}
					continue;
					Block_13:
					bool flag23 = num5 < i + num9;
					if (flag23)
					{
						num9 = num5 - i;
					}
					bool flag24 = LZ4Codec.LZ4_encodeSequence_64(ctx, ref i, ref num3, ref num, num9, num4) != 0;
					if (flag24)
					{
						return 0;
					}
					i = num5;
					bool flag25 = LZ4Codec.LZ4_encodeSequence_64(ctx, ref i, ref num3, ref num, num13, num6) != 0;
					if (flag25)
					{
						return 0;
					}
					continue;
					Block_21:
					return 0;
					Block_27:
					return 0;
				}
			}
			int j = src_end - num;
			bool flag26 = (long)(num3 - dst_base + j + 1 + (j + 255 - 15) / 255) > (long)((ulong)dst_len);
			if (flag26)
			{
				return 0;
			}
			bool flag27 = j >= 15;
			if (flag27)
			{
				dst[num3++] = 240;
				for (j -= 15; j > 254; j -= 255)
				{
					dst[num3++] = byte.MaxValue;
				}
				dst[num3++] = (byte)j;
			}
			else
			{
				dst[num3++] = (byte)(j << 4);
			}
			LZ4Codec.BlockCopy(src, num, dst, num3, src_end - num);
			num3 += src_end - num;
			return num3 - dst_base;
		}

		// Token: 0x04000108 RID: 264
		private const int MEMORY_USAGE = 14;

		// Token: 0x04000109 RID: 265
		private const int NOTCOMPRESSIBLE_DETECTIONLEVEL = 6;

		// Token: 0x0400010A RID: 266
		private const int BLOCK_COPY_LIMIT = 16;

		// Token: 0x0400010B RID: 267
		private const int MINMATCH = 4;

		// Token: 0x0400010C RID: 268
		private const int SKIPSTRENGTH = 6;

		// Token: 0x0400010D RID: 269
		private const int COPYLENGTH = 8;

		// Token: 0x0400010E RID: 270
		private const int LASTLITERALS = 5;

		// Token: 0x0400010F RID: 271
		private const int MFLIMIT = 12;

		// Token: 0x04000110 RID: 272
		private const int MINLENGTH = 13;

		// Token: 0x04000111 RID: 273
		private const int MAXD_LOG = 16;

		// Token: 0x04000112 RID: 274
		private const int MAXD = 65536;

		// Token: 0x04000113 RID: 275
		private const int MAXD_MASK = 65535;

		// Token: 0x04000114 RID: 276
		private const int MAX_DISTANCE = 65535;

		// Token: 0x04000115 RID: 277
		private const int ML_BITS = 4;

		// Token: 0x04000116 RID: 278
		private const int ML_MASK = 15;

		// Token: 0x04000117 RID: 279
		private const int RUN_BITS = 4;

		// Token: 0x04000118 RID: 280
		private const int RUN_MASK = 15;

		// Token: 0x04000119 RID: 281
		private const int STEPSIZE_64 = 8;

		// Token: 0x0400011A RID: 282
		private const int STEPSIZE_32 = 4;

		// Token: 0x0400011B RID: 283
		private const int LZ4_64KLIMIT = 65547;

		// Token: 0x0400011C RID: 284
		private const int HASH_LOG = 12;

		// Token: 0x0400011D RID: 285
		private const int HASH_TABLESIZE = 4096;

		// Token: 0x0400011E RID: 286
		private const int HASH_ADJUST = 20;

		// Token: 0x0400011F RID: 287
		private const int HASH64K_LOG = 13;

		// Token: 0x04000120 RID: 288
		private const int HASH64K_TABLESIZE = 8192;

		// Token: 0x04000121 RID: 289
		private const int HASH64K_ADJUST = 19;

		// Token: 0x04000122 RID: 290
		private const int HASHHC_LOG = 15;

		// Token: 0x04000123 RID: 291
		private const int HASHHC_TABLESIZE = 32768;

		// Token: 0x04000124 RID: 292
		private const int HASHHC_ADJUST = 17;

		// Token: 0x04000125 RID: 293
		private static readonly int[] DECODER_TABLE_32 = new int[] { 0, 3, 2, 3, 0, 0, 0, 0 };

		// Token: 0x04000126 RID: 294
		private static readonly int[] DECODER_TABLE_64 = new int[] { 0, 0, 0, -1, 0, 1, 2, 3 };

		// Token: 0x04000127 RID: 295
		private static readonly int[] DEBRUIJN_TABLE_32 = new int[]
		{
			0, 0, 3, 0, 3, 1, 3, 0, 3, 2,
			2, 1, 3, 2, 0, 1, 3, 3, 1, 2,
			2, 2, 2, 0, 3, 1, 2, 0, 1, 0,
			1, 1
		};

		// Token: 0x04000128 RID: 296
		private static readonly int[] DEBRUIJN_TABLE_64 = new int[]
		{
			0, 0, 0, 0, 0, 1, 1, 2, 0, 3,
			1, 3, 1, 4, 2, 7, 0, 2, 3, 6,
			1, 5, 3, 5, 1, 3, 4, 4, 2, 5,
			6, 7, 7, 0, 1, 2, 3, 3, 4, 6,
			2, 6, 5, 5, 3, 4, 5, 6, 7, 1,
			2, 4, 6, 4, 4, 5, 7, 2, 6, 5,
			7, 6, 7, 7
		};

		// Token: 0x04000129 RID: 297
		private const int MAX_NB_ATTEMPTS = 256;

		// Token: 0x0400012A RID: 298
		private const int OPTIMAL_ML = 18;

		// Token: 0x0200002D RID: 45
		private class LZ4HC_Data_Structure
		{
			// Token: 0x0400012B RID: 299
			public byte[] src;

			// Token: 0x0400012C RID: 300
			public int src_base;

			// Token: 0x0400012D RID: 301
			public int src_end;

			// Token: 0x0400012E RID: 302
			public int src_LASTLITERALS;

			// Token: 0x0400012F RID: 303
			public byte[] dst;

			// Token: 0x04000130 RID: 304
			public int dst_base;

			// Token: 0x04000131 RID: 305
			public int dst_len;

			// Token: 0x04000132 RID: 306
			public int dst_end;

			// Token: 0x04000133 RID: 307
			public int[] hashTable;

			// Token: 0x04000134 RID: 308
			public ushort[] chainTable;

			// Token: 0x04000135 RID: 309
			public int nextToUpdate;
		}
	}
}
