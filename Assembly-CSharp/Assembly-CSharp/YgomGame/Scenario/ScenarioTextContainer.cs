using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.Scenario
{
	// Token: 0x020009E4 RID: 2532
	public class ScenarioTextContainer : ScenarioContainerBase
	{
		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06004996 RID: 18838 RVA: 0x0000216A File Offset: 0x0000036A
		public TMP_FontAsset fontAsset
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004997 RID: 18839 RVA: 0x000F4916 File Offset: 0x000F2B16
		public ScenarioTextContainer(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x06004998 RID: 18840 RVA: 0x0000216A File Offset: 0x0000036A
		public TextMeshProUGUI RentText(int pos)
		{
			return null;
		}

		// Token: 0x06004999 RID: 18841 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReturnText(TextMeshProUGUI rentedText)
		{
		}

		// Token: 0x0600499A RID: 18842 RVA: 0x0000216A File Offset: 0x0000036A
		private Transform GetTextGroup(int pos)
		{
			return null;
		}

		// Token: 0x0600499B RID: 18843 RVA: 0x0000216D File Offset: 0x0000036D
		public void ShowArrow()
		{
		}

		// Token: 0x0600499C RID: 18844 RVA: 0x0000216D File Offset: 0x0000036D
		public void HideArrow()
		{
		}

		// Token: 0x0600499D RID: 18845 RVA: 0x0000216D File Offset: 0x0000036D
		public void ShowTexts()
		{
		}

		// Token: 0x0600499E RID: 18846 RVA: 0x0000216D File Offset: 0x0000036D
		public void HideTexts()
		{
		}

		// Token: 0x04008774 RID: 34676
		private readonly string k_ELabelCenterGroup;

		// Token: 0x04008775 RID: 34677
		private readonly string k_ELabelLeftGroup;

		// Token: 0x04008776 RID: 34678
		private readonly string k_ELabelRightGroup;

		// Token: 0x04008777 RID: 34679
		private readonly string k_ELabelBottomGroup;

		// Token: 0x04008778 RID: 34680
		private readonly string k_ELabelMessageTextTemplate;

		// Token: 0x04008779 RID: 34681
		private readonly string k_ELabelNextArrow;

		// Token: 0x0400877A RID: 34682
		private readonly string k_TweenShowArrow;

		// Token: 0x0400877B RID: 34683
		private readonly string k_TweenHideArrow;

		// Token: 0x0400877C RID: 34684
		private readonly Tween m_ShowArrowTween;

		// Token: 0x0400877D RID: 34685
		private readonly Tween m_HideArrowTween;

		// Token: 0x0400877E RID: 34686
		private readonly List<Tween> m_ArrowTweens;

		// Token: 0x0400877F RID: 34687
		private readonly TextMeshProUGUI m_MessageTextTemplate;

		// Token: 0x04008780 RID: 34688
		private readonly List<TextMeshProUGUI> m_ActiveTexts;

		// Token: 0x04008781 RID: 34689
		private readonly Stack<TextMeshProUGUI> m_InactiveTexts;

		// Token: 0x04008782 RID: 34690
		private readonly GameObject m_NextArrowGom;
	}
}
