using System;
using System.Diagnostics;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x02000248 RID: 584
	internal class RenderGraphBuilders : IBaseRenderGraphBuilder, IDisposable, IComputeRenderGraphBuilder, IRasterRenderGraphBuilder, IUnsafeRenderGraphBuilder
	{
		// Token: 0x06000FA9 RID: 4009 RVA: 0x00039467 File Offset: 0x00037667
		public RenderGraphBuilders()
		{
			this.m_RenderPass = null;
			this.m_Resources = null;
			this.m_RenderGraph = null;
			this.m_Disposed = true;
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x0003948B File Offset: 0x0003768B
		public void Setup(RenderGraphPass renderPass, RenderGraphResourceRegistry resources, RenderGraph renderGraph)
		{
			this.m_RenderPass = renderPass;
			this.m_Resources = resources;
			this.m_RenderGraph = renderGraph;
			this.m_Disposed = false;
			renderPass.useAllGlobalTextures = false;
			if (renderPass.type == RenderGraphPassType.Raster)
			{
				CommandBuffer.ThrowOnSetRenderTarget = true;
			}
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x000394BF File Offset: 0x000376BF
		public void EnableAsyncCompute(bool value)
		{
			this.m_RenderPass.EnableAsyncCompute(value);
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x000394CD File Offset: 0x000376CD
		public void AllowPassCulling(bool value)
		{
			if (value && this.m_RenderPass.allowGlobalState)
			{
				return;
			}
			this.m_RenderPass.AllowPassCulling(value);
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x000394EC File Offset: 0x000376EC
		public void AllowGlobalStateModification(bool value)
		{
			this.m_RenderPass.AllowGlobalState(value);
			if (value)
			{
				this.AllowPassCulling(false);
			}
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x00039504 File Offset: 0x00037704
		public void EnableFoveatedRasterization(bool value)
		{
			this.m_RenderPass.EnableFoveatedRasterization(value);
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x00039514 File Offset: 0x00037714
		public BufferHandle CreateTransientBuffer(in BufferDesc desc)
		{
			BufferHandle result = this.m_Resources.CreateBuffer(in desc, this.m_RenderPass.index);
			this.UseResource(in result.handle, AccessFlags.ReadWrite, true);
			return result;
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x0003954C File Offset: 0x0003774C
		public BufferHandle CreateTransientBuffer(in BufferHandle computebuffer)
		{
			BufferDesc desc = this.m_Resources.GetBufferResourceDesc(in computebuffer.handle, false);
			return this.CreateTransientBuffer(in desc);
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x00039574 File Offset: 0x00037774
		public TextureHandle CreateTransientTexture(in TextureDesc desc)
		{
			TextureHandle result = this.m_Resources.CreateTexture(in desc, this.m_RenderPass.index);
			this.UseResource(in result.handle, AccessFlags.ReadWrite, true);
			return result;
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x000395AC File Offset: 0x000377AC
		public TextureHandle CreateTransientTexture(in TextureHandle texture)
		{
			TextureDesc desc = this.m_Resources.GetTextureResourceDesc(in texture.handle, false);
			return this.CreateTransientTexture(in desc);
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x000395D4 File Offset: 0x000377D4
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x000395E0 File Offset: 0x000377E0
		protected virtual void Dispose(bool disposing)
		{
			if (this.m_Disposed)
			{
				return;
			}
			try
			{
				if (disposing)
				{
					if (this.m_RenderPass.useAllGlobalTextures)
					{
						foreach (TextureHandle texture in this.m_RenderGraph.AllGlobals())
						{
							this.UseTexture(in texture, AccessFlags.Read);
						}
					}
					foreach (ValueTuple<TextureHandle, int> t in this.m_RenderPass.setGlobalsList)
					{
						this.m_RenderGraph.SetGlobal(t.Item1, t.Item2);
					}
					this.m_RenderGraph.OnPassAdded(this.m_RenderPass);
				}
			}
			finally
			{
				if (this.m_RenderPass.type == RenderGraphPassType.Raster)
				{
					CommandBuffer.ThrowOnSetRenderTarget = false;
				}
				this.m_RenderPass = null;
				this.m_Resources = null;
				this.m_RenderGraph = null;
				this.m_Disposed = true;
			}
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x00039700 File Offset: 0x00037900
		[Conditional("DEVELOPMENT_BUILD")]
		[Conditional("UNITY_EDITOR")]
		private void ValidateWriteTo(in ResourceHandle handle)
		{
			if (RenderGraph.enableValidityChecks)
			{
				ResourceHandle resourceHandle = handle;
				if (resourceHandle.IsVersioned)
				{
					string name = this.m_Resources.GetRenderGraphResourceName(in handle);
					throw new InvalidOperationException(string.Concat(new string[]
					{
						"Trying to write to a versioned resource handle. You can only write to unversioned resource handles to avoid branches in the resource history. (pass ",
						this.m_RenderPass.name,
						" resource",
						name,
						")."
					}));
				}
				if (this.m_RenderPass.IsWritten(in handle))
				{
					string name2 = this.m_Resources.GetRenderGraphResourceName(in handle);
					throw new InvalidOperationException(string.Concat(new string[]
					{
						"Trying to write a resource twice in a pass. You can only write the same resource once within a pass (pass ",
						this.m_RenderPass.name,
						" resource",
						name2,
						")."
					}));
				}
			}
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x000397C8 File Offset: 0x000379C8
		private ResourceHandle UseResource(in ResourceHandle handle, AccessFlags flags, bool isTransient = false)
		{
			if ((flags & AccessFlags.Discard) == AccessFlags.None)
			{
				ResourceHandle resourceHandle = handle;
				ResourceHandle versioned;
				if (!resourceHandle.IsVersioned)
				{
					versioned = this.m_Resources.GetLatestVersionHandle(in handle);
				}
				else
				{
					versioned = handle;
				}
				if (isTransient)
				{
					this.m_RenderPass.AddTransientResource(in versioned);
					return this.GetLatestVersionHandle(in handle);
				}
				this.m_RenderPass.AddResourceRead(in versioned);
				if ((flags & AccessFlags.Read) == AccessFlags.None)
				{
					this.m_RenderPass.implicitReadsList.Add(versioned);
				}
			}
			else if ((flags & AccessFlags.Read) != AccessFlags.None)
			{
				RenderGraphPass renderPass = this.m_RenderPass;
				ResourceHandle resourceHandle = this.m_Resources.GetZeroVersionedHandle(in handle);
				renderPass.AddResourceRead(in resourceHandle);
			}
			if ((flags & AccessFlags.Write) != AccessFlags.None)
			{
				RenderGraphPass renderPass2 = this.m_RenderPass;
				ResourceHandle resourceHandle = this.m_Resources.GetNewVersionedHandle(in handle);
				renderPass2.AddResourceWrite(in resourceHandle);
				this.m_Resources.IncrementWriteCount(in handle);
			}
			return this.GetLatestVersionHandle(in handle);
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x0003988E File Offset: 0x00037A8E
		public BufferHandle UseBuffer(in BufferHandle input, AccessFlags flags)
		{
			this.UseResource(in input.handle, flags, false);
			return input;
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x000398A8 File Offset: 0x00037AA8
		[Conditional("DEVELOPMENT_BUILD")]
		[Conditional("UNITY_EDITOR")]
		private void CheckNotUseFragment(TextureHandle tex)
		{
			if (RenderGraph.enableValidityChecks)
			{
				TextureAccess textureAccess = this.m_RenderPass.depthAccess;
				bool flag;
				if (textureAccess.textureHandle.IsValid())
				{
					textureAccess = this.m_RenderPass.depthAccess;
					flag = textureAccess.textureHandle.handle.index == tex.handle.index;
				}
				else
				{
					flag = false;
				}
				bool usedAsFragment = flag;
				if (!usedAsFragment)
				{
					for (int i = 0; i <= this.m_RenderPass.colorBufferMaxIndex; i++)
					{
						if (this.m_RenderPass.colorBufferAccess[i].textureHandle.IsValid() && this.m_RenderPass.colorBufferAccess[i].textureHandle.handle.index == tex.handle.index)
						{
							usedAsFragment = true;
							break;
						}
					}
				}
				if (usedAsFragment)
				{
					string name = this.m_Resources.GetRenderGraphResourceName(in tex.handle);
					throw new ArgumentException(string.Concat(new string[]
					{
						"Trying to UseTexture on a texture that is already used through SetRenderAttachment. Consider updating your code. (pass ",
						this.m_RenderPass.name,
						" resource",
						name,
						")."
					}));
				}
			}
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x000399C3 File Offset: 0x00037BC3
		public void UseTexture(in TextureHandle input, AccessFlags flags)
		{
			this.UseResource(in input.handle, flags, false);
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x000399D4 File Offset: 0x00037BD4
		public void UseGlobalTexture(int propertyId, AccessFlags flags)
		{
			TextureHandle h = this.m_RenderGraph.GetGlobal(propertyId);
			if (h.IsValid())
			{
				this.UseTexture(in h, flags);
				return;
			}
			throw new ArgumentException(string.Format("Trying to read global texture property {0} but no previous pass in the graph assigned a value to this global.", propertyId));
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x00039A16 File Offset: 0x00037C16
		public void UseAllGlobalTextures(bool enable)
		{
			this.m_RenderPass.useAllGlobalTextures = enable;
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x00039A24 File Offset: 0x00037C24
		public void SetGlobalTextureAfterPass(in TextureHandle input, int propertyId)
		{
			this.m_RenderPass.setGlobalsList.Add(ValueTuple.Create<TextureHandle, int>(input, propertyId));
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x00039A44 File Offset: 0x00037C44
		[Conditional("DEVELOPMENT_BUILD")]
		[Conditional("UNITY_EDITOR")]
		private void CheckUseFragment(TextureHandle tex, bool isDepth)
		{
			if (RenderGraph.enableValidityChecks)
			{
				bool alreadyUsed = false;
				for (int i = 0; i < this.m_RenderPass.resourceReadLists[tex.handle.iType].Count; i++)
				{
					if (this.m_RenderPass.resourceReadLists[tex.handle.iType][i].index == tex.handle.index)
					{
						alreadyUsed = true;
						break;
					}
				}
				for (int j = 0; j < this.m_RenderPass.resourceWriteLists[tex.handle.iType].Count; j++)
				{
					if (this.m_RenderPass.resourceWriteLists[tex.handle.iType][j].index == tex.handle.index)
					{
						alreadyUsed = true;
						break;
					}
				}
				if (alreadyUsed)
				{
					string name = this.m_Resources.GetRenderGraphResourceName(in tex.handle);
					throw new InvalidOperationException(string.Concat(new string[]
					{
						"Trying to SetRenderAttachment on a texture that is already used through UseTexture/SetRenderAttachment. Consider updating your code. (pass '",
						this.m_RenderPass.name,
						"' resource '",
						name,
						"')."
					}));
				}
				RenderTargetInfo info;
				this.m_Resources.GetRenderTargetInfo(in tex.handle, out info);
				if (this.m_RenderGraph.nativeRenderPassesEnabled)
				{
					if (isDepth)
					{
						if (!GraphicsFormatUtility.IsDepthFormat(info.format))
						{
							string name2 = this.m_Resources.GetRenderGraphResourceName(in tex.handle);
							throw new InvalidOperationException(string.Format("Trying to SetRenderAttachmentDepth on a texture that has a color format {0}. Use a texture with a depth format instead. (pass '{1}' resource '{2}').", info.format, this.m_RenderPass.name, name2));
						}
					}
					else if (GraphicsFormatUtility.IsDepthFormat(info.format))
					{
						string name3 = this.m_Resources.GetRenderGraphResourceName(in tex.handle);
						throw new InvalidOperationException(string.Concat(new string[]
						{
							"Trying to SetRenderAttachment on a texture that has a depth format. Use a texture with a color format instead. (pass '",
							this.m_RenderPass.name,
							"' resource '",
							name3,
							"')."
						}));
					}
				}
				foreach (ValueTuple<TextureHandle, int> globalTex in this.m_RenderPass.setGlobalsList)
				{
					ResourceHandle handle = globalTex.Item1.handle;
					if (handle.index == tex.handle.index)
					{
						throw new InvalidOperationException("Trying to SetRenderAttachment on a texture that is currently set on a global texture slot. Shaders might be using the texture using samplers. You should ensure textures are not set as globals when using them as fragment attachments.");
					}
				}
			}
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x00039CB8 File Offset: 0x00037EB8
		public void SetRenderAttachment(TextureHandle tex, int index, AccessFlags flags, int mipLevel, int depthSlice)
		{
			ResourceHandle result = this.UseResource(in tex.handle, flags, false);
			TextureHandle th = default(TextureHandle);
			th.handle = result;
			this.m_RenderPass.SetColorBufferRaw(in th, index, flags, mipLevel, depthSlice);
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x00039CF8 File Offset: 0x00037EF8
		public void SetInputAttachment(TextureHandle tex, int index, AccessFlags flags, int mipLevel, int depthSlice)
		{
			ResourceHandle result = this.UseResource(in tex.handle, flags, false);
			TextureHandle th = default(TextureHandle);
			th.handle = result;
			this.m_RenderPass.SetFragmentInputRaw(in th, index, flags, mipLevel, depthSlice);
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x00039D38 File Offset: 0x00037F38
		public void SetRenderAttachmentDepth(TextureHandle tex, AccessFlags flags, int mipLevel, int depthSlice)
		{
			ResourceHandle result = this.UseResource(in tex.handle, flags, false);
			TextureHandle th = default(TextureHandle);
			th.handle = result;
			this.m_RenderPass.SetDepthBufferRaw(in th, flags, mipLevel, depthSlice);
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x00039D78 File Offset: 0x00037F78
		public TextureHandle SetRandomAccessAttachment(TextureHandle input, int index, AccessFlags flags = AccessFlags.Read)
		{
			ResourceHandle result = this.UseResource(in input.handle, flags, false);
			TextureHandle th = default(TextureHandle);
			th.handle = result;
			this.m_RenderPass.SetRandomWriteResourceRaw(in th.handle, index, false, flags);
			return input;
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x00039DBC File Offset: 0x00037FBC
		public BufferHandle UseBufferRandomAccess(BufferHandle input, int index, AccessFlags flags = AccessFlags.Read)
		{
			BufferHandle h = this.UseBuffer(in input, flags);
			this.m_RenderPass.SetRandomWriteResourceRaw(in h.handle, index, true, flags);
			return input;
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x00039DEC File Offset: 0x00037FEC
		public BufferHandle UseBufferRandomAccess(BufferHandle input, int index, bool preserveCounterValue, AccessFlags flags = AccessFlags.Read)
		{
			BufferHandle h = this.UseBuffer(in input, flags);
			this.m_RenderPass.SetRandomWriteResourceRaw(in h.handle, index, preserveCounterValue, flags);
			return input;
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x00039E1B File Offset: 0x0003801B
		public void SetRenderFunc<PassData>(BaseRenderFunc<PassData, ComputeGraphContext> renderFunc) where PassData : class, new()
		{
			((ComputeRenderGraphPass<PassData>)this.m_RenderPass).renderFunc = renderFunc;
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x00039E2E File Offset: 0x0003802E
		public void SetRenderFunc<PassData>(BaseRenderFunc<PassData, RasterGraphContext> renderFunc) where PassData : class, new()
		{
			((RasterRenderGraphPass<PassData>)this.m_RenderPass).renderFunc = renderFunc;
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x00039E41 File Offset: 0x00038041
		public void SetRenderFunc<PassData>(BaseRenderFunc<PassData, UnsafeGraphContext> renderFunc) where PassData : class, new()
		{
			((UnsafeRenderGraphPass<PassData>)this.m_RenderPass).renderFunc = renderFunc;
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x00039E54 File Offset: 0x00038054
		public void UseRendererList(in RendererListHandle input)
		{
			this.m_RenderPass.UseRendererList(in input);
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x00039E62 File Offset: 0x00038062
		private ResourceHandle GetLatestVersionHandle(in ResourceHandle handle)
		{
			if (this.m_Resources.GetRenderGraphResourceTransientIndex(in handle) >= 0)
			{
				return handle;
			}
			return this.m_Resources.GetLatestVersionHandle(in handle);
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x00039E88 File Offset: 0x00038088
		[Conditional("DEVELOPMENT_BUILD")]
		[Conditional("UNITY_EDITOR")]
		private void CheckResource(in ResourceHandle res, bool checkTransientReadWrite = false)
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
					throw new ArgumentException(string.Format("Trying to use a transient {0} (pass index {1}) in a different pass (pass index {2}).", res.type, transientIndex, this.m_RenderPass.index));
				}
			}
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x00039F53 File Offset: 0x00038153
		void IBaseRenderGraphBuilder.UseTexture(in TextureHandle input, AccessFlags flags)
		{
			this.UseTexture(in input, flags);
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x00039F5D File Offset: 0x0003815D
		void IBaseRenderGraphBuilder.SetGlobalTextureAfterPass(in TextureHandle input, int propertyId)
		{
			this.SetGlobalTextureAfterPass(in input, propertyId);
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x00039F67 File Offset: 0x00038167
		BufferHandle IBaseRenderGraphBuilder.UseBuffer(in BufferHandle input, AccessFlags flags)
		{
			return this.UseBuffer(in input, flags);
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x00039F71 File Offset: 0x00038171
		TextureHandle IBaseRenderGraphBuilder.CreateTransientTexture(in TextureDesc desc)
		{
			return this.CreateTransientTexture(in desc);
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x00039F7A File Offset: 0x0003817A
		TextureHandle IBaseRenderGraphBuilder.CreateTransientTexture(in TextureHandle texture)
		{
			return this.CreateTransientTexture(in texture);
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x00039F83 File Offset: 0x00038183
		BufferHandle IBaseRenderGraphBuilder.CreateTransientBuffer(in BufferDesc desc)
		{
			return this.CreateTransientBuffer(in desc);
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x00039F8C File Offset: 0x0003818C
		BufferHandle IBaseRenderGraphBuilder.CreateTransientBuffer(in BufferHandle computebuffer)
		{
			return this.CreateTransientBuffer(in computebuffer);
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x00039F95 File Offset: 0x00038195
		void IBaseRenderGraphBuilder.UseRendererList(in RendererListHandle input)
		{
			this.UseRendererList(in input);
		}

		// Token: 0x04000A2F RID: 2607
		private RenderGraphPass m_RenderPass;

		// Token: 0x04000A30 RID: 2608
		private RenderGraphResourceRegistry m_Resources;

		// Token: 0x04000A31 RID: 2609
		private RenderGraph m_RenderGraph;

		// Token: 0x04000A32 RID: 2610
		private bool m_Disposed;
	}
}
