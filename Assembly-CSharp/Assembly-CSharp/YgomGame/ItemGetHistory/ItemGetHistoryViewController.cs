using System;
using System.Collections.Generic;
using YgomGame.GetHistory;
using YgomGame.Menu;
using YgomSystem.ElementSystem;

namespace YgomGame.ItemGetHistory
{
	// Token: 0x02000BDA RID: 3034
	public class ItemGetHistoryViewController : GetHistoryViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06005669 RID: 22121 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600566A RID: 22122 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x0600566B RID: 22123 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x0600566C RID: 22124 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetTemplate(Dictionary<string, object> dict)
		{
		}

		// Token: 0x0600566D RID: 22125 RVA: 0x0000216D File Offset: 0x0000036D
		private void ClearTemplates()
		{
		}

		// Token: 0x0600566E RID: 22126 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdatePage()
		{
		}

		// Token: 0x0600566F RID: 22127 RVA: 0x0000216D File Offset: 0x0000036D
		private void ButtonRCallBack()
		{
		}

		// Token: 0x06005670 RID: 22128 RVA: 0x0000216D File Offset: 0x0000036D
		private void ButtonLCallBack()
		{
		}

		// Token: 0x06005671 RID: 22129 RVA: 0x0000216A File Offset: 0x0000036A
		private string GetGetReasonText(int getreason)
		{
			return null;
		}

		// Token: 0x06005672 RID: 22130 RVA: 0x0000216A File Offset: 0x0000036A
		private string GetItemNameWithCategory(bool isPeriod, int category, int itemId)
		{
			return null;
		}

		// Token: 0x04009350 RID: 37712
		private readonly string TEXT_GETDATE_LABEL;

		// Token: 0x04009351 RID: 37713
		private readonly string TEXT_ITEMNAME_LABEL;

		// Token: 0x04009352 RID: 37714
		private readonly string TEXT_GETREASON_LABEL;

		// Token: 0x04009353 RID: 37715
		private readonly string TEXT_LIMITDATE_LABEL;

		// Token: 0x04009354 RID: 37716
		private readonly string KEY_ITEM_ID;

		// Token: 0x04009355 RID: 37717
		private readonly string KEY_REASON;

		// Token: 0x04009356 RID: 37718
		private readonly string KEY_CATEGORY;

		// Token: 0x04009357 RID: 37719
		private readonly string KEY_GETDATE;

		// Token: 0x04009358 RID: 37720
		private readonly string KEY_LIMITDATE;

		// Token: 0x04009359 RID: 37721
		private readonly string KEY_NUM;

		// Token: 0x0400935A RID: 37722
		private readonly string KEY_PERIODITEMFLAG;

		// Token: 0x0400935B RID: 37723
		private List<object> m_itemGetHistory;

		// Token: 0x0400935C RID: 37724
		private List<ElementObjectManager> m_templateEOMList;
	}
}
