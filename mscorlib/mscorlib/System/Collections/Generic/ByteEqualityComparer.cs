using System;

namespace System.Collections.Generic
{
	// Token: 0x02000777 RID: 1911
	[Serializable]
	internal class ByteEqualityComparer : EqualityComparer<byte>
	{
		// Token: 0x06003CBA RID: 15546 RVA: 0x000EA466 File Offset: 0x000E8666
		public override bool Equals(byte x, byte y)
		{
			return x == y;
		}

		// Token: 0x06003CBB RID: 15547 RVA: 0x000EA46C File Offset: 0x000E866C
		public override int GetHashCode(byte b)
		{
			return b.GetHashCode();
		}

		// Token: 0x06003CBC RID: 15548 RVA: 0x000EA478 File Offset: 0x000E8678
		internal unsafe override int IndexOf(byte[] array, byte value, int startIndex, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Count must be positive and count must refer to a location within the string/array/collection."));
			}
			if (count > array.Length - startIndex)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			if (count == 0)
			{
				return -1;
			}
			byte* ptr;
			if (array == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			return Buffer.IndexOfByte(ptr, value, startIndex, count);
		}

		// Token: 0x06003CBD RID: 15549 RVA: 0x000EA508 File Offset: 0x000E8708
		internal override int LastIndexOf(byte[] array, byte value, int startIndex, int count)
		{
			int num = startIndex - count + 1;
			for (int i = startIndex; i >= num; i--)
			{
				if (array[i] == value)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06003CBE RID: 15550 RVA: 0x000EA531 File Offset: 0x000E8731
		public override bool Equals(object obj)
		{
			return obj is ByteEqualityComparer;
		}

		// Token: 0x06003CBF RID: 15551 RVA: 0x000E9DC2 File Offset: 0x000E7FC2
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}
	}
}
