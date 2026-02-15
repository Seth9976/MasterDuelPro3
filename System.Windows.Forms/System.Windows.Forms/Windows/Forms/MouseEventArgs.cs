using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.MouseUp" />, <see cref="E:System.Windows.Forms.Control.MouseDown" />, and <see cref="E:System.Windows.Forms.Control.MouseMove" /> events.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000152 RID: 338
	[ComVisible(true)]
	public class MouseEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MouseEventArgs" /> class.</summary>
		/// <param name="button">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values that indicate which mouse button was pressed. </param>
		/// <param name="clicks">The number of times a mouse button was pressed. </param>
		/// <param name="x">The x-coordinate of a mouse click, in pixels. </param>
		/// <param name="y">The y-coordinate of a mouse click, in pixels. </param>
		/// <param name="delta">A signed count of the number of detents the wheel has rotated. </param>
		// Token: 0x06000D2F RID: 3375 RVA: 0x0003A4F5 File Offset: 0x000386F5
		public MouseEventArgs(MouseButtons button, int clicks, int x, int y, int delta)
		{
			this.buttons = button;
			this.clicks = clicks;
			this.delta = delta;
			this.x = x;
			this.y = y;
		}

		/// <summary>Gets which mouse button was pressed.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000D30 RID: 3376 RVA: 0x0003A522 File Offset: 0x00038722
		public MouseButtons Button
		{
			get
			{
				return this.buttons;
			}
		}

		/// <summary>Gets the number of times the mouse button was pressed and released.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the number of times the mouse button was pressed and released.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000D31 RID: 3377 RVA: 0x0003A52A File Offset: 0x0003872A
		public int Clicks
		{
			get
			{
				return this.clicks;
			}
		}

		/// <summary>Gets a signed count of the number of detents the mouse wheel has rotated, multiplied by the WHEEL_DELTA constant. A detent is one notch of the mouse wheel.</summary>
		/// <returns>A signed count of the number of detents the mouse wheel has rotated, multiplied by the WHEEL_DELTA constant.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000D32 RID: 3378 RVA: 0x0003A532 File Offset: 0x00038732
		public int Delta
		{
			get
			{
				return this.delta;
			}
		}

		/// <summary>Gets the x-coordinate of the mouse during the generating mouse event.</summary>
		/// <returns>The x-coordinate of the mouse, in pixels.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000D33 RID: 3379 RVA: 0x0003A53A File Offset: 0x0003873A
		public int X
		{
			get
			{
				return this.x;
			}
		}

		/// <summary>Gets the y-coordinate of the mouse during the generating mouse event.</summary>
		/// <returns>The y-coordinate of the mouse, in pixels.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000D34 RID: 3380 RVA: 0x0003A542 File Offset: 0x00038742
		public int Y
		{
			get
			{
				return this.y;
			}
		}

		/// <summary>Gets the location of the mouse during the generating mouse event.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> that contains the x- and y- mouse coordinates, in pixels, relative to the upper-left corner of the form.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000D35 RID: 3381 RVA: 0x0003A54A File Offset: 0x0003874A
		public Point Location
		{
			get
			{
				return new Point(this.x, this.y);
			}
		}

		// Token: 0x04000865 RID: 2149
		private MouseButtons buttons;

		// Token: 0x04000866 RID: 2150
		private int clicks;

		// Token: 0x04000867 RID: 2151
		private int delta;

		// Token: 0x04000868 RID: 2152
		private int x;

		// Token: 0x04000869 RID: 2153
		private int y;
	}
}
