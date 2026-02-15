using System;
using YgomGame.Menu;
using YgomSystem.UI;

namespace YgomGame.Team
{
	// Token: 0x020008C6 RID: 2246
	public class TeamMemberMatchingViewController : BaseMenuViewController
	{
		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x060041A9 RID: 16809 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060041AA RID: 16810 RVA: 0x0000216D File Offset: 0x0000036D
		protected TeamMemberMatchingViewController.Step step
		{
			get
			{
				return TeamMemberMatchingViewController.Step.WAIT_INIT;
			}
			set
			{
			}
		}

		// Token: 0x060041AB RID: 16811 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(ViewControllerManager manager, ViewController parentView, int deck_id = 0, int regulation_id = 0)
		{
		}

		// Token: 0x060041AC RID: 16812 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x060041AD RID: 16813 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x060041AE RID: 16814 RVA: 0x0000216D File Offset: 0x0000036D
		private void Init()
		{
		}

		// Token: 0x060041AF RID: 16815 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x060041B0 RID: 16816 RVA: 0x0000216D File Offset: 0x0000036D
		private void EvalEachSteps()
		{
		}

		// Token: 0x060041B1 RID: 16817 RVA: 0x0000216D File Offset: 0x0000036D
		private void CallAPIDeckSet(int deckId, int regulationId, Action onFinish = null)
		{
		}

		// Token: 0x060041B2 RID: 16818 RVA: 0x0000216D File Offset: 0x0000036D
		private void CallAPIMatching()
		{
		}

		// Token: 0x060041B3 RID: 16819 RVA: 0x0000216D File Offset: 0x0000036D
		private void ReCallMatching()
		{
		}

		// Token: 0x060041B4 RID: 16820 RVA: 0x0000216D File Offset: 0x0000036D
		private void NotFindTeam()
		{
		}

		// Token: 0x04007FFF RID: 32767
		public const string PREFAB_PATH = "Team/TeamMemberMatching";

		// Token: 0x04008000 RID: 32768
		public const string KEY_PARENT_VIEW = "parent_view";

		// Token: 0x04008001 RID: 32769
		public const string KEY_DECK_ID = "deck_id";

		// Token: 0x04008002 RID: 32770
		public const string KEY_REGULATION_ID = "regulation_id";

		// Token: 0x04008003 RID: 32771
		private readonly string E_ButtonCancel;

		// Token: 0x04008004 RID: 32772
		private readonly string E_TextSearching;

		// Token: 0x04008005 RID: 32773
		protected TeamMemberMatchingViewController.Step m_Step;

		// Token: 0x04008006 RID: 32774
		private ViewController parentView;

		// Token: 0x04008007 RID: 32775
		private bool isRequestCancel;

		// Token: 0x04008008 RID: 32776
		private SelectionButton backBtn;

		// Token: 0x04008009 RID: 32777
		private int callCount;

		// Token: 0x020008C7 RID: 2247
		protected enum Step
		{
			// Token: 0x0400800B RID: 32779
			WAIT_INIT,
			// Token: 0x0400800C RID: 32780
			WAIT_PRE_MATCHING,
			// Token: 0x0400800D RID: 32781
			CALL_MATCHING,
			// Token: 0x0400800E RID: 32782
			WAIT_MATCHING,
			// Token: 0x0400800F RID: 32783
			WAIT_END
		}
	}
}
