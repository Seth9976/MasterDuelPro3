using System;

namespace YgomSystem.Utility
{
	// Token: 0x02000560 RID: 1376
	public class Version
	{
		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06002BF0 RID: 11248 RVA: 0x0000216A File Offset: 0x0000036A
		public static string AppVersion
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06002BF1 RID: 11249 RVA: 0x0000216A File Offset: 0x0000036A
		public static string AppCommonVersion
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002BF2 RID: 11250 RVA: 0x0000216A File Offset: 0x0000036A
		public static string Revision()
		{
			return null;
		}

		// Token: 0x04002A74 RID: 10868
		private const string APP_COMMON_VERSION = "1.6.1";

		// Token: 0x04002A75 RID: 10869
		private static string rev;
	}
}
