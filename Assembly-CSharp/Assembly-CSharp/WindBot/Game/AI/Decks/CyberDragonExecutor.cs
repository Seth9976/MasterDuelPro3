using System;
using System.Collections.Generic;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x020002D1 RID: 721
	[Deck("CyberDragon", "AI_CyberDragon", "NotFinished")]
	public class CyberDragonExecutor : DefaultExecutor
	{
		// Token: 0x060011A3 RID: 4515 RVA: 0x00059FCC File Offset: 0x000581CC
		public CyberDragonExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.SpellSet, 95286165);
			base.AddExecutor(ExecutorType.Activate, 11961740, new Func<bool>(this.Capsule));
			base.AddExecutor(ExecutorType.Activate, 12580477, new Func<bool>(base.DefaultRaigeki));
			base.AddExecutor(ExecutorType.Activate, 24094653, new Func<bool>(this.PolymerizationEffect));
			base.AddExecutor(ExecutorType.Activate, 37630732, new Func<bool>(this.PowerBondEffect));
			base.AddExecutor(ExecutorType.Activate, 52875873, new Func<bool>(this.EvolutionBurstEffect));
			base.AddExecutor(ExecutorType.Activate, 53129443, new Func<bool>(base.DefaultDarkHole));
			base.AddExecutor(ExecutorType.Activate, 66607691);
			base.AddExecutor(ExecutorType.Activate, 95286165, new Func<bool>(this.DeFusionEffect));
			base.AddExecutor(ExecutorType.Activate, 29401950, new Func<bool>(base.DefaultUniqueTrap));
			base.AddExecutor(ExecutorType.Activate, 44095762, new Func<bool>(base.DefaultUniqueTrap));
			base.AddExecutor(ExecutorType.Activate, 91989718);
			base.AddExecutor(ExecutorType.Activate, 3819470, new Func<bool>(base.DefaultTrap));
			base.AddExecutor(ExecutorType.Activate, 97077563, new Func<bool>(base.DefaultCallOfTheHaunted));
			base.AddExecutor(ExecutorType.SummonOrSet, 59281922, new Func<bool>(this.NoCyberDragonSpsummon));
			base.AddExecutor(ExecutorType.SummonOrSet, 3370104, new Func<bool>(this.NoCyberDragonSpsummon));
			base.AddExecutor(ExecutorType.Summon, 3657444, new Func<bool>(this.NoCyberDragonSpsummon));
			base.AddExecutor(ExecutorType.MonsterSet, 23893227, new Func<bool>(this.NoCyberDragonSpsummon));
			base.AddExecutor(ExecutorType.MonsterSet, 67159705, new Func<bool>(this.ArmoredCybernSet));
			base.AddExecutor(ExecutorType.SummonOrSet, 26439287, new Func<bool>(this.ProtoCyberDragonSummon));
			base.AddExecutor(ExecutorType.Summon, 76986005, new Func<bool>(this.CyberKirinSummon));
			base.AddExecutor(ExecutorType.SpSummon, 70095154);
			base.AddExecutor(ExecutorType.SpSummon, 1546123);
			base.AddExecutor(ExecutorType.SpSummon, 74157028);
			base.AddExecutor(ExecutorType.SpSummon, 68774379);
			base.AddExecutor(ExecutorType.SpSummon, 4162088);
			base.AddExecutor(ExecutorType.Activate, 68774379);
			base.AddExecutor(ExecutorType.Activate, 4162088);
			base.AddExecutor(ExecutorType.Activate, 59281922);
			base.AddExecutor(ExecutorType.Activate, 3370104);
			base.AddExecutor(ExecutorType.Activate, 76986005);
			base.AddExecutor(ExecutorType.Activate, 67159705, new Func<bool>(this.ArmoredCybernEffect));
			base.AddExecutor(ExecutorType.Activate, 3657444);
			base.AddExecutor(ExecutorType.SpellSet, new Func<bool>(base.DefaultSpellSet));
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(base.DefaultMonsterRepos));
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x0005A277 File Offset: 0x00058477
		private bool CyberDragonInHand()
		{
			return base.Bot.HasInHand(70095154);
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x0005A289 File Offset: 0x00058489
		private bool CyberDragonInGraveyard()
		{
			return base.Bot.HasInGraveyard(70095154);
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x0005A29B File Offset: 0x0005849B
		private bool CyberDragonInMonsterZone()
		{
			return base.Bot.HasInMonstersZone(70095154, false, false, false);
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x0005A2B0 File Offset: 0x000584B0
		private bool CyberDragonIsBanished()
		{
			return base.Bot.HasInBanished(70095154);
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x0005A2C4 File Offset: 0x000584C4
		private bool Capsule()
		{
			IList<int> SelectedCard = new List<int>();
			SelectedCard.Add(37630732);
			SelectedCard.Add(53129443);
			SelectedCard.Add(12580477);
			base.AI.SelectCard(SelectedCard);
			return true;
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x0005A308 File Offset: 0x00058508
		private bool PolymerizationEffect()
		{
			if (base.Bot.GetCountCardInZone(base.Bot.MonsterZone, 70095154) + base.Bot.GetCountCardInZone(base.Bot.MonsterZone, 26439287) + base.Bot.GetCountCardInZone(base.Bot.MonsterZone, 59281922) + base.Bot.GetCountCardInZone(base.Bot.MonsterZone, 59281922) + base.Bot.GetCountCardInZone(base.Bot.Hand, 70095154) >= 3)
			{
				base.AI.SelectCard(1546123);
			}
			else
			{
				base.AI.SelectCard(74157028);
			}
			return true;
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x0005A3C8 File Offset: 0x000585C8
		private bool PowerBondEffect()
		{
			this.PowerBondUsed = true;
			if (base.Bot.GetCountCardInZone(base.Bot.MonsterZone, 70095154) + base.Bot.GetCountCardInZone(base.Bot.MonsterZone, 26439287) + base.Bot.GetCountCardInZone(base.Bot.Hand, 70095154) + base.Bot.GetCountCardInZone(base.Bot.Graveyard, 70095154) + base.Bot.GetCountCardInZone(base.Bot.Hand, 23893227) + base.Bot.GetCountCardInZone(base.Bot.Graveyard, 23893227) + base.Bot.GetCountCardInZone(base.Bot.Graveyard, 59281922) + base.Bot.GetCountCardInZone(base.Bot.MonsterZone, 59281922) >= 3)
			{
				base.AI.SelectCard(1546123);
			}
			else
			{
				base.AI.SelectCard(74157028);
			}
			return true;
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x0005A4E4 File Offset: 0x000586E4
		private bool EvolutionBurstEffect()
		{
			ClientCard bestMy = base.Bot.GetMonsters().GetHighestAttackMonster(false);
			if (bestMy == null || !base.Util.IsOneEnemyBetterThanValue(bestMy.Attack, false))
			{
				return false;
			}
			base.AI.SelectCard(base.Enemy.MonsterZone.GetHighestAttackMonster(false));
			return true;
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x0005A539 File Offset: 0x00058739
		private bool NoCyberDragonSpsummon()
		{
			return !this.CyberDragonInHand() || base.Bot.GetMonsterCount() != 0 || base.Enemy.GetMonsterCount() == 0;
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x0005A560 File Offset: 0x00058760
		private bool ArmoredCybernSet()
		{
			return (!this.CyberDragonInHand() || base.Bot.GetMonsterCount() != 0 || base.Enemy.GetMonsterCount() == 0) && ((!base.Bot.HasInHand(59281922) && !base.Bot.HasInHand(3370104)) || base.Util.IsOneEnemyBetterThanValue(1800, true));
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x0005A5CC File Offset: 0x000587CC
		private bool ProtoCyberDragonSummon()
		{
			return (base.Bot.GetCountCardInZone(base.Bot.Hand, 70095154) + base.Bot.GetCountCardInZone(base.Bot.MonsterZone, 70095154) + base.Bot.GetCountCardInZone(base.Bot.MonsterZone, 23893227) >= 1 && base.Bot.HasInHand(24094653)) || (base.Bot.GetCountCardInZone(base.Bot.Hand, 70095154) + base.Bot.GetCountCardInZone(base.Bot.MonsterZone, 70095154) + base.Bot.GetCountCardInZone(base.Bot.Graveyard, 70095154) + base.Bot.GetCountCardInZone(base.Bot.Graveyard, 23893227) >= 1 && base.Bot.HasInHand(37630732)) || ((!this.CyberDragonInHand() || base.Bot.GetMonsterCount() != 0 || base.Enemy.GetMonsterCount() == 0) && ((!base.Bot.HasInHand(59281922) && !base.Bot.HasInHand(3370104)) || base.Util.IsOneEnemyBetterThanValue(1800, true)));
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x0005A726 File Offset: 0x00058926
		private bool CyberKirinSummon()
		{
			return this.PowerBondUsed;
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x0005A730 File Offset: 0x00058930
		private bool ArmoredCybernEffect()
		{
			return base.Card.Location == CardLocation.Hand || (base.Card.Location == CardLocation.SpellZone && (base.Util.IsOneEnemyBetterThanValue(base.Bot.GetMonsters().GetHighestAttackMonster(false).Attack, true) && base.ActivateDescription == base.Util.GetStringId(67159705, 2)));
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x0005A79D File Offset: 0x0005899D
		private bool DeFusionEffect()
		{
			return base.Duel.Phase == DuelPhase.Battle && !base.Bot.HasAttackingMonster();
		}

		// Token: 0x0400164E RID: 5710
		private bool PowerBondUsed;

		// Token: 0x020002D2 RID: 722
		public class CardId
		{
			// Token: 0x0400164F RID: 5711
			public const int CyberLaserDragon = 4162088;

			// Token: 0x04001650 RID: 5712
			public const int CyberBarrierDragon = 68774379;

			// Token: 0x04001651 RID: 5713
			public const int CyberDragon = 70095154;

			// Token: 0x04001652 RID: 5714
			public const int CyberDragonDrei = 59281922;

			// Token: 0x04001653 RID: 5715
			public const int CyberPhoenix = 3370104;

			// Token: 0x04001654 RID: 5716
			public const int ArmoredCybern = 67159705;

			// Token: 0x04001655 RID: 5717
			public const int ProtoCyberDragon = 26439287;

			// Token: 0x04001656 RID: 5718
			public const int CyberKirin = 76986005;

			// Token: 0x04001657 RID: 5719
			public const int CyberDragonCore = 23893227;

			// Token: 0x04001658 RID: 5720
			public const int CyberValley = 3657444;

			// Token: 0x04001659 RID: 5721
			public const int Raigeki = 12580477;

			// Token: 0x0400165A RID: 5722
			public const int DarkHole = 53129443;

			// Token: 0x0400165B RID: 5723
			public const int DifferentDimensionCapsule = 11961740;

			// Token: 0x0400165C RID: 5724
			public const int Polymerization = 24094653;

			// Token: 0x0400165D RID: 5725
			public const int PowerBond = 37630732;

			// Token: 0x0400165E RID: 5726
			public const int EvolutionBurst = 52875873;

			// Token: 0x0400165F RID: 5727
			public const int PhotonGeneratorUnit = 66607691;

			// Token: 0x04001660 RID: 5728
			public const int DeFusion = 95286165;

			// Token: 0x04001661 RID: 5729
			public const int BottomlessTrapHole = 29401950;

			// Token: 0x04001662 RID: 5730
			public const int MirrorForce = 44095762;

			// Token: 0x04001663 RID: 5731
			public const int AttackReflectorUnit = 91989718;

			// Token: 0x04001664 RID: 5732
			public const int CyberneticHiddenTechnology = 92773018;

			// Token: 0x04001665 RID: 5733
			public const int CallOfTheHaunted = 97077563;

			// Token: 0x04001666 RID: 5734
			public const int SevenToolsOfTheBandit = 3819470;

			// Token: 0x04001667 RID: 5735
			public const int CyberTwinDragon = 74157028;

			// Token: 0x04001668 RID: 5736
			public const int CyberEndDragon = 1546123;

			// Token: 0x04001669 RID: 5737
			public const int CyberDragonNova = 58069384;
		}
	}
}
