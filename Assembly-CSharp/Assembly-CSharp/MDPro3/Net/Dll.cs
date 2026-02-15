using System;
using System.Runtime.InteropServices;

namespace MDPro3.Net
{
	// Token: 0x0200130D RID: 4877
	internal static class Dll
	{
		// Token: 0x06008EC9 RID: 36553
		[DllImport("ygoserver", CallingConvention = CallingConvention.Cdecl)]
		public static extern int start_server([MarshalAs(UnmanagedType.LPUTF8Str)] string args);

		// Token: 0x06008ECA RID: 36554
		[DllImport("ygoserver", CallingConvention = CallingConvention.Cdecl)]
		public static extern void stop_server();
	}
}
