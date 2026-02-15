using System;
using System.Collections.Generic;

namespace YgomSystem.Utility
{
	// Token: 0x0200055E RID: 1374
	public class UrlScheme
	{
		// Token: 0x06002BE2 RID: 11234 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetArgs(string url)
		{
			return null;
		}

		// Token: 0x06002BE3 RID: 11235 RVA: 0x000F1CB6 File Offset: 0x000EFEB6
		public static bool SplitArgs(string url, out string baseUrl, out Dictionary<string, object> args)
		{
			baseUrl = null;
			args = null;
			return false;
		}

		// Token: 0x06002BE4 RID: 11236 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool OpenList(object urls, object option = null, object context = null)
		{
			return false;
		}

		// Token: 0x06002BE5 RID: 11237 RVA: 0x0000216A File Offset: 0x0000036A
		public static string ProcRelativePath(string path)
		{
			return null;
		}

		// Token: 0x06002BE6 RID: 11238 RVA: 0x000F1CD6 File Offset: 0x000EFED6
		public static bool SplitUrl(string url, out string scheme, out string host, out string option)
		{
			scheme = null;
			host = null;
			option = null;
			return false;
		}

		// Token: 0x06002BE7 RID: 11239 RVA: 0x000F1CE4 File Offset: 0x000EFEE4
		public static ValueTuple<bool, string[], Dictionary<string, object>> AnalyzeUrl(string url)
		{
			return default(ValueTuple<bool, string[], Dictionary<string, object>>);
		}

		// Token: 0x06002BE8 RID: 11240 RVA: 0x0000216D File Offset: 0x0000036D
		public static void AddScheme(string url, Action<string, object, object> act)
		{
		}

		// Token: 0x06002BE9 RID: 11241 RVA: 0x0000216D File Offset: 0x0000036D
		public static void AddArgsCommand(string cmd, Func<string, object> act)
		{
		}

		// Token: 0x06002BEA RID: 11242 RVA: 0x0000216A File Offset: 0x0000036A
		public static object ExecuteArgsCommand(string val)
		{
			return null;
		}

		// Token: 0x06002BEB RID: 11243 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsUrlScheme(string url)
		{
			return false;
		}

		// Token: 0x06002BEC RID: 11244 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool Open(string url, object option = null, object context = null)
		{
			return false;
		}

		// Token: 0x06002BED RID: 11245 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int GetUrlEndTokenIndex(string str, char endToken, int startIndex = 0)
		{
			return 0;
		}

		// Token: 0x04002A68 RID: 10856
		private static SortedDictionary<string, Action<string, object, object>> schemeFuncs;

		// Token: 0x04002A69 RID: 10857
		private static Dictionary<string, Func<string, object>> argsCommands;

		// Token: 0x04002A6A RID: 10858
		public static bool fatalAbort;

		// Token: 0x04002A6B RID: 10859
		public static string LatestUrl;
	}
}
