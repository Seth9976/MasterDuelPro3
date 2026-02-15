using System;

namespace System.Diagnostics
{
	/// <summary>Provides data for the <see cref="E:System.Diagnostics.EventLog.EntryWritten" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000182 RID: 386
	public class EntryWrittenEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EntryWrittenEventArgs" /> class with the specified event log entry.</summary>
		/// <param name="entry">An <see cref="T:System.Diagnostics.EventLogEntry" /> that represents the entry that was written. </param>
		// Token: 0x06000928 RID: 2344 RVA: 0x00030F10 File Offset: 0x0002F110
		public EntryWrittenEventArgs(EventLogEntry entry)
		{
			this.entry = entry;
		}

		// Token: 0x040006F8 RID: 1784
		private EventLogEntry entry;
	}
}
