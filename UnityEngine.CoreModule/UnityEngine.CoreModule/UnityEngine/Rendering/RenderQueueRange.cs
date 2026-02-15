using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003C1 RID: 961
	public struct RenderQueueRange : IEquatable<RenderQueueRange>
	{
		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06001A11 RID: 6673 RVA: 0x00038EDC File Offset: 0x000370DC
		public static RenderQueueRange all
		{
			get
			{
				return new RenderQueueRange
				{
					m_LowerBound = 0,
					m_UpperBound = 5000
				};
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06001A12 RID: 6674 RVA: 0x00038F08 File Offset: 0x00037108
		public static RenderQueueRange opaque
		{
			get
			{
				return new RenderQueueRange
				{
					m_LowerBound = 0,
					m_UpperBound = 2500
				};
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06001A13 RID: 6675 RVA: 0x00038F34 File Offset: 0x00037134
		public static RenderQueueRange transparent
		{
			get
			{
				return new RenderQueueRange
				{
					m_LowerBound = 2501,
					m_UpperBound = 5000
				};
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06001A14 RID: 6676 RVA: 0x00038F64 File Offset: 0x00037164
		public int lowerBound
		{
			get
			{
				return this.m_LowerBound;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06001A15 RID: 6677 RVA: 0x00038F7C File Offset: 0x0003717C
		public int upperBound
		{
			get
			{
				return this.m_UpperBound;
			}
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x00038F94 File Offset: 0x00037194
		public bool Equals(RenderQueueRange other)
		{
			return this.m_LowerBound == other.m_LowerBound && this.m_UpperBound == other.m_UpperBound;
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x00038FC8 File Offset: 0x000371C8
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is RenderQueueRange && this.Equals((RenderQueueRange)obj);
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x00039000 File Offset: 0x00037200
		public override int GetHashCode()
		{
			return (this.m_LowerBound * 397) ^ this.m_UpperBound;
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x00039028 File Offset: 0x00037228
		public static bool operator ==(RenderQueueRange left, RenderQueueRange right)
		{
			return left.Equals(right);
		}

		// Token: 0x04000C4F RID: 3151
		private int m_LowerBound;

		// Token: 0x04000C50 RID: 3152
		private int m_UpperBound;

		// Token: 0x04000C51 RID: 3153
		private const int k_MinimumBound = 0;

		// Token: 0x04000C52 RID: 3154
		public static readonly int minimumBound = 0;

		// Token: 0x04000C53 RID: 3155
		private const int k_MaximumBound = 5000;

		// Token: 0x04000C54 RID: 3156
		public static readonly int maximumBound = 5000;
	}
}
