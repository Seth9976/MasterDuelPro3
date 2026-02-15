using System;
using System.Diagnostics;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x02000247 RID: 583
	[MovedFrom(true, "UnityEngine.Experimental.Rendering.RenderGraphModule", "UnityEngine.Rendering.RenderGraphModule", null)]
	public struct RenderGraphBuilder : IDisposable
	{
		// Token: 0x06000F90 RID: 3984 RVA: 0x00038FAE File Offset: 0x000371AE
		public TextureHandle UseColorBuffer(in TextureHandle input, int index)
		{
			this.m_Resources.IncrementWriteCount(in input.handle);
			this.m_RenderPass.SetColorBuffer(in input, index);
			return input;
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x00038FD4 File Offset: 0x000371D4
		public TextureHandle UseDepthBuffer(in TextureHandle input, DepthAccess flags)
		{
			if ((flags & DepthAccess.Write) != (DepthAccess)0)
			{
				this.m_Resources.IncrementWriteCount(in input.handle);
			}
			if ((flags & DepthAccess.Read) != (DepthAccess)0 && !this.m_Resources.IsRenderGraphResourceImported(in input.handle) && this.m_Resources.TextureNeedsFallback(in input))
			{
				this.WriteTexture(in input);
			}
			this.m_RenderPass.SetDepthBuffer(in input, flags);
			return input;
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x00039038 File Offset: 0x00037238
		public TextureHandle ReadTexture(in TextureHandle input)
		{
			if (!this.m_Resources.IsRenderGraphResourceImported(in input.handle) && this.m_Resources.TextureNeedsFallback(in input))
			{
				TextureResource textureResource = this.m_Resources.GetTextureResource(in input.handle);
				textureResource.desc.clearBuffer = true;
				textureResource.desc.clearColor = Color.black;
				TextureHandle fallback;
				if (this.m_RenderGraph.GetImportedFallback(textureResource.desc, out fallback))
				{
					return fallback;
				}
				this.WriteTexture(in input);
			}
			this.m_RenderPass.AddResourceRead(in input.handle);
			return input;
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x000390CA File Offset: 0x000372CA
		public TextureHandle WriteTexture(in TextureHandle input)
		{
			this.m_Resources.IncrementWriteCount(in input.handle);
			this.m_RenderPass.AddResourceWrite(in input.handle);
			return input;
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x000390F4 File Offset: 0x000372F4
		public TextureHandle ReadWriteTexture(in TextureHandle input)
		{
			this.m_Resources.IncrementWriteCount(in input.handle);
			this.m_RenderPass.AddResourceWrite(in input.handle);
			this.m_RenderPass.AddResourceRead(in input.handle);
			return input;
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x00039130 File Offset: 0x00037330
		public TextureHandle CreateTransientTexture(in TextureDesc desc)
		{
			TextureHandle result = this.m_Resources.CreateTexture(in desc, this.m_RenderPass.index);
			this.m_RenderPass.AddTransientResource(in result.handle);
			return result;
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x00039168 File Offset: 0x00037368
		public TextureHandle CreateTransientTexture(in TextureHandle texture)
		{
			TextureDesc desc = this.m_Resources.GetTextureResourceDesc(in texture.handle, false);
			TextureHandle result = this.m_Resources.CreateTexture(in desc, this.m_RenderPass.index);
			this.m_RenderPass.AddTransientResource(in result.handle);
			return result;
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x000391B4 File Offset: 0x000373B4
		public RayTracingAccelerationStructureHandle WriteRayTracingAccelerationStructure(in RayTracingAccelerationStructureHandle input)
		{
			this.m_Resources.IncrementWriteCount(in input.handle);
			this.m_RenderPass.AddResourceWrite(in input.handle);
			return input;
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x000391DE File Offset: 0x000373DE
		public RayTracingAccelerationStructureHandle ReadRayTracingAccelerationStructure(in RayTracingAccelerationStructureHandle input)
		{
			this.m_RenderPass.AddResourceRead(in input.handle);
			return input;
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x000391F8 File Offset: 0x000373F8
		public RendererListHandle UseRendererList(in RendererListHandle input)
		{
			RendererListHandle rendererListHandle = input;
			if (rendererListHandle.IsValid())
			{
				this.m_RenderPass.UseRendererList(in input);
			}
			return input;
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x00039227 File Offset: 0x00037427
		public BufferHandle ReadBuffer(in BufferHandle input)
		{
			this.m_RenderPass.AddResourceRead(in input.handle);
			return input;
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x00039240 File Offset: 0x00037440
		public BufferHandle WriteBuffer(in BufferHandle input)
		{
			this.m_RenderPass.AddResourceWrite(in input.handle);
			this.m_Resources.IncrementWriteCount(in input.handle);
			return input;
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x0003926C File Offset: 0x0003746C
		public BufferHandle CreateTransientBuffer(in BufferDesc desc)
		{
			BufferHandle result = this.m_Resources.CreateBuffer(in desc, this.m_RenderPass.index);
			this.m_RenderPass.AddTransientResource(in result.handle);
			return result;
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x000392A4 File Offset: 0x000374A4
		public BufferHandle CreateTransientBuffer(in BufferHandle graphicsbuffer)
		{
			BufferDesc desc = this.m_Resources.GetBufferResourceDesc(in graphicsbuffer.handle, false);
			BufferHandle result = this.m_Resources.CreateBuffer(in desc, this.m_RenderPass.index);
			this.m_RenderPass.AddTransientResource(in result.handle);
			return result;
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x000392F0 File Offset: 0x000374F0
		public void SetRenderFunc<PassData>(BaseRenderFunc<PassData, RenderGraphContext> renderFunc) where PassData : class, new()
		{
			((RenderGraphPass<PassData>)this.m_RenderPass).renderFunc = renderFunc;
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x00039303 File Offset: 0x00037503
		public void EnableAsyncCompute(bool value)
		{
			this.m_RenderPass.EnableAsyncCompute(value);
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x00039311 File Offset: 0x00037511
		public void AllowPassCulling(bool value)
		{
			this.m_RenderPass.AllowPassCulling(value);
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x0003931F File Offset: 0x0003751F
		public void EnableFoveatedRasterization(bool value)
		{
			this.m_RenderPass.EnableFoveatedRasterization(value);
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x0003932D File Offset: 0x0003752D
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x00039336 File Offset: 0x00037536
		public void AllowRendererListCulling(bool value)
		{
			this.m_RenderPass.AllowRendererListCulling(value);
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x00039344 File Offset: 0x00037544
		public RendererListHandle DependsOn(in RendererListHandle input)
		{
			this.m_RenderPass.UseRendererList(in input);
			return input;
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x00039358 File Offset: 0x00037558
		internal RenderGraphBuilder(RenderGraphPass renderPass, RenderGraphResourceRegistry resources, RenderGraph renderGraph)
		{
			this.m_RenderPass = renderPass;
			this.m_Resources = resources;
			this.m_RenderGraph = renderGraph;
			this.m_Disposed = false;
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x00039376 File Offset: 0x00037576
		private void Dispose(bool disposing)
		{
			if (this.m_Disposed)
			{
				return;
			}
			this.m_RenderGraph.OnPassAdded(this.m_RenderPass);
			this.m_Disposed = true;
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x0003939C File Offset: 0x0003759C
		[Conditional("DEVELOPMENT_BUILD")]
		[Conditional("UNITY_EDITOR")]
		private void CheckResource(in ResourceHandle res, bool checkTransientReadWrite = true)
		{
			if (RenderGraph.enableValidityChecks)
			{
				ResourceHandle resourceHandle = res;
				if (!resourceHandle.IsValid())
				{
					throw new ArgumentException("Trying to use an invalid resource (pass " + this.m_RenderPass.name + ").");
				}
				int transientIndex = this.m_Resources.GetRenderGraphResourceTransientIndex(in res);
				if (transientIndex == this.m_RenderPass.index && checkTransientReadWrite)
				{
					Debug.LogError("Trying to read or write a transient resource at pass " + this.m_RenderPass.name + ".Transient resource are always assumed to be both read and written.");
				}
				if (transientIndex != -1 && transientIndex != this.m_RenderPass.index)
				{
					throw new ArgumentException(string.Format("Trying to use a transient texture (pass index {0}) in a different pass (pass index {1}).", transientIndex, this.m_RenderPass.index));
				}
			}
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x00039459 File Offset: 0x00037659
		internal void GenerateDebugData(bool value)
		{
			this.m_RenderPass.GenerateDebugData(value);
		}

		// Token: 0x04000A2B RID: 2603
		private RenderGraphPass m_RenderPass;

		// Token: 0x04000A2C RID: 2604
		private RenderGraphResourceRegistry m_Resources;

		// Token: 0x04000A2D RID: 2605
		private RenderGraph m_RenderGraph;

		// Token: 0x04000A2E RID: 2606
		private bool m_Disposed;
	}
}
