using System;
using System.Collections.Generic;

namespace YgomGame.Duel
{
	// Token: 0x02000E25 RID: 3621
	public class EffectTaskTuningSet : EffectTask
	{
		// Token: 0x0600686D RID: 26733 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> PreCreate(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x0600686E RID: 26734 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
		{
			return null;
		}

		// Token: 0x0600686F RID: 26735 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskTuningSet(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
			: base(null)
		{
		}

		// Token: 0x06006870 RID: 26736 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x0400A36F RID: 41839
		private bool finished;
	}
}
