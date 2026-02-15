using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ToolStripDropDown.Closed" /> event. </summary>
	// Token: 0x020001C1 RID: 449
	public class ToolStripDropDownClosedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDownClosedEventArgs" /> class. </summary>
		/// <param name="reason">One of the <see cref="T:System.Windows.Forms.ToolStripDropDownCloseReason" /> values.</param>
		// Token: 0x06001377 RID: 4983 RVA: 0x00062E6A File Offset: 0x0006106A
		public ToolStripDropDownClosedEventArgs(ToolStripDropDownCloseReason reason)
		{
			this.close_reason = reason;
		}

		// Token: 0x04000BDB RID: 3035
		private ToolStripDropDownCloseReason close_reason;
	}
}
