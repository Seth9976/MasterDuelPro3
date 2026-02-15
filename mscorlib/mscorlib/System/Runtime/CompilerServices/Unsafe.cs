using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020005C0 RID: 1472
	internal static class Unsafe
	{
		// Token: 0x06002BA1 RID: 11169 RVA: 0x000AC72E File Offset: 0x000AA92E
		public static ref T Add<T>(ref T source, int elementOffset)
		{
			return (ref source) + (IntPtr)elementOffset * (IntPtr)sizeof(T);
		}

		// Token: 0x06002BA2 RID: 11170 RVA: 0x000AC73B File Offset: 0x000AA93B
		public static ref T Add<T>(ref T source, IntPtr elementOffset)
		{
			return (ref source) + elementOffset * (IntPtr)sizeof(T);
		}

		// Token: 0x06002BA3 RID: 11171 RVA: 0x000AC72E File Offset: 0x000AA92E
		public unsafe static void* Add<T>(void* source, int elementOffset)
		{
			return (void*)((byte*)source + (IntPtr)elementOffset * (IntPtr)sizeof(T));
		}

		// Token: 0x06002BA4 RID: 11172 RVA: 0x000AC747 File Offset: 0x000AA947
		public static ref T AddByteOffset<T>(ref T source, IntPtr byteOffset)
		{
			return (ref source) + byteOffset;
		}

		// Token: 0x06002BA5 RID: 11173 RVA: 0x00033FD7 File Offset: 0x000321D7
		public static bool AreSame<T>(ref T left, ref T right)
		{
			return (ref left) == (ref right);
		}

		// Token: 0x06002BA6 RID: 11174 RVA: 0x00002645 File Offset: 0x00000845
		public static T As<T>(object o) where T : class
		{
			return o;
		}

		// Token: 0x06002BA7 RID: 11175 RVA: 0x00002645 File Offset: 0x00000845
		public static ref TTo As<TFrom, TTo>(ref TFrom source)
		{
			return ref source;
		}

		// Token: 0x06002BA8 RID: 11176 RVA: 0x000AC74C File Offset: 0x000AA94C
		public unsafe static void* AsPointer<T>(ref T value)
		{
			return (void*)(&value);
		}

		// Token: 0x06002BA9 RID: 11177 RVA: 0x00002645 File Offset: 0x00000845
		public unsafe static ref T AsRef<T>(void* source)
		{
			return ref *(T*)source;
		}

		// Token: 0x06002BAA RID: 11178 RVA: 0x00002645 File Offset: 0x00000845
		public static ref T AsRef<T>(in T source)
		{
			return ref source;
		}

		// Token: 0x06002BAB RID: 11179 RVA: 0x000AC750 File Offset: 0x000AA950
		public static void InitBlockUnaligned(ref byte startAddress, byte value, uint byteCount)
		{
			initblk(ref startAddress, value, byteCount);
		}

		// Token: 0x06002BAC RID: 11180 RVA: 0x000AC75A File Offset: 0x000AA95A
		public unsafe static T Read<T>(void* source)
		{
			return *(T*)source;
		}

		// Token: 0x06002BAD RID: 11181 RVA: 0x000AC762 File Offset: 0x000AA962
		public static T ReadUnaligned<T>(ref byte source)
		{
			return source;
		}

		// Token: 0x06002BAE RID: 11182 RVA: 0x000AC76D File Offset: 0x000AA96D
		public static int SizeOf<T>()
		{
			return sizeof(T);
		}

		// Token: 0x06002BAF RID: 11183 RVA: 0x000AC775 File Offset: 0x000AA975
		public static void WriteUnaligned<T>(ref byte destination, T value)
		{
			destination = value;
		}

		// Token: 0x06002BB0 RID: 11184 RVA: 0x000AC781 File Offset: 0x000AA981
		public static bool IsAddressLessThan<T>(ref T left, ref T right)
		{
			return (ref left) < (ref right);
		}

		// Token: 0x06002BB1 RID: 11185 RVA: 0x000AC787 File Offset: 0x000AA987
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static ref T AddByteOffset<T>(ref T source, ulong byteOffset)
		{
			return Unsafe.AddByteOffset<T>(ref source, (IntPtr)byteOffset);
		}
	}
}
