using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Spine.Unity.Playables
{
	// Token: 0x02000010 RID: 16
	[TrackColor(0.855f, 0.8623f, 0.87f)]
	[TrackClipType(typeof(SpineSkeletonFlipClip))]
	[TrackBindingType(typeof(SpinePlayableHandleBase))]
	public class SpineSkeletonFlipTrack : TrackAsset
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00002DA2 File Offset: 0x00000FA2
		public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
		{
			return ScriptPlayable<SpineSkeletonFlipMixerBehaviour>.Create(graph, inputCount);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002DB0 File Offset: 0x00000FB0
		public override void GatherProperties(PlayableDirector director, IPropertyCollector driver)
		{
			base.GatherProperties(director, driver);
		}
	}
}
