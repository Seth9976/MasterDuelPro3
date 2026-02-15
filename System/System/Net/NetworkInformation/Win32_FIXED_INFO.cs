using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000471 RID: 1137
	internal struct Win32_FIXED_INFO
	{
		// Token: 0x04001320 RID: 4896
		public string HostName;

		// Token: 0x04001321 RID: 4897
		public string DomainName;

		// Token: 0x04001322 RID: 4898
		public IntPtr CurrentDnsServer;

		// Token: 0x04001323 RID: 4899
		public Win32_IP_ADDR_STRING DnsServerList;

		// Token: 0x04001324 RID: 4900
		public NetBiosNodeType NodeType;

		// Token: 0x04001325 RID: 4901
		public string ScopeId;

		// Token: 0x04001326 RID: 4902
		public uint EnableRouting;

		// Token: 0x04001327 RID: 4903
		public uint EnableProxy;

		// Token: 0x04001328 RID: 4904
		public uint EnableDns;
	}
}
