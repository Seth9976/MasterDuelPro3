using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020005BB RID: 1467
	internal static class JitHelpers
	{
		// Token: 0x06002B81 RID: 11137 RVA: 0x000ABEEF File Offset: 0x000AA0EF
		internal static T UnsafeCast<T>(object o) where T : class
		{
			return Array.UnsafeMov<object, T>(o);
		}

		// Token: 0x06002B82 RID: 11138 RVA: 0x000ABEF7 File Offset: 0x000AA0F7
		internal static int UnsafeEnumCast<T>(T val) where T : struct
		{
			return Array.UnsafeMov<T, int>(val);
		}

		// Token: 0x06002B83 RID: 11139 RVA: 0x000ABEFF File Offset: 0x000AA0FF
		internal static long UnsafeEnumCastLong<T>(T val) where T : struct
		{
			return Array.UnsafeMov<T, long>(val);
		}
	}
}
