using System;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000C3 RID: 195
	public class UniversalCameraData : ContextItem
	{
		// Token: 0x060004BF RID: 1215 RVA: 0x000126C3 File Offset: 0x000108C3
		internal void SetViewAndProjectionMatrix(Matrix4x4 viewMatrix, Matrix4x4 projectionMatrix)
		{
			this.m_ViewMatrix = viewMatrix;
			this.m_ProjectionMatrix = projectionMatrix;
			this.m_JitterMatrix = Matrix4x4.identity;
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x000126DE File Offset: 0x000108DE
		internal void SetViewProjectionAndJitterMatrix(Matrix4x4 viewMatrix, Matrix4x4 projectionMatrix, Matrix4x4 jitterMatrix)
		{
			this.m_ViewMatrix = viewMatrix;
			this.m_ProjectionMatrix = projectionMatrix;
			this.m_JitterMatrix = jitterMatrix;
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x000126F8 File Offset: 0x000108F8
		internal void PushBuiltinShaderConstantsXR(RasterCommandBuffer cmd, bool renderIntoTexture)
		{
			if ((!this.m_InitBuiltinXRConstants || this.m_CachedRenderIntoTextureXR != renderIntoTexture || !this.xr.singlePassEnabled) && this.xr.enabled)
			{
				Matrix4x4 projection0 = this.GetProjectionMatrix(0);
				Matrix4x4 view0 = this.GetViewMatrix(0);
				cmd.SetViewProjectionMatrices(view0, projection0);
				if (this.xr.singlePassEnabled)
				{
					Matrix4x4 projection = this.GetProjectionMatrix(1);
					Matrix4x4 viewMatrix = this.GetViewMatrix(1);
					XRBuiltinShaderConstants.UpdateBuiltinShaderConstants(view0, projection0, renderIntoTexture, 0);
					XRBuiltinShaderConstants.UpdateBuiltinShaderConstants(viewMatrix, projection, renderIntoTexture, 1);
					XRBuiltinShaderConstants.SetBuiltinShaderConstants(cmd);
				}
				else
				{
					Vector3 worldSpaceCameraPos = Matrix4x4.Inverse(this.GetViewMatrix(0)).GetColumn(3);
					cmd.SetGlobalVector(ShaderPropertyId.worldSpaceCameraPos, worldSpaceCameraPos);
				}
				this.m_CachedRenderIntoTextureXR = renderIntoTexture;
				this.m_InitBuiltinXRConstants = true;
			}
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x000127C4 File Offset: 0x000109C4
		public Matrix4x4 GetViewMatrix(int viewIndex = 0)
		{
			if (this.xr.enabled)
			{
				return this.xr.GetViewMatrix(viewIndex);
			}
			return this.m_ViewMatrix;
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x000127E6 File Offset: 0x000109E6
		public Matrix4x4 GetProjectionMatrix(int viewIndex = 0)
		{
			if (this.xr.enabled)
			{
				return this.m_JitterMatrix * this.xr.GetProjMatrix(viewIndex);
			}
			return this.m_JitterMatrix * this.m_ProjectionMatrix;
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0001281E File Offset: 0x00010A1E
		internal Matrix4x4 GetProjectionMatrixNoJitter(int viewIndex = 0)
		{
			if (this.xr.enabled)
			{
				return this.xr.GetProjMatrix(viewIndex);
			}
			return this.m_ProjectionMatrix;
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00012840 File Offset: 0x00010A40
		public Matrix4x4 GetGPUProjectionMatrix(int viewIndex = 0)
		{
			return this.m_JitterMatrix * GL.GetGPUProjectionMatrix(this.GetProjectionMatrixNoJitter(viewIndex), this.IsCameraProjectionMatrixFlipped());
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0001285F File Offset: 0x00010A5F
		public Matrix4x4 GetGPUProjectionMatrixNoJitter(int viewIndex = 0)
		{
			return GL.GetGPUProjectionMatrix(this.GetProjectionMatrixNoJitter(viewIndex), this.IsCameraProjectionMatrixFlipped());
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00012873 File Offset: 0x00010A73
		internal Matrix4x4 GetGPUProjectionMatrix(bool renderIntoTexture, int viewIndex = 0)
		{
			return this.m_JitterMatrix * GL.GetGPUProjectionMatrix(this.GetProjectionMatrix(viewIndex), renderIntoTexture);
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x0001288D File Offset: 0x00010A8D
		public int scaledWidth
		{
			get
			{
				return Mathf.Max(1, (int)((float)this.camera.pixelWidth * this.renderScale));
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x000128A9 File Offset: 0x00010AA9
		public int scaledHeight
		{
			get
			{
				return Mathf.Max(1, (int)((float)this.camera.pixelHeight * this.renderScale));
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x000128C5 File Offset: 0x00010AC5
		// (set) Token: 0x060004CB RID: 1227 RVA: 0x000128CD File Offset: 0x00010ACD
		public UniversalCameraHistory historyManager
		{
			get
			{
				return this.m_HistoryManager;
			}
			set
			{
				this.m_HistoryManager = value;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x000128D8 File Offset: 0x00010AD8
		internal bool requireSrgbConversion
		{
			get
			{
				if (this.xr.enabled)
				{
					return !this.xr.renderTargetDesc.sRGB && (this.xr.renderTargetDesc.graphicsFormat == GraphicsFormat.R8G8B8A8_UNorm || this.xr.renderTargetDesc.graphicsFormat == GraphicsFormat.B8G8R8A8_UNorm) && QualitySettings.activeColorSpace == ColorSpace.Linear;
				}
				return this.targetTexture == null && Display.main.requiresSrgbBlitToBackbuffer;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x00012959 File Offset: 0x00010B59
		public bool isGameCamera
		{
			get
			{
				return this.cameraType == CameraType.Game;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x00012964 File Offset: 0x00010B64
		public bool isSceneViewCamera
		{
			get
			{
				return this.cameraType == CameraType.SceneView;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x0001296F File Offset: 0x00010B6F
		public bool isPreviewCamera
		{
			get
			{
				return this.cameraType == CameraType.Preview;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x0001297A File Offset: 0x00010B7A
		internal bool isRenderPassSupportedCamera
		{
			get
			{
				return this.cameraType == CameraType.Game || this.cameraType == CameraType.Reflection;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x00012991 File Offset: 0x00010B91
		internal bool resolveToScreen
		{
			get
			{
				return this.targetTexture == null && this.resolveFinalTarget && (this.cameraType == CameraType.Game || this.camera.cameraType == CameraType.VR);
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x000129C4 File Offset: 0x00010BC4
		public bool isHDROutputActive
		{
			get
			{
				bool hdrDisplayOutputActive = UniversalRenderPipeline.HDROutputForMainDisplayIsActive();
				if (this.xr.enabled)
				{
					hdrDisplayOutputActive = this.xr.isHDRDisplayOutputActive;
				}
				return hdrDisplayOutputActive && this.allowHDROutput && this.resolveToScreen;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x00012A04 File Offset: 0x00010C04
		public HDROutputUtils.HDRDisplayInformation hdrDisplayInformation
		{
			get
			{
				HDROutputUtils.HDRDisplayInformation displayInformation;
				if (this.xr.enabled)
				{
					displayInformation = this.xr.hdrDisplayOutputInformation;
				}
				else
				{
					HDROutputSettings displaySettings = HDROutputSettings.main;
					displayInformation = new HDROutputUtils.HDRDisplayInformation(displaySettings.maxFullFrameToneMapLuminance, displaySettings.maxToneMapLuminance, displaySettings.minToneMapLuminance, displaySettings.paperWhiteNits);
				}
				return displayInformation;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x00012A52 File Offset: 0x00010C52
		public ColorGamut hdrDisplayColorGamut
		{
			get
			{
				if (this.xr.enabled)
				{
					return this.xr.hdrDisplayOutputColorGamut;
				}
				return HDROutputSettings.main.displayColorGamut;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x00012A77 File Offset: 0x00010C77
		public bool rendersOverlayUI
		{
			get
			{
				return SupportedRenderingFeatures.active.rendersUIOverlay && this.resolveToScreen;
			}
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00012A90 File Offset: 0x00010C90
		public bool IsHandleYFlipped(RTHandle handle)
		{
			if (!SystemInfo.graphicsUVStartsAtTop)
			{
				return true;
			}
			if (this.cameraType == CameraType.SceneView || this.cameraType == CameraType.Preview)
			{
				return true;
			}
			RenderTargetIdentifier handleID = new RenderTargetIdentifier(handle.nameID, 0, CubemapFace.Unknown, 0);
			bool isBackbuffer = handleID == BuiltinRenderTextureType.CameraTarget || handleID == BuiltinRenderTextureType.Depth;
			if (this.xr.enabled)
			{
				isBackbuffer |= handleID == new RenderTargetIdentifier(this.xr.renderTarget, 0, CubemapFace.Unknown, 0);
			}
			return !isBackbuffer;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00012B18 File Offset: 0x00010D18
		public bool IsCameraProjectionMatrixFlipped()
		{
			if (!SystemInfo.graphicsUVStartsAtTop)
			{
				return false;
			}
			ScriptableRenderer renderer = ScriptableRenderer.current;
			return renderer == null || this.IsHandleYFlipped(renderer.cameraColorTargetHandle) || this.targetTexture != null;
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00012B55 File Offset: 0x00010D55
		public bool IsRenderTargetProjectionMatrixFlipped(RTHandle color, RTHandle depth = null)
		{
			return !SystemInfo.graphicsUVStartsAtTop || this.targetTexture != null || this.IsHandleYFlipped(color ?? depth);
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00012B7C File Offset: 0x00010D7C
		internal bool IsTemporalAAEnabled()
		{
			UniversalAdditionalCameraData additionalCameraData;
			this.camera.TryGetComponent<UniversalAdditionalCameraData>(out additionalCameraData);
			return this.antialiasing == AntialiasingMode.TemporalAntiAliasing && this.postProcessEnabled && this.taaHistory != null && this.cameraTargetDescriptor.msaaSamples == 1 && (additionalCameraData == null || additionalCameraData.renderType != CameraRenderType.Overlay) && (additionalCameraData == null || additionalCameraData.cameraStack.Count <= 0) && !this.camera.allowDynamicResolution && this.renderer.SupportsMotionVectors();
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00012BF5 File Offset: 0x00010DF5
		internal bool IsSTPEnabled()
		{
			return this.imageScalingMode == ImageScalingMode.Upscaling && this.upscalingFilter == ImageUpscalingFilter.STP;
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x00012C0B File Offset: 0x00010E0B
		// (set) Token: 0x060004DC RID: 1244 RVA: 0x00012C13 File Offset: 0x00010E13
		public XRPass xr { get; internal set; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x00012C1C File Offset: 0x00010E1C
		internal XRPassUniversal xrUniversal
		{
			get
			{
				return this.xr as XRPassUniversal;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x00012C29 File Offset: 0x00010E29
		internal bool resetHistory
		{
			get
			{
				return this.taaSettings.resetHistoryFrames != 0;
			}
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00012C3C File Offset: 0x00010E3C
		public override void Reset()
		{
			this.m_ViewMatrix = default(Matrix4x4);
			this.m_ProjectionMatrix = default(Matrix4x4);
			this.m_JitterMatrix = default(Matrix4x4);
			this.m_CachedRenderIntoTextureXR = false;
			this.m_InitBuiltinXRConstants = false;
			this.camera = null;
			this.renderType = CameraRenderType.Base;
			this.targetTexture = null;
			this.cameraTargetDescriptor = default(RenderTextureDescriptor);
			this.pixelRect = default(Rect);
			this.useScreenCoordOverride = false;
			this.screenSizeOverride = default(Vector4);
			this.screenCoordScaleBias = default(Vector4);
			this.pixelWidth = 0;
			this.pixelHeight = 0;
			this.aspectRatio = 0f;
			this.renderScale = 1f;
			this.imageScalingMode = ImageScalingMode.None;
			this.upscalingFilter = ImageUpscalingFilter.Point;
			this.fsrOverrideSharpness = false;
			this.fsrSharpness = 0f;
			this.hdrColorBufferPrecision = HDRColorBufferPrecision._32Bits;
			this.clearDepth = false;
			this.cameraType = CameraType.Game;
			this.isDefaultViewport = false;
			this.isHdrEnabled = false;
			this.allowHDROutput = false;
			this.isAlphaOutputEnabled = false;
			this.requiresDepthTexture = false;
			this.requiresOpaqueTexture = false;
			this.postProcessingRequiresDepthTexture = false;
			this.xrRendering = false;
			this.useGPUOcclusionCulling = false;
			this.defaultOpaqueSortFlags = SortingCriteria.None;
			this.xr = null;
			this.maxShadowDistance = 0f;
			this.postProcessEnabled = false;
			this.captureActions = null;
			this.volumeLayerMask = 0;
			this.volumeTrigger = null;
			this.isStopNaNEnabled = false;
			this.isDitheringEnabled = false;
			this.antialiasing = AntialiasingMode.None;
			this.antialiasingQuality = AntialiasingQuality.Low;
			this.renderer = null;
			this.resolveFinalTarget = false;
			this.worldSpaceCameraPos = default(Vector3);
			this.backgroundColor = Color.black;
			this.taaHistory = null;
			this.stpHistory = null;
			this.taaSettings = default(TemporalAA.Settings);
			this.baseCamera = null;
			this.stackAnyPostProcessingEnabled = false;
			this.stackLastCameraOutputToHDR = false;
		}

		// Token: 0x0400040B RID: 1035
		private Matrix4x4 m_ViewMatrix;

		// Token: 0x0400040C RID: 1036
		private Matrix4x4 m_ProjectionMatrix;

		// Token: 0x0400040D RID: 1037
		private Matrix4x4 m_JitterMatrix;

		// Token: 0x0400040E RID: 1038
		private bool m_CachedRenderIntoTextureXR;

		// Token: 0x0400040F RID: 1039
		private bool m_InitBuiltinXRConstants;

		// Token: 0x04000410 RID: 1040
		public Camera camera;

		// Token: 0x04000411 RID: 1041
		internal UniversalCameraHistory m_HistoryManager;

		// Token: 0x04000412 RID: 1042
		public CameraRenderType renderType;

		// Token: 0x04000413 RID: 1043
		public RenderTexture targetTexture;

		// Token: 0x04000414 RID: 1044
		public RenderTextureDescriptor cameraTargetDescriptor;

		// Token: 0x04000415 RID: 1045
		internal Rect pixelRect;

		// Token: 0x04000416 RID: 1046
		internal bool useScreenCoordOverride;

		// Token: 0x04000417 RID: 1047
		internal Vector4 screenSizeOverride;

		// Token: 0x04000418 RID: 1048
		internal Vector4 screenCoordScaleBias;

		// Token: 0x04000419 RID: 1049
		internal int pixelWidth;

		// Token: 0x0400041A RID: 1050
		internal int pixelHeight;

		// Token: 0x0400041B RID: 1051
		internal float aspectRatio;

		// Token: 0x0400041C RID: 1052
		public float renderScale;

		// Token: 0x0400041D RID: 1053
		internal ImageScalingMode imageScalingMode;

		// Token: 0x0400041E RID: 1054
		internal ImageUpscalingFilter upscalingFilter;

		// Token: 0x0400041F RID: 1055
		internal bool fsrOverrideSharpness;

		// Token: 0x04000420 RID: 1056
		internal float fsrSharpness;

		// Token: 0x04000421 RID: 1057
		internal HDRColorBufferPrecision hdrColorBufferPrecision;

		// Token: 0x04000422 RID: 1058
		public bool clearDepth;

		// Token: 0x04000423 RID: 1059
		public CameraType cameraType;

		// Token: 0x04000424 RID: 1060
		public bool isDefaultViewport;

		// Token: 0x04000425 RID: 1061
		public bool isHdrEnabled;

		// Token: 0x04000426 RID: 1062
		public bool allowHDROutput;

		// Token: 0x04000427 RID: 1063
		public bool isAlphaOutputEnabled;

		// Token: 0x04000428 RID: 1064
		public bool requiresDepthTexture;

		// Token: 0x04000429 RID: 1065
		public bool requiresOpaqueTexture;

		// Token: 0x0400042A RID: 1066
		public bool postProcessingRequiresDepthTexture;

		// Token: 0x0400042B RID: 1067
		public bool xrRendering;

		// Token: 0x0400042C RID: 1068
		internal bool useGPUOcclusionCulling;

		// Token: 0x0400042D RID: 1069
		internal bool stackLastCameraOutputToHDR;

		// Token: 0x0400042E RID: 1070
		public SortingCriteria defaultOpaqueSortFlags;

		// Token: 0x04000430 RID: 1072
		public float maxShadowDistance;

		// Token: 0x04000431 RID: 1073
		public bool postProcessEnabled;

		// Token: 0x04000432 RID: 1074
		internal bool stackAnyPostProcessingEnabled;

		// Token: 0x04000433 RID: 1075
		public IEnumerator<Action<RenderTargetIdentifier, CommandBuffer>> captureActions;

		// Token: 0x04000434 RID: 1076
		public LayerMask volumeLayerMask;

		// Token: 0x04000435 RID: 1077
		public Transform volumeTrigger;

		// Token: 0x04000436 RID: 1078
		public bool isStopNaNEnabled;

		// Token: 0x04000437 RID: 1079
		public bool isDitheringEnabled;

		// Token: 0x04000438 RID: 1080
		public AntialiasingMode antialiasing;

		// Token: 0x04000439 RID: 1081
		public AntialiasingQuality antialiasingQuality;

		// Token: 0x0400043A RID: 1082
		public ScriptableRenderer renderer;

		// Token: 0x0400043B RID: 1083
		public bool resolveFinalTarget;

		// Token: 0x0400043C RID: 1084
		public Vector3 worldSpaceCameraPos;

		// Token: 0x0400043D RID: 1085
		public Color backgroundColor;

		// Token: 0x0400043E RID: 1086
		internal TaaHistory taaHistory;

		// Token: 0x0400043F RID: 1087
		internal StpHistory stpHistory;

		// Token: 0x04000440 RID: 1088
		internal TemporalAA.Settings taaSettings;

		// Token: 0x04000441 RID: 1089
		public Camera baseCamera;
	}
}
