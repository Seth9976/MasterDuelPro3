using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YgomSystem.Utility;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000BB0 RID: 2992
	public class MDMarkupGraphFactory : MonoBehaviour
	{
		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06005580 RID: 21888 RVA: 0x0000216A File Offset: 0x0000036A
		public MDMarkupIndentFactory indentFactory
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06005581 RID: 21889 RVA: 0x0000216A File Offset: 0x0000036A
		private string GetContainerPath(MDMarkupGraphFactory.Style style)
		{
			return null;
		}

		// Token: 0x06005582 RID: 21890 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(Action onComplete)
		{
		}

		// Token: 0x06005583 RID: 21891 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yInitialize(Action onComplete)
		{
			return null;
		}

		// Token: 0x06005584 RID: 21892 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x06005585 RID: 21893 RVA: 0x0000216A File Offset: 0x0000036A
		public IEnumerator LoadOrGetMarkupFactory(MDMarkupDef.MarkupType markupType, Action<IMDMarkupWidgetFactory> callback)
		{
			return null;
		}

		// Token: 0x04009296 RID: 37526
		private readonly string k_MDTemplateContainerPath;

		// Token: 0x04009297 RID: 37527
		private readonly string k_MDTemplateContainerPagerPath;

		// Token: 0x04009298 RID: 37528
		private readonly string k_CLabelIndent0Template;

		// Token: 0x04009299 RID: 37529
		private readonly string k_CLabelIndent1Template;

		// Token: 0x0400929A RID: 37530
		private readonly string k_CLabelIndent2Template;

		// Token: 0x0400929B RID: 37531
		private readonly string k_CLabelHeader1Template;

		// Token: 0x0400929C RID: 37532
		private readonly string k_CLabelHeader2Template;

		// Token: 0x0400929D RID: 37533
		private readonly string k_CLabelTextTemplate;

		// Token: 0x0400929E RID: 37534
		private readonly string k_CLabelImageTemplate;

		// Token: 0x0400929F RID: 37535
		private readonly string k_CLabelSeparatorTemplate;

		// Token: 0x040092A0 RID: 37536
		private readonly string k_CLabelSpacerSTemplate;

		// Token: 0x040092A1 RID: 37537
		private readonly string k_CLabelSpacerMTemplate;

		// Token: 0x040092A2 RID: 37538
		private readonly string k_CLabelSpacerLTemplate;

		// Token: 0x040092A3 RID: 37539
		private readonly string k_CLabelTableRootTemplate;

		// Token: 0x040092A4 RID: 37540
		private readonly string k_CLabelTableRowTemplate;

		// Token: 0x040092A5 RID: 37541
		private readonly string k_CLabelTableCellTextHeaderTemplate;

		// Token: 0x040092A6 RID: 37542
		private readonly string k_CLabelTableCellTextNormalTemplate;

		// Token: 0x040092A7 RID: 37543
		private readonly string k_CLabelTableCellImageTemplate;

		// Token: 0x040092A8 RID: 37544
		private readonly string k_CLabelTableCellCardTemplate;

		// Token: 0x040092A9 RID: 37545
		private readonly string k_CLabelTableCellItemTemplate;

		// Token: 0x040092AA RID: 37546
		private readonly string k_CLabelTableCellBannerTemplate;

		// Token: 0x040092AB RID: 37547
		private readonly string k_CLabelTableCellButtonSTemplate;

		// Token: 0x040092AC RID: 37548
		private readonly string k_CLabelTableCellButtonMTemplate;

		// Token: 0x040092AD RID: 37549
		private readonly string k_CLabelTableCellButtonLTemplate;

		// Token: 0x040092AE RID: 37550
		private readonly string k_CLabelFullImagePageTemplate;

		// Token: 0x040092AF RID: 37551
		private readonly string k_CLabelFullTextPageTemplate;

		// Token: 0x040092B0 RID: 37552
		private readonly string k_CLabelHalfImageTextPageTemplate;

		// Token: 0x040092B1 RID: 37553
		private readonly string k_CLabelHalfImageMarkupPageTemplate;

		// Token: 0x040092B2 RID: 37554
		public MDMarkupGraphFactory.Style style;

		// Token: 0x040092B3 RID: 37555
		[SerializeField]
		private AssetLinkContainer m_MDTemplateContainer;

		// Token: 0x040092B4 RID: 37556
		private Dictionary<MDMarkupDef.MarkupType, IMDMarkupWidgetFactory> m_FactoryCacheMap;

		// Token: 0x040092B5 RID: 37557
		private MDMarkupIndentFactory m_IndentFactory;

		// Token: 0x040092B6 RID: 37558
		private Coroutine m_InitializeCoroutine;

		// Token: 0x02000BB1 RID: 2993
		public enum Style
		{
			// Token: 0x040092B8 RID: 37560
			Default,
			// Token: 0x040092B9 RID: 37561
			Pager
		}
	}
}
