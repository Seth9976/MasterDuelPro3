using System;
using UnityEngine.Categorization;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200015E RID: 350
	[SupportedOnRenderPipeline(typeof(UniversalRenderPipelineAsset))]
	[CategoryInfo(Name = "R: Runtime XR", Order = 1000)]
	[HideInInspector]
	[Serializable]
	public class UniversalRenderPipelineRuntimeXRResources : IRenderPipelineResources, IRenderPipelineGraphicsSettings
	{
		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000793 RID: 1939 RVA: 0x00002886 File Offset: 0x00000A86
		public int version
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x000039B4 File Offset: 0x00001BB4
		bool IRenderPipelineGraphicsSettings.isAvailableInPlayerBuild
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000795 RID: 1941 RVA: 0x00024DBE File Offset: 0x00022FBE
		// (set) Token: 0x06000796 RID: 1942 RVA: 0x00024DC6 File Offset: 0x00022FC6
		public Shader xrOcclusionMeshPS
		{
			get
			{
				return this.m_xrOcclusionMeshPS;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_xrOcclusionMeshPS, value, "m_xrOcclusionMeshPS");
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000797 RID: 1943 RVA: 0x00024DDA File Offset: 0x00022FDA
		// (set) Token: 0x06000798 RID: 1944 RVA: 0x00024DE2 File Offset: 0x00022FE2
		public Shader xrMirrorViewPS
		{
			get
			{
				return this.m_xrMirrorViewPS;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_xrMirrorViewPS, value, "m_xrMirrorViewPS");
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000799 RID: 1945 RVA: 0x00024DF6 File Offset: 0x00022FF6
		// (set) Token: 0x0600079A RID: 1946 RVA: 0x00024DFE File Offset: 0x00022FFE
		public Shader xrMotionVector
		{
			get
			{
				return this.m_xrMotionVector;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_xrMotionVector, value, "m_xrMotionVector");
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600079B RID: 1947 RVA: 0x00024E12 File Offset: 0x00023012
		internal bool valid
		{
			get
			{
				return !(this.xrOcclusionMeshPS == null) && !(this.xrMirrorViewPS == null) && !(this.m_xrMotionVector == null);
			}
		}

		// Token: 0x04000810 RID: 2064
		[SerializeField]
		[ResourcePath("Shaders/XR/XROcclusionMesh.shader", SearchType.ProjectPath)]
		private Shader m_xrOcclusionMeshPS;

		// Token: 0x04000811 RID: 2065
		[SerializeField]
		[ResourcePath("Shaders/XR/XRMirrorView.shader", SearchType.ProjectPath)]
		public Shader m_xrMirrorViewPS;

		// Token: 0x04000812 RID: 2066
		[SerializeField]
		[ResourcePath("Shaders/XR/XRMotionVector.shader", SearchType.ProjectPath)]
		public Shader m_xrMotionVector;
	}
}
