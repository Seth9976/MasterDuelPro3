using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace YgomSystem.Timeline
{
	// Token: 0x020006B2 RID: 1714
	[ExcludeFromPreset]
	[TrackClipType(typeof(LoopClip))]
	[Serializable]
	public class LoopTrack : TrackAsset
	{
		// Token: 0x060035A7 RID: 13735 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreateClip(TimelineClip clip)
		{
		}

		// Token: 0x060035A8 RID: 13736 RVA: 0x000F3360 File Offset: 0x000F1560
		public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
		{
			ScriptPlayable<LoopMixerBehaviour> playable = ScriptPlayable<LoopMixerBehaviour>.Create(graph, inputCount);
			LoopMixerBehaviour behaviour = playable.GetBehaviour();
			behaviour.loopClips = new List<TimelineClip>();
			foreach (TimelineClip clip in base.GetClips())
			{
				if (clip.asset is LoopClip)
				{
					behaviour.loopClips.Add(clip);
				}
			}
			return playable;
		}
	}
}
