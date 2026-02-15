using System;
using System.Collections.Generic;
using UnityEngine;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.Menu
{
	// Token: 0x02000ABA RID: 2746
	public class PresentBoxViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06004FF5 RID: 20469 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004FF6 RID: 20470 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004FF7 RID: 20471 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06004FF8 RID: 20472 RVA: 0x0000216D File Offset: 0x0000036D
		private void CallAPIPresentBoxReceive(int presentBoxID, int isAll)
		{
		}

		// Token: 0x06004FF9 RID: 20473 RVA: 0x0000216A File Offset: 0x0000036A
		private string SetDialogMessage(string beforeStr, string addStr)
		{
			return null;
		}

		// Token: 0x06004FFA RID: 20474 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateData()
		{
		}

		// Token: 0x06004FFB RID: 20475 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateScrollDataCount(int count)
		{
		}

		// Token: 0x06004FFC RID: 20476 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnItemSetData(GameObject gob, int dataindex)
		{
		}

		// Token: 0x04008DE1 RID: 36321
		private readonly string SCROLL_LABEL;

		// Token: 0x04008DE2 RID: 36322
		private readonly string TXT_EMPTY_LABEL;

		// Token: 0x04008DE3 RID: 36323
		private readonly string TXT_MESSAGE_LABEL;

		// Token: 0x04008DE4 RID: 36324
		private readonly string TXT_NAME_LABEL;

		// Token: 0x04008DE5 RID: 36325
		private readonly string BTN_ALL_LABEL;

		// Token: 0x04008DE6 RID: 36326
		private readonly string BTN_LABEL;

		// Token: 0x04008DE7 RID: 36327
		private readonly string TXT_DATE_LABEL;

		// Token: 0x04008DE8 RID: 36328
		private readonly string IMG_ICON_LABEL;

		// Token: 0x04008DE9 RID: 36329
		private InfinityScrollView infinityScroll;

		// Token: 0x04008DEA RID: 36330
		private List<PresentBoxViewController.Data> dataList;

		// Token: 0x02000ABB RID: 2747
		internal class Data
		{
			// Token: 0x06004FFE RID: 20478 RVA: 0x00002739 File Offset: 0x00000939
			public Data(int pID, int itemCategory, int itemID, int quantity, string message, string limitDate, bool isPeriod)
			{
			}

			// Token: 0x04008DEB RID: 36331
			internal int pID;

			// Token: 0x04008DEC RID: 36332
			internal int itemCategory;

			// Token: 0x04008DED RID: 36333
			internal int itemID;

			// Token: 0x04008DEE RID: 36334
			internal int quantity;

			// Token: 0x04008DEF RID: 36335
			internal string message;

			// Token: 0x04008DF0 RID: 36336
			internal string limitDate;

			// Token: 0x04008DF1 RID: 36337
			internal bool isPeriod;
		}
	}
}
