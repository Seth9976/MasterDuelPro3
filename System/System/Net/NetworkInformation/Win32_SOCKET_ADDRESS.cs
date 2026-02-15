using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200047D RID: 1149
	internal struct Win32_SOCKET_ADDRESS
	{
		// Token: 0x06001C69 RID: 7273 RVA: 0x0007C608 File Offset: 0x0007A808
		public IPAddress GetIPAddress()
		{
			Win32_SOCKADDR win32_SOCKADDR = (Win32_SOCKADDR)Marshal.PtrToStructure(this.Sockaddr, typeof(Win32_SOCKADDR));
			byte[] array;
			if (win32_SOCKADDR.AddressFamily == 23)
			{
				array = new byte[16];
				Array.Copy(win32_SOCKADDR.AddressData, 6, array, 0, 16);
			}
			else
			{
				array = new byte[4];
				Array.Copy(win32_SOCKADDR.AddressData, 2, array, 0, 4);
			}
			return new IPAddress(array);
		}

		// Token: 0x0400137F RID: 4991
		public IntPtr Sockaddr;

		// Token: 0x04001380 RID: 4992
		public int SockaddrLength;
	}
}
