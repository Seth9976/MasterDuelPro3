using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000BC RID: 188
	[ReloadGroup]
	[Obsolete("Moved to UniversalRenderPipelineRuntimeShaders on GraphicsSettings. #from(2023.3)", false)]
	[Serializable]
	public sealed class ShaderResources
	{
		// Token: 0x040003C2 RID: 962
		[Obsolete("Moved to UniversalRenderPipelineRuntimeShaders on GraphicsSettings. #from(2023.3)", false)]
		[Reload("Shaders/Utils/Blit.shader", ReloadAttribute.Package.Root)]
		public Shader blitPS;

		// Token: 0x040003C3 RID: 963
		[Reload("Shaders/Utils/CopyDepth.shader", ReloadAttribute.Package.Root)]
		[Obsolete("Moved to UniversalRenderPipelineRuntimeShaders on GraphicsSettings. #from(2023.3)", false)]
		public Shader copyDepthPS;

		// Token: 0x040003C4 RID: 964
		[Obsolete("Obsolete, this feature will be supported by new 'ScreenSpaceShadows' renderer feature", true)]
		public Shader screenSpaceShadowPS;

		// Token: 0x040003C5 RID: 965
		[Obsolete("Moved to UniversalRenderPipelineRuntimeShaders on GraphicsSettings. #from(2023.3)", false)]
		[Reload("Shaders/Utils/Sampling.shader", ReloadAttribute.Package.Root)]
		public Shader samplingPS;

		// Token: 0x040003C6 RID: 966
		[Reload("Shaders/Utils/StencilDeferred.shader", ReloadAttribute.Package.Root)]
		[Obsolete("Moved to UniversalRenderPipelineRuntimeShaders on GraphicsSettings. #from(2023.3)", false)]
		public Shader stencilDeferredPS;

		// Token: 0x040003C7 RID: 967
		[Reload("Shaders/Utils/FallbackError.shader", ReloadAttribute.Package.Root)]
		[Obsolete("Moved to UniversalRenderPipelineRuntimeShaders on GraphicsSettings. #from(2023.3)", false)]
		public Shader fallbackErrorPS;

		// Token: 0x040003C8 RID: 968
		[Reload("Shaders/Utils/FallbackLoading.shader", ReloadAttribute.Package.Root)]
		[Obsolete("Moved to UniversalRenderPipelineRuntimeShaders on GraphicsSettings. #from(2023.3)", false)]
		public Shader fallbackLoadingPS;

		// Token: 0x040003C9 RID: 969
		[Obsolete("Use fallbackErrorPS instead", true)]
		public Shader materialErrorPS;

		// Token: 0x040003CA RID: 970
		[Reload("Shaders/Utils/CoreBlit.shader", ReloadAttribute.Package.Root)]
		[SerializeField]
		[Obsolete("Moved to UniversalRenderPipelineRuntimeShaders on GraphicsSettings. #from(2023.3)", false)]
		internal Shader coreBlitPS;

		// Token: 0x040003CB RID: 971
		[Reload("Shaders/Utils/CoreBlitColorAndDepth.shader", ReloadAttribute.Package.Root)]
		[SerializeField]
		[Obsolete("Moved to UniversalRenderPipelineRuntimeShaders on GraphicsSettings. #from(2023.3)", false)]
		internal Shader coreBlitColorAndDepthPS;

		// Token: 0x040003CC RID: 972
		[Reload("Shaders/Utils/BlitHDROverlay.shader", ReloadAttribute.Package.Root)]
		[SerializeField]
		[Obsolete("Moved to UniversalRenderPipelineRuntimeShaders on GraphicsSettings. #from(2023.3)", false)]
		internal Shader blitHDROverlay;

		// Token: 0x040003CD RID: 973
		[Reload("Shaders/CameraMotionVectors.shader", ReloadAttribute.Package.Root)]
		[Obsolete("Moved to UniversalRenderPipelineRuntimeShaders on GraphicsSettings. #from(2023.3)", false)]
		public Shader cameraMotionVector;

		// Token: 0x040003CE RID: 974
		[Reload("Shaders/PostProcessing/LensFlareScreenSpace.shader", ReloadAttribute.Package.Root)]
		[Obsolete("Moved to UniversalRenderPipelineRuntimeShaders on GraphicsSettings. #from(2023.3)", false)]
		public Shader screenSpaceLensFlare;

		// Token: 0x040003CF RID: 975
		[Reload("Shaders/PostProcessing/LensFlareDataDriven.shader", ReloadAttribute.Package.Root)]
		[Obsolete("Moved to UniversalRenderPipelineRuntimeShaders on GraphicsSettings. #from(2023.3)", false)]
		public Shader dataDrivenLensFlare;
	}
}
