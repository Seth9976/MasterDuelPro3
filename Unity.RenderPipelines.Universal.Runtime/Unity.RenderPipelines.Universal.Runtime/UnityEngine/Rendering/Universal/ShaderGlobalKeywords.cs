using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020001D1 RID: 465
	internal static class ShaderGlobalKeywords
	{
		// Token: 0x06000A54 RID: 2644 RVA: 0x00033120 File Offset: 0x00031320
		public static void InitializeShaderGlobalKeywords()
		{
			ShaderGlobalKeywords.MainLightShadows = GlobalKeyword.Create("_MAIN_LIGHT_SHADOWS");
			ShaderGlobalKeywords.MainLightShadowCascades = GlobalKeyword.Create("_MAIN_LIGHT_SHADOWS_CASCADE");
			ShaderGlobalKeywords.MainLightShadowScreen = GlobalKeyword.Create("_MAIN_LIGHT_SHADOWS_SCREEN");
			ShaderGlobalKeywords.CastingPunctualLightShadow = GlobalKeyword.Create("_CASTING_PUNCTUAL_LIGHT_SHADOW");
			ShaderGlobalKeywords.AdditionalLightsVertex = GlobalKeyword.Create("_ADDITIONAL_LIGHTS_VERTEX");
			ShaderGlobalKeywords.AdditionalLightsPixel = GlobalKeyword.Create("_ADDITIONAL_LIGHTS");
			ShaderGlobalKeywords.ForwardPlus = GlobalKeyword.Create("_FORWARD_PLUS");
			ShaderGlobalKeywords.AdditionalLightShadows = GlobalKeyword.Create("_ADDITIONAL_LIGHT_SHADOWS");
			ShaderGlobalKeywords.ReflectionProbeBoxProjection = GlobalKeyword.Create("_REFLECTION_PROBE_BOX_PROJECTION");
			ShaderGlobalKeywords.ReflectionProbeBlending = GlobalKeyword.Create("_REFLECTION_PROBE_BLENDING");
			ShaderGlobalKeywords.SoftShadows = GlobalKeyword.Create("_SHADOWS_SOFT");
			ShaderGlobalKeywords.SoftShadowsLow = GlobalKeyword.Create("_SHADOWS_SOFT_LOW");
			ShaderGlobalKeywords.SoftShadowsMedium = GlobalKeyword.Create("_SHADOWS_SOFT_MEDIUM");
			ShaderGlobalKeywords.MixedLightingSubtractive = GlobalKeyword.Create("_MIXED_LIGHTING_SUBTRACTIVE");
			ShaderGlobalKeywords.LightmapShadowMixing = GlobalKeyword.Create("LIGHTMAP_SHADOW_MIXING");
			ShaderGlobalKeywords.ShadowsShadowMask = GlobalKeyword.Create("SHADOWS_SHADOWMASK");
			ShaderGlobalKeywords.LightLayers = GlobalKeyword.Create("_LIGHT_LAYERS");
			ShaderGlobalKeywords.RenderPassEnabled = GlobalKeyword.Create("_RENDER_PASS_ENABLED");
			ShaderGlobalKeywords.BillboardFaceCameraPos = GlobalKeyword.Create("BILLBOARD_FACE_CAMERA_POS");
			ShaderGlobalKeywords.LightCookies = GlobalKeyword.Create("_LIGHT_COOKIES");
			ShaderGlobalKeywords.DepthNoMsaa = GlobalKeyword.Create("_DEPTH_NO_MSAA");
			ShaderGlobalKeywords.DepthMsaa2 = GlobalKeyword.Create("_DEPTH_MSAA_2");
			ShaderGlobalKeywords.DepthMsaa4 = GlobalKeyword.Create("_DEPTH_MSAA_4");
			ShaderGlobalKeywords.DepthMsaa8 = GlobalKeyword.Create("_DEPTH_MSAA_8");
			ShaderGlobalKeywords.DBufferMRT1 = GlobalKeyword.Create("_DBUFFER_MRT1");
			ShaderGlobalKeywords.DBufferMRT2 = GlobalKeyword.Create("_DBUFFER_MRT2");
			ShaderGlobalKeywords.DBufferMRT3 = GlobalKeyword.Create("_DBUFFER_MRT3");
			ShaderGlobalKeywords.DecalNormalBlendLow = GlobalKeyword.Create("_DECAL_NORMAL_BLEND_LOW");
			ShaderGlobalKeywords.DecalNormalBlendMedium = GlobalKeyword.Create("_DECAL_NORMAL_BLEND_MEDIUM");
			ShaderGlobalKeywords.DecalNormalBlendHigh = GlobalKeyword.Create("_DECAL_NORMAL_BLEND_HIGH");
			ShaderGlobalKeywords.DecalLayers = GlobalKeyword.Create("_DECAL_LAYERS");
			ShaderGlobalKeywords.WriteRenderingLayers = GlobalKeyword.Create("_WRITE_RENDERING_LAYERS");
			ShaderGlobalKeywords.ScreenSpaceOcclusion = GlobalKeyword.Create("_SCREEN_SPACE_OCCLUSION");
			ShaderGlobalKeywords._SPOT = GlobalKeyword.Create("_SPOT");
			ShaderGlobalKeywords._DIRECTIONAL = GlobalKeyword.Create("_DIRECTIONAL");
			ShaderGlobalKeywords._POINT = GlobalKeyword.Create("_POINT");
			ShaderGlobalKeywords._DEFERRED_STENCIL = GlobalKeyword.Create("_DEFERRED_STENCIL");
			ShaderGlobalKeywords._DEFERRED_FIRST_LIGHT = GlobalKeyword.Create("_DEFERRED_FIRST_LIGHT");
			ShaderGlobalKeywords._DEFERRED_MAIN_LIGHT = GlobalKeyword.Create("_DEFERRED_MAIN_LIGHT");
			ShaderGlobalKeywords._GBUFFER_NORMALS_OCT = GlobalKeyword.Create("_GBUFFER_NORMALS_OCT");
			ShaderGlobalKeywords._DEFERRED_MIXED_LIGHTING = GlobalKeyword.Create("_DEFERRED_MIXED_LIGHTING");
			ShaderGlobalKeywords.LIGHTMAP_ON = GlobalKeyword.Create("LIGHTMAP_ON");
			ShaderGlobalKeywords.DYNAMICLIGHTMAP_ON = GlobalKeyword.Create("DYNAMICLIGHTMAP_ON");
			ShaderGlobalKeywords._ALPHATEST_ON = GlobalKeyword.Create("_ALPHATEST_ON");
			ShaderGlobalKeywords.DIRLIGHTMAP_COMBINED = GlobalKeyword.Create("DIRLIGHTMAP_COMBINED");
			ShaderGlobalKeywords._DETAIL_MULX2 = GlobalKeyword.Create("_DETAIL_MULX2");
			ShaderGlobalKeywords._DETAIL_SCALED = GlobalKeyword.Create("_DETAIL_SCALED");
			ShaderGlobalKeywords._CLEARCOAT = GlobalKeyword.Create("_CLEARCOAT");
			ShaderGlobalKeywords._CLEARCOATMAP = GlobalKeyword.Create("_CLEARCOATMAP");
			ShaderGlobalKeywords.DEBUG_DISPLAY = GlobalKeyword.Create("DEBUG_DISPLAY");
			ShaderGlobalKeywords.LOD_FADE_CROSSFADE = GlobalKeyword.Create("LOD_FADE_CROSSFADE");
			ShaderGlobalKeywords.USE_UNITY_CROSSFADE = GlobalKeyword.Create("USE_UNITY_CROSSFADE");
			ShaderGlobalKeywords._EMISSION = GlobalKeyword.Create("_EMISSION");
			ShaderGlobalKeywords._RECEIVE_SHADOWS_OFF = GlobalKeyword.Create("_RECEIVE_SHADOWS_OFF");
			ShaderGlobalKeywords._SURFACE_TYPE_TRANSPARENT = GlobalKeyword.Create("_SURFACE_TYPE_TRANSPARENT");
			ShaderGlobalKeywords._ALPHAPREMULTIPLY_ON = GlobalKeyword.Create("_ALPHAPREMULTIPLY_ON");
			ShaderGlobalKeywords._ALPHAMODULATE_ON = GlobalKeyword.Create("_ALPHAMODULATE_ON");
			ShaderGlobalKeywords._NORMALMAP = GlobalKeyword.Create("_NORMALMAP");
			ShaderGlobalKeywords._ADD_PRECOMPUTED_VELOCITY = GlobalKeyword.Create("_ADD_PRECOMPUTED_VELOCITY");
			ShaderGlobalKeywords.EDITOR_VISUALIZATION = GlobalKeyword.Create("EDITOR_VISUALIZATION");
			ShaderGlobalKeywords.FoveatedRenderingNonUniformRaster = GlobalKeyword.Create("_FOVEATED_RENDERING_NON_UNIFORM_RASTER");
			ShaderGlobalKeywords.DisableTexture2DXArray = GlobalKeyword.Create("DISABLE_TEXTURE2D_X_ARRAY");
			ShaderGlobalKeywords.BlitSingleSlice = GlobalKeyword.Create("BLIT_SINGLE_SLICE");
			ShaderGlobalKeywords.XROcclusionMeshCombined = GlobalKeyword.Create("XR_OCCLUSION_MESH_COMBINED");
			ShaderGlobalKeywords.SCREEN_COORD_OVERRIDE = GlobalKeyword.Create("SCREEN_COORD_OVERRIDE");
			ShaderGlobalKeywords.DOWNSAMPLING_SIZE_2 = GlobalKeyword.Create("DOWNSAMPLING_SIZE_2");
			ShaderGlobalKeywords.DOWNSAMPLING_SIZE_4 = GlobalKeyword.Create("DOWNSAMPLING_SIZE_4");
			ShaderGlobalKeywords.DOWNSAMPLING_SIZE_8 = GlobalKeyword.Create("DOWNSAMPLING_SIZE_8");
			ShaderGlobalKeywords.DOWNSAMPLING_SIZE_16 = GlobalKeyword.Create("DOWNSAMPLING_SIZE_16");
			ShaderGlobalKeywords.EVALUATE_SH_MIXED = GlobalKeyword.Create("EVALUATE_SH_MIXED");
			ShaderGlobalKeywords.EVALUATE_SH_VERTEX = GlobalKeyword.Create("EVALUATE_SH_VERTEX");
			ShaderGlobalKeywords.ProbeVolumeL1 = GlobalKeyword.Create("PROBE_VOLUMES_L1");
			ShaderGlobalKeywords.ProbeVolumeL2 = GlobalKeyword.Create("PROBE_VOLUMES_L2");
			ShaderGlobalKeywords._OUTPUT_DEPTH = GlobalKeyword.Create("_OUTPUT_DEPTH");
			ShaderGlobalKeywords.LinearToSRGBConversion = GlobalKeyword.Create("_LINEAR_TO_SRGB_CONVERSION");
			ShaderGlobalKeywords._ENABLE_ALPHA_OUTPUT = GlobalKeyword.Create("_ENABLE_ALPHA_OUTPUT");
		}

		// Token: 0x04000A59 RID: 2649
		public static GlobalKeyword MainLightShadows;

		// Token: 0x04000A5A RID: 2650
		public static GlobalKeyword MainLightShadowCascades;

		// Token: 0x04000A5B RID: 2651
		public static GlobalKeyword MainLightShadowScreen;

		// Token: 0x04000A5C RID: 2652
		public static GlobalKeyword CastingPunctualLightShadow;

		// Token: 0x04000A5D RID: 2653
		public static GlobalKeyword AdditionalLightsVertex;

		// Token: 0x04000A5E RID: 2654
		public static GlobalKeyword AdditionalLightsPixel;

		// Token: 0x04000A5F RID: 2655
		public static GlobalKeyword ForwardPlus;

		// Token: 0x04000A60 RID: 2656
		public static GlobalKeyword AdditionalLightShadows;

		// Token: 0x04000A61 RID: 2657
		public static GlobalKeyword ReflectionProbeBoxProjection;

		// Token: 0x04000A62 RID: 2658
		public static GlobalKeyword ReflectionProbeBlending;

		// Token: 0x04000A63 RID: 2659
		public static GlobalKeyword SoftShadows;

		// Token: 0x04000A64 RID: 2660
		public static GlobalKeyword SoftShadowsLow;

		// Token: 0x04000A65 RID: 2661
		public static GlobalKeyword SoftShadowsMedium;

		// Token: 0x04000A66 RID: 2662
		public static GlobalKeyword SoftShadowsHigh;

		// Token: 0x04000A67 RID: 2663
		public static GlobalKeyword MixedLightingSubtractive;

		// Token: 0x04000A68 RID: 2664
		public static GlobalKeyword LightmapShadowMixing;

		// Token: 0x04000A69 RID: 2665
		public static GlobalKeyword ShadowsShadowMask;

		// Token: 0x04000A6A RID: 2666
		public static GlobalKeyword LightLayers;

		// Token: 0x04000A6B RID: 2667
		public static GlobalKeyword RenderPassEnabled;

		// Token: 0x04000A6C RID: 2668
		public static GlobalKeyword BillboardFaceCameraPos;

		// Token: 0x04000A6D RID: 2669
		public static GlobalKeyword LightCookies;

		// Token: 0x04000A6E RID: 2670
		public static GlobalKeyword DepthNoMsaa;

		// Token: 0x04000A6F RID: 2671
		public static GlobalKeyword DepthMsaa2;

		// Token: 0x04000A70 RID: 2672
		public static GlobalKeyword DepthMsaa4;

		// Token: 0x04000A71 RID: 2673
		public static GlobalKeyword DepthMsaa8;

		// Token: 0x04000A72 RID: 2674
		public static GlobalKeyword DBufferMRT1;

		// Token: 0x04000A73 RID: 2675
		public static GlobalKeyword DBufferMRT2;

		// Token: 0x04000A74 RID: 2676
		public static GlobalKeyword DBufferMRT3;

		// Token: 0x04000A75 RID: 2677
		public static GlobalKeyword DecalNormalBlendLow;

		// Token: 0x04000A76 RID: 2678
		public static GlobalKeyword DecalNormalBlendMedium;

		// Token: 0x04000A77 RID: 2679
		public static GlobalKeyword DecalNormalBlendHigh;

		// Token: 0x04000A78 RID: 2680
		public static GlobalKeyword DecalLayers;

		// Token: 0x04000A79 RID: 2681
		public static GlobalKeyword WriteRenderingLayers;

		// Token: 0x04000A7A RID: 2682
		public static GlobalKeyword ScreenSpaceOcclusion;

		// Token: 0x04000A7B RID: 2683
		public static GlobalKeyword _SPOT;

		// Token: 0x04000A7C RID: 2684
		public static GlobalKeyword _DIRECTIONAL;

		// Token: 0x04000A7D RID: 2685
		public static GlobalKeyword _POINT;

		// Token: 0x04000A7E RID: 2686
		public static GlobalKeyword _DEFERRED_STENCIL;

		// Token: 0x04000A7F RID: 2687
		public static GlobalKeyword _DEFERRED_FIRST_LIGHT;

		// Token: 0x04000A80 RID: 2688
		public static GlobalKeyword _DEFERRED_MAIN_LIGHT;

		// Token: 0x04000A81 RID: 2689
		public static GlobalKeyword _GBUFFER_NORMALS_OCT;

		// Token: 0x04000A82 RID: 2690
		public static GlobalKeyword _DEFERRED_MIXED_LIGHTING;

		// Token: 0x04000A83 RID: 2691
		public static GlobalKeyword LIGHTMAP_ON;

		// Token: 0x04000A84 RID: 2692
		public static GlobalKeyword DYNAMICLIGHTMAP_ON;

		// Token: 0x04000A85 RID: 2693
		public static GlobalKeyword _ALPHATEST_ON;

		// Token: 0x04000A86 RID: 2694
		public static GlobalKeyword DIRLIGHTMAP_COMBINED;

		// Token: 0x04000A87 RID: 2695
		public static GlobalKeyword _DETAIL_MULX2;

		// Token: 0x04000A88 RID: 2696
		public static GlobalKeyword _DETAIL_SCALED;

		// Token: 0x04000A89 RID: 2697
		public static GlobalKeyword _CLEARCOAT;

		// Token: 0x04000A8A RID: 2698
		public static GlobalKeyword _CLEARCOATMAP;

		// Token: 0x04000A8B RID: 2699
		public static GlobalKeyword DEBUG_DISPLAY;

		// Token: 0x04000A8C RID: 2700
		public static GlobalKeyword LOD_FADE_CROSSFADE;

		// Token: 0x04000A8D RID: 2701
		public static GlobalKeyword USE_UNITY_CROSSFADE;

		// Token: 0x04000A8E RID: 2702
		public static GlobalKeyword _EMISSION;

		// Token: 0x04000A8F RID: 2703
		public static GlobalKeyword _RECEIVE_SHADOWS_OFF;

		// Token: 0x04000A90 RID: 2704
		public static GlobalKeyword _SURFACE_TYPE_TRANSPARENT;

		// Token: 0x04000A91 RID: 2705
		public static GlobalKeyword _ALPHAPREMULTIPLY_ON;

		// Token: 0x04000A92 RID: 2706
		public static GlobalKeyword _ALPHAMODULATE_ON;

		// Token: 0x04000A93 RID: 2707
		public static GlobalKeyword _NORMALMAP;

		// Token: 0x04000A94 RID: 2708
		public static GlobalKeyword _ADD_PRECOMPUTED_VELOCITY;

		// Token: 0x04000A95 RID: 2709
		public static GlobalKeyword EDITOR_VISUALIZATION;

		// Token: 0x04000A96 RID: 2710
		public static GlobalKeyword FoveatedRenderingNonUniformRaster;

		// Token: 0x04000A97 RID: 2711
		public static GlobalKeyword DisableTexture2DXArray;

		// Token: 0x04000A98 RID: 2712
		public static GlobalKeyword BlitSingleSlice;

		// Token: 0x04000A99 RID: 2713
		public static GlobalKeyword XROcclusionMeshCombined;

		// Token: 0x04000A9A RID: 2714
		public static GlobalKeyword SCREEN_COORD_OVERRIDE;

		// Token: 0x04000A9B RID: 2715
		public static GlobalKeyword DOWNSAMPLING_SIZE_2;

		// Token: 0x04000A9C RID: 2716
		public static GlobalKeyword DOWNSAMPLING_SIZE_4;

		// Token: 0x04000A9D RID: 2717
		public static GlobalKeyword DOWNSAMPLING_SIZE_8;

		// Token: 0x04000A9E RID: 2718
		public static GlobalKeyword DOWNSAMPLING_SIZE_16;

		// Token: 0x04000A9F RID: 2719
		public static GlobalKeyword EVALUATE_SH_MIXED;

		// Token: 0x04000AA0 RID: 2720
		public static GlobalKeyword EVALUATE_SH_VERTEX;

		// Token: 0x04000AA1 RID: 2721
		public static GlobalKeyword ProbeVolumeL1;

		// Token: 0x04000AA2 RID: 2722
		public static GlobalKeyword ProbeVolumeL2;

		// Token: 0x04000AA3 RID: 2723
		public static GlobalKeyword _OUTPUT_DEPTH;

		// Token: 0x04000AA4 RID: 2724
		public static GlobalKeyword LinearToSRGBConversion;

		// Token: 0x04000AA5 RID: 2725
		public static GlobalKeyword _ENABLE_ALPHA_OUTPUT;
	}
}
