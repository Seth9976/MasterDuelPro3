using System;
using System.Collections.Generic;

namespace Unity.Properties
{
	// Token: 0x02000053 RID: 83
	public interface ISetPropertyBagVisitor
	{
		// Token: 0x06000138 RID: 312
		void Visit<TSet, TValue>(ISetPropertyBag<TSet, TValue> properties, ref TSet container) where TSet : ISet<TValue>;
	}
}
