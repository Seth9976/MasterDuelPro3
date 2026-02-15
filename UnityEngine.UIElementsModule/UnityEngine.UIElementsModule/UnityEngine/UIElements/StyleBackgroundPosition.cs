using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020003C8 RID: 968
	public struct StyleBackgroundPosition : IStyleValue<BackgroundPosition>, IEquatable<StyleBackgroundPosition>
	{
		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x06001C85 RID: 7301 RVA: 0x0006A930 File Offset: 0x00068B30
		// (set) Token: 0x06001C86 RID: 7302 RVA: 0x0006A95B File Offset: 0x00068B5B
		public BackgroundPosition value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : default(BackgroundPosition);
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x06001C87 RID: 7303 RVA: 0x0006A96C File Offset: 0x00068B6C
		// (set) Token: 0x06001C88 RID: 7304 RVA: 0x0006A984 File Offset: 0x00068B84
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

		// Token: 0x06001C89 RID: 7305 RVA: 0x0006A98E File Offset: 0x00068B8E
		public StyleBackgroundPosition(BackgroundPosition v)
		{
			this = new StyleBackgroundPosition(v, StyleKeyword.Undefined);
		}

		// Token: 0x06001C8A RID: 7306 RVA: 0x0006A99C File Offset: 0x00068B9C
		public StyleBackgroundPosition(StyleKeyword keyword)
		{
			this = new StyleBackgroundPosition(default(BackgroundPosition), keyword);
		}

		// Token: 0x06001C8B RID: 7307 RVA: 0x0006A9BB File Offset: 0x00068BBB
		internal StyleBackgroundPosition(BackgroundPosition v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x06001C8C RID: 7308 RVA: 0x0006A9CC File Offset: 0x00068BCC
		public static bool operator ==(StyleBackgroundPosition lhs, StyleBackgroundPosition rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x06001C8D RID: 7309 RVA: 0x0006AA00 File Offset: 0x00068C00
		public static implicit operator StyleBackgroundPosition(StyleKeyword keyword)
		{
			return new StyleBackgroundPosition(keyword);
		}

		// Token: 0x06001C8E RID: 7310 RVA: 0x0006AA18 File Offset: 0x00068C18
		public bool Equals(StyleBackgroundPosition other)
		{
			return other == this;
		}

		// Token: 0x06001C8F RID: 7311 RVA: 0x0006AA38 File Offset: 0x00068C38
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleBackgroundPosition)
			{
				StyleBackgroundPosition other = (StyleBackgroundPosition)obj;
				flag = this.Equals(other);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001C90 RID: 7312 RVA: 0x0006AA64 File Offset: 0x00068C64
		public override int GetHashCode()
		{
			return (this.m_Value.GetHashCode() * 397) ^ (int)this.m_Keyword;
		}

		// Token: 0x06001C91 RID: 7313 RVA: 0x0006AA98 File Offset: 0x00068C98
		public override string ToString()
		{
			return this.DebugString<BackgroundPosition>();
		}

		// Token: 0x04000C82 RID: 3202
		private BackgroundPosition m_Value;

		// Token: 0x04000C83 RID: 3203
		private StyleKeyword m_Keyword;
	}
}
