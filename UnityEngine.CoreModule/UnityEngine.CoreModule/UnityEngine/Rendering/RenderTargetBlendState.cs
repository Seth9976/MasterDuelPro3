using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003C4 RID: 964
	public struct RenderTargetBlendState : IEquatable<RenderTargetBlendState>
	{
		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06001A29 RID: 6697 RVA: 0x00039284 File Offset: 0x00037484
		public static RenderTargetBlendState defaultValue
		{
			get
			{
				return new RenderTargetBlendState(ColorWriteMask.All, BlendMode.One, BlendMode.Zero, BlendMode.One, BlendMode.Zero, BlendOp.Add, BlendOp.Add);
			}
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x000392A4 File Offset: 0x000374A4
		public RenderTargetBlendState(ColorWriteMask writeMask = ColorWriteMask.All, BlendMode sourceColorBlendMode = BlendMode.One, BlendMode destinationColorBlendMode = BlendMode.Zero, BlendMode sourceAlphaBlendMode = BlendMode.One, BlendMode destinationAlphaBlendMode = BlendMode.Zero, BlendOp colorBlendOperation = BlendOp.Add, BlendOp alphaBlendOperation = BlendOp.Add)
		{
			this.m_WriteMask = (byte)writeMask;
			this.m_SourceColorBlendMode = (byte)sourceColorBlendMode;
			this.m_DestinationColorBlendMode = (byte)destinationColorBlendMode;
			this.m_SourceAlphaBlendMode = (byte)sourceAlphaBlendMode;
			this.m_DestinationAlphaBlendMode = (byte)destinationAlphaBlendMode;
			this.m_ColorBlendOperation = (byte)colorBlendOperation;
			this.m_AlphaBlendOperation = (byte)alphaBlendOperation;
			this.m_Padding = 0;
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x000392F8 File Offset: 0x000374F8
		public bool Equals(RenderTargetBlendState other)
		{
			return this.m_WriteMask == other.m_WriteMask && this.m_SourceColorBlendMode == other.m_SourceColorBlendMode && this.m_DestinationColorBlendMode == other.m_DestinationColorBlendMode && this.m_SourceAlphaBlendMode == other.m_SourceAlphaBlendMode && this.m_DestinationAlphaBlendMode == other.m_DestinationAlphaBlendMode && this.m_ColorBlendOperation == other.m_ColorBlendOperation && this.m_AlphaBlendOperation == other.m_AlphaBlendOperation;
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x00039370 File Offset: 0x00037570
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is RenderTargetBlendState && this.Equals((RenderTargetBlendState)obj);
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x000393A8 File Offset: 0x000375A8
		public override int GetHashCode()
		{
			int hashCode = this.m_WriteMask.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_SourceColorBlendMode.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_DestinationColorBlendMode.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_SourceAlphaBlendMode.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_DestinationAlphaBlendMode.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_ColorBlendOperation.GetHashCode();
			return (hashCode * 397) ^ this.m_AlphaBlendOperation.GetHashCode();
		}

		// Token: 0x04000C62 RID: 3170
		private byte m_WriteMask;

		// Token: 0x04000C63 RID: 3171
		private byte m_SourceColorBlendMode;

		// Token: 0x04000C64 RID: 3172
		private byte m_DestinationColorBlendMode;

		// Token: 0x04000C65 RID: 3173
		private byte m_SourceAlphaBlendMode;

		// Token: 0x04000C66 RID: 3174
		private byte m_DestinationAlphaBlendMode;

		// Token: 0x04000C67 RID: 3175
		private byte m_ColorBlendOperation;

		// Token: 0x04000C68 RID: 3176
		private byte m_AlphaBlendOperation;

		// Token: 0x04000C69 RID: 3177
		private byte m_Padding;
	}
}
