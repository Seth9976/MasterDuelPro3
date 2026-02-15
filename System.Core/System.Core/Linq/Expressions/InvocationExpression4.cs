using System;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x020000A5 RID: 165
	internal sealed class InvocationExpression4 : InvocationExpression
	{
		// Token: 0x06000598 RID: 1432 RVA: 0x000159F9 File Offset: 0x00013BF9
		public InvocationExpression4(Expression lambda, Type returnType, Expression arg0, Expression arg1, Expression arg2, Expression arg3)
			: base(lambda, returnType)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = arg2;
			this._arg3 = arg3;
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00015A24 File Offset: 0x00013C24
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
			default:
				throw new ArgumentOutOfRangeException("index");
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x00012B8C File Offset: 0x00010D8C
		public override int ArgumentCount
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00015A74 File Offset: 0x00013C74
		internal override InvocationExpression Rewrite(Expression lambda, Expression[] arguments)
		{
			if (arguments != null)
			{
				return Expression.Invoke(lambda, arguments[0], arguments[1], arguments[2], arguments[3]);
			}
			return Expression.Invoke(lambda, ExpressionUtils.ReturnObject<Expression>(this._arg0), this._arg1, this._arg2, this._arg3);
		}

		// Token: 0x040001B5 RID: 437
		private object _arg0;

		// Token: 0x040001B6 RID: 438
		private readonly Expression _arg1;

		// Token: 0x040001B7 RID: 439
		private readonly Expression _arg2;

		// Token: 0x040001B8 RID: 440
		private readonly Expression _arg3;
	}
}
