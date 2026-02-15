using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using YGOSharp.OCGWrapper;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x02000421 RID: 1057
	[Deck("Witchcraft", "AI_Witchcraft", "Normal")]
	internal class WitchcraftExecutor : DefaultExecutor
	{
		// Token: 0x06002205 RID: 8709 RVA: 0x000DC044 File Offset: 0x000DA244
		public WitchcraftExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.Activate, 49238328, new Func<bool>(this.PotofExtravaganceActivate));
			base.AddExecutor(ExecutorType.SpellSet, new Func<bool>(this.SpellSetForFiveRainbow));
			base.AddExecutor(ExecutorType.Activate, 54693926, new Func<bool>(this.DarkRulerNoMoreActivate));
			base.AddExecutor(ExecutorType.Activate, 14532163, new Func<bool>(this.LightningStormActivate));
			base.AddExecutor(ExecutorType.Activate, 94259633);
			base.AddExecutor(ExecutorType.Activate, 21744288, new Func<bool>(this.DeckSSWitchcraft));
			base.AddExecutor(ExecutorType.Activate, 95245544, new Func<bool>(this.DeckSSWitchcraft));
			base.AddExecutor(ExecutorType.Activate, 59851535, new Func<bool>(this.DeckSSWitchcraft));
			base.AddExecutor(ExecutorType.Activate, 64756282, new Func<bool>(this.DeckSSWitchcraft));
			base.AddExecutor(ExecutorType.Activate, 38814750, new Func<bool>(this.PSYGammaActivate));
			base.AddExecutor(ExecutorType.Activate, 23434538, new Func<bool>(this.MaxxCActivate));
			base.AddExecutor(ExecutorType.Activate, 71074418, new Func<bool>(this.GolemAruruActivate));
			base.AddExecutor(ExecutorType.Activate, 27548199, new Func<bool>(this.BorreloadSavageDragonActivate));
			base.AddExecutor(ExecutorType.Activate, 10045474, new Func<bool>(this.InfiniteImpermanenceActivate));
			base.AddExecutor(ExecutorType.Activate, 14558127, new Func<bool>(this.AshBlossom_JoyousSpringActivate));
			base.AddExecutor(ExecutorType.Activate, 24224830, new Func<bool>(this.CalledbytheGraveActivate));
			base.AddExecutor(ExecutorType.Activate, 65681983, new Func<bool>(this.CrossoutDesignatorActivate));
			base.AddExecutor(ExecutorType.Activate, 87769556, new Func<bool>(this.SpellsActivate));
			base.AddExecutor(ExecutorType.Activate, 13758665, new Func<bool>(this.SpellsActivate));
			base.AddExecutor(ExecutorType.Activate, 70226289, new Func<bool>(this.UnveilingActivate));
			base.AddExecutor(ExecutorType.Activate, 56894757, new Func<bool>(this.DrapingActivate));
			base.AddExecutor(ExecutorType.Activate, 74586817, new Func<bool>(this.PSYOmegaActivate));
			base.AddExecutor(ExecutorType.Activate, 5041348, new Func<bool>(this.DracoBerserkeroftheTenyiActivate));
			base.AddExecutor(ExecutorType.Activate, 21522601, new Func<bool>(this.MadameVerreActivate));
			base.AddExecutor(ExecutorType.Activate, 84523092, new Func<bool>(this.HaineActivate));
			base.AddExecutor(ExecutorType.Activate, 60303245, new Func<bool>(this.SalamangreatAlmirajActivate));
			base.AddExecutor(ExecutorType.Activate, 8802510);
			base.AddExecutor(ExecutorType.SpSummon, 8802510, new Func<bool>(this.PSYLambdaSummon));
			base.AddExecutor(ExecutorType.SpSummon, 74586817, new Func<bool>(this.Lv8Summon));
			base.AddExecutor(ExecutorType.SpSummon, 27548199, new Func<bool>(this.BorreloadSavageDragonSummon));
			base.AddExecutor(ExecutorType.SpSummon, 5041348, new Func<bool>(this.Lv8Summon));
			base.AddExecutor(ExecutorType.SpSummon, 27548199, new Func<bool>(this.Lv8Summon));
			base.AddExecutor(ExecutorType.Activate, 83289866, new Func<bool>(this.WitchcraftRecycle));
			base.AddExecutor(ExecutorType.Activate, new Func<bool>(this.WitchcraftRecycle));
			base.AddExecutor(ExecutorType.Activate, 73594093);
			base.AddExecutor(ExecutorType.Activate, 98558751, new Func<bool>(this.TGWonderMagicianActivate));
			base.AddExecutor(ExecutorType.Activate, 38342335, new Func<bool>(this.KnightmareUnicornActivate));
			base.AddExecutor(ExecutorType.Activate, 2857636, new Func<bool>(this.KnightmarePhoenixActivate));
			base.AddExecutor(ExecutorType.Activate, 50588353, new Func<bool>(this.CrystronHalqifibraxActivate));
			base.AddExecutor(ExecutorType.Activate, 11110587, new Func<bool>(this.SpellsActivatewithCounter));
			base.AddExecutor(ExecutorType.Activate, 58577036, new Func<bool>(this.SpellsActivatewithCounter));
			base.AddExecutor(ExecutorType.Activate, 55072170, new Func<bool>(this.MasterpieceActivate));
			base.AddExecutor(ExecutorType.Activate, 94553671, new Func<bool>(this.PatronusActivate));
			base.AddExecutor(ExecutorType.Activate, 40252269, new Func<bool>(this.MagiciansRestageActivate));
			base.AddExecutor(ExecutorType.Activate, 83301414, new Func<bool>(this.HolidayActivate));
			base.AddExecutor(ExecutorType.Summon, 21744288, new Func<bool>(this.WitchcraftSummon));
			base.AddExecutor(ExecutorType.Summon, 95245544, new Func<bool>(this.WitchcraftSummon));
			base.AddExecutor(ExecutorType.Summon, 59851535, new Func<bool>(this.WitchcraftSummon));
			base.AddExecutor(ExecutorType.Summon, 64756282, new Func<bool>(this.WitchcraftSummon));
			base.AddExecutor(ExecutorType.Activate, 57916305, new Func<bool>(this.CreationActivate));
			base.AddExecutor(ExecutorType.Activate, 95245544, new Func<bool>(this.PittoreActivate));
			base.AddExecutor(ExecutorType.Activate, 21744288, new Func<bool>(this.SchmiettaActivate));
			base.AddExecutor(ExecutorType.Activate, 64756282, new Func<bool>(this.GenniActivate));
			base.AddExecutor(ExecutorType.Activate, 59851535, new Func<bool>(this.PotterieActivate));
			base.AddExecutor(ExecutorType.SpSummon, 2857636, new Func<bool>(this.KnightmarePhoenixSummon));
			base.AddExecutor(ExecutorType.SpSummon, 94259633, new Func<bool>(this.RelinquishedAnimaSummon));
			base.AddExecutor(ExecutorType.SpSummon, 50588353, new Func<bool>(this.CrystronHalqifibraxSummon));
			base.AddExecutor(ExecutorType.SpSummon, 85289965, new Func<bool>(this.BorrelswordDragonSummon));
			base.AddExecutor(ExecutorType.SpSummon, 38342335, new Func<bool>(this.KnightmareUnicornSummon));
			base.AddExecutor(ExecutorType.SpSummon, 60303245, new Func<bool>(this.SalamangreatAlmirajSummon));
			base.AddExecutor(ExecutorType.Summon, new Func<bool>(this.SummonForLink));
			base.AddExecutor(ExecutorType.Activate, 11110587, new Func<bool>(this.SpellsActivateNoCost));
			base.AddExecutor(ExecutorType.Activate, 58577036, new Func<bool>(this.SpellsActivateNoCost));
			base.AddExecutor(ExecutorType.Activate, 87769556, new Func<bool>(this.SpellsActivateNoCost));
			base.AddExecutor(ExecutorType.Activate, 13758665, new Func<bool>(this.SpellsActivateNoCost));
			base.AddExecutor(ExecutorType.Summon, new Func<bool>(this.WitchcraftSummonForRecycle));
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(this.MonsterRepos));
			base.AddExecutor(ExecutorType.Activate, 83289866, new Func<bool>(this.WitchcrafterBystreetActivate));
			base.AddExecutor(ExecutorType.Activate, 19673561, new Func<bool>(this.ScrollActivate));
			base.AddExecutor(ExecutorType.SpellSet, new Func<bool>(this.SpellSet));
		}

		// Token: 0x06002206 RID: 8710 RVA: 0x0000763C File Offset: 0x0000583C
		public override bool OnSelectHand()
		{
			return true;
		}

		// Token: 0x06002207 RID: 8711 RVA: 0x000DC76C File Offset: 0x000DA96C
		public override void OnChainEnd()
		{
			this.currentNegatingIdList.Clear();
			base.OnChainEnd();
		}

		// Token: 0x06002208 RID: 8712 RVA: 0x000DC780 File Offset: 0x000DA980
		public override void OnChainSolved(int chainIndex)
		{
			ChainInfo currentCard = base.Duel.GetCurrentSolvingChainInfo();
			if (currentCard != null && currentCard.ActivatePlayer == 1)
			{
				if (base.Duel.IsCurrentSolvingChainNegated())
				{
					if (!this.MagicianRightHand_used && currentCard.IsSpell())
					{
						if (base.Bot.MonsterZone.GetFirstMatchingCard((ClientCard c) => c.HasRace(CardRace.SpellCaster) && c.IsFaceup()) != null && base.Bot.HasInSpellZone(87769556, true, false))
						{
							Logger.DebugWriteLine("MagicianRightHand negate: " + currentCard.RelatedCard.Name);
							this.MagicianRightHand_used = true;
						}
					}
					if (!this.MagiciansLeftHand_used && currentCard.IsTrap() && currentCard.ActivatePlayer == 1)
					{
						if (base.Bot.MonsterZone.GetFirstMatchingCard((ClientCard c) => c.HasRace(CardRace.SpellCaster) && c.IsFaceup()) != null && base.Bot.HasInSpellZone(13758665, true, false))
						{
							Logger.DebugWriteLine("MagiciansLeftHand negate: " + currentCard.RelatedCard.Name);
							this.MagiciansLeftHand_used = true;
						}
					}
				}
				if (!base.Duel.IsCurrentSolvingChainNegated())
				{
					if (currentCard.IsCode(23434538))
					{
						this.enemy_activate_MaxxC = true;
					}
					if (currentCard.IsCode(91800273))
					{
						this.enemy_activate_DimensionShifter = true;
					}
					if (currentCard.IsCode(10045474))
					{
						for (int i = 0; i < 5; i++)
						{
							if (base.Enemy.SpellZone[i] == currentCard.RelatedCard)
							{
								this.Impermanence_list.Add(4 - i);
								return;
							}
						}
					}
				}
			}
		}

		// Token: 0x06002209 RID: 8713 RVA: 0x000DC924 File Offset: 0x000DAB24
		public override void OnNewTurn()
		{
			this.MadameVerreGainedATK = false;
			this.summoned = false;
			this.enemy_activate_MaxxC = false;
			this.enemy_activate_DimensionShifter = false;
			this.MagiciansLeftHand_used = false;
			this.MagicianRightHand_used = false;
			this.Impermanence_list.Clear();
			this.FirstCheckSS.Clear();
			this.UseSSEffect.Clear();
			this.ActivatedCards.Clear();
			this.currentNegatingIdList.Clear();
			base.OnNewTurn();
		}

		// Token: 0x0600220A RID: 8714 RVA: 0x000DC998 File Offset: 0x000DAB98
		public override bool OnPreBattleBetween(ClientCard attacker, ClientCard defender)
		{
			if (!defender.IsMonsterHasPreventActivationEffectInBattle() && !this.MadameVerreGainedATK && base.Bot.HasInMonstersZone(21522601, true, false, true) && attacker.HasSetcode(this.Witchcraft_setcode))
			{
				attacker.RealPower += this.CheckPlusAttackforMadameVerre(false, false, false);
			}
			return base.OnPreBattleBetween(attacker, defender);
		}

		// Token: 0x0600220B RID: 8715 RVA: 0x000DC9F8 File Offset: 0x000DABF8
		public override IList<ClientCard> OnSelectCard(IList<ClientCard> cards, int min, int max, int hint, bool cancelable)
		{
			if (hint == 506)
			{
				bool flag = true;
				foreach (ClientCard card in cards)
				{
					if (!card.HasSetcode(this.Witchcraft_setcode) || card.Location != CardLocation.Removed || !card.IsSpell())
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					Logger.DebugWriteLine("** Patronus recycle.");
					IList<ClientCard> selected = new List<ClientCard>();
					for (int i = 1; i <= max; i++)
					{
						selected.Add(cards[cards.Count - i]);
						Logger.DebugWriteLine("** Select " + cards[cards.Count - i].Name);
					}
					return selected;
				}
			}
			if (hint == 509 && this.enemy_activate_MaxxC)
			{
				bool flag2 = true;
				List<int> levels = new List<int>();
				List<int> check_cardid = new List<int> { 84523092, 21522601, 71074418 };
				List<ClientCard> checked_card = new List<ClientCard> { null, null, null };
				foreach (ClientCard card2 in cards)
				{
					if (card2 == null || card2.Location != CardLocation.Deck || card2.Controller != 0 || !card2.HasSetcode(this.Witchcraft_setcode))
					{
						flag2 = false;
						break;
					}
					for (int j = 0; j < 3; j++)
					{
						if (card2.Id == check_cardid[j])
						{
							checked_card[j] = card2;
						}
					}
					if (!levels.Contains(card2.Level))
					{
						levels.Add(card2.Level);
					}
				}
				if (flag2 && levels.Count > 1)
				{
					Logger.DebugWriteLine("SS with MaxxC.");
					IList<ClientCard> result = new List<ClientCard>();
					int extra_attack = this.CheckPlusAttackforMadameVerre(true, true, true);
					int bot_best = base.Util.GetBestAttack(base.Bot);
					if (this.CheckProblematicCards(false, false) != null && !base.Util.IsAllEnemyBetterThanValue(bot_best + extra_attack, true) && !base.Bot.HasInMonstersZone(21522601, false, false, false) && checked_card[1] != null)
					{
						result.Add(checked_card[1]);
						return result;
					}
					for (int k = 0; k < 3; k++)
					{
						if (checked_card[k] != null)
						{
							result.Add(checked_card[k]);
							return result;
						}
					}
				}
			}
			if (hint == 526)
			{
				Logger.DebugWriteLine("** min-max: " + min.ToString() + " / " + max.ToString());
				foreach (ClientCard clientCard in cards)
				{
					Logger.DebugWriteLine(clientCard.Name ?? "???");
				}
				IList<ClientCard> selected2 = new List<ClientCard>();
				for (int l = 1; l <= max; l++)
				{
					selected2.Add(cards[cards.Count - l]);
					Logger.DebugWriteLine("** Select " + cards[cards.Count - l].Name);
				}
				return selected2;
			}
			return base.OnSelectCard(cards, min, max, hint, cancelable);
		}

		// Token: 0x0600220C RID: 8716 RVA: 0x000DCD6C File Offset: 0x000DAF6C
		public override CardPosition OnSelectPosition(int cardId, IList<CardPosition> positions)
		{
			NamedCard Data = NamedCard.Get(cardId);
			if (Data == null)
			{
				return base.OnSelectPosition(cardId, positions);
			}
			if ((!base.Enemy.HasInMonstersZone(55410871, false, false, false) && base.Duel.Player == 1 && (cardId == 21522601 || base.Util.GetOneEnemyBetterThanValue(Data.Attack + 1, false, false) != null)) || cardId == 23434538 || cardId == 14558127)
			{
				return CardPosition.FaceUpDefence;
			}
			if (cardId == 21522601 && base.Util.IsTurn1OrMain2())
			{
				return CardPosition.FaceUpDefence;
			}
			return base.OnSelectPosition(cardId, positions);
		}

		// Token: 0x0600220D RID: 8717 RVA: 0x000DCE00 File Offset: 0x000DB000
		public List<ClientCard> CardListShuffle(List<ClientCard> list)
		{
			int i = list.Count;
			while (i-- > 1)
			{
				int index = Program.Rand.Next(i + 1);
				ClientCard temp = list[index];
				list[index] = list[i];
				list[i] = temp;
			}
			return list;
		}

		// Token: 0x0600220E RID: 8718 RVA: 0x000DCE4D File Offset: 0x000DB04D
		public int CheckCalledbytheGrave(int id)
		{
			if (this.currentNegatingIdList.Contains(id))
			{
				return 1;
			}
			if (base.DefaultCheckWhetherCardIdIsNegated(id))
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x0600220F RID: 8719 RVA: 0x000DCE6C File Offset: 0x000DB06C
		public List<ClientCard> CheckDangerousCardinEnemyGrave(bool onlyMonster = false)
		{
			return base.Enemy.Graveyard.GetMatchingCards((ClientCard card) => (!onlyMonster || card.IsMonster()) && card.HasSetcode(283)).ToList<ClientCard>();
		}

		// Token: 0x06002210 RID: 8720 RVA: 0x000DCEA8 File Offset: 0x000DB0A8
		public int CheckDiscardableSpellCount(ClientCard except = null)
		{
			int discardable_hands = 0;
			int count_witchcraftspell = base.Bot.Hand.GetMatchingCardsCount((ClientCard card) => card.IsSpell() && card.HasSetcode(this.Witchcraft_setcode) && card != except);
			int count_remainhands = this.CheckRemainInDeck(new int[] { 13758665, 87769556 });
			int count_MagiciansRestage = base.Bot.Hand.GetMatchingCardsCount((ClientCard card) => card.Id == 40252269 && card != except);
			int count_MetalfoesFusion = base.Bot.Hand.GetCardCount(73594093);
			int count_WitchcrafterBystreet = base.Bot.SpellZone.GetMatchingCardsCount((ClientCard card) => card.IsFaceup() && card.Id == 83289866 && !card.IsDisabled());
			if (count_MagiciansRestage > 0)
			{
				discardable_hands += ((count_MagiciansRestage > count_remainhands) ? count_remainhands : count_MagiciansRestage);
			}
			if (!this.ActivatedCards.Contains(83289866) && (count_WitchcrafterBystreet >= 2 || (count_WitchcrafterBystreet >= 1 && base.Duel.Phase > DuelPhase.Battle)))
			{
				discardable_hands++;
			}
			return discardable_hands + (count_witchcraftspell + count_MetalfoesFusion);
		}

		// Token: 0x06002211 RID: 8721 RVA: 0x000DCFB8 File Offset: 0x000DB1B8
		public bool CheckLastChainNegated()
		{
			ClientCard lastcard = base.Util.GetLastChainCard();
			if (lastcard == null || lastcard.Controller != 1)
			{
				return false;
			}
			if (lastcard.IsMonster() && lastcard.HasSetcode(this.TimeLord_setcode) && base.Duel.Phase == DuelPhase.Standby)
			{
				return false;
			}
			if (base.DefaultCheckWhetherCardIdIsNegated(lastcard.GetOriginCode()))
			{
				return false;
			}
			if (!this.MagicianRightHand_used && lastcard.IsSpell())
			{
				if (base.Bot.MonsterZone.GetFirstMatchingCard((ClientCard c) => c.HasRace(CardRace.SpellCaster) && c.IsFaceup()) != null && base.Bot.HasInSpellZone(87769556, true, false))
				{
					return true;
				}
			}
			if (!this.MagiciansLeftHand_used && lastcard.IsTrap())
			{
				if (base.Bot.MonsterZone.GetFirstMatchingCard((ClientCard c) => c.HasRace(CardRace.SpellCaster) && c.IsFaceup()) != null && base.Bot.HasInSpellZone(13758665, true, false))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002212 RID: 8722 RVA: 0x000DD0C4 File Offset: 0x000DB2C4
		public bool CheckLinkMaterialsMatch(int LinkCount, int MaterialCount, List<ClientCard> list, bool need_tune = false)
		{
			if (list.Count < MaterialCount)
			{
				return false;
			}
			int linkcount = 0;
			foreach (ClientCard card2 in list)
			{
				linkcount += (card2.HasType(CardType.Link) ? card2.LinkCount : 1);
			}
			if (linkcount != LinkCount)
			{
				foreach (ClientCard clientCard in list)
				{
					linkcount++;
				}
				if (linkcount != LinkCount)
				{
					return false;
				}
			}
			if (need_tune)
			{
				if (list.GetFirstMatchingCard((ClientCard card) => card.IsTuner()) == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002213 RID: 8723 RVA: 0x000DD1A4 File Offset: 0x000DB3A4
		public List<ClientCard> CheckLinkMaterials(int LinkCount, int MaterialCount, bool need_tuner = false, List<ClientCard> extra = null)
		{
			List<int> psy_cardids = new List<int> { 38814750, 49036338 };
			List<ClientCard> result = base.Bot.MonsterZone.GetMatchingCards((ClientCard card) => card.IsFaceup() && psy_cardids.Contains(card.Id)).ToList<ClientCard>();
			if (this.CheckLinkMaterialsMatch(LinkCount, MaterialCount, result, need_tuner))
			{
				return result;
			}
			List<ClientCard> bot_monsters = base.Enemy.MonsterZone.GetMatchingCards((ClientCard c) => c.IsFaceup()).ToList<ClientCard>();
			if (extra != null)
			{
				bot_monsters = bot_monsters.Union(extra).ToList<ClientCard>();
			}
			bot_monsters.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			int remaindiscard = this.CheckDiscardableSpellCount(null);
			int enemybest = base.Util.GetBestAttack(base.Enemy);
			foreach (ClientCard card2 in bot_monsters)
			{
				if ((!card2.HasSetcode(this.Witchcraft_setcode) || (card2.Level < 5 && remaindiscard < 2)) && card2.Attack < enemybest && (!card2.HasType(CardType.Link) || card2.LinkMarker <= 2))
				{
					result.Add(card2);
					if (this.CheckLinkMaterialsMatch(LinkCount, MaterialCount, result, need_tuner))
					{
						return result;
					}
				}
			}
			if (!this.CheckLinkMaterialsMatch(LinkCount, MaterialCount, result, need_tuner))
			{
				result.Clear();
			}
			return result;
		}

		// Token: 0x06002214 RID: 8724 RVA: 0x000DD328 File Offset: 0x000DB528
		public int CheckPlusAttackforMadameVerre(bool ignore_activated = false, bool check_recycle = false, bool force = false)
		{
			if (!force)
			{
				if (base.Bot.MonsterZone.GetFirstMatchingCard((ClientCard card) => card.Id == 21522601 && !card.IsDisabled()) == null)
				{
					return 0;
				}
			}
			if (!ignore_activated && this.MadameVerreGainedATK)
			{
				return 0;
			}
			HashSet<int> spells_id = new HashSet<int>();
			foreach (ClientCard card2 in base.Bot.Hand)
			{
				if (card2.IsSpell())
				{
					spells_id.Add(card2.Id);
				}
			}
			if (check_recycle && base.Bot.MonsterZone.GetFirstMatchingCard((ClientCard card) => card.IsFaceup() && card.HasSetcode(this.Witchcraft_setcode)) != null)
			{
				foreach (int cardid in new List<int> { 83301414, 57916305, 56894757, 70226289, 10805153 })
				{
					if (base.Bot.HasInGraveyard(cardid) && !this.ActivatedCards.Contains(cardid))
					{
						spells_id.Add(base.Card.Id);
					}
				}
			}
			return ((spells_id.Count<int>() >= 6) ? 6 : spells_id.Count<int>()) * 1000;
		}

		// Token: 0x06002215 RID: 8725 RVA: 0x000DD4B4 File Offset: 0x000DB6B4
		public ClientCard CheckProblematicCards(bool canBeTarget = false, bool OnlyDanger = false)
		{
			ClientCard card = base.Enemy.MonsterZone.GetFloodgate(canBeTarget);
			if (card != null)
			{
				return card;
			}
			card = base.Enemy.MonsterZone.GetDangerousMonster(canBeTarget);
			if (card != null && (base.Duel.Player == 0 || (base.Duel.Phase > DuelPhase.Main1 && base.Duel.Phase < DuelPhase.Main2)))
			{
				return card;
			}
			card = base.Enemy.MonsterZone.GetInvincibleMonster(canBeTarget);
			if (card != null && (base.Duel.Player == 0 || (base.Duel.Phase > DuelPhase.Main1 && base.Duel.Phase < DuelPhase.Main2)))
			{
				return card;
			}
			List<ClientCard> list = base.Enemy.MonsterZone.GetMatchingCards((ClientCard c) => c.IsFaceup()).ToList<ClientCard>();
			list.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			list.Reverse();
			foreach (ClientCard target in list)
			{
				if ((target.HasType(CardType.Fusion) || target.HasType(CardType.Ritual) || target.HasType(CardType.Synchro) || target.HasType(CardType.Xyz) || (target.HasType(CardType.Link) && target.LinkCount >= 2)) && (!canBeTarget || (!target.IsShouldNotBeTarget() && !target.IsShouldNotBeMonsterTarget())))
				{
					return target;
				}
			}
			if (OnlyDanger)
			{
				return null;
			}
			int highest_self = base.Util.GetBestPower(base.Bot, false);
			if (!this.MadameVerreGainedATK && base.Bot.HasInMonstersZone(21522601, true, false, true))
			{
				highest_self += this.CheckPlusAttackforMadameVerre(false, false, false);
			}
			return base.Util.GetProblematicEnemyCard(highest_self, canBeTarget);
		}

		// Token: 0x06002216 RID: 8726 RVA: 0x000DD698 File Offset: 0x000DB898
		public int CheckRecyclableCount(bool tohand = false, bool ignore_monster = false)
		{
			if (!ignore_monster && base.Bot.MonsterZone.GetFirstMatchingCard((ClientCard card) => card.IsFaceup() && card.HasSetcode(this.Witchcraft_setcode)) == null)
			{
				return 0;
			}
			int result = 0;
			List<int> spell_checklist = new List<int> { 83301414, 57916305, 56894757, 70226289, 10805153 };
			if (!tohand)
			{
				spell_checklist.Add(83289866);
				spell_checklist.Add(19673561);
			}
			foreach (int cardid in spell_checklist)
			{
				if (base.Bot.HasInGraveyard(cardid) && !this.ActivatedCards.Contains(cardid))
				{
					result++;
				}
			}
			return result;
		}

		// Token: 0x06002217 RID: 8727 RVA: 0x000DD77C File Offset: 0x000DB97C
		public int CheckRemainInDeck(int id)
		{
			if (id <= 54693926)
			{
				if (id <= 21522601)
				{
					if (id <= 13758665)
					{
						if (id <= 10805153)
						{
							if (id == 10045474)
							{
								return base.Bot.GetRemainingCount(10045474, 3);
							}
							if (id == 10805153)
							{
								return base.Bot.GetRemainingCount(10805153, 1);
							}
						}
						else
						{
							if (id == 11110587)
							{
								return base.Bot.GetRemainingCount(11110587, 2);
							}
							if (id == 13758665)
							{
								return base.Bot.GetRemainingCount(13758665, 1);
							}
						}
					}
					else if (id <= 14558127)
					{
						if (id == 14532163)
						{
							return base.Bot.GetRemainingCount(14532163, 2);
						}
						if (id == 14558127)
						{
							return base.Bot.GetRemainingCount(14558127, 1);
						}
					}
					else
					{
						if (id == 19673561)
						{
							return base.Bot.GetRemainingCount(19673561, 1);
						}
						if (id == 21522601)
						{
							return base.Bot.GetRemainingCount(21522601, 1);
						}
					}
				}
				else if (id <= 38814750)
				{
					if (id <= 23434538)
					{
						if (id == 21744288)
						{
							return base.Bot.GetRemainingCount(21744288, 3);
						}
						if (id == 23434538)
						{
							return base.Bot.GetRemainingCount(23434538, 1);
						}
					}
					else
					{
						if (id == 24224830)
						{
							return base.Bot.GetRemainingCount(24224830, 3);
						}
						if (id == 38814750)
						{
							return base.Bot.GetRemainingCount(38814750, 3);
						}
					}
				}
				else if (id <= 49036338)
				{
					if (id == 40252269)
					{
						return base.Bot.GetRemainingCount(40252269, 2);
					}
					if (id == 49036338)
					{
						return base.Bot.GetRemainingCount(49036338, 1);
					}
				}
				else
				{
					if (id == 49238328)
					{
						return base.Bot.GetRemainingCount(49238328, 3);
					}
					if (id == 54693926)
					{
						return base.Bot.GetRemainingCount(54693926, 2);
					}
				}
			}
			else if (id <= 70226289)
			{
				if (id <= 58577036)
				{
					if (id <= 56894757)
					{
						if (id == 55072170)
						{
							return base.Bot.GetRemainingCount(55072170, 1);
						}
						if (id == 56894757)
						{
							return base.Bot.GetRemainingCount(56894757, 1);
						}
					}
					else
					{
						if (id == 57916305)
						{
							return base.Bot.GetRemainingCount(57916305, 3);
						}
						if (id == 58577036)
						{
							return base.Bot.GetRemainingCount(58577036, 3);
						}
					}
				}
				else if (id <= 64756282)
				{
					if (id == 59851535)
					{
						return base.Bot.GetRemainingCount(59851535, 1);
					}
					if (id == 64756282)
					{
						return base.Bot.GetRemainingCount(64756282, 2);
					}
				}
				else
				{
					if (id == 65681983)
					{
						return base.Bot.GetRemainingCount(65681983, 2);
					}
					if (id == 70226289)
					{
						return base.Bot.GetRemainingCount(70226289, 1);
					}
				}
			}
			else if (id <= 83301414)
			{
				if (id <= 73594093)
				{
					if (id == 71074418)
					{
						return base.Bot.GetRemainingCount(71074418, 1);
					}
					if (id == 73594093)
					{
						return base.Bot.GetRemainingCount(73594093, 1);
					}
				}
				else
				{
					if (id == 83289866)
					{
						return base.Bot.GetRemainingCount(83289866, 3);
					}
					if (id == 83301414)
					{
						return base.Bot.GetRemainingCount(83301414, 3);
					}
				}
			}
			else if (id <= 87769556)
			{
				if (id == 84523092)
				{
					return base.Bot.GetRemainingCount(84523092, 2);
				}
				if (id == 87769556)
				{
					return base.Bot.GetRemainingCount(87769556, 1);
				}
			}
			else
			{
				if (id == 94553671)
				{
					return base.Bot.GetRemainingCount(94553671, 2);
				}
				if (id == 95245544)
				{
					return base.Bot.GetRemainingCount(95245544, 3);
				}
			}
			return 0;
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x000DDBF4 File Offset: 0x000DBDF4
		public int CheckRemainInDeck(params int[] ids)
		{
			int result = 0;
			foreach (int cardid in ids)
			{
				result += this.CheckRemainInDeck(cardid);
			}
			return result;
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x000DDC24 File Offset: 0x000DBE24
		public bool CheckWhetherWillbeRemoved()
		{
			if (this.enemy_activate_DimensionShifter)
			{
				return true;
			}
			foreach (int cardid in new List<int> { 94853057, 61528025, 30241314, 81674782 })
			{
				foreach (ClientField cf in new List<ClientField> { base.Bot, base.Enemy })
				{
					if (cf.HasInMonstersZone(cardid, true, false, false) || cf.HasInSpellZone(cardid, true, false))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600221A RID: 8730 RVA: 0x000DDD18 File Offset: 0x000DBF18
		public bool SpellNegatable(bool isCounter = false, ClientCard target = null)
		{
			if (target == null)
			{
				target = base.Card;
			}
			if (this.CheckCalledbytheGrave(target.GetOriginCode()) > 0)
			{
				return true;
			}
			if (target.Location != CardLocation.SpellZone && target.Location != CardLocation.Hand)
			{
				return false;
			}
			if (base.Enemy.HasInMonstersZone(99916754, true, false, false) && !isCounter)
			{
				return true;
			}
			if (target.IsSpell())
			{
				if (base.Enemy.HasInMonstersZone(33198837, true, false, false))
				{
					return true;
				}
				if (base.Enemy.HasInSpellZone(61740673, true, false) || base.Bot.HasInSpellZone(61740673, true, false))
				{
					return true;
				}
				if (base.Enemy.HasInMonstersZone(37267041, true, false, false) || base.Bot.HasInMonstersZone(37267041, true, false, false))
				{
					return true;
				}
			}
			return target.IsTrap() && (base.Enemy.HasInSpellZone(51452091, true, false) || base.Bot.HasInSpellZone(51452091, true, false));
		}

		// Token: 0x0600221B RID: 8731 RVA: 0x000DDE18 File Offset: 0x000DC018
		public bool NegatedCheck(bool disablecheck = true)
		{
			if ((base.Card.IsSpell() || base.Card.IsTrap()) && this.SpellNegatable(false, null))
			{
				return true;
			}
			if (this.CheckCalledbytheGrave(base.Card.GetOriginCode()) > 0)
			{
				return true;
			}
			if (base.Card.IsMonster() && base.Card.Location == CardLocation.MonsterZone && base.Card.IsDefense())
			{
				if (base.Enemy.MonsterZone.GetFirstMatchingFaceupCard((ClientCard card) => card.Id == 90590303 && card.IsDefense() && !card.IsDisabled()) == null)
				{
					if (base.Bot.MonsterZone.GetFirstMatchingFaceupCard((ClientCard card) => card.Id == 90590303 && card.IsDefense() && !card.IsDisabled()) == null)
					{
						goto IL_00C8;
					}
				}
				return true;
			}
			IL_00C8:
			return disablecheck && base.Card.IsDisabled();
		}

		// Token: 0x0600221C RID: 8732 RVA: 0x000DDF00 File Offset: 0x000DC100
		public void SelectSTPlace(ClientCard card = null, bool avoid_Impermanence = false, List<int> avoid_list = null)
		{
			if (card == null)
			{
				card = base.Card;
			}
			List<int> list = new List<int>();
			for (int seq = 0; seq < 5; seq++)
			{
				if (base.Bot.SpellZone[seq] == null && (card == null || card.Location != CardLocation.Hand || !avoid_Impermanence || !this.Impermanence_list.Contains(seq)) && (avoid_list == null || !avoid_list.Contains(seq)))
				{
					list.Add(seq);
				}
			}
			int i = list.Count;
			while (i-- > 1)
			{
				int index = Program.Rand.Next(list.Count);
				int nextIndex = (index + Program.Rand.Next(list.Count - 1)) % list.Count;
				int tempInt = list[index];
				list[index] = list[nextIndex];
				list[nextIndex] = tempInt;
			}
			if (avoid_Impermanence)
			{
				if (base.Bot.GetMonsters().Any((ClientCard c) => c.IsFaceup() && !c.IsDisabled()))
				{
					foreach (int seq2 in list)
					{
						ClientCard enemySpell = base.Enemy.SpellZone[4 - seq2];
						if (enemySpell == null || !enemySpell.IsFacedown())
						{
							int zone = (int)Math.Pow(2.0, (double)seq2);
							base.AI.SelectPlace(zone);
							return;
						}
					}
				}
			}
			using (List<int>.Enumerator enumerator = list.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					int seq3 = enumerator.Current;
					int zone2 = (int)Math.Pow(2.0, (double)seq3);
					base.AI.SelectPlace(zone2);
					return;
				}
			}
			base.AI.SelectPlace(0);
		}

		// Token: 0x0600221D RID: 8733 RVA: 0x000DE0EC File Offset: 0x000DC2EC
		public bool SpellSet()
		{
			if (base.Duel.Phase == DuelPhase.Main1 && base.Bot.HasAttackingMonster() && base.Duel.Turn > 1)
			{
				return false;
			}
			if (base.Card.Id == 65681983 && base.Duel.Turn >= 5)
			{
				return false;
			}
			if (new int[] { 55072170, 56894757 }.Contains(base.Card.Id) && base.Bot.MonsterZone.GetFirstMatchingCard((ClientCard card) => card.HasSetcode(this.Witchcraft_setcode)) == null)
			{
				return false;
			}
			if (base.Card.Id == 70226289)
			{
				return false;
			}
			if (base.Card.Id == 94553671)
			{
				int count = base.Bot.Banished.GetMatchingCardsCount((ClientCard card) => card.HasSetcode(this.Witchcraft_setcode));
				if (count == 0)
				{
					count += base.Bot.Graveyard.GetMatchingCardsCount((ClientCard card) => card.HasSetcode(this.Witchcraft_setcode));
				}
				if (count == 0)
				{
					return false;
				}
			}
			if (base.Card.IsSpell())
			{
				int spells_todiscard = this.CheckRecyclableCount(false, false) + base.Bot.Hand.GetMatchingCardsCount((ClientCard card) => card.IsSpell());
				int will_discard = 0;
				if (base.Bot.HasInMonstersZone(84523092, false, false, false))
				{
					will_discard++;
				}
				if (base.Bot.HasInMonstersZone(21522601, false, false, false))
				{
					will_discard++;
				}
				if (will_discard >= spells_todiscard)
				{
					return false;
				}
			}
			if (base.Card.IsTrap() || base.Card.HasType(CardType.QuickPlay))
			{
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
					this.SelectSTPlace(null, false, null);
					return true;
				}
				if (base.Card.IsCode(10045474))
				{
					base.AI.SelectPlace(Impermanence_set);
					return true;
				}
				this.SelectSTPlace(base.Card, false, avoid_list);
				return true;
			}
			else
			{
				if ((base.Enemy.HasInSpellZone(58921041, true, false) || base.Bot.HasInSpellZone(58921041, true, false)) && base.Card.IsSpell() && base.Card.Id != 73594093)
				{
					this.SelectSTPlace(null, false, null);
					return true;
				}
				return false;
			}
		}

		// Token: 0x0600221E RID: 8734 RVA: 0x000DE3B0 File Offset: 0x000DC5B0
		public bool SpellSetForFiveRainbow()
		{
			bool have_FiveRainbow = false;
			List<ClientCard> list = new List<ClientCard>();
			if (base.Duel.IsNewRule || base.Duel.IsNewRule2020)
			{
				list.Add(base.Enemy.SpellZone[0]);
				list.Add(base.Enemy.SpellZone[4]);
			}
			else
			{
				list.Add(base.Enemy.SpellZone[6]);
				list.Add(base.Enemy.SpellZone[7]);
			}
			foreach (ClientCard card2 in list)
			{
				if (card2 != null && card2.Id == 19619755)
				{
					have_FiveRainbow = true;
					break;
				}
			}
			if (!have_FiveRainbow)
			{
				return false;
			}
			if (base.Bot.GetMonsterCount() != 0)
			{
				if (base.Bot.SpellZone.GetFirstMatchingCard((ClientCard card) => card.IsFacedown()) == null)
				{
					if (base.Card.IsSpell())
					{
						this.SelectSTPlace(null, true, null);
						return true;
					}
					return false;
				}
			}
			return false;
		}

		// Token: 0x0600221F RID: 8735 RVA: 0x000DE4DC File Offset: 0x000DC6DC
		public bool MonsterRepos()
		{
			int self_attack = base.Card.Attack + 1;
			int extra_attack = this.CheckPlusAttackforMadameVerre(true, true, false);
			Logger.DebugWriteLine("self_attack of " + (base.Card.Name ?? "X") + ": " + self_attack.ToString());
			if (base.Card.HasSetcode(this.Witchcraft_setcode))
			{
				self_attack += extra_attack;
			}
			if (base.Card.IsFaceup() && base.Card.IsDefense() && self_attack <= 1)
			{
				return false;
			}
			int best_attack = 0;
			foreach (ClientCard clientCard in base.Bot.GetMonsters())
			{
				int attack = clientCard.Attack;
				if (clientCard.HasSetcode(this.Witchcraft_setcode))
				{
					attack += extra_attack;
				}
				if (attack >= best_attack)
				{
					best_attack = attack;
				}
			}
			bool enemyBetter = base.Util.IsAllEnemyBetterThanValue(best_attack, true);
			return (base.Card.IsAttack() && enemyBetter) || (base.Card.IsDefense() && !enemyBetter && self_attack >= base.Card.Defense);
		}

		// Token: 0x06002220 RID: 8736 RVA: 0x000DE610 File Offset: 0x000DC810
		public void SelectDiscardSpell()
		{
			int count_remainhands = this.CheckRemainInDeck(new int[] { 13758665, 87769556 });
			int count_witchcraftspell = base.Bot.Hand.GetMatchingCardsCount((ClientCard card) => card.IsSpell() && card.HasSetcode(this.Witchcraft_setcode));
			int WitchcrafterBystreet_count = base.Bot.SpellZone.GetMatchingCardsCount((ClientCard card) => card.IsFaceup() && card.Id == 83289866);
			if (base.Bot.HasInHand(40252269) && count_remainhands > 0)
			{
				base.AI.SelectCard(40252269);
				return;
			}
			if (base.Bot.HasInHand(73594093))
			{
				base.AI.SelectCard(73594093);
				return;
			}
			if (!this.ActivatedCards.Contains(19673561) && base.Bot.SpellZone.GetCardCount(19673561) > 0)
			{
				base.AI.SelectCard(base.Bot.SpellZone.GetFirstMatchingFaceupCard((ClientCard card) => card.Id == 19673561));
				this.ActivatedCards.Add(19673561);
				return;
			}
			if (!this.ActivatedCards.Contains(83289866) && WitchcrafterBystreet_count >= 2)
			{
				base.AI.SelectCard(base.Bot.SpellZone.GetFirstMatchingFaceupCard((ClientCard card) => card.Id == 83289866));
				this.ActivatedCards.Add(83289866);
				return;
			}
			if (count_witchcraftspell > 0)
			{
				List<int> cost_list = new List<int> { 19673561, 83289866, 10805153, 70226289, 56894757 };
				if (base.Duel.Player == 1)
				{
					cost_list.Add(57916305);
					cost_list.Add(83301414);
				}
				else
				{
					cost_list.Add(83301414);
					cost_list.Add(57916305);
				}
				using (List<int>.Enumerator enumerator = cost_list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						int cardid = enumerator.Current;
						IList<ClientCard> targets = base.Bot.Hand.GetMatchingCards((ClientCard card) => card.Id == cardid);
						if (targets.Count<ClientCard>() > 0)
						{
							base.AI.SelectCard(targets);
							return;
						}
					}
				}
				base.AI.SelectCard(new int[] { 19673561, 83289866 });
				return;
			}
			if (base.Bot.HasInHand(49238328) && base.Bot.ExtraDeck.Count < 6)
			{
				base.AI.SelectCard(49238328);
				return;
			}
			if (WitchcrafterBystreet_count >= 1)
			{
				base.AI.SelectCard(base.Bot.SpellZone.GetFirstMatchingFaceupCard((ClientCard card) => card.Id == 83289866));
				this.ActivatedCards.Add(83289866);
				return;
			}
			base.AI.SelectCard(new int[] { 11110587, 14532163, 49238328, 13758665, 87769556, 65681983, 24224830 });
		}

		// Token: 0x06002221 RID: 8737 RVA: 0x000DE970 File Offset: 0x000DCB70
		public bool SpellsActivate()
		{
			if (this.SpellNegatable(false, null))
			{
				return false;
			}
			if (this.CheckDiscardableSpellCount(null) <= 1)
			{
				return false;
			}
			if ((base.Card.Id == 11110587 || base.Card.Id == 58577036) && this.CheckWhetherWillbeRemoved())
			{
				return false;
			}
			if (base.Card.Id == 13758665 || base.Card.Id == 87769556)
			{
				if (base.Bot.MonsterZone.GetFirstMatchingCard((ClientCard card) => card.HasRace(CardRace.SpellCaster)) == null)
				{
					if (!this.summoned)
					{
						if (base.Bot.Hand.GetFirstMatchingCard((ClientCard card) => card.HasRace(CardRace.SpellCaster) && card.Level <= 4) != null)
						{
							goto IL_00D6;
						}
					}
					return false;
				}
			}
			IL_00D6:
			this.SelectSTPlace(base.Card, true, null);
			return true;
		}

		// Token: 0x06002222 RID: 8738 RVA: 0x000DEA64 File Offset: 0x000DCC64
		public bool SpellsActivateNoCost()
		{
			if (this.SpellNegatable(false, null))
			{
				return false;
			}
			if ((base.Card.Id == 11110587 || base.Card.Id == 58577036) && this.CheckWhetherWillbeRemoved())
			{
				return false;
			}
			if (base.Card.Id == 13758665 || base.Card.Id == 87769556)
			{
				if (base.Bot.MonsterZone.GetFirstMatchingCard((ClientCard card) => card.HasRace(CardRace.SpellCaster)) == null)
				{
					if (!this.summoned)
					{
						if (base.Bot.Hand.GetFirstMatchingCard((ClientCard card) => card.HasRace(CardRace.SpellCaster) && card.Level <= 4) != null)
						{
							goto IL_00CA;
						}
					}
					return false;
				}
			}
			IL_00CA:
			this.SelectSTPlace(base.Card, true, null);
			return true;
		}

		// Token: 0x06002223 RID: 8739 RVA: 0x000DEB4C File Offset: 0x000DCD4C
		public bool SpellsActivatewithCounter()
		{
			if (this.SpellNegatable(false, null))
			{
				return false;
			}
			if ((base.Card.Id == 11110587 || base.Card.Id == 58577036) && this.CheckWhetherWillbeRemoved())
			{
				return false;
			}
			int[] counter_cards = new int[] { 38814750, 24224830, 65681983 };
			if (base.Bot.Hand.GetMatchingCardsCount((ClientCard card) => counter_cards.Contains(card.Id)) + base.Bot.SpellZone.GetMatchingCardsCount((ClientCard card) => counter_cards.Contains(card.Id)) > 0 || base.Bot.Hand.GetCardCount(base.Card.Id) >= 2)
			{
				this.SelectSTPlace(base.Card, true, null);
				return true;
			}
			return Program.Rand.Next(2) > 0;
		}

		// Token: 0x06002224 RID: 8740 RVA: 0x000DEC28 File Offset: 0x000DCE28
		public bool WitchcraftSummon()
		{
			if (this.UseSSEffect.Contains(base.Card.Id))
			{
				return false;
			}
			int count_spell = base.Bot.Hand.GetMatchingCardsCount((ClientCard card) => card.IsSpell());
			int count_target = this.CheckRemainInDeck(new int[] { 21522601, 84523092, 71074418 });
			if (count_spell > 0 && count_target > 0)
			{
				this.summoned = true;
				return true;
			}
			return false;
		}

		// Token: 0x06002225 RID: 8741 RVA: 0x000DECAC File Offset: 0x000DCEAC
		public bool WitchcraftSummonForRecycle()
		{
			if (!base.Card.HasSetcode(this.Witchcraft_setcode) || base.Card.Level > 4)
			{
				return false;
			}
			if (this.CheckRecyclableCount(false, true) > 0 && base.Bot.MonsterZone.GetFirstMatchingFaceupCard((ClientCard card) => card.HasSetcode(this.Witchcraft_setcode)) == null)
			{
				this.summoned = true;
				return true;
			}
			return false;
		}

		// Token: 0x06002226 RID: 8742 RVA: 0x000DED10 File Offset: 0x000DCF10
		public bool SummonForLink()
		{
			if (base.Card.Level >= 5)
			{
				return false;
			}
			this.summoned = true;
			if (this.BorrelswordDragonSummonCheck(base.Card).Count >= 3)
			{
				Logger.DebugWriteLine("Summon for BorrelswordDragon.");
				foreach (ClientCard clientCard in this.BorrelswordDragonSummonCheck(base.Card))
				{
					Logger.DebugWriteLine(clientCard.Name ?? "???");
				}
				return true;
			}
			if (this.KnightmareUnicornSummonCheck(base.Card).Count >= 2)
			{
				Logger.DebugWriteLine("Summon for KnightmareUnicorn.");
				return true;
			}
			if (this.KnightmarePhoenixSummonCheck(base.Card).Count >= 2)
			{
				Logger.DebugWriteLine("Summon for KnightmarePhoenix.");
				return true;
			}
			if (this.RelinquishedAnimaSummonCheck(base.Card) != -1)
			{
				Logger.DebugWriteLine("Summon for RelinquishedAnima.");
				return true;
			}
			if (this.SalamangreatAlmirajSummonCheck(base.Card))
			{
				Logger.DebugWriteLine("Summon for SalamangreatAlmiraj.");
				return true;
			}
			this.summoned = false;
			return false;
		}

		// Token: 0x06002227 RID: 8743 RVA: 0x000DEE28 File Offset: 0x000DD028
		public bool DeckSSWitchcraft()
		{
			if (base.Card.Location != CardLocation.MonsterZone)
			{
				return false;
			}
			if (base.Duel.LastChainPlayer == 0)
			{
				return false;
			}
			if (this.NegatedCheck(false))
			{
				return false;
			}
			if (base.Duel.Player == 0 && !this.FirstCheckSS.Contains(base.Card.Id))
			{
				this.FirstCheckSS.Add(base.Card.Id);
				return false;
			}
			int discardable_hands = this.CheckDiscardableSpellCount(null);
			if (discardable_hands == 0 && base.Bot.MonsterZone.GetFirstMatchingCard((ClientCard card) => card.HasSetcode(this.Witchcraft_setcode) && card.Level >= 6) != null)
			{
				return false;
			}
			this.SelectDiscardSpell();
			bool lesssummon = false;
			int extra_attack = this.CheckPlusAttackforMadameVerre(true, false, true);
			int best_power = base.Util.GetBestAttack(base.Bot);
			if (this.CheckRemainInDeck(84523092) > 0 && best_power < 2400)
			{
				best_power = 2400;
			}
			Logger.DebugWriteLine("less summon check: " + (best_power + extra_attack - 1000).ToString() + " to " + (best_power + extra_attack).ToString());
			if (base.Util.GetOneEnemyBetterThanValue(best_power, false, false) != null && base.Util.GetOneEnemyBetterThanValue(best_power + extra_attack, false, false) == null && base.Util.GetOneEnemyBetterThanValue(best_power + extra_attack - 1000, false, false) != null)
			{
				lesssummon = true;
			}
			if (!this.enemy_activate_MaxxC && !lesssummon && discardable_hands >= 2 && base.Duel.Player == 0)
			{
				int[] array = new int[] { 21744288, 95245544, 64756282, 59851535 };
				for (int i = 0; i < array.Length; i++)
				{
					int cardid = array[i];
					if (!this.UseSSEffect.Contains(cardid) && base.Card.Id != cardid && this.CheckRemainInDeck(cardid) > 0 && base.Bot.MonsterZone.GetFirstMatchingCard((ClientCard card) => card.Id == cardid && card.IsFaceup()) == null)
					{
						this.UseSSEffect.Add(base.Card.Id);
						base.AI.SelectNextCard(cardid);
						return true;
					}
				}
			}
			if (((base.Util.GetOneEnemyBetterThanValue(base.Card.Attack, false, false) == null) ^ base.Card.IsDefense()) && base.Duel.Player == 1)
			{
				return false;
			}
			if (this.CheckRemainInDeck(new int[] { 84523092, 21522601, 71074418 }) == 0)
			{
				return false;
			}
			if (base.Bot.HasInMonstersZone(84523092, false, false, false) || (lesssummon && !base.Bot.HasInMonstersZone(21522601, true, false, false)))
			{
				base.AI.SelectNextCard(new int[] { 21522601, 84523092, 71074418 });
			}
			else
			{
				base.AI.SelectNextCard(new int[] { 84523092, 21522601, 71074418 });
			}
			this.UseSSEffect.Add(base.Card.Id);
			return true;
		}

		// Token: 0x06002228 RID: 8744 RVA: 0x000DF130 File Offset: 0x000DD330
		public bool WitchcraftRecycle()
		{
			if (base.Card.IsSpell() && base.Card.HasSetcode(this.Witchcraft_setcode) && base.Card.Location == CardLocation.Grave)
			{
				this.ActivatedCards.Add(base.Card.Id);
				if (base.Card.HasType(CardType.Continuous))
				{
					this.SelectSTPlace(base.Card, false, null);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06002229 RID: 8745 RVA: 0x000DF1A8 File Offset: 0x000DD3A8
		public bool GolemAruruActivate()
		{
			if (base.ActivateDescription == base.Util.GetStringId(71074418, 2))
			{
				return true;
			}
			if (this.NegatedCheck(true))
			{
				return false;
			}
			ClientCard targetcard = this.CheckProblematicCards(true, false);
			if (targetcard != null)
			{
				base.AI.SelectCard(targetcard);
				return true;
			}
			base.AI.SelectCard(new int[] { 83301414, 57916305, 56894757, 19673561, 83289866, 70226289, 10805153 });
			return true;
		}

		// Token: 0x0600222A RID: 8746 RVA: 0x000DF214 File Offset: 0x000DD414
		public bool MadameVerreActivate()
		{
			if (this.NegatedCheck(true))
			{
				return false;
			}
			if (base.ActivateDescription != base.Util.GetStringId(21522601, 1))
			{
				ClientCard self_card = base.Bot.BattlingMonster;
				ClientCard enemy_card = base.Enemy.BattlingMonster;
				if (self_card != null && enemy_card != null)
				{
					int power_cangain = this.CheckPlusAttackforMadameVerre(false, false, false);
					int diff = enemy_card.GetDefensePower() - self_card.GetDefensePower();
					Logger.DebugWriteLine("power: " + power_cangain.ToString());
					Logger.DebugWriteLine("diff: " + diff.ToString());
					if (diff > 0)
					{
						if (self_card.IsDefense() && power_cangain < diff)
						{
							return false;
						}
						base.AI.SelectCard(base.Bot.Hand.GetMatchingCards((ClientCard card) => card.IsSpell()));
						this.MadameVerreGainedATK = true;
						return true;
					}
					else if (base.Enemy.GetMonsterCount() == 1 || (enemy_card.IsAttack() && base.Enemy.LifePoints <= diff + power_cangain))
					{
						base.AI.SelectCard(base.Bot.Hand.GetMatchingCards((ClientCard card) => card.IsSpell()));
						this.MadameVerreGainedATK = true;
						return true;
					}
				}
				return false;
			}
			if (base.Card.IsDisabled())
			{
				return false;
			}
			if (this.CheckLastChainNegated())
			{
				return false;
			}
			if (base.Enemy.MonsterZone.GetFirstMatchingCard((ClientCard card) => card.IsMonsterShouldBeDisabledBeforeItUseEffect() && !card.IsDisabled()) != null)
			{
				this.SelectDiscardSpell();
				return true;
			}
			ClientCard LastChainCard = base.Util.GetLastChainCard();
			if (LastChainCard != null && LastChainCard.Controller == 1 && LastChainCard.Location == CardLocation.MonsterZone)
			{
				this.SelectDiscardSpell();
				return true;
			}
			if (base.Duel.Phase > DuelPhase.Main1 && base.Duel.Phase < DuelPhase.Main2 && base.Enemy.MonsterZone.GetFirstMatchingCard((ClientCard card) => card.IsMonsterDangerous() || (base.Duel.Player == 0 && card.IsMonsterInvincible())) != null)
			{
				this.SelectDiscardSpell();
				return true;
			}
			return false;
		}

		// Token: 0x0600222B RID: 8747 RVA: 0x000DF438 File Offset: 0x000DD638
		public bool HaineActivate()
		{
			if (this.NegatedCheck(true) || base.Duel.LastChainPlayer == 0)
			{
				return false;
			}
			ClientCard targetcard = base.Enemy.MonsterZone.GetFloodgate(true);
			if (targetcard == null)
			{
				Logger.DebugWriteLine("*** Haine 2nd check.");
				targetcard = base.Enemy.SpellZone.FirstOrDefault((ClientCard card) => ((card != null) ? card.Data : null) != null && card.IsFloodgate() && card.IsFaceup() && (!card.IsShouldNotBeTarget() || !base.Duel.ChainTargets.Contains(card)));
			}
			if (targetcard == null)
			{
				Logger.DebugWriteLine("*** Haine 3rd check.");
				targetcard = this.CheckProblematicCards(true, base.Duel.Phase <= DuelPhase.Main1 || base.Duel.Phase >= DuelPhase.Main2);
				if (targetcard != null && targetcard.HasSetcode(this.TimeLord_setcode) && !targetcard.IsDisabled())
				{
					targetcard = null;
				}
			}
			if (targetcard == null && base.Duel.LastChainPlayer == 1)
			{
				Logger.DebugWriteLine("*** Haine 4th check.");
				ClientCard lastcard = base.Util.GetLastChainCard();
				if (lastcard != null && !lastcard.IsDisabled() && !this.CheckLastChainNegated() && (lastcard.HasType(CardType.Continuous) || lastcard.HasType(CardType.Equip) || lastcard.HasType(CardType.Field)) && (lastcard.Location == CardLocation.SpellZone || lastcard.Location == CardLocation.FieldZone))
				{
					targetcard = lastcard;
				}
			}
			if (targetcard != null)
			{
				Logger.DebugWriteLine("*** Haine target: " + targetcard.Name);
				this.SelectDiscardSpell();
				base.AI.SelectNextCard(targetcard);
				return true;
			}
			if (!this.CheckLastChainNegated())
			{
				ClientCard i;
				ClientCard r;
				if (base.Duel.IsNewRule || base.Duel.IsNewRule2020)
				{
					i = base.Enemy.SpellZone[0];
					r = base.Enemy.SpellZone[4];
				}
				else
				{
					i = base.Enemy.SpellZone[6];
					r = base.Enemy.SpellZone[7];
				}
				if (i != null && r != null && i.LScale != r.RScale)
				{
					Logger.DebugWriteLine("*** Haine pendulum destroy");
					this.SelectDiscardSpell();
					base.AI.SelectNextCard((Program.Rand.Next(2) == 1) ? i : r);
					return true;
				}
			}
			if (base.Duel.Player != 0 || base.Duel.Phase != DuelPhase.End)
			{
				return false;
			}
			Logger.DebugWriteLine("*** Haine self check");
			int selected_cost = 0;
			foreach (int cardid in new int[] { 10805153, 70226289, 19673561, 83301414, 57916305, 56894757 })
			{
				if (!this.ActivatedCards.Contains(cardid) && base.Bot.HasInHand(cardid))
				{
					selected_cost = cardid;
					break;
				}
			}
			if (selected_cost == 0)
			{
				return false;
			}
			IEnumerable<ClientCard> matchingCards = base.Enemy.SpellZone.GetMatchingCards((ClientCard card) => card.IsFaceup());
			IList<ClientCard> target_2 = base.Enemy.MonsterZone.GetMatchingCards((ClientCard card) => card.IsFaceup());
			List<ClientCard> targets = matchingCards.Union(target_2).ToList<ClientCard>();
			if (targets.Count == 0)
			{
				return false;
			}
			targets = this.CardListShuffle(targets);
			base.AI.SelectCard(selected_cost);
			base.AI.SelectNextCard(targets);
			return true;
		}

		// Token: 0x0600222C RID: 8748 RVA: 0x000DF764 File Offset: 0x000DD964
		public bool SchmiettaActivate()
		{
			if (base.Card.Location != CardLocation.Grave)
			{
				return false;
			}
			if (this.NegatedCheck(false) || this.CheckWhetherWillbeRemoved())
			{
				return false;
			}
			bool can_recycle = base.Bot.MonsterZone.GetFirstMatchingCard((ClientCard card) => card.IsFaceup() && card.HasSetcode(this.Witchcraft_setcode) && card.Id != 71074418) != null;
			if (can_recycle)
			{
				foreach (int cardid in new int[] { 83289866, 83301414, 57916305, 56894757, 19673561, 70226289, 10805153 })
				{
					if (this.CheckRemainInDeck(cardid) > 0 && !base.Bot.HasInHandOrInSpellZone(cardid) && !base.Bot.HasInGraveyard(cardid) && !this.ActivatedCards.Contains(cardid))
					{
						base.AI.SelectCard(cardid);
						this.ActivatedCards.Add(21744288);
						return true;
					}
				}
			}
			bool can_find_Holiday = base.Bot.HasInHandOrInSpellZone(83301414) || (can_recycle && base.Bot.HasInGraveyard(83301414) && !this.ActivatedCards.Contains(83301414));
			if (base.Bot.HasInHand(this.important_witchcraft) && !base.Bot.HasInGraveyard(95245544) && !this.ActivatedCards.Contains(95245544) && this.CheckRemainInDeck(95245544) > 0 && can_find_Holiday)
			{
				base.AI.SelectCard(95245544);
				this.ActivatedCards.Add(21744288);
				return true;
			}
			if (base.Bot.HasInHand(83301414) && !this.ActivatedCards.Contains(83301414) && !base.Bot.HasInGraveyard(this.important_witchcraft))
			{
				base.AI.SelectCard(this.important_witchcraft);
				this.ActivatedCards.Add(21744288);
				return true;
			}
			if (!this.ActivatedCards.Contains(64756282))
			{
				int has_Genni = (base.Bot.HasInGraveyard(64756282) ? 1 : 0);
				int has_Holiday = (base.Bot.HasInGraveyard(83301414) ? 1 : 0);
				int has_important = (base.Bot.HasInGraveyard(this.important_witchcraft) ? 1 : 0);
				if (has_Genni + has_Holiday + has_important == 2)
				{
					if (has_Genni == 0)
					{
						base.AI.SelectCard(64756282);
						this.ActivatedCards.Add(21744288);
						return true;
					}
					if (has_Holiday == 0)
					{
						base.AI.SelectCard(83301414);
						this.ActivatedCards.Add(21744288);
						return true;
					}
					if (has_important == 0)
					{
						base.AI.SelectCard(this.important_witchcraft);
						this.ActivatedCards.Add(21744288);
						return true;
					}
				}
			}
			if (!this.ActivatedCards.Contains(95245544) && !base.Bot.HasInGraveyard(95245544) && this.PittoreActivate())
			{
				base.AI.SelectCard(95245544);
				this.ActivatedCards.Add(21744288);
				return true;
			}
			if (this.CheckRemainInDeck(55072170) >= 2)
			{
				base.AI.SelectCard(55072170);
				this.ActivatedCards.Add(21744288);
				return true;
			}
			return false;
		}

		// Token: 0x0600222D RID: 8749 RVA: 0x000DFA98 File Offset: 0x000DDC98
		public bool PittoreActivate()
		{
			if (base.Card.Location != CardLocation.Grave)
			{
				return false;
			}
			if (this.NegatedCheck(false) || this.CheckWhetherWillbeRemoved())
			{
				return false;
			}
			if (base.Bot.Hand.GetFirstMatchingCard((ClientCard card) => card.HasSetcode(this.Witchcraft_setcode)) == null)
			{
				return false;
			}
			if (base.Bot.Hand.GetFirstMatchingCard((ClientCard card) => card.Id == 21522601 || card.Id == 84523092) != null)
			{
				base.AI.SelectCard(new int[] { 21522601, 84523092 });
				this.ActivatedCards.Add(95245544);
				return true;
			}
			int[] spell_checklist = new int[] { 19673561, 70226289, 10805153, 56894757, 83289866, 83301414, 57916305 };
			foreach (int cardid in spell_checklist)
			{
				if (base.Bot.HasInHand(cardid) && !this.ActivatedCards.Contains(cardid))
				{
					base.AI.SelectCard(cardid);
					this.ActivatedCards.Add(95245544);
					return true;
				}
			}
			if ((base.Bot.HasInHand(21744288) && !this.ActivatedCards.Contains(21744288)) || base.Bot.Hand.GetMatchingCardsCount((ClientCard card) => card.HasSetcode(this.Witchcraft_setcode) && card.Level <= 4) >= 2)
			{
				RuntimeHelpers.InitializeArray(new int[4], fieldof(<PrivateImplementationDetails>.3B694D15FD5CADD5FE586AD493C923A4704704CF72AD60B45A6D6270C97EDE0E).FieldHandle);
				foreach (int cardid2 in spell_checklist)
				{
					if (base.Bot.HasInHand(cardid2))
					{
						base.AI.SelectCard(cardid2);
						this.ActivatedCards.Add(95245544);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600222E RID: 8750 RVA: 0x000DFC46 File Offset: 0x000DDE46
		public bool AshBlossom_JoyousSpringActivate()
		{
			return !this.NegatedCheck(true) && !this.CheckLastChainNegated() && base.DefaultAshBlossomAndJoyousSpring();
		}

		// Token: 0x0600222F RID: 8751 RVA: 0x000DFC61 File Offset: 0x000DDE61
		public bool PSYGammaActivate()
		{
			return !this.NegatedCheck(true);
		}

		// Token: 0x06002230 RID: 8752 RVA: 0x000DFC6F File Offset: 0x000DDE6F
		public bool MaxxCActivate()
		{
			return !this.NegatedCheck(true) && base.DefaultMaxxC();
		}

		// Token: 0x06002231 RID: 8753 RVA: 0x000DFC84 File Offset: 0x000DDE84
		public bool PotterieActivate()
		{
			if (base.Card.Location != CardLocation.Grave)
			{
				return false;
			}
			if (this.NegatedCheck(true))
			{
				return false;
			}
			if (!this.ActivatedCards.Contains(83301414) && base.Bot.HasInGraveyard(83301414) && base.Bot.HasInGraveyard(this.important_witchcraft))
			{
				base.AI.SelectCard(83301414);
				this.ActivatedCards.Add(59851535);
				return true;
			}
			if (this.CheckProblematicCards(false, false) == null)
			{
				foreach (int cardid in new int[] { 94553671, 71074418 })
				{
					if (base.Bot.HasInGraveyard(cardid))
					{
						base.AI.SelectCard(cardid);
						this.ActivatedCards.Add(59851535);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002232 RID: 8754 RVA: 0x000DFD68 File Offset: 0x000DDF68
		public bool GenniActivate()
		{
			if (base.Card.Location != CardLocation.Grave)
			{
				return false;
			}
			if (this.NegatedCheck(true))
			{
				return false;
			}
			int matchingCardsCount = base.Bot.Graveyard.GetMatchingCardsCount((ClientCard card) => card.Id == 83301414);
			int SS_id = this.HolidayCheck(base.Card);
			if (matchingCardsCount > 0 && SS_id > 0)
			{
				base.AI.SelectCard(83301414);
				base.AI.SelectNextCard(SS_id);
				this.ActivatedCards.Add(64756282);
				return true;
			}
			if (base.Bot.HasInGraveyard(56894757) && base.Enemy.GetMonsterCount() == 0 && base.Duel.Phase == DuelPhase.Main1)
			{
				int total_attack = 0;
				foreach (ClientCard card2 in base.Bot.GetMonsters())
				{
					total_attack += card2.Attack;
				}
				if (total_attack >= base.Enemy.LifePoints)
				{
					int matchingCardsCount2 = base.Bot.MonsterZone.GetMatchingCardsCount((ClientCard card) => card.IsFaceup() && card.HasSetcode(this.Witchcraft_setcode));
					IList<ClientCard> enemy_cards = base.Enemy.GetSpells();
					if (matchingCardsCount2 >= enemy_cards.Count<ClientCard>())
					{
						base.AI.SelectCard(56894757);
						base.AI.SelectNextCard(enemy_cards);
						this.ActivatedCards.Add(64756282);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002233 RID: 8755 RVA: 0x000DFEF8 File Offset: 0x000DE0F8
		public bool CollaborationActivate()
		{
			if (base.Card.Location == CardLocation.Grave)
			{
				return false;
			}
			if (this.NegatedCheck(true))
			{
				return false;
			}
			ClientCard target = base.Util.GetBestBotMonster(true);
			if (base.Util.GetOneEnemyBetterThanMyBest(false, false) == null)
			{
				if (base.Enemy.SpellZone.GetFirstMatchingCard((ClientCard card) => card.IsFacedown()) != null || base.Enemy.MonsterZone.GetMatchingCardsCount((ClientCard card) => card.GetDefensePower() < target.Attack) >= 2)
				{
					base.AI.SelectCard(target);
					this.SelectSTPlace(null, true, null);
					this.ActivatedCards.Add(10805153);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002234 RID: 8756 RVA: 0x000DFFC8 File Offset: 0x000DE1C8
		public bool LightningStormActivate()
		{
			int bestPower = 0;
			foreach (ClientCard hand in base.Bot.Hand)
			{
				if (hand.IsMonster() && hand.Level <= 4 && hand.Attack > bestPower)
				{
					bestPower = hand.Attack;
				}
			}
			int opt = -1;
			if (base.Enemy.MonsterZone.GetFirstMatchingCard((ClientCard card) => card.IsFloodgate() && card.IsAttack()) != null || base.Enemy.MonsterZone.GetMatchingCardsCount((ClientCard card) => card.IsAttack() && card.Attack >= bestPower) >= 2)
			{
				opt = 0;
			}
			else if (base.Enemy.GetSpellCount() >= 2 || base.Util.GetProblematicEnemySpell() != null)
			{
				opt = 1;
			}
			if (opt == -1)
			{
				return false;
			}
			if (base.Enemy.MonsterZone.GetFirstMatchingCard((ClientCard card) => card.IsAttack()) == null || base.Enemy.GetSpellCount() == 0)
			{
				base.AI.SelectOption(0);
				this.SelectSTPlace(null, true, null);
				return true;
			}
			base.AI.SelectOption(opt);
			this.SelectSTPlace(null, true, null);
			return true;
		}

		// Token: 0x06002235 RID: 8757 RVA: 0x000E0130 File Offset: 0x000DE330
		public bool PotofExtravaganceActivate()
		{
			if (this.SpellNegatable(false, null))
			{
				return false;
			}
			this.SelectSTPlace(base.Card, true, null);
			base.AI.SelectOption(1);
			return true;
		}

		// Token: 0x06002236 RID: 8758 RVA: 0x000E015C File Offset: 0x000DE35C
		public bool DarkRulerNoMoreActivate()
		{
			if (this.SpellNegatable(false, null))
			{
				return false;
			}
			if (base.Enemy.MonsterZone.GetFirstMatchingCard((ClientCard card) => card.IsFloodgate() && !card.IsDisabled()) != null)
			{
				this.SelectSTPlace(null, true, null);
				return true;
			}
			return false;
		}

		// Token: 0x06002237 RID: 8759 RVA: 0x000E01B4 File Offset: 0x000DE3B4
		public bool CreationActivate()
		{
			if (base.Card.Location == CardLocation.Grave)
			{
				return false;
			}
			if (this.NegatedCheck(true))
			{
				return false;
			}
			int least_cost = (base.Bot.HasInMonstersZone(84523092, false, false, false) ? 1 : 0) + (base.Bot.HasInMonstersZone(21522601, false, false, false) ? 1 : 0);
			if (base.Bot.Hand.GetMatchingCardsCount((ClientCard card) => card != base.Card && card.IsSpell()) + this.CheckRecyclableCount(false, false) - 1 < least_cost)
			{
				return false;
			}
			if (!this.summoned || (base.Bot.MonsterZone.GetFirstMatchingCard((ClientCard card) => card.HasSetcode(this.Witchcraft_setcode)) == null && base.Bot.Hand.GetFirstMatchingCard((ClientCard card) => card.IsMonster() && card.HasSetcode(this.Witchcraft_setcode) && card.Level <= 4) == null))
			{
				base.AI.SelectCard(new int[] { 21744288, 95245544, 64756282, 59851535, 71074418 });
				this.SelectSTPlace(null, true, null);
				this.ActivatedCards.Add(57916305);
				return true;
			}
			if (base.Bot.HasInHand(71074418))
			{
				return false;
			}
			if (base.Bot.MonsterZone.GetFirstMatchingCard((ClientCard card) => card.IsFaceup() && card.HasSetcode(this.Witchcraft_setcode)) == null)
			{
				base.AI.SelectCard(new int[] { 71074418, 21744288, 95245544, 64756282, 59851535 });
				this.SelectSTPlace(null, true, null);
				this.ActivatedCards.Add(57916305);
				return true;
			}
			base.AI.SelectCard(new int[] { 21744288, 95245544, 64756282, 59851535, 71074418 });
			this.SelectSTPlace(null, true, null);
			this.ActivatedCards.Add(57916305);
			return true;
		}

		// Token: 0x06002238 RID: 8760 RVA: 0x000E0360 File Offset: 0x000DE560
		public int HolidayCheck(ClientCard except_card = null)
		{
			List<int> check_list = new List<int> { 84523092, 21522601, 71074418 };
			using (List<int>.Enumerator enumerator = check_list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int cardid2 = enumerator.Current;
					if (base.Bot.HasInGraveyard(cardid2) && base.Bot.MonsterZone.GetFirstMatchingCard((ClientCard card) => card.IsFaceup() && card.Id == cardid2) == null)
					{
						Logger.DebugWriteLine("*** Holiday check 1st: " + cardid2.ToString());
						return cardid2;
					}
				}
			}
			check_list.Clear();
			if (this.CheckProblematicCards(false, false) == null)
			{
				if (base.Bot.HasInGraveyard(71074418) && base.Bot.MonsterZone.GetFirstMatchingCard((ClientCard card) => card.IsFaceup() && card.HasSetcode(this.Witchcraft_setcode)) != null)
				{
					Logger.DebugWriteLine("*** Holiday check 2nd: GolemAruru");
					return 71074418;
				}
				check_list.Add(21744288);
				check_list.Add(95245544);
				check_list.Add(64756282);
				check_list.Add(59851535);
				using (List<int>.Enumerator enumerator = check_list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						int cardid3 = enumerator.Current;
						if (!this.UseSSEffect.Contains(cardid3) && base.Bot.Graveyard.GetFirstMatchingCard((ClientCard card) => card.Id == cardid3 && card != except_card) != null && this.CheckDiscardableSpellCount(base.Card) > 0)
						{
							Logger.DebugWriteLine("*** Holiday check 3rd: " + cardid3.ToString());
							return cardid3;
						}
					}
					return 0;
				}
			}
			check_list.Add(84523092);
			check_list.Add(21522601);
			check_list.Add(71074418);
			using (List<int>.Enumerator enumerator = check_list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int cardid = enumerator.Current;
					if (base.Bot.Graveyard.GetFirstMatchingCard((ClientCard card) => card.Id == cardid && card != except_card) != null)
					{
						return cardid;
					}
				}
			}
			return 0;
		}

		// Token: 0x06002239 RID: 8761 RVA: 0x000E0620 File Offset: 0x000DE820
		public bool HolidayActivate()
		{
			if (base.Card.Location == CardLocation.Grave)
			{
				return false;
			}
			if (this.NegatedCheck(true))
			{
				return false;
			}
			int target = this.HolidayCheck(null);
			if (target != 0)
			{
				base.AI.SelectCard(target);
				this.SelectSTPlace(null, true, null);
				this.ActivatedCards.Add(83301414);
				return true;
			}
			return false;
		}

		// Token: 0x0600223A RID: 8762 RVA: 0x000E067C File Offset: 0x000DE87C
		public bool CalledbytheGraveActivate()
		{
			if (this.NegatedCheck(true))
			{
				return false;
			}
			if (base.Duel.LastChainPlayer == 1)
			{
				if (base.Util.GetLastChainCard().IsMonster())
				{
					int code = base.Util.GetLastChainCard().GetOriginCode();
					if (code == 0)
					{
						return false;
					}
					if (this.CheckCalledbytheGrave(code) > 0)
					{
						return false;
					}
					ClientCard target = base.Enemy.Graveyard.GetFirstMatchingCard((ClientCard card) => card.IsMonster() && card.IsOriginalCode(code));
					if (target != null)
					{
						if (base.Card.Location != CardLocation.SpellZone)
						{
							this.SelectSTPlace(null, true, null);
						}
						base.AI.SelectCard(target);
						this.currentNegatingIdList.Add(code);
						return true;
					}
				}
				foreach (ClientCard cards in base.Enemy.Graveyard)
				{
					if (base.Duel.ChainTargets.Contains(cards))
					{
						int code4 = cards.GetOriginCode();
						base.AI.SelectCard(cards);
						this.currentNegatingIdList.Add(code4);
						return true;
					}
				}
				if (!base.Duel.ChainTargets.Contains(base.Card))
				{
					goto IL_01CC;
				}
				List<ClientCard> enemy_monsters = base.Enemy.Graveyard.GetMatchingCards((ClientCard card) => card.IsMonster()).ToList<ClientCard>();
				if (enemy_monsters.Count > 0)
				{
					enemy_monsters.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
					enemy_monsters.Reverse();
					int code2 = enemy_monsters[0].GetOriginCode();
					base.AI.SelectCard(enemy_monsters);
					this.currentNegatingIdList.Add(code2);
					return true;
				}
			}
			IL_01CC:
			if (base.Duel.LastChainPlayer == 1)
			{
				return false;
			}
			List<ClientCard> targets = this.CheckDangerousCardinEnemyGrave(true);
			if (targets.Count<ClientCard>() > 0)
			{
				int code3 = targets[0].GetOriginCode();
				if (base.Card.Location != CardLocation.SpellZone)
				{
					this.SelectSTPlace(null, true, null);
				}
				base.AI.SelectCard(targets);
				this.currentNegatingIdList.Add(code3);
				return true;
			}
			return false;
		}

		// Token: 0x0600223B RID: 8763 RVA: 0x000E08CC File Offset: 0x000DEACC
		public bool DrapingActivate()
		{
			if (base.Card.Location == CardLocation.Grave)
			{
				return false;
			}
			if (this.NegatedCheck(true))
			{
				return false;
			}
			IList<ClientCard> dangerours_spells = base.Enemy.SpellZone.GetMatchingCards((ClientCard card) => card.IsFloodgate() && !card.IsDisabled() && card.IsSpell());
			IList<ClientCard> dangerours_traps = base.Enemy.SpellZone.GetMatchingCards((ClientCard card) => card.IsFloodgate() && !card.IsDisabled() && card.IsTrap());
			List<ClientCard> faceup_spells = this.CardListShuffle(base.Enemy.SpellZone.GetMatchingCards((ClientCard card) => card.IsFaceup() && card.IsSpell()).ToList<ClientCard>());
			List<ClientCard> faceup_traps = this.CardListShuffle(base.Enemy.SpellZone.GetMatchingCards((ClientCard card) => card.IsFaceup() && card.IsTrap()).ToList<ClientCard>());
			List<ClientCard> setcards = this.CardListShuffle(base.Enemy.SpellZone.GetMatchingCards((ClientCard card) => card.IsFacedown()).ToList<ClientCard>());
			if (base.Duel.Player == 0 || base.Duel.Phase == DuelPhase.End)
			{
				IList<ClientCard> targets_ = dangerours_spells.Union(dangerours_traps).Union(faceup_spells).Union(faceup_traps)
					.Union(setcards)
					.ToList<ClientCard>();
				if (targets_.Count<ClientCard>() == 0)
				{
					return false;
				}
				base.AI.SelectCard(targets_);
				this.SelectSTPlace(null, true, null);
				this.ActivatedCards.Add(56894757);
				return true;
			}
			else
			{
				IList<ClientCard> targets_2 = dangerours_traps.Union(faceup_traps).ToList<ClientCard>();
				if (targets_2.Count<ClientCard>() == 0)
				{
					return false;
				}
				targets_2 = targets_2.Union(dangerours_spells).Union(faceup_spells).Union(setcards)
					.ToList<ClientCard>();
				base.AI.SelectCard(targets_2);
				this.SelectSTPlace(null, true, null);
				this.ActivatedCards.Add(56894757);
				return true;
			}
		}

		// Token: 0x0600223C RID: 8764 RVA: 0x000E0AD8 File Offset: 0x000DECD8
		public bool CrossoutDesignatorActivate()
		{
			if (this.NegatedCheck(true) || this.CheckLastChainNegated())
			{
				return false;
			}
			if (base.Duel.LastChainPlayer == 1 && base.Util.GetLastChainCard() != null)
			{
				int code = base.Util.GetLastChainCard().GetOriginCode();
				if (code == 0)
				{
					return false;
				}
				if (this.CheckCalledbytheGrave(code) > 0)
				{
					return false;
				}
				if (this.CheckRemainInDeck(code) > 0)
				{
					if (base.Card.Location != CardLocation.SpellZone)
					{
						this.SelectSTPlace(null, true, null);
					}
					base.AI.SelectAnnounceID(code);
					this.currentNegatingIdList.Add(code);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600223D RID: 8765 RVA: 0x000E0B74 File Offset: 0x000DED74
		public bool UnveilingActivate()
		{
			if (base.Card.Location == CardLocation.Grave)
			{
				return false;
			}
			if (this.NegatedCheck(true))
			{
				return false;
			}
			if (base.Bot.HasInHandOrInSpellZone(14532163))
			{
				if (base.Bot.SpellZone.GetMatchingCardsCount((ClientCard card) => card.IsFaceup()) + base.Bot.MonsterZone.GetMatchingCardsCount((ClientCard card) => card.IsFaceup()) == 0 && this.LightningStormActivate())
				{
					return false;
				}
			}
			if (base.Bot.HasInHand(this.important_witchcraft))
			{
				base.AI.SelectCard(this.important_witchcraft);
				this.SelectSTPlace(null, true, null);
				this.ActivatedCards.Add(70226289);
				return true;
			}
			return false;
		}

		// Token: 0x0600223E RID: 8766 RVA: 0x000E0C5C File Offset: 0x000DEE5C
		public bool ScrollActivate()
		{
			if (this.SpellNegatable(false, null) || base.Card.Location == CardLocation.Grave || base.Duel.Phase == DuelPhase.Main2)
			{
				return false;
			}
			if (base.Bot.MonsterZone.GetFirstMatchingCard((ClientCard card) => card.HasRace(CardRace.SpellCaster)) == null)
			{
				return false;
			}
			this.SelectSTPlace(null, true, null);
			return true;
		}

		// Token: 0x0600223F RID: 8767 RVA: 0x000E0CD4 File Offset: 0x000DEED4
		public bool MagiciansRestageActivate()
		{
			if (base.Card.Location == CardLocation.Grave)
			{
				if (base.Enemy.SpellZone.GetFirstMatchingCard((ClientCard card) => card.IsFacedown()) != null)
				{
					base.AI.SelectCard(new int[] { 13758665, 87769556 });
				}
				else
				{
					base.AI.SelectCard(new int[] { 87769556, 13758665 });
				}
				return true;
			}
			if (this.SpellNegatable(false, null))
			{
				return false;
			}
			if (this.CheckDiscardableSpellCount(base.Card) < 1)
			{
				return false;
			}
			int target = 0;
			foreach (int cardid in new int[] { 64756282, 95245544, 59851535 })
			{
				if (!this.UseSSEffect.Contains(cardid) && base.Bot.HasInGraveyard(cardid))
				{
					target = cardid;
					break;
				}
			}
			if (target == 0)
			{
				return false;
			}
			if (base.Card.Location == CardLocation.Hand)
			{
				this.SelectSTPlace(null, true, null);
				return true;
			}
			base.AI.SelectCard(target);
			return true;
		}

		// Token: 0x06002240 RID: 8768 RVA: 0x000E0DF8 File Offset: 0x000DEFF8
		public bool WitchcrafterBystreetActivate()
		{
			if (this.SpellNegatable(false, null) || base.Card.Location == CardLocation.Grave)
			{
				return false;
			}
			if (base.Bot.HasInSpellZone(83289866, true, false) || base.Bot.MonsterZone.GetFirstMatchingCard((ClientCard card) => card.HasSetcode(this.Witchcraft_setcode) && card.IsFaceup()) == null)
			{
				return false;
			}
			this.SelectSTPlace(null, true, null);
			return true;
		}

		// Token: 0x06002241 RID: 8769 RVA: 0x000E0E60 File Offset: 0x000DF060
		public bool InfiniteImpermanenceActivate()
		{
			if (this.SpellNegatable(false, null))
			{
				return false;
			}
			if (this.CheckLastChainNegated())
			{
				return false;
			}
			foreach (ClientCard i in base.Enemy.GetMonsters())
			{
				if (!i.IsDisabled() && base.Duel.LastChainPlayer != 0 && (i.IsMonsterShouldBeDisabledBeforeItUseEffect() || i.IsFloodgate() || (base.Duel.Phase > DuelPhase.Main1 && base.Duel.Phase < DuelPhase.Main2 && (i.IsMonsterDangerous() || i.IsMonsterInvincible() || (i.IsMonsterHasPreventActivationEffectInBattle() && base.Bot.HasInMonstersZone(21522601, false, false, false))))))
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
						this.SelectSTPlace(base.Card, true, null);
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
			if (LastChainCard == null || LastChainCard.Controller != 1 || LastChainCard.Location != CardLocation.MonsterZone || this.CheckLastChainNegated() || LastChainCard.IsShouldNotBeTarget() || LastChainCard.IsShouldNotBeSpellTrapTarget())
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
				this.SelectSTPlace(base.Card, true, null);
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

		// Token: 0x06002242 RID: 8770 RVA: 0x000E12A8 File Offset: 0x000DF4A8
		public bool MasterpieceActivate()
		{
			if (base.Card.Location != CardLocation.SpellZone)
			{
				if (base.Bot.HasInHandOrInSpellZone(14532163))
				{
					if (base.Bot.SpellZone.GetMatchingCardsCount((ClientCard card) => card.IsFaceup()) + base.Bot.MonsterZone.GetMatchingCardsCount((ClientCard card) => card.IsFaceup()) == 0 && this.LightningStormActivate())
					{
						return false;
					}
				}
				List<ClientCard> tobanish_spells = this.CardListShuffle(base.Bot.Graveyard.GetMatchingCards((ClientCard card) => card.IsSpell() && !card.HasSetcode(this.Witchcraft_setcode) && card.Id != 73594093).ToList<ClientCard>());
				if (base.Bot.HasInGraveyard(94553671))
				{
					tobanish_spells = this.CardListShuffle(base.Bot.Graveyard.GetMatchingCards((ClientCard card) => card.IsSpell() && card.HasSetcode(this.Witchcraft_setcode)).ToList<ClientCard>()).Union(tobanish_spells).ToList<ClientCard>();
				}
				int max_level = tobanish_spells.Count<ClientCard>();
				int discardable_hands = this.CheckDiscardableSpellCount(null);
				foreach (int cardid in new List<int> { 84523092, 21522601, 21744288, 95245544, 59851535, 64756282 })
				{
					if (base.Bot.HasInMonstersZone(cardid, false, false, false))
					{
						discardable_hands--;
					}
				}
				if (discardable_hands >= 1 && base.Duel.Player == 0)
				{
					foreach (int cardid2 in new int[] { 21744288, 95245544, 64756282, 59851535 })
					{
						int level = this.witchcraft_level[cardid2];
						if ((!this.UseSSEffect.Contains(cardid2) & (this.CheckRemainInDeck(cardid2) > 0)) && level <= max_level)
						{
							base.AI.SelectNumber(level);
							base.AI.SelectCard(tobanish_spells);
							base.AI.SelectNextCard(cardid2);
							return true;
						}
					}
				}
				List<int> ss_priority = new List<int>();
				if (base.Bot.HasInMonstersZone(84523092, false, false, false))
				{
					ss_priority.Add(21522601);
					ss_priority.Add(84523092);
				}
				else
				{
					ss_priority.Add(84523092);
					ss_priority.Add(21522601);
				}
				ss_priority.Add(71074418);
				foreach (int cardid3 in ss_priority)
				{
					int level2 = this.witchcraft_level[cardid3];
					if (this.CheckRemainInDeck(cardid3) > 0 && level2 <= max_level)
					{
						base.AI.SelectNumber(level2);
						base.AI.SelectCard(tobanish_spells);
						base.AI.SelectNextCard(cardid3);
						return true;
					}
				}
				return false;
			}
			if (this.NegatedCheck(true))
			{
				return false;
			}
			IList<ClientCard> target_ = base.Bot.Graveyard.GetMatchingCards((ClientCard card) => card.IsSpell() && this.CheckRemainInDeck(card.Id) > 0);
			IList<ClientCard> target_2 = base.Enemy.Graveyard.GetMatchingCards((ClientCard card) => card.IsSpell() && this.CheckRemainInDeck(card.Id) > 0);
			List<ClientCard> targets = this.CardListShuffle(target_.Union(target_2).ToList<ClientCard>());
			base.AI.SelectCard(targets);
			return true;
		}

		// Token: 0x06002243 RID: 8771 RVA: 0x000E1640 File Offset: 0x000DF840
		public bool PatronusActivate()
		{
			if (base.ActivateDescription == 94)
			{
				return true;
			}
			if (base.Card.Location == CardLocation.SpellZone)
			{
				if (this.NegatedCheck(true) || base.Duel.LastChainPlayer == 0)
				{
					return false;
				}
				int lack_spells = 0;
				foreach (int cardid2 in new int[] { 83289866, 83301414, 57916305, 56894757, 19673561, 70226289, 10805153 })
				{
					if (!base.Bot.HasInHandOrInSpellZone(cardid2) && !base.Bot.HasInGraveyard(cardid2))
					{
						lack_spells = cardid2;
						break;
					}
				}
				List<int> banish_checklist = new List<int> { 84523092, 21522601, 71074418, 21744288, 95245544 };
				if (lack_spells == 0)
				{
					List<int> new_list = new List<int> { 95245544, 64756282, 21744288, 59851535 };
					banish_checklist = banish_checklist.Union(new_list).ToList<int>();
				}
				else
				{
					List<int> new_list2 = new List<int> { 21744288, 95245544, 64756282, 59851535 };
					banish_checklist = banish_checklist.Union(new_list2).ToList<int>();
				}
				using (List<int>.Enumerator enumerator = banish_checklist.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						int cardid = enumerator.Current;
						ClientCard target = base.Bot.Banished.GetFirstMatchingCard((ClientCard card) => card.Id == cardid);
						if (target != null)
						{
							base.AI.SelectCard(target);
							base.AI.SelectNextCard(lack_spells);
							return true;
						}
					}
				}
			}
			if (base.Card.Location != CardLocation.Grave)
			{
				return false;
			}
			if (base.Bot.HasInHandOrInSpellZoneOrInGraveyard(55072170))
			{
				return false;
			}
			IList<ClientCard> targets = base.Bot.Banished.GetMatchingCards((ClientCard card) => card.IsSpell() && card.HasSetcode(this.Witchcraft_setcode));
			base.AI.SelectCard(targets);
			return true;
		}

		// Token: 0x06002244 RID: 8772 RVA: 0x000E1870 File Offset: 0x000DFA70
		public bool Lv8Summon()
		{
			if (base.Bot.HasInMonstersZone(38814750, false, false, false) && base.Bot.HasInMonstersZone(49036338, false, false, false))
			{
				List<int> targets = new List<int> { 49036338, 38814750 };
				base.AI.SelectMaterials(targets, 0);
				return true;
			}
			return false;
		}

		// Token: 0x06002245 RID: 8773 RVA: 0x000E18D3 File Offset: 0x000DFAD3
		public bool BorreloadSavageDragonSummon()
		{
			return base.Bot.Graveyard.GetFirstMatchingCard((ClientCard card) => card.HasType(CardType.Link)) != null && this.Lv8Summon();
		}

		// Token: 0x06002246 RID: 8774 RVA: 0x000E1910 File Offset: 0x000DFB10
		public static int BorreloadSavageDragonEquipCompare(ClientCard cardA, ClientCard cardB)
		{
			if (cardA.LinkCount > cardB.LinkCount)
			{
				return -1;
			}
			if (cardA.LinkCount < cardB.LinkCount)
			{
				return -1;
			}
			if (cardA.Attack > cardB.Attack)
			{
				return 1;
			}
			if (cardA.Attack < cardB.Attack)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06002247 RID: 8775 RVA: 0x000E1960 File Offset: 0x000DFB60
		public bool BorreloadSavageDragonActivate()
		{
			if (base.ActivateDescription == base.Util.GetStringId(27548199, 0))
			{
				List<ClientCard> links = base.Bot.Graveyard.GetMatchingCards((ClientCard card) => card.HasType(CardType.Link)).ToList<ClientCard>();
				links.Sort(new Comparison<ClientCard>(WitchcraftExecutor.BorreloadSavageDragonEquipCompare));
				base.AI.SelectCard(links);
				return true;
			}
			if (this.NegatedCheck(true) || base.Duel.LastChainPlayer != 1)
			{
				return false;
			}
			if (base.Util.GetLastChainCard().HasSetcode(286))
			{
				CardLocation location = base.Util.GetLastChainCard().Location;
				return false;
			}
			return false;
		}

		// Token: 0x06002248 RID: 8776 RVA: 0x000E1A24 File Offset: 0x000DFC24
		public bool DracoBerserkeroftheTenyiActivate()
		{
			Logger.DebugWriteLine("DracoBerserkeroftheTenyi's Effect: " + base.ActivateDescription.ToString());
			return true;
		}

		// Token: 0x06002249 RID: 8777 RVA: 0x000E1A50 File Offset: 0x000DFC50
		public bool PSYOmegaActivate()
		{
			if (base.Duel.Phase == DuelPhase.Standby)
			{
				if (base.Bot.Banished.Count == 0)
				{
					return false;
				}
				List<ClientCard> targets = this.CardListShuffle(base.Bot.Banished.GetMatchingCards((ClientCard card) => card.HasSetcode(this.Witchcraft_setcode)).ToList<ClientCard>());
				base.AI.SelectCard(targets);
				return true;
			}
			else
			{
				if (base.Card.Location == CardLocation.MonsterZone)
				{
					return base.Duel.Player == 1 || base.Bot.HasInMonstersZone(8802510, false, false, false) || base.Util.IsChainTarget(base.Card) || base.Util.IsAllEnemyBetterThanValue(base.Card.Attack, true);
				}
				if (base.Card.Location == CardLocation.Grave)
				{
					List<ClientCard> enemy_danger = this.CheckDangerousCardinEnemyGrave(false);
					if (enemy_danger.Count > 0)
					{
						base.AI.SelectCard(enemy_danger);
						return true;
					}
					if (!base.Bot.HasInHandOrInSpellZoneOrInGraveyard(83301414) && base.Bot.HasInGraveyard(this.important_witchcraft))
					{
						base.AI.SelectCard(this.important_witchcraft);
						return true;
					}
					if (this.CheckProblematicCards(false, false) == null)
					{
						base.AI.SelectCard(new int[] { 24224830, 65681983, 23434538, 14558127, 87769556, 13758665, 40252269, 94553671, 14532163, 58577036 });
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x0600224A RID: 8778 RVA: 0x000E1BA8 File Offset: 0x000DFDA8
		public bool TGWonderMagicianActivate()
		{
			if (base.Card.Location != CardLocation.MonsterZone)
			{
				return true;
			}
			Logger.DebugWriteLine("TGWonderMagician: " + base.ActivateDescription.ToString());
			IEnumerable<ClientCard> enumerable = base.Enemy.SpellZone.GetMatchingCards((ClientCard card) => card.IsFloodgate()).ToList<ClientCard>();
			List<ClientCard> faceup_cards = base.Enemy.SpellZone.GetMatchingCards((ClientCard card) => card.IsFaceup()).ToList<ClientCard>();
			List<ClientCard> facedown_cards = base.Enemy.SpellZone.GetMatchingCards((ClientCard card) => card.IsFacedown()).ToList<ClientCard>();
			List<ClientCard> result = enumerable.Union(faceup_cards).ToList<ClientCard>().Union(facedown_cards)
				.ToList<ClientCard>();
			base.AI.SelectCard(result);
			return true;
		}

		// Token: 0x0600224B RID: 8779 RVA: 0x000E1CA4 File Offset: 0x000DFEA4
		public List<ClientCard> BorrelswordDragonSummonCheck(ClientCard included = null)
		{
			List<ClientCard> empty_list = new List<ClientCard>();
			List<ClientCard> extra_list = new List<ClientCard>();
			if (included != null)
			{
				extra_list.Add(included);
			}
			List<ClientCard> materials = this.CheckLinkMaterials(4, 3, false, extra_list);
			if (materials.Count < 3)
			{
				return empty_list;
			}
			if (base.Util.GetOneEnemyBetterThanMyBest(false, false) != null)
			{
				return materials;
			}
			int total_attack = 0;
			foreach (ClientCard card in materials)
			{
				total_attack += card.Attack;
			}
			if (total_attack >= 3000)
			{
				return empty_list;
			}
			return materials;
		}

		// Token: 0x0600224C RID: 8780 RVA: 0x000E1D44 File Offset: 0x000DFF44
		public bool BorrelswordDragonSummon()
		{
			List<ClientCard> materials = this.BorrelswordDragonSummonCheck(null);
			if (materials.Count < 3)
			{
				return false;
			}
			base.AI.SelectMaterials(materials, 0);
			return true;
		}

		// Token: 0x0600224D RID: 8781 RVA: 0x000E1D74 File Offset: 0x000DFF74
		public bool BorrelswordDragonActivate()
		{
			if (base.ActivateDescription == -1)
			{
				ClientCard enemy_monster = base.Enemy.BattlingMonster;
				return enemy_monster == null || !enemy_monster.HasPosition(CardPosition.Attack) || base.Card.Attack - enemy_monster.Attack < base.Enemy.LifePoints;
			}
			ClientCard BestEnemy = base.Util.GetBestEnemyMonster(true, false);
			ClientCard WorstBot = base.Bot.GetMonsters().GetLowestAttackMonster(false);
			if (BestEnemy == null || BestEnemy.HasPosition(CardPosition.FaceDown))
			{
				return false;
			}
			if (WorstBot == null || WorstBot.HasPosition(CardPosition.FaceDown))
			{
				return false;
			}
			if (BestEnemy.Attack >= WorstBot.RealPower)
			{
				base.AI.SelectCard(BestEnemy);
				return true;
			}
			return false;
		}

		// Token: 0x0600224E RID: 8782 RVA: 0x000E1E20 File Offset: 0x000E0020
		public List<ClientCard> KnightmareUnicornSummonCheck(ClientCard included = null)
		{
			List<ClientCard> empty_list = new List<ClientCard>();
			List<ClientCard> extra_list = new List<ClientCard>();
			if (included != null)
			{
				extra_list.Add(included);
			}
			List<ClientCard> materials = this.CheckLinkMaterials(3, 2, false, extra_list);
			if (materials.Count < 2)
			{
				return empty_list;
			}
			if (this.CheckProblematicCards(true, true) != null)
			{
				if (base.Bot.Hand.GetMatchingCardsCount((ClientCard card) => card != base.Card) == 0)
				{
					return empty_list;
				}
				return materials;
			}
			else
			{
				int total_attack = 0;
				foreach (ClientCard card2 in materials)
				{
					total_attack += card2.Attack;
				}
				if (total_attack >= 2200)
				{
					return empty_list;
				}
				return materials;
			}
		}

		// Token: 0x0600224F RID: 8783 RVA: 0x000E1ED8 File Offset: 0x000E00D8
		public bool KnightmareUnicornSummon()
		{
			List<ClientCard> materials = this.KnightmareUnicornSummonCheck(null);
			if (materials.Count < 2)
			{
				return false;
			}
			base.AI.SelectMaterials(materials, 0);
			return true;
		}

		// Token: 0x06002250 RID: 8784 RVA: 0x000E1F08 File Offset: 0x000E0108
		public bool KnightmareUnicornActivate()
		{
			ClientCard card = this.CheckProblematicCards(true, false);
			if (card == null)
			{
				return false;
			}
			IList<ClientCard> enemy_list = new List<ClientCard>();
			if (!card.IsShouldNotBeMonsterTarget() && !card.IsShouldNotBeTarget())
			{
				enemy_list.Add(card);
			}
			foreach (ClientCard enemy in base.Enemy.GetMonstersInExtraZone())
			{
				if (enemy != null && !enemy_list.Contains(enemy) && !enemy.IsShouldNotBeMonsterTarget() && !enemy.IsShouldNotBeTarget())
				{
					enemy_list.Add(enemy);
				}
			}
			foreach (ClientCard enemy2 in base.Enemy.GetMonstersInMainZone())
			{
				if (enemy2 != null && !enemy_list.Contains(enemy2) && !enemy2.IsShouldNotBeMonsterTarget() && !enemy2.IsShouldNotBeTarget())
				{
					enemy_list.Add(enemy2);
				}
			}
			foreach (ClientCard enemy3 in base.Enemy.GetSpells())
			{
				if (enemy3 != null && !enemy_list.Contains(enemy3) && !enemy3.IsShouldNotBeMonsterTarget() && !enemy3.IsShouldNotBeTarget())
				{
					enemy_list.Add(enemy3);
				}
			}
			if (enemy_list.Count > 0)
			{
				this.SelectDiscardSpell();
				base.AI.SelectNextCard(enemy_list);
				return true;
			}
			return false;
		}

		// Token: 0x06002251 RID: 8785 RVA: 0x000E2098 File Offset: 0x000E0298
		public List<ClientCard> KnightmarePhoenixSummonCheck(ClientCard included = null)
		{
			List<ClientCard> empty_list = new List<ClientCard>();
			List<ClientCard> extra_list = new List<ClientCard>();
			if (included != null)
			{
				extra_list.Add(included);
			}
			List<ClientCard> materials = this.CheckLinkMaterials(2, 2, true, extra_list);
			if (materials.Count < 2)
			{
				return empty_list;
			}
			if (base.Util.GetProblematicEnemySpell() != null)
			{
				if (base.Bot.Hand.GetMatchingCardsCount((ClientCard card) => card != base.Card) == 0)
				{
					return empty_list;
				}
				return materials;
			}
			else
			{
				if (materials[0].Attack + materials[1].Attack >= 1900)
				{
					return empty_list;
				}
				return materials;
			}
		}

		// Token: 0x06002252 RID: 8786 RVA: 0x000E2124 File Offset: 0x000E0324
		public bool KnightmarePhoenixSummon()
		{
			List<ClientCard> materials = this.KnightmarePhoenixSummonCheck(null);
			if (materials.Count < 2)
			{
				return false;
			}
			base.AI.SelectMaterials(materials, 0);
			return true;
		}

		// Token: 0x06002253 RID: 8787 RVA: 0x000E2154 File Offset: 0x000E0354
		public bool KnightmarePhoenixActivate()
		{
			List<ClientCard> targets = new List<ClientCard>();
			targets.Add(base.Util.GetProblematicEnemySpell());
			List<ClientCard> spells = base.Enemy.GetSpells();
			List<ClientCard> faceups = new List<ClientCard>();
			List<ClientCard> facedowns = new List<ClientCard>();
			this.CardListShuffle(spells);
			foreach (ClientCard card in spells)
			{
				if (card.HasPosition(CardPosition.FaceUp) && !card.IsShouldNotBeTarget() && !card.IsShouldNotBeMonsterTarget())
				{
					faceups.Add(card);
				}
				else if (card.HasPosition(CardPosition.FaceDown))
				{
					facedowns.Add(card);
				}
			}
			targets = targets.Union(faceups).Union(facedowns).ToList<ClientCard>();
			if (targets.Count == 0)
			{
				return false;
			}
			this.SelectDiscardSpell();
			base.AI.SelectNextCard(targets);
			return true;
		}

		// Token: 0x06002254 RID: 8788 RVA: 0x000E2240 File Offset: 0x000E0440
		public List<ClientCard> CrystronHalqifibraxSummonCheck(ClientCard included = null)
		{
			List<ClientCard> empty_list = new List<ClientCard>();
			List<ClientCard> extra_list = new List<ClientCard>();
			if (included != null)
			{
				extra_list.Add(included);
			}
			if (this.CheckLinkMaterials(2, 2, true, extra_list).Count < 2)
			{
				return empty_list;
			}
			this.CheckRemainInDeck(new int[] { 38814750, 14558127 });
			return empty_list;
		}

		// Token: 0x06002255 RID: 8789 RVA: 0x000E2298 File Offset: 0x000E0498
		public bool CrystronHalqifibraxSummon()
		{
			List<ClientCard> materials = this.CrystronHalqifibraxSummonCheck(null);
			if (materials.Count < 2)
			{
				return false;
			}
			base.AI.SelectMaterials(materials, 0);
			return true;
		}

		// Token: 0x06002256 RID: 8790 RVA: 0x000E22C8 File Offset: 0x000E04C8
		public bool CrystronHalqifibraxActivate()
		{
			if (base.Duel.Player == 0)
			{
				return true;
			}
			if (base.Util.IsChainTarget(base.Card) || base.Util.GetProblematicEnemySpell() != null)
			{
				return true;
			}
			if (base.Duel.Player == 1 && base.Duel.Phase == DuelPhase.BattleStart && base.Util.IsOneEnemyBetterThanValue(1500, true))
			{
				if (base.Util.IsOneEnemyBetterThanValue(1900, true))
				{
					base.AI.SelectPosition(CardPosition.FaceUpDefence);
				}
				else
				{
					base.AI.SelectPosition(CardPosition.FaceUpAttack);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06002257 RID: 8791 RVA: 0x000E2368 File Offset: 0x000E0568
		public bool SalamangreatAlmirajSummonCheck(ClientCard included = null)
		{
			if (this.CheckDiscardableSpellCount(null) >= 2)
			{
				return false;
			}
			List<ClientCard> materials = base.Bot.GetMonsters();
			if (included != null)
			{
				materials.Add(included);
			}
			return materials.GetCardCount(95245544) + materials.GetCardCount(64756282) != 0 && base.Bot.HasInHand(this.important_witchcraft);
		}

		// Token: 0x06002258 RID: 8792 RVA: 0x000E23C8 File Offset: 0x000E05C8
		public bool SalamangreatAlmirajSummon()
		{
			if (!this.SalamangreatAlmirajSummonCheck(null))
			{
				return false;
			}
			List<int> material = new List<int> { 95245544, 64756282 };
			base.AI.SelectMaterials(material, 0);
			return true;
		}

		// Token: 0x06002259 RID: 8793 RVA: 0x000E240A File Offset: 0x000E060A
		public bool SalamangreatAlmirajActivate()
		{
			if (base.Card.Location == CardLocation.Grave)
			{
				return true;
			}
			if (base.Duel.Player == 1)
			{
				base.AI.SelectCard(base.Util.GetBestBotMonster(false));
				return true;
			}
			return false;
		}

		// Token: 0x0600225A RID: 8794 RVA: 0x000E2448 File Offset: 0x000E0648
		public bool PSYLambdaSummon()
		{
			if (base.Bot.HasInMonstersZone(38814750, false, false, false) && base.Bot.HasInMonstersZone(49036338, false, false, false) && (base.Bot.HasInHand(38814750) || base.Bot.HasInMonstersZone(74586817, false, false, false)))
			{
				List<int> targets = new List<int> { 49036338, 38814750 };
				base.AI.SelectMaterials(targets, 0);
				return true;
			}
			return false;
		}

		// Token: 0x0600225B RID: 8795 RVA: 0x000E24D4 File Offset: 0x000E06D4
		public int RelinquishedAnimaSummonCheck(ClientCard included = null)
		{
			if (this.CheckDiscardableSpellCount(null) >= 2)
			{
				return -1;
			}
			List<ClientCard> materials = base.Bot.GetMonsters();
			if (included != null)
			{
				materials.Add(included);
			}
			int place = -1;
			int attack = base.Util.GetBestAttack(base.Bot);
			List<ClientCard> checklist = new List<ClientCard>
			{
				base.Enemy.MonsterZone[6],
				base.Enemy.MonsterZone[5]
			};
			List<int> placelist = new List<int> { 1, 3 };
			for (int i = 0; i < 2; i++)
			{
				ClientCard card = checklist[i];
				int _place = placelist[i];
				if (card != null && card.HasLinkMarker(128) && card.Attack > attack && !card.IsShouldNotBeMonsterTarget() && !card.IsShouldNotBeTarget())
				{
					ClientCard self_card = base.Bot.MonsterZone[_place];
					if (self_card == null || self_card.Level == 1)
					{
						place = _place;
						attack = card.Attack;
					}
				}
			}
			checklist = new List<ClientCard>
			{
				base.Enemy.MonsterZone[3],
				base.Enemy.MonsterZone[1]
			};
			placelist = new List<int> { 5, 6 };
			for (int j = 0; j < 2; j++)
			{
				ClientCard card2 = checklist[j];
				int _place2 = placelist[j];
				if (card2 != null && card2.Attack > attack && !card2.IsShouldNotBeMonsterTarget() && !card2.IsShouldNotBeTarget() && base.Enemy.MonsterZone[11 - _place2] == null)
				{
					ClientCard self_card2 = base.Bot.MonsterZone[_place2];
					if (self_card2 == null || self_card2.Level == 1)
					{
						place = _place2;
						attack = card2.Attack;
					}
				}
			}
			return place;
		}

		// Token: 0x0600225C RID: 8796 RVA: 0x000E2698 File Offset: 0x000E0898
		public bool RelinquishedAnimaSummon()
		{
			int place = this.RelinquishedAnimaSummonCheck(null);
			Logger.DebugWriteLine("RelinquishedAnima summon check: " + place.ToString());
			if (place != -1)
			{
				int zone = (int)Math.Pow(2.0, (double)place);
				base.AI.SelectPlace(zone);
				if (base.Bot.MonsterZone[place] != null && base.Bot.MonsterZone[place].Level == 1)
				{
					base.AI.SelectMaterials(base.Bot.MonsterZone[place], 0);
				}
				else
				{
					base.AI.SelectMaterials(64756282, 0);
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600225D RID: 8797 RVA: 0x000E2738 File Offset: 0x000E0938
		public bool ChickenGame()
		{
			return !this.SpellNegatable(false, null) && base.Bot.LifePoints > 1000 && ((base.Bot.LifePoints - 1000 <= base.Enemy.LifePoints && base.ActivateDescription == base.Util.GetStringId(67616300, 0)) || (base.Bot.LifePoints - 1000 > base.Enemy.LifePoints && base.ActivateDescription == base.Util.GetStringId(67616300, 1)));
		}

		// Token: 0x0600225E RID: 8798 RVA: 0x000E27D8 File Offset: 0x000E09D8
		protected override bool DefaultSetForDiabellze()
		{
			if (base.DefaultSetForDiabellze())
			{
				this.SelectSTPlace(null, true, null);
				return true;
			}
			return false;
		}

		// Token: 0x04002469 RID: 9321
		private int Witchcraft_setcode = 296;

		// Token: 0x0400246A RID: 9322
		private int TimeLord_setcode = 74;

		// Token: 0x0400246B RID: 9323
		private int[] important_witchcraft = new int[] { 84523092, 21522601 };

		// Token: 0x0400246C RID: 9324
		private Dictionary<int, int> witchcraft_level = new Dictionary<int, int>
		{
			{ 71074418, 8 },
			{ 21522601, 7 },
			{ 84523092, 7 },
			{ 21744288, 4 },
			{ 95245544, 3 },
			{ 59851535, 2 },
			{ 64756282, 1 }
		};

		// Token: 0x0400246D RID: 9325
		private List<int> Impermanence_list = new List<int>();

		// Token: 0x0400246E RID: 9326
		private List<int> FirstCheckSS = new List<int>();

		// Token: 0x0400246F RID: 9327
		private List<int> UseSSEffect = new List<int>();

		// Token: 0x04002470 RID: 9328
		private List<int> ActivatedCards = new List<int>();

		// Token: 0x04002471 RID: 9329
		private List<int> currentNegatingIdList = new List<int>();

		// Token: 0x04002472 RID: 9330
		private bool MadameVerreGainedATK;

		// Token: 0x04002473 RID: 9331
		private bool summoned;

		// Token: 0x04002474 RID: 9332
		private bool enemy_activate_MaxxC;

		// Token: 0x04002475 RID: 9333
		private bool enemy_activate_DimensionShifter;

		// Token: 0x04002476 RID: 9334
		private bool MagiciansLeftHand_used;

		// Token: 0x04002477 RID: 9335
		private bool MagicianRightHand_used;

		// Token: 0x02000422 RID: 1058
		public class CardId
		{
			// Token: 0x04002478 RID: 9336
			public const int PSYDriver = 49036338;

			// Token: 0x04002479 RID: 9337
			public const int GolemAruru = 71074418;

			// Token: 0x0400247A RID: 9338
			public const int MadameVerre = 21522601;

			// Token: 0x0400247B RID: 9339
			public const int Haine = 84523092;

			// Token: 0x0400247C RID: 9340
			public const int Schmietta = 21744288;

			// Token: 0x0400247D RID: 9341
			public const int Pittore = 95245544;

			// Token: 0x0400247E RID: 9342
			public const int PSYGamma = 38814750;

			// Token: 0x0400247F RID: 9343
			public const int Potterie = 59851535;

			// Token: 0x04002480 RID: 9344
			public const int Genni = 64756282;

			// Token: 0x04002481 RID: 9345
			public const int Collaboration = 10805153;

			// Token: 0x04002482 RID: 9346
			public const int ThatGrassLooksGreener = 11110587;

			// Token: 0x04002483 RID: 9347
			public const int PotofExtravagance = 49238328;

			// Token: 0x04002484 RID: 9348
			public const int DarkRulerNoMore = 54693926;

			// Token: 0x04002485 RID: 9349
			public const int Creation = 57916305;

			// Token: 0x04002486 RID: 9350
			public const int Reasoning = 58577036;

			// Token: 0x04002487 RID: 9351
			public const int MetalfoesFusion = 73594093;

			// Token: 0x04002488 RID: 9352
			public const int Holiday = 83301414;

			// Token: 0x04002489 RID: 9353
			public const int Draping = 56894757;

			// Token: 0x0400248A RID: 9354
			public const int Unveiling = 70226289;

			// Token: 0x0400248B RID: 9355
			public const int MagiciansLeftHand = 13758665;

			// Token: 0x0400248C RID: 9356
			public const int Scroll = 19673561;

			// Token: 0x0400248D RID: 9357
			public const int MagiciansRestage = 40252269;

			// Token: 0x0400248E RID: 9358
			public const int WitchcrafterBystreet = 83289866;

			// Token: 0x0400248F RID: 9359
			public const int MagicianRightHand = 87769556;

			// Token: 0x04002490 RID: 9360
			public const int Masterpiece = 55072170;

			// Token: 0x04002491 RID: 9361
			public const int Patronus = 94553671;

			// Token: 0x04002492 RID: 9362
			public const int BorreloadSavageDragon = 27548199;

			// Token: 0x04002493 RID: 9363
			public const int DracoBerserkeroftheTenyi = 5041348;

			// Token: 0x04002494 RID: 9364
			public const int PSYOmega = 74586817;

			// Token: 0x04002495 RID: 9365
			public const int TGWonderMagician = 98558751;

			// Token: 0x04002496 RID: 9366
			public const int BorrelswordDragon = 85289965;

			// Token: 0x04002497 RID: 9367
			public const int KnightmareUnicorn = 38342335;

			// Token: 0x04002498 RID: 9368
			public const int KnightmarePhoenix = 2857636;

			// Token: 0x04002499 RID: 9369
			public const int PSYLambda = 8802510;

			// Token: 0x0400249A RID: 9370
			public const int CrystronHalqifibrax = 50588353;

			// Token: 0x0400249B RID: 9371
			public const int SalamangreatAlmiraj = 60303245;

			// Token: 0x0400249C RID: 9372
			public const int RelinquishedAnima = 94259633;

			// Token: 0x0400249D RID: 9373
			public const int NaturalExterio = 99916754;

			// Token: 0x0400249E RID: 9374
			public const int SwordsmanLV7 = 37267041;

			// Token: 0x0400249F RID: 9375
			public const int Anti_Spell = 58921041;

			// Token: 0x040024A0 RID: 9376
			public const int Numbe41BagooskatheTerriblyTiredTapir = 90590303;

			// Token: 0x040024A1 RID: 9377
			public const int PerformapalFive_RainbowMagician = 19619755;

			// Token: 0x040024A2 RID: 9378
			public const int DimensionShifter = 91800273;

			// Token: 0x040024A3 RID: 9379
			public const int MacroCosmos = 30241314;

			// Token: 0x040024A4 RID: 9380
			public const int DimensionalFissure = 81674782;

			// Token: 0x040024A5 RID: 9381
			public const int BanisheroftheRadiance = 94853057;

			// Token: 0x040024A6 RID: 9382
			public const int BanisheroftheLight = 61528025;
		}
	}
}
