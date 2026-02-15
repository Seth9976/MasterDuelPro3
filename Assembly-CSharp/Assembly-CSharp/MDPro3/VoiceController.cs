using System;

namespace MDPro3
{
	// Token: 0x0200124F RID: 4687
	public class VoiceController
	{
		// Token: 0x02001250 RID: 4688
		public enum Category
		{
			// Token: 0x0400C587 RID: 50567
			Null,
			// Token: 0x0400C588 RID: 50568
			BeforeDuel,
			// Token: 0x0400C589 RID: 50569
			DuelStart,
			// Token: 0x0400C58A RID: 50570
			TurnStart,
			// Token: 0x0400C58B RID: 50571
			Draw,
			// Token: 0x0400C58C RID: 50572
			DestinyDraw,
			// Token: 0x0400C58D RID: 50573
			BeforeCardEffect,
			// Token: 0x0400C58E RID: 50574
			CardEffect,
			// Token: 0x0400C58F RID: 50575
			MainMagicTrap,
			// Token: 0x0400C590 RID: 50576
			MainMonsterEffect,
			// Token: 0x0400C591 RID: 50577
			BeforeSummon,
			// Token: 0x0400C592 RID: 50578
			Summon,
			// Token: 0x0400C593 RID: 50579
			None,
			// Token: 0x0400C594 RID: 50580
			MainMonsterSummon,
			// Token: 0x0400C595 RID: 50581
			BattleStart,
			// Token: 0x0400C596 RID: 50582
			BeforeAttackNormal,
			// Token: 0x0400C597 RID: 50583
			BeforeAttackFinish,
			// Token: 0x0400C598 RID: 50584
			Attack,
			// Token: 0x0400C599 RID: 50585
			DirectAttack,
			// Token: 0x0400C59A RID: 50586
			MainMonsterAttack,
			// Token: 0x0400C59B RID: 50587
			CardSet,
			// Token: 0x0400C59C RID: 50588
			TurnEnd,
			// Token: 0x0400C59D RID: 50589
			Damage,
			// Token: 0x0400C59E RID: 50590
			FinishDamage,
			// Token: 0x0400C59F RID: 50591
			CostDamage,
			// Token: 0x0400C5A0 RID: 50592
			BigDamage,
			// Token: 0x0400C5A1 RID: 50593
			AfterDamage,
			// Token: 0x0400C5A2 RID: 50594
			AfterBigDamage,
			// Token: 0x0400C5A3 RID: 50595
			Win,
			// Token: 0x0400C5A4 RID: 50596
			Lose,
			// Token: 0x0400C5A5 RID: 50597
			Taunt,
			// Token: 0x0400C5A6 RID: 50598
			Surprise,
			// Token: 0x0400C5A7 RID: 50599
			Title,
			// Token: 0x0400C5A8 RID: 50600
			Skill,
			// Token: 0x0400C5A9 RID: 50601
			Chat,
			// Token: 0x0400C5AA RID: 50602
			CharaChange,
			// Token: 0x0400C5AB RID: 50603
			SwitchToPartner,
			// Token: 0x0400C5AC RID: 50604
			BeforeMainSummon,
			// Token: 0x0400C5AD RID: 50605
			RidingDuelStart,
			// Token: 0x0400C5AE RID: 50606
			CoinTossOfMagicTrap,
			// Token: 0x0400C5AF RID: 50607
			CoinTossOfMonster,
			// Token: 0x0400C5B0 RID: 50608
			BeforeDimensionDuel,
			// Token: 0x0400C5B1 RID: 50609
			DimensionDuelStart,
			// Token: 0x0400C5B2 RID: 50610
			Transformation,
			// Token: 0x0400C5B3 RID: 50611
			ActionDuelStart,
			// Token: 0x0400C5B4 RID: 50612
			ActionCard,
			// Token: 0x0400C5B5 RID: 50613
			BeforeMainReincarnationSummon,
			// Token: 0x0400C5B6 RID: 50614
			MainMonsterReincarnationSummon,
			// Token: 0x0400C5B7 RID: 50615
			RushDuelStart,
			// Token: 0x0400C5B8 RID: 50616
			RidingRushDuelStart
		}

		// Token: 0x02001251 RID: 4689
		public enum SummonSub
		{
			// Token: 0x0400C5BA RID: 50618
			Normal,
			// Token: 0x0400C5BB RID: 50619
			Guard,
			// Token: 0x0400C5BC RID: 50620
			Special,
			// Token: 0x0400C5BD RID: 50621
			Release,
			// Token: 0x0400C5BE RID: 50622
			Advance,
			// Token: 0x0400C5BF RID: 50623
			Fusion,
			// Token: 0x0400C5C0 RID: 50624
			Ritual,
			// Token: 0x0400C5C1 RID: 50625
			Sync,
			// Token: 0x0400C5C2 RID: 50626
			Xyz,
			// Token: 0x0400C5C3 RID: 50627
			Pendulum,
			// Token: 0x0400C5C4 RID: 50628
			Link,
			// Token: 0x0400C5C5 RID: 50629
			Maximum,
			// Token: 0x0400C5C6 RID: 50630
			RushFusion
		}

		// Token: 0x02001252 RID: 4690
		public enum CardSetSub
		{
			// Token: 0x0400C5C8 RID: 50632
			MagicTrap,
			// Token: 0x0400C5C9 RID: 50633
			Monster
		}

		// Token: 0x02001253 RID: 4691
		public enum CardEffectSub
		{
			// Token: 0x0400C5CB RID: 50635
			FromHand,
			// Token: 0x0400C5CC RID: 50636
			Magic,
			// Token: 0x0400C5CD RID: 50637
			QuickPlayMagic,
			// Token: 0x0400C5CE RID: 50638
			PermanentMagic,
			// Token: 0x0400C5CF RID: 50639
			EquipMagic,
			// Token: 0x0400C5D0 RID: 50640
			RitualMagic,
			// Token: 0x0400C5D1 RID: 50641
			Trap,
			// Token: 0x0400C5D2 RID: 50642
			PermanentTrap,
			// Token: 0x0400C5D3 RID: 50643
			CounterTrap,
			// Token: 0x0400C5D4 RID: 50644
			Reverse,
			// Token: 0x0400C5D5 RID: 50645
			MonsterEffect,
			// Token: 0x0400C5D6 RID: 50646
			FieldMagic,
			// Token: 0x0400C5D7 RID: 50647
			General,
			// Token: 0x0400C5D8 RID: 50648
			PendulumScale,
			// Token: 0x0400C5D9 RID: 50649
			PendulumEffect
		}

		// Token: 0x02001254 RID: 4692
		public enum MainMonsterSub
		{
			// Token: 0x0400C5DB RID: 50651
			Default,
			// Token: 0x0400C5DC RID: 50652
			Maximum
		}

		// Token: 0x02001255 RID: 4693
		public enum RushDuelStartSub
		{
			// Token: 0x0400C5DE RID: 50654
			Beginning,
			// Token: 0x0400C5DF RID: 50655
			Main,
			// Token: 0x0400C5E0 RID: 50656
			Max
		}

		// Token: 0x02001256 RID: 4694
		public enum NormalEffectState
		{
			// Token: 0x0400C5E2 RID: 50658
			Default,
			// Token: 0x0400C5E3 RID: 50659
			FromHand,
			// Token: 0x0400C5E4 RID: 50660
			Reverse,
			// Token: 0x0400C5E5 RID: 50661
			PendulumScale
		}
	}
}
