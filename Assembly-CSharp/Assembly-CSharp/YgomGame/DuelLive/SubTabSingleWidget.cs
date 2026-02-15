using System;
using YgomSystem.ElementSystem;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.DuelLive
{
	// Token: 0x02000C70 RID: 3184
	public class SubTabSingleWidget : ElementWidgetBase, ISubTabWidget
	{
		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x06005B3A RID: 23354 RVA: 0x0000216A File Offset: 0x0000036A
		public DuelLiveTabWidget tabWidget
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x06005B3B RID: 23355 RVA: 0x0000216A File Offset: 0x0000036A
		public SubTabGroupWidget parentGroup
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06005B3C RID: 23356 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public SubTabSingleWidget(ElementObjectManager eom, SubTabGroupWidget parentGroupWidget = null)
			: base(null)
		{
		}

		// Token: 0x0400966A RID: 38506
		private readonly SubTabGroupWidget m_ParentGroupWidget;

		// Token: 0x0400966B RID: 38507
		private readonly DuelLiveTabWidget m_TabWidget;
	}
}
