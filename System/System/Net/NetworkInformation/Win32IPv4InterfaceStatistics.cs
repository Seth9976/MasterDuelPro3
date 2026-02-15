using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200046C RID: 1132
	internal class Win32IPv4InterfaceStatistics : IPv4InterfaceStatistics
	{
		// Token: 0x06001C5B RID: 7259 RVA: 0x0007C2D5 File Offset: 0x0007A4D5
		public Win32IPv4InterfaceStatistics(Win32_MIB_IFROW info)
		{
			this.info = info;
		}

		// Token: 0x04001318 RID: 4888
		private Win32_MIB_IFROW info;
	}
}
