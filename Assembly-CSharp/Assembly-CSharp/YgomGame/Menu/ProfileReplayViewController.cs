using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Duel;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.Menu
{
	// Token: 0x02000ACE RID: 2766
	public class ProfileReplayViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06005098 RID: 20632 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06005099 RID: 20633 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x0600509A RID: 20634 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x0600509B RID: 20635 RVA: 0x0000216D File Offset: 0x0000036D
		private void DeleteReplayDialog(long did, string strDate)
		{
		}

		// Token: 0x0600509C RID: 20636 RVA: 0x0000216D File Offset: 0x0000036D
		private void DeleteReplay(long did)
		{
		}

		// Token: 0x0600509D RID: 20637 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayReplay(long did)
		{
		}

		// Token: 0x0600509E RID: 20638 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetReplayOpen(long did, bool isOpen)
		{
		}

		// Token: 0x0600509F RID: 20639 RVA: 0x0000216D File Offset: 0x0000036D
		private void CheckOpponentDeck(long did, int regulationId = 0)
		{
		}

		// Token: 0x060050A0 RID: 20640 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateProfileReplay(Dictionary<string, object> replayDic)
		{
		}

		// Token: 0x060050A1 RID: 20641 RVA: 0x0000216D File Offset: 0x0000036D
		private void SortReplayDate(bool descendingOrder = true)
		{
		}

		// Token: 0x060050A2 RID: 20642 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnItemSetData(GameObject gob, int dataindex)
		{
		}

		// Token: 0x060050A3 RID: 20643 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnGsvStanby()
		{
		}

		// Token: 0x060050A4 RID: 20644 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenProfileCard(long pcode)
		{
		}

		// Token: 0x04008EB5 RID: 36533
		private readonly string IMG_ICON_LABEL;

		// Token: 0x04008EB6 RID: 36534
		private readonly string SCROLL_REPLAY_LABEL;

		// Token: 0x04008EB7 RID: 36535
		private readonly string TXT_NOTBOOKMARK_LABEL;

		// Token: 0x04008EB8 RID: 36536
		private readonly string TXT_TITLE_LABEL;

		// Token: 0x04008EB9 RID: 36537
		private readonly string TMP_BTN_LABEL;

		// Token: 0x04008EBA RID: 36538
		private readonly string TMP_IMG_TITLE_LABEL;

		// Token: 0x04008EBB RID: 36539
		private readonly string TMP_IMG_OPEN_LABEL;

		// Token: 0x04008EBC RID: 36540
		private readonly string TMP_TXT_DATE_LABEL;

		// Token: 0x04008EBD RID: 36541
		private readonly string TMP_TXT_RESULT_LABEL;

		// Token: 0x04008EBE RID: 36542
		private readonly string TMP_TXT_TITLE_LABEL;

		// Token: 0x04008EBF RID: 36543
		private readonly string TMP_TXT_TURN_LABEL;

		// Token: 0x04008EC0 RID: 36544
		private readonly string TMP_TXT_PLATFORM_NAME_LABEL;

		// Token: 0x04008EC1 RID: 36545
		private readonly string TMP_TXT_PLATFORM_ICON_LABEL;

		// Token: 0x04008EC2 RID: 36546
		private InfinityScrollView isv;

		// Token: 0x04008EC3 RID: 36547
		private long pcode;

		// Token: 0x04008EC4 RID: 36548
		private List<ProfileReplayViewController.ProfileReplayInfo> replayInfos;

		// Token: 0x02000ACF RID: 2767
		internal class ProfileReplayInfo
		{
			// Token: 0x060050A6 RID: 20646 RVA: 0x00002739 File Offset: 0x00000939
			public ProfileReplayInfo(long did, int mode, int iconID, int frameID, int rank, int tier, long timestamp, string dateTime, string name, Engine.ResultType result, string title, string turn, bool isOpen, int eventId, long pcode = 0L, bool isSameOS = false, string onlineId = null, int regulationId = 0)
			{
			}

			// Token: 0x04008EC5 RID: 36549
			internal long did;

			// Token: 0x04008EC6 RID: 36550
			internal int mode;

			// Token: 0x04008EC7 RID: 36551
			internal int iconID;

			// Token: 0x04008EC8 RID: 36552
			internal int frameID;

			// Token: 0x04008EC9 RID: 36553
			internal int rank;

			// Token: 0x04008ECA RID: 36554
			internal int tier;

			// Token: 0x04008ECB RID: 36555
			internal long timestamp;

			// Token: 0x04008ECC RID: 36556
			internal string dateTime;

			// Token: 0x04008ECD RID: 36557
			internal string name;

			// Token: 0x04008ECE RID: 36558
			internal Engine.ResultType result;

			// Token: 0x04008ECF RID: 36559
			internal string title;

			// Token: 0x04008ED0 RID: 36560
			internal string turn;

			// Token: 0x04008ED1 RID: 36561
			internal bool isOpen;

			// Token: 0x04008ED2 RID: 36562
			internal int eventId;

			// Token: 0x04008ED3 RID: 36563
			internal long pcode;

			// Token: 0x04008ED4 RID: 36564
			internal bool isSameOS;

			// Token: 0x04008ED5 RID: 36565
			internal string onlineId;

			// Token: 0x04008ED6 RID: 36566
			internal int regulationId;
		}
	}
}
