using System;
using UnityEngine.Categorization;

namespace UnityEngine.Rendering
{
	// Token: 0x02000027 RID: 39
	[SupportedOnRenderPipeline(new Type[] { })]
	[CategoryInfo(Name = "R: GPU Resident Drawers", Order = 1000)]
	[HideInInspector]
	[Serializable]
	internal class GPUResidentDrawerResources : IRenderPipelineResources, IRenderPipelineGraphicsSettings
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00005819 File Offset: 0x00003A19
		int IRenderPipelineGraphicsSettings.version
		{
			get
			{
				return (int)this.m_Version;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00005821 File Offset: 0x00003A21
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00005829 File Offset: 0x00003A29
		public ComputeShader instanceDataBufferCopyKernels
		{
			get
			{
				return this.m_InstanceDataBufferCopyKernels;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_InstanceDataBufferCopyKernels, value, "m_InstanceDataBufferCopyKernels");
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000DC RID: 220 RVA: 0x0000583D File Offset: 0x00003A3D
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00005845 File Offset: 0x00003A45
		public ComputeShader instanceDataBufferUploadKernels
		{
			get
			{
				return this.m_InstanceDataBufferUploadKernels;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_InstanceDataBufferUploadKernels, value, "m_InstanceDataBufferUploadKernels");
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00005859 File Offset: 0x00003A59
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00005861 File Offset: 0x00003A61
		public ComputeShader transformUpdaterKernels
		{
			get
			{
				return this.m_TransformUpdaterKernels;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_TransformUpdaterKernels, value, "m_TransformUpdaterKernels");
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00005875 File Offset: 0x00003A75
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x0000587D File Offset: 0x00003A7D
		public ComputeShader windDataUpdaterKernels
		{
			get
			{
				return this.m_WindDataUpdaterKernels;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_WindDataUpdaterKernels, value, "m_WindDataUpdaterKernels");
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00005891 File Offset: 0x00003A91
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00005899 File Offset: 0x00003A99
		public ComputeShader occluderDepthPyramidKernels
		{
			get
			{
				return this.m_OccluderDepthPyramidKernels;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_OccluderDepthPyramidKernels, value, "m_OccluderDepthPyramidKernels");
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x000058AD File Offset: 0x00003AAD
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x000058B5 File Offset: 0x00003AB5
		public ComputeShader instanceOcclusionCullingKernels
		{
			get
			{
				return this.m_InstanceOcclusionCullingKernels;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_InstanceOcclusionCullingKernels, value, "m_InstanceOcclusionCullingKernels");
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x000058C9 File Offset: 0x00003AC9
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x000058D1 File Offset: 0x00003AD1
		public ComputeShader occlusionCullingDebugKernels
		{
			get
			{
				return this.m_OcclusionCullingDebugKernels;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_OcclusionCullingDebugKernels, value, "m_OcclusionCullingDebugKernels");
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x000058E5 File Offset: 0x00003AE5
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x000058ED File Offset: 0x00003AED
		public Shader debugOcclusionTestPS
		{
			get
			{
				return this.m_DebugOcclusionTestPS;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_DebugOcclusionTestPS, value, "m_DebugOcclusionTestPS");
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00005901 File Offset: 0x00003B01
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00005909 File Offset: 0x00003B09
		public Shader debugOccluderPS
		{
			get
			{
				return this.m_DebugOccluderPS;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_DebugOccluderPS, value, "m_DebugOccluderPS");
			}
		}

		// Token: 0x0400007B RID: 123
		[SerializeField]
		[HideInInspector]
		private GPUResidentDrawerResources.Version m_Version;

		// Token: 0x0400007C RID: 124
		[SerializeField]
		[ResourcePath("Runtime/RenderPipelineResources/GPUDriven/InstanceDataBufferCopyKernels.compute", SearchType.ProjectPath)]
		private ComputeShader m_InstanceDataBufferCopyKernels;

		// Token: 0x0400007D RID: 125
		[SerializeField]
		[ResourcePath("Runtime/RenderPipelineResources/GPUDriven/InstanceDataBufferUploadKernels.compute", SearchType.ProjectPath)]
		private ComputeShader m_InstanceDataBufferUploadKernels;

		// Token: 0x0400007E RID: 126
		[SerializeField]
		[ResourcePath("Runtime/RenderPipelineResources/GPUDriven/InstanceTransformUpdateKernels.compute", SearchType.ProjectPath)]
		private ComputeShader m_TransformUpdaterKernels;

		// Token: 0x0400007F RID: 127
		[SerializeField]
		[ResourcePath("Runtime/RenderPipelineResources/GPUDriven/InstanceWindDataUpdateKernels.compute", SearchType.ProjectPath)]
		public ComputeShader m_WindDataUpdaterKernels;

		// Token: 0x04000080 RID: 128
		[SerializeField]
		[ResourcePath("Runtime/RenderPipelineResources/GPUDriven/OccluderDepthPyramidKernels.compute", SearchType.ProjectPath)]
		private ComputeShader m_OccluderDepthPyramidKernels;

		// Token: 0x04000081 RID: 129
		[SerializeField]
		[ResourcePath("Runtime/RenderPipelineResources/GPUDriven/InstanceOcclusionCullingKernels.compute", SearchType.ProjectPath)]
		private ComputeShader m_InstanceOcclusionCullingKernels;

		// Token: 0x04000082 RID: 130
		[SerializeField]
		[ResourcePath("Runtime/RenderPipelineResources/GPUDriven/OcclusionCullingDebug.compute", SearchType.ProjectPath)]
		private ComputeShader m_OcclusionCullingDebugKernels;

		// Token: 0x04000083 RID: 131
		[SerializeField]
		[ResourcePath("Runtime/RenderPipelineResources/GPUDriven/DebugOcclusionTest.shader", SearchType.ProjectPath)]
		private Shader m_DebugOcclusionTestPS;

		// Token: 0x04000084 RID: 132
		[SerializeField]
		[ResourcePath("Runtime/RenderPipelineResources/GPUDriven/DebugOccluder.shader", SearchType.ProjectPath)]
		private Shader m_DebugOccluderPS;

		// Token: 0x02000028 RID: 40
		public enum Version
		{
			// Token: 0x04000086 RID: 134
			Initial,
			// Token: 0x04000087 RID: 135
			Count,
			// Token: 0x04000088 RID: 136
			Latest = 0
		}
	}
}
