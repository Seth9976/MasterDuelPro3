using System;
using UnityEngine.Categorization;

namespace UnityEngine.Rendering
{
	// Token: 0x02000134 RID: 308
	[SupportedOnRenderPipeline(new Type[] { })]
	[CategoryInfo(Name = "R: Adaptive Probe Volumes", Order = 1000)]
	[HideInInspector]
	[Serializable]
	internal class ProbeVolumeBakingResources : IRenderPipelineResources, IRenderPipelineGraphicsSettings
	{
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060009D7 RID: 2519 RVA: 0x0001FF42 File Offset: 0x0001E142
		public int version
		{
			get
			{
				return this.m_Version;
			}
		}

		// Token: 0x040005BA RID: 1466
		[SerializeField]
		[HideInInspector]
		private int m_Version = 1;

		// Token: 0x040005BB RID: 1467
		[Header("Baking")]
		[ResourcePath("Editor/Lighting/ProbeVolume/ProbeVolumeCellDilation.compute", SearchType.ProjectPath)]
		public ComputeShader dilationShader;

		// Token: 0x040005BC RID: 1468
		[ResourcePath("Editor/Lighting/ProbeVolume/ProbeVolumeSubdivide.compute", SearchType.ProjectPath)]
		public ComputeShader subdivideSceneCS;

		// Token: 0x040005BD RID: 1469
		[ResourcePath("Editor/Lighting/ProbeVolume/VoxelizeScene.shader", SearchType.ProjectPath)]
		public Shader voxelizeSceneShader;

		// Token: 0x040005BE RID: 1470
		[ResourcePath("Editor/Lighting/ProbeVolume/VirtualOffset/TraceVirtualOffset.urtshader", SearchType.ProjectPath)]
		public ComputeShader traceVirtualOffsetCS;

		// Token: 0x040005BF RID: 1471
		[ResourcePath("Editor/Lighting/ProbeVolume/VirtualOffset/TraceVirtualOffset.urtshader", SearchType.ProjectPath)]
		public RayTracingShader traceVirtualOffsetRT;

		// Token: 0x040005C0 RID: 1472
		[ResourcePath("Editor/Lighting/ProbeVolume/DynamicGI/DynamicGISkyOcclusion.urtshader", SearchType.ProjectPath)]
		public ComputeShader skyOcclusionCS;

		// Token: 0x040005C1 RID: 1473
		[ResourcePath("Editor/Lighting/ProbeVolume/DynamicGI/DynamicGISkyOcclusion.urtshader", SearchType.ProjectPath)]
		public RayTracingShader skyOcclusionRT;

		// Token: 0x040005C2 RID: 1474
		[ResourcePath("Editor/Lighting/ProbeVolume/RenderingLayerMask/TraceRenderingLayerMask.urtshader", SearchType.ProjectPath)]
		public ComputeShader renderingLayerCS;

		// Token: 0x040005C3 RID: 1475
		[ResourcePath("Editor/Lighting/ProbeVolume/RenderingLayerMask/TraceRenderingLayerMask.urtshader", SearchType.ProjectPath)]
		public RayTracingShader renderingLayerRT;
	}
}
