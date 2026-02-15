using System;

namespace System.Linq.Expressions
{
	// Token: 0x0200005F RID: 95
	internal class SimpleBinaryExpression : BinaryExpression
	{
		// Token: 0x060002F2 RID: 754 RVA: 0x0000CAA2 File Offset: 0x0000ACA2
		internal SimpleBinaryExpression(ExpressionType nodeType, Expression left, Expression right, Type type)
			: base(left, right)
		{
			this.NodeType = nodeType;
			this.Type = type;
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000CABB File Offset: 0x0000ACBB
		public sealed override ExpressionType NodeType { get; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000CAC3 File Offset: 0x0000ACC3
		public sealed override Type Type { get; }
	}
}
