using System;
using System.IO;
using System.Runtime.InteropServices;
using K4os.Compression.LZ4.Internal;

namespace K4os.Compression.LZ4
{
	// Token: 0x02000006 RID: 6
	public static class LZ4Pickler
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002252 File Offset: 0x00000452
		public static byte[] Pickle(byte[] source, LZ4Level level = LZ4Level.L00_FAST)
		{
			return LZ4Pickler.Pickle(source, 0, source.Length, level);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002260 File Offset: 0x00000460
		public unsafe static byte[] Pickle(byte[] source, int sourceOffset, int sourceLength, LZ4Level level = LZ4Level.L00_FAST)
		{
			source.Validate(sourceOffset, sourceLength);
			byte* ptr;
			if (source == null || source.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &source[0];
			}
			return LZ4Pickler.Pickle(ptr + sourceOffset, sourceLength, level);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002298 File Offset: 0x00000498
		public unsafe static byte[] Pickle(ReadOnlySpan<byte> source, LZ4Level level = LZ4Level.L00_FAST)
		{
			int length = source.Length;
			if (length <= 0)
			{
				return Mem.Empty;
			}
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(source))
			{
				return LZ4Pickler.Pickle(reference, length, level);
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022C8 File Offset: 0x000004C8
		public unsafe static byte[] Pickle(byte* source, int sourceLength, LZ4Level level = LZ4Level.L00_FAST)
		{
			if (sourceLength <= 0)
			{
				return Mem.Empty;
			}
			int num = sourceLength - 1;
			byte* ptr = (byte*)Mem.Alloc(sourceLength);
			byte[] array;
			try
			{
				int num2 = LZ4Codec.Encode(source, sourceLength, ptr, num, level);
				array = ((num2 <= 0) ? LZ4Pickler.PickleV0(source, sourceLength, sourceLength) : LZ4Pickler.PickleV0(ptr, num2, sourceLength));
			}
			finally
			{
				Mem.Free((void*)ptr);
			}
			return array;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002328 File Offset: 0x00000528
		public static byte[] Unpickle(byte[] source)
		{
			return LZ4Pickler.Unpickle(source, 0, source.Length);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002334 File Offset: 0x00000534
		public unsafe static byte[] Unpickle(byte[] source, int sourceOffset, int sourceLength)
		{
			source.Validate(sourceOffset, sourceLength);
			if (sourceLength <= 0)
			{
				return Mem.Empty;
			}
			byte* ptr;
			if (source == null || source.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &source[0];
			}
			return LZ4Pickler.Unpickle(ptr + sourceOffset, sourceLength);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002374 File Offset: 0x00000574
		public unsafe static byte[] Unpickle(ReadOnlySpan<byte> source)
		{
			if (source.Length <= 0)
			{
				return Mem.Empty;
			}
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(source))
			{
				return LZ4Pickler.Unpickle(reference, source.Length);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000023A8 File Offset: 0x000005A8
		public unsafe static byte[] Unpickle(byte* source, int sourceLength)
		{
			if (sourceLength <= 0)
			{
				return Mem.Empty;
			}
			byte b = *source;
			int num = (int)(b & 7);
			if (num == 0)
			{
				return LZ4Pickler.UnpickleV0(b, source + 1, sourceLength - 1);
			}
			throw new InvalidDataException(string.Format("Pickle version {0} is not supported", num));
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000023EB File Offset: 0x000005EB
		private static Exception CorruptedPickle(string message)
		{
			return new InvalidDataException("Pickle is corrupted: " + message);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002400 File Offset: 0x00000600
		private unsafe static byte[] PickleV0(byte* target, int targetLength, int sourceLength)
		{
			int num = sourceLength - targetLength;
			int num2 = ((num == 0) ? 0 : ((num < 256) ? 1 : ((num < 65536) ? 2 : 4)));
			byte[] array2;
			byte[] array = (array2 = new byte[targetLength + 1 + num2]);
			byte* ptr;
			if (array == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			byte b = (byte)((((num2 == 4) ? 3 : num2) << 6) | 0);
			Mem.Poke8((void*)ptr, b);
			if (num2 == 1)
			{
				Mem.Poke8((void*)(ptr + 1), (byte)num);
			}
			else if (num2 == 2)
			{
				Mem.Poke16((void*)(ptr + 1), (ushort)num);
			}
			else if (num2 == 4)
			{
				Mem.Poke32((void*)(ptr + 1), (uint)num);
			}
			Mem.Move(ptr + num2 + 1, target, targetLength);
			array2 = null;
			return array;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000024A4 File Offset: 0x000006A4
		private unsafe static byte[] UnpickleV0(byte flags, byte* source, int sourceLength)
		{
			int num = (flags >> 6) & 3;
			if (num == 3)
			{
				num = 4;
			}
			if (sourceLength < num)
			{
				throw LZ4Pickler.CorruptedPickle("Source buffer is too small.");
			}
			int num2;
			if (num != 0)
			{
				if (num != 1)
				{
					if (num != 2)
					{
						if (num != 4)
						{
							throw LZ4Pickler.CorruptedPickle("Unexpected length descriptor.");
						}
						num2 = (int)(*(uint*)source);
					}
					else
					{
						num2 = (int)(*(ushort*)source);
					}
				}
				else
				{
					num2 = (int)(*source);
				}
			}
			else
			{
				num2 = 0;
			}
			int num3 = num2;
			source += num;
			sourceLength -= num;
			int num4 = sourceLength + num3;
			byte[] array2;
			byte[] array = (array2 = new byte[num4]);
			byte* ptr;
			if (array == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			if (num3 == 0)
			{
				Mem.Copy(ptr, source, num4);
			}
			else
			{
				int num5 = LZ4Codec.Decode(source, sourceLength, ptr, num4);
				if (num5 != num4)
				{
					throw new ArgumentException(string.Format("Expected {0} bytes but {1} has been decoded", num4, num5));
				}
			}
			array2 = null;
			return array;
		}

		// Token: 0x0400000D RID: 13
		private const byte VersionMask = 7;

		// Token: 0x0400000E RID: 14
		private const byte CurrentVersion = 0;
	}
}
