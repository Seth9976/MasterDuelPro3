using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Bg
{
	// Token: 0x02001147 RID: 4423
	public class BgStandModelSetting : ScriptableObject
	{
		// Token: 0x06008393 RID: 33683 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetModelName(int id)
		{
			return null;
		}

		// Token: 0x06008394 RID: 33684 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetModelNo(int id)
		{
			return 0;
		}

		// Token: 0x06008395 RID: 33685 RVA: 0x000029CC File Offset: 0x00000BCC
		public BgUnit.AvatarStandType GetModelType(int id)
		{
			return BgUnit.AvatarStandType.None;
		}

		// Token: 0x06008396 RID: 33686 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetModelResPath(int id, BgUnit.Side side)
		{
			return null;
		}

		// Token: 0x06008397 RID: 33687 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Load(Action<BgStandModelSetting> onFinish)
		{
		}

		// Token: 0x06008398 RID: 33688 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetModelResPath(BgUnit.AvatarStandType standType, int standNo, BgUnit.Side side)
		{
			return null;
		}

		// Token: 0x06008399 RID: 33689 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetAvatarStandTypeInital(BgUnit.AvatarStandType standType)
		{
			return null;
		}

		// Token: 0x0400BF02 RID: 48898
		private const string standModelSettingPath = "Duel/ScriptableObject/Bg/BgStandModelSetting";

		// Token: 0x0400BF03 RID: 48899
		private const string standModelResPath = "Duel/BG/AvatarStand/AvatarStand_{0}{1:000}/AvatarStand_{0}{1:000}_{2}";

		// Token: 0x0400BF04 RID: 48900
		private const string standModelResTypePath = "Duel/BG/AvatarStand/AvatarStand_{0}{1:000}/<_RESOURCE_TYPE_>/AvatarStand_{0}{1:000}_{2}";

		// Token: 0x0400BF05 RID: 48901
		public List<BgStandModelSetting.StandModelInfo> infoList;

		// Token: 0x02001148 RID: 4424
		[Serializable]
		public class StandModelInfo
		{
			// Token: 0x0600839B RID: 33691 RVA: 0x0000216A File Offset: 0x0000036A
			public BgStandModelSetting.StandModelInfo Copy()
			{
				return null;
			}

			// Token: 0x0400BF06 RID: 48902
			public int id;

			// Token: 0x0400BF07 RID: 48903
			public string modelName;
		}
	}
}
