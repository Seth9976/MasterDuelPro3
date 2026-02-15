using System;

namespace YgomGame.Duel
{
	// Token: 0x02000DEC RID: 3564
	public class EffectTaskDuelEnd : EffectTask
	{
		// Token: 0x060067B3 RID: 26547 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x060067B4 RID: 26548 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskDuelEnd(RunEffectWorker worker, int param1, int param2, int param3)
			: base(null)
		{
		}

		// Token: 0x060067B5 RID: 26549 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x0400A296 RID: 41622
		private Engine.ResultType exactResultType;

		// Token: 0x0400A297 RID: 41623
		private Engine.ResultType resultType;

		// Token: 0x0400A298 RID: 41624
		private Engine.FinishType finishType;
	}
}
