using System;

namespace YgomGame.Shop
{
	// Token: 0x02000934 RID: 2356
	public interface ISubTabWidget
	{
		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x060044A4 RID: 17572
		SubTabGroupWidget parentGroup { get; }

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x060044A5 RID: 17573
		ShopTabWidget tabWidget { get; }
	}
}
