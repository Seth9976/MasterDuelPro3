using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003B6 RID: 950
	public struct RasterState : IEquatable<RasterState>
	{
		// Token: 0x060019AA RID: 6570 RVA: 0x00037F84 File Offset: 0x00036184
		public RasterState(CullMode cullingMode = CullMode.Back, int offsetUnits = 0, float offsetFactor = 0f, bool depthClip = true)
		{
			this.m_CullingMode = cullingMode;
			this.m_OffsetUnits = offsetUnits;
			this.m_OffsetFactor = offsetFactor;
			this.m_DepthClip = Convert.ToByte(depthClip);
			this.m_Conservative = Convert.ToByte(false);
			this.m_Padding1 = 0;
			this.m_Padding2 = 0;
		}

		// Token: 0x060019AB RID: 6571 RVA: 0x00037FC4 File Offset: 0x000361C4
		public bool Equals(RasterState other)
		{
			return this.m_CullingMode == other.m_CullingMode && this.m_OffsetUnits == other.m_OffsetUnits && this.m_OffsetFactor.Equals(other.m_OffsetFactor) && this.m_DepthClip == other.m_DepthClip && this.m_Conservative == other.m_Conservative;
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x00038024 File Offset: 0x00036224
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is RasterState && this.Equals((RasterState)obj);
		}

		// Token: 0x060019AD RID: 6573 RVA: 0x0003805C File Offset: 0x0003625C
		public override int GetHashCode()
		{
			int hashCode = (int)this.m_CullingMode;
			hashCode = (hashCode * 397) ^ this.m_OffsetUnits;
			hashCode = (hashCode * 397) ^ this.m_OffsetFactor.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_DepthClip.GetHashCode();
			return (hashCode * 397) ^ this.m_Conservative.GetHashCode();
		}

		// Token: 0x04000C1B RID: 3099
		public static readonly RasterState defaultValue = new RasterState(CullMode.Back, 0, 0f, true);

		// Token: 0x04000C1C RID: 3100
		private CullMode m_CullingMode;

		// Token: 0x04000C1D RID: 3101
		private int m_OffsetUnits;

		// Token: 0x04000C1E RID: 3102
		private float m_OffsetFactor;

		// Token: 0x04000C1F RID: 3103
		private byte m_DepthClip;

		// Token: 0x04000C20 RID: 3104
		private byte m_Conservative;

		// Token: 0x04000C21 RID: 3105
		private byte m_Padding1;

		// Token: 0x04000C22 RID: 3106
		private byte m_Padding2;
	}
}
