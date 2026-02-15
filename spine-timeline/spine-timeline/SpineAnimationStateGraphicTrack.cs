using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Spine.Unity.Playables
{
	// Token: 0x0200000C RID: 12
	[TrackColor(1f, 0.2509804f, 0.003921569f)]
	[TrackClipType(typeof(SpineAnimationStateClip))]
	[TrackBindingType(typeof(SkeletonGraphic))]
	public class SpineAnimationStateGraphicTrack : TrackAsset
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00002354 File Offset: 0x00000554
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

		// Token: 0x0400001D RID: 29
		public int trackIndex;

		// Token: 0x0400001E RID: 30
		[Tooltip("Whenever starting a new animation clip of this track, SkeletonGraphic.UnscaledTime will be set to this value. This allows you to play back Timeline clips either in normal game time or unscaled game time. Note that PlayableDirector.UpdateMethod is ignored and replaced by this property, which allows more fine-granular control per Timeline track.")]
		public bool unscaledTime;
	}
}
