using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003C2 RID: 962
	public struct RenderStateBlock : IEquatable<RenderStateBlock>
	{
		// Token: 0x06001A1B RID: 6683 RVA: 0x00039054 File Offset: 0x00037254
		public RenderStateBlock(RenderStateMask mask)
		{
			this.m_BlendState = BlendState.defaultValue;
			this.m_RasterState = RasterState.defaultValue;
			this.m_DepthState = DepthState.defaultValue;
			this.m_StencilState = StencilState.defaultValue;
			this.m_StencilReference = 0;
			this.m_Mask = mask;
		}

		// Token: 0x170003F3 RID: 1011
		// (set) Token: 0x06001A1C RID: 6684 RVA: 0x00039091 File Offset: 0x00037291
		public BlendState blendState
		{
			set
			{
				this.m_BlendState = value;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (set) Token: 0x06001A1D RID: 6685 RVA: 0x0003909B File Offset: 0x0003729B
		public RasterState rasterState
		{
			set
			{
				this.m_RasterState = value;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06001A1E RID: 6686 RVA: 0x000390A8 File Offset: 0x000372A8
		// (set) Token: 0x06001A1F RID: 6687 RVA: 0x000390C0 File Offset: 0x000372C0
		public DepthState depthState
		{
			get
			{
				return this.m_DepthState;
			}
			set
			{
				this.m_DepthState = value;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06001A20 RID: 6688 RVA: 0x000390CC File Offset: 0x000372CC
		// (set) Token: 0x06001A21 RID: 6689 RVA: 0x000390E4 File Offset: 0x000372E4
		public StencilState stencilState
		{
			get
			{
				return this.m_StencilState;
			}
			set
			{
				this.m_StencilState = value;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06001A22 RID: 6690 RVA: 0x000390F0 File Offset: 0x000372F0
		// (set) Token: 0x06001A23 RID: 6691 RVA: 0x00039108 File Offset: 0x00037308
		public int stencilReference
		{
			get
			{
				return this.m_StencilReference;
			}
			set
			{
				this.m_StencilReference = value;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06001A24 RID: 6692 RVA: 0x00039114 File Offset: 0x00037314
		// (set) Token: 0x06001A25 RID: 6693 RVA: 0x0003912C File Offset: 0x0003732C
		public RenderStateMask mask
		{
			get
			{
				return this.m_Mask;
			}
			set
			{
				this.m_Mask = value;
			}
		}

		// Token: 0x06001A26 RID: 6694 RVA: 0x00039138 File Offset: 0x00037338
		public bool Equals(RenderStateBlock other)
		{
			return this.m_BlendState.Equals(other.m_BlendState) && this.m_RasterState.Equals(other.m_RasterState) && this.m_DepthState.Equals(other.m_DepthState) && this.m_StencilState.Equals(other.m_StencilState) && this.m_StencilReference == other.m_StencilReference && this.m_Mask == other.m_Mask;
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x000391B8 File Offset: 0x000373B8
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is RenderStateBlock && this.Equals((RenderStateBlock)obj);
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x000391F0 File Offset: 0x000373F0
		public override int GetHashCode()
		{
			int hashCode = this.m_BlendState.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_RasterState.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_DepthState.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_StencilState.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_StencilReference;
			return (hashCode * 397) ^ (int)this.m_Mask;
		}

		// Token: 0x04000C55 RID: 3157
		private BlendState m_BlendState;

		// Token: 0x04000C56 RID: 3158
		private RasterState m_RasterState;

		// Token: 0x04000C57 RID: 3159
		private DepthState m_DepthState;

		// Token: 0x04000C58 RID: 3160
		private StencilState m_StencilState;

		// Token: 0x04000C59 RID: 3161
		private int m_StencilReference;

		// Token: 0x04000C5A RID: 3162
		private RenderStateMask m_Mask;
	}
}
