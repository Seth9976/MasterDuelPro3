using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003CC RID: 972
	public struct SortingLayerRange : IEquatable<SortingLayerRange>
	{
		// Token: 0x06001A8B RID: 6795 RVA: 0x00039FE6 File Offset: 0x000381E6
		public SortingLayerRange(short lowerBound, short upperBound)
		{
			this.m_LowerBound = lowerBound;
			this.m_UpperBound = upperBound;
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06001A8C RID: 6796 RVA: 0x00039FF8 File Offset: 0x000381F8
		public short lowerBound
		{
			get
			{
				return this.m_LowerBound;
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06001A8D RID: 6797 RVA: 0x0003A010 File Offset: 0x00038210
		public short upperBound
		{
			get
			{
				return this.m_UpperBound;
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06001A8E RID: 6798 RVA: 0x0003A028 File Offset: 0x00038228
		public static SortingLayerRange all
		{
			get
			{
				return new SortingLayerRange
				{
					m_LowerBound = short.MinValue,
					m_UpperBound = short.MaxValue
				};
			}
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x0003A058 File Offset: 0x00038258
		public bool Equals(SortingLayerRange other)
		{
			return this.m_LowerBound == other.m_LowerBound && this.m_UpperBound == other.m_UpperBound;
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x0003A08C File Offset: 0x0003828C
		public override bool Equals(object obj)
		{
			bool flag = !(obj is SortingLayerRange);
			return !flag && this.Equals((SortingLayerRange)obj);
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x0003A0C0 File Offset: 0x000382C0
		public override int GetHashCode()
		{
			return ((int)this.m_UpperBound << 16) | ((int)this.m_LowerBound & 65535);
		}

		// Token: 0x04000C90 RID: 3216
		private short m_LowerBound;

		// Token: 0x04000C91 RID: 3217
		private short m_UpperBound;
	}
}
