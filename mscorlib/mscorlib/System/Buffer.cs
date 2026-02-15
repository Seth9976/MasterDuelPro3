using System;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Threading;

namespace System
{
	/// <summary>Manipulates arrays of primitive types.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000196 RID: 406
	[ComVisible(true)]
	public static class Buffer
	{
		// Token: 0x06000EE9 RID: 3817
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool InternalBlockCopy(Array src, int srcOffsetBytes, Array dst, int dstOffsetBytes, int byteCount);

		// Token: 0x06000EEA RID: 3818 RVA: 0x0003E288 File Offset: 0x0003C488
		internal unsafe static int IndexOfByte(byte* src, byte value, int index, int count)
		{
			byte* ptr = src + index;
			while ((ptr & 3) != 0)
			{
				if (count == 0)
				{
					return -1;
				}
				if (*ptr == value)
				{
					return (int)((long)(ptr - src));
				}
				count--;
				ptr++;
			}
			uint num = (uint)(((int)value << 8) + (int)value);
			num = (num << 16) + num;
			while (count > 3)
			{
				uint num2 = *(uint*)ptr;
				num2 ^= num;
				uint num3 = 2130640639U + num2;
				num2 ^= uint.MaxValue;
				num2 ^= num3;
				num2 &= 2164326656U;
				if (num2 != 0U)
				{
					int num4 = (int)((long)(ptr - src));
					if (*ptr == value)
					{
						return num4;
					}
					if (ptr[1] == value)
					{
						return num4 + 1;
					}
					if (ptr[2] == value)
					{
						return num4 + 2;
					}
					if (ptr[3] == value)
					{
						return num4 + 3;
					}
				}
				count -= 4;
				ptr += 4;
			}
			while (count > 0)
			{
				if (*ptr == value)
				{
					return (int)((long)(ptr - src));
				}
				count--;
				ptr++;
			}
			return -1;
		}

		// Token: 0x06000EEB RID: 3819
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int _ByteLength(Array array);

