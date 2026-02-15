using System;
using System.Collections.Generic;

namespace Unity.Properties
{
	// Token: 0x02000027 RID: 39
	public class DictionaryPropertyBag<TKey, TValue> : KeyValueCollectionPropertyBag<Dictionary<TKey, TValue>, TKey, TValue>
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000095 RID: 149 RVA: 0x000044A9 File Offset: 0x000026A9
		protected override InstantiationKind InstantiationKind
		{
			get
			{
				return InstantiationKind.PropertyBagOverride;
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004561 File Offset: 0x00002761
		protected override Dictionary<TKey, TValue> Instantiate()
		{
			return new Dictionary<TKey, TValue>();
		}
	}
}
