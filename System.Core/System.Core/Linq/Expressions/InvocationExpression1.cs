using System;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x020000A2 RID: 162
	internal sealed class InvocationExpression1 : InvocationExpression
	{
		// Token: 0x0600058C RID: 1420 RVA: 0x000158B6 File Offset: 0x00013AB6
		public InvocationExpression1(Expression lambda, Type returnType, Expression arg0)
			: base(lambda, returnType)
		{
			this._arg0 = arg0;
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x000158C7 File Offset: 0x00013AC7
		public override Expression GetArgument(int index)
		{
			if (index == 0)
			{
				return ExpressionUtils.ReturnObject<Expression>(this._arg0);
			}
			throw new ArgumentOutOfRangeException("index");
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x00009F9F File Offset: 0x0000819F
		public override int ArgumentCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x000158E2 File Offset: 0x00013AE2
		internal override InvocationExpression Rewrite(Expression lambda, Expression[] arguments)
		{
			if (arguments != null)
			{
				return Expression.Invoke(lambda, arguments[0]);
			}
			return Expression.Invoke(lambda, ExpressionUtils.ReturnObject<Expression>(this._arg0));
		}

		// Token: 0x040001AF RID: 431
		private object _arg0;
	}
}
