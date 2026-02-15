using System;

namespace YgomGame.Download
{
	// Token: 0x02000F50 RID: 3920
	public class DLCVersion
	{
		// Token: 0x06007396 RID: 29590 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool ExistFlag()
		{
			return false;
		}

		// Token: 0x06007397 RID: 29591 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Initialize()
		{
		}

		// Token: 0x06007398 RID: 29592 RVA: 0x0000216D File Offset: 0x0000036D
		private static void DLEndNotificator(object value)
		{
		}

		// Token: 0x06007399 RID: 29593 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Save()
		{
		}

		// Token: 0x0600739A RID: 29594 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Load()
		{
		}

		// Token: 0x0400ACA5 RID: 44197
		private const string kFileName = "dlcVersion";

		// Token: 0x0400ACA6 RID: 44198
		private static bool s_isInitialize;

		// Token: 0x0400ACA7 RID: 44199
		private static bool s_isExistFlag;
	}
}
