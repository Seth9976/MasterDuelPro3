using System;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000CD RID: 205
	internal abstract class SingleHistoryBase : CameraHistoryItem
	{
		// Token: 0x06000545 RID: 1349 RVA: 0x000138D8 File Offset: 0x00011AD8
		public override void OnCreate(BufferedRTHandleSystem owner, uint typeId)
		{
			base.OnCreate(owner, typeId);
			this.m_Id = base.MakeId(0U);
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x000138EF File Offset: 0x00011AEF
		public RTHandle GetTexture(int frameIndex = 0)
		{
			if ((ulong)frameIndex >= (ulong)((long)this.GetHistoryFrameCount()))
			{
				return null;
			}
			return base.storage.GetFrameRT(this.m_Id, frameIndex);
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00013910 File Offset: 0x00011B10
		public RTHandle GetCurrentTexture()
		{
			return base.GetCurrentFrameRT(this.m_Id);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0001391E File Offset: 0x00011B1E
		public RTHandle GetPreviousTexture()
		{
			return this.GetTexture(1);
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00013927 File Offset: 0x00011B27
		internal bool IsAllocated()
		{
			return this.GetTexture(0) != null;
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00013933 File Offset: 0x00011B33
		internal bool IsDirty(ref RenderTextureDescriptor desc)
		{
			return this.m_DescKey != Hash128.Compute<RenderTextureDescriptor>(ref desc);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00013946 File Offset: 0x00011B46
		private void Alloc(ref RenderTextureDescriptor desc)
		{
			base.AllocHistoryFrameRT(this.m_Id, this.GetHistoryFrameCount(), ref desc, this.GetHistoryName());
			this.m_Descriptor = desc;
			this.m_DescKey = Hash128.Compute<RenderTextureDescriptor>(ref desc);
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0001397A File Offset: 0x00011B7A
		public override void Reset()
		{
			base.ReleaseHistoryFrameRT(this.m_Id);
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00013988 File Offset: 0x00011B88
		internal bool Update(ref RenderTextureDescriptor cameraDesc)
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
					this.Alloc(ref historyDesc);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600054E RID: 1358
		protected abstract int GetHistoryFrameCount();

		// Token: 0x0600054F RID: 1359
		protected abstract string GetHistoryName();

		// Token: 0x06000550 RID: 1360
		protected abstract RenderTextureDescriptor GetHistoryDescriptor(ref RenderTextureDescriptor cameraDesc);

		// Token: 0x04000493 RID: 1171
		private int m_Id;

		// Token: 0x04000494 RID: 1172
		private RenderTextureDescriptor m_Descriptor;

		// Token: 0x04000495 RID: 1173
		private Hash128 m_DescKey;
	}
}
