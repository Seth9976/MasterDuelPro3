using System;

namespace YgomGame.Duel
{
	// Token: 0x02000DAC RID: 3500
	public abstract class EffectTask
	{
		// Token: 0x060066CB RID: 26315 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool Update()
		{
			return false;
		}

		// Token: 0x060066CC RID: 26316 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void OnDestroy()
		{
		}

		// Token: 0x060066CD RID: 26317 RVA: 0x00002739 File Offset: 0x00000939
		public EffectTask(RunEffectWorker worker)
		{
		}

		// Token: 0x0400A10E RID: 41230
		protected RunEffectWorker worker;
	}
}
