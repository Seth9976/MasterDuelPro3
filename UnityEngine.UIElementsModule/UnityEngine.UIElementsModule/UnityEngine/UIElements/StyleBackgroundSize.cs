using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020003CA RID: 970
	public struct StyleBackgroundSize : IStyleValue<BackgroundSize>, IEquatable<StyleBackgroundSize>
	{
		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06001C9F RID: 7327 RVA: 0x0006AC48 File Offset: 0x00068E48
		// (set) Token: 0x06001CA0 RID: 7328 RVA: 0x0006AC73 File Offset: 0x00068E73
		public BackgroundSize value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : default(BackgroundSize);
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x06001CA1 RID: 7329 RVA: 0x0006AC84 File Offset: 0x00068E84
		// (set) Token: 0x06001CA2 RID: 7330 RVA: 0x0006AC9C File Offset: 0x00068E9C
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

		// Token: 0x06001CA3 RID: 7331 RVA: 0x0006ACA8 File Offset: 0x00068EA8
		public StyleBackgroundSize(StyleKeyword keyword)
		{
			this = new StyleBackgroundSize(default(BackgroundSize), keyword);
		}

		// Token: 0x06001CA4 RID: 7332 RVA: 0x0006ACC7 File Offset: 0x00068EC7
		internal StyleBackgroundSize(BackgroundSize v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x06001CA5 RID: 7333 RVA: 0x0006ACD8 File Offset: 0x00068ED8
		public static bool operator ==(StyleBackgroundSize lhs, StyleBackgroundSize rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x06001CA6 RID: 7334 RVA: 0x0006AD0C File Offset: 0x00068F0C
		public static implicit operator StyleBackgroundSize(StyleKeyword keyword)
		{
			return new StyleBackgroundSize(keyword);
		}

		// Token: 0x06001CA7 RID: 7335 RVA: 0x0006AD24 File Offset: 0x00068F24
		public bool Equals(StyleBackgroundSize other)
		{
			return other == this;
		}

		// Token: 0x06001CA8 RID: 7336 RVA: 0x0006AD44 File Offset: 0x00068F44
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleBackgroundSize)
			{
				StyleBackgroundSize other = (StyleBackgroundSize)obj;
				flag = this.Equals(other);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001CA9 RID: 7337 RVA: 0x0006AD70 File Offset: 0x00068F70
		public override int GetHashCode()
		{
			return (this.m_Value.GetHashCode() * 397) ^ (int)this.m_Keyword;
		}

		// Token: 0x06001CAA RID: 7338 RVA: 0x0006ADA4 File Offset: 0x00068FA4
		public override string ToString()
		{
			return this.DebugString<BackgroundSize>();
		}

		// Token: 0x04000C86 RID: 3206
		private BackgroundSize m_Value;

		// Token: 0x04000C87 RID: 3207
		private StyleKeyword m_Keyword;
	}
}
