using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020003D4 RID: 980
	public struct StyleLength : IStyleValue<Length>, IEquatable<StyleLength>
	{
		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06001D1E RID: 7454 RVA: 0x0006BB54 File Offset: 0x00069D54
		// (set) Token: 0x06001D1F RID: 7455 RVA: 0x0006BB9C File Offset: 0x00069D9C
		public Length value
		{
			get
			{
				bool flag = this.m_Keyword == StyleKeyword.Auto || this.m_Keyword == StyleKeyword.None || this.m_Keyword == StyleKeyword.Undefined;
				Length length;
				if (flag)
				{
					length = this.m_Value;
				}
				else
				{
					length = default(Length);
				}
				return length;
			}
			set
			{
				bool flag = value.IsAuto();
				if (flag)
				{
					this.m_Keyword = StyleKeyword.Auto;
				}
				else
				{
					bool flag2 = value.IsNone();
					if (flag2)
					{
						this.m_Keyword = StyleKeyword.None;
					}
					else
					{
						this.m_Keyword = StyleKeyword.Undefined;
					}
				}
				this.m_Value = value;
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06001D20 RID: 7456 RVA: 0x0006BBE0 File Offset: 0x00069DE0
		// (set) Token: 0x06001D21 RID: 7457 RVA: 0x0006BBF8 File Offset: 0x00069DF8
		public StyleKeyword keyword
		{
			get
			{
				return this.m_Keyword;
			}
			set
			{
				this.m_Keyword = value;
				bool flag = this.m_Keyword == StyleKeyword.Auto;
				if (flag)
				{
					this.m_Value = Length.Auto();
				}
				else
				{
					bool flag2 = this.m_Keyword == StyleKeyword.None;
					if (flag2)
					{
						this.m_Value = Length.None();
					}
					else
					{
						bool flag3 = this.m_Keyword > StyleKeyword.Undefined;
						if (flag3)
						{
							this.m_Value = default(Length);
						}
					}
				}
			}
		}

		// Token: 0x06001D22 RID: 7458 RVA: 0x0006BC60 File Offset: 0x00069E60
		public StyleLength(float v)
		{
			this = new StyleLength(new Length(v, LengthUnit.Pixel), StyleKeyword.Undefined);
		}

		// Token: 0x06001D23 RID: 7459 RVA: 0x0006BC72 File Offset: 0x00069E72
		public StyleLength(Length v)
		{
			this = new StyleLength(v, StyleKeyword.Undefined);
		}

		// Token: 0x06001D24 RID: 7460 RVA: 0x0006BC80 File Offset: 0x00069E80
		public StyleLength(StyleKeyword keyword)
		{
			this = new StyleLength(default(Length), keyword);
		}

		// Token: 0x06001D25 RID: 7461 RVA: 0x0006BCA0 File Offset: 0x00069EA0
		internal StyleLength(Length v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
			bool flag = v.IsAuto();
			if (flag)
			{
				this.m_Keyword = StyleKeyword.Auto;
			}
			else
			{
				bool flag2 = v.IsNone();
				if (flag2)
				{
					this.m_Keyword = StyleKeyword.None;
				}
			}
		}

		// Token: 0x06001D26 RID: 7462 RVA: 0x0006BCE4 File Offset: 0x00069EE4
		public static bool operator ==(StyleLength lhs, StyleLength rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x06001D27 RID: 7463 RVA: 0x0006BD18 File Offset: 0x00069F18
		public static implicit operator StyleLength(StyleKeyword keyword)
		{
			return new StyleLength(keyword);
		}

		// Token: 0x06001D28 RID: 7464 RVA: 0x0006BD30 File Offset: 0x00069F30
		public static implicit operator StyleLength(float v)
		{
			return new StyleLength(v);
		}

		// Token: 0x06001D29 RID: 7465 RVA: 0x0006BD48 File Offset: 0x00069F48
		public static implicit operator StyleLength(Length v)
		{
			return new StyleLength(v);
		}

		// Token: 0x06001D2A RID: 7466 RVA: 0x0006BD60 File Offset: 0x00069F60
		public bool Equals(StyleLength other)
		{
			return other == this;
		}

		// Token: 0x06001D2B RID: 7467 RVA: 0x0006BD80 File Offset: 0x00069F80
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleLength)
			{
				StyleLength other = (StyleLength)obj;
				flag = this.Equals(other);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001D2C RID: 7468 RVA: 0x0006BDAC File Offset: 0x00069FAC
		public override int GetHashCode()
		{
			return (this.m_Value.GetHashCode() * 397) ^ (int)this.m_Keyword;
		}

		// Token: 0x06001D2D RID: 7469 RVA: 0x0006BDE0 File Offset: 0x00069FE0
		public override string ToString()
		{
			return this.DebugString<Length>();
		}

		// Token: 0x04000C9B RID: 3227
		private Length m_Value;

		// Token: 0x04000C9C RID: 3228
		private StyleKeyword m_Keyword;
	}
}
