using System;

namespace System.Collections.Generic
{
	// Token: 0x02000772 RID: 1906
	[Serializable]
	internal class ComparisonComparer<T> : Comparer<T>
	{
		// Token: 0x06003C9A RID: 15514 RVA: 0x000E9E4D File Offset: 0x000E804D
		public ComparisonComparer(Comparison<T> comparison)
		{
			this._comparison = comparison;
		}

		// Token: 0x06003C9B RID: 15515 RVA: 0x000E9E5C File Offset: 0x000E805C
		public override int Compare(T x, T y)
		{
			return this._comparison(x, y);
		}

		// Token: 0x04001F54 RID: 8020
		private readonly Comparison<T> _comparison;
	}
}
