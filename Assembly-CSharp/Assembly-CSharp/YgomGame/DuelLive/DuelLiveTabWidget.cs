using System;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.DuelLive
{
	// Token: 0x02000C5B RID: 3163
	public class DuelLiveTabWidget : ElementWidgetBase
	{
		// Token: 0x06005A48 RID: 23112 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public DuelLiveTabWidget(ElementObjectManager eom, bool isOn = false)
			: base(null)
		{
		}

		// Token: 0x06005A49 RID: 23113 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetLabel(string label)
		{
		}

		// Token: 0x06005A4A RID: 23114 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetIsOn(bool isOn)
		{
		}

		// Token: 0x040095C1 RID: 38337
		private readonly string k_ELabelButton;

		// Token: 0x040095C2 RID: 38338
		private readonly string k_ELabelOn;

		// Token: 0x040095C3 RID: 38339
		private readonly string k_ELabelOff;

		// Token: 0x040095C4 RID: 38340
		private const string k_ELabelOnText = "OnText";

		// Token: 0x040095C5 RID: 38341
		private const string k_ELabelOffText = "OffText";

		// Token: 0x040095C6 RID: 38342
		private const string k_ELabelBadge = "Badge";

		// Token: 0x040095C7 RID: 38343
		private const string k_ELabelExIconRoot = "EXIconRoot";

		// Token: 0x040095C8 RID: 38344
		public readonly SelectionButton button;

		// Token: 0x040095C9 RID: 38345
		public readonly GameObject onImage;

		// Token: 0x040095CA RID: 38346
		public readonly GameObject offImage;

		// Token: 0x040095CB RID: 38347
		public readonly GameObject badge;

		// Token: 0x040095CC RID: 38348
		public readonly GameObject ExIconRoot;
	}
}
