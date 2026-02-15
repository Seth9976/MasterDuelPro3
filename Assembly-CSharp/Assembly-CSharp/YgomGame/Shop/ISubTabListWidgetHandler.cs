using System;
using System.Collections.Generic;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Shop
{
	// Token: 0x02000932 RID: 2354
	public interface ISubTabListWidgetHandler
	{
		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06004499 RID: 17561
		int currentIdx { get; }

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x0600449A RID: 17562
		int currentSectionIdx { get; }

		// Token: 0x0600449B RID: 17563
		ValueTuple<int, int> CategoryIdOfIndex(int dataIdx);

		// Token: 0x0600449C RID: 17564
		void OnUpdateSubTabWidget(List<int> templateIds);

		// Token: 0x0600449D RID: 17565
		void OnUpdateTabWidget(ShopTabWidget widget, int dataIdx);

		// Token: 0x0600449E RID: 17566
		void OnUpdateSectionFactory(ElementEntityFactory entityFactory, int dataIdx);

		// Token: 0x0600449F RID: 17567
		void OnUpdateSectionTabWidget(ShopTabWidget widget, int dataIdx, int sectionIdx);
	}
}
