using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000C0 RID: 192
	[Obsolete("ForwardRendererData has been deprecated (UnityUpgradable) -> UniversalRendererData", true)]
	[ReloadGroup]
	[ExcludeFromPreset]
	[Serializable]
	public class ForwardRendererData : ScriptableRendererData
	{
		// Token: 0x0600049E RID: 1182 RVA: 0x00012497 File Offset: 0x00010697
		protected override ScriptableRenderer Create()
		{
			Debug.LogWarning("Forward Renderer Data has been deprecated, " + base.name + " will be upgraded to a UniversalRendererData.");
			return null;
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x000124B4 File Offset: 0x000106B4
		// (set) Token: 0x060004A0 RID: 1184 RVA: 0x000124B4 File Offset: 0x000106B4
		public LayerMask opaqueLayerMask
		{
			get
			{
				throw new NotSupportedException("ForwardRendererData has been deprecated. Use UniversalRendererData instead");
			}
			set
			{
				throw new NotSupportedException("ForwardRendererData has been deprecated. Use UniversalRendererData instead");
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x000124B4 File Offset: 0x000106B4
		// (set) Token: 0x060004A2 RID: 1186 RVA: 0x000124B4 File Offset: 0x000106B4
		public LayerMask transparentLayerMask
		{
			get
			{
				throw new NotSupportedException("ForwardRendererData has been deprecated. Use UniversalRendererData instead");
			}
			set
			{
				throw new NotSupportedException("ForwardRendererData has been deprecated. Use UniversalRendererData instead");
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x000124B4 File Offset: 0x000106B4
		// (set) Token: 0x060004A4 RID: 1188 RVA: 0x000124B4 File Offset: 0x000106B4
		public StencilStateData defaultStencilState
		{
			get
			{
				throw new NotSupportedException("ForwardRendererData has been deprecated. Use UniversalRendererData instead");
			}
			set
			{
				throw new NotSupportedException("ForwardRendererData has been deprecated. Use UniversalRendererData instead");
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x000124B4 File Offset: 0x000106B4
		// (set) Token: 0x060004A6 RID: 1190 RVA: 0x000124B4 File Offset: 0x000106B4
		public bool shadowTransparentReceive
		{
			get
			{
				throw new NotSupportedException("ForwardRendererData has been deprecated. Use UniversalRendererData instead");
			}
			set
			{
				throw new NotSupportedException("ForwardRendererData has been deprecated. Use UniversalRendererData instead");
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x000124B4 File Offset: 0x000106B4
		// (set) Token: 0x060004A8 RID: 1192 RVA: 0x000124B4 File Offset: 0x000106B4
		public RenderingMode renderingMode
		{
			get
			{
				throw new NotSupportedException("ForwardRendererData has been deprecated. Use UniversalRendererData instead");
			}
			set
			{
				throw new NotSupportedException("ForwardRendererData has been deprecated. Use UniversalRendererData instead");
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x000124B4 File Offset: 0x000106B4
		// (set) Token: 0x060004AA RID: 1194 RVA: 0x000124B4 File Offset: 0x000106B4
		public bool accurateGbufferNormals
		{
			get
			{
				throw new NotSupportedException("ForwardRendererData has been deprecated. Use UniversalRendererData instead");
			}
			set
			{
				throw new NotSupportedException("ForwardRendererData has been deprecated. Use UniversalRendererData instead");
			}
		}

		// Token: 0x040003EB RID: 1003
		private const string k_ErrorMessage = "ForwardRendererData has been deprecated. Use UniversalRendererData instead";

		// Token: 0x040003EC RID: 1004
		public ForwardRendererData.ShaderResources shaders;

		// Token: 0x040003ED RID: 1005
		public PostProcessData postProcessData;

		// Token: 0x040003EE RID: 1006
		public XRSystemData xrSystemData;

		// Token: 0x040003EF RID: 1007
		[SerializeField]
		private LayerMask m_OpaqueLayerMask;

		// Token: 0x040003F0 RID: 1008
		[SerializeField]
		private LayerMask m_TransparentLayerMask;

		// Token: 0x040003F1 RID: 1009
		[SerializeField]
		private StencilStateData m_DefaultStencilState;

		// Token: 0x040003F2 RID: 1010
		[SerializeField]
		private bool m_ShadowTransparentReceive;

		// Token: 0x040003F3 RID: 1011
		[SerializeField]
		private RenderingMode m_RenderingMode;

		// Token: 0x040003F4 RID: 1012
		[SerializeField]
		private DepthPrimingMode m_DepthPrimingMode;

		// Token: 0x040003F5 RID: 1013
		[SerializeField]
		private bool m_AccurateGbufferNormals;

		// Token: 0x040003F6 RID: 1014
		[SerializeField]
		private bool m_ClusteredRendering;

		// Token: 0x040003F7 RID: 1015
		[SerializeField]
		private TileSize m_TileSize;

		// Token: 0x020000C1 RID: 193
		[ReloadGroup]
		[Serializable]
		public sealed class ShaderResources
		{
			// Token: 0x040003F8 RID: 1016
			[Reload("Shaders/Utils/Blit.shader", ReloadAttribute.Package.Root)]
			public Shader blitPS;

			// Token: 0x040003F9 RID: 1017
			[Reload("Shaders/Utils/CopyDepth.shader", ReloadAttribute.Package.Root)]
			public Shader copyDepthPS;

			// Token: 0x040003FA RID: 1018
			[Obsolete("Obsolete, this feature will be supported by new 'ScreenSpaceShadows' renderer feature", true)]
			public Shader screenSpaceShadowPS;

			// Token: 0x040003FB RID: 1019
			[Reload("Shaders/Utils/Sampling.shader", ReloadAttribute.Package.Root)]
			public Shader samplingPS;

			// Token: 0x040003FC RID: 1020
			[Reload("Shaders/Utils/StencilDeferred.shader", ReloadAttribute.Package.Root)]
			public Shader stencilDeferredPS;

			// Token: 0x040003FD RID: 1021
			[Reload("Shaders/Utils/FallbackError.shader", ReloadAttribute.Package.Root)]
			public Shader fallbackErrorPS;

			// Token: 0x040003FE RID: 1022
			[Reload("Shaders/Utils/FallbackLoading.shader", ReloadAttribute.Package.Root)]
			public Shader fallbackLoadingPS;

			// Token: 0x040003FF RID: 1023
			[Obsolete("Use fallbackErrorPS instead", true)]
			[Reload("Shaders/Utils/MaterialError.shader", ReloadAttribute.Package.Root)]
			public Shader materialErrorPS;

			// Token: 0x04000400 RID: 1024
			[Reload("Shaders/Utils/CoreBlit.shader", ReloadAttribute.Package.Root)]
			[SerializeField]
			internal Shader coreBlitPS;

			// Token: 0x04000401 RID: 1025
			[Reload("Shaders/Utils/CoreBlitColorAndDepth.shader", ReloadAttribute.Package.Root)]
			[SerializeField]
			internal Shader coreBlitColorAndDepthPS;

			// Token: 0x04000402 RID: 1026
			[Reload("Shaders/CameraMotionVectors.shader", ReloadAttribute.Package.Root)]
			public Shader cameraMotionVector;

			// Token: 0x04000403 RID: 1027
			[Reload("Shaders/ObjectMotionVectors.shader", ReloadAttribute.Package.Root)]
			public Shader objectMotionVector;
		}
	}
}
