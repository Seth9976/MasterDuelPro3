using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderGrip" /> event. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001ED RID: 493
	public class ToolStripSeparatorRenderEventArgs : ToolStripItemRenderEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripSeparatorRenderEventArgs" /> class. </summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> to paint with.</param>
		/// <param name="separator">The <see cref="T:System.Windows.Forms.ToolStripSeparator" /> to be painted.</param>
		/// <param name="vertical">A value indicating whether or not the <see cref="T:System.Windows.Forms.ToolStripSeparator" /> is to be drawn vertically.</param>
		// Token: 0x06001507 RID: 5383 RVA: 0x000698EB File Offset: 0x00067AEB
		public ToolStripSeparatorRenderEventArgs(Graphics g, ToolStripSeparator separator, bool vertical)
			: base(g, separator)
		{
			this.vertical = vertical;
		}

		/// <summary>Gets a value indicating whether the display style for the grip is vertical. </summary>
		/// <returns>true if the display style for the <see cref="T:System.Windows.Forms.ToolStripSeparator" /> is vertical; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06001508 RID: 5384 RVA: 0x000698FC File Offset: 0x00067AFC
		public bool Vertical
		{
			get
			{
				return this.vertical;
			}
		}

		// Token: 0x04000CAA RID: 3242
		private bool vertical;
	}
}
