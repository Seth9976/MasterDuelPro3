using System;
using System.Collections.Generic;
using System.Dynamic.Utils;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x020000C1 RID: 193
	internal sealed class MethodCallExpression1 : MethodCallExpression, IArgumentProvider
	{
		// Token: 0x06000638 RID: 1592 RVA: 0x00016514 File Offset: 0x00014714
		public MethodCallExpression1(MethodInfo method, Expression arg0)
			: base(method)
		{
			this._arg0 = arg0;
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00016524 File Offset: 0x00014724
		public override Expression GetArgument(int index)
		{
			if (index == 0)
			{
				return ExpressionUtils.ReturnObject<Expression>(this._arg0);
			}
			throw new ArgumentOutOfRangeException("index");
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x00009F9F File Offset: 0x0000819F
		public override int ArgumentCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0001653F File Offset: 0x0001473F
		internal override MethodCallExpression Rewrite(Expression instance, IReadOnlyList<Expression> args)
		{
			if (args != null)
			{
				return Expression.Call(base.Method, args[0]);
			}
			return Expression.Call(base.Method, ExpressionUtils.ReturnObject<Expression>(this._arg0));
		}

		// Token: 0x040001E3 RID: 483
		private object _arg0;
	}
}
