using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Rendering;
using UnityEngine.XR;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x02000014 RID: 20
	public static class XRSystem
	{
		// Token: 0x06000079 RID: 121 RVA: 0x00003F29 File Offset: 0x00002129
		public static XRDisplaySubsystem GetActiveDisplay()
		{
			return XRSystem.s_Display;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00003F30 File Offset: 0x00002130
		public static bool displayActive
		{
			get
			{
				return XRSystem.s_Display != null && XRSystem.s_Display.running;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00003F48 File Offset: 0x00002148
		public static bool isHDRDisplayOutputActive
		{
			get
			{
				XRDisplaySubsystem xrdisplaySubsystem = XRSystem.s_Display;
				bool? flag;
				if (xrdisplaySubsystem == null)
				{
					flag = null;
				}
				else
				{
					HDROutputSettings hdrOutputSettings = xrdisplaySubsystem.hdrOutputSettings;
					flag = ((hdrOutputSettings != null) ? new bool?(hdrOutputSettings.active) : null);
				}
				bool? flag2 = flag;
				return flag2.GetValueOrDefault();
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00003F8F File Offset: 0x0000218F
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00003F96 File Offset: 0x00002196
		public static bool singlePassAllowed { get; set; } = true;

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003F9E File Offset: 0x0000219E
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00003FA5 File Offset: 0x000021A5
		public static FoveatedRenderingCaps foveatedRenderingCaps { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00003FAD File Offset: 0x000021AD
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00003FB4 File Offset: 0x000021B4
		public static bool dumpDebugInfo { get; set; } = false;

		// Token: 0x06000082 RID: 130 RVA: 0x00003FBC File Offset: 0x000021BC
		public static void Initialize(Func<XRPassCreateInfo, XRPass> passAllocator, Shader occlusionMeshPS, Shader mirrorViewPS)
		{
			if (passAllocator == null)
			{
				throw new ArgumentNullException("passCreator");
			}
			XRSystem.s_PassAllocator = passAllocator;
			XRSystem.RefreshDeviceInfo();
			XRSystem.foveatedRenderingCaps = SystemInfo.foveatedRenderingCaps;
			if (occlusionMeshPS != null && XRSystem.s_OcclusionMeshMaterial == null)
			{
				XRSystem.s_OcclusionMeshMaterial = CoreUtils.CreateEngineMaterial(occlusionMeshPS);
			}
			if (mirrorViewPS != null && XRSystem.s_MirrorViewMaterial == null)
			{
				XRSystem.s_MirrorViewMaterial = CoreUtils.CreateEngineMaterial(mirrorViewPS);
			}
			if (XRGraphicsAutomatedTests.enabled)
			{
				XRSystem.SetLayoutOverride(new Action<XRLayout, Camera>(XRGraphicsAutomatedTests.OverrideLayout));
			}
			SinglepassKeywords.STEREO_MULTIVIEW_ON = GlobalKeyword.Create("STEREO_MULTIVIEW_ON");
			SinglepassKeywords.STEREO_INSTANCING_ON = GlobalKeyword.Create("STEREO_INSTANCING_ON");
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004064 File Offset: 0x00002264
		public static void SetDisplayMSAASamples(MSAASamples msaaSamples)
		{
			if (XRSystem.s_MSAASamples == msaaSamples)
			{
				return;
			}
			XRSystem.s_MSAASamples = msaaSamples;
			SubsystemManager.GetSubsystems<XRDisplaySubsystem>(XRSystem.s_DisplayList);
			foreach (XRDisplaySubsystem xrdisplaySubsystem in XRSystem.s_DisplayList)
			{
				xrdisplaySubsystem.SetMSAALevel((int)XRSystem.s_MSAASamples);
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000040D4 File Offset: 0x000022D4
		public static MSAASamples GetDisplayMSAASamples()
		{
			return XRSystem.s_MSAASamples;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000040DB File Offset: 0x000022DB
		internal static void SetOcclusionMeshScale(float occlusionMeshScale)
		{
			XRSystem.s_OcclusionMeshScaling = occlusionMeshScale;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000040E3 File Offset: 0x000022E3
		internal static float GetOcclusionMeshScale()
		{
			return XRSystem.s_OcclusionMeshScaling;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000040EA File Offset: 0x000022EA
		internal static void SetMirrorViewMode(int mirrorBlitMode)
		{
			if (XRSystem.s_Display == null)
			{
				return;
			}
			XRSystem.s_Display.SetPreferredMirrorBlitMode(mirrorBlitMode);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000040FF File Offset: 0x000022FF
		internal static int GetMirrorViewMode()
		{
			if (XRSystem.s_Display == null)
			{
				return -6;
			}
			return XRSystem.s_Display.GetPreferredMirrorBlitMode();
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004118 File Offset: 0x00002318
		public static void SetRenderScale(float renderScale)
		{
			SubsystemManager.GetSubsystems<XRDisplaySubsystem>(XRSystem.s_DisplayList);
			foreach (XRDisplaySubsystem xrdisplaySubsystem in XRSystem.s_DisplayList)
			{
				xrdisplaySubsystem.scaleOfAllRenderTargets = renderScale;
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004174 File Offset: 0x00002374
		public static float GetRenderViewportScale()
		{
			return XRSystem.s_Display.scaleOfAllViewports;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004180 File Offset: 0x00002380
		public static XRLayout NewLayout()
		{
			XRSystem.RefreshDeviceInfo();
			return XRSystem.s_Layout.New();
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004191 File Offset: 0x00002391
		public static void EndLayout()
		{
			if (XRSystem.dumpDebugInfo)
			{
				XRSystem.s_Layout.top.LogDebugInfo();
			}
			XRSystem.s_Layout.Release();
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000041B3 File Offset: 0x000023B3
		public static void RenderMirrorView(CommandBuffer cmd, Camera camera)
		{
			XRMirrorView.RenderMirrorView(cmd, camera, XRSystem.s_MirrorViewMaterial, XRSystem.s_Display);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000041C6 File Offset: 0x000023C6
		public static void Dispose()
		{
			if (XRSystem.s_OcclusionMeshMaterial != null)
			{
				CoreUtils.Destroy(XRSystem.s_OcclusionMeshMaterial);
				XRSystem.s_OcclusionMeshMaterial = null;
			}
			if (XRSystem.s_MirrorViewMaterial != null)
			{
				CoreUtils.Destroy(XRSystem.s_MirrorViewMaterial);
				XRSystem.s_MirrorViewMaterial = null;
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004202 File Offset: 0x00002402
		internal static void SetDisplayZRange(float zNear, float zFar)
		{
			if (XRSystem.s_Display != null)
			{
				XRSystem.s_Display.zNear = zNear;
				XRSystem.s_Display.zFar = zFar;
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004221 File Offset: 0x00002421
		private static void SetLayoutOverride(Action<XRLayout, Camera> action)
		{
			XRSystem.s_LayoutOverride = action;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004229 File Offset: 0x00002429
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
		private static void XRSystemInit()
		{
			if (GraphicsSettings.currentRenderPipeline != null)
			{
				XRSystem.RefreshDeviceInfo();
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004240 File Offset: 0x00002440
		private static void RefreshDeviceInfo()
		{
			SubsystemManager.GetSubsystems<XRDisplaySubsystem>(XRSystem.s_DisplayList);
			if (XRSystem.s_DisplayList.Count <= 0)
			{
				XRSystem.s_Display = null;
				return;
			}
			if (XRSystem.s_DisplayList.Count > 1)
			{
				throw new NotImplementedException("Only one XR display is supported!");
			}
			XRSystem.s_Display = XRSystem.s_DisplayList[0];
			XRSystem.s_Display.disableLegacyRenderer = true;
			XRSystem.s_Display.sRGB = QualitySettings.activeColorSpace == ColorSpace.Linear;
			XRSystem.s_Display.textureLayout = XRDisplaySubsystem.TextureLayout.Texture2DArray;
			TextureXR.maxViews = Math.Max(TextureXR.slices, 2);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000042CC File Offset: 0x000024CC
		internal static void CreateDefaultLayout(Camera camera, XRLayout layout)
		{
			XRSystem.<>c__DisplayClass44_0 CS$<>8__locals1;
			CS$<>8__locals1.camera = camera;
			if (XRSystem.s_Display == null)
			{
				throw new NullReferenceException("s_Display");
			}
			for (int renderPassIndex = 0; renderPassIndex < XRSystem.s_Display.GetRenderPassCount(); renderPassIndex++)
			{
				XRDisplaySubsystem.XRRenderPass renderPass;
				XRSystem.s_Display.GetRenderPass(renderPassIndex, out renderPass);
				ScriptableCullingParameters cullingParams;
				XRSystem.s_Display.GetCullingParameters(CS$<>8__locals1.camera, renderPass.cullingPassIndex, out cullingParams);
				int renderParameterCount = renderPass.GetRenderParameterCount();
				if (XRSystem.CanUseSinglePass(CS$<>8__locals1.camera, renderPass))
				{
					XRPassCreateInfo createInfo = XRSystem.BuildPass(renderPass, cullingParams, layout);
					XRPass xrPass = XRSystem.s_PassAllocator(createInfo);
					for (int renderParamIndex = 0; renderParamIndex < renderParameterCount; renderParamIndex++)
					{
						XRSystem.<CreateDefaultLayout>g__AddViewToPass|44_0(xrPass, renderPass, renderParamIndex, ref CS$<>8__locals1);
					}
					layout.AddPass(CS$<>8__locals1.camera, xrPass);
				}
				else
				{
					for (int renderParamIndex2 = 0; renderParamIndex2 < renderParameterCount; renderParamIndex2++)
					{
						XRPassCreateInfo createInfo2 = XRSystem.BuildPass(renderPass, cullingParams, layout);
						XRPass xrPass2 = XRSystem.s_PassAllocator(createInfo2);
						XRSystem.<CreateDefaultLayout>g__AddViewToPass|44_0(xrPass2, renderPass, renderParamIndex2, ref CS$<>8__locals1);
						layout.AddPass(CS$<>8__locals1.camera, xrPass2);
					}
				}
			}
			Action<XRLayout, Camera> action = XRSystem.s_LayoutOverride;
			if (action == null)
			{
				return;
			}
			action(layout, CS$<>8__locals1.camera);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000043E8 File Offset: 0x000025E8
		internal static void ReconfigurePass(XRPass xrPass, Camera camera)
		{
			if (xrPass.enabled && XRSystem.s_Display != null)
			{
				XRDisplaySubsystem.XRRenderPass renderPass;
				XRSystem.s_Display.GetRenderPass(xrPass.multipassId, out renderPass);
				ScriptableCullingParameters cullingParams;
				XRSystem.s_Display.GetCullingParameters(camera, renderPass.cullingPassIndex, out cullingParams);
				xrPass.AssignCullingParams(renderPass.cullingPassIndex, cullingParams);
				for (int renderParamIndex = 0; renderParamIndex < renderPass.GetRenderParameterCount(); renderParamIndex++)
				{
					XRDisplaySubsystem.XRRenderParameter renderParam;
					renderPass.GetRenderParameter(camera, renderParamIndex, out renderParam);
					xrPass.AssignView(renderParamIndex, XRSystem.BuildView(renderPass, renderParam));
				}
				Action<XRLayout, Camera> action = XRSystem.s_LayoutOverride;
				if (action == null)
				{
					return;
				}
				action(XRSystem.s_Layout.top, camera);
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000447C File Offset: 0x0000267C
		private static bool CanUseSinglePass(Camera camera, XRDisplaySubsystem.XRRenderPass renderPass)
		{
			if (!XRSystem.singlePassAllowed)
			{
				return false;
			}
			if (renderPass.renderTargetDesc.dimension != TextureDimension.Tex2DArray)
			{
				return false;
			}
			if (renderPass.GetRenderParameterCount() != 2 || renderPass.renderTargetDesc.volumeDepth != 2)
			{
				return false;
			}
			XRDisplaySubsystem.XRRenderParameter renderParam0;
			renderPass.GetRenderParameter(camera, 0, out renderParam0);
			XRDisplaySubsystem.XRRenderParameter renderParam;
			renderPass.GetRenderParameter(camera, 1, out renderParam);
			return renderParam0.textureArraySlice == 0 && renderParam.textureArraySlice == 1;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000044E8 File Offset: 0x000026E8
		private static XRView BuildView(XRDisplaySubsystem.XRRenderPass renderPass, XRDisplaySubsystem.XRRenderParameter renderParameter)
		{
			Rect viewport = renderParameter.viewport;
			viewport.x *= (float)renderPass.renderTargetDesc.width;
			viewport.width *= (float)renderPass.renderTargetDesc.width;
			viewport.y *= (float)renderPass.renderTargetDesc.height;
			viewport.height *= (float)renderPass.renderTargetDesc.height;
			Mesh occlusionMesh = (XRGraphicsAutomatedTests.running ? null : renderParameter.occlusionMesh);
			return new XRView(renderParameter.projection, renderParameter.view, renderParameter.previousView, renderParameter.isPreviousViewValid, viewport, occlusionMesh, renderParameter.textureArraySlice);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000045A0 File Offset: 0x000027A0
		private static RenderTextureDescriptor XrRenderTextureDescToUnityRenderTextureDesc(RenderTextureDescriptor xrDesc)
		{
			return new RenderTextureDescriptor(xrDesc.width, xrDesc.height, xrDesc.graphicsFormat, xrDesc.depthStencilFormat, xrDesc.mipCount)
			{
				dimension = xrDesc.dimension,
				volumeDepth = xrDesc.volumeDepth,
				vrUsage = xrDesc.vrUsage,
				sRGB = xrDesc.sRGB,
				shadowSamplingMode = xrDesc.shadowSamplingMode
			};
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004620 File Offset: 0x00002820
		private static XRPassCreateInfo BuildPass(XRDisplaySubsystem.XRRenderPass xrRenderPass, ScriptableCullingParameters cullingParameters, XRLayout layout)
		{
			return new XRPassCreateInfo
			{
				renderTarget = xrRenderPass.renderTarget,
				renderTargetDesc = XRSystem.XrRenderTextureDescToUnityRenderTextureDesc(xrRenderPass.renderTargetDesc),
				hasMotionVectorPass = xrRenderPass.hasMotionVectorPass,
				motionVectorRenderTarget = xrRenderPass.motionVectorRenderTarget,
				motionVectorRenderTargetDesc = XRSystem.XrRenderTextureDescToUnityRenderTextureDesc(xrRenderPass.motionVectorRenderTargetDesc),
				cullingParameters = cullingParameters,
				occlusionMeshMaterial = XRSystem.s_OcclusionMeshMaterial,
				occlusionMeshScale = XRSystem.GetOcclusionMeshScale(),
				foveatedRenderingInfo = xrRenderPass.foveatedRenderingInfo,
				multipassId = layout.GetActivePasses().Count,
				cullingPassId = xrRenderPass.cullingPassIndex,
				copyDepth = xrRenderPass.shouldFillOutDepth,
				xrSdkRenderPass = xrRenderPass
			};
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004738 File Offset: 0x00002938
		[CompilerGenerated]
		internal static void <CreateDefaultLayout>g__AddViewToPass|44_0(XRPass xrPass, XRDisplaySubsystem.XRRenderPass renderPass, int renderParamIndex, ref XRSystem.<>c__DisplayClass44_0 A_3)
		{
			XRDisplaySubsystem.XRRenderParameter renderParam;
			renderPass.GetRenderParameter(A_3.camera, renderParamIndex, out renderParam);
			xrPass.AddView(XRSystem.BuildView(renderPass, renderParam));
		}

		// Token: 0x04000064 RID: 100
		private static XRLayoutStack s_Layout = new XRLayoutStack();

		// Token: 0x04000065 RID: 101
		private static Func<XRPassCreateInfo, XRPass> s_PassAllocator = null;

		// Token: 0x04000066 RID: 102
		private static List<XRDisplaySubsystem> s_DisplayList = new List<XRDisplaySubsystem>();

		// Token: 0x04000067 RID: 103
		private static XRDisplaySubsystem s_Display;

		// Token: 0x04000068 RID: 104
		private static MSAASamples s_MSAASamples = MSAASamples.None;

		// Token: 0x04000069 RID: 105
		private static float s_OcclusionMeshScaling = 1f;

		// Token: 0x0400006A RID: 106
		private static Material s_OcclusionMeshMaterial;

		// Token: 0x0400006B RID: 107
		private static Material s_MirrorViewMaterial;

		// Token: 0x0400006C RID: 108
		private static Action<XRLayout, Camera> s_LayoutOverride = null;

		// Token: 0x0400006D RID: 109
		public static readonly XRPass emptyPass = new XRPass();
	}
}
