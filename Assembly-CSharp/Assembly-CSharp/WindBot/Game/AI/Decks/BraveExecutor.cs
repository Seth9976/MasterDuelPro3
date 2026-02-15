using System;
using System.Collections.Generic;
using System.Linq;
using YGOSharp.OCGWrapper;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x020002BF RID: 703
	[Deck("Brave", "AI_Brave", "Normal")]
	internal class BraveExecutor : DefaultExecutor
	{
		// Token: 0x0600110C RID: 4364 RVA: 0x00055C20 File Offset: 0x00053E20
		public BraveExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.Activate, 2563463, new Func<bool>(this.WanderingGryphonRiderCounter));
			base.AddExecutor(ExecutorType.Activate, 14558127, new Func<bool>(base.DefaultAshBlossomAndJoyousSpring));
			base.AddExecutor(ExecutorType.Activate, 24224830, new Func<bool>(base.DefaultCalledByTheGrave));
			base.AddExecutor(ExecutorType.Activate, 65681983, new Func<bool>(this.CrossoutDesignatorEffect));
			base.AddExecutor(ExecutorType.Activate, 97268402, new Func<bool>(base.DefaultEffectVeiler));
			base.AddExecutor(ExecutorType.Activate, 10045474, new Func<bool>(base.DefaultInfiniteImpermanence));
			base.AddExecutor(ExecutorType.Activate, 27548199, new Func<bool>(this.BorreloadSavageDragonEffect));
			base.AddExecutor(ExecutorType.Activate, 84815190, new Func<bool>(this.BaronessDeFleurEffect));
			base.AddExecutor(ExecutorType.Activate, 40605147, new Func<bool>(base.DefaultSolemnStrike));
			base.AddExecutor(ExecutorType.Activate, 23434538, new Func<bool>(base.DefaultMaxxC));
			base.AddExecutor(ExecutorType.Activate, 18144506);
			base.AddExecutor(ExecutorType.Activate, 15291624);
			base.AddExecutor(ExecutorType.Activate, 38745520, new Func<bool>(this.DracobackTheDragonSteedBounce));
			base.AddExecutor(ExecutorType.Activate, 60461804, new Func<bool>(this.DestinyHeroDestroyPhoenixEnforcerEffect));
			base.AddExecutor(ExecutorType.Activate, 3285551, new Func<bool>(this.RiteofAramesiaEffect));
			base.AddExecutor(ExecutorType.Activate, 30680659, new Func<bool>(this.AquamancerOfTheSanctuarySearchEffect));
			base.AddExecutor(ExecutorType.Activate, 39568067, new Func<bool>(this.JourneyOfDestinyActivate));
			base.AddExecutor(ExecutorType.Activate, 2563463, new Func<bool>(this.WanderingGryphonRiderSummon));
			base.AddExecutor(ExecutorType.Activate, 81439173, new Func<bool>(this.FoolishBurialFirst));
			base.AddExecutor(ExecutorType.Summon, 26202165);
			base.AddExecutor(ExecutorType.Summon, 72291078);
			base.AddExecutor(ExecutorType.Activate, 72291078, new Func<bool>(this.MechaPhantomBeastOLionEffect));
			base.AddExecutor(ExecutorType.Summon, 9742784);
			base.AddExecutor(ExecutorType.Activate, 39568067, new Func<bool>(this.JourneyOfDestinyEffect));
			base.AddExecutor(ExecutorType.SpSummon, 60303245, new Func<bool>(this.SalamangreatAlmirajSummonFirst));
			base.AddExecutor(ExecutorType.SpSummon, 91646304, new Func<bool>(this.CrusadiaArboriaSummon));
			base.AddExecutor(ExecutorType.SpSummon, 50588353, new Func<bool>(this.CrystronNeedlefiberSummon));
			base.AddExecutor(ExecutorType.Activate, 50588353, new Func<bool>(this.CrystronNeedlefiberEffect));
			base.AddExecutor(ExecutorType.SpSummon, 44097050, new Func<bool>(this.MechaPhantomBeastAuroradonSummon));
			base.AddExecutor(ExecutorType.Activate, 44097050, new Func<bool>(this.MechaPhantomBeastAuroradonEffect));
			base.AddExecutor(ExecutorType.Activate, 30680659, new Func<bool>(this.AquamancerOfTheSanctuarySummonEffect));
			base.AddExecutor(ExecutorType.SpSummon, 90953320, new Func<bool>(this.TGHyperLibrarianSummon));
			base.AddExecutor(ExecutorType.Activate, 90953320);
			base.AddExecutor(ExecutorType.SpSummon, 21915012, new Func<bool>(this.CupidPitchSummon));
			base.AddExecutor(ExecutorType.Activate, 21915012, new Func<bool>(this.CupidPitchEffect));
			base.AddExecutor(ExecutorType.Activate, 9742784, new Func<bool>(this.JetSynchronEffect));
			base.AddExecutor(ExecutorType.SpSummon, 60303245, new Func<bool>(this.SalamangreatAlmirajSummon));
			base.AddExecutor(ExecutorType.SpSummon, 98978921, new Func<bool>(this.LinkSpiderSummon));
			base.AddExecutor(ExecutorType.Activate, 38745520, new Func<bool>(this.DracobackTheDragonSteedEquip));
			base.AddExecutor(ExecutorType.SpSummon, 27548199, new Func<bool>(this.BorreloadSavageDragonSummon));
			base.AddExecutor(ExecutorType.Activate, 72090076);
			base.AddExecutor(ExecutorType.SpSummon, 15291624);
			base.AddExecutor(ExecutorType.Summon, 91646304, new Func<bool>(this.SummonForMaterial));
			base.AddExecutor(ExecutorType.Summon, 14558127, new Func<bool>(this.SummonForMaterial));
			base.AddExecutor(ExecutorType.Summon, 97268402, new Func<bool>(this.SummonForMaterial));
			base.AddExecutor(ExecutorType.Summon, 30680659, new Func<bool>(this.SummonForMaterial));
			base.AddExecutor(ExecutorType.Summon, 23434538, new Func<bool>(this.SummonForMaterial));
			base.AddExecutor(ExecutorType.Activate, 81439173, new Func<bool>(this.FoolishBurialEffect));
			base.AddExecutor(ExecutorType.Activate, 83764718, new Func<bool>(this.MonsterRebornEffect));
			base.AddExecutor(ExecutorType.SpSummon, 84815190, new Func<bool>(this.BaronessDeFleurSummon));
			base.AddExecutor(ExecutorType.SpSummon, 92519087, new Func<bool>(this.VirtualWorldKyubiShenshenSummon));
			base.AddExecutor(ExecutorType.Activate, 92519087, new Func<bool>(this.VirtualWorldKyubiShenshenEffect));
			base.AddExecutor(ExecutorType.SpSummon, 42566602);
			base.AddExecutor(ExecutorType.Activate, 42566602, new Func<bool>(this.CoralDragonEffect));
			base.AddExecutor(ExecutorType.Activate, 98558751, new Func<bool>(this.TGWonderMagicianEffect));
			base.AddExecutor(ExecutorType.Activate, 52947044, new Func<bool>(this.FusionDestinyEffect));
			base.AddExecutor(ExecutorType.SpSummon, 70369116, new Func<bool>(this.PredaplantVerteAnacondaSummon));
			base.AddExecutor(ExecutorType.Activate, 70369116, new Func<bool>(this.PredaplantVerteAnacondaEffect));
			base.AddExecutor(ExecutorType.Summon, 72090076, new Func<bool>(this.SummonForMaterial));
			base.AddExecutor(ExecutorType.Summon, 63362460, new Func<bool>(this.SummonForMaterial));
			base.AddExecutor(ExecutorType.Activate, 81866673, new Func<bool>(this.DestinyHeroDasherEffect));
			base.AddExecutor(ExecutorType.Activate, 63362460, new Func<bool>(this.DestinyHeroCelestialEffect));
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(this.MonsterRepos));
			base.AddExecutor(ExecutorType.SpellSet, 10045474, new Func<bool>(this.TrapSet));
			base.AddExecutor(ExecutorType.SpellSet, 40605147, new Func<bool>(this.TrapSet));
			base.AddExecutor(ExecutorType.SpellSet, 65681983, new Func<bool>(this.TrapSet));
			base.AddExecutor(ExecutorType.MonsterSet, 26202165);
			base.AddExecutor(ExecutorType.SpellSet, new Func<bool>(this.SetForCelestial));
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x0000763C File Offset: 0x0000583C
		public override bool OnSelectHand()
		{
			return true;
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x00056213 File Offset: 0x00054413
		public override void OnNewTurn()
		{
			this.BeastOLionUsed = false;
			this.JetSynchronUsed = false;
			this.FusionDestinyUsed = false;
			this.PhoenixTarget = null;
			this.PhoenixSelectingTarget = 0;
			base.OnNewTurn();
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x00056240 File Offset: 0x00054440
		public override CardPosition OnSelectPosition(int cardId, IList<CardPosition> positions)
		{
			NamedCard cardData = NamedCard.Get(cardId);
			if (cardData != null)
			{
				if (cardData.Attack <= 1000)
				{
					return CardPosition.FaceUpDefence;
				}
				if (base.Util.IsTurn1OrMain2() && cardData.Attack <= 2500)
				{
					return CardPosition.FaceUpDefence;
				}
			}
			return (CardPosition)0;
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x00056284 File Offset: 0x00054484
		public override int OnSelectPlace(int cardId, int player, CardLocation location, int available)
		{
			if (location != CardLocation.MonsterZone)
			{
				return 0;
			}
			if (cardId == 60303245)
			{
				return available & 96;
			}
			if (cardId == 90953320 && base.Bot.GetMonsterCount() >= 3)
			{
				return available & 96;
			}
			return available & 31 & ~base.Bot.GetLinkedZones() & -21;
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x000562D8 File Offset: 0x000544D8
		public override IList<ClientCard> OnSelectCard(IList<ClientCard> cards, int min, int max, int hint, bool cancelable)
		{
			if (hint != 502)
			{
				this.PhoenixSelectingTarget = 0;
			}
			if (hint == 506 && max == 1)
			{
				foreach (ClientCard card in cards)
				{
					if (card.IsCode(91646304))
					{
						return new List<ClientCard>(new ClientCard[] { card });
					}
					if (card.IsCode(72090076))
					{
						return new List<ClientCard>(new ClientCard[] { card });
					}
				}
				foreach (ClientCard card2 in cards)
				{
					if (card2.IsCode(23434538) && !base.Bot.HasInHand(23434538))
					{
						return new List<ClientCard>(new ClientCard[] { card2 });
					}
					if (card2.IsCode(14558127) && !base.Bot.HasInHand(14558127))
					{
						return new List<ClientCard>(new ClientCard[] { card2 });
					}
					if (card2.IsCode(97268402) && !base.Bot.HasInHand(97268402))
					{
						return new List<ClientCard>(new ClientCard[] { card2 });
					}
				}
			}
			if (hint == 509 && max == 1)
			{
				foreach (ClientCard card3 in cards)
				{
					if (card3.IsCode(60461804))
					{
						return new List<ClientCard>(new ClientCard[] { card3 });
					}
				}
			}
			if (hint == 502 && max == 1)
			{
				this.PhoenixSelectingTarget++;
				if (this.PhoenixSelectingTarget >= 2 && !cards.Contains(this.PhoenixTarget))
				{
					ClientCard target = base.Util.GetProblematicEnemyCard(0, false);
					if (target == null || !cards.Contains(target))
					{
						target = base.Util.GetBestEnemyCard(false, false);
					}
					if (target != null && cards.Contains(target))
					{
						return new List<ClientCard>(new ClientCard[] { target });
					}
				}
			}
			if (hint == 533 && cancelable && min == 0)
			{
				return new List<ClientCard>();
			}
			return base.OnSelectCard(cards, min, max, hint, cancelable);
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x00056558 File Offset: 0x00054758
		public override int OnSelectOption(IList<int> options)
		{
			if (options.Count == 2 && options[0] == base.Util.GetStringId(21915012, 1))
			{
				return 0;
			}
			return base.OnSelectOption(options);
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x00056588 File Offset: 0x00054788
		private IList<int> GetHandCost()
		{
			List<int> result = new List<int>();
			if (base.Bot.HasInMonstersZone(3285552, false, false, false))
			{
				result.Add(38745520);
			}
			if (base.Bot.Hand.Count((ClientCard card) => card.IsCode(52947044)) >= 2)
			{
				result.Add(52947044);
			}
			if (base.Bot.Hand.Count((ClientCard card) => card.IsCode(26202165)) >= 2)
			{
				result.Add(26202165);
			}
			if (base.Bot.Hand.Count((ClientCard card) => card.IsCode(3285551)) >= 2)
			{
				result.Add(3285551);
			}
			if (base.Bot.Hand.Count((ClientCard card) => card.IsCode(30680659)) >= 2)
			{
				result.Add(30680659);
			}
			if (base.Bot.HasInGraveyardOrInBanished(63362460) || !base.Bot.HasInExtra(60461804))
			{
				result.Add(81866673);
			}
			if (base.Bot.HasInGraveyardOrInBanished(81866673) || !base.Bot.HasInExtra(60461804))
			{
				result.Add(63362460);
			}
			if (base.Bot.Hand.Count((ClientCard card) => card.IsCode(14558127)) >= 2)
			{
				result.Add(14558127);
			}
			if (base.Bot.Hand.Count((ClientCard card) => card.IsCode(65681983)) >= 2)
			{
				result.Add(65681983);
			}
			if (base.Bot.HasInHand(result))
			{
				return result;
			}
			result.AddRange(new int[]
			{
				18144506, 72291078, 26202165, 14558127, 23434538, 97268402, 65681983, 24224830, 3285551, 30680659,
				10045474, 40605147
			});
			return result;
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x000567B0 File Offset: 0x000549B0
		private bool WanderingGryphonRiderCounter()
		{
			return base.Card.Location != CardLocation.Hand && base.Duel.LastChainPlayer == 1;
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x000567D0 File Offset: 0x000549D0
		private bool CrossoutDesignatorEffect()
		{
			ClientCard LastChainCard = base.Util.GetLastChainCard();
			return LastChainCard != null && base.Duel.LastChainPlayer == 1 && (this.CrossoutDesignatorCheck(LastChainCard, 30680659, 3) || this.CrossoutDesignatorCheck(LastChainCard, 26202165, 3) || this.CrossoutDesignatorCheck(LastChainCard, 14558127, 3) || this.CrossoutDesignatorCheck(LastChainCard, 23434538, 2) || this.CrossoutDesignatorCheck(LastChainCard, 97268402, 1) || this.CrossoutDesignatorCheck(LastChainCard, 3285551, 3) || this.CrossoutDesignatorCheck(LastChainCard, 18144506, 1) || this.CrossoutDesignatorCheck(LastChainCard, 52947044, 3) || this.CrossoutDesignatorCheck(LastChainCard, 81439173, 1) || this.CrossoutDesignatorCheck(LastChainCard, 83764718, 1) || this.CrossoutDesignatorCheck(LastChainCard, 24224830, 2) || this.CrossoutDesignatorCheck(LastChainCard, 65681983, 3) || this.CrossoutDesignatorCheck(LastChainCard, 10045474, 3));
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x000568CB File Offset: 0x00054ACB
		private bool CrossoutDesignatorCheck(ClientCard LastChainCard, int id, int count)
		{
			if (LastChainCard.IsCode(id) && base.Bot.GetRemainingCount(id, count) > 0)
			{
				base.AI.SelectAnnounceID(id);
				return true;
			}
			return false;
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x000568F8 File Offset: 0x00054AF8
		private bool DestinyHeroDestroyPhoenixEnforcerEffect()
		{
			if (base.Card.Location == CardLocation.Grave)
			{
				return true;
			}
			ClientCard target = base.Util.GetProblematicEnemyCard(2500, false);
			if (target != null && !base.Util.ChainContainPlayer(0))
			{
				base.AI.SelectCard(60461804);
				base.AI.SelectNextCard(target);
				return true;
			}
			target = base.Util.GetBestEnemyCard(false, false);
			if (target == null)
			{
				return false;
			}
			if (base.DefaultOnBecomeTarget() || base.Bot.UnderAttack || base.Duel.Phase == DuelPhase.End || (base.Duel.Player == 0 && base.Util.IsTurn1OrMain2()) || (base.Duel.Player == 1 && base.Enemy.GetMonsterCount() >= 2))
			{
				this.PhoenixTarget = target;
				base.AI.SelectCard(60461804);
				base.AI.SelectNextCard(target);
				return true;
			}
			return false;
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x000569F0 File Offset: 0x00054BF0
		private bool DracobackTheDragonSteedBounce()
		{
			if (base.Card.Location != CardLocation.SpellZone)
			{
				return false;
			}
			ClientCard target = base.Util.GetProblematicEnemyCard(0, false);
			base.AI.SelectCard(target);
			return true;
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x00056A28 File Offset: 0x00054C28
		private bool DracobackTheDragonSteedEquip()
		{
			if (base.Card.Location == CardLocation.SpellZone)
			{
				return false;
			}
			if (base.Card.Location == CardLocation.Grave)
			{
				return true;
			}
			if (base.Bot.HasInMonstersZone(3285552, false, false, true))
			{
				base.AI.SelectCard(3285552);
				return true;
			}
			return false;
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x00056A80 File Offset: 0x00054C80
		private bool BorreloadSavageDragonSummon()
		{
			int[] materials = new int[] { 21915012, 31533705, 30680659 };
			if (base.Bot.MonsterZone.GetMatchingCardsCount((ClientCard card) => card.IsCode(materials)) >= 2)
			{
				base.AI.SelectMaterials(materials, 0);
				return true;
			}
			return false;
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x00056ADE File Offset: 0x00054CDE
		private bool BorreloadSavageDragonEffect()
		{
			if (base.ActivateDescription == -1)
			{
				base.AI.SelectCard(new int[] { 44097050, 50588353, 70369116 });
				return true;
			}
			return true;
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x00056B08 File Offset: 0x00054D08
		private bool RiteofAramesiaEffect()
		{
			base.AI.SelectYesNo(true);
			return true;
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x00056B18 File Offset: 0x00054D18
		private bool WanderingGryphonRiderSummon()
		{
			return base.Card.Location == CardLocation.Hand && (base.Bot.HasInMonstersZone(3285552, false, false, false) || (base.Duel.Player == 0 && (base.Duel.LastChainPlayer == -1 || base.Bot.HasInSpellZone(39568067, false, false))));
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x00056B7D File Offset: 0x00054D7D
		private bool JourneyOfDestinyActivate()
		{
			return base.Card.Location == CardLocation.Hand;
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x00056B90 File Offset: 0x00054D90
		private bool JourneyOfDestinyEffect()
		{
			if (base.Card.Location == CardLocation.Hand)
			{
				return false;
			}
			if (base.ActivateDescription == -1 || base.ActivateDescription == base.Util.GetStringId(39568067, 1))
			{
				base.AI.SelectOption(0);
				return true;
			}
			if (base.Bot.GetRemainingCount(2563463, 1) == 0 || base.Bot.GetHandCount() == 0 || !base.Bot.HasInMonstersZone(3285552, false, false, false))
			{
				base.AI.SelectCard(30680659);
				if (base.Bot.HasInHandOrInMonstersZoneOrInGraveyard(30680659))
				{
					base.AI.SelectNextCard(30680659);
				}
				else
				{
					base.AI.SelectNextCard(this.GetHandCost());
				}
			}
			else
			{
				base.AI.SelectCard(2563463);
				base.AI.SelectNextCard(this.GetHandCost());
			}
			return true;
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x00056C7C File Offset: 0x00054E7C
		private bool AquamancerOfTheSanctuarySearchEffect()
		{
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			if (base.Card.Location == CardLocation.Grave)
			{
				base.AI.SelectCard(CardLocation.Deck);
				return true;
			}
			return base.ActivateDescription != base.Util.GetStringId(30680659, 0) && !base.Bot.HasInHand(3285551) && !base.Bot.HasInMonstersZone(3285552, false, false, false);
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x00056CFB File Offset: 0x00054EFB
		private bool AquamancerOfTheSanctuarySummonEffect()
		{
			return base.ActivateDescription == base.Util.GetStringId(30680659, 0) && base.Bot.GetMonsterCount() <= 3;
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x00056D2C File Offset: 0x00054F2C
		private bool MechaPhantomBeastOLionEffect()
		{
			if (base.Bot.GetMonsterCount() >= 3 && ((base.Bot.HasInExtra(44097050) && base.Bot.HasInMonstersZone(50588353, false, false, false)) || base.Bot.HasInMonstersZone(44097050, false, false, false)))
			{
				return false;
			}
			if (base.ActivateDescription == -1)
			{
				this.BeastOLionUsed = true;
				return true;
			}
			return !this.BeastOLionUsed;
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x00056DA0 File Offset: 0x00054FA0
		private bool CrusadiaArboriaSummon()
		{
			return !base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup() && card.IsTuner());
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x00056DD4 File Offset: 0x00054FD4
		private bool CrystronNeedlefiberSummon()
		{
			if (this.JetSynchronUsed && !base.Bot.HasInMonstersZone(72291078, false, false, false))
			{
				return false;
			}
			List<int> materials = new List<int>
			{
				26202165, 55063751, 70369116, 60303245, 98978921, 23434538, 31533705, 30680659, 91646304, 72291078,
				9742784, 14558127, 97268402
			};
			if (!base.Bot.HasInMonstersZone(3285552, false, false, false) || !base.Bot.HasInMonstersZone(2563463, false, false, false))
			{
				materials.Add(3285552);
				materials.Add(2563463);
			}
			if (base.Bot.MonsterZone.GetMatchingCardsCount((ClientCard card) => card.IsCode(materials)) >= 2)
			{
				base.AI.SelectMaterials(materials, 0);
				return true;
			}
			return false;
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x00056F20 File Offset: 0x00055120
		private bool CrystronNeedlefiberEffect()
		{
			if (base.Duel.Player == 0)
			{
				base.AI.SelectCard(new int[] { 9742784, 72291078, 97268402 });
				return true;
			}
			if (base.Enemy.GetSpells().Any((ClientCard card) => card.IsFacedown() || card.HasType(CardType.Continuous) || card.HasType(CardType.Field) || card.HasType(CardType.Equip)))
			{
				base.AI.SelectCard(98558751);
			}
			else
			{
				base.AI.SelectCard(42566602);
			}
			return true;
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x00056FAC File Offset: 0x000551AC
		private bool TGWonderMagicianEffect()
		{
			ClientCard target = base.Util.GetProblematicEnemySpell();
			if (target == null)
			{
				target = base.Enemy.GetSpells().Find((ClientCard card) => card.IsFacedown() || card.HasType(CardType.Continuous) || card.HasType(CardType.Field) || card.HasType(CardType.Equip));
			}
			if (target == null)
			{
				return false;
			}
			base.AI.SelectCard(target);
			return true;
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x0005700A File Offset: 0x0005520A
		private bool MechaPhantomBeastAuroradonSummon()
		{
			return base.Bot.GetMonsterCount() <= 4;
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x0005701D File Offset: 0x0005521D
		private bool MechaPhantomBeastAuroradonEffect()
		{
			if (base.ActivateDescription == -1)
			{
				return true;
			}
			base.AI.SelectOption(1);
			base.AI.SelectCard(44097050);
			base.AI.SelectNextCard(31533705);
			return true;
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x00057058 File Offset: 0x00055258
		private bool TGHyperLibrarianSummon()
		{
			int[] materials = new int[] { 72291078, 31533705, 30680659 };
			if (base.Bot.MonsterZone.GetMatchingCardsCount((ClientCard card) => card.IsCode(materials)) >= 3)
			{
				base.AI.SelectMaterials(materials, 0);
				return true;
			}
			return false;
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x000570B8 File Offset: 0x000552B8
		private bool JetSynchronEffect()
		{
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			int[] materials = new int[] { 31533705 };
			if (base.Bot.MonsterZone.GetMatchingCardsCount((ClientCard card) => card.IsCode(materials)) >= 2 || base.Bot.GetMonsterCount() <= 3)
			{
				this.JetSynchronUsed = true;
				base.AI.SelectCard(this.GetHandCost());
				return true;
			}
			return false;
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x00057138 File Offset: 0x00055338
		private bool CupidPitchSummon()
		{
			int[] materials = new int[] { 9742784, 97268402, 31533705, 30680659, 26202165, 90953320, 3285552 };
			if (base.Bot.MonsterZone.GetMatchingCardsCount((ClientCard card) => card.IsCode(materials)) >= 3)
			{
				base.AI.SelectMaterials(materials, 0);
				return true;
			}
			return false;
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x00057196 File Offset: 0x00055396
		private bool CupidPitchEffect()
		{
			if (base.Card.Location == CardLocation.MonsterZone)
			{
				base.AI.SelectOption(1);
			}
			else
			{
				base.AI.SelectCard(72090076);
			}
			return true;
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x000571C8 File Offset: 0x000553C8
		private bool SalamangreatAlmirajSummonFirst()
		{
			int[] materials = new int[] { 26202165 };
			if (base.Bot.MonsterZone.GetMatchingCardsCount((ClientCard card) => card.IsCode(materials) && !card.IsSpecialSummoned) == 0)
			{
				return false;
			}
			base.AI.SelectMaterials(materials, 0);
			return true;
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x00057224 File Offset: 0x00055424
		private bool SalamangreatAlmirajSummon()
		{
			if (this.PhoenixNotAvail())
			{
				return false;
			}
			int[] materials = new int[] { 72291078, 9742784 };
			if (base.Bot.MonsterZone.GetMatchingCardsCount((ClientCard card) => card.IsCode(materials) && !card.IsSpecialSummoned) == 0)
			{
				return false;
			}
			base.AI.SelectMaterials(materials, 0);
			return true;
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x00057290 File Offset: 0x00055490
		private bool LinkSpiderSummon()
		{
			if (this.PhoenixNotAvail())
			{
				return false;
			}
			List<int> materials = new List<int> { 31533705 };
			if (!base.Bot.HasInMonstersZone(3285552, false, false, false) || !base.Bot.HasInMonstersZone(2563463, false, false, false))
			{
				materials.Add(3285552);
			}
			if (base.Bot.GetMonsters().Any((ClientCard card) => card.IsCode(27204312) && card.Attack <= 4000))
			{
				materials.Add(27204312);
			}
			if (base.Bot.MonsterZone.GetMatchingCardsCount((ClientCard card) => card.IsCode(materials)) == 0)
			{
				return false;
			}
			base.AI.SelectMaterials(materials, 0);
			return true;
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x00057374 File Offset: 0x00055574
		private bool NeedMonster()
		{
			if (base.Bot.HasInMonstersZone(70369116, true, false, false) || this.PhoenixNotAvail())
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

		// Token: 0x06001131 RID: 4401 RVA: 0x00057428 File Offset: 0x00055628
		private bool SummonForMaterial()
		{
			if (base.Bot.HasInMonstersZone(70369116, true, false, false) || !base.Bot.HasInExtra(70369116))
			{
				return false;
			}
			return base.Bot.MonsterZone.GetMatchingCardsCount((ClientCard card) => (card.HasType(CardType.Effect) || card.IsTuner()) && card.Level < 8) == 1;
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x00057494 File Offset: 0x00055694
		private bool PhoenixNotAvail()
		{
			return base.Bot.LifePoints <= 2000 || base.Bot.GetRemainingCount(52947044, 3) == 0 || base.Bot.HasInHand(52947044) || !base.Bot.HasInExtra(70369116) || !base.Bot.HasInExtra(60461804) || (base.Bot.GetRemainingCount(63362460, 1) == 0 && !base.Bot.HasInHand(63362460)) || (base.Bot.GetRemainingCount(81866673, 1) == 0 && !base.Bot.HasInHand(81866673));
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x00057554 File Offset: 0x00055754
		private bool PredaplantVerteAnacondaSummon()
		{
			if (this.PhoenixNotAvail())
			{
				return false;
			}
			List<int> materials = new List<int>
			{
				55063751, 30680659, 26202165, 60303245, 98978921, 72090076, 63362460, 81866673, 91646304, 14558127,
				72291078, 23434538, 9742784, 97268402, 50588353, 90953320
			};
			if (!base.Bot.HasInMonstersZone(3285552, false, false, false) || !base.Bot.HasInMonstersZone(2563463, false, false, false))
			{
				materials.Add(2563463);
			}
			if (base.Bot.MonsterZone.GetMatchingCardsCount((ClientCard card) => card.IsCode(materials)) >= 2)
			{
				base.AI.SelectMaterials(materials, 0);
				return true;
			}
			return false;
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x0005769C File Offset: 0x0005589C
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
			this.FusionDestinyUsed = true;
			base.AI.SelectCard(52947044);
			base.AI.SelectMaterials(CardLocation.Deck, 0);
			return true;
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x000576F9 File Offset: 0x000558F9
		private bool FusionDestinyEffect()
		{
			this.FusionDestinyUsed = true;
			return true;
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x00057704 File Offset: 0x00055904
		private bool FoolishBurialFirst()
		{
			if (!base.Bot.HasInHand(3285551) && !base.Bot.HasInHandOrInGraveyard(30680659) && !base.Bot.HasInMonstersZone(3285552, false, false, false))
			{
				base.AI.SelectCard(30680659);
				return true;
			}
			return false;
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x0005775D File Offset: 0x0005595D
		private bool FoolishBurialEffect()
		{
			if (this.FusionDestinyUsed)
			{
				return false;
			}
			if (!this.NeedMonster())
			{
				return false;
			}
			base.AI.SelectCard(new int[] { 72291078 });
			return true;
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x00057790 File Offset: 0x00055990
		private bool MonsterRebornEffect()
		{
			if (base.Bot.HasInGraveyard(84815190))
			{
				base.AI.SelectCard(84815190);
				return true;
			}
			if (base.Bot.HasInGraveyard(2563463))
			{
				base.AI.SelectCard(2563463);
				return true;
			}
			if (!this.NeedMonster())
			{
				return false;
			}
			base.AI.SelectCard(new int[] { 26202165, 72291078, 91646304, 14558127 });
			return true;
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x0005780C File Offset: 0x00055A0C
		private bool DestinyHeroDasherEffect()
		{
			return base.Bot.Hand.Count((ClientCard card) => card.IsMonster()) > 1;
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x00057840 File Offset: 0x00055A40
		private bool DestinyHeroCelestialEffect()
		{
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			if (!base.Bot.HasInGraveyard(81866673))
			{
				return false;
			}
			base.AI.SelectCard(81866673);
			return true;
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x00057878 File Offset: 0x00055A78
		private bool BaronessDeFleurSummon()
		{
			int[] materials = new int[] { 21915012, 98558751, 90953320 };
			if (base.Bot.MonsterZone.GetMatchingCardsCount((ClientCard card) => card.IsCode(materials)) >= 2)
			{
				base.AI.SelectMaterials(materials, 0);
				return true;
			}
			if (!base.Bot.HasInMonstersZone(3285552, false, false, false))
			{
				materials = new int[] { 14558127, 91646304, 2563463 };
				if (base.Bot.MonsterZone.GetMatchingCardsCount((ClientCard card) => card.IsCode(materials)) >= 2)
				{
					base.AI.SelectMaterials(materials, 0);
					return true;
				}
			}
			if (!base.Bot.HasInMonstersZone(2563463, false, false, false) || base.Bot.HasInHand(3285551))
			{
				materials = new int[] { 42566602, 3285552 };
				if (base.Bot.MonsterZone.GetMatchingCardsCount((ClientCard card) => card.IsCode(materials)) >= 2)
				{
					base.AI.SelectMaterials(materials, 0);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x000579AC File Offset: 0x00055BAC
		private bool BaronessDeFleurEffect()
		{
			if (base.Duel.LastChainPlayer == 0)
			{
				return false;
			}
			if (base.Duel.LastChainPlayer == 1)
			{
				this.BaronessDeFleurUsed = true;
				return true;
			}
			if (base.Duel.Phase == DuelPhase.Standby && this.BaronessDeFleurUsed)
			{
				this.BaronessDeFleurUsed = false;
				return true;
			}
			if (base.Duel.Phase == DuelPhase.Main1 || base.Duel.Phase == DuelPhase.Main2)
			{
				ClientCard target = base.Util.GetProblematicEnemyCard(0, true);
				if (target == null)
				{
					target = base.Util.GetBestEnemyCard(false, true);
				}
				if (target != null)
				{
					base.AI.SelectCard(target);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x00057A50 File Offset: 0x00055C50
		private bool VirtualWorldKyubiShenshenSummon()
		{
			if (base.Bot.HasInMonstersZone(60461804, false, false, false))
			{
				return false;
			}
			int[] materials = new int[] { 42566602, 30680659, 26202165 };
			if (base.Bot.MonsterZone.GetMatchingCardsCount((ClientCard card) => card.IsCode(materials)) >= 2)
			{
				base.AI.SelectMaterials(materials, 0);
				return true;
			}
			if (!base.Bot.HasInMonstersZone(3285552, false, false, false))
			{
				materials = new int[] { 72291078, 2563463 };
				if (base.Bot.MonsterZone.GetMatchingCardsCount((ClientCard card) => card.IsCode(materials)) >= 2)
				{
					base.AI.SelectMaterials(materials, 0);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x00057B2C File Offset: 0x00055D2C
		private bool VirtualWorldKyubiShenshenEffect()
		{
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			if (base.Card.Location == CardLocation.MonsterZone && base.Bot.HasInBanished(30680659))
			{
				base.AI.SelectCard(30680659);
				return true;
			}
			int[] costs = new int[]
			{
				72090076, 26202165, 91646304, 14558127, 72291078, 23434538, 97268402, 15291624, 27548199, 42566602,
				90953320, 98558751, 21915012, 50588353, 70369116, 98978921, 60303245
			};
			base.AI.SelectCard(costs);
			base.AI.SelectNextCard(costs);
			return true;
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x00057BA8 File Offset: 0x00055DA8
		private bool CoralDragonEffect()
		{
			if (base.Card.Location == CardLocation.Grave)
			{
				return true;
			}
			ClientCard target = base.Util.GetProblematicEnemyCard(0, true);
			if (target != null)
			{
				base.AI.SelectCard(target);
				return true;
			}
			return false;
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x00057BE6 File Offset: 0x00055DE6
		private bool TrapSet()
		{
			base.AI.SelectPlace(27);
			return true;
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x00057BF6 File Offset: 0x00055DF6
		private bool SetForCelestial()
		{
			return !this.FusionDestinyUsed && base.Bot.HasInGraveyard(63362460) && base.Bot.HasInGraveyard(81866673) && this.TrapSet();
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x00057C2C File Offset: 0x00055E2C
		private bool MonsterRepos()
		{
			return base.Card.IsFacedown() || base.DefaultMonsterRepos();
		}

		// Token: 0x040015C1 RID: 5569
		private bool BeastOLionUsed;

		// Token: 0x040015C2 RID: 5570
		private bool JetSynchronUsed;

		// Token: 0x040015C3 RID: 5571
		private bool FusionDestinyUsed;

		// Token: 0x040015C4 RID: 5572
		private bool BaronessDeFleurUsed;

		// Token: 0x040015C5 RID: 5573
		private ClientCard PhoenixTarget;

		// Token: 0x040015C6 RID: 5574
		private int PhoenixSelectingTarget;

		// Token: 0x020002C0 RID: 704
		public class CardId
		{
			// Token: 0x040015C7 RID: 5575
			public const int WanderingGryphonRider = 2563463;

			// Token: 0x040015C8 RID: 5576
			public const int DestinyHeroDasher = 81866673;

			// Token: 0x040015C9 RID: 5577
			public const int NemesesCorridor = 72090076;

			// Token: 0x040015CA RID: 5578
			public const int DestinyHeroCelestial = 63362460;

			// Token: 0x040015CB RID: 5579
			public const int AquamancerOfTheSanctuary = 30680659;

			// Token: 0x040015CC RID: 5580
			public const int Sangan = 26202165;

			// Token: 0x040015CD RID: 5581
			public const int CrusadiaArboria = 91646304;

			// Token: 0x040015CE RID: 5582
			public const int AshBlossomJoyousSpring = 14558127;

			// Token: 0x040015CF RID: 5583
			public const int MechaPhantomBeastOLion = 72291078;

			// Token: 0x040015D0 RID: 5584
			public const int MaxxC = 23434538;

			// Token: 0x040015D1 RID: 5585
			public const int JetSynchron = 9742784;

			// Token: 0x040015D2 RID: 5586
			public const int EffectVeiler = 97268402;

			// Token: 0x040015D3 RID: 5587
			public const int RiteofAramesia = 3285551;

			// Token: 0x040015D4 RID: 5588
			public const int HarpiesFeatherDuster = 18144506;

			// Token: 0x040015D5 RID: 5589
			public const int FusionDestiny = 52947044;

			// Token: 0x040015D6 RID: 5590
			public const int FoolishBurial = 81439173;

			// Token: 0x040015D7 RID: 5591
			public const int MonsterReborn = 83764718;

			// Token: 0x040015D8 RID: 5592
			public const int CalledByTheGrave = 24224830;

			// Token: 0x040015D9 RID: 5593
			public const int CrossoutDesignator = 65681983;

			// Token: 0x040015DA RID: 5594
			public const int JourneyOfDestiny = 39568067;

			// Token: 0x040015DB RID: 5595
			public const int DracobackTheDragonSteed = 38745520;

			// Token: 0x040015DC RID: 5596
			public const int InfiniteImpermanence = 10045474;

			// Token: 0x040015DD RID: 5597
			public const int SolemnStrike = 40605147;

			// Token: 0x040015DE RID: 5598
			public const int ThunderDragonColossus = 15291624;

			// Token: 0x040015DF RID: 5599
			public const int DestinyHeroDestroyPhoenixEnforcer = 60461804;

			// Token: 0x040015E0 RID: 5600
			public const int BaronessDeFleur = 84815190;

			// Token: 0x040015E1 RID: 5601
			public const int VirtualWorldKyubiShenshen = 92519087;

			// Token: 0x040015E2 RID: 5602
			public const int BorreloadSavageDragon = 27548199;

			// Token: 0x040015E3 RID: 5603
			public const int CoralDragon = 42566602;

			// Token: 0x040015E4 RID: 5604
			public const int TGHyperLibrarian = 90953320;

			// Token: 0x040015E5 RID: 5605
			public const int TGWonderMagician = 98558751;

			// Token: 0x040015E6 RID: 5606
			public const int CupidPitch = 21915012;

			// Token: 0x040015E7 RID: 5607
			public const int MechaPhantomBeastAuroradon = 44097050;

			// Token: 0x040015E8 RID: 5608
			public const int CrystronHalqifibrax = 50588353;

			// Token: 0x040015E9 RID: 5609
			public const int PredaplantVerteAnaconda = 70369116;

			// Token: 0x040015EA RID: 5610
			public const int LinkSpider = 98978921;

			// Token: 0x040015EB RID: 5611
			public const int SalamangreatAlmiraj = 60303245;

			// Token: 0x040015EC RID: 5612
			public const int BraveToken = 3285552;

			// Token: 0x040015ED RID: 5613
			public const int MechaPhantomBeastToken = 31533705;

			// Token: 0x040015EE RID: 5614
			public const int PrimalBeingToken = 27204312;
		}
	}
}
