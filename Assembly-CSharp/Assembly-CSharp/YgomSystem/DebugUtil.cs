using System;
using System.Runtime.CompilerServices;

namespace YgomSystem
{
	// Token: 0x0200049B RID: 1179
	public class DebugUtil
	{
		// Token: 0x06002628 RID: 9768 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Log(string message)
		{
		}

		// Token: 0x06002629 RID: 9769 RVA: 0x0000216D File Offset: 0x0000036D
		public static void LogFormat(string format, params object[] args)
		{
		}

		// Token: 0x0600262A RID: 9770 RVA: 0x0000216D File Offset: 0x0000036D
		public static void LogWarning(string message)
		{
		}

		// Token: 0x0600262B RID: 9771 RVA: 0x0000216D File Offset: 0x0000036D
		public static void LogError(string message)
		{
		}

		// Token: 0x0600262C RID: 9772 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ThreadLog(string message, [CallerMemberName] string member = "")
		{
		}

		// Token: 0x0600262D RID: 9773 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ThreadLogWarning(string message, [CallerMemberName] string member = "")
		{
		}

		// Token: 0x0600262E RID: 9774 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ThreadLogError(string message, [CallerMemberName] string member = "")
		{
		}

		// Token: 0x0600262F RID: 9775 RVA: 0x0000216A File Offset: 0x0000036A
		private static string makeThreadLogString(string member, string message)
		{
			return null;
		}

		// Token: 0x06002630 RID: 9776 RVA: 0x0000216D File Offset: 0x0000036D
		public static void TimeStampLog(string message)
		{
		}

		// Token: 0x06002631 RID: 9777 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Assert(bool condition)
		{
		}

		// Token: 0x06002632 RID: 9778 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Assert(bool condition, string message)
		{
		}

		// Token: 0x06002633 RID: 9779 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Assert(bool condition, Func<string> getMessage)
		{
		}

		// Token: 0x06002634 RID: 9780 RVA: 0x0000216D File Offset: 0x0000036D
		public static void PrintObj(object obj, string label = "")
		{
		}
	}
}
