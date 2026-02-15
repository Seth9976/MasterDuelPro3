using System;
using System.Collections.Generic;
using System.Dynamic.Utils;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x020000C8 RID: 200
	internal sealed class InstanceMethodCallExpression2 : InstanceMethodCallExpression, IArgumentProvider
	{
		// Token: 0x06000654 RID: 1620 RVA: 0x000168D9 File Offset: 0x00014AD9
		public InstanceMethodCallExpression2(MethodInfo method, Expression instance, Expression arg0, Expression arg1)
			: base(method, instance)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x000168F2 File Offset: 0x00014AF2
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

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x00012A81 File Offset: 0x00010C81
		public override int ArgumentCount
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0001691A File Offset: 0x00014B1A
		internal override MethodCallExpression Rewrite(Expression instance, IReadOnlyList<Expression> args)
		{
			if (args != null)
			{
				return Expression.Call(instance, base.Method, args[0], args[1]);
			}
			return Expression.Call(instance, base.Method, ExpressionUtils.ReturnObject<Expression>(this._arg0), this._arg1);
		}

		// Token: 0x040001F3 RID: 499
		private object _arg0;

		// Token: 0x040001F4 RID: 500
		private readonly Expression _arg1;
	}
}
