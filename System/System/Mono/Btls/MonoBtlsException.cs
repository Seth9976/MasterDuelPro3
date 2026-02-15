using System;

namespace Mono.Btls
{
	// Token: 0x020000A2 RID: 162
	internal class MonoBtlsException : Exception
	{
		// Token: 0x060002A2 RID: 674 RVA: 0x0000A7B2 File Offset: 0x000089B2
		public MonoBtlsException()
		{
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000A7BA File Offset: 0x000089BA
		public MonoBtlsException(MonoBtlsSslError error)
			: base(error.ToString())
		{
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000A7CF File Offset: 0x000089CF
		public MonoBtlsException(string message)
			: base(message)
		{
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000A7D8 File Offset: 0x000089D8
		public MonoBtlsException(string format, params object[] args)
			: base(string.Format(format, args))
		{
		}
	}
}
