using System;
using TMPro;
using YgomSystem.ElementSystem;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Shop
{
	// Token: 0x0200093C RID: 2364
	public class ProductGroupHeaderWidget : ElementWidgetBase
	{
		// Token: 0x06004539 RID: 17721 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public ProductGroupHeaderWidget(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x04008342 RID: 33602
		private const string k_ELabelLabel = "Label";

		// Token: 0x04008343 RID: 33603
		public readonly TMP_Text label;
	}
}
