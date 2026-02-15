using System;
using System.Collections.Generic;

namespace Unity.Properties
{
	// Token: 0x02000037 RID: 55
	public interface IDictionaryPropertyBag<TDictionary, TKey, TValue> : ICollectionPropertyBag<TDictionary, KeyValuePair<TKey, TValue>>, IPropertyBag<TDictionary>, IPropertyBag, ICollectionPropertyBagAccept<TDictionary>, IDictionaryPropertyBagAccept<TDictionary>, IKeyedProperties<TDictionary, object> where TDictionary : IDictionary<TKey, TValue>
	{
	}
}
