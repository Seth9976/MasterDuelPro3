using System;
using UnityEngine.Playables;
using UnityEngine.Rendering;

namespace YgomSystem
{
	// Token: 0x020004B3 RID: 1203
	public class PostEffectPlayableBehaviour : PlayableBehaviour
	{
		// Token: 0x060026A9 RID: 9897 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnGraphStart(Playable playable)
		{
		}

		// Token: 0x060026AA RID: 9898 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnGraphStop(Playable playable)
		{
		}

		// Token: 0x060026AB RID: 9899 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnBehaviourPlay(Playable playable, FrameData info)
		{
		}

		// Token: 0x060026AC RID: 9900 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnBehaviourPause(Playable playable, FrameData info)
		{
		}

		// Token: 0x060026AD RID: 9901 RVA: 0x0000216D File Offset: 0x0000036D
		public override void PrepareFrame(Playable playable, FrameData info)
		{
		}

		// Token: 0x040027AD RID: 10157
		public Volume volume;

		// Token: 0x040027AE RID: 10158
		public VolumeProfile volumeProfile;

		// Token: 0x040027AF RID: 10159
		public PlayableDirector playableDirector;
	}
}
