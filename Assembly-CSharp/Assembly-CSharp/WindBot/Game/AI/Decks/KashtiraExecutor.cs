using System;
using System.Collections.Generic;
using System.Linq;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x02000318 RID: 792
	[Deck("Kashtira", "AI_Kashtira", "Normal")]
	internal class KashtiraExecutor : DefaultExecutor
	{
		// Token: 0x0600147C RID: 5244 RVA: 0x00072900 File Offset: 0x00070B00
		public KashtiraExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.Activate, 15291624);
			base.AddExecutor(ExecutorType.SpSummon, 15291624);
			base.AddExecutor(ExecutorType.Activate, 27204311, new Func<bool>(this.NibiruEffect));
			base.AddExecutor(ExecutorType.Activate, 10045474, new Func<bool>(this.Impermanence_activate));
			base.AddExecutor(ExecutorType.Activate, 91800273, new Func<bool>(this.DimensionShifterEffect));
			base.AddExecutor(ExecutorType.Activate, 23434538, new Func<bool>(base.DefaultMaxxC));
			base.AddExecutor(ExecutorType.Activate, 14558127, new Func<bool>(base.DefaultAshBlossomAndJoyousSpring));
			base.AddExecutor(ExecutorType.Activate, 24224830, new Func<bool>(this.CalledbytheGraveEffect));
			base.AddExecutor(ExecutorType.Activate, 27548199, new Func<bool>(this.BorreloadSavageDragonEffect));
			base.AddExecutor(ExecutorType.Activate, 65681983, new Func<bool>(this.CrossoutDesignatorEffect));
			base.AddExecutor(ExecutorType.Activate, 73628505, new Func<bool>(this.TerraformingEffect));
			base.AddExecutor(ExecutorType.Activate, 84211599, new Func<bool>(this.PotofProsperityEffect));
			base.AddExecutor(ExecutorType.Activate, 68304193, new Func<bool>(this.KashtiraUnicornEffect));
			base.AddExecutor(ExecutorType.Activate, 32909498, new Func<bool>(this.KashtiraFenrirEffect));
			base.AddExecutor(ExecutorType.Activate, 71832012, new Func<bool>(this.PrimePlanetParaisosEffect));
			base.AddExecutor(ExecutorType.Activate, 69540484, new Func<bool>(this.KashtiraBirthEffect));
			base.AddExecutor(ExecutorType.Activate, 95474755, new Func<bool>(this.DiablosistheMindHackerEffect));
			base.AddExecutor(ExecutorType.SpSummon, 32909498, new Func<bool>(this.KashtiraFenrirSummon));
			base.AddExecutor(ExecutorType.SpSummon, 68304193, delegate
			{
				this.summon_KashtiraUnicorn = true;
				return true;
			});
			base.AddExecutor(ExecutorType.SpSummon, 32909498, delegate
			{
				this.summon_KashtiraFenrir = true;
				return true;
			});
			base.AddExecutor(ExecutorType.Summon, 68304193, new Func<bool>(this.DefaultSummon));
			base.AddExecutor(ExecutorType.Summon, 32909498, new Func<bool>(this.DefaultSummon));
			base.AddExecutor(ExecutorType.Activate, 34447918, new Func<bool>(this.KashtiraPapiyasEffect));
			base.AddExecutor(ExecutorType.Activate, 4928565, new Func<bool>(this.KashtiraTearlamentsEffect));
			base.AddExecutor(ExecutorType.Activate, 78534861, new Func<bool>(this.KashtiraScareclawEffect));
			base.AddExecutor(ExecutorType.Activate, 69540484, new Func<bool>(this.KashtiraBirthEffect_2));
			base.AddExecutor(ExecutorType.Activate, 10389142);
			base.AddExecutor(ExecutorType.SpSummon, 10389142, new Func<bool>(this.GalaxyTomahawkSummon));
			base.AddExecutor(ExecutorType.SpSummon, 73542331, new Func<bool>(this.KashtiraShangriIraSummon));
			base.AddExecutor(ExecutorType.Activate, 73542331, new Func<bool>(this.KashtiraShangriIraEffect));
			base.AddExecutor(ExecutorType.SpSummon, 48626373, new Func<bool>(this.KashtiraAriseHeartSummon_2));
			base.AddExecutor(ExecutorType.SpSummon, 95474755, new Func<bool>(this.DiablosistheMindHackerSummon_2));
			base.AddExecutor(ExecutorType.Activate, 48626373, new Func<bool>(this.KashtiraAriseHeartEffect));
			base.AddExecutor(ExecutorType.SpSummon, 48626373, new Func<bool>(this.KashtiraAriseHeartSummon));
			base.AddExecutor(ExecutorType.SpSummon, 95474755, new Func<bool>(this.DiablosistheMindHackerSummon));
			base.AddExecutor(ExecutorType.SpSummon, 22423493, new Func<bool>(this.QliphortGeniusSummon));
			base.AddExecutor(ExecutorType.SpSummon, 65741786, new Func<bool>(this.IPSummon));
			base.AddExecutor(ExecutorType.Activate, 65741786, new Func<bool>(this.IPEffect));
			base.AddExecutor(ExecutorType.SpSummon, 44097050, new Func<bool>(this.MechaPhantomBeastAuroradonSummon));
			base.AddExecutor(ExecutorType.Activate, 44097050, new Func<bool>(this.MechaPhantomBeastAuroradonEffect));
			base.AddExecutor(ExecutorType.SpSummon, 21915012, new Func<bool>(this.CupidPitchSummon));
			base.AddExecutor(ExecutorType.Activate, 21915012);
			base.AddExecutor(ExecutorType.SpSummon, 27548199, new Func<bool>(this.BorreloadSavageDragonSummon));
			base.AddExecutor(ExecutorType.Activate, 72090076, new Func<bool>(this.NemesesCorridorEffect));
			base.AddExecutor(ExecutorType.SpSummon, 21887175, new Func<bool>(this.MekkKnightCrusadiaAvramaxSummon));
			base.AddExecutor(ExecutorType.Activate, 21887175, new Func<bool>(this.MekkKnightCrusadiaAvramaxEffect));
			base.AddExecutor(ExecutorType.Activate, 31149212, new Func<bool>(this.KashtiraRiseheartEffect_2));
			base.AddExecutor(ExecutorType.Activate, 31149212, new Func<bool>(this.KashtiraRiseheartEffect));
			base.AddExecutor(ExecutorType.Activate, 33925864, new Func<bool>(this.KashtiraBigBangEffect));
			base.AddExecutor(ExecutorType.Activate, 34447918, new Func<bool>(this.KashtiraPapiyasEffect_2));
			base.AddExecutor(ExecutorType.Activate, 69540484, new Func<bool>(this.KashtiraBirthEffect_3));
			base.AddExecutor(ExecutorType.Summon, 31149212, new Func<bool>(this.KashtiraRiseheartSummon));
			base.AddExecutor(ExecutorType.Summon, 4928565, new Func<bool>(this.DefaultSummon));
			base.AddExecutor(ExecutorType.SpellSet, new Func<bool>(this.SpellSet));
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(this.DefaultRepos));
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x0000763C File Offset: 0x0000583C
		public override bool OnSelectHand()
		{
			return true;
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x00072E48 File Offset: 0x00071048
		public override void OnNewTurn()
		{
			if (this.pre_link_mode < 0)
			{
				this.pre_link_mode = Program.Rand.Next(2);
			}
			this.isSummoned = false;
			this.onlyXyzSummon = false;
			this.activate_KashtiraUnicorn_1 = false;
			this.activate_KashtiraFenrir_1 = false;
			this.activate_KashtiraRiseheart_1 = false;
			this.activate_KashtiraRiseheart_2 = false;
			this.activate_PrimePlanetParaisos = false;
			this.activate_KashtiraScareclaw_1 = false;
			this.activate_KashtiraTearlaments_1 = false;
			this.activate_KashtiraShangriIra = false;
			this.activate_pre_PrimePlanetParaisos_2 = false;
			this.active_KashtiraPapiyas_1 = false;
			this.active_KashtiraPapiyas_2 = false;
			this.active_KashtiraBirth = false;
			this.active_NemesesCorridor = false;
			this.link_mode = false;
			this.summon_KashtiraUnicorn = false;
			this.summon_KashtiraFenrir = false;
			this.opt_0 = false;
			this.opt_1 = false;
			this.opt_2 = false;
			if (this.flag >= 0)
			{
				this.flag++;
			}
			if (this.flag >= 2)
			{
				this.flag = -1;
				this.activate_DimensionShifter = false;
			}
			base.OnNewTurn();
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x00072F36 File Offset: 0x00071136
		public override bool OnSelectYesNo(int desc)
		{
			if (desc == 1149312192)
			{
				this.activate_pre_PrimePlanetParaisos = true;
			}
			return base.OnSelectYesNo(desc);
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x00072F4E File Offset: 0x0007114E
		public override CardPosition OnSelectPosition(int cardId, IList<CardPosition> positions)
		{
			if (cardId == 27204312 || cardId == 31480215)
			{
				return CardPosition.FaceUpDefence;
			}
			return base.OnSelectPosition(cardId, positions);
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x00072F6C File Offset: 0x0007116C
		public override int OnSelectOption(IList<int> options)
		{
			if (options.Count == 2 && options[1] == base.Util.GetStringId(69540484, 0))
			{
				return 1;
			}
			if (options.Count == 2 && options.Contains(base.Util.GetStringId(4928565, 1)))
			{
				if (!this.isEffectByRemove() && base.Enemy.Deck.Count > 3)
				{
					return 0;
				}
				return 1;
			}
			else
			{
				if (!options.Contains(base.Util.GetStringId(44097050, 3)))
				{
					return base.OnSelectOption(options);
				}
				if (this.opt_1)
				{
					return options.IndexOf(base.Util.GetStringId(44097050, 3));
				}
				if (this.opt_0)
				{
					return 0;
				}
				return options[options.Count - 1];
			}
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x00073038 File Offset: 0x00071238
		public override int OnSelectPlace(int cardId, int player, CardLocation location, int available)
		{
			if (cardId == 0 && player == 1)
			{
				for (int i = 4; i >= 0; i--)
				{
					int zone = (int)Math.Pow(2.0, (double)i);
					if ((available & zone) > 0)
					{
						return zone;
					}
				}
			}
			if (cardId == 10389142)
			{
				if ((available & 32) > 0)
				{
					return 32;
				}
				if ((available & 64) > 0)
				{
					return 64;
				}
			}
			return base.OnSelectPlace(cardId, player, location, available);
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x000730A0 File Offset: 0x000712A0
		public override IList<ClientCard> OnSelectCard(IList<ClientCard> cards, int min, int max, int hint, bool cancelable)
		{
			if (cards.Any((ClientCard card) => card != null && card.Location == CardLocation.Extra && hint == 503 && min == 1 && max == 1))
			{
				int index = Program.Rand.Next(cards.Count<ClientCard>());
				if (index < 0 || index >= cards.Count<ClientCard>())
				{
					return null;
				}
				IList<ClientCard> res = new List<ClientCard>();
				res.Add(cards.ElementAtOrDefault(index));
				return base.Util.CheckSelectCount(res, cards, min, max);
			}
			else if (cards.Any((ClientCard card) => card != null && card.Location == CardLocation.Grave && card.Controller == 1) && hint == 503 && ((min == 1 && max == 1) || (min == 3 && max == 3)))
			{
				if (this.select_CalledbytheGrave)
				{
					this.select_CalledbytheGrave = false;
					return null;
				}
				List<ClientCard> copyCards = new List<ClientCard>(cards);
				List<int> keyCardsId = new List<int>
				{
					44097050, 15291624, 63288573, 70369116, 83152482, 72329844, 24094258, 86066372, 74997493, 85289965,
					21887175, 11738489, 98127546, 50588353, 10389142, 90590303, 27548199
				};
				List<ClientCard> preCards = new List<ClientCard>();
				List<ClientCard> resCards = new List<ClientCard>();
				foreach (ClientCard card5 in copyCards)
				{
					if (card5 != null && (keyCardsId.Contains(card5.Id) || (card5.Alias != 0 && keyCardsId.Contains(card5.Alias))))
					{
						preCards.Add(card5);
					}
					else
					{
						resCards.Add(card5);
					}
				}
				if (preCards.Count > 0)
				{
					return base.Util.CheckSelectCount(preCards, cards, min, max);
				}
				resCards = this.FilterdRepeatIdCards(resCards);
				if (resCards != null)
				{
					resCards.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
					resCards.Reverse();
					return base.Util.CheckSelectCount(resCards, cards, min, max);
				}
				copyCards.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				copyCards.Reverse();
				return base.Util.CheckSelectCount(copyCards, cards, min, max);
			}
			else if (cards.Any((ClientCard card) => card != null && card.Location == CardLocation.Extra && card.Controller == 0) && hint == 503 && min == 3 && max == 3)
			{
				List<ClientCard> repeatIdCards = this.FilterdRepeatIdCards(cards);
				if (repeatIdCards != null && repeatIdCards.Count >= 3 && this.pre_link_mode == 1)
				{
					return base.Util.CheckSelectCount(repeatIdCards, cards, min, max);
				}
				List<ClientCard> resCards2 = new List<ClientCard>();
				List<int> cardsId = new List<int> { 90590303, 27548199, 21915012, 22423493, 65741786, 44097050, 21887175, 15291624 };
				List<ClientCard> filterCards = this.CardsIdToClientCards(cardsId, cards, true, true).ToList<ClientCard>();
				if (repeatIdCards != null && repeatIdCards.Count > 0 && this.pre_link_mode == 0)
				{
					resCards2.AddRange(repeatIdCards);
				}
				if (filterCards != null && filterCards.Count > 0)
				{
					resCards2.AddRange(filterCards);
				}
				if (repeatIdCards != null && repeatIdCards.Count > 0 && this.pre_link_mode == 1)
				{
					resCards2.AddRange(repeatIdCards);
				}
				if (resCards2.Count > 0)
				{
					return base.Util.CheckSelectCount(resCards2, cards, min, max);
				}
				return null;
			}
			else
			{
				if (hint == 503)
				{
					if (cards.Any((ClientCard card) => card != null && (card.Location == CardLocation.Hand || card.Location == CardLocation.Grave) && card.Controller == 0) && min == 1 && max == 1)
					{
						if (this.select_Cards.Count > 0)
						{
							return base.Util.CheckSelectCount(this.select_Cards, cards, min, max);
						}
						IList<ClientCard> grave_cards = cards.GetMatchingCards((ClientCard card) => card != null && card.Location == CardLocation.Grave);
						if (grave_cards.Count > 0)
						{
							return base.Util.CheckSelectCount(grave_cards, cards, min, max);
						}
						return null;
					}
				}
				if (hint == 513)
				{
					if (cards.Any((ClientCard card) => card != null && card.Location == CardLocation.Removed) && min == 1 && max == 1)
					{
						List<ClientCard> m_cards = new List<ClientCard>();
						List<ClientCard> e_cards_u = new List<ClientCard>();
						List<ClientCard> e_cards_d = new List<ClientCard>();
						foreach (ClientCard card2 in cards)
						{
							if (card2 != null && card2.Controller == 0)
							{
								m_cards.Add(card2);
							}
							if (card2 != null && card2.Controller == 1 && card2.IsFaceup())
							{
								e_cards_u.Add(card2);
							}
							if (card2 != null && card2.Controller == 1 && card2.IsFacedown())
							{
								e_cards_d.Add(card2);
							}
						}
						List<ClientCard> res2 = new List<ClientCard>();
						if (e_cards_u.Count >= 0)
						{
							e_cards_u.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
							e_cards_u.Reverse();
							res2.AddRange(e_cards_u);
						}
						IList<int> cardsId2 = new List<int> { 33925864, 34447918 };
						IList<ClientCard> m_pre_cards = this.CardsIdToClientCards(cardsId2, m_cards, false, true);
						if (m_pre_cards != null && m_pre_cards.Count >= 0)
						{
							res2.AddRange(m_pre_cards);
						}
						else if (m_cards.Count >= 0)
						{
							res2.AddRange(m_cards);
						}
						if (e_cards_d.Count >= 0)
						{
							res2.AddRange(e_cards_d);
						}
						if (res2.Count <= 0)
						{
							return null;
						}
						return base.Util.CheckSelectCount(res2, cards, min, max);
					}
				}
				if (hint == 500)
				{
					if (cards.Any((ClientCard card) => card != null && card.Location == CardLocation.MonsterZone))
					{
						List<ClientCard> tRelease = new List<ClientCard>();
						List<ClientCard> nRelease = new List<ClientCard>();
						foreach (ClientCard card3 in cards)
						{
							if (card3 != null && (!card3.IsExtraCard() || card3.Id == 95474755) && !card3.IsFacedown())
							{
								if (card3.Id == 10389143 || card3.Id == 44097051)
								{
									tRelease.Add(card3);
								}
								else
								{
									nRelease.Add(card3);
								}
							}
						}
						if (this.opt_1)
						{
							IList<int> cardsId3 = new List<int> { 10389143, 44097051 };
							tRelease = this.CardsIdToClientCards(cardsId3, tRelease, false, true).ToList<ClientCard>();
							if (tRelease != null && tRelease.Count > 0)
							{
								nRelease.AddRange(tRelease);
							}
							if (nRelease.Count <= 0)
							{
								return null;
							}
							return base.Util.CheckSelectCount(nRelease, cards, min, max);
						}
						else
						{
							tRelease.AddRange(nRelease);
							if (tRelease.Count <= 0)
							{
								return null;
							}
							return base.Util.CheckSelectCount(tRelease, cards, min, max);
						}
					}
				}
				if (hint == 502)
				{
					if (cards.Any((ClientCard card) => card != null && card.Controller == 1 && (card.Location & CardLocation.Onfield) > (CardLocation)0) && min == 1 && max == 1)
					{
						ClientCard card4 = base.Util.GetBestEnemyCard(false, false);
						List<ClientCard> res3 = new List<ClientCard>();
						if (card4 != null && cards.Contains(card4))
						{
							res3.Add(card4);
							return base.Util.CheckSelectCount(res3, cards, min, max);
						}
						res3 = cards.Where((ClientCard _card) => _card != null && _card.Controller == 1).ToList<ClientCard>();
						if (res3.Count <= 0)
						{
							return null;
						}
						res3.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
						res3.Reverse();
						return base.Util.CheckSelectCount(res3, cards, min, max);
					}
				}
				if (!this.activate_pre_PrimePlanetParaisos)
				{
					return base.OnSelectCard(cards, min, max, hint, cancelable);
				}
				this.activate_pre_PrimePlanetParaisos = false;
				IList<int> cardsId4 = new List<int>();
				if (!base.Bot.HasInHand(68304193) && !this.activate_KashtiraUnicorn_1 && this.CheckRemainInDeck(68304193) > 0)
				{
					cardsId4.Add(68304193);
				}
				if (!base.Bot.HasInHand(32909498) && !this.activate_KashtiraFenrir_1 && this.CheckRemainInDeck(32909498) > 0)
				{
					cardsId4.Add(32909498);
				}
				if (!base.Bot.HasInHand(78534861) && !this.activate_KashtiraScareclaw_1 && this.CheckRemainInDeck(78534861) > 0)
				{
					cardsId4.Add(78534861);
				}
				if (!base.Bot.HasInHand(4928565) && !this.activate_KashtiraTearlaments_1 && this.CheckRemainInDeck(4928565) > 0)
				{
					cardsId4.Add(4928565);
				}
				if (!base.Bot.HasInHand(31149212) && (!this.activate_KashtiraRiseheart_2 || !this.activate_KashtiraRiseheart_1) && this.CheckRemainInDeck(31149212) > 0)
				{
					cardsId4.Add(31149212);
				}
				IList<ClientCard> copyCards2 = new List<ClientCard>(cards);
				IList<ClientCard> res4 = this.CardsIdToClientCards(cardsId4, copyCards2, true, true);
				if (res4 != null && res4.Count <= 0)
				{
					return null;
				}
				return base.Util.CheckSelectCount(res4, cards, min, max);
			}
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x00073B94 File Offset: 0x00071D94
		private int CheckRemainInDeck(int id)
		{
			if (id <= 33925864)
			{
				if (id <= 24224830)
				{
					if (id <= 10045474)
					{
						if (id == 4928565)
						{
							return base.Bot.GetRemainingCount(4928565, 1);
						}
						if (id == 10045474)
						{
							return base.Bot.GetRemainingCount(10045474, 2);
						}
					}
					else
					{
						if (id == 14558127)
						{
							return base.Bot.GetRemainingCount(14558127, 3);
						}
						if (id == 23434538)
						{
							return base.Bot.GetRemainingCount(23434538, 2);
						}
						if (id == 24224830)
						{
							return base.Bot.GetRemainingCount(24224830, 2);
						}
					}
				}
				else if (id <= 31149212)
				{
					if (id == 27204311)
					{
						return base.Bot.GetRemainingCount(27204311, 1);
					}
					if (id == 31149212)
					{
						return base.Bot.GetRemainingCount(31149212, 3);
					}
				}
				else
				{
					if (id == 31480215)
					{
						return base.Bot.GetRemainingCount(31480215, 1);
					}
					if (id == 32909498)
					{
						return base.Bot.GetRemainingCount(32909498, 3);
					}
					if (id == 33925864)
					{
						return base.Bot.GetRemainingCount(33925864, 1);
					}
				}
			}
			else if (id <= 71832012)
			{
				if (id <= 65681983)
				{
					if (id == 34447918)
					{
						return base.Bot.GetRemainingCount(34447918, 3);
					}
					if (id == 65681983)
					{
						return base.Bot.GetRemainingCount(65681983, 1);
					}
				}
				else
				{
					if (id == 68304193)
					{
						return base.Bot.GetRemainingCount(68304193, 3);
					}
					if (id == 69540484)
					{
						return base.Bot.GetRemainingCount(69540484, 3);
					}
					if (id == 71832012)
					{
						return base.Bot.GetRemainingCount(71832012, 3);
					}
				}
			}
			else if (id <= 73628505)
			{
				if (id == 72090076)
				{
					return base.Bot.GetRemainingCount(72090076, 1);
				}
				if (id == 73628505)
				{
					return base.Bot.GetRemainingCount(73628505, 1);
				}
			}
			else
			{
				if (id == 78534861)
				{
					return base.Bot.GetRemainingCount(78534861, 2);
				}
				if (id == 84211599)
				{
					return base.Bot.GetRemainingCount(84211599, 2);
				}
				if (id == 91800273)
				{
					return base.Bot.GetRemainingCount(91800273, 2);
				}
			}
			return 0;
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x00073E40 File Offset: 0x00072040
		public bool Impermanence_activate()
		{
			foreach (ClientCard i in base.Enemy.GetMonsters())
			{
				if (i.IsMonsterShouldBeDisabledBeforeItUseEffect() && !i.IsDisabled() && base.Duel.LastChainPlayer != 0)
				{
					if (base.Card.Location == CardLocation.SpellZone)
					{
						for (int j = 0; j < 5; j++)
						{
							if (base.Bot.SpellZone[j] == base.Card)
							{
								this.Impermanence_list.Add(j);
								break;
							}
						}
					}
					if (base.Card.Location == CardLocation.Hand)
					{
						base.AI.SelectPlace(this.SelectSTPlace(base.Card, true));
					}
					base.AI.SelectCard(i);
					return true;
				}
			}
			ClientCard LastChainCard = base.Util.GetLastChainCard();
			if (base.Card.Location == CardLocation.SpellZone)
			{
				int this_seq = -1;
				int that_seq = -1;
				for (int k = 0; k < 5; k++)
				{
					if (base.Bot.SpellZone[k] == base.Card)
					{
						this_seq = k;
					}
					if (LastChainCard != null && LastChainCard.Controller == 1 && LastChainCard.Location == CardLocation.SpellZone && base.Enemy.SpellZone[k] == LastChainCard)
					{
						that_seq = k;
					}
					else if (base.Duel.Player == 0 && base.Util.GetProblematicEnemySpell() != null && base.Enemy.SpellZone[k] != null && base.Enemy.SpellZone[k].IsFloodgate())
					{
						that_seq = k;
					}
				}
				if ((this_seq * that_seq >= 0 && this_seq + that_seq == 4) || base.Util.IsChainTarget(base.Card) || (LastChainCard != null && LastChainCard.Controller == 1 && LastChainCard.IsCode(18144506)))
				{
					List<ClientCard> monsters = base.Enemy.GetMonsters();
					monsters.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
					monsters.Reverse();
					foreach (ClientCard card in monsters)
					{
						if (card.IsFaceup() && !card.IsShouldNotBeTarget() && !card.IsShouldNotBeSpellTrapTarget())
						{
							base.AI.SelectCard(card);
							this.Impermanence_list.Add(this_seq);
							return true;
						}
					}
				}
			}
			if (LastChainCard == null || LastChainCard.Controller != 1 || LastChainCard.Location != CardLocation.MonsterZone || LastChainCard.IsDisabled() || LastChainCard.IsShouldNotBeTarget() || LastChainCard.IsShouldNotBeSpellTrapTarget())
			{
				return false;
			}
			if (this.is_should_not_negate() && LastChainCard.Location == CardLocation.MonsterZone)
			{
				return false;
			}
			if (base.Card.Location == CardLocation.SpellZone)
			{
				for (int l = 0; l < 5; l++)
				{
					if (base.Bot.SpellZone[l] == base.Card)
					{
						this.Impermanence_list.Add(l);
						break;
					}
				}
			}
			if (base.Card.Location == CardLocation.Hand)
			{
				base.AI.SelectPlace(this.SelectSTPlace(base.Card, true));
			}
			if (LastChainCard != null)
			{
				base.AI.SelectCard(LastChainCard);
			}
			else
			{
				List<ClientCard> monsters2 = base.Enemy.GetMonsters();
				monsters2.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				monsters2.Reverse();
				foreach (ClientCard card2 in monsters2)
				{
					if (card2.IsFaceup() && !card2.IsShouldNotBeTarget() && !card2.IsShouldNotBeSpellTrapTarget())
					{
						base.AI.SelectCard(card2);
						return true;
					}
				}
			}
			return true;
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x00074214 File Offset: 0x00072414
		public int SelectSTPlace(ClientCard card = null, bool avoid_Impermanence = false)
		{
			List<int> list = new List<int> { 0, 1, 2, 3, 4 };
			int i = list.Count;
			while (i-- > 1)
			{
				int index = Program.Rand.Next(i + 1);
				int temp = list[index];
				list[index] = list[i];
				list[i] = temp;
			}
			foreach (int seq in list)
			{
				int zone = (int)Math.Pow(2.0, (double)seq);
				if (base.Bot.SpellZone[seq] == null && (card == null || card.Location != CardLocation.Hand || !avoid_Impermanence || !this.Impermanence_list.Contains(seq)))
				{
					return zone;
				}
			}
			return 0;
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x00074314 File Offset: 0x00072514
		public bool is_should_not_negate()
		{
			ClientCard last_card = base.Util.GetLastChainCard();
			return last_card != null && last_card.Controller == 1 && last_card.IsCode(this.should_not_negate);
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x0007434C File Offset: 0x0007254C
		private List<ClientCard> FilterdRepeatIdCards(IList<ClientCard> cards)
		{
			IList<ClientCard> temp = new List<ClientCard>();
			List<ClientCard> res = new List<ClientCard>();
			using (IEnumerator<ClientCard> enumerator = cards.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ClientCard card = enumerator.Current;
					if (card != null)
					{
						if (temp.Count((ClientCard _card) => _card != null && _card.Id == card.Id) > 0 && res.Count((ClientCard _card) => _card != null && _card.Id == card.Id) <= 0)
						{
							res.Add(card);
						}
						else
						{
							temp.Add(card);
						}
					}
				}
			}
			if (res.Count >= 0)
			{
				return res;
			}
			return null;
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x000743FC File Offset: 0x000725FC
		private IList<ClientCard> CardsIdToClientCards(IList<int> cardsId, IList<ClientCard> cardsList, bool uniqueId = true, bool alias = true)
		{
			if ((cardsList != null && cardsList.Count<ClientCard>() <= 0) || (cardsId != null && cardsId.Count<int>() <= 0))
			{
				return new List<ClientCard>();
			}
			List<ClientCard> res = new List<ClientCard>();
			using (IEnumerator<int> enumerator = cardsId.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int cardid = enumerator.Current;
					List<ClientCard> cards = cardsList.Where((ClientCard card) => card != null && (card.Id == cardid || ((card.Alias != 0 && cardid == card.Alias) & alias))).ToList<ClientCard>();
					if (cards == null || cards.Count > 0)
					{
						cards.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
						if (uniqueId)
						{
							res.Add(cards.First<ClientCard>());
						}
						else
						{
							res.AddRange(cards);
						}
					}
				}
			}
			return res;
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x000744D8 File Offset: 0x000726D8
		private IList<int> ClientCardsToCardsId(IList<ClientCard> cardsList, bool uniqueId = false, bool alias = false)
		{
			if (cardsList == null)
			{
				return null;
			}
			if (cardsList.Count <= 0)
			{
				return new List<int>();
			}
			IList<int> res = new List<int>();
			foreach (ClientCard card in cardsList)
			{
				if (card != null)
				{
					if (card.Alias != 0 && alias && (!res.Contains(card.Alias) || !uniqueId))
					{
						res.Add(card.Alias);
					}
					else if (card.Id != 0 && (!res.Contains(card.Id) || !uniqueId))
					{
						res.Add(card.Id);
					}
				}
			}
			if (res.Count >= 0)
			{
				return res;
			}
			return null;
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x00074594 File Offset: 0x00072794
		private bool DefaultRepos()
		{
			return base.Card.Id != 78534861 && (base.Card.Id != 73542331 || base.Card.Attack >= 2000) && base.DefaultMonsterRepos();
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x000745D4 File Offset: 0x000727D4
		private bool CrossoutDesignatorCheck(ClientCard LastChainCard, int id)
		{
			if (LastChainCard.IsCode(id) && this.CheckRemainInDeck(id) > 0)
			{
				base.AI.SelectAnnounceID(id);
				return true;
			}
			return false;
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x000745F8 File Offset: 0x000727F8
		private bool CrossoutDesignatorEffect()
		{
			ClientCard LastChainCard = base.Util.GetLastChainCard();
			if (LastChainCard == null || base.Duel.LastChainPlayer != 1)
			{
				return false;
			}
			if (this.CrossoutDesignatorCheck(LastChainCard, 27204311) || this.CrossoutDesignatorCheck(LastChainCard, 14558127) || this.CrossoutDesignatorCheck(LastChainCard, 23434538) || this.CrossoutDesignatorCheck(LastChainCard, 72090076) || this.CrossoutDesignatorCheck(LastChainCard, 10045474) || this.CrossoutDesignatorCheck(LastChainCard, 24224830) || this.CrossoutDesignatorCheck(LastChainCard, 73628505) || this.CrossoutDesignatorCheck(LastChainCard, 84211599) || this.CrossoutDesignatorCheck(LastChainCard, 34447918) || this.CrossoutDesignatorCheck(LastChainCard, 68304193) || this.CrossoutDesignatorCheck(LastChainCard, 32909498) || this.CrossoutDesignatorCheck(LastChainCard, 69540484))
			{
				if (base.Card.Location == CardLocation.Hand)
				{
					base.AI.SelectPlace(this.SelectSTPlace(base.Card, true));
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x000746FC File Offset: 0x000728FC
		private bool MekkKnightCrusadiaAvramaxEffect()
		{
			if (base.Card.Location != CardLocation.Grave)
			{
				return true;
			}
			List<ClientCard> cards = base.Enemy.GetMonsters();
			cards.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			cards.Reverse();
			cards.AddRange(base.Enemy.GetSpells());
			if (cards.Count <= 0)
			{
				return false;
			}
			base.AI.SelectCard(cards);
			return true;
		}

		// Token: 0x0600148F RID: 5263 RVA: 0x00074767 File Offset: 0x00072967
		private bool SpellSet()
		{
			return base.Card.HasType(CardType.QuickPlay) || base.Card.HasType(CardType.Trap);
		}

		// Token: 0x06001490 RID: 5264 RVA: 0x0007478C File Offset: 0x0007298C
		private bool NibiruEffect()
		{
			return (!base.Bot.HasInMonstersZone(48626373, true, false, true) || base.Util.GetBestAttack(base.Bot) <= base.Util.GetBestAttack(base.Enemy)) && (base.Bot.GetMonsterCount() <= 0 || base.Bot.GetMonsterCount() < base.Enemy.GetMonsterCount());
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x000747FC File Offset: 0x000729FC
		private bool CalledbytheGraveEffect()
		{
			ClientCard card = base.Util.GetLastChainCard();
			if (card == null)
			{
				return false;
			}
			int id = card.Id;
			List<ClientCard> g_cards = (from g_card in base.Enemy.GetGraveyardMonsters()
				where g_card != null && g_card.Id == id
				select g_card).ToList<ClientCard>();
			if (base.Duel.LastChainPlayer != 0 && card != null)
			{
				if (base.Card.Location == CardLocation.Hand)
				{
					base.AI.SelectPlace(this.SelectSTPlace(base.Card, true));
				}
				if (card.Location == CardLocation.Grave && card.HasType(CardType.Monster))
				{
					base.AI.SelectCard(card);
				}
				else
				{
					if (g_cards.Count<ClientCard>() <= 0 || !card.HasType(CardType.Monster))
					{
						return false;
					}
					base.AI.SelectCard(g_cards);
				}
				this.select_CalledbytheGrave = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x000748D4 File Offset: 0x00072AD4
		private bool CupidPitchSummon()
		{
			if (!base.Bot.HasInMonstersZone(44097051, false, false, false) && !base.Bot.HasInMonstersZone(31480215, false, false, false))
			{
				return false;
			}
			IList<int> cardsId = new List<int> { 31480215, 44097051 };
			IList<ClientCard> cards = this.CardsIdToClientCards(cardsId, base.Bot.GetMonsters(), false, true);
			if (cards != null && cards.Count <= 0)
			{
				return false;
			}
			base.AI.SelectMaterials(cards, 0);
			return true;
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x0007495C File Offset: 0x00072B5C
		private bool BorreloadSavageDragonSummon()
		{
			if (!base.Bot.HasInMonstersZone(44097051, false, false, false) && !base.Bot.HasInMonstersZone(21915012, false, false, false))
			{
				return false;
			}
			IList<int> cardsId = new List<int> { 21915012, 44097051 };
			IList<ClientCard> cards = this.CardsIdToClientCards(cardsId, base.Bot.GetMonsters(), false, true);
			if (cards != null && cards.Count <= 0)
			{
				return false;
			}
			base.AI.SelectMaterials(cards, 0);
			this.link_mode = false;
			return true;
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x000749E9 File Offset: 0x00072BE9
		private bool BorreloadSavageDragonEffect()
		{
			if (base.ActivateDescription == -1)
			{
				base.AI.SelectCard(new int[] { 21887175, 44097050, 65741786, 22423493 });
				return true;
			}
			return true;
		}

		// Token: 0x06001495 RID: 5269 RVA: 0x00074A14 File Offset: 0x00072C14
		private bool IPSummon()
		{
			if (!base.Bot.HasInMonstersZone(10389143, false, false, false) && !base.Bot.HasInMonstersZone(44097051, false, false, false))
			{
				return false;
			}
			if (!base.Bot.HasInExtra(44097050) || !base.Bot.HasInExtra(21887175) || (!base.Bot.HasInExtra(22423493) && !base.Bot.HasInMonstersZone(22423493, false, false, true)))
			{
				return false;
			}
			List<ClientCard> cards = (from card in base.Bot.GetMonsters()
				where card != null && !card.HasType(CardType.Link) && card.IsFaceup() && card.HasType(CardType.Monster) && !card.HasType(CardType.Xyz)
				select card).ToList<ClientCard>();
			if (cards != null && cards.Count < 2 && !this.link_mode)
			{
				return false;
			}
			IList<int> cardsId = new List<int> { 10389142, 10389143 };
			IList<ClientCard> pre_cards = this.CardsIdToClientCards(cardsId, cards, false, true);
			if (pre_cards != null && pre_cards.Count >= 2)
			{
				base.AI.SelectMaterials(pre_cards, 0);
				return true;
			}
			cards.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			base.AI.SelectMaterials(cards, 0);
			return true;
		}

		// Token: 0x06001496 RID: 5270 RVA: 0x00074B48 File Offset: 0x00072D48
		private bool IPEffect()
		{
			if (!base.Bot.HasInExtra(21887175) && !base.Bot.HasInMonstersZone(22423493, false, false, true) && !base.Bot.HasInMonstersZone(44097050, false, false, true))
			{
				return false;
			}
			base.AI.SelectCard(21887175);
			IList<int> cardsId = new List<int> { 44097050, 65741786, 22423493 };
			List<ClientCard> i = new List<ClientCard>();
			IList<ClientCard> pre_m = this.CardsIdToClientCards(cardsId, base.Bot.GetMonsters(), true, true);
			if (pre_m != null && pre_m.Count <= 0)
			{
				return false;
			}
			int link_count = 0;
			foreach (ClientCard card in pre_m)
			{
				i.Add(card);
				link_count += (card.HasType(CardType.Link) ? card.LinkCount : 1);
				if (link_count >= 4)
				{
					break;
				}
			}
			if (link_count < 4)
			{
				return false;
			}
			base.AI.SelectMaterials(i, 0);
			return true;
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x00074C6C File Offset: 0x00072E6C
		private bool MechaPhantomBeastAuroradonEffect()
		{
			if (base.ActivateDescription == -1)
			{
				return true;
			}
			if (this.CheckRemainInDeck(31480215) <= 0 && this.GetEnemyOnFields().Count <= 0)
			{
				if (base.Bot.Graveyard.Count((ClientCard card) => card != null && card.HasType(CardType.Trap)) <= 0)
				{
					return false;
				}
			}
			List<ClientCard> tRelease = new List<ClientCard>();
			List<ClientCard> nRelease = new List<ClientCard>();
			foreach (ClientCard card2 in base.Bot.GetMonsters())
			{
				if (card2 != null && (!card2.IsExtraCard() || card2.Id == 95474755) && !card2.IsFacedown())
				{
					if (card2.Id == 10389143 || card2.Id == 44097051)
					{
						tRelease.Add(card2);
					}
					else
					{
						nRelease.Add(card2);
					}
				}
			}
			int num = tRelease.Count<ClientCard>() + nRelease.Count<ClientCard>();
			this.opt_0 = false;
			this.opt_1 = false;
			this.opt_2 = false;
			if (num >= 3)
			{
				if (base.Bot.Graveyard.Count((ClientCard card) => card != null && card.HasType(CardType.Trap)) > 0)
				{
					this.opt_2 = true;
				}
			}
			if (num >= 2 && this.CheckRemainInDeck(31480215) > 0)
			{
				this.opt_1 = true;
			}
			if (num >= 1 && this.GetEnemyOnFields().Count > 0)
			{
				this.opt_0 = true;
			}
			return this.opt_0 || this.opt_1 || this.opt_2;
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x00074E1C File Offset: 0x0007301C
		private bool QliphortGeniusSummon()
		{
			List<ClientCard> cards = (from card in base.Bot.GetMonsters()
				where card != null && card.Id == 10389143
				select card).ToList<ClientCard>();
			if (cards.Count <= 2)
			{
				return false;
			}
			base.AI.SelectMaterials(cards, 0);
			return true;
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x00074E78 File Offset: 0x00073078
		private bool MekkKnightCrusadiaAvramaxSummon()
		{
			IList<int> cardsId = new List<int> { 44097050, 65741786, 22423493 };
			List<ClientCard> cards = this.CardsIdToClientCards(cardsId, base.Bot.GetMonsters(), true, true).ToList<ClientCard>();
			if (cards.Count <= 0)
			{
				return false;
			}
			List<ClientCard> i = new List<ClientCard>();
			int link_count = 0;
			foreach (ClientCard card in cards)
			{
				i.Add(card);
				link_count += (card.HasType(CardType.Link) ? card.LinkCount : 1);
				if (link_count >= 4)
				{
					break;
				}
			}
			if (link_count < 4 || i.Count < 2)
			{
				return false;
			}
			base.AI.SelectMaterials(i, 0);
			return true;
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x00074F5C File Offset: 0x0007315C
		private bool MechaPhantomBeastAuroradonSummon()
		{
			if (!base.Bot.HasInMonstersZone(22423493, false, false, true) && !base.Bot.HasInMonstersZone(10389143, false, false, true))
			{
				return false;
			}
			List<ClientCard> i = new List<ClientCard>();
			List<ClientCard> m = (from card in base.Bot.GetMonsters()
				where card != null && card.Id == 22423493
				select card).ToList<ClientCard>();
			List<ClientCard> m2 = (from card in base.Bot.GetMonsters()
				where card != null && card.Id == 10389143
				select card).ToList<ClientCard>();
			if (m.Count > 0)
			{
				i.AddRange(m);
			}
			if (m2.Count > 0)
			{
				i.AddRange(m2);
			}
			m.Clear();
			int link_count = 0;
			foreach (ClientCard card2 in i)
			{
				m.Add(card2);
				link_count += (card2.HasType(CardType.Link) ? card2.LinkCount : 1);
				if (link_count >= 3)
				{
					break;
				}
			}
			if (link_count < 3)
			{
				return false;
			}
			base.AI.SelectMaterials(m, 0);
			return true;
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x000750A8 File Offset: 0x000732A8
		private bool KashtiraFenrirSummon()
		{
			if (base.Bot.HasInHandOrInSpellZone(69540484) && base.Bot.HasInHandOrInSpellZone(34447918))
			{
				this.summon_KashtiraFenrir = true;
				return true;
			}
			return false;
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x000750D8 File Offset: 0x000732D8
		private bool DiablosistheMindHackerEffect()
		{
			base.AI.SelectCard(new int[] { 32909498, 68304193, 78534861, 4928565 });
			return true;
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x000750F7 File Offset: 0x000732F7
		private bool KashtiraRiseheartSummon()
		{
			this.isSummoned = true;
			return !this.activate_KashtiraRiseheart_2;
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x00075109 File Offset: 0x00073309
		private bool DimensionShifterEffect()
		{
			if (this.activate_DimensionShifter)
			{
				return false;
			}
			this.flag = -1;
			this.flag++;
			this.activate_DimensionShifter = true;
			return true;
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x00075134 File Offset: 0x00073334
		private bool KashtiraBigBangEffect()
		{
			if (base.Card.Location == CardLocation.Removed)
			{
				base.AI.SelectCard(73542331);
				base.AI.SelectNextCard(new int[] { 68304193, 32909498, 4928565, 78534861 });
				return true;
			}
			return base.Card.Location == CardLocation.SpellZone && base.Enemy.GetMonsterCount() > 0 && (base.Bot.GetMonsterCount() <= 1 || base.Bot.GetMonsterCount() < base.Enemy.GetMonsterCount());
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x000751C8 File Offset: 0x000733C8
		private bool SpellActivate()
		{
			return base.Card.Location == CardLocation.Hand || (base.Card.IsFacedown() && (base.Card.Location == CardLocation.SpellZone || base.Card.Location == CardLocation.FieldZone));
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x00075218 File Offset: 0x00073418
		private bool PrimePlanetParaisosEffect()
		{
			if (this.SpellActivate())
			{
				this.activate_pre_PrimePlanetParaisos_2 = true;
				return true;
			}
			if (this.activate_pre_PrimePlanetParaisos_2 || this.activate_pre_PrimePlanetParaisos)
			{
				return false;
			}
			List<ClientCard> cards = (from card in this.GetEnemyOnFields()
				where card != null && !card.IsShouldNotBeTarget()
				select card).ToList<ClientCard>();
			return cards != null && cards.Count > 0;
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x00075288 File Offset: 0x00073488
		private bool DiablosistheMindHackerSummon()
		{
			List<ClientCard> cards = base.Bot.GetMonsters();
			cards.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			base.AI.SelectMaterials(cards, 0);
			return true;
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x000752C4 File Offset: 0x000734C4
		private bool XyzCheck()
		{
			return (base.Bot.GetMonsters().Count((ClientCard card) => card != null && card.IsFaceup() && card.Level == 7) < 4 || !base.Bot.HasInExtra(73542331)) && ((this.active_KashtiraPapiyas_1 || !base.Bot.HasInHandOrInSpellZone(34447918)) && (this.activate_KashtiraUnicorn_1 || !base.Bot.HasInHand(68304193) || this.isSummoned || !base.Bot.HasInSpellZone(69540484, true, true)) && (this.activate_KashtiraFenrir_1 || !base.Bot.HasInHand(32909498) || this.isSummoned || !base.Bot.HasInSpellZone(69540484, true, true)) && (this.activate_KashtiraScareclaw_1 || !base.Bot.HasInHand(78534861) || this.isSummoned || !base.Bot.HasInSpellZone(69540484, true, true)) && (this.activate_KashtiraTearlaments_1 || !base.Bot.HasInHand(4928565) || this.isSummoned || !base.Bot.HasInSpellZone(69540484, true, true)) && (this.activate_KashtiraRiseheart_2 || !base.Bot.HasInHand(31149212) || this.activate_KashtiraRiseheart_1 || this.isSummoned));
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x00075440 File Offset: 0x00073640
		private bool GalaxyTomahawkSummon()
		{
			if (this.CheckRemainInDeck(31480215) <= 0)
			{
				return false;
			}
			if (base.Bot.GetMonsterCount() >= 4)
			{
				return false;
			}
			if (this.onlyXyzSummon || this.activate_DimensionShifter || base.Bot.HasInMonstersZone(48626373, true, false, true))
			{
				return false;
			}
			if (!base.Bot.HasInExtra(21887175) && !base.Bot.HasInExtra(21915012) && !base.Bot.HasInExtra(27548199))
			{
				return false;
			}
			this.link_mode = true;
			return this.DiablosistheMindHackerSummon();
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x000754D9 File Offset: 0x000736D9
		private bool DiablosistheMindHackerSummon_2()
		{
			return !base.Bot.HasInMonstersZone(95474755, false, false, false) && this.DiablosistheMindHackerSummon();
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x000754F8 File Offset: 0x000736F8
		private bool isEffectByRemove()
		{
			return this.activate_DimensionShifter || base.Bot.HasInMonstersZone(48626373, true, false, true) || base.Enemy.HasInMonstersZone(48626373, true, false, true);
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x0007552C File Offset: 0x0007372C
		private bool NemesesCorridorEffect()
		{
			return base.Card.Location == CardLocation.Hand && (base.Bot.GetMonsterCount() <= 0 || (!this.onlyXyzSummon && base.Bot.HasInExtra(15291624)));
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x0007556C File Offset: 0x0007376C
		private bool KashtiraTearlamentsEffect()
		{
			if (base.Card.Location == CardLocation.Hand)
			{
				if (base.Duel.Player != 0)
				{
					return false;
				}
				if (base.Duel.CurrentChain.Count > 0)
				{
					return false;
				}
				if (!this.ActivateLimit(base.Card.Id))
				{
					return false;
				}
				this.activate_KashtiraTearlaments_1 = true;
				return true;
			}
			else
			{
				if (base.Card.Location == CardLocation.MonsterZone)
				{
					return (this.isEffectByRemove() && base.Enemy.Deck.Count >= 3) || (!this.isEffectByRemove() && base.Bot.Deck.Count > 10);
				}
				return base.Card.Location == CardLocation.Grave && !this.isEffectByRemove() && base.Bot.Deck.Count > 10;
			}
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x00075644 File Offset: 0x00073844
		private bool ActivateLimit(int cardId)
		{
			if (base.Bot.MonsterZone.Count<ClientCard>() <= 0 && ((base.Bot.HasInHand(32909498) && !this.activate_KashtiraFenrir_1) || (base.Bot.HasInHand(68304193) && !this.activate_KashtiraUnicorn_1)))
			{
				return false;
			}
			if (base.Bot.HasInHand(71832012) && !this.activate_pre_PrimePlanetParaisos_2)
			{
				return false;
			}
			new List<ClientCard>();
			List<ClientCard> hand_cards = base.Bot.Hand.GetMatchingCards((ClientCard card) => card != null && card.HasSetcode(393)).ToList<ClientCard>();
			List<ClientCard> grave_cards = base.Bot.Graveyard.GetMatchingCards((ClientCard card) => card != null && card.HasSetcode(393)).ToList<ClientCard>();
			List<int> cardsid = new List<int>();
			if (grave_cards.Count <= 0)
			{
				if (base.Bot.HasInSpellZone(69540484, true, true))
				{
					if (hand_cards.Count((ClientCard card) => card != null && card.Id == 69540484) > 0)
					{
						goto IL_014B;
					}
				}
				if (hand_cards.Count((ClientCard card) => card != null && card.Id == 69540484) <= 1)
				{
					goto IL_0156;
				}
				IL_014B:
				cardsid.Add(69540484);
				IL_0156:
				if (this.active_KashtiraPapiyas_1)
				{
					if (hand_cards.Count((ClientCard card) => card != null && card.Id == 34447918) > 0)
					{
						goto IL_01AE;
					}
				}
				if (hand_cards.Count((ClientCard card) => card != null && card.Id == 34447918) <= 1)
				{
					goto IL_01B9;
				}
				IL_01AE:
				cardsid.Add(34447918);
				IL_01B9:
				if (this.activate_KashtiraFenrir_1 || this.summon_KashtiraFenrir)
				{
					if (hand_cards.Count((ClientCard card) => card != null && card.Id == 32909498) > 0)
					{
						goto IL_0219;
					}
				}
				if (hand_cards.Count((ClientCard card) => card != null && card.Id == 32909498) <= 1)
				{
					goto IL_0224;
				}
				IL_0219:
				cardsid.Add(32909498);
				IL_0224:
				if (this.activate_KashtiraUnicorn_1 || this.summon_KashtiraUnicorn)
				{
					if (hand_cards.Count((ClientCard card) => card != null && card.Id == 68304193) > 0)
					{
						goto IL_0284;
					}
				}
				if (hand_cards.Count((ClientCard card) => card != null && card.Id == 68304193) <= 1)
				{
					goto IL_028F;
				}
				IL_0284:
				cardsid.Add(68304193);
				IL_028F:
				if (cardId != 78534861)
				{
					if (hand_cards.Count((ClientCard card) => card != null && card.Id == 78534861) > 0)
					{
						goto IL_02E7;
					}
				}
				if (hand_cards.Count((ClientCard card) => card != null && card.Id == 78534861) <= 1)
				{
					goto IL_02F2;
				}
				IL_02E7:
				cardsid.Add(78534861);
				IL_02F2:
				if (this.activate_KashtiraRiseheart_2)
				{
					if (hand_cards.Count((ClientCard card) => card != null && card.Id == 31149212) > 0)
					{
						goto IL_034A;
					}
				}
				if (hand_cards.Count((ClientCard card) => card != null && card.Id == 31149212) <= 1)
				{
					goto IL_0355;
				}
				IL_034A:
				cardsid.Add(31149212);
				IL_0355:
				if (cardId != 4928565)
				{
					if (hand_cards.Count((ClientCard card) => card != null && card.Id == 4928565) > 0)
					{
						cardsid.Add(4928565);
					}
				}
				if (hand_cards.Count((ClientCard card) => card != null && card.Id == 33925864) > 0)
				{
					cardsid.Add(33925864);
				}
				if (cardsid.Count <= 0)
				{
					return false;
				}
			}
			if (base.Bot.HasInHand(32909498) && !this.activate_KashtiraFenrir_1 && base.Bot.GetMonsterCount() <= 0)
			{
				return false;
			}
			if (base.Bot.HasInHand(68304193) && !this.activate_KashtiraUnicorn_1 && base.Bot.GetMonsterCount() <= 0)
			{
				return false;
			}
			this.select_Cards.Clear();
			this.select_Cards.AddRange(grave_cards);
			this.select_Cards.AddRange(this.CardsIdToClientCards(cardsid, hand_cards, false, true));
			return true;
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x00075AA0 File Offset: 0x00073CA0
		private bool KashtiraScareclawEffect()
		{
			if (base.Card.Location != CardLocation.Hand)
			{
				return false;
			}
			if (base.Duel.Player != 0)
			{
				return false;
			}
			if (base.Duel.CurrentChain.Count > 0)
			{
				return false;
			}
			if (!this.ActivateLimit(base.Card.Id))
			{
				return false;
			}
			this.activate_KashtiraScareclaw_1 = true;
			return true;
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x00075AFE File Offset: 0x00073CFE
		private bool KashtiraAriseHeartEffect()
		{
			return !base.Card.IsDisabled() && (base.ActivateDescription == base.Util.GetStringId(48626373, 1) || this.SelectEnemyCard(false, true));
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x00075B34 File Offset: 0x00073D34
		private bool KashtiraAriseHeartSummon_2()
		{
			int xcount = 0;
			int xcount_2 = 0;
			int xcount_3 = 0;
			foreach (ClientCard card in base.Bot.GetMonsters())
			{
				if (card != null && !card.IsFacedown())
				{
					if (card.Level == 7)
					{
						xcount++;
					}
					if (card.Level == 7 && card.HasSetcode(393))
					{
						xcount_2++;
					}
					if (card.Level != 7 && card.HasSetcode(393) && !card.HasType(CardType.Xyz))
					{
						xcount_3++;
					}
				}
			}
			return xcount >= 2 && (xcount_3 > 0 || xcount - xcount_2 > 0) && this.KashtiraAriseHeartSummon();
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x00075C04 File Offset: 0x00073E04
		private bool KashtiraAriseHeartSummon()
		{
			if (base.Bot.HasInMonstersZone(73542331, false, false, true) && !this.activate_KashtiraShangriIra)
			{
				return false;
			}
			if (!this.activate_KashtiraShangriIra)
			{
				return this.DiablosistheMindHackerSummon();
			}
			List<ClientCard> materials = base.Bot.GetMonsters().GetMatchingCards((ClientCard card) => !card.IsExtraCard() && card.IsFaceup() && base.Card.HasSetcode(393)).ToList<ClientCard>();
			if (materials.Count<ClientCard>() <= 0)
			{
				return false;
			}
			materials.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			materials.Sort(new Comparison<ClientCard>(CardContainer.CompareCardLevel));
			base.AI.SelectMaterials(materials, 0);
			return true;
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x00075CA0 File Offset: 0x00073EA0
		private bool DefaultSummon()
		{
			if (base.Bot.HasInSpellZone(69540484, true, true) && base.Bot.GetMonstersInMainZone().Count < 5)
			{
				this.isSummoned = true;
				if (base.Card.Id == 68304193)
				{
					this.summon_KashtiraUnicorn = true;
				}
				else if (base.Card.Id == 32909498)
				{
					this.summon_KashtiraFenrir = true;
				}
				return true;
			}
			return false;
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x00075D14 File Offset: 0x00073F14
		private bool KashtiraShangriIraSummon()
		{
			if (base.Bot.HasInMonstersZone(73542331, true, false, true))
			{
				return false;
			}
			List<ClientCard> materials = new List<ClientCard>();
			foreach (ClientCard card in base.Bot.GetMonsters())
			{
				if (materials.Count<ClientCard>() >= 2)
				{
					break;
				}
				if (card != null && card.IsFaceup() && card.Level == 7)
				{
					materials.Add(card);
				}
			}
			if (materials.Count<ClientCard>() < 2)
			{
				return false;
			}
			materials.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			base.AI.SelectMaterials(materials, 0);
			return true;
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x00075DD4 File Offset: 0x00073FD4
		private List<ClientCard> GetEnemyOnFields()
		{
			List<ClientCard> res = new List<ClientCard>();
			List<ClientCard> m_cards = base.Enemy.GetMonsters();
			List<ClientCard> s_cards = base.Enemy.GetSpells();
			if (m_cards.Count > 0)
			{
				res.AddRange(m_cards);
			}
			if (s_cards.Count > 0)
			{
				res.AddRange(s_cards);
			}
			return res;
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x00075E20 File Offset: 0x00074020
		private bool KashtiraShangriIraEffect()
		{
			if (!base.Bot.HasInMonstersZone(68304193, true, false, true) && base.Enemy.ExtraDeck.Count > 0 && this.CheckRemainInDeck(68304193) > 0)
			{
				base.AI.SelectCard(68304193);
			}
			else if (!base.Bot.HasInMonstersZone(32909498, true, false, true) && this.CheckRemainInDeck(32909498) > 0)
			{
				base.AI.SelectCard(32909498);
			}
			else if (!base.Bot.HasInMonstersZone(78534861, true, false, true) && this.CheckRemainInDeck(78534861) > 0)
			{
				base.AI.SelectCard(78534861);
			}
			else
			{
				base.AI.SelectCard(new int[] { 4928565, 68304193, 32909498, 78534861 });
			}
			this.activate_KashtiraShangriIra = true;
			return true;
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x00075F08 File Offset: 0x00074108
		private void DefaultAddCardId(List<int> cardsid)
		{
			if (!base.Bot.HasInHand(68304193) && !this.activate_KashtiraUnicorn_1)
			{
				cardsid.Add(68304193);
			}
			if (!base.Bot.HasInHand(32909498) && !this.activate_KashtiraFenrir_1)
			{
				cardsid.Add(32909498);
			}
			if (!base.Bot.HasInHand(31149212) && !this.activate_KashtiraRiseheart_2)
			{
				cardsid.Add(31149212);
			}
			if (!base.Bot.HasInHand(4928565) && !this.activate_KashtiraTearlaments_1)
			{
				cardsid.Add(4928565);
			}
			if (!base.Bot.HasInHand(78534861) && !this.activate_KashtiraScareclaw_1)
			{
				cardsid.Add(78534861);
			}
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x00075FD0 File Offset: 0x000741D0
		private bool KashtiraPapiyasEffect_2()
		{
			if (base.Card.Location == CardLocation.Removed)
			{
				List<int> cardsid = new List<int>();
				this.DefaultAddCardId(cardsid);
				if (!base.Bot.HasInExtra(48626373))
				{
					cardsid.Add(48626373);
				}
				if (!base.Bot.HasInExtra(73542331))
				{
					cardsid.Add(73542331);
				}
				cardsid.AddRange(new List<int> { 33925864, 68304193, 32909498, 31149212, 78534861, 4928565 });
				base.AI.SelectCard(cardsid);
				this.active_KashtiraPapiyas_2 = true;
				return true;
			}
			base.AI.SelectCard(32909498);
			List<int> cardsid2 = new List<int>();
			this.DefaultAddCardId(cardsid2);
			cardsid2.AddRange(new List<int> { 68304193, 32909498, 31149212, 78534861, 4928565 });
			base.AI.SelectNextCard(cardsid2);
			if (base.Card.Location == CardLocation.Hand)
			{
				base.AI.SelectPlace(this.SelectSTPlace(base.Card, true));
			}
			this.active_KashtiraPapiyas_1 = true;
			this.onlyXyzSummon = true;
			return true;
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x00076138 File Offset: 0x00074338
		private bool KashtiraPapiyasEffect()
		{
			return !this.link_mode && this.KashtiraPapiyasEffect_2();
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x0007614C File Offset: 0x0007434C
		private bool SelectEnemyCard(bool faceUp = true, bool isXyz = false)
		{
			ClientCard card = base.Util.GetLastChainCard();
			if (card != null && card.Controller == 1 && card.IsFaceup() && (card.HasType(CardType.Monster) || card.HasType(CardType.Continuous) || card.HasType(CardType.Equip) || card.HasType(CardType.Field)) && (card.Location & CardLocation.Onfield) > (CardLocation)0 && !card.IsShouldNotBeTarget())
			{
				base.AI.SelectCard(card);
				if (isXyz)
				{
					base.AI.SelectNextCard(card);
				}
				return true;
			}
			if (this.GetEnemyOnFields().Count((ClientCard _card) => _card != null && !_card.IsShouldNotBeTarget() && !(faceUp & !_card.IsFaceup()) && !_card.HasType(CardType.Token)) <= 0)
			{
				return false;
			}
			ClientCard dcard = this.GetEnemyOnFields().GetDangerousMonster(true);
			if (base.Duel.Phase < DuelPhase.BattleStart && base.Util.GetBestAttack(base.Enemy) < base.Util.GetBestAttack(base.Bot) && dcard == null)
			{
				return false;
			}
			if (dcard != null)
			{
				base.AI.SelectCard(dcard);
				if (isXyz)
				{
					base.AI.SelectNextCard(dcard);
				}
				return true;
			}
			List<ClientCard> cards = (from _card in this.GetEnemyOnFields()
				where _card != null && !_card.IsShouldNotBeTarget() && !(!_card.IsFaceup() & faceUp)
				select _card).ToList<ClientCard>();
			cards.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			cards.Reverse();
			if (cards.Count <= 0)
			{
				return false;
			}
			base.AI.SelectCard(cards);
			if (isXyz)
			{
				base.AI.SelectNextCard(cards);
			}
			return true;
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x000762C4 File Offset: 0x000744C4
		private bool KashtiraFenrirEffect()
		{
			if (base.Card.IsDisabled())
			{
				return false;
			}
			if (base.ActivateDescription == base.Util.GetStringId(32909498, 1))
			{
				IList<int> cardsId = new List<int>();
				if ((!base.Bot.HasInHandOrInSpellZone(69540484) || this.isSummoned) && !base.Bot.HasInHand(31149212) && !this.activate_KashtiraRiseheart_2 && (!this.activate_KashtiraRiseheart_1 || !this.isSummoned) && this.CheckRemainInDeck(31149212) > 0)
				{
					cardsId.Add(31149212);
				}
				if (base.Bot.HasInHandOrInSpellZone(69540484) && !this.isSummoned && !base.Bot.HasInHand(68304193) && !this.activate_KashtiraUnicorn_1 && this.CheckRemainInDeck(68304193) > 0)
				{
					cardsId.Add(68304193);
				}
				if (!base.Bot.HasInHand(4928565) && !this.activate_KashtiraTearlaments_1 && this.CheckRemainInDeck(4928565) > 0)
				{
					cardsId.Add(4928565);
				}
				if (!base.Bot.HasInHand(78534861) && !this.activate_KashtiraScareclaw_1 && this.CheckRemainInDeck(78534861) > 0)
				{
					cardsId.Add(78534861);
				}
				cardsId.Add(68304193);
				cardsId.Add(31149212);
				this.activate_KashtiraFenrir_1 = true;
				base.AI.SelectCard(cardsId);
				return true;
			}
			if (base.Duel.LastChainPlayer == 0 && base.Util.GetLastChainCard() != null && base.Util.GetLastChainCard().Id == 71832012)
			{
				return false;
			}
			List<ClientCard> cards = (from card in this.GetEnemyOnFields()
				where card != null && card.IsFaceup()
				select card).ToList<ClientCard>();
			if (cards.Count > 0)
			{
				cards.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				cards.Reverse();
				base.AI.SelectCard(cards);
			}
			return true;
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x000764CE File Offset: 0x000746CE
		private bool TerraformingEffect()
		{
			if (base.Card.Location == CardLocation.Hand)
			{
				base.AI.SelectPlace(this.SelectSTPlace(base.Card, true));
			}
			return true;
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x000764F8 File Offset: 0x000746F8
		private bool PotofProsperityEffect()
		{
			if (base.Bot.ExtraDeck.Count <= 3)
			{
				return false;
			}
			List<int> cardsId = new List<int>();
			if (!base.Bot.HasInHandOrInSpellZone(71832012) && !this.activate_PrimePlanetParaisos)
			{
				cardsId.Add(71832012);
			}
			if (!base.Bot.HasInHandOrInSpellZone(71832012) && !this.activate_PrimePlanetParaisos && this.CheckRemainInDeck(71832012) > 0)
			{
				cardsId.Add(73628505);
			}
			if (!base.Bot.HasInHand(68304193) && !this.activate_KashtiraUnicorn_1)
			{
				cardsId.Add(68304193);
			}
			if (!base.Bot.HasInHand(32909498) && !this.activate_KashtiraFenrir_1)
			{
				cardsId.Add(32909498);
			}
			if (!base.Bot.HasInHand(34447918) && !this.active_KashtiraPapiyas_1)
			{
				cardsId.Add(34447918);
			}
			if (!base.Bot.HasInHand(31149212) && !this.activate_KashtiraRiseheart_2)
			{
				cardsId.Add(31149212);
			}
			if (!base.Bot.HasInHandOrInSpellZone(69540484))
			{
				cardsId.Add(69540484);
			}
			if (!base.Bot.HasInHand(78534861) && !this.activate_KashtiraScareclaw_1)
			{
				cardsId.Add(78534861);
			}
			if (!base.Bot.HasInHand(4928565) && !this.activate_KashtiraTearlaments_1)
			{
				cardsId.Add(4928565);
			}
			if (base.Bot.HasInExtra(15291624))
			{
				if (base.Bot.Banished.Count((ClientCard card) => card != null && card.IsFaceup() && card.HasType(CardType.Monster)) > 0)
				{
					cardsId.Add(72090076);
				}
			}
			if (!base.Bot.HasInHand(23434538))
			{
				cardsId.Add(23434538);
			}
			if (!base.Bot.HasInHand(14558127))
			{
				cardsId.Add(14558127);
			}
			cardsId.AddRange(new List<int> { 65681983, 24224830, 27204311, 10045474 });
			if (base.Card.Location == CardLocation.Hand)
			{
				base.AI.SelectPlace(this.SelectSTPlace(base.Card, true));
			}
			base.AI.SelectCard(cardsId);
			return true;
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x00076768 File Offset: 0x00074968
		private bool KashtiraRiseheartEffect_2()
		{
			if (base.Card.Location != CardLocation.Hand)
			{
				if (this.CheckRemainInDeck(33925864) > 0)
				{
					if (base.Bot.GetMonsters().GetMatchingCards((ClientCard card) => card != null && card.HasType(CardType.Xyz) && card.HasSetcode(393) && card.IsFaceup() && card.Overlays.Count > 0).Count > 0)
					{
						base.AI.SelectCard(33925864);
						goto IL_030F;
					}
				}
				if (base.Bot.HasInHandOrInSpellZone(69540484) && !this.active_KashtiraBirth)
				{
					if (!base.Bot.HasInGraveyardOrInBanished(68304193) && !this.activate_KashtiraUnicorn_1 && this.CheckRemainInDeck(68304193) > 0 && !this.active_KashtiraPapiyas_1 && !base.Bot.HasInHand(34447918) && this.CheckRemainInDeck(34447918) > 0)
					{
						base.AI.SelectCard(68304193);
					}
					else if (!base.Bot.HasInGraveyardOrInBanished(32909498) && !this.activate_KashtiraFenrir_1 && this.CheckRemainInDeck(32909498) > 0)
					{
						base.AI.SelectCard(32909498);
					}
					else if (!base.Bot.HasInGraveyardOrInBanished(68304193) && !this.activate_KashtiraUnicorn_1 && this.CheckRemainInDeck(68304193) > 0)
					{
						base.AI.SelectCard(32909498);
					}
					else if (base.Bot.Graveyard.Count((ClientCard card) => card != null && card.HasType(CardType.Monster) && card.HasSetcode(393) && !card.HasType(CardType.Xyz)) + base.Bot.Banished.Count((ClientCard card_2) => card_2 != null && card_2.HasType(CardType.Monster) && card_2.HasSetcode(393) && !card_2.HasType(CardType.Xyz)) <= 0)
					{
						base.AI.SelectCard(new int[] { 32909498, 68304193, 78534861, 4928565, 31149212 });
					}
					else
					{
						base.AI.SelectCard(new int[] { 32909498, 68304193, 78534861, 4928565, 31149212 });
					}
				}
				else
				{
					if (base.Bot.HasInHand(72090076) && !this.active_NemesesCorridor)
					{
						if (base.Bot.Banished.Count((ClientCard card_2) => card_2 != null && card_2.HasType(CardType.Monster)) <= 0 && base.Bot.HasInExtra(15291624))
						{
							base.AI.SelectCard(new int[] { 32909498, 68304193, 78534861, 4928565, 31149212 });
							goto IL_030F;
						}
					}
					if (!this.active_KashtiraPapiyas_2 && this.CheckRemainInDeck(34447918) > 0)
					{
						if (base.Bot.Banished.GetMatchingCardsCount((ClientCard card) => card != null && card.IsFaceup() && card.HasSetcode(393) && card.Id != 34447918) > 0)
						{
							base.AI.SelectCard(new int[] { 34447918, 32909498, 68304193, 78534861, 4928565, 31149212 });
							goto IL_030F;
						}
					}
					base.AI.SelectCard(new int[] { 32909498, 68304193, 78534861, 4928565, 31149212 });
				}
				IL_030F:
				this.activate_KashtiraRiseheart_2 = true;
				return true;
			}
			return false;
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x00076A8E File Offset: 0x00074C8E
		private bool KashtiraRiseheartEffect()
		{
			if (base.Card.Location == CardLocation.Hand)
			{
				this.activate_KashtiraRiseheart_1 = true;
				this.onlyXyzSummon = true;
				return true;
			}
			return false;
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x00076AB0 File Offset: 0x00074CB0
		private bool KashtiraBirthEffect()
		{
			if ((base.Card.Location == CardLocation.Hand || (base.Card.Location == CardLocation.SpellZone && base.Card.IsFacedown())) && !base.Bot.HasInSpellZone(69540484, true, true))
			{
				if (base.Card.Location == CardLocation.Hand)
				{
					base.AI.SelectPlace(this.SelectSTPlace(base.Card, true));
				}
				return true;
			}
			return false;
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x00076B23 File Offset: 0x00074D23
		private bool KashtiraBirthEffect_2()
		{
			return !this.link_mode && this.KashtiraBirthEffect_3();
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x00076B38 File Offset: 0x00074D38
		private bool KashtiraBirthEffect_3()
		{
			if (base.Card.Location == CardLocation.Hand || (base.Card.Location == CardLocation.SpellZone && base.Card.IsFacedown()))
			{
				return false;
			}
			List<int> cardsid = new List<int>();
			if (!this.activate_KashtiraUnicorn_1 && !this.active_KashtiraPapiyas_1 && (base.Bot.HasInHand(34447918) || this.CheckRemainInDeck(34447918) > 0))
			{
				cardsid.Add(34447918);
			}
			if (!this.activate_KashtiraFenrir_1)
			{
				cardsid.Add(32909498);
			}
			if (!this.activate_KashtiraUnicorn_1)
			{
				cardsid.Add(68304193);
			}
			if (!this.activate_KashtiraRiseheart_2)
			{
				cardsid.Add(31149212);
			}
			cardsid.Add(32909498);
			cardsid.Add(68304193);
			cardsid.Add(4928565);
			cardsid.Add(78534861);
			cardsid.Add(31149212);
			base.AI.SelectCard(cardsid);
			return true;
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x00076C30 File Offset: 0x00074E30
		private bool KashtiraUnicornEffect()
		{
			if (base.Card.IsDisabled())
			{
				return false;
			}
			if (base.ActivateDescription == base.Util.GetStringId(68304193, 1))
			{
				if ((!base.Bot.HasInHand(34447918) && !this.active_KashtiraPapiyas_1) || (base.Bot.HasInHandOrInSpellZone(69540484) && !base.Bot.HasInHand(34447918)))
				{
					base.AI.SelectCard(new int[] { 34447918, 69540484 });
				}
				else
				{
					base.AI.SelectCard(new int[] { 69540484, 34447918 });
				}
				this.activate_KashtiraUnicorn_1 = true;
				return true;
			}
			return true;
		}

		// Token: 0x0400190D RID: 6413
		private bool isSummoned;

		// Token: 0x0400190E RID: 6414
		private bool onlyXyzSummon;

		// Token: 0x0400190F RID: 6415
		private bool activate_KashtiraUnicorn_1;

		// Token: 0x04001910 RID: 6416
		private bool activate_KashtiraFenrir_1;

		// Token: 0x04001911 RID: 6417
		private bool activate_KashtiraRiseheart_1;

		// Token: 0x04001912 RID: 6418
		private bool activate_KashtiraRiseheart_2;

		// Token: 0x04001913 RID: 6419
		private bool activate_PrimePlanetParaisos;

		// Token: 0x04001914 RID: 6420
		private bool activate_KashtiraScareclaw_1;

		// Token: 0x04001915 RID: 6421
		private bool activate_KashtiraShangriIra;

		// Token: 0x04001916 RID: 6422
		private bool activate_KashtiraTearlaments_1;

		// Token: 0x04001917 RID: 6423
		private bool activate_DimensionShifter;

		// Token: 0x04001918 RID: 6424
		private bool activate_pre_PrimePlanetParaisos;

		// Token: 0x04001919 RID: 6425
		private bool activate_pre_PrimePlanetParaisos_2;

		// Token: 0x0400191A RID: 6426
		private bool active_KashtiraPapiyas_1;

		// Token: 0x0400191B RID: 6427
		private bool active_KashtiraPapiyas_2;

		// Token: 0x0400191C RID: 6428
		private bool active_KashtiraBirth;

		// Token: 0x0400191D RID: 6429
		private bool active_NemesesCorridor;

		// Token: 0x0400191E RID: 6430
		private bool select_CalledbytheGrave;

		// Token: 0x0400191F RID: 6431
		private bool summon_KashtiraUnicorn;

		// Token: 0x04001920 RID: 6432
		private bool summon_KashtiraFenrir;

		// Token: 0x04001921 RID: 6433
		private bool link_mode;

		// Token: 0x04001922 RID: 6434
		private bool opt_0;

		// Token: 0x04001923 RID: 6435
		private bool opt_1;

		// Token: 0x04001924 RID: 6436
		private bool opt_2;

		// Token: 0x04001925 RID: 6437
		private int flag = -1;

		// Token: 0x04001926 RID: 6438
		private int pre_link_mode = -1;

		// Token: 0x04001927 RID: 6439
		private List<ClientCard> select_Cards = new List<ClientCard>();

		// Token: 0x04001928 RID: 6440
		private List<int> Impermanence_list = new List<int>();

		// Token: 0x04001929 RID: 6441
		private List<int> should_not_negate = new List<int> { 81275020, 28985331 };

		// Token: 0x02000319 RID: 793
		public class CardId
		{
			// Token: 0x0400192A RID: 6442
			public const int Nibiru = 27204311;

			// Token: 0x0400192B RID: 6443
			public const int KashtiraUnicorn = 68304193;

			// Token: 0x0400192C RID: 6444
			public const int KashtiraFenrir = 32909498;

			// Token: 0x0400192D RID: 6445
			public const int KashtiraTearlaments = 4928565;

			// Token: 0x0400192E RID: 6446
			public const int KashtiraScareclaw = 78534861;

			// Token: 0x0400192F RID: 6447
			public const int DimensionShifter = 91800273;

			// Token: 0x04001930 RID: 6448
			public const int NemesesCorridor = 72090076;

			// Token: 0x04001931 RID: 6449
			public const int KashtiraRiseheart = 31149212;

			// Token: 0x04001932 RID: 6450
			public const int G = 23434538;

			// Token: 0x04001933 RID: 6451
			public const int AshBlossom = 14558127;

			// Token: 0x04001934 RID: 6452
			public const int MechaPhantom = 31480215;

			// Token: 0x04001935 RID: 6453
			public const int Terraforming = 73628505;

			// Token: 0x04001936 RID: 6454
			public const int PotofProsperity = 84211599;

			// Token: 0x04001937 RID: 6455
			public const int KashtiraPapiyas = 34447918;

			// Token: 0x04001938 RID: 6456
			public const int CalledbytheGrave = 24224830;

			// Token: 0x04001939 RID: 6457
			public const int CrossoutDesignator = 65681983;

			// Token: 0x0400193A RID: 6458
			public const int KashtiraBirth = 69540484;

			// Token: 0x0400193B RID: 6459
			public const int PrimePlanetParaisos = 71832012;

			// Token: 0x0400193C RID: 6460
			public const int KashtiraBigBang = 33925864;

			// Token: 0x0400193D RID: 6461
			public const int InfiniteImpermanence = 10045474;

			// Token: 0x0400193E RID: 6462
			public const int ThunderDragonColossus = 15291624;

			// Token: 0x0400193F RID: 6463
			public const int BorreloadSavageDragon = 27548199;

			// Token: 0x04001940 RID: 6464
			public const int CupidPitch = 21915012;

			// Token: 0x04001941 RID: 6465
			public const int KashtiraAriseHeart = 48626373;

			// Token: 0x04001942 RID: 6466
			public const int DiablosistheMindHacker = 95474755;

			// Token: 0x04001943 RID: 6467
			public const int KashtiraShangriIra = 73542331;

			// Token: 0x04001944 RID: 6468
			public const int GalaxyTomahawk = 10389142;

			// Token: 0x04001945 RID: 6469
			public const int BagooskatheTerriblyTiredTapir = 90590303;

			// Token: 0x04001946 RID: 6470
			public const int MekkKnightCrusadiaAvramax = 21887175;

			// Token: 0x04001947 RID: 6471
			public const int MechaPhantomBeastAuroradon = 44097050;

			// Token: 0x04001948 RID: 6472
			public const int QliphortGenius = 22423493;

			// Token: 0x04001949 RID: 6473
			public const int IP = 65741786;

			// Token: 0x0400194A RID: 6474
			public const int Token = 10389143;

			// Token: 0x0400194B RID: 6475
			public const int Token_2 = 44097051;
		}
	}
}
