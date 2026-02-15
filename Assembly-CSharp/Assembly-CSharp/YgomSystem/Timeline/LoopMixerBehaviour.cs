using System;
using System.Collections.Generic;
using MDPro3.Duel;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace YgomSystem.Timeline
{
	// Token: 0x020006B1 RID: 1713
	[Serializable]
	public class LoopMixerBehaviour : PlayableBehaviour
	{
		// Token: 0x0600359A RID: 13722 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnPlayableCreate(Playable playable)
		{
		}

		// Token: 0x0600359B RID: 13723 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnBehaviourPlay(Playable playable, FrameData info)
		{
		}

		// Token: 0x0600359C RID: 13724 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnBehaviourPause(Playable playable, FrameData info)
		{
		}

		// Token: 0x0600359D RID: 13725 RVA: 0x0000216D File Offset: 0x0000036D
		public override void PrepareData(Playable playable, FrameData info)
		{
		}

		// Token: 0x0600359E RID: 13726 RVA: 0x000F3148 File Offset: 0x000F1348
		public override void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
			if (!this.m_Initialized)
			{
				if (!this.inied)
				{
					this.Ini(playable);
				}
				LabeledPlayableController lpc;
				if (this.m_Director.TryGetComponent<LabeledPlayableController>(out lpc))
				{
					lpc.loopMixerBehaviour = this;
					this.m_Initialized = true;
				}
			}
		}

		// Token: 0x0600359F RID: 13727 RVA: 0x000F318C File Offset: 0x000F138C
		private void Ini(Playable playable)
		{
			IExposedPropertyTable resolver = playable.GetGraph<Playable>().GetResolver();
			if (resolver is PlayableDirector)
			{
				this.m_Director = (PlayableDirector)resolver;
			}
			if (this.loopClips != null && this.loopClips.Count > 0)
			{
				this.currentClip = this.loopClips[0];
				foreach (TimelineClip clip in this.loopClips)
				{
					LoopClip loopClip = clip.asset as LoopClip;
					if (loopClip != null)
					{
						loopClip.loopClip = clip;
						loopClip.PassClip();
					}
				}
			}
			LoopTrackManager ltm;
			if (this.m_Director.TryGetComponent<LoopTrackManager>(out ltm))
			{
				ltm.loopMixerBehaviour = this;
			}
			this.inied = true;
		}

		// Token: 0x060035A0 RID: 13728 RVA: 0x000F326C File Offset: 0x000F146C
		public override void PrepareFrame(Playable playable, FrameData info)
		{
			if (this.currentClip != null && this.needLoop && this.m_Director.time > this.currentClip.extrapolatedStart + this.currentClip.duration - 0.019999999552965164)
			{
				this.m_Director.time = this.currentClip.extrapolatedStart;
			}
		}

		// Token: 0x060035A1 RID: 13729 RVA: 0x000F32D0 File Offset: 0x000F14D0
		public void PlayClip(string label, TimelineClip loopClip)
		{
			foreach (TimelineClip clip in this.track.GetClips())
			{
				if (clip.asset is LabelClip && clip.displayName == label)
				{
					this.m_Director.time = clip.extrapolatedStart;
					this.currentClip = loopClip;
					break;
				}
			}
		}

		// Token: 0x060035A2 RID: 13730 RVA: 0x0000216D File Offset: 0x0000036D
		private void CheckLoopClip()
		{
		}

		// Token: 0x060035A3 RID: 13731 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetNextClipDuration()
		{
		}

		// Token: 0x060035A4 RID: 13732 RVA: 0x0000216A File Offset: 0x0000036A
		private TimelineClip SearchCurrentClip()
		{
			return null;
		}

		// Token: 0x060035A5 RID: 13733 RVA: 0x0000216A File Offset: 0x0000036A
		private TimelineClip SearchNextClip()
		{
			return null;
		}

		// Token: 0x040030E0 RID: 12512
		private readonly double k_SecMargine;

		// Token: 0x040030E1 RID: 12513
		private PlayableDirector m_Director;

		// Token: 0x040030E2 RID: 12514
		[NonSerialized]
		public List<TimelineClip> loopClips;

		// Token: 0x040030E3 RID: 12515
		private bool m_Initialized;

		// Token: 0x040030E4 RID: 12516
		public LabelTrack track;

		// Token: 0x040030E5 RID: 12517
		private TimelineClip currentClip;

		// Token: 0x040030E6 RID: 12518
		private bool inied;

		// Token: 0x040030E7 RID: 12519
		public bool needLoop = true;
	}
}
