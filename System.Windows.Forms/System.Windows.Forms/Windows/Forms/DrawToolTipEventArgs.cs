using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ToolTip.Draw" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200007C RID: 124
	public class DrawToolTipEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DrawToolTipEventArgs" /> class.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> context used to draw the ToolTip. </param>
		/// <param name="associatedWindow">The <see cref="T:System.Windows.Forms.IWin32Window" /> that the ToolTip is bound to.</param>
		/// <param name="associatedControl">The <see cref="T:System.Windows.Forms.Control" /> that the ToolTip is being created for.</param>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> that outlines the area where the ToolTip is to be displayed.</param>
		/// <param name="toolTipText">A <see cref="T:System.String" /> containing the text for the ToolTip.</param>
		/// <param name="backColor">The <see cref="T:System.Drawing.Color" /> of the ToolTip background.</param>
		/// <param name="foreColor">The <see cref="T:System.Drawing.Color" /> of the ToolTip text. </param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> used to draw the ToolTip text.</param>
		// Token: 0x06000516 RID: 1302 RVA: 0x0001374C File Offset: 0x0001194C
		public DrawToolTipEventArgs(Graphics graphics, IWin32Window associatedWindow, Control associatedControl, Rectangle bounds, string toolTipText, Color backColor, Color foreColor, Font font)
		{
			this.graphics = graphics;
			this.associated_window = associatedWindow;
			this.associated_control = associatedControl;
			this.bounds = bounds;
			this.tooltip_text = toolTipText;
			this.back_color = backColor;
			this.fore_color = foreColor;
			this.font = font;
		}

		/// <summary>Gets the size and location of the <see cref="T:System.Windows.Forms.ToolTip" /> to draw.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the <see cref="T:System.Windows.Forms.ToolTip" /> to draw.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x0001379C File Offset: 0x0001199C
		public Rectangle Bounds
		{
			get
			{
				return this.bounds;
			}
		}

		/// <summary>Gets the graphics surface used to draw the <see cref="T:System.Windows.Forms.ToolTip" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Graphics" /> on which to draw the <see cref="T:System.Windows.Forms.ToolTip" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x000137A4 File Offset: 0x000119A4
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		// Token: 0x0400031B RID: 795
		private Control associated_control;

		// Token: 0x0400031C RID: 796
		private IWin32Window associated_window;

		// Token: 0x0400031D RID: 797
		private Color back_color;

		// Token: 0x0400031E RID: 798
		private Font font;

		// Token: 0x0400031F RID: 799
		private Rectangle bounds;

		// Token: 0x04000320 RID: 800
		private Color fore_color;

		// Token: 0x04000321 RID: 801
		private Graphics graphics;

		// Token: 0x04000322 RID: 802
		private string tooltip_text;
	}
}
