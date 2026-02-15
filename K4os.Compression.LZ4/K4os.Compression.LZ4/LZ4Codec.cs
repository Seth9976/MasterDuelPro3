using System;
using System.Runtime.InteropServices;
using K4os.Compression.LZ4.Engine;

namespace K4os.Compression.LZ4
{
	// Token: 0x02000004 RID: 4
	public class LZ4Codec
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020AC File Offset: 0x000002AC
		public static int MaximumOutputSize(int length)
		{
			return LZ4_xx.LZ4_compressBound(length);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020B4 File Offset: 0x000002B4
		public unsafe static int Encode(byte* source, int sourceLength, byte* target, int targetLength, LZ4Level level = LZ4Level.L00_FAST)
		{
			if (sourceLength <= 0)
			{
				return 0;
			}
			int num = ((level == LZ4Level.L00_FAST) ? LZ4_64.LZ4_compress_default(source, target, sourceLength, targetLength) : LZ4_64_HC.LZ4_compress_HC(source, target, sourceLength, targetLength, (int)level));
			if (num > 0)
			{
				return num;
			}
			return -1;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020EC File Offset: 0x000002EC
		public unsafe static int Encode(ReadOnlySpan<byte> source, Span<byte> target, LZ4Level level = LZ4Level.L00_FAST)
		{
			int length = source.Length;
			if (length <= 0)
			{
				return 0;
			}
			int length2 = target.Length;
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(source))
			{
				byte* ptr = reference;
				fixed (byte* reference2 = MemoryMarshal.GetReference<byte>(target))
				{
					byte* ptr2 = reference2;
					return LZ4Codec.Encode(ptr, length, ptr2, length2, level);
				}
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002130 File Offset: 0x00000330
		public unsafe static int Encode(byte[] source, int sourceOffset, int sourceLength, byte[] target, int targetOffset, int targetLength, LZ4Level level = LZ4Level.L00_FAST)
		{
			source.Validate(sourceOffset, sourceLength);
			target.Validate(targetOffset, targetLength);
			byte* ptr;
			if (source == null || source.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &source[0];
			}
			byte* ptr2;
			if (target == null || target.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &target[0];
			}
			return LZ4Codec.Encode(ptr + sourceOffset, sourceLength, ptr2 + targetOffset, targetLength, level);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002190 File Offset: 0x00000390
		public unsafe static int Decode(byte* source, int sourceLength, byte* target, int targetLength)
		{
			if (sourceLength <= 0)
			{
				return 0;
			}
			int num = LZ4_xx.LZ4_decompress_safe(source, target, sourceLength, targetLength);
			if (num > 0)
			{
				return num;
			}
			return -1;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021B4 File Offset: 0x000003B4
		public unsafe static int Decode(ReadOnlySpan<byte> source, Span<byte> target)
		{
			int length = source.Length;
			if (length <= 0)
			{
				return 0;
			}
			int length2 = target.Length;
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(source))
			{
				byte* ptr = reference;
				fixed (byte* reference2 = MemoryMarshal.GetReference<byte>(target))
				{
					byte* ptr2 = reference2;
					return LZ4Codec.Decode(ptr, length, ptr2, length2);
				}
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021F4 File Offset: 0x000003F4
		public unsafe static int Decode(byte[] source, int sourceOffset, int sourceLength, byte[] target, int targetOffset, int targetLength)
		{
			source.Validate(sourceOffset, sourceLength);
			target.Validate(targetOffset, targetLength);
			byte* ptr;
			if (source == null || source.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &source[0];
			}
			byte* ptr2;
			if (target == null || target.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &target[0];
			}
			return LZ4Codec.Decode(ptr + sourceOffset, sourceLength, ptr2 + targetOffset, targetLength);
		}
	}
}
