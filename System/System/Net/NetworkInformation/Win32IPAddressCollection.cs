using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000465 RID: 1125
	internal class Win32IPAddressCollection : IPAddressCollection
	{
		// Token: 0x06001C47 RID: 7239 RVA: 0x0007BF56 File Offset: 0x0007A156
		private Win32IPAddressCollection()
		{
		}

		// Token: 0x06001C48 RID: 7240 RVA: 0x0007BF60 File Offset: 0x0007A160
		public Win32IPAddressCollection(params IntPtr[] heads)
		{
			foreach (IntPtr intPtr in heads)
			{
				this.AddSubsequentlyString(intPtr);
			}
		}

		// Token: 0x06001C49 RID: 7241 RVA: 0x0007BF90 File Offset: 0x0007A190
		public static Win32IPAddressCollection FromDnsServer(IntPtr ptr)
		{
			Win32IPAddressCollection win32IPAddressCollection = new Win32IPAddressCollection();
			IntPtr intPtr = ptr;
			while (intPtr != IntPtr.Zero)
			{
				Win32_IP_ADAPTER_DNS_SERVER_ADDRESS win32_IP_ADAPTER_DNS_SERVER_ADDRESS = (Win32_IP_ADAPTER_DNS_SERVER_ADDRESS)Marshal.PtrToStructure(intPtr, typeof(Win32_IP_ADAPTER_DNS_SERVER_ADDRESS));
				win32IPAddressCollection.InternalAdd(win32_IP_ADAPTER_DNS_SERVER_ADDRESS.Address.GetIPAddress());
				intPtr = win32_IP_ADAPTER_DNS_SERVER_ADDRESS.Next;
			}
			return win32IPAddressCollection;
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x0007BFE4 File Offset: 0x0007A1E4
		private void AddSubsequentlyString(IntPtr head)
		{
			IntPtr intPtr = head;
			while (intPtr != IntPtr.Zero)
			{
				Win32_IP_ADDR_STRING win32_IP_ADDR_STRING = (Win32_IP_ADDR_STRING)Marshal.PtrToStructure(intPtr, typeof(Win32_IP_ADDR_STRING));
				base.InternalAdd(IPAddress.Parse(win32_IP_ADDR_STRING.IpAddress));
				intPtr = win32_IP_ADDR_STRING.Next;
			}
		}

		// Token: 0x04001307 RID: 4871
		public static readonly Win32IPAddressCollection Empty = new Win32IPAddressCollection(new IntPtr[] { IntPtr.Zero });
	}
}
