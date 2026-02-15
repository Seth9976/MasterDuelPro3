using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000463 RID: 1123
	internal abstract class NetworkInterfaceFactory
	{
		// Token: 0x06001C43 RID: 7235
		public abstract NetworkInterface[] GetAllNetworkInterfaces();

		// Token: 0x06001C44 RID: 7236 RVA: 0x0007BF28 File Offset: 0x0007A128
		public static NetworkInterfaceFactory Create()
		{
			return NetworkInterfaceFactoryPal.Create();
		}
	}
}
