using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.UIElements
{
	// Token: 0x020003D5 RID: 981
	public struct StyleList<T> : IStyleValue<List<T>>, IEquatable<StyleList<T>>
	{
		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x06001D2E RID: 7470 RVA: 0x0006BE04 File Offset: 0x0006A004
		// (set) Token: 0x06001D2F RID: 7471 RVA: 0x0006BE27 File Offset: 0x0006A027
		public List<T> value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : null;
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06001D30 RID: 7472 RVA: 0x0006BE38 File Offset: 0x0006A038
		// (set) Token: 0x06001D31 RID: 7473 RVA: 0x0006BE50 File Offset: 0x0006A050
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

		// Token: 0x06001D32 RID: 7474 RVA: 0x0006BE5A File Offset: 0x0006A05A
		public StyleList(StyleKeyword keyword)
		{
			this = new StyleList<T>(null, keyword);
		}

		// Token: 0x06001D33 RID: 7475 RVA: 0x0006BE66 File Offset: 0x0006A066
		internal StyleList(List<T> v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x06001D34 RID: 7476 RVA: 0x0006BE78 File Offset: 0x0006A078
		public static bool operator ==(StyleList<T> lhs, StyleList<T> rhs)
		{
			bool flag = lhs.m_Keyword != rhs.m_Keyword;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				List<T> list = lhs.m_Value;
				List<T> list2 = rhs.m_Value;
				bool flag3 = list == list2;
				if (flag3)
				{
					flag2 = true;
				}
				else
				{
					bool flag4 = list == null || list2 == null;
					flag2 = !flag4 && list.Count == list2.Count && list.SequenceEqual(list2);
				}
			}
			return flag2;
		}

		// Token: 0x06001D35 RID: 7477 RVA: 0x0006BEEC File Offset: 0x0006A0EC
		public static implicit operator StyleList<T>(StyleKeyword keyword)
		{
			return new StyleList<T>(keyword);
		}

		// Token: 0x06001D36 RID: 7478 RVA: 0x0006BF04 File Offset: 0x0006A104
		public bool Equals(StyleList<T> other)
		{
			return other == this;
		}

		// Token: 0x06001D37 RID: 7479 RVA: 0x0006BF24 File Offset: 0x0006A124
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleList<T>)
			{
				StyleList<T> other = (StyleList<T>)obj;
				flag = this.Equals(other);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001D38 RID: 7480 RVA: 0x0006BF50 File Offset: 0x0006A150
		public override int GetHashCode()
		{
			int hashCode = 0;
			bool flag = this.m_Value != null && this.m_Value.Count > 0;
			if (flag)
			{
				hashCode = EqualityComparer<T>.Default.GetHashCode(this.m_Value[0]);
				for (int i = 1; i < this.m_Value.Count; i++)
				{
					hashCode = (hashCode * 397) ^ EqualityComparer<T>.Default.GetHashCode(this.m_Value[i]);
				}
			}
			return (hashCode * 397) ^ (int)this.m_Keyword;
		}

		// Token: 0x06001D39 RID: 7481 RVA: 0x0006BFE8 File Offset: 0x0006A1E8
		public override string ToString()
		{
			return this.DebugString<List<T>>();
		}

		// Token: 0x04000C9D RID: 3229
		private StyleKeyword m_Keyword;

		// Token: 0x04000C9E RID: 3230
		private List<T> m_Value;
	}
}
