using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x02000083 RID: 131
	internal class ScopeExpression : BlockExpression
	{
		// Token: 0x06000405 RID: 1029 RVA: 0x00012CA1 File Offset: 0x00010EA1
		internal ScopeExpression(IReadOnlyList<ParameterExpression> variables)
		{
			this._variables = variables;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00012CB0 File Offset: 0x00010EB0
		internal override ReadOnlyCollection<ParameterExpression> GetOrMakeVariables()
		{
			return ExpressionUtils.ReturnReadOnly<ParameterExpression>(ref this._variables);
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x00012CBD File Offset: 0x00010EBD
		protected IReadOnlyList<ParameterExpression> VariablesList
		{
			get
			{
				return this._variables;
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00012CC5 File Offset: 0x00010EC5
		internal IReadOnlyList<ParameterExpression> ReuseOrValidateVariables(ReadOnlyCollection<ParameterExpression> variables)
		{
			if (variables != null && variables != this.VariablesList)
			{
				Expression.ValidateVariables(variables, "variables");
				return variables;
			}
			return this.VariablesList;
		}

		// Token: 0x0400012C RID: 300
		private IReadOnlyList<ParameterExpression> _variables;
	}
}
