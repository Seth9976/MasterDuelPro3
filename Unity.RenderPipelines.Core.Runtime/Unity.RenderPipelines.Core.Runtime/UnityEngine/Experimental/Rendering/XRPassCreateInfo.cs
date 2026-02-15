using System;
using UnityEngine.Rendering;
using UnityEngine.XR;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x02000011 RID: 17
	public struct XRPassCreateInfo
	{
		// Token: 0x04000048 RID: 72
		internal RenderTargetIdentifier renderTarget;

		// Token: 0x04000049 RID: 73
		internal RenderTextureDescriptor renderTargetDesc;

		// Token: 0x0400004A RID: 74
		internal RenderTargetIdentifier motionVectorRenderTarget;

		// Token: 0x0400004B RID: 75
		internal RenderTextureDescriptor motionVectorRenderTargetDesc;

		// Token: 0x0400004C RID: 76
		internal ScriptableCullingParameters cullingParameters;

		// Token: 0x0400004D RID: 77
		internal Material occlusionMeshMaterial;

		// Token: 0x0400004E RID: 78
		internal float occlusionMeshScale;

		// Token: 0x0400004F RID: 79
		internal IntPtr foveatedRenderingInfo;

		// Token: 0x04000050 RID: 80
		internal int multipassId;

		// Token: 0x04000051 RID: 81
		internal int cullingPassId;

		// Token: 0x04000052 RID: 82
		internal bool copyDepth;

		// Token: 0x04000053 RID: 83
		internal bool hasMotionVectorPass;

		// Token: 0x04000054 RID: 84
		internal XRDisplaySubsystem.XRRenderPass xrSdkRenderPass;
	}
}
