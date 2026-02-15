using System;
using System.Collections.Generic;

namespace Unity.Properties
{
	// Token: 0x02000028 RID: 40
	public class HashSetPropertyBag<TElement> : SetPropertyBagBase<HashSet<TElement>, TElement>
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000044A9 File Offset: 0x000026A9
		protected override InstantiationKind InstantiationKind
		{
			get
			{
				return InstantiationKind.PropertyBagOverride;
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004571 File Offset: 0x00002771
		protected override HashSet<TElement> Instantiate()
		{
			return new HashSet<TElement>();
		}
	}
}
