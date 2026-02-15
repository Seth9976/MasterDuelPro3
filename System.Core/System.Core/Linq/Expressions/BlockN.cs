using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x02000082 RID: 130
	internal class BlockN : BlockExpression
	{
		// Token: 0x06000400 RID: 1024 RVA: 0x00012C62 File Offset: 0x00010E62
		internal BlockN(IReadOnlyList<Expression> expressions)
		{
			this._expressions = expressions;
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00012C71 File Offset: 0x00010E71
		internal override Expression GetExpression(int index)
		{
			return this._expressions[index];
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x00012C7F File Offset: 0x00010E7F
		internal override int ExpressionCount
		{
			get
			{
				return this._expressions.Count;
			}
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00012C8C File Offset: 0x00010E8C
		internal override ReadOnlyCollection<Expression> GetOrMakeExpressions()
		{
			return ExpressionUtils.ReturnReadOnly<Expression>(ref this._expressions);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00012C99 File Offset: 0x00010E99
		internal override BlockExpression Rewrite(ReadOnlyCollection<ParameterExpression> variables, Expression[] args)
		{
			return new BlockN(args);
		}

		// Token: 0x0400012B RID: 299
		private IReadOnlyList<Expression> _expressions;
	}
}
