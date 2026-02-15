using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x02000220 RID: 544
	internal class NamespaceListNode : SyntaxTreeNode
	{
		// Token: 0x06001AA7 RID: 6823 RVA: 0x0009A872 File Offset: 0x00098A72
		public NamespaceListNode(NamespaceList namespaceList, object particle)
		{
			this.namespaceList = namespaceList;
			this.particle = particle;
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x0009A888 File Offset: 0x00098A88
		public virtual ICollection GetResolvedSymbols(SymbolsDictionary symbols)
		{
			return symbols.GetNamespaceListSymbols(this.namespaceList);
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x0009A898 File Offset: 0x00098A98
		public override void ExpandTree(InteriorNode parent, SymbolsDictionary symbols, Positions positions)
		{
			SyntaxTreeNode syntaxTreeNode = null;
			foreach (object obj in this.GetResolvedSymbols(symbols))
			{
				int num = (int)obj;
				if (symbols.GetParticle(num) != this.particle)
				{
					symbols.IsUpaEnforced = false;
				}
				LeafNode leafNode = new LeafNode(positions.Add(num, this.particle));
				if (syntaxTreeNode == null)
				{
					syntaxTreeNode = leafNode;
				}
				else
				{
					syntaxTreeNode = new ChoiceNode
					{
						LeftChild = syntaxTreeNode,
						RightChild = leafNode
					};
				}
			}
			if (parent.LeftChild == this)
			{
				parent.LeftChild = syntaxTreeNode;
				return;
			}
			parent.RightChild = syntaxTreeNode;
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x000356D5 File Offset: 0x000338D5
		public override void ConstructPos(BitSet firstpos, BitSet lastpos, BitSet[] followpos)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06001AAB RID: 6827 RVA: 0x000356D5 File Offset: 0x000338D5
		public override bool IsNullable
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x04000B68 RID: 2920
		protected NamespaceList namespaceList;

		// Token: 0x04000B69 RID: 2921
		protected object particle;
	}
}
