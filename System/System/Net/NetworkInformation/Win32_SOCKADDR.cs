using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200047C RID: 1148
	internal struct Win32_SOCKADDR
	{
		// Token: 0x0400137D RID: 4989
		public ushort AddressFamily;

		// Token: 0x0400137E RID: 4990
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 28)]
		public byte[] AddressData;
	}
}
