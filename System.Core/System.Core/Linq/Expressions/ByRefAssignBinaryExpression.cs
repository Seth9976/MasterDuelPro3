using System;

namespace System.Linq.Expressions
{
	// Token: 0x0200005C RID: 92
	internal class ByRefAssignBinaryExpression : AssignBinaryExpression
	{
		// Token: 0x060002EA RID: 746 RVA: 0x0000CA50 File Offset: 0x0000AC50
		internal ByRefAssignBinaryExpression(Expression left, Expression right)
			: base(left, right)
		{
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060002EB RID: 747 RVA: 0x00009F9F File Offset: 0x0000819F
		internal override bool IsByRef
		{
			get
			{
				return true;
			}
		}
	}
}
