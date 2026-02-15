using System;
using System.Collections.Generic;
using System.Dynamic.Utils;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x020000C4 RID: 196
	internal sealed class MethodCallExpression4 : MethodCallExpression, IArgumentProvider
	{
		// Token: 0x06000644 RID: 1604 RVA: 0x00016693 File Offset: 0x00014893
		public MethodCallExpression4(MethodInfo method, Expression arg0, Expression arg1, Expression arg2, Expression arg3)
			: base(method)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = arg2;
			this._arg3 = arg3;
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x000166BC File Offset: 0x000148BC
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

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x00012B8C File Offset: 0x00010D8C
		public override int ArgumentCount
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0001670C File Offset: 0x0001490C
		internal override MethodCallExpression Rewrite(Expression instance, IReadOnlyList<Expression> args)
		{
			if (args != null)
			{
				return Expression.Call(base.Method, args[0], args[1], args[2], args[3]);
			}
			return Expression.Call(base.Method, ExpressionUtils.ReturnObject<Expression>(this._arg0), this._arg1, this._arg2, this._arg3);
		}

		// Token: 0x040001E9 RID: 489
		private object _arg0;

		// Token: 0x040001EA RID: 490
		private readonly Expression _arg1;

		// Token: 0x040001EB RID: 491
		private readonly Expression _arg2;

		// Token: 0x040001EC RID: 492
		private readonly Expression _arg3;
	}
}
