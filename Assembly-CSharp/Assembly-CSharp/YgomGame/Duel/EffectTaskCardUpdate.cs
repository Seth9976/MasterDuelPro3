using System;
using System.Collections.Generic;

namespace YgomGame.Duel
{
	// Token: 0x02000DD3 RID: 3539
	public class EffectTaskCardUpdate : EffectTask
	{
		// Token: 0x06006765 RID: 26469 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> PreCreate(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x06006766 RID: 26470 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
		{
			return null;
		}

		// Token: 0x06006767 RID: 26471 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskCardUpdate(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
			: base(null)
		{
		}

		// Token: 0x06006768 RID: 26472 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}
	}
}
