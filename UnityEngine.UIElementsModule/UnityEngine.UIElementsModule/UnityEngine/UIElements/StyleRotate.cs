using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020003D6 RID: 982
	public struct StyleRotate : IStyleValue<Rotate>, IEquatable<StyleRotate>
	{
		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06001D3A RID: 7482 RVA: 0x0006C00C File Offset: 0x0006A20C
		// (set) Token: 0x06001D3B RID: 7483 RVA: 0x0006C071 File Offset: 0x0006A271
		public Rotate value
		{
			get
			{
				StyleKeyword keyword = this.m_Keyword;
				if (!true)
				{
				}
				Rotate rotate;
				switch (keyword)
				{
				case StyleKeyword.Undefined:
					rotate = this.m_Value;
					goto IL_004F;
				case StyleKeyword.Null:
					rotate = Rotate.None();
					goto IL_004F;
				case StyleKeyword.None:
					rotate = Rotate.None();
					goto IL_004F;
				case StyleKeyword.Initial:
					rotate = Rotate.Initial();
					goto IL_004F;
				}
				throw new NotImplementedException();
				IL_004F:
				if (!true)
				{
				}
				return rotate;
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06001D3C RID: 7484 RVA: 0x0006C084 File Offset: 0x0006A284
		// (set) Token: 0x06001D3D RID: 7485 RVA: 0x0006C09C File Offset: 0x0006A29C
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

		// Token: 0x06001D3E RID: 7486 RVA: 0x0006C0A6 File Offset: 0x0006A2A6
		public StyleRotate(Rotate v)
		{
			this = new StyleRotate(v, StyleKeyword.Undefined);
		}

		// Token: 0x06001D3F RID: 7487 RVA: 0x0006C0B4 File Offset: 0x0006A2B4
		public StyleRotate(StyleKeyword keyword)
		{
			this = new StyleRotate(default(Rotate), keyword);
		}

		// Token: 0x06001D40 RID: 7488 RVA: 0x0006C0D3 File Offset: 0x0006A2D3
		internal StyleRotate(Rotate v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x06001D41 RID: 7489 RVA: 0x0006C0E4 File Offset: 0x0006A2E4
		public static bool operator ==(StyleRotate lhs, StyleRotate rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x06001D42 RID: 7490 RVA: 0x0006C118 File Offset: 0x0006A318
		public static implicit operator StyleRotate(StyleKeyword keyword)
		{
			return new StyleRotate(keyword);
		}

		// Token: 0x06001D43 RID: 7491 RVA: 0x0006C130 File Offset: 0x0006A330
		public static implicit operator StyleRotate(Rotate v)
		{
			return new StyleRotate(v);
		}

		// Token: 0x06001D44 RID: 7492 RVA: 0x0006C148 File Offset: 0x0006A348
		public bool Equals(StyleRotate other)
		{
			return other == this;
		}

		// Token: 0x06001D45 RID: 7493 RVA: 0x0006C168 File Offset: 0x0006A368
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleRotate)
			{
				StyleRotate other = (StyleRotate)obj;
				flag = this.Equals(other);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001D46 RID: 7494 RVA: 0x0006C194 File Offset: 0x0006A394
		public override int GetHashCode()
		{
			return (this.m_Value.GetHashCode() * 397) ^ (int)this.m_Keyword;
		}

		// Token: 0x06001D47 RID: 7495 RVA: 0x0006C1C8 File Offset: 0x0006A3C8
		public override string ToString()
		{
			return this.DebugString<Rotate>();
		}

		// Token: 0x04000C9F RID: 3231
		private Rotate m_Value;

		// Token: 0x04000CA0 RID: 3232
		private StyleKeyword m_Keyword;
	}
}
