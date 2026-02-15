using System;
using UnityEngine.Playables;

namespace YgomSystem.Timeline
{
	// Token: 0x020006B3 RID: 1715
	[Serializable]
	public class PauseBehaviour : PlayableBehaviour
	{
		// Token: 0x060035AA RID: 13738 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnPlayableCreate(Playable playable)
		{
		}

		// Token: 0x060035AB RID: 13739 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnGraphStart(Playable playable)
		{
		}

		// Token: 0x060035AC RID: 13740 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnGraphStop(Playable playable)
		{
		}

		// Token: 0x060035AD RID: 13741 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnBehaviourPlay(Playable playable, FrameData info)
		{
		}

		// Token: 0x060035AE RID: 13742 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnBehaviourPause(Playable playable, FrameData info)
		{
		}

		// Token: 0x060035AF RID: 13743 RVA: 0x0000216D File Offset: 0x0000036D
		public override void PrepareFrame(Playable playable, FrameData info)
		{
		}

		// Token: 0x060035B0 RID: 13744 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnPlayableDestroy(Playable playable)
		{
		}

		// Token: 0x040030E8 RID: 12520
		private PlayableDirector m_Director;

		// Token: 0x040030E9 RID: 12521
		[NonSerialized]
		public double startTime;

		// Token: 0x040030EA RID: 12522
		[NonSerialized]
		public double endTime;

		// Token: 0x040030EB RID: 12523
		private bool m_Ready;

		// Token: 0x040030EC RID: 12524
		private bool m_IsCompletePause;

		// Token: 0x040030ED RID: 12525
		private bool m_IsCompleteResume;
	}
}
