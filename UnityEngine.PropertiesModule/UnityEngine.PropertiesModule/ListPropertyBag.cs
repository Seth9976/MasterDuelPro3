using System;
using System.Collections.Generic;

namespace Unity.Properties
{
	// Token: 0x0200003F RID: 63
	public class ListPropertyBag<TElement> : IndexedCollectionPropertyBag<List<TElement>, TElement>
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000EB RID: 235 RVA: 0x000044A9 File Offset: 0x000026A9
		protected override InstantiationKind InstantiationKind
		{
			get
			{
				return InstantiationKind.PropertyBagOverride;
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004D1B File Offset: 0x00002F1B
		protected override List<TElement> InstantiateWithCount(int count)
		{
			return new List<TElement>(count);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004D23 File Offset: 0x00002F23
		protected override List<TElement> Instantiate()
		{
			return new List<TElement>();
		}
	}
}
