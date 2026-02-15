using System;

namespace YgomGame.Duel
{
	// Token: 0x02000CC1 RID: 3265
	public class CardEffectHoldEffect : CardEffectBase
	{
		// Token: 0x06005D0B RID: 23819 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardEffectHoldEffect CreateShowEffect(CardRoot cardRoot, string label, DuelEffectPool.Type type, CardEffectHoldEffect.Mode mode)
		{
			return null;
		}

		// Token: 0x06005D0C RID: 23820 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardEffectHoldEffect CreateHideEffect(CardRoot cardRoot, string label, bool quitImmediate)
		{
			return null;
		}

		// Token: 0x06005D0D RID: 23821 RVA: 0x0000216D File Offset: 0x0000036D
		public override void StartEffect()
		{
		}

		// Token: 0x04009878 RID: 39032
		private string label;

		// Token: 0x04009879 RID: 39033
		private DuelEffectPool.Type type;

		// Token: 0x0400987A RID: 39034
		private CardEffectHoldEffect.Mode mode;

		// Token: 0x0400987B RID: 39035
		private bool show;

		// Token: 0x0400987C RID: 39036
		private bool quitImmediate;

		// Token: 0x02000CC2 RID: 3266
		public enum Mode
		{
			// Token: 0x0400987E RID: 39038
			TracePosition,
			// Token: 0x0400987F RID: 39039
			TracePosture,
			// Token: 0x04009880 RID: 39040
			ChildPosture,
			// Token: 0x04009881 RID: 39041
			ChildPosition
		}
	}
}
