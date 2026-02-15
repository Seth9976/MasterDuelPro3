using System;

namespace Microsoft.Win32
{
	// Token: 0x02000087 RID: 135
	internal static class Win32Native
	{
		// Token: 0x06000299 RID: 665 RVA: 0x000106B3 File Offset: 0x0000E8B3
		public static string GetMessage(int hr)
		{
			return "Error " + hr.ToString();
		}

		// Token: 0x0600029A RID: 666 RVA: 0x000106C6 File Offset: 0x0000E8C6
		public static int MakeHRFromErrorCode(int errorCode)
		{
			return -2147024896 | errorCode;
		}
	}
}
