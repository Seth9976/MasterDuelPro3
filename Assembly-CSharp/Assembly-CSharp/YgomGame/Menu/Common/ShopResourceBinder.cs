using System;
using UnityEngine;
using YgomGame.Shop;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B60 RID: 2912
	public class ShopResourceBinder : ResourceBinderBase
	{
		// Token: 0x06005436 RID: 21558 RVA: 0x000F4C2A File Offset: 0x000F2E2A
		public ShopResourceBinder(string cardThumbSettingPath, string highlightThumbImagePath, string highlightThumbPrefPath)
		{
		}

		// Token: 0x06005437 RID: 21559 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingShopProductThumb BindPackThumb(RectTransform target, int thumbType, string thumbData, ShopCardThumbSettings.Format thumbFormat)
		{
			return null;
		}

		// Token: 0x06005438 RID: 21560 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetHighlightThumbImagePath(string name)
		{
			return null;
		}

		// Token: 0x06005439 RID: 21561 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetHighlightThumbPrefPath(string name)
		{
			return null;
		}

		// Token: 0x040091B5 RID: 37301
		public readonly string cardThumbSettingPath;

		// Token: 0x040091B6 RID: 37302
		private readonly string m_HighlightThumbImagePath;

		// Token: 0x040091B7 RID: 37303
		private readonly string m_HighlightThumbPrefPath;
	}
}
