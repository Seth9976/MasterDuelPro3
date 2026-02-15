using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.TreeView.AfterCheck" />, <see cref="E:System.Windows.Forms.TreeView.AfterCollapse" />, <see cref="E:System.Windows.Forms.TreeView.AfterExpand" />, or <see cref="E:System.Windows.Forms.TreeView.AfterSelect" /> events of a <see cref="T:System.Windows.Forms.TreeView" /> control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000206 RID: 518
	public class TreeViewEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TreeViewEventArgs" /> class for the specified tree node.</summary>
		/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> that the event is responding to. </param>
		// Token: 0x06001641 RID: 5697 RVA: 0x0006FA48 File Offset: 0x0006DC48
		public TreeViewEventArgs(TreeNode node)
		{
			this.node = node;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TreeViewEventArgs" /> class for the specified tree node and with the specified type of action that raised the event.</summary>
		/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> that the event is responding to. </param>
		/// <param name="action">The type of <see cref="T:System.Windows.Forms.TreeViewAction" /> that raised the event. </param>
		// Token: 0x06001642 RID: 5698 RVA: 0x0006FA57 File Offset: 0x0006DC57
		public TreeViewEventArgs(TreeNode node, TreeViewAction action)
			: this(node)
		{
			this.action = action;
		}

		/// <summary>Gets the tree node that has been checked, expanded, collapsed, or selected.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> that has been checked, expanded, collapsed, or selected.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06001643 RID: 5699 RVA: 0x0006FA67 File Offset: 0x0006DC67
		public TreeNode Node
		{
			get
			{
				return this.node;
			}
		}

		// Token: 0x04000D5C RID: 3420
		private TreeNode node;

		// Token: 0x04000D5D RID: 3421
		private TreeViewAction action;
	}
}
