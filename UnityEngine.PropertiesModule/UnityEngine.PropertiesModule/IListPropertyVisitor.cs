using System;
using System.Collections.Generic;

namespace Unity.Properties
{
	// Token: 0x02000056 RID: 86
	public interface IListPropertyVisitor
	{
		// Token: 0x0600013B RID: 315
		void Visit<TContainer, TList, TElement>(Property<TContainer, TList> property, ref TContainer container, ref TList list) where TList : IList<TElement>;
	}
}
