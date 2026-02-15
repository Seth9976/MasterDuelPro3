using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000479 RID: 1145
	internal struct Win32_IP_ADDR_STRING
	{
		// Token: 0x04001374 RID: 4980
		public IntPtr Next;

		// Token: 0x04001375 RID: 4981
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
		public string IpAddress;

		// Token: 0x04001376 RID: 4982
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
		public string IpMask;

		// Token: 0x04001377 RID: 4983
		public uint Context;
	}
}
