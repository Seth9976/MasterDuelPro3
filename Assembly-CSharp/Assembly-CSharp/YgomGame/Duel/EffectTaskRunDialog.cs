using System;
using System.Collections.Generic;

namespace YgomGame.Duel
{
	// Token: 0x02000E0B RID: 3595
	public class EffectTaskRunDialog : EffectTask
	{
		// Token: 0x0600680B RID: 26635 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> PreCreate(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x0600680C RID: 26636 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
		{
			return null;
		}

		// Token: 0x0600680D RID: 26637 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskRunDialog(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
			: base(null)
		{
		}

		// Token: 0x0600680E RID: 26638 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x0600680F RID: 26639 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool WaitCardEffectStep()
		{
			return false;
		}

		// Token: 0x06006810 RID: 26640 RVA: 0x0000216D File Offset: 0x0000036D
		private void FinishStep()
		{
		}

		// Token: 0x06006811 RID: 26641 RVA: 0x0000216D File Offset: 0x0000036D
		private void RunDialog()
		{
		}

		// Token: 0x06006812 RID: 26642 RVA: 0x0000216D File Offset: 0x0000036D
		private void StartDialogInput()
		{
		}

		// Token: 0x0400A305 RID: 41733
		private EffectTaskRunDialog.Step step;

		// Token: 0x0400A306 RID: 41734
		private bool finished;

		// Token: 0x0400A307 RID: 41735
		private Engine.DialogType type;

		// Token: 0x0400A308 RID: 41736
		private int textId;

		// Token: 0x0400A309 RID: 41737
		private int dwParam;

		// Token: 0x0400A30A RID: 41738
		private string text;

		// Token: 0x0400A30B RID: 41739
		private static string activateCardSelectionText;

		// Token: 0x02000E0C RID: 3596
		private enum Step
		{
			// Token: 0x0400A30D RID: 41741
			WaitCardEffect,
			// Token: 0x0400A30E RID: 41742
			WaitTutorial,
			// Token: 0x0400A30F RID: 41743
			Finish
		}
	}
}
