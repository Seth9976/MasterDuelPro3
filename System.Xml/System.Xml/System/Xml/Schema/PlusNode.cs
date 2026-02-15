using System;

namespace System.Xml.Schema
{
	// Token: 0x02000225 RID: 549
	internal sealed class PlusNode : InteriorNode
	{
		// Token: 0x06001ABD RID: 6845 RVA: 0x0009ACFC File Offset: 0x00098EFC
		public override void ConstructPos(BitSet firstpos, BitSet lastpos, BitSet[] followpos)
		{
			base.LeftChild.ConstructPos(firstpos, lastpos, followpos);
			for (int num = lastpos.NextSet(-1); num != -1; num = lastpos.NextSet(num))
			{
				followpos[num].Or(firstpos);
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001ABE RID: 6846 RVA: 0x0009AD36 File Offset: 0x00098F36
		public override bool IsNullable
		{
			get
			{
				return base.LeftChild.IsNullable;
			}
		}
	}
}
