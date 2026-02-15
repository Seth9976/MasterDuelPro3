using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020003D7 RID: 983
	public struct StyleScale : IStyleValue<Scale>, IEquatable<StyleScale>
	{
		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06001D48 RID: 7496 RVA: 0x0006C1EC File Offset: 0x0006A3EC
		// (set) Token: 0x06001D49 RID: 7497 RVA: 0x0006C251 File Offset: 0x0006A451
		public Scale value
		{
			get
			{
				StyleKeyword keyword = this.m_Keyword;
				if (!true)
				{
				}
				Scale scale;
				switch (keyword)
				{
				case StyleKeyword.Undefined:
					scale = this.m_Value;
					goto IL_004F;
				case StyleKeyword.Null:
					scale = Scale.None();
					goto IL_004F;
				case StyleKeyword.None:
					scale = Scale.None();
					goto IL_004F;
				case StyleKeyword.Initial:
					scale = Scale.Initial();
					goto IL_004F;
				}
				throw new NotImplementedException();
				IL_004F:
				if (!true)
				{
				}
				return scale;
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06001D4A RID: 7498 RVA: 0x0006C264 File Offset: 0x0006A464
		// (set) Token: 0x06001D4B RID: 7499 RVA: 0x0006C27C File Offset: 0x0006A47C
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

		// Token: 0x06001D4C RID: 7500 RVA: 0x0006C286 File Offset: 0x0006A486
		public StyleScale(Scale v)
		{
			this = new StyleScale(v, StyleKeyword.Undefined);
		}

		// Token: 0x06001D4D RID: 7501 RVA: 0x0006C294 File Offset: 0x0006A494
		public StyleScale(StyleKeyword keyword)
		{
			this = new StyleScale(default(Scale), keyword);
		}

		// Token: 0x06001D4E RID: 7502 RVA: 0x0006C2B3 File Offset: 0x0006A4B3
		internal StyleScale(Scale v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x06001D4F RID: 7503 RVA: 0x0006C2C4 File Offset: 0x0006A4C4
		public static bool operator ==(StyleScale lhs, StyleScale rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x06001D50 RID: 7504 RVA: 0x0006C2F8 File Offset: 0x0006A4F8
		public static implicit operator StyleScale(StyleKeyword keyword)
		{
			return new StyleScale(keyword);
		}

		// Token: 0x06001D51 RID: 7505 RVA: 0x0006C310 File Offset: 0x0006A510
		public static implicit operator StyleScale(Scale v)
		{
			return new StyleScale(v);
		}

		// Token: 0x06001D52 RID: 7506 RVA: 0x0006C328 File Offset: 0x0006A528
		public bool Equals(StyleScale other)
		{
			return other == this;
		}

		// Token: 0x06001D53 RID: 7507 RVA: 0x0006C348 File Offset: 0x0006A548
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleScale)
			{
				StyleScale other = (StyleScale)obj;
				flag = this.Equals(other);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001D54 RID: 7508 RVA: 0x0006C374 File Offset: 0x0006A574
		public override int GetHashCode()
		{
			return (this.m_Value.GetHashCode() * 397) ^ (int)this.m_Keyword;
		}

		// Token: 0x06001D55 RID: 7509 RVA: 0x0006C3A8 File Offset: 0x0006A5A8
		public override string ToString()
		{
			return this.DebugString<Scale>();
		}

		// Token: 0x04000CA1 RID: 3233
		private Scale m_Value;

		// Token: 0x04000CA2 RID: 3234
		private StyleKeyword m_Keyword;
	}
}
