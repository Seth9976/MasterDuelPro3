using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MDPro3.Duel
{
	// Token: 0x02001518 RID: 5400
	[Serializable]
	public class VoicesData
	{
		// Token: 0x06009D1B RID: 40219 RVA: 0x001952D8 File Offset: 0x001934D8
		public VoiceInfoEntry GetCategoryEntry(VoiceController.Category category)
		{
			switch (category)
			{
			case VoiceController.Category.BeforeDuel:
				return this.BeforeDuel;
			case VoiceController.Category.DuelStart:
				return this.DuelStart;
			case VoiceController.Category.TurnStart:
				return this.TurnStart;
			case VoiceController.Category.Draw:
				return this.Draw;
			case VoiceController.Category.DestinyDraw:
				return this.DestinyDraw;
			case VoiceController.Category.BeforeCardEffect:
				return this.BeforeCardEffect;
			case VoiceController.Category.CardEffect:
				return this.CardEffect;
			case VoiceController.Category.MainMagicTrap:
				return this.MainMagicTrap;
			case VoiceController.Category.MainMonsterEffect:
				return this.MainMonsterEffect;
			case VoiceController.Category.BeforeSummon:
				return this.BeforeSummon;
			case VoiceController.Category.Summon:
				return this.Summon;
			case VoiceController.Category.None:
				return this.None;
			case VoiceController.Category.MainMonsterSummon:
				return this.MainMonsterSummon;
			case VoiceController.Category.BattleStart:
				return this.BattleStart;
			case VoiceController.Category.BeforeAttackNormal:
				return this.BeforeAttackNormal;
			case VoiceController.Category.BeforeAttackFinish:
				return this.BeforeAttackFinish;
			case VoiceController.Category.Attack:
				return this.Attack;
			case VoiceController.Category.DirectAttack:
				return this.DirectAttack;
			case VoiceController.Category.MainMonsterAttack:
				return this.MainMonsterAttack;
			case VoiceController.Category.CardSet:
				return this.CardSet;
			case VoiceController.Category.TurnEnd:
				return this.TurnEnd;
			case VoiceController.Category.Damage:
				return this.Damage;
			case VoiceController.Category.FinishDamage:
				return this.FinishDamage;
			case VoiceController.Category.CostDamage:
				return this.CostDamage;
			case VoiceController.Category.BigDamage:
				return this.BigDamage;
			case VoiceController.Category.AfterDamage:
				return this.AfterDamage;
			case VoiceController.Category.AfterBigDamage:
				return this.AfterBigDamage;
			case VoiceController.Category.Win:
				return this.Win;
			case VoiceController.Category.Lose:
				return this.Lose;
			case VoiceController.Category.Taunt:
				return this.Taunt;
			case VoiceController.Category.Surprise:
				return this.Surprise;
			case VoiceController.Category.Title:
				return this.Title;
			case VoiceController.Category.Skill:
				return this.Skill;
			case VoiceController.Category.Chat:
				return this.Chat;
			case VoiceController.Category.CharaChange:
				return this.CharaChange;
			case VoiceController.Category.SwitchToPartner:
				return this.SwitchToPartner;
			case VoiceController.Category.BeforeMainSummon:
				return this.BeforeMainSummon;
			case VoiceController.Category.RidingDuelStart:
				return this.RidingDuelStart;
			case VoiceController.Category.CoinTossOfMagicTrap:
				return this.CoinTossOfMagicTrap;
			case VoiceController.Category.CoinTossOfMonster:
				return this.CoinTossOfMonster;
			case VoiceController.Category.BeforeDimensionDuel:
				return this.BeforeDimensionDuel;
			case VoiceController.Category.DimensionDuelStart:
				return this.DimensionDuelStart;
			case VoiceController.Category.Transformation:
				return this.Transformation;
			case VoiceController.Category.ActionDuelStart:
				return this.ActionDuelStart;
			case VoiceController.Category.ActionCard:
				return this.ActionCard;
			case VoiceController.Category.BeforeMainReincarnationSummon:
				return this.BeforeMainReincarnationSummon;
			case VoiceController.Category.MainMonsterReincarnationSummon:
				return this.MainMonsterReincarnationSummon;
			case VoiceController.Category.RushDuelStart:
				return this.RushDuelStart;
			case VoiceController.Category.RidingRushDuelStart:
				return this.RidingRushDuelStart;
			default:
				return null;
			}
		}

		// Token: 0x06009D1C RID: 40220 RVA: 0x00195510 File Offset: 0x00193710
		public List<VoiceInfoEntry> GetEntryWithCard()
		{
			return new List<VoiceInfoEntry> { this.MainMagicTrap, this.MainMonsterEffect, this.MainMonsterSummon, this.MainMonsterAttack, this.BeforeMainSummon, this.BeforeMainReincarnationSummon, this.MainMonsterReincarnationSummon };
		}

		// Token: 0x0400DAF6 RID: 56054
		public int DummyFlag;

		// Token: 0x0400DAF7 RID: 56055
		public Dictionary<string, int> NumVoices;

		// Token: 0x0400DAF8 RID: 56056
		public int labelver;

		// Token: 0x0400DAF9 RID: 56057
		public VoiceInfoEntry BeforeDuel;

		// Token: 0x0400DAFA RID: 56058
		public VoiceInfoEntry BeforeDuelSp;

		// Token: 0x0400DAFB RID: 56059
		public VoiceInfoEntry DuelStart;

		// Token: 0x0400DAFC RID: 56060
		public VoiceInfoEntry TurnStart;

		// Token: 0x0400DAFD RID: 56061
		[JsonProperty("TurnStart.01")]
		public VoiceInfoEntry TurnStart01;

		// Token: 0x0400DAFE RID: 56062
		public VoiceInfoEntry Draw;

		// Token: 0x0400DAFF RID: 56063
		[JsonProperty("Draw.01")]
		public VoiceInfoEntry Draw01;

		// Token: 0x0400DB00 RID: 56064
		public VoiceInfoEntry DestinyDraw;

		// Token: 0x0400DB01 RID: 56065
		public VoiceInfoEntry BeforeCardEffect;

		// Token: 0x0400DB02 RID: 56066
		public VoiceInfoEntry CardEffect;

		// Token: 0x0400DB03 RID: 56067
		public VoiceInfoEntry MainMagicTrap;

		// Token: 0x0400DB04 RID: 56068
		public VoiceInfoEntry MainMonsterEffect;

		// Token: 0x0400DB05 RID: 56069
		public VoiceInfoEntry BeforeSummon;

		// Token: 0x0400DB06 RID: 56070
		public VoiceInfoEntry Summon;

		// Token: 0x0400DB07 RID: 56071
		public VoiceInfoEntry None;

		// Token: 0x0400DB08 RID: 56072
		public VoiceInfoEntry MainMonsterSummon;

		// Token: 0x0400DB09 RID: 56073
		public VoiceInfoEntry BattleStart;

		// Token: 0x0400DB0A RID: 56074
		public VoiceInfoEntry BeforeAttackNormal;

		// Token: 0x0400DB0B RID: 56075
		public VoiceInfoEntry BeforeAttackFinish;

		// Token: 0x0400DB0C RID: 56076
		public VoiceInfoEntry Attack;

		// Token: 0x0400DB0D RID: 56077
		public VoiceInfoEntry DirectAttack;

		// Token: 0x0400DB0E RID: 56078
		public VoiceInfoEntry MainMonsterAttack;

		// Token: 0x0400DB0F RID: 56079
		public VoiceInfoEntry CardSet;

		// Token: 0x0400DB10 RID: 56080
		public VoiceInfoEntry TurnEnd;

		// Token: 0x0400DB11 RID: 56081
		public VoiceInfoEntry Damage;

		// Token: 0x0400DB12 RID: 56082
		public VoiceInfoEntry FinishDamage;

		// Token: 0x0400DB13 RID: 56083
		public VoiceInfoEntry CostDamage;

		// Token: 0x0400DB14 RID: 56084
		public VoiceInfoEntry BigDamage;

		// Token: 0x0400DB15 RID: 56085
		public VoiceInfoEntry AfterDamage;

		// Token: 0x0400DB16 RID: 56086
		public VoiceInfoEntry AfterBigDamage;

		// Token: 0x0400DB17 RID: 56087
		public VoiceInfoEntry Win;

		// Token: 0x0400DB18 RID: 56088
		public VoiceInfoEntry WinSp;

		// Token: 0x0400DB19 RID: 56089
		public VoiceInfoEntry Lose;

		// Token: 0x0400DB1A RID: 56090
		public VoiceInfoEntry LoseSp;

		// Token: 0x0400DB1B RID: 56091
		public VoiceInfoEntry Taunt;

		// Token: 0x0400DB1C RID: 56092
		public VoiceInfoEntry Surprise;

		// Token: 0x0400DB1D RID: 56093
		public VoiceInfoEntry Title;

		// Token: 0x0400DB1E RID: 56094
		public VoiceInfoEntry Skill;

		// Token: 0x0400DB1F RID: 56095
		public VoiceInfoEntry Chat;

		// Token: 0x0400DB20 RID: 56096
		public VoiceInfoEntry CharaChange;

		// Token: 0x0400DB21 RID: 56097
		public VoiceInfoEntry SwitchToPartner;

		// Token: 0x0400DB22 RID: 56098
		public VoiceInfoEntry BeforeMainSummon;

		// Token: 0x0400DB23 RID: 56099
		public VoiceInfoEntry RidingDuelStart;

		// Token: 0x0400DB24 RID: 56100
		public VoiceInfoEntry CoinTossOfMagicTrap;

		// Token: 0x0400DB25 RID: 56101
		public VoiceInfoEntry CoinTossOfMonster;

		// Token: 0x0400DB26 RID: 56102
		public VoiceInfoEntry BeforeDimensionDuel;

		// Token: 0x0400DB27 RID: 56103
		public VoiceInfoEntry DimensionDuelStart;

		// Token: 0x0400DB28 RID: 56104
		public VoiceInfoEntry Transformation;

		// Token: 0x0400DB29 RID: 56105
		public VoiceInfoEntry ActionDuelStart;

		// Token: 0x0400DB2A RID: 56106
		public VoiceInfoEntry ActionCard;

		// Token: 0x0400DB2B RID: 56107
		public VoiceInfoEntry BeforeMainReincarnationSummon;

		// Token: 0x0400DB2C RID: 56108
		public VoiceInfoEntry MainMonsterReincarnationSummon;

		// Token: 0x0400DB2D RID: 56109
		public VoiceInfoEntry RushDuelStart;

		// Token: 0x0400DB2E RID: 56110
		public VoiceInfoEntry RidingRushDuelStart;
	}
}
