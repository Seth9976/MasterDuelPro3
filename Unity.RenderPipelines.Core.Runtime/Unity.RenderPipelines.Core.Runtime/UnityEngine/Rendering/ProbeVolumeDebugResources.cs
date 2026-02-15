using System;
using UnityEngine.Categorization;

namespace UnityEngine.Rendering
{
	// Token: 0x02000133 RID: 307
	[SupportedOnRenderPipeline(new Type[] { })]
	[CategoryInfo(Name = "R: Adaptive Probe Volumes", Order = 1000)]
	[HideInInspector]
	[Serializable]
	internal class ProbeVolumeDebugResources : IRenderPipelineResources, IRenderPipelineGraphicsSettings
	{
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060009D5 RID: 2517 RVA: 0x0001FF2B File Offset: 0x0001E12B
		public int version
		{
			get
			{
				return this.m_Version;
			}
		}

		// Token: 0x040005B3 RID: 1459
		[SerializeField]
		[HideInInspector]
		private int m_Version = 1;

		// Token: 0x040005B4 RID: 1460
		[Header("Debug")]
		[ResourcePath("Runtime/Debug/ProbeVolumeDebug.shader", SearchType.ProjectPath)]
		public Shader probeVolumeDebugShader;

		// Token: 0x040005B5 RID: 1461
		[ResourcePath("Runtime/Debug/ProbeVolumeFragmentationDebug.shader", SearchType.ProjectPath)]
		public Shader probeVolumeFragmentationDebugShader;

		// Token: 0x040005B6 RID: 1462
		[ResourcePath("Runtime/Debug/ProbeVolumeSamplingDebug.shader", SearchType.ProjectPath)]
		public Shader probeVolumeSamplingDebugShader;

		// Token: 0x040005B7 RID: 1463
		[ResourcePath("Runtime/Debug/ProbeVolumeOffsetDebug.shader", SearchType.ProjectPath)]
		public Shader probeVolumeOffsetDebugShader;

		// Token: 0x040005B8 RID: 1464
		[ResourcePath("Runtime/Debug/ProbeSamplingDebugMesh.fbx", SearchType.ProjectPath)]
		public Mesh probeSamplingDebugMesh;

		// Token: 0x040005B9 RID: 1465
		[ResourcePath("Runtime/Debug/ProbeVolumeNumbersDisplayTex.png", SearchType.ProjectPath)]
		public Texture2D numbersDisplayTex;
	}
}
