using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Card;

namespace YgomGame.Duel
{
	// Token: 0x02000C95 RID: 3221
	public class BattleRunAttackSetting : ScriptableObject
	{
		// Token: 0x06005C47 RID: 23623 RVA: 0x000029CC File Offset: 0x00000BCC
		public EffectTaskBattleRun.AttackType GetDefaultAttackType(Content.Attribute attribute)
		{
			return EffectTaskBattleRun.AttackType.Strike;
		}

		// Token: 0x06005C48 RID: 23624 RVA: 0x000029CC File Offset: 0x00000BCC
		public EffectTaskBattleRun.AttackType GetCardAttackType(int card_id)
		{
			return EffectTaskBattleRun.AttackType.Strike;
		}

		// Token: 0x06005C49 RID: 23625 RVA: 0x000029CC File Offset: 0x00000BCC
		public LethalEffect.EffectType GetCardLethaEffectType(int card_id)
		{
			return LethalEffect.EffectType.Normal;
		}

		// Token: 0x0400977E RID: 38782
		public List<BattleRunAttackSetting.DefaultMotionType> defaultMotionSettings;

		// Token: 0x0400977F RID: 38783
		public List<BattleRunAttackSetting.CardMotionType> cardMotionSettings;

		// Token: 0x02000C96 RID: 3222
		[Serializable]
		public class DefaultMotionType
		{
			// Token: 0x06005C4B RID: 23627 RVA: 0x0000216A File Offset: 0x0000036A
			public BattleRunAttackSetting.DefaultMotionType Copy()
			{
				return null;
			}

			// Token: 0x04009780 RID: 38784
			public Content.Attribute keyAttribute;

			// Token: 0x04009781 RID: 38785
			public EffectTaskBattleRun.AttackType attackType;
		}

		// Token: 0x02000C97 RID: 3223
		[Serializable]
		public class CardMotionType
		{
			// Token: 0x06005C4D RID: 23629 RVA: 0x0000216A File Offset: 0x0000036A
			public BattleRunAttackSetting.CardMotionType Copy()
			{
				return null;
			}

			// Token: 0x04009782 RID: 38786
			public int cardID;

			// Token: 0x04009783 RID: 38787
			public EffectTaskBattleRun.AttackType attackType;

			// Token: 0x04009784 RID: 38788
			public LethalEffect.EffectType lethalEffectType;
		}
	}
}
