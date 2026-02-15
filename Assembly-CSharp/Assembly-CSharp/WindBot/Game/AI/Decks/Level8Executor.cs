using System;
using System.Collections.Generic;
using System.Linq;
using YGOSharp.OCGWrapper;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x0200034B RID: 843
	[Deck("Level VIII", "AI_Level8", "Normal")]
	internal class Level8Executor : DefaultExecutor
	{
		// Token: 0x06001693 RID: 5779 RVA: 0x00085E24 File Offset: 0x00084024
		public Level8Executor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.Activate, 24224830, new Func<bool>(base.DefaultCalledByTheGrave));
			base.AddExecutor(ExecutorType.Activate, 12580477);
			base.AddExecutor(ExecutorType.Activate, 18144506);
			base.AddExecutor(ExecutorType.Repos, 90590303, new Func<bool>(this.MonsterRepos));
			base.AddExecutor(ExecutorType.Activate, 50954680, new Func<bool>(this.CrystalWingSynchroDragonEffect));
			base.AddExecutor(ExecutorType.Activate, 27548199, new Func<bool>(this.BorreloadSavageDragonEffect));
			base.AddExecutor(ExecutorType.Activate, 82012319, new Func<bool>(this.ScrapGolemEffect));
			base.AddExecutor(ExecutorType.Activate, 911883, new Func<bool>(this.UnexpectedDaiFirst));
			base.AddExecutor(ExecutorType.SpSummon, 65367484, new Func<bool>(this.PhotonThrasherSummonFirst));
			base.AddExecutor(ExecutorType.Activate, 911883);
			base.AddExecutor(ExecutorType.SpSummon, 65367484);
			base.AddExecutor(ExecutorType.Activate, 32807846, new Func<bool>(this.ReinforcementofTheArmyEffect));
			base.AddExecutor(ExecutorType.Activate, 81439173, new Func<bool>(this.FoolishBurialEffect));
			base.AddExecutor(ExecutorType.Activate, 83764718, new Func<bool>(base.DefaultCallOfTheHaunted));
			base.AddExecutor(ExecutorType.Summon, 4334811, new Func<bool>(this.ScrapRecyclerSummonFirst));
			base.AddExecutor(ExecutorType.Activate, 4334811, new Func<bool>(this.ScrapRecyclerEffect));
			base.AddExecutor(ExecutorType.Activate, 72291078, new Func<bool>(this.MechaPhantomBeastOLionEffect));
			base.AddExecutor(ExecutorType.SpSummon, 47363932, new Func<bool>(this.ScrapWyvernSummon));
			base.AddExecutor(ExecutorType.Activate, 47363932, new Func<bool>(this.ScrapWyvernEffect));
			base.AddExecutor(ExecutorType.Activate, 9742784, new Func<bool>(this.JetSynchronEffect));
			base.AddExecutor(ExecutorType.SpSummon, 50588353, new Func<bool>(this.CrystronNeedlefiberSummon));
			base.AddExecutor(ExecutorType.Activate, 50588353, new Func<bool>(this.CrystronNeedlefiberEffect));
			base.AddExecutor(ExecutorType.SpSummon, 21887175, new Func<bool>(this.MekkKnightCrusadiaAstramSummon));
			base.AddExecutor(ExecutorType.Activate, 21887175, new Func<bool>(this.MekkKnightCrusadiaAstramEffect));
			base.AddExecutor(ExecutorType.Activate, 94886282);
			base.AddExecutor(ExecutorType.Summon, 25259669, new Func<bool>(this.GoblindberghSummonFirst));
			base.AddExecutor(ExecutorType.Activate, 25259669, new Func<bool>(this.GoblindberghEffect));
			base.AddExecutor(ExecutorType.Summon, 53573406, new Func<bool>(this.MaskedChameleonSummonFirst));
			base.AddExecutor(ExecutorType.Activate, 53573406, new Func<bool>(this.MaskedChameleonEffect));
			base.AddExecutor(ExecutorType.SpSummon, 12213463);
			base.AddExecutor(ExecutorType.Summon, 12213463, new Func<bool>(this.WhiteRoseDragonSummonFirst));
			base.AddExecutor(ExecutorType.Activate, 12213463, new Func<bool>(this.WhiteRoseDragonEffect));
			base.AddExecutor(ExecutorType.Summon, 77558536, new Func<bool>(this.L4TunerSummonFirst));
			base.AddExecutor(ExecutorType.Summon, 19139516, new Func<bool>(this.L4TunerSummonFirst));
			base.AddExecutor(ExecutorType.Summon, 87979586, new Func<bool>(this.L4TunerSummonFirst));
			base.AddExecutor(ExecutorType.Summon, 53573406, new Func<bool>(this.L4TunerSummonFirst));
			base.AddExecutor(ExecutorType.Summon, 67696066, new Func<bool>(this.L4NonTunerSummonFirst));
			base.AddExecutor(ExecutorType.Summon, 25259669, new Func<bool>(this.L4NonTunerSummonFirst));
			base.AddExecutor(ExecutorType.Summon, 44928016, new Func<bool>(this.L4NonTunerSummonFirst));
			base.AddExecutor(ExecutorType.Summon, 12213463, new Func<bool>(this.L4NonTunerSummonFirst));
			base.AddExecutor(ExecutorType.Summon, 26118970, new Func<bool>(this.OtherTunerSummonFirst));
			base.AddExecutor(ExecutorType.Summon, 9742784, new Func<bool>(this.OtherTunerSummonFirst));
			base.AddExecutor(ExecutorType.Summon, 72291078, new Func<bool>(this.OtherTunerSummonFirst));
			base.AddExecutor(ExecutorType.Summon, 77558536);
			base.AddExecutor(ExecutorType.Summon, 25259669);
			base.AddExecutor(ExecutorType.Summon, 19139516);
			base.AddExecutor(ExecutorType.Summon, 67696066);
			base.AddExecutor(ExecutorType.Summon, 87979586);
			base.AddExecutor(ExecutorType.Summon, 44928016);
			base.AddExecutor(ExecutorType.Summon, 53573406);
			base.AddExecutor(ExecutorType.Summon, 12213463);
			base.AddExecutor(ExecutorType.Summon, 26118970);
			base.AddExecutor(ExecutorType.Summon, 9742784);
			base.AddExecutor(ExecutorType.Summon, 72291078);
			base.AddExecutor(ExecutorType.Summon, 4334811);
			base.AddExecutor(ExecutorType.Activate, 26118970);
			base.AddExecutor(ExecutorType.Activate, 77558536);
			base.AddExecutor(ExecutorType.Activate, 67696066, new Func<bool>(this.PerformageTrickClownEffect));
			base.AddExecutor(ExecutorType.Activate, 44928016, new Func<bool>(this.WorldCarrotweightChampionEffect));
			base.AddExecutor(ExecutorType.SpSummon, 27548199, new Func<bool>(this.BorreloadSavageDragonSummon));
			base.AddExecutor(ExecutorType.SpSummon, 76774528, new Func<bool>(this.ScrapDragonSummon));
			base.AddExecutor(ExecutorType.Activate, 76774528, new Func<bool>(this.ScrapDragonEffect));
			base.AddExecutor(ExecutorType.SpSummon, 50954680);
			base.AddExecutor(ExecutorType.SpSummon, 80666118, new Func<bool>(base.DefaultScarlightRedDragonArchfiendSummon));
			base.AddExecutor(ExecutorType.Activate, 80666118, new Func<bool>(base.DefaultScarlightRedDragonArchfiendEffect));
			base.AddExecutor(ExecutorType.SpSummon, 74586817);
			base.AddExecutor(ExecutorType.Activate, 74586817, new Func<bool>(this.PSYFramelordOmegaEffect));
			base.AddExecutor(ExecutorType.SpSummon, 89907227);
			base.AddExecutor(ExecutorType.Activate, 89907227);
			base.AddExecutor(ExecutorType.SpSummon, 53325667);
			base.AddExecutor(ExecutorType.Activate, 53325667);
			base.AddExecutor(ExecutorType.SpSummon, 42566602);
			base.AddExecutor(ExecutorType.Activate, 42566602, new Func<bool>(this.CoralDragonEffect));
			base.AddExecutor(ExecutorType.SpSummon, 68431965, new Func<bool>(this.ShootingRiserDragonSummon));
			base.AddExecutor(ExecutorType.Activate, 68431965, new Func<bool>(this.ShootingRiserDragonEffect));
			base.AddExecutor(ExecutorType.SpSummon, 33698022);
			base.AddExecutor(ExecutorType.SpSummon, 90590303, new Func<bool>(this.Number41BagooskaTheTerriblyTiredTapirSummon));
			base.AddExecutor(ExecutorType.Summon, 82012319, new Func<bool>(this.ScrapGolemSummon));
			base.AddExecutor(ExecutorType.SpellSet, 24224830);
			base.AddExecutor(ExecutorType.SpellSet, 40605147);
			base.AddExecutor(ExecutorType.Activate, 40605147, new Func<bool>(base.DefaultSolemnStrike));
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(this.MonsterRepos));
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x000864C1 File Offset: 0x000846C1
		public override void OnNewTurn()
		{
			this.BeastOLionUsed = false;
			this.JetSynchronUsed = false;
			this.ScrapWyvernUsed = false;
			this.MaskedChameleonUsed = false;
			base.OnNewTurn();
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x000864E5 File Offset: 0x000846E5
		public override void OnChainEnd()
		{
			base.OnChainEnd();
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x000864F0 File Offset: 0x000846F0
		public override CardPosition OnSelectPosition(int cardId, IList<CardPosition> positions)
		{
			NamedCard cardData = NamedCard.Get(cardId);
			if (cardData != null && cardData.Attack <= 1000)
			{
				return CardPosition.FaceUpDefence;
			}
			return (CardPosition)0;
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x00086518 File Offset: 0x00084718
		public override int OnSelectPlace(int cardId, int player, CardLocation location, int available)
		{
			if (location == CardLocation.SpellZone)
			{
				if (cardId == 21887175 || cardId == 47363932 || cardId == 50588353)
				{
					ClientCard b = base.Bot.MonsterZone.GetFirstMatchingCard((ClientCard card) => card.Id == 27548199);
					int zone = (1 << ((b != null) ? b.Sequence : 0)) & available;
					if (zone > 0)
					{
						return zone;
					}
				}
				if ((available & 16) > 0)
				{
					return 16;
				}
				if ((available & 8) > 0)
				{
					return 8;
				}
				if ((available & 4) > 0)
				{
					return 4;
				}
				if ((available & 2) > 0)
				{
					return 2;
				}
				if ((available & 1) > 0)
				{
					return 1;
				}
			}
			if (location == CardLocation.MonsterZone)
			{
				if ((available & 64) > 0)
				{
					return 64;
				}
				if ((available & 32) > 0)
				{
					return 32;
				}
				if ((available & 1) > 0)
				{
					return 1;
				}
				if ((available & 4) > 0)
				{
					return 4;
				}
				if ((available & 16) > 0)
				{
					return 16;
				}
				if ((available & 2) > 0)
				{
					return 2;
				}
				if ((available & 8) > 0)
				{
					return 8;
				}
			}
			return 0;
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x00086608 File Offset: 0x00084808
		public override bool OnPreBattleBetween(ClientCard attacker, ClientCard defender)
		{
			if (!defender.IsMonsterHasPreventActivationEffectInBattle() && !attacker.IsDisabled() && ((attacker.IsCode(21887175) && defender.IsSpecialSummoned) || (attacker.IsCode(50954680) && defender.Level >= 5)))
			{
				attacker.RealPower += defender.Attack;
			}
			return base.OnPreBattleBetween(attacker, defender);
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x0008666B File Offset: 0x0008486B
		private bool UnexpectedDaiFirst()
		{
			return base.Bot.HasInHand(4334811) || base.Bot.HasInHand(new int[] { 44928016, 67696066, 25259669, 12213463 });
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x000866A2 File Offset: 0x000848A2
		private bool PhotonThrasherSummonFirst()
		{
			return base.Bot.HasInHand(4334811) || base.Bot.HasInHand(this.L4Tuners);
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x000866D0 File Offset: 0x000848D0
		private bool ReinforcementofTheArmyEffect()
		{
			if (base.Bot.GetMonsterCount() == 0 && this.PhotonThrasherSummonFirst() && !base.Bot.HasInHand(65367484))
			{
				base.AI.SelectCard(65367484);
				return true;
			}
			if (this.GoblindberghSummonFirst() && !base.Bot.HasInHand(25259669))
			{
				base.AI.SelectCard(25259669);
				return true;
			}
			base.AI.SelectCard(new int[] { 25259669, 77558536, 65367484 });
			return true;
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x00086760 File Offset: 0x00084960
		private bool FoolishBurialEffect()
		{
			if (!base.Bot.HasInHandOrInMonstersZoneOrInGraveyard(4334811))
			{
				base.AI.SelectCard(4334811);
				return true;
			}
			if (this.L4NonTunerSummonFirst() && base.Bot.GetRemainingCount(67696066, 1) > 0)
			{
				base.AI.SelectCard(67696066);
				return true;
			}
			base.AI.SelectCard(new int[] { 9742784, 72291078, 19139516, 65367484 });
			return true;
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x000867DC File Offset: 0x000849DC
		private bool ScrapRecyclerSummonFirst()
		{
			return base.Bot.GetRemainingCount(82012319, 2) > 0 && base.Bot.GetRemainingCount(72291078, 2) > 0 && base.Bot.GetRemainingCount(9742784, 2) > 0;
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x0008681C File Offset: 0x00084A1C
		private bool ScrapRecyclerEffect()
		{
			if ((base.Bot.HasInMonstersZone(82012319, false, false, false) && !this.JetSynchronUsed) || this.BeastOLionUsed)
			{
				base.AI.SelectCard(new int[] { 9742784, 72291078 });
			}
			else
			{
				base.AI.SelectCard(new int[] { 72291078, 9742784 });
			}
			return true;
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x00086893 File Offset: 0x00084A93
		private bool MechaPhantomBeastOLionEffect()
		{
			if (base.ActivateDescription == -1)
			{
				this.BeastOLionUsed = true;
				return true;
			}
			return !this.BeastOLionUsed;
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x000868B0 File Offset: 0x00084AB0
		private bool ScrapWyvernSummon()
		{
			if (this.ScrapWyvernUsed || this.MaskedChameleonUsed || base.Bot.HasInMonstersZone(47363932, false, false, false))
			{
				return false;
			}
			if (!base.Bot.HasInMonstersZone(new int[] { 19139516, 82012319, 4334811 }, false, false, false) || !base.Bot.HasInMonstersZoneOrInGraveyard(4334811))
			{
				return false;
			}
			base.AI.SelectMaterials(new int[] { 72291079, 65367484, 25259669, 87979586, 67696066, 44928016, 12213463, 19139516, 82012319, 4334811 }, 0);
			return true;
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x00086938 File Offset: 0x00084B38
		private bool ScrapWyvernEffect()
		{
			if (base.ActivateDescription != -1)
			{
				int[] targets = new int[] { 4334811, 19139516, 82012319, 76774528 };
				base.AI.SelectCard(targets);
				base.AI.SelectNextCard(targets);
				this.ScrapWyvernUsed = true;
				return true;
			}
			base.AI.SelectCard(new int[] { 82012319, 19139516 });
			ClientCard target = base.Util.GetBestEnemyCard(false, false);
			if (target != null)
			{
				base.AI.SelectNextCard(target);
			}
			else
			{
				base.AI.SelectNextCard(new int[] { 24224830, 65367484, 67696066, 72291079, 44928016, 12213463, 25259669, 87979586, 47363932 });
			}
			return true;
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x000869DF File Offset: 0x00084BDF
		private bool ScrapGolemEffect()
		{
			if (base.Bot.GetMonstersInMainZone().Count == 5)
			{
				return false;
			}
			base.AI.SelectCard(4334811);
			base.AI.SelectOption(0);
			return true;
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x00086A14 File Offset: 0x00084C14
		private bool JetSynchronEffect()
		{
			if (!base.Bot.HasInMonstersZone(33698022, false, false, false))
			{
				if (base.Bot.MonsterZone.GetMatchingCardsCount((ClientCard card) => card.IsFaceup() && card.Level >= 2 && card.Level <= 5) < 2)
				{
					return false;
				}
			}
			base.AI.SelectCard(this.HandCosts);
			return true;
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x00086A7C File Offset: 0x00084C7C
		private bool CrystronNeedlefiberSummon()
		{
			if (this.MaskedChameleonUsed)
			{
				return false;
			}
			int nonTunerCount = base.Bot.MonsterZone.GetMatchingCardsCount((ClientCard card) => card.IsFaceup() && !card.IsTuner());
			if (base.Bot.GetMonsterCount() < 3 || nonTunerCount == 0)
			{
				return false;
			}
			if (nonTunerCount == 1)
			{
				base.AI.SelectMaterials(new int[]
				{
					9742784, 72291078, 87979586, 77558536, 19139516, 53573406, 67696066, 72291079, 4334811, 12213463,
					65367484, 25259669, 44928016
				}, 0);
			}
			else
			{
				base.AI.SelectMaterials(new int[]
				{
					72291079, 4334811, 12213463, 65367484, 25259669, 44928016, 67696066, 9742784, 72291078, 87979586,
					77558536, 19139516, 53573406
				}, 0);
			}
			return true;
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x00086B1C File Offset: 0x00084D1C
		private bool CrystronNeedlefiberEffect()
		{
			if (base.Duel.Player == 0)
			{
				base.AI.SelectCard(26118970);
				return true;
			}
			if (base.Bot.HasInExtra(68431965))
			{
				if (base.Bot.MonsterZone.IsExistingMatchingCard((ClientCard card) => card.Level >= 3 && card.Level <= 5 && card.IsFaceup() && !card.IsTuner(), 1))
				{
					base.AI.SelectCard(68431965);
					return true;
				}
			}
			if (base.Util.IsOneEnemyBetterThanValue(1500, true) || base.DefaultOnBecomeTarget())
			{
				base.AI.SelectCard(42566602);
				return true;
			}
			return false;
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x00086BCC File Offset: 0x00084DCC
		private bool MekkKnightCrusadiaAstramSummon()
		{
			int[] matcodes = new int[] { 47363932, 50588353 };
			if (base.Bot.MonsterZone.GetMatchingCardsCount((ClientCard card) => card.IsCode(matcodes)) < 2)
			{
				return false;
			}
			base.AI.SelectMaterials(matcodes, 0);
			return true;
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x00086C30 File Offset: 0x00084E30
		private bool MekkKnightCrusadiaAstramEffect()
		{
			if (base.Card.Location == CardLocation.MonsterZone)
			{
				return true;
			}
			ClientCard target = base.Util.GetBestEnemyCard(false, false);
			if (target == null)
			{
				return false;
			}
			base.AI.SelectCard(target);
			return true;
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x00086C6D File Offset: 0x00084E6D
		private bool GoblindberghSummonFirst()
		{
			return base.Bot.HasInHand(this.L4Tuners);
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x00086C85 File Offset: 0x00084E85
		private bool GoblindberghEffect()
		{
			base.AI.SelectCard(this.L4Tuners);
			return true;
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x00086C99 File Offset: 0x00084E99
		private bool MaskedChameleonSummonFirst()
		{
			return base.Bot.HasInGraveyard(new int[] { 65367484, 44928016, 25259669 });
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x00086CBC File Offset: 0x00084EBC
		private bool MaskedChameleonEffect()
		{
			if (base.Bot.MonsterZone.GetMatchingCardsCount((ClientCard card) => card.IsFaceup() && !card.IsTuner()) == 0)
			{
				this.MaskedChameleonUsed = true;
				base.AI.SelectCard(this.L4NonTuners);
				return true;
			}
			return false;
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x00086D15 File Offset: 0x00084F15
		private bool WhiteRoseDragonSummonFirst()
		{
			return base.Bot.HasInGraveyard(new int[] { 26118970 });
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x00086D35 File Offset: 0x00084F35
		private bool WhiteRoseDragonEffect()
		{
			if (base.Card.Location != CardLocation.Grave)
			{
				return true;
			}
			if (base.Bot.GetRemainingCount(44928016, 1) > 0)
			{
				base.AI.SelectCard(44928016);
				return true;
			}
			return false;
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x00086D6F File Offset: 0x00084F6F
		private bool L4TunerSummonFirst()
		{
			return base.Bot.HasInMonstersZone(this.L4NonTuners, false, false, true);
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x00086D85 File Offset: 0x00084F85
		private bool L4NonTunerSummonFirst()
		{
			return base.Bot.HasInMonstersZone(this.L4Tuners, false, false, true);
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x00086D6F File Offset: 0x00084F6F
		private bool OtherTunerSummonFirst()
		{
			return base.Bot.HasInMonstersZone(this.L4NonTuners, false, false, true);
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x00086D9B File Offset: 0x00084F9B
		private bool PerformageTrickClownEffect()
		{
			if (base.Bot.LifePoints <= 1000)
			{
				return false;
			}
			base.AI.SelectPosition(CardPosition.FaceUpDefence);
			return true;
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x00086DBE File Offset: 0x00084FBE
		private bool WorldCarrotweightChampionEffect()
		{
			return !base.Bot.HasInMonstersZone(this.L4NonTuners, false, false, false);
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x00086DD7 File Offset: 0x00084FD7
		private bool ScrapGolemSummon()
		{
			return base.Bot.GetMonsterCount() <= 2 && base.Bot.HasInMonstersZoneOrInGraveyard(4334811);
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x00086DF9 File Offset: 0x00084FF9
		private bool BorreloadSavageDragonSummon()
		{
			return base.Bot.HasInGraveyard(new int[] { 47363932, 50588353, 21887175 });
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x00086E1C File Offset: 0x0008501C
		private bool BorreloadSavageDragonEffect()
		{
			if (base.ActivateDescription == -1)
			{
				base.AI.SelectCard(new int[] { 21887175, 50588353, 47363932 });
				return true;
			}
			return true;
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x00086E46 File Offset: 0x00085046
		private bool ScrapDragonSummon()
		{
			return base.Util.GetProblematicEnemyCard(3000, false) != null || base.Bot.HasInGraveyard(new int[] { 19139516, 4334811, 82012319, 47363932 });
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x00086E80 File Offset: 0x00085080
		private bool ScrapDragonEffect()
		{
			ClientCard invincible = base.Util.GetProblematicEnemyCard(3000, false);
			if (invincible == null && !base.Util.IsOneEnemyBetterThanValue(2799, false))
			{
				return false;
			}
			List<ClientCard> monsters = base.Enemy.GetMonsters();
			monsters.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			ClientCard destroyCard = invincible;
			if (destroyCard == null)
			{
				for (int i = monsters.Count - 1; i >= 0; i--)
				{
					if (monsters[i].IsAttack())
					{
						destroyCard = monsters[i];
						break;
					}
				}
			}
			if (destroyCard == null)
			{
				return false;
			}
			base.AI.SelectCard(new int[]
			{
				24224830, 72291079, 4334811, 12213463, 65367484, 25259669, 44928016, 67696066, 9742784, 72291078,
				87979586, 77558536, 19139516, 53573406
			});
			base.AI.SelectNextCard(destroyCard);
			return true;
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x000675A8 File Offset: 0x000657A8
		private bool CrystalWingSynchroDragonEffect()
		{
			return base.Duel.LastChainPlayer != 0;
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x00086F34 File Offset: 0x00085134
		private bool PSYFramelordOmegaEffect()
		{
			if (base.Card.Location == CardLocation.Grave)
			{
				return false;
			}
			if (base.Duel.Player == 0)
			{
				return base.DefaultOnBecomeTarget();
			}
			if (base.Duel.Player == 1)
			{
				if (base.Duel.Phase != DuelPhase.Standby)
				{
					return base.Enemy.MonsterZone.GetMatchingCards((ClientCard card) => card.IsAttack()).Sum((ClientCard card) => card.Attack) < base.Bot.LifePoints;
				}
				if (base.Bot.HasInBanished(9742784) && !base.Bot.HasInGraveyard(9742784))
				{
					base.AI.SelectCard(9742784);
					return true;
				}
				if (base.Bot.HasInBanished(50588353))
				{
					base.AI.SelectCard(50588353);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x00087044 File Offset: 0x00085244
		private bool CoralDragonEffect()
		{
			if (base.Card.Location == CardLocation.Grave)
			{
				return true;
			}
			ClientCard target = base.Util.GetProblematicEnemyCard(0, true);
			if (target != null)
			{
				base.AI.SelectCard(this.HandCosts);
				base.AI.SelectNextCard(target);
				return true;
			}
			return false;
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x00087093 File Offset: 0x00085293
		private bool ShootingRiserDragonSummon()
		{
			return base.Bot.MonsterZone.GetMatchingCardsCount((ClientCard card) => card.IsFaceup() && !card.IsTuner()) >= 2;
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x000870CC File Offset: 0x000852CC
		private bool ShootingRiserDragonEffect()
		{
			if (base.ActivateDescription == -1 || base.ActivateDescription == base.Util.GetStringId(68431965, 0))
			{
				int targetLevel = 8;
				if (base.Bot.MonsterZone.IsExistingMatchingCard((ClientCard card) => card.Level == targetLevel - 5 && card.IsFaceup() && !card.IsTuner(), 1) && base.Bot.GetRemainingCount(72291078, 2) > 0)
				{
					base.AI.SelectCard(72291078);
				}
				else if (base.Bot.MonsterZone.IsExistingMatchingCard((ClientCard card) => card.Level == targetLevel - 4 && card.IsFaceup() && !card.IsTuner(), 1))
				{
					base.AI.SelectCard(new int[] { 4334811, 26118970 });
				}
				else if (base.Bot.MonsterZone.IsExistingMatchingCard((ClientCard card) => card.Level == targetLevel - 3 && card.IsFaceup() && !card.IsTuner(), 1))
				{
					base.AI.SelectCard(new int[] { 19139516, 65367484, 25259669, 44928016, 12213463, 77558536, 87979586, 67696066, 53573406 });
				}
				else
				{
					this.FoolishBurialEffect();
				}
				return true;
			}
			if (base.Duel.LastChainPlayer == 0)
			{
				return false;
			}
			base.AI.SelectCard(new int[] { 33698022, 76774528, 74586817 });
			return true;
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x00087208 File Offset: 0x00085408
		private bool Number41BagooskaTheTerriblyTiredTapirSummon()
		{
			if (!base.Util.IsTurn1OrMain2())
			{
				return false;
			}
			if (base.Bot.GetMonsterCount() > 3)
			{
				return false;
			}
			base.AI.SelectPosition(CardPosition.FaceUpDefence);
			return true;
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x00087238 File Offset: 0x00085438
		private bool MonsterRepos()
		{
			if (base.Card.IsFacedown())
			{
				return true;
			}
			if (base.Card.IsCode(90590303) && base.Card.IsDefense())
			{
				return base.Card.Overlays.Count == 0;
			}
			return base.DefaultMonsterRepos();
		}

		// Token: 0x04001AAA RID: 6826
		private bool BeastOLionUsed;

		// Token: 0x04001AAB RID: 6827
		private bool JetSynchronUsed;

		// Token: 0x04001AAC RID: 6828
		private bool ScrapWyvernUsed;

		// Token: 0x04001AAD RID: 6829
		private bool MaskedChameleonUsed;

		// Token: 0x04001AAE RID: 6830
		private int[] HandCosts = new int[] { 67696066, 9742784, 72291078, 82012319, 44928016 };

		// Token: 0x04001AAF RID: 6831
		private int[] L4NonTuners = new int[] { 65367484, 44928016, 67696066, 25259669, 12213463 };

		// Token: 0x04001AB0 RID: 6832
		private int[] L4Tuners = new int[] { 77558536, 19139516, 87979586, 53573406 };

		// Token: 0x0200034C RID: 844
		public class CardId
		{
			// Token: 0x04001AB1 RID: 6833
			public const int AngelTrumpeter = 87979586;

			// Token: 0x04001AB2 RID: 6834
			public const int ScrapGolem = 82012319;

			// Token: 0x04001AB3 RID: 6835
			public const int PhotonThrasher = 65367484;

			// Token: 0x04001AB4 RID: 6836
			public const int WorldCarrotweightChampion = 44928016;

			// Token: 0x04001AB5 RID: 6837
			public const int RaidenHandofTheLightsworn = 77558536;

			// Token: 0x04001AB6 RID: 6838
			public const int ScrapBeast = 19139516;

			// Token: 0x04001AB7 RID: 6839
			public const int PerformageTrickClown = 67696066;

			// Token: 0x04001AB8 RID: 6840
			public const int MaskedChameleon = 53573406;

			// Token: 0x04001AB9 RID: 6841
			public const int Goblindbergh = 25259669;

			// Token: 0x04001ABA RID: 6842
			public const int WhiteRoseDragon = 12213463;

			// Token: 0x04001ABB RID: 6843
			public const int RedRoseDragon = 26118970;

			// Token: 0x04001ABC RID: 6844
			public const int ScrapRecycler = 4334811;

			// Token: 0x04001ABD RID: 6845
			public const int MechaPhantomBeastOLion = 72291078;

			// Token: 0x04001ABE RID: 6846
			public const int MechaPhantomBeastOLionToken = 72291079;

			// Token: 0x04001ABF RID: 6847
			public const int JetSynchron = 9742784;

			// Token: 0x04001AC0 RID: 6848
			public const int UnexpectedDai = 911883;

			// Token: 0x04001AC1 RID: 6849
			public const int Raigeki = 12580477;

			// Token: 0x04001AC2 RID: 6850
			public const int HarpiesFeatherDuster = 18144506;

			// Token: 0x04001AC3 RID: 6851
			public const int ReinforcementofTheArmy = 32807846;

			// Token: 0x04001AC4 RID: 6852
			public const int FoolishBurial = 81439173;

			// Token: 0x04001AC5 RID: 6853
			public const int MonsterReborn = 83764718;

			// Token: 0x04001AC6 RID: 6854
			public const int ChargeofTheLightBrigade = 94886282;

			// Token: 0x04001AC7 RID: 6855
			public const int CalledbyTheGrave = 24224830;

			// Token: 0x04001AC8 RID: 6856
			public const int SolemnStrike = 40605147;

			// Token: 0x04001AC9 RID: 6857
			public const int WhiteAuraBihamut = 89907227;

			// Token: 0x04001ACA RID: 6858
			public const int BorreloadSavageDragon = 27548199;

			// Token: 0x04001ACB RID: 6859
			public const int CrystalWingSynchroDragon = 50954680;

			// Token: 0x04001ACC RID: 6860
			public const int ScarlightRedDragonArchfiend = 80666118;

			// Token: 0x04001ACD RID: 6861
			public const int PSYFramelordOmega = 74586817;

			// Token: 0x04001ACE RID: 6862
			public const int ScrapDragon = 76774528;

			// Token: 0x04001ACF RID: 6863
			public const int BlackRoseMoonlightDragon = 33698022;

			// Token: 0x04001AD0 RID: 6864
			public const int ShootingRiserDragon = 68431965;

			// Token: 0x04001AD1 RID: 6865
			public const int CoralDragon = 42566602;

			// Token: 0x04001AD2 RID: 6866
			public const int GardenRoseMaiden = 53325667;

			// Token: 0x04001AD3 RID: 6867
			public const int Number41BagooskaTheTerriblyTiredTapir = 90590303;

			// Token: 0x04001AD4 RID: 6868
			public const int MekkKnightCrusadiaAstram = 21887175;

			// Token: 0x04001AD5 RID: 6869
			public const int ScrapWyvern = 47363932;

			// Token: 0x04001AD6 RID: 6870
			public const int CrystronNeedlefiber = 50588353;
		}
	}
}
