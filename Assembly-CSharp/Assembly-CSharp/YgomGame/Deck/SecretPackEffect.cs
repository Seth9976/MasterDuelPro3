using System;
using System.Collections;
using UnityEngine;
using YgomGame.Effect;

namespace YgomGame.Deck
{
	// Token: 0x02001002 RID: 4098
	public class SecretPackEffect
	{
		// Token: 0x06007B69 RID: 31593 RVA: 0x0000216A File Offset: 0x0000036A
		public static SecretPackEffect Create(RectTransform targetButton, bool isMobile)
		{
			return null;
		}

		// Token: 0x06007B6A RID: 31594 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize(RectTransform targetButton, bool isMobile)
		{
		}

		// Token: 0x06007B6B RID: 31595 RVA: 0x0000216D File Offset: 0x0000036D
		public void StartEffect(Action onPlayGetEffect)
		{
		}

		// Token: 0x06007B6C RID: 31596 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator UpdateCreateEffect(Action onPlayGetEffect)
		{
			return null;
		}

		// Token: 0x06007B6D RID: 31597 RVA: 0x0000216D File Offset: 0x0000036D
		public void DestroyGetEffect()
		{
		}

		// Token: 0x06007B6E RID: 31598 RVA: 0x0000216D File Offset: 0x0000036D
		public void Terminate()
		{
		}

		// Token: 0x0400B30B RID: 45835
		private const string prefabPathGetSecret = "Prefabs/UI/DeckEdit/fxp_DeckEditUI/fxp_DeckEditUI_sctget_001";

		// Token: 0x0400B30C RID: 45836
		private const string prefabPathActiveSecret = "Prefabs/UI/DeckEdit/fxp_DeckEditUI/fxp_DeckEditUI_sctactive_001";

		// Token: 0x0400B30D RID: 45837
		private EffectHandler effectGetSecret;

		// Token: 0x0400B30E RID: 45838
		private EffectHandler effectActiveSecret;

		// Token: 0x0400B30F RID: 45839
		private SecretPackEffect.Step step;

		// Token: 0x02001003 RID: 4099
		private enum Step
		{
			// Token: 0x0400B311 RID: 45841
			Loading,
			// Token: 0x0400B312 RID: 45842
			GetEffect,
			// Token: 0x0400B313 RID: 45843
			ActiveEffect,
			// Token: 0x0400B314 RID: 45844
			Finished
		}
	}
}
