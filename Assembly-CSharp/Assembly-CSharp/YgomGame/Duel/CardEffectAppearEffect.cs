using System;

namespace YgomGame.Duel
{
	// Token: 0x02000CBB RID: 3259
	public class CardEffectAppearEffect : CardEffectBase
	{
		// Token: 0x06005CF1 RID: 23793 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardEffectAppearEffect Create(CardRoot cardRoot, bool isToken, bool waitEffect, Action onEffectFinished)
		{
			return null;
		}

		// Token: 0x06005CF2 RID: 23794 RVA: 0x0000216D File Offset: 0x0000036D
		public override void StartEffect()
		{
		}

		// Token: 0x04009871 RID: 39025
		private bool isToken;

		// Token: 0x04009872 RID: 39026
		private bool waitEffect;

		// Token: 0x04009873 RID: 39027
		private Action onEffectFinished;
	}
}
