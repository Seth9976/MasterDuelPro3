using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000472 RID: 1138
	internal struct Win32_FIXED_INFO_Marshal
	{
		// Token: 0x04001329 RID: 4905
		[FixedBuffer(typeof(byte), 132)]
		public Win32_FIXED_INFO_Marshal.<HostName>e__FixedBuffer HostName;

		// Token: 0x0400132A RID: 4906
		[FixedBuffer(typeof(byte), 132)]
		public Win32_FIXED_INFO_Marshal.<DomainName>e__FixedBuffer DomainName;

		// Token: 0x0400132B RID: 4907
		public IntPtr CurrentDnsServer;

		// Token: 0x0400132C RID: 4908
		public Win32_IP_ADDR_STRING DnsServerList;

		// Token: 0x0400132D RID: 4909
		public NetBiosNodeType NodeType;

		// Token: 0x0400132E RID: 4910
		[FixedBuffer(typeof(byte), 260)]
		public Win32_FIXED_INFO_Marshal.<ScopeId>e__FixedBuffer ScopeId;

		// Token: 0x0400132F RID: 4911
		public uint EnableRouting;

		// Token: 0x04001330 RID: 4912
		public uint EnableProxy;

		// Token: 0x04001331 RID: 4913
		public uint EnableDns;

		// Token: 0x02000473 RID: 1139
		[UnsafeValueType]
		[CompilerGenerated]
		[StructLayout(LayoutKind.Sequential, Size = 132)]
		public struct <HostName>e__FixedBuffer
		{
			// Token: 0x04001332 RID: 4914
			public byte FixedElementField;
		}

		// Token: 0x02000474 RID: 1140
		[UnsafeValueType]
		[CompilerGenerated]
		[StructLayout(LayoutKind.Sequential, Size = 132)]
		public struct <DomainName>e__FixedBuffer
		{
			// Token: 0x04001333 RID: 4915
			public byte FixedElementField;
		}

		// Token: 0x02000475 RID: 1141
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 260)]
		public struct <ScopeId>e__FixedBuffer
		{
			// Token: 0x04001334 RID: 4916
			public byte FixedElementField;
		}
	}
}
