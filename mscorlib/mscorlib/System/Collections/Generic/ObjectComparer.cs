using System;

namespace System.Collections.Generic
{
	// Token: 0x02000771 RID: 1905
	[Serializable]
	internal class ObjectComparer<T> : Comparer<T>
	{
		// Token: 0x06003C96 RID: 15510 RVA: 0x000E9E2A File Offset: 0x000E802A
		public override int Compare(T x, T y)
		{
			return Comparer.Default.Compare(x, y);
		}

		// Token: 0x06003C97 RID: 15511 RVA: 0x000E9E42 File Offset: 0x000E8042
		public override bool Equals(object obj)
		{
			return obj is ObjectComparer<T>;
		}

		// Token: 0x06003C98 RID: 15512 RVA: 0x000E9DC2 File Offset: 0x000E7FC2
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}
	}
}
