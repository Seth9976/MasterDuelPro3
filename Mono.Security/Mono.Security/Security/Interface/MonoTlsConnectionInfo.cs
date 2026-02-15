using System;
using System.Runtime.CompilerServices;

namespace Mono.Security.Interface
{
	// Token: 0x0200003E RID: 62
	public class MonoTlsConnectionInfo
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000138 RID: 312 RVA: 0x000093F1 File Offset: 0x000075F1
		// (set) Token: 0x06000139 RID: 313 RVA: 0x000093F9 File Offset: 0x000075F9
		[CLSCompliant(false)]
		public CipherSuiteCode CipherSuiteCode { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00009402 File Offset: 0x00007602
		// (set) Token: 0x0600013B RID: 315 RVA: 0x0000940A File Offset: 0x0000760A
		public TlsProtocols ProtocolVersion { get; set; }

		// Token: 0x17000058 RID: 88
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00009413 File Offset: 0x00007613
		public string PeerDomainName
		{
			[CompilerGenerated]
			set
			{
				this.<PeerDomainName>k__BackingField = value;
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000941C File Offset: 0x0000761C
		public override string ToString()
		{
			return string.Format("[MonoTlsConnectionInfo: {0}:{1}]", this.ProtocolVersion, this.CipherSuiteCode);
		}
	}
}
