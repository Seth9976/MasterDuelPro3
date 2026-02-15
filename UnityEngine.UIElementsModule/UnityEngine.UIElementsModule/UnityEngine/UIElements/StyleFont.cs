using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020003D1 RID: 977
	public struct StyleFont : IStyleValue<Font>, IEquatable<StyleFont>
	{
		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06001CF3 RID: 7411 RVA: 0x0006B6C0 File Offset: 0x000698C0
		// (set) Token: 0x06001CF4 RID: 7412 RVA: 0x0006B6E3 File Offset: 0x000698E3
		public Font value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : null;
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06001CF5 RID: 7413 RVA: 0x0006B6F4 File Offset: 0x000698F4
		// (set) Token: 0x06001CF6 RID: 7414 RVA: 0x0006B70C File Offset: 0x0006990C
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

		// Token: 0x06001CF7 RID: 7415 RVA: 0x0006B716 File Offset: 0x00069916
		public StyleFont(Font v)
		{
			this = new StyleFont(v, StyleKeyword.Undefined);
		}

		// Token: 0x06001CF8 RID: 7416 RVA: 0x0006B722 File Offset: 0x00069922
		public StyleFont(StyleKeyword keyword)
		{
			this = new StyleFont(null, keyword);
		}

		// Token: 0x06001CF9 RID: 7417 RVA: 0x0006B72E File Offset: 0x0006992E
		internal StyleFont(Font v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x06001CFA RID: 7418 RVA: 0x0006B740 File Offset: 0x00069940
		public static bool operator ==(StyleFont lhs, StyleFont rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x06001CFB RID: 7419 RVA: 0x0006B774 File Offset: 0x00069974
		public static implicit operator StyleFont(StyleKeyword keyword)
		{
			return new StyleFont(keyword);
		}

		// Token: 0x06001CFC RID: 7420 RVA: 0x0006B78C File Offset: 0x0006998C
		public static implicit operator StyleFont(Font v)
		{
			return new StyleFont(v);
		}

		// Token: 0x06001CFD RID: 7421 RVA: 0x0006B7A4 File Offset: 0x000699A4
		public bool Equals(StyleFont other)
		{
			return other == this;
		}

		// Token: 0x06001CFE RID: 7422 RVA: 0x0006B7C4 File Offset: 0x000699C4
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleFont)
			{
				StyleFont other = (StyleFont)obj;
				flag = this.Equals(other);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001CFF RID: 7423 RVA: 0x0006B7F0 File Offset: 0x000699F0
		public override int GetHashCode()
		{
			return (((this.m_Value != null) ? this.m_Value.GetHashCode() : 0) * 397) ^ (int)this.m_Keyword;
		}

		// Token: 0x06001D00 RID: 7424 RVA: 0x0006B82C File Offset: 0x00069A2C
		public override string ToString()
		{
			return this.DebugString<Font>();
		}

		// Token: 0x04000C95 RID: 3221
		private Font m_Value;

		// Token: 0x04000C96 RID: 3222
		private StyleKeyword m_Keyword;
	}
}
