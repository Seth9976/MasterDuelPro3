using System;
using System.Collections.Generic;

namespace YgomGame.Duel
{
	// Token: 0x02000DD1 RID: 3537
	public class EffectTaskCardSwap : EffectTask
	{
		// Token: 0x0600675C RID: 26460 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> PreCreate(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x0600675D RID: 26461 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
		{
			return null;
		}

		// Token: 0x0600675E RID: 26462 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskCardSwap(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
			: base(null)
		{
		}

		// Token: 0x0600675F RID: 26463 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x06006760 RID: 26464 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitCardMoveStep()
		{
		}

		// Token: 0x06006761 RID: 26465 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitCamMoveStep()
		{
		}

		// Token: 0x06006762 RID: 26466 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitSwapStep()
		{
		}

		// Token: 0x06006763 RID: 26467 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetBackCardStatus(CardRoot cardRoot, int uniqueID, int cardID)
		{
		}

		// Token: 0x06006764 RID: 26468 RVA: 0x0000216D File Offset: 0x0000036D
		private void ShowBackCardStatus(CardRoot cardRoot, Engine.CardStatus cardStatus)
		{
		}

		// Token: 0x0400A22C RID: 41516
		private bool finished;

		// Token: 0x0400A22D RID: 41517
		private EffectTaskCardSwap.Step step;

		// Token: 0x0400A22E RID: 41518
		private Engine.CardStatus from;

		// Token: 0x0400A22F RID: 41519
		private Engine.CardStatus to;

		// Token: 0x0400A230 RID: 41520
		private bool camMoved;

		// Token: 0x0400A231 RID: 41521
		private CardPlace fromPlace;

		// Token: 0x0400A232 RID: 41522
		private CardPlace toPlace;

		// Token: 0x0400A233 RID: 41523
		private CardRoot fromCardRoot;

		// Token: 0x0400A234 RID: 41524
		private CardRoot toCardRoot;

		// Token: 0x0400A235 RID: 41525
		private int fromCardID;

		// Token: 0x0400A236 RID: 41526
		private int fromUniqueID;

		// Token: 0x0400A237 RID: 41527
		private int toCardID;

		// Token: 0x0400A238 RID: 41528
		private int toUniqueID;

		// Token: 0x02000DD2 RID: 3538
		private enum Step
		{
			// Token: 0x0400A23A RID: 41530
			WaitCardMove,
			// Token: 0x0400A23B RID: 41531
			WaitCamMove,
			// Token: 0x0400A23C RID: 41532
			WaitSwap
		}
	}
}
