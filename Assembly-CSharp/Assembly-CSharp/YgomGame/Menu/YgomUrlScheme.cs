using System;
using System.Collections.Generic;
using YgomSystem.Network;
using YgomSystem.UI;

namespace YgomGame.Menu
{
	// Token: 0x02000B04 RID: 2820
	public class YgomUrlScheme
	{
		// Token: 0x060051F5 RID: 20981 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CleanupYgomScheme()
		{
		}

		// Token: 0x060051F6 RID: 20982 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetDelayUrlOpen(object url, bool inputMask, float time, long utime, string label)
		{
		}

		// Token: 0x060051F7 RID: 20983 RVA: 0x0000216D File Offset: 0x0000036D
		public static void DelayUpdate()
		{
		}

		// Token: 0x060051F8 RID: 20984 RVA: 0x0000216D File Offset: 0x0000036D
		private static void transitionHandle(ViewController.TransitionType tt, ViewController vc, ViewController preVc)
		{
		}

		// Token: 0x060051F9 RID: 20985 RVA: 0x0000216D File Offset: 0x0000036D
		private static void transitionHandleClear()
		{
		}

		// Token: 0x060051FA RID: 20986 RVA: 0x0000216D File Offset: 0x0000036D
		public static void RegistYgomArgCommand()
		{
		}

		// Token: 0x060051FB RID: 20987 RVA: 0x0000216D File Offset: 0x0000036D
		public static void RegistYgomScheme()
		{
		}

		// Token: 0x060051FC RID: 20988 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool isScheme(ref string url, string scheme)
		{
			return false;
		}

		// Token: 0x060051FD RID: 20989 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle CallWabApi(string url, object option = null, string schemename = "webapi://", float timeout = 30f)
		{
			return null;
		}

		// Token: 0x060051FE RID: 20990 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle CallWabApiWithClientWork(string url, object option = null, string schemename = "webapicw://", float timeout = 30f)
		{
			return null;
		}

		// Token: 0x060051FF RID: 20991 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ResponseNotificator(object value)
		{
		}

		// Token: 0x06005200 RID: 20992 RVA: 0x0000216D File Offset: 0x0000036D
		public static void RegisterCallSchemeFunction(string callName, Action<Dictionary<string, object>> act)
		{
		}

		// Token: 0x06005201 RID: 20993 RVA: 0x0000216D File Offset: 0x0000036D
		public static void UnregisterCallSchemeFunction(string callName)
		{
		}

		// Token: 0x06005202 RID: 20994 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsCallSchemeFunction(string callName)
		{
			return false;
		}

		// Token: 0x06005203 RID: 20995 RVA: 0x0000216D File Offset: 0x0000036D
		public static void AddKeepScheme(string url, object option = null, object context = null)
		{
		}

		// Token: 0x06005204 RID: 20996 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ClearKeepScheme()
		{
		}

		// Token: 0x06005205 RID: 20997 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenKeepScheme()
		{
		}

		// Token: 0x06005206 RID: 20998 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool checkMultiPlay()
		{
			return false;
		}

		// Token: 0x06005207 RID: 20999 RVA: 0x0000216D File Offset: 0x0000036D
		private static void parsePlayModeMenuUrl(string url, Action<Dictionary<string, object>> homepushAction)
		{
		}

		// Token: 0x0400906B RID: 36971
		private static List<YgomUrlScheme.DelayCallUrl> delayCall;

		// Token: 0x0400906C RID: 36972
		private static Dictionary<string, Action<Dictionary<string, object>>> callSchemeFuncs;

		// Token: 0x0400906D RID: 36973
		private static Dictionary<string, int> callSchemeRefCounter;

		// Token: 0x0400906E RID: 36974
		private const string URLQUEPATH = "UrlQueue";

		// Token: 0x0400906F RID: 36975
		private static Dictionary<string, string> transitionCall;

		// Token: 0x04009070 RID: 36976
		public static string NOTIFICATIONPATH;

		// Token: 0x04009071 RID: 36977
		private static List<YgomUrlScheme.SchemeInfo> keepSchemeContainer;

		// Token: 0x02000B05 RID: 2821
		private class DelayCallUrl
		{
			// Token: 0x06005209 RID: 21001 RVA: 0x00002739 File Offset: 0x00000939
			public DelayCallUrl(object url_, bool imask, float time, long utime, string label)
			{
			}

			// Token: 0x04009072 RID: 36978
			public string delayLabel;

			// Token: 0x04009073 RID: 36979
			public float delayTime;

			// Token: 0x04009074 RID: 36980
			public long delayUnixTime;

			// Token: 0x04009075 RID: 36981
			public object url;

			// Token: 0x04009076 RID: 36982
			public bool inputMask;
		}

		// Token: 0x02000B06 RID: 2822
		private struct SchemeInfo
		{
			// Token: 0x04009077 RID: 36983
			public string url;

			// Token: 0x04009078 RID: 36984
			public object option;

			// Token: 0x04009079 RID: 36985
			public object context;
		}
	}
}
