using System;
using System.Collections.Generic;

namespace YgomGame.Duel
{
	// Token: 0x02000E29 RID: 3625
	public class EffectTaskWaitInput : EffectTask
	{
		// Token: 0x0600687B RID: 26747 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> PreCreate(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x0600687C RID: 26748 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
		{
			return null;
		}

		// Token: 0x0600687D RID: 26749 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskWaitInput(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
			: base(null)
		{
		}

		// Token: 0x0600687E RID: 26750 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x0600687F RID: 26751 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDrawPhase(int param1, int param2, int param3)
		{
		}

		// Token: 0x06006880 RID: 26752 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnBattlePhase(int param1, int param2, int param3)
		{
		}

		// Token: 0x06006881 RID: 26753 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool WaitCardEffect()
		{
			return false;
		}

		// Token: 0x06006882 RID: 26754 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitInput()
		{
		}

		// Token: 0x0400A37D RID: 41853
		private bool finished;

		// Token: 0x0400A37E RID: 41854
		private EffectTaskWaitInput.Step step;

		// Token: 0x0400A37F RID: 41855
		private Engine.MenuActType menuActType;

		// Token: 0x0400A380 RID: 41856
		private int param1;

		// Token: 0x0400A381 RID: 41857
		private int param2;

		// Token: 0x0400A382 RID: 41858
		private int param3;

		// Token: 0x0400A383 RID: 41859
		private string text;

		// Token: 0x0400A384 RID: 41860
		public const uint cmdBitHighlight = 2045U;

		// Token: 0x02000E2A RID: 3626
		private enum Step
		{
			// Token: 0x0400A386 RID: 41862
			WaitCardEffect,
			// Token: 0x0400A387 RID: 41863
			WaitTutorial,
			// Token: 0x0400A388 RID: 41864
			WaitInput,
			// Token: 0x0400A389 RID: 41865
			WaitInputStart,
			// Token: 0x0400A38A RID: 41866
			Finish
		}
	}
}
