using System;
using System.Collections.Generic;
using System.Dynamic.Utils;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x020000C7 RID: 199
	internal sealed class InstanceMethodCallExpression1 : InstanceMethodCallExpression, IArgumentProvider
	{
		// Token: 0x06000650 RID: 1616 RVA: 0x0001687D File Offset: 0x00014A7D
		public InstanceMethodCallExpression1(MethodInfo method, Expression instance, Expression arg0)
			: base(method, instance)
		{
			this._arg0 = arg0;
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0001688E File Offset: 0x00014A8E
		public override Expression GetArgument(int index)
		{
			if (index == 0)
			{
				return ExpressionUtils.ReturnObject<Expression>(this._arg0);
			}
			throw new ArgumentOutOfRangeException("index");
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x00009F9F File Offset: 0x0000819F
		public override int ArgumentCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x000168A9 File Offset: 0x00014AA9
		internal override MethodCallExpression Rewrite(Expression instance, IReadOnlyList<Expression> args)
		{
			if (args != null)
			{
				return Expression.Call(instance, base.Method, args[0]);
			}
			return Expression.Call(instance, base.Method, ExpressionUtils.ReturnObject<Expression>(this._arg0));
		}

		// Token: 0x040001F2 RID: 498
		private object _arg0;
	}
}
