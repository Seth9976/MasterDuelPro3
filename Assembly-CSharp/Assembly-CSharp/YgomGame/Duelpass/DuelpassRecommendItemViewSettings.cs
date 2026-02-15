using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Duelpass
{
	// Token: 0x02000C3E RID: 3134
	public class DuelpassRecommendItemViewSettings : ScriptableObject
	{
		// Token: 0x0600595E RID: 22878 RVA: 0x0000216D File Offset: 0x0000036D
		public static void LoadAsync(Action<DuelpassRecommendItemViewSettings> onFinished)
		{
		}

		// Token: 0x04009526 RID: 38182
		public const string path = "Definition/Duelpass/DuelpassRecommendItemViewSettings";

		// Token: 0x04009527 RID: 38183
		[SerializeField]
		public List<ItemViewSetting> settings;
	}
}
