using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.TreeView.DrawNode" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200007E RID: 126
	public class DrawTreeNodeEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DrawTreeNodeEventArgs" /> class.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> surface on which to draw. </param>
		/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> to draw. </param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> within which to draw. </param>
		/// <param name="state">A bitwise combination of the <see cref="T:System.Windows.Forms.TreeNodeStates" /> values indicating the current state of the <see cref="T:System.Windows.Forms.TreeNode" /> to draw. </param>
		// Token: 0x0600051B RID: 1307 RVA: 0x000137AC File Offset: 0x000119AC
		public DrawTreeNodeEventArgs(Graphics graphics, TreeNode node, Rectangle bounds, TreeNodeStates state)
		{
			this.bounds = bounds;
			this.draw_default = false;
			this.graphics = graphics;
			this.node = node;
			this.state = state;
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.TreeNode" /> should be drawn by the operating system rather than being owner drawn.</summary>
		/// <returns>true if the node should be drawn by the operating system; false if the node will be drawn in the event handler. The default value is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x000137D8 File Offset: 0x000119D8
		public bool DrawDefault
		{
			get
			{
				return this.draw_default;
			}
		}

		// Token: 0x04000323 RID: 803
		private Rectangle bounds;

		// Token: 0x04000324 RID: 804
		private bool draw_default;

		// Token: 0x04000325 RID: 805
		private Graphics graphics;

		// Token: 0x04000326 RID: 806
		private TreeNode node;

		// Token: 0x04000327 RID: 807
		private TreeNodeStates state;
	}
}
