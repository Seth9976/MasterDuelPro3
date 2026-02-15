using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000C8B RID: 3211
	public class AvatarMotionSetting : ScriptableObject
	{
		// Token: 0x06005C0E RID: 23566 RVA: 0x0000216A File Offset: 0x0000036A
		public AvatarMotionSetting.AvatarMotionInfo GetMotionInfo(AvatarMotionSetting.MotionID id)
		{
			return null;
		}

		// Token: 0x06005C0F RID: 23567 RVA: 0x0000216A File Offset: 0x0000036A
		public static AvatarMotionSetting Load()
		{
			return null;
		}

		// Token: 0x0400972E RID: 38702
		private const string avatarMotionSettingPath = "Duel/ScriptableObject/AvatarMotionSetting";

		// Token: 0x0400972F RID: 38703
		public List<AvatarMotionSetting.AvatarMotionInfo> motionInfoList;

		// Token: 0x02000C8C RID: 3212
		public enum MotionID
		{
			// Token: 0x04009731 RID: 38705
			WAIT1,
			// Token: 0x04009732 RID: 38706
			WAIT2,
			// Token: 0x04009733 RID: 38707
			WAIT3,
			// Token: 0x04009734 RID: 38708
			MATCHING,
			// Token: 0x04009735 RID: 38709
			ENTRY,
			// Token: 0x04009736 RID: 38710
			ATTACK,
			// Token: 0x04009737 RID: 38711
			DAMAGE,
			// Token: 0x04009738 RID: 38712
			COST_DAMAGE,
			// Token: 0x04009739 RID: 38713
			VICTORY,
			// Token: 0x0400973A RID: 38714
			DEFEAT,
			// Token: 0x0400973B RID: 38715
			TAP1,
			// Token: 0x0400973C RID: 38716
			TAP2,
			// Token: 0x0400973D RID: 38717
			TAP3,
			// Token: 0x0400973E RID: 38718
			APPEAL,
			// Token: 0x0400973F RID: 38719
			SHOP,
			// Token: 0x04009740 RID: 38720
			PROFILE,
			// Token: 0x04009741 RID: 38721
			OUTGAME,
			// Token: 0x04009742 RID: 38722
			CHANGE
		}

		// Token: 0x02000C8D RID: 3213
		[Serializable]
		public class AvatarMotionInfo
		{
			// Token: 0x06005C11 RID: 23569 RVA: 0x0000216A File Offset: 0x0000036A
			public AvatarMotionSetting.AvatarMotionInfo Copy()
			{
				return null;
			}

			// Token: 0x04009743 RID: 38723
			public AvatarMotionSetting.MotionID motionID;

			// Token: 0x04009744 RID: 38724
			public string label;
		}
	}
}
