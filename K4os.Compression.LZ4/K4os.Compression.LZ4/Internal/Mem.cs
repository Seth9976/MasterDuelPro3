using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace K4os.Compression.LZ4.Internal
{
	// Token: 0x02000007 RID: 7
	public class Mem
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00002560 File Offset: 0x00000760
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int RoundUp(int value, int step)
		{
			return (value + step - 1) / step * step;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000256B File Offset: 0x0000076B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void Copy(byte* target, byte* source, int length)
		{
			Buffer.MemoryCopy((void*)source, (void*)target, (long)length, (long)length);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000256B File Offset: 0x0000076B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void Move(byte* target, byte* source, int length)
		{
			Buffer.MemoryCopy((void*)source, (void*)target, (long)length, (long)length);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002578 File Offset: 0x00000778
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void WildCopy(byte* target, byte* source, void* limit)
		{
			do
			{
				*(long*)target = *(long*)source;
				target += 8;
				source += 8;
			}
			while (target < (byte*)limit);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000258C File Offset: 0x0000078C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void Zero(byte* target, int length)
		{
			while (length >= 8)
			{
				*(long*)target = 0L;
				target += 8;
				length -= 8;
			}
			if (length >= 4)
			{
				*(int*)target = 0;
				target += 4;
				length -= 4;
			}
			if (length >= 2)
			{
				*(short*)target = 0;
				target += 2;
				length -= 2;
			}
			if (length > 0)
			{
				*target = 0;
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000025CC File Offset: 0x000007CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void Fill(byte* target, byte value, int length)
		{
			ulong num = (ulong)value;
			num |= num << 8;
			num |= num << 16;
			num |= num << 32;
			while (length >= 8)
			{
				*(long*)target = (long)num;
				target += 8;
				length -= 8;
			}
			if (length >= 4)
			{
				*(int*)target = (int)((uint)num);
				target += 4;
				length -= 4;
			}
			if (length >= 2)
			{
				*(short*)target = (short)((ushort)num);
				target += 2;
				length -= 2;
			}
			if (length > 0)
			{
				*target = value;
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002630 File Offset: 0x00000830
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void LoopCopy(byte* target, byte* source, int length)
		{
			while (length >= 8)
			{
				*(long*)target = *(long*)source;
				target += 8;
				source += 8;
				length -= 8;
			}
			if (length >= 4)
			{
				*(int*)target = (int)(*(uint*)source);
				target += 4;
				source += 4;
				length -= 4;
			}
			if (length >= 2)
			{
				*(short*)target = (short)(*(ushort*)source);
				target += 2;
				source += 2;
				length -= 2;
			}
			if (length > 0)
			{
				*target = *source;
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000268C File Offset: 0x0000088C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static void LoopCopyBack(byte* target, byte* source, int length)
		{
			if (length <= 0)
			{
				return;
			}
			target += length;
			source += length;
			while (length >= 8)
			{
				target -= 8;
				source -= 8;
				length -= 8;
				*(long*)target = *(long*)source;
			}
			if (length >= 4)
			{
				target -= 4;
				source -= 4;
				length -= 4;
				*(int*)target = (int)(*(uint*)source);
			}
			if (length >= 2)
			{
				target -= 2;
				source -= 2;
				length -= 2;
				*(short*)target = (short)(*(ushort*)source);
			}
			if (length > 0)
			{
				target--;
				source--;
				*target = *source;
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002701 File Offset: 0x00000901
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void LoopMove(byte* target, byte* source, int length)
		{
			if (length <= 0 || source == target)
			{
				return;
			}
			if (source >= target || source + length == target)
			{
				Mem.LoopCopy(target, source, length);
				return;
			}
			Mem.LoopCopyBack(target, source, length);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002727 File Offset: 0x00000927
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void Copy8(byte* target, byte* source)
		{
			*(long*)target = *(long*)source;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000272D File Offset: 0x0000092D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void Copy16(byte* target, byte* source)
		{
			*(long*)target = *(long*)source;
			*(long*)(target + 8) = *(long*)(source + 8);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000273B File Offset: 0x0000093B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void Copy18(byte* target, byte* source)
		{
			*(long*)target = *(long*)source;
			*(long*)(target + 8) = *(long*)(source + 8);
			*(short*)(target + 16) = (short)(*(ushort*)(source + 16));
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002754 File Offset: 0x00000954
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void* Alloc(int size)
		{
			return Marshal.AllocHGlobal(size).ToPointer();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000276F File Offset: 0x0000096F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void* AllocZero(int size)
		{
			void* ptr = Mem.Alloc(size);
			Mem.Zero((byte*)ptr, size);
			return ptr;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000277E File Offset: 0x0000097E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void Free(void* ptr)
		{
			Marshal.FreeHGlobal(new IntPtr(ptr));
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000278B File Offset: 0x0000098B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static byte Peek8(void* p)
		{
			return *(byte*)p;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000278F File Offset: 0x0000098F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static ushort Peek16(void* p)
		{
			return *(ushort*)p;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002793 File Offset: 0x00000993
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static uint Peek32(void* p)
		{
			return *(uint*)p;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002797 File Offset: 0x00000997
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static ulong Peek64(void* p)
		{
			return (ulong)(*(long*)p);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000279B File Offset: 0x0000099B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void Poke8(void* p, byte v)
		{
			*(byte*)p = v;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000027A0 File Offset: 0x000009A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void Poke16(void* p, ushort v)
		{
			*(short*)p = (short)v;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000027A5 File Offset: 0x000009A5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void Poke32(void* p, uint v)
		{
			*(int*)p = (int)v;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000027AA File Offset: 0x000009AA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void Poke64(void* p, ulong v)
		{
			*(long*)p = (long)v;
		}

		// Token: 0x0400000F RID: 15
		public const int K1 = 1024;

		// Token: 0x04000010 RID: 16
		public const int K2 = 2048;

		// Token: 0x04000011 RID: 17
		public const int K4 = 4096;

		// Token: 0x04000012 RID: 18
		public const int K8 = 8192;

		// Token: 0x04000013 RID: 19
		public const int K16 = 16384;

		// Token: 0x04000014 RID: 20
		public const int K32 = 32768;

		// Token: 0x04000015 RID: 21
		public const int K64 = 65536;

		// Token: 0x04000016 RID: 22
		public const int K128 = 131072;

		// Token: 0x04000017 RID: 23
		public const int K256 = 262144;

		// Token: 0x04000018 RID: 24
		public const int K512 = 524288;

		// Token: 0x04000019 RID: 25
		public const int M1 = 1048576;

		// Token: 0x0400001A RID: 26
		public const int M4 = 4194304;

		// Token: 0x0400001B RID: 27
		public static readonly byte[] Empty = Array.Empty<byte>();
	}
}
