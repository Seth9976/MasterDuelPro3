using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.TreeView.NodeMouseClick" /> and <see cref="E:System.Windows.Forms.TreeView.NodeMouseDoubleClick" /> events. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001FC RID: 508
	public class TreeNodeMouseClickEventArgs : MouseEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TreeNodeMouseClickEventArgs" /> class. </summary>
		/// <param name="node">The node that was clicked.</param>
		/// <param name="button">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> members.</param>
		/// <param name="clicks">The number of clicks that occurred.</param>
		/// <param name="x">The x-coordinate where the click occurred.</param>
		/// <param name="y">The y-coordinate where the click occurred.</param>
		// Token: 0x060015B1 RID: 5553 RVA: 0x0006C9D7 File Offset: 0x0006ABD7
		public TreeNodeMouseClickEventArgs(TreeNode node, MouseButtons button, int clicks, int x, int y)
			: base(button, clicks, x, y, 0)
		{
			this.node = node;
		}

		// Token: 0x04000CFB RID: 3323
		private TreeNode node;
	}
}
