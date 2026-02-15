using System;
using System.Globalization;
using System.Reflection;

namespace System.Diagnostics
{
	// Token: 0x02000187 RID: 391
	[DefaultMember("Item")]
	internal abstract class EventLogImpl
	{
		// Token: 0x06000950 RID: 2384 RVA: 0x00031690 File Offset: 0x0002F890
		protected EventLogImpl(EventLog coreEventLog)
		{
			this._coreEventLog = coreEventLog;
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000951 RID: 2385 RVA: 0x0003169F File Offset: 0x0002F89F
		protected EventLog CoreEventLog
		{
			get
			{
				return this._coreEventLog;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000952 RID: 2386 RVA: 0x000316A8 File Offset: 0x0002F8A8
		public int EntryCount
		{
			get
			{
				if (this._coreEventLog.Log == null || this._coreEventLog.Log.Length == 0)
				{
					throw new ArgumentException("Log property is not set.");
				}
				if (!EventLog.Exists(this._coreEventLog.Log, this._coreEventLog.MachineName))
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "The event log '{0}' on  computer '{1}' does not exist.", this._coreEventLog.Log, this._coreEventLog.MachineName));
				}
				return this.GetEntryCount();
			}
		}

		// Token: 0x06000953 RID: 2387
		public abstract void DisableNotification();

		// Token: 0x06000954 RID: 2388
		public abstract void EnableNotification();

		// Token: 0x06000955 RID: 2389
		public abstract void Close();

		// Token: 0x06000956 RID: 2390
		public abstract void CreateEventSource(EventSourceCreationData sourceData);

		// Token: 0x06000957 RID: 2391
		public abstract void Dispose(bool disposing);

		// Token: 0x06000958 RID: 2392
		public abstract bool Exists(string logName, string machineName);

		// Token: 0x06000959 RID: 2393
		protected abstract int GetEntryCount();

		// Token: 0x0600095A RID: 2394
		protected abstract EventLogEntry GetEntry(int index);

		// Token: 0x0600095B RID: 2395
		public abstract string LogNameFromSourceName(string source, string machineName);

		// Token: 0x0600095C RID: 2396
		public abstract bool SourceExists(string source, string machineName);

		// Token: 0x0600095D RID: 2397
		public abstract void WriteEntry(string[] replacementStrings, EventLogEntryType type, uint instanceID, short category, byte[] rawData);

		// Token: 0x0600095E RID: 2398
		protected abstract string FormatMessage(string source, uint messageID, string[] replacementStrings);

		// Token: 0x0600095F RID: 2399
		protected abstract string[] GetLogNames(string machineName);

		// Token: 0x06000960 RID: 2400 RVA: 0x00031730 File Offset: 0x0002F930
		protected void ValidateCustomerLogName(string logName, string machineName)
		{
			if (logName.Length >= 8)
			{
				string text = logName.Substring(0, 8);
				if (string.Compare(text, "AppEvent", true) == 0 || string.Compare(text, "SysEvent", true) == 0 || string.Compare(text, "SecEvent", true) == 0)
				{
					throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The log name: '{0}' is invalid for customer log creation.", logName));
				}
				foreach (string text2 in this.GetLogNames(machineName))
				{
					if (text2.Length >= 8 && string.Compare(text2, 0, text, 0, 8, true) == 0)
					{
						throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Only the first eight characters of a custom log name are significant, and there is already another log on the system using the first eight characters of the name given. Name given: '{0}', name of existing log: '{1}'.", logName, text2));
					}
				}
			}
			if (!this.SourceExists(logName, machineName))
			{
				return;
			}
			if (machineName == ".")
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Log {0} has already been registered as a source on the local computer.", logName));
			}
			throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Log {0} has already been registered as a source on the computer {1}.", logName, machineName));
		}

		// Token: 0x04000713 RID: 1811
		private readonly EventLog _coreEventLog;
	}
}
