using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Dialog.CommonDialog;
using YgomGame.Utility;
using YgomSystem.ElementSystem;

namespace YgomGame.Shop
{
	// Token: 0x02000923 RID: 2339
	public class ConfirmRegDialogTextWidget : ContentWidgetBase<ConfirmRegDialogTextWidget, EntryInsertWidgetData>, IContentWidgetDirectionalInputListener
	{
		// Token: 0x0600442A RID: 17450 RVA: 0x0000216A File Offset: 0x0000036A
		public static ConfirmRegDialogTextWidget Create(ElementObjectManager eom)
		{
			return null;
		}

		// Token: 0x0600442B RID: 17451 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CollectComponents()
		{
		}

		// Token: 0x0600442C RID: 17452 RVA: 0x0000216D File Offset: 0x0000036D
		public void InsertContents(List<object> confirmRegDatas, TextGroupLoadHolder textGruopLoadHolder, Func<string, object> paramFormatFunc)
		{
		}

		// Token: 0x0600442D RID: 17453 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void InnerBinding(EntryInsertWidgetData entryData)
		{
		}

		// Token: 0x0600442E RID: 17454 RVA: 0x0000216D File Offset: 0x0000036D
		private void InsertHeader(Dictionary<string, object> line)
		{
		}

		// Token: 0x0600442F RID: 17455 RVA: 0x0000216D File Offset: 0x0000036D
		private void InsertBody(Dictionary<string, object> line, TextGroupLoadHolder textGruopLoadHolder, Func<string, object> paramFormatFunc)
		{
		}

		// Token: 0x06004430 RID: 17456 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnMainAnalogInput(Vector2 dir)
		{
		}

		// Token: 0x06004431 RID: 17457 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnSubAnalogInput(Vector2 dir)
		{
		}

		// Token: 0x06004432 RID: 17458 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnLeftInput()
		{
		}

		// Token: 0x06004433 RID: 17459 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnRightInput()
		{
		}

		// Token: 0x06004434 RID: 17460 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnUpInput()
		{
		}

		// Token: 0x06004435 RID: 17461 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnDownInput()
		{
		}

		// Token: 0x040082EC RID: 33516
		public const string k_FormatProductName = "product_name";

		// Token: 0x040082ED RID: 33517
		public const string k_FormatProductNumPaidGem = "product_num_paid_gem";

		// Token: 0x040082EE RID: 33518
		public const string k_FormatProductNumFreeGem = "product_num_free_gem";

		// Token: 0x040082EF RID: 33519
		public const string k_FormatProductNum = "product_num";

		// Token: 0x040082F0 RID: 33520
		public const string k_FormatProductPrice = "price";

		// Token: 0x040082F1 RID: 33521
		public const string k_FormatProductDoubleNotationPrice = "doubleNotationPrice";

		// Token: 0x040082F2 RID: 33522
		public const string k_FormatProductPriceLabel = "price_label";

		// Token: 0x040082F3 RID: 33523
		public const string k_FormatProductLimitdateTs = "limitdate_ts";

		// Token: 0x040082F4 RID: 33524
		public const string k_FormatProductLimitdate = "limitdate";

		// Token: 0x040082F5 RID: 33525
		public const string k_FormatProductLimitBuyCount = "limit_buy_count";

		// Token: 0x040082F6 RID: 33526
		public const string k_FormatProductTypeName = "product_type_name";

		// Token: 0x040082F7 RID: 33527
		private ElementObjectManager m_HeaderGroupTemplate;

		// Token: 0x040082F8 RID: 33528
		private ElementObjectManager m_TextTemplate;

		// Token: 0x040082F9 RID: 33529
		private List<object> m_FormatParamSearcher;
	}
}
