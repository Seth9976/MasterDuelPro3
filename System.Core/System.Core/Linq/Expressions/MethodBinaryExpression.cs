using System;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x02000060 RID: 96
	internal class MethodBinaryExpression : SimpleBinaryExpression
	{
		// Token: 0x060002F5 RID: 757 RVA: 0x0000CACB File Offset: 0x0000ACCB
		internal MethodBinaryExpression(ExpressionType nodeType, Expression left, Expression right, Type type, MethodInfo method)
			: base(nodeType, left, right, type)
		{
			this._method = method;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000CAE0 File Offset: 0x0000ACE0
		internal override MethodInfo GetMethod()
		{
			return this._method;
		}

		// Token: 0x04000117 RID: 279
		private readonly MethodInfo _method;
	}
}
