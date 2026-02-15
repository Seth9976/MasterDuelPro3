using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Form.FormClosed" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000B4 RID: 180
	public class FormClosedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.FormClosedEventArgs" /> class.</summary>
		/// <param name="closeReason">A <see cref="T:System.Windows.Forms.CloseReason" /> value that represents the reason why the form was closed.</param>
		// Token: 0x0600074B RID: 1867 RVA: 0x000206AF File Offset: 0x0001E8AF
		public FormClosedEventArgs(CloseReason closeReason)
		{
			this.close_reason = closeReason;
		}

		// Token: 0x040004B1 RID: 1201
		private CloseReason close_reason;
	}
}
