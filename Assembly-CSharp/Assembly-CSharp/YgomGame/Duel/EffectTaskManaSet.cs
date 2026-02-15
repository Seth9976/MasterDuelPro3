using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000DFD RID: 3581
	public class EffectTaskManaSet : EffectTask
	{
		// Token: 0x060067E3 RID: 26595 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> PreCreate(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x060067E4 RID: 26596 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
		{
			return null;
		}

		// Token: 0x060067E5 RID: 26597 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskManaSet(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
			: base(null)
		{
		}

		// Token: 0x060067E6 RID: 26598 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x060067E7 RID: 26599 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitCardEffectStep()
		{
		}

		// Token: 0x060067E8 RID: 26600 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitManaSetStep()
		{
		}

		// Token: 0x0400A2DB RID: 41691
		private Vector3 anchor;

		// Token: 0x0400A2DC RID: 41692
		private float timer;

		// Token: 0x0400A2DD RID: 41693
		private const float waitTime = 0.5f;

		// Token: 0x0400A2DE RID: 41694
		private Engine.CounterType counterType;

		// Token: 0x0400A2DF RID: 41695
		private int dispCount;

		// Token: 0x0400A2E0 RID: 41696
		private int targetCount;

		// Token: 0x0400A2E1 RID: 41697
		private ManaSet manaSet;

		// Token: 0x0400A2E2 RID: 41698
		private EffectTaskManaSet.Step step;

		// Token: 0x02000DFE RID: 3582
		private enum Step
		{
			// Token: 0x0400A2E4 RID: 41700
			WaitCardEffect,
			// Token: 0x0400A2E5 RID: 41701
			WaitManaSet,
			// Token: 0x0400A2E6 RID: 41702
			Finish
		}
	}
}
