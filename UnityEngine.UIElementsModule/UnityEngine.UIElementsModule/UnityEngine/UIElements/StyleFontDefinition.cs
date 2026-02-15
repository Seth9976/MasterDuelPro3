using System;
using UnityEngine.TextCore.Text;

namespace UnityEngine.UIElements
{
	// Token: 0x020003D2 RID: 978
	public struct StyleFontDefinition : IStyleValue<FontDefinition>, IEquatable<StyleFontDefinition>
	{
		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x06001D01 RID: 7425 RVA: 0x0006B850 File Offset: 0x00069A50
		// (set) Token: 0x06001D02 RID: 7426 RVA: 0x0006B87B File Offset: 0x00069A7B
		public FontDefinition value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : default(FontDefinition);
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x06001D03 RID: 7427 RVA: 0x0006B88C File Offset: 0x00069A8C
		// (set) Token: 0x06001D04 RID: 7428 RVA: 0x0006B8A4 File Offset: 0x00069AA4
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

		// Token: 0x06001D05 RID: 7429 RVA: 0x0006B8AE File Offset: 0x00069AAE
		public StyleFontDefinition(FontDefinition f)
		{
			this = new StyleFontDefinition(f, StyleKeyword.Undefined);
		}

		// Token: 0x06001D06 RID: 7430 RVA: 0x0006B8BA File Offset: 0x00069ABA
		public StyleFontDefinition(FontAsset f)
		{
			this = new StyleFontDefinition(f, StyleKeyword.Undefined);
		}

		// Token: 0x06001D07 RID: 7431 RVA: 0x0006B8C6 File Offset: 0x00069AC6
		public StyleFontDefinition(Font f)
		{
			this = new StyleFontDefinition(f, StyleKeyword.Undefined);
		}

		// Token: 0x06001D08 RID: 7432 RVA: 0x0006B8D4 File Offset: 0x00069AD4
		public StyleFontDefinition(StyleKeyword keyword)
		{
			this = new StyleFontDefinition(default(FontDefinition), keyword);
		}

		// Token: 0x06001D09 RID: 7433 RVA: 0x0006B8F3 File Offset: 0x00069AF3
		internal StyleFontDefinition(object obj, StyleKeyword keyword)
		{
			this = new StyleFontDefinition(FontDefinition.FromObject(obj), keyword);
		}

		// Token: 0x06001D0A RID: 7434 RVA: 0x0006B904 File Offset: 0x00069B04
		internal StyleFontDefinition(FontAsset f, StyleKeyword keyword)
		{
			this = new StyleFontDefinition(FontDefinition.FromSDFFont(f), keyword);
		}

		// Token: 0x06001D0B RID: 7435 RVA: 0x0006B915 File Offset: 0x00069B15
		internal StyleFontDefinition(Font f, StyleKeyword keyword)
		{
			this = new StyleFontDefinition(FontDefinition.FromFont(f), keyword);
		}

		// Token: 0x06001D0C RID: 7436 RVA: 0x0006B926 File Offset: 0x00069B26
		internal StyleFontDefinition(FontDefinition f, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = f;
		}

		// Token: 0x06001D0D RID: 7437 RVA: 0x0006B938 File Offset: 0x00069B38
		public static implicit operator StyleFontDefinition(StyleKeyword keyword)
		{
			return new StyleFontDefinition(keyword);
		}

		// Token: 0x06001D0E RID: 7438 RVA: 0x0006B950 File Offset: 0x00069B50
		public static implicit operator StyleFontDefinition(FontDefinition f)
		{
			return new StyleFontDefinition(f);
		}

		// Token: 0x06001D0F RID: 7439 RVA: 0x0006B968 File Offset: 0x00069B68
		public bool Equals(StyleFontDefinition other)
		{
			return this.m_Keyword == other.m_Keyword && this.m_Value.Equals(other.m_Value);
		}

		// Token: 0x06001D10 RID: 7440 RVA: 0x0006B99C File Offset: 0x00069B9C
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleFontDefinition)
			{
				StyleFontDefinition other = (StyleFontDefinition)obj;
				flag = this.Equals(other);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001D11 RID: 7441 RVA: 0x0006B9C8 File Offset: 0x00069BC8
		public override int GetHashCode()
		{
			return (int)((this.m_Keyword * (StyleKeyword)397) ^ (StyleKeyword)this.m_Value.GetHashCode());
		}

		// Token: 0x04000C97 RID: 3223
		private StyleKeyword m_Keyword;

		// Token: 0x04000C98 RID: 3224
		private FontDefinition m_Value;
	}
}
