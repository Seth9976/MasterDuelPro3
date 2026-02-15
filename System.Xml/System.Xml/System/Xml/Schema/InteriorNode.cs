using System;
using System.Collections.Generic;

namespace System.Xml.Schema
{
	// Token: 0x02000221 RID: 545
	internal abstract class InteriorNode : SyntaxTreeNode
	{
		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06001AAC RID: 6828 RVA: 0x0009A94C File Offset: 0x00098B4C
		// (set) Token: 0x06001AAD RID: 6829 RVA: 0x0009A954 File Offset: 0x00098B54
		public SyntaxTreeNode LeftChild
		{
			get
			{
				return this.leftChild;
			}
			set
			{
				this.leftChild = value;
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06001AAE RID: 6830 RVA: 0x0009A95D File Offset: 0x00098B5D
		// (set) Token: 0x06001AAF RID: 6831 RVA: 0x0009A965 File Offset: 0x00098B65
		public SyntaxTreeNode RightChild
		{
			get
			{
				return this.rightChild;
			}
			set
			{
				this.rightChild = value;
			}
		}

		// Token: 0x06001AB0 RID: 6832 RVA: 0x0009A970 File Offset: 0x00098B70
		protected void ExpandTreeNoRecursive(InteriorNode parent, SymbolsDictionary symbols, Positions positions)
		{
			Stack<InteriorNode> stack = new Stack<InteriorNode>();
			InteriorNode interiorNode = this;
			while (interiorNode.leftChild is ChoiceNode || interiorNode.leftChild is SequenceNode)
			{
				stack.Push(interiorNode);
				interiorNode = (InteriorNode)interiorNode.leftChild;
			}
			interiorNode.leftChild.ExpandTree(interiorNode, symbols, positions);
			for (;;)
			{
				if (interiorNode.rightChild != null)
				{
					interiorNode.rightChild.ExpandTree(interiorNode, symbols, positions);
				}
				if (stack.Count == 0)
				{
					break;
				}
				interiorNode = stack.Pop();
			}
		}

		// Token: 0x06001AB1 RID: 6833 RVA: 0x0009A9E9 File Offset: 0x00098BE9
		public override void ExpandTree(InteriorNode parent, SymbolsDictionary symbols, Positions positions)
		{
			this.leftChild.ExpandTree(this, symbols, positions);
			if (this.rightChild != null)
			{
				this.rightChild.ExpandTree(this, symbols, positions);
			}
		}

		// Token: 0x04000B6A RID: 2922
		private SyntaxTreeNode leftChild;

		// Token: 0x04000B6B RID: 2923
		private SyntaxTreeNode rightChild;
	}
}
