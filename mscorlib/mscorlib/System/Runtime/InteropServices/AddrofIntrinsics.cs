using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000566 RID: 1382
	internal static class AddrofIntrinsics
	{
		// Token: 0x06002A8B RID: 10891 RVA: 0x000AA45E File Offset: 0x000A865E
		internal static IntPtr AddrOf<T>(T ftn)
		{
			return Marshal.GetFunctionPointerForDelegate<T>(ftn);
		}
	}
}
