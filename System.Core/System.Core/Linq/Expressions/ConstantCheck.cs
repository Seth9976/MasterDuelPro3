using System;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x0200008C RID: 140
	internal static class ConstantCheck
	{
		// Token: 0x06000435 RID: 1077 RVA: 0x00013068 File Offset: 0x00011268
		internal static bool IsNull(Expression e)
		{
			ExpressionType nodeType = e.NodeType;
			if (nodeType != ExpressionType.Constant)
			{
				return nodeType == ExpressionType.Default && e.Type.IsNullableOrReferenceType();
			}
			return ((ConstantExpression)e).Value == null;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x000130A4 File Offset: 0x000112A4
		internal static AnalyzeTypeIsResult AnalyzeTypeIs(TypeBinaryExpression typeIs)
		{
			return ConstantCheck.AnalyzeTypeIs(typeIs.Expression, typeIs.TypeOperand);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x000130B8 File Offset: 0x000112B8
		private static AnalyzeTypeIsResult AnalyzeTypeIs(Expression operand, Type testType)
		{
			Type type = operand.Type;
			if (type == typeof(void))
			{
				if (!(testType == typeof(void)))
				{
					return AnalyzeTypeIsResult.KnownFalse;
				}
				return AnalyzeTypeIsResult.KnownTrue;
			}
			else
			{
				if (testType == typeof(void) || testType.IsPointer)
				{
					return AnalyzeTypeIsResult.KnownFalse;
				}
				Type nonNullableType = type.GetNonNullableType();
				if (!testType.GetNonNullableType().IsAssignableFrom(nonNullableType))
				{
					return AnalyzeTypeIsResult.Unknown;
				}
				if (type.IsValueType && !type.IsNullableType())
				{
					return AnalyzeTypeIsResult.KnownTrue;
				}
				return AnalyzeTypeIsResult.KnownAssignable;
			}
		}
	}
}
