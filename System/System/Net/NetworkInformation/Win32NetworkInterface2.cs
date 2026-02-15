using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200046E RID: 1134
	internal sealed class Win32NetworkInterface2 : NetworkInterface
	{
		// Token: 0x06001C60 RID: 7264
		[DllImport("iphlpapi.dll", SetLastError = true)]
		private static extern int GetIfEntry(ref Win32_MIB_IFROW row);

		// Token: 0x06001C61 RID: 7265 RVA: 0x0007C3C8 File Offset: 0x0007A5C8
		internal Win32NetworkInterface2(Win32_IP_ADAPTER_ADDRESSES addr)
		{
			this.addr = addr;
			this.mib4 = default(Win32_MIB_IFROW);
			this.mib4.Index = addr.Alignment.IfIndex;
			if (Win32NetworkInterface2.GetIfEntry(ref this.mib4) != 0)
			{
				this.mib4.Index = -1;
			}
			this.mib6 = default(Win32_MIB_IFROW);
			this.mib6.Index = addr.Ipv6IfIndex;
			if (Win32NetworkInterface2.GetIfEntry(ref this.mib6) != 0)
			{
				this.mib6.Index = -1;
			}
			this.ip4stats = new Win32IPv4InterfaceStatistics(this.mib4);
			this.ip_if_props = new Win32IPInterfaceProperties2(addr, this.mib4, this.mib6);
		}

		// Token: 0x06001C62 RID: 7266 RVA: 0x0007C47C File Offset: 0x0007A67C
		public override IPInterfaceProperties GetIPProperties()
		{
			return this.ip_if_props;
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06001C63 RID: 7267 RVA: 0x0007C484 File Offset: 0x0007A684
		public override NetworkInterfaceType NetworkInterfaceType
		{
			get
			{
				return this.addr.IfType;
			}
		}

		// Token: 0x04001319 RID: 4889
		private Win32_IP_ADAPTER_ADDRESSES addr;

		// Token: 0x0400131A RID: 4890
		private Win32_MIB_IFROW mib4;

		// Token: 0x0400131B RID: 4891
		private Win32_MIB_IFROW mib6;

		// Token: 0x0400131C RID: 4892
		private Win32IPv4InterfaceStatistics ip4stats;

		// Token: 0x0400131D RID: 4893
		private IPInterfaceProperties ip_if_props;
	}
}
