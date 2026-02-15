using System;
using UnityEngine.UI;
using YgomSystem.ElementSystem;
using YgomSystem.UI.ElementWidget;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.Mission
{
	// Token: 0x02000A3A RID: 2618
	public class MissionTabListWidget : ElementWidgetBase
	{
		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06004C09 RID: 19465 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isSelected
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004C0A RID: 19466 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public MissionTabListWidget(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x04008A09 RID: 35337
		public readonly InfinityScrollView scrollView;

		// Token: 0x04008A0A RID: 35338
		public readonly ScrollRect scrollRect;
	}
}
