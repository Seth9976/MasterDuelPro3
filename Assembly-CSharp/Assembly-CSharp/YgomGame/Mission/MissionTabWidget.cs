using System;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Mission
{
	// Token: 0x02000A3C RID: 2620
	public class MissionTabWidget : ElementWidgetBase
	{
		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06004C0B RID: 19467 RVA: 0x0000216A File Offset: 0x0000036A
		private GameObject onRoot
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06004C0C RID: 19468 RVA: 0x0000216A File Offset: 0x0000036A
		private GameObject offRoot
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000705 RID: 1797
		// (set) Token: 0x06004C0D RID: 19469 RVA: 0x0000216D File Offset: 0x0000036D
		public string label
		{
			set
			{
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06004C0E RID: 19470 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06004C0F RID: 19471 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isOn
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000707 RID: 1799
		// (set) Token: 0x06004C10 RID: 19472 RVA: 0x0000216D File Offset: 0x0000036D
		public bool badgeVisible
		{
			set
			{
			}
		}

		// Token: 0x17000708 RID: 1800
		// (set) Token: 0x06004C11 RID: 19473 RVA: 0x0000216D File Offset: 0x0000036D
		public bool numBadgeVisible
		{
			set
			{
			}
		}

		// Token: 0x17000709 RID: 1801
		// (set) Token: 0x06004C12 RID: 19474 RVA: 0x0000216D File Offset: 0x0000036D
		public int numBadgeCnt
		{
			set
			{
			}
		}

		// Token: 0x1700070A RID: 1802
		// (set) Token: 0x06004C13 RID: 19475 RVA: 0x0000216D File Offset: 0x0000036D
		public bool completeVisible
		{
			set
			{
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06004C14 RID: 19476 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionButton button
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004C15 RID: 19477 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public MissionTabWidget(ElementObjectManager eom)
			: base(null)
		{
		}
	}
}
