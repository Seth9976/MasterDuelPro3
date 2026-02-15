using System;
using System.Collections.Generic;
using YgomGame.GetHistory;
using YgomGame.Menu;
using YgomSystem.ElementSystem;

namespace YgomGame.GemGetHistory
{
	// Token: 0x02000C06 RID: 3078
	public class GemGetHistoryViewController : GetHistoryViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x0600575C RID: 22364 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600575D RID: 22365 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x0600575E RID: 22366 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x0600575F RID: 22367 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetFirstView()
		{
		}

		// Token: 0x06005760 RID: 22368 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetTemplateCons(Dictionary<string, object> dict)
		{
		}

		// Token: 0x06005761 RID: 22369 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetTemplateAdd(Dictionary<string, object> dict)
		{
		}

		// Token: 0x06005762 RID: 22370 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetTemplateExpire(Dictionary<string, object> dict)
		{
		}

		// Token: 0x06005763 RID: 22371 RVA: 0x0000216A File Offset: 0x0000036A
		private string ToAlertStyle(string text)
		{
			return null;
		}

		// Token: 0x06005764 RID: 22372 RVA: 0x0000216D File Offset: 0x0000036D
		private void ClearTemplates()
		{
		}

		// Token: 0x06005765 RID: 22373 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdatePage()
		{
		}

		// Token: 0x06005766 RID: 22374 RVA: 0x0000216D File Offset: 0x0000036D
		private void ButtonRCallBack()
		{
		}

		// Token: 0x06005767 RID: 22375 RVA: 0x0000216D File Offset: 0x0000036D
		private void ButtonLCallBack()
		{
		}

		// Token: 0x06005768 RID: 22376 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetAllTemplates()
		{
		}

		// Token: 0x04009421 RID: 37921
		private readonly string TEXT_GETDATE_LABEL;

		// Token: 0x04009422 RID: 37922
		private readonly string TEXT_NUM_LABEL;

		// Token: 0x04009423 RID: 37923
		private readonly string TEXT_LIMITDATE_LABEL;

		// Token: 0x04009424 RID: 37924
		private readonly string TEXT_UNUSEDGEM_LABEL;

		// Token: 0x04009425 RID: 37925
		private readonly string TEXT_CONSDATE_LABEL;

		// Token: 0x04009426 RID: 37926
		private readonly string TEXT_PAIDNUM_LABEL;

		// Token: 0x04009427 RID: 37927
		private readonly string TEXT_FREENUM_LABEL;

		// Token: 0x04009428 RID: 37928
		private readonly string TEXT_DESC_LABEL;

		// Token: 0x04009429 RID: 37929
		private readonly string KEY_PAGEMAX;

		// Token: 0x0400942A RID: 37930
		private readonly string KEY_ISJP;

		// Token: 0x0400942B RID: 37931
		private readonly string KEY_NEXT_EXPIREDATE;

		// Token: 0x0400942C RID: 37932
		private readonly string KEY_NEXT_EXPIREPOINT;

		// Token: 0x0400942D RID: 37933
		private readonly string KEY_HISTORY;

		// Token: 0x0400942E RID: 37934
		private readonly string KEY_FREE_POINT_LIMIT;

		// Token: 0x0400942F RID: 37935
		private readonly string KEY_ORDERDATE;

		// Token: 0x04009430 RID: 37936
		private readonly string KEY_EXPIREDATE;

		// Token: 0x04009431 RID: 37937
		private readonly string KEY_ORDERTYPE_ID;

		// Token: 0x04009432 RID: 37938
		private readonly string KEY_PAID_POINT;

		// Token: 0x04009433 RID: 37939
		private readonly string KEY_FREE_POINT;

		// Token: 0x04009434 RID: 37940
		private readonly string KEY_UNUSED_PAID_POINT;

		// Token: 0x04009435 RID: 37941
		private readonly string KEY_ORDERTYPE_ADD;

		// Token: 0x04009436 RID: 37942
		private readonly string KEY_ORDERTYPE_CONSUME;

		// Token: 0x04009437 RID: 37943
		private readonly string KEY_ORDERTYPE_EXPIRE;

		// Token: 0x04009438 RID: 37944
		private List<ElementObjectManager> m_templateEOMList;

		// Token: 0x04009439 RID: 37945
		private List<object> m_historyList;

		// Token: 0x0400943A RID: 37946
		private int m_haveFreeNum;

		// Token: 0x0400943B RID: 37947
		private int m_havePaidNum;

		// Token: 0x0400943C RID: 37948
		private int maxPosFreeNum;

		// Token: 0x0400943D RID: 37949
		private const int maxPosPaidNum = 499999;

		// Token: 0x0400943E RID: 37950
		private readonly string PATH_BILLINGHISTORY;
	}
}
