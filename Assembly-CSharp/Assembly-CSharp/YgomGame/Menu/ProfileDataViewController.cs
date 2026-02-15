using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.TextIDs;
using YgomSystem.UI;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.Menu
{
	// Token: 0x02000AC1 RID: 2753
	public class ProfileDataViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06005025 RID: 20517 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06005026 RID: 20518 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06005027 RID: 20519 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06005028 RID: 20520 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06005029 RID: 20521 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateProfileData(Dictionary<string, object> recordDict)
		{
		}

		// Token: 0x0600502A RID: 20522 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnItemSetData(GameObject gob, int dataindex)
		{
		}

		// Token: 0x0600502B RID: 20523 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnGsvStanby()
		{
		}

		// Token: 0x0600502C RID: 20524 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenCautionDialog()
		{
		}

		// Token: 0x04008E3A RID: 36410
		private readonly string SCROLL_DATA_LABEL;

		// Token: 0x04008E3B RID: 36411
		private readonly string BTN_CAUTION_LABEL;

		// Token: 0x04008E3C RID: 36412
		private readonly string TEXT_TITLE_LABEL;

		// Token: 0x04008E3D RID: 36413
		private readonly string TEXT_VALUE_LABEL;

		// Token: 0x04008E3E RID: 36414
		private InfinityScrollView isvData;

		// Token: 0x04008E3F RID: 36415
		private SelectionButton m_CautionButton;

		// Token: 0x04008E40 RID: 36416
		private SelectionButtonUntouchable m_DataAreaButton;

		// Token: 0x04008E41 RID: 36417
		private long totalPvP;

		// Token: 0x04008E42 RID: 36418
		private long pcode;

		// Token: 0x04008E43 RID: 36419
		private List<object> rankHistory;

		// Token: 0x04008E44 RID: 36420
		private List<ProfileDataViewController.ProfileDataInfo> dataInfos;

		// Token: 0x02000AC2 RID: 2754
		internal class ProfileDataInfo
		{
			// Token: 0x0600502E RID: 20526 RVA: 0x00002739 File Offset: 0x00000939
			public ProfileDataInfo(IDS_RECORD record, long value)
			{
			}

			// Token: 0x04008E45 RID: 36421
			internal IDS_RECORD record;

			// Token: 0x04008E46 RID: 36422
			internal long value;
		}
	}
}
