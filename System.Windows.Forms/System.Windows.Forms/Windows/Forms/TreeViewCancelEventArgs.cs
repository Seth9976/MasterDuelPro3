using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.TreeView.BeforeCheck" />, <see cref="E:System.Windows.Forms.TreeView.BeforeCollapse" />, <see cref="E:System.Windows.Forms.TreeView.BeforeExpand" />, and <see cref="E:System.Windows.Forms.TreeView.BeforeSelect" /> events of a <see cref="T:System.Windows.Forms.TreeView" /> control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000203 RID: 515
	public class TreeViewCancelEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TreeViewCancelEventArgs" /> class with the specified tree node, a value specifying whether the event is to be canceled, and the type of tree view action that raised the event.</summary>
		/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> that the event is responding to. </param>
		/// <param name="cancel">true to cancel the event; otherwise, false. </param>
		/// <param name="action">One of the <see cref="T:System.Windows.Forms.TreeViewAction" /> values indicating the type of action that raised the event. </param>
		// Token: 0x0600163D RID: 5693 RVA: 0x0006FA29 File Offset: 0x0006DC29
		public TreeViewCancelEventArgs(TreeNode node, bool cancel, TreeViewAction action)
			: base(cancel)
		{
			this.node = node;
			this.action = action;
		}

		/// <summary>Gets the tree node to be checked, expanded, collapsed, or selected.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> to be checked, expanded, collapsed, or selected.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x0600163E RID: 5694 RVA: 0x0006FA40 File Offset: 0x0006DC40
		public TreeNode Node
		{
			get
			{
				return this.node;
			}
		}

		// Token: 0x04000D56 RID: 3414
		private TreeNode node;

		// Token: 0x04000D57 RID: 3415
		private TreeViewAction action;
	}
}
