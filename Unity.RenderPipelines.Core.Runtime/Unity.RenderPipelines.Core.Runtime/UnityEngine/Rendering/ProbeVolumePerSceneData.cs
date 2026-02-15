using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering
{
	// Token: 0x02000136 RID: 310
	[ExecuteAlways]
	[AddComponentMenu("")]
	public class ProbeVolumePerSceneData : MonoBehaviour
	{
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060009DD RID: 2525 RVA: 0x0001FF8C File Offset: 0x0001E18C
		public ProbeVolumeBakingSet bakingSet
		{
			get
			{
				return this.serializedBakingSet;
			}
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0001FF94 File Offset: 0x0001E194
		internal void Clear()
		{
			this.QueueSceneRemoval();
			this.serializedBakingSet = null;
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0001FFA3 File Offset: 0x0001E1A3
		internal void QueueSceneLoading()
		{
			if (this.serializedBakingSet == null)
			{
				return;
			}
			ProbeReferenceVolume.instance.AddPendingSceneLoading(this.sceneGUID, this.serializedBakingSet);
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x0001FFCA File Offset: 0x0001E1CA
		internal void QueueSceneRemoval()
		{
			if (this.serializedBakingSet != null)
			{
				ProbeReferenceVolume.instance.AddPendingSceneRemoval(this.sceneGUID);
			}
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x0001FFEA File Offset: 0x0001E1EA
		private void OnEnable()
		{
			ProbeReferenceVolume.instance.RegisterPerSceneData(this);
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0001FFF7 File Offset: 0x0001E1F7
		private void OnDisable()
		{
			this.QueueSceneRemoval();
			ProbeReferenceVolume.instance.UnregisterPerSceneData(this);
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x00005704 File Offset: 0x00003904
		private void OnValidate()
		{
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x0002000A File Offset: 0x0001E20A
		internal void Initialize()
		{
			ProbeReferenceVolume.instance.RegisterBakingSet(this);
			this.QueueSceneRemoval();
			this.QueueSceneLoading();
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x00020023 File Offset: 0x0001E223
		internal bool ResolveCellData()
		{
			return this.serializedBakingSet != null && this.serializedBakingSet.ResolveCellData(this.serializedBakingSet.GetSceneCellIndexList(this.sceneGUID));
		}

		// Token: 0x040005C6 RID: 1478
		[SerializeField]
		[FormerlySerializedAs("bakingSet")]
		internal ProbeVolumeBakingSet serializedBakingSet;

		// Token: 0x040005C7 RID: 1479
		[SerializeField]
		internal string sceneGUID = "";

		// Token: 0x040005C8 RID: 1480
		[FormerlySerializedAs("asset")]
		[SerializeField]
		internal ObsoleteProbeVolumeAsset obsoleteAsset;

		// Token: 0x040005C9 RID: 1481
		[FormerlySerializedAs("cellSharedDataAsset")]
		[SerializeField]
		internal TextAsset obsoleteCellSharedDataAsset;

		// Token: 0x040005CA RID: 1482
		[FormerlySerializedAs("cellSupportDataAsset")]
		[SerializeField]
		internal TextAsset obsoleteCellSupportDataAsset;

		// Token: 0x040005CB RID: 1483
		[FormerlySerializedAs("serializedScenarios")]
		[SerializeField]
		private List<ProbeVolumePerSceneData.ObsoleteSerializablePerScenarioDataItem> obsoleteSerializedScenarios = new List<ProbeVolumePerSceneData.ObsoleteSerializablePerScenarioDataItem>();

		// Token: 0x02000137 RID: 311
		[Serializable]
		internal struct ObsoletePerScenarioData
		{
			// Token: 0x040005CC RID: 1484
			public int sceneHash;

			// Token: 0x040005CD RID: 1485
			public TextAsset cellDataAsset;

			// Token: 0x040005CE RID: 1486
			public TextAsset cellOptionalDataAsset;
		}

		// Token: 0x02000138 RID: 312
		[Serializable]
		private struct ObsoleteSerializablePerScenarioDataItem
		{
			// Token: 0x040005CF RID: 1487
			public string scenario;

			// Token: 0x040005D0 RID: 1488
			public ProbeVolumePerSceneData.ObsoletePerScenarioData data;
		}
	}
}
