using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace YgomSystem.Timeline
{
	// Token: 0x020006AD RID: 1709
	[ExcludeFromPreset]
	[TrackClipType(typeof(LabelClip))]
	[Serializable]
	public class LabelTrack : TrackAsset
	{
		// Token: 0x06003572 RID: 13682 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreateClip(TimelineClip clip)
		{
		}

		// Token: 0x06003573 RID: 13683 RVA: 0x000F2FB0 File Offset: 0x000F11B0
		public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
		{
			ScriptPlayable<LoopMixerBehaviour> playable = ScriptPlayable<LoopMixerBehaviour>.Create(graph, inputCount);
			LoopMixerBehaviour behaviour = playable.GetBehaviour();
			behaviour.loopClips = new List<TimelineClip>();
			foreach (TimelineClip clip in base.GetClips())
			{
				LabelClipEX clipEx = clip.asset as LabelClipEX;
				if (clipEx != null && clipEx.wrapmode == LabelDirectorWrapMode.Loop)
				{
					behaviour.loopClips.Add(clip);
				}
			}
			behaviour.track = this;
			return playable;
		}
	}
}
