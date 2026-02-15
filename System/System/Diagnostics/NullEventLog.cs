using System;

namespace System.Diagnostics
{
	// Token: 0x0200018F RID: 399
	internal class NullEventLog : EventLogImpl
	{
		// Token: 0x06000986 RID: 2438 RVA: 0x000318F1 File Offset: 0x0002FAF1
		public NullEventLog(EventLog coreEventLog)
			: base(coreEventLog)
		{
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x00002FA0 File Offset: 0x000011A0
		public override void Close()
		{
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x00002FA0 File Offset: 0x000011A0
		public override void CreateEventSource(EventSourceCreationData sourceData)
		{
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x00002FA0 File Offset: 0x000011A0
		public override void Dispose(bool disposing)
		{
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x00002FA0 File Offset: 0x000011A0
		public override void DisableNotification()
		{
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x00002FA0 File Offset: 0x000011A0
		public override void EnableNotification()
		{
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x00003BCC File Offset: 0x00001DCC
		public override bool Exists(string logName, string machineName)
		{
			return true;
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x00031A3E File Offset: 0x0002FC3E
		protected override string FormatMessage(string source, uint messageID, string[] replacementStrings)
		{
			return string.Join(", ", replacementStrings);
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x000028AE File Offset: 0x00000AAE
		protected override int GetEntryCount()
		{
			return 0;
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x000027B6 File Offset: 0x000009B6
		protected override EventLogEntry GetEntry(int index)
		{
			return null;
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0003220C File Offset: 0x0003040C
		protected override string[] GetLogNames(string machineName)
		{
			return new string[0];
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x000027B6 File Offset: 0x000009B6
		public override string LogNameFromSourceName(string source, string machineName)
		{
			return null;
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x000028AE File Offset: 0x00000AAE
		public override bool SourceExists(string source, string machineName)
		{
			return false;
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x00002FA0 File Offset: 0x000011A0
		public override void WriteEntry(string[] replacementStrings, EventLogEntryType type, uint instanceID, short category, byte[] rawData)
		{
		}
	}
}
