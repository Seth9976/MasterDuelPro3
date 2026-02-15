using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020003CB RID: 971
	public struct StyleColor : IStyleValue<Color>, IEquatable<StyleColor>
	{
		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x06001CAB RID: 7339 RVA: 0x0006ADC8 File Offset: 0x00068FC8
		// (set) Token: 0x06001CAC RID: 7340 RVA: 0x0006ADEF File Offset: 0x00068FEF
		public Color value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : Color.clear;
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06001CAD RID: 7341 RVA: 0x0006AE00 File Offset: 0x00069000
		// (set) Token: 0x06001CAE RID: 7342 RVA: 0x0006AE18 File Offset: 0x00069018
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

		// Token: 0x06001CAF RID: 7343 RVA: 0x0006AE22 File Offset: 0x00069022
		public StyleColor(Color v)
		{
			this = new StyleColor(v, StyleKeyword.Undefined);
		}

		// Token: 0x06001CB0 RID: 7344 RVA: 0x0006AE2E File Offset: 0x0006902E
		public StyleColor(StyleKeyword keyword)
		{
			this = new StyleColor(Color.clear, keyword);
		}

		// Token: 0x06001CB1 RID: 7345 RVA: 0x0006AE3E File Offset: 0x0006903E
		internal StyleColor(Color v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x0006AE50 File Offset: 0x00069050
		public static bool operator ==(StyleColor lhs, StyleColor rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x06001CB3 RID: 7347 RVA: 0x0006AE84 File Offset: 0x00069084
		public static implicit operator StyleColor(StyleKeyword keyword)
		{
			return new StyleColor(keyword);
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x0006AE9C File Offset: 0x0006909C
		public static implicit operator StyleColor(Color v)
		{
			return new StyleColor(v);
		}

		// Token: 0x06001CB5 RID: 7349 RVA: 0x0006AEB4 File Offset: 0x000690B4
		public bool Equals(StyleColor other)
		{
			return other == this;
		}

		// Token: 0x06001CB6 RID: 7350 RVA: 0x0006AED4 File Offset: 0x000690D4
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleColor)
			{
				StyleColor other = (StyleColor)obj;
				flag = this.Equals(other);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001CB7 RID: 7351 RVA: 0x0006AF00 File Offset: 0x00069100
		public override int GetHashCode()
		{
			return (this.m_Value.GetHashCode() * 397) ^ (int)this.m_Keyword;
		}

		// Token: 0x06001CB8 RID: 7352 RVA: 0x0006AF34 File Offset: 0x00069134
		public override string ToString()
		{
			return this.DebugString<Color>();
		}

		// Token: 0x04000C88 RID: 3208
		private Color m_Value;

		// Token: 0x04000C89 RID: 3209
		private StyleKeyword m_Keyword;
	}
}
