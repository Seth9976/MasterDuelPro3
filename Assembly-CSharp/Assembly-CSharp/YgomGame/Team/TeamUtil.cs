using System;
using System.Runtime.CompilerServices;
using UnityEngine.Events;
using YgomSystem.Network;
using YgomSystem.UI;

namespace YgomGame.Team
{
	// Token: 0x020008E0 RID: 2272
	public class TeamUtil
	{
		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x0600429B RID: 17051 RVA: 0x0000216A File Offset: 0x0000036A
		private static string stringCrossPlatformPlay
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600429C RID: 17052 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsForceLeaveError(TeamCode code)
		{
			return false;
		}

		// Token: 0x0600429D RID: 17053 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsFatalError(TeamCode code)
		{
			return false;
		}

		// Token: 0x0600429E RID: 17054 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool HandleResultCode(Handle handle, UnityAction onSuccess, UnityAction<TeamCode> onFailed, bool showDialog = true)
		{
			return false;
		}

		// Token: 0x0600429F RID: 17055 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenRegulationSelect(int[] regulationList, Action<int, string> onResult)
		{
		}

		// Token: 0x060042A0 RID: 17056 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenDeckSelect(ViewControllerManager manager, int regulationID, int deckId)
		{
		}

		// Token: 0x060042A1 RID: 17057 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CallAPI_DeckCheck(int regulationId, Action onEnd)
		{
		}

		// Token: 0x060042A2 RID: 17058 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CallAPI_SetReulationSetID(int regSetId, Action<TeamCode> onEnd)
		{
		}

		// Token: 0x060042A3 RID: 17059 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CallAPI_TeamEntry(int teamId, Action onSuccess)
		{
		}

		// Token: 0x060042A4 RID: 17060 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CallAPI_TeamEntry(int teamId, int matchType, Action onSuccess, Action onFailed)
		{
		}

		// Token: 0x060042A5 RID: 17061 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CallAPI_TeamEntryAndArrive(int teamId, int matchType, int tableNo, Action onSuccess, Action onFailed)
		{
		}

		// Token: 0x060042A6 RID: 17062 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetStringFromTextID(string text)
		{
			return null;
		}

		// Token: 0x020008E1 RID: 2273
		public class RegulationSet
		{
			// Token: 0x1700052B RID: 1323
			// (get) Token: 0x060042A8 RID: 17064 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x060042A9 RID: 17065 RVA: 0x0000216D File Offset: 0x0000036D
			public string name
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				set
				{
				}
			}

			// Token: 0x1700052C RID: 1324
			// (get) Token: 0x060042AA RID: 17066 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x060042AB RID: 17067 RVA: 0x0000216D File Offset: 0x0000036D
			public int id
			{
				[CompilerGenerated]
				get
				{
					return 0;
				}
				[CompilerGenerated]
				set
				{
				}
			}

			// Token: 0x1700052D RID: 1325
			// (get) Token: 0x060042AC RID: 17068 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x060042AD RID: 17069 RVA: 0x0000216D File Offset: 0x0000036D
			public int[] regulations
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				set
				{
				}
			}
		}

		// Token: 0x020008E2 RID: 2274
		public enum MatchType
		{
			// Token: 0x0400811D RID: 33053
			Null,
			// Token: 0x0400811E RID: 33054
			Normal,
			// Token: 0x0400811F RID: 33055
			Max
		}
	}
}
