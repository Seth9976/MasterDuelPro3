using System;

namespace YgomGame.Duel
{
	// Token: 0x02000CC8 RID: 3272
	public class CardEffectSummonMaterial : CardEffectBase
	{
		// Token: 0x06005D21 RID: 23841 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardEffectSummonMaterial Create(CardRoot cardRoot, SharedDefinition.SummonMaterialType type)
		{
			return null;
		}

		// Token: 0x06005D22 RID: 23842 RVA: 0x0000216D File Offset: 0x0000036D
		public override void StartEffect()
		{
		}

		// Token: 0x06005D23 RID: 23843 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool UpdateEffect()
		{
			return false;
		}

		// Token: 0x0400989A RID: 39066
		private SharedDefinition.SummonMaterialType type;
	}
}
