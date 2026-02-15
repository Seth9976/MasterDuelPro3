using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

// Token: 0x02000003 RID: 3
[Serializable]
public class SpineSkeletonFlipClip : PlayableAsset, ITimelineClipAsset
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
	public ClipCaps clipCaps
	{
		get
		{
			return ClipCaps.None;
		}
	}

	// Token: 0x06000003 RID: 3 RVA: 0x0000205B File Offset: 0x0000025B
	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		return ScriptPlayable<SpineSkeletonFlipBehaviour>.Create(graph, this.template, 0);
	}

	// Token: 0x04000003 RID: 3
	public SpineSkeletonFlipBehaviour template = new SpineSkeletonFlipBehaviour();
}
