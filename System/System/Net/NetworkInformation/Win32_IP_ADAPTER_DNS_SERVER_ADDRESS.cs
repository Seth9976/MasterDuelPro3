using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200047B RID: 1147
	internal struct Win32_IP_ADAPTER_DNS_SERVER_ADDRESS
	{
		// Token: 0x0400137A RID: 4986
		public Win32LengthFlagsUnion LengthFlags;

		// Token: 0x0400137B RID: 4987
		public IntPtr Next;

		// Token: 0x0400137C RID: 4988
		public Win32_SOCKET_ADDRESS Address;
	}
}
