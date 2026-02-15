using System;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x020000A4 RID: 164
	internal sealed class InvocationExpression3 : InvocationExpression
	{
		// Token: 0x06000594 RID: 1428 RVA: 0x0001596C File Offset: 0x00013B6C
		public InvocationExpression3(Expression lambda, Type returnType, Expression arg0, Expression arg1, Expression arg2)
			: base(lambda, returnType)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = arg2;
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0001598D File Offset: 0x00013B8D
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
			default:
				throw new ArgumentOutOfRangeException("index");
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x00012AF6 File Offset: 0x00010CF6
		public override int ArgumentCount
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x000159C7 File Offset: 0x00013BC7
		internal override InvocationExpression Rewrite(Expression lambda, Expression[] arguments)
		{
			if (arguments != null)
			{
				return Expression.Invoke(lambda, arguments[0], arguments[1], arguments[2]);
			}
			return Expression.Invoke(lambda, ExpressionUtils.ReturnObject<Expression>(this._arg0), this._arg1, this._arg2);
		}

		// Token: 0x040001B2 RID: 434
		private object _arg0;

		// Token: 0x040001B3 RID: 435
		private readonly Expression _arg1;

		// Token: 0x040001B4 RID: 436
		private readonly Expression _arg2;
	}
}
