using System;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000F1A RID: 3866
	public class Sound
	{
		// Token: 0x060071D2 RID: 29138 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsReady()
		{
			return false;
		}

		// Token: 0x060071D3 RID: 29139 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsLoaded()
		{
			return false;
		}

		// Token: 0x060071D4 RID: 29140 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool Init(string[] bgms)
		{
			return false;
		}

		// Token: 0x060071D5 RID: 29141 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Term()
		{
		}

		// Token: 0x060071D6 RID: 29142 RVA: 0x0000216D File Offset: 0x0000036D
		public static void LoadSE()
		{
		}

		// Token: 0x060071D7 RID: 29143 RVA: 0x0000216D File Offset: 0x0000036D
		private static void UnloadSE()
		{
		}

		// Token: 0x060071D8 RID: 29144 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int PlaySE(string label)
		{
			return 0;
		}

		// Token: 0x060071D9 RID: 29145 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int PlaySE(string label, Vector3 position)
		{
			return 0;
		}

		// Token: 0x060071DA RID: 29146 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int PlaySE(string label, GameObject traceTarget)
		{
			return 0;
		}

		// Token: 0x060071DB RID: 29147 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Stop(int instanceId, float fade = -1f)
		{
		}

		// Token: 0x060071DC RID: 29148 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Stop(string label, float fade = -1f)
		{
		}

		// Token: 0x060071DD RID: 29149 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetPan(int instanceID, float newPan, float moveTime = 0f)
		{
		}

		// Token: 0x060071DE RID: 29150 RVA: 0x0000216D File Offset: 0x0000036D
		public static void LoadBGM()
		{
		}

		// Token: 0x060071DF RID: 29151 RVA: 0x0000216D File Offset: 0x0000036D
		private static void UnloadBGM()
		{
		}

		// Token: 0x060071E0 RID: 29152 RVA: 0x0000216D File Offset: 0x0000036D
		public static void PlayBGM(Sound.DuelBGM idx, float delay = -1f)
		{
		}

		// Token: 0x060071E1 RID: 29153 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetBGMLabel(Sound.DuelBGM idx)
		{
			return null;
		}

		// Token: 0x060071E2 RID: 29154 RVA: 0x0000216D File Offset: 0x0000036D
		public static void StopBGM(float fade = -1f)
		{
		}

		// Token: 0x060071E3 RID: 29155 RVA: 0x000029CC File Offset: 0x00000BCC
		public static Sound.DuelBGM GetCurrentBGM()
		{
			return Sound.DuelBGM.DuelEarly;
		}

		// Token: 0x060071E4 RID: 29156 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsSameBGM(Sound.DuelBGM stepA, Sound.DuelBGM stepB)
		{
			return false;
		}

		// Token: 0x0400ABB1 RID: 43953
		private const string DefaultLabel = "BGM_DUEL_NORMAL_01";

		// Token: 0x0400ABB2 RID: 43954
		private static Sound.Work s_work;

		// Token: 0x02000F1B RID: 3867
		public enum DuelBGM
		{
			// Token: 0x0400ABB4 RID: 43956
			DuelEarly,
			// Token: 0x0400ABB5 RID: 43957
			DuelMiddle,
			// Token: 0x0400ABB6 RID: 43958
			DuelLate,
			// Token: 0x0400ABB7 RID: 43959
			DuelStart = -1
		}

		// Token: 0x02000F1C RID: 3868
		internal class Work
		{
			// Token: 0x060071E6 RID: 29158 RVA: 0x00002739 File Offset: 0x00000939
			public Work(string[] bgms)
			{
			}

			// Token: 0x0400ABB8 RID: 43960
			public string[] bgms;

			// Token: 0x0400ABB9 RID: 43961
			public Sound.DuelBGM bgm_step;

			// Token: 0x0400ABBA RID: 43962
			public bool isSeLoaded;

			// Token: 0x0400ABBB RID: 43963
			public bool isBgmLoaded;
		}
	}
}
