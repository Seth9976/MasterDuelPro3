using System;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using Microsoft.Win32.SafeHandles;

namespace System.Threading
{
	// Token: 0x02000281 RID: 641
	internal static class NativeEventCalls
	{
		// Token: 0x060017D0 RID: 6096 RVA: 0x0005BEC0 File Offset: 0x0005A0C0
		public unsafe static IntPtr CreateEvent_internal(bool manual, bool initial, string name, out int errorCode)
		{
			char* ptr = name;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return NativeEventCalls.CreateEvent_icall(manual, initial, ptr, (name != null) ? name.Length : 0, out errorCode);
		}

		// Token: 0x060017D1 RID: 6097
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern IntPtr CreateEvent_icall(bool manual, bool initial, char* name, int name_length, out int errorCode);

		// Token: 0x060017D2 RID: 6098 RVA: 0x0005BEF4 File Offset: 0x0005A0F4
		public static bool SetEvent(SafeWaitHandle handle)
		{
			bool flag = false;
			bool flag2;
			try
			{
				handle.DangerousAddRef(ref flag);
				flag2 = NativeEventCalls.SetEvent_internal(handle.DangerousGetHandle());
			}
			finally
			{
				if (flag)
				{
					handle.DangerousRelease();
				}
			}
			return flag2;
		}

		// Token: 0x060017D3 RID: 6099
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetEvent_internal(IntPtr handle);

		// Token: 0x060017D4 RID: 6100 RVA: 0x0005BF34 File Offset: 0x0005A134
		public static bool ResetEvent(SafeWaitHandle handle)
		{
			bool flag = false;
			bool flag2;
			try
			{
				handle.DangerousAddRef(ref flag);
				flag2 = NativeEventCalls.ResetEvent_internal(handle.DangerousGetHandle());
			}
			finally
			{
				if (flag)
				{
					handle.DangerousRelease();
				}
			}
			return flag2;
		}

		// Token: 0x060017D5 RID: 6101
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ResetEvent_internal(IntPtr handle);

		// Token: 0x060017D6 RID: 6102
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CloseEvent_internal(IntPtr handle);

		// Token: 0x060017D7 RID: 6103 RVA: 0x0005BF74 File Offset: 0x0005A174
		public unsafe static IntPtr OpenEvent_internal(string name, EventWaitHandleRights rights, out int errorCode)
		{
			char* ptr = name;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return NativeEventCalls.OpenEvent_icall(ptr, (name != null) ? name.Length : 0, rights, out errorCode);
		}

		// Token: 0x060017D8 RID: 6104
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern IntPtr OpenEvent_icall(char* name, int name_length, EventWaitHandleRights rights, out int errorCode);
	}
}
