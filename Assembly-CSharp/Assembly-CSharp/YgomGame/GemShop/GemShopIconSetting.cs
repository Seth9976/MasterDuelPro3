using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.GemShop
{
	// Token: 0x02000BFC RID: 3068
	public class GemShopIconSetting : ScriptableObject
	{
		// Token: 0x06005717 RID: 22295 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool ValidIconId(int iconId)
		{
			return false;
		}

		// Token: 0x06005718 RID: 22296 RVA: 0x0000216A File Offset: 0x0000036A
		private GemShopIconSetting.IconData GetIconData(int iconId)
		{
			return null;
		}

		// Token: 0x06005719 RID: 22297 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetIconPath(int iconId)
		{
			return null;
		}

		// Token: 0x0600571A RID: 22298 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetEffectId(int iconId)
		{
			return 0;
		}

		// Token: 0x040093E7 RID: 37863
		[SerializeField]
		private string m_ThumbPathFormat;

		// Token: 0x040093E8 RID: 37864
		[SerializeField]
		private GemShopIconSetting.IconData[] m_IconDatas;

		// Token: 0x040093E9 RID: 37865
		private Dictionary<int, GemShopIconSetting.IconData> m_IconDataMap;

		// Token: 0x02000BFD RID: 3069
		[Serializable]
		public class IconData
		{
			// Token: 0x040093EA RID: 37866
			public int iconId;

			// Token: 0x040093EB RID: 37867
			public string thumbFxpId;

			// Token: 0x040093EC RID: 37868
			public int effectId;
		}
	}
}
