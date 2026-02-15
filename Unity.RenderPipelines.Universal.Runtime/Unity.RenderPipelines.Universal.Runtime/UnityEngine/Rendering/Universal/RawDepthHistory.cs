using System;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000CC RID: 204
	public sealed class RawDepthHistory : CameraHistoryItem
	{
		// Token: 0x0600053A RID: 1338 RVA: 0x00013714 File Offset: 0x00011914
		public override void OnCreate(BufferedRTHandleSystem owner, uint typeId)
		{
			base.OnCreate(owner, typeId);
			this.m_Ids[0] = base.MakeId(0U);
			this.m_Ids[1] = base.MakeId(1U);
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0001373C File Offset: 0x0001193C
		public RTHandle GetCurrentTexture(int eyeIndex = 0)
		{
			if ((ulong)eyeIndex >= (ulong)((long)this.m_Ids.Length))
			{
				return null;
			}
			return base.GetCurrentFrameRT(this.m_Ids[eyeIndex]);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0001375B File Offset: 0x0001195B
		public RTHandle GetPreviousTexture(int eyeIndex = 0)
		{
			if ((ulong)eyeIndex >= (ulong)((long)this.m_Ids.Length))
			{
				return null;
			}
			return base.GetPreviousFrameRT(this.m_Ids[eyeIndex]);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0001377A File Offset: 0x0001197A
		private bool IsAllocated()
		{
			return this.GetCurrentTexture(0) != null;
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00013786 File Offset: 0x00011986
		private bool IsDirty(ref RenderTextureDescriptor desc)
		{
			return this.m_DescKey != Hash128.Compute<RenderTextureDescriptor>(ref desc);
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0001379C File Offset: 0x0001199C
		private void Alloc(ref RenderTextureDescriptor desc, bool xrMultipassEnabled)
		{
			base.AllocHistoryFrameRT(this.m_Ids[0], 2, ref desc, RawDepthHistory.m_Names[0]);
			if (xrMultipassEnabled)
			{
				base.AllocHistoryFrameRT(this.m_Ids[1], 2, ref desc, RawDepthHistory.m_Names[1]);
			}
			this.m_Descriptor = desc;
			this.m_DescKey = Hash128.Compute<RenderTextureDescriptor>(ref desc);
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x000137F4 File Offset: 0x000119F4
		public override void Reset()
		{
			for (int i = 0; i < this.m_Ids.Length; i++)
			{
				base.ReleaseHistoryFrameRT(this.m_Ids[i]);
			}
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00013824 File Offset: 0x00011A24
		internal RenderTextureDescriptor GetHistoryDescriptor(ref RenderTextureDescriptor cameraDesc)
		{
			RenderTextureDescriptor depthDesc = cameraDesc;
			depthDesc.mipCount = 0;
			depthDesc.msaaSamples = 1;
			return depthDesc;
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0001384C File Offset: 0x00011A4C
		internal bool Update(ref RenderTextureDescriptor cameraDesc, bool xrMultipassEnabled)
		{
			if (cameraDesc.width > 0 && cameraDesc.height > 0 && (cameraDesc.depthStencilFormat != GraphicsFormat.None || cameraDesc.graphicsFormat != GraphicsFormat.None))
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

		// Token: 0x0400048F RID: 1167
		private int[] m_Ids = new int[2];

		// Token: 0x04000490 RID: 1168
		private static readonly string[] m_Names = new string[] { "RawDepthHistory0", "RawDepthHistory1" };

		// Token: 0x04000491 RID: 1169
		private RenderTextureDescriptor m_Descriptor;

		// Token: 0x04000492 RID: 1170
		private Hash128 m_DescKey;
	}
}
