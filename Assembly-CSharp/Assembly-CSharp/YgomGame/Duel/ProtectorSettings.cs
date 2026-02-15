using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000EE0 RID: 3808
	public class ProtectorSettings : ScriptableObject
	{
		// Token: 0x06006F17 RID: 28439 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Load(Action<ProtectorSettings> onLoaded)
		{
		}

		// Token: 0x06006F18 RID: 28440 RVA: 0x0000216A File Offset: 0x0000036A
		private ProtectorSettings.Info GetInfoInternal(int protectorID)
		{
			return null;
		}

		// Token: 0x06006F19 RID: 28441 RVA: 0x000029C5 File Offset: 0x00000BC5
		private float GetDrawEffectIntensityInternal(int protectorID)
		{
			return 0f;
		}

		// Token: 0x06006F1A RID: 28442 RVA: 0x000029C5 File Offset: 0x00000BC5
		public static float GetDrawEffectIntensity(int protectorID)
		{
			return 0f;
		}

		// Token: 0x0400AA24 RID: 43556
		private const string path = "Duel/ScriptableObject/ProtectorSettings";

		// Token: 0x0400AA25 RID: 43557
		private static ProtectorSettings setting;

		// Token: 0x0400AA26 RID: 43558
		public const float defaultDrawEffectIntensity = 0.3f;

		// Token: 0x0400AA27 RID: 43559
		public List<ProtectorSettings.Info> infoList;

		// Token: 0x0400AA28 RID: 43560
		public float drawEffectIntensity;

		// Token: 0x02000EE1 RID: 3809
		[Serializable]
		public class Info
		{
			// Token: 0x0400AA29 RID: 43561
			public int protectorID;

			// Token: 0x0400AA2A RID: 43562
			public float drawEffectIntensity;
		}
	}
}
