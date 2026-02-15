using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020003D8 RID: 984
	public struct StyleTranslate : IStyleValue<Translate>, IEquatable<StyleTranslate>
	{
		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06001D56 RID: 7510 RVA: 0x0006C3CC File Offset: 0x0006A5CC
		// (set) Token: 0x06001D57 RID: 7511 RVA: 0x0006C431 File Offset: 0x0006A631
		public Translate value
		{
			get
			{
				StyleKeyword keyword = this.m_Keyword;
				if (!true)
				{
				}
				Translate translate;
				switch (keyword)
				{
				case StyleKeyword.Undefined:
					translate = this.m_Value;
					goto IL_004F;
				case StyleKeyword.Null:
					translate = Translate.None();
					goto IL_004F;
				case StyleKeyword.None:
					translate = Translate.None();
					goto IL_004F;
				case StyleKeyword.Initial:
					translate = Translate.None();
					goto IL_004F;
				}
				throw new NotImplementedException();
				IL_004F:
				if (!true)
				{
				}
				return translate;
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06001D58 RID: 7512 RVA: 0x0006C444 File Offset: 0x0006A644
		// (set) Token: 0x06001D59 RID: 7513 RVA: 0x0006C45C File Offset: 0x0006A65C
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

		// Token: 0x06001D5A RID: 7514 RVA: 0x0006C466 File Offset: 0x0006A666
		public StyleTranslate(Translate v)
		{
			this = new StyleTranslate(v, StyleKeyword.Undefined);
		}

		// Token: 0x06001D5B RID: 7515 RVA: 0x0006C474 File Offset: 0x0006A674
		public StyleTranslate(StyleKeyword keyword)
		{
			this = new StyleTranslate(default(Translate), keyword);
		}

		// Token: 0x06001D5C RID: 7516 RVA: 0x0006C493 File Offset: 0x0006A693
		internal StyleTranslate(Translate v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x06001D5D RID: 7517 RVA: 0x0006C4A4 File Offset: 0x0006A6A4
		public static bool operator ==(StyleTranslate lhs, StyleTranslate rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x06001D5E RID: 7518 RVA: 0x0006C4D8 File Offset: 0x0006A6D8
		public static implicit operator StyleTranslate(StyleKeyword keyword)
		{
			return new StyleTranslate(keyword);
		}

		// Token: 0x06001D5F RID: 7519 RVA: 0x0006C4F0 File Offset: 0x0006A6F0
		public static implicit operator StyleTranslate(Translate v)
		{
			return new StyleTranslate(v);
		}

		// Token: 0x06001D60 RID: 7520 RVA: 0x0006C508 File Offset: 0x0006A708
		public bool Equals(StyleTranslate other)
		{
			return other == this;
		}

		// Token: 0x06001D61 RID: 7521 RVA: 0x0006C528 File Offset: 0x0006A728
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleTranslate)
			{
				StyleTranslate other = (StyleTranslate)obj;
				flag = this.Equals(other);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001D62 RID: 7522 RVA: 0x0006C554 File Offset: 0x0006A754
		public override int GetHashCode()
		{
			return (this.m_Value.GetHashCode() * 397) ^ (int)this.m_Keyword;
		}

		// Token: 0x06001D63 RID: 7523 RVA: 0x0006C588 File Offset: 0x0006A788
		public override string ToString()
		{
			return this.DebugString<Translate>();
		}

		// Token: 0x04000CA3 RID: 3235
		private Translate m_Value;

		// Token: 0x04000CA4 RID: 3236
		private StyleKeyword m_Keyword;
	}
}
