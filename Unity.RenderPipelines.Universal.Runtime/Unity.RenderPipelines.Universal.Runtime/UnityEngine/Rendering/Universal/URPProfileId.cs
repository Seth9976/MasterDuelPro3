using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020001D3 RID: 467
	internal enum URPProfileId
	{
		// Token: 0x04000B0E RID: 2830
		UniversalRenderTotal,
		// Token: 0x04000B0F RID: 2831
		UpdateVolumeFramework,
		// Token: 0x04000B10 RID: 2832
		RenderCameraStack,
		// Token: 0x04000B11 RID: 2833
		AdditionalLightsShadow,
		// Token: 0x04000B12 RID: 2834
		ColorGradingLUT,
		// Token: 0x04000B13 RID: 2835
		CopyColor,
		// Token: 0x04000B14 RID: 2836
		CopyDepth,
		// Token: 0x04000B15 RID: 2837
		DrawDepthNormalPrepass,
		// Token: 0x04000B16 RID: 2838
		DepthPrepass,
		// Token: 0x04000B17 RID: 2839
		UpdateReflectionProbeAtlas,
		// Token: 0x04000B18 RID: 2840
		DrawOpaqueObjects,
		// Token: 0x04000B19 RID: 2841
		DrawTransparentObjects,
		// Token: 0x04000B1A RID: 2842
		DrawScreenSpaceUI,
		// Token: 0x04000B1B RID: 2843
		RecordRenderGraph,
		// Token: 0x04000B1C RID: 2844
		LightCookies,
		// Token: 0x04000B1D RID: 2845
		MainLightShadow,
		// Token: 0x04000B1E RID: 2846
		ResolveShadows,
		// Token: 0x04000B1F RID: 2847
		SSAO,
		// Token: 0x04000B20 RID: 2848
		StopNaNs,
		// Token: 0x04000B21 RID: 2849
		SMAA,
		// Token: 0x04000B22 RID: 2850
		GaussianDepthOfField,
		// Token: 0x04000B23 RID: 2851
		BokehDepthOfField,
		// Token: 0x04000B24 RID: 2852
		TemporalAA,
		// Token: 0x04000B25 RID: 2853
		MotionBlur,
		// Token: 0x04000B26 RID: 2854
		PaniniProjection,
		// Token: 0x04000B27 RID: 2855
		UberPostProcess,
		// Token: 0x04000B28 RID: 2856
		Bloom,
		// Token: 0x04000B29 RID: 2857
		LensFlareDataDrivenComputeOcclusion,
		// Token: 0x04000B2A RID: 2858
		LensFlareDataDriven,
		// Token: 0x04000B2B RID: 2859
		LensFlareScreenSpace,
		// Token: 0x04000B2C RID: 2860
		DrawMotionVectors,
		// Token: 0x04000B2D RID: 2861
		DrawFullscreen,
		// Token: 0x04000B2E RID: 2862
		[HideInDebugUI]
		RG_SetupPostFX,
		// Token: 0x04000B2F RID: 2863
		[HideInDebugUI]
		RG_StopNaNs,
		// Token: 0x04000B30 RID: 2864
		[HideInDebugUI]
		RG_SMAAMaterialSetup,
		// Token: 0x04000B31 RID: 2865
		[HideInDebugUI]
		RG_SMAAEdgeDetection,
		// Token: 0x04000B32 RID: 2866
		[HideInDebugUI]
		RG_SMAABlendWeight,
		// Token: 0x04000B33 RID: 2867
		[HideInDebugUI]
		RG_SMAANeighborhoodBlend,
		// Token: 0x04000B34 RID: 2868
		[HideInDebugUI]
		RG_SetupDoF,
		// Token: 0x04000B35 RID: 2869
		[HideInDebugUI]
		RG_DOFComputeCOC,
		// Token: 0x04000B36 RID: 2870
		[HideInDebugUI]
		RG_DOFDownscalePrefilter,
		// Token: 0x04000B37 RID: 2871
		[HideInDebugUI]
		RG_DOFBlurH,
		// Token: 0x04000B38 RID: 2872
		[HideInDebugUI]
		RG_DOFBlurV,
		// Token: 0x04000B39 RID: 2873
		[HideInDebugUI]
		RG_DOFBlurBokeh,
		// Token: 0x04000B3A RID: 2874
		[HideInDebugUI]
		RG_DOFPostFilter,
		// Token: 0x04000B3B RID: 2875
		[HideInDebugUI]
		RG_DOFComposite,
		// Token: 0x04000B3C RID: 2876
		[HideInDebugUI]
		RG_TAA,
		// Token: 0x04000B3D RID: 2877
		[HideInDebugUI]
		RG_TAACopyHistory,
		// Token: 0x04000B3E RID: 2878
		[HideInDebugUI]
		RG_MotionBlur,
		// Token: 0x04000B3F RID: 2879
		[HideInDebugUI]
		RG_BloomSetup,
		// Token: 0x04000B40 RID: 2880
		[HideInDebugUI]
		RG_BloomPrefilter,
		// Token: 0x04000B41 RID: 2881
		[HideInDebugUI]
		RG_BloomDownsample,
		// Token: 0x04000B42 RID: 2882
		[HideInDebugUI]
		RG_BloomUpsample,
		// Token: 0x04000B43 RID: 2883
		[HideInDebugUI]
		RG_UberPostSetupBloomPass,
		// Token: 0x04000B44 RID: 2884
		[HideInDebugUI]
		RG_UberPost,
		// Token: 0x04000B45 RID: 2885
		[HideInDebugUI]
		RG_FinalSetup,
		// Token: 0x04000B46 RID: 2886
		[HideInDebugUI]
		RG_FinalFSRScale,
		// Token: 0x04000B47 RID: 2887
		[HideInDebugUI]
		RG_FinalBlit,
		// Token: 0x04000B48 RID: 2888
		BlitFinalToBackBuffer,
		// Token: 0x04000B49 RID: 2889
		DrawSkybox
	}
}
