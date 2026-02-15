using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Win32
{
	// Token: 0x020000E0 RID: 224
	internal static class NativeMethods
	{
		// Token: 0x06000427 RID: 1063 RVA: 0x0000EB6C File Offset: 0x0000CD6C
		public static bool DuplicateHandle(HandleRef hSourceProcessHandle, SafeHandle hSourceHandle, HandleRef hTargetProcess, out SafeWaitHandle targetHandle, int dwDesiredAccess, bool bInheritHandle, int dwOptions)
		{
			bool flag = false;
			bool flag3;
			try
			{
				hSourceHandle.DangerousAddRef(ref flag);
				IntPtr intPtr;
				MonoIOError monoIOError;
				bool flag2 = MonoIO.DuplicateHandle(hSourceProcessHandle.Handle, hSourceHandle.DangerousGetHandle(), hTargetProcess.Handle, out intPtr, dwDesiredAccess, bInheritHandle ? 1 : 0, dwOptions, out monoIOError);
				if (monoIOError != MonoIOError.ERROR_SUCCESS)
				{
					throw MonoIO.GetException(monoIOError);
				}
				targetHandle = new SafeWaitHandle(intPtr, true);
				flag3 = flag2;
			}
			finally
			{
				if (flag)
				{
					hSourceHandle.DangerousRelease();
				}
			}
			return flag3;
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000EBE0 File Offset: 0x0000CDE0
		public static bool DuplicateHandle(HandleRef hSourceProcessHandle, HandleRef hSourceHandle, HandleRef hTargetProcess, out SafeProcessHandle targetHandle, int dwDesiredAccess, bool bInheritHandle, int dwOptions)
		{
			IntPtr intPtr;
			MonoIOError monoIOError;
			bool flag = MonoIO.DuplicateHandle(hSourceProcessHandle.Handle, hSourceHandle.Handle, hTargetProcess.Handle, out intPtr, dwDesiredAccess, bInheritHandle ? 1 : 0, dwOptions, out monoIOError);
			if (monoIOError != MonoIOError.ERROR_SUCCESS)
			{
				throw MonoIO.GetException(monoIOError);
			}
			targetHandle = new SafeProcessHandle(intPtr, true);
			return flag;
		}

		// Token: 0x06000429 RID: 1065
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetCurrentProcess();

		// Token: 0x0600042A RID: 1066
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetExitCodeProcess(IntPtr processHandle, out int exitCode);

		// Token: 0x0600042B RID: 1067 RVA: 0x0000EC2C File Offset: 0x0000CE2C
		public static bool GetExitCodeProcess(SafeProcessHandle processHandle, out int exitCode)
		{
			bool flag = false;
			bool exitCodeProcess;
			try
			{
				processHandle.DangerousAddRef(ref flag);
				exitCodeProcess = NativeMethods.GetExitCodeProcess(processHandle.DangerousGetHandle(), out exitCode);
			}
			finally
			{
				if (flag)
				{
					processHandle.DangerousRelease();
				}
			}
			return exitCodeProcess;
		}

		// Token: 0x0600042C RID: 1068
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetCurrentProcessId();

		// Token: 0x0600042D RID: 1069
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool CloseProcess(IntPtr handle);
	}
}
