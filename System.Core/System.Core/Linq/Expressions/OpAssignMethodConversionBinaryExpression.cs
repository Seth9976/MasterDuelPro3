using System;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x0200005E RID: 94
	internal sealed class OpAssignMethodConversionBinaryExpression : MethodBinaryExpression
	{
		// Token: 0x060002F0 RID: 752 RVA: 0x0000CA83 File Offset: 0x0000AC83
		internal OpAssignMethodConversionBinaryExpression(ExpressionType nodeType, Expression left, Expression right, Type type, MethodInfo method, LambdaExpression conversion)
			: base(nodeType, left, right, type, method)
		{
			this._conversion = conversion;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000CA9A File Offset: 0x0000AC9A
		internal override LambdaExpression GetConversion()
		{
			return this._conversion;
		}

		// Token: 0x04000114 RID: 276
		private readonly LambdaExpression _conversion;
	}
}
