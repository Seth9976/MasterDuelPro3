using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020001B1 RID: 433
	[DisallowMultipleComponent]
	[RequireComponent(typeof(Camera))]
	[ExecuteAlways]
	public class UniversalAdditionalCameraData : MonoBehaviour, ISerializationCallbackReceiver, IAdditionalData
	{
		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000907 RID: 2311 RVA: 0x0002DD85 File Offset: 0x0002BF85
		public float version
		{
			get
			{
				return this.m_Version;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x0002DD8D File Offset: 0x0002BF8D
		internal static UniversalAdditionalCameraData defaultAdditionalCameraData
		{
			get
			{
				if (UniversalAdditionalCameraData.s_DefaultAdditionalCameraData == null)
				{
					UniversalAdditionalCameraData.s_DefaultAdditionalCameraData = new UniversalAdditionalCameraData();
				}
				return UniversalAdditionalCameraData.s_DefaultAdditionalCameraData;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x0002DDAB File Offset: 0x0002BFAB
		internal Camera camera
		{
			get
			{
				if (!this.m_Camera)
				{
					base.gameObject.TryGetComponent<Camera>(out this.m_Camera);
				}
				return this.m_Camera;
			}
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x0002DDD2 File Offset: 0x0002BFD2
		private void Start()
		{
			if (this.m_CameraType == CameraRenderType.Overlay)
			{
				this.camera.clearFlags = CameraClearFlags.Nothing;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x0002DDE9 File Offset: 0x0002BFE9
		// (set) Token: 0x0600090C RID: 2316 RVA: 0x0002DDF1 File Offset: 0x0002BFF1
		public bool renderShadows
		{
			get
			{
				return this.m_RenderShadows;
			}
			set
			{
				this.m_RenderShadows = value;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x0002DDFA File Offset: 0x0002BFFA
		// (set) Token: 0x0600090E RID: 2318 RVA: 0x0002DE02 File Offset: 0x0002C002
		public CameraOverrideOption requiresDepthOption
		{
			get
			{
				return this.m_RequiresDepthTextureOption;
			}
			set
			{
				this.m_RequiresDepthTextureOption = value;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x0600090F RID: 2319 RVA: 0x0002DE0B File Offset: 0x0002C00B
		// (set) Token: 0x06000910 RID: 2320 RVA: 0x0002DE13 File Offset: 0x0002C013
		public CameraOverrideOption requiresColorOption
		{
			get
			{
				return this.m_RequiresOpaqueTextureOption;
			}
			set
			{
				this.m_RequiresOpaqueTextureOption = value;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x0002DE1C File Offset: 0x0002C01C
		// (set) Token: 0x06000912 RID: 2322 RVA: 0x0002DE24 File Offset: 0x0002C024
		public CameraRenderType renderType
		{
			get
			{
				return this.m_CameraType;
			}
			set
			{
				this.m_CameraType = value;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x0002DE30 File Offset: 0x0002C030
		public List<Camera> cameraStack
		{
			get
			{
				if (this.renderType != CameraRenderType.Base)
				{
					Camera camera = base.gameObject.GetComponent<Camera>();
					Debug.LogWarning(string.Format("{0}: This camera is of {1} type. Only Base cameras can have a camera stack.", camera.name, this.renderType));
					return null;
				}
				if (!this.scriptableRenderer.SupportsCameraStackingType(CameraRenderType.Base))
				{
					Camera camera2 = base.gameObject.GetComponent<Camera>();
					Debug.LogWarning(string.Format("{0}: This camera has a ScriptableRenderer that doesn't support camera stacking. Camera stack is null.", camera2.name));
					return null;
				}
				return this.m_Cameras;
			}
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0002DEAC File Offset: 0x0002C0AC
		internal void UpdateCameraStack()
		{
			int count = this.m_Cameras.Count;
			this.m_Cameras.RemoveAll((Camera cam) => cam == null);
			int curr = this.m_Cameras.Count;
			int removedCamsCount = count - curr;
			if (removedCamsCount != 0)
			{
				Debug.LogWarning(string.Concat(new string[]
				{
					base.name,
					": ",
					removedCamsCount.ToString(),
					" camera overlay",
					(removedCamsCount > 1) ? "s" : "",
					" no longer exists and will be removed from the camera stack."
				}));
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x0002DF4F File Offset: 0x0002C14F
		public bool clearDepth
		{
			get
			{
				return this.m_ClearDepth;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x0002DF57 File Offset: 0x0002C157
		// (set) Token: 0x06000917 RID: 2327 RVA: 0x0002DF76 File Offset: 0x0002C176
		public bool requiresDepthTexture
		{
			get
			{
				if (this.m_RequiresDepthTextureOption == CameraOverrideOption.UsePipelineSettings)
				{
					return UniversalRenderPipeline.asset.supportsCameraDepthTexture;
				}
				return this.m_RequiresDepthTextureOption == CameraOverrideOption.On;
			}
			set
			{
				this.m_RequiresDepthTextureOption = (value ? CameraOverrideOption.On : CameraOverrideOption.Off);
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x0002DF85 File Offset: 0x0002C185
		// (set) Token: 0x06000919 RID: 2329 RVA: 0x0002DFA4 File Offset: 0x0002C1A4
		public bool requiresColorTexture
		{
			get
			{
				if (this.m_RequiresOpaqueTextureOption == CameraOverrideOption.UsePipelineSettings)
				{
					return UniversalRenderPipeline.asset.supportsCameraOpaqueTexture;
				}
				return this.m_RequiresOpaqueTextureOption == CameraOverrideOption.On;
			}
			set
			{
				this.m_RequiresOpaqueTextureOption = (value ? CameraOverrideOption.On : CameraOverrideOption.Off);
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x0002DFB4 File Offset: 0x0002C1B4
		public ScriptableRenderer scriptableRenderer
		{
			get
			{
				if (UniversalRenderPipeline.asset == null)
				{
					return null;
				}
				if (!UniversalRenderPipeline.asset.ValidateRendererData(this.m_RendererIndex))
				{
					int defaultIndex = UniversalRenderPipeline.asset.m_DefaultRendererIndex;
					Debug.LogWarning(string.Concat(new string[]
					{
						"Renderer at <b>index ",
						this.m_RendererIndex.ToString(),
						"</b> is missing for camera <b>",
						this.camera.name,
						"</b>, falling back to Default Renderer. <b>",
						UniversalRenderPipeline.asset.m_RendererDataList[defaultIndex].name,
						"</b>"
					}), UniversalRenderPipeline.asset);
					return UniversalRenderPipeline.asset.GetRenderer(defaultIndex);
				}
				return UniversalRenderPipeline.asset.GetRenderer(this.m_RendererIndex);
			}
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0002E068 File Offset: 0x0002C268
		public void SetRenderer(int index)
		{
			this.m_RendererIndex = index;
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x0002E071 File Offset: 0x0002C271
		// (set) Token: 0x0600091D RID: 2333 RVA: 0x0002E079 File Offset: 0x0002C279
		public LayerMask volumeLayerMask
		{
			get
			{
				return this.m_VolumeLayerMask;
			}
			set
			{
				this.m_VolumeLayerMask = value;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x0002E082 File Offset: 0x0002C282
		// (set) Token: 0x0600091F RID: 2335 RVA: 0x0002E08A File Offset: 0x0002C28A
		public Transform volumeTrigger
		{
			get
			{
				return this.m_VolumeTrigger;
			}
			set
			{
				this.m_VolumeTrigger = value;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x0002E093 File Offset: 0x0002C293
		// (set) Token: 0x06000921 RID: 2337 RVA: 0x0002E09B File Offset: 0x0002C29B
		internal VolumeFrameworkUpdateMode volumeFrameworkUpdateMode
		{
			get
			{
				return this.m_VolumeFrameworkUpdateModeOption;
			}
			set
			{
				this.m_VolumeFrameworkUpdateModeOption = value;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x0002E0A4 File Offset: 0x0002C2A4
		public bool requiresVolumeFrameworkUpdate
		{
			get
			{
				if (this.m_VolumeFrameworkUpdateModeOption == VolumeFrameworkUpdateMode.UsePipelineSettings)
				{
					return UniversalRenderPipeline.asset.volumeFrameworkUpdateMode != VolumeFrameworkUpdateMode.ViaScripting;
				}
				return this.m_VolumeFrameworkUpdateModeOption == VolumeFrameworkUpdateMode.EveryFrame;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x0002E0C9 File Offset: 0x0002C2C9
		// (set) Token: 0x06000924 RID: 2340 RVA: 0x0002E0D4 File Offset: 0x0002C2D4
		public VolumeStack volumeStack
		{
			get
			{
				return this.m_VolumeStack;
			}
			set
			{
				if (value == null && this.m_VolumeStack != null && this.m_VolumeStack.isValid)
				{
					if (UniversalAdditionalCameraData.s_CachedVolumeStacks == null)
					{
						UniversalAdditionalCameraData.s_CachedVolumeStacks = new List<VolumeStack>(4);
					}
					UniversalAdditionalCameraData.s_CachedVolumeStacks.Add(this.m_VolumeStack);
				}
				this.m_VolumeStack = value;
			}
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0002E124 File Offset: 0x0002C324
		internal void GetOrCreateVolumeStack()
		{
			if (UniversalAdditionalCameraData.s_CachedVolumeStacks != null && UniversalAdditionalCameraData.s_CachedVolumeStacks.Count > 0)
			{
				int index = UniversalAdditionalCameraData.s_CachedVolumeStacks.Count - 1;
				VolumeStack stack = UniversalAdditionalCameraData.s_CachedVolumeStacks[index];
				UniversalAdditionalCameraData.s_CachedVolumeStacks.RemoveAt(index);
				if (stack.isValid)
				{
					this.volumeStack = stack;
				}
			}
			if (this.volumeStack == null)
			{
				this.volumeStack = VolumeManager.instance.CreateStack();
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000926 RID: 2342 RVA: 0x0002E190 File Offset: 0x0002C390
		// (set) Token: 0x06000927 RID: 2343 RVA: 0x0002E198 File Offset: 0x0002C398
		public bool renderPostProcessing
		{
			get
			{
				return this.m_RenderPostProcessing;
			}
			set
			{
				this.m_RenderPostProcessing = value;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x0002E1A1 File Offset: 0x0002C3A1
		// (set) Token: 0x06000929 RID: 2345 RVA: 0x0002E1A9 File Offset: 0x0002C3A9
		public AntialiasingMode antialiasing
		{
			get
			{
				return this.m_Antialiasing;
			}
			set
			{
				this.m_Antialiasing = value;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x0002E1B2 File Offset: 0x0002C3B2
		// (set) Token: 0x0600092B RID: 2347 RVA: 0x0002E1BA File Offset: 0x0002C3BA
		public AntialiasingQuality antialiasingQuality
		{
			get
			{
				return this.m_AntialiasingQuality;
			}
			set
			{
				this.m_AntialiasingQuality = value;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x0002E1C3 File Offset: 0x0002C3C3
		public ref TemporalAA.Settings taaSettings
		{
			get
			{
				return ref this.m_TaaSettings;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x0002E1CB File Offset: 0x0002C3CB
		public ICameraHistoryReadAccess history
		{
			get
			{
				return this.m_History;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x0002E1CB File Offset: 0x0002C3CB
		internal UniversalCameraHistory historyManager
		{
			get
			{
				return this.m_History;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x0002E1D3 File Offset: 0x0002C3D3
		internal MotionVectorsPersistentData motionVectorsPersistentData
		{
			get
			{
				return this.m_MotionVectorsPersistentData;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000930 RID: 2352 RVA: 0x0002E1DB File Offset: 0x0002C3DB
		// (set) Token: 0x06000931 RID: 2353 RVA: 0x0002E1EB File Offset: 0x0002C3EB
		public bool resetHistory
		{
			get
			{
				return this.m_TaaSettings.resetHistoryFrames != 0;
			}
			set
			{
				this.m_TaaSettings.resetHistoryFrames = this.m_TaaSettings.resetHistoryFrames + (value ? 1 : 0);
				this.m_MotionVectorsPersistentData.Reset();
				this.m_TaaSettings.jitterFrameCountOffset = -Time.frameCount;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x0002E21F File Offset: 0x0002C41F
		// (set) Token: 0x06000933 RID: 2355 RVA: 0x0002E227 File Offset: 0x0002C427
		public bool stopNaN
		{
			get
			{
				return this.m_StopNaN;
			}
			set
			{
				this.m_StopNaN = value;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x0002E230 File Offset: 0x0002C430
		// (set) Token: 0x06000935 RID: 2357 RVA: 0x0002E238 File Offset: 0x0002C438
		public bool dithering
		{
			get
			{
				return this.m_Dithering;
			}
			set
			{
				this.m_Dithering = value;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x0002E241 File Offset: 0x0002C441
		// (set) Token: 0x06000937 RID: 2359 RVA: 0x0002E249 File Offset: 0x0002C449
		public bool allowXRRendering
		{
			get
			{
				return this.m_AllowXRRendering;
			}
			set
			{
				this.m_AllowXRRendering = value;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x0002E252 File Offset: 0x0002C452
		// (set) Token: 0x06000939 RID: 2361 RVA: 0x0002E25A File Offset: 0x0002C45A
		public bool useScreenCoordOverride
		{
			get
			{
				return this.m_UseScreenCoordOverride;
			}
			set
			{
				this.m_UseScreenCoordOverride = value;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x0002E263 File Offset: 0x0002C463
		// (set) Token: 0x0600093B RID: 2363 RVA: 0x0002E26B File Offset: 0x0002C46B
		public Vector4 screenSizeOverride
		{
			get
			{
				return this.m_ScreenSizeOverride;
			}
			set
			{
				this.m_ScreenSizeOverride = value;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x0002E274 File Offset: 0x0002C474
		// (set) Token: 0x0600093D RID: 2365 RVA: 0x0002E27C File Offset: 0x0002C47C
		public Vector4 screenCoordScaleBias
		{
			get
			{
				return this.m_ScreenCoordScaleBias;
			}
			set
			{
				this.m_ScreenCoordScaleBias = value;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x0002E285 File Offset: 0x0002C485
		// (set) Token: 0x0600093F RID: 2367 RVA: 0x0002E28D File Offset: 0x0002C48D
		public bool allowHDROutput
		{
			get
			{
				return this.m_AllowHDROutput;
			}
			set
			{
				this.m_AllowHDROutput = value;
			}
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0000217F File Offset: 0x0000037F
		public void OnBeforeSerialize()
		{
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0002E296 File Offset: 0x0002C496
		public void OnAfterDeserialize()
		{
			if (this.version <= 1f)
			{
				this.m_RequiresDepthTextureOption = (this.m_RequiresDepthTexture ? CameraOverrideOption.On : CameraOverrideOption.Off);
				this.m_RequiresOpaqueTextureOption = (this.m_RequiresColorTexture ? CameraOverrideOption.On : CameraOverrideOption.Off);
				this.m_Version = 2f;
			}
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0002E2D4 File Offset: 0x0002C4D4
		public void OnValidate()
		{
			if (this.m_CameraType == CameraRenderType.Overlay && this.m_Camera != null)
			{
				this.m_Camera.clearFlags = CameraClearFlags.Nothing;
			}
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x0002E2FC File Offset: 0x0002C4FC
		public void OnDrawGizmos()
		{
			string gizmoName = "";
			Color tint = Color.white;
			if (this.m_CameraType == CameraRenderType.Base)
			{
				gizmoName = "Packages/com.unity.render-pipelines.universal/Editor/Gizmos/Camera_Base.png";
			}
			else if (this.m_CameraType == CameraRenderType.Overlay)
			{
				gizmoName = "Packages/com.unity.render-pipelines.universal/Editor/Gizmos/Camera_Base.png";
			}
			if (!string.IsNullOrEmpty(gizmoName))
			{
				Gizmos.DrawIcon(base.transform.position, gizmoName, true, tint);
			}
			if (this.renderPostProcessing)
			{
				Gizmos.DrawIcon(base.transform.position, "Packages/com.unity.render-pipelines.universal/Editor/Gizmos/Camera_PostProcessing.png", true, tint);
			}
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x0002E370 File Offset: 0x0002C570
		public void OnDestroy()
		{
			this.m_Camera.DestroyVolumeStack(this);
			if (this.camera.cameraType != CameraType.SceneView)
			{
				ScriptableRenderer rawRenderer = this.GetRawRenderer();
				if (rawRenderer != null)
				{
					rawRenderer.ReleaseRenderTargets();
				}
			}
			UniversalCameraHistory history = this.m_History;
			if (history != null)
			{
				history.Dispose();
			}
			this.m_History = null;
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x0002E3C0 File Offset: 0x0002C5C0
		private unsafe ScriptableRenderer GetRawRenderer()
		{
			if (UniversalRenderPipeline.asset == null)
			{
				return null;
			}
			ReadOnlySpan<ScriptableRenderer> renderers = UniversalRenderPipeline.asset.renderers;
			if (renderers == null || renderers.IsEmpty)
			{
				return null;
			}
			if (this.m_RendererIndex >= renderers.Length || this.m_RendererIndex < 0)
			{
				return null;
			}
			return *renderers[this.m_RendererIndex];
		}

		// Token: 0x04000972 RID: 2418
		private const string k_GizmoPath = "Packages/com.unity.render-pipelines.universal/Editor/Gizmos/";

		// Token: 0x04000973 RID: 2419
		private const string k_BaseCameraGizmoPath = "Packages/com.unity.render-pipelines.universal/Editor/Gizmos/Camera_Base.png";

		// Token: 0x04000974 RID: 2420
		private const string k_OverlayCameraGizmoPath = "Packages/com.unity.render-pipelines.universal/Editor/Gizmos/Camera_Base.png";

		// Token: 0x04000975 RID: 2421
		private const string k_PostProcessingGizmoPath = "Packages/com.unity.render-pipelines.universal/Editor/Gizmos/Camera_PostProcessing.png";

		// Token: 0x04000976 RID: 2422
		[FormerlySerializedAs("renderShadows")]
		[SerializeField]
		private bool m_RenderShadows = true;

		// Token: 0x04000977 RID: 2423
		[SerializeField]
		private CameraOverrideOption m_RequiresDepthTextureOption = CameraOverrideOption.UsePipelineSettings;

		// Token: 0x04000978 RID: 2424
		[SerializeField]
		private CameraOverrideOption m_RequiresOpaqueTextureOption = CameraOverrideOption.UsePipelineSettings;

		// Token: 0x04000979 RID: 2425
		[SerializeField]
		private CameraRenderType m_CameraType;

		// Token: 0x0400097A RID: 2426
		[SerializeField]
		private List<Camera> m_Cameras = new List<Camera>();

		// Token: 0x0400097B RID: 2427
		[SerializeField]
		private int m_RendererIndex = -1;

		// Token: 0x0400097C RID: 2428
		[SerializeField]
		private LayerMask m_VolumeLayerMask = 1;

		// Token: 0x0400097D RID: 2429
		[SerializeField]
		private Transform m_VolumeTrigger;

		// Token: 0x0400097E RID: 2430
		[SerializeField]
		private VolumeFrameworkUpdateMode m_VolumeFrameworkUpdateModeOption = VolumeFrameworkUpdateMode.UsePipelineSettings;

		// Token: 0x0400097F RID: 2431
		[SerializeField]
		private bool m_RenderPostProcessing;

		// Token: 0x04000980 RID: 2432
		[SerializeField]
		private AntialiasingMode m_Antialiasing;

		// Token: 0x04000981 RID: 2433
		[SerializeField]
		private AntialiasingQuality m_AntialiasingQuality = AntialiasingQuality.High;

		// Token: 0x04000982 RID: 2434
		[SerializeField]
		private bool m_StopNaN;

		// Token: 0x04000983 RID: 2435
		[SerializeField]
		private bool m_Dithering;

		// Token: 0x04000984 RID: 2436
		[SerializeField]
		private bool m_ClearDepth = true;

		// Token: 0x04000985 RID: 2437
		[SerializeField]
		private bool m_AllowXRRendering = true;

		// Token: 0x04000986 RID: 2438
		[SerializeField]
		private bool m_AllowHDROutput = true;

		// Token: 0x04000987 RID: 2439
		[SerializeField]
		private bool m_UseScreenCoordOverride;

		// Token: 0x04000988 RID: 2440
		[SerializeField]
		private Vector4 m_ScreenSizeOverride;

		// Token: 0x04000989 RID: 2441
		[SerializeField]
		private Vector4 m_ScreenCoordScaleBias;

		// Token: 0x0400098A RID: 2442
		[NonSerialized]
		private Camera m_Camera;

		// Token: 0x0400098B RID: 2443
		[FormerlySerializedAs("requiresDepthTexture")]
		[SerializeField]
		private bool m_RequiresDepthTexture;

		// Token: 0x0400098C RID: 2444
		[FormerlySerializedAs("requiresColorTexture")]
		[SerializeField]
		private bool m_RequiresColorTexture;

		// Token: 0x0400098D RID: 2445
		[HideInInspector]
		[SerializeField]
		private float m_Version = 2f;

		// Token: 0x0400098E RID: 2446
		[NonSerialized]
		private MotionVectorsPersistentData m_MotionVectorsPersistentData = new MotionVectorsPersistentData();

		// Token: 0x0400098F RID: 2447
		[NonSerialized]
		internal UniversalCameraHistory m_History = new UniversalCameraHistory();

		// Token: 0x04000990 RID: 2448
		[SerializeField]
		internal TemporalAA.Settings m_TaaSettings = TemporalAA.Settings.Create();

		// Token: 0x04000991 RID: 2449
		private static UniversalAdditionalCameraData s_DefaultAdditionalCameraData;

		// Token: 0x04000992 RID: 2450
		private static List<VolumeStack> s_CachedVolumeStacks;

		// Token: 0x04000993 RID: 2451
		private VolumeStack m_VolumeStack;
	}
}
