using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YgomGame.Menu;
using YgomSystem.Billing;
using YgomSystem.UI;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.GemShop
{
	// Token: 0x02000BFF RID: 3071
	public class GemShopViewController : BaseMenuViewController, IDynamicChangeDispHeaderSupported
	{
		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x0600571E RID: 22302 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600571F RID: 22303 RVA: 0x000029CC File Offset: 0x00000BCC
		public HeaderViewController.IsDispHeader IsDispContents()
		{
			return (HeaderViewController.IsDispHeader)0;
		}

		// Token: 0x06005720 RID: 22304 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open()
		{
		}

		// Token: 0x06005721 RID: 22305 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenOnHome()
		{
		}

		// Token: 0x06005722 RID: 22306 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06005723 RID: 22307 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06005724 RID: 22308 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06005725 RID: 22309 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnTransitionStart(ViewController.TransitionType type)
		{
		}

		// Token: 0x06005726 RID: 22310 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnTransitionEnd(ViewController.TransitionType type)
		{
		}

		// Token: 0x06005727 RID: 22311 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x06005728 RID: 22312 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCreatedEntity(GameObject entity)
		{
		}

		// Token: 0x06005729 RID: 22313 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnUpdateEntity(GameObject entity, int idx)
		{
		}

		// Token: 0x0600572A RID: 22314 RVA: 0x0000216D File Offset: 0x0000036D
		private void ImportProducts()
		{
		}

		// Token: 0x0600572B RID: 22315 RVA: 0x0000216D File Offset: 0x0000036D
		private void RefreshView(bool import = true)
		{
		}

		// Token: 0x0600572C RID: 22316 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickServiceInfo()
		{
		}

		// Token: 0x0600572D RID: 22317 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickGemHistory()
		{
		}

		// Token: 0x0600572E RID: 22318 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickCaution()
		{
		}

		// Token: 0x0600572F RID: 22319 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickProductWidget(ProductWidget productWidget)
		{
		}

		// Token: 0x06005730 RID: 22320 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenConfirmRegDialog(int shopPaidId, ProductContext productContext, IReadOnlyDictionary<string, object> productData, List<object> confirmRegDatas)
		{
		}

		// Token: 0x06005731 RID: 22321 RVA: 0x0000216D File Offset: 0x0000036D
		private void ProcessBuyItem(int shopPaidId, IReadOnlyDictionary<string, object> productData)
		{
		}

		// Token: 0x06005732 RID: 22322 RVA: 0x0000216D File Offset: 0x0000036D
		private void BuyResultSequence(int step, ResultCode resultCode, string resultTitle, string resultMessage)
		{
		}

		// Token: 0x040093ED RID: 37869
		private readonly string k_ELabelProductList;

		// Token: 0x040093EE RID: 37870
		private readonly string k_ELabelDoubleNotationPriceRate;

		// Token: 0x040093EF RID: 37871
		private readonly string k_ELabelServiceInfoButton;

		// Token: 0x040093F0 RID: 37872
		private readonly string k_ELabelGemGetHistoryButton;

		// Token: 0x040093F1 RID: 37873
		private readonly string k_ELabelCautionButton;

		// Token: 0x040093F2 RID: 37874
		private readonly string k_ELabelProductEmptyText;

		// Token: 0x040093F3 RID: 37875
		private readonly string k_ELabelServiceInfoText;

		// Token: 0x040093F4 RID: 37876
		private readonly string k_ALabelConfirmRegDialogTextWidget;

		// Token: 0x040093F5 RID: 37877
		private readonly string k_PLabelDoubleNotationLabel;

		// Token: 0x040093F6 RID: 37878
		private readonly string k_PLabelDoubleNotationConfirmFormat;

		// Token: 0x040093F7 RID: 37879
		private string m_DoubleNotationRateLabel;

		// Token: 0x040093F8 RID: 37880
		private string m_DoubleNotationRateFormatLabel;

		// Token: 0x040093F9 RID: 37881
		private Dictionary<GameObject, ProductWidget> m_EntityWidgetMap;

		// Token: 0x040093FA RID: 37882
		private List<ProductContext> m_ProductContexts;

		// Token: 0x040093FB RID: 37883
		private JsonGemShopAnalyzer m_JsonGemShopAnalyzer;

		// Token: 0x040093FC RID: 37884
		private InfinityScrollView m_ScrollView;

		// Token: 0x040093FD RID: 37885
		private TMP_Text m_ServiceInfo;

		// Token: 0x040093FE RID: 37886
		private string m_CautionHelpPath;
	}
}
