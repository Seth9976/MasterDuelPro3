using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using YgomGame.Utility;

namespace YgomGame.Mission
{
	// Token: 0x02000A2E RID: 2606
	public class MissionContext : IComparable<MissionContext>
	{
		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06004B90 RID: 19344 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06004B91 RID: 19345 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isNew
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06004B92 RID: 19346 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06004B93 RID: 19347 RVA: 0x0000216D File Offset: 0x0000036D
		public int progressGoalPage
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06004B94 RID: 19348 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06004B95 RID: 19349 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isExpire
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06004B96 RID: 19350 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06004B97 RID: 19351 RVA: 0x0000216D File Offset: 0x0000036D
		public int hideGoalIdx
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06004B98 RID: 19352 RVA: 0x000F1669 File Offset: 0x000EF869
		// (set) Token: 0x06004B99 RID: 19353 RVA: 0x0000216D File Offset: 0x0000036D
		public long orderTs
		{
			[CompilerGenerated]
			get
			{
				return 0L;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06004B9A RID: 19354 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06004B9B RID: 19355 RVA: 0x0000216D File Offset: 0x0000036D
		public GoalContext[] goals
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06004B9C RID: 19356 RVA: 0x0000216A File Offset: 0x0000036A
		public string hintPath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06004B9D RID: 19357 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isExistsRewardUnlocked
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004B9E RID: 19358 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetLastGoalRequire()
		{
			return 0;
		}

		// Token: 0x06004B9F RID: 19359 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsProgressCompleted()
		{
			return false;
		}

		// Token: 0x06004BA0 RID: 19360 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsCompleteAndReceived()
		{
			return false;
		}

		// Token: 0x06004BA1 RID: 19361 RVA: 0x0000216A File Offset: 0x0000036A
		public string MakeMissionName(TextGroupLoadHolder textGroupLoadHolder)
		{
			return null;
		}

		// Token: 0x06004BA2 RID: 19362 RVA: 0x00002739 File Offset: 0x00000939
		public MissionContext(int idx, Dictionary<string, object> data, Dictionary<string, object> master)
		{
		}

		// Token: 0x06004BA3 RID: 19363 RVA: 0x0000216D File Offset: 0x0000036D
		public void ImportOverride(Dictionary<string, object> data)
		{
		}

		// Token: 0x06004BA4 RID: 19364 RVA: 0x0000216D File Offset: 0x0000036D
		public void TurnOffNew()
		{
		}

		// Token: 0x06004BA5 RID: 19365 RVA: 0x0000216D File Offset: 0x0000036D
		private void ClampCursorIdx(int setCursorIdx)
		{
		}

		// Token: 0x06004BA6 RID: 19366 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetCursorLimit()
		{
			return 0;
		}

		// Token: 0x06004BA7 RID: 19367 RVA: 0x0000216A File Offset: 0x0000036A
		public IReadOnlyList<GoalContext> GetCurrentGoals()
		{
			return null;
		}

		// Token: 0x06004BA8 RID: 19368 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int GetCategoryOrder(MissionCategory category)
		{
			return 0;
		}

		// Token: 0x06004BA9 RID: 19369 RVA: 0x000029CC File Offset: 0x00000BCC
		public int Compare(MissionContext a, MissionContext b)
		{
			return 0;
		}

		// Token: 0x06004BAA RID: 19370 RVA: 0x000029CC File Offset: 0x00000BCC
		public int CompareTo(MissionContext other)
		{
			return 0;
		}

		// Token: 0x04008983 RID: 35203
		public readonly int idx;

		// Token: 0x04008984 RID: 35204
		public readonly int poolId;

		// Token: 0x04008985 RID: 35205
		public readonly int missionId;

		// Token: 0x04008986 RID: 35206
		public readonly MissionCategory category;

		// Token: 0x04008987 RID: 35207
		private string m_MissionNameCache;

		// Token: 0x04008988 RID: 35208
		private readonly int m_LogicNo;

		// Token: 0x04008989 RID: 35209
		private readonly List<object> m_NameParams;

		// Token: 0x0400898A RID: 35210
		public readonly long endTimeStamp;

		// Token: 0x0400898B RID: 35211
		public readonly long resultEndTimeStamp;

		// Token: 0x0400898C RID: 35212
		public readonly string hintSfx;

		// Token: 0x0400898D RID: 35213
		public readonly int progress;

		// Token: 0x0400898E RID: 35214
		public readonly bool isExistsProgressData;

		// Token: 0x0400898F RID: 35215
		public readonly List<List<GoalContext>> goalPages;

		// Token: 0x04008990 RID: 35216
		public bool focusEffectVisible;

		// Token: 0x04008991 RID: 35217
		public bool completeEffectVisible;

		// Token: 0x04008992 RID: 35218
		public int goalPageIdx;

		// Token: 0x04008993 RID: 35219
		public int goalProgressPageIdx;

		// Token: 0x04008994 RID: 35220
		public int goalCursorIdx;
	}
}
