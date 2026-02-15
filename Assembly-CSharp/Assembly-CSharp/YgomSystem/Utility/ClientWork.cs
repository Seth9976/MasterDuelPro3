using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace YgomSystem.Utility
{
	// Token: 0x0200050D RID: 1293
	public static class ClientWork
	{
		// Token: 0x0600286B RID: 10347 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ClearData()
		{
		}

		// Token: 0x0600286C RID: 10348 RVA: 0x0000216D File Offset: 0x0000036D
		internal static void dirty(object add, ClientWork.notyfyEvent localnotyfy)
		{
		}

		// Token: 0x0600286D RID: 10349 RVA: 0x0000216D File Offset: 0x0000036D
		internal static void dirtyDictionary(Dictionary<string, object> dicadd, ClientWork.notyfyEvent localnotyfy)
		{
		}

		// Token: 0x0600286E RID: 10350 RVA: 0x0000216D File Offset: 0x0000036D
		internal static void dirtyList(List<object> listadd, ClientWork.notyfyEvent localnotyfy)
		{
		}

		// Token: 0x0600286F RID: 10351 RVA: 0x0000216D File Offset: 0x0000036D
		internal static void marge(object dst, object add)
		{
		}

		// Token: 0x06002870 RID: 10352 RVA: 0x0000216D File Offset: 0x0000036D
		internal static void margeDictionary(Dictionary<string, object> dicdst, Dictionary<string, object> dicadd)
		{
		}

		// Token: 0x06002871 RID: 10353 RVA: 0x0000216D File Offset: 0x0000036D
		internal static void margeList(List<object> listdst, List<object> listadd)
		{
		}

		// Token: 0x06002872 RID: 10354 RVA: 0x0000216A File Offset: 0x0000036A
		internal static ClientWork.notyfyEvent findNotificator(string jsonPath)
		{
			return null;
		}

		// Token: 0x06002873 RID: 10355 RVA: 0x000F1A54 File Offset: 0x000EFC54
		internal static T getTypedByJsonPath<T>(string jsonPath, T defaultValue)
		{
			return default(T);
		}

		// Token: 0x06002874 RID: 10356 RVA: 0x0000216D File Offset: 0x0000036D
		public static void update(object dict, bool keep = false)
		{
		}

		// Token: 0x06002875 RID: 10357 RVA: 0x0000216D File Offset: 0x0000036D
		public static void update(string jsonPath, object dict, bool keep = false)
		{
		}

		// Token: 0x06002876 RID: 10358 RVA: 0x0000216D File Offset: 0x0000036D
		public static void updateValue(string jsonPath, object value, bool keep = false)
		{
		}

		// Token: 0x06002877 RID: 10359 RVA: 0x0000216D File Offset: 0x0000036D
		public static void updateJson(string jsonString)
		{
		}

		// Token: 0x06002878 RID: 10360 RVA: 0x0000216D File Offset: 0x0000036D
		public static void updateJson(string jsonPath, string jsonString)
		{
		}

		// Token: 0x06002879 RID: 10361 RVA: 0x0000216D File Offset: 0x0000036D
		public static void updateKeep()
		{
		}

		// Token: 0x0600287A RID: 10362 RVA: 0x0000216D File Offset: 0x0000036D
		public static void clearKeep()
		{
		}

		// Token: 0x0600287B RID: 10363 RVA: 0x0000216D File Offset: 0x0000036D
		public static void deleteByJsonPath(string jsonPath, bool keep = false)
		{
		}

		// Token: 0x0600287C RID: 10364 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int getRevision()
		{
			return 0;
		}

		// Token: 0x0600287D RID: 10365 RVA: 0x0000216A File Offset: 0x0000036A
		public static object getByJsonPath(string jsonPath)
		{
			return null;
		}

		// Token: 0x0600287E RID: 10366 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int getIntByJsonPath(string jsonPath, int defaultValue = 0)
		{
			return 0;
		}

		// Token: 0x0600287F RID: 10367 RVA: 0x000F1669 File Offset: 0x000EF869
		public static long getLongByJsonPath(string jsonPath, long defaultValue = 0L)
		{
			return 0L;
		}

		// Token: 0x06002880 RID: 10368 RVA: 0x000029C5 File Offset: 0x00000BC5
		public static float getFloatByJsonPath(string jsonPath, float defaultValue = 0f)
		{
			return 0f;
		}

		// Token: 0x06002881 RID: 10369 RVA: 0x0000216A File Offset: 0x0000036A
		public static string getStringByJsonPath(string jsonPath, string defaultValue = "")
		{
			return null;
		}

		// Token: 0x06002882 RID: 10370 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool getBoolByJsonPath(string jsonPath, bool defaultValue = false)
		{
			return false;
		}

		// Token: 0x06002883 RID: 10371 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> getDictionaryByJsonPath(string jsonPath, Dictionary<string, object> defaultValue = null)
		{
			return null;
		}

		// Token: 0x06002884 RID: 10372 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<object> getListByJsonPath(string jsonPath, List<object> defaultValue = null)
		{
			return null;
		}

		// Token: 0x06002885 RID: 10373 RVA: 0x0000216A File Offset: 0x0000036A
		public static object getObjectByJsonPath(string jsonPath, object defaultValue = null)
		{
			return null;
		}

		// Token: 0x06002886 RID: 10374 RVA: 0x0000216A File Offset: 0x0000036A
		public static object getByJsonPathWithCache(string jsonPath)
		{
			return null;
		}

		// Token: 0x06002887 RID: 10375 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool ContainsJsonPath(string jsonPath)
		{
			return false;
		}

		// Token: 0x06002888 RID: 10376 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetNotificator(string jsonPath, ClientWork.NotyfyEventHandler handle)
		{
		}

		// Token: 0x06002889 RID: 10377 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ResetNotificator(string jsonPath, ClientWork.NotyfyEventHandler handle)
		{
		}

		// Token: 0x0600288A RID: 10378 RVA: 0x0000216D File Offset: 0x0000036D
		public static void DebugDump()
		{
		}

		// Token: 0x0600288B RID: 10379 RVA: 0x0000216A File Offset: 0x0000036A
		public static ClientWork.notyfyEvent GetDebugNotyfyEvent(string jsonPath)
		{
			return null;
		}

		// Token: 0x04002926 RID: 10534
		private static int s_revision;

		// Token: 0x04002927 RID: 10535
		private static Dictionary<string, object> s_data;

		// Token: 0x04002928 RID: 10536
		private static ClientWork.notyfyEvent s_notyfy;

		// Token: 0x04002929 RID: 10537
		private static SortedDictionary<string, object> s_cache;

		// Token: 0x0400292A RID: 10538
		private static List<ClientWork.UpdateInfo> s_keepUpdateList;

		// Token: 0x0400292B RID: 10539
		private static List<string> s_keepDeleteList;

		// Token: 0x0200050E RID: 1294
		private struct UpdateInfo
		{
			// Token: 0x0400292C RID: 10540
			public string path;

			// Token: 0x0400292D RID: 10541
			public object data;
		}

		// Token: 0x0200050F RID: 1295
		// (Invoke) Token: 0x0600288D RID: 10381
		public delegate void NotyfyEventHandler(object v);

		// Token: 0x02000510 RID: 1296
		public class notyfyEvent
		{
			// Token: 0x14000020 RID: 32
			// (add) Token: 0x06002890 RID: 10384 RVA: 0x0000216D File Offset: 0x0000036D
			// (remove) Token: 0x06002891 RID: 10385 RVA: 0x0000216D File Offset: 0x0000036D
			public event ClientWork.NotyfyEventHandler handler
			{
				[CompilerGenerated]
				add
				{
				}
				[CompilerGenerated]
				remove
				{
				}
			}

			// Token: 0x06002892 RID: 10386 RVA: 0x0000216D File Offset: 0x0000036D
			public void callHandle(Dictionary<string, object> dic)
			{
			}

			// Token: 0x06002893 RID: 10387 RVA: 0x0000216D File Offset: 0x0000036D
			public void notyfyDirty(object val)
			{
			}

			// Token: 0x06002894 RID: 10388 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool isLeaf()
			{
				return false;
			}

			// Token: 0x06002895 RID: 10389 RVA: 0x000029CC File Offset: 0x00000BCC
			public int getLeafCount()
			{
				return 0;
			}

			// Token: 0x0400292E RID: 10542
			public Dictionary<string, ClientWork.notyfyEvent> entry;

			// Token: 0x0400292F RID: 10543
			public bool dirty;

			// Token: 0x04002930 RID: 10544
			public bool modify;
		}
	}
}
