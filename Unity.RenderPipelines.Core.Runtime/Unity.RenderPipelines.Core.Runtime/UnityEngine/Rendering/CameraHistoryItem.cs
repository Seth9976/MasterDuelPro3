using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200001B RID: 27
	public abstract class CameraHistoryItem : ContextItem
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x00004806 File Offset: 0x00002A06
		public virtual void OnCreate(BufferedRTHandleSystem owner, uint typeId)
		{
			this.m_owner = owner;
			this.m_TypeId = typeId;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00004816 File Offset: 0x00002A16
		protected BufferedRTHandleSystem storage
		{
			get
			{
				return this.m_owner;
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000481E File Offset: 0x00002A1E
		protected int MakeId(uint index)
		{
			return (int)(((this.m_TypeId & 65535U) << 16) | (index & 65535U));
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004838 File Offset: 0x00002A38
		protected RTHandle AllocHistoryFrameRT(int id, int count, ref RenderTextureDescriptor desc, string name = "")
		{
			this.m_owner.AllocBuffer(id, count, ref desc, FilterMode.Bilinear, TextureWrapMode.Clamp, false, 0, 0f, name);
			return this.GetCurrentFrameRT(0);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004865 File Offset: 0x00002A65
		protected void ReleaseHistoryFrameRT(int id)
		{
			this.m_owner.ReleaseBuffer(id);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004873 File Offset: 0x00002A73
		protected RTHandle GetPreviousFrameRT(int id)
		{
			return this.m_owner.GetFrameRT(id, 1);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004882 File Offset: 0x00002A82
		protected RTHandle GetCurrentFrameRT(int id)
		{
			return this.m_owner.GetFrameRT(id, 0);
		}

		// Token: 0x0400007A RID: 122
		private BufferedRTHandleSystem m_owner;

		// Token: 0x0400007B RID: 123
		private uint m_TypeId = uint.MaxValue;
	}
}
