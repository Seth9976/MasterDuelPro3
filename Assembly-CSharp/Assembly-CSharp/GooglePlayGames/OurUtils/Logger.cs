using System;

namespace GooglePlayGames.OurUtils
{
	// Token: 0x020011AC RID: 4524
	public class Logger
	{
		// Token: 0x17001130 RID: 4400
		// (get) Token: 0x06008758 RID: 34648 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06008759 RID: 34649 RVA: 0x0000216D File Offset: 0x0000036D
		public static bool DebugLogEnabled
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17001131 RID: 4401
		// (get) Token: 0x0600875A RID: 34650 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600875B RID: 34651 RVA: 0x0000216D File Offset: 0x0000036D
		public static bool WarningLogEnabled
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x0600875C RID: 34652 RVA: 0x0000216D File Offset: 0x0000036D
		public static void d(string msg)
		{
		}

		// Token: 0x0600875D RID: 34653 RVA: 0x0000216D File Offset: 0x0000036D
		public static void w(string msg)
		{
		}

		// Token: 0x0600875E RID: 34654 RVA: 0x0000216D File Offset: 0x0000036D
		public static void e(string msg)
		{
		}

		// Token: 0x0600875F RID: 34655 RVA: 0x0000216A File Offset: 0x0000036A
		public static string describe(byte[] b)
		{
			return null;
		}

		// Token: 0x06008760 RID: 34656 RVA: 0x0000216A File Offset: 0x0000036A
		private static string ToLogMessage(string prefix, string logType, string msg)
		{
			return null;
		}

		// Token: 0x0400C19F RID: 49567
		private static bool debugLogEnabled;

		// Token: 0x0400C1A0 RID: 49568
		private static bool warningLogEnabled;
	}
}
