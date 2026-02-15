using System;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000CF RID: 207
	public sealed class TaaHistory : CameraHistoryItem
	{
		// Token: 0x06000557 RID: 1367 RVA: 0x00013B03 File Offset: 0x00011D03
		public override void OnCreate(BufferedRTHandleSystem owner, uint typeId)
		{
			base.OnCreate(owner, typeId);
			this.m_TaaAccumulationTextureIds[0] = base.MakeId(0U);
			this.m_TaaAccumulationTextureIds[1] = base.MakeId(1U);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00013B2C File Offset: 0x00011D2C
		public override void Reset()
		{
			for (int i = 0; i < this.m_TaaAccumulationTextureIds.Length; i++)
			{
				base.ReleaseHistoryFrameRT(this.m_TaaAccumulationTextureIds[i]);
				this.m_TaaAccumulationVersions[i] = -1;
			}
			this.m_Descriptor.width = 0;
			this.m_Descriptor.height = 0;
			this.m_Descriptor.graphicsFormat = GraphicsFormat.None;
			this.m_DescKey = Hash128.Compute(0);
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00013B93 File Offset: 0x00011D93
		public RTHandle GetAccumulationTexture(int eyeIndex = 0)
		{
			return base.GetCurrentFrameRT(this.m_TaaAccumulationTextureIds[eyeIndex]);
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00013BA3 File Offset: 0x00011DA3
		public int GetAccumulationVersion(int eyeIndex = 0)
		{
			return this.m_TaaAccumulationVersions[eyeIndex];
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00013BAD File Offset: 0x00011DAD
		internal void SetAccumulationVersion(int eyeIndex, int version)
		{
			this.m_TaaAccumulationVersions[eyeIndex] = version;
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00013BB8 File Offset: 0x00011DB8
		private bool IsValid()
		{
			return this.GetAccumulationTexture(0) != null;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00013BC4 File Offset: 0x00011DC4
		private bool IsDirty(ref RenderTextureDescriptor desc)
		{
			return this.m_DescKey != Hash128.Compute<RenderTextureDescriptor>(ref desc);
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00013BD8 File Offset: 0x00011DD8
		private void Alloc(ref RenderTextureDescriptor desc, bool xrMultipassEnabled)
		{
			base.AllocHistoryFrameRT(this.m_TaaAccumulationTextureIds[0], 1, ref desc, TaaHistory.m_TaaAccumulationNames[0]);
			if (xrMultipassEnabled)
			{
				base.AllocHistoryFrameRT(this.m_TaaAccumulationTextureIds[1], 1, ref desc, TaaHistory.m_TaaAccumulationNames[1]);
			}
			this.m_Descriptor = desc;
			this.m_DescKey = Hash128.Compute<RenderTextureDescriptor>(ref desc);
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00013C30 File Offset: 0x00011E30
		internal bool Update(ref RenderTextureDescriptor cameraDesc, bool xrMultipassEnabled = false)
		{
			if (cameraDesc.width > 0 && cameraDesc.height > 0 && cameraDesc.graphicsFormat != GraphicsFormat.None)
			{
				RenderTextureDescriptor taaDesc = TemporalAA.TemporalAADescFromCameraDesc(ref cameraDesc);
				if (this.IsDirty(ref taaDesc))
				{
					this.Reset();
				}
				if (!this.IsValid())
				{
					this.Alloc(ref taaDesc, xrMultipassEnabled);
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000497 RID: 1175
		private int[] m_TaaAccumulationTextureIds = new int[2];

		// Token: 0x04000498 RID: 1176
		private int[] m_TaaAccumulationVersions = new int[2];

		// Token: 0x04000499 RID: 1177
		private static readonly string[] m_TaaAccumulationNames = new string[] { "TaaAccumulationTex0", "TaaAccumulationTex1" };

		// Token: 0x0400049A RID: 1178
		private RenderTextureDescriptor m_Descriptor;

		// Token: 0x0400049B RID: 1179
		private Hash128 m_DescKey;
	}
}
