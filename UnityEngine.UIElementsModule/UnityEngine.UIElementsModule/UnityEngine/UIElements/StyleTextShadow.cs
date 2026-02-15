using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020003D9 RID: 985
	public struct StyleTextShadow : IStyleValue<TextShadow>, IEquatable<StyleTextShadow>
	{
		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06001D64 RID: 7524 RVA: 0x0006C5AC File Offset: 0x0006A7AC
		// (set) Token: 0x06001D65 RID: 7525 RVA: 0x0006C5D7 File Offset: 0x0006A7D7
		public TextShadow value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : default(TextShadow);
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06001D66 RID: 7526 RVA: 0x0006C5E8 File Offset: 0x0006A7E8
		// (set) Token: 0x06001D67 RID: 7527 RVA: 0x0006C600 File Offset: 0x0006A800
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

		// Token: 0x06001D68 RID: 7528 RVA: 0x0006C60C File Offset: 0x0006A80C
		public StyleTextShadow(StyleKeyword keyword)
		{
			this = new StyleTextShadow(default(TextShadow), keyword);
		}

		// Token: 0x06001D69 RID: 7529 RVA: 0x0006C62B File Offset: 0x0006A82B
		internal StyleTextShadow(TextShadow v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x06001D6A RID: 7530 RVA: 0x0006C63C File Offset: 0x0006A83C
		public static bool operator ==(StyleTextShadow lhs, StyleTextShadow rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x06001D6B RID: 7531 RVA: 0x0006C670 File Offset: 0x0006A870
		public static implicit operator StyleTextShadow(StyleKeyword keyword)
		{
			return new StyleTextShadow(keyword);
		}

		// Token: 0x06001D6C RID: 7532 RVA: 0x0006C688 File Offset: 0x0006A888
		public bool Equals(StyleTextShadow other)
		{
			return other == this;
		}

		// Token: 0x06001D6D RID: 7533 RVA: 0x0006C6A8 File Offset: 0x0006A8A8
		public override bool Equals(object obj)
		{
			bool flag = !(obj is StyleTextShadow);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				StyleTextShadow v = (StyleTextShadow)obj;
				flag2 = v == this;
			}
			return flag2;
		}

		// Token: 0x06001D6E RID: 7534 RVA: 0x0006C6E4 File Offset: 0x0006A8E4
		public override int GetHashCode()
		{
			int hashCode = 917506989;
			hashCode = hashCode * -1521134295 + this.m_Keyword.GetHashCode();
			return hashCode * -1521134295 + this.m_Value.GetHashCode();
		}

		// Token: 0x06001D6F RID: 7535 RVA: 0x0006C734 File Offset: 0x0006A934
		public override string ToString()
		{
			return this.DebugString<TextShadow>();
		}

		// Token: 0x04000CA5 RID: 3237
		private StyleKeyword m_Keyword;

		// Token: 0x04000CA6 RID: 3238
		private TextShadow m_Value;
	}
}
