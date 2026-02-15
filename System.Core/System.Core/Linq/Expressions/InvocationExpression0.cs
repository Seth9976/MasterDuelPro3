using System;

namespace System.Linq.Expressions
{
	// Token: 0x020000A1 RID: 161
	internal sealed class InvocationExpression0 : InvocationExpression
	{
		// Token: 0x06000588 RID: 1416 RVA: 0x00015898 File Offset: 0x00013A98
		public InvocationExpression0(Expression lambda, Type returnType)
			: base(lambda, returnType)
		{
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x000158A2 File Offset: 0x00013AA2
		public override Expression GetArgument(int index)
		{
			throw new ArgumentOutOfRangeException("index");
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x0000B252 File Offset: 0x00009452
		public override int ArgumentCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x000158AE File Offset: 0x00013AAE
		internal override InvocationExpression Rewrite(Expression lambda, Expression[] arguments)
		{
			return Expression.Invoke(lambda);
		}
	}
}
