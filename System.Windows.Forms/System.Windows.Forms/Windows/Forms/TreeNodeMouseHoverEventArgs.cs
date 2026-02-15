using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.TreeView.NodeMouseHover" /> event. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001FE RID: 510
	[ComVisible(true)]
	public class TreeNodeMouseHoverEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TreeNodeMouseHoverEventArgs" /> class. </summary>
		/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> the mouse pointer is currently resting on.</param>
		// Token: 0x060015B4 RID: 5556 RVA: 0x0006C9ED File Offset: 0x0006ABED
		public TreeNodeMouseHoverEventArgs(TreeNode node)
		{
			this.node = node;
		}

		// Token: 0x04000CFC RID: 3324
		private TreeNode node;
	}
}
