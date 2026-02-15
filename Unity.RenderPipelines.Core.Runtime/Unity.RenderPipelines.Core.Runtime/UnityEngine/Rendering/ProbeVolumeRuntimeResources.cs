using System;
using UnityEngine.Categorization;

namespace UnityEngine.Rendering
{
	// Token: 0x02000132 RID: 306
	[SupportedOnRenderPipeline(new Type[] { })]
	[CategoryInfo(Name = "R: Adaptive Probe Volumes", Order = 1000)]
	[HideInInspector]
	[Serializable]
	internal class ProbeVolumeRuntimeResources : IRenderPipelineResources, IRenderPipelineGraphicsSettings
	{
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x0001FF14 File Offset: 0x0001E114
		public int version
		{
			get
			{
				return this.m_Version;
			}
		}

		// Token: 0x040005AF RID: 1455
		[SerializeField]
		[HideInInspector]
		private int m_Version = 1;

		// Token: 0x040005B0 RID: 1456
		[Header("Runtime")]
		[ResourcePath("Runtime/Lighting/ProbeVolume/ProbeVolumeBlendStates.compute", SearchType.ProjectPath)]
		public ComputeShader probeVolumeBlendStatesCS;

		// Token: 0x040005B1 RID: 1457
		[ResourcePath("Runtime/Lighting/ProbeVolume/ProbeVolumeUploadData.compute", SearchType.ProjectPath)]
		public ComputeShader probeVolumeUploadDataCS;

		// Token: 0x040005B2 RID: 1458
		[ResourcePath("Runtime/Lighting/ProbeVolume/ProbeVolumeUploadDataL2.compute", SearchType.ProjectPath)]
		public ComputeShader probeVolumeUploadDataL2CS;
	}
}
