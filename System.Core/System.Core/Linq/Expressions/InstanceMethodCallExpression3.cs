using System;
using System.Collections.Generic;
using System.Dynamic.Utils;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x020000C9 RID: 201
	internal sealed class InstanceMethodCallExpression3 : InstanceMethodCallExpression, IArgumentProvider
	{
		// Token: 0x06000658 RID: 1624 RVA: 0x00016957 File Offset: 0x00014B57
		public InstanceMethodCallExpression3(MethodInfo method, Expression instance, Expression arg0, Expression arg1, Expression arg2)
			: base(method, instance)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = arg2;
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00016978 File Offset: 0x00014B78
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

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600065A RID: 1626 RVA: 0x00012AF6 File Offset: 0x00010CF6
		public override int ArgumentCount
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x000169B4 File Offset: 0x00014BB4
		internal override MethodCallExpression Rewrite(Expression instance, IReadOnlyList<Expression> args)
		{
			if (args != null)
			{
				return Expression.Call(instance, base.Method, args[0], args[1], args[2]);
			}
			return Expression.Call(instance, base.Method, ExpressionUtils.ReturnObject<Expression>(this._arg0), this._arg1, this._arg2);
		}

		// Token: 0x040001F5 RID: 501
		private object _arg0;

		// Token: 0x040001F6 RID: 502
		private readonly Expression _arg1;

		// Token: 0x040001F7 RID: 503
		private readonly Expression _arg2;
	}
}
