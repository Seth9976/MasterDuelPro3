using System;
using YgomGame.Menu;
using YgomSystem.UI;

namespace YgomGame.Team
{
	// Token: 0x020008BE RID: 2238
	public class TeamLeaderMatchingViewController : BaseMenuViewController
	{
		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06004177 RID: 16759 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06004178 RID: 16760 RVA: 0x0000216D File Offset: 0x0000036D
		protected TeamLeaderMatchingViewController.Step step
		{
			get
			{
				return TeamLeaderMatchingViewController.Step.WAIT_INIT;
			}
			set
			{
			}
		}

		// Token: 0x06004179 RID: 16761 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(ViewControllerManager manager)
		{
		}

		// Token: 0x0600417A RID: 16762 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x0600417B RID: 16763 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x0600417C RID: 16764 RVA: 0x0000216D File Offset: 0x0000036D
		private void Init()
		{
		}

		// Token: 0x0600417D RID: 16765 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x0600417E RID: 16766 RVA: 0x0000216D File Offset: 0x0000036D
		private void EvalEachSteps()
		{
		}

		// Token: 0x0600417F RID: 16767 RVA: 0x0000216D File Offset: 0x0000036D
		private void CallAPIMatching()
		{
		}

		// Token: 0x06004180 RID: 16768 RVA: 0x0000216D File Offset: 0x0000036D
		private void NotFindMember()
		{
		}

		// Token: 0x04007FCB RID: 32715
		public const string PREFAB_PATH = "Team/TeamLeaderMatching";

		// Token: 0x04007FCC RID: 32716
		private readonly string E_ButtonCancel;

		// Token: 0x04007FCD RID: 32717
		private readonly string E_TextSearching;

		// Token: 0x04007FCE RID: 32718
		protected TeamLeaderMatchingViewController.Step m_Step;

		// Token: 0x04007FCF RID: 32719
		private bool isRequestCancel;

		// Token: 0x04007FD0 RID: 32720
		private SelectionButton backBtn;

		// Token: 0x04007FD1 RID: 32721
		private int callCount;

		// Token: 0x020008BF RID: 2239
		protected enum Step
		{
			// Token: 0x04007FD3 RID: 32723
			WAIT_INIT,
			// Token: 0x04007FD4 RID: 32724
			WAIT_PRE_MATCHING,
			// Token: 0x04007FD5 RID: 32725
			CALL_MATCHING,
			// Token: 0x04007FD6 RID: 32726
			WAIT_MATCHING,
			// Token: 0x04007FD7 RID: 32727
			WAIT_END
		}
	}
}
