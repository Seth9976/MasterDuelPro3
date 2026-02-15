using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020003D3 RID: 979
	public struct StyleInt : IStyleValue<int>, IEquatable<StyleInt>
	{
		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06001D12 RID: 7442 RVA: 0x0006B9FC File Offset: 0x00069BFC
		// (set) Token: 0x06001D13 RID: 7443 RVA: 0x0006BA1F File Offset: 0x00069C1F
		public int value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : 0;
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06001D14 RID: 7444 RVA: 0x0006BA30 File Offset: 0x00069C30
		// (set) Token: 0x06001D15 RID: 7445 RVA: 0x0006BA48 File Offset: 0x00069C48
		public StyleKeyword keyword
		{
			get
			{
				return this.m_Keyword;
			}
			set
			{
				this.m_Keyword = value;
			}
		}

		// Token: 0x06001D16 RID: 7446 RVA: 0x0006BA52 File Offset: 0x00069C52
		public StyleInt(StyleKeyword keyword)
		{
			this = new StyleInt(0, keyword);
		}

		// Token: 0x06001D17 RID: 7447 RVA: 0x0006BA5E File Offset: 0x00069C5E
		internal StyleInt(int v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x06001D18 RID: 7448 RVA: 0x0006BA70 File Offset: 0x00069C70
		public static bool operator ==(StyleInt lhs, StyleInt rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x06001D19 RID: 7449 RVA: 0x0006BAA4 File Offset: 0x00069CA4
		public static implicit operator StyleInt(StyleKeyword keyword)
		{
			return new StyleInt(keyword);
		}

		// Token: 0x06001D1A RID: 7450 RVA: 0x0006BABC File Offset: 0x00069CBC
		public bool Equals(StyleInt other)
		{
			return other == this;
		}

		// Token: 0x06001D1B RID: 7451 RVA: 0x0006BADC File Offset: 0x00069CDC
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleInt)
			{
				StyleInt other = (StyleInt)obj;
				flag = this.Equals(other);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001D1C RID: 7452 RVA: 0x0006BB08 File Offset: 0x00069D08
		public override int GetHashCode()
		{
			return (this.m_Value * 397) ^ (int)this.m_Keyword;
		}

		// Token: 0x06001D1D RID: 7453 RVA: 0x0006BB30 File Offset: 0x00069D30
		public override string ToString()
		{
			return this.DebugString<int>();
		}

		// Token: 0x04000C99 RID: 3225
		private int m_Value;

		// Token: 0x04000C9A RID: 3226
		private StyleKeyword m_Keyword;
	}
}
