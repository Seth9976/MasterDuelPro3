using System;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering
{
	// Token: 0x0200013A RID: 314
	[Obsolete("This class is no longer necessary for APV implementation.")]
	[Serializable]
	public class ProbeVolumeSceneData
	{
		// Token: 0x060009ED RID: 2541 RVA: 0x0002059A File Offset: 0x0001E79A
		public ProbeVolumeSceneData(Object parentAsset)
		{
			this.SetParentObject(parentAsset);
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x000205A9 File Offset: 0x0001E7A9
		[Obsolete]
		public void SetParentObject(Object parent)
		{
			this.parentAsset = parent;
		}

		// Token: 0x040005D3 RID: 1491
		internal Object parentAsset;

		// Token: 0x040005D4 RID: 1492
		[SerializeField]
		[FormerlySerializedAs("sceneBounds")]
		[Obsolete("This data is now serialized directly in the baking set asset")]
		internal SerializedDictionary<string, Bounds> obsoleteSceneBounds;

		// Token: 0x040005D5 RID: 1493
		[SerializeField]
		[FormerlySerializedAs("hasProbeVolumes")]
		[Obsolete("This data is now serialized directly in the baking set asset")]
		internal SerializedDictionary<string, bool> obsoleteHasProbeVolumes;
	}
}
