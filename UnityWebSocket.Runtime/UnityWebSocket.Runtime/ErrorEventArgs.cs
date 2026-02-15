using System;

namespace UnityWebSocket
{
	// Token: 0x02000006 RID: 6
	public class ErrorEventArgs : EventArgs
	{
		// Token: 0x0600000F RID: 15 RVA: 0x0000214E File Offset: 0x0000034E
		internal ErrorEventArgs(string message)
			: this(message, null)
		{
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002158 File Offset: 0x00000358
		internal ErrorEventArgs(string message, Exception exception)
		{
			this.Message = message;
			this.Exception = exception;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000216E File Offset: 0x0000036E
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002176 File Offset: 0x00000376
		public Exception Exception { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000013 RID: 19 RVA: 0x0000217F File Offset: 0x0000037F
		// (set) Token: 0x06000014 RID: 20 RVA: 0x00002187 File Offset: 0x00000387
		public string Message { get; private set; }
	}
}
