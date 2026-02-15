using System;
using System.Runtime.InteropServices;

namespace Mono.Btls
{
	// Token: 0x020000A1 RID: 161
	internal static class MonoBtlsError
	{
		// Token: 0x0600029A RID: 666
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_error_clear_error();

		// Token: 0x0600029B RID: 667
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_error_get_error_line(out IntPtr file, out int line);

		// Token: 0x0600029C RID: 668
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_error_get_error_string_n(int error, IntPtr buf, int len);

		// Token: 0x0600029D RID: 669
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_error_get_reason(int error);

		// Token: 0x0600029E RID: 670 RVA: 0x0000A71A File Offset: 0x0000891A
		public static void ClearError()
		{
			MonoBtlsError.mono_btls_error_clear_error();
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000A724 File Offset: 0x00008924
		public static string GetErrorString(int error)
		{
			int num = 1024;
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			if (intPtr == IntPtr.Zero)
			{
				throw new OutOfMemoryException();
			}
			string text;
			try
			{
				MonoBtlsError.mono_btls_error_get_error_string_n(error, intPtr, num);
				text = Marshal.PtrToStringAnsi(intPtr);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return text;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000A77C File Offset: 0x0000897C
		public static int GetError(out string file, out int line)
		{
			IntPtr intPtr;
			int num = MonoBtlsError.mono_btls_error_get_error_line(out intPtr, out line);
			if (intPtr != IntPtr.Zero)
			{
				file = Marshal.PtrToStringAnsi(intPtr);
				return num;
			}
			file = null;
			return num;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000A7AA File Offset: 0x000089AA
		public static int GetErrorReason(int error)
		{
			return MonoBtlsError.mono_btls_error_get_reason(error);
		}
	}
}
