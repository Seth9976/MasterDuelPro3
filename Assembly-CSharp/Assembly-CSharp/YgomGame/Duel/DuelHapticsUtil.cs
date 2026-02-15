using System;
using System.Collections.Generic;
using YgomSystem;

namespace YgomGame.Duel
{
	// Token: 0x02000D79 RID: 3449
	public static class DuelHapticsUtil
	{
		// Token: 0x06006538 RID: 25912 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetVibrateFlag(bool enable)
		{
		}

		// Token: 0x06006539 RID: 25913 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Vibrate(DuelHapticsUtil.Type type)
		{
		}

		// Token: 0x04009F5C RID: 40796
		private static Dictionary<DuelHapticsUtil.Type, GamePad.VIBRATION> intensityList;

		// Token: 0x02000D7A RID: 3450
		public enum Type
		{
			// Token: 0x04009F5E RID: 40798
			None,
			// Token: 0x04009F5F RID: 40799
			MonsterCutin,
			// Token: 0x04009F60 RID: 40800
			LandingMiddle,
			// Token: 0x04009F61 RID: 40801
			LandingHigh,
			// Token: 0x04009F62 RID: 40802
			CardBreak,
			// Token: 0x04009F63 RID: 40803
			AttackLow,
			// Token: 0x04009F64 RID: 40804
			AttackHigh,
			// Token: 0x04009F65 RID: 40805
			DirectAttack,
			// Token: 0x04009F66 RID: 40806
			EffectDamage,
			// Token: 0x04009F67 RID: 40807
			BgBreak,
			// Token: 0x04009F68 RID: 40808
			LethalEffect
		}
	}
}
