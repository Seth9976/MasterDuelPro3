using System;
using UnityEngine;
using UnityEngine.Playables;
using YgomSystem.Timeline;

namespace MDPro3.Duel
{
	// Token: 0x020014BE RID: 5310
	public class LoopTrackManager : MonoBehaviour
	{
		// Token: 0x1700148A RID: 5258
		// (get) Token: 0x06009B14 RID: 39700 RVA: 0x00180040 File Offset: 0x0017E240
		private PlayableDirector PlayableDirector
		{
			get
			{
				return this.playableDirector = ((this.playableDirector != null) ? this.playableDirector : base.GetComponent<PlayableDirector>());
			}
		}

		// Token: 0x06009B15 RID: 39701 RVA: 0x00180072 File Offset: 0x0017E272
		public void StopLoop()
		{
			if (this.loopMixerBehaviour != null)
			{
				this.loopMixerBehaviour.needLoop = false;
			}
			if (this.loopBehaviour != null)
			{
				this.loopBehaviour.loopClip = null;
				this.loopBehaviour = null;
			}
		}

		// Token: 0x0400D90E RID: 55566
		private PlayableDirector playableDirector;

		// Token: 0x0400D90F RID: 55567
		public LoopMixerBehaviour loopMixerBehaviour;

		// Token: 0x0400D910 RID: 55568
		public LoopBehaviour loopBehaviour;
	}
}
