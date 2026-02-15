using System;

namespace System.Linq.Expressions
{
	// Token: 0x0200005A RID: 90
	internal sealed class LogicalBinaryExpression : BinaryExpression
	{
		// Token: 0x060002E2 RID: 738 RVA: 0x0000C9FC File Offset: 0x0000ABFC
		internal LogicalBinaryExpression(ExpressionType nodeType, Expression left, Expression right)
			: base(left, right)
		{
			this.NodeType = nodeType;
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x0000CA0D File Offset: 0x0000AC0D
		public sealed override Type Type
		{
			get
			{
				return typeof(bool);
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x0000CA19 File Offset: 0x0000AC19
		public sealed override ExpressionType NodeType { get; }
	}
}
