using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x020000C6 RID: 198
	internal sealed class InstanceMethodCallExpression0 : InstanceMethodCallExpression, IArgumentProvider
	{
		// Token: 0x0600064C RID: 1612 RVA: 0x00016865 File Offset: 0x00014A65
		public InstanceMethodCallExpression0(MethodInfo method, Expression instance)
			: base(method, instance)
		{
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x000158A2 File Offset: 0x00013AA2
		public override Expression GetArgument(int index)
		{
			throw new ArgumentOutOfRangeException("index");
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x0000B252 File Offset: 0x00009452
		public override int ArgumentCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x0001686F File Offset: 0x00014A6F
		internal override MethodCallExpression Rewrite(Expression instance, IReadOnlyList<Expression> args)
		{
			return Expression.Call(instance, base.Method);
		}
	}
}
