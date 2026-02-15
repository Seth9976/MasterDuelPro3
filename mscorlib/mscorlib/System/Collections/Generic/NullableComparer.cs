using System;

namespace System.Collections.Generic
{
	// Token: 0x02000770 RID: 1904
	[Serializable]
	internal class NullableComparer<T> : Comparer<T?> where T : struct, IComparable<T>
	{
		// Token: 0x06003C92 RID: 15506 RVA: 0x000E9DDC File Offset: 0x000E7FDC
		public override int Compare(T? x, T? y)
		{
			if (x != null)
			{
				if (y != null)
				{
					return x.value.CompareTo(y.value);
				}
				return 1;
			}
			else
			{
				if (y != null)
				{
					return -1;
				}
				return 0;
			}
		}

		// Token: 0x06003C93 RID: 15507 RVA: 0x000E9E17 File Offset: 0x000E8017
		public override bool Equals(object obj)
		{
			return obj is NullableComparer<T>;
		}

		// Token: 0x06003C94 RID: 15508 RVA: 0x000E9DC2 File Offset: 0x000E7FC2
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}
	}
}
