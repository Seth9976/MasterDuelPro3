using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020001CF RID: 463
	internal static class ShaderPropertyId
	{
		// Token: 0x04000A1D RID: 2589
		public static readonly int glossyEnvironmentColor = Shader.PropertyToID("_GlossyEnvironmentColor");

		// Token: 0x04000A1E RID: 2590
		public static readonly int subtractiveShadowColor = Shader.PropertyToID("_SubtractiveShadowColor");

		// Token: 0x04000A1F RID: 2591
		public static readonly int glossyEnvironmentCubeMap = Shader.PropertyToID("_GlossyEnvironmentCubeMap");

		// Token: 0x04000A20 RID: 2592
		public static readonly int glossyEnvironmentCubeMapHDR = Shader.PropertyToID("_GlossyEnvironmentCubeMap_HDR");

		// Token: 0x04000A21 RID: 2593
		public static readonly int ambientSkyColor = Shader.PropertyToID("unity_AmbientSky");

		// Token: 0x04000A22 RID: 2594
		public static readonly int ambientEquatorColor = Shader.PropertyToID("unity_AmbientEquator");

		// Token: 0x04000A23 RID: 2595
		public static readonly int ambientGroundColor = Shader.PropertyToID("unity_AmbientGround");

		// Token: 0x04000A24 RID: 2596
		public static readonly int time = Shader.PropertyToID("_Time");

		// Token: 0x04000A25 RID: 2597
		public static readonly int sinTime = Shader.PropertyToID("_SinTime");

		// Token: 0x04000A26 RID: 2598
		public static readonly int cosTime = Shader.PropertyToID("_CosTime");

		// Token: 0x04000A27 RID: 2599
		public static readonly int deltaTime = Shader.PropertyToID("unity_DeltaTime");

		// Token: 0x04000A28 RID: 2600
		public static readonly int timeParameters = Shader.PropertyToID("_TimeParameters");

		// Token: 0x04000A29 RID: 2601
		public static readonly int lastTimeParameters = Shader.PropertyToID("_LastTimeParameters");

		// Token: 0x04000A2A RID: 2602
		public static readonly int scaledScreenParams = Shader.PropertyToID("_ScaledScreenParams");

		// Token: 0x04000A2B RID: 2603
		public static readonly int worldSpaceCameraPos = Shader.PropertyToID("_WorldSpaceCameraPos");

		// Token: 0x04000A2C RID: 2604
		public static readonly int screenParams = Shader.PropertyToID("_ScreenParams");

		// Token: 0x04000A2D RID: 2605
		public static readonly int alphaToMaskAvailable = Shader.PropertyToID("_AlphaToMaskAvailable");

		// Token: 0x04000A2E RID: 2606
		public static readonly int projectionParams = Shader.PropertyToID("_ProjectionParams");

		// Token: 0x04000A2F RID: 2607
		public static readonly int zBufferParams = Shader.PropertyToID("_ZBufferParams");

		// Token: 0x04000A30 RID: 2608
		public static readonly int orthoParams = Shader.PropertyToID("unity_OrthoParams");

		// Token: 0x04000A31 RID: 2609
		public static readonly int globalMipBias = Shader.PropertyToID("_GlobalMipBias");

		// Token: 0x04000A32 RID: 2610
		public static readonly int screenSize = Shader.PropertyToID("_ScreenSize");

		// Token: 0x04000A33 RID: 2611
		public static readonly int screenCoordScaleBias = Shader.PropertyToID("_ScreenCoordScaleBias");

		// Token: 0x04000A34 RID: 2612
		public static readonly int screenSizeOverride = Shader.PropertyToID("_ScreenSizeOverride");

		// Token: 0x04000A35 RID: 2613
		public static readonly int viewMatrix = Shader.PropertyToID("unity_MatrixV");

		// Token: 0x04000A36 RID: 2614
		public static readonly int projectionMatrix = Shader.PropertyToID("glstate_matrix_projection");

		// Token: 0x04000A37 RID: 2615
		public static readonly int viewAndProjectionMatrix = Shader.PropertyToID("unity_MatrixVP");

		// Token: 0x04000A38 RID: 2616
		public static readonly int inverseViewMatrix = Shader.PropertyToID("unity_MatrixInvV");

		// Token: 0x04000A39 RID: 2617
		public static readonly int inverseProjectionMatrix = Shader.PropertyToID("unity_MatrixInvP");

		// Token: 0x04000A3A RID: 2618
		public static readonly int inverseViewAndProjectionMatrix = Shader.PropertyToID("unity_MatrixInvVP");

		// Token: 0x04000A3B RID: 2619
		public static readonly int cameraProjectionMatrix = Shader.PropertyToID("unity_CameraProjection");

		// Token: 0x04000A3C RID: 2620
		public static readonly int inverseCameraProjectionMatrix = Shader.PropertyToID("unity_CameraInvProjection");

		// Token: 0x04000A3D RID: 2621
		public static readonly int worldToCameraMatrix = Shader.PropertyToID("unity_WorldToCamera");

		// Token: 0x04000A3E RID: 2622
		public static readonly int cameraToWorldMatrix = Shader.PropertyToID("unity_CameraToWorld");

		// Token: 0x04000A3F RID: 2623
		public static readonly int shadowBias = Shader.PropertyToID("_ShadowBias");

		// Token: 0x04000A40 RID: 2624
		public static readonly int lightDirection = Shader.PropertyToID("_LightDirection");

		// Token: 0x04000A41 RID: 2625
		public static readonly int lightPosition = Shader.PropertyToID("_LightPosition");

		// Token: 0x04000A42 RID: 2626
		public static readonly int cameraWorldClipPlanes = Shader.PropertyToID("unity_CameraWorldClipPlanes");

		// Token: 0x04000A43 RID: 2627
		public static readonly int billboardNormal = Shader.PropertyToID("unity_BillboardNormal");

		// Token: 0x04000A44 RID: 2628
		public static readonly int billboardTangent = Shader.PropertyToID("unity_BillboardTangent");

		// Token: 0x04000A45 RID: 2629
		public static readonly int billboardCameraParams = Shader.PropertyToID("unity_BillboardCameraParams");

		// Token: 0x04000A46 RID: 2630
		public static readonly int previousViewProjectionNoJitter = Shader.PropertyToID("_PrevViewProjMatrix");

		// Token: 0x04000A47 RID: 2631
		public static readonly int viewProjectionNoJitter = Shader.PropertyToID("_NonJitteredViewProjMatrix");

		// Token: 0x04000A48 RID: 2632
		public static readonly int previousViewProjectionNoJitterStereo = Shader.PropertyToID("_PrevViewProjMatrixStereo");

		// Token: 0x04000A49 RID: 2633
		public static readonly int viewProjectionNoJitterStereo = Shader.PropertyToID("_NonJitteredViewProjMatrixStereo");

		// Token: 0x04000A4A RID: 2634
		public static readonly int blitTexture = Shader.PropertyToID("_BlitTexture");

		// Token: 0x04000A4B RID: 2635
		public static readonly int blitScaleBias = Shader.PropertyToID("_BlitScaleBias");

		// Token: 0x04000A4C RID: 2636
		public static readonly int sourceTex = Shader.PropertyToID("_SourceTex");

		// Token: 0x04000A4D RID: 2637
		public static readonly int scaleBias = Shader.PropertyToID("_ScaleBias");

		// Token: 0x04000A4E RID: 2638
		public static readonly int scaleBiasRt = Shader.PropertyToID("_ScaleBiasRt");

		// Token: 0x04000A4F RID: 2639
		public static readonly int rtHandleScale = Shader.PropertyToID("_RTHandleScale");

		// Token: 0x04000A50 RID: 2640
		public static readonly int rendererColor = Shader.PropertyToID("_RendererColor");

		// Token: 0x04000A51 RID: 2641
		public static readonly int ditheringTexture = Shader.PropertyToID("_DitheringTexture");

		// Token: 0x04000A52 RID: 2642
		public static readonly int ditheringTextureInvSize = Shader.PropertyToID("_DitheringTextureInvSize");

		// Token: 0x04000A53 RID: 2643
		public static readonly int renderingLayerMaxInt = Shader.PropertyToID("_RenderingLayerMaxInt");

		// Token: 0x04000A54 RID: 2644
		public static readonly int renderingLayerRcpMaxInt = Shader.PropertyToID("_RenderingLayerRcpMaxInt");

		// Token: 0x04000A55 RID: 2645
		public static readonly int overlayUITexture = Shader.PropertyToID("_OverlayUITexture");

		// Token: 0x04000A56 RID: 2646
		public static readonly int hdrOutputLuminanceParams = Shader.PropertyToID("_HDROutputLuminanceParams");

		// Token: 0x04000A57 RID: 2647
		public static readonly int hdrOutputGradingParams = Shader.PropertyToID("_HDROutputGradingParams");
	}
}
