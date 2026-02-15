using System;
using System.Collections.Generic;

namespace Unity.Properties
{
	// Token: 0x02000054 RID: 84
	public interface IDictionaryPropertyBagVisitor
	{
		// Token: 0x06000139 RID: 313
		void Visit<TDictionary, TKey, TValue>(IDictionaryPropertyBag<TDictionary, TKey, TValue> properties, ref TDictionary container) where TDictionary : IDictionary<TKey, TValue>;
	}
}
