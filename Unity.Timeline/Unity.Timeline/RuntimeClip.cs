using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x0200003D RID: 61
	internal class RuntimeClip : RuntimeClipBase
	{
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600024F RID: 591 RVA: 0x00008770 File Offset: 0x00006970
		public override double start
		{
			get
			{
				return this.m_Clip.extrapolatedStart;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000250 RID: 592 RVA: 0x0000877D File Offset: 0x0000697D
		public override double duration
		{
			get
			{
				return this.m_Clip.extrapolatedDuration;
			}
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000878A File Offset: 0x0000698A
		public RuntimeClip(TimelineClip clip, Playable clipPlayable, Playable parentMixer)
		{
			this.Create(clip, clipPlayable, parentMixer);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000879B File Offset: 0x0000699B
		private void Create(TimelineClip clip, Playable clipPlayable, Playable parentMixer)
		{
			this.m_Clip = clip;
			this.m_Playable = clipPlayable;
			this.m_ParentMixer = parentMixer;
			clipPlayable.Pause<Playable>();
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000253 RID: 595 RVA: 0x000087B8 File Offset: 0x000069B8
		public TimelineClip clip
		{
			get
			{
				return this.m_Clip;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000254 RID: 596 RVA: 0x000087C0 File Offset: 0x000069C0
		public Playable mixer
		{
			get
			{
				return this.m_ParentMixer;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000255 RID: 597 RVA: 0x000087C8 File Offset: 0x000069C8
		public Playable playable
		{
			get
			{
				return this.m_Playable;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (set) Token: 0x06000256 RID: 598 RVA: 0x000087D0 File Offset: 0x000069D0
		public override bool enable
		{
			set
			{
				if (value && this.m_Playable.GetPlayState<Playable>() != PlayState.Playing)
				{
					this.m_Playable.Play<Playable>();
					this.SetTime(this.m_Clip.clipIn);
					return;
				}
				if (!value && this.m_Playable.GetPlayState<Playable>() != PlayState.Paused)
				{
					this.m_Playable.Pause<Playable>();
					if (this.m_ParentMixer.IsValid<Playable>())
					{
						this.m_ParentMixer.SetInputWeight(this.m_Playable, 0f);
					}
				}
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00008849 File Offset: 0x00006A49
		public void SetTime(double time)
		{
			this.m_Playable.SetTime(time);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00008857 File Offset: 0x00006A57
		public void SetDuration(double duration)
		{
			this.m_Playable.SetDuration(duration);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00008868 File Offset: 0x00006A68
		public override void EvaluateAt(double localTime, FrameData frameData)
		{
			this.enable = true;
			if (frameData.timeLooped)
			{
				this.SetTime(this.clip.clipIn);
				this.SetTime(this.clip.clipIn);
			}
			float weight;
			if (this.clip.IsPreExtrapolatedTime(localTime))
			{
				weight = this.clip.EvaluateMixIn((double)((float)this.clip.start));
			}
			else if (this.clip.IsPostExtrapolatedTime(localTime))
			{
				weight = this.clip.EvaluateMixOut((double)((float)this.clip.end));
			}
			else
			{
				weight = this.clip.EvaluateMixIn(localTime) * this.clip.EvaluateMixOut(localTime);
			}
			if (this.mixer.IsValid<Playable>())
			{
				this.mixer.SetInputWeight(this.playable, weight);
			}
			double clipTime = this.clip.ToLocalTime(localTime);
			if (clipTime >= -DiscreteTime.tickValue / 2.0)
			{
				this.SetTime(clipTime);
			}
			this.SetDuration(this.clip.extrapolatedDuration);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00008970 File Offset: 0x00006B70
		public override void DisableAt(double localTime, double rootDuration, FrameData frameData)
		{
			double time = Math.Min(localTime, (double)DiscreteTime.FromTicks(this.intervalEnd));
			if (frameData.timeLooped)
			{
				time = Math.Min(time, rootDuration);
			}
			double clipTime = this.clip.ToLocalTime(time);
			if (clipTime > -DiscreteTime.tickValue / 2.0)
			{
				this.SetTime(clipTime);
			}
			this.enable = false;
		}

		// Token: 0x0400011D RID: 285
		private TimelineClip m_Clip;

		// Token: 0x0400011E RID: 286
		private Playable m_Playable;

		// Token: 0x0400011F RID: 287
		private Playable m_ParentMixer;
	}
}
