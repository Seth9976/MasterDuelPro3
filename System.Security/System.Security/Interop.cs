using System;
using System.Runtime.InteropServices;
using System.Text;

// Token: 0x02000002 RID: 2
internal static class Interop
{
	// Token: 0x02000003 RID: 3
	internal class Kernel32
	{
		// Token: 0x06000001 RID: 1
		[DllImport("kernel32.dll", BestFitMapping = true, CharSet = CharSet.Unicode, EntryPoint = "FormatMessageW", SetLastError = true)]
		private static extern int FormatMessage(int dwFlags, IntPtr lpSource, uint dwMessageId, int dwLanguageId, [Out] StringBuilder lpBuffer, int nSize, IntPtr[] arguments);

		// Token: 0x06000002 RID: 2 RVA: 0x00002050 File Offset: 0x00000250
		internal static string GetMessage(int errorCode)
		{
			return global::Interop.Kernel32.GetMessage(IntPtr.Zero, errorCode);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002060 File Offset: 0x00000260
		internal static string GetMessage(IntPtr moduleHandle, int errorCode)
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			string text;
			while (!global::Interop.Kernel32.TryGetErrorMessage(moduleHandle, errorCode, stringBuilder, out text))
			{
				stringBuilder.Capacity *= 4;
				if (stringBuilder.Capacity >= 66560)
				{
					return string.Format("Unknown error (0x{0:x})", errorCode);
				}
			}
			return text;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020B4 File Offset: 0x000002B4
		private static bool TryGetErrorMessage(IntPtr moduleHandle, int errorCode, StringBuilder sb, out string errorMsg)
		{
			errorMsg = "";
			int num = 12800;
			if (moduleHandle != IntPtr.Zero)
			{
				num |= 2048;
			}
			if (global::Interop.Kernel32.FormatMessage(num, moduleHandle, (uint)errorCode, 0, sb, sb.Capacity, null) != 0)
			{
				int i;
				for (i = sb.Length; i > 0; i--)
				{
					char c = sb[i - 1];
					if (c > ' ' && c != '.')
					{
						break;
					}
				}
				errorMsg = sb.ToString(0, i);
			}
			else
			{
				if (Marshal.GetLastWin32Error() == 122)
				{
					return false;
				}
				errorMsg = string.Format("Unknown error (0x{0:x})", errorCode);
			}
			return true;
		}
	}

	// Token: 0x02000004 RID: 4
	internal class Crypt32
	{
		// Token: 0x06000005 RID: 5
		[DllImport("crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool CryptProtectData([In] ref global::Interop.Crypt32.DATA_BLOB pDataIn, [In] string szDataDescr, [In] ref global::Interop.Crypt32.DATA_BLOB pOptionalEntropy, [In] IntPtr pvReserved, [In] IntPtr pPromptStruct, [In] global::Interop.Crypt32.CryptProtectDataFlags dwFlags, out global::Interop.Crypt32.DATA_BLOB pDataOut);

		// Token: 0x06000006 RID: 6
		[DllImport("crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool CryptUnprotectData([In] ref global::Interop.Crypt32.DATA_BLOB pDataIn, [In] IntPtr ppszDataDescr, [In] ref global::Interop.Crypt32.DATA_BLOB pOptionalEntropy, [In] IntPtr pvReserved, [In] IntPtr pPromptStruct, [In] global::Interop.Crypt32.CryptProtectDataFlags dwFlags, out global::Interop.Crypt32.DATA_BLOB pDataOut);

		// Token: 0x02000005 RID: 5
		[Flags]
		internal enum CryptProtectDataFlags
		{
			// Token: 0x04000002 RID: 2
			CRYPTPROTECT_UI_FORBIDDEN = 1,
			// Token: 0x04000003 RID: 3
			CRYPTPROTECT_LOCAL_MACHINE = 4,
			// Token: 0x04000004 RID: 4
			CRYPTPROTECT_CRED_SYNC = 8,
			// Token: 0x04000005 RID: 5
			CRYPTPROTECT_AUDIT = 16,
			// Token: 0x04000006 RID: 6
			CRYPTPROTECT_NO_RECOVERY = 32,
			// Token: 0x04000007 RID: 7
			CRYPTPROTECT_VERIFY_PROTECTION = 64
		}

		// Token: 0x02000006 RID: 6
		internal struct DATA_BLOB
		{
			// Token: 0x06000007 RID: 7 RVA: 0x00002145 File Offset: 0x00000345
			internal DATA_BLOB(IntPtr handle, uint size)
			{
				this.cbData = size;
				this.pbData = handle;
			}

			// Token: 0x04000008 RID: 8
			internal uint cbData;

			// Token: 0x04000009 RID: 9
			internal IntPtr pbData;
		}
	}
}
