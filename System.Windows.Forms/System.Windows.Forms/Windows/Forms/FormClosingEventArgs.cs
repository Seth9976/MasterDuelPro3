using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Form.FormClosing" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000B6 RID: 182
	public class FormClosingEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.FormClosingEventArgs" /> class.</summary>
		/// <param name="closeReason">A <see cref="T:System.Windows.Forms.CloseReason" /> value that represents the reason why the form is being closed.</param>
		/// <param name="cancel">true to cancel the event; otherwise, false.</param>
		// Token: 0x0600074E RID: 1870 RVA: 0x000206BE File Offset: 0x0001E8BE
		public FormClosingEventArgs(CloseReason closeReason, bool cancel)
			: base(cancel)
		{
			this.close_reason = closeReason;
		}

		// Token: 0x040004B2 RID: 1202
		private CloseReason close_reason;
	}
}
