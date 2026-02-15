using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200046D RID: 1133
	internal class Win32NetworkInterfaceAPI : NetworkInterfaceFactory
	{
		// Token: 0x06001C5C RID: 7260
		[DllImport("iphlpapi.dll", SetLastError = true)]
		private static extern int GetAdaptersAddresses(uint family, uint flags, IntPtr reserved, IntPtr info, ref int size);

		// Token: 0x06001C5D RID: 7261 RVA: 0x0007C2E4 File Offset: 0x0007A4E4
		private static Win32_IP_ADAPTER_ADDRESSES[] GetAdaptersAddresses()
		{
			IntPtr intPtr = IntPtr.Zero;
			int num = 0;
			uint num2 = 192U;
			Win32NetworkInterfaceAPI.GetAdaptersAddresses(0U, num2, IntPtr.Zero, intPtr, ref num);
			if (Marshal.SizeOf(typeof(Win32_IP_ADAPTER_ADDRESSES)) > num)
			{
				throw new NetworkInformationException();
			}
			intPtr = Marshal.AllocHGlobal(num);
			int adaptersAddresses = Win32NetworkInterfaceAPI.GetAdaptersAddresses(0U, num2, IntPtr.Zero, intPtr, ref num);
			if (adaptersAddresses != 0)
			{
				throw new NetworkInformationException(adaptersAddresses);
			}
			List<Win32_IP_ADAPTER_ADDRESSES> list = new List<Win32_IP_ADAPTER_ADDRESSES>();
			IntPtr intPtr2 = intPtr;
			while (intPtr2 != IntPtr.Zero)
			{
				Win32_IP_ADAPTER_ADDRESSES win32_IP_ADAPTER_ADDRESSES = Marshal.PtrToStructure<Win32_IP_ADAPTER_ADDRESSES>(intPtr2);
				list.Add(win32_IP_ADAPTER_ADDRESSES);
				intPtr2 = win32_IP_ADAPTER_ADDRESSES.Next;
			}
			return list.ToArray();
		}

		// Token: 0x06001C5E RID: 7262 RVA: 0x0007C384 File Offset: 0x0007A584
		public override NetworkInterface[] GetAllNetworkInterfaces()
		{
			Win32_IP_ADAPTER_ADDRESSES[] adaptersAddresses = Win32NetworkInterfaceAPI.GetAdaptersAddresses();
			NetworkInterface[] array = new NetworkInterface[adaptersAddresses.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new Win32NetworkInterface2(adaptersAddresses[i]);
			}
			return array;
		}
	}
}
