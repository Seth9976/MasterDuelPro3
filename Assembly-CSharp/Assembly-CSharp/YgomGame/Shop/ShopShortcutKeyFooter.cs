using System;
using TMPro;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Shop
{
	// Token: 0x02000967 RID: 2407
	public class ShopShortcutKeyFooter : ElementWidgetBase
	{
		// Token: 0x06004650 RID: 18000 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public ShopShortcutKeyFooter(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x040084B2 RID: 33970
		private readonly string k_ELabelPlayButton;

		// Token: 0x040084B3 RID: 33971
		private readonly string k_ELabelPlayIcon;

		// Token: 0x040084B4 RID: 33972
		private readonly string k_ELabelPlayText;

		// Token: 0x040084B5 RID: 33973
		public readonly SelectionButton playButton;

		// Token: 0x040084B6 RID: 33974
		public readonly GameObject playIcon;

		// Token: 0x040084B7 RID: 33975
		public readonly TMP_Text playText;
	}
}
