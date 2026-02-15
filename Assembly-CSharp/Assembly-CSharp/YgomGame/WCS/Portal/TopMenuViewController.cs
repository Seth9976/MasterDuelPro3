using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using YgomGame.Menu;
using YgomSystem.UI;

namespace YgomGame.WCS.Portal
{
	// Token: 0x0200080A RID: 2058
	public class TopMenuViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06003FA9 RID: 16297 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003FAA RID: 16298 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06003FAB RID: 16299 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06003FAC RID: 16300 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnTransitionStart(ViewController.TransitionType type)
		{
		}

		// Token: 0x06003FAD RID: 16301 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x06003FAE RID: 16302 RVA: 0x0000216D File Offset: 0x0000036D
		private void updateCampaignInfo(bool isOnCreate)
		{
		}

		// Token: 0x06003FAF RID: 16303 RVA: 0x0000216D File Offset: 0x0000036D
		private void updateAllUI(bool isOnCreate)
		{
		}

		// Token: 0x06003FB0 RID: 16304 RVA: 0x0000216D File Offset: 0x0000036D
		private void setWinnerTeamUI(bool isOnCreate)
		{
		}

		// Token: 0x06003FB1 RID: 16305 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator loadIconImage(Image imageUI, string imagePath)
		{
			return null;
		}

		// Token: 0x06003FB2 RID: 16306 RVA: 0x0000216D File Offset: 0x0000036D
		private void setOpenStatusFooterUI(GameObject root, TopMenuViewController.OpenStatus status, string text)
		{
		}

		// Token: 0x040038E0 RID: 14560
		private const string LABEL_BTN_1STSTAGE = "Button1stStage";

		// Token: 0x040038E1 RID: 14561
		private const string LABEL_BTN_CAMPAIGN = "ButtonCampaign";

		// Token: 0x040038E2 RID: 14562
		private const string LABEL_BTN_TOURNAMENT = "ButtonTournament";

		// Token: 0x040038E3 RID: 14563
		private const string LABEL_BTN_WATCH = "ButtonWatch";

		// Token: 0x040038E4 RID: 14564
		private const string LABEL_BTN_REGULATION = "ButtonRegulation";

		// Token: 0x040038E5 RID: 14565
		private const string LABEL_ROOT_WINNER = "ResultRoot";

		// Token: 0x040038E6 RID: 14566
		private const string LABEL_IMG_WINNERICON = "ResultTeamGroupIcon";

		// Token: 0x040038E7 RID: 14567
		private const string LABEL_TEXT_WINNERAREA = "TextResultTeamGroup";

		// Token: 0x040038E8 RID: 14568
		private const string LABEL_TEXT_WINNERNAME = "TextResultTeamName";

		// Token: 0x040038E9 RID: 14569
		private int m_winnerTeamID;

		// Token: 0x040038EA RID: 14570
		private GameObject m_uiWinnerRoot;

		// Token: 0x040038EB RID: 14571
		private GameObject m_ui1stStageRoot;

		// Token: 0x040038EC RID: 14572
		private GameObject m_uiTournamentRoot;

		// Token: 0x040038ED RID: 14573
		private GameObject m_uiVoteRewardBadge;

		// Token: 0x0200080B RID: 2059
		private enum OpenStatus
		{
			// Token: 0x040038EF RID: 14575
			Before,
			// Token: 0x040038F0 RID: 14576
			Open,
			// Token: 0x040038F1 RID: 14577
			After
		}
	}
}
