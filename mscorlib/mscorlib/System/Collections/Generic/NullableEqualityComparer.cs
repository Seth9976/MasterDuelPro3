using System;

namespace System.Collections.Generic
{
	// Token: 0x02000775 RID: 1909
	[Serializable]
	internal class NullableEqualityComparer<T> : EqualityComparer<T?> where T : struct, IEquatable<T>
	{
		// Token: 0x06003CAC RID: 15532 RVA: 0x000EA1F1 File Offset: 0x000E83F1
		public override bool Equals(T? x, T? y)
		{
			if (x != null)
			{
				return y != null && x.value.Equals(y.value);
			}
			return y == null;
		}

		// Token: 0x06003CAD RID: 15533 RVA: 0x000EA22C File Offset: 0x000E842C
		public override int GetHashCode(T? obj)
		{
			return obj.GetHashCode();
		}

		// Token: 0x06003CAE RID: 15534 RVA: 0x000EA23C File Offset: 0x000E843C
		internal override int IndexOf(T?[] array, T? value, int startIndex, int count)
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
					if (array[j] != null && array[j].value.Equals(value.value))
					{
						return j;
					}
				}
			}
			return -1;
		}

		// Token: 0x06003CAF RID: 15535 RVA: 0x000EA2B4 File Offset: 0x000E84B4
		internal override int LastIndexOf(T?[] array, T? value, int startIndex, int count)
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
					if (array[j] != null && array[j].value.Equals(value.value))
					{
						return j;
					}
				}
			}
			return -1;
		}

		// Token: 0x06003CB0 RID: 15536 RVA: 0x000EA32B File Offset: 0x000E852B
		public override bool Equals(object obj)
		{
			return obj is NullableEqualityComparer<T>;
		}

		// Token: 0x06003CB1 RID: 15537 RVA: 0x000E9DC2 File Offset: 0x000E7FC2
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}
	}
}
