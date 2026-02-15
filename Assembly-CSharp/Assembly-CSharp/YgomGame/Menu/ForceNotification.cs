using System;

namespace YgomGame.Menu
{
	// Token: 0x02000A71 RID: 2673
	public class ForceNotification
	{
		// Token: 0x06004E00 RID: 19968 RVA: 0x0000216D File Offset: 0x0000036D
		private static void Open(Action onClosed)
		{
		}

		// Token: 0x06004E01 RID: 19969 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenWithSaveLastDate(Action onClosed)
		{
		}

		// Token: 0x06004E02 RID: 19970 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsDispForceNotification()
		{
			return false;
		}

		// Token: 0x06004E03 RID: 19971 RVA: 0x0000216D File Offset: 0x0000036D
		public static void RemoveLastDispDate()
		{
		}

		// Token: 0x06004E04 RID: 19972 RVA: 0x0000216D File Offset: 0x0000036D
		public static void InitLastDispDate()
		{
		}

		// Token: 0x06004E05 RID: 19973 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool EqualCurrentDate(DateTime targetDate)
		{
			return false;
		}

		// Token: 0x06004E06 RID: 19974 RVA: 0x0000216D File Offset: 0x0000036D
		private static void SaveLastDate()
		{
		}

		// Token: 0x04008BE8 RID: 35816
		private const string FORCE_NOTIFY_SAVE_PATH = "LastForceNotify";

		// Token: 0x04008BE9 RID: 35817
		private static DateTime lastDispDateCache;
	}
}
