using System;
using System.Collections.Generic;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000052 RID: 82
	[Obsolete("For best performance use PlayableAsset and PlayableBehaviour.")]
	[Serializable]
	public class BasicPlayableBehaviour : ScriptableObject, IPlayableAsset, IPlayableBehaviour
	{
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x00009637 File Offset: 0x00007837
		public virtual double duration
		{
			get
			{
				return PlayableBinding.DefaultDuration;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x000094EC File Offset: 0x000076EC
		public virtual IEnumerable<PlayableBinding> outputs
		{
			get
			{
				return PlayableBinding.None;
			}
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00002811 File Offset: 0x00000A11
		public virtual void OnGraphStart(Playable playable)
		{
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00002811 File Offset: 0x00000A11
		public virtual void OnGraphStop(Playable playable)
		{
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00002811 File Offset: 0x00000A11
		public virtual void OnPlayableCreate(Playable playable)
		{
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00002811 File Offset: 0x00000A11
		public virtual void OnPlayableDestroy(Playable playable)
		{
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00002811 File Offset: 0x00000A11
		public virtual void OnBehaviourPlay(Playable playable, FrameData info)
		{
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00002811 File Offset: 0x00000A11
		public virtual void OnBehaviourPause(Playable playable, FrameData info)
		{
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00002811 File Offset: 0x00000A11
		public virtual void PrepareFrame(Playable playable, FrameData info)
		{
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00002811 File Offset: 0x00000A11
		public virtual void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000963E File Offset: 0x0000783E
		public virtual Playable CreatePlayable(PlayableGraph graph, GameObject owner)
		{
			return ScriptPlayable<BasicPlayableBehaviour>.Create(graph, this, 0);
		}
	}
}
