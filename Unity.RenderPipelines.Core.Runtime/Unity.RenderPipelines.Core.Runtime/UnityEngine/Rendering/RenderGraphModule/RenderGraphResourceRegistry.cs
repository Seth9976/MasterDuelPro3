using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.RendererUtils;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x02000266 RID: 614
	internal class RenderGraphResourceRegistry
	{
		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06001090 RID: 4240 RVA: 0x0003BD33 File Offset: 0x00039F33
		// (set) Token: 0x06001091 RID: 4241 RVA: 0x0003BD3A File Offset: 0x00039F3A
		internal static RenderGraphResourceRegistry current
		{
			get
			{
				return RenderGraphResourceRegistry.m_CurrentRegistry;
			}
			set
			{
				RenderGraphResourceRegistry.m_CurrentRegistry = value;
			}
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x0003BD42 File Offset: 0x00039F42
		[Conditional("DEVELOPMENT_BUILD")]
		[Conditional("UNITY_EDITOR")]
		private void CheckTextureResource(TextureResource texResource)
		{
			if (texResource.graphicsResource == null && !texResource.imported)
			{
				throw new InvalidOperationException("Trying to use a texture (" + texResource.GetName() + ") that was already released or not yet created. Make sure you declare it for reading in your pass or you don't read it before it's been written to at least once.");
			}
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x0003BD70 File Offset: 0x00039F70
		internal RTHandle GetTexture(in TextureHandle handle)
		{
			TextureHandle textureHandle = handle;
			if (!textureHandle.IsValid())
			{
				return null;
			}
			return this.GetTextureResource(in handle.handle).graphicsResource;
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x0003BDA0 File Offset: 0x00039FA0
		internal RTHandle GetTexture(int index)
		{
			return this.GetTextureResource(index).graphicsResource;
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x0003BDB0 File Offset: 0x00039FB0
		internal bool TextureNeedsFallback(in TextureHandle handle)
		{
			TextureHandle textureHandle = handle;
			return textureHandle.IsValid() && this.GetTextureResource(in handle.handle).NeedsFallBack();
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x0003BDE0 File Offset: 0x00039FE0
		internal RendererList GetRendererList(in RendererListHandle handle)
		{
			RendererListHandle rendererListHandle = handle;
			if (!rendererListHandle.IsValid())
			{
				return RendererList.nullRendererList;
			}
			RendererListHandleType type = handle.type;
			if (type != RendererListHandleType.Renderers)
			{
				if (type != RendererListHandleType.Legacy)
				{
					return RendererList.nullRendererList;
				}
				if (handle >= this.m_RendererListLegacyResources.size)
				{
					return RendererList.nullRendererList;
				}
				if (!this.m_RendererListLegacyResources[handle].isActive)
				{
					return RendererList.nullRendererList;
				}
				return this.m_RendererListLegacyResources[handle].rendererList;
			}
			else
			{
				if (handle >= this.m_RendererListResources.size)
				{
					return RendererList.nullRendererList;
				}
				return this.m_RendererListResources[handle].rendererList;
			}
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x0003BEB2 File Offset: 0x0003A0B2
		[Conditional("DEVELOPMENT_BUILD")]
		[Conditional("UNITY_EDITOR")]
		private void CheckBufferResource(BufferResource bufferResource)
		{
			if (bufferResource.graphicsResource == null)
			{
				throw new InvalidOperationException("Trying to use a graphics buffer (" + bufferResource.GetName() + ") that was already released or not yet created. Make sure you declare it for reading in your pass or you don't read it before it's been written to at least once.");
			}
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x0003BED8 File Offset: 0x0003A0D8
		internal GraphicsBuffer GetBuffer(in BufferHandle handle)
		{
			BufferHandle bufferHandle = handle;
			if (!bufferHandle.IsValid())
			{
				return null;
			}
			return this.GetBufferResource(in handle.handle).graphicsResource;
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x0003BF08 File Offset: 0x0003A108
		internal GraphicsBuffer GetBuffer(int index)
		{
			return this.GetBufferResource(index).graphicsResource;
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x0003BF18 File Offset: 0x0003A118
		internal RayTracingAccelerationStructure GetRayTracingAccelerationStructure(in RayTracingAccelerationStructureHandle handle)
		{
			RayTracingAccelerationStructureHandle rayTracingAccelerationStructureHandle = handle;
			if (!rayTracingAccelerationStructureHandle.IsValid())
			{
				return null;
			}
			return this.GetRayTracingAccelerationStructureResource(in handle.handle).graphicsResource;
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x0003BF48 File Offset: 0x0003A148
		internal int GetSharedResourceCount(RenderGraphResourceType type)
		{
			return this.m_RenderGraphResources[(int)type].sharedResourcesCount;
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x0003BF58 File Offset: 0x0003A158
		private RenderGraphResourceRegistry()
		{
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x0003BFB0 File Offset: 0x0003A1B0
		internal RenderGraphResourceRegistry(RenderGraphDebugParams renderGraphDebug, RenderGraphLogger frameInformationLogger)
		{
			this.m_RenderGraphDebug = renderGraphDebug;
			this.m_FrameInformationLogger = frameInformationLogger;
			for (int i = 0; i < 3; i++)
			{
				this.m_RenderGraphResources[i] = new RenderGraphResourceRegistry.RenderGraphResourcesData();
			}
			this.m_RenderGraphResources[0].createResourceCallback = new RenderGraphResourceRegistry.ResourceCreateCallback(this.CreateTextureCallback);
			this.m_RenderGraphResources[0].releaseResourceCallback = new RenderGraphResourceRegistry.ResourceCallback(this.ReleaseTextureCallback);
			this.m_RenderGraphResources[0].pool = new TexturePool();
			this.m_RenderGraphResources[1].pool = new BufferPool();
			this.m_RenderGraphResources[2].pool = null;
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x0003C092 File Offset: 0x0003A292
		internal void BeginRenderGraph(int executionCount)
		{
			this.m_ExecutionCount = executionCount;
			ResourceHandle.NewFrame(executionCount);
			if (this.m_RenderGraphDebug.enableLogging)
			{
				this.m_ResourceLogger.Initialize("RenderGraph Resources");
			}
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x0003C0BE File Offset: 0x0003A2BE
		internal void BeginExecute(int currentFrameIndex)
		{
			this.m_CurrentFrameIndex = currentFrameIndex;
			this.ManageSharedRenderGraphResources();
			RenderGraphResourceRegistry.current = this;
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x0003C0D3 File Offset: 0x0003A2D3
		internal void EndExecute()
		{
			RenderGraphResourceRegistry.current = null;
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x00005704 File Offset: 0x00003904
		[Conditional("DEVELOPMENT_BUILD")]
		[Conditional("UNITY_EDITOR")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void CheckHandleValidity(in ResourceHandle res)
		{
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x0003C0DC File Offset: 0x0003A2DC
		[Conditional("DEVELOPMENT_BUILD")]
		[Conditional("UNITY_EDITOR")]
		private void CheckHandleValidity(RenderGraphResourceType type, int index)
		{
			if (RenderGraph.enableValidityChecks)
			{
				DynamicArray<IRenderGraphResource> resources = this.m_RenderGraphResources[(int)type].resourceArray;
				if (index == 0)
				{
					throw new ArgumentException(string.Format("Trying to access resource of type {0} with an null resource index.", type));
				}
				if (index >= resources.size)
				{
					throw new ArgumentException(string.Format("Trying to access resource of type {0} with an invalid resource index {1}", type, index));
				}
			}
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x0003C13C File Offset: 0x0003A33C
		internal unsafe void IncrementWriteCount(in ResourceHandle res)
		{
			RenderGraphResourceRegistry.RenderGraphResourcesData[] renderGraphResources = this.m_RenderGraphResources;
			ResourceHandle resourceHandle = res;
			DynamicArray<IRenderGraphResource> resourceArray = renderGraphResources[resourceHandle.iType].resourceArray;
			resourceHandle = res;
			resourceArray[resourceHandle.index]->IncrementWriteCount();
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x0003C17C File Offset: 0x0003A37C
		internal unsafe void NewVersion(in ResourceHandle res)
		{
			RenderGraphResourceRegistry.RenderGraphResourcesData[] renderGraphResources = this.m_RenderGraphResources;
			ResourceHandle resourceHandle = res;
			DynamicArray<IRenderGraphResource> resourceArray = renderGraphResources[resourceHandle.iType].resourceArray;
			resourceHandle = res;
			resourceArray[resourceHandle.index]->NewVersion();
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x0003C1C0 File Offset: 0x0003A3C0
		internal unsafe ResourceHandle GetLatestVersionHandle(in ResourceHandle res)
		{
			RenderGraphResourceRegistry.RenderGraphResourcesData[] renderGraphResources = this.m_RenderGraphResources;
			ResourceHandle resourceHandle = res;
			DynamicArray<IRenderGraphResource> resourceArray = renderGraphResources[resourceHandle.iType].resourceArray;
			resourceHandle = res;
			int ver = resourceArray[resourceHandle.index]->version;
			if (this.IsRenderGraphResourceShared(in res))
			{
				ver -= this.m_ExecutionCount;
			}
			return new ResourceHandle(in res, ver);
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x0003C21C File Offset: 0x0003A41C
		internal unsafe int GetLatestVersionNumber(in ResourceHandle res)
		{
			RenderGraphResourceRegistry.RenderGraphResourcesData[] renderGraphResources = this.m_RenderGraphResources;
			ResourceHandle resourceHandle = res;
			DynamicArray<IRenderGraphResource> resourceArray = renderGraphResources[resourceHandle.iType].resourceArray;
			resourceHandle = res;
			int ver = resourceArray[resourceHandle.index]->version;
			if (this.IsRenderGraphResourceShared(in res))
			{
				ver -= this.m_ExecutionCount;
			}
			return ver;
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x0003C270 File Offset: 0x0003A470
		internal ResourceHandle GetZeroVersionedHandle(in ResourceHandle res)
		{
			return new ResourceHandle(in res, 0);
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x0003C27C File Offset: 0x0003A47C
		internal unsafe ResourceHandle GetNewVersionedHandle(in ResourceHandle res)
		{
			RenderGraphResourceRegistry.RenderGraphResourcesData[] renderGraphResources = this.m_RenderGraphResources;
			ResourceHandle resourceHandle = res;
			DynamicArray<IRenderGraphResource> resourceArray = renderGraphResources[resourceHandle.iType].resourceArray;
			resourceHandle = res;
			int ver = resourceArray[resourceHandle.index]->NewVersion();
			if (this.IsRenderGraphResourceShared(in res))
			{
				ver -= this.m_ExecutionCount;
			}
			return new ResourceHandle(in res, ver);
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x0003C2D8 File Offset: 0x0003A4D8
		internal unsafe IRenderGraphResource GetResourceLowLevel(in ResourceHandle res)
		{
			RenderGraphResourceRegistry.RenderGraphResourcesData[] renderGraphResources = this.m_RenderGraphResources;
			ResourceHandle resourceHandle = res;
			DynamicArray<IRenderGraphResource> resourceArray = renderGraphResources[resourceHandle.iType].resourceArray;
			resourceHandle = res;
			return *resourceArray[resourceHandle.index];
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x0003C314 File Offset: 0x0003A514
		internal unsafe string GetRenderGraphResourceName(in ResourceHandle res)
		{
			RenderGraphResourceRegistry.RenderGraphResourcesData[] renderGraphResources = this.m_RenderGraphResources;
			ResourceHandle resourceHandle = res;
			DynamicArray<IRenderGraphResource> resourceArray = renderGraphResources[resourceHandle.iType].resourceArray;
			resourceHandle = res;
			return resourceArray[resourceHandle.index]->GetName();
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x0003C354 File Offset: 0x0003A554
		internal unsafe string GetRenderGraphResourceName(RenderGraphResourceType type, int index)
		{
			return this.m_RenderGraphResources[(int)type].resourceArray[index]->GetName();
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x0003C370 File Offset: 0x0003A570
		internal unsafe bool IsRenderGraphResourceImported(in ResourceHandle res)
		{
			RenderGraphResourceRegistry.RenderGraphResourcesData[] renderGraphResources = this.m_RenderGraphResources;
			ResourceHandle resourceHandle = res;
			DynamicArray<IRenderGraphResource> resourceArray = renderGraphResources[resourceHandle.iType].resourceArray;
			resourceHandle = res;
			return resourceArray[resourceHandle.index]->imported;
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x0003C3B0 File Offset: 0x0003A5B0
		internal unsafe bool IsRenderGraphResourceForceReleased(RenderGraphResourceType type, int index)
		{
			return this.m_RenderGraphResources[(int)type].resourceArray[index]->forceRelease;
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x0003C3CB File Offset: 0x0003A5CB
		internal bool IsRenderGraphResourceShared(RenderGraphResourceType type, int index)
		{
			return index <= this.m_RenderGraphResources[(int)type].sharedResourcesCount;
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x0003C3E0 File Offset: 0x0003A5E0
		internal bool IsRenderGraphResourceShared(in ResourceHandle res)
		{
			RenderGraphResourceType type = res.type;
			ResourceHandle resourceHandle = res;
			return this.IsRenderGraphResourceShared(type, resourceHandle.index);
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x0003C408 File Offset: 0x0003A608
		internal unsafe bool IsGraphicsResourceCreated(in ResourceHandle res)
		{
			RenderGraphResourceRegistry.RenderGraphResourcesData[] renderGraphResources = this.m_RenderGraphResources;
			ResourceHandle resourceHandle = res;
			DynamicArray<IRenderGraphResource> resourceArray = renderGraphResources[resourceHandle.iType].resourceArray;
			resourceHandle = res;
			return resourceArray[resourceHandle.index]->IsCreated();
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x0003C448 File Offset: 0x0003A648
		internal bool IsRendererListCreated(in RendererListHandle res)
		{
			RendererListHandleType type = res.type;
			if (type != RendererListHandleType.Renderers)
			{
				return type == RendererListHandleType.Legacy && this.m_RendererListLegacyResources[res].isActive && this.m_RendererListLegacyResources[res].rendererList.isValid;
			}
			return this.m_RendererListResources[res].rendererList.isValid;
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x0003C4C7 File Offset: 0x0003A6C7
		internal unsafe bool IsRenderGraphResourceImported(RenderGraphResourceType type, int index)
		{
			return this.m_RenderGraphResources[(int)type].resourceArray[index]->imported;
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x0003C4E4 File Offset: 0x0003A6E4
		internal unsafe int GetRenderGraphResourceTransientIndex(in ResourceHandle res)
		{
			RenderGraphResourceRegistry.RenderGraphResourcesData[] renderGraphResources = this.m_RenderGraphResources;
			ResourceHandle resourceHandle = res;
			DynamicArray<IRenderGraphResource> resourceArray = renderGraphResources[resourceHandle.iType].resourceArray;
			resourceHandle = res;
			return resourceArray[resourceHandle.index]->transientPassIndex;
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x0003C524 File Offset: 0x0003A724
		internal TextureHandle ImportTexture(in RTHandle rt, bool isBuiltin = false)
		{
			ImportResourceParams importParams = default(ImportResourceParams);
			importParams.clearOnFirstUse = false;
			importParams.discardOnLastUse = false;
			return this.ImportTexture(in rt, in importParams, isBuiltin);
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x0003C554 File Offset: 0x0003A754
		internal TextureHandle ImportTexture(in RTHandle rt, in ImportResourceParams importParams, bool isBuiltin = false)
		{
			if (rt != null && !(rt.m_RT != null))
			{
				rt.m_ExternalTexture != null;
			}
			TextureResource texResource;
			int newHandle = this.m_RenderGraphResources[0].AddNewRenderGraphResource<TextureResource>(out texResource, true);
			texResource.graphicsResource = rt;
			texResource.imported = true;
			RenderTexture renderTexture = ((rt != null) ? ((rt.m_RT != null) ? rt.m_RT : (rt.m_ExternalTexture as RenderTexture)) : null);
			if (renderTexture)
			{
				texResource.desc = new TextureDesc(renderTexture);
				texResource.validDesc = true;
			}
			texResource.desc.clearBuffer = importParams.clearOnFirstUse;
			texResource.desc.clearColor = importParams.clearColor;
			texResource.desc.discardBuffer = importParams.discardOnLastUse;
			TextureHandle textureHandle = new TextureHandle(newHandle, false, isBuiltin);
			RTHandle rthandle = rt;
			return textureHandle;
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x0003C628 File Offset: 0x0003A828
		internal TextureHandle ImportTexture(in RTHandle rt, RenderTargetInfo info, in ImportResourceParams importParams)
		{
			TextureResource texResource;
			int newHandle = this.m_RenderGraphResources[0].AddNewRenderGraphResource<TextureResource>(out texResource, true);
			texResource.graphicsResource = rt;
			texResource.imported = true;
			texResource.desc = default(TextureDesc);
			if (rt != null && rt.m_NameID != RenderGraphResourceRegistry.emptyId)
			{
				texResource.desc.format = info.format;
				texResource.desc.width = info.width;
				texResource.desc.height = info.height;
				texResource.desc.slices = info.volumeDepth;
				texResource.desc.msaaSamples = (MSAASamples)info.msaaSamples;
				texResource.desc.bindTextureMS = info.bindMS;
				texResource.desc.clearBuffer = importParams.clearOnFirstUse;
				texResource.desc.clearColor = importParams.clearColor;
				texResource.desc.discardBuffer = importParams.discardOnLastUse;
				texResource.validDesc = false;
			}
			return new TextureHandle(newHandle, false, false);
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x0003C728 File Offset: 0x0003A928
		internal unsafe TextureHandle CreateSharedTexture(in TextureDesc desc, bool explicitRelease)
		{
			RenderGraphResourceRegistry.RenderGraphResourcesData textureResources = this.m_RenderGraphResources[0];
			int sharedTextureCount = textureResources.sharedResourcesCount;
			TextureResource texResource = null;
			int textureIndex = -1;
			for (int i = 1; i < sharedTextureCount + 1; i++)
			{
				if (!textureResources.resourceArray[i]->shared)
				{
					texResource = (TextureResource)(*textureResources.resourceArray[i]);
					textureIndex = i;
					break;
				}
			}
			if (texResource == null)
			{
				textureIndex = this.m_RenderGraphResources[0].AddNewRenderGraphResource<TextureResource>(out texResource, false);
				textureResources.sharedResourcesCount++;
			}
			texResource.imported = true;
			texResource.shared = true;
			texResource.sharedExplicitRelease = explicitRelease;
			texResource.desc = desc;
			texResource.validDesc = true;
			return new TextureHandle(textureIndex, true, false);
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x0003C7DB File Offset: 0x0003A9DB
		internal void RefreshSharedTextureDesc(in TextureHandle texture, in TextureDesc desc)
		{
			TextureResource textureResource = this.GetTextureResource(in texture.handle);
			textureResource.ReleaseGraphicsResource();
			textureResource.desc = desc;
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x0003C7FC File Offset: 0x0003A9FC
		internal void ReleaseSharedTexture(in TextureHandle texture)
		{
			RenderGraphResourceRegistry.RenderGraphResourcesData texResources = this.m_RenderGraphResources[0];
			ResourceHandle handle = texture.handle;
			if (handle.index == texResources.sharedResourcesCount)
			{
				texResources.sharedResourcesCount--;
			}
			TextureResource textureResource = this.GetTextureResource(in texture.handle);
			textureResource.ReleaseGraphicsResource();
			textureResource.Reset(null);
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x0003C850 File Offset: 0x0003AA50
		internal TextureHandle ImportBackbuffer(RenderTargetIdentifier rt, in RenderTargetInfo info, in ImportResourceParams importParams)
		{
			if (this.m_CurrentBackbuffer != null)
			{
				this.m_CurrentBackbuffer.SetTexture(rt);
			}
			else
			{
				this.m_CurrentBackbuffer = RTHandles.Alloc(rt, "Backbuffer");
			}
			TextureResource texResource;
			int newHandle = this.m_RenderGraphResources[0].AddNewRenderGraphResource<TextureResource>(out texResource, true);
			texResource.graphicsResource = this.m_CurrentBackbuffer;
			texResource.imported = true;
			texResource.desc = default(TextureDesc);
			texResource.desc.width = info.width;
			texResource.desc.height = info.height;
			texResource.desc.slices = info.volumeDepth;
			texResource.desc.msaaSamples = (MSAASamples)info.msaaSamples;
			texResource.desc.bindTextureMS = info.bindMS;
			texResource.desc.format = info.format;
			texResource.desc.clearBuffer = importParams.clearOnFirstUse;
			texResource.desc.clearColor = importParams.clearColor;
			texResource.desc.discardBuffer = importParams.discardOnLastUse;
			texResource.validDesc = false;
			return new TextureHandle(newHandle, false, false);
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x0003C95C File Offset: 0x0003AB5C
		[Conditional("DEVELOPMENT_BUILD")]
		[Conditional("UNITY_EDITOR")]
		private void ValidateRenderTarget(in ResourceHandle res)
		{
			if (RenderGraph.enableValidityChecks)
			{
				RenderTargetInfo outInfo;
				this.GetRenderTargetInfo(in res, out outInfo);
			}
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x0003C97C File Offset: 0x0003AB7C
		internal void GetRenderTargetInfo(in ResourceHandle res, out RenderTargetInfo outInfo)
		{
			TextureResource tex = this.GetTextureResource(in res);
			if (!tex.imported)
			{
				TextureDesc desc = this.GetTextureResourceDesc(in res, false);
				Vector2Int dim = desc.CalculateFinalDimensions();
				outInfo = default(RenderTargetInfo);
				outInfo.width = dim.x;
				outInfo.height = dim.y;
				outInfo.volumeDepth = desc.slices;
				outInfo.msaaSamples = (int)desc.msaaSamples;
				outInfo.bindMS = desc.bindTextureMS;
				outInfo.format = desc.format;
				return;
			}
			RTHandle handle = tex.graphicsResource;
			if (handle == null)
			{
				outInfo = default(RenderTargetInfo);
				return;
			}
			if (handle.m_RT != null)
			{
				outInfo = default(RenderTargetInfo);
				outInfo.width = handle.m_RT.width;
				outInfo.height = handle.m_RT.height;
				outInfo.volumeDepth = handle.m_RT.volumeDepth;
				outInfo.format = this.GetFormat(handle.m_RT.graphicsFormat, handle.m_RT.depthStencilFormat);
				outInfo.msaaSamples = handle.m_RT.antiAliasing;
				outInfo.bindMS = handle.m_RT.bindTextureMS;
				return;
			}
			if (handle.m_ExternalTexture != null)
			{
				outInfo = default(RenderTargetInfo);
				outInfo.width = handle.m_ExternalTexture.width;
				outInfo.height = handle.m_ExternalTexture.height;
				outInfo.volumeDepth = 1;
				if (handle.m_ExternalTexture is RenderTexture)
				{
					RenderTexture rt = (RenderTexture)handle.m_ExternalTexture;
					outInfo.format = this.GetFormat(rt.graphicsFormat, rt.depthStencilFormat);
					outInfo.msaaSamples = rt.antiAliasing;
				}
				else
				{
					outInfo.format = handle.m_ExternalTexture.graphicsFormat;
					outInfo.msaaSamples = 1;
				}
				outInfo.bindMS = false;
				return;
			}
			if (handle.m_NameID != RenderGraphResourceRegistry.emptyId)
			{
				TextureDesc desc2 = this.GetTextureResourceDesc(in res, true);
				outInfo = default(RenderTargetInfo);
				outInfo.width = desc2.width;
				outInfo.height = desc2.height;
				outInfo.volumeDepth = desc2.slices;
				outInfo.msaaSamples = (int)desc2.msaaSamples;
				outInfo.format = desc2.format;
				outInfo.bindMS = desc2.bindTextureMS;
				return;
			}
			throw new Exception("Invalid imported texture. The RTHandle provided is invalid.");
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x0003CBB9 File Offset: 0x0003ADB9
		internal GraphicsFormat GetFormat(GraphicsFormat color, GraphicsFormat depthStencil)
		{
			if (depthStencil == GraphicsFormat.None)
			{
				return color;
			}
			return depthStencil;
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x0003CBC1 File Offset: 0x0003ADC1
		[Conditional("DEVELOPMENT_BUILD")]
		[Conditional("UNITY_EDITOR")]
		internal void ValidateFormat(GraphicsFormat color, GraphicsFormat depthStencil)
		{
			if (RenderGraph.enableValidityChecks && color != GraphicsFormat.None && depthStencil != GraphicsFormat.None)
			{
				throw new Exception("Invalid imported texture. Both a color and a depthStencil format are provided. The texture needs to either have a color format or a depth stencil format.");
			}
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x0003CBDC File Offset: 0x0003ADDC
		internal TextureHandle CreateTexture(in TextureDesc desc, int transientPassIndex = -1)
		{
			TextureResource texResource;
			int num = this.m_RenderGraphResources[0].AddNewRenderGraphResource<TextureResource>(out texResource, true);
			texResource.desc = desc;
			texResource.validDesc = true;
			texResource.transientPassIndex = transientPassIndex;
			texResource.requestFallBack = desc.fallBackToBlackTexture;
			return new TextureHandle(num, false, false);
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x0003CC26 File Offset: 0x0003AE26
		internal int GetResourceCount(RenderGraphResourceType type)
		{
			return this.m_RenderGraphResources[(int)type].resourceArray.size;
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x0003CC3A File Offset: 0x0003AE3A
		internal int GetTextureResourceCount()
		{
			return this.GetResourceCount(RenderGraphResourceType.Texture);
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x0003CC44 File Offset: 0x0003AE44
		internal unsafe TextureResource GetTextureResource(in ResourceHandle handle)
		{
			DynamicArray<IRenderGraphResource> resourceArray = this.m_RenderGraphResources[0].resourceArray;
			ResourceHandle resourceHandle = handle;
			return (*resourceArray[resourceHandle.index]) as TextureResource;
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x0003CC77 File Offset: 0x0003AE77
		internal unsafe TextureResource GetTextureResource(int index)
		{
			return (*this.m_RenderGraphResources[0].resourceArray[index]) as TextureResource;
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x0003CC94 File Offset: 0x0003AE94
		internal unsafe TextureDesc GetTextureResourceDesc(in ResourceHandle handle, bool noThrowOnInvalidDesc = false)
		{
			DynamicArray<IRenderGraphResource> resourceArray = this.m_RenderGraphResources[0].resourceArray;
			ResourceHandle resourceHandle = handle;
			TextureResource textureResource = (*resourceArray[resourceHandle.index]) as TextureResource;
			if (!textureResource.validDesc && !noThrowOnInvalidDesc)
			{
				throw new ArgumentException("The passed in texture handle does not have a valid descriptor. (This is most commonly cause by the handle referencing a built-in texture such as the system back buffer.)", "handle");
			}
			return textureResource.desc;
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x0003CCE8 File Offset: 0x0003AEE8
		internal RendererListHandle CreateRendererList(in RendererListDesc desc)
		{
			DynamicArray<RendererListResource> rendererListResources = this.m_RendererListResources;
			RendererListParams rendererListParams = RendererListDesc.ConvertToParameters(in desc);
			RendererListResource rendererListResource = new RendererListResource(in rendererListParams);
			return new RendererListHandle(rendererListResources.Add(in rendererListResource), RendererListHandleType.Renderers);
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x0003CD18 File Offset: 0x0003AF18
		internal RendererListHandle CreateRendererList(in RendererListParams desc)
		{
			DynamicArray<RendererListResource> rendererListResources = this.m_RendererListResources;
			RendererListResource rendererListResource = new RendererListResource(in desc);
			return new RendererListHandle(rendererListResources.Add(in rendererListResource), RendererListHandleType.Renderers);
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x0003CD40 File Offset: 0x0003AF40
		internal RendererListHandle CreateShadowRendererList(ScriptableRenderContext context, ref ShadowDrawingSettings shadowDrawinSettings)
		{
			RendererListLegacyResource resource = default(RendererListLegacyResource);
			resource.rendererList = context.CreateShadowRendererList(ref shadowDrawinSettings);
			return new RendererListHandle(this.m_RendererListLegacyResources.Add(in resource), RendererListHandleType.Legacy);
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x0003CD78 File Offset: 0x0003AF78
		internal RendererListHandle CreateGizmoRendererList(ScriptableRenderContext context, in Camera camera, in GizmoSubset gizmoSubset)
		{
			RendererListLegacyResource resource = default(RendererListLegacyResource);
			resource.rendererList = context.CreateGizmoRendererList(camera, gizmoSubset);
			return new RendererListHandle(this.m_RendererListLegacyResources.Add(in resource), RendererListHandleType.Legacy);
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x0003CDB4 File Offset: 0x0003AFB4
		internal RendererListHandle CreateUIOverlayRendererList(ScriptableRenderContext context, in Camera camera, in UISubset uiSubset)
		{
			RendererListLegacyResource resource = default(RendererListLegacyResource);
			resource.rendererList = context.CreateUIOverlayRendererList(camera, uiSubset);
			return new RendererListHandle(this.m_RendererListLegacyResources.Add(in resource), RendererListHandleType.Legacy);
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x0003CDF0 File Offset: 0x0003AFF0
		internal RendererListHandle CreateWireOverlayRendererList(ScriptableRenderContext context, in Camera camera)
		{
			RendererListLegacyResource resource = default(RendererListLegacyResource);
			resource.rendererList = context.CreateWireOverlayRendererList(camera);
			return new RendererListHandle(this.m_RendererListLegacyResources.Add(in resource), RendererListHandleType.Legacy);
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x0003CE28 File Offset: 0x0003B028
		internal RendererListHandle CreateSkyboxRendererList(ScriptableRenderContext context, in Camera camera)
		{
			RendererListLegacyResource resource = default(RendererListLegacyResource);
			resource.rendererList = context.CreateSkyboxRendererList(camera);
			return new RendererListHandle(this.m_RendererListLegacyResources.Add(in resource), RendererListHandleType.Legacy);
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x0003CE60 File Offset: 0x0003B060
		internal RendererListHandle CreateSkyboxRendererList(ScriptableRenderContext context, in Camera camera, Matrix4x4 projectionMatrix, Matrix4x4 viewMatrix)
		{
			RendererListLegacyResource resource = default(RendererListLegacyResource);
			resource.rendererList = context.CreateSkyboxRendererList(camera, projectionMatrix, viewMatrix);
			return new RendererListHandle(this.m_RendererListLegacyResources.Add(in resource), RendererListHandleType.Legacy);
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x0003CE9C File Offset: 0x0003B09C
		internal RendererListHandle CreateSkyboxRendererList(ScriptableRenderContext context, in Camera camera, Matrix4x4 projectionMatrixL, Matrix4x4 viewMatrixL, Matrix4x4 projectionMatrixR, Matrix4x4 viewMatrixR)
		{
			RendererListLegacyResource resource = default(RendererListLegacyResource);
			resource.rendererList = context.CreateSkyboxRendererList(camera, projectionMatrixL, viewMatrixL, projectionMatrixR, viewMatrixR);
			return new RendererListHandle(this.m_RendererListLegacyResources.Add(in resource), RendererListHandleType.Legacy);
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x0003CEDC File Offset: 0x0003B0DC
		internal BufferHandle ImportBuffer(GraphicsBuffer graphicsBuffer, bool forceRelease = false)
		{
			BufferResource bufferResource;
			int num = this.m_RenderGraphResources[1].AddNewRenderGraphResource<BufferResource>(out bufferResource, true);
			bufferResource.graphicsResource = graphicsBuffer;
			bufferResource.imported = true;
			bufferResource.forceRelease = forceRelease;
			bufferResource.validDesc = false;
			return new BufferHandle(num, false);
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x0003CF1C File Offset: 0x0003B11C
		internal BufferHandle CreateBuffer(in BufferDesc desc, int transientPassIndex = -1)
		{
			BufferResource bufferResource;
			int num = this.m_RenderGraphResources[1].AddNewRenderGraphResource<BufferResource>(out bufferResource, true);
			bufferResource.desc = desc;
			bufferResource.validDesc = true;
			bufferResource.transientPassIndex = transientPassIndex;
			return new BufferHandle(num, false);
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x0003CF5C File Offset: 0x0003B15C
		internal unsafe BufferDesc GetBufferResourceDesc(in ResourceHandle handle, bool noThrowOnInvalidDesc = false)
		{
			DynamicArray<IRenderGraphResource> resourceArray = this.m_RenderGraphResources[1].resourceArray;
			ResourceHandle resourceHandle = handle;
			BufferResource bufferResource = (*resourceArray[resourceHandle.index]) as BufferResource;
			if (!bufferResource.validDesc && !noThrowOnInvalidDesc)
			{
				throw new ArgumentException("The passed in buffer handle does not have a valid descriptor. (This is most commonly cause by importing the buffer.)", "handle");
			}
			return bufferResource.desc;
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x0003CFAF File Offset: 0x0003B1AF
		internal int GetBufferResourceCount()
		{
			return this.GetResourceCount(RenderGraphResourceType.Buffer);
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x0003CFB8 File Offset: 0x0003B1B8
		private unsafe BufferResource GetBufferResource(in ResourceHandle handle)
		{
			DynamicArray<IRenderGraphResource> resourceArray = this.m_RenderGraphResources[1].resourceArray;
			ResourceHandle resourceHandle = handle;
			return (*resourceArray[resourceHandle.index]) as BufferResource;
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x0003CFEB File Offset: 0x0003B1EB
		private unsafe BufferResource GetBufferResource(int index)
		{
			return (*this.m_RenderGraphResources[1].resourceArray[index]) as BufferResource;
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x0003D008 File Offset: 0x0003B208
		private unsafe RayTracingAccelerationStructureResource GetRayTracingAccelerationStructureResource(in ResourceHandle handle)
		{
			DynamicArray<IRenderGraphResource> resourceArray = this.m_RenderGraphResources[2].resourceArray;
			ResourceHandle resourceHandle = handle;
			return (*resourceArray[resourceHandle.index]) as RayTracingAccelerationStructureResource;
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x0003D03B File Offset: 0x0003B23B
		internal int GetRayTracingAccelerationStructureResourceCount()
		{
			return this.GetResourceCount(RenderGraphResourceType.AccelerationStructure);
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x0003D044 File Offset: 0x0003B244
		internal RayTracingAccelerationStructureHandle ImportRayTracingAccelerationStructure(in RayTracingAccelerationStructure accelStruct, string name)
		{
			RayTracingAccelerationStructureResource accelStructureResource;
			int num = this.m_RenderGraphResources[2].AddNewRenderGraphResource<RayTracingAccelerationStructureResource>(out accelStructureResource, false);
			accelStructureResource.graphicsResource = accelStruct;
			accelStructureResource.imported = true;
			accelStructureResource.forceRelease = false;
			accelStructureResource.desc.name = name;
			return new RayTracingAccelerationStructureHandle(num);
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x0003D088 File Offset: 0x0003B288
		internal unsafe void UpdateSharedResourceLastFrameIndex(int type, int index)
		{
			this.m_RenderGraphResources[type].resourceArray[index]->sharedResourceLastFrameUsed = this.m_ExecutionCount;
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x0003D0AC File Offset: 0x0003B2AC
		internal void UpdateSharedResourceLastFrameIndex(in ResourceHandle handle)
		{
			int type = (int)handle.type;
			ResourceHandle resourceHandle = handle;
			this.UpdateSharedResourceLastFrameIndex(type, resourceHandle.index);
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x0003D0D4 File Offset: 0x0003B2D4
		private unsafe void ManageSharedRenderGraphResources()
		{
			for (int type = 0; type < 3; type++)
			{
				RenderGraphResourceRegistry.RenderGraphResourcesData resources = this.m_RenderGraphResources[type];
				for (int i = 1; i < resources.sharedResourcesCount + 1; i++)
				{
					IRenderGraphResource resource = *this.m_RenderGraphResources[type].resourceArray[i];
					bool isCreated = resource.IsCreated();
					if (resource.sharedResourceLastFrameUsed == this.m_ExecutionCount && !isCreated)
					{
						resource.CreateGraphicsResource();
					}
					else if (isCreated && !resource.sharedExplicitRelease && resource.sharedResourceLastFrameUsed + 30 < this.m_ExecutionCount)
					{
						resource.ReleaseGraphicsResource();
					}
				}
			}
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x0003D164 File Offset: 0x0003B364
		internal unsafe bool CreatePooledResource(InternalRenderGraphContext rgContext, int type, int index)
		{
			bool? executedWork = new bool?(false);
			IRenderGraphResource resource = *this.m_RenderGraphResources[type].resourceArray[index];
			if (!resource.imported)
			{
				resource.CreatePooledGraphicsResource();
				if (this.m_RenderGraphDebug.enableLogging)
				{
					resource.LogCreation(this.m_FrameInformationLogger);
				}
				RenderGraphResourceRegistry.ResourceCreateCallback createResourceCallback = this.m_RenderGraphResources[type].createResourceCallback;
				executedWork = ((createResourceCallback != null) ? new bool?(createResourceCallback(rgContext, resource)) : null);
			}
			return executedWork.GetValueOrDefault();
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x0003D1E8 File Offset: 0x0003B3E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal bool CreatePooledResource(InternalRenderGraphContext rgContext, in ResourceHandle handle)
		{
			ResourceHandle resourceHandle = handle;
			int iType = resourceHandle.iType;
			resourceHandle = handle;
			return this.CreatePooledResource(rgContext, iType, resourceHandle.index);
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x0003D218 File Offset: 0x0003B418
		private bool CreateTextureCallback(InternalRenderGraphContext rgContext, IRenderGraphResource res)
		{
			TextureResource resource = res as TextureResource;
			FastMemoryDesc fastMemDesc = resource.desc.fastMemoryDesc;
			if (fastMemDesc.inFastMemory)
			{
				resource.graphicsResource.SwitchToFastMemory(rgContext.cmd, fastMemDesc.residencyFraction, fastMemDesc.flags, false);
			}
			bool executedWork = false;
			if ((this.forceManualClearOfResource && resource.desc.clearBuffer) || this.m_RenderGraphDebug.clearRenderTargetsAtCreation)
			{
				bool flag = this.m_RenderGraphDebug.clearRenderTargetsAtCreation && !resource.desc.clearBuffer;
				ClearFlag clearFlag = (GraphicsFormatUtility.IsDepthStencilFormat(resource.desc.format) ? ClearFlag.DepthStencil : ClearFlag.Color);
				Color clearColor = (flag ? Color.magenta : resource.desc.clearColor);
				CoreUtils.SetRenderTarget(rgContext.cmd, resource.graphicsResource, clearFlag, clearColor, 0, CubemapFace.Unknown, -1);
				executedWork = true;
			}
			return executedWork;
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x0003D2E8 File Offset: 0x0003B4E8
		internal unsafe void ReleasePooledResource(InternalRenderGraphContext rgContext, int type, int index)
		{
			IRenderGraphResource resource = *this.m_RenderGraphResources[type].resourceArray[index];
			if (!resource.imported || resource.forceRelease)
			{
				RenderGraphResourceRegistry.ResourceCallback releaseResourceCallback = this.m_RenderGraphResources[type].releaseResourceCallback;
				if (releaseResourceCallback != null)
				{
					releaseResourceCallback(rgContext, resource);
				}
				if (this.m_RenderGraphDebug.enableLogging)
				{
					resource.LogRelease(this.m_FrameInformationLogger);
				}
				resource.ReleasePooledGraphicsResource(this.m_CurrentFrameIndex);
			}
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x0003D35C File Offset: 0x0003B55C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void ReleasePooledResource(InternalRenderGraphContext rgContext, in ResourceHandle handle)
		{
			ResourceHandle resourceHandle = handle;
			int iType = resourceHandle.iType;
			resourceHandle = handle;
			this.ReleasePooledResource(rgContext, iType, resourceHandle.index);
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x0003D38C File Offset: 0x0003B58C
		private void ReleaseTextureCallback(InternalRenderGraphContext rgContext, IRenderGraphResource res)
		{
			TextureResource resource = res as TextureResource;
			if (this.m_RenderGraphDebug.clearRenderTargetsAtRelease)
			{
				ClearFlag clearFlag = (GraphicsFormatUtility.IsDepthStencilFormat(resource.desc.format) ? ClearFlag.DepthStencil : ClearFlag.Color);
				CoreUtils.SetRenderTarget(rgContext.cmd, resource.graphicsResource, clearFlag, Color.magenta, 0, CubemapFace.Unknown, -1);
			}
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x0003D3E0 File Offset: 0x0003B5E0
		[Conditional("DEVELOPMENT_BUILD")]
		[Conditional("UNITY_EDITOR")]
		private void ValidateTextureDesc(in TextureDesc desc)
		{
			if (RenderGraph.enableValidityChecks)
			{
				if (desc.format == GraphicsFormat.None)
				{
					throw new ArgumentException("Texture was created with with no format. The texture needs to either have a color format or a depth stencil format.");
				}
				if (desc.dimension == TextureDimension.None)
				{
					throw new ArgumentException("Texture was created with an invalid texture dimension.");
				}
				if (desc.slices == 0)
				{
					throw new ArgumentException("Texture was created with a slices parameter value of zero.");
				}
				if (desc.sizeMode == TextureSizeMode.Explicit && (desc.width == 0 || desc.height == 0))
				{
					throw new ArgumentException("Texture using Explicit size mode was create with either width or height at zero.");
				}
			}
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x0003D450 File Offset: 0x0003B650
		[Conditional("DEVELOPMENT_BUILD")]
		[Conditional("UNITY_EDITOR")]
		private void ValidateRendererListDesc(in RendererListDesc desc)
		{
			if (RenderGraph.enableValidityChecks)
			{
				RendererListDesc rendererListDesc = desc;
				if (!rendererListDesc.IsValid())
				{
					throw new ArgumentException("Renderer List descriptor is not valid.");
				}
				RenderQueueRange renderQueueRange = desc.renderQueueRange;
				if (renderQueueRange.lowerBound == 0)
				{
					renderQueueRange = desc.renderQueueRange;
					if (renderQueueRange.upperBound == 0)
					{
						throw new ArgumentException("Renderer List creation descriptor must have a valid RenderQueueRange.");
					}
				}
			}
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x0003D4AA File Offset: 0x0003B6AA
		[Conditional("DEVELOPMENT_BUILD")]
		[Conditional("UNITY_EDITOR")]
		private void ValidateBufferDesc(in BufferDesc desc)
		{
			if (RenderGraph.enableValidityChecks)
			{
				if (desc.stride % 4 != 0)
				{
					throw new ArgumentException("Invalid Graphics Buffer creation descriptor: Graphics Buffer stride must be at least 4.");
				}
				if (desc.count == 0)
				{
					throw new ArgumentException("Invalid Graphics Buffer creation descriptor: Graphics Buffer count  must be non zero.");
				}
			}
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x0003D4DC File Offset: 0x0003B6DC
		internal void CreateRendererLists(List<RendererListHandle> rendererLists, ScriptableRenderContext context, bool manualDispatch = false)
		{
			this.m_ActiveRendererLists.Clear();
			foreach (RendererListHandle rendererList in rendererLists)
			{
				RendererListHandleType type = rendererList.type;
				if (type != RendererListHandleType.Renderers)
				{
					if (type == RendererListHandleType.Legacy)
					{
						this.m_RendererListLegacyResources[rendererList].isActive = true;
					}
				}
				else
				{
					ref RendererListResource rendererListResource = ref this.m_RendererListResources[rendererList];
					ref RendererListParams desc = ref rendererListResource.desc;
					rendererListResource.rendererList = context.CreateRendererList(ref desc);
					this.m_ActiveRendererLists.Add(rendererListResource.rendererList);
				}
			}
			if (manualDispatch)
			{
				context.PrepareRendererListsAsync(this.m_ActiveRendererLists);
			}
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x0003D5A4 File Offset: 0x0003B7A4
		internal void Clear(bool onException)
		{
			this.LogResources();
			for (int i = 0; i < 3; i++)
			{
				this.m_RenderGraphResources[i].Clear(onException, this.m_CurrentFrameIndex);
			}
			this.m_RendererListResources.Clear();
			this.m_RendererListLegacyResources.Clear();
			this.m_ActiveRendererLists.Clear();
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x0003D5F8 File Offset: 0x0003B7F8
		internal void PurgeUnusedGraphicsResources()
		{
			for (int i = 0; i < 3; i++)
			{
				this.m_RenderGraphResources[i].PurgeUnusedGraphicsResources(this.m_CurrentFrameIndex);
			}
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x0003D624 File Offset: 0x0003B824
		internal void Cleanup()
		{
			for (int i = 0; i < 3; i++)
			{
				this.m_RenderGraphResources[i].Cleanup();
			}
			RTHandles.Release(this.m_CurrentBackbuffer);
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x0003D655 File Offset: 0x0003B855
		internal void FlushLogs()
		{
			Debug.Log(this.m_ResourceLogger.GetAllLogs());
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x0003D668 File Offset: 0x0003B868
		private void LogResources()
		{
			if (this.m_RenderGraphDebug.enableLogging)
			{
				this.m_ResourceLogger.LogLine("==== Allocated Resources ====\n", Array.Empty<object>());
				for (int type = 0; type < 3; type++)
				{
					if (this.m_RenderGraphResources[type].pool != null)
					{
						this.m_RenderGraphResources[type].pool.LogResources(this.m_ResourceLogger);
						this.m_ResourceLogger.LogLine("", Array.Empty<object>());
					}
				}
			}
		}

		// Token: 0x04000A91 RID: 2705
		private const int kSharedResourceLifetime = 30;

		// Token: 0x04000A92 RID: 2706
		private static RenderGraphResourceRegistry m_CurrentRegistry;

		// Token: 0x04000A93 RID: 2707
		private RenderGraphResourceRegistry.RenderGraphResourcesData[] m_RenderGraphResources = new RenderGraphResourceRegistry.RenderGraphResourcesData[3];

		// Token: 0x04000A94 RID: 2708
		private DynamicArray<RendererListResource> m_RendererListResources = new DynamicArray<RendererListResource>();

		// Token: 0x04000A95 RID: 2709
		private DynamicArray<RendererListLegacyResource> m_RendererListLegacyResources = new DynamicArray<RendererListLegacyResource>();

		// Token: 0x04000A96 RID: 2710
		private RenderGraphDebugParams m_RenderGraphDebug;

		// Token: 0x04000A97 RID: 2711
		private RenderGraphLogger m_ResourceLogger = new RenderGraphLogger();

		// Token: 0x04000A98 RID: 2712
		private RenderGraphLogger m_FrameInformationLogger;

		// Token: 0x04000A99 RID: 2713
		private int m_CurrentFrameIndex;

		// Token: 0x04000A9A RID: 2714
		private int m_ExecutionCount;

		// Token: 0x04000A9B RID: 2715
		private RTHandle m_CurrentBackbuffer;

		// Token: 0x04000A9C RID: 2716
		private const int kInitialRendererListCount = 256;

		// Token: 0x04000A9D RID: 2717
		private List<RendererList> m_ActiveRendererLists = new List<RendererList>(256);

		// Token: 0x04000A9E RID: 2718
		private static RenderTargetIdentifier emptyId = default(RenderTargetIdentifier);

		// Token: 0x04000A9F RID: 2719
		private static RenderTargetIdentifier builtinCameraRenderTarget = new RenderTargetIdentifier(BuiltinRenderTextureType.CameraTarget);

		// Token: 0x04000AA0 RID: 2720
		internal bool forceManualClearOfResource = true;

		// Token: 0x02000267 RID: 615
		// (Invoke) Token: 0x060010EB RID: 4331
		private delegate bool ResourceCreateCallback(InternalRenderGraphContext rgContext, IRenderGraphResource res);

		// Token: 0x02000268 RID: 616
		// (Invoke) Token: 0x060010EF RID: 4335
		private delegate void ResourceCallback(InternalRenderGraphContext rgContext, IRenderGraphResource res);

		// Token: 0x02000269 RID: 617
		private class RenderGraphResourcesData
		{
			// Token: 0x060010F2 RID: 4338 RVA: 0x0003D6F7 File Offset: 0x0003B8F7
			public RenderGraphResourcesData()
			{
				this.resourceArray.Resize(1, false);
			}

			// Token: 0x060010F3 RID: 4339 RVA: 0x0003D717 File Offset: 0x0003B917
			public void Clear(bool onException, int frameIndex)
			{
				this.resourceArray.Resize(this.sharedResourcesCount + 1, false);
				if (this.pool != null)
				{
					this.pool.CheckFrameAllocation(onException, frameIndex);
				}
			}

			// Token: 0x060010F4 RID: 4340 RVA: 0x0003D744 File Offset: 0x0003B944
			public unsafe void Cleanup()
			{
				for (int i = 1; i < this.sharedResourcesCount + 1; i++)
				{
					IRenderGraphResource resource = *this.resourceArray[i];
					if (resource != null)
					{
						resource.ReleaseGraphicsResource();
					}
				}
				if (this.pool != null)
				{
					this.pool.Cleanup();
				}
			}

			// Token: 0x060010F5 RID: 4341 RVA: 0x0003D78E File Offset: 0x0003B98E
			public void PurgeUnusedGraphicsResources(int frameIndex)
			{
				if (this.pool != null)
				{
					this.pool.PurgeUnusedResources(frameIndex);
				}
			}

			// Token: 0x060010F6 RID: 4342 RVA: 0x0003D7A4 File Offset: 0x0003B9A4
			public unsafe int AddNewRenderGraphResource<ResType>(out ResType outRes, bool pooledResource = true) where ResType : IRenderGraphResource, new()
			{
				int result = this.resourceArray.size;
				this.resourceArray.Resize(this.resourceArray.size + 1, true);
				if (*this.resourceArray[result] == null)
				{
					*this.resourceArray[result] = new ResType();
				}
				outRes = (*this.resourceArray[result]) as ResType;
				outRes.Reset(pooledResource ? this.pool : null);
				return result;
			}

			// Token: 0x04000AA1 RID: 2721
			public DynamicArray<IRenderGraphResource> resourceArray = new DynamicArray<IRenderGraphResource>();

			// Token: 0x04000AA2 RID: 2722
			public int sharedResourcesCount;

			// Token: 0x04000AA3 RID: 2723
			public IRenderGraphResourcePool pool;

			// Token: 0x04000AA4 RID: 2724
			public RenderGraphResourceRegistry.ResourceCreateCallback createResourceCallback;

			// Token: 0x04000AA5 RID: 2725
			public RenderGraphResourceRegistry.ResourceCallback releaseResourceCallback;
		}
	}
}
