using System;
using System.Collections.Generic;

namespace YgomGame.Duel
{
	// Token: 0x02000DB8 RID: 3512
	public class EffectTaskCardCheat : EffectTask
	{
		// Token: 0x06006700 RID: 26368 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> PreCreate(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x06006701 RID: 26369 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
		{
			return null;
		}

		// Token: 0x06006702 RID: 26370 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskCardCheat(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
			: base(null)
		{
		}

		// Token: 0x06006703 RID: 26371 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x06006704 RID: 26372 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitCardMoveStep()
		{
		}

		// Token: 0x06006705 RID: 26373 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitCamMoveStep()
		{
		}

		// Token: 0x06006706 RID: 26374 RVA: 0x0000216D File Offset: 0x0000036D
		private void StartPlacementStep()
		{
		}

		// Token: 0x06006707 RID: 26375 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitEffectStep()
		{
		}

		// Token: 0x06006708 RID: 26376 RVA: 0x0000216D File Offset: 0x0000036D
		private void FinishStep()
		{
		}

		// Token: 0x0400A164 RID: 41316
		private bool finished;

		// Token: 0x0400A165 RID: 41317
		private EffectTaskCardCheat.Step step;

		// Token: 0x0400A166 RID: 41318
		private Engine.CardStatus st;

		// Token: 0x0400A167 RID: 41319
		private int cardId;

		// Token: 0x0400A168 RID: 41320
		private CardLocator cardLocator;

		// Token: 0x0400A169 RID: 41321
		private bool camMoved;

		// Token: 0x0400A16A RID: 41322
		private CardPlace cardPlace;

		// Token: 0x0400A16B RID: 41323
		private CardRoot cardRoot;

		// Token: 0x0400A16C RID: 41324
		private bool isFace;

		// Token: 0x0400A16D RID: 41325
		private bool waitEffect;

		// Token: 0x0400A16E RID: 41326
		private int uniqueID;

		// Token: 0x02000DB9 RID: 3513
		private enum Step
		{
			// Token: 0x0400A170 RID: 41328
			WaitCardMove,
			// Token: 0x0400A171 RID: 41329
			WaitCamMove,
			// Token: 0x0400A172 RID: 41330
			StartPlacement,
			// Token: 0x0400A173 RID: 41331
			WaitSetCard,
			// Token: 0x0400A174 RID: 41332
			WaitEffect,
			// Token: 0x0400A175 RID: 41333
			Finish
		}
	}
}
