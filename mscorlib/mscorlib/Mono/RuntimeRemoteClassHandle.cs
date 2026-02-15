using System;

namespace Mono
{
	// Token: 0x02000034 RID: 52
	internal struct RuntimeRemoteClassHandle
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002872 File Offset: 0x00000A72
		internal unsafe RuntimeClassHandle ProxyClass
		{
			get
			{
				return new RuntimeClassHandle(this.value->proxy_class);
			}
		}

		// Token: 0x0400010D RID: 269
		private unsafe RuntimeStructs.RemoteClass* value;
	}
}
