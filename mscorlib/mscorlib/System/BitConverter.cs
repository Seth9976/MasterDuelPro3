using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Converts base data types to an array of bytes, and an array of bytes to base data types.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000C9 RID: 201
	public static class BitConverter
	{
		/// <summary>Returns the specified Boolean value as an array of bytes.</summary>
		/// <returns>An array of bytes with length 1.</returns>
		/// <param name="value">A Boolean value. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004E8 RID: 1256 RVA: 0x00018F36 File Offset: 0x00017136
		public static byte[] GetBytes(bool value)
		{
			return new byte[] { value ? 1 : 0 };
		}

		/// <summary>Returns the specified Unicode character value as an array of bytes.</summary>
		/// <returns>An array of bytes with length 2.</returns>
		/// <param name="value">A character to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004E9 RID: 1257 RVA: 0x00018F48 File Offset: 0x00017148
		public unsafe static byte[] GetBytes(char value)
		{
			byte[] array = new byte[2];
			*Unsafe.As<byte, char>(ref array[0]) = value;
			return array;
		}

		/// <summary>Returns the specified 16-bit signed integer value as an array of bytes.</summary>
		/// <returns>An array of bytes with length 2.</returns>
		/// <param name="value">The number to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004EA RID: 1258 RVA: 0x00018F5E File Offset: 0x0001715E
		public unsafe static byte[] GetBytes(short value)
		{
			byte[] array = new byte[2];
			*Unsafe.As<byte, short>(ref array[0]) = value;
			return array;
		}

		/// <summary>Returns the specified 32-bit signed integer value as an array of bytes.</summary>
		/// <returns>An array of bytes with length 4.</returns>
		/// <param name="value">The number to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004EB RID: 1259 RVA: 0x00018F74 File Offset: 0x00017174
		public unsafe static byte[] GetBytes(int value)
		{
			byte[] array = new byte[4];
			*Unsafe.As<byte, int>(ref array[0]) = value;
			return array;
		}

		/// <summary>Returns the specified 64-bit signed integer value as an array of bytes.</summary>
		/// <returns>An array of bytes with length 8.</returns>
		/// <param name="value">The number to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004EC RID: 1260 RVA: 0x00018F8A File Offset: 0x0001718A
		public unsafe static byte[] GetBytes(long value)
		{
			byte[] array = new byte[8];
			*Unsafe.As<byte, long>(ref array[0]) = value;
			return array;
		}

		/// <summary>Returns the specified 16-bit unsigned integer value as an array of bytes.</summary>
		/// <returns>An array of bytes with length 2.</returns>
		/// <param name="value">The number to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004ED RID: 1261 RVA: 0x00018FA0 File Offset: 0x000171A0
		[CLSCompliant(false)]
		public unsafe static byte[] GetBytes(ushort value)
		{
			byte[] array = new byte[2];
			*Unsafe.As<byte, ushort>(ref array[0]) = value;
			return array;
		}

		/// <summary>Returns the specified 32-bit unsigned integer value as an array of bytes.</summary>
		/// <returns>An array of bytes with length 4.</returns>
		/// <param name="value">The number to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004EE RID: 1262 RVA: 0x00018FB6 File Offset: 0x000171B6
		[CLSCompliant(false)]
		public unsafe static byte[] GetBytes(uint value)
		{
			byte[] array = new byte[4];
			*Unsafe.As<byte, uint>(ref array[0]) = value;
			return array;
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00018FCC File Offset: 0x000171CC
		[CLSCompliant(false)]
		public static bool TryWriteBytes(Span<byte> destination, uint value)
		{
			if (destination.Length < 4)
			{
				return false;
			}
			Unsafe.WriteUnaligned<uint>(MemoryMarshal.GetReference<byte>(destination), value);
			return true;
		}

		/// <summary>Returns the specified 64-bit unsigned integer value as an array of bytes.</summary>
		/// <returns>An array of bytes with length 8.</returns>
		/// <param name="value">The number to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004F0 RID: 1264 RVA: 0x00018FE7 File Offset: 0x000171E7
		[CLSCompliant(false)]
		public unsafe static byte[] GetBytes(ulong value)
		{
			byte[] array = new byte[8];
			*Unsafe.As<byte, ulong>(ref array[0]) = value;
			return array;
		}

		/// <summary>Returns the specified single-precision floating point value as an array of bytes.</summary>
		/// <returns>An array of bytes with length 4.</returns>
		/// <param name="value">The number to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004F1 RID: 1265 RVA: 0x00018FFD File Offset: 0x000171FD
		public unsafe static byte[] GetBytes(float value)
		{
			byte[] array = new byte[4];
			*Unsafe.As<byte, float>(ref array[0]) = value;
			return array;
		}

		/// <summary>Returns the specified double-precision floating point value as an array of bytes.</summary>
		/// <returns>An array of bytes with length 8.</returns>
		/// <param name="value">The number to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004F2 RID: 1266 RVA: 0x00019013 File Offset: 0x00017213
		public unsafe static byte[] GetBytes(double value)
		{
			byte[] array = new byte[8];
			*Unsafe.As<byte, double>(ref array[0]) = value;
			return array;
		}

		/// <summary>Returns a Unicode character converted from two bytes at a specified position in a byte array.</summary>
		/// <returns>A character formed by two bytes beginning at <paramref name="startIndex" />.</returns>
		/// <param name="value">An array. </param>
		/// <param name="startIndex">The starting position within <paramref name="value" />. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="startIndex" /> equals the length of <paramref name="value" /> minus 1.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is less than zero or greater than the length of <paramref name="value" /> minus 1. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004F3 RID: 1267 RVA: 0x00019029 File Offset: 0x00017229
		public static char ToChar(byte[] value, int startIndex)
		{
			return (char)BitConverter.ToInt16(value, startIndex);
		}

		/// <summary>Returns a 16-bit signed integer converted from two bytes at a specified position in a byte array.</summary>
		/// <returns>A 16-bit signed integer formed by two bytes beginning at <paramref name="startIndex" />.</returns>
		/// <param name="value">An array of bytes. </param>
		/// <param name="startIndex">The starting position within <paramref name="value" />. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="startIndex" /> equals the length of <paramref name="value" /> minus 1.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is less than zero or greater than the length of <paramref name="value" /> minus 1. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004F4 RID: 1268 RVA: 0x00019033 File Offset: 0x00017233
		public static short ToInt16(byte[] value, int startIndex)
		{
			if (value == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
			}
			if (startIndex >= value.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
			}
			if (startIndex > value.Length - 2)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall, ExceptionArgument.value);
			}
			return Unsafe.ReadUnaligned<short>(ref value[startIndex]);
		}

		/// <summary>Returns a 32-bit signed integer converted from four bytes at a specified position in a byte array.</summary>
		/// <returns>A 32-bit signed integer formed by four bytes beginning at <paramref name="startIndex" />.</returns>
		/// <param name="value">An array of bytes. </param>
		/// <param name="startIndex">The starting position within <paramref name="value" />. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="startIndex" /> is greater than or equal to the length of <paramref name="value" /> minus 3, and is less than or equal to the length of <paramref name="value" /> minus 1.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is less than zero or greater than the length of <paramref name="value" /> minus 1. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004F5 RID: 1269 RVA: 0x0001906A File Offset: 0x0001726A
		public static int ToInt32(byte[] value, int startIndex)
		{
			if (value == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
			}
			if (startIndex >= value.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
			}
			if (startIndex > value.Length - 4)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall, ExceptionArgument.value);
			}
			return Unsafe.ReadUnaligned<int>(ref value[startIndex]);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x000190A1 File Offset: 0x000172A1
		public static int ToInt32(ReadOnlySpan<byte> value)
		{
			if (value.Length < 4)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.value);
			}
			return Unsafe.ReadUnaligned<int>(MemoryMarshal.GetReference<byte>(value));
		}

		/// <summary>Returns a 64-bit signed integer converted from eight bytes at a specified position in a byte array.</summary>
		/// <returns>A 64-bit signed integer formed by eight bytes beginning at <paramref name="startIndex" />.</returns>
		/// <param name="value">An array of bytes. </param>
		/// <param name="startIndex">The starting position within <paramref name="value" />. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="startIndex" /> is greater than or equal to the length of <paramref name="value" /> minus 7, and is less than or equal to the length of <paramref name="value" /> minus 1.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is less than zero or greater than the length of <paramref name="value" /> minus 1. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004F7 RID: 1271 RVA: 0x000190BF File Offset: 0x000172BF
		public static long ToInt64(byte[] value, int startIndex)
		{
			if (value == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
			}
			if (startIndex >= value.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
			}
			if (startIndex > value.Length - 8)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall, ExceptionArgument.value);
			}
			return Unsafe.ReadUnaligned<long>(ref value[startIndex]);
		}

		/// <summary>Returns a 16-bit unsigned integer converted from two bytes at a specified position in a byte array.</summary>
		/// <returns>A 16-bit unsigned integer formed by two bytes beginning at <paramref name="startIndex" />.</returns>
		/// <param name="value">The array of bytes. </param>
		/// <param name="startIndex">The starting position within <paramref name="value" />. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="startIndex" /> equals the length of <paramref name="value" /> minus 1.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is less than zero or greater than the length of <paramref name="value" /> minus 1. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004F8 RID: 1272 RVA: 0x00019029 File Offset: 0x00017229
		[CLSCompliant(false)]
		public static ushort ToUInt16(byte[] value, int startIndex)
		{
			return (ushort)BitConverter.ToInt16(value, startIndex);
		}

		/// <summary>Returns a 32-bit unsigned integer converted from four bytes at a specified position in a byte array.</summary>
		/// <returns>A 32-bit unsigned integer formed by four bytes beginning at <paramref name="startIndex" />.</returns>
		/// <param name="value">An array of bytes. </param>
		/// <param name="startIndex">The starting position within <paramref name="value" />. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="startIndex" /> is greater than or equal to the length of <paramref name="value" /> minus 3, and is less than or equal to the length of <paramref name="value" /> minus 1.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is less than zero or greater than the length of <paramref name="value" /> minus 1. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004F9 RID: 1273 RVA: 0x000190F6 File Offset: 0x000172F6
		[CLSCompliant(false)]
		public static uint ToUInt32(byte[] value, int startIndex)
		{
			return (uint)BitConverter.ToInt32(value, startIndex);
		}

		/// <summary>Returns a single-precision floating point number converted from four bytes at a specified position in a byte array.</summary>
		/// <returns>A single-precision floating point number formed by four bytes beginning at <paramref name="startIndex" />.</returns>
		/// <param name="value">An array of bytes. </param>
		/// <param name="startIndex">The starting position within <paramref name="value" />. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="startIndex" /> is greater than or equal to the length of <paramref name="value" /> minus 3, and is less than or equal to the length of <paramref name="value" /> minus 1.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is less than zero or greater than the length of <paramref name="value" /> minus 1. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004FA RID: 1274 RVA: 0x000190FF File Offset: 0x000172FF
		public static float ToSingle(byte[] value, int startIndex)
		{
			return BitConverter.Int32BitsToSingle(BitConverter.ToInt32(value, startIndex));
		}

		/// <summary>Returns a double-precision floating point number converted from eight bytes at a specified position in a byte array.</summary>
		/// <returns>A double precision floating point number formed by eight bytes beginning at <paramref name="startIndex" />.</returns>
		/// <param name="value">An array of bytes. </param>
		/// <param name="startIndex">The starting position within <paramref name="value" />. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="startIndex" /> is greater than or equal to the length of <paramref name="value" /> minus 7, and is less than or equal to the length of <paramref name="value" /> minus 1.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is less than zero or greater than the length of <paramref name="value" /> minus 1. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004FB RID: 1275 RVA: 0x0001910D File Offset: 0x0001730D
		public static double ToDouble(byte[] value, int startIndex)
		{
			return BitConverter.Int64BitsToDouble(BitConverter.ToInt64(value, startIndex));
		}

		/// <summary>Converts the numeric value of each element of a specified subarray of bytes to its equivalent hexadecimal string representation.</summary>
		/// <returns>A string of hexadecimal pairs separated by hyphens, where each pair represents the corresponding element in a subarray of <paramref name="value" />; for example, "7F-2C-4A-00".</returns>
		/// <param name="value">An array of bytes. </param>
		/// <param name="startIndex">The starting position within <paramref name="value" />. </param>
		/// <param name="length">The number of array elements in <paramref name="value" /> to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> or <paramref name="length" /> is less than zero.-or-<paramref name="startIndex" /> is greater than zero and is greater than or equal to the length of <paramref name="value" />.</exception>
		/// <exception cref="T:System.ArgumentException">The combination of <paramref name="startIndex" /> and <paramref name="length" /> does not specify a position within <paramref name="value" />; that is, the <paramref name="startIndex" /> parameter is greater than the length of <paramref name="value" /> minus the <paramref name="length" /> parameter.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004FC RID: 1276 RVA: 0x0001911C File Offset: 0x0001731C
		public unsafe static string ToString(byte[] value, int startIndex, int length)
		{
			if (value == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
			}
			if (startIndex < 0 || (startIndex >= value.Length && startIndex > 0))
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", "Value must be positive.");
			}
			if (startIndex > value.Length - length)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall, ExceptionArgument.value);
			}
			if (length == 0)
			{
				return string.Empty;
			}
			if (length > 715827882)
			{
				throw new ArgumentOutOfRangeException("length", SR.Format("The specified length exceeds the maximum value of {0}.", 715827882));
			}
			return string.Create<ValueTuple<byte[], int, int>>(length * 3 - 1, new ValueTuple<byte[], int, int>(value, startIndex, length), delegate(Span<char> dst, [TupleElementNames(new string[] { "value", "startIndex", "length" })] ValueTuple<byte[], int, int> state)
			{
				ReadOnlySpan<byte> readOnlySpan = new ReadOnlySpan<byte>(state.Item1, state.Item2, state.Item3);
				int i = 0;
				int num = 0;
				byte b = *readOnlySpan[i++];
				*dst[num++] = "0123456789ABCDEF"[b >> 4];
				*dst[num++] = "0123456789ABCDEF"[(int)(b & 15)];
				while (i < readOnlySpan.Length)
				{
					b = *readOnlySpan[i++];
					*dst[num++] = '-';
					*dst[num++] = "0123456789ABCDEF"[b >> 4];
					*dst[num++] = "0123456789ABCDEF"[(int)(b & 15)];
				}
			});
		}

		/// <summary>Converts the numeric value of each element of a specified array of bytes to its equivalent hexadecimal string representation.</summary>
		/// <returns>A string of hexadecimal pairs separated by hyphens, where each pair represents the corresponding element in <paramref name="value" />; for example, "7F-2C-4A-00".</returns>
		/// <param name="value">An array of bytes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004FD RID: 1277 RVA: 0x000191CF File Offset: 0x000173CF
		public static string ToString(byte[] value)
		{
			if (value == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
			}
			return BitConverter.ToString(value, 0, value.Length);
		}

		/// <summary>Converts the specified double-precision floating point number to a 64-bit signed integer.</summary>
		/// <returns>A 64-bit signed integer whose value is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The number to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004FE RID: 1278 RVA: 0x000191E5 File Offset: 0x000173E5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static long DoubleToInt64Bits(double value)
		{
			return *(long*)(&value);
		}

		/// <summary>Converts the specified 64-bit signed integer to a double-precision floating point number.</summary>
		/// <returns>A double-precision floating point number whose value is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The number to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004FF RID: 1279 RVA: 0x000191EB File Offset: 0x000173EB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static double Int64BitsToDouble(long value)
		{
			return *(double*)(&value);
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x000191F1 File Offset: 0x000173F1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int SingleToInt32Bits(float value)
		{
			return *(int*)(&value);
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x000191F7 File Offset: 0x000173F7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static float Int32BitsToSingle(int value)
		{
			return *(float*)(&value);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00019200 File Offset: 0x00017400
		unsafe static BitConverter()
		{
			ushort num = 4660;
			byte* ptr = (byte*)(&num);
			BitConverter.IsLittleEndian = *ptr == 52;
		}

		/// <summary>Indicates the byte order ("endianness") in which data is stored in this computer architecture.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040002D9 RID: 729
		[Intrinsic]
		public static readonly bool IsLittleEndian;
	}
}
