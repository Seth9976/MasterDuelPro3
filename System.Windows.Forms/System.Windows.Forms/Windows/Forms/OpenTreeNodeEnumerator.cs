using System;
using System.Collections;

namespace System.Windows.Forms
{
	// Token: 0x02000159 RID: 345
	internal class OpenTreeNodeEnumerator : IEnumerator
	{
		// Token: 0x06000D64 RID: 3428 RVA: 0x0003ADC8 File Offset: 0x00038FC8
		public OpenTreeNodeEnumerator(TreeNode start)
		{
			this.start = start;
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000D65 RID: 3429 RVA: 0x0003ADD7 File Offset: 0x00038FD7
		public object Current
		{
			get
			{
				return this.current;
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000D66 RID: 3430 RVA: 0x0003ADD7 File Offset: 0x00038FD7
		public TreeNode CurrentNode
		{
			get
			{
				return this.current;
			}
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x0003ADE0 File Offset: 0x00038FE0
		public bool MoveNext()
		{
			if (!this.started)
			{
				this.started = true;
				this.current = this.start;
				return this.current != null;
			}
			if (this.current.is_expanded && this.current.Nodes.Count > 0)
			{
				this.current = this.current.Nodes[0];
				return true;
			}
			TreeNode parent = this.current;
			TreeNode treeNode = this.current.NextNode;
			while (treeNode == null)
			{
				if (parent.parent == null)
				{
					return false;
				}
				parent = parent.parent;
				if (parent.parent != null)
				{
					treeNode = parent.NextNode;
				}
			}
			this.current = treeNode;
			return true;
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x0003AE8C File Offset: 0x0003908C
		public bool MovePrevious()
		{
			if (!this.started)
			{
				this.started = true;
				this.current = this.start;
				return this.current != null;
			}
			if (this.current.PrevNode != null)
			{
				TreeNode treeNode = this.current.PrevNode;
				for (TreeNode treeNode2 = treeNode; treeNode2 != null; treeNode2 = treeNode2.LastNode)
				{
					treeNode = treeNode2;
					if (!treeNode2.is_expanded)
					{
						break;
					}
				}
				this.current = treeNode;
				return true;
			}
			if (this.current.Parent == null)
			{
				return false;
			}
			this.current = this.current.Parent;
			return true;
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x0003AF19 File Offset: 0x00039119
		public void Reset()
		{
			this.started = false;
		}

		// Token: 0x04000870 RID: 2160
		private TreeNode start;

		// Token: 0x04000871 RID: 2161
		private TreeNode current;

		// Token: 0x04000872 RID: 2162
		private bool started;
	}
}
