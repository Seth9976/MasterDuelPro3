using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200046B RID: 1131
	internal class Win32IPInterfaceProperties2 : IPInterfaceProperties
	{
		// Token: 0x06001C59 RID: 7257 RVA: 0x0007C2A6 File Offset: 0x0007A4A6
		public Win32IPInterfaceProperties2(Win32_IP_ADAPTER_ADDRESSES addr, Win32_MIB_IFROW mib4, Win32_MIB_IFROW mib6)
		{
			this.addr = addr;
			this.mib4 = mib4;
			this.mib6 = mib6;
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06001C5A RID: 7258 RVA: 0x0007C2C3 File Offset: 0x0007A4C3
		public override IPAddressCollection DnsAddresses
		{
			get
			{
				return Win32IPAddressCollection.FromDnsServer(this.addr.FirstDnsServerAddress);
			}
		}

		// Token: 0x04001315 RID: 4885
		private readonly Win32_IP_ADAPTER_ADDRESSES addr;

		// Token: 0x04001316 RID: 4886
		private readonly Win32_MIB_IFROW mib4;

		// Token: 0x04001317 RID: 4887
		private readonly Win32_MIB_IFROW mib6;
	}
}
