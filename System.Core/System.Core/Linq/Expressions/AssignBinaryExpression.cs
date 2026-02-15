using System;

namespace System.Linq.Expressions
{
	// Token: 0x0200005B RID: 91
	internal class AssignBinaryExpression : BinaryExpression
	{
		// Token: 0x060002E5 RID: 741 RVA: 0x0000CA21 File Offset: 0x0000AC21
		internal AssignBinaryExpression(Expression left, Expression right)
			: base(left, right)
		{
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000CA2B File Offset: 0x0000AC2B
		public static AssignBinaryExpression Make(Expression left, Expression right, bool byRef)
		{
			if (byRef)
			{
				return new ByRefAssignBinaryExpression(left, right);
			}
			return new AssignBinaryExpression(left, right);
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x0000B252 File Offset: 0x00009452
		internal virtual bool IsByRef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000CA3F File Offset: 0x0000AC3F
		public sealed override Type Type
		{
			get
			{
				return base.Left.Type;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000CA4C File Offset: 0x0000AC4C
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Assign;
			}
		}
	}
}
