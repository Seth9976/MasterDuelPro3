using System;

namespace YgomSystem.Utility
{
	// Token: 0x020004F6 RID: 1270
	public class AppInfo
	{
		// Token: 0x040028E2 RID: 10466
		public const string AppIdentifier = "AMAA";

		// Token: 0x040028E3 RID: 10467
		public static AppInfo.BootType bootType;

		// Token: 0x020004F7 RID: 1271
		public enum BootType
		{
			// Token: 0x040028E5 RID: 10469
			StartUp,
			// Token: 0x040028E6 RID: 10470
			ExitReboot,
			// Token: 0x040028E7 RID: 10471
			TitleLoopReboot
		}
	}
}
