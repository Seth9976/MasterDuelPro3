using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020003C9 RID: 969
	public struct StyleBackgroundRepeat : IStyleValue<BackgroundRepeat>, IEquatable<StyleBackgroundRepeat>
	{
		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x06001C92 RID: 7314 RVA: 0x0006AABC File Offset: 0x00068CBC
		// (set) Token: 0x06001C93 RID: 7315 RVA: 0x0006AAE7 File Offset: 0x00068CE7
		public BackgroundRepeat value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : default(BackgroundRepeat);
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x06001C94 RID: 7316 RVA: 0x0006AAF8 File Offset: 0x00068CF8
		// (set) Token: 0x06001C95 RID: 7317 RVA: 0x0006AB10 File Offset: 0x00068D10
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

		// Token: 0x06001C96 RID: 7318 RVA: 0x0006AB1A File Offset: 0x00068D1A
		public StyleBackgroundRepeat(BackgroundRepeat v)
		{
			this = new StyleBackgroundRepeat(v, StyleKeyword.Undefined);
		}

		// Token: 0x06001C97 RID: 7319 RVA: 0x0006AB28 File Offset: 0x00068D28
		public StyleBackgroundRepeat(StyleKeyword keyword)
		{
			this = new StyleBackgroundRepeat(default(BackgroundRepeat), keyword);
		}

		// Token: 0x06001C98 RID: 7320 RVA: 0x0006AB47 File Offset: 0x00068D47
		internal StyleBackgroundRepeat(BackgroundRepeat v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x06001C99 RID: 7321 RVA: 0x0006AB58 File Offset: 0x00068D58
		public static bool operator ==(StyleBackgroundRepeat lhs, StyleBackgroundRepeat rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x0006AB8C File Offset: 0x00068D8C
		public static implicit operator StyleBackgroundRepeat(StyleKeyword keyword)
		{
			return new StyleBackgroundRepeat(keyword);
		}

		// Token: 0x06001C9B RID: 7323 RVA: 0x0006ABA4 File Offset: 0x00068DA4
		public bool Equals(StyleBackgroundRepeat other)
		{
			return other == this;
		}

		// Token: 0x06001C9C RID: 7324 RVA: 0x0006ABC4 File Offset: 0x00068DC4
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleBackgroundRepeat)
			{
				StyleBackgroundRepeat other = (StyleBackgroundRepeat)obj;
				flag = this.Equals(other);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001C9D RID: 7325 RVA: 0x0006ABF0 File Offset: 0x00068DF0
		public override int GetHashCode()
		{
			return (this.m_Value.GetHashCode() * 397) ^ (int)this.m_Keyword;
		}

		// Token: 0x06001C9E RID: 7326 RVA: 0x0006AC24 File Offset: 0x00068E24
		public override string ToString()
		{
			return this.DebugString<BackgroundRepeat>();
		}

		// Token: 0x04000C84 RID: 3204
		private BackgroundRepeat m_Value;

		// Token: 0x04000C85 RID: 3205
		private StyleKeyword m_Keyword;
	}
}
