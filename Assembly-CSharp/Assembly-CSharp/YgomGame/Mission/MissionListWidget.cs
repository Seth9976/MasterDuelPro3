using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.Mission
{
	// Token: 0x02000A35 RID: 2613
	public class MissionListWidget : ElementWidgetBase
	{
		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06004BC9 RID: 19401 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject fadeTargetGo
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004BCA RID: 19402 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public MissionListWidget(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x06004BCB RID: 19403 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnInitializedScrollView(Dictionary<GameObject, MissionPanelWidget> panelWidgetsMap)
		{
		}

		// Token: 0x06004BCC RID: 19404 RVA: 0x0000216A File Offset: 0x0000036A
		public IEnumerator yPlayFadeIn()
		{
			return null;
		}

		// Token: 0x06004BCD RID: 19405 RVA: 0x0000216A File Offset: 0x0000036A
		public IEnumerator yPlayFadeOut()
		{
			return null;
		}

		// Token: 0x06004BCE RID: 19406 RVA: 0x0000216A File Offset: 0x0000036A
		public IEnumerator yPlayFade(string tweenFadeKey)
		{
			return null;
		}

		// Token: 0x06004BCF RID: 19407 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetPlayFocusPanelSpeed(float speed)
		{
		}

		// Token: 0x06004BD0 RID: 19408 RVA: 0x0000216A File Offset: 0x0000036A
		public IEnumerator yPlayFocusPanel(int idx)
		{
			return null;
		}

		// Token: 0x040089CE RID: 35278
		public readonly int k_NormalTemplateIdx;

		// Token: 0x040089CF RID: 35279
		public readonly int k_HeaderTemplateIdx;

		// Token: 0x040089D0 RID: 35280
		public readonly string k_TweenFadeIn;

		// Token: 0x040089D1 RID: 35281
		public readonly string k_TweenFadeOut;

		// Token: 0x040089D2 RID: 35282
		public readonly string k_TweenOnRecieveFocusScroll;

		// Token: 0x040089D3 RID: 35283
		public readonly InfinityScrollView scrollView;

		// Token: 0x040089D4 RID: 35284
		public readonly ScrollRect scrollRect;

		// Token: 0x040089D5 RID: 35285
		public readonly TMP_Text tabNameText;

		// Token: 0x040089D6 RID: 35286
		private TweenPosition m_TweenOnRecieveFocusScrollPosition;

		// Token: 0x040089D7 RID: 35287
		private RectOffset m_OriginalPadding;

		// Token: 0x040089D8 RID: 35288
		private float m_OriginalSpacing;

		// Token: 0x040089D9 RID: 35289
		private float m_EntityHeight;
	}
}
