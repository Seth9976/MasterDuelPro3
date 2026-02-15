using System;
using System.Collections.Generic;

namespace YgomGame.Duel
{
	// Token: 0x02000DB0 RID: 3504
	public class EffectTaskBattleInit : EffectTask
	{
		// Token: 0x060066D8 RID: 26328 RVA: 0x0000216D File Offset: 0x0000036D
		public static void MinimumEffect(RunEffectWorker worker, int param1, int param2, int param3)
		{
		}

		// Token: 0x060066D9 RID: 26329 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> PreCreate(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x060066DA RID: 26330 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
		{
			return null;
		}

		// Token: 0x060066DB RID: 26331 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskBattleInit(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
			: base(null)
		{
		}

		// Token: 0x060066DC RID: 26332 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}
	}
}
