using System;
using System.Collections.Generic;

namespace YgomGame.Duel
{
	// Token: 0x02000DC4 RID: 3524
	public class EffectTaskCardHappen : EffectTask
	{
		// Token: 0x0600672D RID: 26413 RVA: 0x0000216D File Offset: 0x0000036D
		public static void MinimumEffect(RunEffectWorker worker, int param1, int param2, int param3)
		{
		}

		// Token: 0x0600672E RID: 26414 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> PreCreate(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x0600672F RID: 26415 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
		{
			return null;
		}

		// Token: 0x06006730 RID: 26416 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskCardHappen(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
			: base(null)
		{
		}

		// Token: 0x06006731 RID: 26417 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x06006732 RID: 26418 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool StepWaitInit()
		{
			return false;
		}

		// Token: 0x06006733 RID: 26419 RVA: 0x0000216D File Offset: 0x0000036D
		private void StepWaitTutorial()
		{
		}

		// Token: 0x06006734 RID: 26420 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool StepInitCard()
		{
			return false;
		}

		// Token: 0x06006735 RID: 26421 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool StepWaitCardLoad()
		{
			return false;
		}

		// Token: 0x06006736 RID: 26422 RVA: 0x0000216D File Offset: 0x0000036D
		private void StepShow()
		{
		}

		// Token: 0x06006737 RID: 26423 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnInit()
		{
		}

		// Token: 0x06006738 RID: 26424 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnMove()
		{
		}

		// Token: 0x06006739 RID: 26425 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnWait()
		{
		}

		// Token: 0x0600673A RID: 26426 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnBack()
		{
		}

		// Token: 0x0600673B RID: 26427 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnFinish()
		{
		}

		// Token: 0x0400A1AE RID: 41390
		private int cardID;

		// Token: 0x0400A1AF RID: 41391
		private int uniqueID;

		// Token: 0x0400A1B0 RID: 41392
		private int effectID;

		// Token: 0x0400A1B1 RID: 41393
		private int effectTextID;

		// Token: 0x0400A1B2 RID: 41394
		private int efxNo;

		// Token: 0x0400A1B3 RID: 41395
		private bool finished;

		// Token: 0x0400A1B4 RID: 41396
		private CardShow cardShow;

		// Token: 0x0400A1B5 RID: 41397
		private SimpleEffect effAura;

		// Token: 0x0400A1B6 RID: 41398
		private Engine.CardStatus st;

		// Token: 0x0400A1B7 RID: 41399
		private bool face;

		// Token: 0x0400A1B8 RID: 41400
		private CardRoot cardRoot;

		// Token: 0x0400A1B9 RID: 41401
		private bool tempCard;

		// Token: 0x0400A1BA RID: 41402
		private bool isAttacker;

		// Token: 0x0400A1BB RID: 41403
		private EffectTaskCardHappen.Step step;

		// Token: 0x0400A1BC RID: 41404
		private EffectTaskCardHappen.HappenType happenType;

		// Token: 0x02000DC5 RID: 3525
		private enum Step
		{
			// Token: 0x0400A1BE RID: 41406
			WaitInit,
			// Token: 0x0400A1BF RID: 41407
			WaitTutorial,
			// Token: 0x0400A1C0 RID: 41408
			InitCard,
			// Token: 0x0400A1C1 RID: 41409
			WaitCardLoad,
			// Token: 0x0400A1C2 RID: 41410
			Show,
			// Token: 0x0400A1C3 RID: 41411
			Finish
		}

		// Token: 0x02000DC6 RID: 3526
		private enum HappenType
		{
			// Token: 0x0400A1C5 RID: 41413
			Happen,
			// Token: 0x0400A1C6 RID: 41414
			Apply
		}
	}
}
