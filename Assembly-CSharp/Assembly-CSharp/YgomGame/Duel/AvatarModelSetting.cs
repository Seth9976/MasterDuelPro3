using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000C87 RID: 3207
	public class AvatarModelSetting : ScriptableObject
	{
		// Token: 0x06005BFF RID: 23551 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetPath(int id)
		{
			return null;
		}

		// Token: 0x06005C00 RID: 23552 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetSeLabel(int id)
		{
			return null;
		}

		// Token: 0x06005C01 RID: 23553 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetSubAvatarlId(int id)
		{
			return 0;
		}

		// Token: 0x06005C02 RID: 23554 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetSubAvatarPath(int id)
		{
			return null;
		}

		// Token: 0x06005C03 RID: 23555 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetChangeEffectPath(int id)
		{
			return null;
		}

		// Token: 0x06005C04 RID: 23556 RVA: 0x0000216A File Offset: 0x0000036A
		public Dictionary<Character.SubAvatarChange, List<int>> GetSubAvatarChangeCondition(int id)
		{
			return null;
		}

		// Token: 0x06005C05 RID: 23557 RVA: 0x0000216A File Offset: 0x0000036A
		public AvatarModelSetting.SubAvatarInfo GetSubAvatarInfo(int id)
		{
			return null;
		}

		// Token: 0x06005C06 RID: 23558 RVA: 0x0000216A File Offset: 0x0000036A
		public string LoadSoundXml(int id)
		{
			return null;
		}

		// Token: 0x06005C07 RID: 23559 RVA: 0x0000216A File Offset: 0x0000036A
		public static AvatarModelSetting LoadImmediate()
		{
			return null;
		}

		// Token: 0x06005C08 RID: 23560 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Load(Action<AvatarModelSetting> onFinish)
		{
		}

		// Token: 0x04009721 RID: 38689
		private const string avatarModelSettingPath = "Duel/ScriptableObject/AvatarModelSetting";

		// Token: 0x04009722 RID: 38690
		public List<AvatarModelSetting.AvatarModelInfo> infoList;

		// Token: 0x02000C88 RID: 3208
		[Serializable]
		public class ChangeCondition
		{
			// Token: 0x06005C0A RID: 23562 RVA: 0x00002739 File Offset: 0x00000939
			public ChangeCondition(Character.SubAvatarChange condition)
			{
			}

			// Token: 0x04009723 RID: 38691
			public Character.SubAvatarChange condition;

			// Token: 0x04009724 RID: 38692
			public List<int> param;
		}

		// Token: 0x02000C89 RID: 3209
		[Serializable]
		public class AvatarModelInfo
		{
			// Token: 0x06005C0B RID: 23563 RVA: 0x0000216A File Offset: 0x0000036A
			public AvatarModelSetting.AvatarModelInfo Copy()
			{
				return null;
			}

			// Token: 0x04009725 RID: 38693
			public int id;

			// Token: 0x04009726 RID: 38694
			public string path;

			// Token: 0x04009727 RID: 38695
			public string seLabel;

			// Token: 0x04009728 RID: 38696
			public AvatarModelSetting.SubAvatarInfo subAvatarInfo;
		}

		// Token: 0x02000C8A RID: 3210
		[Serializable]
		public class SubAvatarInfo
		{
			// Token: 0x04009729 RID: 38697
			public int subAvatarId;

			// Token: 0x0400972A RID: 38698
			public string changeEffectPath;

			// Token: 0x0400972B RID: 38699
			public List<AvatarModelSetting.ChangeCondition> conditionList;

			// Token: 0x0400972C RID: 38700
			public bool useChangeMotion;

			// Token: 0x0400972D RID: 38701
			public float changeDelay;
		}
	}
}
