using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x02000084 RID: 132
	internal sealed class Scope1 : ScopeExpression
	{
		// Token: 0x06000409 RID: 1033 RVA: 0x00012CE6 File Offset: 0x00010EE6
		internal Scope1(IReadOnlyList<ParameterExpression> variables, Expression body)
			: this(variables, body)
		{
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00012CF0 File Offset: 0x00010EF0
		private Scope1(IReadOnlyList<ParameterExpression> variables, object body)
			: base(variables)
		{
			this._body = body;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00012D00 File Offset: 0x00010F00
		internal override Expression GetExpression(int index)
		{
			if (index == 0)
			{
				return ExpressionUtils.ReturnObject<Expression>(this._body);
			}
			throw Error.ArgumentOutOfRange("index");
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x00009F9F File Offset: 0x0000819F
		internal override int ExpressionCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00012D1B File Offset: 0x00010F1B
		internal override ReadOnlyCollection<Expression> GetOrMakeExpressions()
		{
			return BlockExpression.ReturnReadOnlyExpressions(this, ref this._body);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00012D29 File Offset: 0x00010F29
		internal override BlockExpression Rewrite(ReadOnlyCollection<ParameterExpression> variables, Expression[] args)
		{
			if (args == null)
			{
				Expression.ValidateVariables(variables, "variables");
				return new Scope1(variables, this._body);
			}
			return new Scope1(base.ReuseOrValidateVariables(variables), args[0]);
		}

		// Token: 0x0400012D RID: 301
		private object _body;
	}
}
