using System;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.UI;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B53 RID: 2899
	public class RarityIconBinder : ResourceBinderBase
	{
		// Token: 0x06005405 RID: 21509 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetRarityIconLabel(int id, RarityIconBinder.Type type)
		{
			return null;
		}

		// Token: 0x06005406 RID: 21510 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingSpriteContainer BindRarityIcon(Image target, int id, bool async = true, RarityIconBinder.Type type = RarityIconBinder.Type.Common)
		{
			return null;
		}

		// Token: 0x06005407 RID: 21511 RVA: 0x0000216D File Offset: 0x0000036D
		public void GetRaritySprite(int id, RarityIconBinder.Type type, Action<Sprite> onFinished)
		{
		}

		// Token: 0x0400916E RID: 37230
		public readonly string[] rarityLabelCommonFormat;

		// Token: 0x0400916F RID: 37231
		public readonly string[] rarityLabelDeckFormat;

		// Token: 0x02000B54 RID: 2900
		public enum Type
		{
			// Token: 0x04009171 RID: 37233
			Common,
			// Token: 0x04009172 RID: 37234
			Deck
		}
	}
}
