using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace Microsoft.Win32
{
	// Token: 0x02000003 RID: 3
	internal static class UnsafeNativeMethods
	{
		// Token: 0x06000004 RID: 4
		[DllImport("kernel32.dll", EntryPoint = "GetCurrentPackageId")]
		[return: MarshalAs(UnmanagedType.I4)]
		private static extern int _GetCurrentPackageId(ref int pBufferLength, byte[] pBuffer);

		// Token: 0x06000005 RID: 5
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr GetProcAddress(IntPtr hModule, string methodName);

		// Token: 0x06000006 RID: 6
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr GetModuleHandle(string moduleName);

		// Token: 0x06000007 RID: 7 RVA: 0x00002078 File Offset: 0x00000278
		private static bool DoesWin32MethodExist(string moduleName, string methodName)
		{
			IntPtr moduleHandle = UnsafeNativeMethods.GetModuleHandle(moduleName);
			return !(moduleHandle == IntPtr.Zero) && UnsafeNativeMethods.GetProcAddress(moduleHandle, methodName) != IntPtr.Zero;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020AC File Offset: 0x000002AC
		private static bool _IsPackagedProcess()
		{
			OperatingSystem osversion = Environment.OSVersion;
			if (osversion.Platform == PlatformID.Win32NT && osversion.Version >= new Version(6, 2, 0, 0) && UnsafeNativeMethods.DoesWin32MethodExist("kernel32.dll", "GetCurrentPackageId"))
			{
				int num = 0;
				return UnsafeNativeMethods._GetCurrentPackageId(ref num, null) == 122;
			}
			return false;
		}

		// Token: 0x04000001 RID: 1
		internal static Lazy<bool> IsPackagedProcess = new Lazy<bool>(() => UnsafeNativeMethods._IsPackagedProcess());
	}
}
