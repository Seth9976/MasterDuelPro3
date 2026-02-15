using System;

namespace YgomGame.Duel
{
	// Token: 0x02000CC6 RID: 3270
	public class CardEffectSE : CardEffectBase
	{
		// Token: 0x06005D17 RID: 23831 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardEffectSE CreatePlay(CardRoot cardRoot, string seLabel, bool is3D)
		{
			return null;
		}

		// Token: 0x06005D18 RID: 23832 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardEffectSE CreateStop(CardRoot cardRoot, string seLabel)
		{
			return null;
		}

		// Token: 0x06005D19 RID: 23833 RVA: 0x0000216D File Offset: 0x0000036D
		public override void StartEffect()
		{
		}

		// Token: 0x06005D1A RID: 23834 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool UpdateEffect()
		{
			return false;
		}

		// Token: 0x04009896 RID: 39062
		private string seLabel;

		// Token: 0x04009897 RID: 39063
		private bool play;

		// Token: 0x04009898 RID: 39064
		private bool is3D;
	}
}
