using System;
using System.Collections.Generic;
using System.Linq;
using YGOSharp.OCGWrapper;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x020003F7 RID: 1015
	[Deck("Tearlaments", "AI_Tearlaments", "Normal")]
	internal class TearlamentsExecutor : DefaultExecutor
	{
		// Token: 0x06001F65 RID: 8037 RVA: 0x000C1B44 File Offset: 0x000BFD44
		public TearlamentsExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.Activate, 6767771, new Func<bool>(this.TearlamentsScreamEffect_1));
			base.AddExecutor(ExecutorType.Activate, 21044178, () => base.Duel.Player != 0);
			base.AddExecutor(ExecutorType.Activate, 28226490, new Func<bool>(this.TearlamentsKaleidoHeartEffect));
			base.AddExecutor(ExecutorType.Activate, 98127546);
			base.AddExecutor(ExecutorType.SpSummon, 33158448, new Func<bool>(this.FADawnDragsterSummon));
			base.AddExecutor(ExecutorType.Activate, 92731385, new Func<bool>(this.TearlamentsKitkallosEffect_2));
			base.AddExecutor(ExecutorType.SpSummon, 98127546, new Func<bool>(this.UnderworldGoddessoftheClosedWorldSummon));
			base.AddExecutor(ExecutorType.SpSummon, 27381364, new Func<bool>(this.SprightElfSummon_2));
			base.AddExecutor(ExecutorType.SpSummon, 65741786, new Func<bool>(this.IPSummon_2));
			base.AddExecutor(ExecutorType.Activate, 84815190, new Func<bool>(this.BaronnedeFleurEffect));
			base.AddExecutor(ExecutorType.Activate, 80532587, new Func<bool>(this.ElderEntityNtssEffect));
			base.AddExecutor(ExecutorType.Activate, 69946549, new Func<bool>(this.PredaplantDragostapeliaEffect));
			base.AddExecutor(ExecutorType.Activate, 17266660, new Func<bool>(base.DefaultTrap));
			base.AddExecutor(ExecutorType.Activate, 21074344, new Func<bool>(base.DefaultTrap));
			base.AddExecutor(ExecutorType.Activate, 84330567, new Func<bool>(this.TearlamentsRulkallosEffect));
			base.AddExecutor(ExecutorType.Activate, 33158448);
			base.AddExecutor(ExecutorType.Activate, 77103950, new Func<bool>(this.PrimevalPlanetPerlereinoEffect));
			base.AddExecutor(ExecutorType.Activate, 572850, new Func<bool>(this.TearlamentsScheirenEffect));
			base.AddExecutor(ExecutorType.Activate, 92731385, new Func<bool>(this.TearlamentsKitkallosEffect));
			base.AddExecutor(ExecutorType.Activate, 27381364, new Func<bool>(this.SprightElfEffect));
			base.AddExecutor(ExecutorType.SpSummon, 98127546, new Func<bool>(this.UnderworldGoddessoftheClosedWorldSummon_3));
			base.AddExecutor(ExecutorType.Activate, 99937011, new Func<bool>(this.MudoratheSwordOracleEffect));
			base.AddExecutor(ExecutorType.Activate, 92919429, new Func<bool>(this.DivineroftheHeraldEffect));
			base.AddExecutor(ExecutorType.Activate, 74078255, new Func<bool>(this.TearlamentsMerrliEffect));
			base.AddExecutor(ExecutorType.Activate, 37961969, new Func<bool>(this.TearlamentsHavnisEffect));
			base.AddExecutor(ExecutorType.Activate, 73956664, new Func<bool>(this.TearlamentsReinoheartEffect));
			base.AddExecutor(ExecutorType.Summon, 92919429, new Func<bool>(this.DivineroftheHeraldSummon));
			base.AddExecutor(ExecutorType.Summon, 74078255, delegate
			{
				this.summoned = true;
				return true;
			});
			base.AddExecutor(ExecutorType.Summon, 73956664, delegate
			{
				this.summoned = true;
				return true;
			});
			base.AddExecutor(ExecutorType.Activate, 62320425, new Func<bool>(this.AgidotheAncientSentinelEffect));
			base.AddExecutor(ExecutorType.Activate, 25926710, new Func<bool>(this.KelbektheAncientVanguardEffect));
			base.AddExecutor(ExecutorType.Activate, 97518132, new Func<bool>(this.NaelshaddollArielEffect));
			base.AddExecutor(ExecutorType.Activate, 77723643, new Func<bool>(this.ShaddollDragonEffect));
			base.AddExecutor(ExecutorType.Activate, 40177746, new Func<bool>(this.EvaEffect));
			base.AddExecutor(ExecutorType.Activate, 74920585, new Func<bool>(this.TearlamentsSulliekEffect));
			base.AddExecutor(ExecutorType.Activate, 6767771, () => !this.AllActivated());
			base.AddExecutor(ExecutorType.Activate, 63542003, new Func<bool>(this.MudoratheSwordOracleEffect));
			base.AddExecutor(ExecutorType.Activate, 3717252, () => base.Bot.Deck.Count > 0);
			base.AddExecutor(ExecutorType.Summon, 92919429, () => !base.Bot.HasInHand(21074344) && !base.Bot.HasInHand(17266660));
			base.AddExecutor(ExecutorType.SpSummon, 21044178, new Func<bool>(this.AbyssDwellerSummon_2));
			base.AddExecutor(ExecutorType.SpSummon, 27381364, new Func<bool>(this.SprightElfSummon));
			base.AddExecutor(ExecutorType.SpSummon, 84815190, new Func<bool>(this.BaronnedeFleurSummon));
			base.AddExecutor(ExecutorType.SpSummon, 33158448, delegate
			{
				this.SetSpSummon();
				return true;
			});
			base.AddExecutor(ExecutorType.SpSummon, 21044178, new Func<bool>(this.AbyssDwellerSummon));
			base.AddExecutor(ExecutorType.Activate, 65741786, new Func<bool>(this.IPEffect));
			base.AddExecutor(ExecutorType.SpSummon, 65741786, new Func<bool>(this.IPSummon));
			base.AddExecutor(ExecutorType.SpSummon, 98127546, new Func<bool>(this.UnderworldGoddessoftheClosedWorldSummon_2));
			base.AddExecutor(ExecutorType.Activate, 38342335, new Func<bool>(this.KnightmareUnicornEffect));
			base.AddExecutor(ExecutorType.SpSummon, 38342335, new Func<bool>(this.KnightmareUnicornSummon));
			base.AddExecutor(ExecutorType.Activate, 21887175, new Func<bool>(this.MekkKnightCrusadiaAvramaxEffect));
			base.AddExecutor(ExecutorType.SpSummon, 21887175, new Func<bool>(this.MekkKnightCrusadiaAvramaxSummon));
			base.AddExecutor(ExecutorType.SpSummon, 84815190, new Func<bool>(this.BaronnedeFleurSummon_2));
			base.AddExecutor(ExecutorType.SpSummon, 84815190, () => base.Bot.HasInMonstersZone(27381364, true, false, true));
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(base.DefaultMonsterRepos));
			base.AddExecutor(ExecutorType.SpellSet, new Func<bool>(this.SpellSet));
		}

		// Token: 0x06001F66 RID: 8038 RVA: 0x0000763C File Offset: 0x0000583C
		public override bool OnSelectHand()
		{
			return true;
		}

		// Token: 0x06001F67 RID: 8039 RVA: 0x000C2AD0 File Offset: 0x000C0CD0
		public override void OnNewTurn()
		{
			List<ClientCard> cards = base.Bot.ExtraDeck.Where((ClientCard card) => card != null && card.Id == 69946549).ToList<ClientCard>();
			if (this._PredaplantDragostapelia == null && cards.Count > 0)
			{
				this._PredaplantDragostapelia = cards.FirstOrDefault<ClientCard>();
			}
			if (!this.key_send_to_deck_ids.Contains(this.all_key_card_ids[0]))
			{
				this.key_send_to_deck_ids.AddRange(this.all_key_card_ids);
			}
			if (!this.key_remove_ids.Contains(this.all_key_card_ids[0]))
			{
				this.key_remove_ids.AddRange(this.all_key_card_ids);
			}
			if (this._PredaplantDragostapelia != null && (this._PredaplantDragostapelia.Location != CardLocation.MonsterZone || this._PredaplantDragostapelia.IsFacedown()))
			{
				this.e_PredaplantDragostapelia_cards.Clear();
			}
			foreach (ClientCard card2 in new List<ClientCard>(this.e_PredaplantDragostapelia_cards))
			{
				if (card2 == null || card2.Location != CardLocation.MonsterZone || card2.IsFacedown())
				{
					this.e_PredaplantDragostapelia_cards.Remove(card2);
				}
			}
			this.activate_TearlamentsScheiren_1 = false;
			this.activate_TearlamentsScheiren_2 = false;
			this.activate_TearlamentsReinoheart_1 = false;
			this.activate_TearlamentsReinoheart_2 = false;
			this.activate_TearlamentsHavnis_1 = false;
			this.activate_TearlamentsHavnis_2 = false;
			this.activate_TearlamentsMerrli_1 = false;
			this.activate_TearlamentsMerrli_2 = false;
			this.activate_PrimevalPlanetPerlereino_1 = false;
			this.activate_PrimevalPlanetPerlereino_2 = false;
			this.activate_TearlamentsKitkallos_1 = false;
			this.activate_TearlamentsKitkallos_2 = false;
			this.activate_TearlamentsKitkallos_3 = false;
			this.activate_TearlamentsScream_1 = false;
			this.activate_TearlamentsScream_2 = false;
			this.activate_TearlamentsSulliek_1 = false;
			this.activate_TearlamentsSulliek_2 = false;
			this.activate_TearlamentsRulkallos_1 = false;
			this.activate_TearlamentsKaleidoHeart_1 = false;
			this.activate_TearlamentsKaleidoHeart_2 = false;
			this.activate_AgidotheAncientSentinel_2 = false;
			this.activate_KelbektheAncientVanguard_2 = false;
			this.activate_Eva = false;
			this.activate_DivineroftheHerald = false;
			this.summoned = false;
			this.spsummoned = false;
			this.summon_SprightElf = false;
			this.TearlamentsKitkallos_summoned = false;
			base.OnNewTurn();
		}

		// Token: 0x06001F68 RID: 8040 RVA: 0x000C2CE0 File Offset: 0x000C0EE0
		private List<ClientCard> GetZoneCards(CardLocation loc, ClientField player)
		{
			List<ClientCard> res = new List<ClientCard>();
			List<ClientCard> temp = new List<ClientCard>();
			if ((loc & CardLocation.Hand) > (CardLocation)0)
			{
				temp = player.Hand.Where((ClientCard card) => card != null).ToList<ClientCard>();
				if (temp.Count<ClientCard>() > 0)
				{
					res.AddRange(temp);
				}
			}
			if ((loc & CardLocation.MonsterZone) > (CardLocation)0)
			{
				temp = player.GetMonsters();
				if (temp.Count<ClientCard>() > 0)
				{
					res.AddRange(temp);
				}
			}
			if ((loc & CardLocation.SpellZone) > (CardLocation)0)
			{
				temp = player.GetSpells();
				if (temp.Count<ClientCard>() > 0)
				{
					res.AddRange(temp);
				}
			}
			if ((loc & CardLocation.Grave) > (CardLocation)0)
			{
				temp = player.Graveyard.Where((ClientCard card) => card != null).ToList<ClientCard>();
				if (temp.Count<ClientCard>() > 0)
				{
					res.AddRange(temp);
				}
			}
			if ((loc & CardLocation.Removed) > (CardLocation)0)
			{
				temp = player.Banished.Where((ClientCard card) => card != null).ToList<ClientCard>();
				if (temp.Count<ClientCard>() > 0)
				{
					res.AddRange(temp);
				}
			}
			if ((loc & CardLocation.Extra) > (CardLocation)0)
			{
				temp = player.ExtraDeck.Where((ClientCard card) => card != null).ToList<ClientCard>();
				if (temp.Count<ClientCard>() > 0)
				{
					res.AddRange(temp);
				}
			}
			return res;
		}

		// Token: 0x06001F69 RID: 8041 RVA: 0x000C2E50 File Offset: 0x000C1050
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
			res = res.Distinct<ClientCard>().ToList<ClientCard>();
			return res;
		}

		// Token: 0x06001F6A RID: 8042 RVA: 0x000C2F38 File Offset: 0x000C1138
		private List<ClientCard> GetKeyFusionCard(int key)
		{
			int id = 0;
			if (base.Duel.Player == 0)
			{
				switch (key)
				{
				case 0:
				case 2:
					id = 92731385;
					break;
				case 1:
				case 6:
					id = 84330567;
					break;
				case 3:
					id = 69946549;
					break;
				case 4:
					id = 28226490;
					break;
				case 5:
					id = 69946549;
					break;
				}
			}
			else
			{
				switch (key)
				{
				case 0:
				case 2:
					id = 94977269;
					break;
				case 1:
				case 4:
					id = 92731385;
					break;
				case 3:
					id = 84330567;
					break;
				case 5:
					id = 28226490;
					break;
				case 6:
					id = 69946549;
					break;
				}
			}
			List<ClientCard> res = new List<ClientCard>();
			int index = -1;
			for (int i = 0; i < this.fusionExtra.Count; i++)
			{
				ClientCard card = this.fusionExtra[i];
				if (card != null && card.Id == id)
				{
					index = i;
					res.Add(card);
					break;
				}
			}
			if (index > -1 && index < this.fusionExtra.Count)
			{
				this.fusionExtra.RemoveAt(index);
			}
			return res;
		}

		// Token: 0x06001F6B RID: 8043 RVA: 0x000C3050 File Offset: 0x000C1250
		private bool IsLastFusionCard()
		{
			int count = 0;
			if (this.activate_TearlamentsScheiren_2)
			{
				count++;
			}
			if (this.activate_TearlamentsHavnis_2)
			{
				count++;
			}
			if (this.activate_TearlamentsMerrli_2)
			{
				count++;
			}
			return count >= 2;
		}

		// Token: 0x06001F6C RID: 8044 RVA: 0x000C308C File Offset: 0x000C128C
		private bool CheckFusion(int listindex, int id)
		{
			int key = -1;
			if (base.Duel.Player == 0)
			{
				if (this.fusionExtra.Count > 0)
				{
					bool flag = this.fusionExtra.Count((ClientCard card) => card != null && card.Id == 69946549) > 0 && base.Bot.HasInMonstersZone(94977269, true, false, true);
					bool flag2;
					if ((base.Duel.Phase < DuelPhase.End && (!this.activate_TearlamentsKitkallos_1 || !this.activate_TearlamentsKitkallos_2)) || (base.Duel.Phase == DuelPhase.End && !this.activate_TearlamentsKitkallos_1 && !this.activate_TearlamentsKitkallos_2))
					{
						flag2 = this.fusionExtra.Count((ClientCard card) => card != null && card.Id == 92731385) > 0;
					}
					else
					{
						flag2 = false;
					}
					bool flag_ = flag2;
					bool flag3;
					if (this.fusionExtra.Count((ClientCard card) => card != null && card.Id == 84330567) > 0)
					{
						if (!base.Bot.HasInGraveyard(92731385))
						{
							if (base.Bot.GetMonsters().Count((ClientCard card) => card != null && card.Id == 92731385) <= 1)
							{
								flag3 = base.Duel.CurrentChain.Any((ClientCard card) => card != null && card.Controller == 0 && card != base.Card && (card.Id == 572850 || card.Id == 37961969 || card.Id == 74078255));
								goto IL_016E;
							}
						}
						flag3 = true;
					}
					else
					{
						flag3 = false;
					}
					IL_016E:
					bool flag_2 = flag3;
					bool flag_3 = this.fusionExtra.Count((ClientCard card) => card != null && card.Id == 28226490) > 0 && this.IsShouldSummonFusion(-1, -1, false, 4);
					bool flag_4 = this.fusionExtra.Count((ClientCard card) => card != null && card.Id == 69946549) > 0 && this.IsShouldSummonFusion(-1, -1, false, 8);
					bool flag_5 = this.fusionExtra.Count((ClientCard card) => card != null && card.Id == 84330567) > 0 && this.IsShouldSummonFusion(-1, -1, false, 2) && this.IsLastFusionCard();
					bool flag_6 = this.fusionExtra.Count((ClientCard card) => card != null && card.Id == 92731385) > 0;
					if (flag)
					{
						key = 3;
					}
					else if (flag_)
					{
						key = 0;
					}
					else if (flag_2)
					{
						key = 1;
					}
					else if (flag_3)
					{
						key = 4;
					}
					else if (flag_4)
					{
						key = 5;
					}
					else if (flag_5)
					{
						key = 6;
					}
					else if (flag_6)
					{
						key = 2;
					}
					if (key > -1)
					{
						List<ClientCard> fusionMaterialTemp = new List<ClientCard>(this.fusionMaterial);
						fusionMaterialTemp.Remove(base.Card);
						switch (key)
						{
						case 0:
						case 2:
							fusionMaterialTemp = fusionMaterialTemp.Where((ClientCard card) => card != null && (card.HasRace(CardRace.Aqua) || (card.HasType(CardType.Monster) && card.HasSetcode(this.SETCODE)))).ToList<ClientCard>();
							break;
						case 1:
						case 6:
							fusionMaterialTemp = fusionMaterialTemp.Where((ClientCard card) => card != null && card.Id == 92731385).ToList<ClientCard>();
							break;
						case 3:
							fusionMaterialTemp = fusionMaterialTemp.Where((ClientCard card) => card != null && card.Id == 94977269).ToList<ClientCard>();
							break;
						case 4:
							fusionMaterialTemp = fusionMaterialTemp.Where((ClientCard card) => card != null && card.HasType(CardType.Monster) && card.HasSetcode(this.SETCODE)).ToList<ClientCard>();
							break;
						case 5:
							fusionMaterialTemp = fusionMaterialTemp.Where((ClientCard card) => card != null && (card.Id != 84330567 || card.IsDisabled()) && card.HasType(CardType.Fusion)).ToList<ClientCard>();
							break;
						default:
							return false;
						}
						fusionMaterialTemp = fusionMaterialTemp.Where((ClientCard card) => card != null && (card.HasRace(CardRace.Aqua) || (card.HasType(CardType.Monster) && card.HasSetcode(this.SETCODE)))).ToList<ClientCard>();
						int chaining_key_count = this.on_chaining_cards.Count((ClientCard card) => card != null && (card.Id == 62320425 || card.Id == 25926710));
						int chaining_key_count_2 = this.on_chaining_cards.Count((ClientCard card) => card != null && card.Id == 74920585);
						List<ClientCard> list = new List<ClientCard>(base.Duel.CurrentChain);
						IList<ClientCard> current_chain_key_cards = new List<ClientCard>();
						foreach (ClientCard card2 in list)
						{
							if (card2 != null && card2.Controller != 1 && ((card2.Id == 74078255 && !this.HasInList(current_chain_key_cards, 74078255)) || (card2.Id == 37961969 && !this.HasInList(current_chain_key_cards, 37961969)) || (card2.Id == 572850 && !this.HasInList(current_chain_key_cards, 572850))))
							{
								current_chain_key_cards.Add(card2);
							}
						}
						if ((fusionMaterialTemp.Count <= 0 && chaining_key_count <= 0 && chaining_key_count_2 <= 0) || current_chain_key_cards.Count<ClientCard>() >= 2)
						{
							if (!this.remainCards.Contains(base.Card))
							{
								this.remainCards.Add(base.Card);
							}
							return false;
						}
						if (id != 572850)
						{
							if (id != 37961969)
							{
								if (id == 74078255)
								{
									this.activate_TearlamentsMerrli_2 = true;
								}
							}
							else
							{
								this.activate_TearlamentsHavnis_2 = true;
							}
						}
						else
						{
							this.activate_TearlamentsScheiren_2 = true;
						}
						if (chaining_key_count > 0 || chaining_key_count_2 > 0)
						{
							this.ran_fusion_mode_0[listindex] = true;
							this.ran_fusion_mode_1[listindex] = true;
							this.ran_fusion_mode_2[listindex] = true;
							return true;
						}
						fusionMaterialTemp.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
						fusionMaterialTemp = this.GetDefaultMaterial(fusionMaterialTemp);
						List<ClientCard> res = this.GetKeyFusionCard(key);
						this.mcard_0[listindex] = res.ElementAtOrDefault(0);
						this.mcard_1[listindex] = base.Card;
						this.mcard_2[listindex] = fusionMaterialTemp.ElementAtOrDefault(0);
						if (res.Any((ClientCard card) => card != null && card.Id == 28226490))
						{
							if (fusionMaterialTemp.Count <= 1)
							{
								this.mcard_0[listindex] = null;
								this.mcard_1[listindex] = null;
								this.mcard_2[listindex] = null;
								this.ran_fusion_mode_0[listindex] = true;
								this.ran_fusion_mode_1[listindex] = true;
								this.ran_fusion_mode_2[listindex] = true;
								return true;
							}
							if (this.mcard_2[listindex] != null && this.mcard_2[listindex].Id == 73956664)
							{
								this.mcard_3[listindex] = fusionMaterialTemp.ElementAtOrDefault(1);
							}
							else
							{
								List<ClientCard> temp = fusionMaterialTemp.Where((ClientCard card) => card != null && card.Id == 73956664).ToList<ClientCard>();
								if (temp.Count <= 0)
								{
									this.mcard_0[listindex] = null;
									this.mcard_1[listindex] = null;
									this.mcard_2[listindex] = null;
									this.ran_fusion_mode_0[listindex] = true;
									this.ran_fusion_mode_1[listindex] = true;
									this.ran_fusion_mode_2[listindex] = true;
									return true;
								}
								this.mcard_3[listindex] = temp.ElementAtOrDefault(0);
							}
						}
						if (this.mcard_1[listindex] != null)
						{
							this.fusionMaterial.Remove(this.mcard_1[listindex]);
						}
						if (this.mcard_2[listindex] != null)
						{
							this.fusionMaterial.Remove(this.mcard_2[listindex]);
						}
						if (this.mcard_3[listindex] != null)
						{
							this.fusionMaterial.Remove(this.mcard_3[listindex]);
						}
						return true;
					}
				}
				return false;
			}
			if (this.fusionExtra.Count > 0)
			{
				bool flag4;
				if (this.fusionExtra.Count((ClientCard card) => card != null && card.Id == 94977269) > 0)
				{
					if (this.fusionExtra.Count((ClientCard card) => card != null && card.Id == 92731385) <= 0 || this.activate_TearlamentsKitkallos_1 || base.Bot.GetMonstersInMainZone().Count >= 4 || this.TearlamentsKitkallos_summoned)
					{
						if (base.Bot.GetGraveyardMonsters().Any((ClientCard card) => card != null && card.HasSetcode(157)))
						{
							flag4 = base.Card.HasAttribute(CardAttribute.Dark);
							goto IL_0870;
						}
					}
				}
				flag4 = false;
				IL_0870:
				bool flag_7 = this.fusionExtra.Count((ClientCard card) => card != null && card.Id == 92731385) > 0 && !this.activate_TearlamentsKitkallos_1;
				bool flag5;
				if (this.fusionExtra.Count((ClientCard card) => card != null && card.Id == 94977269) > 0)
				{
					if (base.Bot.GetGraveyardMonsters().Any((ClientCard card) => card != null && card.HasSetcode(157)))
					{
						flag5 = base.Card.HasAttribute(CardAttribute.Dark);
						goto IL_0919;
					}
				}
				flag5 = false;
				IL_0919:
				bool flag_8 = flag5;
				bool flag_9 = this.fusionExtra.Count((ClientCard card) => card != null && card.Id == 84330567) > 0 && (base.Bot.HasInGraveyard(92731385) || base.Bot.HasInMonstersZone(92731385, false, false, true));
				bool flag6;
				if (this.fusionExtra.Count((ClientCard card) => card != null && card.Id == 28226490) > 0 && !this.activate_TearlamentsKaleidoHeart_1)
				{
					if (this.GetZoneCards(CardLocation.Onfield, base.Enemy).Count((ClientCard card) => card != null && !card.IsShouldNotBeTarget()) > 0 && this.IsShouldSummonFusion(-1, -1, false, 4))
					{
						flag6 = !base.Bot.HasInSpellZone(77103950, true, true) || (base.Bot.HasInSpellZone(77103950, true, true) && this.activate_PrimevalPlanetPerlereino_2);
						goto IL_0A22;
					}
				}
				flag6 = false;
				IL_0A22:
				bool flag_10 = flag6;
				bool flag_11 = this.fusionExtra.Count((ClientCard card) => card != null && card.Id == 69946549) > 0 && this.IsShouldSummonFusion(-1, -1, false, 8);
				bool flag_12 = this.fusionExtra.Count((ClientCard card) => card != null && card.Id == 92731385) > 0;
				if (flag4)
				{
					key = 0;
				}
				else if (flag_7)
				{
					key = 1;
				}
				else if (flag_8)
				{
					key = 2;
				}
				else if (flag_9)
				{
					key = 3;
				}
				else if (flag_10)
				{
					key = 5;
				}
				else if (flag_11)
				{
					key = 6;
				}
				else if (flag_12)
				{
					key = 4;
				}
				if (key > -1)
				{
					List<ClientCard> fusionMaterialTemp2 = new List<ClientCard>(this.fusionMaterial);
					fusionMaterialTemp2.Remove(base.Card);
					switch (key)
					{
					case 0:
					case 2:
						fusionMaterialTemp2 = fusionMaterialTemp2.Where((ClientCard card) => card != null && card.HasType(CardType.Monster) && card.HasSetcode(157)).ToList<ClientCard>();
						break;
					case 1:
					case 4:
						fusionMaterialTemp2 = fusionMaterialTemp2.Where((ClientCard card) => card != null && (card.HasRace(CardRace.Aqua) || (card.HasType(CardType.Monster) && card.HasSetcode(this.SETCODE)))).ToList<ClientCard>();
						break;
					case 3:
						fusionMaterialTemp2 = fusionMaterialTemp2.Where((ClientCard card) => card != null && card.Id == 92731385).ToList<ClientCard>();
						break;
					case 5:
						fusionMaterialTemp2 = fusionMaterialTemp2.Where((ClientCard card) => card != null && card.HasType(CardType.Monster) && card.HasSetcode(this.SETCODE)).ToList<ClientCard>();
						break;
					case 6:
						fusionMaterialTemp2 = fusionMaterialTemp2.Where((ClientCard card) => card != null && (card.Id != 84330567 || card.IsDisabled()) && card.HasType(CardType.Fusion)).ToList<ClientCard>();
						break;
					default:
						return false;
					}
					int chaining_key_count2 = this.on_chaining_cards.Count((ClientCard card) => card != null && (card.Id == 62320425 || card.Id == 25926710));
					int chaining_key_count_3 = this.on_chaining_cards.Count((ClientCard card) => card != null && card.Id == 74920585);
					if (fusionMaterialTemp2.Count <= 0 && chaining_key_count2 <= 0 && chaining_key_count_3 <= 0)
					{
						if (!this.remainCards.Contains(base.Card))
						{
							this.remainCards.Add(base.Card);
						}
						return false;
					}
					if (id != 572850)
					{
						if (id != 37961969)
						{
							if (id == 74078255)
							{
								this.activate_TearlamentsMerrli_2 = true;
							}
						}
						else
						{
							this.activate_TearlamentsHavnis_2 = true;
						}
					}
					else
					{
						this.activate_TearlamentsScheiren_2 = true;
					}
					if (chaining_key_count2 > 0 || chaining_key_count_3 > 0)
					{
						this.ran_fusion_mode_0[listindex] = true;
						this.ran_fusion_mode_1[listindex] = true;
						this.ran_fusion_mode_2[listindex] = true;
						return true;
					}
					fusionMaterialTemp2.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
					fusionMaterialTemp2 = this.GetDefaultMaterial(fusionMaterialTemp2);
					List<ClientCard> res2 = this.GetKeyFusionCard(key);
					this.mcard_0[listindex] = res2.ElementAtOrDefault(0);
					this.mcard_1[listindex] = base.Card;
					this.mcard_2[listindex] = fusionMaterialTemp2.ElementAtOrDefault(0);
					if (res2.Any((ClientCard card) => card != null && card.Id == 28226490))
					{
						if (fusionMaterialTemp2.Count <= 1)
						{
							this.mcard_0[listindex] = null;
							this.mcard_1[listindex] = null;
							this.mcard_2[listindex] = null;
							this.ran_fusion_mode_0[listindex] = true;
							this.ran_fusion_mode_1[listindex] = true;
							this.ran_fusion_mode_2[listindex] = true;
							return true;
						}
						if (this.mcard_2[listindex] != null && this.mcard_2[listindex].Id == 73956664)
						{
							this.mcard_3[listindex] = fusionMaterialTemp2.ElementAtOrDefault(1);
						}
						else
						{
							List<ClientCard> temp2 = fusionMaterialTemp2.Where((ClientCard card) => card != null && card.Id == 73956664).ToList<ClientCard>();
							if (temp2.Count <= 0)
							{
								this.mcard_0[listindex] = null;
								this.mcard_1[listindex] = null;
								this.mcard_2[listindex] = null;
								this.ran_fusion_mode_0[listindex] = true;
								this.ran_fusion_mode_1[listindex] = true;
								this.ran_fusion_mode_2[listindex] = true;
								return true;
							}
							this.mcard_3[listindex] = temp2.ElementAtOrDefault(0);
						}
					}
					if (this.mcard_1[listindex] != null)
					{
						this.fusionMaterial.Remove(this.mcard_1[listindex]);
					}
					if (this.mcard_2[listindex] != null)
					{
						this.fusionMaterial.Remove(this.mcard_2[listindex]);
					}
					if (this.mcard_3[listindex] != null)
					{
						this.fusionMaterial.Remove(this.mcard_3[listindex]);
					}
					if (listindex > 0 && this.mcard_0[0] != null && this.mcard_0[listindex] != null && this.mcard_0[listindex].Id == 94977269)
					{
						ClientCard temp3 = this.mcard_0[0];
						this.mcard_0[0] = this.mcard_0[listindex];
						this.mcard_0[listindex] = temp3;
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001F6D RID: 8045 RVA: 0x000C4018 File Offset: 0x000C2218
		private bool FusionEffect(int id)
		{
			if (!this.chainlist)
			{
				this.chainlist = true;
				this.fusionExtra = base.Bot.ExtraDeck.Where((ClientCard fcard) => fcard != null).ToList<ClientCard>();
				this.fusionMaterial = (from mcard in this.GetZoneCards(CardLocation.MonsterZone, base.Bot)
					where mcard != null && mcard.IsFaceup()
					select mcard).ToList<ClientCard>();
				this.fusionMaterial.AddRange(this.GetZoneCards((CardLocation)18, base.Bot));
			}
			int index = 0;
			if (id == 572850)
			{
				index = 0;
			}
			else if (id == 37961969)
			{
				index = 1;
			}
			else if (id == 74078255)
			{
				index = 2;
			}
			if (!this.CheckFusion(index, id))
			{
				return false;
			}
			this.SetSpSummon();
			return true;
		}

		// Token: 0x06001F6E RID: 8046 RVA: 0x000C4100 File Offset: 0x000C2300
		public override CardPosition OnSelectPosition(int cardId, IList<CardPosition> positions)
		{
			NamedCard card = NamedCard.Get(cardId);
			if (base.Duel.Turn > 1 && base.Enemy.GetMonsterCount() <= 0 && (card.Attack > 0 || cardId == 33158448) && base.Duel.Player == 0)
			{
				return CardPosition.FaceUpAttack;
			}
			if (base.Duel.Player == 1)
			{
				if (card.Attack < 2000)
				{
					return CardPosition.FaceUpDefence;
				}
				if (base.Util.GetBestAttack(base.Enemy) > card.Attack)
				{
					return CardPosition.FaceUpDefence;
				}
				return CardPosition.FaceUpAttack;
			}
			else
			{
				if (card.Attack <= 1000)
				{
					return CardPosition.FaceUpDefence;
				}
				return base.OnSelectPosition(cardId, positions);
			}
		}

		// Token: 0x06001F6F RID: 8047 RVA: 0x000C41A4 File Offset: 0x000C23A4
		public override int OnSelectPlace(int cardId, int player, CardLocation location, int available)
		{
			if (player == 0 && location == CardLocation.MonsterZone)
			{
				if (cardId == 27381364)
				{
					if ((32 & available) > 0)
					{
						return 32;
					}
					if ((64 & available) > 0)
					{
						return 64;
					}
				}
				if (cardId == 84330567 && base.Bot.HasInExtra(27381364))
				{
					if ((4 & available) > 0)
					{
						return 4;
					}
					if ((1 & available) > 0)
					{
						return 1;
					}
				}
				ClientCard card = base.Bot.MonsterZone[5];
				if (card != null && card.Id == 27381364)
				{
					if ((1 & available) > 0)
					{
						return 1;
					}
					if ((4 & available) > 0)
					{
						return 4;
					}
				}
				card = base.Bot.MonsterZone[6];
				if (card != null && card.Id == 27381364)
				{
					if ((16 & available) > 0)
					{
						return 16;
					}
					if ((4 & available) > 0)
					{
						return 4;
					}
				}
			}
			if (player == 0 && location == CardLocation.SpellZone && location != CardLocation.FieldZone)
			{
				List<int> keys = new List<int> { 0, 1, 2, 3, 4 };
				while (keys.Count > 0)
				{
					int index = Program.Rand.Next(keys.Count);
					int key = keys[index];
					int zone = 1 << key;
					if ((zone & available) > 0)
					{
						return zone;
					}
					keys.Remove(key);
				}
			}
			return base.OnSelectPlace(cardId, player, location, available);
		}

		// Token: 0x06001F70 RID: 8048 RVA: 0x000C42EC File Offset: 0x000C24EC
		public override int OnSelectOption(IList<int> options)
		{
			if (options.Count != 2 || !options.Contains(1190))
			{
				return base.OnSelectOption(options);
			}
			if (!this.TearlamentsKitkallostohand)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06001F71 RID: 8049 RVA: 0x000C4317 File Offset: 0x000C2517
		public override bool OnSelectYesNo(int desc)
		{
			if (desc == 1233663200)
			{
				this.pre_activate_PrimevalPlanetPerlereino = true;
			}
			return base.OnSelectYesNo(desc);
		}

		// Token: 0x06001F72 RID: 8050 RVA: 0x000C432F File Offset: 0x000C252F
		public override void OnSelectChain(IList<ClientCard> cards)
		{
			if (this.on_chaining_cards.Count <= 0 && cards.Count > 0)
			{
				this.on_chaining_cards = new List<ClientCard>(cards);
			}
			base.OnSelectChain(cards);
		}

		// Token: 0x06001F73 RID: 8051 RVA: 0x000C435C File Offset: 0x000C255C
		public override void OnChaining(int player, ClientCard card)
		{
			if (!this.chainlist)
			{
				this.chainlist = true;
				this.remainCards.Clear();
				this.fusionExtra = base.Bot.ExtraDeck.Where((ClientCard fcard) => fcard != null).ToList<ClientCard>();
				this.fusionMaterial = (from mcard in this.GetZoneCards(CardLocation.MonsterZone, base.Bot)
					where mcard != null && mcard.IsFaceup()
					select mcard).ToList<ClientCard>();
				this.fusionMaterial.AddRange(this.GetZoneCards((CardLocation)18, base.Bot));
			}
			base.OnChaining(player, card);
		}

		// Token: 0x06001F74 RID: 8052 RVA: 0x000C4420 File Offset: 0x000C2620
		public override void OnChainEnd()
		{
			this.remainCards.Clear();
			this.fusionExtra.Clear();
			this.fusionMaterial.Clear();
			this.on_chaining_cards.Clear();
			this.tgcard = null;
			this.no_fusion_card = null;
			for (int i = 0; i < this.mcard_0.Count; i++)
			{
				this.mcard_0[i] = null;
			}
			for (int j = 0; j < this.mcard_1.Count; j++)
			{
				this.mcard_1[j] = null;
			}
			for (int k = 0; k < this.mcard_2.Count; k++)
			{
				this.mcard_2[k] = null;
			}
			for (int l = 0; l < this.mcard_3.Count; l++)
			{
				this.mcard_3[l] = null;
			}
			for (int m = 0; m < this.ran_fusion_mode_0.Count; m++)
			{
				this.ran_fusion_mode_0[m] = false;
			}
			for (int n = 0; n < this.ran_fusion_mode_1.Count; n++)
			{
				this.ran_fusion_mode_1[n] = false;
			}
			for (int i2 = 0; i2 < this.ran_fusion_mode_2.Count; i2++)
			{
				this.ran_fusion_mode_2[i2] = false;
			}
			this.chainlist = false;
			base.OnChainEnd();
		}

		// Token: 0x06001F75 RID: 8053 RVA: 0x000C4578 File Offset: 0x000C2778
		private bool IsAvailableZone(int seq)
		{
			ClientCard card = base.Bot.MonsterZone[seq];
			if (seq == 5 || seq == 6)
			{
				ClientCard card2 = base.Bot.MonsterZone[5];
				ClientCard card3 = base.Bot.MonsterZone[6];
				if (card2 != null && card2.Controller == 0 && this.no_link_ids.Contains(card2.Id))
				{
					return false;
				}
				if (card3 != null && card3.Controller == 0 && this.no_link_ids.Contains(card3.Id))
				{
					return false;
				}
			}
			return card == null || (card.Controller == 0 && !card.IsFacedown() && (card.IsDisabled() || ((card.Id != 27381364 || !this.summon_SprightElf) && !this.no_link_ids.Contains(card.Id))));
		}

		// Token: 0x06001F76 RID: 8054 RVA: 0x000C4648 File Offset: 0x000C2848
		private bool IsAvailableLinkZone()
		{
			int zones = 0;
			foreach (ClientCard card2 in (from card in base.Bot.GetMonstersInMainZone()
				where card != null && card.IsFaceup()
				select card).ToList<ClientCard>())
			{
				zones |= card2.GetLinkedZones();
			}
			ClientCard e_card = base.Bot.MonsterZone[5];
			if (e_card != null && e_card.IsFaceup() && e_card.HasType(CardType.Link))
			{
				if (e_card.Controller == 0)
				{
					if (e_card.HasLinkMarker(CardLinkMarker.BottomLeft))
					{
						zones |= 1;
					}
					if (e_card.HasLinkMarker(CardLinkMarker.Bottom))
					{
						zones |= 2;
					}
					if (e_card.HasLinkMarker(CardLinkMarker.BottomRight))
					{
						zones |= 4;
					}
				}
				if (e_card.Controller == 1)
				{
					if (e_card.HasLinkMarker(CardLinkMarker.TopLeft))
					{
						zones |= 4;
					}
					if (e_card.HasLinkMarker(CardLinkMarker.Top))
					{
						zones |= 2;
					}
					if (e_card.HasLinkMarker(CardLinkMarker.TopRight))
					{
						zones |= 1;
					}
				}
			}
			e_card = base.Bot.MonsterZone[6];
			if (e_card != null && e_card.IsFaceup() && e_card.HasType(CardType.Link))
			{
				if (e_card.Controller == 0)
				{
					if (e_card.HasLinkMarker(CardLinkMarker.BottomLeft))
					{
						zones |= 4;
					}
					if (e_card.HasLinkMarker(CardLinkMarker.Bottom))
					{
						zones |= 8;
					}
					if (e_card.HasLinkMarker(CardLinkMarker.BottomRight))
					{
						zones |= 16;
					}
				}
				if (e_card.Controller == 1)
				{
					if (e_card.HasLinkMarker(CardLinkMarker.TopLeft))
					{
						zones |= 16;
					}
					if (e_card.HasLinkMarker(CardLinkMarker.Top))
					{
						zones |= 8;
					}
					if (e_card.HasLinkMarker(CardLinkMarker.TopRight))
					{
						zones |= 4;
					}
				}
			}
			zones &= 127;
			this.link_card = null;
			if ((zones & 1) > 0 && this.IsAvailableZone(0))
			{
				return this.GetZoneLinkCards(0);
			}
			if ((zones & 2) > 0 && this.IsAvailableZone(1))
			{
				return this.GetZoneLinkCards(1);
			}
			if ((zones & 4) > 0 && this.IsAvailableZone(2))
			{
				return this.GetZoneLinkCards(2);
			}
			if ((zones & 8) > 0 && this.IsAvailableZone(3))
			{
				return this.GetZoneLinkCards(3);
			}
			if ((zones & 16) > 0 && this.IsAvailableZone(4))
			{
				return this.GetZoneLinkCards(4);
			}
			if (this.IsAvailableZone(5))
			{
				return this.GetZoneLinkCards(5);
			}
			return this.IsAvailableZone(6) && this.GetZoneLinkCards(6);
		}

		// Token: 0x06001F77 RID: 8055 RVA: 0x000C4890 File Offset: 0x000C2A90
		private bool GetZoneLinkCards(int index)
		{
			if (index >= base.Bot.MonsterZone.Count<ClientCard>())
			{
				index = 0;
			}
			this.link_card = base.Bot.MonsterZone[index];
			return true;
		}

		// Token: 0x06001F78 RID: 8056 RVA: 0x000C48BC File Offset: 0x000C2ABC
		private List<ClientCard> GetDefaultMaterial(IList<ClientCard> cards)
		{
			List<ClientCard> first_cards = new List<ClientCard>();
			List<ClientCard> first_mzone_cards = new List<ClientCard>();
			List<ClientCard> grave_cards = new List<ClientCard>();
			List<ClientCard> mzone_cards = new List<ClientCard>();
			List<ClientCard> hand_cards = new List<ClientCard>();
			List<ClientCard> last_cards = new List<ClientCard>();
			List<ClientCard> last_cards_2 = new List<ClientCard>();
			foreach (ClientCard card3 in base.Duel.CurrentChain)
			{
				if (card3 != null && ((card3.Id == 572850 && !this.HasInList(last_cards, 572850)) || (card3.Id == 74078255 && !this.HasInList(last_cards, 74078255)) || (card3.Id == 37961969 && !this.HasInList(last_cards, 37961969))) && cards.Contains(card3))
				{
					last_cards.Add(card3);
				}
			}
			foreach (ClientCard card2 in this.remainCards)
			{
				if (card2 != null && !cards.Contains(card2))
				{
					first_cards.Add(card2);
				}
			}
			using (IEnumerator<ClientCard> enumerator = cards.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ClientCard card = enumerator.Current;
					if (card != null && !first_mzone_cards.Contains(card) && !first_cards.Contains(card) && !last_cards.Contains(card) && !grave_cards.Contains(card) && !mzone_cards.Contains(card) && !hand_cards.Contains(card))
					{
						if (card.Id == 94977269 && card.IsFaceup() && card.Location == CardLocation.MonsterZone && !card.IsDisabled())
						{
							first_cards.Add(card);
						}
						else if (card.Id != 84330567 && first_mzone_cards.Count<ClientCard>() <= 0 && card.Location == CardLocation.MonsterZone && base.Bot.GetMonstersInMainZone().Count > 4)
						{
							first_mzone_cards.Add(card);
						}
						else if (card.Location == CardLocation.Grave)
						{
							if ((((card.Id == 572850 && !this.activate_TearlamentsScheiren_2) || (card.Id == 74078255 && !this.activate_TearlamentsMerrli_2) || (card.Id == 37961969 && !this.activate_TearlamentsHavnis_2)) && last_cards.Count((ClientCard mcard) => mcard != null && mcard.Id == card.Id) <= 0 && this.on_chaining_cards.Count((ClientCard mcard) => mcard != null && mcard.Id == card.Id) > 0) || (this.on_chaining_cards.Count((ClientCard ccard) => ccard != null && ccard == card) > 0 && this.on_chaining_cards.Count((ClientCard cccard) => cccard != null && cccard.Id == card.Id) <= 1 && last_cards.Count((ClientCard mcard) => mcard != null && mcard.Id == card.Id) <= 0 && base.Duel.CurrentChain.Count((ClientCard cccard) => cccard != null && cccard.Id == card.Id) > 0) || (this.no_fusion_card != null && this.no_fusion_card == card))
							{
								last_cards.Add(card);
							}
							else
							{
								grave_cards.Add(card);
							}
						}
						else if (card.Location == CardLocation.Hand)
						{
							if (card.Id == 73956664)
							{
								hand_cards.Insert(0, card);
							}
							else if (base.Duel.Player == 1 && card.Id == 37961969 && !this.activate_TearlamentsHavnis_1)
							{
								last_cards.Add(card);
							}
							else
							{
								hand_cards.Add(card);
							}
						}
						else if (card.Location == CardLocation.MonsterZone)
						{
							if (card.Id == 92731385 && card.IsDisabled())
							{
								last_cards.Add(card);
							}
							else if (card.Id == 92731385)
							{
								last_cards_2.Insert(0, card);
							}
							else if (card.Id == 84330567 || card.Id == 28226490 || card.Id == 69946549 || (base.Duel.Player == 1 && card.Id == 94977269))
							{
								last_cards_2.Add(card);
							}
							else
							{
								mzone_cards.Add(card);
							}
						}
						else
						{
							mzone_cards.Add(card);
						}
					}
				}
			}
			first_cards.AddRange(first_mzone_cards);
			first_cards.AddRange(grave_cards);
			first_cards.AddRange(hand_cards);
			first_cards.AddRange(mzone_cards);
			first_cards.AddRange(last_cards);
			first_cards.AddRange(last_cards_2);
			return first_cards;
		}

		// Token: 0x06001F79 RID: 8057 RVA: 0x000C4E6C File Offset: 0x000C306C
		public override IList<ClientCard> OnSelectFusionMaterial(IList<ClientCard> cards, int min, int max)
		{
			if (base.AI.HaveSelectedCards())
			{
				return null;
			}
			int i = 2;
			while (i >= 0)
			{
				List<ClientCard> keys = cards.Where((ClientCard card) => card != null && card.Location != CardLocation.MonsterZone && card.Id == 73956664).ToList<ClientCard>();
				if (this.ran_fusion_mode_1[i] || this.ran_fusion_mode_2[i] || this.ran_fusion_mode_3[i])
				{
					if (this.ran_fusion_mode_1[i])
					{
						this.ran_fusion_mode_1[i] = false;
					}
					else if (this.ran_fusion_mode_2[i])
					{
						this.ran_fusion_mode_2[i] = false;
					}
					else if (this.ran_fusion_mode_3[i])
					{
						this.ran_fusion_mode_3[i] = false;
					}
					cards.ToList<ClientCard>().Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
					if (keys.Count <= 0)
					{
						return base.Util.CheckSelectCount(this.GetDefaultMaterial(cards), cards, min, max);
					}
					return base.Util.CheckSelectCount(keys, cards, min, max);
				}
				else if (this.mcard_1[i] != null)
				{
					if (keys.Count<ClientCard>() > 0)
					{
						this.mcard_1[i] = null;
						return base.Util.CheckSelectCount(keys, cards, min, max);
					}
					this.mcard_1[i] = null;
					List<ClientCard> temp = new List<ClientCard>(cards);
					temp.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
					List<ClientCard> res = this.GetDefaultMaterial(temp);
					return base.Util.CheckSelectCount(res, cards, min, max);
				}
				else
				{
					if (this.mcard_2[i] != null)
					{
						this.mcard_2[i] = null;
						List<ClientCard> temp2 = new List<ClientCard>(cards);
						temp2.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
						List<ClientCard> res = this.GetDefaultMaterial(temp2);
						return base.Util.CheckSelectCount(res, cards, min, max);
					}
					if (this.mcard_3[i] != null)
					{
						this.mcard_3[i] = null;
						List<ClientCard> temp3 = new List<ClientCard>(cards);
						temp3.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
						List<ClientCard> res = this.GetDefaultMaterial(temp3);
						return base.Util.CheckSelectCount(res, cards, min, max);
					}
					i--;
				}
			}
			return base.OnSelectFusionMaterial(cards, min, max);
		}

		// Token: 0x06001F7A RID: 8058 RVA: 0x000C50A8 File Offset: 0x000C32A8
		private bool HasInList(IList<ClientCard> cards, int id)
		{
			return cards != null && cards.Count > 0 && cards.Any((ClientCard card) => card != null && card.Id == id);
		}

		// Token: 0x06001F7B RID: 8059 RVA: 0x000C50E2 File Offset: 0x000C32E2
		private bool IsCanSpSummon()
		{
			return (!base.Bot.HasInMonstersZone(94977269, true, false, true) && !base.Enemy.HasInMonstersZone(94977269, true, false, true)) || !this.spsummoned;
		}

		// Token: 0x06001F7C RID: 8060 RVA: 0x000C5119 File Offset: 0x000C3319
		private void SetSpSummon()
		{
			if (base.Bot.HasInMonstersZone(94977269, true, false, true) || base.Enemy.HasInMonstersZone(94977269, true, false, true))
			{
				this.spsummoned = true;
			}
		}

		// Token: 0x06001F7D RID: 8061 RVA: 0x000C514C File Offset: 0x000C334C
		public override IList<ClientCard> OnSelectCard(IList<ClientCard> cards, int min, int max, int hint, bool cancelable)
		{
			if (base.AI.HaveSelectedCards())
			{
				if (this.mcard_0.All((ClientCard card) => card == null))
				{
					if (this.ran_fusion_mode_0.All((bool flag) => !flag))
					{
						goto IL_0082;
					}
				}
			}
			if (hint != 511)
			{
				if (this.pre_activate_PrimevalPlanetPerlereino)
				{
					this.pre_activate_PrimevalPlanetPerlereino = false;
					List<int> ids = this.GetCardsIdSendToHand();
					return base.Util.CheckSelectCount(this.CardsIdToClientCards(ids, cards, false, true), cards, min, max);
				}
				if (hint == 574)
				{
					IList<int> ids2 = new List<int>();
					IList<ClientCard> res = new List<ClientCard>();
					if (base.Duel.Player == 0)
					{
						if (!this.activate_TearlamentsScheiren_1 && !base.Bot.HasInHand(572850) && this.HasInList(cards, 572850) && !this.AllActivated())
						{
							ids2.Add(572850);
						}
						if (!this.activate_TearlamentsMerrli_1 && !base.Bot.HasInHand(74078255) && this.HasInList(cards, 74078255) && !this.AllActivated())
						{
							ids2.Add(74078255);
						}
						if (!base.Bot.HasInHand(37961969) && this.HasInList(cards, 37961969))
						{
							ids2.Add(37961969);
						}
						if (!this.activate_TearlamentsReinoheart_1 && !base.Bot.HasInHand(73956664) && this.HasInList(cards, 73956664))
						{
							ids2.Add(73956664);
						}
						if (ids2.Count<int>() <= 0 && !this.activate_TearlamentsKitkallos_2 && (from card in this.GetZoneCards((CardLocation)18, base.Bot)
							where card != null && card.HasSetcode(this.SETCODE) && card.HasType(CardType.Monster)
							select card).ToList<ClientCard>().Count<ClientCard>() <= 0)
						{
							if (this.HasInList(cards, 572850))
							{
								ids2.Add(572850);
							}
							if (this.HasInList(cards, 74078255))
							{
								ids2.Add(74078255);
							}
							if (this.HasInList(cards, 73956664))
							{
								ids2.Add(73956664);
							}
							if (this.HasInList(cards, 37961969))
							{
								ids2.Add(37961969);
							}
						}
						if (this.HasInList(cards, 6767771) && !this.activate_TearlamentsScream_1)
						{
							ids2.Add(6767771);
						}
						if (this.HasInList(cards, 74920585))
						{
							if (this.on_chaining_cards.Count((ClientCard card) => card != null && card.Id == 6767771) <= 0)
							{
								ids2.Add(74920585);
							}
						}
						if (this.HasInList(cards, 77103950) && !this.activate_PrimevalPlanetPerlereino_1)
						{
							ids2.Add(77103950);
						}
						if (ids2.Count > 0)
						{
							this.TearlamentsKitkallostohand = true;
						}
						else if (this.IsShouldSummonFusion(this.SETCODE, 64, false, 31))
						{
							if (!this.activate_TearlamentsHavnis_2 && this.HasInList(cards, 37961969))
							{
								ids2.Add(37961969);
							}
							if (!this.activate_TearlamentsScheiren_2 && this.HasInList(cards, 572850))
							{
								ids2.Add(572850);
							}
							if (!this.activate_TearlamentsMerrli_2 && this.HasInList(cards, 74078255))
							{
								ids2.Add(74078255);
							}
							if (ids2.Count > 0)
							{
								this.TearlamentsKitkallostohand = false;
							}
						}
					}
					else
					{
						if (this.IsShouldSummonFusion(this.SETCODE, 64, true, 31))
						{
							if (!this.activate_TearlamentsHavnis_2 && this.HasInList(cards, 37961969) && !base.Bot.HasInHand(37961969))
							{
								ids2.Add(37961969);
							}
							if (!this.activate_TearlamentsScheiren_2 && this.HasInList(cards, 572850) && !base.Bot.HasInHand(572850))
							{
								ids2.Add(572850);
							}
							if (!this.activate_TearlamentsMerrli_2 && this.HasInList(cards, 74078255) && !base.Bot.HasInHand(74078255))
							{
								ids2.Add(74078255);
							}
							if (!this.activate_TearlamentsHavnis_2 && this.HasInList(cards, 37961969) && !ids2.Contains(37961969))
							{
								ids2.Add(37961969);
							}
							if (!this.activate_TearlamentsScheiren_2 && this.HasInList(cards, 572850) && !ids2.Contains(572850))
							{
								ids2.Add(572850);
							}
							if (!this.activate_TearlamentsMerrli_2 && this.HasInList(cards, 74078255) && !ids2.Contains(74078255))
							{
								ids2.Add(74078255);
							}
						}
						if (ids2.Count > 0)
						{
							this.TearlamentsKitkallostohand = false;
						}
						else if (!base.Bot.HasInHand(37961969) && !this.activate_TearlamentsHavnis_1 && this.HasInList(cards, 37961969))
						{
							ids2.Add(37961969);
							this.TearlamentsKitkallostohand = true;
						}
						else if (!this.activate_TearlamentsReinoheart_2 && this.HasInList(cards, 73956664) && base.Bot.Hand.Any((ClientCard card) => card != null && card.HasSetcode(this.SETCODE)))
						{
							ids2.Add(73956664);
							this.TearlamentsKitkallostohand = false;
						}
						else if (!this.activate_TearlamentsSulliek_2 && this.HasInList(cards, 74920585))
						{
							ids2.Add(74920585);
							this.TearlamentsKitkallostohand = false;
						}
						else
						{
							if (!base.Bot.HasInHand(77103950) && this.HasInList(cards, 77103950))
							{
								ids2.Add(77103950);
							}
							if (!base.Bot.HasInHand(572850) && this.HasInList(cards, 572850))
							{
								ids2.Add(572850);
							}
							if (!base.Bot.HasInHand(74078255) && this.HasInList(cards, 74078255))
							{
								ids2.Add(74078255);
							}
							if (!base.Bot.HasInHand(73956664) && this.HasInList(cards, 73956664))
							{
								ids2.Add(73956664);
							}
							if (!base.Bot.HasInHand(6767771) && this.HasInList(cards, 6767771))
							{
								ids2.Add(6767771);
							}
							if (!base.Bot.HasInHand(74920585) && this.HasInList(cards, 74920585))
							{
								ids2.Add(74920585);
							}
							this.TearlamentsKitkallostohand = true;
						}
					}
					res = this.CardsIdToClientCards(ids2, cards, false, true);
					if (res.Count <= 0 || (base.Bot.HasInMonstersZone(94977269, true, false, true) && this.spsummoned))
					{
						this.TearlamentsKitkallostohand = true;
					}
					if (res.Count <= 0)
					{
						return null;
					}
					return base.Util.CheckSelectCount(res, cards, min, max);
				}
				else
				{
					if (hint == 503)
					{
						if (cards.Any((ClientCard card) => card != null && ((card.HasAttribute(CardAttribute.Light) && card.HasRace(CardRace.Fairy)) || card.Controller == 1)))
						{
							List<int> list = new List<int>();
							list.Add(21074344);
							list.Add(17266660);
							list.Add(92919429);
							IList<ClientCard> res2 = this.CardsIdToClientCards(list, cards.Where((ClientCard card) => card != null && card.Location == CardLocation.Grave).ToList<ClientCard>(), false, true);
							List<ClientCard> eres = cards.Where((ClientCard card) => card != null && card.Controller == 1 && !this.key_no_remove_ids.Contains(card.Id)).ToList<ClientCard>();
							eres.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
							eres.Reverse();
							int emax = ((eres.Count > max) ? max : ((eres.Count <= 0) ? min : eres.Count));
							int mmax = ((res2.Count > max) ? max : ((res2.Count <= 0) ? min : res2.Count));
							if (eres.Count > 0)
							{
								return base.Util.CheckSelectCount(eres, cards, emax, emax);
							}
							if (res2.Count <= 0)
							{
								return null;
							}
							return base.Util.CheckSelectCount(res2, cards, mmax, mmax);
						}
					}
					if (hint == 506)
					{
						if (cards.Any((ClientCard card) => card != null && card.Location == CardLocation.Deck))
						{
							List<int> ids3 = new List<int> { 17266660, 21074344, 92919429 };
							if (!base.Bot.HasInSpellZoneOrInGraveyard(92919429) && (base.Bot.HasInHand(17266660) || base.Bot.HasInHand(21074344)))
							{
								ids3.Clear();
								ids3.AddRange(new List<int> { 92919429, 17266660, 21074344 });
							}
							ids3.AddRange(this.GetCardsIdSendToHand().Distinct<int>());
							IList<ClientCard> res3 = this.CardsIdToClientCards(ids3, cards, false, true);
							if (res3.Count <= 0)
							{
								return null;
							}
							return base.Util.CheckSelectCount(res3, cards, max, max);
						}
					}
					if (hint == 504)
					{
						if (cards.Any((ClientCard card) => card != null && card.Location == CardLocation.MonsterZone) && min == 1 && max == 1)
						{
							List<ClientCard> h_cards = new List<ClientCard>();
							List<ClientCard> m_cards = new List<ClientCard>();
							List<ClientCard> s_cards = new List<ClientCard>();
							foreach (ClientCard card8 in cards)
							{
								if (card8 != null)
								{
									if (card8.Location == CardLocation.Hand)
									{
										h_cards.Add(card8);
									}
									else if (card8.Location == CardLocation.SpellZone)
									{
										s_cards.Add(card8);
									}
									else
									{
										m_cards.Add(card8);
									}
								}
							}
							List<ClientCard> mkeycards = new List<ClientCard>();
							List<ClientCard> hkeycards = new List<ClientCard>();
							mkeycards = cards.Where((ClientCard card) => card != null && card.Id == 92731385).ToList<ClientCard>();
							if (!this.activate_TearlamentsKitkallos_3 && mkeycards.Count > 0 && !this.AllActivated() && this.IsShouldSummonFusion(this.SETCODE, 64, false, 31) && ((this.CheckRemainInDeck(37961969) > 0 && !this.activate_TearlamentsHavnis_2) || (this.CheckRemainInDeck(74078255) > 0 && !this.activate_TearlamentsMerrli_2) || (this.CheckRemainInDeck(572850) > 0 && !this.activate_TearlamentsScheiren_2)))
							{
								return base.Util.CheckSelectCount(mkeycards, cards, min, max);
							}
							if (this.IsShouldSummonFusion(-1, -1, false, 31))
							{
								if (!this.activate_TearlamentsScheiren_2 && this.HasInList(cards, 572850))
								{
									hkeycards = cards.Where((ClientCard card) => card != null && card.Id == 572850 && card.Location == CardLocation.Hand).ToList<ClientCard>();
									mkeycards = cards.Where((ClientCard card) => card != null && card.Id == 572850 && card.Location == CardLocation.MonsterZone).ToList<ClientCard>();
								}
								else if (!this.activate_TearlamentsHavnis_2 && this.HasInList(cards, 37961969))
								{
									hkeycards = cards.Where((ClientCard card) => card != null && card.Id == 37961969 && card.Location == CardLocation.Hand).ToList<ClientCard>();
									mkeycards = cards.Where((ClientCard card) => card != null && card.Id == 37961969 && card.Location == CardLocation.MonsterZone).ToList<ClientCard>();
								}
								else if (!this.activate_TearlamentsMerrli_2 && this.HasInList(cards, 74078255))
								{
									hkeycards = cards.Where((ClientCard card) => card != null && card.Id == 74078255 && card.Location == CardLocation.Hand).ToList<ClientCard>();
									mkeycards = cards.Where((ClientCard card) => card != null && card.Id == 74078255 && card.Location == CardLocation.MonsterZone).ToList<ClientCard>();
								}
								else if (!this.activate_TearlamentsReinoheart_2 && (this.HasinZoneKeyCard(CardLocation.Hand) || (base.Bot.Hand.Count(delegate(ClientCard ccard)
								{
									if (ccard != null && ccard.HasSetcode(this.SETCODE))
									{
										return ccard != cards.Where((ClientCard card) => card != null && card.Location == CardLocation.Hand && card.Id == 73956664).FirstOrDefault<ClientCard>();
									}
									return false;
								}) > 0 && this.HasinZoneKeyCard(CardLocation.Deck))))
								{
									hkeycards = cards.Where((ClientCard card) => card != null && card.Id == 73956664 && card.Location == CardLocation.Hand).ToList<ClientCard>();
									mkeycards = cards.Where((ClientCard card) => card != null && card.Id == 73956664 && card.Location == CardLocation.MonsterZone).ToList<ClientCard>();
								}
								if (mkeycards.Count > 0 || hkeycards.Count > 0)
								{
									if (base.Bot.GetMonstersInMainZone().Count < 5)
									{
										hkeycards.AddRange(mkeycards);
										return base.Util.CheckSelectCount(hkeycards, cards, min, max);
									}
									mkeycards.AddRange(hkeycards);
									return base.Util.CheckSelectCount(mkeycards, cards, min, max);
								}
							}
							mkeycards = cards.Where((ClientCard card) => card != null && card.Id == 28226490).ToList<ClientCard>();
							if (!this.activate_TearlamentsKaleidoHeart_2 && this.IsCanSpSummon() && mkeycards.Count > 0)
							{
								return base.Util.CheckSelectCount(mkeycards, cards, min, max);
							}
							mkeycards = cards.Where((ClientCard card) => card != null && card.Id == 84330567).ToList<ClientCard>();
							if (!this.activate_TearlamentsRulkallos_2 && this.IsCanSpSummon() && mkeycards.Count > 0)
							{
								return base.Util.CheckSelectCount(mkeycards, cards, min, max);
							}
							s_cards.AddRange(h_cards);
							m_cards.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
							s_cards.AddRange(m_cards);
							return base.Util.CheckSelectCount(s_cards, cards, min, max);
						}
					}
					if (hint == 504)
					{
						if (cards.Any((ClientCard card) => card != null && card.Location == CardLocation.Hand) && min == 1 && max == 1)
						{
							IList<int> ids4 = new List<int>();
							if (!this.activate_AgidotheAncientSentinel_2)
							{
								ids4.Add(62320425);
							}
							if (!this.activate_KelbektheAncientVanguard_2)
							{
								ids4.Add(25926710);
							}
							if (this.IsShouldSummonFusion(this.SETCODE, 64, false, 31))
							{
								if (this.activate_TearlamentsScheiren_1 && !this.activate_TearlamentsScheiren_2)
								{
									ids4.Add(572850);
								}
								if (this.summoned && !this.activate_TearlamentsMerrli_2)
								{
									ids4.Add(74078255);
								}
								if (!this.activate_TearlamentsHavnis_2)
								{
									ids4.Add(37961969);
								}
								if (!this.activate_TearlamentsMerrli_2)
								{
									ids4.Add(74078255);
								}
								if (!this.activate_TearlamentsScheiren_2)
								{
									ids4.Add(572850);
								}
							}
							if (base.Enemy.GetSpellCount() > 0)
							{
								ids4.Add(77723643);
							}
							if (base.Bot.Deck.Count > 0)
							{
								ids4.Add(3717252);
							}
							if (base.Enemy.Graveyard.Count > 0)
							{
								ids4.Add(97518132);
							}
							if (!this.activate_Eva)
							{
								if (base.Bot.Graveyard.Count((ClientCard card) => card != null && card.HasAttribute(CardAttribute.Light) && card.HasRace(CardRace.Fairy)) > 0 && (this.CheckRemainInDeck(21074344) > 0 || this.CheckRemainInDeck(17266660) > 0))
								{
									ids4.Add(40177746);
								}
							}
							if (base.Bot.HasInHand(17266660) || base.Bot.HasInHand(21074344))
							{
								if (base.Bot.Hand.Count((ClientCard card) => card != null && card.HasRace(CardRace.Fairy)) > 2)
								{
									goto IL_114C;
								}
							}
							if (base.Bot.HasInHand(17266660) || base.Bot.HasInHand(21074344))
							{
								goto IL_1164;
							}
							IL_114C:
							ids4.Add(99937011);
							ids4.Add(63542003);
							IL_1164:
							if (!this.activate_TearlamentsScream_2 && this.CheckRemainInDeck(74920585) > 0)
							{
								ids4.Add(6767771);
							}
							if (!this.activate_TearlamentsSulliek_2)
							{
								ids4.Add(74920585);
							}
							if (!this.activate_TearlamentsReinoheart_2 && this.HasInList(cards, 73956664))
							{
								ids4.Add(73956664);
							}
							if (!cards.Any((ClientCard card) => card != null && !card.HasRace(CardRace.Fairy)))
							{
								ids4.Add(21074344);
								ids4.Add(17266660);
							}
							ids4 = ids4.Distinct<int>().ToList<int>();
							IList<ClientCard> res4 = this.CardsIdToClientCards(ids4, cards, false, true);
							List<ClientCard> temp = new List<ClientCard>(cards);
							foreach (ClientCard card2 in cards)
							{
								if (temp.Count <= 1)
								{
									break;
								}
								if ((!this.summoned && card2.Id == 92919429) || card2.Id == 17266660 || card2.Id == 21074344 || (!this.summoned && card2.Id == 74078255 && !this.activate_TearlamentsHavnis_1))
								{
									temp.Remove(card2);
								}
							}
							temp.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
							if (res4.Count <= 0)
							{
								return base.Util.CheckSelectCount(temp, cards, min, max);
							}
							return base.Util.CheckSelectCount(res4, cards, min, max);
						}
					}
					if (hint == 504)
					{
						if (cards.Any((ClientCard card) => card != null && (card.Location == CardLocation.Deck || card.Location == CardLocation.Extra)) && min == 1 && max == 1)
						{
							List<int> cardsid = new List<int>();
							if (this.HasInList(cards, 62320425) && !this.activate_AgidotheAncientSentinel_2 && !this.AllActivated())
							{
								if (this.HasInList(cards, 25926710) && !this.activate_KelbektheAncientVanguard_2 && base.Bot.HasInHand(62320425))
								{
									cardsid.Add(25926710);
								}
								else
								{
									cardsid.Add(62320425);
								}
							}
							else if (this.HasInList(cards, 25926710) && !this.activate_KelbektheAncientVanguard_2 && !this.AllActivated())
							{
								cardsid.Add(25926710);
							}
							else if (this.HasInList(cards, 80532587))
							{
								if (this.GetZoneCards(CardLocation.Onfield, base.Enemy).Count((ClientCard card) => card != null && !card.IsShouldNotBeTarget()) > 0)
								{
									cardsid.Add(80532587);
								}
							}
							if (this.HasInList(cards, 40177746) && !this.activate_Eva && (this.CheckRemainInDeck(17266660) > 0 || this.CheckRemainInDeck(21074344) > 0))
							{
								cardsid.Add(40177746);
							}
							if (this.HasInList(cards, 99937011))
							{
								cardsid.Add(99937011);
							}
							if (this.HasInList(cards, 63542003))
							{
								cardsid.Add(63542003);
							}
							if (this.IsShouldSummonFusion(-1, -1, false, 31))
							{
								if (cards.Any((ClientCard card) => card != null && card.Id == 37961969) && !this.activate_TearlamentsHavnis_2)
								{
									cardsid.Add(37961969);
								}
								if (cards.Any((ClientCard card) => card != null && card.Id == 74078255) && !this.activate_TearlamentsMerrli_2)
								{
									cardsid.Add(74078255);
								}
								if (cards.Any((ClientCard card) => card != null && card.Id == 572850) && !this.activate_TearlamentsScheiren_2)
								{
									cardsid.Add(572850);
								}
							}
							if (cards.Any((ClientCard card) => card != null && card.Id == 74920585) && !this.activate_TearlamentsSulliek_2)
							{
								cardsid.Add(74920585);
							}
							if (cards.Any((ClientCard card) => card != null && card.Id == 6767771) && !this.activate_TearlamentsScream_2 && this.CheckRemainInDeck(74920585) > 0)
							{
								cardsid.Add(6767771);
							}
							IList<ClientCard> res5 = this.CardsIdToClientCards(cardsid, cards, false, true);
							if (res5.Count > 0)
							{
								this.no_fusion_card = res5[0];
							}
							if (res5.Count <= 0)
							{
								return null;
							}
							return base.Util.CheckSelectCount(res5, cards, min, max);
						}
					}
					if (hint == 507)
					{
						if (cards.Any((ClientCard card) => card != null && card.Location == CardLocation.Grave))
						{
							List<ClientCard> b_cards = new List<ClientCard>();
							List<ClientCard> e_cards = new List<ClientCard>();
							List<ClientCard> e_temp_1 = new List<ClientCard>();
							List<ClientCard> e_temp_2 = new List<ClientCard>();
							List<ClientCard> e_temp_3 = new List<ClientCard>();
							List<ClientCard> e_temp_4 = new List<ClientCard>();
							foreach (ClientCard card3 in cards)
							{
								if (card3 != null)
								{
									if (card3.Controller == 0)
									{
										b_cards.Add(card3);
									}
									else
									{
										e_cards.Add(card3);
									}
								}
							}
							if (e_cards.Count <= 0 && base.Bot.Deck.Count > 2)
							{
								return null;
							}
							int imax = ((e_cards.Count > max) ? max : ((e_cards.Count < 0) ? min : e_cards.Count));
							if (base.Duel.CurrentChain == null || base.Duel.ChainTargets == null)
							{
								return base.Util.CheckSelectCount(cards, cards, imax, imax);
							}
							foreach (ClientCard card4 in base.Duel.CurrentChain)
							{
								if (card4 != null && card4.Controller != 0 && card4.Location == CardLocation.Grave && cards.Contains(card4) && !this.key_no_send_to_deck_ids.Contains(card4.Id))
								{
									if (this.key_send_to_deck_ids.Contains(card4.Id) && !e_temp_1.Contains(card4))
									{
										e_temp_1.Add(card4);
									}
									else if (!e_temp_2.Contains(card4))
									{
										e_temp_2.Add(card4);
									}
								}
							}
							foreach (ClientCard card5 in base.Duel.ChainTargets)
							{
								if (card5 != null && card5.Controller != 0 && card5.Location == CardLocation.Grave && cards.Contains(card5) && !this.key_no_send_to_deck_ids.Contains(card5.Id))
								{
									if (this.key_send_to_deck_ids.Contains(card5.Id) && !e_temp_1.Contains(card5))
									{
										e_temp_1.Add(card5);
									}
									else if (!e_temp_2.Contains(card5))
									{
										e_temp_2.Add(card5);
									}
								}
							}
							foreach (ClientCard card6 in cards)
							{
								if (card6 != null && card6.Controller != 0 && card6.Location == CardLocation.Grave && this.key_send_to_deck_ids.Contains(card6.Id) && !e_temp_1.Contains(card6) && !e_temp_2.Contains(card6))
								{
									e_temp_2.Add(card6);
								}
							}
							if (base.Bot.Deck.Count <= 0)
							{
								if (base.Duel.CurrentChain.Any((ClientCard card) => card != null && card.Controller == 0 && card.Id == 3717252))
								{
									this.bot_send_to_deck_ids = new List<int> { 572850, 74078255, 37961969 };
									IList<ClientCard> temp2 = this.CardsIdToClientCards(this.bot_send_to_deck_ids, b_cards, true, true);
									if (temp2.Count <= 0)
									{
										for (int i = 0; i < b_cards.Count; i++)
										{
											if (i >= 2)
											{
												break;
											}
											if (b_cards[i] != null && !e_temp_1.Contains(b_cards[i]))
											{
												e_temp_1.Insert(0, b_cards[i]);
											}
										}
									}
									else
									{
										int j = 0;
										while (j < temp2.Count && j < 2)
										{
											if (temp2[j] != null && !e_temp_1.Contains(temp2[j]))
											{
												e_temp_1.Insert(0, temp2[j]);
											}
											j++;
										}
									}
								}
							}
							if (base.Bot.Deck.Count < 3)
							{
								this.bot_send_to_deck_ids = new List<int>();
								IList<ClientCard> temp3 = new List<ClientCard>();
								if (b_cards.Count > 0)
								{
									if (base.Bot.ExtraDeck.Any((ClientCard card) => card != null && card.HasType(CardType.Fusion) && card.Id != 80532587))
									{
										if (this.CheckRemainInDeck(572850) <= 0 && this.HasInList(cards, 572850))
										{
											this.bot_send_to_deck_ids.Add(572850);
										}
										if (this.CheckRemainInDeck(74078255) <= 0 && this.HasInList(cards, 74078255))
										{
											this.bot_send_to_deck_ids.Add(74078255);
										}
										if (this.CheckRemainInDeck(37961969) <= 0 && this.HasInList(cards, 37961969))
										{
											this.bot_send_to_deck_ids.Add(37961969);
										}
									}
								}
								temp3 = this.CardsIdToClientCards(this.bot_send_to_deck_ids, b_cards, true, true);
								if (temp3.Count > 0)
								{
									for (int k = 0; k < temp3.Count; k++)
									{
										if (temp3[k] != null && !e_temp_4.Contains(temp3[k]) && !e_temp_1.Contains(temp3[k]) && !e_temp_2.Contains(temp3[k]))
										{
											e_temp_4.Add(temp3[k]);
										}
									}
								}
								this.bot_send_to_deck_ids.Clear();
								if (this.HasInList(cards, 97518132))
								{
									this.bot_send_to_deck_ids.Add(97518132);
								}
								if (this.CheckRemainInDeck(25926710) <= 0 && this.HasInList(cards, 25926710))
								{
									this.bot_send_to_deck_ids.Add(25926710);
								}
								if (this.CheckRemainInDeck(62320425) <= 0 && this.HasInList(cards, 62320425))
								{
									this.bot_send_to_deck_ids.Add(62320425);
								}
								if (this.CheckRemainInDeck(17266660) <= 0 && this.HasInList(cards, 17266660))
								{
									this.bot_send_to_deck_ids.Add(17266660);
								}
								if (this.CheckRemainInDeck(21074344) <= 0 && this.HasInList(cards, 21074344))
								{
									this.bot_send_to_deck_ids.Add(21074344);
								}
								temp3 = this.CardsIdToClientCards(this.bot_send_to_deck_ids, b_cards, true, true);
								if (temp3.Count > 0)
								{
									for (int l = 0; l < temp3.Count; l++)
									{
										if (temp3[l] != null && !e_temp_4.Contains(temp3[l]) && !e_temp_1.Contains(temp3[l]) && !e_temp_2.Contains(temp3[l]))
										{
											e_temp_4.Add(temp3[l]);
										}
									}
								}
								foreach (ClientCard card7 in b_cards)
								{
									if (card7 != null && !e_temp_4.Contains(card7) && !e_temp_1.Contains(card7) && !e_temp_2.Contains(card7))
									{
										e_temp_4.Add(card7);
									}
								}
							}
							e_temp_3 = e_cards.Where((ClientCard card) => card != null && !e_temp_1.Contains(card) && !e_temp_2.Contains(card) && !e_temp_4.Contains(card)).ToList<ClientCard>();
							e_temp_1.AddRange(e_temp_2);
							e_temp_1.AddRange(e_temp_4);
							e_temp_1.AddRange(e_temp_3);
							imax = ((e_temp_1.Count > max) ? max : ((e_temp_1.Count < 0) ? min : e_temp_1.Count));
							if (e_temp_1.Count <= 0)
							{
								return base.Util.CheckSelectCount(e_cards, cards, max, max);
							}
							return base.Util.CheckSelectCount(e_temp_1, cards, imax, imax);
						}
					}
					if (hint == 509)
					{
						if (cards.Any((ClientCard card) => card != null && (card.Level == 2 || card.LinkCount == 2) && card.Location == CardLocation.Grave))
						{
							IList<ClientCard> res6 = new List<ClientCard>();
							List<int> ids5 = new List<int>();
							if (base.Duel.Player == 0)
							{
								if (this.HasInList(cards, 74078255) && !this.activate_TearlamentsMerrli_1 && this.IsCanFusionSummon())
								{
									ids5.Add(74078255);
								}
								else if (this.HasInList(cards, 92919429) && !this.activate_DivineroftheHerald)
								{
									ids5.Add(92919429);
								}
								else if (this.HasInList(cards, 74078255) && !this.activate_TearlamentsMerrli_1)
								{
									ids5.Add(74078255);
								}
								else
								{
									if (this.HasInList(cards, 65741786))
									{
										if (base.Bot.ExtraDeck.Count((ClientCard card) => card != null && card.LinkCount > 2) > 0)
										{
											ids5.Add(65741786);
											goto IL_2271;
										}
									}
									ids5 = new List<int> { 92919429, 74078255, 17266660, 21074344 };
								}
							}
							else
							{
								if (this.HasInList(cards, 92919429) && !this.activate_DivineroftheHerald && this.FusionDeckCheck() && ((this.CheckRemainInDeck(62320425) > 0 && !this.activate_AgidotheAncientSentinel_2) || (this.CheckRemainInDeck(25926710) > 0 && !this.activate_KelbektheAncientVanguard_2)))
								{
									ids5.Add(92919429);
								}
								if (this.HasInList(cards, 74078255) && !this.activate_TearlamentsMerrli_1 && this.IsShouldSummonFusion(-1, -1, true, 31) && this.FusionDeckCheck())
								{
									ids5.Add(74078255);
								}
								else
								{
									if (this.HasInList(cards, 92919429) && base.Bot.HasInExtra(80532587))
									{
										if (this.GetZoneCards(CardLocation.Onfield, base.Enemy).Any((ClientCard card) => card != null && !card.IsShouldNotBeTarget()))
										{
											ids5.Add(92919429);
											goto IL_2271;
										}
									}
									if (this.HasInList(cards, 65741786))
									{
										if (base.Bot.ExtraDeck.Any((ClientCard card) => card != null && card.LinkCount > 2))
										{
											ids5.Add(65741786);
											goto IL_2271;
										}
									}
									ids5 = new List<int> { 92919429, 74078255, 17266660, 21074344 };
								}
							}
							IL_2271:
							res6 = this.CardsIdToClientCards(ids5, cards, false, true);
							if (res6.Count <= 0)
							{
								return null;
							}
							return base.Util.CheckSelectCount(res6, cards, min, max);
						}
					}
					if (hint != 573)
					{
						if (hint == 514)
						{
							if (cards.Any((ClientCard card) => card != null && card.Controller == 1))
							{
								if (this.chain_PredaplantDragostapelia != null && cards.Contains(this.chain_PredaplantDragostapelia))
								{
									List<ClientCard> res7 = new List<ClientCard> { this.chain_PredaplantDragostapelia };
									this.e_PredaplantDragostapelia_cards.Add(this.chain_PredaplantDragostapelia);
									return base.Util.CheckSelectCount(res7, cards, min, max);
								}
								return null;
							}
						}
						int m = 2;
						while (m >= 0)
						{
							if (this.ran_fusion_mode_0[m])
							{
								this.ran_fusion_mode_0[m] = false;
								if (base.Duel.Player == 0)
								{
									List<ClientCard> res8 = cards.Where((ClientCard card) => card != null && card.Id == 69946549).ToList<ClientCard>();
									if (res8.Count > 0 && base.Bot.HasInMonstersZone(94977269, true, false, true))
									{
										return base.Util.CheckSelectCount(res8, cards, min, max);
									}
									res8 = cards.Where((ClientCard card) => card != null && card.Id == 92731385).ToList<ClientCard>();
									if (res8.Count > 0 && ((!this.activate_TearlamentsKitkallos_1 && ((!this.activate_TearlamentsScheiren_2 && this.CheckRemainInDeck(572850) > 0) || (!this.activate_TearlamentsHavnis_2 && this.CheckRemainInDeck(37961969) > 0) || (!this.activate_TearlamentsMerrli_2 && this.CheckRemainInDeck(74078255) > 0))) || !this.activate_TearlamentsKitkallos_2))
									{
										this.TearlamentsKitkallos_summoned = true;
										return base.Util.CheckSelectCount(res8, cards, min, max);
									}
									res8 = cards.Where((ClientCard card) => card != null && card.Id == 28226490).ToList<ClientCard>();
									if (res8.Count > 0 && this.IsShouldSummonFusion(-1, -1, false, 4))
									{
										this.ran_fusion_mode_3[m] = true;
										return base.Util.CheckSelectCount(res8, cards, min, max);
									}
									res8 = cards.Where((ClientCard card) => card != null && card.Id == 84330567).ToList<ClientCard>();
									if (base.Bot.Graveyard.Count((ClientCard card) => card != null && card.Id == 92731385) <= 0)
									{
										if (base.Bot.MonsterZone.Count((ClientCard card) => card != null && card.Id == 92731385 && card.IsFaceup()) <= 0 || res8.Count <= 0)
										{
											res8 = cards.Where((ClientCard card) => card != null && card.Id == 69946549).ToList<ClientCard>();
											if (res8.Count > 0 && this.IsShouldSummonFusion(-1, -1, false, 8))
											{
												return base.Util.CheckSelectCount(res8, cards, min, max);
											}
											List<ClientCard> res9 = cards.Where((ClientCard card) => card != null && card.Id == 92731385).ToList<ClientCard>();
											if (res9.Count > 0)
											{
												List<ClientCard> material = this.GetDefaultMaterial(this.fusionMaterial);
												if (material.Count((ClientCard mcard) => mcard != null && mcard.Id == 92731385 && mcard.Location == CardLocation.MonsterZone) <= 0 || !this.activate_TearlamentsKitkallos_1)
												{
													if (material.Count((ClientCard mcard) => mcard != null && mcard.Id == 92731385 && mcard.Location == CardLocation.MonsterZone) <= 1)
													{
														goto IL_2743;
													}
												}
												if (material.Count <= 2)
												{
													if (res8.Count > 0)
													{
														this.activate_TearlamentsRulkallos_2 = false;
														return base.Util.CheckSelectCount(res8, cards, min, max);
													}
													this.TearlamentsKitkallos_summoned = true;
													return base.Util.CheckSelectCount(res9, cards, min, max);
												}
												IL_2743:
												return base.Util.CheckSelectCount(res9, cards, min, max);
											}
											goto IL_2A9F;
										}
									}
									this.activate_TearlamentsRulkallos_2 = false;
									return base.Util.CheckSelectCount(res8, cards, min, max);
								}
								else
								{
									List<ClientCard> res10 = cards.Where((ClientCard card) => card != null && card.Id == 92731385).ToList<ClientCard>();
									if (res10.Count > 0 && !this.activate_TearlamentsKitkallos_1 && ((!this.activate_TearlamentsScheiren_2 && this.CheckRemainInDeck(572850) > 0) || (!this.activate_TearlamentsHavnis_2 && this.CheckRemainInDeck(37961969) > 0) || (!this.activate_TearlamentsMerrli_2 && this.CheckRemainInDeck(74078255) > 0)) && base.Bot.GetMonstersInMainZone().Count < 4)
									{
										this.TearlamentsKitkallos_summoned = true;
										return base.Util.CheckSelectCount(res10, cards, min, max);
									}
									res10 = cards.Where((ClientCard card) => card != null && card.Id == 84330567).ToList<ClientCard>();
									if (res10.Count > 0)
									{
										return base.Util.CheckSelectCount(res10, cards, min, max);
									}
									res10 = cards.Where((ClientCard card) => card != null && card.Id == 28226490).ToList<ClientCard>();
									if (res10.Count > 0)
									{
										if (this.GetZoneCards(CardLocation.Onfield, base.Enemy).Count((ClientCard card) => card != null && !card.IsShouldNotBeTarget()) > 0 && this.IsShouldSummonFusion(-1, -1, false, 4) && !this.activate_TearlamentsKaleidoHeart_1 && (!base.Bot.HasInSpellZone(77103950, true, true) || (base.Bot.HasInSpellZone(77103950, true, true) && this.activate_PrimevalPlanetPerlereino_2)))
										{
											this.ran_fusion_mode_3[m] = true;
											return base.Util.CheckSelectCount(res10, cards, min, max);
										}
									}
									res10 = cards.Where((ClientCard card) => card != null && card.Id == 94977269).ToList<ClientCard>();
									if (res10.Count > 0)
									{
										return base.Util.CheckSelectCount(res10, cards, min, max);
									}
									res10 = cards.Where((ClientCard card) => card != null && card.Id == 69946549).ToList<ClientCard>();
									if (res10.Count > 0 && this.IsShouldSummonFusion(-1, -1, false, 8))
									{
										return base.Util.CheckSelectCount(res10, cards, min, max);
									}
									res10 = cards.Where((ClientCard card) => card != null && card.Id == 28226490).ToList<ClientCard>();
									if (res10.Count > 0 && this.IsShouldSummonFusion(-1, -1, false, 4))
									{
										return base.Util.CheckSelectCount(res10, cards, min, max);
									}
									res10 = cards.Where((ClientCard card) => card != null && card.Id == 92731385).ToList<ClientCard>();
									if (res10.Count > 0)
									{
										this.TearlamentsKitkallos_summoned = true;
										return base.Util.CheckSelectCount(res10, cards, min, max);
									}
									return null;
								}
							}
							IL_2A9F:
							if (this.mcard_0[m] != null)
							{
								if (cards.Contains(this.mcard_0[m]))
								{
									List<ClientCard> res11 = new List<ClientCard> { this.mcard_0[m] };
									this.mcard_0[m] = null;
									return base.Util.CheckSelectCount(res11, cards, min, max);
								}
								return null;
							}
							else
							{
								m--;
							}
						}
						return base.OnSelectCard(cards, min, max, hint, cancelable);
					}
					if (this.chain_TearlamentsSulliek != null && cards.Contains(this.chain_TearlamentsSulliek))
					{
						List<ClientCard> res12 = new List<ClientCard> { this.chain_TearlamentsSulliek };
						return base.Util.CheckSelectCount(res12, cards, min, max);
					}
					return null;
				}
			}
			IL_0082:
			return null;
		}

		// Token: 0x06001F7E RID: 8062 RVA: 0x000C7CDC File Offset: 0x000C5EDC
		private bool IsCanFusionSummon()
		{
			if ((!this.activate_TearlamentsMerrli_2 && this.CheckRemainInDeck(74078255) > 0) || (!this.activate_TearlamentsHavnis_2 && this.CheckRemainInDeck(37961969) > 0) || (!this.activate_TearlamentsScheiren_2 && this.CheckRemainInDeck(572850) > 0))
			{
				return base.Bot.ExtraDeck.Count((ClientCard card) => card != null && card.HasType(CardType.Fusion) && card.Id != 80532587) > 0;
			}
			return false;
		}

		// Token: 0x06001F7F RID: 8063 RVA: 0x000C7D60 File Offset: 0x000C5F60
		private bool BaronnedeFleurSummon_2()
		{
			if (base.Duel.Turn > 1 && base.Duel.Phase < DuelPhase.Main2)
			{
				return false;
			}
			List<ClientCard> scards = (from card in base.Bot.GetMonsters()
				where card != null && card.Id == 92919429 && card.IsFaceup()
				select card).ToList<ClientCard>();
			List<ClientCard> mcards = (from card in base.Bot.GetMonsters()
				where card != null && !card.HasType(CardType.Tuner) && card.IsFaceup()
				select card).ToList<ClientCard>();
			if (scards.Any((ClientCard card) => card != null && card.Level == 2))
			{
				if (mcards.Count((ClientCard card) => card != null && card.Level == 4) > 1)
				{
					List<ClientCard> s = scards.Where((ClientCard card) => card != null && card.Level == 2 && card.Id == 92919429 && card.IsFaceup()).ToList<ClientCard>();
					List<ClientCard> res = new List<ClientCard> { s.FirstOrDefault<ClientCard>() };
					res.AddRange(mcards.Where((ClientCard card) => card != null && card.Level == 4 && card.IsFaceup()));
					base.AI.SelectMaterials(res, 0);
					this.SetSpSummon();
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001F80 RID: 8064 RVA: 0x000C7ECC File Offset: 0x000C60CC
		private bool BaronnedeFleurSummon()
		{
			List<ClientCard> scards = (from card in base.Bot.GetMonsters()
				where card != null && card.Id == 92919429 && card.IsFaceup()
				select card).ToList<ClientCard>();
			List<ClientCard> mcards = (from card in base.Bot.GetMonsters()
				where card != null && !card.HasType(CardType.Synchro) && card.IsFaceup()
				select card).ToList<ClientCard>();
			if (scards.Any((ClientCard card) => card != null && card.Level == 2))
			{
				if (mcards.Any((ClientCard card) => card != null && card.Level == 8 && card.Id != 84330567 && card.Id != 69946549))
				{
					this.SetSpSummon();
					return true;
				}
			}
			if (scards.Any((ClientCard card) => card != null && card.Level == 5))
			{
				if (mcards.Any((ClientCard card) => card != null && card.Level == 5))
				{
					List<ClientCard> s = (from card in base.Bot.GetMonsters()
						where card != null && card.Level == 5 && card.Id == 92919429 && card.IsFaceup()
						select card).ToList<ClientCard>();
					if (s.Count >= 0)
					{
						List<ClientCard> res = new List<ClientCard> { s.FirstOrDefault<ClientCard>() };
						res.AddRange(from card in base.Bot.GetMonsters()
							where card != null && card.Level == 5 && card.Id == 94977269 && card.IsFaceup()
							select card);
						res.AddRange(from card in base.Bot.GetMonsters()
							where card != null && card.Level == 5 && card.IsFaceup()
							select card);
						base.AI.SelectMaterials(res, 0);
						this.SetSpSummon();
						return true;
					}
					return false;
				}
			}
			if (scards.Any((ClientCard card) => card != null && card.Level == 6))
			{
				if (mcards.Any((ClientCard card) => card != null && card.Level == 4))
				{
					this.SetSpSummon();
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001F81 RID: 8065 RVA: 0x000C8118 File Offset: 0x000C6318
		private bool SprightElfEffect()
		{
			ClientCard card = base.Util.GetLastChainCard();
			if (card != null && card.Controller == 0)
			{
				return false;
			}
			this.SetSpSummon();
			return true;
		}

		// Token: 0x06001F82 RID: 8066 RVA: 0x000C8148 File Offset: 0x000C6348
		private bool FusionDeckCheck()
		{
			return (this.CheckRemainInDeck(37961969) > 0 && !this.activate_TearlamentsHavnis_2) || (this.CheckRemainInDeck(74078255) > 0 && !this.activate_TearlamentsMerrli_2) || (this.CheckRemainInDeck(572850) > 0 && !this.activate_TearlamentsScheiren_2);
		}

		// Token: 0x06001F83 RID: 8067 RVA: 0x000C819C File Offset: 0x000C639C
		private List<int> GetCardsIdSendToHand()
		{
			List<int> ids = new List<int>();
			if (!this.activate_TearlamentsScheiren_1 && !base.Bot.HasInHand(572850) && this.CheckRemainInDeck(572850) > 0)
			{
				if (base.Bot.Hand.Count((ClientCard card) => card != null && card.HasType(CardType.Monster)) > 0)
				{
					ids.Add(572850);
				}
			}
			if (!this.activate_TearlamentsMerrli_1 && !base.Bot.HasInHand(74078255) && this.CheckRemainInDeck(74078255) > 0 && (!this.summoned || !this.activate_TearlamentsKitkallos_2))
			{
				ids.Add(74078255);
			}
			if (!this.activate_TearlamentsReinoheart_1 && !base.Bot.HasInHand(73956664) && this.CheckRemainInDeck(73956664) > 0)
			{
				ids.Add(73956664);
			}
			if (!this.activate_TearlamentsScheiren_1 && !base.Bot.HasInHand(572850) && this.CheckRemainInDeck(572850) > 0)
			{
				ids.Add(572850);
			}
			if (!this.activate_TearlamentsHavnis_1 && !base.Bot.HasInHand(37961969) && this.CheckRemainInDeck(37961969) > 0)
			{
				if (base.Duel.Player == 0 && (base.Duel.Phase != DuelPhase.End || this.AllActivated()))
				{
					ids.Add(37961969);
				}
				else
				{
					ids.Insert(0, 37961969);
				}
			}
			ids.AddRange(new List<int> { 572850, 74078255, 37961969, 73956664 });
			return ids;
		}

		// Token: 0x06001F84 RID: 8068 RVA: 0x000C8360 File Offset: 0x000C6560
		public int CheckRemainInDeck(int id)
		{
			if (id <= 62320425)
			{
				if (id <= 17266660)
				{
					if (id <= 3717252)
					{
						if (id == 572850)
						{
							return base.Bot.GetRemainingCount(572850, 3);
						}
						if (id == 3717252)
						{
							return base.Bot.GetRemainingCount(3717252, 1);
						}
					}
					else
					{
						if (id == 6767771)
						{
							return base.Bot.GetRemainingCount(6767771, 1);
						}
						if (id == 17266660)
						{
							return base.Bot.GetRemainingCount(17266660, 3);
						}
					}
				}
				else if (id <= 25926710)
				{
					if (id == 21074344)
					{
						return base.Bot.GetRemainingCount(21074344, 3);
					}
					if (id == 25926710)
					{
						return base.Bot.GetRemainingCount(25926710, 3);
					}
				}
				else
				{
					if (id == 37961969)
					{
						return base.Bot.GetRemainingCount(37961969, 3);
					}
					if (id == 40177746)
					{
						return base.Bot.GetRemainingCount(40177746, 1);
					}
					if (id == 62320425)
					{
						return base.Bot.GetRemainingCount(62320425, 3);
					}
				}
			}
			else if (id <= 74920585)
			{
				if (id <= 73956664)
				{
					if (id == 63542003)
					{
						return base.Bot.GetRemainingCount(63542003, 2);
					}
					if (id == 73956664)
					{
						return base.Bot.GetRemainingCount(73956664, 2);
					}
				}
				else
				{
					if (id == 74078255)
					{
						return base.Bot.GetRemainingCount(74078255, 3);
					}
					if (id == 74920585)
					{
						return base.Bot.GetRemainingCount(74920585, 2);
					}
				}
			}
			else if (id <= 77723643)
			{
				if (id == 77103950)
				{
					return base.Bot.GetRemainingCount(77103950, 2);
				}
				if (id == 77723643)
				{
					return base.Bot.GetRemainingCount(77723643, 1);
				}
			}
			else
			{
				if (id == 92919429)
				{
					return base.Bot.GetRemainingCount(92919429, 3);
				}
				if (id == 97518132)
				{
					return base.Bot.GetRemainingCount(97518132, 1);
				}
				if (id == 99937011)
				{
					return base.Bot.GetRemainingCount(99937011, 3);
				}
			}
			return 0;
		}

		// Token: 0x06001F85 RID: 8069 RVA: 0x000C85D8 File Offset: 0x000C67D8
		private bool PredaplantDragostapeliaEffect()
		{
			ClientCard card = base.Util.GetLastChainCard();
			if (card != null && card.Controller != 0 && card.Location == CardLocation.MonsterZone && !card.IsShouldNotBeTarget() && !this.e_PredaplantDragostapelia_cards.Contains(card))
			{
				this.chain_PredaplantDragostapelia = card;
				return true;
			}
			return false;
		}

		// Token: 0x06001F86 RID: 8070 RVA: 0x000C8625 File Offset: 0x000C6825
		private bool UnderworldGoddessoftheClosedWorldSummon_2()
		{
			return this.UnderworldGoddessoftheClosedWorldLinkSummon(false);
		}

		// Token: 0x06001F87 RID: 8071 RVA: 0x000C8630 File Offset: 0x000C6830
		private bool UnderworldGoddessoftheClosedWorldLinkSummon(bool filter = true)
		{
			if (base.Duel.Turn == 1 || base.Enemy.GetMonsterCount() <= 0)
			{
				return false;
			}
			List<ClientCard> e_cards = (from card in base.Enemy.GetMonsters()
				where card != null && card.IsFaceup() && card.IsAttack()
				select card).ToList<ClientCard>();
			List<ClientCard> b_cards = (from card in base.Bot.GetMonsters()
				where card != null && card.IsFaceup() && card.IsAttack()
				select card).ToList<ClientCard>();
			if (e_cards.Count <= 0 || b_cards.Count <= 0 || base.Enemy.MonsterZone.GetDangerousMonster(false) == null)
			{
				return false;
			}
			e_cards.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			e_cards.Reverse();
			b_cards.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			b_cards.Reverse();
			if ((e_cards[0].Attack > b_cards[0].Attack && (e_cards[0].IsShouldNotBeTarget() || e_cards[0].Attack >= 2500)) || base.Enemy.MonsterZone.GetDangerousMonster(false) != null)
			{
				List<ClientCard> e_materials = new List<ClientCard>();
				List<ClientCard> m_materials = new List<ClientCard>();
				List<ClientCard> resMaterials = new List<ClientCard>();
				foreach (ClientCard card5 in base.Enemy.GetMonsters())
				{
					if (card5 != null && card5.HasType(CardType.Effect) && card5.IsFaceup())
					{
						e_materials.Add(card5);
					}
				}
				if (e_materials.Count<ClientCard>() <= 0)
				{
					return false;
				}
				List<ClientCard> monsters = base.Bot.GetMonsters();
				monsters.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				foreach (ClientCard card2 in monsters)
				{
					if (card2 != null && !card2.IsFacedown() && (card2.Id != 27381364 || !this.summon_SprightElf) && card2.LinkCount < 3 && (!this.no_link_ids.Contains(card2.Id) || !filter) && card2.IsFaceup() && card2.HasType(CardType.Effect))
					{
						if (card2.Id == 94977269)
						{
							m_materials.Insert(0, card2);
						}
						else
						{
							m_materials.Add(card2);
						}
					}
				}
				if (m_materials.Count<ClientCard>() < 3)
				{
					return false;
				}
				int link_count = 0;
				int e_link_count = 0;
				e_materials.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				e_materials.Reverse();
				foreach (ClientCard card3 in e_materials)
				{
					if (!resMaterials.Contains(card3))
					{
						resMaterials.Add(card3);
					}
					e_link_count += (card3.HasType(CardType.Link) ? ((card3.LinkCount == 2) ? 2 : 1) : 1);
					if (e_link_count >= 1)
					{
						break;
					}
				}
				if (e_link_count <= 0)
				{
					return false;
				}
				link_count += e_link_count;
				foreach (ClientCard card4 in m_materials)
				{
					if (e_link_count <= 1)
					{
						if (!resMaterials.Contains(card4) && card4.LinkCount < 3)
						{
							resMaterials.Add(card4);
							link_count += (card4.HasType(CardType.Link) ? card4.LinkCount : 1);
							if (link_count >= 5)
							{
								break;
							}
						}
					}
					else
					{
						resMaterials.Add(card4);
						link_count++;
						if (link_count >= 5)
						{
							break;
						}
					}
				}
				if (link_count >= 5)
				{
					base.AI.SelectMaterials(resMaterials, 0);
					this.SetSpSummon();
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001F88 RID: 8072 RVA: 0x000C8A24 File Offset: 0x000C6C24
		private bool TearlamentsKaleidoHeartEffect()
		{
			if (base.Card.Location == CardLocation.MonsterZone)
			{
				this.activate_TearlamentsKaleidoHeart_1 = true;
				return this.DestoryEnemyCard();
			}
			this.activate_TearlamentsKaleidoHeart_2 = true;
			return true;
		}

		// Token: 0x06001F89 RID: 8073 RVA: 0x000C8A4A File Offset: 0x000C6C4A
		private bool UnderworldGoddessoftheClosedWorldSummon()
		{
			return base.Bot.HasInMonstersZone(94977269, true, false, true) && this.UnderworldGoddessoftheClosedWorldLinkSummon(true);
		}

		// Token: 0x06001F8A RID: 8074 RVA: 0x000C8A6A File Offset: 0x000C6C6A
		private bool UnderworldGoddessoftheClosedWorldSummon_3()
		{
			return this.UnderworldGoddessoftheClosedWorldLinkSummon(true);
		}

		// Token: 0x06001F8B RID: 8075 RVA: 0x000C8A73 File Offset: 0x000C6C73
		private bool SpellActivate()
		{
			return base.Card.Location == CardLocation.Hand || (base.Card.Location == CardLocation.SpellZone && base.Card.IsFacedown());
		}

		// Token: 0x06001F8C RID: 8076 RVA: 0x000C8AA0 File Offset: 0x000C6CA0
		private bool SprightElfSummon_2()
		{
			List<ClientCard> key_cards = (from card in base.Bot.GetMonsters()
				where card != null && card.Id == 94977269 && !card.IsDisabled() && card.IsFaceup()
				select card).ToList<ClientCard>();
			if (key_cards.Count <= 0 || key_cards.Count > 1 || !this.IsAvailableLinkZone())
			{
				return false;
			}
			List<ClientCard> cards = (from card in base.Bot.GetMonsters()
				where card != null && card.Level == 2 && card.Id != 94977269 && card.IsFaceup()
				select card).ToList<ClientCard>();
			if (cards.Count <= 0)
			{
				return false;
			}
			cards.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			cards.Insert(0, key_cards[0]);
			base.AI.SelectMaterials(cards, 0);
			this.SetSpSummon();
			return true;
		}

		// Token: 0x06001F8D RID: 8077 RVA: 0x000C8B74 File Offset: 0x000C6D74
		private bool FADawnDragsterSummon()
		{
			List<ClientCard> key_cards = (from card in base.Bot.GetMonsters()
				where card != null && card.Id == 94977269 && !card.IsDisabled() && card.IsFaceup()
				select card).ToList<ClientCard>();
			if (key_cards.Count <= 0 || key_cards.Count > 1)
			{
				return false;
			}
			this.SetSpSummon();
			List<int> list = new List<int>();
			list.Add(92919429);
			IList<ClientCard> cards = this.CardsIdToClientCards(list, (from card in base.Bot.GetMonsters()
				where card != null && card.IsFaceup()
				select card).ToList<ClientCard>(), true, true);
			if (cards.Count > 0)
			{
				key_cards.Insert(0, cards[0]);
			}
			base.AI.SelectMaterials(key_cards, 0);
			return true;
		}

		// Token: 0x06001F8E RID: 8078 RVA: 0x000C8C44 File Offset: 0x000C6E44
		private bool IPSummon_2()
		{
			if ((base.Bot.GetMonsterCount() <= 2 && base.Bot.Hand.Count <= 2 && !base.Bot.HasInHand(572850) && !this.activate_TearlamentsScheiren_1 && !base.Bot.HasInHand(74078255) && !this.activate_TearlamentsMerrli_1) || this.AllActivated())
			{
				return false;
			}
			List<ClientCard> key_cards = (from card in base.Bot.GetMonsters()
				where card != null && card.Id == 94977269 && !card.IsDisabled() && card.IsFaceup()
				select card).ToList<ClientCard>();
			if (key_cards.Count <= 0 || key_cards.Count > 1 || !this.IsAvailableLinkZone())
			{
				return false;
			}
			List<ClientCard> cards = (from card in base.Bot.GetMonsters()
				where card != null && !card.HasType(CardType.Link) && !this.no_link_ids.Contains(card.Id) && card.IsFaceup()
				select card).ToList<ClientCard>();
			cards.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			cards.Insert(0, key_cards[0]);
			base.AI.SelectMaterials(cards, 0);
			this.SetSpSummon();
			return true;
		}

		// Token: 0x06001F8F RID: 8079 RVA: 0x000C8D58 File Offset: 0x000C6F58
		private bool SprightElfSummon()
		{
			if (!this.IsAvailableLinkZone())
			{
				return false;
			}
			List<ClientCard> list = (from card in this.GetZoneCards(CardLocation.MonsterZone, base.Bot)
				where card != null && card.IsFaceup()
				select card).ToList<ClientCard>();
			list.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			List<ClientCard> materials = new List<ClientCard>();
			List<ClientCard> materials2 = new List<ClientCard>();
			List<ClientCard> materials3 = new List<ClientCard>();
			List<ClientCard> materials4 = new List<ClientCard>();
			foreach (ClientCard card2 in list)
			{
				if (card2 != null)
				{
					if (card2.Level == 2 && materials.Count <= 0)
					{
						materials.Add(card2);
					}
					else if (this.link_card != null && card2 == this.link_card)
					{
						materials2.Insert(0, this.link_card);
					}
					else if (!card2.IsDisabled() && this.no_link_ids.Contains(card2.Id))
					{
						materials4.Add(card2);
					}
					else if (card2.Level <= 2)
					{
						materials2.Add(card2);
					}
					else
					{
						materials3.Add(card2);
					}
				}
			}
			if (materials.Count <= 0 || materials2.Count + materials3.Count <= 0)
			{
				return false;
			}
			materials3.Sort(new Comparison<ClientCard>(CardContainer.CompareCardLevel));
			materials3.Reverse();
			materials.AddRange(materials2);
			materials.AddRange(materials3);
			this.summon_SprightElf = true;
			base.AI.SelectMaterials(materials, 0);
			this.SetSpSummon();
			return true;
		}

		// Token: 0x06001F90 RID: 8080 RVA: 0x000C8EF4 File Offset: 0x000C70F4
		private bool AbyssDwellerSummon()
		{
			if (base.Duel.Turn > 1 && base.Duel.Phase < DuelPhase.Main2)
			{
				return false;
			}
			this.SetSpSummon();
			return true;
		}

		// Token: 0x06001F91 RID: 8081 RVA: 0x000C8F1F File Offset: 0x000C711F
		private bool AbyssDwellerSummon_2()
		{
			return base.Bot.GetMonsters().Any((ClientCard card) => card != null && card.IsFaceup() && (card.Id == 84330567 || card.Id == 28226490));
		}

		// Token: 0x06001F92 RID: 8082 RVA: 0x000C8F58 File Offset: 0x000C7158
		private bool IPEffect()
		{
			if (base.Duel.LastChainPlayer == 0)
			{
				return false;
			}
			if (!base.Bot.HasInExtra(38342335) && !base.Bot.HasInExtra(21887175) && !base.Bot.HasInExtra(98127546))
			{
				return false;
			}
			List<ClientCard> i = new List<ClientCard>();
			List<ClientCard> pre_m = new List<ClientCard>();
			if (base.Bot.HasInExtra(98127546))
			{
				List<ClientCard> e_cards = (from card in base.Enemy.GetMonsters()
					where card != null && card.IsFaceup() && card.IsAttack()
					select card).ToList<ClientCard>();
				List<ClientCard> b_cards = (from card in base.Bot.GetMonsters()
					where card != null && card.IsFaceup() && card.IsAttack()
					select card).ToList<ClientCard>();
				if (e_cards.Count > 0 && b_cards.Count > 0)
				{
					e_cards.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
					e_cards.Reverse();
					b_cards.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
					b_cards.Reverse();
					if ((e_cards[0].Attack > b_cards[0].Attack && (e_cards[0].IsShouldNotBeTarget() || e_cards[0].Attack >= 2500)) || base.Enemy.MonsterZone.GetDangerousMonster(false) != null)
					{
						pre_m = (from card in base.Bot.GetMonsters()
							where card != null && card != base.Card && card.IsFaceup() && ((card.HasType(CardType.Link) && card.LinkCount < 3) || card.HasType((CardType)8396864)) && (!this.no_link_ids.Contains(card.Id) || card.IsDisabled())
							select card).ToList<ClientCard>();
						List<ClientCard> pre_m2 = new List<ClientCard>();
						pre_m2.Add((base.Enemy.MonsterZone.GetDangerousMonster(false) == null) ? e_cards[0] : base.Enemy.MonsterZone.GetDangerousMonster(false));
						pre_m2.AddRange(e_cards);
						int link_count = 0;
						foreach (ClientCard card4 in pre_m)
						{
							if (card4 != null && (card4.Id != 27381364 || !this.summon_SprightElf))
							{
								link_count++;
								i.Add(card4);
								if (link_count >= 2)
								{
									break;
								}
							}
						}
						if (link_count >= 2)
						{
							base.AI.SelectCard(98127546);
							i.Insert(0, base.Card);
							i.Add(pre_m2.FirstOrDefault<ClientCard>());
							base.AI.SelectMaterials(i, 0);
							return true;
						}
					}
				}
			}
			if (base.Bot.HasInExtra(21887175))
			{
				i.Clear();
				pre_m = (from card in base.Bot.GetMonsters()
					where card != null && card != base.Card && card.IsFaceup() && ((card.HasType(CardType.Link) && card.LinkCount < 3) || card.HasType((CardType)8396864)) && (!this.no_link_ids.Contains(card.Id) || card.IsDisabled())
					select card).ToList<ClientCard>();
				if (pre_m.Count > 0)
				{
					pre_m.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
					int link_count2 = 0;
					foreach (ClientCard card2 in pre_m)
					{
						if (card2 != null && (card2.Id != 27381364 || !this.summon_SprightElf))
						{
							link_count2 += (card2.HasType(CardType.Link) ? card2.LinkCount : 1);
							i.Add(card2);
							if (link_count2 >= 2)
							{
								break;
							}
						}
					}
					if (link_count2 >= 2)
					{
						base.AI.SelectCard(21887175);
						i.Insert(0, base.Card);
						base.AI.SelectMaterials(i, 0);
						return true;
					}
				}
			}
			if (base.Bot.HasInExtra(38342335))
			{
				List<ClientCard> pre_cards = this.GetZoneCards(CardLocation.Onfield, base.Enemy);
				if (base.Bot.Hand.Count > 0)
				{
					if (pre_cards.Count((ClientCard card) => card != null && !card.IsShouldNotBeTarget()) > 0)
					{
						i.Clear();
						pre_m = (from card in base.Bot.GetMonsters()
							where card != null && card != base.Card && card.IsFaceup() && !this.no_link_ids.Contains(card.Id) && card.Id != base.Card.Id
							select card).ToList<ClientCard>();
						if (pre_m.Count > 0)
						{
							pre_m.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
							int link_count3 = 0;
							foreach (ClientCard card3 in pre_m)
							{
								if (card3 != null && (card3.Id != 27381364 || !this.summon_SprightElf))
								{
									link_count3 += (card3.HasType(CardType.Link) ? card3.LinkCount : 1);
									i.Add(card3);
									if (link_count3 >= 1)
									{
										break;
									}
								}
							}
							if (link_count3 >= 1)
							{
								base.AI.SelectCard(38342335);
								i.Insert(0, base.Card);
								base.AI.SelectMaterials(i, 0);
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06001F93 RID: 8083 RVA: 0x000C9450 File Offset: 0x000C7650
		private bool IPSummon()
		{
			if (base.Duel.Turn > 1 && base.Duel.Phase < DuelPhase.Main2)
			{
				return false;
			}
			if (!base.Bot.HasInMonstersZone(27381364, false, false, false) || base.Bot.GetMonsterCount() <= 2)
			{
				return false;
			}
			if (base.Bot.ExtraDeck.Count((ClientCard card) => card != null && card.LinkCount > 2) <= 0 || !this.IsAvailableLinkZone())
			{
				return false;
			}
			List<ClientCard> list = (from card in this.GetZoneCards(CardLocation.MonsterZone, base.Bot)
				where card != null && card.IsFaceup() && !card.HasType(CardType.Link)
				select card).ToList<ClientCard>();
			List<ClientCard> materials = new List<ClientCard>();
			foreach (ClientCard card2 in list)
			{
				if (card2 != null && (card2.IsDisabled() || !this.no_link_ids.Contains(card2.Id)) && card2.LinkCount < 2)
				{
					if (this.link_card != null && card2 == this.link_card)
					{
						materials.Insert(0, this.link_card);
					}
					else
					{
						materials.Add(card2);
					}
				}
			}
			if (materials.Count <= 1)
			{
				return false;
			}
			materials.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			base.AI.SelectMaterials(materials, 0);
			this.SetSpSummon();
			return true;
		}

		// Token: 0x06001F94 RID: 8084 RVA: 0x000C95D4 File Offset: 0x000C77D4
		private bool KnightmareUnicornEffect()
		{
			List<ClientCard> cards = this.GetZoneCards(CardLocation.Onfield, base.Enemy);
			cards = cards.Where((ClientCard card) => card != null && !card.IsShouldNotBeTarget() && (this.tgcard == null || card != this.tgcard)).ToList<ClientCard>();
			if (cards.Count <= 0)
			{
				return false;
			}
			List<int> ids = new List<int>();
			if (!this.activate_KelbektheAncientVanguard_2)
			{
				ids.Add(25926710);
			}
			if (!this.activate_AgidotheAncientSentinel_2)
			{
				ids.Add(62320425);
			}
			ids.AddRange(new List<int> { 99937011, 63542003, 3717252, 77723643, 74920585, 40177746, 73956664 });
			cards.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			cards.Reverse();
			base.AI.SelectCard(ids);
			base.AI.SelectNextCard(cards);
			return true;
		}

		// Token: 0x06001F95 RID: 8085 RVA: 0x000C96C8 File Offset: 0x000C78C8
		private bool MekkKnightCrusadiaAvramaxSummon()
		{
			if (!this.IsAvailableLinkZone() || (base.Duel.Turn > 1 && base.Duel.Phase < DuelPhase.Main2))
			{
				return false;
			}
			if (base.Bot.HasInMonstersZone(27381364, false, false, true) && !this.summon_SprightElf && base.Bot.HasInMonstersZone(65741786, false, false, true))
			{
				List<ClientCard> m = (from card in base.Bot.GetMonsters()
					where card != null && card.IsFaceup() && card.Id == 27381364
					select card).ToList<ClientCard>();
				List<ClientCard> m2 = (from card in base.Bot.GetMonsters()
					where card != null && card.IsFaceup() && card.Id == 65741786
					select card).ToList<ClientCard>();
				if (m.Count <= 0 || m2.Count <= 0)
				{
					return false;
				}
				List<ClientCard> res = new List<ClientCard> { m[0] };
				res.Add(m2[0]);
				base.AI.SelectMaterials(res, 0);
				this.SetSpSummon();
				return true;
			}
			else
			{
				List<ClientCard> cards = (from card in base.Bot.GetMonsters()
					where card != null && card.LinkCount != 2 && card.IsFaceup() && (!this.no_link_ids.Contains(card.Id) || card.IsDisabled()) && card.IsExtraCard() && card.Id != 38342335
					select card).ToList<ClientCard>();
				List<ClientCard> cards2 = (from card in base.Bot.GetMonsters()
					where card != null && card.IsFaceup() && card.Id == 38342335
					select card).ToList<ClientCard>();
				if (cards.Count <= 0 || cards2.Count <= 0)
				{
					return false;
				}
				cards.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				List<ClientCard> res2 = new List<ClientCard>();
				res2.Add(cards.FirstOrDefault<ClientCard>());
				res2.Add(cards.FirstOrDefault<ClientCard>());
				base.AI.SelectMaterials(res2, 0);
				this.SetSpSummon();
				return true;
			}
		}

		// Token: 0x06001F96 RID: 8086 RVA: 0x000C98A2 File Offset: 0x000C7AA2
		private bool MekkKnightCrusadiaAvramaxEffect()
		{
			return base.Card.Location == CardLocation.MonsterZone || this.DestoryEnemyCard();
		}

		// Token: 0x06001F97 RID: 8087 RVA: 0x000C98BC File Offset: 0x000C7ABC
		private bool KnightmareUnicornSummon()
		{
			if (base.Bot.Hand.Count <= 0)
			{
				return false;
			}
			if (!this.IsAvailableLinkZone())
			{
				return false;
			}
			if (this.GetZoneCards(CardLocation.Onfield, base.Enemy).Count((ClientCard card) => card3 != null && !card3.IsShouldNotBeTarget()) <= 0)
			{
				return false;
			}
			List<ClientCard> tmepMaterials = new List<ClientCard>();
			List<ClientCard> resMaterials = new List<ClientCard>();
			using (List<ClientCard>.Enumerator enumerator = base.Bot.GetMonsters().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ClientCard card3 = enumerator.Current;
					if (card3 != null && !card3.IsFacedown() && (card3.Id != 27381364 || !this.summon_SprightElf) && !this.no_link_ids.Contains(card3.Id) && card3.Id != 92731385 && tmepMaterials.Count((ClientCard _card) => _card != null && _card.Id == card3.Id) <= 0)
					{
						tmepMaterials.Add(card3);
					}
				}
			}
			int link_count = 0;
			tmepMaterials.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			List<ClientCard> materials = new List<ClientCard>();
			List<ClientCard> link_materials = tmepMaterials.Where((ClientCard card) => card != null && card.LinkCount == 2).ToList<ClientCard>();
			List<ClientCard> normal_materials = tmepMaterials.Where((ClientCard card) => card != null && card.LinkCount != 2).ToList<ClientCard>();
			if (link_materials.Count<ClientCard>() >= 1)
			{
				materials.Add(link_materials.FirstOrDefault<ClientCard>());
				materials.AddRange(normal_materials);
			}
			else
			{
				materials.AddRange(normal_materials);
				materials.AddRange(link_materials);
			}
			if (materials.Count((ClientCard card) => card != null && card.LinkCount >= 2) > 1)
			{
				if (materials.Count((ClientCard card) => card != null && card.LinkCount < 2) < 1)
				{
					return false;
				}
			}
			foreach (ClientCard card2 in materials)
			{
				if (!resMaterials.Contains(card2) && card2.LinkCount < 3)
				{
					resMaterials.Add(card2);
					link_count += (card2.HasType(CardType.Link) ? card2.LinkCount : 1);
					if (link_count >= 3)
					{
						break;
					}
				}
			}
			if (link_count >= 3)
			{
				base.AI.SelectMaterials(resMaterials, 0);
				this.SetSpSummon();
				return true;
			}
			return false;
		}

		// Token: 0x06001F98 RID: 8088 RVA: 0x000C9B88 File Offset: 0x000C7D88
		private bool BaronnedeFleurEffect()
		{
			if (base.ActivateDescription == base.Util.GetStringId(84815190, 0))
			{
				return this.DestoryEnemyCard();
			}
			return base.ActivateDescription == base.Util.GetStringId(84815190, 1) && base.Duel.LastChainPlayer != 0;
		}

		// Token: 0x06001F99 RID: 8089 RVA: 0x000C9BE0 File Offset: 0x000C7DE0
		private bool PrimevalPlanetPerlereinoEffect()
		{
			if (this.SpellActivate())
			{
				return true;
			}
			ClientCard card = base.Util.GetLastChainCard();
			return (card == null || card.Controller != 0 || card.Id != 28226490) && this.DestoryEnemyCard();
		}

		// Token: 0x06001F9A RID: 8090 RVA: 0x000C9C24 File Offset: 0x000C7E24
		private bool DestoryEnemyCard()
		{
			if (base.Duel.Player == 0 && base.Card.Id == 77103950)
			{
				if (!this.on_chaining_cards.Any((ClientCard ccard) => ccard != null && !ccard.IsDisabled() && ccard.Controller == 0 && ccard.Id == 92731385) && base.Bot.HasInMonstersZone(92731385, false, false, false) && !this.activate_TearlamentsKitkallos_3 && !this.AllActivated())
				{
					List<ClientCard> temp = new List<ClientCard>();
					List<ClientCard> temp2 = new List<ClientCard>();
					foreach (ClientCard ccard2 in base.Bot.GetMonsters())
					{
						if (ccard2 != null)
						{
							if (ccard2.Id == 92731385 && ccard2.IsDisabled())
							{
								temp.Add(ccard2);
							}
							else if (ccard2.Id == 92731385)
							{
								temp2.Add(ccard2);
							}
						}
					}
					temp.AddRange(temp2);
					base.AI.SelectCard(temp);
					this.activate_PrimevalPlanetPerlereino_2 = true;
					return true;
				}
			}
			ClientCard card = base.Util.GetProblematicEnemyMonster(0, true);
			if (card != null && (this.tgcard == null || this.tgcard != card))
			{
				base.AI.SelectCard(card);
				this.tgcard = card;
				return true;
			}
			card = base.Util.GetBestEnemySpell(true);
			if (card != null && (this.tgcard == null || this.tgcard != card))
			{
				base.AI.SelectCard(card);
				this.tgcard = card;
				return true;
			}
			List<ClientCard> cards = this.GetZoneCards(CardLocation.Onfield, base.Enemy);
			cards = cards.Where((ClientCard tcard) => tcard != null && !tcard.IsShouldNotBeTarget() && (this.tgcard == null || tcard != this.tgcard)).ToList<ClientCard>();
			if (cards.Count <= 0)
			{
				return false;
			}
			cards.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			cards.Reverse();
			this.tgcard = cards[0];
			base.AI.SelectCard(cards);
			if (base.Card.Id == 77103950)
			{
				this.activate_PrimevalPlanetPerlereino_2 = true;
			}
			return true;
		}

		// Token: 0x06001F9B RID: 8091 RVA: 0x000C9E48 File Offset: 0x000C8048
		private bool ElderEntityNtssEffect()
		{
			return base.Card.Location != CardLocation.Grave || this.DestoryEnemyCard();
		}

		// Token: 0x06001F9C RID: 8092 RVA: 0x000C9E61 File Offset: 0x000C8061
		private bool DivineroftheHeraldSummon()
		{
			if ((this.CheckRemainInDeck(25926710) > 0 && !this.activate_KelbektheAncientVanguard_2) || (this.CheckRemainInDeck(62320425) > 0 && !this.activate_AgidotheAncientSentinel_2))
			{
				this.summoned = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001F9D RID: 8093 RVA: 0x000C9E9C File Offset: 0x000C809C
		private bool EvaEffect()
		{
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			if ((from card in base.Bot.GetGraveyardMonsters()
				where card != null && card.HasAttribute(CardAttribute.Light) && card.HasRace(CardRace.Fairy) && card != base.Card
				select card).ToList<ClientCard>().Count <= 0)
			{
				return false;
			}
			this.activate_Eva = true;
			return true;
		}

		// Token: 0x06001F9E RID: 8094 RVA: 0x000C9EEC File Offset: 0x000C80EC
		private bool DivineroftheHeraldEffect()
		{
			if (base.Card.Location == CardLocation.MonsterZone)
			{
				if (base.Duel.Player == 0)
				{
					if (!this.IsCanSpSummon() && this.CheckRemainInDeck(99937011) <= 0 && this.CheckRemainInDeck(63542003) <= 0)
					{
						return false;
					}
					if (!this.AllActivated())
					{
						if (base.Bot.ExtraDeck.Count((ClientCard card) => card != null && card.HasType(CardType.Fusion) && card.Id != 80532587) > 0)
						{
							goto IL_009F;
						}
					}
					if (this.CheckRemainInDeck(99937011) <= 0 && this.CheckRemainInDeck(63542003) <= 0)
					{
						return false;
					}
					IL_009F:
					if ((this.activate_KelbektheAncientVanguard_2 || this.CheckRemainInDeck(25926710) <= 0) && (this.activate_AgidotheAncientSentinel_2 || this.CheckRemainInDeck(62320425) <= 0) && base.Bot.HasInExtra(27381364) && (from card in base.Bot.GetMonsters()
						where card != null && card.IsFaceup() && !this.no_link_ids.Contains(card.Id) && card != base.Card
						select card).ToList<ClientCard>().Count<ClientCard>() >= 1)
					{
						return false;
					}
				}
				this.activate_DivineroftheHerald = true;
				return true;
			}
			return true;
		}

		// Token: 0x06001F9F RID: 8095 RVA: 0x000CA00B File Offset: 0x000C820B
		private bool TearlamentsScreamEffect_1()
		{
			return this.SpellActivate();
		}

		// Token: 0x06001FA0 RID: 8096 RVA: 0x000CA014 File Offset: 0x000C8214
		private bool MudoratheSwordOracleEffect()
		{
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			if (base.Card.Location == CardLocation.Hand)
			{
				if (base.Bot.Hand.Count((ClientCard card) => card != null && card.Id == 62320425) <= 0 || this.activate_AgidotheAncientSentinel_2)
				{
					if ((base.Bot.Hand.Count((ClientCard card) => card != null && card.Id == 25926710) <= 0 || this.activate_KelbektheAncientVanguard_2) && !this.summoned && !base.Bot.HasInHand(99937011) && !base.Bot.HasInHand(63542003))
					{
						return false;
					}
				}
				IList<int> cardsid = new List<int>();
				if (!this.activate_AgidotheAncientSentinel_2)
				{
					cardsid.Add(62320425);
				}
				if (!this.activate_KelbektheAncientVanguard_2)
				{
					cardsid.Add(25926710);
				}
				cardsid.Add(63542003);
				cardsid.Add(99937011);
				if (base.Card.Id == 99937011)
				{
					this.activate_MudoratheSwordOracle_2 = true;
				}
				else if (base.Card.Id == 63542003)
				{
					this.activate_KeldotheSacredProtector_2 = true;
				}
				base.AI.SelectCard(cardsid);
				this.SetSpSummon();
				return true;
			}
			if (base.Card.Location == CardLocation.Grave)
			{
				ClientCard chain_card = base.Util.GetLastChainCard();
				if (chain_card != null && chain_card.Controller == 0)
				{
					return false;
				}
				if (this.GetZoneCards(CardLocation.Grave, base.Enemy).Count<ClientCard>() <= 0 && base.Bot.Deck.Count >= 3)
				{
					return false;
				}
				if ((base.Duel.CurrentChain == null || base.Duel.CurrentChain.Count <= 0) && base.Bot.Deck.Count >= 3)
				{
					return false;
				}
				if (base.Duel.CurrentChain.Any((ClientCard card) => (card != null && card.Controller == 0 && (card.Id == 99937011 || card.Id == 63542003)) || card.Id == 97518132))
				{
					return false;
				}
				if (base.Duel.CurrentChain.Any((ClientCard card) => card != null && card.Controller == 0 && card.Id == 3717252) && base.Bot.Deck.Count <= 0)
				{
					if (base.Card.Id == 99937011)
					{
						this.activate_MudoratheSwordOracle_2 = true;
					}
					else if (base.Card.Id == 63542003)
					{
						this.activate_KeldotheSacredProtector_2 = true;
					}
					return true;
				}
				foreach (ClientCard card3 in base.Duel.ChainTargets)
				{
					if (card3 != null && ((card3 == base.Card && (base.Bot.Deck.Count < 5 || base.Enemy.Graveyard.Count((ClientCard ccard) => ccard != null && !this.key_no_send_to_deck_ids.Contains(ccard.Id)) > 0)) || (card3.Controller == 1 && card3.Location == CardLocation.Grave)))
					{
						if (base.Card.Id == 99937011)
						{
							this.activate_MudoratheSwordOracle_2 = true;
						}
						else if (base.Card.Id == 63542003)
						{
							this.activate_KeldotheSacredProtector_2 = true;
						}
						return true;
					}
				}
				foreach (ClientCard card2 in base.Duel.CurrentChain)
				{
					if (card2 != null && card2.Controller != 0 && card2.Location == CardLocation.Grave && this.key_send_to_deck_ids.Contains(card2.Id))
					{
						if (base.Card.Id == 99937011)
						{
							this.activate_MudoratheSwordOracle_2 = true;
						}
						else if (base.Card.Id == 63542003)
						{
							this.activate_KeldotheSacredProtector_2 = true;
						}
						return true;
					}
				}
				if (base.Duel.Phase == DuelPhase.End && base.Bot.Deck.Count < 3 && base.Bot.Graveyard.Count > 0)
				{
					if (base.Card.Id == 99937011)
					{
						this.activate_MudoratheSwordOracle_2 = true;
					}
					else if (base.Card.Id == 63542003)
					{
						this.activate_KeldotheSacredProtector_2 = true;
					}
					return true;
				}
				return false;
			}
			else
			{
				if (base.Duel.Phase == DuelPhase.End && base.Bot.Deck.Count < 3 && base.Bot.Graveyard.Count > 0)
				{
					if (base.Card.Id == 99937011)
					{
						this.activate_MudoratheSwordOracle_2 = true;
					}
					else if (base.Card.Id == 63542003)
					{
						this.activate_KeldotheSacredProtector_2 = true;
					}
					return true;
				}
				return false;
			}
		}

		// Token: 0x06001FA1 RID: 8097 RVA: 0x000CA500 File Offset: 0x000C8700
		private bool TearlamentsScheirenEffect()
		{
			if (base.Card.Location == CardLocation.Grave)
			{
				return this.FusionEffect(572850);
			}
			if (this.AllActivated())
			{
				if (!base.Bot.GetMonsters().Any((ClientCard card) => card != null && card.IsFaceup() && card.Level == 4) || !base.Bot.HasInExtra(21044178))
				{
					return false;
				}
			}
			this.activate_TearlamentsScheiren_1 = true;
			this.SetSpSummon();
			return true;
		}

		// Token: 0x06001FA2 RID: 8098 RVA: 0x000CA583 File Offset: 0x000C8783
		private bool NaelshaddollArielEffect()
		{
			if (base.Card.Location == CardLocation.Grave)
			{
				return base.Enemy.Graveyard.Count((ClientCard card) => card != null && !this.key_no_remove_ids.Contains(card.Id)) > 0;
			}
			this.SetSpSummon();
			return true;
		}

		// Token: 0x06001FA3 RID: 8099 RVA: 0x000CA5BC File Offset: 0x000C87BC
		private bool IsShouldSummonFusion(int setcode = -1, int race = -1, bool all = false, int flag = 31)
		{
			List<ClientCard> cards = (from card in this.GetZoneCards(CardLocation.MonsterZone, base.Bot)
				where card != null && card.IsFaceup()
				select card).ToList<ClientCard>();
			cards.AddRange(this.GetZoneCards((CardLocation)18, base.Bot));
			int xcount_ = 0;
			int xcount_2 = 0;
			int xcount_3 = 0;
			if ((flag & 1) > 0)
			{
				if (base.Bot.ExtraDeck.Count((ClientCard card) => card != null && card.Id == 92731385) > 0)
				{
					foreach (ClientCard card6 in cards)
					{
						if (card6 != null && (card6.Id != 84330567 || card6.Location != CardLocation.MonsterZone || card6.IsDisabled()))
						{
							if (card6.HasSetcode(this.SETCODE) && card6.HasType(CardType.Monster))
							{
								xcount_++;
								if (card6.HasRace(CardRace.Aqua))
								{
									xcount_--;
									xcount_3++;
								}
							}
							else if (card6.HasRace(CardRace.Aqua))
							{
								xcount_2++;
							}
						}
					}
					if (setcode == this.SETCODE)
					{
						xcount_++;
						if (race == 64)
						{
							xcount_--;
							xcount_3++;
						}
					}
					else if (race == 64)
					{
						xcount_2++;
					}
					if (xcount_3 > 1 || (xcount_ > 0 && xcount_3 > 0) || (xcount_ > 0 && xcount_2 > 0) || (xcount_2 > 0 && xcount_3 > 0))
					{
						return true;
					}
				}
			}
			if ((flag & 2) > 0)
			{
				if (base.Bot.ExtraDeck.Count((ClientCard card) => card != null && card.Id == 84330567) > 0)
				{
					xcount_ = 0;
					xcount_2 = 0;
					foreach (ClientCard card2 in cards)
					{
						if (card2 != null)
						{
							if (card2.Id == 92731385 && xcount_ <= 0)
							{
								xcount_++;
							}
							else if (card2.HasSetcode(this.SETCODE) && card2.HasType(CardType.Monster))
							{
								xcount_2++;
							}
						}
					}
					if (setcode == this.SETCODE)
					{
						xcount_2++;
					}
					if (xcount_ > 0 && xcount_2 > 0)
					{
						return true;
					}
				}
			}
			if ((flag & 4) > 0)
			{
				if (base.Bot.ExtraDeck.Count((ClientCard card) => card != null && card.Id == 28226490) > 0)
				{
					xcount_ = 0;
					xcount_2 = 0;
					foreach (ClientCard card3 in cards)
					{
						if (card3 != null && (card3.Id != 84330567 || card3.Location != CardLocation.MonsterZone || card3.IsDisabled()))
						{
							if (card3.Id == 73956664 && xcount_ <= 0)
							{
								xcount_++;
							}
							else if (card3.HasRace(CardRace.Aqua))
							{
								xcount_2++;
							}
						}
					}
					if (race == 64)
					{
						xcount_2++;
					}
					if (xcount_ > 0 && xcount_2 > 0)
					{
						return true;
					}
				}
			}
			if ((flag & 8) > 0)
			{
				if (base.Bot.ExtraDeck.Count((ClientCard card) => card != null && card.Id == 69946549) > 0)
				{
					xcount_ = 0;
					foreach (ClientCard card4 in cards)
					{
						if (card4 != null && (card4.Id != 84330567 || card4.Location != CardLocation.MonsterZone || card4.IsDisabled()) && ((base.Bot.GetMonstersInMainZone().Count > 4 && card4.Location == CardLocation.MonsterZone) || base.Bot.GetMonstersInMainZone().Count <= 4) && card4.HasType(CardType.Fusion))
						{
							xcount_++;
						}
					}
					if (xcount_ > 0)
					{
						return true;
					}
				}
			}
			if (all && (flag & 16) > 0)
			{
				if (base.Bot.ExtraDeck.Count((ClientCard card) => card != null && card.Id == 94977269) > 0)
				{
					List<ClientCard> materials_ = new List<ClientCard>();
					List<ClientCard> materials_2 = new List<ClientCard>();
					foreach (ClientCard card5 in cards)
					{
						if (card5 != null)
						{
							if (card5.HasSetcode(157))
							{
								materials_.Add(card5);
							}
							else if ((card5.Id == 37961969 || card5.Id == 74078255 || card5.Id == 572850) && card5.HasAttribute(CardAttribute.Dark))
							{
								materials_2.Add(card5);
							}
						}
					}
					xcount_ = materials_.Count;
					xcount_2 = materials_2.Count;
					if (setcode == this.SETCODE && race == 64)
					{
						xcount_2++;
					}
					if (xcount_ > 0 && xcount_2 > 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001FA4 RID: 8100 RVA: 0x000CAADC File Offset: 0x000C8CDC
		private bool TearlamentsRulkallosEffect()
		{
			if (base.Card.Location == CardLocation.Grave)
			{
				this.activate_TearlamentsRulkallos_2 = true;
				this.SetSpSummon();
				return true;
			}
			if (base.Card.IsDisabled())
			{
				return false;
			}
			this.activate_TearlamentsRulkallos_1 = true;
			return true;
		}

		// Token: 0x06001FA5 RID: 8101 RVA: 0x000CAB14 File Offset: 0x000C8D14
		private bool AgidotheAncientSentinelEffect()
		{
			if (base.Card.Location != CardLocation.Hand)
			{
				if (this.AllActivated())
				{
					if (!base.Duel.CurrentChain.Any((ClientCard card) => card != null && card.Controller == 0 && (card.Id == 37961969 || card.Id == 74078255 || card.Id == 572850)))
					{
						return false;
					}
				}
				this.activate_AgidotheAncientSentinel_2 = true;
				return true;
			}
			if (base.Duel.Player == 1)
			{
				return false;
			}
			if (!base.Bot.HasInGraveyard(25926710) && !base.Bot.HasInGraveyard(99937011) && !base.Bot.HasInGraveyard(63542003))
			{
				if (base.Bot.Hand.Count((ClientCard card) => card != null && card.Id == 62320425) <= 1)
				{
					return false;
				}
			}
			if (base.Bot.GetMonstersInMainZone().Count < 4)
			{
				if (!this.activate_AgidotheAncientSentinel_2 && (base.Bot.HasInHand(17266660) || base.Bot.HasInHand(21074344)))
				{
					if (base.Bot.Hand.Count((ClientCard card) => card != null && card.Id == 62320425) <= 1)
					{
						return false;
					}
				}
				this.SetSpSummon();
				return true;
			}
			return false;
		}

		// Token: 0x06001FA6 RID: 8102 RVA: 0x000CAC6C File Offset: 0x000C8E6C
		private bool AllActivated()
		{
			return (this.activate_TearlamentsScheiren_2 || this.CheckRemainInDeck(572850) <= 0) && (this.activate_TearlamentsHavnis_2 || this.CheckRemainInDeck(37961969) <= 0) && (this.activate_TearlamentsMerrli_2 || this.CheckRemainInDeck(74078255) <= 0);
		}

		// Token: 0x06001FA7 RID: 8103 RVA: 0x000CACC4 File Offset: 0x000C8EC4
		private bool KelbektheAncientVanguardEffect()
		{
			if (base.Card.Location != CardLocation.Hand)
			{
				if (this.AllActivated())
				{
					if (!base.Duel.CurrentChain.Any((ClientCard card) => card != null && card.Controller == 0 && (card.Id == 37961969 || card.Id == 74078255 || card.Id == 572850)))
					{
						return false;
					}
				}
				this.activate_KelbektheAncientVanguard_2 = true;
				return true;
			}
			if (base.Duel.LastChainPlayer == 0)
			{
				return false;
			}
			if ((base.Bot.HasInHand(17266660) || base.Bot.HasInHand(21074344)) && !this.activate_KelbektheAncientVanguard_2)
			{
				return false;
			}
			List<ClientCard> cards = base.Enemy.GetMonsters();
			cards.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			cards.Reverse();
			base.AI.SelectCard(cards);
			this.SetSpSummon();
			return true;
		}

		// Token: 0x06001FA8 RID: 8104 RVA: 0x000CAD97 File Offset: 0x000C8F97
		private bool TearlamentsMerrliEffect()
		{
			if (base.Card.Location != CardLocation.MonsterZone)
			{
				return this.FusionEffect(74078255);
			}
			if (this.AllActivated())
			{
				return false;
			}
			this.activate_TearlamentsMerrli_1 = true;
			return true;
		}

		// Token: 0x06001FA9 RID: 8105 RVA: 0x000CADC5 File Offset: 0x000C8FC5
		private bool TearlamentsHavnisEffect()
		{
			if (base.Card.Location != CardLocation.Hand)
			{
				return this.FusionEffect(37961969);
			}
			if (this.AllActivated())
			{
				return false;
			}
			this.activate_TearlamentsHavnis_1 = true;
			return true;
		}

		// Token: 0x06001FAA RID: 8106 RVA: 0x000CADF4 File Offset: 0x000C8FF4
		private bool SpellSet()
		{
			if (base.Card.Id == 74920585)
			{
				return !base.Bot.GetSpells().Any((ClientCard card) => card != null && card.Id == 74920585 && (card.IsFacedown() || (card.IsFaceup() && !card.IsDisabled())));
			}
			return base.Card.HasType(CardType.QuickPlay) || base.Card.HasType(CardType.Trap);
		}

		// Token: 0x06001FAB RID: 8107 RVA: 0x000CAE68 File Offset: 0x000C9068
		private bool TearlamentsReinoheartEffect()
		{
			if (base.Card.Location == CardLocation.Grave)
			{
				List<ClientCard> cards = base.Bot.Hand.Where((ClientCard card) => card != null && card.HasSetcode(this.SETCODE)).ToList<ClientCard>();
				foreach (ClientCard card2 in new List<ClientCard>(cards))
				{
					if ((card2.Id != 572850 || this.activate_TearlamentsScheiren_1) && (card2.Id != 74078255 || this.summoned || this.activate_TearlamentsMerrli_1))
					{
						if (card2.Id != 37961969)
						{
							continue;
						}
						if (cards.Count((ClientCard ccard) => ccard != null && ccard.Id == 37961969) <= 1)
						{
							continue;
						}
					}
					cards.Remove(card2);
				}
				if (cards.Count<ClientCard>() <= 0)
				{
					return false;
				}
				this.activate_TearlamentsReinoheart_2 = true;
				this.SetSpSummon();
				return true;
			}
			else
			{
				if (this.AllActivated())
				{
					return false;
				}
				this.activate_TearlamentsReinoheart_1 = true;
				return true;
			}
		}

		// Token: 0x06001FAC RID: 8108 RVA: 0x000CAF84 File Offset: 0x000C9184
		private bool ShaddollDragonEffect()
		{
			if (base.Card.Location != CardLocation.Grave)
			{
				return true;
			}
			ClientCard card = base.Util.GetBestEnemySpell(false);
			List<ClientCard> cards = (from ccard in base.Enemy.GetSpells()
				where this.tgcard == null || this.tgcard != ccard
				select ccard).ToList<ClientCard>();
			if (card != null && (this.tgcard == null || this.tgcard != card))
			{
				base.AI.SelectCard(card);
				return true;
			}
			if (cards.Count > 0)
			{
				base.AI.SelectCard(cards);
				return true;
			}
			return false;
		}

		// Token: 0x06001FAD RID: 8109 RVA: 0x000CB00B File Offset: 0x000C920B
		private bool TearlamentsKitkallosEffect_2()
		{
			return base.Bot.HasInMonstersZone(94977269, true, false, true) && this.TearlamentsKitkallosEffect();
		}

		// Token: 0x06001FAE RID: 8110 RVA: 0x000CB02C File Offset: 0x000C922C
		private bool TearlamentsKitkallosEffect()
		{
			if (base.ActivateDescription == base.Util.GetStringId(92731385, 1))
			{
				if (base.Card.IsDisabled())
				{
					return false;
				}
				if (this.AllActivated() && !base.Bot.HasInGraveyard(28226490) && !base.Bot.HasInGraveyard(84330567) && !base.Bot.HasInGraveyard(92731385))
				{
					return false;
				}
				if (base.Bot.HasInMonstersZone(94977269, true, false, true))
				{
					base.AI.SelectCard(94977269);
				}
				else if (base.Bot.HasInMonstersZone(92731385, false, false, false) && !this.activate_TearlamentsKitkallos_3 && !this.AllActivated())
				{
					base.AI.SelectCard(92731385);
				}
				else if (!this.activate_TearlamentsScheiren_2 && base.Bot.HasInMonstersZone(572850, false, false, false) && this.IsShouldSummonFusion(-1, -1, false, 31))
				{
					base.AI.SelectCard(572850);
				}
				else if (!this.activate_TearlamentsMerrli_2 && base.Bot.HasInMonstersZone(74078255, false, false, false) && this.IsShouldSummonFusion(-1, -1, false, 31))
				{
					base.AI.SelectCard(74078255);
				}
				else if (!this.activate_TearlamentsHavnis_2 && base.Bot.HasInMonstersZone(37961969, false, false, false) && this.IsShouldSummonFusion(-1, -1, false, 31))
				{
					base.AI.SelectCard(37961969);
				}
				else if (base.Bot.HasInMonstersZone(73956664, false, false, false) && !this.activate_TearlamentsReinoheart_1 && !this.AllActivated())
				{
					base.AI.SelectCard(73956664);
				}
				else
				{
					if (base.Bot.HasInMonstersZone(40177746, false, false, false))
					{
						if (base.Bot.Graveyard.Count((ClientCard card) => card != null && card.HasAttribute(CardAttribute.Light) && card.HasRace(CardRace.Fairy)) > 0 && (this.CheckRemainInDeck(21074344) > 0 || this.CheckRemainInDeck(17266660) > 0))
						{
							base.AI.SelectCard(40177746);
							goto IL_0319;
						}
					}
					if (!base.Bot.HasInExtra(84330567) || base.Bot.HasInGraveyard(92731385))
					{
						if (base.Bot.HasInExtra(69946549))
						{
							if (base.Bot.Graveyard.Count((ClientCard card) => card != null && card.HasType(CardType.Fusion)) <= 0)
							{
								goto IL_02A6;
							}
						}
						if (base.Bot.HasInMonstersZone(77723643, false, false, false) && base.Enemy.GetSpellCount() > 0)
						{
							base.AI.SelectCard(77723643);
							goto IL_0319;
						}
						List<ClientCard> mcards = this.GetZoneCards(CardLocation.MonsterZone, base.Bot);
						mcards.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
						base.AI.SelectCard(mcards);
						goto IL_0319;
					}
					IL_02A6:
					base.AI.SelectCard(92731385);
				}
				IL_0319:
				if (!this.activate_TearlamentsKitkallos_1 && base.Bot.HasInGraveyard(92731385))
				{
					base.AI.SelectNextCard(base.Bot.Graveyard.Where((ClientCard card) => card != null && card.Id == 92731385).ToList<ClientCard>());
				}
				if (!this.activate_TearlamentsMerrli_1 && base.Bot.HasInGraveyard(74078255) && !this.AllActivated())
				{
					base.AI.SelectNextCard(base.Bot.Graveyard.Where((ClientCard card) => card != null && card.Id == 74078255).ToList<ClientCard>());
				}
				else if (!this.activate_TearlamentsMerrli_1 && base.Bot.HasInHand(74078255) && !this.AllActivated())
				{
					base.AI.SelectNextCard(base.Bot.Hand.Where((ClientCard card) => card != null && card.Id == 74078255).ToList<ClientCard>());
				}
				else if (!this.activate_TearlamentsReinoheart_1 && base.Bot.HasInGraveyard(73956664) && !this.AllActivated())
				{
					base.AI.SelectNextCard(base.Bot.Graveyard.Where((ClientCard card) => card != null && card.Id == 73956664).ToList<ClientCard>());
				}
				else if (!this.activate_TearlamentsReinoheart_1 && base.Bot.HasInHand(73956664) && !this.AllActivated())
				{
					base.AI.SelectNextCard(base.Bot.Hand.Where((ClientCard card) => card != null && card.Id == 73956664).ToList<ClientCard>());
				}
				else
				{
					base.AI.SelectNextCard(new int[] { 28226490, 84330567, 92731385 });
				}
				this.activate_TearlamentsKitkallos_2 = true;
				this.SetSpSummon();
				return true;
			}
			else if (base.Card.Location == CardLocation.Grave)
			{
				if (this.AllActivated())
				{
					return false;
				}
				this.activate_TearlamentsKitkallos_3 = true;
				return true;
			}
			else
			{
				if (base.Card.IsDisabled())
				{
					return false;
				}
				this.activate_TearlamentsKitkallos_1 = true;
				return true;
			}
		}

		// Token: 0x06001FAF RID: 8111 RVA: 0x000CB5A4 File Offset: 0x000C97A4
		private bool HasinZoneKeyCard(CardLocation loc)
		{
			if (loc == CardLocation.Hand)
			{
				if (!this.activate_TearlamentsScheiren_2 && base.Bot.HasInHand(572850))
				{
					return true;
				}
				if (!this.activate_TearlamentsHavnis_2 && base.Bot.HasInHand(37961969))
				{
					return true;
				}
				if (!this.activate_TearlamentsMerrli_2 && base.Bot.HasInHand(74078255))
				{
					return true;
				}
			}
			if (loc == CardLocation.Deck)
			{
				if (!this.activate_TearlamentsScheiren_2 && this.CheckRemainInDeck(572850) > 0)
				{
					return true;
				}
				if (!this.activate_TearlamentsHavnis_2 && this.CheckRemainInDeck(37961969) > 0)
				{
					return true;
				}
				if (!this.activate_TearlamentsMerrli_2 && this.CheckRemainInDeck(74078255) > 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001FB0 RID: 8112 RVA: 0x000CB658 File Offset: 0x000C9858
		private bool TearlamentsSulliekEffect()
		{
			if (this.SpellActivate())
			{
				return true;
			}
			if (base.Card.Location != CardLocation.SpellZone)
			{
				return true;
			}
			ClientCard card = base.Util.GetLastChainCard();
			if (card == null || card.Controller != 1 || card.Location != CardLocation.MonsterZone || card.IsShouldNotBeTarget())
			{
				if (base.Duel.Player == 1 || base.Duel.Phase == DuelPhase.End)
				{
					if (base.Duel.LastChainPlayer != 0)
					{
						if (base.Duel.CurrentChain.Count((ClientCard ccard) => ccard != null && ccard.Controller == 0 && (ccard.Id == 37961969 || ccard.Id == 74078255 || ccard.Id == 572850)) <= 0)
						{
							if (((base.Bot.HasInMonstersZone(92731385, false, false, true) && !this.activate_TearlamentsKitkallos_3 && this.IsShouldSummonFusion(this.SETCODE, 64, false, 31) && ((this.CheckRemainInDeck(37961969) > 0 && !this.activate_TearlamentsHavnis_2) || (this.CheckRemainInDeck(74078255) > 0 && !this.activate_TearlamentsMerrli_2) || (this.CheckRemainInDeck(572850) > 0 && !this.activate_TearlamentsScheiren_2)) && base.Bot.GetMonsterCount() < 3) || (this.IsCanFusionSummon() && (base.Bot.HasInMonstersZone(572850, false, false, false) || base.Bot.HasInMonstersZone(37961969, false, false, false) || base.Bot.HasInMonstersZone(74078255, false, false, false))) || (!this.activate_TearlamentsReinoheart_2 && base.Bot.HasInMonstersZone(73956664, false, false, false) && (this.HasinZoneKeyCard(CardLocation.Hand) || (base.Bot.Hand.Count((ClientCard ccard) => ccard != null && ccard.HasSetcode(this.SETCODE)) > 0 && this.HasinZoneKeyCard(CardLocation.Deck))) && this.IsShouldSummonFusion(this.SETCODE, 64, false, 31))) && this.IsCanSpSummon())
							{
								List<ClientCard> cards = (from ecard in base.Enemy.GetMonsters()
									where ecard != null && !ecard.IsShouldNotBeTarget() && ecard.IsFaceup()
									select ecard).ToList<ClientCard>();
								if (cards.Count > 0)
								{
									cards.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
									cards.Reverse();
									this.chain_TearlamentsSulliek = cards[0];
								}
								else
								{
									this.chain_TearlamentsSulliek = null;
								}
								return true;
							}
							return false;
						}
					}
					return false;
				}
				return false;
			}
			if (this._PredaplantDragostapelia != null && this._PredaplantDragostapelia.Location == CardLocation.MonsterZone && this._PredaplantDragostapelia.IsFaceup() && this.e_PredaplantDragostapelia_cards.Contains(card))
			{
				return false;
			}
			this.chain_TearlamentsSulliek = card;
			return true;
		}

		// Token: 0x0400220A RID: 8714
		private const bool IS_YGOPRO = true;

		// Token: 0x0400220B RID: 8715
		private int SETCODE = 385;

		// Token: 0x0400220C RID: 8716
		private bool activate_TearlamentsScheiren_1;

		// Token: 0x0400220D RID: 8717
		private bool activate_TearlamentsScheiren_2;

		// Token: 0x0400220E RID: 8718
		private bool activate_TearlamentsReinoheart_1;

		// Token: 0x0400220F RID: 8719
		private bool activate_TearlamentsReinoheart_2;

		// Token: 0x04002210 RID: 8720
		private bool activate_TearlamentsHavnis_1;

		// Token: 0x04002211 RID: 8721
		private bool activate_TearlamentsHavnis_2;

		// Token: 0x04002212 RID: 8722
		private bool activate_TearlamentsMerrli_1;

		// Token: 0x04002213 RID: 8723
		private bool activate_TearlamentsMerrli_2;

		// Token: 0x04002214 RID: 8724
		private bool activate_TearlamentsKitkallos_1;

		// Token: 0x04002215 RID: 8725
		private bool activate_TearlamentsKitkallos_2;

		// Token: 0x04002216 RID: 8726
		private bool activate_TearlamentsKitkallos_3;

		// Token: 0x04002217 RID: 8727
		private bool TearlamentsKitkallostohand = true;

		// Token: 0x04002218 RID: 8728
		private bool activate_TearlamentsScream_1;

		// Token: 0x04002219 RID: 8729
		private bool activate_TearlamentsScream_2;

		// Token: 0x0400221A RID: 8730
		private bool activate_TearlamentsSulliek_1;

		// Token: 0x0400221B RID: 8731
		private bool activate_TearlamentsSulliek_2;

		// Token: 0x0400221C RID: 8732
		private bool activate_AgidotheAncientSentinel_2;

		// Token: 0x0400221D RID: 8733
		private bool activate_KelbektheAncientVanguard_2;

		// Token: 0x0400221E RID: 8734
		private bool activate_TearlamentsRulkallos_1;

		// Token: 0x0400221F RID: 8735
		private bool activate_TearlamentsRulkallos_2;

		// Token: 0x04002220 RID: 8736
		private bool activate_MudoratheSwordOracle_2;

		// Token: 0x04002221 RID: 8737
		private bool activate_KeldotheSacredProtector_2;

		// Token: 0x04002222 RID: 8738
		private bool activate_PrimevalPlanetPerlereino_1;

		// Token: 0x04002223 RID: 8739
		private bool activate_PrimevalPlanetPerlereino_2;

		// Token: 0x04002224 RID: 8740
		private bool activate_TearlamentsKaleidoHeart_1;

		// Token: 0x04002225 RID: 8741
		private bool activate_TearlamentsKaleidoHeart_2;

		// Token: 0x04002226 RID: 8742
		private bool activate_Eva;

		// Token: 0x04002227 RID: 8743
		private bool activate_DivineroftheHerald;

		// Token: 0x04002228 RID: 8744
		private bool summoned;

		// Token: 0x04002229 RID: 8745
		private bool spsummoned;

		// Token: 0x0400222A RID: 8746
		private bool TearlamentsKitkallos_summoned;

		// Token: 0x0400222B RID: 8747
		private bool summon_SprightElf;

		// Token: 0x0400222C RID: 8748
		private bool chainlist;

		// Token: 0x0400222D RID: 8749
		private bool pre_activate_PrimevalPlanetPerlereino;

		// Token: 0x0400222E RID: 8750
		private bool select_TearlamentsKitkallos;

		// Token: 0x0400222F RID: 8751
		private List<ClientCard> remainCards = new List<ClientCard>();

		// Token: 0x04002230 RID: 8752
		private List<ClientCard> fusionExtra = new List<ClientCard>();

		// Token: 0x04002231 RID: 8753
		private List<ClientCard> fusionMaterial = new List<ClientCard>();

		// Token: 0x04002232 RID: 8754
		private List<ClientCard> on_chaining_cards = new List<ClientCard>();

		// Token: 0x04002233 RID: 8755
		private List<ClientCard> mcard_0 = new List<ClientCard> { null, null, null };

		// Token: 0x04002234 RID: 8756
		private List<ClientCard> mcard_1 = new List<ClientCard> { null, null, null };

		// Token: 0x04002235 RID: 8757
		private List<ClientCard> mcard_2 = new List<ClientCard> { null, null, null };

		// Token: 0x04002236 RID: 8758
		private List<ClientCard> mcard_3 = new List<ClientCard> { null, null, null };

		// Token: 0x04002237 RID: 8759
		private List<bool> ran_fusion_mode_0 = new List<bool> { false, false, false };

		// Token: 0x04002238 RID: 8760
		private List<bool> ran_fusion_mode_1 = new List<bool> { false, false, false };

		// Token: 0x04002239 RID: 8761
		private List<bool> ran_fusion_mode_2 = new List<bool> { false, false, false };

		// Token: 0x0400223A RID: 8762
		private List<bool> ran_fusion_mode_3 = new List<bool> { false, false, false };

		// Token: 0x0400223B RID: 8763
		private ClientCard _PredaplantDragostapelia;

		// Token: 0x0400223C RID: 8764
		private ClientCard chain_PredaplantDragostapelia;

		// Token: 0x0400223D RID: 8765
		private ClientCard chain_TearlamentsSulliek;

		// Token: 0x0400223E RID: 8766
		private ClientCard tgcard;

		// Token: 0x0400223F RID: 8767
		private ClientCard no_fusion_card;

		// Token: 0x04002240 RID: 8768
		private List<ClientCard> e_PredaplantDragostapelia_cards = new List<ClientCard>();

		// Token: 0x04002241 RID: 8769
		private ClientCard link_card;

		// Token: 0x04002242 RID: 8770
		private List<int> no_link_ids = new List<int> { 84330567, 84815190, 21887175, 21044178, 69946549, 98127546, 33158448, 28226490 };

		// Token: 0x04002243 RID: 8771
		private List<int> key_send_to_deck_ids = new List<int> { 74078255, 572850, 37961969, 73956664, 8736823, 98715423, 17484499 };

		// Token: 0x04002244 RID: 8772
		private List<int> all_key_card_ids = new List<int>
		{
			55623480, 86682165, 60461804, 10000090, 28651380, 97565997, 87074380, 80208158, 95440946, 93880808,
			16261341, 91749600, 26866984, 5141117, 50383626, 30576089, 22586618, 7445307, 73478096, 18558867,
			51617185, 60880471, 34172284, 88774734, 25451383, 71197066, 24226942, 78077209, 98787535, 29601381,
			83203672, 62383431, 89552119, 92418590, 31042659, 83303851, 17502671, 11366199, 46668237, 64382839,
			46290741, 25607552, 1295442, 5560911, 56174248, 7407724, 1855886, 27198001, 11074235, 48372950,
			94142993, 81866673, 34966096, 42006475, 99733359, 20799347, 71985676, 55787576, 43266605, 3422200,
			97962972, 20773176, 84976088, 55151012, 80208323, 98881700, 56677752, 24506253, 6180710, 20056760,
			44928016, 45702014, 60316373, 37683547, 62962630, 20065259, 8571567, 21351206, 35998832, 26077387,
			37351133, 94730900, 83682209, 38695361, 59707204, 12469386, 1833916, 98806751, 82496097, 27182739,
			28762303, 74578072, 30303854, 5370235, 50546208, 44818, 67436768, 29169993, 10286023, 19667590,
			47897376, 74891384, 66752837, 43534808, 56815977, 81344070, 59724555, 93708824, 43411796, 7084129,
			51993760, 87988305, 23619206, 86962245, 48144778, 30068120, 9047460, 25538345, 69764158, 70645913,
			55702233, 10928224, 79531196, 23893227, 57421866, 94693857, 78080961, 9742784, 59185998, 93169863,
			46576366, 2830693, 68543408, 50820852, 2511, 8972398, 61488417, 71734607, 63180841, 51447164,
			74586817, 28403802, 91575236, 286392, 97584719, 60195675, 24701066, 20665527, 73345237, 23732205,
			146746, 72218246, 41999284, 60303245
		};

		// Token: 0x04002245 RID: 8773
		private List<int> key_no_send_to_deck_ids = new List<int> { 84330567, 50588353, 44097050 };

		// Token: 0x04002246 RID: 8774
		private List<int> key_remove_ids = new List<int>
		{
			74078255, 572850, 37961969, 73956664, 15291624, 11738489, 44097050, 15291624, 63288573, 70369116,
			83152482, 72329844, 24094258, 86066372, 74997493, 85289965, 21887175, 11738489, 98127546, 50588353,
			10389142, 90590303, 27548199
		};

		// Token: 0x04002247 RID: 8775
		private List<int> key_no_remove_ids = new List<int>
		{
			18743376, 72355272, 81555617, 45960523, 29596581, 56713174, 80280944, 61103515, 28297833, 90020780,
			8736823
		};

		// Token: 0x04002248 RID: 8776
		private List<int> bot_send_to_deck_ids = new List<int>();

		// Token: 0x020003F8 RID: 1016
		public class CardId
		{
			// Token: 0x04002249 RID: 8777
			public const int ShaddollBeast = 3717252;

			// Token: 0x0400224A RID: 8778
			public const int ShaddollDragon = 77723643;

			// Token: 0x0400224B RID: 8779
			public const int TearlamentsScheiren = 572850;

			// Token: 0x0400224C RID: 8780
			public const int TearlamentsReinoheart = 73956664;

			// Token: 0x0400224D RID: 8781
			public const int KelbektheAncientVanguard = 25926710;

			// Token: 0x0400224E RID: 8782
			public const int MudoratheSwordOracle = 99937011;

			// Token: 0x0400224F RID: 8783
			public const int AgidotheAncientSentinel = 62320425;

			// Token: 0x04002250 RID: 8784
			public const int KeldotheSacredProtector = 63542003;

			// Token: 0x04002251 RID: 8785
			public const int NaelshaddollAriel = 97518132;

			// Token: 0x04002252 RID: 8786
			public const int TearlamentsHavnis = 37961969;

			// Token: 0x04002253 RID: 8787
			public const int TearlamentsMerrli = 74078255;

			// Token: 0x04002254 RID: 8788
			public const int DivineroftheHerald = 92919429;

			// Token: 0x04002255 RID: 8789
			public const int HeraldofOrangeLight = 17266660;

			// Token: 0x04002256 RID: 8790
			public const int HeraldofGreenLight = 21074344;

			// Token: 0x04002257 RID: 8791
			public const int Eva = 40177746;

			// Token: 0x04002258 RID: 8792
			public const int TearlamentsScream = 6767771;

			// Token: 0x04002259 RID: 8793
			public const int PrimevalPlanetPerlereino = 77103950;

			// Token: 0x0400225A RID: 8794
			public const int TearlamentsSulliek = 74920585;

			// Token: 0x0400225B RID: 8795
			public const int TearlamentsKaleidoHeart = 28226490;

			// Token: 0x0400225C RID: 8796
			public const int TearlamentsRulkallos = 84330567;

			// Token: 0x0400225D RID: 8797
			public const int PredaplantDragostapelia = 69946549;

			// Token: 0x0400225E RID: 8798
			public const int TearlamentsKitkallos = 92731385;

			// Token: 0x0400225F RID: 8799
			public const int ElShaddollWinda = 94977269;

			// Token: 0x04002260 RID: 8800
			public const int ElderEntityNtss = 80532587;

			// Token: 0x04002261 RID: 8801
			public const int BaronnedeFleur = 84815190;

			// Token: 0x04002262 RID: 8802
			public const int FADawnDragster = 33158448;

			// Token: 0x04002263 RID: 8803
			public const int AbyssDweller = 21044178;

			// Token: 0x04002264 RID: 8804
			public const int UnderworldGoddessoftheClosedWorld = 98127546;

			// Token: 0x04002265 RID: 8805
			public const int MekkKnightCrusadiaAvramax = 21887175;

			// Token: 0x04002266 RID: 8806
			public const int KnightmareUnicorn = 38342335;

			// Token: 0x04002267 RID: 8807
			public const int SprightElf = 27381364;

			// Token: 0x04002268 RID: 8808
			public const int IP = 65741786;
		}

		// Token: 0x020003F9 RID: 1017
		private enum Flag
		{
			// Token: 0x0400226A RID: 8810
			TearlamentsKitkallos = 1,
			// Token: 0x0400226B RID: 8811
			TearlamentsRulkallos,
			// Token: 0x0400226C RID: 8812
			TearlamentsKaleidoHeart = 4,
			// Token: 0x0400226D RID: 8813
			PredaplantDragostapelia = 8,
			// Token: 0x0400226E RID: 8814
			ElShaddollWinda = 16
		}
	}
}
