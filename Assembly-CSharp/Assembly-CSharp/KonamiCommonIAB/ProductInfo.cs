using System;

namespace KonamiCommonIAB
{
	// Token: 0x020011A2 RID: 4514
	public abstract class ProductInfo
	{
		// Token: 0x17001115 RID: 4373
		// (get) Token: 0x06008724 RID: 34596 RVA: 0x0000216A File Offset: 0x0000036A
		public string json
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001116 RID: 4374
		// (get) Token: 0x06008725 RID: 34597
		public abstract string productId { get; }

		// Token: 0x17001117 RID: 4375
		// (get) Token: 0x06008726 RID: 34598
		public abstract string title { get; }

		// Token: 0x17001118 RID: 4376
		// (get) Token: 0x06008727 RID: 34599
		public abstract string description { get; }

		// Token: 0x17001119 RID: 4377
		// (get) Token: 0x06008728 RID: 34600
		public abstract string displayedPrice { get; }

		// Token: 0x1700111A RID: 4378
		// (get) Token: 0x06008729 RID: 34601
		public abstract string currency { get; }

		// Token: 0x1700111B RID: 4379
		// (get) Token: 0x0600872A RID: 34602
		public abstract float price { get; }
	}
}
