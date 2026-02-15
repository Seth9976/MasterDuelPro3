using System;

namespace System.Linq
{
	// Token: 0x02000049 RID: 73
	internal abstract class CachingComparer<TElement>
	{
		// Token: 0x06000248 RID: 584
		internal abstract int Compare(TElement element, bool cacheLower);

		// Token: 0x06000249 RID: 585
		internal abstract void SetElement(TElement element);
	}
}
