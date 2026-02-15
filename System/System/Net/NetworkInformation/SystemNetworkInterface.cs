using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000462 RID: 1122
	internal static class SystemNetworkInterface
	{
		// Token: 0x06001C41 RID: 7233 RVA: 0x0007BEE4 File Offset: 0x0007A0E4
		public static NetworkInterface[] GetNetworkInterfaces()
		{
			NetworkInterface[] array;
			try
			{
				array = SystemNetworkInterface.nif.GetAllNetworkInterfaces();
			}
			catch
			{
				array = new NetworkInterface[0];
			}
			return array;
		}

		// Token: 0x04001306 RID: 4870
		private static readonly NetworkInterfaceFactory nif = NetworkInterfaceFactory.Create();
	}
}
