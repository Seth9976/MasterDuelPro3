using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x0200010D RID: 269
	internal static class Platform
	{
		// Token: 0x06000553 RID: 1363
		[DllImport("libc")]
		private static extern int uname(IntPtr buf);

		// Token: 0x06000554 RID: 1364 RVA: 0x0001C1D4 File Offset: 0x0001A3D4
		private static void CheckOS()
		{
			if (Environment.OSVersion.Platform != PlatformID.Unix)
			{
				Platform.checkedOS = true;
				return;
			}
			IntPtr intPtr = Marshal.AllocHGlobal(8192);
			if (Platform.uname(intPtr) == 0)
			{
				string text = Marshal.PtrToStringAnsi(intPtr);
				if (!(text == "Darwin"))
				{
					if (!(text == "FreeBSD"))
					{
						if (!(text == "AIX"))
						{
							if (!(text == "OS400"))
							{
								if (text == "OpenBSD")
								{
									Platform.isOpenBSD = true;
								}
							}
							else
							{
								Platform.isIBMi = true;
							}
						}
						else
						{
							Platform.isAix = true;
						}
					}
					else
					{
						Platform.isFreeBSD = true;
					}
				}
				else
				{
					Platform.isMacOS = true;
				}
			}
			Marshal.FreeHGlobal(intPtr);
			Platform.checkedOS = true;
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x0001C284 File Offset: 0x0001A484
		public static bool IsMacOS
		{
			get
			{
				if (!Platform.checkedOS)
				{
					try
					{
						Platform.CheckOS();
					}
					catch (DllNotFoundException)
					{
						Platform.isMacOS = false;
					}
				}
				return Platform.isMacOS;
			}
		}

		// Token: 0x0400048A RID: 1162
		private static bool checkedOS;

		// Token: 0x0400048B RID: 1163
		private static bool isMacOS;

		// Token: 0x0400048C RID: 1164
		private static bool isAix;

		// Token: 0x0400048D RID: 1165
		private static bool isIBMi;

		// Token: 0x0400048E RID: 1166
		private static bool isFreeBSD;

		// Token: 0x0400048F RID: 1167
		private static bool isOpenBSD;
	}
}
