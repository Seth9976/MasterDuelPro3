using System;

namespace System.Collections.Generic
{
	// Token: 0x02000776 RID: 1910
	[Serializable]
	internal class ObjectEqualityComparer<T> : EqualityComparer<T>
	{
		// Token: 0x06003CB3 RID: 15539 RVA: 0x000EA33E File Offset: 0x000E853E
		public override bool Equals(T x, T y)
		{
			if (x != null)
			{
				return y != null && x.Equals(y);
			}
			return y == null;
		}

		// Token: 0x06003CB4 RID: 15540 RVA: 0x000EA0EA File Offset: 0x000E82EA
		public override int GetHashCode(T obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return obj.GetHashCode();
		}

		// Token: 0x06003CB5 RID: 15541 RVA: 0x000EA374 File Offset: 0x000E8574
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

		// Token: 0x06003CB6 RID: 15542 RVA: 0x000EA3E8 File Offset: 0x000E85E8
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

		// Token: 0x06003CB7 RID: 15543 RVA: 0x000EA45B File Offset: 0x000E865B
		public override bool Equals(object obj)
		{
			return obj is ObjectEqualityComparer<T>;
		}

		// Token: 0x06003CB8 RID: 15544 RVA: 0x000E9DC2 File Offset: 0x000E7FC2
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}
	}
}
