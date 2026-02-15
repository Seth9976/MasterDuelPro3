using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000BD RID: 189
	[DisplayInfo(name = "URP Global Settings Asset", order = 40002)]
	[SupportedOnRenderPipeline(typeof(UniversalRenderPipelineAsset))]
	[DisplayName("URP")]
	internal class UniversalRenderPipelineGlobalSettings : RenderPipelineGlobalSettings<UniversalRenderPipelineGlobalSettings, UniversalRenderPipeline>
	{
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x00002886 File Offset: 0x00000A86
		// (set) Token: 0x06000491 RID: 1169 RVA: 0x0000217F File Offset: 0x0000037F
		[Obsolete("No longer used as Shader Prefiltering automatically strips out unused LOD Crossfade variants. Please use the LOD Crossfade setting in the URP Asset to disable the feature if not used. #from(2023.1)", false)]
		public bool stripUnusedLODCrossFadeVariants
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x000123A2 File Offset: 0x000105A2
		protected override List<IRenderPipelineGraphicsSettings> settingsList
		{
			get
			{
				return this.m_Settings.settingsList;
			}
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x000123AF File Offset: 0x000105AF
		internal bool IsAtLastVersion()
		{
			return 8 == this.m_AssetVersion;
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x000123BA File Offset: 0x000105BA
		public override void Reset()
		{
			base.Reset();
			DecalProjector.UpdateAllDecalProperties();
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x000123C7 File Offset: 0x000105C7
		internal static VolumeProfile GetOrCreateDefaultVolumeProfile(VolumeProfile defaultVolumeProfile)
		{
			return defaultVolumeProfile;
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x00003155 File Offset: 0x00001355
		[Obsolete("This is obsolete, please use prefixedRenderingLayerMaskNames instead.", true)]
		public string[] prefixedLightLayerNames
		{
			get
			{
				return new string[0];
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x00003155 File Offset: 0x00001355
		[Obsolete("This is obsolete, please use renderingLayerMaskNames instead.", false)]
		public string[] lightLayerNames
		{
			get
			{
				return new string[0];
			}
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x000123CA File Offset: 0x000105CA
		internal void ResetRenderingLayerNames()
		{
			this.m_RenderingLayerNames = new string[] { "Default" };
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x000123E0 File Offset: 0x000105E0
		internal ProbeVolumeSceneData GetOrCreateAPVSceneData()
		{
			if (this.apvScenesData == null)
			{
				this.apvScenesData = new ProbeVolumeSceneData(this);
			}
			this.apvScenesData.SetParentObject(this);
			return this.apvScenesData;
		}

		// Token: 0x040003D0 RID: 976
		[SerializeField]
		[Obsolete("Keep for migration. #from(23.2)")]
		internal ShaderStrippingSetting m_ShaderStrippingSetting = new ShaderStrippingSetting();

		// Token: 0x040003D1 RID: 977
		[SerializeField]
		[Obsolete("Keep for migration. #from(23.2)")]
		internal URPShaderStrippingSetting m_URPShaderStrippingSetting = new URPShaderStrippingSetting();

		// Token: 0x040003D2 RID: 978
		[SerializeField]
		[Obsolete("Keep for migration. #from(23.2)")]
		internal ShaderVariantLogLevel m_ShaderVariantLogLevel;

		// Token: 0x040003D3 RID: 979
		[SerializeField]
		[Obsolete("Keep for migration. #from(23.2)")]
		internal bool m_ExportShaderVariants = true;

		// Token: 0x040003D4 RID: 980
		[SerializeField]
		[Obsolete("Keep for migration. #from(23.2)")]
		internal bool m_StripDebugVariants = true;

		// Token: 0x040003D5 RID: 981
		[SerializeField]
		[Obsolete("Keep for migration. #from(23.2)")]
		internal bool m_StripUnusedPostProcessingVariants;

		// Token: 0x040003D6 RID: 982
		[SerializeField]
		[Obsolete("Keep for migration. #from(23.2)")]
		internal bool m_StripUnusedVariants = true;

		// Token: 0x040003D7 RID: 983
		[SerializeField]
		[Obsolete("Keep for migration. #from(23.2)")]
		internal bool m_StripScreenCoordOverrideVariants = true;

		// Token: 0x040003D8 RID: 984
		[Obsolete("Please use stripRuntimeDebugShaders instead. #from(23.1)", false)]
		public bool supportRuntimeDebugDisplay;

		// Token: 0x040003D9 RID: 985
		[SerializeField]
		[Obsolete("Keep for migration. #from(23.2)")]
		internal bool m_EnableRenderGraph;

		// Token: 0x040003DA RID: 986
		[SerializeField]
		private RenderPipelineGraphicsSettingsContainer m_Settings = new RenderPipelineGraphicsSettingsContainer();

		// Token: 0x040003DB RID: 987
		internal const int k_LastVersion = 8;

		// Token: 0x040003DC RID: 988
		[SerializeField]
		[FormerlySerializedAs("k_AssetVersion")]
		internal int m_AssetVersion = 8;

		// Token: 0x040003DD RID: 989
		public const string defaultAssetName = "UniversalRenderPipelineGlobalSettings";

		// Token: 0x040003DE RID: 990
		[SerializeField]
		[FormerlySerializedAs("m_DefaultVolumeProfile")]
		[Obsolete("Kept For Migration. #from(2023.3)")]
		internal VolumeProfile m_ObsoleteDefaultVolumeProfile;

		// Token: 0x040003DF RID: 991
		[SerializeField]
		internal string[] m_RenderingLayerNames = new string[] { "Default" };

		// Token: 0x040003E0 RID: 992
		[SerializeField]
		private uint m_ValidRenderingLayers;

		// Token: 0x040003E1 RID: 993
		[Obsolete("This is obsolete, please use renderingLayerMaskNames instead.", false)]
		public string lightLayerName0;

		// Token: 0x040003E2 RID: 994
		[Obsolete("This is obsolete, please use renderingLayerMaskNames instead.", false)]
		public string lightLayerName1;

		// Token: 0x040003E3 RID: 995
		[Obsolete("This is obsolete, please use renderingLayerMaskNames instead.", false)]
		public string lightLayerName2;

		// Token: 0x040003E4 RID: 996
		[Obsolete("This is obsolete, please use renderingLayerMaskNames instead.", false)]
		public string lightLayerName3;

		// Token: 0x040003E5 RID: 997
		[Obsolete("This is obsolete, please use renderingLayerMaskNames instead.", false)]
		public string lightLayerName4;

		// Token: 0x040003E6 RID: 998
		[Obsolete("This is obsolete, please use renderingLayerMaskNames instead.", false)]
		public string lightLayerName5;

		// Token: 0x040003E7 RID: 999
		[Obsolete("This is obsolete, please use renderingLayerMaskNames instead.", false)]
		public string lightLayerName6;

		// Token: 0x040003E8 RID: 1000
		[Obsolete("This is obsolete, please use renderingLayerNames instead.", false)]
		public string lightLayerName7;

		// Token: 0x040003E9 RID: 1001
		[SerializeField]
		internal ProbeVolumeSceneData apvScenesData;
	}
}
