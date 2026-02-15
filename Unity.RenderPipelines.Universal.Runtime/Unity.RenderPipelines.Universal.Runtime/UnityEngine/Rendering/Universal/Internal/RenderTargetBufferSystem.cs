using System;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.Universal.Internal
{
	// Token: 0x02000226 RID: 550
	internal sealed class RenderTargetBufferSystem
	{
		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000C38 RID: 3128 RVA: 0x000438AF File Offset: 0x00041AAF
		private ref RenderTargetBufferSystem.SwapBuffer backBuffer
		{
			get
			{
				if (!RenderTargetBufferSystem.m_AisBackBuffer)
				{
					return ref this.m_B;
				}
				return ref this.m_A;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x000438C5 File Offset: 0x00041AC5
		private ref RenderTargetBufferSystem.SwapBuffer frontBuffer
		{
			get
			{
				if (!RenderTargetBufferSystem.m_AisBackBuffer)
				{
					return ref this.m_A;
				}
				return ref this.m_B;
			}
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x000438DB File Offset: 0x00041ADB
		public RenderTargetBufferSystem(string name)
		{
			this.m_A.name = name + "A";
			this.m_B.name = name + "B";
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x00043918 File Offset: 0x00041B18
		public void Dispose()
		{
			RTHandle rtMSAA = this.m_A.rtMSAA;
			if (rtMSAA != null)
			{
				rtMSAA.Release();
			}
			RTHandle rtMSAA2 = this.m_B.rtMSAA;
			if (rtMSAA2 != null)
			{
				rtMSAA2.Release();
			}
			RTHandle rtResolve = this.m_A.rtResolve;
			if (rtResolve != null)
			{
				rtResolve.Release();
			}
			RTHandle rtResolve2 = this.m_B.rtResolve;
			if (rtResolve2 == null)
			{
				return;
			}
			rtResolve2.Release();
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x0004397C File Offset: 0x00041B7C
		public RTHandle PeekBackBuffer()
		{
			if (!this.m_AllowMSAA || this.backBuffer.msaa <= 1)
			{
				return this.backBuffer.rtResolve;
			}
			return this.backBuffer.rtMSAA;
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x000439AB File Offset: 0x00041BAB
		public RTHandle GetBackBuffer(CommandBuffer cmd)
		{
			this.ReAllocate(cmd);
			return this.PeekBackBuffer();
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x000439BC File Offset: 0x00041BBC
		public RTHandle GetFrontBuffer(CommandBuffer cmd)
		{
			if (!this.m_AllowMSAA && this.frontBuffer.msaa > 1)
			{
				this.frontBuffer.msaa = 1;
			}
			this.ReAllocate(cmd);
			if (!this.m_AllowMSAA || this.frontBuffer.msaa <= 1)
			{
				return this.frontBuffer.rtResolve;
			}
			return this.frontBuffer.rtMSAA;
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x00043A1F File Offset: 0x00041C1F
		public void Swap()
		{
			RenderTargetBufferSystem.m_AisBackBuffer = !RenderTargetBufferSystem.m_AisBackBuffer;
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x00043A30 File Offset: 0x00041C30
		private void ReAllocate(CommandBuffer cmd)
		{
			RenderTextureDescriptor desc = RenderTargetBufferSystem.m_Desc;
			desc.msaaSamples = this.m_A.msaa;
			if (desc.msaaSamples > 1)
			{
				RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_A.rtMSAA, in desc, this.m_FilterMode, TextureWrapMode.Clamp, 1, 0f, this.m_A.name);
			}
			desc.msaaSamples = this.m_B.msaa;
			if (desc.msaaSamples > 1)
			{
				RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_B.rtMSAA, in desc, this.m_FilterMode, TextureWrapMode.Clamp, 1, 0f, this.m_B.name);
			}
			desc.msaaSamples = 1;
			RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_A.rtResolve, in desc, this.m_FilterMode, TextureWrapMode.Clamp, 1, 0f, this.m_A.name);
			RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_B.rtResolve, in desc, this.m_FilterMode, TextureWrapMode.Clamp, 1, 0f, this.m_B.name);
			cmd.SetGlobalTexture(this.m_A.name, this.m_A.rtResolve);
			cmd.SetGlobalTexture(this.m_B.name, this.m_B.rtResolve);
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x00043B71 File Offset: 0x00041D71
		public void Clear()
		{
			RenderTargetBufferSystem.m_AisBackBuffer = true;
			this.m_AllowMSAA = this.m_A.msaa > 1 || this.m_B.msaa > 1;
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x00043BA0 File Offset: 0x00041DA0
		public void SetCameraSettings(RenderTextureDescriptor desc, FilterMode filterMode)
		{
			desc.depthStencilFormat = GraphicsFormat.None;
			RenderTargetBufferSystem.m_Desc = desc;
			this.m_FilterMode = filterMode;
			this.m_A.msaa = RenderTargetBufferSystem.m_Desc.msaaSamples;
			this.m_B.msaa = RenderTargetBufferSystem.m_Desc.msaaSamples;
			if (RenderTargetBufferSystem.m_Desc.msaaSamples > 1)
			{
				this.EnableMSAA(true);
			}
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x00043C00 File Offset: 0x00041E00
		public RTHandle GetBufferA()
		{
			if (!this.m_AllowMSAA || this.m_A.msaa <= 1)
			{
				return this.m_A.rtResolve;
			}
			return this.m_A.rtMSAA;
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x00043C2F File Offset: 0x00041E2F
		public void EnableMSAA(bool enable)
		{
			this.m_AllowMSAA = enable;
			if (enable)
			{
				this.m_A.msaa = RenderTargetBufferSystem.m_Desc.msaaSamples;
				this.m_B.msaa = RenderTargetBufferSystem.m_Desc.msaaSamples;
			}
		}

		// Token: 0x04000DDA RID: 3546
		private RenderTargetBufferSystem.SwapBuffer m_A;

		// Token: 0x04000DDB RID: 3547
		private RenderTargetBufferSystem.SwapBuffer m_B;

		// Token: 0x04000DDC RID: 3548
		private static bool m_AisBackBuffer = true;

		// Token: 0x04000DDD RID: 3549
		private static RenderTextureDescriptor m_Desc;

		// Token: 0x04000DDE RID: 3550
		private FilterMode m_FilterMode;

		// Token: 0x04000DDF RID: 3551
		private bool m_AllowMSAA = true;

		// Token: 0x02000227 RID: 551
		private struct SwapBuffer
		{
			// Token: 0x04000DE0 RID: 3552
			public RTHandle rtMSAA;

			// Token: 0x04000DE1 RID: 3553
			public RTHandle rtResolve;

			// Token: 0x04000DE2 RID: 3554
			public string name;

			// Token: 0x04000DE3 RID: 3555
			public int msaa;
		}
	}
}
