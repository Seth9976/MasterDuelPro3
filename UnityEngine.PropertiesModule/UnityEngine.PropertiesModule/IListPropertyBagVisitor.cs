using System;
using System.Collections.Generic;

namespace Unity.Properties
{
	// Token: 0x02000052 RID: 82
	public interface IListPropertyBagVisitor
	{
		// Token: 0x06000137 RID: 311
		void Visit<TList, TElement>(IListPropertyBag<TList, TElement> properties, ref TList container) where TList : IList<TElement>;
	}
}
