using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000461 RID: 1121
	internal static class IPGlobalPropertiesFactoryPal
	{
		// Token: 0x06001C40 RID: 7232 RVA: 0x0007BEBC File Offset: 0x0007A0BC
		public static IPGlobalProperties Create()
		{
			IPGlobalProperties ipglobalProperties = UnixIPGlobalPropertiesFactoryPal.Create();
			if (ipglobalProperties == null)
			{
				ipglobalProperties = Win32IPGlobalPropertiesFactoryPal.Create();
			}
			if (ipglobalProperties == null)
			{
				throw new NotImplementedException();
			}
			return ipglobalProperties;
		}
	}
}
