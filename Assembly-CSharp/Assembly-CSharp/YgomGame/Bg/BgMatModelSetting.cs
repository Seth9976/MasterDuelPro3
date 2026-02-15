using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Bg
{
	// Token: 0x02001144 RID: 4420
	public class BgMatModelSetting : ScriptableObject
	{
		// Token: 0x06008385 RID: 33669 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetPath(int id)
		{
			return null;
		}

		// Token: 0x06008386 RID: 33670 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetModelNo(int id)
		{
			return 0;
		}

		// Token: 0x06008387 RID: 33671 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetModelIdFromNo(int no)
		{
			return 0;
		}

		// Token: 0x06008388 RID: 33672 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetSeLabel(int id)
		{
			return null;
		}

		// Token: 0x06008389 RID: 33673 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetModelResPathFromId(int id, BgUnit.Side side)
		{
			return null;
		}

		// Token: 0x0600838A RID: 33674 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Load(Action<BgMatModelSetting> onFinish)
		{
		}

		// Token: 0x0600838B RID: 33675 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetModelResPath(int bgNo, BgUnit.Side side)
		{
			return null;
		}

		// Token: 0x0400BEF6 RID: 48886
		private const string matModelSettingPath = "Duel/ScriptableObject/Bg/BgMatModelSetting";

		// Token: 0x0400BEF7 RID: 48887
		private const string bgModelResPath = "Duel/BG/Mat/Mat_{0:000}/Mat_{0:000}_{1}";

		// Token: 0x0400BEF8 RID: 48888
		private const string bgModelResTypePath = "Duel/BG/Mat/Mat_{0:000}/<_RESOURCE_TYPE_>/Mat_{0:000}_{1}";

		// Token: 0x0400BEF9 RID: 48889
		private const int defaultMatNo = 2;

		// Token: 0x0400BEFA RID: 48890
		public List<BgMatModelSetting.MatModelInfo> infoList;

		// Token: 0x02001145 RID: 4421
		[Serializable]
		public class MatModelInfo
		{
			// Token: 0x0600838D RID: 33677 RVA: 0x0000216A File Offset: 0x0000036A
			public BgMatModelSetting.MatModelInfo Copy()
			{
				return null;
			}

			// Token: 0x0400BEFB RID: 48891
			public int id;

			// Token: 0x0400BEFC RID: 48892
			public string modelName;

			// Token: 0x0400BEFD RID: 48893
			public string seLabel;

			// Token: 0x0400BEFE RID: 48894
			public bool isResources;
		}
	}
}
