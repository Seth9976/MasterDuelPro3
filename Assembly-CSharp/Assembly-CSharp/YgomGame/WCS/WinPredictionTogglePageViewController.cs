using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Menu;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.WCS
{
	// Token: 0x020007FD RID: 2045
	public class WinPredictionTogglePageViewController : BaseMenuViewController
	{
		// Token: 0x06003F7B RID: 16251 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(int index)
		{
		}

		// Token: 0x06003F7C RID: 16252 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06003F7D RID: 16253 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x06003F7E RID: 16254 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06003F7F RID: 16255 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickFooterButton()
		{
		}

		// Token: 0x06003F80 RID: 16256 RVA: 0x0000216D File Offset: 0x0000036D
		private void Import()
		{
		}

		// Token: 0x06003F81 RID: 16257 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateView()
		{
		}

		// Token: 0x06003F82 RID: 16258 RVA: 0x0000216D File Offset: 0x0000036D
		private void ClickButton(int idx)
		{
		}

		// Token: 0x06003F83 RID: 16259 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenErrDialog(string title, string text)
		{
		}

		// Token: 0x04003871 RID: 14449
		private const string ON_BTN_LABEL = "On";

		// Token: 0x04003872 RID: 14450
		private const string OFF_BTN_LABEL = "Off";

		// Token: 0x04003873 RID: 14451
		private const string ARGS_INDEX_LABEL = "onToggeleIndex";

		// Token: 0x04003874 RID: 14452
		private string TEXT_TEAM_GROUP_ON_LABEL;

		// Token: 0x04003875 RID: 14453
		private string TEXT_TEAM_GROUP_OFF_LABEL;

		// Token: 0x04003876 RID: 14454
		private string TEXT_TEAM_NAME_ON_LABEL;

		// Token: 0x04003877 RID: 14455
		private string TEXT_TEAM_NAME_OFF_LABEL;

		// Token: 0x04003878 RID: 14456
		private string FOOTER_BTN_LABEL;

		// Token: 0x04003879 RID: 14457
		private string TOGGLE_TEMPLATE_LABEL;

		// Token: 0x0400387A RID: 14458
		private string CW_TEAM_NAME;

		// Token: 0x0400387B RID: 14459
		private string CW_TEAM_ORDER;

		// Token: 0x0400387C RID: 14460
		private SelectionButton m_FooterButton;

		// Token: 0x0400387D RID: 14461
		private int selectedButtonIndex;

		// Token: 0x0400387E RID: 14462
		private List<WinPredictionTogglePageViewController.TeamData> teamDatas;

		// Token: 0x0400387F RID: 14463
		private Dictionary<int, WinPredictionTogglePageViewController.ToggleButton> toggleButtons;

		// Token: 0x04003880 RID: 14464
		private Dictionary<string, object> m_TeamDataDic;

		// Token: 0x020007FE RID: 2046
		public class TeamData
		{
			// Token: 0x04003881 RID: 14465
			public string teamName;

			// Token: 0x04003882 RID: 14466
			public bool isActive;

			// Token: 0x04003883 RID: 14467
			public int idx;

			// Token: 0x04003884 RID: 14468
			public int Id;
		}

		// Token: 0x020007FF RID: 2047
		public class ToggleButton
		{
			// Token: 0x06003F86 RID: 16262 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetActive(bool active)
			{
			}

			// Token: 0x06003F87 RID: 16263 RVA: 0x00002739 File Offset: 0x00000939
			public ToggleButton(ElementObjectManager elementObjectManager)
			{
			}

			// Token: 0x04003885 RID: 14469
			public ElementObjectManager eom;

			// Token: 0x04003886 RID: 14470
			public GameObject onImage;

			// Token: 0x04003887 RID: 14471
			public GameObject offImage;

			// Token: 0x04003888 RID: 14472
			public SelectionButton button;
		}

		// Token: 0x02000800 RID: 2048
		public enum WinPredictionVoteCode
		{
			// Token: 0x0400388A RID: 14474
			NONE,
			// Token: 0x0400388B RID: 14475
			ERR_OUT_OF_TERM = 4301,
			// Token: 0x0400388C RID: 14476
			ERROR_ID_CONFIG = 4313,
			// Token: 0x0400388D RID: 14477
			ERR_OUT_OF_VOTE_TERM,
			// Token: 0x0400388E RID: 14478
			ERR_FINAL_SUPPORT_ABSENT
		}
	}
}
