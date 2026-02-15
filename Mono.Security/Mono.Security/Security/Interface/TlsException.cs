using System;

namespace Mono.Security.Interface
{
	// Token: 0x02000045 RID: 69
	public sealed class TlsException : Exception
	{
		// Token: 0x0600016D RID: 365 RVA: 0x000096B0 File Offset: 0x000078B0
		public TlsException(Alert alert)
			: this(alert, alert.Description.ToString())
		{
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000096D8 File Offset: 0x000078D8
		public TlsException(Alert alert, string message)
			: base(message)
		{
			this.alert = alert;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000096E8 File Offset: 0x000078E8
		public TlsException(AlertDescription description)
			: this(new Alert(description))
		{
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000096F6 File Offset: 0x000078F6
		public TlsException(AlertDescription description, string message)
			: this(new Alert(description), message)
		{
		}

		// Token: 0x040001E4 RID: 484
		private Alert alert;
	}
}
