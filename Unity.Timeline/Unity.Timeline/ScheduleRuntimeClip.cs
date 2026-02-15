using System;
using UnityEngine.Audio;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000040 RID: 64
	internal class ScheduleRuntimeClip : RuntimeClipBase
	{
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000268 RID: 616 RVA: 0x00008A0E File Offset: 0x00006C0E
		public override double start
		{
			get
			{
				return Math.Max(0.0, this.m_Clip.start - this.m_StartDelay);
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000269 RID: 617 RVA: 0x00008A30 File Offset: 0x00006C30
		public override double duration
		{
			get
			{
				return this.m_Clip.duration + this.m_FinishTail + this.m_Clip.start - this.start;
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00008A57 File Offset: 0x00006C57
		public void SetTime(double time)
		{
			this.m_Playable.SetTime(time);
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600026B RID: 619 RVA: 0x00008A65 File Offset: 0x00006C65
		public TimelineClip clip
		{
			get
			{
				return this.m_Clip;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600026C RID: 620 RVA: 0x00008A6D File Offset: 0x00006C6D
		public Playable mixer
		{
			get
			{
				return this.m_ParentMixer;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600026D RID: 621 RVA: 0x00008A75 File Offset: 0x00006C75
		public Playable playable
		{
			get
			{
				return this.m_Playable;
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00008A7D File Offset: 0x00006C7D
		public ScheduleRuntimeClip(TimelineClip clip, Playable clipPlayable, Playable parentMixer, double startDelay = 0.2, double finishTail = 0.1)
		{
			this.Create(clip, clipPlayable, parentMixer, startDelay, finishTail);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00008A92 File Offset: 0x00006C92
		private void Create(TimelineClip clip, Playable clipPlayable, Playable parentMixer, double startDelay, double finishTail)
		{
			this.m_Clip = clip;
			this.m_Playable = clipPlayable;
			this.m_ParentMixer = parentMixer;
			this.m_StartDelay = startDelay;
			this.m_FinishTail = finishTail;
			clipPlayable.Pause<Playable>();
		}

		// Token: 0x170000B5 RID: 181
		// (set) Token: 0x06000270 RID: 624 RVA: 0x00008AC0 File Offset: 0x00006CC0
		public override bool enable
		{
			set
			{
				if (value && this.m_Playable.GetPlayState<Playable>() != PlayState.Playing)
				{
					this.m_Playable.Play<Playable>();
				}
				else if (!value && this.m_Playable.GetPlayState<Playable>() != PlayState.Paused)
				{
					this.m_Playable.Pause<Playable>();
					if (this.m_ParentMixer.IsValid<Playable>())
					{
						this.m_ParentMixer.SetInputWeight(this.m_Playable, 0f);
					}
				}
				this.m_Started = this.m_Started && value;
			}
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00008B38 File Offset: 0x00006D38
		public override void EvaluateAt(double localTime, FrameData frameData)
		{
			if (frameData.timeHeld)
			{
				this.enable = false;
				return;
			}
			bool forceSeek = frameData.seekOccurred || frameData.timeLooped || frameData.evaluationType == FrameData.EvaluationType.Evaluate;
			if (localTime > this.start + this.duration - this.m_FinishTail)
			{
				return;
			}
			float weight = this.clip.EvaluateMixIn(localTime) * this.clip.EvaluateMixOut(localTime);
			if (this.mixer.IsValid<Playable>())
			{
				this.mixer.SetInputWeight(this.playable, weight);
			}
			if (!this.m_Started || forceSeek)
			{
				double clipTime = this.clip.ToLocalTime(Math.Max(localTime, this.clip.start));
				double startDelay = Math.Max(this.clip.start - localTime, 0.0) * this.clip.timeScale;
				double durationLocal = this.m_Clip.duration * this.clip.timeScale;
				if (this.m_Playable.IsPlayableOfType<AudioClipPlayable>())
				{
					((AudioClipPlayable)this.m_Playable).Seek(clipTime, startDelay, durationLocal);
				}
				this.m_Started = true;
			}
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00008C61 File Offset: 0x00006E61
		public override void DisableAt(double localTime, double rootDuration, FrameData frameData)
		{
			this.enable = false;
		}

		// Token: 0x04000121 RID: 289
		private TimelineClip m_Clip;

		// Token: 0x04000122 RID: 290
		private Playable m_Playable;

		// Token: 0x04000123 RID: 291
		private Playable m_ParentMixer;

		// Token: 0x04000124 RID: 292
		private double m_StartDelay;

		// Token: 0x04000125 RID: 293
		private double m_FinishTail;

		// Token: 0x04000126 RID: 294
		private bool m_Started;
	}
}
