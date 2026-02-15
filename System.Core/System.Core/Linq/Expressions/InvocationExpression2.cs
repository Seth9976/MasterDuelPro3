using System;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x020000A3 RID: 163
	internal sealed class InvocationExpression2 : InvocationExpression
	{
		// Token: 0x06000590 RID: 1424 RVA: 0x00015902 File Offset: 0x00013B02
		public InvocationExpression2(Expression lambda, Type returnType, Expression arg0, Expression arg1)
			: base(lambda, returnType)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0001591B File Offset: 0x00013B1B
		public override Expression GetArgument(int index)
		{
			if (index == 0)
			{
				return ExpressionUtils.ReturnObject<Expression>(this._arg0);
			}
			if (index != 1)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return this._arg1;
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x00012A81 File Offset: 0x00010C81
		public override int ArgumentCount
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00015943 File Offset: 0x00013B43
		internal override InvocationExpression Rewrite(Expression lambda, Expression[] arguments)
		{
			if (arguments != null)
			{
				return Expression.Invoke(lambda, arguments[0], arguments[1]);
			}
			return Expression.Invoke(lambda, ExpressionUtils.ReturnObject<Expression>(this._arg0), this._arg1);
		}

		// Token: 0x040001B0 RID: 432
		private object _arg0;

		// Token: 0x040001B1 RID: 433
		private readonly Expression _arg1;
	}
}
