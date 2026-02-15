using System;
using System.Collections.Generic;

namespace YgomGame.Duel
{
	// Token: 0x02000DBA RID: 3514
	public class EffectTaskCardDisable : EffectTask
	{
		// Token: 0x06006709 RID: 26377 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> PreCreate(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x0600670A RID: 26378 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
		{
			return null;
		}

		// Token: 0x0600670B RID: 26379 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskCardDisable(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
			: base(null)
		{
		}

		// Token: 0x0600670C RID: 26380 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x0600670D RID: 26381 RVA: 0x0000216D File Offset: 0x0000036D
		private void StepWaitInit()
		{
		}

		// Token: 0x0600670E RID: 26382 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitCardLoad()
		{
		}

		// Token: 0x0600670F RID: 26383 RVA: 0x0000216D File Offset: 0x0000036D
		private void StepShow()
		{
		}

		// Token: 0x06006710 RID: 26384 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnInit()
		{
		}

		// Token: 0x06006711 RID: 26385 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnMove()
		{
		}

		// Token: 0x06006712 RID: 26386 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnWait()
		{
		}

		// Token: 0x06006713 RID: 26387 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnBack()
		{
		}

		// Token: 0x06006714 RID: 26388 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnFinish()
		{
		}

		// Token: 0x0400A176 RID: 41334
		private bool finished;

		// Token: 0x0400A177 RID: 41335
		private CardShow cardShow;

		// Token: 0x0400A178 RID: 41336
		private SimpleEffect effAura;

		// Token: 0x0400A179 RID: 41337
		private int player;

		// Token: 0x0400A17A RID: 41338
		private int position;

		// Token: 0x0400A17B RID: 41339
		private int index;

		// Token: 0x0400A17C RID: 41340
		private int uniqueID;

		// Token: 0x0400A17D RID: 41341
		private int cardID;

		// Token: 0x0400A17E RID: 41342
		private bool face;

		// Token: 0x0400A17F RID: 41343
		private CardRoot cardRoot;

		// Token: 0x0400A180 RID: 41344
		private bool tempCard;

		// Token: 0x0400A181 RID: 41345
		private bool isAttacker;

		// Token: 0x0400A182 RID: 41346
		private EffectTaskCardDisable.Step step;

		// Token: 0x02000DBB RID: 3515
		private enum Step
		{
			// Token: 0x0400A184 RID: 41348
			WaitInit,
			// Token: 0x0400A185 RID: 41349
			WaitCardLoad,
			// Token: 0x0400A186 RID: 41350
			Show,
			// Token: 0x0400A187 RID: 41351
			Finish
		}
	}
}
