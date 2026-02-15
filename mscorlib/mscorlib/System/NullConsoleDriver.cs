using System;

namespace System
{
	// Token: 0x020001DA RID: 474
	internal class NullConsoleDriver : IConsoleDriver
	{
		// Token: 0x170001DA RID: 474
		// (set) Token: 0x0600126A RID: 4714 RVA: 0x00002C89 File Offset: 0x00000E89
		public ConsoleColor BackgroundColor
		{
			set
			{
			}
		}

		// Token: 0x170001DB RID: 475
		// (set) Token: 0x0600126B RID: 4715 RVA: 0x00002C89 File Offset: 0x00000E89
		public ConsoleColor ForegroundColor
		{
			set
			{
			}
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x0004ADEC File Offset: 0x00048FEC
		public ConsoleKeyInfo ReadKey(bool intercept)
		{
			return NullConsoleDriver.EmptyConsoleKeyInfo;
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x00002C89 File Offset: 0x00000E89
		public void ResetColor()
		{
		}

		// Token: 0x04000772 RID: 1906
		private static readonly ConsoleKeyInfo EmptyConsoleKeyInfo = new ConsoleKeyInfo('\0', (ConsoleKey)0, false, false, false);
	}
}
