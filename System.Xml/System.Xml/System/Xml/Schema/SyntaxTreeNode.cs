using System;

namespace System.Xml.Schema
{
	// Token: 0x0200021E RID: 542
	internal abstract class SyntaxTreeNode
	{
		// Token: 0x06001A9C RID: 6812
		public abstract void ExpandTree(InteriorNode parent, SymbolsDictionary symbols, Positions positions);

		// Token: 0x06001A9D RID: 6813
		public abstract void ConstructPos(BitSet firstpos, BitSet lastpos, BitSet[] followpos);

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06001A9E RID: 6814
		public abstract bool IsNullable { get; }

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06001A9F RID: 6815 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		public virtual bool IsRangeNode
		{
			get
			{
				return false;
			}
		}
	}
}
