using System;
using System.Collections.Generic;

namespace YgomGame.Duel
{
	// Token: 0x02000E14 RID: 3604
	public class EffectTaskRunList : EffectTask
	{
		// Token: 0x0600683B RID: 26683 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> PreCreate(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x0600683C RID: 26684 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
		{
			return null;
		}

		// Token: 0x0600683D RID: 26685 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskRunList(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
			: base(null)
		{
		}

		// Token: 0x0600683E RID: 26686 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x0600683F RID: 26687 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitCardMoveStep()
		{
		}

		// Token: 0x06006840 RID: 26688 RVA: 0x0000216D File Offset: 0x0000036D
		private void RunList(int iPlayer, int iType, int param)
		{
		}

		// Token: 0x06006841 RID: 26689 RVA: 0x0000216D File Offset: 0x0000036D
		private void FinishStep()
		{
		}

		// Token: 0x0400A33C RID: 41788
		private bool finished;

		// Token: 0x0400A33D RID: 41789
		private EffectTaskRunList.Step step;

		// Token: 0x0400A33E RID: 41790
		private int param1;

		// Token: 0x0400A33F RID: 41791
		private int param2;

		// Token: 0x0400A340 RID: 41792
		private int param3;

		// Token: 0x0400A341 RID: 41793
		private string text;

		// Token: 0x02000E15 RID: 3605
		private enum Step
		{
			// Token: 0x0400A343 RID: 41795
			WaitCardMove,
			// Token: 0x0400A344 RID: 41796
			Finish
		}
	}
}
