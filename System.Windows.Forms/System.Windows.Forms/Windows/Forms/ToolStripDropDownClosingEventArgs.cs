using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ToolStripDropDown.Closing" /> event.</summary>
	// Token: 0x020001C3 RID: 451
	public class ToolStripDropDownClosingEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDownClosingEventArgs" /> class with the specified reason for closing. </summary>
		/// <param name="reason">One of the <see cref="T:System.Windows.Forms.ToolStripDropDownCloseReason" /> values.</param>
		// Token: 0x0600137A RID: 4986 RVA: 0x00062E79 File Offset: 0x00061079
		public ToolStripDropDownClosingEventArgs(ToolStripDropDownCloseReason reason)
		{
			this.close_reason = reason;
		}

		// Token: 0x04000BDC RID: 3036
		private ToolStripDropDownCloseReason close_reason;
	}
}
