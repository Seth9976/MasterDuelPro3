using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering
{
	// Token: 0x0200014C RID: 332
	public sealed class LensFlareCommonSRP
	{
		// Token: 0x06000A28 RID: 2600 RVA: 0x000022CB File Offset: 0x000004CB
		private LensFlareCommonSRP()
		{
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x000213CA File Offset: 0x0001F5CA
		public static bool IsOcclusionRTCompatible()
		{
			return SystemInfo.graphicsDeviceType != GraphicsDeviceType.OpenGLES3 && SystemInfo.graphicsDeviceType != GraphicsDeviceType.OpenGLCore && SystemInfo.graphicsDeviceType != GraphicsDeviceType.Null && SystemInfo.graphicsDeviceType != GraphicsDeviceType.WebGPU && (LensFlareCommonSRP.s_SupportsLensFlare16bitsFormat || LensFlareCommonSRP.s_SupportsLensFlare32bitsFormat);
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x000213FF File Offset: 0x0001F5FF
		private static GraphicsFormat GetOcclusionRTFormat()
		{
			if (LensFlareCommonSRP.s_SupportsLensFlare16bitsFormat)
			{
				return GraphicsFormat.R16_SFloat;
			}
			return GraphicsFormat.R32_SFloat;
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x00021410 File Offset: 0x0001F610
		public static void Initialize()
		{
			LensFlareCommonSRP.frameIdx = 0;
			if (LensFlareCommonSRP.IsOcclusionRTCompatible() && LensFlareCommonSRP.occlusionRT == null)
			{
				LensFlareCommonSRP.occlusionRT = RTHandles.Alloc(LensFlareCommonSRP.maxLensFlareWithOcclusion, Mathf.Max(LensFlareCommonSRP.mergeNeeded * (LensFlareCommonSRP.maxLensFlareWithOcclusionTemporalSample + 1), 1), LensFlareCommonSRP.GetOcclusionRTFormat(), TextureXR.slices, FilterMode.Point, TextureWrapMode.Repeat, TextureDimension.Tex2DArray, true, false, true, false, 1, 0f, MSAASamples.None, false, false, false, RenderTextureMemoryless.None, VRTextureUsage.None, "");
			}
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x00021475 File Offset: 0x0001F675
		public static void Dispose()
		{
			if (LensFlareCommonSRP.IsOcclusionRTCompatible() && LensFlareCommonSRP.occlusionRT != null)
			{
				RTHandles.Release(LensFlareCommonSRP.occlusionRT);
				LensFlareCommonSRP.occlusionRT = null;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000A2D RID: 2605 RVA: 0x00021498 File Offset: 0x0001F698
		public static LensFlareCommonSRP Instance
		{
			get
			{
				if (LensFlareCommonSRP.m_Instance == null)
				{
					object padlock = LensFlareCommonSRP.m_Padlock;
					lock (padlock)
					{
						if (LensFlareCommonSRP.m_Instance == null)
						{
							LensFlareCommonSRP.m_Instance = new LensFlareCommonSRP();
						}
					}
				}
				return LensFlareCommonSRP.m_Instance;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000A2E RID: 2606 RVA: 0x000214F0 File Offset: 0x0001F6F0
		private List<LensFlareCommonSRP.LensFlareCompInfo> Data
		{
			get
			{
				return LensFlareCommonSRP.m_Data;
			}
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x000214F7 File Offset: 0x0001F6F7
		public bool IsEmpty()
		{
			return this.Data.Count == 0;
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x00021508 File Offset: 0x0001F708
		private int GetNextAvailableIndex()
		{
			if (LensFlareCommonSRP.m_AvailableIndicies.Count == 0)
			{
				return LensFlareCommonSRP.m_Data.Count;
			}
			int num = LensFlareCommonSRP.m_AvailableIndicies[LensFlareCommonSRP.m_AvailableIndicies.Count - 1];
			LensFlareCommonSRP.m_AvailableIndicies.RemoveAt(LensFlareCommonSRP.m_AvailableIndicies.Count - 1);
			return num;
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x00021558 File Offset: 0x0001F758
		public void AddData(LensFlareComponentSRP newData)
		{
			if (!LensFlareCommonSRP.m_Data.Exists((LensFlareCommonSRP.LensFlareCompInfo x) => x.comp == newData))
			{
				LensFlareCommonSRP.m_Data.Add(new LensFlareCommonSRP.LensFlareCompInfo(this.GetNextAvailableIndex(), newData));
			}
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x000215A8 File Offset: 0x0001F7A8
		public void RemoveData(LensFlareComponentSRP data)
		{
			LensFlareCommonSRP.LensFlareCompInfo info = LensFlareCommonSRP.m_Data.Find((LensFlareCommonSRP.LensFlareCompInfo x) => x.comp == data);
			if (info != null)
			{
				int newIndex = info.index;
				LensFlareCommonSRP.m_Data.Remove(info);
				LensFlareCommonSRP.m_AvailableIndicies.Add(newIndex);
				if (LensFlareCommonSRP.m_Data.Count == 0)
				{
					LensFlareCommonSRP.m_AvailableIndicies.Clear();
				}
			}
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x000081D4 File Offset: 0x000063D4
		public static float ShapeAttenuationPointLight()
		{
			return 1f;
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x00021610 File Offset: 0x0001F810
		public static float ShapeAttenuationDirLight(Vector3 forward, Vector3 wo)
		{
			return Mathf.Max(Vector3.Dot(-forward, wo), 0f);
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x00021628 File Offset: 0x0001F828
		public static float ShapeAttenuationSpotConeLight(Vector3 forward, Vector3 wo, float spotAngle, float innerSpotPercent01)
		{
			float outerDot = Mathf.Max(Mathf.Cos(0.5f * spotAngle * 0.017453292f), 0f);
			float innerDot = Mathf.Max(Mathf.Cos(0.5f * spotAngle * 0.017453292f * innerSpotPercent01), 0f);
			return Mathf.Clamp01((Mathf.Max(Vector3.Dot(forward, wo), 0f) - outerDot) / (innerDot - outerDot));
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0002168D File Offset: 0x0001F88D
		public static float ShapeAttenuationSpotBoxLight(Vector3 forward, Vector3 wo)
		{
			return Mathf.Max(Mathf.Sign(Vector3.Dot(forward, wo)), 0f);
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x000216A5 File Offset: 0x0001F8A5
		public static float ShapeAttenuationSpotPyramidLight(Vector3 forward, Vector3 wo)
		{
			return LensFlareCommonSRP.ShapeAttenuationSpotBoxLight(forward, wo);
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x000216B0 File Offset: 0x0001F8B0
		public static float ShapeAttenuationAreaTubeLight(Vector3 lightPositionWS, Vector3 lightSide, float lightWidth, Camera cam)
		{
			Vector3 p1Global = lightPositionWS + lightSide * lightWidth * 0.5f;
			Vector3 p2Global = lightPositionWS - lightSide * lightWidth * 0.5f;
			Vector3 p1Front = lightPositionWS + cam.transform.right * lightWidth * 0.5f;
			Vector3 p2Front = lightPositionWS - cam.transform.right * lightWidth * 0.5f;
			Vector3 vector = cam.transform.InverseTransformPoint(p1Global);
			Vector3 p2World = cam.transform.InverseTransformPoint(p2Global);
			Vector3 vector2 = cam.transform.InverseTransformPoint(p1Front);
			Vector3 p2WorldFront = cam.transform.InverseTransformPoint(p2Front);
			float frontModulation = LensFlareCommonSRP.<ShapeAttenuationAreaTubeLight>g__DiffLineIntegral|57_2(vector2, p2WorldFront);
			float worldModulation = LensFlareCommonSRP.<ShapeAttenuationAreaTubeLight>g__DiffLineIntegral|57_2(vector, p2World);
			if (frontModulation <= 0f)
			{
				return 1f;
			}
			return worldModulation / frontModulation;
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0002178B File Offset: 0x0001F98B
		private static float ShapeAttenuateForwardLight(Vector3 forward, Vector3 wo)
		{
			return Mathf.Max(Vector3.Dot(forward, wo), 0f);
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0002179E File Offset: 0x0001F99E
		public static float ShapeAttenuationAreaRectangleLight(Vector3 forward, Vector3 wo)
		{
			return LensFlareCommonSRP.ShapeAttenuateForwardLight(forward, wo);
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x0002179E File Offset: 0x0001F99E
		public static float ShapeAttenuationAreaDiscLight(Vector3 forward, Vector3 wo)
		{
			return LensFlareCommonSRP.ShapeAttenuateForwardLight(forward, wo);
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x000217A8 File Offset: 0x0001F9A8
		private static bool IsLensFlareSRPHidden(Camera cam, LensFlareComponentSRP comp, LensFlareDataSRP data)
		{
			return !comp.enabled || !comp.gameObject.activeSelf || !comp.gameObject.activeInHierarchy || data == null || data.elements == null || data.elements.Length == 0 || comp.intensity <= 0f || (cam.cullingMask & (1 << comp.gameObject.layer)) == 0;
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0002181C File Offset: 0x0001FA1C
		public static Vector4 GetFlareData0(Vector2 screenPos, Vector2 translationScale, Vector2 rayOff0, Vector2 vLocalScreenRatio, float angleDeg, float position, float angularOffset, Vector2 positionOffset, bool autoRotate)
		{
			if (!SystemInfo.graphicsUVStartsAtTop)
			{
				angleDeg *= -1f;
				positionOffset.y *= -1f;
			}
			float globalCos0 = Mathf.Cos(-angularOffset * 0.017453292f);
			float globalSin0 = Mathf.Sin(-angularOffset * 0.017453292f);
			Vector2 rayOff = -translationScale * (screenPos + screenPos * (position - 1f));
			rayOff = new Vector2(globalCos0 * rayOff.x - globalSin0 * rayOff.y, globalSin0 * rayOff.x + globalCos0 * rayOff.y);
			float rotation = angleDeg;
			rotation += 180f;
			if (autoRotate)
			{
				Vector2 pos = rayOff.normalized * vLocalScreenRatio * translationScale;
				rotation += -57.29578f * Mathf.Atan2(pos.y, pos.x);
			}
			rotation *= 0.017453292f;
			float localCos0 = Mathf.Cos(-rotation);
			float localSin0 = Mathf.Sin(-rotation);
			return new Vector4(localCos0, localSin0, positionOffset.x + rayOff0.x * translationScale.x, -positionOffset.y + rayOff0.y * translationScale.y);
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x00021940 File Offset: 0x0001FB40
		private static Vector2 GetLensFlareRayOffset(Vector2 screenPos, float position, float globalCos0, float globalSin0, Vector2 vAspectRatio)
		{
			Vector2 rayOff = -(screenPos + screenPos * (position - 1f));
			return new Vector2(globalCos0 * rayOff.x - globalSin0 * rayOff.y, globalSin0 * rayOff.x + globalCos0 * rayOff.y);
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0002198D File Offset: 0x0001FB8D
		private static Vector3 WorldToViewport(Camera camera, bool isLocalLight, bool isCameraRelative, Matrix4x4 viewProjMatrix, Vector3 positionWS)
		{
			if (isLocalLight)
			{
				return LensFlareCommonSRP.WorldToViewportLocal(isCameraRelative, viewProjMatrix, camera.transform.position, positionWS);
			}
			return LensFlareCommonSRP.WorldToViewportDistance(camera, positionWS);
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x000219B0 File Offset: 0x0001FBB0
		private static Vector3 WorldToViewportLocal(bool isCameraRelative, Matrix4x4 viewProjMatrix, Vector3 cameraPosWS, Vector3 positionWS)
		{
			Vector3 localPositionWS = positionWS;
			if (isCameraRelative)
			{
				localPositionWS -= cameraPosWS;
			}
			Vector4 viewportPos4 = viewProjMatrix * localPositionWS;
			Vector3 viewportPos5 = new Vector3(viewportPos4.x, viewportPos4.y, 0f);
			viewportPos5 /= viewportPos4.w;
			viewportPos5.x = viewportPos5.x * 0.5f + 0.5f;
			viewportPos5.y = viewportPos5.y * 0.5f + 0.5f;
			viewportPos5.y = 1f - viewportPos5.y;
			viewportPos5.z = viewportPos4.w;
			return viewportPos5;
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x00021A50 File Offset: 0x0001FC50
		private static Vector3 WorldToViewportDistance(Camera cam, Vector3 positionWS)
		{
			Vector4 camPos = cam.worldToCameraMatrix * positionWS;
			Vector4 viewportPos4 = cam.projectionMatrix * camPos;
			Vector3 viewportPos5 = new Vector3(viewportPos4.x, viewportPos4.y, 0f);
			viewportPos5 /= viewportPos4.w;
			viewportPos5.x = viewportPos5.x * 0.5f + 0.5f;
			viewportPos5.y = viewportPos5.y * 0.5f + 0.5f;
			viewportPos5.z = viewportPos4.w;
			return viewportPos5;
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x00021AE4 File Offset: 0x0001FCE4
		public static bool IsCloudLayerOpacityNeeded(Camera cam)
		{
			if (LensFlareCommonSRP.Instance.IsEmpty())
			{
				return false;
			}
			foreach (LensFlareCommonSRP.LensFlareCompInfo info in LensFlareCommonSRP.Instance.Data)
			{
				if (info != null && !(info.comp == null))
				{
					LensFlareComponentSRP comp = info.comp;
					LensFlareDataSRP data = comp.lensFlareData;
					if (!LensFlareCommonSRP.IsLensFlareSRPHidden(cam, comp, data) && comp.useOcclusion && (!comp.useOcclusion || comp.sampleCount != 0U) && comp.environmentOcclusion)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x00021B94 File Offset: 0x0001FD94
		private static void SetOcclusionPermutation(CommandBuffer cmd, bool useFogOpacityOcclusion, int _FlareSunOcclusionTex, Texture sunOcclusionTexture)
		{
			uint occlusionPermutation = 1U;
			if (useFogOpacityOcclusion && sunOcclusionTexture != null)
			{
				occlusionPermutation |= 4U;
				cmd.SetGlobalTexture(_FlareSunOcclusionTex, sunOcclusionTexture);
			}
			int convInt = (int)occlusionPermutation;
			cmd.SetGlobalInt(LensFlareCommonSRP._FlareOcclusionPermutation, convInt);
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x00021BD0 File Offset: 0x0001FDD0
		[Obsolete("Use ComputeOcclusion without _FlareOcclusionTex.._FlareData4 parameters.")]
		public static void ComputeOcclusion(Material lensFlareShader, Camera cam, XRPass xr, int xrIndex, float actualWidth, float actualHeight, bool usePanini, float paniniDistance, float paniniCropToFit, bool isCameraRelative, Vector3 cameraPositionWS, Matrix4x4 viewProjMatrix, UnsafeCommandBuffer cmd, bool taaEnabled, bool hasCloudLayer, Texture cloudOpacityTexture, Texture sunOcclusionTexture, int _FlareOcclusionTex, int _FlareCloudOpacity, int _FlareOcclusionIndex, int _FlareTex, int _FlareColorValue, int _FlareSunOcclusionTex, int _FlareData0, int _FlareData1, int _FlareData2, int _FlareData3, int _FlareData4)
		{
			LensFlareCommonSRP.ComputeOcclusion(lensFlareShader, cam, xr, xrIndex, actualWidth, actualHeight, usePanini, paniniDistance, paniniCropToFit, isCameraRelative, cameraPositionWS, viewProjMatrix, cmd.m_WrappedCommandBuffer, taaEnabled, hasCloudLayer, cloudOpacityTexture, sunOcclusionTexture, _FlareOcclusionTex, _FlareCloudOpacity, _FlareOcclusionIndex, _FlareTex, _FlareColorValue, _FlareSunOcclusionTex, _FlareData0, _FlareData1, _FlareData2, _FlareData3, _FlareData4);
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x00021C1C File Offset: 0x0001FE1C
		public static void ComputeOcclusion(Material lensFlareShader, Camera cam, XRPass xr, int xrIndex, float actualWidth, float actualHeight, bool usePanini, float paniniDistance, float paniniCropToFit, bool isCameraRelative, Vector3 cameraPositionWS, Matrix4x4 viewProjMatrix, UnsafeCommandBuffer cmd, bool taaEnabled, bool hasCloudLayer, Texture cloudOpacityTexture, Texture sunOcclusionTexture)
		{
			LensFlareCommonSRP.ComputeOcclusion(lensFlareShader, cam, xr, xrIndex, actualWidth, actualHeight, usePanini, paniniDistance, paniniCropToFit, isCameraRelative, cameraPositionWS, viewProjMatrix, cmd.m_WrappedCommandBuffer, taaEnabled, hasCloudLayer, cloudOpacityTexture, sunOcclusionTexture);
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x00021C54 File Offset: 0x0001FE54
		[Obsolete("Use ComputeOcclusion without _FlareOcclusionTex.._FlareData4 parameters.")]
		public static void ComputeOcclusion(Material lensFlareShader, Camera cam, XRPass xr, int xrIndex, float actualWidth, float actualHeight, bool usePanini, float paniniDistance, float paniniCropToFit, bool isCameraRelative, Vector3 cameraPositionWS, Matrix4x4 viewProjMatrix, CommandBuffer cmd, bool taaEnabled, bool hasCloudLayer, Texture cloudOpacityTexture, Texture sunOcclusionTexture, int _FlareOcclusionTex, int _FlareCloudOpacity, int _FlareOcclusionIndex, int _FlareTex, int _FlareColorValue, int _FlareSunOcclusionTex, int _FlareData0, int _FlareData1, int _FlareData2, int _FlareData3, int _FlareData4)
		{
			LensFlareCommonSRP.ComputeOcclusion(lensFlareShader, cam, xr, xrIndex, actualWidth, actualHeight, usePanini, paniniDistance, paniniCropToFit, isCameraRelative, cameraPositionWS, viewProjMatrix, cmd, taaEnabled, hasCloudLayer, cloudOpacityTexture, sunOcclusionTexture);
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x00021C84 File Offset: 0x0001FE84
		private static bool ForceSingleElement(LensFlareDataElementSRP element)
		{
			return !element.allowMultipleElement || element.count == 1 || element.flareType == SRPLensFlareType.Ring;
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x00021CA4 File Offset: 0x0001FEA4
		public static void ComputeOcclusion(Material lensFlareShader, Camera cam, XRPass xr, int xrIndex, float actualWidth, float actualHeight, bool usePanini, float paniniDistance, float paniniCropToFit, bool isCameraRelative, Vector3 cameraPositionWS, Matrix4x4 viewProjMatrix, CommandBuffer cmd, bool taaEnabled, bool hasCloudLayer, Texture cloudOpacityTexture, Texture sunOcclusionTexture)
		{
			if (!LensFlareCommonSRP.IsOcclusionRTCompatible())
			{
				return;
			}
			xr.StopSinglePass(cmd);
			if (LensFlareCommonSRP.Instance.IsEmpty())
			{
				return;
			}
			Vector2 screenSize = new Vector2(actualWidth, actualHeight);
			float screenRatio = screenSize.x / screenSize.y;
			Vector2 vScreenRatio = new Vector2(screenRatio, 1f);
			if (xr.enabled && xr.singlePassEnabled)
			{
				CoreUtils.SetRenderTarget(cmd, LensFlareCommonSRP.occlusionRT, ClearFlag.None, 0, CubemapFace.Unknown, xrIndex);
				cmd.SetGlobalInt(LensFlareCommonSRP._ViewId, xrIndex);
			}
			else
			{
				CoreUtils.SetRenderTarget(cmd, LensFlareCommonSRP.occlusionRT, ClearFlag.None, 0, CubemapFace.Unknown, -1);
				if (xr.enabled)
				{
					cmd.SetGlobalInt(LensFlareCommonSRP._ViewId, xr.multipassId);
				}
				else
				{
					cmd.SetGlobalInt(LensFlareCommonSRP._ViewId, -1);
				}
			}
			if (!taaEnabled)
			{
				cmd.ClearRenderTarget(false, true, Color.black);
			}
			float num = 1f / (float)LensFlareCommonSRP.maxLensFlareWithOcclusion;
			float num2 = 1f / (float)(LensFlareCommonSRP.maxLensFlareWithOcclusionTemporalSample + LensFlareCommonSRP.mergeNeeded);
			float num3 = 0.5f / (float)LensFlareCommonSRP.maxLensFlareWithOcclusion;
			float num4 = 0.5f / (float)(LensFlareCommonSRP.maxLensFlareWithOcclusionTemporalSample + LensFlareCommonSRP.mergeNeeded);
			foreach (LensFlareCommonSRP.LensFlareCompInfo info in LensFlareCommonSRP.m_Data)
			{
				if (info != null && !(info.comp == null))
				{
					LensFlareComponentSRP comp = info.comp;
					LensFlareDataSRP data = comp.lensFlareData;
					if (!LensFlareCommonSRP.IsLensFlareSRPHidden(cam, comp, data) && comp.useOcclusion && (!comp.useOcclusion || comp.sampleCount != 0U))
					{
						Light light = null;
						if (!comp.TryGetComponent<Light>(out light))
						{
							light = null;
						}
						bool isDirLight = false;
						Vector3 positionWS;
						if (light != null && light.type == LightType.Directional)
						{
							positionWS = -light.transform.forward * cam.farClipPlane;
							isDirLight = true;
						}
						else
						{
							positionWS = comp.transform.position;
						}
						Vector3 viewportPos = LensFlareCommonSRP.WorldToViewport(cam, !isDirLight, isCameraRelative, viewProjMatrix, positionWS);
						if (usePanini && cam == Camera.main)
						{
							viewportPos = LensFlareCommonSRP.DoPaniniProjection(viewportPos, actualWidth, actualHeight, cam.fieldOfView, paniniCropToFit, paniniDistance);
						}
						if (viewportPos.z >= 0f && (comp.allowOffScreen || (viewportPos.x >= 0f && viewportPos.x <= 1f && viewportPos.y >= 0f && viewportPos.y <= 1f)))
						{
							Vector3 diffToObject = positionWS - cameraPositionWS;
							if (Vector3.Dot(cam.transform.forward, diffToObject) >= 0f)
							{
								float magnitude = diffToObject.magnitude;
								float coefDistSample = magnitude / comp.maxAttenuationDistance;
								float coefScaleSample = magnitude / comp.maxAttenuationScale;
								float distanceAttenuation = ((!isDirLight && comp.distanceAttenuationCurve.length > 0) ? comp.distanceAttenuationCurve.Evaluate(coefDistSample) : 1f);
								if (!isDirLight && comp.scaleByDistanceCurve.length >= 1)
								{
									comp.scaleByDistanceCurve.Evaluate(coefScaleSample);
								}
								Vector3 dir;
								if (isDirLight)
								{
									dir = comp.transform.forward;
								}
								else
								{
									dir = (cam.transform.position - comp.transform.position).normalized;
								}
								Vector3 screenPosZ = LensFlareCommonSRP.WorldToViewport(cam, !isDirLight, isCameraRelative, viewProjMatrix, positionWS + dir * comp.occlusionOffset);
								float adjustedOcclusionRadius = (isDirLight ? comp.celestialProjectedOcclusionRadius(cam) : comp.occlusionRadius);
								Vector2 occlusionRadiusEdgeScreenPos0 = viewportPos;
								float occlusionRadius = (LensFlareCommonSRP.WorldToViewport(cam, !isDirLight, isCameraRelative, viewProjMatrix, positionWS + cam.transform.up * adjustedOcclusionRadius) - occlusionRadiusEdgeScreenPos0).magnitude;
								cmd.SetGlobalVector(LensFlareCommonSRP._FlareData1, new Vector4(occlusionRadius, comp.sampleCount, screenPosZ.z, actualHeight / actualWidth));
								LensFlareCommonSRP.SetOcclusionPermutation(cmd, comp.environmentOcclusion, LensFlareCommonSRP._FlareSunOcclusionTex, sunOcclusionTexture);
								cmd.EnableShaderKeyword("FLARE_COMPUTE_OCCLUSION");
								Vector2 screenPos = new Vector2(2f * viewportPos.x - 1f, -(2f * viewportPos.y - 1f));
								if (!SystemInfo.graphicsUVStartsAtTop && isDirLight)
								{
									screenPos.y = -screenPos.y;
								}
								Vector2 radPos = new Vector2(Mathf.Abs(screenPos.x), Mathf.Abs(screenPos.y));
								float radius = Mathf.Max(radPos.x, radPos.y);
								float radialsScaleRadius = ((comp.radialScreenAttenuationCurve.length > 0) ? comp.radialScreenAttenuationCurve.Evaluate(radius) : 1f);
								if (comp.intensity * radialsScaleRadius * distanceAttenuation > 0f)
								{
									float globalCos0 = Mathf.Cos(0f);
									float globalSin0 = Mathf.Sin(0f);
									float position = 0f;
									float usedGradientPosition = Mathf.Clamp01(0.999999f);
									cmd.SetGlobalVector(LensFlareCommonSRP._FlareData3, new Vector4(comp.allowOffScreen ? 1f : (-1f), usedGradientPosition, Mathf.Exp(Mathf.Lerp(0f, 4f, 1f)), 0.33333334f));
									Vector2 rayOff = LensFlareCommonSRP.GetLensFlareRayOffset(screenPos, position, globalCos0, globalSin0, vScreenRatio);
									Vector4 flareData0 = LensFlareCommonSRP.GetFlareData0(screenPos, Vector2.one, rayOff, vScreenRatio, 0f, position, 0f, Vector2.zero, false);
									cmd.SetGlobalVector(LensFlareCommonSRP._FlareData0, flareData0);
									cmd.SetGlobalVector(LensFlareCommonSRP._FlareData2, new Vector4(screenPos.x, screenPos.y, 0f, 0f));
									Rect rect;
									if (taaEnabled)
									{
										rect = new Rect
										{
											x = (float)info.index,
											y = (float)(LensFlareCommonSRP.frameIdx + LensFlareCommonSRP.mergeNeeded),
											width = 1f,
											height = 1f
										};
									}
									else
									{
										rect = new Rect
										{
											x = (float)info.index,
											y = 0f,
											width = 1f,
											height = 1f
										};
									}
									cmd.SetViewport(rect);
									Blitter.DrawQuad(cmd, lensFlareShader, lensFlareShader.FindPass("LensFlareOcclusion"));
								}
							}
						}
					}
				}
			}
			if (taaEnabled)
			{
				CoreUtils.SetRenderTarget(cmd, LensFlareCommonSRP.occlusionRT, ClearFlag.None, 0, CubemapFace.Unknown, xrIndex);
				cmd.SetViewport(new Rect
				{
					x = (float)LensFlareCommonSRP.m_Data.Count,
					y = 0f,
					width = (float)(LensFlareCommonSRP.maxLensFlareWithOcclusion - LensFlareCommonSRP.m_Data.Count),
					height = (float)(LensFlareCommonSRP.maxLensFlareWithOcclusionTemporalSample + LensFlareCommonSRP.mergeNeeded)
				});
				cmd.ClearRenderTarget(false, true, Color.black);
			}
			LensFlareCommonSRP.frameIdx++;
			LensFlareCommonSRP.frameIdx %= LensFlareCommonSRP.maxLensFlareWithOcclusionTemporalSample;
			xr.StartSinglePass(cmd);
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x000223CC File Offset: 0x000205CC
		public static void ProcessLensFlareSRPElementsSingle(LensFlareDataElementSRP element, CommandBuffer cmd, Color globalColorModulation, Light light, float compIntensity, float scale, Material lensFlareShader, Vector2 screenPos, bool compAllowOffScreen, Vector2 vScreenRatio, Vector4 flareData1, bool preview, int depth)
		{
			LensFlareCommonSRP.<>c__DisplayClass74_0 CS$<>8__locals1;
			CS$<>8__locals1.screenPos = screenPos;
			CS$<>8__locals1.vScreenRatio = vScreenRatio;
			CS$<>8__locals1.element = element;
			if (CS$<>8__locals1.element == null || !CS$<>8__locals1.element.visible || (CS$<>8__locals1.element.lensFlareTexture == null && CS$<>8__locals1.element.flareType == SRPLensFlareType.Image) || CS$<>8__locals1.element.localIntensity <= 0f || CS$<>8__locals1.element.count <= 0 || (CS$<>8__locals1.element.flareType == SRPLensFlareType.LensFlareDataSRP && CS$<>8__locals1.element.lensFlareDataSRP == null))
			{
				return;
			}
			if (CS$<>8__locals1.element.flareType == SRPLensFlareType.LensFlareDataSRP && CS$<>8__locals1.element.lensFlareDataSRP != null)
			{
				LensFlareCommonSRP.ProcessLensFlareSRPElements(ref CS$<>8__locals1.element.lensFlareDataSRP.elements, cmd, globalColorModulation, light, compIntensity, scale, lensFlareShader, CS$<>8__locals1.screenPos, compAllowOffScreen, CS$<>8__locals1.vScreenRatio, flareData1, preview, depth + 1);
				return;
			}
			Color colorModulation = globalColorModulation;
			if (light != null && CS$<>8__locals1.element.modulateByLightColor)
			{
				if (light.useColorTemperature)
				{
					colorModulation *= light.color * Mathf.CorrelatedColorTemperatureToRGB(light.colorTemperature);
				}
				else
				{
					colorModulation *= light.color;
				}
			}
			Color curColor = colorModulation;
			float currentIntensity = CS$<>8__locals1.element.localIntensity * compIntensity;
			if (currentIntensity <= 0f)
			{
				return;
			}
			Texture texture = CS$<>8__locals1.element.lensFlareTexture;
			if (CS$<>8__locals1.element.flareType == SRPLensFlareType.Image)
			{
				CS$<>8__locals1.usedAspectRatio = (CS$<>8__locals1.element.preserveAspectRatio ? ((float)texture.height / (float)texture.width) : 1f);
			}
			else
			{
				CS$<>8__locals1.usedAspectRatio = 1f;
			}
			float rotation = CS$<>8__locals1.element.rotation;
			Vector2 elemSizeXY;
			if (CS$<>8__locals1.element.preserveAspectRatio)
			{
				if (CS$<>8__locals1.usedAspectRatio >= 1f)
				{
					elemSizeXY = new Vector2(CS$<>8__locals1.element.sizeXY.x / CS$<>8__locals1.usedAspectRatio, CS$<>8__locals1.element.sizeXY.y);
				}
				else
				{
					elemSizeXY = new Vector2(CS$<>8__locals1.element.sizeXY.x, CS$<>8__locals1.element.sizeXY.y * CS$<>8__locals1.usedAspectRatio);
				}
			}
			else
			{
				elemSizeXY = new Vector2(CS$<>8__locals1.element.sizeXY.x, CS$<>8__locals1.element.sizeXY.y);
			}
			float scaleSize = 0.1f;
			Vector2 size = new Vector2(elemSizeXY.x, elemSizeXY.y);
			CS$<>8__locals1.combinedScale = scaleSize * CS$<>8__locals1.element.uniformScale * scale;
			size *= CS$<>8__locals1.combinedScale;
			curColor *= CS$<>8__locals1.element.tint;
			float angularOffset = (SystemInfo.graphicsUVStartsAtTop ? CS$<>8__locals1.element.angularOffset : (-CS$<>8__locals1.element.angularOffset));
			CS$<>8__locals1.globalCos0 = Mathf.Cos(-angularOffset * 0.017453292f);
			CS$<>8__locals1.globalSin0 = Mathf.Sin(-angularOffset * 0.017453292f);
			CS$<>8__locals1.position = 2f * CS$<>8__locals1.element.position;
			SRPLensFlareBlendMode blendMode = CS$<>8__locals1.element.blendMode;
			int materialPass;
			if (blendMode == SRPLensFlareBlendMode.Additive)
			{
				materialPass = lensFlareShader.FindPass("LensFlareAdditive");
			}
			else if (blendMode == SRPLensFlareBlendMode.Screen)
			{
				materialPass = lensFlareShader.FindPass("LensFlareScreen");
			}
			else if (blendMode == SRPLensFlareBlendMode.Premultiply)
			{
				materialPass = lensFlareShader.FindPass("LensFlarePremultiply");
			}
			else if (blendMode == SRPLensFlareBlendMode.Lerp)
			{
				materialPass = lensFlareShader.FindPass("LensFlareLerp");
			}
			else
			{
				materialPass = lensFlareShader.FindPass("LensFlareOcclusion");
			}
			flareData1.x = (float)CS$<>8__locals1.element.flareType;
			if (LensFlareCommonSRP.ForceSingleElement(CS$<>8__locals1.element))
			{
				cmd.SetGlobalVector(LensFlareCommonSRP._FlareData1, flareData1);
			}
			if (CS$<>8__locals1.element.flareType == SRPLensFlareType.Circle || CS$<>8__locals1.element.flareType == SRPLensFlareType.Polygon || CS$<>8__locals1.element.flareType == SRPLensFlareType.Ring)
			{
				if (CS$<>8__locals1.element.inverseSDF)
				{
					cmd.EnableShaderKeyword("FLARE_INVERSE_SDF");
				}
				else
				{
					cmd.DisableShaderKeyword("FLARE_INVERSE_SDF");
				}
			}
			else
			{
				cmd.DisableShaderKeyword("FLARE_INVERSE_SDF");
			}
			if (CS$<>8__locals1.element.lensFlareTexture != null)
			{
				cmd.SetGlobalTexture(LensFlareCommonSRP._FlareTex, CS$<>8__locals1.element.lensFlareTexture);
			}
			if (CS$<>8__locals1.element.tintColorType != SRPLensFlareColorType.Constant)
			{
				cmd.SetGlobalTexture(LensFlareCommonSRP._FlareRadialTint, CS$<>8__locals1.element.tintGradient.GetTexture());
			}
			float usedGradientPosition = Mathf.Clamp01(1f - CS$<>8__locals1.element.edgeOffset - 1E-06f);
			if (CS$<>8__locals1.element.flareType == SRPLensFlareType.Polygon)
			{
				usedGradientPosition = Mathf.Pow(usedGradientPosition + 1f, 5f);
			}
			float usedSDFRoundness = CS$<>8__locals1.element.sdfRoundness;
			Vector4 data3 = new Vector4(compAllowOffScreen ? 1f : (-1f), usedGradientPosition, Mathf.Exp(Mathf.Lerp(0f, 4f, Mathf.Clamp01(1f - CS$<>8__locals1.element.fallOff))), (CS$<>8__locals1.element.flareType == SRPLensFlareType.Ring) ? CS$<>8__locals1.element.ringThickness : (1f / (float)CS$<>8__locals1.element.sideCount));
			cmd.SetGlobalVector(LensFlareCommonSRP._FlareData3, data3);
			if (CS$<>8__locals1.element.flareType == SRPLensFlareType.Polygon)
			{
				float invSide = 1f / (float)CS$<>8__locals1.element.sideCount;
				float num = Mathf.Cos(3.1415927f * invSide);
				float roundValue = num * usedSDFRoundness;
				float r = num - roundValue;
				float an = 6.2831855f * invSide;
				float he = r * Mathf.Tan(0.5f * an);
				cmd.SetGlobalVector(LensFlareCommonSRP._FlareData4, new Vector4(usedSDFRoundness, r, an, he));
			}
			else if (CS$<>8__locals1.element.flareType == SRPLensFlareType.Ring)
			{
				cmd.SetGlobalVector(LensFlareCommonSRP._FlareData4, new Vector4(CS$<>8__locals1.element.noiseAmplitude, (float)CS$<>8__locals1.element.noiseFrequency, CS$<>8__locals1.element.noiseSpeed, 0f));
			}
			else
			{
				cmd.SetGlobalVector(LensFlareCommonSRP._FlareData4, new Vector4(usedSDFRoundness, 0f, 0f, 0f));
			}
			cmd.SetGlobalVector(LensFlareCommonSRP._FlareData5, new Vector4((float)CS$<>8__locals1.element.tintColorType, currentIntensity, CS$<>8__locals1.element.shapeCutOffSpeed, CS$<>8__locals1.element.shapeCutOffRadius));
			if (LensFlareCommonSRP.ForceSingleElement(CS$<>8__locals1.element))
			{
				Vector2 localSize = size;
				Vector2 rayOff = LensFlareCommonSRP.GetLensFlareRayOffset(CS$<>8__locals1.screenPos, CS$<>8__locals1.position, CS$<>8__locals1.globalCos0, CS$<>8__locals1.globalSin0, CS$<>8__locals1.vScreenRatio);
				if (CS$<>8__locals1.element.enableRadialDistortion)
				{
					Vector2 rayOff2 = LensFlareCommonSRP.GetLensFlareRayOffset(CS$<>8__locals1.screenPos, 0f, CS$<>8__locals1.globalCos0, CS$<>8__locals1.globalSin0, CS$<>8__locals1.vScreenRatio);
					localSize = LensFlareCommonSRP.<ProcessLensFlareSRPElementsSingle>g__ComputeLocalSize|74_0(rayOff, rayOff2, localSize, CS$<>8__locals1.element.distortionCurve, ref CS$<>8__locals1);
				}
				Vector4 flareData2 = LensFlareCommonSRP.GetFlareData0(CS$<>8__locals1.screenPos, CS$<>8__locals1.element.translationScale, rayOff, CS$<>8__locals1.vScreenRatio, rotation, CS$<>8__locals1.position, angularOffset, CS$<>8__locals1.element.positionOffset, CS$<>8__locals1.element.autoRotate);
				cmd.SetGlobalVector(LensFlareCommonSRP._FlareData0, flareData2);
				cmd.SetGlobalVector(LensFlareCommonSRP._FlareData2, new Vector4(CS$<>8__locals1.screenPos.x, CS$<>8__locals1.screenPos.y, localSize.x, localSize.y));
				cmd.SetGlobalVector(LensFlareCommonSRP._FlareColorValue, curColor);
				Blitter.DrawQuad(cmd, lensFlareShader, materialPass);
				return;
			}
			float dLength = 2f * CS$<>8__locals1.element.lengthSpread / (float)(CS$<>8__locals1.element.count - 1);
			if (CS$<>8__locals1.element.distribution == SRPLensFlareDistribution.Uniform)
			{
				float uniformAngle = 0f;
				for (int elemIdx = 0; elemIdx < CS$<>8__locals1.element.count; elemIdx++)
				{
					Vector2 localSize2 = size;
					Vector2 rayOff3 = LensFlareCommonSRP.GetLensFlareRayOffset(CS$<>8__locals1.screenPos, CS$<>8__locals1.position, CS$<>8__locals1.globalCos0, CS$<>8__locals1.globalSin0, CS$<>8__locals1.vScreenRatio);
					if (CS$<>8__locals1.element.enableRadialDistortion)
					{
						Vector2 rayOff4 = LensFlareCommonSRP.GetLensFlareRayOffset(CS$<>8__locals1.screenPos, 0f, CS$<>8__locals1.globalCos0, CS$<>8__locals1.globalSin0, CS$<>8__locals1.vScreenRatio);
						localSize2 = LensFlareCommonSRP.<ProcessLensFlareSRPElementsSingle>g__ComputeLocalSize|74_0(rayOff3, rayOff4, localSize2, CS$<>8__locals1.element.distortionCurve, ref CS$<>8__locals1);
					}
					float timeScale = ((CS$<>8__locals1.element.count >= 2) ? ((float)elemIdx / (float)(CS$<>8__locals1.element.count - 1)) : 0.5f);
					Color col = CS$<>8__locals1.element.colorGradient.Evaluate(timeScale);
					Vector4 flareData3 = LensFlareCommonSRP.GetFlareData0(CS$<>8__locals1.screenPos, CS$<>8__locals1.element.translationScale, rayOff3, CS$<>8__locals1.vScreenRatio, rotation + uniformAngle, CS$<>8__locals1.position, angularOffset, CS$<>8__locals1.element.positionOffset, CS$<>8__locals1.element.autoRotate);
					cmd.SetGlobalVector(LensFlareCommonSRP._FlareData0, flareData3);
					flareData1.y = (float)elemIdx;
					cmd.SetGlobalVector(LensFlareCommonSRP._FlareData1, flareData1);
					cmd.SetGlobalVector(LensFlareCommonSRP._FlareData2, new Vector4(CS$<>8__locals1.screenPos.x, CS$<>8__locals1.screenPos.y, localSize2.x, localSize2.y));
					cmd.SetGlobalVector(LensFlareCommonSRP._FlareColorValue, curColor * col);
					Blitter.DrawQuad(cmd, lensFlareShader, materialPass);
					CS$<>8__locals1.position += dLength;
					uniformAngle += CS$<>8__locals1.element.uniformAngle;
				}
				return;
			}
			if (CS$<>8__locals1.element.distribution == SRPLensFlareDistribution.Random)
			{
				Random.State backupRandState = Random.state;
				Random.InitState(CS$<>8__locals1.element.seed);
				Vector2 side = new Vector2(CS$<>8__locals1.globalSin0, CS$<>8__locals1.globalCos0);
				side *= CS$<>8__locals1.element.positionVariation.y;
				for (int elemIdx2 = 0; elemIdx2 < CS$<>8__locals1.element.count; elemIdx2++)
				{
					float localIntensity = LensFlareCommonSRP.<ProcessLensFlareSRPElementsSingle>g__RandomRange|74_1(-1f, 1f) * CS$<>8__locals1.element.intensityVariation + 1f;
					Vector2 rayOff5 = LensFlareCommonSRP.GetLensFlareRayOffset(CS$<>8__locals1.screenPos, CS$<>8__locals1.position, CS$<>8__locals1.globalCos0, CS$<>8__locals1.globalSin0, CS$<>8__locals1.vScreenRatio);
					Vector2 localSize3 = size;
					if (CS$<>8__locals1.element.enableRadialDistortion)
					{
						Vector2 rayOff6 = LensFlareCommonSRP.GetLensFlareRayOffset(CS$<>8__locals1.screenPos, 0f, CS$<>8__locals1.globalCos0, CS$<>8__locals1.globalSin0, CS$<>8__locals1.vScreenRatio);
						localSize3 = LensFlareCommonSRP.<ProcessLensFlareSRPElementsSingle>g__ComputeLocalSize|74_0(rayOff5, rayOff6, localSize3, CS$<>8__locals1.element.distortionCurve, ref CS$<>8__locals1);
					}
					localSize3 += localSize3 * (CS$<>8__locals1.element.scaleVariation * LensFlareCommonSRP.<ProcessLensFlareSRPElementsSingle>g__RandomRange|74_1(-1f, 1f));
					Color randCol = CS$<>8__locals1.element.colorGradient.Evaluate(LensFlareCommonSRP.<ProcessLensFlareSRPElementsSingle>g__RandomRange|74_1(0f, 1f));
					Vector2 localPositionOffset = CS$<>8__locals1.element.positionOffset + LensFlareCommonSRP.<ProcessLensFlareSRPElementsSingle>g__RandomRange|74_1(-1f, 1f) * side;
					float localRotation = rotation + LensFlareCommonSRP.<ProcessLensFlareSRPElementsSingle>g__RandomRange|74_1(-3.1415927f, 3.1415927f) * CS$<>8__locals1.element.rotationVariation;
					if (localIntensity > 0f)
					{
						Vector4 flareData4 = LensFlareCommonSRP.GetFlareData0(CS$<>8__locals1.screenPos, CS$<>8__locals1.element.translationScale, rayOff5, CS$<>8__locals1.vScreenRatio, localRotation, CS$<>8__locals1.position, angularOffset, localPositionOffset, CS$<>8__locals1.element.autoRotate);
						cmd.SetGlobalVector(LensFlareCommonSRP._FlareData0, flareData4);
						flareData1.y = (float)elemIdx2;
						cmd.SetGlobalVector(LensFlareCommonSRP._FlareData1, flareData1);
						cmd.SetGlobalVector(LensFlareCommonSRP._FlareData2, new Vector4(CS$<>8__locals1.screenPos.x, CS$<>8__locals1.screenPos.y, localSize3.x, localSize3.y));
						cmd.SetGlobalVector(LensFlareCommonSRP._FlareColorValue, curColor * randCol * localIntensity);
						Blitter.DrawQuad(cmd, lensFlareShader, materialPass);
					}
					CS$<>8__locals1.position += dLength;
					CS$<>8__locals1.position += 0.5f * dLength * LensFlareCommonSRP.<ProcessLensFlareSRPElementsSingle>g__RandomRange|74_1(-1f, 1f) * CS$<>8__locals1.element.positionVariation.x;
				}
				Random.state = backupRandState;
				return;
			}
			if (CS$<>8__locals1.element.distribution == SRPLensFlareDistribution.Curve)
			{
				for (int elemIdx3 = 0; elemIdx3 < CS$<>8__locals1.element.count; elemIdx3++)
				{
					float timeScale2 = ((CS$<>8__locals1.element.count >= 2) ? ((float)elemIdx3 / (float)(CS$<>8__locals1.element.count - 1)) : 0.5f);
					Color col2 = CS$<>8__locals1.element.colorGradient.Evaluate(timeScale2);
					float positionSpacing = ((CS$<>8__locals1.element.positionCurve.length > 0) ? CS$<>8__locals1.element.positionCurve.Evaluate(timeScale2) : 1f);
					float localPos = CS$<>8__locals1.position + 2f * CS$<>8__locals1.element.lengthSpread * positionSpacing;
					Vector2 rayOff7 = LensFlareCommonSRP.GetLensFlareRayOffset(CS$<>8__locals1.screenPos, localPos, CS$<>8__locals1.globalCos0, CS$<>8__locals1.globalSin0, CS$<>8__locals1.vScreenRatio);
					Vector2 localSize4 = size;
					if (CS$<>8__locals1.element.enableRadialDistortion)
					{
						Vector2 rayOff8 = LensFlareCommonSRP.GetLensFlareRayOffset(CS$<>8__locals1.screenPos, 0f, CS$<>8__locals1.globalCos0, CS$<>8__locals1.globalSin0, CS$<>8__locals1.vScreenRatio);
						localSize4 = LensFlareCommonSRP.<ProcessLensFlareSRPElementsSingle>g__ComputeLocalSize|74_0(rayOff7, rayOff8, localSize4, CS$<>8__locals1.element.distortionCurve, ref CS$<>8__locals1);
					}
					float sizeCurveValue = ((CS$<>8__locals1.element.scaleCurve.length > 0) ? CS$<>8__locals1.element.scaleCurve.Evaluate(timeScale2) : 1f);
					localSize4 *= sizeCurveValue;
					float angleFromCurve = CS$<>8__locals1.element.uniformAngleCurve.Evaluate(timeScale2) * (180f - 180f / (float)CS$<>8__locals1.element.count);
					Vector4 flareData5 = LensFlareCommonSRP.GetFlareData0(CS$<>8__locals1.screenPos, CS$<>8__locals1.element.translationScale, rayOff7, CS$<>8__locals1.vScreenRatio, rotation + angleFromCurve, localPos, angularOffset, CS$<>8__locals1.element.positionOffset, CS$<>8__locals1.element.autoRotate);
					cmd.SetGlobalVector(LensFlareCommonSRP._FlareData0, flareData5);
					flareData1.y = (float)elemIdx3;
					cmd.SetGlobalVector(LensFlareCommonSRP._FlareData1, flareData1);
					cmd.SetGlobalVector(LensFlareCommonSRP._FlareData2, new Vector4(CS$<>8__locals1.screenPos.x, CS$<>8__locals1.screenPos.y, localSize4.x, localSize4.y));
					cmd.SetGlobalVector(LensFlareCommonSRP._FlareColorValue, curColor * col2);
					Blitter.DrawQuad(cmd, lensFlareShader, materialPass);
				}
			}
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0002320C File Offset: 0x0002140C
		private static void ProcessLensFlareSRPElements(ref LensFlareDataElementSRP[] elements, CommandBuffer cmd, Color globalColorModulation, Light light, float compIntensity, float scale, Material lensFlareShader, Vector2 screenPos, bool compAllowOffScreen, Vector2 vScreenRatio, Vector4 flareData1, bool preview, int depth)
		{
			if (depth > 16)
			{
				Debug.LogWarning("LensFlareSRPAsset contains too deep recursive asset (> 16). Be careful to not have recursive aggregation, A contains B, B contains A, ... which will produce an infinite loop.");
				return;
			}
			LensFlareDataElementSRP[] array = elements;
			for (int i = 0; i < array.Length; i++)
			{
				LensFlareCommonSRP.ProcessLensFlareSRPElementsSingle(array[i], cmd, globalColorModulation, light, compIntensity, scale, lensFlareShader, screenPos, compAllowOffScreen, vScreenRatio, flareData1, preview, depth);
			}
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x00023258 File Offset: 0x00021458
		[Obsolete("Use DoLensFlareDataDrivenCommon without _FlareOcclusionRemapTex.._FlareData4 parameters.")]
		public static void DoLensFlareDataDrivenCommon(Material lensFlareShader, Camera cam, Rect viewport, XRPass xr, int xrIndex, float actualWidth, float actualHeight, bool usePanini, float paniniDistance, float paniniCropToFit, bool isCameraRelative, Vector3 cameraPositionWS, Matrix4x4 viewProjMatrix, UnsafeCommandBuffer cmd, bool taaEnabled, bool hasCloudLayer, Texture cloudOpacityTexture, Texture sunOcclusionTexture, RenderTargetIdentifier colorBuffer, Func<Light, Camera, Vector3, float> GetLensFlareLightAttenuation, int _FlareOcclusionRemapTex, int _FlareOcclusionTex, int _FlareOcclusionIndex, int _FlareCloudOpacity, int _FlareSunOcclusionTex, int _FlareTex, int _FlareColorValue, int _FlareData0, int _FlareData1, int _FlareData2, int _FlareData3, int _FlareData4, bool debugView)
		{
			LensFlareCommonSRP.DoLensFlareDataDrivenCommon(lensFlareShader, cam, viewport, xr, xrIndex, actualWidth, actualHeight, usePanini, paniniDistance, paniniCropToFit, isCameraRelative, cameraPositionWS, viewProjMatrix, cmd, taaEnabled, hasCloudLayer, cloudOpacityTexture, sunOcclusionTexture, colorBuffer, GetLensFlareLightAttenuation, debugView);
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x00023290 File Offset: 0x00021490
		public static void DoLensFlareDataDrivenCommon(Material lensFlareShader, Camera cam, Rect viewport, XRPass xr, int xrIndex, float actualWidth, float actualHeight, bool usePanini, float paniniDistance, float paniniCropToFit, bool isCameraRelative, Vector3 cameraPositionWS, Matrix4x4 viewProjMatrix, UnsafeCommandBuffer cmd, bool taaEnabled, bool hasCloudLayer, Texture cloudOpacityTexture, Texture sunOcclusionTexture, RenderTargetIdentifier colorBuffer, Func<Light, Camera, Vector3, float> GetLensFlareLightAttenuation, bool debugView)
		{
			LensFlareCommonSRP.DoLensFlareDataDrivenCommon(lensFlareShader, cam, viewport, xr, xrIndex, actualWidth, actualHeight, usePanini, paniniDistance, paniniCropToFit, isCameraRelative, cameraPositionWS, viewProjMatrix, cmd.m_WrappedCommandBuffer, taaEnabled, hasCloudLayer, cloudOpacityTexture, sunOcclusionTexture, colorBuffer, GetLensFlareLightAttenuation, debugView);
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x000232D0 File Offset: 0x000214D0
		[Obsolete("Use DoLensFlareDataDrivenCommon without _FlareOcclusionRemapTex.._FlareData4 parameters.")]
		public static void DoLensFlareDataDrivenCommon(Material lensFlareShader, Camera cam, Rect viewport, XRPass xr, int xrIndex, float actualWidth, float actualHeight, bool usePanini, float paniniDistance, float paniniCropToFit, bool isCameraRelative, Vector3 cameraPositionWS, Matrix4x4 viewProjMatrix, CommandBuffer cmd, bool taaEnabled, bool hasCloudLayer, Texture cloudOpacityTexture, Texture sunOcclusionTexture, RenderTargetIdentifier colorBuffer, Func<Light, Camera, Vector3, float> GetLensFlareLightAttenuation, int _FlareOcclusionRemapTex, int _FlareOcclusionTex, int _FlareOcclusionIndex, int _FlareCloudOpacity, int _FlareSunOcclusionTex, int _FlareTex, int _FlareColorValue, int _FlareData0, int _FlareData1, int _FlareData2, int _FlareData3, int _FlareData4, bool debugView)
		{
			LensFlareCommonSRP.DoLensFlareDataDrivenCommon(lensFlareShader, cam, viewport, xr, xrIndex, actualWidth, actualHeight, usePanini, paniniDistance, paniniCropToFit, isCameraRelative, cameraPositionWS, viewProjMatrix, cmd, taaEnabled, hasCloudLayer, cloudOpacityTexture, sunOcclusionTexture, colorBuffer, GetLensFlareLightAttenuation, debugView);
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x00023308 File Offset: 0x00021508
		public static void DoLensFlareDataDrivenCommon(Material lensFlareShader, Camera cam, Rect viewport, XRPass xr, int xrIndex, float actualWidth, float actualHeight, bool usePanini, float paniniDistance, float paniniCropToFit, bool isCameraRelative, Vector3 cameraPositionWS, Matrix4x4 viewProjMatrix, CommandBuffer cmd, bool taaEnabled, bool hasCloudLayer, Texture cloudOpacityTexture, Texture sunOcclusionTexture, RenderTargetIdentifier colorBuffer, Func<Light, Camera, Vector3, float> GetLensFlareLightAttenuation, bool debugView)
		{
			xr.StopSinglePass(cmd);
			if (LensFlareCommonSRP.Instance.IsEmpty())
			{
				return;
			}
			Vector2 screenSize = new Vector2(actualWidth, actualHeight);
			float screenRatio = screenSize.x / screenSize.y;
			Vector2 vScreenRatio = new Vector2(screenRatio, 1f);
			if (xr.enabled && xr.singlePassEnabled)
			{
				CoreUtils.SetRenderTarget(cmd, colorBuffer, ClearFlag.None, 0, CubemapFace.Unknown, xrIndex);
				cmd.SetGlobalInt(LensFlareCommonSRP._ViewId, xrIndex);
			}
			else
			{
				CoreUtils.SetRenderTarget(cmd, colorBuffer, ClearFlag.None, 0, CubemapFace.Unknown, -1);
				if (xr.enabled)
				{
					cmd.SetGlobalInt(LensFlareCommonSRP._ViewId, xr.multipassId);
				}
				else
				{
					cmd.SetGlobalInt(LensFlareCommonSRP._ViewId, 0);
				}
			}
			cmd.SetViewport(viewport);
			if (debugView)
			{
				cmd.ClearRenderTarget(false, true, Color.black);
			}
			foreach (LensFlareCommonSRP.LensFlareCompInfo info in LensFlareCommonSRP.m_Data)
			{
				if (info != null && !(info.comp == null))
				{
					LensFlareComponentSRP comp = info.comp;
					LensFlareDataSRP data = comp.lensFlareData;
					if (!LensFlareCommonSRP.IsLensFlareSRPHidden(cam, comp, data))
					{
						Light light = null;
						if (!comp.TryGetComponent<Light>(out light))
						{
							light = null;
						}
						bool isDirLight = false;
						Vector3 positionWS;
						if (light != null && light.type == LightType.Directional)
						{
							positionWS = -light.transform.forward * cam.farClipPlane;
							isDirLight = true;
						}
						else
						{
							positionWS = comp.transform.position;
						}
						if (comp.lightOverride != null)
						{
							light = comp.lightOverride;
						}
						Vector3 viewportPos = LensFlareCommonSRP.WorldToViewport(cam, !isDirLight, isCameraRelative, viewProjMatrix, positionWS);
						if (usePanini && cam == Camera.main)
						{
							viewportPos = LensFlareCommonSRP.DoPaniniProjection(viewportPos, actualWidth, actualHeight, cam.fieldOfView, paniniCropToFit, paniniDistance);
						}
						if (viewportPos.z >= 0f && (comp.allowOffScreen || (viewportPos.x >= 0f && viewportPos.x <= 1f && viewportPos.y >= 0f && viewportPos.y <= 1f)))
						{
							Vector3 diffToObject = positionWS - cameraPositionWS;
							if (Vector3.Dot(cam.transform.forward, diffToObject) >= 0f)
							{
								float magnitude = diffToObject.magnitude;
								float coefDistSample = magnitude / comp.maxAttenuationDistance;
								float coefScaleSample = magnitude / comp.maxAttenuationScale;
								float distanceAttenuation = ((!isDirLight && comp.distanceAttenuationCurve.length > 0) ? comp.distanceAttenuationCurve.Evaluate(coefDistSample) : 1f);
								float scaleByDistance = ((!isDirLight && comp.scaleByDistanceCurve.length >= 1) ? comp.scaleByDistanceCurve.Evaluate(coefScaleSample) : 1f);
								Color globalColorModulation = Color.white;
								if (light != null && comp.attenuationByLightShape)
								{
									globalColorModulation *= GetLensFlareLightAttenuation(light, cam, -diffToObject.normalized);
								}
								Vector2 screenPos = new Vector2(2f * viewportPos.x - 1f, -(2f * viewportPos.y - 1f));
								if (!SystemInfo.graphicsUVStartsAtTop && isDirLight)
								{
									screenPos.y = -screenPos.y;
								}
								Vector2 radPos = new Vector2(Mathf.Abs(screenPos.x), Mathf.Abs(screenPos.y));
								float radius = Mathf.Max(radPos.x, radPos.y);
								float radialsScaleRadius = ((comp.radialScreenAttenuationCurve.length > 0) ? comp.radialScreenAttenuationCurve.Evaluate(radius) : 1f);
								float compIntensity = comp.intensity * radialsScaleRadius * distanceAttenuation;
								if (compIntensity > 0f)
								{
									globalColorModulation *= distanceAttenuation;
									Vector3 dir = (cam.transform.position - comp.transform.position).normalized;
									Vector3 screenPosZ = LensFlareCommonSRP.WorldToViewport(cam, !isDirLight, isCameraRelative, viewProjMatrix, positionWS + dir * comp.occlusionOffset);
									float adjustedOcclusionRadius = (isDirLight ? comp.celestialProjectedOcclusionRadius(cam) : comp.occlusionRadius);
									Vector2 occlusionRadiusEdgeScreenPos0 = viewportPos;
									float magnitude2 = (LensFlareCommonSRP.WorldToViewport(cam, !isDirLight, isCameraRelative, viewProjMatrix, positionWS + cam.transform.up * adjustedOcclusionRadius) - occlusionRadiusEdgeScreenPos0).magnitude;
									if (comp.useOcclusion)
									{
										cmd.SetGlobalTexture(LensFlareCommonSRP._FlareOcclusionTex, LensFlareCommonSRP.occlusionRT);
										cmd.EnableShaderKeyword("FLARE_HAS_OCCLUSION");
									}
									else
									{
										cmd.DisableShaderKeyword("FLARE_HAS_OCCLUSION");
									}
									if (LensFlareCommonSRP.IsOcclusionRTCompatible())
									{
										cmd.DisableShaderKeyword("FLARE_OPENGL3_OR_OPENGLCORE");
									}
									else
									{
										cmd.EnableShaderKeyword("FLARE_OPENGL3_OR_OPENGLCORE");
									}
									cmd.SetGlobalVector(LensFlareCommonSRP._FlareOcclusionIndex, new Vector4((float)info.index, 0f, 0f, 0f));
									cmd.SetGlobalTexture(LensFlareCommonSRP._FlareOcclusionRemapTex, comp.occlusionRemapCurve.GetTexture());
									Vector4 flareData = new Vector4(0f, comp.sampleCount, screenPosZ.z, actualHeight / actualWidth);
									LensFlareCommonSRP.ProcessLensFlareSRPElements(ref data.elements, cmd, globalColorModulation, light, compIntensity, scaleByDistance * comp.scale, lensFlareShader, screenPos, comp.allowOffScreen, vScreenRatio, flareData, false, 0);
								}
							}
						}
					}
				}
			}
			xr.StartSinglePass(cmd);
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x000238A8 File Offset: 0x00021AA8
		public static void DoLensFlareScreenSpaceCommon(Material lensFlareShader, Camera cam, float actualWidth, float actualHeight, Color tintColor, Texture originalBloomTexture, Texture bloomMipTexture, Texture spectralLut, Texture streakTextureTmp, Texture streakTextureTmp2, Vector4 parameters1, Vector4 parameters2, Vector4 parameters3, Vector4 parameters4, Vector4 parameters5, UnsafeCommandBuffer cmd, RTHandle result, bool debugView)
		{
			LensFlareCommonSRP.DoLensFlareScreenSpaceCommon(lensFlareShader, cam, actualWidth, actualHeight, tintColor, originalBloomTexture, bloomMipTexture, spectralLut, streakTextureTmp, streakTextureTmp2, parameters1, parameters2, parameters3, parameters4, parameters5, cmd.m_WrappedCommandBuffer, result, debugView);
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x000238E0 File Offset: 0x00021AE0
		[Obsolete("Use DoLensFlareScreenSpaceCommon without _Shader IDs parameters.")]
		public static void DoLensFlareScreenSpaceCommon(Material lensFlareShader, Camera cam, float actualWidth, float actualHeight, Color tintColor, Texture originalBloomTexture, Texture bloomMipTexture, Texture spectralLut, Texture streakTextureTmp, Texture streakTextureTmp2, Vector4 parameters1, Vector4 parameters2, Vector4 parameters3, Vector4 parameters4, Vector4 parameters5, CommandBuffer cmd, RTHandle result, int _LensFlareScreenSpaceBloomMipTexture, int _LensFlareScreenSpaceResultTexture, int _LensFlareScreenSpaceSpectralLut, int _LensFlareScreenSpaceStreakTex, int _LensFlareScreenSpaceMipLevel, int _LensFlareScreenSpaceTintColor, int _LensFlareScreenSpaceParams1, int _LensFlareScreenSpaceParams2, int _LensFlareScreenSpaceParams3, int _LensFlareScreenSpaceParams4, int _LensFlareScreenSpaceParams5, bool debugView)
		{
			LensFlareCommonSRP.DoLensFlareScreenSpaceCommon(lensFlareShader, cam, actualWidth, actualHeight, tintColor, originalBloomTexture, bloomMipTexture, spectralLut, streakTextureTmp, streakTextureTmp2, parameters1, parameters2, parameters3, parameters4, parameters5, cmd, result, debugView);
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x00023914 File Offset: 0x00021B14
		public static void DoLensFlareScreenSpaceCommon(Material lensFlareShader, Camera cam, float actualWidth, float actualHeight, Color tintColor, Texture originalBloomTexture, Texture bloomMipTexture, Texture spectralLut, Texture streakTextureTmp, Texture streakTextureTmp2, Vector4 parameters1, Vector4 parameters2, Vector4 parameters3, Vector4 parameters4, Vector4 parameters5, CommandBuffer cmd, RTHandle result, bool debugView)
		{
			parameters2.x = Mathf.Pow(parameters2.x, 0.25f);
			parameters3.z /= 20f;
			parameters4.y *= 10f;
			parameters4.z /= 90f;
			parameters5.y = 1f / parameters5.y;
			parameters5.z = 1f / parameters5.z;
			cmd.SetViewport(new Rect
			{
				width = actualWidth,
				height = actualHeight
			});
			if (debugView)
			{
				cmd.ClearRenderTarget(false, true, Color.black);
			}
			float warpedScaleX = parameters5.y;
			warpedScaleX *= actualWidth / actualHeight;
			parameters5.y = warpedScaleX;
			float streaksLength = parameters4.y;
			streaksLength *= actualWidth * 0.0005f;
			parameters4.y = streaksLength;
			int prefilterPass = lensFlareShader.FindPass("LensFlareScreenSpac Prefilter");
			int downSamplePass = lensFlareShader.FindPass("LensFlareScreenSpace Downsample");
			int upSamplePass = lensFlareShader.FindPass("LensFlareScreenSpace Upsample");
			int compositionPass = lensFlareShader.FindPass("LensFlareScreenSpace Composition");
			int writeToBloomPass = lensFlareShader.FindPass("LensFlareScreenSpace Write to BloomTexture");
			cmd.SetGlobalTexture(LensFlareCommonSRP._LensFlareScreenSpaceBloomMipTexture, bloomMipTexture);
			cmd.SetGlobalTexture(LensFlareCommonSRP._LensFlareScreenSpaceSpectralLut, spectralLut);
			cmd.SetGlobalVector(LensFlareCommonSRP._LensFlareScreenSpaceParams1, parameters1);
			cmd.SetGlobalVector(LensFlareCommonSRP._LensFlareScreenSpaceParams2, parameters2);
			cmd.SetGlobalVector(LensFlareCommonSRP._LensFlareScreenSpaceParams3, parameters3);
			cmd.SetGlobalVector(LensFlareCommonSRP._LensFlareScreenSpaceParams4, parameters4);
			cmd.SetGlobalVector(LensFlareCommonSRP._LensFlareScreenSpaceParams5, parameters5);
			cmd.SetGlobalColor(LensFlareCommonSRP._LensFlareScreenSpaceTintColor, tintColor);
			if (parameters4.x > 0f)
			{
				CoreUtils.SetRenderTarget(cmd, streakTextureTmp, ClearFlag.None, 0, CubemapFace.Unknown, -1);
				Blitter.DrawQuad(cmd, lensFlareShader, prefilterPass);
				int maxLevel = Mathf.FloorToInt(Mathf.Log(Mathf.Max(actualHeight, actualWidth), 2f));
				int maxLevelDownsample = Mathf.Max(1, maxLevel);
				int maxLevelUpsample = 2;
				int startIndex = 0;
				bool even = false;
				for (int i = 0; i < maxLevelDownsample; i++)
				{
					even = i % 2 == 0;
					cmd.SetGlobalInt(LensFlareCommonSRP._LensFlareScreenSpaceMipLevel, i);
					cmd.SetGlobalTexture(LensFlareCommonSRP._LensFlareScreenSpaceStreakTex, even ? streakTextureTmp : streakTextureTmp2);
					CoreUtils.SetRenderTarget(cmd, even ? streakTextureTmp2 : streakTextureTmp, ClearFlag.None, 0, CubemapFace.Unknown, -1);
					Blitter.DrawQuad(cmd, lensFlareShader, downSamplePass);
				}
				if (even)
				{
					startIndex = 1;
				}
				for (int j = startIndex; j < startIndex + maxLevelUpsample; j++)
				{
					even = j % 2 == 0;
					cmd.SetGlobalInt(LensFlareCommonSRP._LensFlareScreenSpaceMipLevel, j - startIndex);
					cmd.SetGlobalTexture(LensFlareCommonSRP._LensFlareScreenSpaceStreakTex, even ? streakTextureTmp : streakTextureTmp2);
					CoreUtils.SetRenderTarget(cmd, even ? streakTextureTmp2 : streakTextureTmp, ClearFlag.None, 0, CubemapFace.Unknown, -1);
					Blitter.DrawQuad(cmd, lensFlareShader, upSamplePass);
				}
				cmd.SetGlobalTexture(LensFlareCommonSRP._LensFlareScreenSpaceStreakTex, even ? streakTextureTmp2 : streakTextureTmp);
			}
			CoreUtils.SetRenderTarget(cmd, result, ClearFlag.None, 0, CubemapFace.Unknown, -1);
			Blitter.DrawQuad(cmd, lensFlareShader, compositionPass);
			cmd.SetGlobalTexture(LensFlareCommonSRP._LensFlareScreenSpaceResultTexture, result);
			CoreUtils.SetRenderTarget(cmd, originalBloomTexture, ClearFlag.None, 0, CubemapFace.Unknown, -1);
			Blitter.DrawQuad(cmd, lensFlareShader, writeToBloomPass);
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x00023C4C File Offset: 0x00021E4C
		private static Vector2 DoPaniniProjection(Vector2 screenPos, float actualWidth, float actualHeight, float fieldOfView, float paniniProjectionCropToFit, float paniniProjectionDistance)
		{
			Vector2 viewExtents = LensFlareCommonSRP.CalcViewExtents(actualWidth, actualHeight, fieldOfView);
			Vector2 vector = LensFlareCommonSRP.CalcCropExtents(actualWidth, actualHeight, fieldOfView, paniniProjectionDistance);
			float scaleX = vector.x / viewExtents.x;
			float scaleY = vector.y / viewExtents.y;
			float scaleF = Mathf.Min(scaleX, scaleY);
			float paniniS = Mathf.Lerp(1f, Mathf.Clamp01(scaleF), paniniProjectionCropToFit);
			Vector2 projPos = LensFlareCommonSRP.Panini_Generic_Inv(new Vector2(2f * screenPos.x - 1f, 2f * screenPos.y - 1f) * viewExtents, paniniProjectionDistance) / (viewExtents * paniniS);
			return new Vector2(0.5f * projPos.x + 0.5f, 0.5f * projPos.y + 0.5f);
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x00023D18 File Offset: 0x00021F18
		private static Vector2 CalcViewExtents(float actualWidth, float actualHeight, float fieldOfView)
		{
			float fovY = fieldOfView * 0.017453292f;
			float num = actualWidth / actualHeight;
			float viewExtY = Mathf.Tan(0.5f * fovY);
			return new Vector2(num * viewExtY, viewExtY);
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x00023D48 File Offset: 0x00021F48
		private static Vector2 CalcCropExtents(float actualWidth, float actualHeight, float fieldOfView, float d)
		{
			float viewDist = 1f + d;
			Vector2 projPos = LensFlareCommonSRP.CalcViewExtents(actualWidth, actualHeight, fieldOfView);
			float projHyp = Mathf.Sqrt(projPos.x * projPos.x + 1f);
			float cylDistMinusD = 1f / projHyp;
			float cylDist = cylDistMinusD + d;
			return projPos * cylDistMinusD * (viewDist / cylDist);
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x00023D9C File Offset: 0x00021F9C
		private static Vector2 Panini_Generic_Inv(Vector2 projPos, float d)
		{
			float viewDist = 1f + d;
			float projHyp = Mathf.Sqrt(projPos.x * projPos.x + 1f);
			float cylDistMinusD = 1f / projHyp;
			float cylDist = cylDistMinusD + d;
			return projPos * cylDistMinusD * (viewDist / cylDist);
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x00023FEC File Offset: 0x000221EC
		[CompilerGenerated]
		internal static float <ShapeAttenuationAreaTubeLight>g__Fpo|57_0(float d, float l)
		{
			return l / (d * (d * d + l * l)) + Mathf.Atan(l / d) / (d * d);
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x00024006 File Offset: 0x00022206
		[CompilerGenerated]
		internal static float <ShapeAttenuationAreaTubeLight>g__Fwt|57_1(float d, float l)
		{
			return l * l / (d * (d * d + l * l));
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x00024018 File Offset: 0x00022218
		[CompilerGenerated]
		internal static float <ShapeAttenuationAreaTubeLight>g__DiffLineIntegral|57_2(Vector3 p1, Vector3 p2)
		{
			Vector3 wt = (p2 - p1).normalized;
			float diffIntegral;
			if ((double)p1.z <= 0.0 && (double)p2.z <= 0.0)
			{
				diffIntegral = 0f;
			}
			else
			{
				if ((double)p1.z < 0.0)
				{
					p1 = (p1 * p2.z - p2 * p1.z) / (p2.z - p1.z);
				}
				if ((double)p2.z < 0.0)
				{
					p2 = (-p1 * p2.z + p2 * p1.z) / (-p2.z + p1.z);
				}
				float l = Vector3.Dot(p1, wt);
				float l2 = Vector3.Dot(p2, wt);
				Vector3 po = p1 - l * wt;
				float d = po.magnitude;
				diffIntegral = ((LensFlareCommonSRP.<ShapeAttenuationAreaTubeLight>g__Fpo|57_0(d, l2) - LensFlareCommonSRP.<ShapeAttenuationAreaTubeLight>g__Fpo|57_0(d, l)) * po.z + (LensFlareCommonSRP.<ShapeAttenuationAreaTubeLight>g__Fwt|57_1(d, l2) - LensFlareCommonSRP.<ShapeAttenuationAreaTubeLight>g__Fwt|57_1(d, l)) * wt.z) / 3.1415927f;
			}
			return diffIntegral;
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00024158 File Offset: 0x00022358
		[CompilerGenerated]
		internal static Vector2 <ProcessLensFlareSRPElementsSingle>g__ComputeLocalSize|74_0(Vector2 rayOff, Vector2 rayOff0, Vector2 curSize, AnimationCurve distortionCurve, ref LensFlareCommonSRP.<>c__DisplayClass74_0 A_4)
		{
			LensFlareCommonSRP.GetLensFlareRayOffset(A_4.screenPos, A_4.position, A_4.globalCos0, A_4.globalSin0, A_4.vScreenRatio);
			float localRadius;
			if (!A_4.element.distortionRelativeToCenter)
			{
				Vector2 localRadPos = (rayOff - rayOff0) * 0.5f;
				localRadius = Mathf.Clamp01(Mathf.Max(Mathf.Abs(localRadPos.x), Mathf.Abs(localRadPos.y)));
			}
			else
			{
				localRadius = Mathf.Clamp01((A_4.screenPos + (rayOff + new Vector2(A_4.element.positionOffset.x, -A_4.element.positionOffset.y)) * A_4.element.translationScale).magnitude);
			}
			float localLerpValue = Mathf.Clamp01(distortionCurve.Evaluate(localRadius));
			return new Vector2(Mathf.Lerp(curSize.x, A_4.element.targetSizeDistortion.x * A_4.combinedScale / A_4.usedAspectRatio, localLerpValue), Mathf.Lerp(curSize.y, A_4.element.targetSizeDistortion.y * A_4.combinedScale, localLerpValue));
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x0002428E File Offset: 0x0002248E
		[CompilerGenerated]
		internal static float <ProcessLensFlareSRPElementsSingle>g__RandomRange|74_1(float min, float max)
		{
			return Random.Range(min, max);
		}

		// Token: 0x04000628 RID: 1576
		private static LensFlareCommonSRP m_Instance = null;

		// Token: 0x04000629 RID: 1577
		private static readonly object m_Padlock = new object();

		// Token: 0x0400062A RID: 1578
		private static List<LensFlareCommonSRP.LensFlareCompInfo> m_Data = new List<LensFlareCommonSRP.LensFlareCompInfo>();

		// Token: 0x0400062B RID: 1579
		private static List<int> m_AvailableIndicies = new List<int>();

		// Token: 0x0400062C RID: 1580
		public static int maxLensFlareWithOcclusion = 128;

		// Token: 0x0400062D RID: 1581
		public static int maxLensFlareWithOcclusionTemporalSample = 8;

		// Token: 0x0400062E RID: 1582
		public static int mergeNeeded = 1;

		// Token: 0x0400062F RID: 1583
		public static RTHandle occlusionRT = null;

		// Token: 0x04000630 RID: 1584
		private static int frameIdx = 0;

		// Token: 0x04000631 RID: 1585
		internal static readonly int _FlareOcclusionPermutation = Shader.PropertyToID("_FlareOcclusionPermutation");

		// Token: 0x04000632 RID: 1586
		internal static readonly int _FlareOcclusionRemapTex = Shader.PropertyToID("_FlareOcclusionRemapTex");

		// Token: 0x04000633 RID: 1587
		internal static readonly int _FlareOcclusionTex = Shader.PropertyToID("_FlareOcclusionTex");

		// Token: 0x04000634 RID: 1588
		internal static readonly int _FlareOcclusionIndex = Shader.PropertyToID("_FlareOcclusionIndex");

		// Token: 0x04000635 RID: 1589
		internal static readonly int _FlareCloudOpacity = Shader.PropertyToID("_FlareCloudOpacity");

		// Token: 0x04000636 RID: 1590
		internal static readonly int _FlareSunOcclusionTex = Shader.PropertyToID("_FlareSunOcclusionTex");

		// Token: 0x04000637 RID: 1591
		internal static readonly int _FlareTex = Shader.PropertyToID("_FlareTex");

		// Token: 0x04000638 RID: 1592
		internal static readonly int _FlareColorValue = Shader.PropertyToID("_FlareColorValue");

		// Token: 0x04000639 RID: 1593
		internal static readonly int _FlareData0 = Shader.PropertyToID("_FlareData0");

		// Token: 0x0400063A RID: 1594
		internal static readonly int _FlareData1 = Shader.PropertyToID("_FlareData1");

		// Token: 0x0400063B RID: 1595
		internal static readonly int _FlareData2 = Shader.PropertyToID("_FlareData2");

		// Token: 0x0400063C RID: 1596
		internal static readonly int _FlareData3 = Shader.PropertyToID("_FlareData3");

		// Token: 0x0400063D RID: 1597
		internal static readonly int _FlareData4 = Shader.PropertyToID("_FlareData4");

		// Token: 0x0400063E RID: 1598
		internal static readonly int _FlareData5 = Shader.PropertyToID("_FlareData5");

		// Token: 0x0400063F RID: 1599
		internal static readonly int _FlareRadialTint = Shader.PropertyToID("_FlareRadialTint");

		// Token: 0x04000640 RID: 1600
		internal static readonly int _ViewId = Shader.PropertyToID("_ViewId");

		// Token: 0x04000641 RID: 1601
		internal static readonly int _LensFlareScreenSpaceBloomMipTexture = Shader.PropertyToID("_LensFlareScreenSpaceBloomMipTexture");

		// Token: 0x04000642 RID: 1602
		internal static readonly int _LensFlareScreenSpaceResultTexture = Shader.PropertyToID("_LensFlareScreenSpaceResultTexture");

		// Token: 0x04000643 RID: 1603
		internal static readonly int _LensFlareScreenSpaceSpectralLut = Shader.PropertyToID("_LensFlareScreenSpaceSpectralLut");

		// Token: 0x04000644 RID: 1604
		internal static readonly int _LensFlareScreenSpaceStreakTex = Shader.PropertyToID("_LensFlareScreenSpaceStreakTex");

		// Token: 0x04000645 RID: 1605
		internal static readonly int _LensFlareScreenSpaceMipLevel = Shader.PropertyToID("_LensFlareScreenSpaceMipLevel");

		// Token: 0x04000646 RID: 1606
		internal static readonly int _LensFlareScreenSpaceTintColor = Shader.PropertyToID("_LensFlareScreenSpaceTintColor");

		// Token: 0x04000647 RID: 1607
		internal static readonly int _LensFlareScreenSpaceParams1 = Shader.PropertyToID("_LensFlareScreenSpaceParams1");

		// Token: 0x04000648 RID: 1608
		internal static readonly int _LensFlareScreenSpaceParams2 = Shader.PropertyToID("_LensFlareScreenSpaceParams2");

		// Token: 0x04000649 RID: 1609
		internal static readonly int _LensFlareScreenSpaceParams3 = Shader.PropertyToID("_LensFlareScreenSpaceParams3");

		// Token: 0x0400064A RID: 1610
		internal static readonly int _LensFlareScreenSpaceParams4 = Shader.PropertyToID("_LensFlareScreenSpaceParams4");

		// Token: 0x0400064B RID: 1611
		internal static readonly int _LensFlareScreenSpaceParams5 = Shader.PropertyToID("_LensFlareScreenSpaceParams5");

		// Token: 0x0400064C RID: 1612
		private static readonly bool s_SupportsLensFlare16bitsFormat = SystemInfo.IsFormatSupported(GraphicsFormat.R16_SFloat, GraphicsFormatUsage.Render);

		// Token: 0x0400064D RID: 1613
		private static readonly bool s_SupportsLensFlare32bitsFormat = SystemInfo.IsFormatSupported(GraphicsFormat.R32_SFloat, GraphicsFormatUsage.Render);

		// Token: 0x0200014D RID: 333
		internal class LensFlareCompInfo
		{
			// Token: 0x06000A5C RID: 2652 RVA: 0x00024297 File Offset: 0x00022497
			internal LensFlareCompInfo(int idx, LensFlareComponentSRP cmp)
			{
				this.index = idx;
				this.comp = cmp;
			}

			// Token: 0x0400064E RID: 1614
			internal int index;

			// Token: 0x0400064F RID: 1615
			internal LensFlareComponentSRP comp;
		}
	}
}
