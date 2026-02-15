using System;
using UnityEngine.Playables;

namespace YgomGame.Duel
{
	// Token: 0x02000EDB RID: 3803
	public class PendulumReadyEffect
	{
		// Token: 0x06006ED3 RID: 28371 RVA: 0x0000216D File Offset: 0x0000036D
		public void Play(bool near, Action onFinished)
		{
		}

		// Token: 0x06006ED4 RID: 28372 RVA: 0x0000216D File Offset: 0x0000036D
		public void LoopOut()
		{
		}

		// Token: 0x06006ED5 RID: 28373 RVA: 0x0000216D File Offset: 0x0000036D
		public void Cancel()
		{
		}

		// Token: 0x06006ED6 RID: 28374 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsPlaying()
		{
			return false;
		}

		// Token: 0x0400A9B3 RID: 43443
		private PlayableDirector timeline;

		// Token: 0x0400A9B4 RID: 43444
		private bool playing;

		// Token: 0x0400A9B5 RID: 43445
		private bool loopout;

		// Token: 0x0400A9B6 RID: 43446
		private bool cancel;
	}
}
