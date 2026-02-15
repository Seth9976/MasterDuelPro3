using System;
using UnityEngine;
using UnityEngine.Events;
using YgomGame.Menu;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.YGomTMPro;

namespace YgomGame.GetHistory
{
	// Token: 0x02000BFB RID: 3067
	public class GetHistoryViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x0600570C RID: 22284 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x0600570D RID: 22285 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetTitleText(string title)
		{
		}

		// Token: 0x0600570E RID: 22286 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetEmptyText(string emptyText)
		{
		}

		// Token: 0x0600570F RID: 22287 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetCautionText(string cautionText)
		{
		}

		// Token: 0x06005710 RID: 22288 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetButtonRCallBack(UnityAction ButtonRCallBack)
		{
		}

		// Token: 0x06005711 RID: 22289 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetButtonLCallBack(UnityAction ButtonLCallBack)
		{
		}

		// Token: 0x06005712 RID: 22290 RVA: 0x0000216D File Offset: 0x0000036D
		protected void UpdateFooter()
		{
		}

		// Token: 0x06005713 RID: 22291 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateButtonStatus()
		{
		}

		// Token: 0x06005714 RID: 22292 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetPageIndexText()
		{
		}

		// Token: 0x06005715 RID: 22293 RVA: 0x0000216D File Offset: 0x0000036D
		private void CleanView()
		{
		}

		// Token: 0x040093C5 RID: 37829
		private readonly string BTN_BTNL_LABEL;

		// Token: 0x040093C6 RID: 37830
		private readonly string BTN_BTNR_LABEL;

		// Token: 0x040093C7 RID: 37831
		private readonly string TEXT_TITLE_LABEL;

		// Token: 0x040093C8 RID: 37832
		private readonly string CONTENT_LABEL;

		// Token: 0x040093C9 RID: 37833
		private readonly string TEXT_PAGEINDEX_LABEL;

		// Token: 0x040093CA RID: 37834
		private readonly string TEXT_CAUTION_LABEL;

		// Token: 0x040093CB RID: 37835
		private readonly string TEMPLATE_LABEL;

		// Token: 0x040093CC RID: 37836
		private readonly string TEMPLATE_CONS_LABEL;

		// Token: 0x040093CD RID: 37837
		private readonly string TEMPLATE_ADD_LABEL;

		// Token: 0x040093CE RID: 37838
		private readonly string TEMPLATE_EXPIRE_LABEL;

		// Token: 0x040093CF RID: 37839
		private readonly string TEXTS_LIMITEDPAID_LABEL;

		// Token: 0x040093D0 RID: 37840
		private readonly string TEXTS_LIMITEDFREE_LABEL;

		// Token: 0x040093D1 RID: 37841
		private readonly string TEXTS_HAVEFREE_LABEL;

		// Token: 0x040093D2 RID: 37842
		private readonly string TEXTS_HAVEPAID_LABEL;

		// Token: 0x040093D3 RID: 37843
		private readonly string TEXT_EMPTYHISTORY_LABEL;

		// Token: 0x040093D4 RID: 37844
		protected ElementObjectManager m_templateEOM;

		// Token: 0x040093D5 RID: 37845
		protected ElementObjectManager m_templateConsEOM;

		// Token: 0x040093D6 RID: 37846
		protected ElementObjectManager m_templateAddEOM;

		// Token: 0x040093D7 RID: 37847
		protected ElementObjectManager m_templateExpireEOM;

		// Token: 0x040093D8 RID: 37848
		protected ElementObjectManager m_textsHaveFreeEOM;

		// Token: 0x040093D9 RID: 37849
		protected ElementObjectManager m_textsHavePaidEOM;

		// Token: 0x040093DA RID: 37850
		protected ElementObjectManager m_textsLimitedFreeEOM;

		// Token: 0x040093DB RID: 37851
		protected ElementObjectManager m_textsLimitedPaidEOM;

		// Token: 0x040093DC RID: 37852
		private ExtendedTextMeshProUGUI m_titleText;

		// Token: 0x040093DD RID: 37853
		private ExtendedTextMeshProUGUI m_cautionText;

		// Token: 0x040093DE RID: 37854
		private ExtendedTextMeshProUGUI m_emptyText;

		// Token: 0x040093DF RID: 37855
		private ExtendedTextMeshProUGUI m_pageText;

		// Token: 0x040093E0 RID: 37856
		protected SelectionButton m_buttonL;

		// Token: 0x040093E1 RID: 37857
		protected SelectionButton m_buttonR;

		// Token: 0x040093E2 RID: 37858
		protected int m_maxTemplatesInContent;

		// Token: 0x040093E3 RID: 37859
		protected GameObject m_contentGO;

		// Token: 0x040093E4 RID: 37860
		protected bool m_isMobile;

		// Token: 0x040093E5 RID: 37861
		protected int m_pageIndex;

		// Token: 0x040093E6 RID: 37862
		protected int m_maxPageIndex;
	}
}
