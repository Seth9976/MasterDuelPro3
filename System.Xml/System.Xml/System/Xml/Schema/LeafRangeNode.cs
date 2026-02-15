using System;

namespace System.Xml.Schema
{
	// Token: 0x02000228 RID: 552
	internal sealed class LeafRangeNode : LeafNode
	{
		// Token: 0x06001AC6 RID: 6854 RVA: 0x0009AD8E File Offset: 0x00098F8E
		public LeafRangeNode(decimal min, decimal max)
			: this(-1, min, max)
		{
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x0009AD99 File Offset: 0x00098F99
		public LeafRangeNode(int pos, decimal min, decimal max)
			: base(pos)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001AC8 RID: 6856 RVA: 0x0009ADB0 File Offset: 0x00098FB0
		public decimal Max
		{
			get
			{
				return this.max;
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06001AC9 RID: 6857 RVA: 0x0009ADB8 File Offset: 0x00098FB8
		public decimal Min
		{
			get
			{
				return this.min;
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001ACA RID: 6858 RVA: 0x0009ADC0 File Offset: 0x00098FC0
		// (set) Token: 0x06001ACB RID: 6859 RVA: 0x0009ADC8 File Offset: 0x00098FC8
		public BitSet NextIteration
		{
			get
			{
				return this.nextIteration;
			}
			set
			{
				this.nextIteration = value;
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001ACC RID: 6860 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		public override bool IsRangeNode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x0009ADD1 File Offset: 0x00098FD1
		public override void ExpandTree(InteriorNode parent, SymbolsDictionary symbols, Positions positions)
		{
			if (parent.LeftChild.IsNullable)
			{
				this.min = 0m;
			}
		}

		// Token: 0x04000B71 RID: 2929
		private decimal min;

		// Token: 0x04000B72 RID: 2930
		private decimal max;

		// Token: 0x04000B73 RID: 2931
		private BitSet nextIteration;
	}
}
