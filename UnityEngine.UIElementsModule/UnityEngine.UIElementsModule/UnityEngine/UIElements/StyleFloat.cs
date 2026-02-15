using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020003D0 RID: 976
	public struct StyleFloat : IStyleValue<float>, IEquatable<StyleFloat>
	{
		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x06001CE5 RID: 7397 RVA: 0x0006B538 File Offset: 0x00069738
		// (set) Token: 0x06001CE6 RID: 7398 RVA: 0x0006B55F File Offset: 0x0006975F
		public float value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : 0f;
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06001CE7 RID: 7399 RVA: 0x0006B570 File Offset: 0x00069770
		// (set) Token: 0x06001CE8 RID: 7400 RVA: 0x0006B588 File Offset: 0x00069788
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

		// Token: 0x06001CE9 RID: 7401 RVA: 0x0006B592 File Offset: 0x00069792
		public StyleFloat(float v)
		{
			this = new StyleFloat(v, StyleKeyword.Undefined);
		}

		// Token: 0x06001CEA RID: 7402 RVA: 0x0006B59E File Offset: 0x0006979E
		public StyleFloat(StyleKeyword keyword)
		{
			this = new StyleFloat(0f, keyword);
		}

		// Token: 0x06001CEB RID: 7403 RVA: 0x0006B5AE File Offset: 0x000697AE
		internal StyleFloat(float v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x06001CEC RID: 7404 RVA: 0x0006B5C0 File Offset: 0x000697C0
		public static bool operator ==(StyleFloat lhs, StyleFloat rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x06001CED RID: 7405 RVA: 0x0006B5F4 File Offset: 0x000697F4
		public static implicit operator StyleFloat(StyleKeyword keyword)
		{
			return new StyleFloat(keyword);
		}

		// Token: 0x06001CEE RID: 7406 RVA: 0x0006B60C File Offset: 0x0006980C
		public static implicit operator StyleFloat(float v)
		{
			return new StyleFloat(v);
		}

		// Token: 0x06001CEF RID: 7407 RVA: 0x0006B624 File Offset: 0x00069824
		public bool Equals(StyleFloat other)
		{
			return other == this;
		}

		// Token: 0x06001CF0 RID: 7408 RVA: 0x0006B644 File Offset: 0x00069844
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleFloat)
			{
				StyleFloat other = (StyleFloat)obj;
				flag = this.Equals(other);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001CF1 RID: 7409 RVA: 0x0006B670 File Offset: 0x00069870
		public override int GetHashCode()
		{
			return (this.m_Value.GetHashCode() * 397) ^ (int)this.m_Keyword;
		}

		// Token: 0x06001CF2 RID: 7410 RVA: 0x0006B69C File Offset: 0x0006989C
		public override string ToString()
		{
			return this.DebugString<float>();
		}

		// Token: 0x04000C93 RID: 3219
		private float m_Value;

		// Token: 0x04000C94 RID: 3220
		private StyleKeyword m_Keyword;
	}
}
