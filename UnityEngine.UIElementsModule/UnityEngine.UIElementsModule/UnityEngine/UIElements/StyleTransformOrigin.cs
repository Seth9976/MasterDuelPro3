using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020003DA RID: 986
	public struct StyleTransformOrigin : IStyleValue<TransformOrigin>, IEquatable<StyleTransformOrigin>
	{
		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06001D70 RID: 7536 RVA: 0x0006C758 File Offset: 0x0006A958
		// (set) Token: 0x06001D71 RID: 7537 RVA: 0x0006C7BD File Offset: 0x0006A9BD
		public TransformOrigin value
		{
			get
			{
				StyleKeyword keyword = this.m_Keyword;
				if (!true)
				{
				}
				TransformOrigin transformOrigin;
				switch (keyword)
				{
				case StyleKeyword.Undefined:
					transformOrigin = this.m_Value;
					goto IL_004F;
				case StyleKeyword.Null:
					transformOrigin = TransformOrigin.Initial();
					goto IL_004F;
				case StyleKeyword.None:
					transformOrigin = TransformOrigin.Initial();
					goto IL_004F;
				case StyleKeyword.Initial:
					transformOrigin = TransformOrigin.Initial();
					goto IL_004F;
				}
				throw new NotImplementedException();
				IL_004F:
				if (!true)
				{
				}
				return transformOrigin;
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06001D72 RID: 7538 RVA: 0x0006C7D0 File Offset: 0x0006A9D0
		// (set) Token: 0x06001D73 RID: 7539 RVA: 0x0006C7E8 File Offset: 0x0006A9E8
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

		// Token: 0x06001D74 RID: 7540 RVA: 0x0006C7F2 File Offset: 0x0006A9F2
		public StyleTransformOrigin(TransformOrigin v)
		{
			this = new StyleTransformOrigin(v, StyleKeyword.Undefined);
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x0006C800 File Offset: 0x0006AA00
		public StyleTransformOrigin(StyleKeyword keyword)
		{
			this = new StyleTransformOrigin(default(TransformOrigin), keyword);
		}

		// Token: 0x06001D76 RID: 7542 RVA: 0x0006C81F File Offset: 0x0006AA1F
		internal StyleTransformOrigin(TransformOrigin v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x06001D77 RID: 7543 RVA: 0x0006C830 File Offset: 0x0006AA30
		public static bool operator ==(StyleTransformOrigin lhs, StyleTransformOrigin rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x06001D78 RID: 7544 RVA: 0x0006C864 File Offset: 0x0006AA64
		public static implicit operator StyleTransformOrigin(StyleKeyword keyword)
		{
			return new StyleTransformOrigin(keyword);
		}

		// Token: 0x06001D79 RID: 7545 RVA: 0x0006C87C File Offset: 0x0006AA7C
		public static implicit operator StyleTransformOrigin(TransformOrigin v)
		{
			return new StyleTransformOrigin(v);
		}

		// Token: 0x06001D7A RID: 7546 RVA: 0x0006C894 File Offset: 0x0006AA94
		public bool Equals(StyleTransformOrigin other)
		{
			return other == this;
		}

		// Token: 0x06001D7B RID: 7547 RVA: 0x0006C8B4 File Offset: 0x0006AAB4
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleTransformOrigin)
			{
				StyleTransformOrigin other = (StyleTransformOrigin)obj;
				flag = this.Equals(other);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001D7C RID: 7548 RVA: 0x0006C8E0 File Offset: 0x0006AAE0
		public override int GetHashCode()
		{
			return (this.m_Value.GetHashCode() * 397) ^ (int)this.m_Keyword;
		}

		// Token: 0x06001D7D RID: 7549 RVA: 0x0006C914 File Offset: 0x0006AB14
		public override string ToString()
		{
			return this.DebugString<TransformOrigin>();
		}

		// Token: 0x04000CA7 RID: 3239
		private TransformOrigin m_Value;

		// Token: 0x04000CA8 RID: 3240
		private StyleKeyword m_Keyword;
	}
}
