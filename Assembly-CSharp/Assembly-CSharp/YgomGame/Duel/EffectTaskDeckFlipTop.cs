using System;
using System.Collections.Generic;

namespace YgomGame.Duel
{
	// Token: 0x02000DE6 RID: 3558
	public class EffectTaskDeckFlipTop : EffectTask
	{
		// Token: 0x060067A0 RID: 26528 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> PreCreate(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x060067A1 RID: 26529 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
		{
			return null;
		}

		// Token: 0x060067A2 RID: 26530 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskDeckFlipTop(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
			: base(null)
		{
		}

		// Token: 0x060067A3 RID: 26531 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x060067A4 RID: 26532 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitCardMoveStep()
		{
		}

		// Token: 0x060067A5 RID: 26533 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitCardLoadingStep()
		{
		}

		// Token: 0x060067A6 RID: 26534 RVA: 0x0000216D File Offset: 0x0000036D
		private void StartMove()
		{
		}

		// Token: 0x0400A278 RID: 41592
		private bool finished;

		// Token: 0x0400A279 RID: 41593
		private EffectTaskDeckFlipTop.Step step;

		// Token: 0x0400A27A RID: 41594
		private int team;

		// Token: 0x0400A27B RID: 41595
		private int position;

		// Token: 0x0400A27C RID: 41596
		private bool isOpen;

		// Token: 0x0400A27D RID: 41597
		private int index;

		// Token: 0x0400A27E RID: 41598
		private int uniqueId;

		// Token: 0x0400A27F RID: 41599
		private int cardId;

		// Token: 0x0400A280 RID: 41600
		private DeckCardPlace deckPlace;

		// Token: 0x0400A281 RID: 41601
		private CardRoot cardRoot;

		// Token: 0x02000DE7 RID: 3559
		private enum Step
		{
			// Token: 0x0400A283 RID: 41603
			WaitCardMoving,
			// Token: 0x0400A284 RID: 41604
			WaitCardLoading,
			// Token: 0x0400A285 RID: 41605
			Wait
		}
	}
}
