using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Buffers.Binary
{
	// Token: 0x0200078E RID: 1934
	public static class BinaryPrimitives
	{
		// Token: 0x06003D16 RID: 15638 RVA: 0x000EB4F4 File Offset: 0x000E96F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static short ReverseEndianness(short value)
		{
			return (short)(((int)(value & 255) << 8) | (((int)value & 65280) >> 8));
		}

		// Token: 0x06003D17 RID: 15639 RVA: 0x000EB50A File Offset: 0x000E970A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ReverseEndianness(int value)
		{
			return (int)BinaryPrimitives.ReverseEndianness((uint)value);
		}

		// Token: 0x06003D18 RID: 15640 RVA: 0x000EB512 File Offset: 0x000E9712
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long ReverseEndianness(long value)
		{
			return (long)BinaryPrimitives.ReverseEndianness((ulong)value);
		}

		// Token: 0x06003D19 RID: 15641 RVA: 0x000EB51A File Offset: 0x000E971A
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ushort ReverseEndianness(ushort value)
		{
			return (ushort)((value >> 8) + ((int)value << 8));
		}

		// Token: 0x06003D1A RID: 15642 RVA: 0x000EB524 File Offset: 0x000E9724
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint ReverseEndianness(uint value)
		{
			uint num = value & 16711935U;
			uint num2 = value & 4278255360U;
			return ((num >> 8) | (num << 24)) + ((num2 << 8) | (num2 >> 24));
		}

		// Token: 0x06003D1B RID: 15643 RVA: 0x000EB552 File Offset: 0x000E9752
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong ReverseEndianness(ulong value)
		{
			return ((ulong)BinaryPrimitives.ReverseEndianness((uint)value) << 32) + (ulong)BinaryPrimitives.ReverseEndianness((uint)(value >> 32));
		}

		// Token: 0x06003D1C RID: 15644 RVA: 0x000EB56C File Offset: 0x000E976C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static short ReadInt16BigEndian(ReadOnlySpan<byte> source)
		{
			short num = MemoryMarshal.Read<short>(source);
			if (BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		// Token: 0x06003D1D RID: 15645 RVA: 0x000EB590 File Offset: 0x000E9790
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ReadInt32BigEndian(ReadOnlySpan<byte> source)
		{
			int num = MemoryMarshal.Read<int>(source);
			if (BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		// Token: 0x06003D1E RID: 15646 RVA: 0x000EB5B4 File Offset: 0x000E97B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long ReadInt64BigEndian(ReadOnlySpan<byte> source)
		{
			long num = MemoryMarshal.Read<long>(source);
			if (BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		// Token: 0x06003D1F RID: 15647 RVA: 0x000EB5D8 File Offset: 0x000E97D8
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ushort ReadUInt16BigEndian(ReadOnlySpan<byte> source)
		{
			ushort num = MemoryMarshal.Read<ushort>(source);
			if (BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		// Token: 0x06003D20 RID: 15648 RVA: 0x000EB5FC File Offset: 0x000E97FC
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint ReadUInt32BigEndian(ReadOnlySpan<byte> source)
		{
			uint num = MemoryMarshal.Read<uint>(source);
			if (BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		// Token: 0x06003D21 RID: 15649 RVA: 0x000EB620 File Offset: 0x000E9820
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong ReadUInt64BigEndian(ReadOnlySpan<byte> source)
		{
			ulong num = MemoryMarshal.Read<ulong>(source);
			if (BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}
	}
}
