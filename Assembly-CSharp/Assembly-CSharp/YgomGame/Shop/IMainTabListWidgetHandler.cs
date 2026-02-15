using System;
using System.Collections.Generic;

namespace YgomGame.Shop
{
	// Token: 0x0200092B RID: 2347
	public interface IMainTabListWidgetHandler
	{
		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06004480 RID: 17536
		int currentIdx { get; }

		// Token: 0x06004481 RID: 17537
		void OnUpdateMainTabDataCount(IReadOnlyList<ShopTabWidget> sourceTabWidgets, List<ShopTabWidget> activeTabWidets);

		// Token: 0x06004482 RID: 17538
		void OnUpdateMainTabData(IReadOnlyList<ShopTabWidget> activeTabWidets);
	}
}
