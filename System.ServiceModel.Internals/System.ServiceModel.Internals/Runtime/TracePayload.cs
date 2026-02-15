using System;

namespace System.Runtime
{
	// Token: 0x02000027 RID: 39
	internal struct TracePayload
	{
		// Token: 0x0600008A RID: 138 RVA: 0x000039EA File Offset: 0x00001BEA
		public TracePayload(string serializedException, string eventSource, string appDomainFriendlyName, string extendedData, string hostReference)
		{
			this.serializedException = serializedException;
			this.eventSource = eventSource;
			this.appDomainFriendlyName = appDomainFriendlyName;
			this.extendedData = extendedData;
			this.hostReference = hostReference;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003A11 File Offset: 0x00001C11
		public string SerializedException
		{
			get
			{
				return this.serializedException;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00003A19 File Offset: 0x00001C19
		public string EventSource
		{
			get
			{
				return this.eventSource;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00003A21 File Offset: 0x00001C21
		public string AppDomainFriendlyName
		{
			get
			{
				return this.appDomainFriendlyName;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00003A29 File Offset: 0x00001C29
		public string ExtendedData
		{
			get
			{
				return this.extendedData;
			}
		}

		// Token: 0x04000055 RID: 85
		private string serializedException;

		// Token: 0x04000056 RID: 86
		private string eventSource;

		// Token: 0x04000057 RID: 87
		private string appDomainFriendlyName;

		// Token: 0x04000058 RID: 88
		private string extendedData;

		// Token: 0x04000059 RID: 89
		private string hostReference;
	}
}
