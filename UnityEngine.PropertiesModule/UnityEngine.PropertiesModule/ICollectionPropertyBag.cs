using System;
using System.Collections.Generic;

namespace Unity.Properties
{
	// Token: 0x02000034 RID: 52
	public interface ICollectionPropertyBag<TCollection, TElement> : IPropertyBag<TCollection>, IPropertyBag, ICollectionPropertyBagAccept<TCollection> where TCollection : ICollection<TElement>
	{
	}
}
