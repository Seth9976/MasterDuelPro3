using System;
using System.Collections.Generic;

namespace YgomGame.Duel
{
	// Token: 0x02000DF6 RID: 3574
	public class EffectTaskLifeDamage : EffectTask
	{
		// Token: 0x060067CF RID: 26575 RVA: 0x0000216D File Offset: 0x0000036D
		public static void MinimumEffect(RunEffectWorker worker, int param1, int param2, int param3)
		{
		}

		// Token: 0x060067D0 RID: 26576 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> PreCreate(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x060067D1 RID: 26577 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
		{
			return null;
		}

		// Token: 0x060067D2 RID: 26578 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskLifeDamage(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
			: base(null)
		{
		}

		// Token: 0x060067D3 RID: 26579 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x060067D4 RID: 26580 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitCardEffect()
		{
		}

		// Token: 0x060067D5 RID: 26581 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayDamageEffect()
		{
		}

		// Token: 0x0400A2C9 RID: 41673
		private EffectTaskLifeDamage.Step step;

		// Token: 0x0400A2CA RID: 41674
		private int player;

		// Token: 0x0400A2CB RID: 41675
		private int damage;

		// Token: 0x0400A2CC RID: 41676
		private bool isPrev;

		// Token: 0x0400A2CD RID: 41677
		private Engine.DamageType type;

		// Token: 0x0400A2CE RID: 41678
		private int currentLP;

		// Token: 0x02000DF7 RID: 3575
		private enum Step
		{
			// Token: 0x0400A2D0 RID: 41680
			WaitCardEffect,
			// Token: 0x0400A2D1 RID: 41681
			WaitDamageEffect,
			// Token: 0x0400A2D2 RID: 41682
			Finished
		}
	}
}
