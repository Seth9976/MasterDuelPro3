using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000344 RID: 836
	public struct RenderTargetBinding
	{
		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06001667 RID: 5735 RVA: 0x0002F154 File Offset: 0x0002D354
		public RenderTargetIdentifier[] colorRenderTargets
		{
			get
			{
				return this.m_ColorRenderTargets;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06001668 RID: 5736 RVA: 0x0002F16C File Offset: 0x0002D36C
		public RenderTargetIdentifier depthRenderTarget
		{
			get
			{
				return this.m_DepthRenderTarget;
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06001669 RID: 5737 RVA: 0x0002F184 File Offset: 0x0002D384
		public RenderBufferLoadAction[] colorLoadActions
		{
			get
			{
				return this.m_ColorLoadActions;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x0600166A RID: 5738 RVA: 0x0002F19C File Offset: 0x0002D39C
		public RenderBufferStoreAction[] colorStoreActions
		{
			get
			{
				return this.m_ColorStoreActions;
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x0600166B RID: 5739 RVA: 0x0002F1B4 File Offset: 0x0002D3B4
		public RenderBufferLoadAction depthLoadAction
		{
			get
			{
				return this.m_DepthLoadAction;
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x0600166C RID: 5740 RVA: 0x0002F1CC File Offset: 0x0002D3CC
		public RenderBufferStoreAction depthStoreAction
		{
			get
			{
				return this.m_DepthStoreAction;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x0600166D RID: 5741 RVA: 0x0002F1E4 File Offset: 0x0002D3E4
		public RenderTargetFlags flags
		{
			get
			{
				return this.m_Flags;
			}
		}

		// Token: 0x04000989 RID: 2441
		private RenderTargetIdentifier[] m_ColorRenderTargets;

		// Token: 0x0400098A RID: 2442
		private RenderTargetIdentifier m_DepthRenderTarget;

		// Token: 0x0400098B RID: 2443
		private RenderBufferLoadAction[] m_ColorLoadActions;

		// Token: 0x0400098C RID: 2444
		private RenderBufferStoreAction[] m_ColorStoreActions;

		// Token: 0x0400098D RID: 2445
		private RenderBufferLoadAction m_DepthLoadAction;

		// Token: 0x0400098E RID: 2446
		private RenderBufferStoreAction m_DepthStoreAction;

		// Token: 0x0400098F RID: 2447
		private RenderTargetFlags m_Flags;
	}
}
