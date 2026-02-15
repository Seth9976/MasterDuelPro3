using System;
using System.Security.Authentication;

namespace Mono.Security.Interface
{
	// Token: 0x02000042 RID: 66
	public abstract class MonoTlsProvider
	{
		// Token: 0x06000143 RID: 323 RVA: 0x0000293D File Offset: 0x00000B3D
		internal MonoTlsProvider()
		{
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000144 RID: 324
		public abstract Guid ID { get; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000145 RID: 325
		public abstract string Name { get; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000146 RID: 326
		public abstract bool SupportsSslStream { get; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000147 RID: 327
		public abstract bool SupportsConnectionInfo { get; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000148 RID: 328
		public abstract bool SupportsMonoExtensions { get; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000149 RID: 329
		public abstract SslProtocols SupportedProtocols { get; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600014A RID: 330
		internal abstract bool SupportsCleanShutdown { get; }
	}
}
