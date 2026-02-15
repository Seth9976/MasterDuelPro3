using System;

namespace System.Xml.Schema
{
	// Token: 0x02000227 RID: 551
	internal sealed class StarNode : InteriorNode
	{
		// Token: 0x06001AC3 RID: 6851 RVA: 0x0009AD54 File Offset: 0x00098F54
		public override void ConstructPos(BitSet firstpos, BitSet lastpos, BitSet[] followpos)
		{
			base.LeftChild.ConstructPos(firstpos, lastpos, followpos);
			for (int num = lastpos.NextSet(-1); num != -1; num = lastpos.NextSet(num))
			{
				followpos[num].Or(firstpos);
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001AC4 RID: 6852 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		public override bool IsNullable
		{
			get
			{
				return true;
			}
		}
	}
}
