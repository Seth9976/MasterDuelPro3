using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x02000085 RID: 133
	internal class ScopeN : ScopeExpression
	{
		// Token: 0x0600040F RID: 1039 RVA: 0x00012D55 File Offset: 0x00010F55
		internal ScopeN(IReadOnlyList<ParameterExpression> variables, IReadOnlyList<Expression> body)
			: base(variables)
		{
			this._body = body;
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x00012D65 File Offset: 0x00010F65
		protected IReadOnlyList<Expression> Body
		{
			get
			{
				return this._body;
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00012D6D File Offset: 0x00010F6D
		internal override Expression GetExpression(int index)
		{
			return this._body[index];
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x00012D7B File Offset: 0x00010F7B
		internal override int ExpressionCount
		{
			get
			{
				return this._body.Count;
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00012D88 File Offset: 0x00010F88
		internal override ReadOnlyCollection<Expression> GetOrMakeExpressions()
		{
			return ExpressionUtils.ReturnReadOnly<Expression>(ref this._body);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00012D95 File Offset: 0x00010F95
		internal override BlockExpression Rewrite(ReadOnlyCollection<ParameterExpression> variables, Expression[] args)
		{
			if (args == null)
			{
				Expression.ValidateVariables(variables, "variables");
				return new ScopeN(variables, this._body);
			}
			return new ScopeN(base.ReuseOrValidateVariables(variables), args);
		}

		// Token: 0x0400012E RID: 302
		private IReadOnlyList<Expression> _body;
	}
}
