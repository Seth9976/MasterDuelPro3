using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Linq.Expressions
{
	// Token: 0x02000086 RID: 134
	internal sealed class ScopeWithType : ScopeN
	{
		// Token: 0x06000415 RID: 1045 RVA: 0x00012DBF File Offset: 0x00010FBF
		internal ScopeWithType(IReadOnlyList<ParameterExpression> variables, IReadOnlyList<Expression> expressions, Type type)
			: base(variables, expressions)
		{
			this.Type = type;
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x00012DD0 File Offset: 0x00010FD0
		public sealed override Type Type { get; }

		// Token: 0x06000417 RID: 1047 RVA: 0x00012DD8 File Offset: 0x00010FD8
		internal override BlockExpression Rewrite(ReadOnlyCollection<ParameterExpression> variables, Expression[] args)
		{
			if (args == null)
			{
				Expression.ValidateVariables(variables, "variables");
				return new ScopeWithType(variables, base.Body, this.Type);
			}
			return new ScopeWithType(base.ReuseOrValidateVariables(variables), args, this.Type);
		}
	}
}
