using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020003C7 RID: 967
	public struct StyleBackground : IStyleValue<Background>, IEquatable<StyleBackground>
	{
		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06001C73 RID: 7283 RVA: 0x0006A758 File Offset: 0x00068958
		// (set) Token: 0x06001C74 RID: 7284 RVA: 0x0006A783 File Offset: 0x00068983
		public Background value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : default(Background);
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06001C75 RID: 7285 RVA: 0x0006A794 File Offset: 0x00068994
		// (set) Token: 0x06001C76 RID: 7286 RVA: 0x0006A7AC File Offset: 0x000689AC
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

		// Token: 0x06001C77 RID: 7287 RVA: 0x0006A7B6 File Offset: 0x000689B6
		public StyleBackground(Texture2D v)
		{
			this = new StyleBackground(v, StyleKeyword.Undefined);
		}

		// Token: 0x06001C78 RID: 7288 RVA: 0x0006A7C2 File Offset: 0x000689C2
		public StyleBackground(Sprite v)
		{
			this = new StyleBackground(v, StyleKeyword.Undefined);
		}

		// Token: 0x06001C79 RID: 7289 RVA: 0x0006A7CE File Offset: 0x000689CE
		public StyleBackground(VectorImage v)
		{
			this = new StyleBackground(v, StyleKeyword.Undefined);
		}

		// Token: 0x06001C7A RID: 7290 RVA: 0x0006A7DC File Offset: 0x000689DC
		public StyleBackground(StyleKeyword keyword)
		{
			this = new StyleBackground(default(Background), keyword);
		}

		// Token: 0x06001C7B RID: 7291 RVA: 0x0006A7FB File Offset: 0x000689FB
		internal StyleBackground(Texture2D v, StyleKeyword keyword)
		{
			this = new StyleBackground(Background.FromTexture2D(v), keyword);
		}

		// Token: 0x06001C7C RID: 7292 RVA: 0x0006A80C File Offset: 0x00068A0C
		internal StyleBackground(Sprite v, StyleKeyword keyword)
		{
			this = new StyleBackground(Background.FromSprite(v), keyword);
		}

		// Token: 0x06001C7D RID: 7293 RVA: 0x0006A81D File Offset: 0x00068A1D
		internal StyleBackground(VectorImage v, StyleKeyword keyword)
		{
			this = new StyleBackground(Background.FromVectorImage(v), keyword);
		}

		// Token: 0x06001C7E RID: 7294 RVA: 0x0006A82E File Offset: 0x00068A2E
		internal StyleBackground(Background v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x06001C7F RID: 7295 RVA: 0x0006A840 File Offset: 0x00068A40
		public static bool operator ==(StyleBackground lhs, StyleBackground rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x06001C80 RID: 7296 RVA: 0x0006A874 File Offset: 0x00068A74
		public static implicit operator StyleBackground(StyleKeyword keyword)
		{
			return new StyleBackground(keyword);
		}

		// Token: 0x06001C81 RID: 7297 RVA: 0x0006A88C File Offset: 0x00068A8C
		public bool Equals(StyleBackground other)
		{
			return other == this;
		}

		// Token: 0x06001C82 RID: 7298 RVA: 0x0006A8AC File Offset: 0x00068AAC
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleBackground)
			{
				StyleBackground other = (StyleBackground)obj;
				flag = this.Equals(other);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001C83 RID: 7299 RVA: 0x0006A8D8 File Offset: 0x00068AD8
		public override int GetHashCode()
		{
			return (this.m_Value.GetHashCode() * 397) ^ (int)this.m_Keyword;
		}

		// Token: 0x06001C84 RID: 7300 RVA: 0x0006A90C File Offset: 0x00068B0C
		public override string ToString()
		{
			return this.DebugString<Background>();
		}

		// Token: 0x04000C80 RID: 3200
		private Background m_Value;

		// Token: 0x04000C81 RID: 3201
		private StyleKeyword m_Keyword;
	}
}
