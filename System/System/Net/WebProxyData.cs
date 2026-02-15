using System;
using System.Collections;

namespace System.Net
{
	// Token: 0x020003EF RID: 1007
	internal class WebProxyData
	{
		// Token: 0x04000FFA RID: 4090
		internal bool bypassOnLocal;

		// Token: 0x04000FFB RID: 4091
		internal bool automaticallyDetectSettings;

		// Token: 0x04000FFC RID: 4092
		internal Uri proxyAddress;

		// Token: 0x04000FFD RID: 4093
		internal Hashtable proxyHostAddresses;

		// Token: 0x04000FFE RID: 4094
		internal Uri scriptLocation;

		// Token: 0x04000FFF RID: 4095
		internal ArrayList bypassList;
	}
}
