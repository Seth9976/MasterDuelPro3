using System;
using System.Collections.Generic;

namespace YgomGame.Duel
{
	// Token: 0x02000DC1 RID: 3521
	public class EffectTaskCardFlipTurn : EffectTask
	{
		// Token: 0x06006722 RID: 26402 RVA: 0x0000216D File Offset: 0x0000036D
		public static void MinimumEffect(RunEffectWorker worker, int param1, int param2, int param3)
		{
		}

		// Token: 0x06006723 RID: 26403 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> PreCreate(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x06006724 RID: 26404 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
		{
			return null;
		}

		// Token: 0x06006725 RID: 26405 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskCardFlipTurn(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
			: base(null)
		{
		}

		// Token: 0x06006726 RID: 26406 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnDestroy()
		{
		}

		// Token: 0x06006727 RID: 26407 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x06006728 RID: 26408 RVA: 0x0000216D File Offset: 0x0000036D
		private void FinishStep()
		{
		}

		// Token: 0x06006729 RID: 26409 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitCardMoveStep()
		{
		}

		// Token: 0x0600672A RID: 26410 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitSetCardStep()
		{
		}

		// Token: 0x0600672B RID: 26411 RVA: 0x0000216D File Offset: 0x0000036D
		private void StartMoveStep()
		{
		}

		// Token: 0x0600672C RID: 26412 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitCamMoveStep()
		{
		}

		// Token: 0x0400A196 RID: 41366
		private bool finished;

		// Token: 0x0400A197 RID: 41367
		private EffectTaskCardFlipTurn.Step step;

		// Token: 0x0400A198 RID: 41368
		private Engine.CardStatus st;

		// Token: 0x0400A199 RID: 41369
		private CardRoot cardRoot;

		// Token: 0x0400A19A RID: 41370
		private bool camMoved;

		// Token: 0x0400A19B RID: 41371
		private CardPlace cardPlace;

		// Token: 0x0400A19C RID: 41372
		private int cardId;

		// Token: 0x0400A19D RID: 41373
		private int uniqueID;

		// Token: 0x0400A19E RID: 41374
		private EffectTaskCardFlipTurn.ReasonType reasonType;

		// Token: 0x0400A19F RID: 41375
		private bool isReverse;

		// Token: 0x02000DC2 RID: 3522
		private enum Step
		{
			// Token: 0x0400A1A1 RID: 41377
			WaitCardMove,
			// Token: 0x0400A1A2 RID: 41378
			WaitSetCard,
			// Token: 0x0400A1A3 RID: 41379
			StartMove,
			// Token: 0x0400A1A4 RID: 41380
			WaitCamMove,
			// Token: 0x0400A1A5 RID: 41381
			WaitFlipTurn,
			// Token: 0x0400A1A6 RID: 41382
			WaitSummon,
			// Token: 0x0400A1A7 RID: 41383
			Finish
		}

		// Token: 0x02000DC3 RID: 3523
		private enum ReasonType
		{
			// Token: 0x0400A1A9 RID: 41385
			CardEffect,
			// Token: 0x0400A1AA RID: 41386
			BattleAttack,
			// Token: 0x0400A1AB RID: 41387
			StandChange,
			// Token: 0x0400A1AC RID: 41388
			FlipSummon,
			// Token: 0x0400A1AD RID: 41389
			Look
		}
	}
}
