using System;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000196 RID: 406
	internal static class StpUtils
	{
		// Token: 0x060008AC RID: 2220 RVA: 0x0002947A File Offset: 0x0002767A
		private static void CalculateJitter(int frameIndex, out Vector2 jitter, out bool allowScaling)
		{
			jitter = -STP.Jit16(frameIndex);
			allowScaling = false;
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00029490 File Offset: 0x00027690
		private static void PopulateStpConfig(UniversalCameraData cameraData, TextureHandle inputColor, TextureHandle inputDepth, TextureHandle inputMotion, int debugViewIndex, TextureHandle debugView, TextureHandle destination, Texture2D noiseTexture, out STP.Config config)
		{
			UniversalAdditionalCameraData additionalCameraData;
			cameraData.camera.TryGetComponent<UniversalAdditionalCameraData>(out additionalCameraData);
			MotionVectorsPersistentData motionData = additionalCameraData.motionVectorsPersistentData;
			config.enableHwDrs = false;
			config.enableTexArray = cameraData.xr.enabled && cameraData.xr.singlePassEnabled;
			config.enableMotionScaling = true;
			config.noiseTexture = noiseTexture;
			config.inputColor = inputColor;
			config.inputDepth = inputDepth;
			config.inputMotion = inputMotion;
			config.inputStencil = TextureHandle.nullHandle;
			config.stencilMask = 0;
			config.debugView = debugView;
			config.destination = destination;
			StpHistory stpHistory = cameraData.stpHistory;
			int eyeIndex = ((cameraData.xr.enabled && !cameraData.xr.singlePassEnabled) ? cameraData.xr.multipassId : 0);
			config.historyContext = stpHistory.GetHistoryContext(eyeIndex);
			config.nearPlane = cameraData.camera.nearClipPlane;
			config.farPlane = cameraData.camera.farClipPlane;
			config.frameIndex = TemporalAA.CalculateTaaFrameIndex(ref cameraData.taaSettings);
			config.hasValidHistory = !cameraData.resetHistory;
			config.debugViewIndex = debugViewIndex;
			config.deltaTime = motionData.deltaTime;
			config.lastDeltaTime = motionData.lastDeltaTime;
			config.currentImageSize = new Vector2Int(cameraData.cameraTargetDescriptor.width, cameraData.cameraTargetDescriptor.height);
			config.priorImageSize = config.currentImageSize;
			config.outputImageSize = new Vector2Int(cameraData.pixelWidth, cameraData.pixelHeight);
			int numActiveViews = (cameraData.xr.enabled ? cameraData.xr.viewCount : 1);
			for (int viewIndex = 0; viewIndex < numActiveViews; viewIndex++)
			{
				int targetIndex = viewIndex + eyeIndex;
				STP.PerViewConfig perViewConfig;
				perViewConfig.currentProj = motionData.projectionStereo[targetIndex];
				perViewConfig.lastProj = motionData.previousProjectionStereo[targetIndex];
				perViewConfig.lastLastProj = motionData.previousPreviousProjectionStereo[targetIndex];
				perViewConfig.currentView = motionData.viewStereo[targetIndex];
				perViewConfig.lastView = motionData.previousViewStereo[targetIndex];
				perViewConfig.lastLastView = motionData.previousPreviousViewStereo[targetIndex];
				Vector3 currentPosition = motionData.worldSpaceCameraPos;
				Vector3 lastPosition = motionData.previousWorldSpaceCameraPos;
				Vector3 lastLastPosition = motionData.previousPreviousWorldSpaceCameraPos;
				perViewConfig.currentView.SetColumn(3, new Vector4(-currentPosition.x, -currentPosition.y, -currentPosition.z, 1f));
				perViewConfig.lastView.SetColumn(3, new Vector4(-lastPosition.x, -lastPosition.y, -lastPosition.z, 1f));
				perViewConfig.lastLastView.SetColumn(3, new Vector4(-lastLastPosition.x, -lastLastPosition.y, -lastLastPosition.z, 1f));
				STP.perViewConfigs[viewIndex] = perViewConfig;
			}
			config.numActiveViews = numActiveViews;
			config.perViewConfigs = STP.perViewConfigs;
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x00029794 File Offset: 0x00027994
		internal static void Execute(RenderGraph renderGraph, UniversalResourceData resourceData, UniversalCameraData cameraData, TextureHandle inputColor, TextureHandle inputDepth, TextureHandle inputMotion, TextureHandle destination, Texture2D noiseTexture)
		{
			TextureHandle debugView = TextureHandle.nullHandle;
			int debugViewIndex = 0;
			DebugHandler debugHandler = ScriptableRenderPass.GetActiveDebugHandler(cameraData);
			DebugFullScreenMode fullscreenDebugMode;
			if (debugHandler != null && debugHandler.TryGetFullscreenDebugMode(out fullscreenDebugMode) && fullscreenDebugMode == DebugFullScreenMode.STP)
			{
				TextureDesc textureDesc = new TextureDesc(cameraData.pixelWidth, cameraData.pixelHeight, false, cameraData.xr.enabled && cameraData.xr.singlePassEnabled);
				textureDesc.name = "STP Debug View";
				textureDesc.format = GraphicsFormat.R8G8B8A8_UNorm;
				textureDesc.clearBuffer = true;
				textureDesc.enableRandomWrite = true;
				debugView = renderGraph.CreateTexture(in textureDesc);
				debugViewIndex = debugHandler.stpDebugViewIndex;
				resourceData.stpDebugView = debugView;
			}
			STP.Config config;
			StpUtils.PopulateStpConfig(cameraData, inputColor, inputDepth, inputMotion, debugViewIndex, debugView, destination, noiseTexture, out config);
			STP.Execute(renderGraph, ref config);
		}

		// Token: 0x040008F6 RID: 2294
		internal static TemporalAA.JitterFunc s_JitterFunc = new TemporalAA.JitterFunc(StpUtils.CalculateJitter);
	}
}
