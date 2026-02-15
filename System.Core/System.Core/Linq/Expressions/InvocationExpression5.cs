using System;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x020000A6 RID: 166
	internal sealed class InvocationExpression5 : InvocationExpression
	{
		// Token: 0x0600059C RID: 1436 RVA: 0x00015AAF File Offset: 0x00013CAF
		public InvocationExpression5(Expression lambda, Type returnType, Expression arg0, Expression arg1, Expression arg2, Expression arg3, Expression arg4)
			: base(lambda, returnType)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = arg2;
			this._arg3 = arg3;
			this._arg4 = arg4;
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00015AE0 File Offset: 0x00013CE0
		public override Expression GetArgument(int index)
		{
			switch (index)
			{
			case 0:
				return ExpressionUtils.ReturnObject<Expression>(this._arg0);
			case 1:
				return this._arg1;
			case 2:
				return this._arg2;
			case 3:
				return this._arg3;
			case 4:
				return this._arg4;
			default:
				throw new ArgumentOutOfRangeException("index");
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x00012C3B File Offset: 0x00010E3B
		public override int ArgumentCount
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00015B3C File Offset: 0x00013D3C
		internal override InvocationExpression Rewrite(Expression lambda, Expression[] arguments)
		{
			if (arguments != null)
			{
				return Expression.Invoke(lambda, arguments[0], arguments[1], arguments[2], arguments[3], arguments[4]);
			}
			return Expression.Invoke(lambda, ExpressionUtils.ReturnObject<Expression>(this._arg0), this._arg1, this._arg2, this._arg3, this._arg4);
		}

		// Token: 0x040001B9 RID: 441
		private object _arg0;

		// Token: 0x040001BA RID: 442
		private readonly Expression _arg1;

		// Token: 0x040001BB RID: 443
		private readonly Expression _arg2;

		// Token: 0x040001BC RID: 444
		private readonly Expression _arg3;

		// Token: 0x040001BD RID: 445
		private readonly Expression _arg4;
	}
}