		// Token: 0x06000EEC RID: 3820 RVA: 0x0003E34C File Offset: 0x0003C54C
		internal unsafe static void ZeroMemory(byte* src, long len)
		{
			for (;;)
			{
				long num = len;
				len = num - 1L;
				if (num <= 0L)
				{
					break;
				}
				src[len] = 0;
			}
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x0003E364 File Offset: 0x0003C564
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal unsafe static void Memcpy(byte* pDest, int destIndex, byte[] src, int srcIndex, int len)
		{
			if (len == 0)
			{
				return;
			}
			fixed (byte[] array = src)
			{
				byte* ptr;
				if (src == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				Buffer.Memcpy(pDest + destIndex, ptr + srcIndex, len);
			}
		}

		// Token: 0x06000EEE RID: 3822
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void InternalMemcpy(byte* dest, byte* src, int count);

		/// <summary>Returns the number of bytes in the specified array.</summary>
		/// <returns>The number of bytes in the array.</returns>
		/// <param name="array">An array. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is not a primitive. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="array" /> is larger than 2 gigabytes (GB).</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EEF RID: 3823 RVA: 0x0003E39D File Offset: 0x0003C59D
		public static int ByteLength(Array array)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int num = Buffer._ByteLength(array);
			if (num < 0)
			{
				throw new ArgumentException("Object must be an array of primitives.");
			}
			return num;
		}

		/// <summary>Copies a specified number of bytes from a source array starting at a particular offset to a destination array starting at a particular offset.</summary>
		/// <param name="src">The source buffer. </param>
		/// <param name="srcOffset">The zero-based byte offset into <paramref name="src" />. </param>
		/// <param name="dst">The destination buffer. </param>
		/// <param name="dstOffset">The zero-based byte offset into <paramref name="dst" />. </param>
		/// <param name="count">The number of bytes to copy. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="src" /> or <paramref name="dst" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="src" /> or <paramref name="dst" /> is not an array of primitives.-or- The number of bytes in <paramref name="src" /> is less than <paramref name="srcOffset" /> plus <paramref name="count" />.-or- The number of bytes in <paramref name="dst" /> is less than <paramref name="dstOffset" /> plus <paramref name="count" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="srcOffset" />, <paramref name="dstOffset" />, or <paramref name="count" /> is less than 0. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EF0 RID: 3824 RVA: 0x0003E3C4 File Offset: 0x0003C5C4
		public static void BlockCopy(Array src, int srcOffset, Array dst, int dstOffset, int count)
		{
			if (src == null)
			{
				throw new ArgumentNullException("src");
			}
			if (dst == null)
			{
				throw new ArgumentNullException("dst");
			}
			if (srcOffset < 0)
			{
				throw new ArgumentOutOfRangeException("srcOffset", "Non-negative number required.");
			}
			if (dstOffset < 0)
			{
				throw new ArgumentOutOfRangeException("dstOffset", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (!Buffer.InternalBlockCopy(src, srcOffset, dst, dstOffset, count) && (srcOffset > Buffer.ByteLength(src) - count || dstOffset > Buffer.ByteLength(dst) - count))
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x0003E45C File Offset: 0x0003C65C
		[CLSCompliant(false)]
		public unsafe static void MemoryCopy(void* source, void* destination, long destinationSizeInBytes, long sourceBytesToCopy)
		{
			if (sourceBytesToCopy > destinationSizeInBytes)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.sourceBytesToCopy);
			}
			byte* ptr = (byte*)source;
			byte* ptr2 = (byte*)destination;
			while (sourceBytesToCopy > (long)((ulong)(-1)))
			{
				Buffer.Memmove(ptr2, ptr, uint.MaxValue);
				sourceBytesToCopy -= (long)((ulong)(-1));
				ptr += -1;
				ptr2 += -1;
			}
			Buffer.Memmove(ptr2, ptr, (uint)sourceBytesToCopy);
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x0003E4A0 File Offset: 0x0003C6A0
		internal unsafe static void memcpy4(byte* dest, byte* src, int size)
		{
			while (size >= 16)
			{
				*(int*)dest = *(int*)src;
				*(int*)(dest + 4) = *(int*)(src + 4);
				*(int*)(dest + (IntPtr)2 * 4) = *(int*)(src + (IntPtr)2 * 4);
				*(int*)(dest + (IntPtr)3 * 4) = *(int*)(src + (IntPtr)3 * 4);
				dest += 16;
				src += 16;
				size -= 16;
			}
			while (size >= 4)
			{
				*(int*)dest = *(int*)src;
				dest += 4;
				src += 4;
				size -= 4;
			}
			while (size > 0)
			{
				*dest = *src;
				dest++;
				src++;
				size--;
			}
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x0003E520 File Offset: 0x0003C720
		internal unsafe static void memcpy2(byte* dest, byte* src, int size)
		{
			while (size >= 8)
			{
				*(short*)dest = *(short*)src;
				*(short*)(dest + 2) = *(short*)(src + 2);
				*(short*)(dest + (IntPtr)2 * 2) = *(short*)(src + (IntPtr)2 * 2);
				*(short*)(dest + (IntPtr)3 * 2) = *(short*)(src + (IntPtr)3 * 2);
				dest += 8;
				src += 8;
				size -= 8;
			}
			while (size >= 2)
			{
				*(short*)dest = *(short*)src;
				dest += 2;
				src += 2;
				size -= 2;
			}
			if (size > 0)
			{
				*dest = *src;
			}
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x0003E58C File Offset: 0x0003C78C
		private unsafe static void memcpy1(byte* dest, byte* src, int size)
		{
			while (size >= 8)
			{
				*dest = *src;
				dest[1] = src[1];
				dest[2] = src[2];
				dest[3] = src[3];
				dest[4] = src[4];
				dest[5] = src[5];
				dest[6] = src[6];
				dest[7] = src[7];
				dest += 8;
				src += 8;
				size -= 8;
			}
			while (size >= 2)
			{
				*dest = *src;
				dest[1] = src[1];
				dest += 2;
				src += 2;
				size -= 2;
			}
			if (size > 0)
			{
				*dest = *src;
			}
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x0003E614 File Offset: 0x0003C814
		internal unsafe static void Memcpy(byte* dest, byte* src, int len)
		{
			if (len > 32)
			{
				long num = (long)((ulong)(*dest));
				Interlocked.Read(ref num);
				Buffer.InternalMemcpy(dest, src, len);
				return;
			}
			if (((dest | src) & 3) != 0)
			{
				if ((dest & 1) != 0 && (src & 1) != 0 && len >= 1)
				{
					*dest = *src;
					dest++;
					src++;
					len--;
				}
				if ((dest & 2) != 0 && (src & 2) != 0 && len >= 2)
				{
					*(short*)dest = *(short*)src;
					dest += 2;
					src += 2;
					len -= 2;
				}
				if (((dest | src) & 1) != 0)
				{
					Buffer.memcpy1(dest, src, len);
					return;
				}
				if (((dest | src) & 2) != 0)
				{
					Buffer.memcpy2(dest, src, len);
					return;
				}
			}
			Buffer.memcpy4(dest, src, len);
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x0003E6B6 File Offset: 0x0003C8B6
		internal unsafe static void Memmove(byte* dest, byte* src, uint len)
		{
			if ((ulong)(dest - src) >= (ulong)len && (ulong)(src - dest) >= (ulong)len)
			{
				Buffer.Memcpy(dest, src, (int)len);
				return;
			}
			RuntimeImports.Memmove(dest, src, len);
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x0003E6DC File Offset: 0x0003C8DC
		internal unsafe static void Memmove<T>(ref T destination, ref T source, ulong elementCount)
		{
			if (!RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				fixed (byte* ptr = Unsafe.As<T, byte>(ref destination))
				{
					byte* ptr2 = ptr;
					fixed (byte* ptr3 = Unsafe.As<T, byte>(ref source))
					{
						byte* ptr4 = ptr3;
						Buffer.Memmove(ptr2, ptr4, (uint)elementCount * (uint)Unsafe.SizeOf<T>());
						ptr = null;
					}
					return;
				}
			}
			fixed (byte* ptr3 = Unsafe.As<T, byte>(ref destination))
			{
				byte* ptr5 = ptr3;
				fixed (byte* ptr = Unsafe.As<T, byte>(ref source))
				{
					byte* ptr6 = ptr;
					RuntimeImports.Memmove_wbarrier(ptr5, ptr6, (uint)elementCount, typeof(T).TypeHandle.Value);
					ptr3 = null;
				}
				return;
			}
		}
	}
}
