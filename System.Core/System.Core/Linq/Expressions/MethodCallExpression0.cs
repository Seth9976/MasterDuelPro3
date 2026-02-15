using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x020000C0 RID: 192
	internal sealed class MethodCallExpression0 : MethodCallExpression, IArgumentProvider
	{
		// Token: 0x06000634 RID: 1588 RVA: 0x000164FE File Offset: 0x000146FE
		public MethodCallExpression0(MethodInfo method)
			: base(method)
		{
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x000158A2 File Offset: 0x00013AA2
		public override Expression GetArgument(int index)
		{
			throw new ArgumentOutOfRangeException("index");
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x0000B252 File Offset: 0x00009452
		public override int ArgumentCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00016507 File Offset: 0x00014707
		internal override MethodCallExpression Rewrite(Expression instance, IReadOnlyList<Expression> args)
		{
			return Expression.Call(base.Method);
		}
	}
}
