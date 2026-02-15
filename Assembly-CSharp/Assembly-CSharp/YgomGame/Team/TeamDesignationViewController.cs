using System;
using System.Collections.Generic;
using YgomGame.Menu;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;
using YgomSystem.YGomTMPro;

namespace YgomGame.Team
{
	// Token: 0x020008BA RID: 2234
	public class TeamDesignationViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x0600415C RID: 16732 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(ViewControllerManager manager, List<ValueTuple<int, string, int, int>> duelDurationConfigList, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x0600415D RID: 16733 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x0600415E RID: 16734 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x0600415F RID: 16735 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadData()
		{
		}

		// Token: 0x06004160 RID: 16736 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnApplyingForMatch()
		{
		}

		// Token: 0x06004161 RID: 16737 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnTeamIDInputEnd(string text)
		{
		}

		// Token: 0x06004162 RID: 16738 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDuelDurationSelecting()
		{
		}

		// Token: 0x06004163 RID: 16739 RVA: 0x0000216A File Offset: 0x0000036A
		private string AcquireDuelDurationConfigName(int index)
		{
			return null;
		}

		// Token: 0x04007FAC RID: 32684
		private const string VC_PATH = "Team/TeamDesignation";

		// Token: 0x04007FAD RID: 32685
		private const string ARGKEY_DURATION_CONFIG = "duration_config";

		// Token: 0x04007FAE RID: 32686
		private TeamLobbyPollingWatcher _watchDog;

		// Token: 0x04007FAF RID: 32687
		private InputFieldWidget _teamIdInput;

		// Token: 0x04007FB0 RID: 32688
		private SelectionButton _duelDurationSelectBtn;

		// Token: 0x04007FB1 RID: 32689
		private ExtendedTextMeshProUGUI _duelDurationValueText;

		// Token: 0x04007FB2 RID: 32690
		private SelectionButton _applyButton;

		// Token: 0x04007FB3 RID: 32691
		private List<ValueTuple<int, string, int, int>> _duelDurationConfigList;

		// Token: 0x04007FB4 RID: 32692
		private int _selectedDuelDurationIndex;
	}
}
