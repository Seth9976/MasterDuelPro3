using System;
using UnityEngine;
using YgomGame.Menu;

namespace YgomGame.Duel
{
	// Token: 0x02000D6D RID: 3437
	public class DuelEntryControllerSetting : ScriptableObject
	{
		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x0600642C RID: 25644 RVA: 0x0000216A File Offset: 0x0000036A
		protected static DuelEntryControllerSetting Instance
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600642D RID: 25645 RVA: 0x000F5B64 File Offset: 0x000F3D64
		public static ValueTuple<bool, string, string> GetTimelineExInfo(int logo_mixid)
		{
			return default(ValueTuple<bool, string, string>);
		}

		// Token: 0x04009EFC RID: 40700
		[SerializeField]
		private DuelEntryControllerSetting.DuelEntryTimelineInfoEx[] m_DuelEntryTimelineInfoExTable;

		// Token: 0x04009EFD RID: 40701
		private static DuelEntryControllerSetting m_Instance;

		// Token: 0x04009EFE RID: 40702
		private const string PATH = "Duel/ScriptableObject/DuelEntryControllerSetting";

		// Token: 0x02000D6E RID: 3438
		[Serializable]
		public class DuelEntryTimelineInfoEx
		{
			// Token: 0x04009EFF RID: 40703
			public PvpMenuDefine.MatchingType match_type;

			// Token: 0x04009F00 RID: 40704
			public int logo_id;

			// Token: 0x04009F01 RID: 40705
			public int logo_type;

			// Token: 0x04009F02 RID: 40706
			public string event_label;

			// Token: 0x04009F03 RID: 40707
			public string prefab_path;
		}
	}
}
