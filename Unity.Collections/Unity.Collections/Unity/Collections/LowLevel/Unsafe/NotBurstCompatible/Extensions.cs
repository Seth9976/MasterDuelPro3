using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections.LowLevel.Unsafe.NotBurstCompatible
{
	// Token: 0x0200014E RID: 334
	public static class Extensions
	{
		// Token: 0x06000DC9 RID: 3529 RVA: 0x0002A990 File Offset: 0x00028B90
		public static T[] ToArray<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> set) where T : struct, ValueType, IEquatable<T>
		{
			NativeArray<T> array = set.ToNativeArray(Allocator.TempJob);
			T[] array2 = array.ToArray();
			array.Dispose();
			return array2;
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x0002A9BC File Offset: 0x00028BBC
		[ExcludeFromBurstCompatTesting("Takes managed string")]
		public unsafe static void AddNBC(this UnsafeAppendBuffer buffer, string value)
		{
			if (value != null)
			{
				buffer.Add<int>(value.Length);
				fixed (string text = value)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					buffer.Add((void*)ptr, 2 * value.Length);
				}
				return;
			}
			buffer.Add<int>(-1);
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x0002AA04 File Offset: 0x00028C04
		[ExcludeFromBurstCompatTesting("Returns managed array")]
		public unsafe static byte[] ToBytesNBC(this UnsafeAppendBuffer buffer)
		{
			byte[] array2;
			byte[] array = (array2 = new byte[buffer.Length]);
			byte* dstPtr;
			if (array == null || array2.Length == 0)
			{
				dstPtr = null;
			}
			else
			{
				dstPtr = &array2[0];
			}
			UnsafeUtility.MemCpy((void*)dstPtr, (void*)buffer.Ptr, (long)buffer.Length);
			array2 = null;
			return array;
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x0002AA4C File Offset: 0x00028C4C
		[ExcludeFromBurstCompatTesting("Managed string out argument")]
		public unsafe static void ReadNextNBC(this UnsafeAppendBuffer.Reader reader, out string value)
		{
			int length;
			reader.ReadNext<int>(out length);
			if (length != -1)
			{
				value = new string('0', length);
				fixed (string text = value)
				{
					char* buf = text;
					if (buf != null)
					{
						buf += RuntimeHelpers.OffsetToStringData / 2;
					}
					int bufLen = length * 2;
					UnsafeUtility.MemCpy((void*)buf, reader.ReadNext(bufLen), (long)bufLen);
				}
				return;
			}
			value = null;
		}
	}
}
