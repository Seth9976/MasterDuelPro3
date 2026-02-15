using System;
using System.Collections.Generic;
using System.Linq;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x020003D1 RID: 977
	[Deck("Salamangreat", "AI_Salamangreat", "Normal")]
	internal class SalamangreatExecutor : DefaultExecutor
	{
		// Token: 0x06001DBE RID: 7614 RVA: 0x000B3150 File Offset: 0x000B1350
		public SalamangreatExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.Activate, 18144507);
			base.AddExecutor(ExecutorType.Activate, 23434538, new Func<bool>(this.G_activate));
			base.AddExecutor(ExecutorType.Activate, 24224830, new Func<bool>(this.Called_activate));
			base.AddExecutor(ExecutorType.Activate, 14558127, new Func<bool>(this.Hand_act_eff));
			base.AddExecutor(ExecutorType.Activate, 97268402, new Func<bool>(base.DefaultBreakthroughSkill));
			base.AddExecutor(ExecutorType.Activate, 10045474, new Func<bool>(this.Impermanence_activate));
			base.AddExecutor(ExecutorType.Activate, 51339637, new Func<bool>(this.SolemnJudgment_activate));
			base.AddExecutor(ExecutorType.Activate, 40605147, new Func<bool>(this.SolemnStrike_activate));
			base.AddExecutor(ExecutorType.Activate, 41420027, new Func<bool>(this.SolemnJudgment_activate));
			base.AddExecutor(ExecutorType.Activate, 1295111, new Func<bool>(this.Sanctuary_activate));
			base.AddExecutor(ExecutorType.Activate, 48815792);
			base.AddExecutor(ExecutorType.Activate, 87871125, new Func<bool>(this.Wolf_activate));
			base.AddExecutor(ExecutorType.Activate, 16188701, new Func<bool>(this.Fadydebug_activate));
			base.AddExecutor(ExecutorType.Activate, 94620082, new Func<bool>(this.Foxy_activate));
			base.AddExecutor(ExecutorType.Activate, 20618081, new Func<bool>(this.Falco_activate));
			base.AddExecutor(ExecutorType.Activate, 52155219, new Func<bool>(this.Circle_activate));
			base.AddExecutor(ExecutorType.Activate, 85289965, new Func<bool>(this.Borrelsword_eff));
			base.AddExecutor(ExecutorType.Activate, 26889158, new Func<bool>(this.Gazelle_activate));
			base.AddExecutor(ExecutorType.Activate, 52277807, new Func<bool>(this.Spinny_activate));
			base.AddExecutor(ExecutorType.Activate, 87327776, new Func<bool>(this.Stallio_activate));
			base.AddExecutor(ExecutorType.Activate, 14812471);
			base.AddExecutor(ExecutorType.Activate, 56003780, new Func<bool>(this.JackJaguar_activate));
			base.AddExecutor(ExecutorType.Summon, 16188701);
			base.AddExecutor(ExecutorType.Summon, 94620082);
			base.AddExecutor(ExecutorType.Summon, 52277807);
			base.AddExecutor(ExecutorType.Summon, 56003780);
			base.AddExecutor(ExecutorType.Summon, 26889158);
			base.AddExecutor(ExecutorType.Summon, 89662401);
			base.AddExecutor(ExecutorType.Activate, 52277807, new Func<bool>(this.Spinny_activate));
			base.AddExecutor(ExecutorType.Activate, 41463181, new Func<bool>(base.DefaultMysticalSpaceTyphoon));
			base.AddExecutor(ExecutorType.SpSummon, 85289965, new Func<bool>(this.Borrelsword_ss));
			base.AddExecutor(ExecutorType.SpSummon, 14812471, new Func<bool>(this.Veilynx_summon));
			base.AddExecutor(ExecutorType.SpSummon, 87327776, new Func<bool>(this.Stallio_summon));
			base.AddExecutor(ExecutorType.Activate, 87327776, new Func<bool>(this.Stallio_activate));
			base.AddExecutor(ExecutorType.SpSummon, 48815792, new Func<bool>(this.Charmer_summon));
			base.AddExecutor(ExecutorType.SpSummon, 87871125, new Func<bool>(this.SunlightWolf_summon));
			base.AddExecutor(ExecutorType.SpSummon, 41463181, new Func<bool>(this.HeatLeo_summon));
			base.AddExecutor(ExecutorType.SpSummon, 6983839);
			base.AddExecutor(ExecutorType.Activate, 6983839, new Func<bool>(base.DefaultMysticalSpaceTyphoon));
			base.AddExecutor(ExecutorType.Activate, 14934922, new Func<bool>(this.Rage_activate));
			base.AddExecutor(ExecutorType.Activate, 89662401, new Func<bool>(this.Fowl_activate));
			base.AddExecutor(ExecutorType.Activate, 87871125, new Func<bool>(this.Wolf_activate));
			base.AddExecutor(ExecutorType.Activate, 26889158, new Func<bool>(this.Gazelle_activate));
			base.AddExecutor(ExecutorType.Activate, 81439174, new Func<bool>(this.FoolishBurial_activate));
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(base.DefaultMonsterRepos));
			base.AddExecutor(ExecutorType.SpellSet, new Func<bool>(this.SpellSet));
		}

		// Token: 0x06001DBF RID: 7615 RVA: 0x000B38BC File Offset: 0x000B1ABC
		public int get_Wolf_linkzone()
		{
			ClientCard WolfInExtra = (from x in base.Bot.GetMonstersInExtraZone()
				where x.Id == 87871125
				select x).ToList<ClientCard>().FirstOrDefault((ClientCard x) => x.Id == 87871125);
			if (WolfInExtra != null)
			{
				int zone = WolfInExtra.Position;
				if (zone == 5)
				{
					return 1;
				}
				if (zone == 6)
				{
					return 3;
				}
			}
			return -1;
		}

		// Token: 0x06001DC0 RID: 7616 RVA: 0x000B393C File Offset: 0x000B1B3C
		private bool Charmer_summon()
		{
			if (base.Duel.Phase != DuelPhase.Main1)
			{
				return false;
			}
			if (base.Duel.Turn == 1)
			{
				return false;
			}
			if (base.Enemy.Graveyard.Where((ClientCard x) => x.Attribute == 4).Count<ClientCard>() > 0)
			{
				if (base.Bot.GetMonstersInExtraZone().Count != 0)
				{
					if ((from x in base.Bot.GetMonstersInExtraZone()
						where (x.Id == 14812471 || x.Id == 87327776) && x.Owner == 0
						select x).Count<ClientCard>() != 1)
					{
						return false;
					}
				}
				List<ClientCard> material_list = new List<ClientCard>();
				List<ClientCard> monsters = base.Bot.GetMonsters();
				monsters.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				int link_count = 0;
				foreach (ClientCard card in monsters)
				{
					if (!card.IsFacedown() && !material_list.Contains(card) && card.LinkCount < 2)
					{
						material_list.Add(card);
						link_count += (card.HasType(CardType.Link) ? card.LinkCount : 1);
						if (link_count >= 4)
						{
							break;
						}
					}
				}
				if (link_count >= 3)
				{
					base.AI.SelectCard(14812471);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001DC1 RID: 7617 RVA: 0x000B3AA4 File Offset: 0x000B1CA4
		private bool HeatLeo_summon()
		{
			if (base.Duel.Turn == 1)
			{
				return false;
			}
			if (base.Duel.Phase != DuelPhase.Main1)
			{
				return false;
			}
			if (this.wasWolfSummonedUsingItself && base.Bot.GetMonsters().Count<ClientCard>() <= 3)
			{
				return false;
			}
			ClientCard self_best = base.Util.GetBestBotMonster(true);
			int self_power = ((self_best != null) ? self_best.Attack : 0);
			ClientCard enemy_best = base.Util.GetBestEnemyMonster(true, false);
			if (((enemy_best != null) ? enemy_best.GetDefensePower() : 0) < self_power)
			{
				return false;
			}
			if ((from x in base.Enemy.GetSpells()
				where x.IsFloodgate()
				select x).Count<ClientCard>() > 0)
			{
				List<ClientCard> material_list = new List<ClientCard>();
				List<ClientCard> monsters = base.Bot.GetMonsters();
				monsters.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				int link_count = 0;
				foreach (ClientCard card in monsters)
				{
					if (!card.IsFacedown() && !material_list.Contains(card) && card.LinkCount < 2)
					{
						material_list.Add(card);
						link_count += (card.HasType(CardType.Link) ? card.LinkCount : 1);
						if (link_count >= 3)
						{
							break;
						}
					}
				}
				if (link_count >= 3)
				{
					base.AI.SelectMaterials(material_list, 0);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001DC2 RID: 7618 RVA: 0x000B3C20 File Offset: 0x000B1E20
		private bool Stallio_summon()
		{
			if (!this.wasStallioActivated)
			{
				base.AI.SelectMaterials(52277807, 0);
				return true;
			}
			return false;
		}

		// Token: 0x06001DC3 RID: 7619 RVA: 0x000B3C40 File Offset: 0x000B1E40
		private bool SunlightWolf_summon()
		{
			if (!base.Bot.HasInMonstersZone(87871125, false, false, false))
			{
				this.wasWolfSummonedUsingItself = false;
				if (base.Bot.HasInMonstersZone(14812471, false, false, false))
				{
					if (base.Bot.HasInMonstersZone(87327776, false, false, false) && base.Bot.HasInMonstersZone(14812471, false, false, false) && base.Bot.HasInMonstersZone(26889158, false, false, false))
					{
						base.AI.SelectCard(14812471);
						base.AI.SelectNextCard(87327776);
					}
					else
					{
						base.AI.SelectCard(this.WolfMaterials);
						base.AI.SelectNextCard(this.WolfMaterials);
					}
					this.sunlightPosition = this.SelectSetPlace(new List<int> { 14812471 }, true);
					base.AI.SelectPlace(this.sunlightPosition);
				}
				return true;
			}
			if (this.wasWolfSummonedUsingItself)
			{
				return false;
			}
			if ((!this.wasFieldspellUsedThisTurn && base.Bot.HasInGraveyard(this.salamangreat_spellTrap)) || base.Bot.HasInHandOrInSpellZone(14934922))
			{
				base.AI.SelectOption(1);
				base.AI.SelectMaterials(new List<int> { 87871125, 14812471, 56003780, 26889158 }, 0);
				this.wasWolfSummonedUsingItself = true;
				base.AI.SelectPlace(this.sunlightPosition);
				return true;
			}
			return false;
		}

		// Token: 0x06001DC4 RID: 7620 RVA: 0x000B3DD4 File Offset: 0x000B1FD4
		private bool Wolf_activate()
		{
			this.wasWolfActivatedThisTurn = true;
			base.AI.SelectCard(new List<int> { 26889158, 51339637, 14934922, 94620082, 14558127, 89662401, 87871125, 14812471, 41463181, 52277807 });
			return true;
		}

		// Token: 0x06001DC5 RID: 7621 RVA: 0x000B3E68 File Offset: 0x000B2068
		private bool Stallio_activate()
		{
			if (base.Card.Location == CardLocation.MonsterZone)
			{
				this.wasStallioActivated = true;
				if (!this.wasGazelleSummonedThisTurn)
				{
					base.AI.SelectCard(new int[] { 26889158, 52277807 });
					base.AI.SelectNextCard(26889158);
					return true;
				}
				if (!base.Bot.HasInHandOrInMonstersZoneOrInGraveyard(56003780))
				{
					base.AI.SelectCard(26889158);
					base.AI.SelectNextCard(56003780);
					return true;
				}
				if (!base.Bot.HasInHandOrInMonstersZoneOrInGraveyard(20618081) && this.FalcoToGY(true))
				{
					base.AI.SelectCard(26889158);
					base.AI.SelectNextCard(20618081);
					return true;
				}
				base.AI.SelectCard(26889158);
				return true;
			}
			else
			{
				if (base.Util.GetBestEnemyMonster(false, true) != null)
				{
					base.AI.SelectCard(base.Util.GetBestEnemyMonster(false, true));
					return true;
				}
				return false;
			}
		}

		// Token: 0x06001DC6 RID: 7622 RVA: 0x000B3F78 File Offset: 0x000B2178
		private bool Veilynx_summon()
		{
			if (this.wasStallioActivated && this.wasWolfActivatedThisTurn)
			{
				return false;
			}
			if ((this.wasStallioActivated && !this.wasWolfActivatedThisTurn) || (!this.wasStallioActivated && this.wasWolfActivatedThisTurn))
			{
				return false;
			}
			if (base.Bot.HasInHand(26889158) && !this.wasGazelleSummonedThisTurn && !base.Bot.HasInGraveyard(56003780))
			{
				if ((from x in base.Bot.GetMonstersInMainZone()
					where x.Level == 3
					select x).Count<ClientCard>() <= 1)
				{
					goto IL_00C6;
				}
			}
			if (!base.Bot.HasInMonstersZone(87871125, false, false, false) || base.Bot.HasInSpellZoneOrInGraveyard(1295111) || this.wasWolfSummonedUsingItself)
			{
				if (!base.Bot.HasInMonstersZone(14812471, false, false, false) && base.Bot.GetMonstersInMainZone().Count >= 3)
				{
					if ((from x in base.Bot.GetMonstersInExtraZone()
						where x.Owner == 0
						select x).Count<ClientCard>() == 0)
					{
						List<ClientCard> monsters = base.Bot.GetMonstersInMainZone();
						monsters.Sort(new Comparison<ClientCard>(CardContainer.CompareCardLevel));
						monsters.Reverse();
						base.AI.SelectMaterials(monsters, 0);
						return true;
					}
				}
				if ((from x in this.CombosInHand
					where x != 94620082
					where x != 52277807
					select x).Count<int>() == 0 && base.Bot.HasInHand(52277807))
				{
					if (base.Bot.HasInMonstersZone(26889158, false, false, false) && base.Bot.HasInMonstersZone(87871125, false, false, false))
					{
						base.AI.SelectMaterials(26889158, 0);
						return true;
					}
					if (!this.wasVeilynxSummonedThisTurn)
					{
						this.wasVeilynxSummonedThisTurn = true;
						return true;
					}
				}
				return false;
			}
			IL_00C6:
			List<ClientCard> monsters2 = base.Bot.GetMonstersInMainZone();
			if (base.Bot.HasInMonstersZone(14812471, false, false, false) && monsters2.Count == 2)
			{
				return false;
			}
			monsters2.Sort(new Comparison<ClientCard>(CardContainer.CompareCardLevel));
			monsters2.Reverse();
			base.AI.SelectMaterials(monsters2, 0);
			return true;
		}

		// Token: 0x06001DC7 RID: 7623 RVA: 0x000B41E8 File Offset: 0x000B23E8
		private bool JackJaguar_activate()
		{
			if (base.Card.Location == CardLocation.Grave)
			{
				if (!base.Bot.HasInGraveyard(this.JackJaguarTargets))
				{
					if (base.Bot.Graveyard.Where((ClientCard x) => x.Id == 14812471).Count<ClientCard>() < 2 && (base.Bot.HasInGraveyard(this.salamangreat_spellTrap) || !base.Bot.HasInMonstersZone(87871125, false, false, false) || !base.Bot.HasInGraveyard(26889158) || base.Bot.HasInHand(26889158)))
					{
						return false;
					}
				}
				this.JackJaguarActivatedThisTurn = true;
				if (base.Bot.Graveyard.Where((ClientCard x) => x.Id == 14812471).Count<ClientCard>() >= 2)
				{
					if (base.Bot.Graveyard.Select((ClientCard x) => x.Id).Intersect(this.JackJaguarTargets).Count<int>() == 0)
					{
						base.AI.SelectCard(14812471);
						return true;
					}
				}
				base.AI.SelectCard(this.JackJaguarTargets);
				return true;
			}
			return false;
		}

		// Token: 0x06001DC8 RID: 7624 RVA: 0x000B4351 File Offset: 0x000B2551
		private bool Fowl_activate()
		{
			return base.Card.Location == CardLocation.Hand && base.Bot.HasInMonstersZone(56003780, false, false, false) && this.JackJaguarActivatedThisTurn;
		}

		// Token: 0x06001DC9 RID: 7625 RVA: 0x000B4380 File Offset: 0x000B2580
		private bool Spinny_activate()
		{
			if (base.Card.Location == CardLocation.Hand)
			{
				if (base.Bot.HasInGraveyard(94620082) && !this.FoxyActivatedThisTurn)
				{
					return false;
				}
				if ((from x in this.CombosInHand
					where x != 94620082
					where x != 52277807
					select x).Count<int>() == 0)
				{
					return false;
				}
				if (!base.Bot.HasInMonstersZoneOrInGraveyard(52277807) && base.Util.GetBestBotMonster(true) != null && (base.Bot.GetMonsters().Count != 1 || !base.Bot.HasInMonstersZone(52277807, false, false, false)))
				{
					base.AI.SelectCard(base.Util.GetBestBotMonster(true));
					return true;
				}
			}
			return true;
		}

		// Token: 0x06001DCA RID: 7626 RVA: 0x000B4473 File Offset: 0x000B2673
		private bool Falco_activate()
		{
			if (!this.falcoUsedReturnST && this.falcoHitGY && base.Bot.HasInGraveyard(this.salamangreat_spellTrap))
			{
				this.falcoUsedReturnST = true;
				base.AI.SelectCard(this.salamangreat_spellTrap);
				return true;
			}
			return false;
		}

		// Token: 0x06001DCB RID: 7627 RVA: 0x000B44B4 File Offset: 0x000B26B4
		private bool Gazelle_activate()
		{
			this.wasGazelleSummonedThisTurn = true;
			if (!base.Bot.HasInHandOrInMonstersZoneOrInGraveyard(52277807))
			{
				base.AI.SelectCard(52277807);
				return true;
			}
			if (!base.Bot.HasInSpellZoneOrInGraveyard(51339637))
			{
				base.AI.SelectCard(51339637);
				return true;
			}
			if (!base.Bot.HasInSpellZoneOrInGraveyard(14934922))
			{
				base.AI.SelectCard(14934922);
				return true;
			}
			if (!base.Bot.HasInHandOrInMonstersZoneOrInGraveyard(56003780))
			{
				base.AI.SelectCard(56003780);
				return true;
			}
			if (!base.Bot.HasInHandOrInMonstersZoneOrInGraveyard(94620082))
			{
				base.AI.SelectCard(94620082);
				return true;
			}
			if (!base.Bot.HasInHandOrInMonstersZoneOrInGraveyard(20618081))
			{
				base.AI.SelectCard(20618081);
				return true;
			}
			return true;
		}

		// Token: 0x06001DCC RID: 7628 RVA: 0x000B45A4 File Offset: 0x000B27A4
		private bool Foxy_activate()
		{
			if (base.Card.Location == CardLocation.MonsterZone)
			{
				if ((from x in this.CombosInHand
					where x != 94620082
					where x != 52277807
					select x).Count<int>() == 0 && base.Bot.HasInHand(52277807))
				{
					return false;
				}
				base.AI.SelectCard(this.salamangreat_combopieces);
				this.FoxyActivatedThisTurn = true;
				return true;
			}
			else
			{
				if (base.DefaultCheckWhetherCardIsNegated(base.Card))
				{
					return false;
				}
				if (base.Bot.HasInHand(52277807) || this.FalcoToGY(false))
				{
					if (base.Bot.HasInHand(52277807) && !base.Bot.HasInGraveyard(52277807))
					{
						base.AI.SelectCard(52277807);
					}
					else
					{
						if (!this.FalcoToGY(false))
						{
							return false;
						}
						base.AI.SelectCard(20618081);
					}
					if (base.Util.GetBestEnemySpell(true) != null)
					{
						base.AI.SelectNextCard(base.Util.GetBestEnemySpell(true));
						this.foxyPopEnemySpell = true;
					}
					this.FoxyActivatedThisTurn = true;
					return true;
				}
				return false;
			}
		}

		// Token: 0x06001DCD RID: 7629 RVA: 0x000B46FC File Offset: 0x000B28FC
		private bool FalcoToGY(bool FromDeck)
		{
			if (FromDeck && base.Bot.Deck.ContainsCardWithId(20618081))
			{
				return base.Bot.HasInGraveyard(this.salamangreat_spellTrap);
			}
			return base.Bot.HasInHand(20618081) && base.Bot.HasInGraveyard(this.salamangreat_spellTrap);
		}

		// Token: 0x06001DCE RID: 7630 RVA: 0x000B4764 File Offset: 0x000B2964
		private bool Fadydebug_activate()
		{
			if (!base.Bot.HasInHand(26889158))
			{
				base.AI.SelectCard(26889158);
				return true;
			}
			if (!base.Bot.HasInHandOrInGraveyard(52277807))
			{
				base.AI.SelectCard(52277807);
				return true;
			}
			if (!base.Bot.HasInHand(94620082))
			{
				base.AI.SelectCard(94620082);
				return true;
			}
			return true;
		}

		// Token: 0x06001DCF RID: 7631 RVA: 0x000B47E0 File Offset: 0x000B29E0
		private bool Circle_activate()
		{
			int activateDescription = base.ActivateDescription;
			if (base.ActivateDescription != base.Util.GetStringId(52155219, 0) && base.ActivateDescription != 0)
			{
				return false;
			}
			base.AI.SelectOption(0);
			if (!base.Bot.HasInHand(26889158))
			{
				base.AI.SelectCard(26889158);
				return true;
			}
			if (!base.Bot.HasInHandOrInGraveyard(52277807))
			{
				base.AI.SelectCard(52277807);
				return true;
			}
			if (!base.Bot.HasInHand(94620082))
			{
				base.AI.SelectCard(94620082);
				return true;
			}
			if (!base.Bot.HasInHand(89662401))
			{
				base.AI.SelectCard(89662401);
				return true;
			}
			if (!base.Bot.HasInHand(56003780))
			{
				base.AI.SelectCard(56003780);
				return true;
			}
			if (!base.Bot.HasInHand(20618081))
			{
				base.AI.SelectCard(20618081);
				return true;
			}
			return false;
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x000B4900 File Offset: 0x000B2B00
		private bool FoolishBurial_activate()
		{
			if (this.FalcoToGY(true) && base.Bot.HasInHandOrInGraveyard(52277807))
			{
				base.AI.SelectCard(20618081);
				return true;
			}
			base.AI.SelectCard(new int[] { 52277807, 56003780, 94620082 });
			return true;
		}

		// Token: 0x06001DD1 RID: 7633 RVA: 0x000B4957 File Offset: 0x000B2B57
		private bool Sanctuary_activate()
		{
			return base.Card.Location == CardLocation.Hand;
		}

		// Token: 0x06001DD2 RID: 7634 RVA: 0x000B496C File Offset: 0x000B2B6C
		private bool Rage_activate()
		{
			if (base.ActivateDescription == base.Util.GetStringId(14934922, 1))
			{
				base.AI.SelectCard(this.salamangreat_links);
				base.AI.SelectOption(1);
				IList<ClientCard> targets = new List<ClientCard>();
				ClientCard target = base.Util.GetBestEnemyMonster(false, true);
				if (target != null)
				{
					targets.Add(target);
				}
				ClientCard target2 = base.Util.GetBestEnemySpell(false);
				if (target2 != null)
				{
					targets.Add(target2);
				}
				foreach (ClientCard target3 in base.Enemy.GetMonsters())
				{
					if (targets.Count >= 2)
					{
						break;
					}
					if (!targets.Contains(target3))
					{
						targets.Add(target3);
					}
				}
				foreach (ClientCard target4 in base.Enemy.GetSpells())
				{
					if (targets.Count >= 2)
					{
						break;
					}
					if (!targets.Contains(target4))
					{
						targets.Add(target4);
					}
				}
				if (targets.Count == 0)
				{
					return false;
				}
				base.AI.SelectNextCard(targets);
				return true;
			}
			else
			{
				if (base.Util.GetProblematicEnemyCard(0, true) != null)
				{
					if (base.Util.GetBestBotMonster(true) != null)
					{
						base.AI.SelectCard(base.Util.GetProblematicEnemyCard(base.Util.GetBestBotMonster(true).Attack, true));
					}
					else
					{
						base.AI.SelectCard(base.Util.GetProblematicEnemyCard(0, true));
					}
					return true;
				}
				return false;
			}
		}

		// Token: 0x06001DD3 RID: 7635 RVA: 0x000348E3 File Offset: 0x00032AE3
		public bool G_activate()
		{
			return !base.DefaultCheckWhetherCardIsNegated(base.Card) && base.Duel.Player == 1;
		}

		// Token: 0x06001DD4 RID: 7636 RVA: 0x000B4B24 File Offset: 0x000B2D24
		public bool Hand_act_eff()
		{
			return !base.DefaultCheckWhetherCardIsNegated(base.Card) && base.Duel.LastChainPlayer == 1;
		}

		// Token: 0x06001DD5 RID: 7637 RVA: 0x000B4B44 File Offset: 0x000B2D44
		public bool Impermanence_activate()
		{
			if (!this.Should_counter())
			{
				return false;
			}
			if (!this.spell_trap_activate(false, null))
			{
				return false;
			}
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

		// Token: 0x06001DD6 RID: 7638 RVA: 0x000B4F30 File Offset: 0x000B3130
		public bool is_should_not_negate()
		{
			ClientCard last_card = base.Util.GetLastChainCard();
			return last_card != null && last_card.Controller == 1 && last_card.IsCode(this.should_not_negate);
		}

		// Token: 0x06001DD7 RID: 7639 RVA: 0x000B4F66 File Offset: 0x000B3166
		public bool SolemnStrike_activate()
		{
			return this.Should_counter() && base.DefaultSolemnStrike() && this.spell_trap_activate(true, null);
		}

		// Token: 0x06001DD8 RID: 7640 RVA: 0x000B4F84 File Offset: 0x000B3184
		public bool SolemnJudgment_activate()
		{
			return !base.Util.IsChainTargetOnly(base.Card) && (base.Duel.Player != 0 || base.Duel.LastChainPlayer != -1) && base.DefaultTrap() && this.spell_trap_activate(true, null);
		}

		// Token: 0x06001DD9 RID: 7641 RVA: 0x000B4FD4 File Offset: 0x000B31D4
		public bool spell_trap_activate(bool isCounter = false, ClientCard target = null)
		{
			if (target == null)
			{
				target = base.Card;
			}
			if (target.Location != CardLocation.SpellZone && target.Location != CardLocation.Hand)
			{
				return true;
			}
			if (target.IsSpell())
			{
				return (!base.Enemy.HasInMonstersZone(33198837, true, false, false) || base.Bot.HasInHandOrHasInMonstersZone(59438930) || isCounter || base.Bot.HasInSpellZone(40605147, false, false)) && !base.Enemy.HasInSpellZone(61740673, true, false) && !base.Bot.HasInSpellZone(61740673, true, false) && !base.Enemy.HasInMonstersZone(37267041, true, false, false) && !base.Bot.HasInMonstersZone(37267041, true, false, false);
			}
			return target.IsTrap() && !base.Enemy.HasInSpellZone(51452091, true, false) && !base.Bot.HasInSpellZone(51452091, true, false);
		}

		// Token: 0x06001DDA RID: 7642 RVA: 0x000B50D8 File Offset: 0x000B32D8
		public bool Should_counter()
		{
			if (base.Duel.CurrentChain.Count < 2)
			{
				return true;
			}
			ClientCard self_card = base.Duel.CurrentChain[base.Duel.CurrentChain.Count - 2];
			if (self_card == null || self_card.Controller != 0 || (self_card.Location != CardLocation.MonsterZone && self_card.Location != CardLocation.SpellZone))
			{
				return true;
			}
			ClientCard enemy_card = base.Duel.CurrentChain[base.Duel.CurrentChain.Count - 1];
			return enemy_card == null || enemy_card.Controller != 1 || !enemy_card.IsCode(this.normal_counter);
		}

		// Token: 0x06001DDB RID: 7643 RVA: 0x000B518C File Offset: 0x000B338C
		public int SelectSTPlace(ClientCard card = null, bool avoid_Impermanence = false)
		{
			List<int> list = new List<int>();
			list.Add(0);
			list.Add(1);
			list.Add(2);
			list.Add(3);
			list.Add(4);
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

		// Token: 0x06001DDC RID: 7644 RVA: 0x000B528C File Offset: 0x000B348C
		public override bool OnSelectYesNo(int desc)
		{
			if (desc == base.Util.GetStringId(1295111, 0))
			{
				this.wasFieldspellUsedThisTurn = true;
			}
			if (desc == base.Util.GetStringId(94620082, 3))
			{
				return this.foxyPopEnemySpell;
			}
			return base.OnSelectYesNo(desc);
		}

		// Token: 0x06001DDD RID: 7645 RVA: 0x000B52CC File Offset: 0x000B34CC
		public override void OnNewTurn()
		{
			this.FoxyActivatedThisTurn = false;
			this.JackJaguarActivatedThisTurn = false;
			this.wasWolfActivatedThisTurn = false;
			this.wasStallioActivated = false;
			this.falcoUsedReturnST = false;
			this.CombosInHand = base.Bot.Hand.Select((ClientCard h) => h.Id).Intersect(this.Combo_cards).ToList<int>();
			this.wasFieldspellUsedThisTurn = false;
			this.wasGazelleSummonedThisTurn = false;
			base.OnNewTurn();
		}

		// Token: 0x06001DDE RID: 7646 RVA: 0x0000763C File Offset: 0x0000583C
		public override bool OnSelectHand()
		{
			return true;
		}

		// Token: 0x06001DDF RID: 7647 RVA: 0x000B5358 File Offset: 0x000B3558
		public bool SpellSet()
		{
			if (base.Card.Id == 52155219)
			{
				return false;
			}
			if (base.Duel.Phase == DuelPhase.Main1 && base.Bot.HasAttackingMonster() && base.Duel.Turn > 1)
			{
				return false;
			}
			if (base.Card.IsCode(40605147) && base.Bot.LifePoints <= 1500)
			{
				return false;
			}
			if (!base.Card.IsTrap() && !base.Card.HasType(CardType.QuickPlay))
			{
				return false;
			}
			List<int> avoid_list = new List<int>();
			int Impermanence_set = 0;
			for (int i = 0; i < 5; i++)
			{
				if (base.Enemy.SpellZone[i] != null && base.Enemy.SpellZone[i].IsFaceup() && base.Bot.SpellZone[4 - i] == null)
				{
					avoid_list.Add(4 - i);
					Impermanence_set += (int)Math.Pow(2.0, (double)(4 - i));
				}
			}
			if (!base.Bot.HasInHand(10045474))
			{
				base.AI.SelectPlace(this.SelectSTPlace(null, false));
				return true;
			}
			if (base.Card.IsCode(10045474))
			{
				base.AI.SelectPlace(Impermanence_set);
				return true;
			}
			base.AI.SelectPlace(this.SelectSetPlace(avoid_list, true));
			return true;
		}

		// Token: 0x06001DE0 RID: 7648 RVA: 0x000B54B0 File Offset: 0x000B36B0
		public bool Called_activate()
		{
			if (!base.DefaultUniqueTrap())
			{
				return false;
			}
			if (base.Duel.Player == 1)
			{
				ClientCard target = base.Enemy.MonsterZone.GetShouldBeDisabledBeforeItUseEffectMonster(true);
				if (target != null && base.Enemy.HasInGraveyard(target.Id))
				{
					base.AI.SelectCard(target.Id);
					return true;
				}
			}
			ClientCard LastChainCard = base.Util.GetLastChainCard();
			if (LastChainCard != null && LastChainCard.Controller == 1 && (LastChainCard.Location == CardLocation.Grave || LastChainCard.Location == CardLocation.Hand || LastChainCard.Location == CardLocation.MonsterZone || LastChainCard.Location == CardLocation.Removed) && !LastChainCard.IsDisabled() && !LastChainCard.IsShouldNotBeTarget() && !LastChainCard.IsShouldNotBeSpellTrapTarget() && base.Enemy.HasInGraveyard(LastChainCard.Id))
			{
				base.AI.SelectCard(LastChainCard.Id);
				return true;
			}
			if (base.Bot.BattlingMonster != null && base.Enemy.BattlingMonster != null && !base.Enemy.BattlingMonster.IsDisabled() && base.Enemy.BattlingMonster.IsCode(63845230) && base.Enemy.HasInGraveyard(63845230))
			{
				base.AI.SelectCard(base.Enemy.BattlingMonster.Id);
				return true;
			}
			if (base.Duel.Phase == DuelPhase.BattleStart && base.Duel.Player == 1 && base.Enemy.HasInMonstersZone(56832966, true, false, false) && base.Enemy.HasInGraveyard(56832966))
			{
				base.AI.SelectCard(56832966);
				return true;
			}
			return false;
		}

		// Token: 0x06001DE1 RID: 7649 RVA: 0x000B5654 File Offset: 0x000B3854
		public bool Borrelsword_ss()
		{
			if (base.Duel.Phase != DuelPhase.Main1)
			{
				return false;
			}
			if (base.Duel.Turn == 1)
			{
				return false;
			}
			if (this.wasStallioActivated)
			{
				return false;
			}
			List<ClientCard> material_list = new List<ClientCard>();
			List<ClientCard> monsters = base.Bot.GetMonsters();
			monsters.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			int link_count = 0;
			foreach (ClientCard card in monsters)
			{
				if (!card.IsFacedown() && !material_list.Contains(card) && card.LinkCount < 3)
				{
					material_list.Add(card);
					link_count += (card.HasType(CardType.Link) ? card.LinkCount : 1);
				}
			}
			if (link_count >= 4)
			{
				if (link_count > 4)
				{
					if (material_list.Where((ClientCard x) => x.Id == 87327776).Count<ClientCard>() > 0)
					{
						material_list.Remove(material_list.First((ClientCard x) => x.Id == 87327776));
					}
				}
				base.AI.SelectMaterials(material_list, 0);
				return true;
			}
			return false;
		}

		// Token: 0x06001DE2 RID: 7650 RVA: 0x000B5794 File Offset: 0x000B3994
		public bool Borrelsword_eff()
		{
			if (base.ActivateDescription == -1)
			{
				return true;
			}
			if ((base.Duel.Phase > DuelPhase.Main1 && base.Duel.Phase < DuelPhase.Main2) || base.Util.IsChainTarget(base.Card))
			{
				List<ClientCard> monsters = base.Enemy.GetMonsters();
				monsters.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				monsters.Reverse();
				foreach (ClientCard card in monsters)
				{
					if (card.HasPosition(CardPosition.Attack) && !card.HasType(CardType.Link))
					{
						base.AI.SelectCard(card);
						return true;
					}
				}
				List<ClientCard> monsters2 = base.Bot.GetMonsters();
				monsters2.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				foreach (ClientCard card2 in monsters2)
				{
					if (card2.HasPosition(CardPosition.Attack) && !card2.HasType(CardType.Link))
					{
						base.AI.SelectCard(card2);
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06001DE3 RID: 7651 RVA: 0x000B58E4 File Offset: 0x000B3AE4
		public override void OnChainEnd()
		{
			if (!this.falcoHitGY && !this.falcoUsedReturnST && base.Bot.HasInGraveyard(20618081))
			{
				this.falcoHitGY = true;
			}
			else if (!base.Bot.HasInGraveyard(20618081))
			{
				this.falcoHitGY = false;
			}
			base.OnChainEnd();
		}

		// Token: 0x06001DE4 RID: 7652 RVA: 0x000B593C File Offset: 0x000B3B3C
		public override int OnSelectPlace(int cardId, int player, CardLocation location, int available)
		{
			if (player == 0 && location == CardLocation.MonsterZone)
			{
				if ((from x in base.Bot.GetMonstersInExtraZone()
					where x.Id == 87871125
					select x).Count<ClientCard>() > 1)
				{
					for (int i = 0; i < 7; i++)
					{
						if (base.Bot.MonsterZone[i] != null && base.Bot.MonsterZone[i].IsCode(87871125))
						{
							int next_index = this.get_Wolf_linkzone();
							if (next_index != -1 && (available & (int)Math.Pow(2.0, (double)next_index)) > 0)
							{
								return (int)Math.Pow(2.0, (double)next_index);
							}
						}
					}
				}
			}
			return base.OnSelectPlace(cardId, player, location, available);
		}

		// Token: 0x06001DE5 RID: 7653 RVA: 0x000B5A00 File Offset: 0x000B3C00
		public override CardPosition OnSelectPosition(int cardId, IList<CardPosition> positions)
		{
			if (base.Util.IsTurn1OrMain2() && (cardId == 26889158 || cardId == 52277807 || cardId == 94620082))
			{
				return CardPosition.FaceUpDefence;
			}
			return (CardPosition)0;
		}

		// Token: 0x06001DE6 RID: 7654 RVA: 0x000B5A2C File Offset: 0x000B3C2C
		public int SelectSetPlace(List<int> avoid_list = null, bool avoid = true)
		{
			List<int> list = new List<int>();
			list.Add(5);
			list.Add(6);
			int i = list.Count;
			while (i-- > 1)
			{
				int index = Program.Rand.Next(i + 1);
				int temp = list[index];
				list[index] = list[i];
				list[i] = temp;
			}
			using (List<int>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int seq = enumerator.Current;
					int zone = (int)Math.Pow(2.0, (double)seq);
					if (base.Bot.MonsterZone[seq] == null || !avoid)
					{
						if (avoid)
						{
							if (avoid_list == null || !avoid_list.Contains(seq))
							{
								return zone;
							}
						}
						else if (avoid_list != null && avoid_list.Contains(seq))
						{
							return list.First((int x) => x == seq);
						}
					}
				}
			}
			return 0;
		}

		// Token: 0x06001DE7 RID: 7655 RVA: 0x000B5B54 File Offset: 0x000B3D54
		public override BattlePhaseAction OnSelectAttackTarget(ClientCard attacker, IList<ClientCard> defenders)
		{
			foreach (ClientCard defender in defenders)
			{
				attacker.RealPower = attacker.Attack;
				defender.RealPower = defender.GetDefensePower();
				if (attacker.IsCode(85289965) && !attacker.IsDisabled())
				{
					return base.AI.Attack(attacker, defender);
				}
				if (this.OnPreBattleBetween(attacker, defender) && (attacker.RealPower > defender.RealPower || (attacker.RealPower > defender.RealPower && attacker.IsLastAttacker && defender.IsAttack())))
				{
					return base.AI.Attack(attacker, defender);
				}
			}
			if (attacker.CanDirectAttack)
			{
				return base.AI.Attack(attacker, null);
			}
			return null;
		}

		// Token: 0x040020A0 RID: 8352
		private bool foxyPopEnemySpell;

		// Token: 0x040020A1 RID: 8353
		private bool wasGazelleSummonedThisTurn;

		// Token: 0x040020A2 RID: 8354
		private bool wasFieldspellUsedThisTurn;

		// Token: 0x040020A3 RID: 8355
		private bool wasWolfSummonedUsingItself;

		// Token: 0x040020A4 RID: 8356
		private int sunlightPosition;

		// Token: 0x040020A5 RID: 8357
		private bool wasVeilynxSummonedThisTurn;

		// Token: 0x040020A6 RID: 8358
		private bool falcoHitGY;

		// Token: 0x040020A7 RID: 8359
		private List<int> CombosInHand;

		// Token: 0x040020A8 RID: 8360
		private List<int> Impermanence_list = new List<int>();

		// Token: 0x040020A9 RID: 8361
		private List<int> Combo_cards = new List<int> { 52277807, 56003780, 89662401, 94620082, 20618081, 52155219, 26889158, 81439174 };

		// Token: 0x040020AA RID: 8362
		private List<int> normal_counter = new List<int>
		{
			53262004, 98338152, 32617464, 45041488, 40605147, 61257789, 23440231, 27354732, 12408276, 82419869,
			10045474, 49680980, 18621798, 38814750, 17266660, 94689635, 14558127, 74762582, 75286651, 4810828,
			44665365, 21123811, 50954680, 82044279, 82044280, 79606837, 10443957, 1621413, 90809975, 8165596,
			9753964, 53347303, 88307361, 55063751, 5818294, 2948263, 6150044, 26268488, 51447164, 63941210,
			97268402
		};

		// Token: 0x040020AB RID: 8363
		private List<int> should_not_negate = new List<int> { 81275020, 28985331 };

		// Token: 0x040020AC RID: 8364
		private List<int> salamangreat_links = new List<int> { 41463181, 87871125, 14812471 };

		// Token: 0x040020AD RID: 8365
		private List<int> JackJaguarTargets = new List<int> { 87871125, 87327776, 41463181 };

		// Token: 0x040020AE RID: 8366
		private List<int> salamangreat_combopieces = new List<int> { 26889158, 52277807, 56003780, 94620082, 52155219, 20618081 };

		// Token: 0x040020AF RID: 8367
		private List<int> WolfMaterials = new List<int> { 14812471, 56003780, 20618081, 94620082, 87327776, 26889158 };

		// Token: 0x040020B0 RID: 8368
		private List<int> salamangreat_spellTrap = new List<int> { 51339637, 14934922, 52155219, 1295111 };

		// Token: 0x040020B1 RID: 8369
		private bool falcoUsedReturnST;

		// Token: 0x040020B2 RID: 8370
		private bool wasStallioActivated;

		// Token: 0x040020B3 RID: 8371
		private bool wasWolfActivatedThisTurn;

		// Token: 0x040020B4 RID: 8372
		private bool JackJaguarActivatedThisTurn;

		// Token: 0x040020B5 RID: 8373
		private bool FoxyActivatedThisTurn;

		// Token: 0x020003D2 RID: 978
		public class CardId
		{
			// Token: 0x040020B6 RID: 8374
			public const int JackJaguar = 56003780;

			// Token: 0x040020B7 RID: 8375
			public const int EffectVeiler = 97268402;

			// Token: 0x040020B8 RID: 8376
			public const int LadyDebug = 16188701;

			// Token: 0x040020B9 RID: 8377
			public const int Foxy = 94620082;

			// Token: 0x040020BA RID: 8378
			public const int Gazelle = 26889158;

			// Token: 0x040020BB RID: 8379
			public const int Fowl = 89662401;

			// Token: 0x040020BC RID: 8380
			public const int Falco = 20618081;

			// Token: 0x040020BD RID: 8381
			public const int Spinny = 52277807;

			// Token: 0x040020BE RID: 8382
			public const int MaxxC = 23434538;

			// Token: 0x040020BF RID: 8383
			public const int AshBlossom = 14558127;

			// Token: 0x040020C0 RID: 8384
			public const int FusionOfFire = 25800447;

			// Token: 0x040020C1 RID: 8385
			public const int Circle = 52155219;

			// Token: 0x040020C2 RID: 8386
			public const int HarpieFeatherDuster = 18144507;

			// Token: 0x040020C3 RID: 8387
			public const int FoolishBurial = 81439174;

			// Token: 0x040020C4 RID: 8388
			public const int Sanctuary = 1295111;

			// Token: 0x040020C5 RID: 8389
			public const int CalledByTheGrave = 24224830;

			// Token: 0x040020C6 RID: 8390
			public const int SalamangreatRage = 14934922;

			// Token: 0x040020C7 RID: 8391
			public const int SalamangreatRoar = 51339637;

			// Token: 0x040020C8 RID: 8392
			public const int Impermanence = 10045474;

			// Token: 0x040020C9 RID: 8393
			public const int SolemnJudgment = 41420027;

			// Token: 0x040020CA RID: 8394
			public const int SolemnStrike = 40605147;

			// Token: 0x040020CB RID: 8395
			public const int SalamangreatVioletChimera = 37261776;

			// Token: 0x040020CC RID: 8396
			public const int ExcitionKnight = 46772449;

			// Token: 0x040020CD RID: 8397
			public const int MirageStallio = 87327776;

			// Token: 0x040020CE RID: 8398
			public const int SunlightWolf = 87871125;

			// Token: 0x040020CF RID: 8399
			public const int Borrelload = 31833038;

			// Token: 0x040020D0 RID: 8400
			public const int HeatLeo = 41463181;

			// Token: 0x040020D1 RID: 8401
			public const int Veilynx = 14812471;

			// Token: 0x040020D2 RID: 8402
			public const int Charmer = 48815792;

			// Token: 0x040020D3 RID: 8403
			public const int KnightmarePheonix = 2857636;

			// Token: 0x040020D4 RID: 8404
			public const int Borrelsword = 85289965;

			// Token: 0x040020D5 RID: 8405
			public const int GO_SR = 59438930;

			// Token: 0x040020D6 RID: 8406
			public const int DarkHole = 53129443;

			// Token: 0x040020D7 RID: 8407
			public const int NaturalBeast = 33198837;

			// Token: 0x040020D8 RID: 8408
			public const int SwordsmanLV7 = 37267041;

			// Token: 0x040020D9 RID: 8409
			public const int RoyalDecreel = 51452091;

			// Token: 0x040020DA RID: 8410
			public const int Anti_Spell = 58921041;

			// Token: 0x040020DB RID: 8411
			public const int Hayate = 8491308;

			// Token: 0x040020DC RID: 8412
			public const int Raye = 26077387;

			// Token: 0x040020DD RID: 8413
			public const int Drones_Token = 52340445;

			// Token: 0x040020DE RID: 8414
			public const int Iblee = 10158145;

			// Token: 0x040020DF RID: 8415
			public const int ImperialOrder = 61740673;

			// Token: 0x040020E0 RID: 8416
			public const int TornadoDragon = 6983839;
		}
	}
}
