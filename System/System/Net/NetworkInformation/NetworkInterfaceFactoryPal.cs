using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000464 RID: 1124
	internal static class NetworkInterfaceFactoryPal
	{
		// Token: 0x06001C46 RID: 7238 RVA: 0x0007BF30 File Offset: 0x0007A130
		public static NetworkInterfaceFactory Create()
		{
			NetworkInterfaceFactory networkInterfaceFactory = UnixNetworkInterfaceFactoryPal.Create();
			if (networkInterfaceFactory == null)
			{
				networkInterfaceFactory = Win32NetworkInterfaceFactoryPal.Create();
			}
			if (networkInterfaceFactory == null)
			{
				throw new NotImplementedException();
			}
			return networkInterfaceFactory;
		}
	}
}
