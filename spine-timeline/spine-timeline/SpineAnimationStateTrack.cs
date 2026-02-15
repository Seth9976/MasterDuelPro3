using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Spine.Unity.Playables
{
	// Token: 0x0200000E RID: 14
	[TrackColor(1f, 0.2509804f, 0.003921569f)]
	[TrackClipType(typeof(SpineAnimationStateClip))]
	[TrackBindingType(typeof(SkeletonAnimation))]
	public class SpineAnimationStateTrack : TrackAsset
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00002B04 File Offset: 0x00000D04
		public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
		{
			foreach (TimelineClip clip in base.GetClips())
			{
				SpineAnimationStateClip animationStateClip = clip.asset as SpineAnimationStateClip;
				if (animationStateClip != null)
				{
					animationStateClip.timelineClip = clip;
				}
			}
			ScriptPlayable<SpineAnimationStateMixerBehaviour> scriptPlayable = ScriptPlayable<SpineAnimationStateMixerBehaviour>.Create(graph, inputCount);
			SpineAnimationStateMixerBehaviour behaviour = scriptPlayable.GetBehaviour();
			behaviour.trackIndex = this.trackIndex;
			behaviour.unscaledTime = this.unscaledTime;
			return scriptPlayable;
		}

		// Token: 0x0400002D RID: 45
		public int trackIndex;

		// Token: 0x0400002E RID: 46
		[Tooltip("Whenever starting a new animation clip of this track, SkeletonAnimation.UnscaledTime will be set to this value. This allows you to play back Timeline clips either in normal game time or unscaled game time. Note that PlayableDirector.UpdateMethod is ignored and replaced by this property, which allows more fine-granular control per Timeline track.")]
		public bool unscaledTime;
	}
}
