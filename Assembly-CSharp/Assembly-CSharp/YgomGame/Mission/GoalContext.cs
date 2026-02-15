using System;
using System.Collections.Generic;

namespace YgomGame.Mission
{
	// Token: 0x02000A27 RID: 2599
	public class GoalContext
	{
		// Token: 0x06004B67 RID: 19303 RVA: 0x000029CC File Offset: 0x00000BCC
		public MissionGoalWidget.GoalType GetGoalType(MissionContext missionContext)
		{
			return MissionGoalWidget.GoalType.None;
		}

		// Token: 0x06004B68 RID: 19304 RVA: 0x00002739 File Offset: 0x00000939
		public GoalContext(int idx, Dictionary<string, object> data, Dictionary<string, object> master)
		{
		}

		// Token: 0x06004B69 RID: 19305 RVA: 0x0000216D File Offset: 0x0000036D
		public void Import(Dictionary<string, object> data)
		{
		}

		// Token: 0x0400895B RID: 35163
		public readonly int idx;

		// Token: 0x0400895C RID: 35164
		public readonly int requirement;

		// Token: 0x0400895D RID: 35165
		public readonly bool isPeriod;

		// Token: 0x0400895E RID: 35166
		public readonly int itemCategory;

		// Token: 0x0400895F RID: 35167
		public readonly int itemId;

		// Token: 0x04008960 RID: 35168
		public readonly int itemCount;

		// Token: 0x04008961 RID: 35169
		public bool isRewardUnlocked;

		// Token: 0x04008962 RID: 35170
		public bool recievedIconVisible;
	}
}
