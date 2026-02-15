using System;

namespace System.Xml.Schema
{
	// Token: 0x02000206 RID: 518
	internal class AxisElement
	{
		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x060019EA RID: 6634 RVA: 0x00097E6B File Offset: 0x0009606B
		internal DoubleLinkAxis CurNode
		{
			get
			{
				return this.curNode;
			}
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x00097E74 File Offset: 0x00096074
		internal AxisElement(DoubleLinkAxis node, int depth)
		{
			this.curNode = node;
			this.curDepth = depth;
			this.rootDepth = depth;
			this.isMatch = false;
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x00097EA8 File Offset: 0x000960A8
		internal void SetDepth(int depth)
		{
			this.curDepth = depth;
			this.rootDepth = depth;
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x00097EC8 File Offset: 0x000960C8
		internal void MoveToParent(int depth, ForwardAxis parent)
		{
			if (depth != this.curDepth - 1)
			{
				if (depth == this.curDepth && this.isMatch)
				{
					this.isMatch = false;
				}
				return;
			}
			if (this.curNode.Input == parent.RootNode && parent.IsDss)
			{
				this.curNode = parent.RootNode;
				this.rootDepth = (this.curDepth = -1);
				return;
			}
			if (this.curNode.Input != null)
			{
				this.curNode = (DoubleLinkAxis)this.curNode.Input;
				this.curDepth--;
				return;
			}
		}

		// Token: 0x060019EE RID: 6638 RVA: 0x00097F64 File Offset: 0x00096164
		internal bool MoveToChild(string name, string URN, int depth, ForwardAxis parent)
		{
			if (Asttree.IsAttribute(this.curNode))
			{
				return false;
			}
			if (this.isMatch)
			{
				this.isMatch = false;
			}
			if (!AxisStack.Equal(this.curNode.Name, this.curNode.Urn, name, URN))
			{
				return false;
			}
			if (this.curDepth == -1)
			{
				this.SetDepth(depth);
			}
			else if (depth > this.curDepth)
			{
				return false;
			}
			if (this.curNode == parent.TopNode)
			{
				this.isMatch = true;
				return true;
			}
			DoubleLinkAxis doubleLinkAxis = (DoubleLinkAxis)this.curNode.Next;
			if (Asttree.IsAttribute(doubleLinkAxis))
			{
				this.isMatch = true;
				return false;
			}
			this.curNode = doubleLinkAxis;
			this.curDepth++;
			return false;
		}

		// Token: 0x04000B06 RID: 2822
		internal DoubleLinkAxis curNode;

		// Token: 0x04000B07 RID: 2823
		internal int rootDepth;

		// Token: 0x04000B08 RID: 2824
		internal int curDepth;

		// Token: 0x04000B09 RID: 2825
		internal bool isMatch;
	}
}
