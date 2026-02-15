using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x020001C7 RID: 455
	internal static class ConsoleDriver
	{
		// Token: 0x060011E4 RID: 4580 RVA: 0x00048AD0 File Offset: 0x00046CD0
		static ConsoleDriver()
		{
			if (!ConsoleDriver.IsConsole)
			{
				ConsoleDriver.driver = ConsoleDriver.CreateNullConsoleDriver();
				return;
			}
			if (Environment.IsRunningOnWindows)
			{
				ConsoleDriver.driver = ConsoleDriver.CreateWindowsConsoleDriver();
				return;
			}
			string environmentVariable = Environment.GetEnvironmentVariable("TERM");
			if (environmentVariable == "dumb")
			{
				ConsoleDriver.is_console = false;
				ConsoleDriver.driver = ConsoleDriver.CreateNullConsoleDriver();
				return;
			}
			ConsoleDriver.driver = ConsoleDriver.CreateTermInfoDriver(environmentVariable);
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x00048B35 File Offset: 0x00046D35
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static IConsoleDriver CreateNullConsoleDriver()
		{
			return new NullConsoleDriver();
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x00048B3C File Offset: 0x00046D3C
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static IConsoleDriver CreateWindowsConsoleDriver()
		{
			return new WindowsConsoleDriver();
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x00048B43 File Offset: 0x00046D43
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static IConsoleDriver CreateTermInfoDriver(string term)
		{
			return new TermInfoDriver(term);
		}

		// Token: 0x170001CC RID: 460
		// (set) Token: 0x060011E8 RID: 4584 RVA: 0x00048B4B File Offset: 0x00046D4B
		public static ConsoleColor BackgroundColor
		{
			set
			{
				if (value < ConsoleColor.Black || value > ConsoleColor.White)
				{
					throw new ArgumentOutOfRangeException("value", "Not a ConsoleColor value.");
				}
				ConsoleDriver.driver.BackgroundColor = value;
			}
		}

		// Token: 0x170001CD RID: 461
		// (set) Token: 0x060011E9 RID: 4585 RVA: 0x00048B71 File Offset: 0x00046D71
		public static ConsoleColor ForegroundColor
		{
			set
			{
				if (value < ConsoleColor.Black || value > ConsoleColor.White)
				{
					throw new ArgumentOutOfRangeException("value", "Not a ConsoleColor value.");
				}
				ConsoleDriver.driver.ForegroundColor = value;
			}
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x00048B97 File Offset: 0x00046D97
		public static ConsoleKeyInfo ReadKey(bool intercept)
		{
			return ConsoleDriver.driver.ReadKey(intercept);
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x00048BA4 File Offset: 0x00046DA4
		public static void ResetColor()
		{
			ConsoleDriver.driver.ResetColor();
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060011EC RID: 4588 RVA: 0x00048BB0 File Offset: 0x00046DB0
		public static bool IsConsole
		{
			get
			{
				if (ConsoleDriver.called_isatty)
				{
					return ConsoleDriver.is_console;
				}
				ConsoleDriver.is_console = ConsoleDriver.Isatty(MonoIO.ConsoleOutput) && ConsoleDriver.Isatty(MonoIO.ConsoleInput);
				ConsoleDriver.called_isatty = true;
				return ConsoleDriver.is_console;
			}
		}

		// Token: 0x060011ED RID: 4589
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Isatty(IntPtr handle);

		// Token: 0x060011EE RID: 4590
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int InternalKeyAvailable(int ms_timeout);

		// Token: 0x060011EF RID: 4591
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern bool TtySetup(string keypadXmit, string teardown, out byte[] control_characters, out int* address);

		// Token: 0x060011F0 RID: 4592
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool SetEcho(bool wantEcho);

		// Token: 0x0400073D RID: 1853
		internal static IConsoleDriver driver;

		// Token: 0x0400073E RID: 1854
		private static bool is_console;

		// Token: 0x0400073F RID: 1855
		private static bool called_isatty;
	}
}
