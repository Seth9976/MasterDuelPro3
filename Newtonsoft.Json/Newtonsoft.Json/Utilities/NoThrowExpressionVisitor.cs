using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000C0 RID: 192
	[NullableContext(1)]
	[Nullable(0)]
	internal class NoThrowExpressionVisitor : ExpressionVisitor
	{
		// Token: 0x060005F7 RID: 1527 RVA: 0x0001F973 File Offset: 0x0001DB73
		protected override Expression VisitConditional(ConditionalExpression node)
		{
			if (node.IfFalse.NodeType == ExpressionType.Throw)
			{
				return Expression.Condition(node.Test, node.IfTrue, Expression.Constant(NoThrowExpressionVisitor.ErrorResult));
			}
			return base.VisitConditional(node);
		}

		// Token: 0x0400043C RID: 1084
		internal static readonly object ErrorResult = new object();
	}
}
