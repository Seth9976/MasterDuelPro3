using System;
using System.Collections.Generic;

namespace System.Data
{
	// Token: 0x02000077 RID: 119
	internal sealed class ZeroOpNode : ExpressionNode
	{
		// Token: 0x06000690 RID: 1680 RVA: 0x000204DD File Offset: 0x0001E6DD
		internal ZeroOpNode(int op)
			: base(null)
		{
			this._op = op;
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x00003FD2 File Offset: 0x000021D2
		internal override void Bind(DataTable table, List<DataColumn> list)
		{
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x000204F0 File Offset: 0x0001E6F0
		internal override object Eval()
		{
			switch (this._op)
			{
			case 32:
				return DBNull.Value;
			case 33:
				return true;
			case 34:
				return false;
			default:
				return DBNull.Value;
			}
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0001D6D6 File Offset: 0x0001B8D6
		internal override object Eval(DataRow row, DataRowVersion version)
		{
			return this.Eval();
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0001D6D6 File Offset: 0x0001B8D6
		internal override object Eval(int[] recordNos)
		{
			return this.Eval();
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0000593A File Offset: 0x00003B3A
		internal override bool IsConstant()
		{
			return true;
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0000593A File Offset: 0x00003B3A
		internal override bool IsTableConstant()
		{
			return true;
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x00011ED5 File Offset: 0x000100D5
		internal override bool HasLocalAggregate()
		{
			return false;
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00011ED5 File Offset: 0x000100D5
		internal override bool HasRemoteAggregate()
		{
			return false;
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0000207F File Offset: 0x0000027F
		internal override ExpressionNode Optimize()
		{
			return this;
		}

		// Token: 0x04000279 RID: 633
		internal readonly int _op;
	}
}
