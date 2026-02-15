using System;

namespace YgomGame.Duel
{
	// Token: 0x02000CC7 RID: 3271
	public class CardEffectStoneMode : CardEffectBase
	{
		// Token: 0x06005D1C RID: 23836 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardEffectStoneMode Create(CardRoot cardRoot, int topos)
		{
			return null;
		}

		// Token: 0x06005D1D RID: 23837 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardEffectStoneMode Create(CardRoot cardRoot, CardRoot.ModelType mdoelType)
		{
			return null;
		}

		// Token: 0x06005D1E RID: 23838 RVA: 0x0000216D File Offset: 0x0000036D
		public override void StartEffect()
		{
		}

		// Token: 0x06005D1F RID: 23839 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool UpdateEffect()
		{
			return false;
		}

		// Token: 0x04009899 RID: 39065
		private CardRoot.ModelType modelType;
	}
}
