using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;

namespace YgomSystem
{
	// Token: 0x020004B2 RID: 1202
	[Serializable]
	public class PostEffectPlayableAsset : PlayableAsset
	{
		// Token: 0x060026A7 RID: 9895 RVA: 0x000F1690 File Offset: 0x000EF890
		public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
		{
			return default(Playable);
		}

		// Token: 0x040027AB RID: 10155
		public ExposedReference<Volume> Volume;

		// Token: 0x040027AC RID: 10156
		public ExposedReference<VolumeProfile> VolumeProfile;
	}
}
