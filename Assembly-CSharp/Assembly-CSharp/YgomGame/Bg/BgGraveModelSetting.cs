using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Bg
{
	// Token: 0x02001140 RID: 4416
	public class BgGraveModelSetting : ScriptableObject
	{
		// Token: 0x06008350 RID: 33616 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetModelName(int id)
		{
			return null;
		}

		// Token: 0x06008351 RID: 33617 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetModelNo(int id)
		{
			return 0;
		}

		// Token: 0x06008352 RID: 33618 RVA: 0x000029CC File Offset: 0x00000BCC
		public BgGrave.GraveType GetModelType(int id)
		{
			return BgGrave.GraveType.None;
		}

		// Token: 0x06008353 RID: 33619 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetModelResPath(int id, BgUnit.Side side)
		{
			return null;
		}

		// Token: 0x06008354 RID: 33620 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Load(Action<BgGraveModelSetting> onFinish)
		{
		}

		// Token: 0x06008355 RID: 33621 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetModelResPath(BgGrave.GraveType graveType, int graveNo, BgUnit.Side side)
		{
			return null;
		}

		// Token: 0x06008356 RID: 33622 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetGraveTypeInital(BgGrave.GraveType graveType)
		{
			return null;
		}

		// Token: 0x0400BECF RID: 48847
		private const string graveModelSettingPath = "Duel/ScriptableObject/Bg/BgGraveModelSetting";

		// Token: 0x0400BED0 RID: 48848
		private const string graveResPath = "Duel/BG/Grave/Grave_{0}{1:000}/Grave_{0}{1:000}_{2}";

		// Token: 0x0400BED1 RID: 48849
		private const string graveResTypePath = "Duel/BG/Grave/Grave_{0}{1:000}/<_RESOURCE_TYPE_>/Grave_{0}{1:000}_{2}";

		// Token: 0x0400BED2 RID: 48850
		private const int defaultGraveNo = 2;

		// Token: 0x0400BED3 RID: 48851
		public List<BgGraveModelSetting.GraveModelInfo> infoList;

		// Token: 0x02001141 RID: 4417
		[Serializable]
		public class GraveModelInfo
		{
			// Token: 0x06008358 RID: 33624 RVA: 0x0000216A File Offset: 0x0000036A
			public BgGraveModelSetting.GraveModelInfo Copy()
			{
				return null;
			}

			// Token: 0x0400BED4 RID: 48852
			public int id;

			// Token: 0x0400BED5 RID: 48853
			public string modelName;
		}
	}
}
