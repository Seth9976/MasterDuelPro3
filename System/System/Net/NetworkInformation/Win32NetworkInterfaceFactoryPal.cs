using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200046F RID: 1135
	internal static class Win32NetworkInterfaceFactoryPal
	{
		// Token: 0x06001C64 RID: 7268 RVA: 0x0007C494 File Offset: 0x0007A694
		public static NetworkInterfaceFactory Create()
		{
			Version version = new Version(5, 1);
			if (Environment.OSVersion.Version >= version)
			{
				return new Win32NetworkInterfaceAPI();
			}
			return null;
		}
	}
}
