using System;

namespace Unity.Properties
{
	// Token: 0x02000025 RID: 37
	public sealed class ArrayPropertyBag<TElement> : IndexedCollectionPropertyBag<TElement[], TElement>
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000044A9 File Offset: 0x000026A9
		protected override InstantiationKind InstantiationKind
		{
			get
			{
				return InstantiationKind.PropertyBagOverride;
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000044AC File Offset: 0x000026AC
		protected override TElement[] InstantiateWithCount(int count)
		{
			return new TElement[count];
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000044B4 File Offset: 0x000026B4
		protected override TElement[] Instantiate()
		{
			return Array.Empty<TElement>();
		}
	}
}
