using System;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020001CA RID: 458
	public struct CameraData
	{
		// Token: 0x060009EB RID: 2539 RVA: 0x00032664 File Offset: 0x00030864
		internal CameraData(ContextContainer frameData)
		{
			this.frameData = frameData;
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060009EC RID: 2540 RVA: 0x0003266D File Offset: 0x0003086D
		internal UniversalCameraData universalCameraData
		{
			get
			{
				return this.frameData.Get<UniversalCameraData>();
			}
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x0003267A File Offset: 0x0003087A
		internal void SetViewAndProjectionMatrix(Matrix4x4 viewMatrix, Matrix4x4 projectionMatrix)
		{
			this.frameData.Get<UniversalCameraData>().SetViewAndProjectionMatrix(viewMatrix, projectionMatrix);
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x0003268E File Offset: 0x0003088E
		internal void SetViewProjectionAndJitterMatrix(Matrix4x4 viewMatrix, Matrix4x4 projectionMatrix, Matrix4x4 jitterMatrix)
		{
			this.frameData.Get<UniversalCameraData>().SetViewProjectionAndJitterMatrix(viewMatrix, projectionMatrix, jitterMatrix);
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x000326A3 File Offset: 0x000308A3
		internal void PushBuiltinShaderConstantsXR(RasterCommandBuffer cmd, bool renderIntoTexture)
		{
			this.frameData.Get<UniversalCameraData>().PushBuiltinShaderConstantsXR(cmd, renderIntoTexture);
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x000326B7 File Offset: 0x000308B7
		public Matrix4x4 GetViewMatrix(int viewIndex = 0)
		{
			return this.frameData.Get<UniversalCameraData>().GetViewMatrix(viewIndex);
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x000326CA File Offset: 0x000308CA
		public Matrix4x4 GetProjectionMatrix(int viewIndex = 0)
		{
			return this.frameData.Get<UniversalCameraData>().GetProjectionMatrix(viewIndex);
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x000326DD File Offset: 0x000308DD
		internal Matrix4x4 GetProjectionMatrixNoJitter(int viewIndex = 0)
		{
			return this.frameData.Get<UniversalCameraData>().GetProjectionMatrixNoJitter(viewIndex);
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x000326F0 File Offset: 0x000308F0
		public Matrix4x4 GetGPUProjectionMatrix(int viewIndex = 0)
		{
			return this.frameData.Get<UniversalCameraData>().GetGPUProjectionMatrix(viewIndex);
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x00032703 File Offset: 0x00030903
		public Matrix4x4 GetGPUProjectionMatrixNoJitter(int viewIndex = 0)
		{
			return this.frameData.Get<UniversalCameraData>().GetGPUProjectionMatrixNoJitter(viewIndex);
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x00032716 File Offset: 0x00030916
		internal Matrix4x4 GetGPUProjectionMatrix(bool renderIntoTexture, int viewIndex = 0)
		{
			return this.frameData.Get<UniversalCameraData>().GetGPUProjectionMatrix(renderIntoTexture, viewIndex);
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060009F6 RID: 2550 RVA: 0x0003272A File Offset: 0x0003092A
		public ref Camera camera
		{
			get
			{
				return ref this.frameData.Get<UniversalCameraData>().camera;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060009F7 RID: 2551 RVA: 0x0003273C File Offset: 0x0003093C
		public ref UniversalCameraHistory historyManager
		{
			get
			{
				return ref this.frameData.Get<UniversalCameraData>().m_HistoryManager;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060009F8 RID: 2552 RVA: 0x0003274E File Offset: 0x0003094E
		public ref CameraRenderType renderType
		{
			get
			{
				return ref this.frameData.Get<UniversalCameraData>().renderType;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060009F9 RID: 2553 RVA: 0x00032760 File Offset: 0x00030960
		public ref RenderTexture targetTexture
		{
			get
			{
				return ref this.frameData.Get<UniversalCameraData>().targetTexture;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x00032772 File Offset: 0x00030972
		public ref RenderTextureDescriptor cameraTargetDescriptor
		{
			get
			{
				return ref this.frameData.Get<UniversalCameraData>().cameraTargetDescriptor;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x00032784 File Offset: 0x00030984
		internal ref Rect pixelRect
		{
			get
			{
				return ref this.frameData.Get<UniversalCameraData>().pixelRect;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060009FC RID: 2556 RVA: 0x00032796 File Offset: 0x00030996
		internal ref bool useScreenCoordOverride
		{
			get
			{
				return ref this.frameData.Get<UniversalCameraData>().useScreenCoordOverride;
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x000327A8 File Offset: 0x000309A8
		internal ref Vector4 screenSizeOverride
		{
			get
			{
				return ref this.frameData.Get<UniversalCameraData>().screenSizeOverride;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060009FE RID: 2558 RVA: 0x000327BA File Offset: 0x000309BA
		internal ref Vector4 screenCoordScaleBias
		{
			get
			{
				return ref this.frameData.Get<UniversalCameraData>().screenCoordScaleBias;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x000327CC File Offset: 0x000309CC
		internal ref int pixelWidth
		{
			get
			{
				return ref this.frameData.Get<UniversalCameraData>().pixelWidth;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x000327DE File Offset: 0x000309DE
		internal ref int pixelHeight
		{
			get
			{
				return ref this.frameData.Get<UniversalCameraData>().pixelHeight;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x000327F0 File Offset: 0x000309F0
		internal ref float aspectRatio
		{
			get
			{
				return ref this.frameData.Get<UniversalCameraData>().aspectRatio;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000A02 RID: 2562 RVA: 0x00032802 File Offset: 0x00030A02
		public ref float renderScale
		{
			get
			{
				return ref this.frameData.Get<UniversalCameraData>().renderScale;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x00032814 File Offset: 0x00030A14
		internal ref ImageScalingMode imageScalingMode
		{
			get
			{
				return ref this.frameData.Get<UniversalCameraData>().imageScalingMode;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000A04 RID: 2564 RVA: 0x00032826 File Offset: 0x00030A26
		internal ref ImageUpscalingFilter upscalingFilter
		{
			get
			{
				return ref this.frameData.Get<UniversalCameraData>().upscalingFilter;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x00032838 File Offset: 0x00030A38
		internal ref bool fsrOverrideSharpness
		{
			get
			{
				return ref this.frameData.Get<UniversalCameraData>().fsrOverrideSharpness;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000A06 RID: 2566 RVA: 0x0003284A File Offset: 0x00030A4A
		internal ref float fsrSharpness
		{
			get
			{
				return ref this.frameData.Get<UniversalCameraData>().fsrSharpness;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x0003285C File Offset: 0x00030A5C
		internal ref HDRColorBufferPrecision hdrColorBufferPrecision
		{
			get
			{
				return ref this.frameData.Get<UniversalCameraData>().hdrColorBufferPrecision;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x0003286E File Offset: 0x00030A6E
		public ref bool clearDepth
		{
			get
			{
				return ref this.frameData.Get<UniversalCameraData>().clearDepth;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x00032880 File Offset: 0x00030A80
		public ref CameraType cameraType
		{
			get
			{
				return ref this.frameData.Get<UniversalCameraData>().cameraType;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x00032892 File Offset: 0x00030A92
		public ref bool isDefaultViewport
		{
			get
			{
				return ref this.frameData.Get<UniversalCameraData>().isDefaultViewport;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x000328A4 File Offset: 0x00030AA4
		public ref bool isHdrEnabled
		{
			get
			{
				return ref this.frameData.Get<UniversalCameraData>().isHdrEnabled;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x000328B6 File Offset: 0x00030AB6
		public ref bool allowHDROutput
		{
			get
			{
				return ref this.frameData.Get<UniversalCameraData>().allowHDROutput;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x000328C8 File Offset: 0x00030AC8
		public ref bool isAlphaOutputEnabled
		{
			get
			{
				return ref this.frameData.Get<UniversalCameraData>().isAlphaOutputEnabled;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x000328DA File Offset: 0x00030ADA
		public ref bool requiresDepthTexture
		{
			get
			{
				return ref this.frameData.Get<UniversalCameraData>().requiresDepthTexture;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000A0F RID: 2575 RVA: 0x000328EC File Offset: 0x00030AEC
		public ref bool requiresOpaqueTexture
		{
			get
			{
				return ref this.frameData.Get<UniversalCameraData>().requiresOpaqueTexture;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000A10 RID: 2576 RVA: 0x000328FE File Offset: 0x00030AFE
		public ref bool postProcessingRequiresDepthTexture
		{
			get
			{
				return ref this.frameData.Get<UniversalCameraData>().postProcessingRequiresDepthTexture;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000A11 RID: 2577 RVA: 0x00032910 File Offset: 0x00030B10
		public ref bool xrRendering
		{
			get
			{
				return ref this.frameData.Get<UniversalCameraData>().xrRendering;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000A12 RID: 2578 RVA: 0x00032922 File Offset: 0x00030B22
		internal bool requireSrgbConversion
		{
			get
			{
				return this.frameData.Get<UniversalCameraData>().requireSrgbConversion;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000A13 RID: 2579 RVA: 0x00032934 File Offset: 0x00030B34
		public bool isSceneViewCamera
		{
			get
			{
				return this.frameData.Get<UniversalCameraData>().isSceneViewCamera;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000A14 RID: 2580 RVA: 0x00032946 File Offset: 0x00030B46
		public bool isPreviewCamera
		{
			get
			{
				return this.frameData.Get<UniversalCameraData>().isPreviewCamera;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000A15 RID: 2581 RVA: 0x00032958 File Offset: 0x00030B58
		internal bool isRenderPassSupportedCamera
		{
			get
			{
				return this.frameData.Get<UniversalCameraData>().isRenderPassSupportedCamera;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000A16 RID: 2582 RVA: 0x0003296A File Offset: 0x00030B6A
		internal bool resolveToScreen
		{
			get
			{
				return this.frameData.Get<UniversalCameraData>().resolveToScreen;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000A17 RID: 2583 RVA: 0x0003297C File Offset: 0x00030B7C
		public bool isHDROutputActive
		{
			get
			{
				return this.frameData.Get<UniversalCameraData>().isHDROutputActive;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000A18 RID: 2584 RVA: 0x0003298E File Offset: 0x00030B8E
		public HDROutputUtils.HDRDisplayInformation hdrDisplayInformation
		{
			get
			{
				return this.frameData.Get<UniversalCameraData>().hdrDisplayInformation;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000A19 RID: 2585 RVA: 0x000329A0 File Offset: 0x00030BA0
		public ColorGamut hdrDisplayColorGamut
		{
			get
			{
				return this.frameData.Get<UniversalCameraData>().hdrDisplayColorGamut;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x000329B2 File Offset: 0x00030BB2
		public bool rendersOverlayUI
		{
			get
			{
				return this.frameData.Get<UniversalCameraData>().rendersOverlayUI;
			}
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x000329C4 File Offset: 0x00030BC4
		public bool IsHandleYFlipped(RTHandle handle)
		{
			return this.frameData.Get<UniversalCameraData>().IsHandleYFlipped(handle);
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x000329D7 File Offset: 0x00030BD7
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public bool IsCameraProjectionMatrixFlipped()
		{
			return this.frameData.Get<UniversalCameraData>().IsCameraProjectionMatrixFlipped();
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x000329E9 File Offset: 0x00030BE9
		public bool IsRenderTargetProjectionMatrixFlipped(RTHandle color, RTHandle depth = null)
		{
			return this.frameData.Get<UniversalCameraData>().IsRenderTargetProjectionMatrixFlipped(color, depth);
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x000329FD File Offset: 0x00030BFD
		internal bool IsTemporalAAEnabled()
		{
			return this.frameData.Get<UniversalCameraData>().IsTemporalAAEnabled();
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x00032A0F File Offset: 0x00030C0F
		public ref SortingCriteria defaultOpaqueSortFlags
		{
			get
			{
				return ref this.frameData.Get<UniversalCameraData>().defaultOpaqueSortFlags;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000A20 RID: 2592 RVA: 0x00032A21 File Offset: 0x00030C21
		// (set) Token: 0x06000A21 RID: 2593 RVA: 0x00032A33 File Offset: 0x00030C33
		public XRPass xr
		{
			get
			{
				return this.frameData.Get<UniversalCameraData>().xr;
			}
			internal set
			{
				this.frameData.Get<UniversalCameraData>().xr = value;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000A22 RID: 2594 RVA: 0x00032A46 File Offset: 0x00030C46
		internal XRPassUniversal xrUniversal
		{
			get
			{
				return this.frameData.Get<UniversalCameraData>().xrUniversal;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000A23 RID: 2595 RVA: 0x00032A58 File Offset: 0x00030C58
		public ref float maxShadowDistance
		{
			get
			{
				return ref this.frameData.Get<UniversalCameraData>().maxShadowDistance;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x00032A6A File Offset: 0x00030C6A
		public ref bool postProcessEnabled
		{
			get
			{
				return ref this.frameData.Get<UniversalCameraData>().postProcessEnabled;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000A25 RID: 2597 RVA: 0x00032A7C File Offset: 0x00030C7C
		public ref IEnumerator<Action<RenderTargetIdentifier, CommandBuffer>> captureActions
		{
			get
			{
				return ref this.frameData.Get<UniversalCameraData>().captureActions;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x00032A8E File Offset: 0x00030C8E
		public ref LayerMask volumeLayerMask
		{
			get
			{
				return ref this.frameData.Get<UniversalCameraData>().volumeLayerMask;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000A27 RID: 2599 RVA: 0x00032AA0 File Offset: 0x00030CA0
		public ref Transform volumeTrigger
		{
			get
			{
				return ref this.frameData.Get<UniversalCameraData>().volumeTrigger;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000A28 RID: 2600 RVA: 0x00032AB2 File Offset: 0x00030CB2
		public ref bool isStopNaNEnabled
		{
			get
			{
				return ref this.frameData.Get<UniversalCameraData>().isStopNaNEnabled;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000A29 RID: 2601 RVA: 0x00032AC4 File Offset: 0x00030CC4
		public ref bool isDitheringEnabled
		{
			get
			{
				return ref this.frameData.Get<UniversalCameraData>().isDitheringEnabled;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000A2A RID: 2602 RVA: 0x00032AD6 File Offset: 0x00030CD6
		public ref AntialiasingMode antialiasing
		{
			get
			{
				return ref this.frameData.Get<UniversalCameraData>().antialiasing;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000A2B RID: 2603 RVA: 0x00032AE8 File Offset: 0x00030CE8
		public ref AntialiasingQuality antialiasingQuality
		{
			get
			{
				return ref this.frameData.Get<UniversalCameraData>().antialiasingQuality;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000A2C RID: 2604 RVA: 0x00032AFA File Offset: 0x00030CFA
		public ref ScriptableRenderer renderer
		{
			get
			{
				return ref this.frameData.Get<UniversalCameraData>().renderer;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000A2D RID: 2605 RVA: 0x00032B0C File Offset: 0x00030D0C
		public ref bool resolveFinalTarget
		{
			get
			{
				return ref this.frameData.Get<UniversalCameraData>().resolveFinalTarget;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000A2E RID: 2606 RVA: 0x00032B1E File Offset: 0x00030D1E
		public ref Vector3 worldSpaceCameraPos
		{
			get
			{
				return ref this.frameData.Get<UniversalCameraData>().worldSpaceCameraPos;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000A2F RID: 2607 RVA: 0x00032B30 File Offset: 0x00030D30
		public ref Color backgroundColor
		{
			get
			{
				return ref this.frameData.Get<UniversalCameraData>().backgroundColor;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000A30 RID: 2608 RVA: 0x00032B42 File Offset: 0x00030D42
		internal ref TaaHistory taaHistory
		{
			get
			{
				return ref this.frameData.Get<UniversalCameraData>().taaHistory;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x00032B54 File Offset: 0x00030D54
		internal ref TemporalAA.Settings taaSettings
		{
			get
			{
				return ref this.frameData.Get<UniversalCameraData>().taaSettings;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000A32 RID: 2610 RVA: 0x00032B66 File Offset: 0x00030D66
		internal bool resetHistory
		{
			get
			{
				return this.frameData.Get<UniversalCameraData>().resetHistory;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000A33 RID: 2611 RVA: 0x00032B78 File Offset: 0x00030D78
		public ref Camera baseCamera
		{
			get
			{
				return ref this.frameData.Get<UniversalCameraData>().baseCamera;
			}
		}

		// Token: 0x04000A0B RID: 2571
		private ContextContainer frameData;
	}
}
