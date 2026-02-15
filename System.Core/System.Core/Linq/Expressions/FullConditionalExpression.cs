using System;

namespace System.Linq.Expressions
{
	// Token: 0x0200008E RID: 142
	internal class FullConditionalExpression : ConditionalExpression
	{
		// Token: 0x06000442 RID: 1090 RVA: 0x00013217 File Offset: 0x00011417
		internal FullConditionalExpression(Expression test, Expression ifTrue, Expression ifFalse)
			: base(test, ifTrue)
		{
			this._false = ifFalse;
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00013228 File Offset: 0x00011428
		internal override Expression GetFalse()
		{
			return this._false;
		}

		// Token: 0x04000141 RID: 321
		private readonly Expression _false;
	}
}
