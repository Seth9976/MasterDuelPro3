using System;

namespace System.ComponentModel
{
	/// <summary>Provides data for a cancelable event.</summary>
	// Token: 0x020002A9 RID: 681
	public class CancelEventArgs : EventArgs
	{
		/// <summary>Gets or sets a value indicating whether the event should be canceled.</summary>
		/// <returns>true if the event should be canceled; otherwise, false.</returns>
		// Token: 0x17000374 RID: 884
		// (get) Token: 0x0600104A RID: 4170 RVA: 0x00044399 File Offset: 0x00042599
		// (set) Token: 0x0600104B RID: 4171 RVA: 0x000443A1 File Offset: 0x000425A1
		public bool Cancel { get; set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.CancelEventArgs" /> class with the <see cref="P:System.ComponentModel.CancelEventArgs.Cancel" /> property set to false.</summary>
		// Token: 0x0600104C RID: 4172 RVA: 0x00009981 File Offset: 0x00007B81
		public CancelEventArgs()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.CancelEventArgs" /> class with the <see cref="P:System.ComponentModel.CancelEventArgs.Cancel" /> property set to the given value.</summary>
		/// <param name="cancel">true to cancel the event; otherwise, false. </param>
		// Token: 0x0600104D RID: 4173 RVA: 0x000443AA File Offset: 0x000425AA
		public CancelEventArgs(bool cancel)
		{
			this.Cancel = cancel;
		}
	}
}
