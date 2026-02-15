using System;

namespace YgomSystem
{
	// Token: 0x020004D4 RID: 1236
	public class SteamAchievement
	{
		// Token: 0x060027A6 RID: 10150 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetNotificator()
		{
		}

		// Token: 0x060027A7 RID: 10151 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ReleaseNotificator()
		{
		}

		// Token: 0x060027A8 RID: 10152 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OnAchievementDone(object value)
		{
		}

		// Token: 0x060027A9 RID: 10153 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Store()
		{
		}

		// Token: 0x060027AA RID: 10154 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ShowUI()
		{
		}

		// Token: 0x060027AB RID: 10155 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Progress(string param, string id, uint cursor, uint max)
		{
		}

		// Token: 0x060027AC RID: 10156 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Unlock(string id)
		{
		}

		// Token: 0x060027AD RID: 10157 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Unlock(SteamAchievement.ID id)
		{
		}

		// Token: 0x04002859 RID: 10329
		private static bool enable_notificator;

		// Token: 0x0400285A RID: 10330
		private static readonly string id_convert_format;

		// Token: 0x020004D5 RID: 1237
		public enum ID
		{
			// Token: 0x0400285C RID: 10332
			jp_konami_masterduel_ach_001,
			// Token: 0x0400285D RID: 10333
			jp_konami_masterduel_ach_002,
			// Token: 0x0400285E RID: 10334
			jp_konami_masterduel_ach_003,
			// Token: 0x0400285F RID: 10335
			jp_konami_masterduel_ach_004,
			// Token: 0x04002860 RID: 10336
			jp_konami_masterduel_ach_005,
			// Token: 0x04002861 RID: 10337
			jp_konami_masterduel_ach_006,
			// Token: 0x04002862 RID: 10338
			jp_konami_masterduel_ach_007,
			// Token: 0x04002863 RID: 10339
			jp_konami_masterduel_ach_008,
			// Token: 0x04002864 RID: 10340
			jp_konami_masterduel_ach_009,
			// Token: 0x04002865 RID: 10341
			jp_konami_masterduel_ach_010,
			// Token: 0x04002866 RID: 10342
			jp_konami_masterduel_ach_011
		}
	}
}
