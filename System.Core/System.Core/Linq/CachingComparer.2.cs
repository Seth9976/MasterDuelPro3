using System;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x0200004A RID: 74
	internal class CachingComparer<TElement, TKey> : CachingComparer<TElement>
	{
		// Token: 0x0600024B RID: 587 RVA: 0x0000AE24 File Offset: 0x00009024
		public CachingComparer(Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending)
		{
			this._keySelector = keySelector;
			this._comparer = comparer;
			this._descending = descending;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000AE44 File Offset: 0x00009044
		internal override int Compare(TElement element, bool cacheLower)
		{
			TKey tkey = this._keySelector(element);
			int num = (this._descending ? this._comparer.Compare(this._lastKey, tkey) : this._comparer.Compare(tkey, this._lastKey));
			if (cacheLower == num < 0)
			{
				this._lastKey = tkey;
			}
			return num;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000AE9C File Offset: 0x0000909C
		internal override void SetElement(TElement element)
		{
			this._lastKey = this._keySelector(element);
		}

		// Token: 0x040000C5 RID: 197
		protected readonly Func<TElement, TKey> _keySelector;

		// Token: 0x040000C6 RID: 198
		protected readonly IComparer<TKey> _comparer;

		// Token: 0x040000C7 RID: 199
		protected readonly bool _descending;

		// Token: 0x040000C8 RID: 200
		protected TKey _lastKey;
	}
}
