using System;

namespace YgomGame.Duel
{
	// Token: 0x02000DBD RID: 3517
	public class EffectTaskCardExclude : EffectTask
	{
		// Token: 0x06006718 RID: 26392 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x06006719 RID: 26393 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskCardExclude(RunEffectWorker worker, int param1, int param2, int param3)
			: base(null)
		{
		}

		// Token: 0x0600671A RID: 26394 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x0600671B RID: 26395 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitCardMoveStep()
		{
		}

		// Token: 0x0600671C RID: 26396 RVA: 0x0000216D File Offset: 0x0000036D
		private void FinishStep()
		{
		}

		// Token: 0x0400A188 RID: 41352
		private bool finished;

		// Token: 0x0400A189 RID: 41353
		private EffectTaskCardExclude.Step step;

		// Token: 0x0400A18A RID: 41354
		private int team;

		// Token: 0x0400A18B RID: 41355
		private int position;

		// Token: 0x0400A18C RID: 41356
		private int index;

		// Token: 0x0400A18D RID: 41357
		private int uniqueID;

		// Token: 0x02000DBE RID: 3518
		private enum Step
		{
			// Token: 0x0400A18F RID: 41359
			WaitCardMove,
			// Token: 0x0400A190 RID: 41360
			Finish
		}
	}
}
