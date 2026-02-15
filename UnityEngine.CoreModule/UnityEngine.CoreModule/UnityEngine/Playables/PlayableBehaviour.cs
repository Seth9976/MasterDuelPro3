using System;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x02000307 RID: 775
	[RequiredByNativeCode]
	[Serializable]
	public abstract class PlayableBehaviour : IPlayableBehaviour, ICloneable
	{
		// Token: 0x06001559 RID: 5465 RVA: 0x000205EB File Offset: 0x0001E7EB
		public PlayableBehaviour()
		{
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x00003D56 File Offset: 0x00001F56
		public virtual void OnGraphStart(Playable playable)
		{
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x00003D56 File Offset: 0x00001F56
		public virtual void OnGraphStop(Playable playable)
		{
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x00003D56 File Offset: 0x00001F56
		public virtual void OnPlayableCreate(Playable playable)
		{
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x00003D56 File Offset: 0x00001F56
		public virtual void OnPlayableDestroy(Playable playable)
		{
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x00003D56 File Offset: 0x00001F56
		public virtual void OnBehaviourPlay(Playable playable, FrameData info)
		{
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x00003D56 File Offset: 0x00001F56
		public virtual void OnBehaviourPause(Playable playable, FrameData info)
		{
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x00003D56 File Offset: 0x00001F56
		public virtual void PrepareData(Playable playable, FrameData info)
		{
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x00003D56 File Offset: 0x00001F56
		public virtual void PrepareFrame(Playable playable, FrameData info)
		{
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x00003D56 File Offset: 0x00001F56
		public virtual void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x0002D1BC File Offset: 0x0002B3BC
		public virtual object Clone()
		{
			return base.MemberwiseClone();
		}
	}
}
