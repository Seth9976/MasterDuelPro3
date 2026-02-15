using System;
using TMPro;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Mission
{
	// Token: 0x02000A29 RID: 2601
	public class MissionBulkRecieveButtonWidget : ElementWidgetBase
	{
		// Token: 0x06004B6D RID: 19309 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public MissionBulkRecieveButtonWidget(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x04008963 RID: 35171
		public readonly SelectionButton button;

		// Token: 0x04008964 RID: 35172
		public readonly GameObject numBadge;

		// Token: 0x04008965 RID: 35173
		public readonly TMP_Text numBadgeText;
	}
}
