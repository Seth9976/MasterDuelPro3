using System;
using System.Collections.Generic;
using System.Dynamic.Utils;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x020000C5 RID: 197
	internal sealed class MethodCallExpression5 : MethodCallExpression, IArgumentProvider
	{
		// Token: 0x06000648 RID: 1608 RVA: 0x0001676C File Offset: 0x0001496C
		public MethodCallExpression5(MethodInfo method, Expression arg0, Expression arg1, Expression arg2, Expression arg3, Expression arg4)
			: base(method)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = arg2;
			this._arg3 = arg3;
			this._arg4 = arg4;
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0001679C File Offset: 0x0001499C
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
			case 4:
				return this._arg4;
			default:
				throw new ArgumentOutOfRangeException("index");
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x00012C3B File Offset: 0x00010E3B
		public override int ArgumentCount
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x000167F8 File Offset: 0x000149F8
		internal override MethodCallExpression Rewrite(Expression instance, IReadOnlyList<Expression> args)
		{
			if (args != null)
			{
				return Expression.Call(base.Method, args[0], args[1], args[2], args[3], args[4]);
			}
			return Expression.Call(base.Method, ExpressionUtils.ReturnObject<Expression>(this._arg0), this._arg1, this._arg2, this._arg3, this._arg4);
		}

		// Token: 0x040001ED RID: 493
		private object _arg0;

		// Token: 0x040001EE RID: 494
		private readonly Expression _arg1;

		// Token: 0x040001EF RID: 495
		private readonly Expression _arg2;

		// Token: 0x040001F0 RID: 496
		private readonly Expression _arg3;

		// Token: 0x040001F1 RID: 497
		private readonly Expression _arg4;
	}
}
