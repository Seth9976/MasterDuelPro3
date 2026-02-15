using System;

namespace YgomGame.Duel
{
	// Token: 0x02000CCA RID: 3274
	public class CardEffectWait : CardEffectBase
	{
		// Token: 0x06005D2B RID: 23851 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardEffectWait Create(Func<bool> waitFunc)
		{
			return null;
		}

		// Token: 0x06005D2C RID: 23852 RVA: 0x0000216D File Offset: 0x0000036D
		public override void StartEffect()
		{
		}

		// Token: 0x06005D2D RID: 23853 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool UpdateEffect()
		{
			return false;
		}

		// Token: 0x0400989E RID: 39070
		private Func<bool> waitFunc;
	}
}
