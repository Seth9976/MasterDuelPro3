using System;
using System.Collections.Generic;

namespace Unity.Properties
{
	// Token: 0x02000051 RID: 81
	public interface ICollectionPropertyBagVisitor
	{
		// Token: 0x06000136 RID: 310
		void Visit<TCollection, TElement>(ICollectionPropertyBag<TCollection, TElement> properties, ref TCollection container) where TCollection : ICollection<TElement>;
	}
}
