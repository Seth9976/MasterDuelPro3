using System;
using System.Collections.Generic;
using System.Dynamic.Utils;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x020000C3 RID: 195
	internal sealed class MethodCallExpression3 : MethodCallExpression, IArgumentProvider
	{
		// Token: 0x06000640 RID: 1600 RVA: 0x000165E7 File Offset: 0x000147E7
		public MethodCallExpression3(MethodInfo method, Expression arg0, Expression arg1, Expression arg2)
			: base(method)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = arg2;
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00016606 File Offset: 0x00014806
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

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x00012AF6 File Offset: 0x00010CF6
		public override int ArgumentCount
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00016640 File Offset: 0x00014840
		internal override MethodCallExpression Rewrite(Expression instance, IReadOnlyList<Expression> args)
		{
			if (args != null)
			{
				return Expression.Call(base.Method, args[0], args[1], args[2]);
			}
			return Expression.Call(base.Method, ExpressionUtils.ReturnObject<Expression>(this._arg0), this._arg1, this._arg2);
		}

		// Token: 0x040001E6 RID: 486
		private object _arg0;

		// Token: 0x040001E7 RID: 487
		private readonly Expression _arg1;

		// Token: 0x040001E8 RID: 488
		private readonly Expression _arg2;
	}
}
