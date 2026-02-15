using System;

namespace YgomGame.Duel
{
	// Token: 0x02000CC3 RID: 3267
	public class CardEffectMove : CardEffectBase
	{
		// Token: 0x06005D0F RID: 23823 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardEffectMove Create(CardRoot cardRoot, CardLocator from, CardLocator to, bool isFace, bool isAttack, bool immediate, CardRootTransition transition, Action onStarted, Action onFinished)
		{
			return null;
		}

		// Token: 0x06005D10 RID: 23824 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardEffectMove CreateFlipTurn(CardRoot cardRoot, bool isFace, bool isAttack, bool immediate, Action onStarted, Action onFinished)
		{
			return null;
		}

		// Token: 0x06005D11 RID: 23825 RVA: 0x0000216D File Offset: 0x0000036D
		public override void StartEffect()
		{
		}

		// Token: 0x06005D12 RID: 23826 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool UpdateEffect()
		{
			return false;
		}

		// Token: 0x04009882 RID: 39042
		private CardLocator from;

		// Token: 0x04009883 RID: 39043
		private CardLocator to;

		// Token: 0x04009884 RID: 39044
		private bool isFace;

		// Token: 0x04009885 RID: 39045
		private bool isAttack;

		// Token: 0x04009886 RID: 39046
		private bool immediate;

		// Token: 0x04009887 RID: 39047
		private bool firstUpdate;

		// Token: 0x04009888 RID: 39048
		private CardRootTransition transition;

		// Token: 0x04009889 RID: 39049
		private Action onStarted;

		// Token: 0x0400988A RID: 39050
		private bool finishFlipTurn;
	}
}
