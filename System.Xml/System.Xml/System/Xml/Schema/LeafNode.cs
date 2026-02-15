using System;

namespace System.Xml.Schema
{
	// Token: 0x0200021F RID: 543
	internal class LeafNode : SyntaxTreeNode
	{
		// Token: 0x06001AA1 RID: 6817 RVA: 0x0009A838 File Offset: 0x00098A38
		public LeafNode(int pos)
		{
			this.pos = pos;
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06001AA2 RID: 6818 RVA: 0x0009A847 File Offset: 0x00098A47
		// (set) Token: 0x06001AA3 RID: 6819 RVA: 0x0009A84F File Offset: 0x00098A4F
		public int Pos
		{
			get
			{
				return this.pos;
			}
			set
			{
				this.pos = value;
			}
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x0000A558 File Offset: 0x00008758
		public override void ExpandTree(InteriorNode parent, SymbolsDictionary symbols, Positions positions)
		{
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x0009A858 File Offset: 0x00098A58
		public override void ConstructPos(BitSet firstpos, BitSet lastpos, BitSet[] followpos)
		{
			firstpos.Set(this.pos);
			lastpos.Set(this.pos);
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06001AA6 RID: 6822 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		public override bool IsNullable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000B67 RID: 2919
		private int pos;
	}
}
