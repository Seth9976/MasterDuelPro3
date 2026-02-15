using System;

namespace YgomGame.Duel
{
	// Token: 0x02000CBD RID: 3261
	public class CardEffectCallback : CardEffectBase
	{
		// Token: 0x06005CFE RID: 23806 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardEffectCallback Create(CardRoot cardRoot, float delay, Action onFinished)
		{
			return null;
		}

		// Token: 0x06005CFF RID: 23807 RVA: 0x0000216D File Offset: 0x0000036D
		public override void StartEffect()
		{
		}

		// Token: 0x06005D00 RID: 23808 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool UpdateEffect()
		{
			return false;
		}

		// Token: 0x04009874 RID: 39028
		private float delay;

		// Token: 0x04009875 RID: 39029
		private float currentTime;
	}
}
