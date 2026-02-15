using System;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x0200003B RID: 59
	internal readonly struct Buffer<TElement>
	{
		// Token: 0x060001EE RID: 494 RVA: 0x00009EDC File Offset: 0x000080DC
		internal Buffer(IEnumerable<TElement> source)
		{
			IIListProvider<TElement> iilistProvider = source as IIListProvider<TElement>;
			if (iilistProvider != null)
			{
				TElement[] array = iilistProvider.ToArray();
				this._items = array;
				this._count = array.Length;
				return;
			}
			this._items = global::System.Collections.Generic.EnumerableHelpers.ToArray<TElement>(source, out this._count);
		}

		// Token: 0x0400009C RID: 156
		internal readonly TElement[] _items;

		// Token: 0x0400009D RID: 157
		internal readonly int _count;
	}
}
