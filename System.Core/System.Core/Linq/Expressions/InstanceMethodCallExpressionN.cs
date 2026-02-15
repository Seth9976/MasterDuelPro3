using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x020000BF RID: 191
	internal sealed class InstanceMethodCallExpressionN : InstanceMethodCallExpression, IArgumentProvider
	{
		// Token: 0x06000630 RID: 1584 RVA: 0x000164B9 File Offset: 0x000146B9
		public InstanceMethodCallExpressionN(MethodInfo method, Expression instance, IReadOnlyList<Expression> args)
			: base(method, instance)
		{
			this._arguments = args;
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x000164CA File Offset: 0x000146CA
		public override Expression GetArgument(int index)
		{
			return this._arguments[index];
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x000164D8 File Offset: 0x000146D8
		public override int ArgumentCount
		{
			get
			{
				return this._arguments.Count;
			}
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x000164E5 File Offset: 0x000146E5
		internal override MethodCallExpression Rewrite(Expression instance, IReadOnlyList<Expression> args)
		{
			return Expression.Call(instance, base.Method, args ?? this._arguments);
		}

		// Token: 0x040001E2 RID: 482
		private IReadOnlyList<Expression> _arguments;
	}
}
