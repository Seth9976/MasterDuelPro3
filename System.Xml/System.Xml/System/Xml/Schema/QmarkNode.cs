using System;

namespace System.Xml.Schema
{
	// Token: 0x02000226 RID: 550
	internal sealed class QmarkNode : InteriorNode
	{
		// Token: 0x06001AC0 RID: 6848 RVA: 0x0009AD43 File Offset: 0x00098F43
		public override void ConstructPos(BitSet firstpos, BitSet lastpos, BitSet[] followpos)
		{
			base.LeftChild.ConstructPos(firstpos, lastpos, followpos);
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001AC1 RID: 6849 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		public override bool IsNullable
		{
			get
			{
				return true;
			}
		}
	}
}
