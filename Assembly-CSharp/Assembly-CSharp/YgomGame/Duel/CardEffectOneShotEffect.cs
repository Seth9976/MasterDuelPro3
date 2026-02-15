using System;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000CC4 RID: 3268
	public class CardEffectOneShotEffect : CardEffectBase
	{
		// Token: 0x06005D14 RID: 23828 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardEffectOneShotEffect Create(CardRoot cardRoot, DuelEffectPool.Type type, CardEffectOneShotEffect.Mode mode, bool waitEffect, Quaternion rotation)
		{
			return null;
		}

		// Token: 0x06005D15 RID: 23829 RVA: 0x0000216D File Offset: 0x0000036D
		public override void StartEffect()
		{
		}

		// Token: 0x0400988B RID: 39051
		protected DuelEffectPool.Type type;

		// Token: 0x0400988C RID: 39052
		protected bool waitEffect;

		// Token: 0x0400988D RID: 39053
		protected CardEffectOneShotEffect.Mode mode;

		// Token: 0x0400988E RID: 39054
		protected Quaternion rotation;

		// Token: 0x0400988F RID: 39055
		protected SimpleEffect effect;

		// Token: 0x02000CC5 RID: 3269
		public enum Mode
		{
			// Token: 0x04009891 RID: 39057
			TraceRootPosition,
			// Token: 0x04009892 RID: 39058
			TraceRootPosture,
			// Token: 0x04009893 RID: 39059
			TraceCardPosition,
			// Token: 0x04009894 RID: 39060
			TraceCardPosture,
			// Token: 0x04009895 RID: 39061
			Child
		}
	}
}
