using System;
using UnityEngine.Categorization;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200015F RID: 351
	[SupportedOnRenderPipeline(typeof(UniversalRenderPipelineAsset))]
	[CategoryInfo(Name = "R: Universal Renderer Shaders", Order = 1000)]
	[HideInInspector]
	[Serializable]
	public class UniversalRendererResources : IRenderPipelineResources, IRenderPipelineGraphicsSettings
	{
		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600079D RID: 1949 RVA: 0x00024E45 File Offset: 0x00023045
		public int version
		{
			get
			{
				return this.m_Version;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x000039B4 File Offset: 0x00001BB4
		bool IRenderPipelineGraphicsSettings.isAvailableInPlayerBuild
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x0600079F RID: 1951 RVA: 0x00024E4D File Offset: 0x0002304D
		// (set) Token: 0x060007A0 RID: 1952 RVA: 0x00024E55 File Offset: 0x00023055
		public Shader copyDepthPS
		{
			get
			{
				return this.m_CopyDepthPS;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_CopyDepthPS, value, "m_CopyDepthPS");
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x00024E69 File Offset: 0x00023069
		// (set) Token: 0x060007A2 RID: 1954 RVA: 0x00024E71 File Offset: 0x00023071
		public Shader cameraMotionVector
		{
			get
			{
				return this.m_CameraMotionVector;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_CameraMotionVector, value, "m_CameraMotionVector");
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x00024E85 File Offset: 0x00023085
		// (set) Token: 0x060007A4 RID: 1956 RVA: 0x00024E8D File Offset: 0x0002308D
		public Shader stencilDeferredPS
		{
			get
			{
				return this.m_StencilDeferredPS;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_StencilDeferredPS, value, "m_StencilDeferredPS");
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060007A5 RID: 1957 RVA: 0x00024EA1 File Offset: 0x000230A1
		// (set) Token: 0x060007A6 RID: 1958 RVA: 0x00024EA9 File Offset: 0x000230A9
		public Shader decalDBufferClear
		{
			get
			{
				return this.m_DBufferClear;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_DBufferClear, value, "m_DBufferClear");
			}
		}

		// Token: 0x04000813 RID: 2067
		[SerializeField]
		[HideInInspector]
		private int m_Version;

		// Token: 0x04000814 RID: 2068
		[SerializeField]
		[ResourcePath("Shaders/Utils/CopyDepth.shader", SearchType.ProjectPath)]
		private Shader m_CopyDepthPS;

		// Token: 0x04000815 RID: 2069
		[SerializeField]
		[ResourcePath("Shaders/CameraMotionVectors.shader", SearchType.ProjectPath)]
		private Shader m_CameraMotionVector;

		// Token: 0x04000816 RID: 2070
		[SerializeField]
		[ResourcePath("Shaders/Utils/StencilDeferred.shader", SearchType.ProjectPath)]
		private Shader m_StencilDeferredPS;

		// Token: 0x04000817 RID: 2071
		[Header("Decal Renderer Feature Specific")]
		[SerializeField]
		[ResourcePath("Runtime/Decal/DBuffer/DBufferClear.shader", SearchType.ProjectPath)]
		private Shader m_DBufferClear;
	}
}
