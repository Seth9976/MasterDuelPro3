using System;
using System.ComponentModel;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000015 RID: 21
	[ExcludeFromPreset]
	public class UniversalRenderPipelineAsset : RenderPipelineAsset<UniversalRenderPipeline>, ISerializationCallbackReceiver, IProbeVolumeEnabledRenderPipeline, IGPUResidentRenderPipeline, IRenderGraphEnabledRenderPipeline, ISTPEnabledRenderPipeline
	{
		// Token: 0x06000029 RID: 41 RVA: 0x0000278C File Offset: 0x0000098C
		private Material GetMaterial(DefaultMaterialType materialType)
		{
			return null;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600002A RID: 42 RVA: 0x0000278F File Offset: 0x0000098F
		public override Material defaultMaterial
		{
			get
			{
				return this.GetMaterial(DefaultMaterialType.Default);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002798 File Offset: 0x00000998
		public override Material defaultParticleMaterial
		{
			get
			{
				return this.GetMaterial(DefaultMaterialType.Particle);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002798 File Offset: 0x00000998
		public override Material defaultLineMaterial
		{
			get
			{
				return this.GetMaterial(DefaultMaterialType.Particle);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000027A1 File Offset: 0x000009A1
		public override Material defaultTerrainMaterial
		{
			get
			{
				return this.GetMaterial(DefaultMaterialType.Terrain);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000027AA File Offset: 0x000009AA
		public override Material default2DMaterial
		{
			get
			{
				return this.GetMaterial(DefaultMaterialType.Sprite);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000027B3 File Offset: 0x000009B3
		public override Material default2DMaskMaterial
		{
			get
			{
				return this.GetMaterial(DefaultMaterialType.SpriteMask);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000027BC File Offset: 0x000009BC
		public Material decalMaterial
		{
			get
			{
				return this.GetMaterial(DefaultMaterialType.Decal);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000027C5 File Offset: 0x000009C5
		public override Shader defaultShader
		{
			get
			{
				if (this.m_DefaultShader == null)
				{
					this.m_DefaultShader = Shader.Find(ShaderUtils.GetShaderPath(ShaderPathID.Lit));
				}
				return this.m_DefaultShader;
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000027EC File Offset: 0x000009EC
		internal bool IsAtLastVersion()
		{
			return 12 == this.k_AssetVersion;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000027F8 File Offset: 0x000009F8
		GPUResidentDrawerSettings IGPUResidentRenderPipeline.gpuResidentDrawerSettings
		{
			get
			{
				return new GPUResidentDrawerSettings
				{
					mode = this.m_GPUResidentDrawerMode,
					enableOcclusionCulling = this.m_GPUResidentDrawerEnableOcclusionCullingInCameras,
					supportDitheringCrossFade = this.m_EnableLODCrossFade,
					allowInEditMode = true,
					smallMeshScreenPercentage = this.m_SmallMeshScreenPercentage,
					errorShader = Shader.Find("Hidden/Universal Render Pipeline/FallbackError"),
					loadingShader = Shader.Find("Hidden/Universal Render Pipeline/FallbackLoading")
				};
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000034 RID: 52 RVA: 0x0000286C File Offset: 0x00000A6C
		public ReadOnlySpan<ScriptableRendererData> rendererDataList
		{
			get
			{
				return this.m_RendererDataList;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002879 File Offset: 0x00000A79
		public ReadOnlySpan<ScriptableRenderer> renderers
		{
			get
			{
				return this.m_Renderers;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002886 File Offset: 0x00000A86
		public bool isImmediateModeSupported
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002889 File Offset: 0x00000A89
		public ScriptableRendererData LoadBuiltinRendererData(RendererType type = RendererType.UniversalRenderer)
		{
			this.m_RendererDataList[0] = null;
			return this.m_RendererDataList[0];
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000289C File Offset: 0x00000A9C
		protected override void EnsureGlobalSettings()
		{
			base.EnsureGlobalSettings();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000028A4 File Offset: 0x00000AA4
		protected override RenderPipeline CreatePipeline()
		{
			if (this.m_RendererDataList == null)
			{
				this.m_RendererDataList = new ScriptableRendererData[1];
			}
			if (this.m_DefaultRendererIndex < this.m_RendererDataList.Length && !(this.m_RendererDataList[this.m_DefaultRendererIndex] == null))
			{
				this.DestroyRenderers();
				RenderPipeline renderPipeline = new UniversalRenderPipeline(this);
				this.CreateRenderers();
				IGPUResidentRenderPipeline.ReinitializeGPUResidentDrawer();
				return renderPipeline;
			}
			if (this.k_AssetPreviousVersion != this.k_AssetVersion)
			{
				return null;
			}
			Debug.LogError("Default Renderer is missing, make sure there is a Renderer assigned as the default on the current Universal RP asset:" + UniversalRenderPipeline.asset.name, this);
			return null;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002930 File Offset: 0x00000B30
		internal void DestroyRenderers()
		{
			if (this.m_Renderers == null)
			{
				return;
			}
			for (int i = 0; i < this.m_Renderers.Length; i++)
			{
				this.DestroyRenderer(ref this.m_Renderers[i]);
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000296B File Offset: 0x00000B6B
		private void DestroyRenderer(ref ScriptableRenderer renderer)
		{
			if (renderer != null)
			{
				renderer.Dispose();
				renderer = null;
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000297B File Offset: 0x00000B7B
		protected override void OnDisable()
		{
			this.DestroyRenderers();
			base.OnDisable();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000298C File Offset: 0x00000B8C
		private void CreateRenderers()
		{
			if (this.m_Renderers != null)
			{
				for (int i = 0; i < this.m_Renderers.Length; i++)
				{
					if (this.m_Renderers[i] != null)
					{
						Debug.LogError(string.Format("Creating renderers but previous instance wasn't properly destroyed: m_Renderers[{0}]", i));
					}
				}
			}
			if (this.m_Renderers == null || this.m_Renderers.Length != this.m_RendererDataList.Length)
			{
				this.m_Renderers = new ScriptableRenderer[this.m_RendererDataList.Length];
			}
			for (int j = 0; j < this.m_RendererDataList.Length; j++)
			{
				if (this.m_RendererDataList[j] != null)
				{
					this.m_Renderers[j] = this.m_RendererDataList[j].InternalCreateRenderer();
				}
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002A38 File Offset: 0x00000C38
		public ScriptableRenderer scriptableRenderer
		{
			get
			{
				ScriptableRendererData[] rendererDataList = this.m_RendererDataList;
				int? num = ((rendererDataList != null) ? new int?(rendererDataList.Length) : null);
				int defaultRendererIndex = this.m_DefaultRendererIndex;
				if (((num.GetValueOrDefault() > defaultRendererIndex) & (num != null)) && this.m_RendererDataList[this.m_DefaultRendererIndex] == null)
				{
					Debug.LogError("Default renderer is missing from the current Pipeline Asset.", this);
					return null;
				}
				if (this.scriptableRendererData.isInvalidated || this.m_Renderers[this.m_DefaultRendererIndex] == null)
				{
					this.DestroyRenderer(ref this.m_Renderers[this.m_DefaultRendererIndex]);
					this.m_Renderers[this.m_DefaultRendererIndex] = this.scriptableRendererData.InternalCreateRenderer();
					if (this.gpuResidentDrawerMode != GPUResidentDrawerMode.Disabled)
					{
						IGPUResidentRenderPipeline.ReinitializeGPUResidentDrawer();
					}
				}
				return this.m_Renderers[this.m_DefaultRendererIndex];
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002B04 File Offset: 0x00000D04
		public ScriptableRenderer GetRenderer(int index)
		{
			if (index == -1)
			{
				index = this.m_DefaultRendererIndex;
			}
			if (index >= this.m_RendererDataList.Length || index < 0 || this.m_RendererDataList[index] == null)
			{
				Debug.LogWarning("Renderer at index " + index.ToString() + " is missing, falling back to Default Renderer " + this.m_RendererDataList[this.m_DefaultRendererIndex].name, this);
				index = this.m_DefaultRendererIndex;
			}
			if (this.m_Renderers == null || this.m_Renderers.Length < this.m_RendererDataList.Length)
			{
				this.DestroyRenderers();
				this.CreateRenderers();
			}
			if (this.m_RendererDataList[index].isInvalidated || this.m_Renderers[index] == null)
			{
				this.DestroyRenderer(ref this.m_Renderers[index]);
				this.m_Renderers[index] = this.m_RendererDataList[index].InternalCreateRenderer();
				if (this.gpuResidentDrawerMode != GPUResidentDrawerMode.Disabled)
				{
					IGPUResidentRenderPipeline.ReinitializeGPUResidentDrawer();
				}
			}
			return this.m_Renderers[index];
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002BED File Offset: 0x00000DED
		internal ScriptableRendererData scriptableRendererData
		{
			get
			{
				if (this.m_RendererDataList[this.m_DefaultRendererIndex] == null)
				{
					this.CreatePipeline();
				}
				return this.m_RendererDataList[this.m_DefaultRendererIndex];
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002C18 File Offset: 0x00000E18
		internal GraphicsFormat additionalLightsCookieFormat
		{
			get
			{
				GraphicsFormat result = GraphicsFormat.None;
				foreach (GraphicsFormat format in UniversalRenderPipelineAsset.s_LightCookieFormatList[(int)this.m_AdditionalLightsCookieFormat])
				{
					if (SystemInfo.IsFormatSupported(format, GraphicsFormatUsage.Render))
					{
						result = format;
						break;
					}
				}
				if (QualitySettings.activeColorSpace == ColorSpace.Gamma)
				{
					result = GraphicsFormatUtility.GetLinearFormat(result);
				}
				if (result == GraphicsFormat.None)
				{
					result = GraphicsFormat.R8G8B8A8_UNorm;
					Debug.LogWarning(string.Format("Additional Lights Cookie Format ({0}) is not supported by the platform. Falling back to {1}-bit format ({2})", this.m_AdditionalLightsCookieFormat.ToString(), GraphicsFormatUtility.GetBlockSize(result) * 8U, GraphicsFormatUtility.GetFormatString(result)));
				}
				return result;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002C9B File Offset: 0x00000E9B
		internal Vector2Int additionalLightsCookieResolution
		{
			get
			{
				return new Vector2Int((int)this.m_AdditionalLightsCookieResolution, (int)this.m_AdditionalLightsCookieResolution);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002CB0 File Offset: 0x00000EB0
		internal int[] rendererIndexList
		{
			get
			{
				int[] list = new int[this.m_RendererDataList.Length + 1];
				for (int i = 0; i < list.Length; i++)
				{
					list[i] = i - 1;
				}
				return list;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002CE2 File Offset: 0x00000EE2
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002CEA File Offset: 0x00000EEA
		public bool supportsCameraDepthTexture
		{
			get
			{
				return this.m_RequireDepthTexture;
			}
			set
			{
				this.m_RequireDepthTexture = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002CF3 File Offset: 0x00000EF3
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002CFB File Offset: 0x00000EFB
		public bool supportsCameraOpaqueTexture
		{
			get
			{
				return this.m_RequireOpaqueTexture;
			}
			set
			{
				this.m_RequireOpaqueTexture = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002D04 File Offset: 0x00000F04
		public Downsampling opaqueDownsampling
		{
			get
			{
				return this.m_OpaqueDownsampling;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002D0C File Offset: 0x00000F0C
		public bool supportsTerrainHoles
		{
			get
			{
				return this.m_SupportsTerrainHoles;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002D14 File Offset: 0x00000F14
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00002D1C File Offset: 0x00000F1C
		public StoreActionsOptimization storeActionsOptimization
		{
			get
			{
				return this.m_StoreActionsOptimization;
			}
			set
			{
				this.m_StoreActionsOptimization = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002D25 File Offset: 0x00000F25
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00002D2D File Offset: 0x00000F2D
		public bool supportsHDR
		{
			get
			{
				return this.m_SupportsHDR;
			}
			set
			{
				this.m_SupportsHDR = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002D36 File Offset: 0x00000F36
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00002D3E File Offset: 0x00000F3E
		public HDRColorBufferPrecision hdrColorBufferPrecision
		{
			get
			{
				return this.m_HDRColorBufferPrecision;
			}
			set
			{
				this.m_HDRColorBufferPrecision = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002D47 File Offset: 0x00000F47
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00002D4F File Offset: 0x00000F4F
		public int msaaSampleCount
		{
			get
			{
				return (int)this.m_MSAA;
			}
			set
			{
				this.m_MSAA = (MsaaQuality)value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002D58 File Offset: 0x00000F58
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00002D60 File Offset: 0x00000F60
		public float renderScale
		{
			get
			{
				return this.m_RenderScale;
			}
			set
			{
				this.m_RenderScale = this.ValidateRenderScale(value);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002D6F File Offset: 0x00000F6F
		public bool enableLODCrossFade
		{
			get
			{
				return this.m_EnableLODCrossFade;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002D77 File Offset: 0x00000F77
		public LODCrossFadeDitheringType lodCrossFadeDitheringType
		{
			get
			{
				return this.m_LODCrossFadeDitheringType;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002D7F File Offset: 0x00000F7F
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00002D87 File Offset: 0x00000F87
		public UpscalingFilterSelection upscalingFilter
		{
			get
			{
				return this.m_UpscalingFilter;
			}
			set
			{
				this.m_UpscalingFilter = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002D90 File Offset: 0x00000F90
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00002D98 File Offset: 0x00000F98
		public bool fsrOverrideSharpness
		{
			get
			{
				return this.m_FsrOverrideSharpness;
			}
			set
			{
				this.m_FsrOverrideSharpness = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002DA1 File Offset: 0x00000FA1
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00002DA9 File Offset: 0x00000FA9
		public float fsrSharpness
		{
			get
			{
				return this.m_FsrSharpness;
			}
			set
			{
				this.m_FsrSharpness = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002DB2 File Offset: 0x00000FB2
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00002DBA File Offset: 0x00000FBA
		public ShEvalMode shEvalMode
		{
			get
			{
				return this.m_ShEvalMode;
			}
			internal set
			{
				this.m_ShEvalMode = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002DC3 File Offset: 0x00000FC3
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00002DCB File Offset: 0x00000FCB
		public LightProbeSystem lightProbeSystem
		{
			get
			{
				return this.m_LightProbeSystem;
			}
			internal set
			{
				this.m_LightProbeSystem = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002DD4 File Offset: 0x00000FD4
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00002DDC File Offset: 0x00000FDC
		public ProbeVolumeTextureMemoryBudget probeVolumeMemoryBudget
		{
			get
			{
				return this.m_ProbeVolumeMemoryBudget;
			}
			internal set
			{
				this.m_ProbeVolumeMemoryBudget = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002DE5 File Offset: 0x00000FE5
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00002DED File Offset: 0x00000FED
		public ProbeVolumeBlendingTextureMemoryBudget probeVolumeBlendingMemoryBudget
		{
			get
			{
				return this.m_ProbeVolumeBlendingMemoryBudget;
			}
			internal set
			{
				this.m_ProbeVolumeBlendingMemoryBudget = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002DF6 File Offset: 0x00000FF6
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00002DFE File Offset: 0x00000FFE
		[Obsolete("This is obsolete, use supportProbeVolumeGPUStreaming instead.")]
		public bool supportProbeVolumeStreaming
		{
			get
			{
				return this.m_SupportProbeVolumeGPUStreaming;
			}
			internal set
			{
				this.m_SupportProbeVolumeGPUStreaming = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002DF6 File Offset: 0x00000FF6
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00002DFE File Offset: 0x00000FFE
		public bool supportProbeVolumeGPUStreaming
		{
			get
			{
				return this.m_SupportProbeVolumeGPUStreaming;
			}
			internal set
			{
				this.m_SupportProbeVolumeGPUStreaming = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002E07 File Offset: 0x00001007
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00002E0F File Offset: 0x0000100F
		public bool supportProbeVolumeDiskStreaming
		{
			get
			{
				return this.m_SupportProbeVolumeDiskStreaming;
			}
			internal set
			{
				this.m_SupportProbeVolumeDiskStreaming = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002E18 File Offset: 0x00001018
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00002E20 File Offset: 0x00001020
		public bool supportProbeVolumeScenarios
		{
			get
			{
				return this.m_SupportProbeVolumeScenarios;
			}
			internal set
			{
				this.m_SupportProbeVolumeScenarios = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002E29 File Offset: 0x00001029
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00002E31 File Offset: 0x00001031
		public bool supportProbeVolumeScenarioBlending
		{
			get
			{
				return this.m_SupportProbeVolumeScenarioBlending;
			}
			internal set
			{
				this.m_SupportProbeVolumeScenarioBlending = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002E3A File Offset: 0x0000103A
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00002E42 File Offset: 0x00001042
		public ProbeVolumeSHBands probeVolumeSHBands
		{
			get
			{
				return this.m_ProbeVolumeSHBands;
			}
			internal set
			{
				this.m_ProbeVolumeSHBands = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002E4B File Offset: 0x0000104B
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00002E53 File Offset: 0x00001053
		public LightRenderingMode mainLightRenderingMode
		{
			get
			{
				return this.m_MainLightRenderingMode;
			}
			internal set
			{
				this.m_MainLightRenderingMode = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002E5C File Offset: 0x0000105C
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00002E64 File Offset: 0x00001064
		public bool supportsMainLightShadows
		{
			get
			{
				return this.m_MainLightShadowsSupported;
			}
			internal set
			{
				this.m_MainLightShadowsSupported = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00002E6D File Offset: 0x0000106D
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00002E75 File Offset: 0x00001075
		public int mainLightShadowmapResolution
		{
			get
			{
				return (int)this.m_MainLightShadowmapResolution;
			}
			set
			{
				this.m_MainLightShadowmapResolution = (ShadowResolution)value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002E7E File Offset: 0x0000107E
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00002E86 File Offset: 0x00001086
		public LightRenderingMode additionalLightsRenderingMode
		{
			get
			{
				return this.m_AdditionalLightsRenderingMode;
			}
			internal set
			{
				this.m_AdditionalLightsRenderingMode = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00002E8F File Offset: 0x0000108F
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00002E97 File Offset: 0x00001097
		public int maxAdditionalLightsCount
		{
			get
			{
				return this.m_AdditionalLightsPerObjectLimit;
			}
			set
			{
				this.m_AdditionalLightsPerObjectLimit = this.ValidatePerObjectLights(value);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00002EA6 File Offset: 0x000010A6
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00002EAE File Offset: 0x000010AE
		public bool supportsAdditionalLightShadows
		{
			get
			{
				return this.m_AdditionalLightShadowsSupported;
			}
			internal set
			{
				this.m_AdditionalLightShadowsSupported = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00002EB7 File Offset: 0x000010B7
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00002EBF File Offset: 0x000010BF
		public int additionalLightsShadowmapResolution
		{
			get
			{
				return (int)this.m_AdditionalLightsShadowmapResolution;
			}
			set
			{
				this.m_AdditionalLightsShadowmapResolution = (ShadowResolution)value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00002EC8 File Offset: 0x000010C8
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00002ED0 File Offset: 0x000010D0
		public int additionalLightsShadowResolutionTierLow
		{
			get
			{
				return this.m_AdditionalLightsShadowResolutionTierLow;
			}
			internal set
			{
				this.m_AdditionalLightsShadowResolutionTierLow = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00002ED9 File Offset: 0x000010D9
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00002EE1 File Offset: 0x000010E1
		public int additionalLightsShadowResolutionTierMedium
		{
			get
			{
				return this.m_AdditionalLightsShadowResolutionTierMedium;
			}
			internal set
			{
				this.m_AdditionalLightsShadowResolutionTierMedium = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00002EEA File Offset: 0x000010EA
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00002EF2 File Offset: 0x000010F2
		public int additionalLightsShadowResolutionTierHigh
		{
			get
			{
				return this.m_AdditionalLightsShadowResolutionTierHigh;
			}
			internal set
			{
				this.m_AdditionalLightsShadowResolutionTierHigh = value;
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002EFB File Offset: 0x000010FB
		internal int GetAdditionalLightsShadowResolution(int additionalLightsShadowResolutionTier)
		{
			if (additionalLightsShadowResolutionTier <= UniversalAdditionalLightData.AdditionalLightsShadowResolutionTierLow)
			{
				return this.additionalLightsShadowResolutionTierLow;
			}
			if (additionalLightsShadowResolutionTier == UniversalAdditionalLightData.AdditionalLightsShadowResolutionTierMedium)
			{
				return this.additionalLightsShadowResolutionTierMedium;
			}
			if (additionalLightsShadowResolutionTier >= UniversalAdditionalLightData.AdditionalLightsShadowResolutionTierHigh)
			{
				return this.additionalLightsShadowResolutionTierHigh;
			}
			return this.additionalLightsShadowResolutionTierMedium;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00002F30 File Offset: 0x00001130
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00002F38 File Offset: 0x00001138
		public bool reflectionProbeBlending
		{
			get
			{
				return this.m_ReflectionProbeBlending;
			}
			internal set
			{
				this.m_ReflectionProbeBlending = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00002F41 File Offset: 0x00001141
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00002F49 File Offset: 0x00001149
		public bool reflectionProbeBoxProjection
		{
			get
			{
				return this.m_ReflectionProbeBoxProjection;
			}
			internal set
			{
				this.m_ReflectionProbeBoxProjection = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00002F52 File Offset: 0x00001152
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00002F5A File Offset: 0x0000115A
		public float shadowDistance
		{
			get
			{
				return this.m_ShadowDistance;
			}
			set
			{
				this.m_ShadowDistance = Mathf.Max(0f, value);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00002F6D File Offset: 0x0000116D
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00002F75 File Offset: 0x00001175
		public int shadowCascadeCount
		{
			get
			{
				return this.m_ShadowCascadeCount;
			}
			set
			{
				if (value < 1 || value > 4)
				{
					throw new ArgumentException(string.Format("Value ({0}) needs to be between {1} and {2}.", value, 1, 4));
				}
				this.m_ShadowCascadeCount = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00002FA8 File Offset: 0x000011A8
		// (set) Token: 0x0600008E RID: 142 RVA: 0x00002FB0 File Offset: 0x000011B0
		public float cascade2Split
		{
			get
			{
				return this.m_Cascade2Split;
			}
			set
			{
				this.m_Cascade2Split = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00002FB9 File Offset: 0x000011B9
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00002FC1 File Offset: 0x000011C1
		public Vector2 cascade3Split
		{
			get
			{
				return this.m_Cascade3Split;
			}
			set
			{
				this.m_Cascade3Split = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00002FCA File Offset: 0x000011CA
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00002FD2 File Offset: 0x000011D2
		public Vector3 cascade4Split
		{
			get
			{
				return this.m_Cascade4Split;
			}
			set
			{
				this.m_Cascade4Split = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00002FDB File Offset: 0x000011DB
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00002FE3 File Offset: 0x000011E3
		public float cascadeBorder
		{
			get
			{
				return this.m_CascadeBorder;
			}
			set
			{
				this.m_CascadeBorder = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00002FEC File Offset: 0x000011EC
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00002FF4 File Offset: 0x000011F4
		public float shadowDepthBias
		{
			get
			{
				return this.m_ShadowDepthBias;
			}
			set
			{
				this.m_ShadowDepthBias = this.ValidateShadowBias(value);
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00003003 File Offset: 0x00001203
		// (set) Token: 0x06000098 RID: 152 RVA: 0x0000300B File Offset: 0x0000120B
		public float shadowNormalBias
		{
			get
			{
				return this.m_ShadowNormalBias;
			}
			set
			{
				this.m_ShadowNormalBias = this.ValidateShadowBias(value);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000099 RID: 153 RVA: 0x0000301A File Offset: 0x0000121A
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00003022 File Offset: 0x00001222
		public bool supportsSoftShadows
		{
			get
			{
				return this.m_SoftShadowsSupported;
			}
			internal set
			{
				this.m_SoftShadowsSupported = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600009B RID: 155 RVA: 0x0000302B File Offset: 0x0000122B
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00003033 File Offset: 0x00001233
		internal SoftShadowQuality softShadowQuality
		{
			get
			{
				return this.m_SoftShadowQuality;
			}
			set
			{
				this.m_SoftShadowQuality = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600009D RID: 157 RVA: 0x0000303C File Offset: 0x0000123C
		// (set) Token: 0x0600009E RID: 158 RVA: 0x00003044 File Offset: 0x00001244
		public bool supportsDynamicBatching
		{
			get
			{
				return this.m_SupportsDynamicBatching;
			}
			set
			{
				this.m_SupportsDynamicBatching = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600009F RID: 159 RVA: 0x0000304D File Offset: 0x0000124D
		public bool supportsMixedLighting
		{
			get
			{
				return this.m_MixedLightingSupported;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003055 File Offset: 0x00001255
		public bool supportsLightCookies
		{
			get
			{
				return this.m_SupportsLightCookies;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x0000305D File Offset: 0x0000125D
		[Obsolete("This is obsolete, use useRenderingLayers instead.", true)]
		public bool supportsLightLayers
		{
			get
			{
				return this.m_SupportsLightLayers;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x0000305D File Offset: 0x0000125D
		public bool useRenderingLayers
		{
			get
			{
				return this.m_SupportsLightLayers;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00003065 File Offset: 0x00001265
		public VolumeFrameworkUpdateMode volumeFrameworkUpdateMode
		{
			get
			{
				return this.m_VolumeFrameworkUpdateMode;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x0000306D File Offset: 0x0000126D
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x00003075 File Offset: 0x00001275
		public VolumeProfile volumeProfile
		{
			get
			{
				return this.m_VolumeProfile;
			}
			set
			{
				this.m_VolumeProfile = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00002886 File Offset: 0x00000A86
		[Obsolete("PipelineDebugLevel is deprecated and replaced to use the profiler. Calling debugLevel is not necessary.", true)]
		public PipelineDebugLevel debugLevel
		{
			get
			{
				return PipelineDebugLevel.Disabled;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x0000307E File Offset: 0x0000127E
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00003086 File Offset: 0x00001286
		public bool useSRPBatcher
		{
			get
			{
				return this.m_UseSRPBatcher;
			}
			set
			{
				this.m_UseSRPBatcher = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00003090 File Offset: 0x00001290
		[Obsolete("This has been deprecated, please use GraphicsSettings.GetRenderPipelineSettings<RenderGraphSettings>().enableRenderCompatibilityMode instead.")]
		public bool enableRenderGraph
		{
			get
			{
				RenderGraphSettings renderGraphSettings;
				return RenderGraphGraphicsAutomatedTests.enabled || (GraphicsSettings.TryGetRenderPipelineSettings<RenderGraphSettings>(out renderGraphSettings) && !renderGraphSettings.enableRenderCompatibilityMode);
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000030BA File Offset: 0x000012BA
		internal void OnEnableRenderGraphChanged()
		{
			this.OnValidate();
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000AB RID: 171 RVA: 0x000030C2 File Offset: 0x000012C2
		// (set) Token: 0x060000AC RID: 172 RVA: 0x000030CA File Offset: 0x000012CA
		public ColorGradingMode colorGradingMode
		{
			get
			{
				return this.m_ColorGradingMode;
			}
			set
			{
				this.m_ColorGradingMode = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000AD RID: 173 RVA: 0x000030D3 File Offset: 0x000012D3
		// (set) Token: 0x060000AE RID: 174 RVA: 0x000030DB File Offset: 0x000012DB
		public int colorGradingLutSize
		{
			get
			{
				return this.m_ColorGradingLutSize;
			}
			set
			{
				this.m_ColorGradingLutSize = Mathf.Clamp(value, 16, 65);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000AF RID: 175 RVA: 0x000030ED File Offset: 0x000012ED
		public bool allowPostProcessAlphaOutput
		{
			get
			{
				return this.m_AllowPostProcessAlphaOutput;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x000030F5 File Offset: 0x000012F5
		public bool useFastSRGBLinearConversion
		{
			get
			{
				return this.m_UseFastSRGBLinearConversion;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x000030FD File Offset: 0x000012FD
		public bool supportScreenSpaceLensFlare
		{
			get
			{
				return this.m_SupportScreenSpaceLensFlare;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00003105 File Offset: 0x00001305
		public bool supportDataDrivenLensFlare
		{
			get
			{
				return this.m_SupportDataDrivenLensFlare;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x0000310D File Offset: 0x0000130D
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00003115 File Offset: 0x00001315
		public bool useAdaptivePerformance
		{
			get
			{
				return this.m_UseAdaptivePerformance;
			}
			set
			{
				this.m_UseAdaptivePerformance = value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x0000311E File Offset: 0x0000131E
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x00003126 File Offset: 0x00001326
		public bool conservativeEnclosingSphere
		{
			get
			{
				return this.m_ConservativeEnclosingSphere;
			}
			set
			{
				this.m_ConservativeEnclosingSphere = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x0000312F File Offset: 0x0000132F
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00003137 File Offset: 0x00001337
		public int numIterationsEnclosingSphere
		{
			get
			{
				return this.m_NumIterationsEnclosingSphere;
			}
			set
			{
				this.m_NumIterationsEnclosingSphere = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00003140 File Offset: 0x00001340
		public override string renderPipelineShaderTag
		{
			get
			{
				return "UniversalPipeline";
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00003147 File Offset: 0x00001347
		[Obsolete("This property is obsolete. Use RenderingLayerMask API and Tags & Layers project settings instead. #from(23.3)", false)]
		public override string[] renderingLayerMaskNames
		{
			get
			{
				return RenderingLayerMask.GetDefinedRenderingLayerNames();
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000BB RID: 187 RVA: 0x0000314E File Offset: 0x0000134E
		[Obsolete("This property is obsolete. Use RenderingLayerMask API and Tags & Layers project settings instead. #from(23.3)", false)]
		public override string[] prefixedRenderingLayerMaskNames
		{
			get
			{
				return Array.Empty<string>();
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00003155 File Offset: 0x00001355
		[Obsolete("This is obsolete, please use renderingLayerMaskNames instead.", true)]
		public string[] lightLayerMaskNames
		{
			get
			{
				return new string[0];
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000BD RID: 189 RVA: 0x0000315D File Offset: 0x0000135D
		// (set) Token: 0x060000BE RID: 190 RVA: 0x00003165 File Offset: 0x00001365
		public GPUResidentDrawerMode gpuResidentDrawerMode
		{
			get
			{
				return this.m_GPUResidentDrawerMode;
			}
			set
			{
				if (value == this.m_GPUResidentDrawerMode)
				{
					return;
				}
				this.m_GPUResidentDrawerMode = value;
				this.OnValidate();
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000BF RID: 191 RVA: 0x0000317E File Offset: 0x0000137E
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x00003186 File Offset: 0x00001386
		public bool gpuResidentDrawerEnableOcclusionCullingInCameras
		{
			get
			{
				return this.m_GPUResidentDrawerEnableOcclusionCullingInCameras;
			}
			set
			{
				if (value == this.m_GPUResidentDrawerEnableOcclusionCullingInCameras)
				{
					return;
				}
				this.m_GPUResidentDrawerEnableOcclusionCullingInCameras = value;
				this.OnValidate();
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000031A0 File Offset: 0x000013A0
		public bool IsGPUResidentDrawerSupportedBySRP(out string message, out LogType severty)
		{
			message = string.Empty;
			severty = LogType.Warning;
			ScriptableRendererData[] rendererDataList = this.m_RendererDataList;
			for (int i = 0; i < rendererDataList.Length; i++)
			{
				UniversalRendererData universalRendererData = rendererDataList[i] as UniversalRendererData;
				if (universalRendererData == null)
				{
					message = UniversalRenderPipelineAsset.Strings.notURPRenderer;
					return false;
				}
				if (universalRendererData.renderingMode != RenderingMode.ForwardPlus)
				{
					message = UniversalRenderPipelineAsset.Strings.forwardPlusMissing;
					return false;
				}
			}
			return true;
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x000031F4 File Offset: 0x000013F4
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x000031FC File Offset: 0x000013FC
		public float smallMeshScreenPercentage
		{
			get
			{
				return this.m_SmallMeshScreenPercentage;
			}
			set
			{
				if (Math.Abs(value - this.m_SmallMeshScreenPercentage) < 1E-45f)
				{
					return;
				}
				this.m_SmallMeshScreenPercentage = Mathf.Clamp(value, 0f, 20f);
				this.OnValidate();
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000217F File Offset: 0x0000037F
		public void OnBeforeSerialize()
		{
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003230 File Offset: 0x00001430
		public void OnAfterDeserialize()
		{
			if (this.k_AssetVersion < 3)
			{
				this.m_SoftShadowsSupported = this.m_ShadowType == ShadowQuality.SoftShadows;
				this.k_AssetPreviousVersion = this.k_AssetVersion;
				this.k_AssetVersion = 3;
			}
			if (this.k_AssetVersion < 4)
			{
				this.m_AdditionalLightShadowsSupported = this.m_LocalShadowsSupported;
				this.m_AdditionalLightsShadowmapResolution = this.m_LocalShadowsAtlasResolution;
				this.m_AdditionalLightsPerObjectLimit = this.m_MaxPixelLights;
				this.m_MainLightShadowmapResolution = this.m_ShadowAtlasResolution;
				this.k_AssetPreviousVersion = this.k_AssetVersion;
				this.k_AssetVersion = 4;
			}
			if (this.k_AssetVersion < 5)
			{
				if (this.m_RendererType == RendererType.Custom)
				{
					this.m_RendererDataList[0] = this.m_RendererData;
				}
				this.k_AssetPreviousVersion = this.k_AssetVersion;
				this.k_AssetVersion = 5;
			}
			if (this.k_AssetVersion < 6)
			{
				int value = (int)this.m_ShadowCascades;
				if (value == 2)
				{
					this.m_ShadowCascadeCount = 4;
				}
				else
				{
					this.m_ShadowCascadeCount = value + 1;
				}
				this.k_AssetVersion = 6;
			}
			if (this.k_AssetVersion < 7)
			{
				this.k_AssetPreviousVersion = this.k_AssetVersion;
				this.k_AssetVersion = 7;
			}
			if (this.k_AssetVersion < 8)
			{
				this.k_AssetPreviousVersion = this.k_AssetVersion;
				this.m_CascadeBorder = 0.1f;
				this.k_AssetVersion = 8;
			}
			if (this.k_AssetVersion < 9)
			{
				if (this.m_AdditionalLightsShadowResolutionTierHigh == UniversalRenderPipelineAsset.AdditionalLightsDefaultShadowResolutionTierHigh && this.m_AdditionalLightsShadowResolutionTierMedium == UniversalRenderPipelineAsset.AdditionalLightsDefaultShadowResolutionTierMedium && this.m_AdditionalLightsShadowResolutionTierLow == UniversalRenderPipelineAsset.AdditionalLightsDefaultShadowResolutionTierLow)
				{
					this.m_AdditionalLightsShadowResolutionTierHigh = (int)this.m_AdditionalLightsShadowmapResolution;
					this.m_AdditionalLightsShadowResolutionTierMedium = Mathf.Max(this.m_AdditionalLightsShadowResolutionTierHigh / 2, UniversalAdditionalLightData.AdditionalLightsShadowMinimumResolution);
					this.m_AdditionalLightsShadowResolutionTierLow = Mathf.Max(this.m_AdditionalLightsShadowResolutionTierMedium / 2, UniversalAdditionalLightData.AdditionalLightsShadowMinimumResolution);
				}
				this.k_AssetPreviousVersion = this.k_AssetVersion;
				this.k_AssetVersion = 9;
			}
			if (this.k_AssetVersion < 10)
			{
				this.k_AssetPreviousVersion = this.k_AssetVersion;
				this.k_AssetVersion = 10;
			}
			if (this.k_AssetVersion < 11)
			{
				this.k_AssetPreviousVersion = this.k_AssetVersion;
				this.k_AssetVersion = 11;
			}
			if (this.k_AssetVersion < 12)
			{
				this.k_AssetPreviousVersion = this.k_AssetVersion;
				this.k_AssetVersion = 12;
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003439 File Offset: 0x00001639
		private float ValidateShadowBias(float value)
		{
			return Mathf.Max(0f, Mathf.Min(value, UniversalRenderPipeline.maxShadowBias));
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003450 File Offset: 0x00001650
		private int ValidatePerObjectLights(int value)
		{
			return Math.Max(0, Math.Min(value, UniversalRenderPipeline.maxPerObjectLights));
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003463 File Offset: 0x00001663
		private float ValidateRenderScale(float value)
		{
			return Mathf.Max(UniversalRenderPipeline.minRenderScale, Mathf.Min(value, UniversalRenderPipeline.maxRenderScale));
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000347C File Offset: 0x0000167C
		internal bool ValidateRendererDataList(bool partial = false)
		{
			int emptyEntries = 0;
			for (int i = 0; i < this.m_RendererDataList.Length; i++)
			{
				emptyEntries += (this.ValidateRendererData(i) ? 0 : 1);
			}
			if (partial)
			{
				return emptyEntries == 0;
			}
			return emptyEntries != this.m_RendererDataList.Length;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000034C4 File Offset: 0x000016C4
		internal bool ValidateRendererData(int index)
		{
			if (index == -1)
			{
				index = this.m_DefaultRendererIndex;
			}
			return index < this.m_RendererDataList.Length && this.m_RendererDataList[index] != null;
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000CB RID: 203 RVA: 0x000034ED File Offset: 0x000016ED
		public bool supportProbeVolume
		{
			get
			{
				return this.lightProbeSystem == LightProbeSystem.ProbeVolumes;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000CC RID: 204 RVA: 0x000034F8 File Offset: 0x000016F8
		public ProbeVolumeSHBands maxSHBands
		{
			get
			{
				if (this.lightProbeSystem == LightProbeSystem.ProbeVolumes)
				{
					return this.probeVolumeSHBands;
				}
				return ProbeVolumeSHBands.SphericalHarmonicsL1;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000CD RID: 205 RVA: 0x0000278C File Offset: 0x0000098C
		[Obsolete("This property is no longer necessary.")]
		public ProbeVolumeSceneData probeVolumeSceneData
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000CE RID: 206 RVA: 0x0000350B File Offset: 0x0000170B
		public bool isStpUsed
		{
			get
			{
				return this.m_UpscalingFilter == UpscalingFilterSelection.STP;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00003516 File Offset: 0x00001716
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00003522 File Offset: 0x00001722
		[Obsolete("Use GraphicsSettings.GetRenderPipelineSettings<ShaderStrippingSetting>().shaderVariantLogLevel instead.", true)]
		public ShaderVariantLogLevel shaderVariantLogLevel
		{
			get
			{
				return (ShaderVariantLogLevel)GraphicsSettings.GetRenderPipelineSettings<ShaderStrippingSetting>().shaderVariantLogLevel;
			}
			set
			{
				GraphicsSettings.GetRenderPipelineSettings<ShaderStrippingSetting>().shaderVariantLogLevel = (ShaderVariantLogLevel)value;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00003530 File Offset: 0x00001730
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x0000356E File Offset: 0x0000176E
		[Obsolete("This is obsolete, please use shadowCascadeCount instead.", true)]
		public ShadowCascadesOption shadowCascadeOption
		{
			get
			{
				switch (this.shadowCascadeCount)
				{
				case 1:
					return ShadowCascadesOption.NoCascades;
				case 2:
					return ShadowCascadesOption.TwoCascades;
				case 4:
					return ShadowCascadesOption.FourCascades;
				}
				throw new InvalidOperationException("Cascade count is not compatible with obsolete API, please use shadowCascadeCount instead.");
			}
			set
			{
				switch (value)
				{
				case ShadowCascadesOption.NoCascades:
					this.shadowCascadeCount = 1;
					return;
				case ShadowCascadesOption.TwoCascades:
					this.shadowCascadeCount = 2;
					return;
				case ShadowCascadesOption.FourCascades:
					this.shadowCascadeCount = 4;
					return;
				default:
					throw new InvalidOperationException("Cascade count is not compatible with obsolete API, please use shadowCascadeCount instead.");
				}
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x000035A6 File Offset: 0x000017A6
		[Obsolete("Moved to UniversalRenderPipelineRuntimeTextures on GraphicsSettings. #from(2023.3)", false)]
		public UniversalRenderPipelineAsset.TextureResources textures
		{
			get
			{
				if (this.m_Textures == null)
				{
					this.m_Textures = new UniversalRenderPipelineAsset.TextureResources();
				}
				return this.m_Textures;
			}
		}

		// Token: 0x0400004B RID: 75
		private Shader m_DefaultShader;

		// Token: 0x0400004C RID: 76
		private ScriptableRenderer[] m_Renderers = new ScriptableRenderer[1];

		// Token: 0x0400004D RID: 77
		private const int k_LastVersion = 12;

		// Token: 0x0400004E RID: 78
		[SerializeField]
		private int k_AssetVersion = 12;

		// Token: 0x0400004F RID: 79
		[SerializeField]
		private int k_AssetPreviousVersion = 12;

		// Token: 0x04000050 RID: 80
		[SerializeField]
		private RendererType m_RendererType = RendererType.UniversalRenderer;

		// Token: 0x04000051 RID: 81
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use m_RendererDataList instead.")]
		[SerializeField]
		internal ScriptableRendererData m_RendererData;

		// Token: 0x04000052 RID: 82
		[SerializeField]
		internal ScriptableRendererData[] m_RendererDataList = new ScriptableRendererData[1];

		// Token: 0x04000053 RID: 83
		[SerializeField]
		internal int m_DefaultRendererIndex;

		// Token: 0x04000054 RID: 84
		[SerializeField]
		private bool m_RequireDepthTexture;

		// Token: 0x04000055 RID: 85
		[SerializeField]
		private bool m_RequireOpaqueTexture;

		// Token: 0x04000056 RID: 86
		[SerializeField]
		private Downsampling m_OpaqueDownsampling = Downsampling._2xBilinear;

		// Token: 0x04000057 RID: 87
		[SerializeField]
		private bool m_SupportsTerrainHoles = true;

		// Token: 0x04000058 RID: 88
		[SerializeField]
		private bool m_SupportsHDR = true;

		// Token: 0x04000059 RID: 89
		[SerializeField]
		private HDRColorBufferPrecision m_HDRColorBufferPrecision;

		// Token: 0x0400005A RID: 90
		[SerializeField]
		private MsaaQuality m_MSAA = MsaaQuality.Disabled;

		// Token: 0x0400005B RID: 91
		[SerializeField]
		private float m_RenderScale = 1f;

		// Token: 0x0400005C RID: 92
		[SerializeField]
		private UpscalingFilterSelection m_UpscalingFilter;

		// Token: 0x0400005D RID: 93
		[SerializeField]
		private bool m_FsrOverrideSharpness;

		// Token: 0x0400005E RID: 94
		[SerializeField]
		private float m_FsrSharpness = 0.92f;

		// Token: 0x0400005F RID: 95
		[SerializeField]
		private bool m_EnableLODCrossFade = true;

		// Token: 0x04000060 RID: 96
		[SerializeField]
		private LODCrossFadeDitheringType m_LODCrossFadeDitheringType = LODCrossFadeDitheringType.BlueNoise;

		// Token: 0x04000061 RID: 97
		[SerializeField]
		private ShEvalMode m_ShEvalMode;

		// Token: 0x04000062 RID: 98
		[SerializeField]
		private LightProbeSystem m_LightProbeSystem;

		// Token: 0x04000063 RID: 99
		[SerializeField]
		private ProbeVolumeTextureMemoryBudget m_ProbeVolumeMemoryBudget = ProbeVolumeTextureMemoryBudget.MemoryBudgetMedium;

		// Token: 0x04000064 RID: 100
		[SerializeField]
		private ProbeVolumeBlendingTextureMemoryBudget m_ProbeVolumeBlendingMemoryBudget = ProbeVolumeBlendingTextureMemoryBudget.MemoryBudgetMedium;

		// Token: 0x04000065 RID: 101
		[SerializeField]
		[FormerlySerializedAs("m_SupportProbeVolumeStreaming")]
		private bool m_SupportProbeVolumeGPUStreaming;

		// Token: 0x04000066 RID: 102
		[SerializeField]
		private bool m_SupportProbeVolumeDiskStreaming;

		// Token: 0x04000067 RID: 103
		[SerializeField]
		private bool m_SupportProbeVolumeScenarios;

		// Token: 0x04000068 RID: 104
		[SerializeField]
		private bool m_SupportProbeVolumeScenarioBlending;

		// Token: 0x04000069 RID: 105
		[SerializeField]
		private ProbeVolumeSHBands m_ProbeVolumeSHBands = ProbeVolumeSHBands.SphericalHarmonicsL1;

		// Token: 0x0400006A RID: 106
		[SerializeField]
		private LightRenderingMode m_MainLightRenderingMode = LightRenderingMode.PerPixel;

		// Token: 0x0400006B RID: 107
		[SerializeField]
		private bool m_MainLightShadowsSupported = true;

		// Token: 0x0400006C RID: 108
		[SerializeField]
		private ShadowResolution m_MainLightShadowmapResolution = ShadowResolution._2048;

		// Token: 0x0400006D RID: 109
		[SerializeField]
		private LightRenderingMode m_AdditionalLightsRenderingMode = LightRenderingMode.PerPixel;

		// Token: 0x0400006E RID: 110
		[SerializeField]
		private int m_AdditionalLightsPerObjectLimit = 4;

		// Token: 0x0400006F RID: 111
		[SerializeField]
		private bool m_AdditionalLightShadowsSupported;

		// Token: 0x04000070 RID: 112
		[SerializeField]
		private ShadowResolution m_AdditionalLightsShadowmapResolution = ShadowResolution._2048;

		// Token: 0x04000071 RID: 113
		[SerializeField]
		private int m_AdditionalLightsShadowResolutionTierLow = UniversalRenderPipelineAsset.AdditionalLightsDefaultShadowResolutionTierLow;

		// Token: 0x04000072 RID: 114
		[SerializeField]
		private int m_AdditionalLightsShadowResolutionTierMedium = UniversalRenderPipelineAsset.AdditionalLightsDefaultShadowResolutionTierMedium;

		// Token: 0x04000073 RID: 115
		[SerializeField]
		private int m_AdditionalLightsShadowResolutionTierHigh = UniversalRenderPipelineAsset.AdditionalLightsDefaultShadowResolutionTierHigh;

		// Token: 0x04000074 RID: 116
		[SerializeField]
		private bool m_ReflectionProbeBlending;

		// Token: 0x04000075 RID: 117
		[SerializeField]
		private bool m_ReflectionProbeBoxProjection;

		// Token: 0x04000076 RID: 118
		[SerializeField]
		private float m_ShadowDistance = 50f;

		// Token: 0x04000077 RID: 119
		[SerializeField]
		private int m_ShadowCascadeCount = 1;

		// Token: 0x04000078 RID: 120
		[SerializeField]
		private float m_Cascade2Split = 0.25f;

		// Token: 0x04000079 RID: 121
		[SerializeField]
		private Vector2 m_Cascade3Split = new Vector2(0.1f, 0.3f);

		// Token: 0x0400007A RID: 122
		[SerializeField]
		private Vector3 m_Cascade4Split = new Vector3(0.067f, 0.2f, 0.467f);

		// Token: 0x0400007B RID: 123
		[SerializeField]
		private float m_CascadeBorder = 0.2f;

		// Token: 0x0400007C RID: 124
		[SerializeField]
		private float m_ShadowDepthBias = 1f;

		// Token: 0x0400007D RID: 125
		[SerializeField]
		private float m_ShadowNormalBias = 1f;

		// Token: 0x0400007E RID: 126
		[SerializeField]
		private bool m_SoftShadowsSupported;

		// Token: 0x0400007F RID: 127
		[SerializeField]
		private bool m_ConservativeEnclosingSphere;

		// Token: 0x04000080 RID: 128
		[SerializeField]
		private int m_NumIterationsEnclosingSphere = 64;

		// Token: 0x04000081 RID: 129
		[SerializeField]
		private SoftShadowQuality m_SoftShadowQuality = SoftShadowQuality.Medium;

		// Token: 0x04000082 RID: 130
		[SerializeField]
		private LightCookieResolution m_AdditionalLightsCookieResolution = LightCookieResolution._2048;

		// Token: 0x04000083 RID: 131
		[SerializeField]
		private LightCookieFormat m_AdditionalLightsCookieFormat = LightCookieFormat.ColorHigh;

		// Token: 0x04000084 RID: 132
		[SerializeField]
		private bool m_UseSRPBatcher = true;

		// Token: 0x04000085 RID: 133
		[SerializeField]
		private bool m_SupportsDynamicBatching;

		// Token: 0x04000086 RID: 134
		[SerializeField]
		private bool m_MixedLightingSupported = true;

		// Token: 0x04000087 RID: 135
		[SerializeField]
		private bool m_SupportsLightCookies = true;

		// Token: 0x04000088 RID: 136
		[SerializeField]
		private bool m_SupportsLightLayers;

		// Token: 0x04000089 RID: 137
		[SerializeField]
		[Obsolete("", true)]
		private PipelineDebugLevel m_DebugLevel;

		// Token: 0x0400008A RID: 138
		[SerializeField]
		private StoreActionsOptimization m_StoreActionsOptimization;

		// Token: 0x0400008B RID: 139
		[SerializeField]
		private bool m_UseAdaptivePerformance = true;

		// Token: 0x0400008C RID: 140
		[SerializeField]
		private ColorGradingMode m_ColorGradingMode;

		// Token: 0x0400008D RID: 141
		[SerializeField]
		private int m_ColorGradingLutSize = 32;

		// Token: 0x0400008E RID: 142
		[SerializeField]
		private bool m_AllowPostProcessAlphaOutput;

		// Token: 0x0400008F RID: 143
		[SerializeField]
		private bool m_UseFastSRGBLinearConversion;

		// Token: 0x04000090 RID: 144
		[SerializeField]
		private bool m_SupportDataDrivenLensFlare = true;

		// Token: 0x04000091 RID: 145
		[SerializeField]
		private bool m_SupportScreenSpaceLensFlare = true;

		// Token: 0x04000092 RID: 146
		[FormerlySerializedAs("m_MacroBatcherMode")]
		[SerializeField]
		private GPUResidentDrawerMode m_GPUResidentDrawerMode;

		// Token: 0x04000093 RID: 147
		[SerializeField]
		private float m_SmallMeshScreenPercentage;

		// Token: 0x04000094 RID: 148
		[SerializeField]
		private bool m_GPUResidentDrawerEnableOcclusionCullingInCameras;

		// Token: 0x04000095 RID: 149
		[SerializeField]
		private ShadowQuality m_ShadowType = ShadowQuality.HardShadows;

		// Token: 0x04000096 RID: 150
		[SerializeField]
		private bool m_LocalShadowsSupported;

		// Token: 0x04000097 RID: 151
		[SerializeField]
		private ShadowResolution m_LocalShadowsAtlasResolution = ShadowResolution._256;

		// Token: 0x04000098 RID: 152
		[SerializeField]
		private int m_MaxPixelLights;

		// Token: 0x04000099 RID: 153
		[SerializeField]
		private ShadowResolution m_ShadowAtlasResolution = ShadowResolution._256;

		// Token: 0x0400009A RID: 154
		[SerializeField]
		private VolumeFrameworkUpdateMode m_VolumeFrameworkUpdateMode;

		// Token: 0x0400009B RID: 155
		[SerializeField]
		private VolumeProfile m_VolumeProfile;

		// Token: 0x0400009C RID: 156
		public const int k_MinLutSize = 16;

		// Token: 0x0400009D RID: 157
		public const int k_MaxLutSize = 65;

		// Token: 0x0400009E RID: 158
		internal const int k_ShadowCascadeMinCount = 1;

		// Token: 0x0400009F RID: 159
		internal const int k_ShadowCascadeMaxCount = 4;

		// Token: 0x040000A0 RID: 160
		public static readonly int AdditionalLightsDefaultShadowResolutionTierLow = 256;

		// Token: 0x040000A1 RID: 161
		public static readonly int AdditionalLightsDefaultShadowResolutionTierMedium = 512;

		// Token: 0x040000A2 RID: 162
		public static readonly int AdditionalLightsDefaultShadowResolutionTierHigh = 1024;

		// Token: 0x040000A3 RID: 163
		private static string[] s_Names;

		// Token: 0x040000A4 RID: 164
		private static int[] s_Values;

		// Token: 0x040000A5 RID: 165
		private static GraphicsFormat[][] s_LightCookieFormatList = new GraphicsFormat[][]
		{
			new GraphicsFormat[] { GraphicsFormat.R8_UNorm },
			new GraphicsFormat[] { GraphicsFormat.R16_UNorm },
			new GraphicsFormat[]
			{
				GraphicsFormat.R5G6B5_UNormPack16,
				GraphicsFormat.B5G6R5_UNormPack16,
				GraphicsFormat.R5G5B5A1_UNormPack16,
				GraphicsFormat.B5G5R5A1_UNormPack16
			},
			new GraphicsFormat[]
			{
				GraphicsFormat.A2B10G10R10_UNormPack32,
				GraphicsFormat.R8G8B8A8_SRGB,
				GraphicsFormat.B8G8R8A8_SRGB
			},
			new GraphicsFormat[] { GraphicsFormat.B10G11R11_UFloatPack32 }
		};

		// Token: 0x040000A6 RID: 166
		[SerializeField]
		[Obsolete("Kept for migration. #from(2023.3")]
		internal ProbeVolumeSceneData apvScenesData;

		// Token: 0x040000A7 RID: 167
		[SerializeField]
		private int m_ShaderVariantLogLevel;

		// Token: 0x040000A8 RID: 168
		[Obsolete("This is obsolete, please use shadowCascadeCount instead.", false)]
		[SerializeField]
		private ShadowCascadesOption m_ShadowCascades;

		// Token: 0x040000A9 RID: 169
		[Obsolete("Moved to UniversalRenderPipelineRuntimeTextures on GraphicsSettings. #from(2023.3)", false)]
		[SerializeField]
		private UniversalRenderPipelineAsset.TextureResources m_Textures;

		// Token: 0x02000016 RID: 22
		private static class Strings
		{
			// Token: 0x040000AA RID: 170
			public static readonly string notURPRenderer = "GPUResidentDrawer Disabled due to some configured Universal Renderers not being UniversalRendererData.";

			// Token: 0x040000AB RID: 171
			public static readonly string forwardPlusMissing = "GPUResidentDrawer Disabled due to some configured Universal Renderers not supporting Forward+.";
		}

		// Token: 0x02000017 RID: 23
		[ReloadGroup]
		[Obsolete("Moved to UniversalRenderPipelineRuntimeTextures on GraphicsSettings. #from(2023.3)", false)]
		[Serializable]
		public sealed class TextureResources
		{
			// Token: 0x060000D7 RID: 215 RVA: 0x00003831 File Offset: 0x00001A31
			public bool NeedsReload()
			{
				return this.blueNoise64LTex == null || this.bayerMatrixTex == null;
			}

			// Token: 0x040000AC RID: 172
			[Reload("Textures/BlueNoise64/L/LDR_LLL1_0.png", ReloadAttribute.Package.Root)]
			public Texture2D blueNoise64LTex;

			// Token: 0x040000AD RID: 173
			[Reload("Textures/BayerMatrix.png", ReloadAttribute.Package.Root)]
			public Texture2D bayerMatrixTex;
		}
	}
}
