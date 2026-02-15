using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000B6 RID: 182
	[Obsolete("Moved to UniversalRenderPipelineRuntimeXRResources on GraphicsSettings. #from(2023.3)", false)]
	[Serializable]
	public class XRSystemData : ScriptableObject
	{
		// Token: 0x040003A2 RID: 930
		[Obsolete("Moved to UniversalRenderPipelineRuntimeXRResources on GraphicsSettings. #from(2023.3)", false)]
		public XRSystemData.ShaderResources shaders;

		// Token: 0x020000B7 RID: 183
		[ReloadGroup]
		[Obsolete("Moved to UniversalRenderPipelineRuntimeXRResources on GraphicsSettings. #from(2023.3)", false)]
		[Serializable]
		public sealed class ShaderResources
		{
			// Token: 0x040003A3 RID: 931
			[Reload("Shaders/XR/XROcclusionMesh.shader", ReloadAttribute.Package.Root)]
			public Shader xrOcclusionMeshPS;

			// Token: 0x040003A4 RID: 932
			[Reload("Shaders/XR/XRMirrorView.shader", ReloadAttribute.Package.Root)]
			public Shader xrMirrorViewPS;
		}
	}
}
