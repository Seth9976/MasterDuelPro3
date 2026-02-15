using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.HelpRequested" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000C2 RID: 194
	[ComVisible(true)]
	public class HelpEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.HelpEventArgs" /> class.</summary>
		/// <param name="mousePos">The coordinates of the mouse pointer. </param>
		// Token: 0x06000776 RID: 1910 RVA: 0x00020DEF File Offset: 0x0001EFEF
		public HelpEventArgs(Point mousePos)
		{
			this.mouse_position = mousePos;
			this.event_handled = false;
		}

		// Token: 0x040004C3 RID: 1219
		private Point mouse_position;

		// Token: 0x040004C4 RID: 1220
		private bool event_handled;
	}
}
