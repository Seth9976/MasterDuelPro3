using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020001D2 RID: 466
	public static class ShaderKeywordStrings
	{
		// Token: 0x04000AA6 RID: 2726
		public const string MainLightShadows = "_MAIN_LIGHT_SHADOWS";

		// Token: 0x04000AA7 RID: 2727
		public const string MainLightShadowCascades = "_MAIN_LIGHT_SHADOWS_CASCADE";

		// Token: 0x04000AA8 RID: 2728
		public const string MainLightShadowScreen = "_MAIN_LIGHT_SHADOWS_SCREEN";

		// Token: 0x04000AA9 RID: 2729
		public const string CastingPunctualLightShadow = "_CASTING_PUNCTUAL_LIGHT_SHADOW";

		// Token: 0x04000AAA RID: 2730
		public const string AdditionalLightsVertex = "_ADDITIONAL_LIGHTS_VERTEX";

		// Token: 0x04000AAB RID: 2731
		public const string AdditionalLightsPixel = "_ADDITIONAL_LIGHTS";

		// Token: 0x04000AAC RID: 2732
		internal const string ForwardPlus = "_FORWARD_PLUS";

		// Token: 0x04000AAD RID: 2733
		public const string AdditionalLightShadows = "_ADDITIONAL_LIGHT_SHADOWS";

		// Token: 0x04000AAE RID: 2734
		public const string ReflectionProbeBoxProjection = "_REFLECTION_PROBE_BOX_PROJECTION";

		// Token: 0x04000AAF RID: 2735
		public const string ReflectionProbeBlending = "_REFLECTION_PROBE_BLENDING";

		// Token: 0x04000AB0 RID: 2736
		public const string SoftShadows = "_SHADOWS_SOFT";

		// Token: 0x04000AB1 RID: 2737
		public const string SoftShadowsLow = "_SHADOWS_SOFT_LOW";

		// Token: 0x04000AB2 RID: 2738
		public const string SoftShadowsMedium = "_SHADOWS_SOFT_MEDIUM";

		// Token: 0x04000AB3 RID: 2739
		public const string SoftShadowsHigh = "_SHADOWS_SOFT_HIGH";

		// Token: 0x04000AB4 RID: 2740
		public const string MixedLightingSubtractive = "_MIXED_LIGHTING_SUBTRACTIVE";

		// Token: 0x04000AB5 RID: 2741
		public const string LightmapShadowMixing = "LIGHTMAP_SHADOW_MIXING";

		// Token: 0x04000AB6 RID: 2742
		public const string ShadowsShadowMask = "SHADOWS_SHADOWMASK";

		// Token: 0x04000AB7 RID: 2743
		public const string LightLayers = "_LIGHT_LAYERS";

		// Token: 0x04000AB8 RID: 2744
		public const string RenderPassEnabled = "_RENDER_PASS_ENABLED";

		// Token: 0x04000AB9 RID: 2745
		public const string BillboardFaceCameraPos = "BILLBOARD_FACE_CAMERA_POS";

		// Token: 0x04000ABA RID: 2746
		public const string LightCookies = "_LIGHT_COOKIES";

		// Token: 0x04000ABB RID: 2747
		public const string DepthNoMsaa = "_DEPTH_NO_MSAA";

		// Token: 0x04000ABC RID: 2748
		public const string DepthMsaa2 = "_DEPTH_MSAA_2";

		// Token: 0x04000ABD RID: 2749
		public const string DepthMsaa4 = "_DEPTH_MSAA_4";

		// Token: 0x04000ABE RID: 2750
		public const string DepthMsaa8 = "_DEPTH_MSAA_8";

		// Token: 0x04000ABF RID: 2751
		public const string LinearToSRGBConversion = "_LINEAR_TO_SRGB_CONVERSION";

		// Token: 0x04000AC0 RID: 2752
		internal const string UseFastSRGBLinearConversion = "_USE_FAST_SRGB_LINEAR_CONVERSION";

		// Token: 0x04000AC1 RID: 2753
		public const string DBufferMRT1 = "_DBUFFER_MRT1";

		// Token: 0x04000AC2 RID: 2754
		public const string DBufferMRT2 = "_DBUFFER_MRT2";

		// Token: 0x04000AC3 RID: 2755
		public const string DBufferMRT3 = "_DBUFFER_MRT3";

		// Token: 0x04000AC4 RID: 2756
		public const string DecalNormalBlendLow = "_DECAL_NORMAL_BLEND_LOW";

		// Token: 0x04000AC5 RID: 2757
		public const string DecalNormalBlendMedium = "_DECAL_NORMAL_BLEND_MEDIUM";

		// Token: 0x04000AC6 RID: 2758
		public const string DecalNormalBlendHigh = "_DECAL_NORMAL_BLEND_HIGH";

		// Token: 0x04000AC7 RID: 2759
		public const string DecalLayers = "_DECAL_LAYERS";

		// Token: 0x04000AC8 RID: 2760
		public const string WriteRenderingLayers = "_WRITE_RENDERING_LAYERS";

		// Token: 0x04000AC9 RID: 2761
		public const string SmaaLow = "_SMAA_PRESET_LOW";

		// Token: 0x04000ACA RID: 2762
		public const string SmaaMedium = "_SMAA_PRESET_MEDIUM";

		// Token: 0x04000ACB RID: 2763
		public const string SmaaHigh = "_SMAA_PRESET_HIGH";

		// Token: 0x04000ACC RID: 2764
		public const string PaniniGeneric = "_GENERIC";

		// Token: 0x04000ACD RID: 2765
		public const string PaniniUnitDistance = "_UNIT_DISTANCE";

		// Token: 0x04000ACE RID: 2766
		public const string BloomLQ = "_BLOOM_LQ";

		// Token: 0x04000ACF RID: 2767
		public const string BloomHQ = "_BLOOM_HQ";

		// Token: 0x04000AD0 RID: 2768
		public const string BloomLQDirt = "_BLOOM_LQ_DIRT";

		// Token: 0x04000AD1 RID: 2769
		public const string BloomHQDirt = "_BLOOM_HQ_DIRT";

		// Token: 0x04000AD2 RID: 2770
		public const string Distortion = "_DISTORTION";

		// Token: 0x04000AD3 RID: 2771
		public const string ChromaticAberration = "_CHROMATIC_ABERRATION";

		// Token: 0x04000AD4 RID: 2772
		public const string HDRGrading = "_HDR_GRADING";

		// Token: 0x04000AD5 RID: 2773
		public const string HDROverlay = "_HDR_OVERLAY";

		// Token: 0x04000AD6 RID: 2774
		public const string TonemapACES = "_TONEMAP_ACES";

		// Token: 0x04000AD7 RID: 2775
		public const string TonemapNeutral = "_TONEMAP_NEUTRAL";

		// Token: 0x04000AD8 RID: 2776
		public const string FilmGrain = "_FILM_GRAIN";

		// Token: 0x04000AD9 RID: 2777
		public const string Fxaa = "_FXAA";

		// Token: 0x04000ADA RID: 2778
		public const string Dithering = "_DITHERING";

		// Token: 0x04000ADB RID: 2779
		public const string ScreenSpaceOcclusion = "_SCREEN_SPACE_OCCLUSION";

		// Token: 0x04000ADC RID: 2780
		public const string PointSampling = "_POINT_SAMPLING";

		// Token: 0x04000ADD RID: 2781
		public const string Rcas = "_RCAS";

		// Token: 0x04000ADE RID: 2782
		public const string EasuRcasAndHDRInput = "_EASU_RCAS_AND_HDR_INPUT";

		// Token: 0x04000ADF RID: 2783
		public const string Gamma20 = "_GAMMA_20";

		// Token: 0x04000AE0 RID: 2784
		public const string Gamma20AndHDRInput = "_GAMMA_20_AND_HDR_INPUT";

		// Token: 0x04000AE1 RID: 2785
		public const string HighQualitySampling = "_HIGH_QUALITY_SAMPLING";

		// Token: 0x04000AE2 RID: 2786
		public const string _SPOT = "_SPOT";

		// Token: 0x04000AE3 RID: 2787
		public const string _DIRECTIONAL = "_DIRECTIONAL";

		// Token: 0x04000AE4 RID: 2788
		public const string _POINT = "_POINT";

		// Token: 0x04000AE5 RID: 2789
		public const string _DEFERRED_STENCIL = "_DEFERRED_STENCIL";

		// Token: 0x04000AE6 RID: 2790
		public const string _DEFERRED_FIRST_LIGHT = "_DEFERRED_FIRST_LIGHT";

		// Token: 0x04000AE7 RID: 2791
		public const string _DEFERRED_MAIN_LIGHT = "_DEFERRED_MAIN_LIGHT";

		// Token: 0x04000AE8 RID: 2792
		public const string _GBUFFER_NORMALS_OCT = "_GBUFFER_NORMALS_OCT";

		// Token: 0x04000AE9 RID: 2793
		public const string _DEFERRED_MIXED_LIGHTING = "_DEFERRED_MIXED_LIGHTING";

		// Token: 0x04000AEA RID: 2794
		public const string LIGHTMAP_ON = "LIGHTMAP_ON";

		// Token: 0x04000AEB RID: 2795
		public const string DYNAMICLIGHTMAP_ON = "DYNAMICLIGHTMAP_ON";

		// Token: 0x04000AEC RID: 2796
		public const string _ALPHATEST_ON = "_ALPHATEST_ON";

		// Token: 0x04000AED RID: 2797
		public const string DIRLIGHTMAP_COMBINED = "DIRLIGHTMAP_COMBINED";

		// Token: 0x04000AEE RID: 2798
		public const string _DETAIL_MULX2 = "_DETAIL_MULX2";

		// Token: 0x04000AEF RID: 2799
		public const string _DETAIL_SCALED = "_DETAIL_SCALED";

		// Token: 0x04000AF0 RID: 2800
		public const string _CLEARCOAT = "_CLEARCOAT";

		// Token: 0x04000AF1 RID: 2801
		public const string _CLEARCOATMAP = "_CLEARCOATMAP";

		// Token: 0x04000AF2 RID: 2802
		public const string DEBUG_DISPLAY = "DEBUG_DISPLAY";

		// Token: 0x04000AF3 RID: 2803
		public const string LOD_FADE_CROSSFADE = "LOD_FADE_CROSSFADE";

		// Token: 0x04000AF4 RID: 2804
		public const string USE_UNITY_CROSSFADE = "USE_UNITY_CROSSFADE";

		// Token: 0x04000AF5 RID: 2805
		public const string _EMISSION = "_EMISSION";

		// Token: 0x04000AF6 RID: 2806
		public const string _RECEIVE_SHADOWS_OFF = "_RECEIVE_SHADOWS_OFF";

		// Token: 0x04000AF7 RID: 2807
		public const string _SURFACE_TYPE_TRANSPARENT = "_SURFACE_TYPE_TRANSPARENT";

		// Token: 0x04000AF8 RID: 2808
		public const string _ALPHAPREMULTIPLY_ON = "_ALPHAPREMULTIPLY_ON";

		// Token: 0x04000AF9 RID: 2809
		public const string _ALPHAMODULATE_ON = "_ALPHAMODULATE_ON";

		// Token: 0x04000AFA RID: 2810
		public const string _NORMALMAP = "_NORMALMAP";

		// Token: 0x04000AFB RID: 2811
		public const string _ADD_PRECOMPUTED_VELOCITY = "_ADD_PRECOMPUTED_VELOCITY";

		// Token: 0x04000AFC RID: 2812
		public const string EDITOR_VISUALIZATION = "EDITOR_VISUALIZATION";

		// Token: 0x04000AFD RID: 2813
		public const string FoveatedRenderingNonUniformRaster = "_FOVEATED_RENDERING_NON_UNIFORM_RASTER";

		// Token: 0x04000AFE RID: 2814
		public const string DisableTexture2DXArray = "DISABLE_TEXTURE2D_X_ARRAY";

		// Token: 0x04000AFF RID: 2815
		public const string BlitSingleSlice = "BLIT_SINGLE_SLICE";

		// Token: 0x04000B00 RID: 2816
		public const string XROcclusionMeshCombined = "XR_OCCLUSION_MESH_COMBINED";

		// Token: 0x04000B01 RID: 2817
		public const string SCREEN_COORD_OVERRIDE = "SCREEN_COORD_OVERRIDE";

		// Token: 0x04000B02 RID: 2818
		public const string DOWNSAMPLING_SIZE_2 = "DOWNSAMPLING_SIZE_2";

		// Token: 0x04000B03 RID: 2819
		public const string DOWNSAMPLING_SIZE_4 = "DOWNSAMPLING_SIZE_4";

		// Token: 0x04000B04 RID: 2820
		public const string DOWNSAMPLING_SIZE_8 = "DOWNSAMPLING_SIZE_8";

		// Token: 0x04000B05 RID: 2821
		public const string DOWNSAMPLING_SIZE_16 = "DOWNSAMPLING_SIZE_16";

		// Token: 0x04000B06 RID: 2822
		public const string EVALUATE_SH_MIXED = "EVALUATE_SH_MIXED";

		// Token: 0x04000B07 RID: 2823
		public const string EVALUATE_SH_VERTEX = "EVALUATE_SH_VERTEX";

		// Token: 0x04000B08 RID: 2824
		public const string ProbeVolumeL1 = "PROBE_VOLUMES_L1";

		// Token: 0x04000B09 RID: 2825
		public const string ProbeVolumeL2 = "PROBE_VOLUMES_L2";

		// Token: 0x04000B0A RID: 2826
		public const string USE_LEGACY_LIGHTMAPS = "USE_LEGACY_LIGHTMAPS";

		// Token: 0x04000B0B RID: 2827
		public const string _OUTPUT_DEPTH = "_OUTPUT_DEPTH";

		// Token: 0x04000B0C RID: 2828
		public const string _ENABLE_ALPHA_OUTPUT = "_ENABLE_ALPHA_OUTPUT";
	}
}
