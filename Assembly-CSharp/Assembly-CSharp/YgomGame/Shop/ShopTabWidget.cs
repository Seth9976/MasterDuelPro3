using System;
using System.Collections.Generic;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Shop
{
	// Token: 0x0200096A RID: 2410
	public class ShopTabWidget : ElementWidgetBase
	{
		// Token: 0x06004658 RID: 18008 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public ShopTabWidget(ElementObjectManager eom, bool isOn = false)
			: base(null)
		{
		}

		// Token: 0x06004659 RID: 18009 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetLabel(string label)
		{
		}

		// Token: 0x0600465A RID: 18010 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetIsOn(bool isOn)
		{
		}

		// Token: 0x040084BA RID: 33978
		private readonly string k_ELabelButton;

		// Token: 0x040084BB RID: 33979
		private readonly string k_ELabelOn;

		// Token: 0x040084BC RID: 33980
		private readonly string k_ELabelOff;

		// Token: 0x040084BD RID: 33981
		private const string k_ELabelOnText = "OnText";

		// Token: 0x040084BE RID: 33982
		private const string k_ELabelOffText = "OffText";

		// Token: 0x040084BF RID: 33983
		private const string k_ELabelBadge = "Badge";

		// Token: 0x040084C0 RID: 33984
		private const string k_TLabelToOn = "ToOn";

		// Token: 0x040084C1 RID: 33985
		public readonly SelectionButton button;

		// Token: 0x040084C2 RID: 33986
		public readonly GameObject onImage;

		// Token: 0x040084C3 RID: 33987
		public readonly GameObject offImage;

		// Token: 0x040084C4 RID: 33988
		public readonly GameObject badge;

		// Token: 0x040084C5 RID: 33989
		private bool m_ToOnDirty;

		// Token: 0x040084C6 RID: 33990
		private List<Tween> m_TCache_ToOn;
	}
}
