using System;

namespace System.Collections.Generic
{
	// Token: 0x02000774 RID: 1908
	[Serializable]
	internal class GenericEqualityComparer<T> : EqualityComparer<T> where T : IEquatable<T>
	{
		// Token: 0x06003CA5 RID: 15525 RVA: 0x000EA0BC File Offset: 0x000E82BC
		public override bool Equals(T x, T y)
		{
			if (x != null)
			{
				return y != null && x.Equals(y);
			}
			return y == null;
		}

		// Token: 0x06003CA6 RID: 15526 RVA: 0x000EA0EA File Offset: 0x000E82EA
		public override int GetHashCode(T obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return obj.GetHashCode();
		}

		// Token: 0x06003CA7 RID: 15527 RVA: 0x000EA104 File Offset: 0x000E8304
		internal override int IndexOf(T[] array, T value, int startIndex, int count)
		{
			int num = startIndex + count;
			if (value == null)
			{
				for (int i = startIndex; i < num; i++)
				{
					if (array[i] == null)
					{
						return i;
					}
				}
			}
			else
			{
				for (int j = startIndex; j < num; j++)
				{
					if (array[j] != null && array[j].Equals(value))
					{
						return j;
					}
				}
			}
			return -1;
		}

		// Token: 0x06003CA8 RID: 15528 RVA: 0x000EA170 File Offset: 0x000E8370
		internal override int LastIndexOf(T[] array, T value, int startIndex, int count)
		{
			int num = startIndex - count + 1;
			if (value == null)
			{
				for (int i = startIndex; i >= num; i--)
				{
					if (array[i] == null)
					{
						return i;
					}
				}
			}
			else
			{
				for (int j = startIndex; j >= num; j--)
				{
					if (array[j] != null && array[j].Equals(value))
					{
						return j;
					}
				}
			}
			return -1;
		}

		// Token: 0x06003CA9 RID: 15529 RVA: 0x000EA1DE File Offset: 0x000E83DE
		public override bool Equals(object obj)
		{
			return obj is GenericEqualityComparer<T>;
		}

		// Token: 0x06003CAA RID: 15530 RVA: 0x000E9DC2 File Offset: 0x000E7FC2
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}
	}
}
