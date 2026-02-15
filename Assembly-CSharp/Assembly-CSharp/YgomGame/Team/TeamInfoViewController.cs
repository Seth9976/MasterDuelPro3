using System;
using YgomGame.Menu;
using YgomSystem.UI;

namespace YgomGame.Team
{
	// Token: 0x020008BB RID: 2235
	public class TeamInfoViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06004165 RID: 16741 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004166 RID: 16742 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(ViewControllerManager manager)
		{
		}

		// Token: 0x06004167 RID: 16743 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004168 RID: 16744 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadData()
		{
		}

		// Token: 0x06004169 RID: 16745 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x04007FB5 RID: 32693
		private const string VC_PATH = "Team/TeamInfo";

		// Token: 0x04007FB6 RID: 32694
		private SelectionButton _copyBtn;

		// Token: 0x04007FB7 RID: 32695
		private int _joiningNum;

		// Token: 0x04007FB8 RID: 32696
		private int _joiningMax;

		// Token: 0x04007FB9 RID: 32697
		private int _teamId;

		// Token: 0x04007FBA RID: 32698
		private string _regulationSetName;

		// Token: 0x04007FBB RID: 32699
		private string[] _regulationNames;

		// Token: 0x04007FBC RID: 32700
		private int _mrk;
	}
}
