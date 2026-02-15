using System;
using System.Runtime.InteropServices;

namespace Mono.Unix.Native
{
	// Token: 0x02000006 RID: 6
	public class Stdlib
	{
		// Token: 0x0600000C RID: 12
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Unix_VersionString")]
		private static extern IntPtr VersionStringPtr();

		// Token: 0x0600000D RID: 13 RVA: 0x000021E4 File Offset: 0x000003E4
		internal static void VersionCheck()
		{
			if (Stdlib.versionCheckPerformed)
			{
				return;
			}
			string text = "MonoProject-2015-12-1";
			string text2 = Marshal.PtrToStringAnsi(Stdlib.VersionStringPtr());
			if (text != text2)
			{
				throw new Exception(string.Concat(new string[] { "Mono.Posix assembly loaded with a different version (\"", text, "\") than MonoPosixHelper (\"", text2, "\"). You may need to reinstall Mono.Posix." }));
			}
			Stdlib.versionCheckPerformed = true;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000224C File Offset: 0x0000044C
		static Stdlib()
		{
			Stdlib.VersionCheck();
		}

		// Token: 0x0600000F RID: 15
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_SIG_DFL")]
		private static extern IntPtr GetDefaultSignal();

		// Token: 0x06000010 RID: 16
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_SIG_ERR")]
		private static extern IntPtr GetErrorSignal();

		// Token: 0x06000011 RID: 17
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_SIG_IGN")]
		private static extern IntPtr GetIgnoreSignal();

		// Token: 0x06000012 RID: 18 RVA: 0x00002369 File Offset: 0x00000569
		private static void _ErrorHandler(int signum)
		{
			Console.Error.WriteLine("Error handler invoked for signum " + signum + ".  Don't do that.");
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000238A File Offset: 0x0000058A
		private static void _DefaultHandler(int signum)
		{
			Console.Error.WriteLine("Default handler invoked for signum " + signum + ".  Don't do that.");
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000023AB File Offset: 0x000005AB
		private static void _IgnoreHandler(int signum)
		{
			Console.Error.WriteLine("Ignore handler invoked for signum " + signum + ".  Don't do that.");
		}

		// Token: 0x06000015 RID: 21
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib__IOFBF")]
		private static extern int GetFullyBuffered();

		// Token: 0x06000016 RID: 22
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib__IOLBF")]
		private static extern int GetLineBuffered();

		// Token: 0x06000017 RID: 23
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib__IONBF")]
		private static extern int GetNonBuffered();

		// Token: 0x06000018 RID: 24
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_BUFSIZ")]
		private static extern int GetBufferSize();

		// Token: 0x06000019 RID: 25
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_EOF")]
		private static extern int GetEOF();

		// Token: 0x0600001A RID: 26
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_FILENAME_MAX")]
		private static extern int GetFilenameMax();

		// Token: 0x0600001B RID: 27
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_FOPEN_MAX")]
		private static extern int GetFopenMax();

		// Token: 0x0600001C RID: 28
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_L_tmpnam")]
		private static extern int GetTmpnamLength();

		// Token: 0x0600001D RID: 29
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_stdin")]
		private static extern IntPtr GetStandardInput();

		// Token: 0x0600001E RID: 30
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_stdout")]
		private static extern IntPtr GetStandardOutput();

		// Token: 0x0600001F RID: 31
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_stderr")]
		private static extern IntPtr GetStandardError();

		// Token: 0x06000020 RID: 32
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_TMP_MAX")]
		private static extern int GetTmpMax();

		// Token: 0x06000021 RID: 33
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_EXIT_FAILURE")]
		private static extern int GetExitFailure();

		// Token: 0x06000022 RID: 34
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_EXIT_SUCCESS")]
		private static extern int GetExitSuccess();

		// Token: 0x06000023 RID: 35
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_MB_CUR_MAX")]
		private static extern int GetMbCurMax();

		// Token: 0x06000024 RID: 36
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_RAND_MAX")]
		private static extern int GetRandMax();

		// Token: 0x04000006 RID: 6
		private static bool versionCheckPerformed = false;

		// Token: 0x04000007 RID: 7
		private static readonly IntPtr _SIG_DFL = Stdlib.GetDefaultSignal();

		// Token: 0x04000008 RID: 8
		private static readonly IntPtr _SIG_ERR = Stdlib.GetErrorSignal();

		// Token: 0x04000009 RID: 9
		private static readonly IntPtr _SIG_IGN = Stdlib.GetIgnoreSignal();

		// Token: 0x0400000A RID: 10
		[CLSCompliant(false)]
		public static readonly SignalHandler SIG_DFL = new SignalHandler(Stdlib._DefaultHandler);

		// Token: 0x0400000B RID: 11
		[CLSCompliant(false)]
		public static readonly SignalHandler SIG_ERR = new SignalHandler(Stdlib._ErrorHandler);

		// Token: 0x0400000C RID: 12
		[CLSCompliant(false)]
		public static readonly SignalHandler SIG_IGN = new SignalHandler(Stdlib._IgnoreHandler);

		// Token: 0x0400000D RID: 13
		[CLSCompliant(false)]
		public static readonly int _IOFBF = Stdlib.GetFullyBuffered();

		// Token: 0x0400000E RID: 14
		[CLSCompliant(false)]
		public static readonly int _IOLBF = Stdlib.GetLineBuffered();

		// Token: 0x0400000F RID: 15
		[CLSCompliant(false)]
		public static readonly int _IONBF = Stdlib.GetNonBuffered();

		// Token: 0x04000010 RID: 16
		[CLSCompliant(false)]
		public static readonly int BUFSIZ = Stdlib.GetBufferSize();

		// Token: 0x04000011 RID: 17
		[CLSCompliant(false)]
		public static readonly int EOF = Stdlib.GetEOF();

		// Token: 0x04000012 RID: 18
		[CLSCompliant(false)]
		public static readonly int FOPEN_MAX = Stdlib.GetFopenMax();

		// Token: 0x04000013 RID: 19
		[CLSCompliant(false)]
		public static readonly int FILENAME_MAX = Stdlib.GetFilenameMax();

		// Token: 0x04000014 RID: 20
		[CLSCompliant(false)]
		public static readonly int L_tmpnam = Stdlib.GetTmpnamLength();

		// Token: 0x04000015 RID: 21
		public static readonly IntPtr stderr = Stdlib.GetStandardError();

		// Token: 0x04000016 RID: 22
		public static readonly IntPtr stdin = Stdlib.GetStandardInput();

		// Token: 0x04000017 RID: 23
		public static readonly IntPtr stdout = Stdlib.GetStandardOutput();

		// Token: 0x04000018 RID: 24
		[CLSCompliant(false)]
		public static readonly int TMP_MAX = Stdlib.GetTmpMax();

		// Token: 0x04000019 RID: 25
		private static object tmpnam_lock = new object();

		// Token: 0x0400001A RID: 26
		[CLSCompliant(false)]
		public static readonly int EXIT_FAILURE = Stdlib.GetExitFailure();

		// Token: 0x0400001B RID: 27
		[CLSCompliant(false)]
		public static readonly int EXIT_SUCCESS = Stdlib.GetExitSuccess();

		// Token: 0x0400001C RID: 28
		[CLSCompliant(false)]
		public static readonly int MB_CUR_MAX = Stdlib.GetMbCurMax();

		// Token: 0x0400001D RID: 29
		[CLSCompliant(false)]
		public static readonly int RAND_MAX = Stdlib.GetRandMax();

		// Token: 0x0400001E RID: 30
		private static object strerror_lock = new object();
	}
}
