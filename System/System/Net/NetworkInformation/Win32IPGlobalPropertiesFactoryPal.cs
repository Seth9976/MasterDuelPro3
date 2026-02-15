using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200046A RID: 1130
	internal static class Win32IPGlobalPropertiesFactoryPal
	{
		// Token: 0x06001C58 RID: 7256 RVA: 0x0007C29F File Offset: 0x0007A49F
		public static IPGlobalProperties Create()
		{
			return new Win32IPGlobalProperties();
		}
	}
}
