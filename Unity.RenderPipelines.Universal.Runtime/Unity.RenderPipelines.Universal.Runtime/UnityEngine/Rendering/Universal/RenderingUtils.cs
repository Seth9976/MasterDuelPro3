using System;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000183 RID: 387
	public static class RenderingUtils
	{
		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x000267A6 File Offset: 0x000249A6
		internal static AttachmentDescriptor emptyAttachment
		{
			get
			{
				return RenderingUtils.s_EmptyAttachment;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000814 RID: 2068 RVA: 0x000267B0 File Offset: 0x000249B0
		[Obsolete("Use Blitter.BlitCameraTexture instead of CommandBuffer.DrawMesh(fullscreenMesh, ...)")]
		public static Mesh fullscreenMesh
		{
			get
			{
				if (RenderingUtils.s_FullscreenMesh != null)
				{
					return RenderingUtils.s_FullscreenMesh;
				}
				float topV = 1f;
				float bottomV = 0f;
				RenderingUtils.s_FullscreenMesh = new Mesh
				{
					name = "Fullscreen Quad"
				};
				RenderingUtils.s_FullscreenMesh.SetVertices(new List<Vector3>
				{
					new Vector3(-1f, -1f, 0f),
					new Vector3(-1f, 1f, 0f),
					new Vector3(1f, -1f, 0f),
					new Vector3(1f, 1f, 0f)
				});
				RenderingUtils.s_FullscreenMesh.SetUVs(0, new List<Vector2>
				{
					new Vector2(0f, bottomV),
					new Vector2(0f, topV),
					new Vector2(1f, bottomV),
					new Vector2(1f, topV)
				});
				RenderingUtils.s_FullscreenMesh.SetIndices(new int[] { 0, 1, 2, 2, 1, 3 }, MeshTopology.Triangles, 0, false);
				RenderingUtils.s_FullscreenMesh.UploadMeshData(true);
				return RenderingUtils.s_FullscreenMesh;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x00002886 File Offset: 0x00000A86
		internal static bool useStructuredBuffer
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x000039B4 File Offset: 0x00001BB4
		internal static bool SupportsLightLayers(GraphicsDeviceType type)
		{
			return true;
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000817 RID: 2071 RVA: 0x000268EC File Offset: 0x00024AEC
		private static Material errorMaterial
		{
			get
			{
				if (RenderingUtils.s_ErrorMaterial == null)
				{
					try
					{
						RenderingUtils.s_ErrorMaterial = new Material(Shader.Find("Hidden/Universal Render Pipeline/FallbackError"));
					}
					catch
					{
					}
				}
				return RenderingUtils.s_ErrorMaterial;
			}
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00026934 File Offset: 0x00024B34
		public static void SetViewAndProjectionMatrices(CommandBuffer cmd, Matrix4x4 viewMatrix, Matrix4x4 projectionMatrix, bool setInverseMatrices)
		{
			RenderingUtils.SetViewAndProjectionMatrices(CommandBufferHelpers.GetRasterCommandBuffer(cmd), viewMatrix, projectionMatrix, setInverseMatrices);
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x00026944 File Offset: 0x00024B44
		internal static void SetViewAndProjectionMatrices(RasterCommandBuffer cmd, Matrix4x4 viewMatrix, Matrix4x4 projectionMatrix, bool setInverseMatrices)
		{
			Matrix4x4 viewAndProjectionMatrix = projectionMatrix * viewMatrix;
			cmd.SetGlobalMatrix(ShaderPropertyId.viewMatrix, viewMatrix);
			cmd.SetGlobalMatrix(ShaderPropertyId.projectionMatrix, projectionMatrix);
			cmd.SetGlobalMatrix(ShaderPropertyId.viewAndProjectionMatrix, viewAndProjectionMatrix);
			if (setInverseMatrices)
			{
				Matrix4x4 inverseViewMatrix = Matrix4x4.Inverse(viewMatrix);
				Matrix4x4 inverseProjectionMatrix = Matrix4x4.Inverse(projectionMatrix);
				Matrix4x4 inverseViewProjection = inverseViewMatrix * inverseProjectionMatrix;
				cmd.SetGlobalMatrix(ShaderPropertyId.inverseViewMatrix, inverseViewMatrix);
				cmd.SetGlobalMatrix(ShaderPropertyId.inverseProjectionMatrix, inverseProjectionMatrix);
				cmd.SetGlobalMatrix(ShaderPropertyId.inverseViewAndProjectionMatrix, inverseViewProjection);
			}
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x000269BC File Offset: 0x00024BBC
		internal static void SetScaleBiasRt(RasterCommandBuffer cmd, in UniversalCameraData cameraData, RTHandle rTHandle)
		{
			float flipSign = ((cameraData.cameraType != CameraType.Game || !(rTHandle.nameID == BuiltinRenderTextureType.CameraTarget) || !(cameraData.camera.targetTexture == null)) ? (-1f) : 1f);
			Vector4 scaleBiasRt = ((flipSign < 0f) ? new Vector4(flipSign, 1f, -1f, 1f) : new Vector4(flipSign, 0f, 1f, 1f));
			cmd.SetGlobalVector(Shader.PropertyToID("_ScaleBiasRt"), scaleBiasRt);
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x00026A54 File Offset: 0x00024C54
		internal unsafe static void SetScaleBiasRt(RasterCommandBuffer cmd, in RenderingData renderingData)
		{
			CameraData cameraData2 = renderingData.cameraData;
			ScriptableRenderer renderer = *cameraData2.renderer;
			CameraData cameraData = renderingData.cameraData;
			float flipSign = ((*cameraData.cameraType != CameraType.Game || !(renderer.cameraColorTargetHandle.nameID == BuiltinRenderTextureType.CameraTarget) || !(cameraData.camera->targetTexture == null)) ? (-1f) : 1f);
			Vector4 scaleBiasRt = ((flipSign < 0f) ? new Vector4(flipSign, 1f, -1f, 1f) : new Vector4(flipSign, 0f, 1f, 1f));
			cmd.SetGlobalVector(Shader.PropertyToID("_ScaleBiasRt"), scaleBiasRt);
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x00026B0C File Offset: 0x00024D0C
		internal static void Blit(CommandBuffer cmd, RTHandle source, Rect viewport, RTHandle destination, RenderBufferLoadAction loadAction, RenderBufferStoreAction storeAction, ClearFlag clearFlag, Color clearColor, Material material, int passIndex = 0)
		{
			Vector2 viewportScale = (source.useScaling ? new Vector2(source.rtHandleProperties.rtHandleScale.x, source.rtHandleProperties.rtHandleScale.y) : Vector2.one);
			CoreUtils.SetRenderTarget(cmd, destination, loadAction, storeAction, ClearFlag.None, Color.clear, 0, CubemapFace.Unknown, -1);
			cmd.SetViewport(viewport);
			Blitter.BlitTexture(cmd, source, viewportScale, material, passIndex);
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x00026B7C File Offset: 0x00024D7C
		internal static void Blit(CommandBuffer cmd, RTHandle source, Rect viewport, RTHandle destinationColor, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RTHandle destinationDepthStencil, RenderBufferLoadAction depthStencilLoadAction, RenderBufferStoreAction depthStencilStoreAction, ClearFlag clearFlag, Color clearColor, Material material, int passIndex = 0)
		{
			Vector2 viewportScale = (source.useScaling ? new Vector2(source.rtHandleProperties.rtHandleScale.x, source.rtHandleProperties.rtHandleScale.y) : Vector2.one);
			CoreUtils.SetRenderTarget(cmd, destinationColor, colorLoadAction, colorStoreAction, destinationDepthStencil, depthStencilLoadAction, depthStencilStoreAction, clearFlag, clearColor, 0, CubemapFace.Unknown, -1);
			cmd.SetViewport(viewport);
			Blitter.BlitTexture(cmd, source, viewportScale, material, passIndex);
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x00026BF0 File Offset: 0x00024DF0
		internal static void FinalBlit(CommandBuffer cmd, UniversalCameraData cameraData, RTHandle source, RTHandle destination, RenderBufferLoadAction loadAction, RenderBufferStoreAction storeAction, Material material, int passIndex)
		{
			bool isRenderToBackBufferTarget = !cameraData.isSceneViewCamera;
			if (cameraData.xr.enabled)
			{
				isRenderToBackBufferTarget = new RenderTargetIdentifier(destination.nameID, 0, CubemapFace.Unknown, -1) == new RenderTargetIdentifier(cameraData.xr.renderTarget, 0, CubemapFace.Unknown, -1);
			}
			Vector2 viewportScale = (source.useScaling ? new Vector2(source.rtHandleProperties.rtHandleScale.x, source.rtHandleProperties.rtHandleScale.y) : Vector2.one);
			Vector4 scaleBias = ((isRenderToBackBufferTarget && cameraData.targetTexture == null && SystemInfo.graphicsUVStartsAtTop) ? new Vector4(viewportScale.x, -viewportScale.y, 0f, viewportScale.y) : new Vector4(viewportScale.x, viewportScale.y, 0f, 0f));
			CoreUtils.SetRenderTarget(cmd, destination, loadAction, storeAction, ClearFlag.None, Color.clear, 0, CubemapFace.Unknown, -1);
			if (isRenderToBackBufferTarget)
			{
				cmd.SetViewport(cameraData.pixelRect);
			}
			if (GL.wireframe && cameraData.isSceneViewCamera)
			{
				cmd.SetRenderTarget(BuiltinRenderTextureType.CameraTarget, loadAction, storeAction, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.DontCare);
				if (SystemInfo.graphicsDeviceType == GraphicsDeviceType.Vulkan)
				{
					cmd.SetWireframe(false);
					cmd.Blit(source, destination);
					cmd.SetWireframe(true);
					return;
				}
				cmd.Blit(source, destination);
				return;
			}
			else
			{
				if (source.rt == null)
				{
					Blitter.BlitTexture(cmd, source.nameID, scaleBias, material, passIndex);
					return;
				}
				Blitter.BlitTexture(cmd, source, scaleBias, material, passIndex);
				return;
			}
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x00026D74 File Offset: 0x00024F74
		[Conditional("DEVELOPMENT_BUILD")]
		[Conditional("UNITY_EDITOR")]
		internal static void CreateRendererParamsObjectsWithError(ref CullingResults cullResults, Camera camera, FilteringSettings filterSettings, SortingCriteria sortFlags, ref RendererListParams param)
		{
			SortingSettings sortingSettings = new SortingSettings(camera)
			{
				criteria = sortFlags
			};
			DrawingSettings errorSettings = new DrawingSettings(RenderingUtils.m_LegacyShaderPassNames[0], sortingSettings)
			{
				perObjectData = PerObjectData.None,
				overrideMaterial = RenderingUtils.errorMaterial,
				overrideMaterialPassIndex = 0
			};
			for (int i = 1; i < RenderingUtils.m_LegacyShaderPassNames.Count; i++)
			{
				errorSettings.SetShaderPassName(i, RenderingUtils.m_LegacyShaderPassNames[i]);
			}
			param = new RendererListParams(cullResults, errorSettings, filterSettings);
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x00026E08 File Offset: 0x00025008
		[Conditional("DEVELOPMENT_BUILD")]
		[Conditional("UNITY_EDITOR")]
		internal static void CreateRendererListObjectsWithError(ScriptableRenderContext context, ref CullingResults cullResults, Camera camera, FilteringSettings filterSettings, SortingCriteria sortFlags, ref RendererList rl)
		{
			if (RenderingUtils.errorMaterial == null)
			{
				rl = RendererList.nullRendererList;
				return;
			}
			RendererListParams param = default(RendererListParams);
			rl = context.CreateRendererList(ref param);
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x00026E48 File Offset: 0x00025048
		[Conditional("DEVELOPMENT_BUILD")]
		[Conditional("UNITY_EDITOR")]
		internal static void CreateRendererListObjectsWithError(RenderGraph renderGraph, ref CullingResults cullResults, Camera camera, FilteringSettings filterSettings, SortingCriteria sortFlags, ref RendererListHandle rl)
		{
			if (RenderingUtils.errorMaterial == null)
			{
				rl = default(RendererListHandle);
				return;
			}
			RendererListParams param = default(RendererListParams);
			rl = renderGraph.CreateRendererList(in param);
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x00026E82 File Offset: 0x00025082
		[Conditional("DEVELOPMENT_BUILD")]
		[Conditional("UNITY_EDITOR")]
		internal static void DrawRendererListObjectsWithError(RasterCommandBuffer cmd, ref RendererList rl)
		{
			cmd.DrawRendererList(rl);
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x00026E90 File Offset: 0x00025090
		internal unsafe static void CreateRendererListWithRenderStateBlock(ScriptableRenderContext context, ref CullingResults cullResults, DrawingSettings ds, FilteringSettings fs, RenderStateBlock rsb, ref RendererList rl)
		{
			RendererListParams param = default(RendererListParams);
			NativeArray<RenderStateBlock> stateBlocks = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<RenderStateBlock>((void*)(&rsb), 1, Allocator.None);
			ShaderTagId shaderTag = ShaderTagId.none;
			NativeArray<ShaderTagId> tagValues = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<ShaderTagId>((void*)(&shaderTag), 1, Allocator.None);
			param = new RendererListParams(cullResults, ds, fs)
			{
				tagValues = new NativeArray<ShaderTagId>?(tagValues),
				stateBlocks = new NativeArray<RenderStateBlock>?(stateBlocks)
			};
			rl = context.CreateRendererList(ref param);
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x00026F00 File Offset: 0x00025100
		internal static void CreateRendererListWithRenderStateBlock(RenderGraph renderGraph, ref CullingResults cullResults, DrawingSettings ds, FilteringSettings fs, RenderStateBlock rsb, ref RendererListHandle rl)
		{
			RenderingUtils.s_ShaderTagValues[0] = ShaderTagId.none;
			RenderingUtils.s_RenderStateBlocks[0] = rsb;
			NativeArray<ShaderTagId> tagValues = new NativeArray<ShaderTagId>(RenderingUtils.s_ShaderTagValues, Allocator.Temp);
			NativeArray<RenderStateBlock> stateBlocks = new NativeArray<RenderStateBlock>(RenderingUtils.s_RenderStateBlocks, Allocator.Temp);
			RendererListParams param = new RendererListParams(cullResults, ds, fs)
			{
				tagValues = new NativeArray<ShaderTagId>?(tagValues),
				stateBlocks = new NativeArray<RenderStateBlock>?(stateBlocks),
				isPassTagName = false
			};
			rl = renderGraph.CreateRendererList(in param);
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x00026F86 File Offset: 0x00025186
		internal static void ClearSystemInfoCache()
		{
			RenderingUtils.m_RenderTextureFormatSupport.Clear();
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00026F94 File Offset: 0x00025194
		public static bool SupportsRenderTextureFormat(RenderTextureFormat format)
		{
			bool support;
			if (!RenderingUtils.m_RenderTextureFormatSupport.TryGetValue(format, out support))
			{
				support = SystemInfo.SupportsRenderTextureFormat(format);
				RenderingUtils.m_RenderTextureFormatSupport.Add(format, support);
			}
			return support;
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x00026FC4 File Offset: 0x000251C4
		[Obsolete("Use SystemInfo.IsFormatSupported instead.", false)]
		public static bool SupportsGraphicsFormat(GraphicsFormat format, FormatUsage usage)
		{
			GraphicsFormatUsage graphicsFormatUsage = (GraphicsFormatUsage)(1 << (int)usage);
			return SystemInfo.IsFormatSupported(format, graphicsFormatUsage);
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x00026FE0 File Offset: 0x000251E0
		internal static int GetLastValidColorBufferIndex(RenderTargetIdentifier[] colorBuffers)
		{
			int i = colorBuffers.Length - 1;
			while (i >= 0 && !(colorBuffers[i] != 0))
			{
				i--;
			}
			return i;
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00027014 File Offset: 0x00025214
		internal static uint GetValidColorBufferCount(RTHandle[] colorBuffers)
		{
			uint nonNullColorBuffers = 0U;
			if (colorBuffers != null)
			{
				foreach (RTHandle identifier in colorBuffers)
				{
					if (identifier != null && identifier.nameID != 0)
					{
						nonNullColorBuffers += 1U;
					}
				}
			}
			return nonNullColorBuffers;
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x00027055 File Offset: 0x00025255
		internal static bool IsMRT(RTHandle[] colorBuffers)
		{
			return RenderingUtils.GetValidColorBufferCount(colorBuffers) > 1U;
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x00027060 File Offset: 0x00025260
		internal static bool Contains(RenderTargetIdentifier[] source, RenderTargetIdentifier value)
		{
			for (int i = 0; i < source.Length; i++)
			{
				if (source[i] == value)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00027090 File Offset: 0x00025290
		internal static int IndexOf(RTHandle[] source, RenderTargetIdentifier value)
		{
			for (int i = 0; i < source.Length; i++)
			{
				if (source[i] == value)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x000270BE File Offset: 0x000252BE
		internal static int IndexOf(RTHandle[] source, RTHandle value)
		{
			return RenderingUtils.IndexOf(source, value.nameID);
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x000270CC File Offset: 0x000252CC
		internal static uint CountDistinct(RTHandle[] source, RTHandle value)
		{
			uint count = 0U;
			for (int i = 0; i < source.Length; i++)
			{
				if (source[i] != null && source[i].nameID != 0 && source[i].nameID != value.nameID)
				{
					count += 1U;
				}
			}
			return count;
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00027120 File Offset: 0x00025320
		internal static int LastValid(RTHandle[] source)
		{
			for (int i = source.Length - 1; i >= 0; i--)
			{
				if (source[i] != null && source[i].nameID != 0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0002715A File Offset: 0x0002535A
		internal static bool Contains(ClearFlag a, ClearFlag b)
		{
			return (a & b) == b;
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00027164 File Offset: 0x00025364
		internal static bool SequenceEqual(RTHandle[] left, RTHandle[] right)
		{
			if (left.Length != right.Length)
			{
				return false;
			}
			for (int i = 0; i < left.Length; i++)
			{
				if (left[i].nameID != right[i].nameID)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x000271A3 File Offset: 0x000253A3
		internal static bool MultisampleDepthResolveSupported()
		{
			return Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.OSXPlayer && SystemInfo.supportsMultisampleResolveDepth && SystemInfo.supportsMultisampleResolveStencil;
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x000271C4 File Offset: 0x000253C4
		internal static bool RTHandleNeedsReAlloc(RTHandle handle, in TextureDesc descriptor, bool scaled)
		{
			return handle == null || handle.rt == null || handle.useScaling != scaled || (!scaled && (handle.rt.width != descriptor.width || handle.rt.height != descriptor.height)) || ((handle.rt.descriptor.depthStencilFormat != GraphicsFormat.None) ? handle.rt.descriptor.depthStencilFormat : handle.rt.descriptor.graphicsFormat) != descriptor.format || handle.rt.descriptor.dimension != descriptor.dimension || handle.rt.descriptor.enableRandomWrite != descriptor.enableRandomWrite || handle.rt.descriptor.useMipMap != descriptor.useMipMap || handle.rt.descriptor.autoGenerateMips != descriptor.autoGenerateMips || handle.rt.descriptor.msaaSamples != (int)descriptor.msaaSamples || handle.rt.descriptor.bindMS != descriptor.bindTextureMS || handle.rt.descriptor.useDynamicScale != descriptor.useDynamicScale || handle.rt.descriptor.memoryless != descriptor.memoryless || handle.rt.filterMode != descriptor.filterMode || handle.rt.wrapMode != descriptor.wrapMode || handle.rt.anisoLevel != descriptor.anisoLevel || handle.rt.mipMapBias != descriptor.mipMapBias || handle.name != descriptor.name;
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x000273AC File Offset: 0x000255AC
		internal unsafe static RenderTargetIdentifier GetCameraTargetIdentifier(ref RenderingData renderingData)
		{
			ref CameraData cameraData = ref renderingData.cameraData;
			RenderTargetIdentifier cameraTarget = ((*cameraData.targetTexture != null) ? new RenderTargetIdentifier(*cameraData.targetTexture) : BuiltinRenderTextureType.CameraTarget);
			if (cameraData.xr.enabled)
			{
				if (cameraData.xr.singlePassEnabled)
				{
					cameraTarget = cameraData.xr.renderTarget;
				}
				else
				{
					int depthSlice = cameraData.xr.GetTextureArraySlice(0);
					cameraTarget = new RenderTargetIdentifier(cameraData.xr.renderTarget, 0, CubemapFace.Unknown, depthSlice);
				}
			}
			return cameraTarget;
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00027430 File Offset: 0x00025630
		[Obsolete("This method will be removed in a future release. Please use ReAllocateHandleIfNeeded instead. #from(2023.3)")]
		public static bool ReAllocateIfNeeded(ref RTHandle handle, in RenderTextureDescriptor descriptor, FilterMode filterMode = FilterMode.Point, TextureWrapMode wrapMode = TextureWrapMode.Repeat, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, string name = "")
		{
			TextureDesc requestRTDesc = RTHandleResourcePool.CreateTextureDesc(descriptor, TextureSizeMode.Explicit, anisoLevel, 0f, filterMode, wrapMode, name);
			if (!RenderingUtils.RTHandleNeedsReAlloc(handle, in requestRTDesc, false))
			{
				return false;
			}
			if (handle != null && handle.rt != null)
			{
				RenderingUtils.AddStaleResourceToPoolOrRelease(RTHandleResourcePool.CreateTextureDesc(handle.rt.descriptor, TextureSizeMode.Explicit, handle.rt.anisoLevel, handle.rt.mipMapBias, handle.rt.filterMode, handle.rt.wrapMode, handle.name), handle);
			}
			if (UniversalRenderPipeline.s_RTHandlePool.TryGetResource(in requestRTDesc, out handle, true))
			{
				return true;
			}
			handle = RTHandles.Alloc(in descriptor, filterMode, wrapMode, isShadowMap, anisoLevel, mipMapBias, name);
			return true;
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x000274F0 File Offset: 0x000256F0
		[Obsolete("This method will be removed in a future release. Please use ReAllocateHandleIfNeeded instead. #from(2023.3)")]
		public static bool ReAllocateIfNeeded(ref RTHandle handle, Vector2 scaleFactor, in RenderTextureDescriptor descriptor, FilterMode filterMode = FilterMode.Point, TextureWrapMode wrapMode = TextureWrapMode.Repeat, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, string name = "")
		{
			bool flag = handle != null && handle.useScaling && handle.scaleFactor == scaleFactor;
			TextureDesc requestRTDesc = RTHandleResourcePool.CreateTextureDesc(descriptor, TextureSizeMode.Scale, anisoLevel, 0f, filterMode, wrapMode, "");
			if (flag && !RenderingUtils.RTHandleNeedsReAlloc(handle, in requestRTDesc, true))
			{
				return false;
			}
			if (handle != null && handle.rt != null)
			{
				RenderingUtils.AddStaleResourceToPoolOrRelease(RTHandleResourcePool.CreateTextureDesc(handle.rt.descriptor, TextureSizeMode.Scale, handle.rt.anisoLevel, handle.rt.mipMapBias, handle.rt.filterMode, handle.rt.wrapMode, ""), handle);
			}
			if (UniversalRenderPipeline.s_RTHandlePool.TryGetResource(in requestRTDesc, out handle, true))
			{
				return true;
			}
			handle = RTHandles.Alloc(scaleFactor, in descriptor, filterMode, wrapMode, isShadowMap, anisoLevel, mipMapBias, name);
			return true;
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x000275D4 File Offset: 0x000257D4
		[Obsolete("This method will be removed in a future release. Please use ReAllocateHandleIfNeeded instead. #from(2023.3)")]
		public static bool ReAllocateIfNeeded(ref RTHandle handle, ScaleFunc scaleFunc, in RenderTextureDescriptor descriptor, FilterMode filterMode = FilterMode.Point, TextureWrapMode wrapMode = TextureWrapMode.Repeat, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, string name = "")
		{
			bool flag = handle != null && handle.useScaling && handle.scaleFactor == Vector2.zero;
			TextureDesc requestRTDesc = RTHandleResourcePool.CreateTextureDesc(descriptor, TextureSizeMode.Functor, anisoLevel, 0f, filterMode, wrapMode, "");
			if (flag && !RenderingUtils.RTHandleNeedsReAlloc(handle, in requestRTDesc, true))
			{
				return false;
			}
			if (handle != null && handle.rt != null)
			{
				RenderingUtils.AddStaleResourceToPoolOrRelease(RTHandleResourcePool.CreateTextureDesc(handle.rt.descriptor, TextureSizeMode.Functor, handle.rt.anisoLevel, handle.rt.mipMapBias, handle.rt.filterMode, handle.rt.wrapMode, ""), handle);
			}
			if (UniversalRenderPipeline.s_RTHandlePool.TryGetResource(in requestRTDesc, out handle, true))
			{
				return true;
			}
			handle = RTHandles.Alloc(scaleFunc, in descriptor, filterMode, wrapMode, isShadowMap, anisoLevel, mipMapBias, name);
			return true;
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x000276BC File Offset: 0x000258BC
		public static bool ReAllocateHandleIfNeeded(ref RTHandle handle, in RenderTextureDescriptor descriptor, FilterMode filterMode = FilterMode.Point, TextureWrapMode wrapMode = TextureWrapMode.Repeat, int anisoLevel = 1, float mipMapBias = 0f, string name = "")
		{
			TextureDesc requestRTDesc = RTHandleResourcePool.CreateTextureDesc(descriptor, TextureSizeMode.Explicit, anisoLevel, 0f, filterMode, wrapMode, name);
			if (!RenderingUtils.RTHandleNeedsReAlloc(handle, in requestRTDesc, false))
			{
				return false;
			}
			if (handle != null && handle.rt != null)
			{
				RenderingUtils.AddStaleResourceToPoolOrRelease(RTHandleResourcePool.CreateTextureDesc(handle.rt.descriptor, TextureSizeMode.Explicit, handle.rt.anisoLevel, handle.rt.mipMapBias, handle.rt.filterMode, handle.rt.wrapMode, handle.name), handle);
			}
			if (UniversalRenderPipeline.s_RTHandlePool.TryGetResource(in requestRTDesc, out handle, true))
			{
				return true;
			}
			RenderTextureDescriptor renderTextureDescriptor = descriptor;
			GraphicsFormat graphicsFormat;
			if (renderTextureDescriptor.graphicsFormat == GraphicsFormat.None)
			{
				graphicsFormat = descriptor.depthStencilFormat;
			}
			else
			{
				renderTextureDescriptor = descriptor;
				graphicsFormat = renderTextureDescriptor.graphicsFormat;
			}
			GraphicsFormat actualFormat = graphicsFormat;
			RTHandleAllocInfo allocInfo = default(RTHandleAllocInfo);
			allocInfo.slices = descriptor.volumeDepth;
			allocInfo.format = actualFormat;
			allocInfo.filterMode = filterMode;
			allocInfo.wrapModeU = wrapMode;
			allocInfo.wrapModeV = wrapMode;
			allocInfo.wrapModeW = wrapMode;
			allocInfo.dimension = descriptor.dimension;
			renderTextureDescriptor = descriptor;
			allocInfo.enableRandomWrite = renderTextureDescriptor.enableRandomWrite;
			renderTextureDescriptor = descriptor;
			allocInfo.useMipMap = renderTextureDescriptor.useMipMap;
			renderTextureDescriptor = descriptor;
			allocInfo.autoGenerateMips = renderTextureDescriptor.autoGenerateMips;
			allocInfo.anisoLevel = anisoLevel;
			allocInfo.mipMapBias = mipMapBias;
			allocInfo.msaaSamples = (MSAASamples)descriptor.msaaSamples;
			renderTextureDescriptor = descriptor;
			allocInfo.bindTextureMS = renderTextureDescriptor.bindMS;
			renderTextureDescriptor = descriptor;
			allocInfo.useDynamicScale = renderTextureDescriptor.useDynamicScale;
			allocInfo.memoryless = descriptor.memoryless;
			allocInfo.vrUsage = descriptor.vrUsage;
			allocInfo.name = name;
			handle = RTHandles.Alloc(descriptor.width, descriptor.height, allocInfo);
			return true;
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00027898 File Offset: 0x00025A98
		public static bool ReAllocateHandleIfNeeded(ref RTHandle handle, Vector2 scaleFactor, in RenderTextureDescriptor descriptor, FilterMode filterMode = FilterMode.Point, TextureWrapMode wrapMode = TextureWrapMode.Repeat, int anisoLevel = 1, float mipMapBias = 0f, string name = "")
		{
			bool flag = handle != null && handle.useScaling && handle.scaleFactor == scaleFactor;
			TextureDesc requestRTDesc = RTHandleResourcePool.CreateTextureDesc(descriptor, TextureSizeMode.Scale, anisoLevel, 0f, filterMode, wrapMode, "");
			if (flag && !RenderingUtils.RTHandleNeedsReAlloc(handle, in requestRTDesc, true))
			{
				return false;
			}
			if (handle != null && handle.rt != null)
			{
				RenderingUtils.AddStaleResourceToPoolOrRelease(RTHandleResourcePool.CreateTextureDesc(handle.rt.descriptor, TextureSizeMode.Scale, handle.rt.anisoLevel, handle.rt.mipMapBias, handle.rt.filterMode, handle.rt.wrapMode, ""), handle);
			}
			if (UniversalRenderPipeline.s_RTHandlePool.TryGetResource(in requestRTDesc, out handle, true))
			{
				return true;
			}
			RenderTextureDescriptor renderTextureDescriptor = descriptor;
			GraphicsFormat graphicsFormat;
			if (renderTextureDescriptor.graphicsFormat == GraphicsFormat.None)
			{
				graphicsFormat = descriptor.depthStencilFormat;
			}
			else
			{
				renderTextureDescriptor = descriptor;
				graphicsFormat = renderTextureDescriptor.graphicsFormat;
			}
			GraphicsFormat actualFormat = graphicsFormat;
			RTHandleAllocInfo allocInfo = default(RTHandleAllocInfo);
			allocInfo.slices = descriptor.volumeDepth;
			allocInfo.format = actualFormat;
			allocInfo.filterMode = filterMode;
			allocInfo.wrapModeU = wrapMode;
			allocInfo.wrapModeV = wrapMode;
			allocInfo.wrapModeW = wrapMode;
			allocInfo.dimension = descriptor.dimension;
			renderTextureDescriptor = descriptor;
			allocInfo.enableRandomWrite = renderTextureDescriptor.enableRandomWrite;
			renderTextureDescriptor = descriptor;
			allocInfo.useMipMap = renderTextureDescriptor.useMipMap;
			renderTextureDescriptor = descriptor;
			allocInfo.autoGenerateMips = renderTextureDescriptor.autoGenerateMips;
			allocInfo.anisoLevel = anisoLevel;
			allocInfo.mipMapBias = mipMapBias;
			allocInfo.msaaSamples = (MSAASamples)descriptor.msaaSamples;
			renderTextureDescriptor = descriptor;
			allocInfo.bindTextureMS = renderTextureDescriptor.bindMS;
			renderTextureDescriptor = descriptor;
			allocInfo.useDynamicScale = renderTextureDescriptor.useDynamicScale;
			allocInfo.memoryless = descriptor.memoryless;
			allocInfo.vrUsage = descriptor.vrUsage;
			allocInfo.name = name;
			handle = RTHandles.Alloc(scaleFactor, allocInfo);
			return true;
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00027A90 File Offset: 0x00025C90
		public static bool ReAllocateHandleIfNeeded(ref RTHandle handle, ScaleFunc scaleFunc, in RenderTextureDescriptor descriptor, FilterMode filterMode = FilterMode.Point, TextureWrapMode wrapMode = TextureWrapMode.Repeat, int anisoLevel = 1, float mipMapBias = 0f, string name = "")
		{
			bool flag = handle != null && handle.useScaling && handle.scaleFactor == Vector2.zero;
			TextureDesc requestRTDesc = RTHandleResourcePool.CreateTextureDesc(descriptor, TextureSizeMode.Functor, anisoLevel, 0f, filterMode, wrapMode, "");
			if (flag && !RenderingUtils.RTHandleNeedsReAlloc(handle, in requestRTDesc, true))
			{
				return false;
			}
			if (handle != null && handle.rt != null)
			{
				RenderingUtils.AddStaleResourceToPoolOrRelease(RTHandleResourcePool.CreateTextureDesc(handle.rt.descriptor, TextureSizeMode.Functor, handle.rt.anisoLevel, handle.rt.mipMapBias, handle.rt.filterMode, handle.rt.wrapMode, ""), handle);
			}
			if (UniversalRenderPipeline.s_RTHandlePool.TryGetResource(in requestRTDesc, out handle, true))
			{
				return true;
			}
			RenderTextureDescriptor renderTextureDescriptor = descriptor;
			GraphicsFormat graphicsFormat;
			if (renderTextureDescriptor.graphicsFormat == GraphicsFormat.None)
			{
				graphicsFormat = descriptor.depthStencilFormat;
			}
			else
			{
				renderTextureDescriptor = descriptor;
				graphicsFormat = renderTextureDescriptor.graphicsFormat;
			}
			GraphicsFormat actualFormat = graphicsFormat;
			RTHandleAllocInfo allocInfo = default(RTHandleAllocInfo);
			allocInfo.slices = descriptor.volumeDepth;
			allocInfo.format = actualFormat;
			allocInfo.filterMode = filterMode;
			allocInfo.wrapModeU = wrapMode;
			allocInfo.wrapModeV = wrapMode;
			allocInfo.wrapModeW = wrapMode;
			allocInfo.dimension = descriptor.dimension;
			renderTextureDescriptor = descriptor;
			allocInfo.enableRandomWrite = renderTextureDescriptor.enableRandomWrite;
			renderTextureDescriptor = descriptor;
			allocInfo.useMipMap = renderTextureDescriptor.useMipMap;
			renderTextureDescriptor = descriptor;
			allocInfo.autoGenerateMips = renderTextureDescriptor.autoGenerateMips;
			allocInfo.anisoLevel = anisoLevel;
			allocInfo.mipMapBias = mipMapBias;
			allocInfo.msaaSamples = (MSAASamples)descriptor.msaaSamples;
			renderTextureDescriptor = descriptor;
			allocInfo.bindTextureMS = renderTextureDescriptor.bindMS;
			renderTextureDescriptor = descriptor;
			allocInfo.useDynamicScale = renderTextureDescriptor.useDynamicScale;
			allocInfo.memoryless = descriptor.memoryless;
			allocInfo.vrUsage = descriptor.vrUsage;
			allocInfo.name = name;
			handle = RTHandles.Alloc(scaleFunc, allocInfo);
			return true;
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00027C89 File Offset: 0x00025E89
		public static bool SetMaxRTHandlePoolCapacity(int capacity)
		{
			if (UniversalRenderPipeline.s_RTHandlePool == null)
			{
				return false;
			}
			UniversalRenderPipeline.s_RTHandlePool.staleResourceCapacity = capacity;
			return true;
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00027CA0 File Offset: 0x00025EA0
		internal static void AddStaleResourceToPoolOrRelease(TextureDesc desc, RTHandle handle)
		{
			if (!UniversalRenderPipeline.s_RTHandlePool.AddResourceToPool(in desc, handle, Time.frameCount))
			{
				RTHandles.Release(handle);
			}
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00027CBC File Offset: 0x00025EBC
		public static DrawingSettings CreateDrawingSettings(ShaderTagId shaderTagId, ref RenderingData renderingData, SortingCriteria sortingCriteria)
		{
			UniversalRenderingData universalRenderingData = renderingData.frameData.Get<UniversalRenderingData>();
			UniversalCameraData cameraData = renderingData.frameData.Get<UniversalCameraData>();
			UniversalLightData lightData = renderingData.frameData.Get<UniversalLightData>();
			return RenderingUtils.CreateDrawingSettings(shaderTagId, universalRenderingData, cameraData, lightData, sortingCriteria);
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00027CF8 File Offset: 0x00025EF8
		public static DrawingSettings CreateDrawingSettings(ShaderTagId shaderTagId, UniversalRenderingData renderingData, UniversalCameraData cameraData, UniversalLightData lightData, SortingCriteria sortingCriteria)
		{
			Camera camera = cameraData.camera;
			SortingSettings sortingSettings = new SortingSettings(camera)
			{
				criteria = sortingCriteria
			};
			return new DrawingSettings(shaderTagId, sortingSettings)
			{
				perObjectData = renderingData.perObjectData,
				mainLightIndex = lightData.mainLightIndex,
				enableDynamicBatching = renderingData.supportsDynamicBatching,
				enableInstancing = (camera.cameraType != CameraType.Preview)
			};
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00027D64 File Offset: 0x00025F64
		public static DrawingSettings CreateDrawingSettings(List<ShaderTagId> shaderTagIdList, ref RenderingData renderingData, SortingCriteria sortingCriteria)
		{
			UniversalRenderingData universalRenderingData = renderingData.frameData.Get<UniversalRenderingData>();
			UniversalCameraData cameraData = renderingData.frameData.Get<UniversalCameraData>();
			UniversalLightData lightData = renderingData.frameData.Get<UniversalLightData>();
			return RenderingUtils.CreateDrawingSettings(shaderTagIdList, universalRenderingData, cameraData, lightData, sortingCriteria);
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00027DA0 File Offset: 0x00025FA0
		public static DrawingSettings CreateDrawingSettings(List<ShaderTagId> shaderTagIdList, UniversalRenderingData renderingData, UniversalCameraData cameraData, UniversalLightData lightData, SortingCriteria sortingCriteria)
		{
			if (shaderTagIdList == null || shaderTagIdList.Count == 0)
			{
				Debug.LogWarning("ShaderTagId list is invalid. DrawingSettings is created with default pipeline ShaderTagId");
				return RenderingUtils.CreateDrawingSettings(new ShaderTagId("UniversalPipeline"), renderingData, cameraData, lightData, sortingCriteria);
			}
			DrawingSettings settings = RenderingUtils.CreateDrawingSettings(shaderTagIdList[0], renderingData, cameraData, lightData, sortingCriteria);
			for (int i = 1; i < shaderTagIdList.Count; i++)
			{
				settings.SetShaderPassName(i, shaderTagIdList[i]);
			}
			return settings;
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00027E0C File Offset: 0x0002600C
		internal static Vector4 GetFinalBlitScaleBias(RTHandle source, RTHandle destination, UniversalCameraData cameraData)
		{
			Vector2 viewportScale = (source.useScaling ? new Vector2(source.rtHandleProperties.rtHandleScale.x, source.rtHandleProperties.rtHandleScale.y) : Vector2.one);
			if (cameraData.IsRenderTargetProjectionMatrixFlipped(destination, null))
			{
				return new Vector4(viewportScale.x, viewportScale.y, 0f, 0f);
			}
			return new Vector4(viewportScale.x, -viewportScale.y, 0f, viewportScale.y);
		}

		// Token: 0x040008B6 RID: 2230
		private static List<ShaderTagId> m_LegacyShaderPassNames = new List<ShaderTagId>
		{
			new ShaderTagId("Always"),
			new ShaderTagId("ForwardBase"),
			new ShaderTagId("PrepassBase"),
			new ShaderTagId("Vertex"),
			new ShaderTagId("VertexLMRGBM"),
			new ShaderTagId("VertexLM")
		};

		// Token: 0x040008B7 RID: 2231
		private static AttachmentDescriptor s_EmptyAttachment = new AttachmentDescriptor(GraphicsFormat.None);

		// Token: 0x040008B8 RID: 2232
		private static Mesh s_FullscreenMesh = null;

		// Token: 0x040008B9 RID: 2233
		private static Material s_ErrorMaterial;

		// Token: 0x040008BA RID: 2234
		private static ShaderTagId[] s_ShaderTagValues = new ShaderTagId[1];

		// Token: 0x040008BB RID: 2235
		private static RenderStateBlock[] s_RenderStateBlocks = new RenderStateBlock[1];

		// Token: 0x040008BC RID: 2236
		private static Dictionary<RenderTextureFormat, bool> m_RenderTextureFormatSupport = new Dictionary<RenderTextureFormat, bool>();
	}
}
