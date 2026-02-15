using System;

namespace System.Collections.Generic
{
	// Token: 0x0200076F RID: 1903
	[Serializable]
	internal class GenericComparer<T> : Comparer<T> where T : IComparable<T>
	{
		// Token: 0x06003C8E RID: 15502 RVA: 0x000E9D89 File Offset: 0x000E7F89
		public override int Compare(T x, T y)
		{
			if (x != null)
			{
				if (y != null)
				{
					return x.CompareTo(y);
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

		// Token: 0x06003C8F RID: 15503 RVA: 0x000E9DB7 File Offset: 0x000E7FB7
		public override bool Equals(object obj)
		{
			return obj is GenericComparer<T>;
		}

		// Token: 0x06003C90 RID: 15504 RVA: 0x000E9DC2 File Offset: 0x000E7FC2
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}
	}
}
