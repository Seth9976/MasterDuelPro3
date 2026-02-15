using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting
{
	// Token: 0x02000412 RID: 1042
	[Serializable]
	internal class EnvoyInfo : IEnvoyInfo
	{
		// Token: 0x060022C6 RID: 8902 RVA: 0x0008F589 File Offset: 0x0008D789
		public EnvoyInfo(IMessageSink sinks)
		{
			this.envoySinks = sinks;
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x060022C7 RID: 8903 RVA: 0x0008F598 File Offset: 0x0008D798
		public IMessageSink EnvoySinks
		{
			get
			{
				return this.envoySinks;
			}
		}

		// Token: 0x040010E4 RID: 4324
		private IMessageSink envoySinks;
	}
}
