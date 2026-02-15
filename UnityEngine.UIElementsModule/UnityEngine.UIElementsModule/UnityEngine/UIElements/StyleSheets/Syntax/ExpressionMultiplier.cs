using System;

namespace UnityEngine.UIElements.StyleSheets.Syntax
{
	// Token: 0x020005C8 RID: 1480
	internal struct ExpressionMultiplier
	{
		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x0600280F RID: 10255 RVA: 0x000A5600 File Offset: 0x000A3800
		// (set) Token: 0x06002810 RID: 10256 RVA: 0x000A5618 File Offset: 0x000A3818
		public ExpressionMultiplierType type
		{
			get
			{
				return this.m_Type;
			}
			set
			{
				this.SetType(value);
			}
		}

		// Token: 0x06002811 RID: 10257 RVA: 0x000A5624 File Offset: 0x000A3824
		public ExpressionMultiplier(ExpressionMultiplierType type = ExpressionMultiplierType.None)
		{
			this.m_Type = type;
			this.min = (this.max = 1);
			this.SetType(type);
		}

		// Token: 0x06002812 RID: 10258 RVA: 0x000A5654 File Offset: 0x000A3854
		private void SetType(ExpressionMultiplierType value)
		{
			this.m_Type = value;
			switch (value)
			{
			case ExpressionMultiplierType.ZeroOrMore:
				this.min = 0;
				this.max = 100;
				return;
			case ExpressionMultiplierType.OneOrMore:
			case ExpressionMultiplierType.OneOrMoreComma:
			case ExpressionMultiplierType.GroupAtLeastOne:
				this.min = 1;
				this.max = 100;
				return;
			case ExpressionMultiplierType.ZeroOrOne:
				this.min = 0;
				this.max = 1;
				return;
			}
			this.min = (this.max = 1);
		}

		// Token: 0x04001546 RID: 5446
		public const int Infinity = 100;

		// Token: 0x04001547 RID: 5447
		private ExpressionMultiplierType m_Type;

		// Token: 0x04001548 RID: 5448
		public int min;

		// Token: 0x04001549 RID: 5449
		public int max;
	}
}
