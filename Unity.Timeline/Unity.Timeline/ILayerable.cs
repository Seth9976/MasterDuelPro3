using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x0200004E RID: 78
	public interface ILayerable
	{
		// Token: 0x060002BA RID: 698
		Playable CreateLayerMixer(PlayableGraph graph, GameObject go, int inputCount);
	}
}
