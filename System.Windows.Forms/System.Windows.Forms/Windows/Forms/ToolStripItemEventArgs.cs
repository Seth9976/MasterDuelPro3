using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for <see cref="T:System.Windows.Forms.ToolStripItem" /> events.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001D3 RID: 467
	public class ToolStripItemEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripItemEventArgs" /> class, specifying a <see cref="T:System.Windows.Forms.ToolStripItem" />. </summary>
		/// <param name="item">The <see cref="T:System.Windows.Forms.ToolStripItem" /> for which to specify events.</param>
		// Token: 0x0600144C RID: 5196 RVA: 0x000658B5 File Offset: 0x00063AB5
		public ToolStripItemEventArgs(ToolStripItem item)
		{
			this.item = item;
		}

		/// <summary>Gets a <see cref="T:System.Windows.Forms.ToolStripItem" /> for which to handle events.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItem" /> for which to handle events.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x0600144D RID: 5197 RVA: 0x000658C4 File Offset: 0x00063AC4
		public ToolStripItem Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x04000C4A RID: 3146
		private ToolStripItem item;
	}
}
