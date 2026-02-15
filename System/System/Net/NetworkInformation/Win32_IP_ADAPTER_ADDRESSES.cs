using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000477 RID: 1143
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct Win32_IP_ADAPTER_ADDRESSES
	{
		// Token: 0x04001338 RID: 4920
		public AlignmentUnion Alignment;

		// Token: 0x04001339 RID: 4921
		public IntPtr Next;

		// Token: 0x0400133A RID: 4922
		[MarshalAs(UnmanagedType.LPStr)]
		public string AdapterName;

		// Token: 0x0400133B RID: 4923
		public IntPtr FirstUnicastAddress;

		// Token: 0x0400133C RID: 4924
		public IntPtr FirstAnycastAddress;

		// Token: 0x0400133D RID: 4925
		public IntPtr FirstMulticastAddress;

		// Token: 0x0400133E RID: 4926
		public IntPtr FirstDnsServerAddress;

		// Token: 0x0400133F RID: 4927
		public string DnsSuffix;

		// Token: 0x04001340 RID: 4928
		public string Description;

		// Token: 0x04001341 RID: 4929
		public string FriendlyName;

		// Token: 0x04001342 RID: 4930
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public byte[] PhysicalAddress;

		// Token: 0x04001343 RID: 4931
		public uint PhysicalAddressLength;

		// Token: 0x04001344 RID: 4932
		public uint Flags;

		// Token: 0x04001345 RID: 4933
		public uint Mtu;

		// Token: 0x04001346 RID: 4934
		public NetworkInterfaceType IfType;

		// Token: 0x04001347 RID: 4935
		public OperationalStatus OperStatus;

		// Token: 0x04001348 RID: 4936
		public int Ipv6IfIndex;

		// Token: 0x04001349 RID: 4937
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		public uint[] ZoneIndices;

		// Token: 0x0400134A RID: 4938
		public IntPtr FirstPrefix;

		// Token: 0x0400134B RID: 4939
		public ulong TransmitLinkSpeed;

		// Token: 0x0400134C RID: 4940
		public ulong ReceiveLinkSpeed;

		// Token: 0x0400134D RID: 4941
		public IntPtr FirstWinsServerAddress;

		// Token: 0x0400134E RID: 4942
		public IntPtr FirstGatewayAddress;

		// Token: 0x0400134F RID: 4943
		public uint Ipv4Metric;

		// Token: 0x04001350 RID: 4944
		public uint Ipv6Metric;

		// Token: 0x04001351 RID: 4945
		public ulong Luid;

		// Token: 0x04001352 RID: 4946
		public Win32_SOCKET_ADDRESS Dhcpv4Server;

		// Token: 0x04001353 RID: 4947
		public uint CompartmentId;

		// Token: 0x04001354 RID: 4948
		public ulong NetworkGuid;

		// Token: 0x04001355 RID: 4949
		public int ConnectionType;

		// Token: 0x04001356 RID: 4950
		public int TunnelType;

		// Token: 0x04001357 RID: 4951
		public Win32_SOCKET_ADDRESS Dhcpv6Server;

		// Token: 0x04001358 RID: 4952
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 130)]
		public byte[] Dhcpv6ClientDuid;

		// Token: 0x04001359 RID: 4953
		public ulong Dhcpv6ClientDuidLength;

		// Token: 0x0400135A RID: 4954
		public ulong Dhcpv6Iaid;

		// Token: 0x0400135B RID: 4955
		public IntPtr FirstDnsSuffix;
	}
}
