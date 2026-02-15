using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.Mission
{
	// Token: 0x02000A33 RID: 2611
	public class MissionGoalsPagerWidget : ElementWidgetBase
	{
		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06004BC0 RID: 19392 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject shortcutKeyIconR
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06004BC1 RID: 19393 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject shortcutKeyIconL
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06004BC2 RID: 19394 RVA: 0x0000216A File Offset: 0x0000036A
		public MissionGoalsWidget CurrentGoalsWidget
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004BC3 RID: 19395 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsEnabledPrev()
		{
			return false;
		}

		// Token: 0x06004BC4 RID: 19396 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsEnabledNext()
		{
			return false;
		}

		// Token: 0x06004BC5 RID: 19397 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public MissionGoalsPagerWidget(ElementObjectManager eom, MissionPanelWidget ownerPanel)
			: base(null)
		{
		}

		// Token: 0x06004BC6 RID: 19398 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnPagingTweenEnd()
		{
		}

		// Token: 0x06004BC7 RID: 19399 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnPageChanged()
		{
		}

		// Token: 0x040089AD RID: 35245
		private string k_ELabelShortcutKeyIconR;

		// Token: 0x040089AE RID: 35246
		private string k_ELabelShortcutKeyIconL;

		// Token: 0x040089AF RID: 35247
		public readonly MissionPanelWidget ownerPanel;

		// Token: 0x040089B0 RID: 35248
		public readonly Selector selector;

		// Token: 0x040089B1 RID: 35249
		public readonly ScrollRectPageSnap pageSnap;

		// Token: 0x040089B2 RID: 35250
		public readonly ScrollRectPageSnapButtons pageButtons;

		// Token: 0x040089B3 RID: 35251
		public readonly InfinityScrollView scrollView;

		// Token: 0x040089B4 RID: 35252
		public readonly ScrollRect scrollRect;

		// Token: 0x040089B5 RID: 35253
		public readonly List<MissionGoalsWidget> goalsWidgets;

		// Token: 0x040089B6 RID: 35254
		public Action<MissionGoalsPagerWidget> onPageChangedCallback;

		// Token: 0x040089B7 RID: 35255
		public Action onPlayPagingBeginCallback;

		// Token: 0x040089B8 RID: 35256
		public Action onPlayPagingEndCallback;
	}
}
