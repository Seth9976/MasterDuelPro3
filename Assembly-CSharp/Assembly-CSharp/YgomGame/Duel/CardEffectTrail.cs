using System;

namespace YgomGame.Duel
{
	// Token: 0x02000CC9 RID: 3273
	public class CardEffectTrail : CardEffectBase
	{
		// Token: 0x06005D25 RID: 23845 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardEffectTrail Create(CardRoot cardRoot, CardPlane.MoveTrailType trailType, bool persistentVision)
		{
			return null;
		}

		// Token: 0x06005D26 RID: 23846 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardEffectTrail CreateShowTrail(CardRoot cardRoot, DuelEffectPool.Type type, bool persistentVision)
		{
			return null;
		}

		// Token: 0x06005D27 RID: 23847 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardEffectTrail CreateHideTrail(CardRoot cardRoot)
		{
			return null;
		}

		// Token: 0x06005D28 RID: 23848 RVA: 0x0000216D File Offset: 0x0000036D
		public override void StartEffect()
		{
		}

		// Token: 0x06005D29 RID: 23849 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool UpdateEffect()
		{
			return false;
		}

		// Token: 0x0400989B RID: 39067
		private bool show;

		// Token: 0x0400989C RID: 39068
		private DuelEffectPool.Type trailType;

		// Token: 0x0400989D RID: 39069
		private bool persistentVision;
	}
}
