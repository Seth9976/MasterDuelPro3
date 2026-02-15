using System;
using TMPro;
using YgomSystem.ElementSystem;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.DuelLive
{
	// Token: 0x02000C67 RID: 3175
	public class ProductGroupHeaderWidget : ElementWidgetBase
	{
		// Token: 0x06005AB2 RID: 23218 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public ProductGroupHeaderWidget(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x040095E9 RID: 38377
		private const string k_ELabelLabel = "Label";

		// Token: 0x040095EA RID: 38378
		public readonly TMP_Text label;
	}
}
