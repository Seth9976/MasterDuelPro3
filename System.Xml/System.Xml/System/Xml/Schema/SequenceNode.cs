using System;
using System.Collections.Generic;

namespace System.Xml.Schema
{
	// Token: 0x02000222 RID: 546
	internal sealed class SequenceNode : InteriorNode
	{
		// Token: 0x06001AB3 RID: 6835 RVA: 0x0009AA18 File Offset: 0x00098C18
		public override void ConstructPos(BitSet firstpos, BitSet lastpos, BitSet[] followpos)
		{
			Stack<SequenceNode.SequenceConstructPosContext> stack = new Stack<SequenceNode.SequenceConstructPosContext>();
			SequenceNode.SequenceConstructPosContext sequenceConstructPosContext = new SequenceNode.SequenceConstructPosContext(this, firstpos, lastpos);
			SequenceNode sequenceNode;
			for (;;)
			{
				sequenceNode = sequenceConstructPosContext.this_;
				sequenceConstructPosContext.lastposLeft = new BitSet(lastpos.Count);
				if (!(sequenceNode.LeftChild is SequenceNode))
				{
					break;
				}
				stack.Push(sequenceConstructPosContext);
				sequenceConstructPosContext = new SequenceNode.SequenceConstructPosContext((SequenceNode)sequenceNode.LeftChild, sequenceConstructPosContext.firstpos, sequenceConstructPosContext.lastposLeft);
			}
			sequenceNode.LeftChild.ConstructPos(sequenceConstructPosContext.firstpos, sequenceConstructPosContext.lastposLeft, followpos);
			for (;;)
			{
				sequenceConstructPosContext.firstposRight = new BitSet(firstpos.Count);
				sequenceNode.RightChild.ConstructPos(sequenceConstructPosContext.firstposRight, sequenceConstructPosContext.lastpos, followpos);
				if (sequenceNode.LeftChild.IsNullable && !sequenceNode.RightChild.IsRangeNode)
				{
					sequenceConstructPosContext.firstpos.Or(sequenceConstructPosContext.firstposRight);
				}
				if (sequenceNode.RightChild.IsNullable)
				{
					sequenceConstructPosContext.lastpos.Or(sequenceConstructPosContext.lastposLeft);
				}
				for (int num = sequenceConstructPosContext.lastposLeft.NextSet(-1); num != -1; num = sequenceConstructPosContext.lastposLeft.NextSet(num))
				{
					followpos[num].Or(sequenceConstructPosContext.firstposRight);
				}
				if (sequenceNode.RightChild.IsRangeNode)
				{
					((LeafRangeNode)sequenceNode.RightChild).NextIteration = sequenceConstructPosContext.firstpos.Clone();
				}
				if (stack.Count == 0)
				{
					break;
				}
				sequenceConstructPosContext = stack.Pop();
				sequenceNode = sequenceConstructPosContext.this_;
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06001AB4 RID: 6836 RVA: 0x0009AB80 File Offset: 0x00098D80
		public override bool IsNullable
		{
			get
			{
				SequenceNode sequenceNode = this;
				while (!sequenceNode.RightChild.IsRangeNode || !(((LeafRangeNode)sequenceNode.RightChild).Min == 0m))
				{
					if (!sequenceNode.RightChild.IsNullable && !sequenceNode.RightChild.IsRangeNode)
					{
						return false;
					}
					SyntaxTreeNode leftChild = sequenceNode.LeftChild;
					sequenceNode = leftChild as SequenceNode;
					if (sequenceNode == null)
					{
						return leftChild.IsNullable;
					}
				}
				return true;
			}
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x0009ABED File Offset: 0x00098DED
		public override void ExpandTree(InteriorNode parent, SymbolsDictionary symbols, Positions positions)
		{
			base.ExpandTreeNoRecursive(parent, symbols, positions);
		}

		// Token: 0x02000223 RID: 547
		private struct SequenceConstructPosContext
		{
			// Token: 0x06001AB7 RID: 6839 RVA: 0x0009AC00 File Offset: 0x00098E00
			public SequenceConstructPosContext(SequenceNode node, BitSet firstpos, BitSet lastpos)
			{
				this.this_ = node;
				this.firstpos = firstpos;
				this.lastpos = lastpos;
				this.lastposLeft = null;
				this.firstposRight = null;
			}

			// Token: 0x04000B6C RID: 2924
			public SequenceNode this_;

			// Token: 0x04000B6D RID: 2925
			public BitSet firstpos;

			// Token: 0x04000B6E RID: 2926
			public BitSet lastpos;

			// Token: 0x04000B6F RID: 2927
			public BitSet lastposLeft;

			// Token: 0x04000B70 RID: 2928
			public BitSet firstposRight;
		}
	}
}
