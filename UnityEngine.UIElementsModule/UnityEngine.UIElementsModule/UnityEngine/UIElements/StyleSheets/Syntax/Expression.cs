using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements.StyleSheets.Syntax
{
	// Token: 0x020005C3 RID: 1475
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal class Expression
	{
		// Token: 0x0600280E RID: 10254 RVA: 0x000A55CD File Offset: 0x000A37CD
		public Expression(ExpressionType type)
		{
			this.type = type;
			this.combinator = ExpressionCombinator.None;
			this.multiplier = new ExpressionMultiplier(ExpressionMultiplierType.None);
			this.subExpressions = null;
			this.keyword = null;
		}

		// Token: 0x04001520 RID: 5408
		public ExpressionType type;

		// Token: 0x04001521 RID: 5409
		public ExpressionMultiplier multiplier;

		// Token: 0x04001522 RID: 5410
		public DataType dataType;

		// Token: 0x04001523 RID: 5411
		public ExpressionCombinator combinator;

		// Token: 0x04001524 RID: 5412
		public Expression[] subExpressions;

		// Token: 0x04001525 RID: 5413
		public string keyword;
	}
}
