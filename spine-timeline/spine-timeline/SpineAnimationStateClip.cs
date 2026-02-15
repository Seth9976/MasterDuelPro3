using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Spine.Unity.Playables
{
	// Token: 0x0200000B RID: 11
	[Serializable]
	public class SpineAnimationStateClip : PlayableAsset, ITimelineClipAsset
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002298 File Offset: 0x00000498
		public ClipCaps clipCaps
		{
			get
			{
				return ClipCaps.ClipIn | ClipCaps.SpeedMultiplier | ClipCaps.Blending | (this.template.loop ? ClipCaps.Looping : ClipCaps.None);
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000022B0 File Offset: 0x000004B0
		public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
		{
			this.template.timelineClip = this.timelineClip;
			ScriptPlayable<SpineAnimationStateBehaviour> playable = ScriptPlayable<SpineAnimationStateBehaviour>.Create(graph, this.template, 0);
			playable.GetBehaviour();
			return playable;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000022EC File Offset: 0x000004EC
		public override double duration
		{
			get
			{
				if (this.template.animationReference == null || this.template.animationReference.Animation == null)
				{
					return 0.0;
				}
				return (double)this.template.animationReference.Animation.Duration;
			}
		}

		// Token: 0x0400001B RID: 27
		public SpineAnimationStateBehaviour template = new SpineAnimationStateBehaviour();

		// Token: 0x0400001C RID: 28
		[NonSerialized]
		public TimelineClip timelineClip;
	}
}
