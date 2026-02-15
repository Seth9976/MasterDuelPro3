using System;
using System.Collections.Generic;

namespace YgomSystem
{
	// Token: 0x020004B0 RID: 1200
	public static class PersistentSaveData
	{
		// Token: 0x06002694 RID: 9876 RVA: 0x0000216D File Offset: 0x0000036D
		private static void LogData(Dictionary<string, object> data, string basePath)
		{
		}

		// Token: 0x06002695 RID: 9877 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> LoadPersistenceSystem()
		{
			return null;
		}

		// Token: 0x06002696 RID: 9878 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> LoadPersistenceApp()
		{
			return null;
		}

		// Token: 0x06002697 RID: 9879 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> LoadPersistenceCache()
		{
			return null;
		}

		// Token: 0x06002698 RID: 9880 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OnUpdatePersistenceSystem(object obj)
		{
		}

		// Token: 0x06002699 RID: 9881 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OnUpdatePersistenceApp(object obj)
		{
		}

		// Token: 0x0600269A RID: 9882 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OnUpdatePersistenceCache(object obj)
		{
		}

		// Token: 0x0600269B RID: 9883 RVA: 0x0000216D File Offset: 0x0000036D
		private static void saveToFile(Dictionary<string, object> dic, string savePath)
		{
		}

		// Token: 0x0600269C RID: 9884 RVA: 0x0000216A File Offset: 0x0000036A
		private static Dictionary<string, object> loadFromFile(string savePath)
		{
			return null;
		}

		// Token: 0x040027A5 RID: 10149
		public const string SYSTEM_SAVE_PATH = "SteamPersistence.System";

		// Token: 0x040027A6 RID: 10150
		public const string APP_SAVE_PATH = "SteamPersistence.App";

		// Token: 0x040027A7 RID: 10151
		public const string CACHE_SAVE_PATH = "SteamPersistence.Cache";

		// Token: 0x040027A8 RID: 10152
		public static bool ignoreUpdateEvent;
	}
}
