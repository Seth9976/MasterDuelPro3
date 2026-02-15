using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020003CC RID: 972
	public struct StyleCursor : IStyleValue<Cursor>, IEquatable<StyleCursor>
	{
		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x06001CB9 RID: 7353 RVA: 0x0006AF58 File Offset: 0x00069158
		// (set) Token: 0x06001CBA RID: 7354 RVA: 0x0006AF83 File Offset: 0x00069183
		public Cursor value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : default(Cursor);
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x06001CBB RID: 7355 RVA: 0x0006AF94 File Offset: 0x00069194
		// (set) Token: 0x06001CBC RID: 7356 RVA: 0x0006AFAC File Offset: 0x000691AC
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

		// Token: 0x06001CBD RID: 7357 RVA: 0x0006AFB8 File Offset: 0x000691B8
		public StyleCursor(StyleKeyword keyword)
		{
			this = new StyleCursor(default(Cursor), keyword);
		}

		// Token: 0x06001CBE RID: 7358 RVA: 0x0006AFD7 File Offset: 0x000691D7
		internal StyleCursor(Cursor v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x06001CBF RID: 7359 RVA: 0x0006AFE8 File Offset: 0x000691E8
		public static bool operator ==(StyleCursor lhs, StyleCursor rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x06001CC0 RID: 7360 RVA: 0x0006B01C File Offset: 0x0006921C
		public static implicit operator StyleCursor(StyleKeyword keyword)
		{
			return new StyleCursor(keyword);
		}

		// Token: 0x06001CC1 RID: 7361 RVA: 0x0006B034 File Offset: 0x00069234
		public bool Equals(StyleCursor other)
		{
			return other == this;
		}

		// Token: 0x06001CC2 RID: 7362 RVA: 0x0006B054 File Offset: 0x00069254
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleCursor)
			{
				StyleCursor other = (StyleCursor)obj;
				flag = this.Equals(other);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001CC3 RID: 7363 RVA: 0x0006B080 File Offset: 0x00069280
		public override int GetHashCode()
		{
			return (this.m_Value.GetHashCode() * 397) ^ (int)this.m_Keyword;
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x0006B0B4 File Offset: 0x000692B4
		public override string ToString()
		{
			return this.DebugString<Cursor>();
		}

		// Token: 0x04000C8A RID: 3210
		private Cursor m_Value;

		// Token: 0x04000C8B RID: 3211
		private StyleKeyword m_Keyword;
	}
}
