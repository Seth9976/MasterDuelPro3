using System;
using System.Collections.Generic;
using System.Dynamic.Utils;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x020000C2 RID: 194
	internal sealed class MethodCallExpression2 : MethodCallExpression, IArgumentProvider
	{
		// Token: 0x0600063C RID: 1596 RVA: 0x0001656D File Offset: 0x0001476D
		public MethodCallExpression2(MethodInfo method, Expression arg0, Expression arg1)
			: base(method)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00016584 File Offset: 0x00014784
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

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x00012A81 File Offset: 0x00010C81
		public override int ArgumentCount
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x000165AC File Offset: 0x000147AC
		internal override MethodCallExpression Rewrite(Expression instance, IReadOnlyList<Expression> args)
		{
			if (args != null)
			{
				return Expression.Call(base.Method, args[0], args[1]);
			}
			return Expression.Call(base.Method, ExpressionUtils.ReturnObject<Expression>(this._arg0), this._arg1);
		}

		// Token: 0x040001E4 RID: 484
		private object _arg0;

		// Token: 0x040001E5 RID: 485
		private readonly Expression _arg1;
	}
}
