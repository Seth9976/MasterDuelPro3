using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000470 RID: 1136
	internal class Win32NetworkInterface
	{
		// Token: 0x06001C65 RID: 7269
		[DllImport("iphlpapi.dll", SetLastError = true)]
		private static extern int GetNetworkParams(IntPtr ptr, ref int size);

		// Token: 0x06001C66 RID: 7270
		[DllImport("kernel32.dll", SetLastError = true)]
		private unsafe static extern int MultiByteToWideChar(uint CodePage, uint dwFlags, byte* lpMultiByteStr, int cbMultiByte, char* lpWideCharStr, int cchWideChar);

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06001C67 RID: 7271 RVA: 0x0007C4C4 File Offset: 0x0007A6C4
		public unsafe static Win32_FIXED_INFO FixedInfo
		{
			get
			{
				if (!Win32NetworkInterface.initialized)
				{
					int num = 0;
					Win32NetworkInterface.GetNetworkParams(IntPtr.Zero, ref num);
					IntPtr intPtr = Marshal.AllocHGlobal(num);
					Win32NetworkInterface.GetNetworkParams(intPtr, ref num);
					Win32_FIXED_INFO_Marshal win32_FIXED_INFO_Marshal = Marshal.PtrToStructure<Win32_FIXED_INFO_Marshal>(intPtr);
					Win32NetworkInterface.fixedInfo = new Win32_FIXED_INFO
					{
						HostName = Win32NetworkInterface.<get_FixedInfo>g__GetStringFromMultiByte|5_0(&win32_FIXED_INFO_Marshal.HostName.FixedElementField),
						DomainName = Win32NetworkInterface.<get_FixedInfo>g__GetStringFromMultiByte|5_0(&win32_FIXED_INFO_Marshal.DomainName.FixedElementField),
						CurrentDnsServer = win32_FIXED_INFO_Marshal.CurrentDnsServer,
						DnsServerList = win32_FIXED_INFO_Marshal.DnsServerList,
						NodeType = win32_FIXED_INFO_Marshal.NodeType,
						ScopeId = Win32NetworkInterface.<get_FixedInfo>g__GetStringFromMultiByte|5_0(&win32_FIXED_INFO_Marshal.ScopeId.FixedElementField),
						EnableRouting = win32_FIXED_INFO_Marshal.EnableRouting,
						EnableProxy = win32_FIXED_INFO_Marshal.EnableProxy,
						EnableDns = win32_FIXED_INFO_Marshal.EnableDns
					};
					Win32NetworkInterface.initialized = true;
				}
				return Win32NetworkInterface.fixedInfo;
			}
		}

		// Token: 0x06001C68 RID: 7272 RVA: 0x0007C5B4 File Offset: 0x0007A7B4
		[CompilerGenerated]
		internal unsafe static string <get_FixedInfo>g__GetStringFromMultiByte|5_0(byte* bytes)
		{
			int num = Win32NetworkInterface.MultiByteToWideChar(0U, 0U, bytes, -1, null, 0);
			if (num == 0)
			{
				return string.Empty;
			}
			char[] array2;
			char[] array = (array2 = new char[num]);
			char* ptr;
			if (array == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			Win32NetworkInterface.MultiByteToWideChar(0U, 0U, bytes, -1, ptr, num);
			array2 = null;
			return new string(array);
		}

		// Token: 0x0400131E RID: 4894
		private static Win32_FIXED_INFO fixedInfo;

		// Token: 0x0400131F RID: 4895
		private static bool initialized;
	}
}
