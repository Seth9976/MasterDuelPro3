using System;
using System.Collections.Generic;

namespace YgomGame.Shop
{
	// Token: 0x0200092E RID: 2350
	public interface IProductListWidgetHandler : IProductContainerWidgetHandler
	{
		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06004489 RID: 17545
		ProductWidgetController productWidgetController { get; }

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x0600448A RID: 17546
		int showcaseUnloadUnusedCnt { get; }

		// Token: 0x0600448B RID: 17547
		bool EqualCurrentCategoryId(int chkCategoryId, int chkSubCategoryId, int chkSectionId);

		// Token: 0x0600448C RID: 17548
		void OnUpdateDataCount(List<int> templateIdxList, List<string> headerLabels, Dictionary<int, Dictionary<int, Dictionary<int, int>>> headerDataIdxMap, List<ProductContainerWidget.Context> productContainerCtxs);
	}
}
