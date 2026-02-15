using System;

namespace System.Linq.Expressions
{
	// Token: 0x0200008F RID: 143
	internal sealed class FullConditionalExpressionWithType : FullConditionalExpression
	{
		// Token: 0x06000444 RID: 1092 RVA: 0x00013230 File Offset: 0x00011430
		internal FullConditionalExpressionWithType(Expression test, Expression ifTrue, Expression ifFalse, Type type)
			: base(test, ifTrue, ifFalse)
		{
			this.Type = type;
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x00013243 File Offset: 0x00011443
		public sealed override Type Type { get; }
	}
}
