using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.TreeView.BeforeLabelEdit" /> and <see cref="E:System.Windows.Forms.TreeView.AfterLabelEdit" /> events.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000155 RID: 341
	public class NodeLabelEditEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.NodeLabelEditEventArgs" /> class for the specified <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
		/// <param name="node">The tree node containing the text to edit. </param>
		// Token: 0x06000D4B RID: 3403 RVA: 0x0003AB87 File Offset: 0x00038D87
		public NodeLabelEditEventArgs(TreeNode node)
		{
			this.node = node;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.NodeLabelEditEventArgs" /> class for the specified <see cref="T:System.Windows.Forms.TreeNode" /> and the specified text with which to update the tree node label.</summary>
		/// <param name="node">The tree node containing the text to edit. </param>
		/// <param name="label">The new text to associate with the tree node. </param>
		// Token: 0x06000D4C RID: 3404 RVA: 0x0003AB96 File Offset: 0x00038D96
		public NodeLabelEditEventArgs(TreeNode node, string label)
			: this(node)
		{
			this.label = label;
		}

		/// <summary>Gets or sets a value indicating whether the edit has been canceled.</summary>
		/// <returns>true if the edit has been canceled; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000D4D RID: 3405 RVA: 0x0003ABA6 File Offset: 0x00038DA6
		// (set) Token: 0x06000D4E RID: 3406 RVA: 0x0003ABAE File Offset: 0x00038DAE
		public bool CancelEdit
		{
			get
			{
				return this.cancel;
			}
			set
			{
				this.cancel = value;
				if (this.cancel)
				{
					this.node.EndEdit(true);
				}
			}
		}

		/// <summary>Gets the tree node containing the text to edit.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TreeNode" /> that represents the tree node containing the text to edit.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000D4F RID: 3407 RVA: 0x0003ABCB File Offset: 0x00038DCB
		public TreeNode Node
		{
			get
			{
				return this.node;
			}
		}

		/// <summary>Gets the new text to associate with the tree node.</summary>
		/// <returns>The string value that represents the new <see cref="T:System.Windows.Forms.TreeNode" /> label or null if the user cancels the edit. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000D50 RID: 3408 RVA: 0x0003ABD3 File Offset: 0x00038DD3
		public string Label
		{
			get
			{
				return this.label;
			}
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x0003ABDB File Offset: 0x00038DDB
		internal void SetLabel(string label)
		{
			this.label = label;
		}

		// Token: 0x0400086D RID: 2157
		private TreeNode node;

		// Token: 0x0400086E RID: 2158
		private string label;

		// Token: 0x0400086F RID: 2159
		private bool cancel;
	}
}
