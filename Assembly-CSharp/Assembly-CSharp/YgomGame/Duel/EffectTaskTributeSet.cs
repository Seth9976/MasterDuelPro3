using System;

namespace YgomGame.Duel
{
	// Token: 0x02000E22 RID: 3618
	public class EffectTaskTributeSet : EffectTask
	{
		// Token: 0x06006864 RID: 26724 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x06006865 RID: 26725 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskTributeSet(RunEffectWorker worker, int param1, int param2, int param3)
			: base(null)
		{
		}

		// Token: 0x06006866 RID: 26726 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x0400A36C RID: 41836
		private bool finished;
	}
}
