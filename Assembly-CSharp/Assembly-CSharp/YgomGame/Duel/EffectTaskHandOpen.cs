using System;
using System.Collections.Generic;

namespace YgomGame.Duel
{
	// Token: 0x02000DF2 RID: 3570
	public class EffectTaskHandOpen : EffectTask
	{
		// Token: 0x060067C5 RID: 26565 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> PreCreate(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x060067C6 RID: 26566 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
		{
			return null;
		}

		// Token: 0x060067C7 RID: 26567 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskHandOpen(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
			: base(null)
		{
		}

		// Token: 0x060067C8 RID: 26568 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x060067C9 RID: 26569 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitCardMoveStep()
		{
		}

		// Token: 0x060067CA RID: 26570 RVA: 0x0000216D File Offset: 0x0000036D
		private void FinishStep()
		{
		}

		// Token: 0x0400A2B6 RID: 41654
		private bool finished;

		// Token: 0x0400A2B7 RID: 41655
		private EffectTaskHandOpen.Step step;

		// Token: 0x0400A2B8 RID: 41656
		private int team;

		// Token: 0x0400A2B9 RID: 41657
		private bool isOpen;

		// Token: 0x0400A2BA RID: 41658
		private int uniqueId;

		// Token: 0x0400A2BB RID: 41659
		private bool isAll;

		// Token: 0x0400A2BC RID: 41660
		private List<int> cardIds;

		// Token: 0x0400A2BD RID: 41661
		private List<bool> cardFaces;

		// Token: 0x02000DF3 RID: 3571
		private enum Step
		{
			// Token: 0x0400A2BF RID: 41663
			None,
			// Token: 0x0400A2C0 RID: 41664
			WaitCardMove,
			// Token: 0x0400A2C1 RID: 41665
			WaitFlipTurn,
			// Token: 0x0400A2C2 RID: 41666
			Finish
		}
	}
}
