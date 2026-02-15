using System;
using YgomSystem.UI;

namespace YgomGame.Duel
{
	// Token: 0x02000E09 RID: 3593
	public class EffectTaskRunCoin : EffectTask
	{
		// Token: 0x06006805 RID: 26629 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x06006806 RID: 26630 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskRunCoin(RunEffectWorker worker, int param1, int param2, int param3)
			: base(null)
		{
		}

		// Token: 0x06006807 RID: 26631 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x06006808 RID: 26632 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitCardEffect()
		{
		}

		// Token: 0x06006809 RID: 26633 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitCoin()
		{
		}

		// Token: 0x0600680A RID: 26634 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnDestroy()
		{
		}

		// Token: 0x0400A2FA RID: 41722
		private EffectTaskRunCoin.Step step;

		// Token: 0x0400A2FB RID: 41723
		private ScreenSelector selector;

		// Token: 0x0400A2FC RID: 41724
		private int numThrows;

		// Token: 0x0400A2FD RID: 41725
		private int faceBits;

		// Token: 0x0400A2FE RID: 41726
		private int shineBits;

		// Token: 0x0400A2FF RID: 41727
		private bool isTimelineLoaded;

		// Token: 0x02000E0A RID: 3594
		private enum Step
		{
			// Token: 0x0400A301 RID: 41729
			WaitCardEffect,
			// Token: 0x0400A302 RID: 41730
			WaitLoadEffect,
			// Token: 0x0400A303 RID: 41731
			WaitCoin,
			// Token: 0x0400A304 RID: 41732
			Finish
		}
	}
}
