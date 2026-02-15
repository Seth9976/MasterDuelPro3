using System;
using System.Collections.Generic;

namespace YgomGame.Duel
{
	// Token: 0x02000DF0 RID: 3568
	public class EffectTaskGraveTop : EffectTask
	{
		// Token: 0x060067BE RID: 26558 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> PreCreate(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x060067BF RID: 26559 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
		{
			return null;
		}

		// Token: 0x060067C0 RID: 26560 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskGraveTop(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
			: base(null)
		{
		}

		// Token: 0x060067C1 RID: 26561 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x060067C2 RID: 26562 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitCardMoveStep()
		{
		}

		// Token: 0x060067C3 RID: 26563 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitCardLoadStep()
		{
		}

		// Token: 0x060067C4 RID: 26564 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnFinished()
		{
		}

		// Token: 0x0400A2A6 RID: 41638
		private bool finished;

		// Token: 0x0400A2A7 RID: 41639
		private EffectTaskGraveTop.Step step;

		// Token: 0x0400A2A8 RID: 41640
		private int team;

		// Token: 0x0400A2A9 RID: 41641
		private int position;

		// Token: 0x0400A2AA RID: 41642
		private int index;

		// Token: 0x0400A2AB RID: 41643
		private int uniqueId;

		// Token: 0x0400A2AC RID: 41644
		private int cardId;

		// Token: 0x0400A2AD RID: 41645
		private bool face;

		// Token: 0x0400A2AE RID: 41646
		private bool turn;

		// Token: 0x0400A2AF RID: 41647
		private CardPlace cardPlace;

		// Token: 0x0400A2B0 RID: 41648
		private CardLocator noneLocator;

		// Token: 0x0400A2B1 RID: 41649
		private CardRoot cardRoot;

		// Token: 0x02000DF1 RID: 3569
		private enum Step
		{
			// Token: 0x0400A2B3 RID: 41651
			WaitCardMove,
			// Token: 0x0400A2B4 RID: 41652
			WaitCardLoad,
			// Token: 0x0400A2B5 RID: 41653
			WaitDeckTopMove
		}
	}
}
