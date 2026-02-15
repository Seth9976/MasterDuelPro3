using System;

namespace UnityEngine.Playables
{
	// Token: 0x02000305 RID: 773
	public interface IPlayableAsset
	{
		// Token: 0x06001551 RID: 5457
		Playable CreatePlayable(PlayableGraph graph, GameObject owner);

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06001552 RID: 5458
		double duration { get; }
	}
}
