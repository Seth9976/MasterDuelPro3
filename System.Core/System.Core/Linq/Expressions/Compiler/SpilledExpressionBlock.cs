using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic.Utils;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x02000109 RID: 265
	internal sealed class SpilledExpressionBlock : BlockN
	{
		// Token: 0x06000928 RID: 2344 RVA: 0x00024067 File Offset: 0x00022267
		internal SpilledExpressionBlock(IReadOnlyList<Expression> expressions)
			: base(expressions)
		{
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x00012A02 File Offset: 0x00010C02
		[ExcludeFromCodeCoverage]
		internal override BlockExpression Rewrite(ReadOnlyCollection<ParameterExpression> variables, Expression[] args)
		{
			throw ContractUtils.Unreachable;
		}
	}
}
