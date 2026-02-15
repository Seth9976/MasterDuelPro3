using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000EE6 RID: 3814
	public class ResidualEffect
	{
		// Token: 0x06006F4F RID: 28495 RVA: 0x0000216A File Offset: 0x0000036A
		public static ResidualEffect Create(DuelEffectPool pool)
		{
			return null;
		}

		// Token: 0x06006F50 RID: 28496 RVA: 0x0000216A File Offset: 0x0000036A
		public ResidualEffect.EffectInfo Play(DuelEffectPool.Type type, Vector3 position)
		{
			return null;
		}

		// Token: 0x06006F51 RID: 28497 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnTurnEnd()
		{
		}

		// Token: 0x06006F52 RID: 28498 RVA: 0x0000216D File Offset: 0x0000036D
		private void StopAllEffects()
		{
		}

		// Token: 0x06006F53 RID: 28499 RVA: 0x0000216D File Offset: 0x0000036D
		public void Terminate()
		{
		}

		// Token: 0x06006F54 RID: 28500 RVA: 0x0000216D File Offset: 0x0000036D
		public void Prepare(DuelEffectPool.Type type, int player, int position)
		{
		}

		// Token: 0x06006F55 RID: 28501 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayPreparedEffect(DuelFieldBase duelField)
		{
		}

		// Token: 0x06006F56 RID: 28502 RVA: 0x0000216D File Offset: 0x0000036D
		public void CancelPreareEffect()
		{
		}

		// Token: 0x0400AA3D RID: 43581
		private DuelEffectPool pool;

		// Token: 0x0400AA3E RID: 43582
		private List<ResidualEffect.EffectInfo> effects;

		// Token: 0x0400AA3F RID: 43583
		private List<ResidualEffect.PrepareInfo> prepareInfoList;

		// Token: 0x02000EE7 RID: 3815
		public class EffectInfo
		{
			// Token: 0x0400AA40 RID: 43584
			public SimpleEffect effectObj;
		}

		// Token: 0x02000EE8 RID: 3816
		public class PrepareInfo
		{
			// Token: 0x0400AA41 RID: 43585
			public DuelEffectPool.Type type;

			// Token: 0x0400AA42 RID: 43586
			public int player;

			// Token: 0x0400AA43 RID: 43587
			public int position;
		}
	}
}
