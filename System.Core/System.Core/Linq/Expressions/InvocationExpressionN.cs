using System;
using System.Collections.Generic;

namespace System.Linq.Expressions
{
	// Token: 0x020000A0 RID: 160
	internal sealed class InvocationExpressionN : InvocationExpression
	{
		// Token: 0x06000584 RID: 1412 RVA: 0x0001584B File Offset: 0x00013A4B
		public InvocationExpressionN(Expression lambda, IReadOnlyList<Expression> arguments, Type returnType)
			: base(lambda, returnType)
		{
			this._arguments = arguments;
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0001585C File Offset: 0x00013A5C
		public override Expression GetArgument(int index)
		{
			return this._arguments[index];
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x0001586A File Offset: 0x00013A6A
		public override int ArgumentCount
		{
			get
			{
				return this._arguments.Count;
			}
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00015878 File Offset: 0x00013A78
		internal override InvocationExpression Rewrite(Expression lambda, Expression[] arguments)
		{
			return Expression.Invoke(lambda, arguments ?? this._arguments);
		}

		// Token: 0x040001AE RID: 430
		private IReadOnlyList<Expression> _arguments;
	}
}
