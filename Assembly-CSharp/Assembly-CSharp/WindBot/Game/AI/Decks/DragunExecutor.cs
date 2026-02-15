using System;
using System.Collections.Generic;
using YGOSharp.OCGWrapper;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x020002EF RID: 751
	[Deck("Dragun", "AI_Dragun", "Normal")]
	internal class DragunExecutor : DefaultExecutor
	{
		// Token: 0x060012E4 RID: 4836 RVA: 0x00065B58 File Offset: 0x00063D58
		public DragunExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.Activate, 14558127, new Func<bool>(base.DefaultAshBlossomAndJoyousSpring));
			base.AddExecutor(ExecutorType.Activate, 24224830, new Func<bool>(base.DefaultCalledByTheGrave));
			base.AddExecutor(ExecutorType.Activate, 10045474, new Func<bool>(base.DefaultInfiniteImpermanence));
			base.AddExecutor(ExecutorType.Activate, 40605147, new Func<bool>(base.DefaultSolemnStrike));
			base.AddExecutor(ExecutorType.Activate, 37818794, new Func<bool>(this.DragunofRedEyesCounter));
			base.AddExecutor(ExecutorType.Activate, 23434538, new Func<bool>(base.DefaultMaxxC));
			base.AddExecutor(ExecutorType.Activate, 18144506);
			base.AddExecutor(ExecutorType.Activate, 37818794, new Func<bool>(this.DragunofRedEyesDestroy));
			base.AddExecutor(ExecutorType.Activate, 63519819, new Func<bool>(this.ThousandEyesRestrictEffect));
			base.AddExecutor(ExecutorType.Activate, 92353449, new Func<bool>(this.RedEyesInsightEffect));
			base.AddExecutor(ExecutorType.Activate, 6172122, new Func<bool>(this.RedEyesFusionEffect));
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(this.MonsterRepos));
			base.AddExecutor(ExecutorType.Summon, 10802915, new Func<bool>(this.TourGuideFromTheUnderworldSummon));
			base.AddExecutor(ExecutorType.Activate, 10802915, new Func<bool>(this.TourGuideFromTheUnderworldEffect));
			base.AddExecutor(ExecutorType.Summon, 26202165, new Func<bool>(this.SanganSummon));
			base.AddExecutor(ExecutorType.Activate, 26202165, new Func<bool>(this.SanganEffect));
			base.AddExecutor(ExecutorType.Summon, 72291078);
			base.AddExecutor(ExecutorType.Activate, 72291078, new Func<bool>(this.MechaPhantomBeastOLionEffect));
			base.AddExecutor(ExecutorType.SpSummon, 60303245, new Func<bool>(this.SalamangreatAlmirajSummon));
			base.AddExecutor(ExecutorType.SpSummon, 31226177, new Func<bool>(this.ImdukTheWorldChaliceDragonSummon));
			base.AddExecutor(ExecutorType.SpSummon, 98978921, new Func<bool>(this.LinkSpiderSummon));
			base.AddExecutor(ExecutorType.SpSummon, 91646304);
			base.AddExecutor(ExecutorType.Activate, 1845204, new Func<bool>(this.InstantFusionEffect));
			base.AddExecutor(ExecutorType.Summon, 67300516);
			base.AddExecutor(ExecutorType.Summon, 91646304, new Func<bool>(this.SummonForMaterial));
			base.AddExecutor(ExecutorType.Summon, 14558127, new Func<bool>(this.SummonForMaterial));
			base.AddExecutor(ExecutorType.Summon, 23434538, new Func<bool>(this.SummonForMaterial));
			base.AddExecutor(ExecutorType.Activate, 81439173, new Func<bool>(this.FoolishBurialEffect));
			base.AddExecutor(ExecutorType.Activate, 83764718, new Func<bool>(this.MonsterRebornEffect));
			base.AddExecutor(ExecutorType.Activate, 97631303, new Func<bool>(this.MagiciansSoulsEffect));
			base.AddExecutor(ExecutorType.Summon, 97631303, new Func<bool>(this.SummonForMaterial));
			base.AddExecutor(ExecutorType.SpSummon, 50588353, new Func<bool>(this.CrystronNeedlefiberSummon));
			base.AddExecutor(ExecutorType.Activate, 50588353, new Func<bool>(this.CrystronNeedlefiberEffect));
			base.AddExecutor(ExecutorType.SpSummon, 70369116, new Func<bool>(this.PredaplantVerteAnacondaSummon));
			base.AddExecutor(ExecutorType.Activate, 11827244, new Func<bool>(this.MagicalizedFusionEffect));
			base.AddExecutor(ExecutorType.Activate, 70369116, new Func<bool>(this.PredaplantVerteAnacondaEffect));
			base.AddExecutor(ExecutorType.SpellSet, 10045474, new Func<bool>(this.TrapSet));
			base.AddExecutor(ExecutorType.SpellSet, 40605147, new Func<bool>(this.TrapSet));
			base.AddExecutor(ExecutorType.MonsterSet, 26202165);
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x0000763C File Offset: 0x0000583C
		public override bool OnSelectHand()
		{
			return true;
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x00065ED4 File Offset: 0x000640D4
		public override void OnNewTurn()
		{
			this.BeastOLionUsed = false;
			this.RedEyesFusionUsed = false;
			base.OnNewTurn();
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x00065EEC File Offset: 0x000640EC
		public override CardPosition OnSelectPosition(int cardId, IList<CardPosition> positions)
		{
			NamedCard cardData = NamedCard.Get(cardId);
			if (cardData != null && cardData.Attack <= 1000)
			{
				return CardPosition.FaceUpDefence;
			}
			return (CardPosition)0;
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x00065F13 File Offset: 0x00064113
		public override int OnSelectPlace(int cardId, int player, CardLocation location, int available)
		{
			if (location == CardLocation.MonsterZone)
			{
				return available & ~base.Bot.GetLinkedZones();
			}
			return 0;
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x00065F2C File Offset: 0x0006412C
		private bool DragunofRedEyesCounter()
		{
			if (base.ActivateDescription != -1 && base.ActivateDescription != base.Util.GetStringId(37818794, 1))
			{
				return false;
			}
			if (base.Duel.LastChainPlayer != 1)
			{
				return false;
			}
			base.AI.SelectCard(new int[] { 67300516, 72291078 });
			return true;
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x00065F8F File Offset: 0x0006418F
		private bool DragunofRedEyesDestroy()
		{
			if (base.ActivateDescription == -1 || base.ActivateDescription == base.Util.GetStringId(37818794, 1))
			{
				return false;
			}
			base.AI.SelectCard(base.Util.GetBestEnemyMonster(false, false));
			return true;
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x00065FCE File Offset: 0x000641CE
		private bool ThousandEyesRestrictEffect()
		{
			base.AI.SelectCard(base.Util.GetBestEnemyMonster(false, false));
			return true;
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x00065FEC File Offset: 0x000641EC
		private bool RedEyesInsightEffect()
		{
			if (base.Bot.HasInHand(6172122))
			{
				return false;
			}
			if (base.Bot.GetRemainingCount(67300516, 1) == 0 && base.Bot.GetRemainingCount(74677422, 2) == 1 && !base.Bot.HasInHand(74677422))
			{
				return false;
			}
			base.AI.SelectCard(67300516);
			return true;
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x0006605C File Offset: 0x0006425C
		private bool RedEyesFusionEffect()
		{
			if (base.Bot.HasInMonstersZone(new int[] { 37818794, 74677422 }, false, false, false))
			{
				if (base.Util.GetBotAvailZonesFromExtraDeck() == 0)
				{
					return false;
				}
				if (base.Bot.GetRemainingCount(74677422, 2) == 0 && !base.Bot.HasInHand(74677422))
				{
					return false;
				}
			}
			base.AI.SelectMaterials(CardLocation.Deck, 0);
			this.RedEyesFusionUsed = true;
			return true;
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x000660DA File Offset: 0x000642DA
		private bool TourGuideFromTheUnderworldSummon()
		{
			return !base.DefaultCheckWhetherCardIsNegated(base.Card) && (base.Bot.GetRemainingCount(10802915, 2) != 0 || base.Bot.GetRemainingCount(26202165, 2) != 0);
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x00066115 File Offset: 0x00064315
		private bool TourGuideFromTheUnderworldEffect()
		{
			base.AI.SelectCard(26202165);
			return true;
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x0000763C File Offset: 0x0000583C
		private bool SanganSummon()
		{
			return true;
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x00066128 File Offset: 0x00064328
		private bool SanganEffect()
		{
			if (base.Bot.HasInMonstersZone(60303245, false, false, false) && !base.Bot.HasInHand(91646304))
			{
				base.AI.SelectCard(91646304);
			}
			else if (!base.Bot.HasInHand(23434538))
			{
				base.AI.SelectCard(23434538);
			}
			else if (!base.Bot.HasInHand(14558127))
			{
				base.AI.SelectCard(14558127);
			}
			else if (!base.Bot.HasInHand(97631303))
			{
				base.AI.SelectCard(97631303);
			}
			else if (!base.Bot.HasInHand(91646304))
			{
				base.AI.SelectCard(91646304);
			}
			else
			{
				base.AI.SelectCard(new int[] { 14558127, 23434538, 91646304 });
			}
			return true;
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x00066224 File Offset: 0x00064424
		private bool SalamangreatAlmirajSummon()
		{
			int[] materials = new int[] { 26202165, 72291078 };
			if (base.Bot.MonsterZone.GetMatchingCardsCount((ClientCard card) => card.IsCode(materials) && !card.IsSpecialSummoned) == 0)
			{
				return false;
			}
			base.AI.SelectMaterials(materials, 0);
			return true;
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x00066288 File Offset: 0x00064488
		private bool ImdukTheWorldChaliceDragonSummon()
		{
			if (base.Bot.HasInMonstersZone(70369116, true, false, false) || !base.Bot.HasInExtra(70369116))
			{
				return false;
			}
			if (base.Bot.Graveyard.GetMatchingCardsCount((ClientCard card) => (card.Race & 8192) > 0) >= 0)
			{
				return false;
			}
			if (base.Bot.GetMonsterCount() == 1)
			{
				if (base.Bot.Hand.GetMatchingCardsCount((ClientCard card) => card.Level <= 4) == 0 && !base.Util.IsTurn1OrMain2())
				{
					return false;
				}
			}
			if (base.Bot.GetMonsterCount() >= 2)
			{
				if (base.Bot.MonsterZone.GetMatchingCardsCount((ClientCard card) => card.Level >= 8) > 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x00066383 File Offset: 0x00064583
		private bool LinkSpiderSummon()
		{
			if (!base.Bot.HasInMonstersZone(72291079, false, false, false))
			{
				return false;
			}
			base.AI.SelectMaterials(72291079, 0);
			return true;
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x000663B0 File Offset: 0x000645B0
		private bool NeedMonster()
		{
			if (base.Bot.HasInMonstersZone(70369116, true, false, false) || !base.Bot.HasInExtra(70369116))
			{
				return false;
			}
			if (base.Bot.MonsterZone.GetMatchingCardsCount((ClientCard card) => card.Level >= 8) > 0)
			{
				return false;
			}
			if (base.Bot.GetMonsterCount() == 0)
			{
				if (base.Bot.Hand.GetMatchingCardsCount((ClientCard card) => card.Level <= 4) == 0)
				{
					return false;
				}
			}
			return base.Bot.GetMonsterCount() < 2;
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x0006646B File Offset: 0x0006466B
		private bool InstantFusionEffect()
		{
			if (!this.NeedMonster())
			{
				return false;
			}
			if (base.Enemy.GetMonsterCount() > 0)
			{
				base.AI.SelectCard(63519819);
			}
			else
			{
				base.AI.SelectCard(96334243);
			}
			return true;
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x000664A8 File Offset: 0x000646A8
		private bool SummonForMaterial()
		{
			if (base.Bot.HasInMonstersZone(70369116, true, false, false) || !base.Bot.HasInExtra(70369116))
			{
				return false;
			}
			return base.Bot.MonsterZone.GetMatchingCardsCount((ClientCard card) => (card.HasType(CardType.Effect) || card.IsTuner()) && card.Level < 8) == 1 || base.Bot.HasInHand(97631303);
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x00066528 File Offset: 0x00064728
		private bool MagiciansSoulsEffect()
		{
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			if (base.Card.Location == CardLocation.Hand)
			{
				if (this.RedEyesFusionUsed)
				{
					return false;
				}
				if (base.Bot.GetMonsterCount() >= 2)
				{
					return false;
				}
				base.AI.SelectOption(1);
				base.AI.SelectYesNo(true);
				return true;
			}
			else
			{
				int[] costs = new int[] { 92353449, 6172122 };
				if (base.Bot.HasInHand(costs))
				{
					base.AI.SelectCard(costs);
					return true;
				}
				return false;
			}
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x000665BC File Offset: 0x000647BC
		private bool PredaplantVerteAnacondaSummon()
		{
			if (base.Bot.HasInMonstersZone(70369116, true, false, false))
			{
				return false;
			}
			int[] materials = new int[]
			{
				31226177, 26202165, 10802915, 91646304, 72291078, 97631303, 60303245, 98978921, 63519819, 14558127,
				23434538, 67300516, 50588353
			};
			if (base.Bot.MonsterZone.GetMatchingCardsCount((ClientCard card) => card.IsCode(materials)) >= 2)
			{
				base.AI.SelectMaterials(materials, 0);
				return true;
			}
			return false;
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x00066634 File Offset: 0x00064834
		private bool MagicalizedFusionEffect()
		{
			if (base.Bot.HasInMonstersZone(new int[] { 37818794, 74677422 }, false, false, false))
			{
				if (base.Util.GetBotAvailZonesFromExtraDeck() == 0)
				{
					return false;
				}
				if (base.Bot.Graveyard.GetMatchingCardsCount((ClientCard card) => (card.Race & 8192) > 0) == 0)
				{
					return false;
				}
			}
			base.AI.SelectMaterials(CardLocation.Grave, 0);
			return true;
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x000666B8 File Offset: 0x000648B8
		private bool PredaplantVerteAnacondaEffect()
		{
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			if (base.ActivateDescription == base.Util.GetStringId(70369116, 0))
			{
				return false;
			}
			base.AI.SelectCard(6172122);
			base.AI.SelectMaterials(CardLocation.Deck, 0);
			return true;
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x00066710 File Offset: 0x00064910
		private bool FoolishBurialEffect()
		{
			if (this.RedEyesFusionUsed)
			{
				return false;
			}
			if (base.Bot.HasInHand(11827244))
			{
				if (base.Bot.HasInGraveyard(46986414))
				{
					if (base.Bot.Graveyard.GetMatchingCardsCount((ClientCard card) => (card.Race & 8192) > 0) == 0)
					{
						base.AI.SelectCard(new int[] { 67300516, 74677422 });
						return true;
					}
				}
				if (!base.Bot.HasInGraveyard(46986414))
				{
					if (base.Bot.Graveyard.GetMatchingCardsCount((ClientCard card) => (card.Race & 8192) > 0) > 0)
					{
						base.AI.SelectCard(46986414);
						return true;
					}
				}
			}
			if (!this.NeedMonster())
			{
				return false;
			}
			base.AI.SelectCard(new int[] { 72291078 });
			return true;
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x0006681C File Offset: 0x00064A1C
		private bool MonsterRebornEffect()
		{
			if (base.Bot.HasInGraveyard(37818794))
			{
				base.AI.SelectCard(37818794);
				return true;
			}
			if (!this.NeedMonster())
			{
				return false;
			}
			base.AI.SelectCard(new int[] { 70369116, 26202165, 63519819, 72291078, 91646304, 14558127 });
			return true;
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x00066874 File Offset: 0x00064A74
		private bool MechaPhantomBeastOLionEffect()
		{
			if (base.ActivateDescription == -1)
			{
				this.BeastOLionUsed = true;
				return true;
			}
			return !this.BeastOLionUsed;
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x00066894 File Offset: 0x00064A94
		private bool CrystronNeedlefiberSummon()
		{
			if (base.Bot.HasInMonstersZone(70369116, true, false, false))
			{
				return false;
			}
			int[] materials = new int[]
			{
				91646304, 72291078, 14558127, 96334243, 72291079, 46986414, 31226177, 26202165, 10802915, 97631303,
				60303245, 98978921, 63519819, 96334243, 23434538, 67300516
			};
			if (base.Bot.MonsterZone.GetMatchingCardsCount((ClientCard card) => card.IsCode(materials)) >= 2)
			{
				base.AI.SelectMaterials(materials, 0);
				return true;
			}
			return false;
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x0006690A File Offset: 0x00064B0A
		private bool CrystronNeedlefiberEffect()
		{
			if (base.Duel.Player == 0)
			{
				base.AI.SelectCard(72291078);
				return true;
			}
			return true;
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x0006692C File Offset: 0x00064B2C
		private bool TrapSet()
		{
			if (base.Bot.HasInMonstersZone(new int[] { 37818794, 74677422 }, false, false, false) && base.Bot.GetHandCount() == 1)
			{
				return false;
			}
			base.AI.SelectPlace(27);
			return true;
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x00057C2C File Offset: 0x00055E2C
		private bool MonsterRepos()
		{
			return base.Card.IsFacedown() || base.DefaultMonsterRepos();
		}

		// Token: 0x04001780 RID: 6016
		private bool BeastOLionUsed;

		// Token: 0x04001781 RID: 6017
		private bool RedEyesFusionUsed;

		// Token: 0x020002F0 RID: 752
		public class CardId
		{
			// Token: 0x04001782 RID: 6018
			public const int DarkMagician = 46986414;

			// Token: 0x04001783 RID: 6019
			public const int RedEyesBDragon = 74677422;

			// Token: 0x04001784 RID: 6020
			public const int RedEyesWyvern = 67300516;

			// Token: 0x04001785 RID: 6021
			public const int TourGuideFromTheUnderworld = 10802915;

			// Token: 0x04001786 RID: 6022
			public const int Sangan = 26202165;

			// Token: 0x04001787 RID: 6023
			public const int CrusadiaArboria = 91646304;

			// Token: 0x04001788 RID: 6024
			public const int AshBlossomJoyousSpring = 14558127;

			// Token: 0x04001789 RID: 6025
			public const int MechaPhantomBeastOLion = 72291078;

			// Token: 0x0400178A RID: 6026
			public const int MechaPhantomBeastOLionToken = 72291079;

			// Token: 0x0400178B RID: 6027
			public const int MaxxC = 23434538;

			// Token: 0x0400178C RID: 6028
			public const int MagiciansSouls = 97631303;

			// Token: 0x0400178D RID: 6029
			public const int InstantFusion = 1845204;

			// Token: 0x0400178E RID: 6030
			public const int RedEyesFusion = 6172122;

			// Token: 0x0400178F RID: 6031
			public const int MagicalizedFusion = 11827244;

			// Token: 0x04001790 RID: 6032
			public const int HarpiesFeatherDuster = 18144506;

			// Token: 0x04001791 RID: 6033
			public const int FoolishBurial = 81439173;

			// Token: 0x04001792 RID: 6034
			public const int MonsterReborn = 83764718;

			// Token: 0x04001793 RID: 6035
			public const int RedEyesInsight = 92353449;

			// Token: 0x04001794 RID: 6036
			public const int CalledbyTheGrave = 24224830;

			// Token: 0x04001795 RID: 6037
			public const int InfiniteImpermanence = 10045474;

			// Token: 0x04001796 RID: 6038
			public const int SolemnStrike = 40605147;

			// Token: 0x04001797 RID: 6039
			public const int DragunofRedEyes = 37818794;

			// Token: 0x04001798 RID: 6040
			public const int SeaMonsterofTheseus = 96334243;

			// Token: 0x04001799 RID: 6041
			public const int ThousandEyesRestrict = 63519819;

			// Token: 0x0400179A RID: 6042
			public const int CrystronHalqifibrax = 50588353;

			// Token: 0x0400179B RID: 6043
			public const int PredaplantVerteAnaconda = 70369116;

			// Token: 0x0400179C RID: 6044
			public const int LinkSpider = 98978921;

			// Token: 0x0400179D RID: 6045
			public const int ImdukTheWorldChaliceDragon = 31226177;

			// Token: 0x0400179E RID: 6046
			public const int SalamangreatAlmiraj = 60303245;
		}
	}
}
