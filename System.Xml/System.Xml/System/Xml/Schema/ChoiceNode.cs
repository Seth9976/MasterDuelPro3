using System;

namespace System.Xml.Schema
{
	// Token: 0x02000224 RID: 548
	internal sealed class ChoiceNode : InteriorNode
	{
		// Token: 0x06001AB8 RID: 6840 RVA: 0x0009AC28 File Offset: 0x00098E28
		private static void ConstructChildPos(SyntaxTreeNode child, BitSet firstpos, BitSet lastpos, BitSet[] followpos)
		{
			BitSet bitSet = new BitSet(firstpos.Count);
			BitSet bitSet2 = new BitSet(lastpos.Count);
			child.ConstructPos(bitSet, bitSet2, followpos);
			firstpos.Or(bitSet);
			lastpos.Or(bitSet2);
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x0009AC64 File Offset: 0x00098E64
		public override void ConstructPos(BitSet firstpos, BitSet lastpos, BitSet[] followpos)
		{
			BitSet bitSet = new BitSet(firstpos.Count);
			BitSet bitSet2 = new BitSet(lastpos.Count);
			ChoiceNode choiceNode = this;
			SyntaxTreeNode leftChild;
			do
			{
				ChoiceNode.ConstructChildPos(choiceNode.RightChild, bitSet, bitSet2, followpos);
				leftChild = choiceNode.LeftChild;
				choiceNode = leftChild as ChoiceNode;
			}
			while (choiceNode != null);
			leftChild.ConstructPos(firstpos, lastpos, followpos);
			firstpos.Or(bitSet);
			lastpos.Or(bitSet2);
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001ABA RID: 6842 RVA: 0x0009ACC4 File Offset: 0x00098EC4
		public override bool IsNullable
		{
			get
			{
				ChoiceNode choiceNode = this;
				while (!choiceNode.RightChild.IsNullable)
				{
					SyntaxTreeNode leftChild = choiceNode.LeftChild;
					choiceNode = leftChild as ChoiceNode;
					if (choiceNode == null)
					{
						return leftChild.IsNullable;
					}
				}
				return true;
			}
		}

		// Token: 0x06001ABB RID: 6843 RVA: 0x0009ABED File Offset: 0x00098DED
		public override void ExpandTree(InteriorNode parent, SymbolsDictionary symbols, Positions positions)
		{
			base.ExpandTreeNoRecursive(parent, symbols, positions);
		}
	}
}
