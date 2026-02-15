using System;

namespace YgomSystem.Utility
{
	// Token: 0x02000534 RID: 1332
	public static class PlatformDebugUtil
	{
		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06002AA2 RID: 10914 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool isPlatformDebugActive
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06002AA3 RID: 10915 RVA: 0x000029CC File Offset: 0x00000BCC
		public static DeviceInfo.Platform debugPlatform
		{
			get
			{
				return DeviceInfo.Platform.Unknown;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06002AA4 RID: 10916 RVA: 0x000029CC File Offset: 0x00000BCC
		public static DeviceInfo.PlatformType debugPlatformType
		{
			get
			{
				return DeviceInfo.PlatformType.Unknown;
			}
		}

		// Token: 0x040029C5 RID: 10693
		public const string prefsKeyDebugActive = "PlatformDebug";

		// Token: 0x040029C6 RID: 10694
		public const string prefsKeyPlatform = "PlatformDebugType";

		// Token: 0x040029C7 RID: 10695
		public const string prefsKeyDisplayToolbar = "PlatformDebugDisplayToolbar";
	}
}
