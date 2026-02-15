using System;
using System.Diagnostics;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering
{
	// Token: 0x0200001E RID: 30
	public class BaseCommandBuffer
	{
		// Token: 0x060000BF RID: 191 RVA: 0x000051CA File Offset: 0x000033CA
		internal BaseCommandBuffer(CommandBuffer wrapped, RenderGraphPass executingPass, bool isAsync)
		{
			this.m_WrappedCommandBuffer = wrapped;
			this.m_ExecutingPass = executingPass;
			if (isAsync)
			{
				this.m_WrappedCommandBuffer.SetExecutionFlags(CommandBufferExecutionFlags.AsyncCompute);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x000051EF File Offset: 0x000033EF
		public string name
		{
			get
			{
				return this.m_WrappedCommandBuffer.name;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x000051FC File Offset: 0x000033FC
		public int sizeInBytes
		{
			get
			{
				return this.m_WrappedCommandBuffer.sizeInBytes;
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00005209 File Offset: 0x00003409
		[Conditional("DEVELOPMENT_BUILD")]
		[Conditional("UNITY_EDITOR")]
		protected internal void ThrowIfGlobalStateNotAllowed()
		{
			if (this.m_ExecutingPass != null && !this.m_ExecutingPass.allowGlobalState)
			{
				throw new InvalidOperationException(this.m_ExecutingPass.name + ": Modifying global state from this command buffer is not allowed. Please ensure your render graph pass allows modifying global state.");
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000523B File Offset: 0x0000343B
		[Conditional("DEVELOPMENT_BUILD")]
		[Conditional("UNITY_EDITOR")]
		protected internal void ThrowIfRasterNotAllowed()
		{
			if (this.m_ExecutingPass != null && !this.m_ExecutingPass.HasRenderAttachments())
			{
				throw new InvalidOperationException(this.m_ExecutingPass.name + ": Using raster commands from a pass with no active render targets is not allowed as it will use an undefined render target state. Please set-up the pass's render targets using SetRenderAttachments.");
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00005270 File Offset: 0x00003470
		[Conditional("DEVELOPMENT_BUILD")]
		[Conditional("UNITY_EDITOR")]
		protected internal void ValidateTextureHandle(TextureHandle h)
		{
			if (RenderGraph.enableValidityChecks)
			{
				if (this.m_ExecutingPass == null)
				{
					return;
				}
				if (h.IsBuiltin())
				{
					return;
				}
				if (!this.m_ExecutingPass.IsRead(in h.handle) && !this.m_ExecutingPass.IsWritten(in h.handle))
				{
					throw new Exception("Pass '" + this.m_ExecutingPass.name + "' is trying to use a texture on the command buffer that was never registered with the pass builder. Please indicate the texture use to the pass builder.");
				}
				if (this.m_ExecutingPass.IsAttachment(in h))
				{
					throw new Exception("Pass '" + this.m_ExecutingPass.name + "' is using a texture as a fragment attachment (SetRenderAttachment/SetRenderAttachmentDepth) but is also trying to bind it as regular texture. Please fix this pass. ");
				}
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00005314 File Offset: 0x00003514
		[Conditional("DEVELOPMENT_BUILD")]
		[Conditional("UNITY_EDITOR")]
		protected internal void ValidateTextureHandleRead(TextureHandle h)
		{
			if (RenderGraph.enableValidityChecks)
			{
				if (this.m_ExecutingPass == null)
				{
					return;
				}
				if (!this.m_ExecutingPass.IsRead(in h.handle))
				{
					throw new Exception("Pass '" + this.m_ExecutingPass.name + "' is trying to read a texture on the command buffer that was never registered with the pass builder. Please indicate the texture as read to the pass builder.");
				}
				if (this.m_ExecutingPass.IsAttachment(in h))
				{
					throw new Exception("Pass '" + this.m_ExecutingPass.name + "' is using a texture as a fragment attachment (SetRenderAttachment/SetRenderAttachmentDepth) but is also trying to bind it as regular texture. Please fix this pass. ");
				}
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00005394 File Offset: 0x00003594
		[Conditional("DEVELOPMENT_BUILD")]
		[Conditional("UNITY_EDITOR")]
		protected internal void ValidateTextureHandleWrite(TextureHandle h)
		{
			if (RenderGraph.enableValidityChecks)
			{
				if (this.m_ExecutingPass == null)
				{
					return;
				}
				if (h.IsBuiltin())
				{
					throw new Exception("Pass '" + this.m_ExecutingPass.name + "' is trying to write to a built-in texture. This is not allowed built-in textures are small default resources like `white` or `black` that cannot be written to.");
				}
				if (!this.m_ExecutingPass.IsWritten(in h.handle))
				{
					throw new Exception("Pass '" + this.m_ExecutingPass.name + "' is trying to write a texture on the command buffer that was never registered with the pass builder. Please indicate the texture as written to the pass builder.");
				}
				if (this.m_ExecutingPass.IsAttachment(in h))
				{
					throw new Exception("Pass '" + this.m_ExecutingPass.name + "' is using a texture as a fragment attachment (SetRenderAttachment/SetRenderAttachmentDepth) but is also trying to bind it as regular texture. Please fix this pass. ");
				}
			}
		}

		// Token: 0x04000099 RID: 153
		protected internal CommandBuffer m_WrappedCommandBuffer;

		// Token: 0x0400009A RID: 154
		internal RenderGraphPass m_ExecutingPass;
	}
}
