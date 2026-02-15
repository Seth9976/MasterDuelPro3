using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

// Token: 0x02000009 RID: 9
[TrackClipType(typeof(CinemachineShot))]
[TrackBindingType(typeof(CinemachineBrain), TrackBindingFlags.None)]
[TrackColor(0.53f, 0f, 0.08f)]
[Serializable]
public class CinemachineTrack : TrackAsset
{
	// Token: 0x06000019 RID: 25 RVA: 0x0000264D File Offset: 0x0000084D
	public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
	{
		ScriptPlayable<CinemachineMixer> scriptPlayable = ScriptPlayable<CinemachineMixer>.Create(graph, 0);
		scriptPlayable.SetInputCount(inputCount);
		return scriptPlayable;
	}
}
