using System;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000CB RID: 203
	public sealed class RawColorHistory : CameraHistoryItem
	{
		// Token: 0x0600052F RID: 1327 RVA: 0x00013553 File Offset: 0x00011753
		public override void OnCreate(BufferedRTHandleSystem owner, uint typeId)
		{
			base.OnCreate(owner, typeId);
			this.m_Ids[0] = base.MakeId(0U);
			this.m_Ids[1] = base.MakeId(1U);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0001357B File Offset: 0x0001177B
		public RTHandle GetCurrentTexture(int eyeIndex = 0)
		{
			if ((ulong)eyeIndex >= (ulong)((long)this.m_Ids.Length))
			{
				return null;
			}
			return base.GetCurrentFrameRT(this.m_Ids[eyeIndex]);
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0001359A File Offset: 0x0001179A
		public RTHandle GetPreviousTexture(int eyeIndex = 0)
		{
			if ((ulong)eyeIndex >= (ulong)((long)this.m_Ids.Length))
			{
				return null;
			}
			return base.GetPreviousFrameRT(this.m_Ids[eyeIndex]);
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x000135B9 File Offset: 0x000117B9
		private bool IsAllocated()
		{
			return this.GetCurrentTexture(0) != null;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x000135C5 File Offset: 0x000117C5
		private bool IsDirty(ref RenderTextureDescriptor desc)
		{
			return this.m_DescKey != Hash128.Compute<RenderTextureDescriptor>(ref desc);
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x000135D8 File Offset: 0x000117D8
		private void Alloc(ref RenderTextureDescriptor desc, bool xrMultipassEnabled)
		{
			base.AllocHistoryFrameRT(this.m_Ids[0], 2, ref desc, RawColorHistory.m_Names[0]);
			if (xrMultipassEnabled)
			{
				base.AllocHistoryFrameRT(this.m_Ids[1], 2, ref desc, RawColorHistory.m_Names[1]);
			}
			this.m_Descriptor = desc;
			this.m_DescKey = Hash128.Compute<RenderTextureDescriptor>(ref desc);
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00013630 File Offset: 0x00011830
		public override void Reset()
		{
			for (int i = 0; i < this.m_Ids.Length; i++)
			{
				base.ReleaseHistoryFrameRT(this.m_Ids[i]);
			}
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00013660 File Offset: 0x00011860
		internal RenderTextureDescriptor GetHistoryDescriptor(ref RenderTextureDescriptor cameraDesc)
		{
			RenderTextureDescriptor colorDesc = cameraDesc;
			colorDesc.depthStencilFormat = GraphicsFormat.None;
			colorDesc.mipCount = 0;
			colorDesc.msaaSamples = 1;
			return colorDesc;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00013690 File Offset: 0x00011890
		internal bool Update(ref RenderTextureDescriptor cameraDesc, bool xrMultipassEnabled = false)
		{
			if (cameraDesc.width > 0 && cameraDesc.height > 0 && cameraDesc.graphicsFormat != GraphicsFormat.None)
			{
				RenderTextureDescriptor historyDesc = this.GetHistoryDescriptor(ref cameraDesc);
				if (this.IsDirty(ref historyDesc))
				{
					this.Reset();
				}
				if (!this.IsAllocated())
				{
					this.Alloc(ref historyDesc, xrMultipassEnabled);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400048B RID: 1163
		private int[] m_Ids = new int[2];

		// Token: 0x0400048C RID: 1164
		private static readonly string[] m_Names = new string[] { "RawColorHistory0", "RawColorHistory1" };

		// Token: 0x0400048D RID: 1165
		private RenderTextureDescriptor m_Descriptor;

		// Token: 0x0400048E RID: 1166
		private Hash128 m_DescKey;
	}
}
