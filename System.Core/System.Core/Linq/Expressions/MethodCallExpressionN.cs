using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x020000BE RID: 190
	internal sealed class MethodCallExpressionN : MethodCallExpression, IArgumentProvider
	{
		// Token: 0x0600062C RID: 1580 RVA: 0x00016476 File Offset: 0x00014676
		public MethodCallExpressionN(MethodInfo method, IReadOnlyList<Expression> args)
			: base(method)
		{
			this._arguments = args;
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00016486 File Offset: 0x00014686
		public override Expression GetArgument(int index)
		{
			return this._arguments[index];
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x00016494 File Offset: 0x00014694
		public override int ArgumentCount
		{
			get
			{
				return this._arguments.Count;
			}
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x000164A1 File Offset: 0x000146A1
		internal override MethodCallExpression Rewrite(Expression instance, IReadOnlyList<Expression> args)
		{
			return Expression.Call(base.Method, args ?? this._arguments);
		}

		// Token: 0x040001E1 RID: 481
		private IReadOnlyList<Expression> _arguments;
	}
}
