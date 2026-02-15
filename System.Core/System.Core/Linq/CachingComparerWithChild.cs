using System;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x0200004B RID: 75
	internal sealed class CachingComparerWithChild<TElement, TKey> : CachingComparer<TElement, TKey>
	{
		// Token: 0x0600024E RID: 590 RVA: 0x0000AEB0 File Offset: 0x000090B0
		public CachingComparerWithChild(Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending, CachingComparer<TElement> child)
			: base(keySelector, comparer, descending)
		{
			this._child = child;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000AEC4 File Offset: 0x000090C4
		internal override int Compare(TElement element, bool cacheLower)
		{
			TKey tkey = this._keySelector(element);
			int num = (this._descending ? this._comparer.Compare(this._lastKey, tkey) : this._comparer.Compare(tkey, this._lastKey));
			if (num == 0)
			{
				return this._child.Compare(element, cacheLower);
			}
			if (cacheLower == num < 0)
			{
				this._lastKey = tkey;
				this._child.SetElement(element);
			}
			return num;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000AF39 File Offset: 0x00009139
		internal override void SetElement(TElement element)
		{
			base.SetElement(element);
			this._child.SetElement(element);
		}

		// Token: 0x040000C9 RID: 201
		private readonly CachingComparer<TElement> _child;
	}
}
