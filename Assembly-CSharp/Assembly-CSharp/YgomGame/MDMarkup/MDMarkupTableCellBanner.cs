using System;
using TMPro;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000BC3 RID: 3011
	public class MDMarkupTableCellBanner : ElementWidgetBase, IMDMarkupAsyncWidget, IMDMarkupLayoutWidget
	{
		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x060055DB RID: 21979 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060055DC RID: 21980 RVA: 0x0000216D File Offset: 0x0000036D
		public bool borderVisible
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x060055DD RID: 21981 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isReady
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060055DE RID: 21982 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public MDMarkupTableCellBanner(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x060055DF RID: 21983 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetAlignment(TextAlignmentOptions alignment)
		{
		}

		// Token: 0x060055E0 RID: 21984 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetSizeRate(float sizeRate)
		{
		}

		// Token: 0x060055E1 RID: 21985 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetBanner(MDMarkupBannerContext bannerContext, float overrideHeight)
		{
		}

		// Token: 0x060055E2 RID: 21986 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnReady()
		{
		}

		// Token: 0x060055E3 RID: 21987 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnConcreatedLayout()
		{
		}

		// Token: 0x040092EA RID: 37610
		private readonly string k_ELabelBannerHolder;

		// Token: 0x040092EB RID: 37611
		private RectTransform m_BannerHolder;

		// Token: 0x040092EC RID: 37612
		private MDMarkupBannerContext m_BannerContext;

		// Token: 0x040092ED RID: 37613
		private int m_LoadingCnt;

		// Token: 0x040092EE RID: 37614
		private Vector2 m_BannerSize;

		// Token: 0x040092EF RID: 37615
		private float m_OverrideHeight;
	}
}
