using System;
using System.Collections.Generic;
using System.Linq;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x0200041D RID: 1053
	[Deck("Trickstar", "AI_Trickstar", "Normal")]
	public class TrickstarExecutor : DefaultExecutor
	{
		// Token: 0x060021B9 RID: 8633 RVA: 0x000D7838 File Offset: 0x000D5A38
		public int getLinkMarker(int id)
		{
			if (id == 31833038 || id == 74997493)
			{
				return 4;
			}
			if (id == 9753964 || id == 34408491 || id == 99916754 || id == 86221741 || id == 87460579)
			{
				return 5;
			}
			if (id == 38342335)
			{
				return 3;
			}
			if (id == 50588353 || id == 2857636 || id == 99111753 || id == 3987233)
			{
				return 2;
			}
			return 1;
		}

		// Token: 0x060021BA RID: 8634 RVA: 0x000D78B0 File Offset: 0x000D5AB0
		public TrickstarExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.Activate, 23434538, new Func<bool>(this.G_act));
			base.AddExecutor(ExecutorType.Activate, 40605147, new Func<bool>(base.DefaultSolemnStrike));
			base.AddExecutor(ExecutorType.Activate, 84749824, new Func<bool>(base.DefaultSolemnWarning));
			base.AddExecutor(ExecutorType.Activate, 10813327, new Func<bool>(this.Awaken_ss));
			base.AddExecutor(ExecutorType.Activate, 14558127, new Func<bool>(this.Hand_act_eff));
			base.AddExecutor(ExecutorType.Activate, 59438930, new Func<bool>(this.Hand_act_eff));
			base.AddExecutor(ExecutorType.Activate, 83555666, new Func<bool>(this.Ring_act));
			base.AddExecutor(ExecutorType.Activate, 9753964, new Func<bool>(this.Abyss_eff));
			base.AddExecutor(ExecutorType.Activate, 99916754, new Func<bool>(this.Exterio_counter));
			base.AddExecutor(ExecutorType.Activate, 87460579);
			base.AddExecutor(ExecutorType.Activate, 24224830, new Func<bool>(this.GraveCall_eff));
			base.AddExecutor(ExecutorType.Activate, 53129443, new Func<bool>(this.DarkHole_eff));
			base.AddExecutor(ExecutorType.Activate, new Func<bool>(this.field_activate));
			base.AddExecutor(ExecutorType.Activate, 35371948, new Func<bool>(this.Stage_Lock));
			base.AddExecutor(ExecutorType.Activate, 18144506, new Func<bool>(this.Feather_Act));
			base.AddExecutor(ExecutorType.Activate, 35371948, new Func<bool>(this.Stage_act));
			base.AddExecutor(ExecutorType.Activate, 5133471, new Func<bool>(this.GalaxyCyclone));
			base.AddExecutor(ExecutorType.Activate, 98558751, new Func<bool>(this.TG_eff));
			base.AddExecutor(ExecutorType.Activate, 67441435, new Func<bool>(this.Tuner_eff));
			base.AddExecutor(ExecutorType.SpellSet, new Func<bool>(this.Five_Rainbow));
			base.AddExecutor(ExecutorType.SpSummon, 31833038, new Func<bool>(this.Borrel_ss));
			base.AddExecutor(ExecutorType.SpSummon, 3987233, new Func<bool>(this.Missus_ss));
			base.AddExecutor(ExecutorType.SpSummon, 2857636, new Func<bool>(this.Phoneix_ss));
			base.AddExecutor(ExecutorType.SpSummon, 74997493, new Func<bool>(this.Snake_ss));
			base.AddExecutor(ExecutorType.SpSummon, 50588353, new Func<bool>(this.Crystal_ss));
			base.AddExecutor(ExecutorType.SpSummon, 99111753, new Func<bool>(this.Safedragon_ss));
			base.AddExecutor(ExecutorType.Activate, 99111753, new Func<bool>(base.DefaultCompulsoryEvacuationDevice));
			base.AddExecutor(ExecutorType.Activate, 41999284, new Func<bool>(this.Linkuri_eff));
			base.AddExecutor(ExecutorType.SpSummon, 41999284, new Func<bool>(this.Linkuri_ss));
			base.AddExecutor(ExecutorType.SpSummon, 38342335, new Func<bool>(this.Unicorn_ss));
			base.AddExecutor(ExecutorType.SpSummon, 98978921);
			base.AddExecutor(ExecutorType.Activate, 34408491);
			base.AddExecutor(ExecutorType.Activate, 3987233, new Func<bool>(this.Missus_eff));
			base.AddExecutor(ExecutorType.Activate, 50588353, new Func<bool>(this.Crystal_eff));
			base.AddExecutor(ExecutorType.Activate, 2857636, new Func<bool>(this.Phoneix_eff));
			base.AddExecutor(ExecutorType.Activate, 38342335, new Func<bool>(this.Unicorn_eff));
			base.AddExecutor(ExecutorType.Activate, 74997493, new Func<bool>(this.Snake_eff));
			base.AddExecutor(ExecutorType.Activate, 31833038, new Func<bool>(this.Borrel_eff));
			base.AddExecutor(ExecutorType.Activate, 73628505);
			base.AddExecutor(ExecutorType.SpSummon, 9929398, new Func<bool>(this.BF_pos));
			base.AddExecutor(ExecutorType.Activate, 9929398, new Func<bool>(this.BF_pos));
			base.AddExecutor(ExecutorType.Activate, 73915051, new Func<bool>(this.Sheep_Act));
			base.AddExecutor(ExecutorType.Activate, 63845230, new Func<bool>(this.Eater_eff));
			base.AddExecutor(ExecutorType.Activate, 94145021, new Func<bool>(this.LockBird_act));
			base.AddExecutor(ExecutorType.Activate, 98700941, new Func<bool>(this.Pink_eff));
			base.AddExecutor(ExecutorType.Activate, 21076084, new Func<bool>(this.Reincarnation));
			base.AddExecutor(ExecutorType.Activate, 35199656, new Func<bool>(this.Red_ss));
			base.AddExecutor(ExecutorType.Activate, 61283655, new Func<bool>(this.Yellow_eff));
			base.AddExecutor(ExecutorType.Activate, 98169343, new Func<bool>(this.White_eff));
			base.AddExecutor(ExecutorType.Activate, 22159429, new Func<bool>(this.Crown_eff));
			base.AddExecutor(ExecutorType.Summon, 61283655, new Func<bool>(this.Yellow_sum));
			base.AddExecutor(ExecutorType.Summon, 35199656, new Func<bool>(this.Red_sum));
			base.AddExecutor(ExecutorType.Summon, 98700941, new Func<bool>(this.Pink_sum));
			base.AddExecutor(ExecutorType.SpSummon, 63845230, new Func<bool>(this.Eater_ss));
			base.AddExecutor(ExecutorType.Summon, 67441435, new Func<bool>(this.Tuner_ns));
			base.AddExecutor(ExecutorType.Summon, 14558127, new Func<bool>(this.Tuner_ns));
			base.AddExecutor(ExecutorType.Summon, 59438930, new Func<bool>(this.Tuner_ns));
			base.AddExecutor(ExecutorType.Activate, 35261759, new Func<bool>(this.Pot_Act));
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(this.MonsterRepos));
			base.AddExecutor(ExecutorType.SummonOrSet, 35199656);
			base.AddExecutor(ExecutorType.SummonOrSet, 98700941);
			base.AddExecutor(ExecutorType.SpellSet, new Func<bool>(this.SpellSet));
		}

		// Token: 0x060021BB RID: 8635 RVA: 0x000D7F00 File Offset: 0x000D6100
		public bool Five_Rainbow()
		{
			if (!base.Enemy.HasInSpellZone(19619755, true, false) && !base.Bot.HasInSpellZone(19619755, true, false))
			{
				return false;
			}
			if (base.Card.HasType(CardType.Field))
			{
				return false;
			}
			bool has_setcard = false;
			for (int i = 0; i < 5; i++)
			{
				ClientCard sp = base.Bot.SpellZone[i];
				if (sp != null && sp.HasPosition(CardPosition.FaceDown))
				{
					has_setcard = true;
					break;
				}
			}
			if (has_setcard)
			{
				return false;
			}
			base.AI.SelectPlace(this.SelectSTPlace(null, false));
			return true;
		}

		// Token: 0x060021BC RID: 8636 RVA: 0x000D7F94 File Offset: 0x000D6194
		public int SelectSTPlace(ClientCard card = null, bool avoid_Impermanence = false)
		{
			if (card == null)
			{
				card = base.Card;
			}
			List<int> list = new List<int>();
			for (int seq = 0; seq < 5; seq++)
			{
				if (base.Bot.SpellZone[seq] == null && (card == null || card.Location != CardLocation.Hand || !avoid_Impermanence || !this.Impermanence_list.Contains(seq)))
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
							return (int)Math.Pow(2.0, (double)seq2);
						}
					}
				}
			}
			using (List<int>.Enumerator enumerator = list.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					int seq3 = enumerator.Current;
					return (int)Math.Pow(2.0, (double)seq3);
				}
			}
			return 0;
		}

		// Token: 0x060021BD RID: 8637 RVA: 0x000D8150 File Offset: 0x000D6350
		public bool SpellSet()
		{
			if (base.Card.IsCode(73915051) && base.Bot.HasInSpellZone(73915051, false, false))
			{
				return false;
			}
			if (base.DefaultSpellSet())
			{
				base.AI.SelectPlace(this.SelectSTPlace(null, false));
				return true;
			}
			if (base.Enemy.HasInSpellZone(58921041, true, false) || base.Bot.HasInSpellZone(58921041, true, false))
			{
				if (base.Card.IsCode(35371948))
				{
					return !base.Bot.HasInSpellZone(35371948, false, false);
				}
				if (base.Card.IsSpell())
				{
					base.AI.SelectPlace(this.SelectSTPlace(null, false));
					return true;
				}
			}
			return false;
		}

		// Token: 0x060021BE RID: 8638 RVA: 0x000D8215 File Offset: 0x000D6415
		public bool IsTrickstar(ClientCard card)
		{
			return card.HasSetcode(141);
		}

		// Token: 0x060021BF RID: 8639 RVA: 0x000D8224 File Offset: 0x000D6424
		public bool field_activate()
		{
			return base.Card.HasPosition(CardPosition.FaceDown) && base.Card.HasType(CardType.Field) && base.Card.Location == CardLocation.SpellZone && !base.Card.IsCode(new int[] { 71650854, 78082039 });
		}

		// Token: 0x060021C0 RID: 8640 RVA: 0x000D8288 File Offset: 0x000D6488
		public bool spell_trap_activate()
		{
			if (base.Card.Location != CardLocation.SpellZone && base.Card.Location != CardLocation.Hand)
			{
				return true;
			}
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			if (base.Enemy.HasInMonstersZone(99916754, true, false, false) && !base.Bot.HasInHandOrHasInMonstersZone(59438930))
			{
				return false;
			}
			if (base.Card.IsSpell())
			{
				return (!base.Enemy.HasInMonstersZone(33198837, true, false, false) || base.Bot.HasInHandOrHasInMonstersZone(59438930)) && !base.Enemy.HasInSpellZone(61740673, true, false) && !base.Bot.HasInSpellZone(61740673, true, false) && !base.Enemy.HasInMonstersZone(37267041, true, false, false) && !base.Bot.HasInMonstersZone(37267041, true, false, false);
			}
			return base.Card.IsTrap() && !base.Enemy.HasInSpellZone(51452091, true, false) && !base.Bot.HasInSpellZone(51452091, true, false);
		}

		// Token: 0x060021C1 RID: 8641 RVA: 0x000D83B7 File Offset: 0x000D65B7
		public int[] Useless_List()
		{
			return new int[]
			{
				67441435, 10813327, 22159429, 98700941, 35261759, 9929398, 98169343, 73628505, 5133471, 18144506,
				73915051, 21076084, 35199656, 61283655, 63845230, 23434538, 59438930, 14558127, 35371948, 83555666,
				84749824, 40605147
			};
		}

		// Token: 0x060021C2 RID: 8642 RVA: 0x000D83CC File Offset: 0x000D65CC
		public int GetTotalATK(IList<ClientCard> list)
		{
			int atk = 0;
			foreach (ClientCard c in list)
			{
				if (c != null)
				{
					atk += c.Attack;
				}
			}
			return atk;
		}

		// Token: 0x060021C3 RID: 8643 RVA: 0x000D841C File Offset: 0x000D661C
		public bool Awaken_ss()
		{
			bool judge = base.Bot.ExtraDeck.Count > 0;
			if (base.Enemy.GetMonstersExtraZoneCount() > 1)
			{
				judge = false;
			}
			if (base.Bot.GetMonstersExtraZoneCount() >= 1)
			{
				foreach (ClientCard card in base.Bot.GetMonstersInExtraZone())
				{
					if (this.getLinkMarker(card.Id) == 5)
					{
						judge = false;
					}
				}
			}
			if (judge)
			{
				bool fornextss = base.Util.ChainContainsCard(10813327);
				IEnumerable<ClientCard> extraDeck = base.Bot.ExtraDeck;
				ClientCard ex_best = null;
				foreach (ClientCard ex_card in extraDeck)
				{
					if (!fornextss)
					{
						if (base.Bot.HasInExtra(99916754))
						{
							bool has_skystriker = false;
							foreach (ClientCard card2 in base.Enemy.Graveyard)
							{
								if (card2 != null && card2.IsCode(this.SkyStrike_list))
								{
									has_skystriker = true;
									break;
								}
							}
							if (!has_skystriker)
							{
								foreach (ClientCard card3 in base.Enemy.GetSpells())
								{
									if (card3 != null && card3.IsCode(this.SkyStrike_list))
									{
										has_skystriker = true;
										break;
									}
								}
							}
							if (!has_skystriker)
							{
								foreach (ClientCard card4 in base.Enemy.GetSpells())
								{
									if (card4 != null && card4.IsCode(this.SkyStrike_list))
									{
										has_skystriker = true;
										break;
									}
								}
							}
							if (has_skystriker)
							{
								base.AI.SelectCard(99916754);
								return true;
							}
							if (ex_best == null || ex_card.Attack > ex_best.Attack)
							{
								ex_best = ex_card;
							}
						}
						else if (ex_best == null || ex_card.Attack > ex_best.Attack)
						{
							ex_best = ex_card;
						}
					}
					else if (this.getLinkMarker(ex_card.Id) != 5 && (ex_best == null || ex_card.Attack > ex_best.Attack))
					{
						ex_best = ex_card;
					}
				}
				if (ex_best != null)
				{
					base.AI.SelectCard(ex_best);
				}
			}
			IL_0260:
			if (!judge || base.Util.ChainContainsCard(10813327))
			{
				int[] secondselect = new int[] { 31833038, 86221741, 9753964, 87460579, 99916754, 59438930, 98169343, 35199656, 61283655, 98700941 };
				if (!base.Util.ChainContainsCard(10813327))
				{
					if (!judge && base.Bot.GetRemainingCount(59438930, 2) > 0)
					{
						base.AI.SelectCard(59438930);
						base.AI.SelectPosition(CardPosition.FaceUpDefence);
					}
					else
					{
						base.AI.SelectCard(secondselect);
					}
				}
				else
				{
					if (!judge)
					{
						base.AI.SelectCard(secondselect);
					}
					base.AI.SelectNextCard(secondselect);
					base.AI.SelectThirdCard(secondselect);
				}
			}
			return true;
		}

		// Token: 0x060021C4 RID: 8644 RVA: 0x000D87B8 File Offset: 0x000D69B8
		public bool Abyss_eff()
		{
			if (base.ActivateDescription == -1)
			{
				base.AI.SelectCard(new int[] { 59438930, 98558751, 67441435, 14558127, 9929398 });
				return true;
			}
			if (!base.Enemy.HasInMonstersZone(59438930, false, false, false) || base.Enemy.GetHandCount() <= 1)
			{
				ClientCard tosolve = base.Util.GetProblematicEnemyCard(0, false);
				if (tosolve == null && base.Duel.LastChainPlayer == 1 && base.Util.GetLastChainCard() != null)
				{
					ClientCard target = base.Util.GetLastChainCard();
					if (target.HasPosition(CardPosition.FaceUp) && (target.Location == CardLocation.MonsterZone || target.Location == CardLocation.SpellZone))
					{
						tosolve = target;
					}
				}
				if (tosolve != null)
				{
					base.AI.SelectCard(tosolve);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060021C5 RID: 8645 RVA: 0x000D8878 File Offset: 0x000D6A78
		public void RandomSort(List<ClientCard> list)
		{
			int i = list.Count;
			while (i-- > 1)
			{
				int index = Program.Rand.Next(i + 1);
				ClientCard temp = list[index];
				list[index] = list[i];
				list[i] = temp;
			}
		}

		// Token: 0x060021C6 RID: 8646 RVA: 0x000D88C4 File Offset: 0x000D6AC4
		public bool Stage_Lock()
		{
			if (base.Card.Location != CardLocation.SpellZone)
			{
				return false;
			}
			List<ClientCard> spells = base.Enemy.GetSpells();
			this.RandomSort(spells);
			if (spells.Count == 0)
			{
				return false;
			}
			foreach (ClientCard card in spells)
			{
				if (card.IsFacedown())
				{
					base.AI.SelectCard(card);
					this.stage_locked = card;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060021C7 RID: 8647 RVA: 0x000D895C File Offset: 0x000D6B5C
		public bool GalaxyCyclone()
		{
			if (!base.Bot.HasInSpellZone(35371948, false, false))
			{
				this.stage_locked = null;
			}
			List<ClientCard> spells = base.Enemy.GetSpells();
			if (spells.Count == 0)
			{
				return false;
			}
			ClientCard selected = null;
			if (base.Card.Location == CardLocation.Grave)
			{
				selected = base.Util.GetBestEnemySpell(true);
			}
			else
			{
				if (!this.spell_trap_activate())
				{
					return false;
				}
				foreach (ClientCard card in spells)
				{
					if (card.IsFacedown() && card != this.stage_locked)
					{
						selected = card;
						break;
					}
				}
			}
			if (selected == null)
			{
				return false;
			}
			base.AI.SelectCard(selected);
			base.AI.SelectPlace(this.SelectSTPlace(base.Card, true));
			return true;
		}

		// Token: 0x060021C8 RID: 8648 RVA: 0x000B3104 File Offset: 0x000B1304
		public bool BF_pos()
		{
			base.AI.SelectPosition(CardPosition.FaceUpDefence);
			return true;
		}

		// Token: 0x060021C9 RID: 8649 RVA: 0x000D8A40 File Offset: 0x000D6C40
		public bool Feather_Act()
		{
			if (!this.spell_trap_activate())
			{
				return false;
			}
			if (base.Util.GetProblematicEnemySpell() != null)
			{
				using (List<ClientCard>.Enumerator enumerator = base.Bot.GetGraveyardSpells().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.IsCode(5133471))
						{
							return false;
						}
					}
				}
				base.AI.SelectPlace(this.SelectSTPlace(base.Card, true));
				return true;
			}
			if (base.Enemy.GetSpellCount() <= 1)
			{
				return false;
			}
			base.AI.SelectPlace(this.SelectSTPlace(base.Card, true));
			return true;
		}

		// Token: 0x060021CA RID: 8650 RVA: 0x000D8AFC File Offset: 0x000D6CFC
		public bool Sheep_Act()
		{
			if (!this.spell_trap_activate())
			{
				return false;
			}
			if (base.Duel.Player == 0)
			{
				return false;
			}
			if (base.Duel.Phase == DuelPhase.End)
			{
				return true;
			}
			if (base.Duel.LastChainPlayer == 1 && (base.Util.IsChainTarget(base.Card) || (base.Util.GetLastChainCard().IsCode(18144506) && !base.Bot.HasInSpellZone(10813327, false, false))))
			{
				return true;
			}
			if (base.Duel.Phase > DuelPhase.Main1 && base.Duel.Phase < DuelPhase.Main2)
			{
				int total_atk = 0;
				foreach (ClientCard i in base.Enemy.GetMonsters())
				{
					if (i.IsAttack() && !i.Attacked)
					{
						total_atk += i.Attack;
					}
				}
				if (total_atk >= base.Bot.LifePoints)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060021CB RID: 8651 RVA: 0x000D8C14 File Offset: 0x000D6E14
		public bool Stage_act()
		{
			if (base.Card.Location == CardLocation.SpellZone && base.Card.HasPosition(CardPosition.FaceUp))
			{
				return false;
			}
			if (!this.spell_trap_activate())
			{
				return false;
			}
			if (!this.NormalSummoned)
			{
				if (!base.Bot.HasInHand(61283655))
				{
					base.AI.SelectCard(new int[] { 61283655, 98700941, 35199656, 98169343 });
					this.stage_locked = null;
					return true;
				}
				if (base.Enemy.LifePoints <= 1000 && base.Bot.GetRemainingCount(98700941, 1) > 0)
				{
					base.AI.SelectCard(new int[] { 98700941, 61283655, 35199656, 98169343 });
					this.stage_locked = null;
					return true;
				}
				if (base.Bot.HasInHand(61283655) && !base.Bot.HasInHand(35199656))
				{
					base.AI.SelectCard(new int[] { 35199656, 98700941, 61283655, 98169343 });
					this.stage_locked = null;
					return true;
				}
				if (base.Enemy.GetMonsterCount() > 0 && base.Util.GetBestEnemyMonster(false, false).Attack >= base.Util.GetBestAttack(base.Bot))
				{
					base.AI.SelectCard(new int[] { 98169343, 61283655, 98700941, 35199656 });
					this.stage_locked = null;
					return true;
				}
				if (!base.Bot.HasInSpellZone(35371948, false, false))
				{
					base.AI.SelectCard(new int[] { 61283655, 98700941, 35199656, 98169343 });
					this.stage_locked = null;
					return true;
				}
				return false;
			}
			else
			{
				if (!this.NormalSummoned)
				{
					this.stage_locked = null;
					return false;
				}
				if (base.Enemy.LifePoints <= 1000 && !this.pink_ss && base.Bot.GetRemainingCount(98700941, 1) > 0)
				{
					base.AI.SelectCard(new int[] { 98700941, 61283655, 35199656, 98169343 });
					this.stage_locked = null;
					return true;
				}
				if (base.Enemy.GetMonsterCount() > 0 && base.Util.GetBestEnemyMonster(false, false).Attack >= base.Util.GetBestAttack(base.Bot) && !base.Bot.HasInHand(98169343) && !base.DefaultCheckWhetherCardIdIsNegated(98169343))
				{
					base.AI.SelectCard(new int[] { 98169343, 61283655, 98700941, 35199656 });
					this.stage_locked = null;
					return true;
				}
				if (base.Bot.HasInMonstersZone(61283655, false, false, false) && !base.Bot.HasInHand(35199656))
				{
					base.AI.SelectCard(new int[] { 35199656, 98700941, 61283655, 98169343 });
					this.stage_locked = null;
					return true;
				}
				base.AI.SelectCard(new int[] { 61283655, 98700941, 35199656, 98169343 });
				this.stage_locked = null;
				return true;
			}
		}

		// Token: 0x060021CC RID: 8652 RVA: 0x000D8F00 File Offset: 0x000D7100
		public bool Pot_Act()
		{
			if (!this.spell_trap_activate())
			{
				return false;
			}
			if (base.Bot.Deck.Count > 15)
			{
				base.AI.SelectPlace(this.SelectSTPlace(base.Card, true));
				return true;
			}
			return false;
		}

		// Token: 0x060021CD RID: 8653 RVA: 0x000D8F3C File Offset: 0x000D713C
		public bool Hand_act_eff()
		{
			return !base.DefaultCheckWhetherCardIsNegated(base.Card) && (!base.Card.IsCode(14558127) || !base.Util.GetLastChainCard().HasSetcode(286) || base.Util.GetLastChainCard().Location != CardLocation.Hand) && (!base.Card.IsCode(14558127) || !base.Bot.HasInHand(94145021) || !base.Bot.HasInSpellZone(21076084, false, false)) && (!base.Card.IsCode(59438930) || base.Card.Location != CardLocation.Hand || !base.Bot.HasInMonstersZone(59438930, false, false, false)) && base.Duel.LastChainPlayer == 1;
		}

		// Token: 0x060021CE RID: 8654 RVA: 0x000D9016 File Offset: 0x000D7216
		public bool Exterio_counter()
		{
			if (base.Duel.LastChainPlayer == 1)
			{
				base.AI.SelectCard(this.Useless_List());
				return true;
			}
			return false;
		}

		// Token: 0x060021CF RID: 8655 RVA: 0x0004D320 File Offset: 0x0004B520
		public bool G_act()
		{
			return base.Duel.Player == 1 && !base.DefaultCheckWhetherCardIsNegated(base.Card);
		}

		// Token: 0x060021D0 RID: 8656 RVA: 0x000D903C File Offset: 0x000D723C
		public bool Pink_eff()
		{
			if (base.Card.Location == CardLocation.Hand)
			{
				if ((base.Enemy.LifePoints <= 1000 && base.Bot.HasInSpellZone(35371948, false, false)) || base.Enemy.LifePoints <= 800 || (!this.NormalSummoned && base.Bot.HasInGraveyard(35199656)))
				{
					this.pink_ss = true;
					return true;
				}
				if (base.Enemy.GetMonsterCount() > 0 && base.Util.GetBestEnemyMonster(false, false).Attack - 800 >= base.Bot.LifePoints)
				{
					return false;
				}
				this.pink_ss = true;
				return true;
			}
			else
			{
				if (base.Card.Location != CardLocation.Onfield)
				{
					return true;
				}
				if (!this.NormalSummoned && base.Bot.HasInGraveyard(61283655))
				{
					base.AI.SelectCard(new int[] { 61283655, 35199656, 98169343 });
					return true;
				}
				base.AI.SelectCard(new int[] { 35199656, 61283655, 98169343 });
				return true;
			}
		}

		// Token: 0x060021D1 RID: 8657 RVA: 0x000D915C File Offset: 0x000D735C
		public bool Eater_ss()
		{
			if (base.Util.GetProblematicEnemyMonster(0, false) == null && base.Bot.ExtraDeck.Count < 5)
			{
				return false;
			}
			if (base.Bot.GetMonstersInMainZone().Count >= 5)
			{
				return false;
			}
			if (base.Util.IsTurn1OrMain2())
			{
				return false;
			}
			base.AI.SelectPosition(CardPosition.FaceUpAttack);
			IList<ClientCard> targets = new List<ClientCard>();
			if (base.Bot.SpellZone[5] != null && !base.Bot.SpellZone[5].IsCode(35371948))
			{
				targets.Add(base.Bot.SpellZone[5]);
			}
			if (base.Bot.SpellZone[5] != null && base.Bot.SpellZone[5].IsCode(35371948) && base.Bot.HasInHand(35371948))
			{
				targets.Add(base.Bot.SpellZone[5]);
			}
			foreach (ClientCard e_c in base.Bot.ExtraDeck)
			{
				targets.Add(e_c);
				if (targets.Count >= 5)
				{
					base.AI.SelectMaterials(targets, 503);
					return true;
				}
			}
			Logger.DebugWriteLine("*** Eater use up the extra deck.");
			foreach (ClientCard s_c in base.Bot.GetSpells())
			{
				targets.Add(s_c);
				if (targets.Count >= 5)
				{
					base.AI.SelectMaterials(targets, 503);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060021D2 RID: 8658 RVA: 0x000D9324 File Offset: 0x000D7524
		public bool Eater_eff()
		{
			return !base.Enemy.BattlingMonster.HasPosition(CardPosition.Attack) || base.Bot.BattlingMonster.Attack - base.Enemy.BattlingMonster.GetDefensePower() < base.Enemy.LifePoints;
		}

		// Token: 0x060021D3 RID: 8659 RVA: 0x000D9378 File Offset: 0x000D7578
		public void Red_SelectPos(ClientCard return_card = null)
		{
			int self_power = ((base.Bot.HasInHand(98169343) && !this.white_eff_used) ? 3200 : 1600);
			if (base.Duel.Player == 0)
			{
				List<ClientCard> monsters = base.Bot.GetMonsters();
				monsters.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				monsters.Reverse();
				foreach (ClientCard card in monsters)
				{
					if (this.IsTrickstar(card) && card != return_card && card.HasPosition(CardPosition.Attack))
					{
						int this_power = ((base.Bot.HasInHand(98169343) && !this.white_eff_used) ? (card.RealPower + card.Attack) : card.RealPower);
						if (this_power >= self_power)
						{
							self_power = this_power;
						}
					}
					else if (card.RealPower >= self_power)
					{
						self_power = card.RealPower;
					}
				}
			}
			if (base.Util.GetOneEnemyBetterThanValue(self_power, true, false) != null)
			{
				base.AI.SelectPosition(CardPosition.FaceUpDefence);
				return;
			}
			base.AI.SelectPosition(CardPosition.FaceUpAttack);
		}

		// Token: 0x060021D4 RID: 8660 RVA: 0x000D94A0 File Offset: 0x000D76A0
		public bool Red_ss()
		{
			if ((base.Util.ChainContainsCard(53129443) || base.Util.ChainContainsCard(99330325) || base.Util.ChainContainsCard(53582587)) && base.Util.ChainContainsCard(35199656))
			{
				return false;
			}
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			if (base.Duel.LastChainPlayer == 0 && base.Util.GetLastChainCard().IsCode(35199656))
			{
				foreach (ClientCard i in base.Bot.GetMonsters())
				{
					if (base.Util.IsChainTarget(i) && this.IsTrickstar(i))
					{
						base.AI.SelectCard(i);
						this.Red_SelectPos(null);
						return true;
					}
				}
			}
			if (base.Duel.LastChainPlayer == 1)
			{
				return true;
			}
			if (base.Duel.Player == 0)
			{
				if (base.Util.IsTurn1OrMain2())
				{
					return false;
				}
				if (base.Duel.Phase <= DuelPhase.Main1 || base.Duel.Phase >= DuelPhase.Main2)
				{
					return false;
				}
				List<ClientCard> monsters = base.Bot.GetMonsters();
				ClientCard tosolve_enemy = base.Util.GetOneEnemyBetterThanMyBest(false, false);
				using (List<ClientCard>.Enumerator enumerator = monsters.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ClientCard c = enumerator.Current;
						if (this.IsTrickstar(c) && !c.IsCode(35199656))
						{
							if (c.Attacked)
							{
								base.AI.SelectCard(c);
								this.Red_SelectPos(c);
								return true;
							}
							if (c.IsCode(98700941))
							{
								return false;
							}
							if (tosolve_enemy != null)
							{
								if (base.Bot.HasInHand(98169343) && c.Attack + c.BaseAttack < tosolve_enemy.Attack)
								{
									if (tosolve_enemy.Attack > 3200)
									{
										base.AI.SelectPosition(CardPosition.FaceUpDefence);
									}
									base.AI.SelectCard(c);
									this.Red_SelectPos(c);
									return true;
								}
								if (!base.Bot.HasInHand(98169343) && tosolve_enemy.Attack <= 3200 && c.IsCode(98169343))
								{
									base.AI.SelectCard(c);
									this.Red_SelectPos(c);
									return true;
								}
								if (!base.Bot.HasInHand(98169343) && c.Attack < tosolve_enemy.Attack)
								{
									if (!c.Attacked)
									{
										ClientCard badatk = base.Enemy.GetMonsters().GetLowestAttackMonster(false);
										ClientCard baddef = base.Enemy.GetMonsters().GetLowestDefenseMonster(false);
										int enemy_power = 99999;
										if (badatk != null && badatk.Attack <= enemy_power)
										{
											enemy_power = badatk.Attack;
										}
										if (baddef != null && baddef.Defense <= enemy_power)
										{
											enemy_power = baddef.Defense;
										}
										if (c.Attack > enemy_power)
										{
											return false;
										}
									}
									if (tosolve_enemy.Attack > 1600)
									{
										base.AI.SelectPosition(CardPosition.FaceUpDefence);
									}
									base.AI.SelectCard(c);
									this.Red_SelectPos(c);
									return true;
								}
							}
						}
					}
					return false;
				}
			}
			if (base.Duel.Phase > DuelPhase.Main1 && base.Duel.Phase < DuelPhase.Main2 && base.Util.GetOneEnemyBetterThanMyBest(false, false) != null)
			{
				List<ClientCard> monsters2 = base.Bot.GetMonsters();
				monsters2.Sort(new Comparison<ClientCard>(CardContainer.CompareDefensePower));
				foreach (ClientCard card in monsters2)
				{
					if (this.IsTrickstar(card) && !card.IsCode(35199656))
					{
						base.AI.SelectCard(card);
						this.Red_SelectPos(card);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060021D5 RID: 8661 RVA: 0x000D9908 File Offset: 0x000D7B08
		public bool Yellow_eff()
		{
			if (!base.Bot.HasInHand(35371948) && !base.Bot.HasInSpellZone(35371948, false, false) && base.Bot.GetRemainingCount(35371948, 3) > 0)
			{
				base.AI.SelectCard(new int[] { 35371948, 35199656, 98169343, 98700941, 21076084, 22159429, 61283655 });
				return true;
			}
			if (base.Enemy.LifePoints <= 1000)
			{
				if (base.Bot.GetRemainingCount(98700941, 1) > 0 && !this.pink_ss)
				{
					base.AI.SelectCard(new int[] { 98700941, 35371948, 35199656, 98169343, 21076084, 22159429, 61283655 });
					return true;
				}
				if (base.Bot.HasInGraveyard(98700941) && base.Bot.GetRemainingCount(22159429, 1) > 0)
				{
					base.AI.SelectCard(new int[] { 22159429, 98700941, 21076084, 35371948, 35199656, 98169343, 61283655 });
					return true;
				}
			}
			if (base.Enemy.GetMonsterCount() == 0 && !base.Util.IsTurn1OrMain2())
			{
				if (base.Bot.HasInGraveyard(35199656) && base.Bot.GetRemainingCount(98700941, 1) > 0 && !this.pink_ss)
				{
					base.AI.SelectCard(new int[] { 98700941, 35199656, 98169343, 21076084, 35371948, 22159429, 61283655 });
				}
				else if (base.Bot.HasInGraveyard(98700941) && base.Bot.HasInGraveyard(35199656) && base.Bot.GetRemainingCount(83555666, 1) > 0)
				{
					base.AI.SelectCard(new int[] { 22159429, 35199656, 98169343, 21076084, 35371948, 98700941, 61283655 });
				}
				else if (base.Bot.GetRemainingCount(98169343, 2) > 0 && base.Enemy.LifePoints <= 4000)
				{
					base.AI.SelectCard(new int[] { 98169343, 35199656, 98700941, 21076084, 35371948, 22159429, 61283655 });
				}
				else if (base.Bot.HasInGraveyard(98169343) && base.Bot.GetRemainingCount(22159429, 1) > 0)
				{
					base.AI.SelectCard(new int[] { 22159429, 35199656, 98700941, 21076084, 35371948, 98169343, 61283655 });
				}
				else
				{
					base.AI.SelectCard(new int[] { 35199656, 98700941, 21076084, 22159429, 35371948, 98169343, 61283655 });
				}
				return true;
			}
			if (base.Util.GetProblematicEnemyMonster(0, false) != null)
			{
				int power = base.Util.GetProblematicEnemyMonster(0, false).GetDefensePower();
				if (power >= 1800 && power <= 3600 && base.Bot.GetRemainingCount(98169343, 2) > 0 && !base.Bot.HasInHand(98169343))
				{
					base.AI.SelectCard(new int[] { 98169343, 35199656, 98700941, 21076084, 35371948, 22159429, 61283655 });
				}
				else
				{
					base.AI.SelectCard(new int[] { 35199656, 98700941, 21076084, 22159429, 35371948, 98169343, 61283655 });
				}
				return true;
			}
			if ((base.Bot.HasInHand(35199656) || base.Bot.HasInHand(35371948) || base.Bot.HasInHand(61283655)) && base.Bot.GetRemainingCount(21076084, 1) > 0)
			{
				base.AI.SelectCard(new int[] { 21076084, 35199656, 98169343, 22159429, 98700941, 35371948, 61283655 });
				return true;
			}
			base.AI.SelectCard(new int[] { 35199656, 98700941, 21076084, 22159429, 35371948, 98169343, 61283655 });
			return true;
		}

		// Token: 0x060021D6 RID: 8662 RVA: 0x000D9C88 File Offset: 0x000D7E88
		public bool White_eff()
		{
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			if (base.Duel.Phase >= DuelPhase.Main2)
			{
				return false;
			}
			if (base.Duel.Phase > DuelPhase.Main1 && base.Duel.Phase < DuelPhase.Main2)
			{
				if (base.Bot.BattlingMonster == null || base.Enemy.BattlingMonster == null || !this.IsTrickstar(base.Bot.BattlingMonster) || base.Bot.BattlingMonster.HasPosition(CardPosition.Defence))
				{
					return false;
				}
				if (base.Bot.BattlingMonster.Attack <= base.Enemy.BattlingMonster.RealPower && base.Bot.BattlingMonster.Attack + base.Bot.BattlingMonster.BaseAttack >= base.Enemy.BattlingMonster.RealPower)
				{
					this.white_eff_used = true;
					return true;
				}
				return false;
			}
			else
			{
				if (base.Enemy.GetMonsterCount() == 0 && !base.Util.IsTurn1OrMain2())
				{
					this.white_eff_used = true;
					return true;
				}
				if (base.Enemy.GetMonsterCount() != 0)
				{
					ClientCard tosolve = base.Util.GetBestEnemyMonster(true, false);
					ClientCard self_card = base.Bot.GetMonsters().GetHighestAttackMonster(false);
					if (tosolve == null || self_card == null || (tosolve != null && self_card != null && !this.IsTrickstar(self_card)))
					{
						if (base.Enemy.GetMonsters().GetHighestAttackMonster(false) == null || base.Enemy.GetMonsters().GetHighestDefenseMonster(false) == null || base.Enemy.GetMonsters().GetHighestAttackMonster(false).GetDefensePower() < 2000 || base.Enemy.GetMonsters().GetHighestDefenseMonster(false).GetDefensePower() < 2000)
						{
							this.white_eff_used = true;
							return true;
						}
						return false;
					}
					else if (tosolve != null && self_card != null && this.IsTrickstar(self_card) && !tosolve.IsMonsterHasPreventActivationEffectInBattle())
					{
						int defender_power = tosolve.GetDefensePower();
						Logger.DebugWriteLine("battle check 0:" + base.Duel.Phase.ToString());
						Logger.DebugWriteLine("battle check 1:" + self_card.Attack.ToString());
						Logger.DebugWriteLine("battle check 2:" + (self_card.Attack + self_card.BaseAttack).ToString());
						Logger.DebugWriteLine("battle check 3:" + defender_power.ToString());
						if (self_card.Attack <= defender_power && self_card.Attack + self_card.BaseAttack >= defender_power)
						{
							return false;
						}
						if (defender_power <= 2000)
						{
							this.white_eff_used = true;
							return true;
						}
					}
				}
				return false;
			}
		}

		// Token: 0x060021D7 RID: 8663 RVA: 0x000D9F30 File Offset: 0x000D8130
		public bool LockBird_act()
		{
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			if (base.Duel.Player == 0 || this.lockbird_used)
			{
				return false;
			}
			this.lockbird_useful = true;
			if (base.Bot.HasInSpellZone(21076084, false, false))
			{
				if (base.Util.ChainContainsCard(21076084))
				{
					this.lockbird_used = true;
				}
				return base.Util.ChainContainsCard(21076084);
			}
			this.lockbird_used = true;
			return true;
		}

		// Token: 0x060021D8 RID: 8664 RVA: 0x000D9FB4 File Offset: 0x000D81B4
		public bool Reincarnation()
		{
			if (base.Card.Location == CardLocation.Grave)
			{
				return this.Ts_reborn();
			}
			if (!this.spell_trap_activate())
			{
				return false;
			}
			if (!base.Bot.HasInHand(94145021))
			{
				return true;
			}
			if (this.lockbird_useful || base.Util.IsChainTarget(base.Card) || (base.Duel.Player == 1 && base.Util.ChainContainsCard(18144506)))
			{
				this.lockbird_useful = false;
				return true;
			}
			return false;
		}

		// Token: 0x060021D9 RID: 8665 RVA: 0x000DA03C File Offset: 0x000D823C
		public bool Crown_eff()
		{
			if (base.Card.Location == CardLocation.Hand || (base.Card.Location == CardLocation.SpellZone && base.Card.HasPosition(CardPosition.FaceDown)))
			{
				if (!this.spell_trap_activate())
				{
					return false;
				}
				if (base.Duel.Phase <= DuelPhase.Main1 && this.Ts_reborn())
				{
					base.AI.SelectPlace(this.SelectSTPlace(base.Card, true));
					return true;
				}
				return false;
			}
			else
			{
				if (base.Bot.HasInHand(98700941) && base.DefaultCheckWhetherCardIdIsNegated(98700941))
				{
					base.AI.SelectCard(98700941);
					return true;
				}
				if (base.Enemy.GetMonsterCount() == 0)
				{
					foreach (ClientCard hand in base.Bot.Hand)
					{
						if (hand.IsMonster() && this.IsTrickstar(hand))
						{
							if (hand.Attack >= base.Enemy.LifePoints)
							{
								return true;
							}
							if (!hand.IsCode(61283655) && base.Util.GetOneEnemyBetterThanValue(hand.Attack, false, false) == null)
							{
								return true;
							}
						}
					}
					return false;
				}
				return false;
			}
		}

		// Token: 0x060021DA RID: 8666 RVA: 0x000DA180 File Offset: 0x000D8380
		public bool Ts_reborn()
		{
			if (base.Util.IsTurn1OrMain2())
			{
				return false;
			}
			if (base.Duel.Player == 0 && base.Enemy.LifePoints <= 1000)
			{
				base.AI.SelectCard(98700941);
				return true;
			}
			if (base.Duel.Player != 0 || !this.NormalSummoned)
			{
				base.AI.SelectCard(new int[] { 35199656, 98169343, 61283655, 98700941 });
				return true;
			}
			if (base.Duel.Phase < DuelPhase.Main2 && base.Bot.HasInGraveyard(98700941))
			{
				base.AI.SelectCard(98700941);
				return true;
			}
			base.AI.SelectCard(new int[] { 35199656, 98169343, 61283655, 98700941 });
			return true;
		}

		// Token: 0x060021DB RID: 8667 RVA: 0x000DA256 File Offset: 0x000D8456
		public bool Yellow_sum()
		{
			this.NormalSummoned = true;
			return true;
		}

		// Token: 0x060021DC RID: 8668 RVA: 0x000DA260 File Offset: 0x000D8460
		public bool Red_sum()
		{
			if ((base.Enemy.GetMonsterCount() == 0 && base.Enemy.LifePoints <= 1800) || (base.Duel.Turn == 1 && base.Bot.HasInHand(21076084)))
			{
				this.NormalSummoned = true;
				return true;
			}
			return false;
		}

		// Token: 0x060021DD RID: 8669 RVA: 0x000DA2B8 File Offset: 0x000D84B8
		public bool Pink_sum()
		{
			if (base.Enemy.LifePoints <= 1000)
			{
				this.NormalSummoned = true;
				return true;
			}
			if (!base.Util.IsTurn1OrMain2() && (base.Bot.HasInGraveyard(61283655) || base.Bot.HasInGraveyard(35199656)))
			{
				this.NormalSummoned = true;
				return true;
			}
			return false;
		}

		// Token: 0x060021DE RID: 8670 RVA: 0x000DA31B File Offset: 0x000D851B
		public bool Tuner_ns()
		{
			if ((base.Card.IsCode(67441435) && base.Bot.HasInExtra(50588353) && !this.tuner_eff_used) || this.Tuner_ss())
			{
				this.NormalSummoned = true;
				return true;
			}
			return false;
		}

		// Token: 0x060021DF RID: 8671 RVA: 0x000DA35C File Offset: 0x000D855C
		public bool Tuner_ss()
		{
			if (this.crystal_eff_used || base.Bot.HasInMonstersZone(50588353, false, false, false))
			{
				return false;
			}
			if (base.Bot.GetMonsterCount() == 0 || !base.Bot.HasInExtra(50588353))
			{
				return false;
			}
			if (base.Card.IsCode(59438930) && base.Bot.GetRemainingCount(59438930, 2) <= 0)
			{
				return false;
			}
			int count = 0;
			if (!base.Card.IsCode(14558127))
			{
				count++;
			}
			using (IEnumerator<ClientCard> enumerator = base.Bot.Hand.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsCode(base.Card.Id))
					{
						count++;
					}
				}
			}
			if (count < 2)
			{
				return false;
			}
			foreach (ClientCard i in base.Bot.GetMonsters())
			{
				if (!i.IsCode(63845230) && this.getLinkMarker(i.Id) <= 2)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060021E0 RID: 8672 RVA: 0x000DA4A8 File Offset: 0x000D86A8
		public bool Tuner_eff()
		{
			this.tuner_eff_used = true;
			base.AI.SelectPosition(CardPosition.FaceUpDefence);
			return true;
		}

		// Token: 0x060021E1 RID: 8673 RVA: 0x000DA4C0 File Offset: 0x000D86C0
		public bool Ring_act()
		{
			if (base.Duel.LastChainPlayer == 0 && base.Util.GetLastChainCard() != null && base.Util.GetLastChainCard().IsCode(59438930))
			{
				return false;
			}
			if (!this.spell_trap_activate())
			{
				return false;
			}
			ClientCard target = base.Util.GetProblematicEnemyMonster(0, false);
			if (target == null && base.Util.IsChainTarget(base.Card))
			{
				target = base.Util.GetBestEnemyMonster(false, false);
			}
			if (target == null)
			{
				return false;
			}
			if (base.Bot.LifePoints <= target.Attack)
			{
				return false;
			}
			base.AI.SelectCard(target);
			return true;
		}

		// Token: 0x060021E2 RID: 8674 RVA: 0x000DA564 File Offset: 0x000D8764
		public bool Linkuri_ss()
		{
			foreach (ClientCard c in base.Bot.GetMonsters())
			{
				if (!c.IsCode(new int[] { 63845230, 41999284, 98978921 }) && c.Level == 1)
				{
					base.AI.SelectCard(c);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060021E3 RID: 8675 RVA: 0x000DA5EC File Offset: 0x000D87EC
		public bool Linkuri_eff()
		{
			if (base.Duel.LastChainPlayer == 0 && base.Util.GetLastChainCard().IsCode(41999284))
			{
				return false;
			}
			base.AI.SelectCard(new int[] { 67441435, 9929399 });
			return true;
		}

		// Token: 0x060021E4 RID: 8676 RVA: 0x000DA644 File Offset: 0x000D8844
		public bool Crystal_ss()
		{
			if (this.crystal_eff_used)
			{
				return false;
			}
			if (base.Bot.HasInMonstersZone(9929398, false, false, false) && base.Bot.HasInMonstersZone(9929399, false, false, false))
			{
				base.AI.SelectCard(new int[] { 9929398, 9929399 });
				return true;
			}
			foreach (ClientCard extra_card in base.Bot.GetMonstersInExtraZone())
			{
				if (this.getLinkMarker(extra_card.Id) >= 5)
				{
					return false;
				}
			}
			IList<ClientCard> targets = new List<ClientCard>();
			foreach (ClientCard t_check in base.Bot.GetMonsters())
			{
				if (!t_check.IsFacedown() && t_check.IsCode(new int[] { 9929398, 67441435, 14558127, 59438930 }))
				{
					targets.Add(t_check);
					break;
				}
			}
			if (targets.Count == 0)
			{
				return false;
			}
			List<ClientCard> list = new List<ClientCard>(base.Bot.GetMonsters());
			list.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			foreach (ClientCard e_check in list)
			{
				if (!e_check.IsFacedown() && targets[0] != e_check && this.getLinkMarker(e_check.Id) <= 2 && !e_check.IsCode(new int[] { 63845230, 50588353 }))
				{
					targets.Add(e_check);
					break;
				}
			}
			if (targets.Count <= 1)
			{
				return false;
			}
			base.AI.SelectMaterials(targets, 0);
			return true;
		}

		// Token: 0x060021E5 RID: 8677 RVA: 0x000DA840 File Offset: 0x000D8A40
		public bool Crystal_eff()
		{
			if (base.Duel.Player == 0)
			{
				this.crystal_eff_used = true;
				base.AI.SelectCard(new int[] { 67441435, 59438930, 14558127 });
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

		// Token: 0x060021E6 RID: 8678 RVA: 0x000DA900 File Offset: 0x000D8B00
		public bool TG_eff()
		{
			if (base.Card.Location != CardLocation.MonsterZone)
			{
				return true;
			}
			ClientCard target = base.Util.GetProblematicEnemySpell();
			IList<ClientCard> list = new List<ClientCard>();
			if (target != null)
			{
				list.Add(target);
			}
			foreach (ClientCard spells in base.Enemy.GetSpells())
			{
				if (spells != null && !list.Contains(spells))
				{
					list.Add(spells);
				}
			}
			base.AI.SelectCard(list);
			return true;
		}

		// Token: 0x060021E7 RID: 8679 RVA: 0x000DA9A0 File Offset: 0x000D8BA0
		public bool Safedragon_ss()
		{
			if (base.Util.IsTurn1OrMain2())
			{
				return false;
			}
			ClientCard i = base.Util.GetProblematicEnemyMonster(0, false);
			foreach (ClientCard ex_m in base.Bot.GetMonstersInExtraZone())
			{
				if (this.getLinkMarker(ex_m.Id) >= 4)
				{
					return false;
				}
			}
			if ((i == null || i.HasPosition(CardPosition.FaceDown)) && base.Enemy.LifePoints <= 1100 && base.Enemy.GetMonsterCount() == 0 && base.Duel.Phase < DuelPhase.Battle)
			{
				IList<ClientCard> list = new List<ClientCard>();
				foreach (ClientCard monster in base.Bot.GetMonsters())
				{
					if (this.getLinkMarker(monster.Id) <= 2)
					{
						list.Add(monster);
					}
					if (list.Count == 2)
					{
						break;
					}
				}
				if (list.Count == 2 && this.GetTotalATK(list) <= 1100)
				{
					base.AI.SelectMaterials(list, 0);
					return true;
				}
				return false;
			}
			else
			{
				ClientCard ex_ = base.Bot.MonsterZone[5];
				ClientCard ex_2 = base.Bot.MonsterZone[6];
				ClientCard ex = null;
				if (ex_ != null && ex_.Controller == 0)
				{
					ex = ex_;
				}
				if (ex_2 != null && ex_2.Controller == 0)
				{
					ex = ex_2;
				}
				if (ex == null)
				{
					return false;
				}
				if (!ex.HasLinkMarker(2))
				{
					return false;
				}
				IList<ClientCard> targets = new List<ClientCard>();
				foreach (ClientCard s_m in base.Bot.GetMonsters())
				{
					if (!s_m.IsCode(63845230))
					{
						if (s_m != base.Bot.MonsterZone[5] && s_m != base.Bot.MonsterZone[6])
						{
							targets.Add(s_m);
						}
						if (targets.Count == 2)
						{
							break;
						}
					}
				}
				if (targets.Count == 2)
				{
					base.AI.SelectMaterials(targets, 0);
					return true;
				}
				return false;
			}
			bool flag;
			return flag;
		}

		// Token: 0x060021E8 RID: 8680 RVA: 0x000DAC00 File Offset: 0x000D8E00
		public bool Phoneix_ss()
		{
			if (base.Util.GetProblematicEnemySpell() == null)
			{
				if (base.Enemy.GetMonsterCount() == 0 && base.Enemy.LifePoints <= 1900 && base.Duel.Phase == DuelPhase.Main1)
				{
					IList<ClientCard> m_list = new List<ClientCard>();
					List<ClientCard> list = new List<ClientCard>(base.Bot.GetMonsters());
					list.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
					foreach (ClientCard monster in list)
					{
						if (this.getLinkMarker(monster.Id) == 1 && monster.IsFaceup())
						{
							m_list.Add(monster);
						}
						if (m_list.Count == 2)
						{
							break;
						}
					}
					if (m_list.Count == 2 && this.GetTotalATK(m_list) <= 1900)
					{
						base.AI.SelectMaterials(m_list, 0);
						return true;
					}
				}
				return false;
			}
			if (base.Bot.Hand.Count == 0)
			{
				return false;
			}
			IList<ClientCard> targets = new List<ClientCard>();
			List<ClientCard> list2 = new List<ClientCard>(base.Bot.GetMonstersInMainZone());
			list2.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			foreach (ClientCard s_m in list2)
			{
				if (!s_m.IsFacedown())
				{
					if ((!s_m.IsCode(63845230) || (s_m.IsCode(63845230) && s_m.IsDisabled())) && !targets.ContainsCardWithId(s_m.Id))
					{
						targets.Add(s_m);
					}
					if (targets.Count == 2)
					{
						break;
					}
				}
			}
			if (targets.Count < 2)
			{
				foreach (ClientCard s_m2 in base.Bot.GetMonstersInExtraZone())
				{
					if (!s_m2.IsFacedown())
					{
						if (!s_m2.IsCode(63845230) && !targets.ContainsCardWithId(s_m2.Id))
						{
							targets.Add(s_m2);
						}
						if (targets.Count == 2)
						{
							break;
						}
					}
				}
			}
			if (targets.Count < 2)
			{
				return false;
			}
			base.AI.SelectMaterials(targets, 0);
			return true;
		}

		// Token: 0x060021E9 RID: 8681 RVA: 0x000DAE64 File Offset: 0x000D9064
		public bool Phoneix_eff()
		{
			base.AI.SelectCard(this.Useless_List());
			ClientCard target = base.Util.GetProblematicEnemySpell();
			if (target != null)
			{
				base.AI.SelectNextCard(target);
			}
			else
			{
				List<ClientCard> spells = base.Enemy.GetSpells();
				this.RandomSort(spells);
				foreach (ClientCard card in spells)
				{
					if ((card != this.stage_locked || card.HasPosition(CardPosition.FaceUp)) && !card.IsShouldNotBeTarget() && !card.IsShouldNotBeMonsterTarget())
					{
						base.AI.SelectNextCard(card);
					}
				}
			}
			return true;
		}

		// Token: 0x060021EA RID: 8682 RVA: 0x000DAF1C File Offset: 0x000D911C
		public bool Unicorn_ss()
		{
			ClientCard i = base.Util.GetProblematicEnemyCard(0, false);
			int link_count = 0;
			if (i == null)
			{
				if (base.Enemy.GetMonsterCount() == 0 && base.Enemy.LifePoints <= 2200 && base.Duel.Phase == DuelPhase.Main1)
				{
					IList<ClientCard> m_list = new List<ClientCard>();
					List<ClientCard> list = new List<ClientCard>(base.Bot.GetMonsters());
					list.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
					foreach (ClientCard monster in list)
					{
						if (this.getLinkMarker(monster.Id) == 2)
						{
							link_count += 2;
							m_list.Add(monster);
						}
						else if (this.getLinkMarker(monster.Id) == 1 && monster.IsFaceup())
						{
							link_count++;
							m_list.Add(monster);
						}
						if (link_count >= 3)
						{
							break;
						}
					}
					if (link_count >= 3 && this.GetTotalATK(m_list) <= 2200)
					{
						base.AI.SelectMaterials(m_list, 0);
						return true;
					}
				}
				return false;
			}
			if (base.Bot.Hand.Count == 0)
			{
				return false;
			}
			IList<ClientCard> targets = new List<ClientCard>();
			List<ClientCard> monsters = base.Bot.GetMonsters();
			monsters.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			foreach (ClientCard s_m in monsters)
			{
				if ((!s_m.IsCode(63845230) || (s_m.IsCode(63845230) && i.IsMonsterHasPreventActivationEffectInBattle())) && this.getLinkMarker(s_m.Id) <= 2 && s_m.IsFaceup())
				{
					if (!targets.ContainsCardWithId(s_m.Id))
					{
						targets.Add(s_m);
						link_count += this.getLinkMarker(s_m.Id);
					}
					if (link_count >= 3)
					{
						break;
					}
				}
			}
			if (link_count < 3)
			{
				return false;
			}
			base.AI.SelectMaterials(targets, 0);
			return true;
		}

		// Token: 0x060021EB RID: 8683 RVA: 0x000DB130 File Offset: 0x000D9330
		public bool Unicorn_eff()
		{
			ClientCard i = base.Util.GetProblematicEnemyCard(0, false);
			if (i == null)
			{
				return false;
			}
			base.AI.SelectCard(this.Useless_List());
			IList<ClientCard> enemy_list = new List<ClientCard>();
			if (!i.IsShouldNotBeMonsterTarget() && !i.IsShouldNotBeTarget())
			{
				enemy_list.Add(i);
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
				base.AI.SelectNextCard(enemy_list);
				return true;
			}
			return false;
		}

		// Token: 0x060021EC RID: 8684 RVA: 0x000DB2D0 File Offset: 0x000D94D0
		public bool Snake_ss()
		{
			IList<ClientCard> targets = new List<ClientCard>();
			foreach (ClientCard e_m in base.Bot.GetMonstersInExtraZone())
			{
				if (e_m.Attack < 1900 && !targets.ContainsCardWithId(e_m.Id) && e_m.IsFaceup())
				{
					targets.Add(e_m);
				}
			}
			List<ClientCard> list = new List<ClientCard>(base.Bot.GetMonstersInMainZone());
			list.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			foreach (ClientCard i in list)
			{
				if (i.Attack < 1900 && !targets.ContainsCardWithId(i.Id) && i.IsFaceup())
				{
					targets.Add(i);
				}
				if (targets.Count >= 4)
				{
					if (base.Enemy.LifePoints <= this.GetTotalATK(targets) && base.Enemy.GetMonsterCount() == 0)
					{
						return false;
					}
					base.AI.SelectMaterials(targets, 0);
					base.AI.SelectYesNo(true);
					this.snake_four_s = true;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060021ED RID: 8685 RVA: 0x000DB430 File Offset: 0x000D9630
		public bool Snake_eff()
		{
			if (this.snake_four_s)
			{
				this.snake_four_s = false;
				base.AI.SelectCard(this.Useless_List());
				return true;
			}
			if (base.ActivateDescription == base.Util.GetStringId(74997493, 1))
			{
				foreach (ClientCard hand in base.Bot.Hand)
				{
					if (hand.IsCode(new int[] { 35199656, 98700941 }))
					{
						base.AI.SelectCard(hand);
						return true;
					}
					if (hand.IsCode(new int[] { 14558127, 59438930 }) && this.Tuner_ss())
					{
						base.AI.SelectCard(hand);
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060021EE RID: 8686 RVA: 0x000DB520 File Offset: 0x000D9720
		public bool Missus_ss()
		{
			IList<ClientCard> material_list = new List<ClientCard>();
			foreach (ClientCard monster in base.Bot.GetMonsters())
			{
				if (monster.HasAttribute(CardAttribute.Earth) && this.getLinkMarker(monster.Id) == 1)
				{
					material_list.Add(monster);
				}
				if (material_list.Count == 2)
				{
					break;
				}
			}
			if (material_list.Count < 2)
			{
				return false;
			}
			if (base.Enemy.GetMonsterCount() == 0 || base.Util.GetProblematicEnemyMonster(2000, false) == null)
			{
				base.AI.SelectMaterials(material_list, 0);
				return true;
			}
			if (base.Util.GetProblematicEnemyMonster(2000, false) != null && base.Bot.HasInExtra(31833038) && !base.Bot.HasInMonstersZone(3987233, false, false, false))
			{
				base.AI.SelectMaterials(material_list, 0);
				return true;
			}
			return false;
		}

		// Token: 0x060021EF RID: 8687 RVA: 0x000DB628 File Offset: 0x000D9828
		public bool Missus_eff()
		{
			base.AI.SelectCard(new int[] { 23434538, 3987233, 74997493 });
			return true;
		}

		// Token: 0x060021F0 RID: 8688 RVA: 0x000DB648 File Offset: 0x000D9848
		public bool Borrel_ss()
		{
			bool already_link2 = false;
			IList<ClientCard> material_list = new List<ClientCard>();
			if (base.Util.GetProblematicEnemyMonster(2000, false) == null)
			{
				Logger.DebugWriteLine("***borrel:null");
			}
			else
			{
				Logger.DebugWriteLine("***borrel:" + (base.Util.GetProblematicEnemyMonster(2000, false).Name ?? "unknown"));
			}
			if (base.Util.GetProblematicEnemyMonster(2000, false) != null || (base.Enemy.GetMonsterCount() == 0 && base.Duel.Phase == DuelPhase.Main1 && base.Enemy.LifePoints <= 3000))
			{
				foreach (ClientCard e_m in base.Bot.GetMonstersInExtraZone())
				{
					if (this.getLinkMarker(e_m.Id) < 3)
					{
						if (this.getLinkMarker(e_m.Id) == 2)
						{
							already_link2 = true;
						}
						material_list.Add(e_m);
					}
				}
				List<ClientCard> list = new List<ClientCard>(base.Bot.GetMonstersInMainZone());
				list.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				foreach (ClientCard i in list)
				{
					if (this.getLinkMarker(i.Id) < 3)
					{
						if (this.getLinkMarker(i.Id) == 2 && !already_link2)
						{
							already_link2 = true;
							material_list.Add(i);
						}
						else if (!i.IsCode(new int[] { 73915052, 63845230 }))
						{
							material_list.Add(i);
						}
						if (already_link2 && material_list.Count == 3)
						{
							break;
						}
						if (!already_link2 && material_list.Count == 4)
						{
							break;
						}
					}
				}
				if ((already_link2 && material_list.Count == 3) || (!already_link2 && material_list.Count == 4))
				{
					if (base.Enemy.GetMonsterCount() == 0 && base.Duel.Phase == DuelPhase.Main1 && base.Enemy.LifePoints <= 3000 && this.GetTotalATK(material_list) >= 3000)
					{
						return false;
					}
					base.AI.SelectMaterials(material_list, 0);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060021F1 RID: 8689 RVA: 0x000DB890 File Offset: 0x000D9A90
		public bool Borrel_eff()
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

		// Token: 0x060021F2 RID: 8690 RVA: 0x000DB93C File Offset: 0x000D9B3C
		public bool GraveCall_eff()
		{
			if (!this.spell_trap_activate())
			{
				return false;
			}
			if (base.Duel.LastChainPlayer == 1 && base.Util.GetLastChainCard().IsMonster())
			{
				int code = base.Util.GetLastChainCard().GetOriginCode();
				if (this.CheckWhetherNegated(code))
				{
					return false;
				}
				ClientCard target = base.Enemy.Graveyard.GetFirstMatchingCard((ClientCard c) => c.GetOriginCode() == code);
				if (target != null)
				{
					this.currentNegatingIdList.Add(code);
					base.AI.SelectCard(target);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060021F3 RID: 8691 RVA: 0x000DB9E0 File Offset: 0x000D9BE0
		public bool DarkHole_eff()
		{
			if (!this.spell_trap_activate())
			{
				return false;
			}
			if (base.Bot.GetMonsterCount() == 0)
			{
				int bestPower = -1;
				foreach (ClientCard hand in base.Bot.Hand)
				{
					if (hand.IsMonster() && hand.Attack > bestPower)
					{
						bestPower = hand.Attack;
					}
				}
				int bestenemy = -1;
				foreach (ClientCard enemy in base.Enemy.GetMonsters())
				{
					if (enemy.IsMonsterDangerous())
					{
						base.AI.SelectPlace(this.SelectSTPlace(base.Card, true));
						return true;
					}
					if (enemy.IsFaceup() && enemy.GetDefensePower() > bestenemy)
					{
						bestenemy = enemy.GetDefensePower();
					}
				}
				if (bestPower <= bestenemy)
				{
					base.AI.SelectPlace(this.SelectSTPlace(base.Card, true));
					return true;
				}
				return false;
			}
			return false;
		}

		// Token: 0x060021F4 RID: 8692 RVA: 0x000DBB08 File Offset: 0x000D9D08
		public bool IsAllEnemyBetter()
		{
			int bestPower = -1;
			for (int i = 0; i < 7; i++)
			{
				ClientCard card = base.Bot.MonsterZone[i];
				if (card != null && card.Data != null)
				{
					int newPower = card.Attack;
					if (this.IsTrickstar(card) && base.Bot.HasInHand(98169343) && !this.white_eff_used)
					{
						newPower += card.RealPower;
					}
					if (newPower > bestPower)
					{
						bestPower = newPower;
					}
				}
			}
			return base.Util.IsAllEnemyBetterThanValue(bestPower, true);
		}

		// Token: 0x060021F5 RID: 8693 RVA: 0x000DBB84 File Offset: 0x000D9D84
		public bool MonsterRepos()
		{
			if (base.Card.IsCode(63845230))
			{
				return !base.Card.HasPosition(CardPosition.Attack);
			}
			if (this.IsTrickstar(base.Card) && !this.white_eff_used && base.Bot.HasInHand(98169343) && base.Card.IsAttack() && base.Duel.Phase == DuelPhase.Main1)
			{
				return false;
			}
			if (base.Card.IsFaceup() && base.Card.IsDefense() && base.Card.Attack == 0)
			{
				return false;
			}
			if (base.Card.IsCode(98700941) && ((base.Bot.HasInSpellZone(35371948, true, false) && base.Enemy.LifePoints <= 1000) || (!base.Bot.HasInSpellZone(35371948, true, false) && base.Enemy.LifePoints <= 800)))
			{
				return !base.Card.HasPosition(CardPosition.Attack);
			}
			bool enemyBetter = this.IsAllEnemyBetter();
			return (base.Card.IsAttack() && enemyBetter) || (base.Card.IsDefense() && !enemyBetter && base.Card.Attack >= base.Card.Defense);
		}

		// Token: 0x060021F6 RID: 8694 RVA: 0x0000763C File Offset: 0x0000583C
		public override bool OnSelectHand()
		{
			return true;
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x000DBCD4 File Offset: 0x000D9ED4
		public override void OnNewTurn()
		{
			this.NormalSummoned = false;
			this.stage_locked = null;
			this.pink_ss = false;
			this.snake_four_s = false;
			this.crystal_eff_used = false;
			this.white_eff_used = false;
			this.lockbird_useful = false;
			this.lockbird_used = false;
			this.Impermanence_list.Clear();
			this.currentNegatingIdList.Clear();
			base.OnNewTurn();
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x000DBD38 File Offset: 0x000D9F38
		public override void OnChaining(int player, ClientCard card)
		{
			if (card == null)
			{
				return;
			}
			if (player == 1 && card.IsCode(10045474))
			{
				for (int i = 0; i < 5; i++)
				{
					if (base.Enemy.SpellZone[i] == card)
					{
						this.Impermanence_list.Add(4 - i);
						break;
					}
				}
			}
			base.OnChaining(player, card);
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x000DBD8E File Offset: 0x000D9F8E
		public override void OnChainEnd()
		{
			this.currentNegatingIdList.Clear();
			base.OnChainEnd();
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x000DBDA1 File Offset: 0x000D9FA1
		public bool CheckWhetherNegated(int cardId)
		{
			return !base.DefaultCheckWhetherCardIdIsNegated(cardId) && !this.currentNegatingIdList.Contains(cardId);
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x000DBDC0 File Offset: 0x000D9FC0
		public override BattlePhaseAction OnSelectAttackTarget(ClientCard attacker, IList<ClientCard> defenders)
		{
			ClientCard lowestattack = null;
			for (int i = defenders.Count - 1; i >= 0; i--)
			{
				ClientCard defender = defenders[i];
				if (defender.HasPosition(CardPosition.Attack) && !defender.IsMonsterDangerous() && (lowestattack == null || defender.Attack < lowestattack.Attack))
				{
					lowestattack = defender;
				}
			}
			if (lowestattack != null && attacker.Attack - lowestattack.Attack >= base.Enemy.LifePoints)
			{
				return base.AI.Attack(attacker, lowestattack);
			}
			for (int j = 0; j < defenders.Count; j++)
			{
				ClientCard defender2 = defenders[j];
				attacker.RealPower = attacker.Attack;
				defender2.RealPower = defender2.GetDefensePower();
				if (!defender2.IsMonsterHasPreventActivationEffectInBattle() && !attacker.IsDisabled())
				{
					if ((attacker.IsCode(63845230) && !defender2.HasType(CardType.Token)) || attacker.IsCode(31833038))
					{
						return base.AI.Attack(attacker, defender2);
					}
					if (attacker.IsCode(new int[] { 86221741, 87460579 }) && attacker.RealPower > defender2.RealPower)
					{
						return base.AI.Attack(attacker, defender2);
					}
				}
				if (this.OnPreBattleBetween(attacker, defender2) && (attacker.RealPower > defender2.RealPower || (attacker.RealPower >= defender2.RealPower && attacker.IsLastAttacker && defender2.IsAttack())))
				{
					return base.AI.Attack(attacker, defender2);
				}
			}
			if (attacker.CanDirectAttack)
			{
				return base.AI.Attack(attacker, null);
			}
			return null;
		}

		// Token: 0x060021FC RID: 8700 RVA: 0x000DBF58 File Offset: 0x000DA158
		public override ClientCard OnSelectAttacker(IList<ClientCard> attackers, IList<ClientCard> defenders)
		{
			for (int i = 0; i < attackers.Count; i++)
			{
				ClientCard attacker = attackers[i];
				if (attacker.IsCode(new int[] { 31833038, 63845230 }))
				{
					return attacker;
				}
			}
			return null;
		}

		// Token: 0x060021FD RID: 8701 RVA: 0x000DBFA0 File Offset: 0x000DA1A0
		public override bool OnPreBattleBetween(ClientCard attacker, ClientCard defender)
		{
			if (!defender.IsMonsterHasPreventActivationEffectInBattle() && this.IsTrickstar(attacker) && base.Bot.HasInHand(98169343) && !this.white_eff_used && !this.CheckWhetherNegated(98169343))
			{
				attacker.RealPower += attacker.Attack;
			}
			return base.OnPreBattleBetween(attacker, defender);
		}

		// Token: 0x060021FE RID: 8702 RVA: 0x000DC000 File Offset: 0x000DA200
		protected override bool DefaultSetForDiabellze()
		{
			if (base.DefaultSetForDiabellze())
			{
				base.AI.SelectPlace(this.SelectSTPlace(base.Card, true));
				return true;
			}
			return false;
		}

		// Token: 0x04002432 RID: 9266
		private List<int> Impermanence_list = new List<int>();

		// Token: 0x04002433 RID: 9267
		private bool NormalSummoned;

		// Token: 0x04002434 RID: 9268
		private ClientCard stage_locked;

		// Token: 0x04002435 RID: 9269
		private bool pink_ss;

		// Token: 0x04002436 RID: 9270
		private bool snake_four_s;

		// Token: 0x04002437 RID: 9271
		private bool tuner_eff_used;

		// Token: 0x04002438 RID: 9272
		private bool crystal_eff_used;

		// Token: 0x04002439 RID: 9273
		private bool white_eff_used;

		// Token: 0x0400243A RID: 9274
		private bool lockbird_useful;

		// Token: 0x0400243B RID: 9275
		private bool lockbird_used;

		// Token: 0x0400243C RID: 9276
		private List<int> currentNegatingIdList = new List<int>();

		// Token: 0x0400243D RID: 9277
		private List<int> SkyStrike_list = new List<int>
		{
			26077387, 8491308, 63288573, 90673288, 21623008, 25955749, 63166095, 99550630, 25733157, 51227866,
			52340444, 98338152, 24010609, 97616504, 50005218
		};

		// Token: 0x0200041E RID: 1054
		public class CardId
		{
			// Token: 0x0400243E RID: 9278
			public const int White = 98169343;

			// Token: 0x0400243F RID: 9279
			public const int BF = 9929398;

			// Token: 0x04002440 RID: 9280
			public const int Yellow = 61283655;

			// Token: 0x04002441 RID: 9281
			public const int Red = 35199656;

			// Token: 0x04002442 RID: 9282
			public const int Urara = 14558127;

			// Token: 0x04002443 RID: 9283
			public const int Ghost = 59438930;

			// Token: 0x04002444 RID: 9284
			public const int Pink = 98700941;

			// Token: 0x04002445 RID: 9285
			public const int MG = 23434538;

			// Token: 0x04002446 RID: 9286
			public const int Tuner = 67441435;

			// Token: 0x04002447 RID: 9287
			public const int Eater = 63845230;

			// Token: 0x04002448 RID: 9288
			public const int LockBird = 94145021;

			// Token: 0x04002449 RID: 9289
			public const int Feather = 18144506;

			// Token: 0x0400244A RID: 9290
			public const int Galaxy = 5133471;

			// Token: 0x0400244B RID: 9291
			public const int Pot = 35261759;

			// Token: 0x0400244C RID: 9292
			public const int Trans = 73628505;

			// Token: 0x0400244D RID: 9293
			public const int Sheep = 73915051;

			// Token: 0x0400244E RID: 9294
			public const int Crown = 22159429;

			// Token: 0x0400244F RID: 9295
			public const int Stage = 35371948;

			// Token: 0x04002450 RID: 9296
			public const int GraveCall = 24224830;

			// Token: 0x04002451 RID: 9297
			public const int DarkHole = 53129443;

			// Token: 0x04002452 RID: 9298
			public const int Re = 21076084;

			// Token: 0x04002453 RID: 9299
			public const int Ring = 83555666;

			// Token: 0x04002454 RID: 9300
			public const int Strike = 40605147;

			// Token: 0x04002455 RID: 9301
			public const int Warn = 84749824;

			// Token: 0x04002456 RID: 9302
			public const int Awaken = 10813327;

			// Token: 0x04002457 RID: 9303
			public const int Linkuri = 41999284;

			// Token: 0x04002458 RID: 9304
			public const int Linkspi = 98978921;

			// Token: 0x04002459 RID: 9305
			public const int SafeDra = 99111753;

			// Token: 0x0400245A RID: 9306
			public const int Crystal = 50588353;

			// Token: 0x0400245B RID: 9307
			public const int Phoneix = 2857636;

			// Token: 0x0400245C RID: 9308
			public const int Unicorn = 38342335;

			// Token: 0x0400245D RID: 9309
			public const int Snake = 74997493;

			// Token: 0x0400245E RID: 9310
			public const int Borrel = 31833038;

			// Token: 0x0400245F RID: 9311
			public const int TG = 98558751;

			// Token: 0x04002460 RID: 9312
			public const int Beelze = 34408491;

			// Token: 0x04002461 RID: 9313
			public const int Abyss = 9753964;

			// Token: 0x04002462 RID: 9314
			public const int Exterio = 99916754;

			// Token: 0x04002463 RID: 9315
			public const int Ultimate = 86221741;

			// Token: 0x04002464 RID: 9316
			public const int Cardian = 87460579;

			// Token: 0x04002465 RID: 9317
			public const int Missus = 3987233;
		}
	}
}
