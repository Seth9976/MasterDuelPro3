using System;
using System.Collections.Generic;
using System.Linq;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x020002B5 RID: 693
	[Deck("Altergeist", "AI_Altergeist", "Normal")]
	public class AltergeistExecutor : DefaultExecutor
	{
		// Token: 0x0600105D RID: 4189 RVA: 0x0004B67C File Offset: 0x0004987C
		public AltergeistExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.Activate, 67616300, new Func<bool>(this.ChickenGame));
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(this.EvenlyMatched_Repos));
			base.AddExecutor(ExecutorType.Activate, 23434538, new Func<bool>(this.G_activate));
			base.AddExecutor(ExecutorType.Activate, 58921041, new Func<bool>(this.Anti_Spell_activate));
			base.AddExecutor(ExecutorType.Activate, 49238328, new Func<bool>(this.PotofIndulgence_activate));
			base.AddExecutor(ExecutorType.Activate, new Func<bool>(this.field_activate));
			base.AddExecutor(ExecutorType.Activate, 68462976, new Func<bool>(this.SecretVillage_activate));
			base.AddExecutor(ExecutorType.Activate, 1508649, new Func<bool>(this.Hexstia_eff));
			base.AddExecutor(ExecutorType.Activate, 99916754, new Func<bool>(this.NaturalExterio_eff));
			base.AddExecutor(ExecutorType.Activate, 49725936, new Func<bool>(this.TripleBurstDragon_eff));
			base.AddExecutor(ExecutorType.Activate, 61740673, new Func<bool>(this.ImperialOrder_activate));
			base.AddExecutor(ExecutorType.Activate, 40605147, new Func<bool>(this.SolemnStrike_activate));
			base.AddExecutor(ExecutorType.Activate, 41420027, new Func<bool>(this.SolemnJudgment_activate));
			base.AddExecutor(ExecutorType.Activate, 27541563, new Func<bool>(this.Protocol_negate_better));
			base.AddExecutor(ExecutorType.Activate, 10045474, new Func<bool>(this.Impermanence_activate));
			base.AddExecutor(ExecutorType.Activate, 27541563, new Func<bool>(this.Protocol_negate));
			base.AddExecutor(ExecutorType.Activate, 27541563, new Func<bool>(this.Protocol_activate_not_use));
			base.AddExecutor(ExecutorType.Activate, 14558127, new Func<bool>(this.Hand_act_eff));
			base.AddExecutor(ExecutorType.Activate, 73642296, new Func<bool>(this.Hand_act_eff));
			base.AddExecutor(ExecutorType.Activate, 59438930, new Func<bool>(this.Hand_act_eff));
			base.AddExecutor(ExecutorType.Activate, 62015408, new Func<bool>(this.GR_WC_activate));
			base.AddExecutor(ExecutorType.Activate, 10813327, new Func<bool>(this.WakingtheDragon_eff));
			base.AddExecutor(ExecutorType.Activate, 15693423, new Func<bool>(this.EvenlyMatched_activate));
			base.AddExecutor(ExecutorType.Activate, 18144506, new Func<bool>(this.Feather_activate));
			base.AddExecutor(ExecutorType.Activate, 23924608, new Func<bool>(this.Storm_activate));
			base.AddExecutor(ExecutorType.Activate, 25533642, new Func<bool>(this.Meluseek_eff));
			base.AddExecutor(ExecutorType.Activate, 89538537, new Func<bool>(this.Silquitous_eff));
			base.AddExecutor(ExecutorType.Activate, 85289965, new Func<bool>(this.Borrelsword_eff));
			base.AddExecutor(ExecutorType.Activate, 42790071, new Func<bool>(this.Multifaker_handss));
			base.AddExecutor(ExecutorType.Activate, 35146019, new Func<bool>(this.Manifestation_eff));
			base.AddExecutor(ExecutorType.SpSummon, 94259633, new Func<bool>(this.Anima_ss));
			base.AddExecutor(ExecutorType.Activate, 94259633);
			base.AddExecutor(ExecutorType.Activate, 50588353, new Func<bool>(this.Needlefiber_eff));
			base.AddExecutor(ExecutorType.Activate, 53936268, new Func<bool>(this.Spoofing_eff));
			base.AddExecutor(ExecutorType.Activate, 52927340, new Func<bool>(this.Kunquery_eff));
			base.AddExecutor(ExecutorType.Activate, 42790071, new Func<bool>(this.Multifaker_deckss));
			base.AddExecutor(ExecutorType.SpSummon, 1508649, new Func<bool>(this.Hexstia_ss));
			base.AddExecutor(ExecutorType.SpSummon, 41999284, new Func<bool>(this.Linkuriboh_ss));
			base.AddExecutor(ExecutorType.Activate, 41999284, new Func<bool>(this.Linkuriboh_eff));
			base.AddExecutor(ExecutorType.Activate, 53143898, new Func<bool>(this.Marionetter_eff));
			base.AddExecutor(ExecutorType.Activate, 2295440, new Func<bool>(this.OneForOne_activate));
			base.AddExecutor(ExecutorType.Summon, 25533642, new Func<bool>(this.Meluseek_summon));
			base.AddExecutor(ExecutorType.Summon, 53143898, new Func<bool>(this.Marionetter_summon));
			base.AddExecutor(ExecutorType.Summon, 62015408, new Func<bool>(this.tuner_summon));
			base.AddExecutor(ExecutorType.SpSummon, 50588353, new Func<bool>(this.Needlefiber_ss));
			base.AddExecutor(ExecutorType.SpSummon, 85289965, new Func<bool>(this.Borrelsword_ss));
			base.AddExecutor(ExecutorType.SpSummon, 49725936, new Func<bool>(this.TripleBurstDragon_ss));
			base.AddExecutor(ExecutorType.Activate, 35261759, new Func<bool>(this.PotofDesires_activate));
			base.AddExecutor(ExecutorType.Summon, 89538537, new Func<bool>(this.Silquitous_summon));
			base.AddExecutor(ExecutorType.Summon, 42790071, new Func<bool>(this.Multifaker_summon));
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(this.MonsterRepos));
			base.AddExecutor(ExecutorType.Summon, new Func<bool>(this.MonsterSummon));
			base.AddExecutor(ExecutorType.MonsterSet, new Func<bool>(this.MonsterSet));
			base.AddExecutor(ExecutorType.SpellSet, new Func<bool>(this.SpellSet));
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x0004BF3C File Offset: 0x0004A13C
		public bool EvenlyMatched_ready()
		{
			return base.Bot.HasInHand(15693423) && base.Bot.GetSpellCount() == 0 && base.Duel.Phase < DuelPhase.Main2 && base.Enemy.GetFieldCount() >= 3 && base.Bot.HasInMonstersZone(10158145, false, false, false);
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x0004BFA0 File Offset: 0x0004A1A0
		public bool has_altergeist_left()
		{
			return base.Bot.GetRemainingCount(53143898, 3) > 0 || base.Bot.GetRemainingCount(42790071, 2) > 0 || base.Bot.GetRemainingCount(25533642, 3) > 0 || base.Bot.GetRemainingCount(89538537, 2) > 0 || base.Bot.GetRemainingCount(52927340, 1) > 0;
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x0004C013 File Offset: 0x0004A213
		public bool EvenlyMatched_Repos()
		{
			return this.EvenlyMatched_ready() && !base.Card.HasPosition(CardPosition.Attack);
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x0004C030 File Offset: 0x0004A230
		public bool isAltergeist(int id)
		{
			return id == 53143898 || id == 1508649 || id == 27541563 || id == 42790071 || id == 25533642 || id == 52927340 || id == 35146019 || id == 89538537;
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x0004C07F File Offset: 0x0004A27F
		public bool isAltergeist(ClientCard card)
		{
			return card != null && card.HasSetcode(259);
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x0004C094 File Offset: 0x0004A294
		public int GetSequence(ClientCard card)
		{
			if (base.Card.Location != CardLocation.MonsterZone)
			{
				return -1;
			}
			for (int i = 0; i < 7; i++)
			{
				if (base.Bot.MonsterZone[i] == card)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x0004C0D0 File Offset: 0x0004A2D0
		public bool trap_can_activate(int id)
		{
			return id != 10813327 && id != 15693423 && (id != 40605147 || base.Bot.LifePoints > 1500);
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x0004C104 File Offset: 0x0004A304
		public bool Should_counter()
		{
			if (base.Duel.CurrentChain.Count < 2)
			{
				return true;
			}
			if (!this.Protocol_activing())
			{
				return true;
			}
			ClientCard self_card = base.Duel.CurrentChain[base.Duel.CurrentChain.Count - 2];
			if (self_card == null || self_card.Controller != 0 || (self_card.Location != CardLocation.MonsterZone && self_card.Location != CardLocation.SpellZone) || !this.isAltergeist(self_card))
			{
				return true;
			}
			ClientCard enemy_card = base.Duel.CurrentChain[base.Duel.CurrentChain.Count - 1];
			return enemy_card == null || enemy_card.Controller != 1 || !enemy_card.IsCode(this.normal_counter);
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x0004C1CC File Offset: 0x0004A3CC
		public bool Should_activate_Protocol()
		{
			if (base.Duel.CurrentChain.Count < 2)
			{
				return false;
			}
			if (this.Protocol_activing())
			{
				return false;
			}
			ClientCard self_card = base.Duel.CurrentChain[base.Duel.CurrentChain.Count - 2];
			if (self_card == null || self_card.Controller != 0 || (self_card.Location != CardLocation.MonsterZone && self_card.Location != CardLocation.SpellZone) || !this.isAltergeist(self_card))
			{
				return false;
			}
			ClientCard enemy_card = base.Duel.CurrentChain[base.Duel.CurrentChain.Count - 1];
			return enemy_card != null && enemy_card.Controller == 1 && enemy_card.IsCode(this.normal_counter);
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x0004C294 File Offset: 0x0004A494
		public bool is_should_not_negate()
		{
			ClientCard last_card = base.Util.GetLastChainCard();
			return last_card != null && last_card.Controller == 1 && last_card.IsCode(this.should_not_negate);
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x0004C2CC File Offset: 0x0004A4CC
		public bool Multifaker_can_ss()
		{
			foreach (ClientCard sp in base.Bot.GetSpells())
			{
				if (sp.IsTrap() && sp.IsFacedown() && this.trap_can_activate(sp.Id))
				{
					return true;
				}
			}
			foreach (ClientCard h in base.Bot.Hand)
			{
				if (h.IsTrap() && this.trap_can_activate(h.Id))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x0004C39C File Offset: 0x0004A59C
		public bool Multifaker_candeckss()
		{
			return !this.Multifaker_ssfromdeck && !this.ss_other_monster;
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x0004C3B4 File Offset: 0x0004A5B4
		public bool Protocol_activing()
		{
			foreach (ClientCard card in base.Bot.GetSpells())
			{
				if (card.IsCode(27541563) && card.IsFaceup() && !card.IsDisabled() && !base.Duel.CurrentChain.Contains(card))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x0004C43C File Offset: 0x0004A63C
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

		// Token: 0x0600106C RID: 4204 RVA: 0x0004C48C File Offset: 0x0004A68C
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

		// Token: 0x0600106D RID: 4205 RVA: 0x0004C648 File Offset: 0x0004A848
		public int SelectSetPlace(List<int> avoid_list = null)
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
				if (base.Bot.SpellZone[seq] == null && (avoid_list == null || !avoid_list.Contains(seq)))
				{
					return zone;
				}
			}
			return 0;
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x0004C734 File Offset: 0x0004A934
		public bool spell_trap_activate(bool isCounter = false, ClientCard target = null)
		{
			if (target == null)
			{
				target = base.Card;
			}
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			if (target.Location != CardLocation.SpellZone && target.Location != CardLocation.Hand)
			{
				return true;
			}
			if (base.Enemy.HasInMonstersZone(99916754, true, false, false) && !base.Bot.HasInHandOrHasInMonstersZone(59438930) && !isCounter && !base.Bot.HasInSpellZone(40605147, false, false))
			{
				return false;
			}
			if (target.IsSpell())
			{
				return (!base.Enemy.HasInMonstersZone(33198837, true, false, false) || base.Bot.HasInHandOrHasInMonstersZone(59438930) || isCounter || base.Bot.HasInSpellZone(40605147, false, false)) && !base.Enemy.HasInSpellZone(61740673, true, false) && !base.Bot.HasInSpellZone(61740673, true, false) && !base.Enemy.HasInMonstersZone(37267041, true, false, false) && !base.Bot.HasInMonstersZone(37267041, true, false, false);
			}
			return target.IsTrap() && !base.Enemy.HasInSpellZone(51452091, true, false) && !base.Bot.HasInSpellZone(51452091, true, false);
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x0004C888 File Offset: 0x0004AA88
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

		// Token: 0x06001070 RID: 4208 RVA: 0x0004C8D2 File Offset: 0x0004AAD2
		public int get_Hexstia_linkzone(int zone)
		{
			if (zone >= 0 && zone < 4)
			{
				return zone + 1;
			}
			if (zone == 5)
			{
				return 1;
			}
			if (zone == 6)
			{
				return 3;
			}
			return -1;
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x0004C8F0 File Offset: 0x0004AAF0
		public bool get_linked_by_Hexstia(int place)
		{
			if (place == 0)
			{
				return false;
			}
			if (place == 2 || place == 4)
			{
				int last_place = place - 1;
				return base.Bot.MonsterZone[last_place] != null && base.Bot.MonsterZone[last_place].IsCode(1508649);
			}
			if (place == 1 || place == 3)
			{
				int last_place_;
				int last_place_2;
				if (place == 1)
				{
					last_place_ = 0;
					last_place_2 = 5;
				}
				else
				{
					last_place_ = 2;
					last_place_2 = 6;
				}
				return (base.Bot.MonsterZone[last_place_] != null && base.Bot.MonsterZone[last_place_].IsCode(1508649)) || (base.Bot.MonsterZone[last_place_2] != null && base.Bot.MonsterZone[last_place_2].IsCode(1508649));
			}
			return false;
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x0004C9A4 File Offset: 0x0004ABA4
		public ClientCard GetFloodgate_Alter(bool canBeTarget = false, bool is_bounce = true)
		{
			foreach (ClientCard card in base.Enemy.GetSpells())
			{
				if (card != null && card.IsFloodgate() && card.IsFaceup() && !card.IsCode(new int[] { 58921041, 61740673 }) && (!is_bounce || card.IsTrap()) && (!canBeTarget || !card.IsShouldNotBeTarget()))
				{
					return card;
				}
			}
			return null;
		}

		// Token: 0x06001073 RID: 4211 RVA: 0x0004CA44 File Offset: 0x0004AC44
		public ClientCard GetProblematicEnemyCard_Alter(bool canBeTarget = false, bool is_bounce = true)
		{
			ClientCard card = base.Enemy.MonsterZone.GetFloodgate(canBeTarget);
			if (card != null)
			{
				return card;
			}
			card = this.GetFloodgate_Alter(canBeTarget, is_bounce);
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
			if (card != null)
			{
				return card;
			}
			List<ClientCard> monsters = base.Enemy.GetMonsters();
			monsters.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			monsters.Reverse();
			foreach (ClientCard target in monsters)
			{
				if ((target.HasType(CardType.Fusion) || target.HasType(CardType.Ritual) || target.HasType(CardType.Synchro) || target.HasType(CardType.Xyz) || (target.HasType(CardType.Link) && target.LinkCount >= 2)) && !target.IsCode(new int[] { 63288573, 90673288 }) && (!canBeTarget || (!target.IsShouldNotBeTarget() && !target.IsShouldNotBeMonsterTarget())))
				{
					return target;
				}
			}
			return null;
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x0004CBB4 File Offset: 0x0004ADB4
		public ClientCard GetBestEnemyCard_random()
		{
			ClientCard card = base.Util.GetProblematicEnemyMonster(0, true);
			if (card != null)
			{
				return card;
			}
			if (base.Util.GetOneEnemyBetterThanMyBest(false, false) != null)
			{
				card = base.Enemy.MonsterZone.GetHighestAttackMonster(true);
				if (card != null)
				{
					return card;
				}
			}
			List<ClientCard> enemy_spells = base.Enemy.GetSpells();
			this.RandomSort(enemy_spells);
			foreach (ClientCard sp in enemy_spells)
			{
				if (sp.IsFaceup() && !sp.IsDisabled())
				{
					return sp;
				}
			}
			if (enemy_spells.Count > 0)
			{
				return enemy_spells[0];
			}
			List<ClientCard> monsters = base.Enemy.GetMonsters();
			if (monsters.Count > 0)
			{
				this.RandomSort(monsters);
				return monsters[0];
			}
			return null;
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x0004CC9C File Offset: 0x0004AE9C
		public bool bot_can_s_Meluseek()
		{
			if (base.Duel.Player != 0)
			{
				return false;
			}
			foreach (ClientCard card in base.Bot.GetMonsters())
			{
				if (card.IsCode(25533642) && !card.IsDisabled() && !card.Attacked)
				{
					return true;
				}
			}
			return base.Bot.HasInMonstersZone(25533642, false, false, false) || (base.Bot.HasInMonstersZone(53143898, false, false, false) && !this.Marionetter_reborn && base.Bot.HasInGraveyard(25533642)) || (!this.summoned && (base.Bot.HasInHand(25533642) || (base.Bot.HasInHand(53143898) && base.Bot.HasInGraveyard(25533642))));
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x0004CDAC File Offset: 0x0004AFAC
		public bool SpellSet()
		{
			if (base.Duel.Phase == DuelPhase.Main1 && base.Bot.HasAttackingMonster() && base.Duel.Turn > 1)
			{
				return false;
			}
			if (base.Card.IsCode(15693423) && !base.Bot.HasInHandOrInSpellZone(53936268) && !base.Bot.HasInHandOrInSpellZone(27541563) && !base.Bot.HasInHandOrInSpellZone(61740673))
			{
				return false;
			}
			if (base.Card.IsCode(15693423) && base.Bot.HasInSpellZone(15693423, false, false))
			{
				return false;
			}
			if (base.Card.IsCode(40605147) && base.Bot.LifePoints <= 1500)
			{
				return false;
			}
			if (base.Card.IsCode(53936268) && base.Bot.HasInSpellZone(53936268, false, false))
			{
				return false;
			}
			if (base.Card.IsCode(35146019) && base.Bot.HasInHandOrInSpellZone(53936268))
			{
				bool can_activate = false;
				foreach (ClientCard g in base.Bot.GetGraveyardMonsters())
				{
					if (g.IsMonster() && this.isAltergeist(g))
					{
						can_activate = true;
						break;
					}
				}
				Logger.DebugWriteLine("Manifestation_set: " + can_activate.ToString());
				if (!can_activate)
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
					base.AI.SelectPlace(this.SelectSTPlace(null, false));
					return true;
				}
				if (base.Card.IsCode(10045474))
				{
					base.AI.SelectPlace(Impermanence_set);
					return true;
				}
				base.AI.SelectPlace(this.SelectSetPlace(avoid_list));
				return true;
			}
			else
			{
				if ((base.Enemy.HasInSpellZone(58921041, true, false) || base.Bot.HasInSpellZone(58921041, true, false)) && base.Card.IsSpell() && (!base.Card.IsCode(2295440) || base.Bot.GetRemainingCount(25533642, 3) > 0))
				{
					base.AI.SelectPlace(this.SelectSTPlace(null, false));
					return true;
				}
				return false;
			}
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x0004D0A0 File Offset: 0x0004B2A0
		public bool field_activate()
		{
			return base.Card.HasPosition(CardPosition.FaceDown) && base.Card.HasType(CardType.Field) && base.Card.Location == CardLocation.SpellZone && !base.Card.IsCode(new int[] { 71650854, 78082039 });
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x0004D104 File Offset: 0x0004B304
		public bool ChickenGame()
		{
			Logger.DebugWriteLine("Use override");
			return this.spell_trap_activate(false, null) && base.Bot.LifePoints > 1000 && ((base.Bot.LifePoints - 1000 <= base.Enemy.LifePoints && base.ActivateDescription == base.Util.GetStringId(67616300, 0)) || (base.Bot.LifePoints - 1000 > base.Enemy.LifePoints && base.ActivateDescription == base.Util.GetStringId(67616300, 1)));
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x0004D1B0 File Offset: 0x0004B3B0
		public bool Anti_Spell_activate()
		{
			foreach (ClientCard card in base.Bot.GetSpells())
			{
				if (card.IsCode(58921041) && card.IsFaceup() && base.Duel.LastChainPlayer == 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x0004D22C File Offset: 0x0004B42C
		public bool SecretVillage_activate()
		{
			if (!this.spell_trap_activate(false, null))
			{
				return false;
			}
			if (base.Bot.SpellZone[5] != null && base.Bot.SpellZone[5].IsFaceup() && base.Bot.SpellZone[5].IsCode(68462976) && base.Bot.SpellZone[5].Disabled == 0)
			{
				return false;
			}
			if (this.Multifaker_can_ss() && base.Bot.HasInHand(42790071))
			{
				return true;
			}
			foreach (ClientCard card in base.Bot.GetMonsters())
			{
				if (card != null && card.IsFaceup() && (card.Race & 2) != 0 && !card.IsCode(25533642))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x0004D320 File Offset: 0x0004B520
		public bool G_activate()
		{
			return base.Duel.Player == 1 && !base.DefaultCheckWhetherCardIsNegated(base.Card);
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x0004D341 File Offset: 0x0004B541
		public bool NaturalExterio_eff()
		{
			if (base.Duel.LastChainPlayer != 0)
			{
				base.AI.SelectCard(new int[]
				{
					18144506, 35261759, 2295440, 59438930, 14558127, 62015408, 23434538, 53936268, 41420027, 40605147,
					61740673, 53936268, 23924608, 15693423, 10813327, 10045474, 53143898
				});
				return true;
			}
			return false;
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x0004D370 File Offset: 0x0004B570
		public bool SolemnStrike_activate()
		{
			return this.Should_counter() && base.DefaultSolemnStrike() && this.spell_trap_activate(true, null);
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x0004D390 File Offset: 0x0004B590
		public bool SolemnJudgment_activate()
		{
			if (base.Util.IsChainTargetOnly(base.Card) && (base.Bot.HasInHand(42790071) || this.Multifaker_candeckss()))
			{
				return false;
			}
			if (!this.Should_counter())
			{
				return false;
			}
			if (base.DefaultSolemnJudgment() && this.spell_trap_activate(true, null))
			{
				ClientCard target = base.Util.GetLastChainCard();
				return target == null || target.IsMonster() || this.spell_trap_activate(false, target);
			}
			return false;
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x0004D410 File Offset: 0x0004B610
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
			if (LastChainCard == null && (base.Duel.Player != 1 || base.Duel.Phase <= DuelPhase.Main2 || !base.Bot.HasInHand(42790071) || !this.Multifaker_candeckss() || this.Multifaker_ssfromhand))
			{
				return false;
			}
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
				if ((this_seq * that_seq >= 0 && this_seq + that_seq == 4) || (base.Util.IsChainTarget(base.Card) || (LastChainCard != null && LastChainCard.Controller == 1 && LastChainCard.IsCode(18144506))) || (base.Duel.Player == 1 && base.Duel.Phase > DuelPhase.Main2 && base.Bot.HasInHand(42790071) && this.Multifaker_candeckss() && !this.Multifaker_ssfromhand))
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
			if ((LastChainCard == null || LastChainCard.Controller != 1 || LastChainCard.Location != CardLocation.MonsterZone || LastChainCard.IsDisabled() || LastChainCard.IsShouldNotBeTarget() || LastChainCard.IsShouldNotBeSpellTrapTarget()) && (base.Duel.Player != 1 || base.Duel.Phase <= DuelPhase.Main2 || !base.Bot.HasInHand(42790071) || !this.Multifaker_candeckss() || this.Multifaker_ssfromhand))
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

		// Token: 0x06001080 RID: 4224 RVA: 0x0004D8CC File Offset: 0x0004BACC
		public bool Hand_act_eff()
		{
			return !base.DefaultCheckWhetherCardIsNegated(base.Card) && (!base.Card.IsCode(14558127) || !base.Util.GetLastChainCard().HasSetcode(286) || base.Util.GetLastChainCard().Location != CardLocation.Hand) && (!base.Card.IsCode(59438930) || base.Card.Location != CardLocation.Hand || !base.Bot.HasInMonstersZone(59438930, false, false, false)) && base.Duel.LastChainPlayer == 1;
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x0004D96C File Offset: 0x0004BB6C
		public bool WakingtheDragon_eff()
		{
			if (base.Bot.HasInExtra(99916754) && !this.Multifaker_ssfromdeck)
			{
				bool has_skystriker = false;
				foreach (ClientCard card in base.Enemy.Graveyard)
				{
					if (card != null && card.IsCode(this.SkyStrike_list))
					{
						has_skystriker = true;
						break;
					}
				}
				if (!has_skystriker)
				{
					foreach (ClientCard card2 in base.Enemy.GetSpells())
					{
						if (card2 != null && card2.IsCode(this.SkyStrike_list))
						{
							has_skystriker = true;
							break;
						}
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
				if (has_skystriker)
				{
					base.AI.SelectCard(99916754);
					this.ss_other_monster = true;
					return true;
				}
			}
			foreach (int id in ((IEnumerable<int>)new int[]
			{
				86221741, 85289965, 99916754, 5043010, 49725936, 24094258, 59934749, 1508649, 50588353, 42790071,
				52927340
			}))
			{
				if (base.Bot.HasInExtra(id))
				{
					if (!this.isAltergeist(id))
					{
						if (this.Multifaker_ssfromdeck)
						{
							continue;
						}
						this.ss_other_monster = true;
					}
					Logger.DebugWriteLine(id.ToString());
					base.AI.SelectCard(id);
					return true;
				}
			}
			return true;
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x0004DB54 File Offset: 0x0004BD54
		public bool GR_WC_activate()
		{
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			int warrior_count = 0;
			int pendulum_count = 0;
			int link_count = 0;
			int altergeis_count = 0;
			bool has_skystriker_acer = false;
			bool has_tuner = false;
			bool has_level_ = false;
			foreach (ClientCard card in base.Enemy.MonsterZone)
			{
				if (card != null)
				{
					if (card.IsCode(new int[] { 63288573, 90673288, 8491308, 26077387, 52340445 }))
					{
						has_skystriker_acer = true;
					}
					if (card.HasType(CardType.Pendulum))
					{
						pendulum_count++;
					}
					if ((card.Race & 1) != 0)
					{
						warrior_count++;
					}
					if (card.IsTuner() && base.Enemy.GetMonsterCount() >= 2)
					{
						has_tuner = true;
					}
					if (this.isAltergeist(card))
					{
						altergeis_count++;
					}
					if (!card.HasType(CardType.Link) && !card.HasType(CardType.Xyz) && card.Level == 1)
					{
						has_level_ = true;
					}
					link_count += (card.HasType(CardType.Link) ? card.LinkCount : 1);
				}
			}
			if (has_skystriker_acer)
			{
				if (!base.Enemy.HasInBanished(63288573) && base.Bot.HasInExtra(63288573))
				{
					base.AI.SelectCard(63288573);
					return true;
				}
				if (!base.Enemy.HasInBanished(90673288) && base.Bot.HasInExtra(90673288))
				{
					base.AI.SelectCard(90673288);
					return true;
				}
			}
			if (pendulum_count >= 2 && !base.Enemy.HasInMonstersZoneOrInGraveyard(24094258) && !base.Enemy.HasInBanished(24094258) && base.Bot.HasInExtra(24094258))
			{
				base.AI.SelectCard(24094258);
				return true;
			}
			if (warrior_count >= 2 && !base.Enemy.HasInMonstersZoneOrInGraveyard(59934749) && !base.Enemy.HasInBanished(59934749) && base.Bot.HasInExtra(59934749))
			{
				base.AI.SelectCard(59934749);
				return true;
			}
			if (has_tuner && !base.Enemy.HasInBanished(50588353) && base.Bot.HasInExtra(50588353) && !base.Enemy.HasInMonstersZone(50588353, false, false, false))
			{
				base.AI.SelectCard(50588353);
				return true;
			}
			if (has_level_ && !base.Enemy.HasInHandOrInMonstersZoneOrInGraveyard(41999284) && !base.Enemy.HasInBanished(41999284) && base.Bot.HasInExtra(41999284))
			{
				base.AI.SelectCard(41999284);
				return true;
			}
			if (altergeis_count > 0 && !base.Enemy.HasInBanished(1508649) && base.Bot.HasInExtra(1508649))
			{
				base.AI.SelectCard(1508649);
				return true;
			}
			if (link_count >= 4)
			{
				if ((base.Bot.HasInMonstersZone(86221741, false, false, false) || base.Bot.HasInMonstersZone(99916754, false, false, false)) && !base.Enemy.HasInMonstersZoneOrInGraveyard(85289965) && !base.Enemy.HasInBanished(85289965) && base.Bot.HasInExtra(85289965))
				{
					base.AI.SelectCard(85289965);
					return true;
				}
				if (!base.Enemy.HasInMonstersZoneOrInGraveyard(5043010) && !base.Enemy.HasInBanished(5043010) && base.Bot.HasInExtra(5043010))
				{
					base.AI.SelectCard(5043010);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x0004DF00 File Offset: 0x0004C100
		public bool ImperialOrder_activate()
		{
			if (!base.Card.HasPosition(CardPosition.FaceDown))
			{
				return true;
			}
			using (List<ClientCard>.Enumerator enumerator = base.Enemy.GetSpells().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsSpell() && this.spell_trap_activate(false, null))
					{
						return true;
					}
				}
			}
			return base.Duel.Player == 1 && base.Duel.Phase > DuelPhase.Main2 && base.Bot.HasInHand(42790071) && !this.Multifaker_ssfromhand && this.Multifaker_candeckss();
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x0004DFC0 File Offset: 0x0004C1C0
		public bool EvenlyMatched_activate()
		{
			return this.spell_trap_activate(false, null);
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x0004DFDC File Offset: 0x0004C1DC
		public bool Feather_activate()
		{
			if (!this.spell_trap_activate(false, null))
			{
				return false;
			}
			if (base.Util.GetProblematicEnemySpell() != null)
			{
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

		// Token: 0x06001086 RID: 4230 RVA: 0x0004E048 File Offset: 0x0004C248
		public bool Storm_activate()
		{
			if (!this.spell_trap_activate(false, null))
			{
				return false;
			}
			List<ClientCard> select_list = new List<ClientCard>();
			int activate_immediately = 0;
			List<ClientCard> spells = base.Enemy.GetSpells();
			this.RandomSort(spells);
			foreach (ClientCard card in spells)
			{
				if (card != null && card.IsFaceup() && (card.HasType(CardType.Equip) || card.HasType(CardType.Pendulum) || card.HasType(CardType.Field) || card.HasType(CardType.Continuous)))
				{
					select_list.Add(card);
					activate_immediately++;
				}
			}
			foreach (ClientCard card2 in spells)
			{
				if (card2 != null && card2.IsFacedown())
				{
					select_list.Add(card2);
				}
			}
			foreach (ClientCard card3 in spells)
			{
				if (card3 != null && card3.IsFaceup() && !select_list.Contains(card3))
				{
					select_list.Add(card3);
				}
			}
			if ((base.Duel.Phase == DuelPhase.End || activate_immediately >= 2 || base.Util.IsChainTarget(base.Card) || (base.Util.GetLastChainCard() != null && base.Util.GetLastChainCard().Controller == 1 && base.Util.GetLastChainCard().IsCode(18144506))) && select_list.Count > 0)
			{
				base.AI.SelectCard(select_list);
				return true;
			}
			return false;
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x0004E224 File Offset: 0x0004C424
		public bool Kunquery_eff()
		{
			if (base.Card.Location == CardLocation.Hand)
			{
				if (base.Duel.Phase > DuelPhase.Main1 && base.Duel.Phase < DuelPhase.Main2)
				{
					if (base.Util.ChainContainsCard(41999284))
					{
						return false;
					}
					if (base.Bot.BattlingMonster == null || base.Enemy.BattlingMonster.Attack >= base.Bot.BattlingMonster.GetDefensePower() || base.Enemy.BattlingMonster.IsMonsterDangerous())
					{
						base.AI.SelectPosition(CardPosition.FaceUpDefence);
						return true;
					}
				}
				return false;
			}
			ClientCard target = this.GetProblematicEnemyCard_Alter(true, false);
			if (target != null)
			{
				base.AI.SelectCard(target);
				return true;
			}
			List<ClientCard> spells = base.Enemy.GetSpells();
			this.RandomSort(spells);
			foreach (ClientCard card in spells)
			{
				if (card.IsFaceup() && !card.IsDisabled())
				{
					base.AI.SelectCard(card);
					return true;
				}
			}
			List<ClientCard> monsters = base.Enemy.GetMonsters();
			this.RandomSort(monsters);
			foreach (ClientCard card2 in monsters)
			{
				if (card2.IsFaceup() && !card2.IsDisabled() && !card2.IsShouldNotBeMonsterTarget() && !card2.IsShouldNotBeTarget())
				{
					base.AI.SelectCard(card2);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x0004E3DC File Offset: 0x0004C5DC
		public bool Marionetter_eff()
		{
			if (base.ActivateDescription == -1)
			{
				if (!base.Bot.HasInHandOrInSpellZone(27541563) && base.Bot.GetRemainingCount(27541563, 2) > 0)
				{
					base.AI.SelectCard(new int[] { 27541563, 35146019 });
					base.AI.SelectPlace(this.SelectSetPlace(null));
					return true;
				}
				base.AI.SelectCard(new int[] { 35146019, 27541563 });
				base.AI.SelectPlace(this.SelectSetPlace(null));
				return true;
			}
			else
			{
				if (base.Card.IsDisabled() && !this.Protocol_activing())
				{
					return false;
				}
				int next_card = 0;
				bool choose_other = false;
				bool can_choose_other = false;
				foreach (ClientCard card in base.Bot.GetSpells())
				{
					if (card.IsFaceup() && this.isAltergeist(card))
					{
						can_choose_other = true;
						break;
					}
				}
				if (!can_choose_other)
				{
					foreach (ClientCard card2 in base.Bot.GetMonsters())
					{
						if (card2.IsFaceup() && card2 != base.Card && this.isAltergeist(card2))
						{
							can_choose_other = true;
						}
					}
				}
				if (!base.Util.IsTurn1OrMain2())
				{
					ClientCard self_best = base.Util.GetBestBotMonster(false);
					bool problematicEnemyCard = base.Util.GetProblematicEnemyCard(self_best.Attack, true) != null;
					ClientCard enemy_target = this.GetProblematicEnemyCard_Alter(true, false);
					if ((problematicEnemyCard || enemy_target != null) && base.Bot.HasInGraveyard(25533642))
					{
						next_card = 25533642;
					}
					else if (base.Enemy.GetMonsterCount() <= 1 && base.Bot.HasInGraveyard(25533642) && base.Enemy.GetFieldCount() > 0)
					{
						next_card = 25533642;
					}
					else if (base.Bot.HasInGraveyard(1508649) && base.Util.GetProblematicEnemySpell() == null && base.Util.GetOneEnemyBetterThanValue(3100, true, false) == null && can_choose_other)
					{
						next_card = 1508649;
						choose_other = base.Util.GetOneEnemyBetterThanMyBest(true, false) != null;
					}
				}
				else if (!this.Meluseek_searched && !base.Bot.HasInMonstersZone(25533642, false, false, false) && base.Bot.HasInGraveyard(25533642))
				{
					if (this.Multifaker_candeckss() && base.Bot.HasInGraveyard(42790071) && base.Bot.GetRemainingCount(25533642, 3) > 0)
					{
						next_card = 42790071;
					}
					else
					{
						next_card = 25533642;
					}
				}
				else if (this.Multifaker_candeckss() && base.Bot.HasInGraveyard(42790071) && this.has_altergeist_left())
				{
					next_card = 42790071;
				}
				else if (base.Bot.HasInGraveyard(1508649))
				{
					next_card = 1508649;
					choose_other = base.Bot.GetMonsterCount() <= 1 && !base.Bot.HasInHand(42790071);
				}
				else if (base.Bot.HasInGraveyard(89538537))
				{
					int alter_count = 0;
					foreach (ClientCard card3 in base.Bot.Hand)
					{
						if (this.isAltergeist(card3) && (card3.IsTrap() || (!this.summoned && card3.IsMonster())))
						{
							alter_count++;
						}
					}
					foreach (ClientCard s in base.Bot.GetSpells())
					{
						if (this.isAltergeist(s))
						{
							alter_count++;
						}
					}
					foreach (ClientCard i in base.Bot.GetMonsters())
					{
						if (this.isAltergeist(i) && i != base.Card)
						{
							alter_count++;
						}
					}
					if (alter_count > 0)
					{
						next_card = 89538537;
					}
				}
				if (next_card != 0)
				{
					int Protocol_count = 0;
					using (IEnumerator<ClientCard> enumerator2 = base.Bot.Hand.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							if (enumerator2.Current.IsCode(27541563))
							{
								Protocol_count++;
							}
						}
					}
					foreach (ClientCard s2 in base.Bot.GetSpells())
					{
						if (s2.IsCode(27541563))
						{
							Protocol_count += (s2.IsFaceup() ? 11 : 1);
						}
					}
					if (Protocol_count >= 12)
					{
						base.AI.SelectCard(27541563);
						base.AI.SelectNextCard(next_card);
						this.Marionetter_reborn = true;
						if (next_card == 25533642 && base.Util.IsTurn1OrMain2())
						{
							base.AI.SelectPosition(CardPosition.FaceUpDefence);
						}
						return true;
					}
					List<ClientCard> monsters = base.Bot.GetMonsters();
					monsters.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
					using (List<ClientCard>.Enumerator enumerator = monsters.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							ClientCard card4 = enumerator.Current;
							if (this.isAltergeist(card4) && (!choose_other || card4 != base.Card))
							{
								base.AI.SelectCard(card4);
								base.AI.SelectNextCard(next_card);
								if (next_card == 25533642 && base.Util.IsTurn1OrMain2())
								{
									base.AI.SelectPosition(CardPosition.FaceUpDefence);
								}
								this.Marionetter_reborn = true;
								return true;
							}
						}
						return false;
					}
					return false;
				}
				return false;
			}
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x0004EA2C File Offset: 0x0004CC2C
		public bool Hexstia_eff()
		{
			if (base.Card.Location == CardLocation.MonsterZone && base.Duel.LastChainPlayer != 0 && (this.Protocol_activing() || !base.Card.IsDisabled()))
			{
				ClientCard target = base.Util.GetLastChainCard();
				if (target != null && !this.spell_trap_activate(false, target))
				{
					return false;
				}
				if (!this.Should_counter())
				{
					return false;
				}
				int this_seq = this.GetSequence(base.Card);
				if (this_seq != -1)
				{
					this_seq = this.get_Hexstia_linkzone(this_seq);
				}
				if (this_seq != -1)
				{
					ClientCard linked_card = base.Bot.MonsterZone[this_seq];
					if (linked_card != null && linked_card.IsCode(1508649))
					{
						int next_seq = this.get_Hexstia_linkzone(this_seq);
						if (next_seq != -1 && base.Bot.MonsterZone[next_seq] != null && this.isAltergeist(base.Bot.MonsterZone[next_seq]))
						{
							return false;
						}
					}
				}
				return true;
			}
			else
			{
				if (base.ActivateDescription == base.Util.GetStringId(1508649, 0))
				{
					return false;
				}
				if (base.Enemy.HasInSpellZone(82732705, false, false) && base.Bot.GetRemainingCount(27541563, 3) > 0 && !base.Bot.HasInHandOrInSpellZone(27541563))
				{
					base.AI.SelectCard(27541563);
					return true;
				}
				if (base.Duel.Player == 0 && !this.summoned && base.Bot.GetRemainingCount(53143898, 3) > 0)
				{
					base.AI.SelectCard(53143898);
					return true;
				}
				if (!base.Bot.HasInHandOrHasInMonstersZone(42790071) && base.Bot.GetRemainingCount(42790071, 2) > 0 && this.Multifaker_can_ss())
				{
					base.AI.SelectCard(42790071);
					return true;
				}
				if (!base.Bot.HasInHand(53143898) && base.Bot.GetRemainingCount(53143898, 3) > 0)
				{
					base.AI.SelectCard(53143898);
					return true;
				}
				if (!base.Bot.HasInHandOrInSpellZone(35146019) && base.Bot.GetRemainingCount(35146019, 2) > 0)
				{
					base.AI.SelectCard(35146019);
					return true;
				}
				if (!base.Bot.HasInHandOrInSpellZone(27541563) && base.Bot.GetRemainingCount(27541563, 2) > 0)
				{
					base.AI.SelectCard(27541563);
					return true;
				}
				base.AI.SelectCard(new int[] { 25533642, 52927340, 53143898, 42790071, 35146019, 27541563, 89538537 });
				return true;
			}
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x0004ECB0 File Offset: 0x0004CEB0
		public bool Meluseek_eff()
		{
			if (base.ActivateDescription != base.Util.GetStringId(25533642, 0) && (base.ActivateDescription != -1 || base.Card.Location != CardLocation.MonsterZone))
			{
				if (base.Duel.Player == 1)
				{
					if (!base.Bot.HasInHandOrHasInMonstersZone(42790071) && base.Bot.GetRemainingCount(42790071, 2) > 0 && this.Multifaker_candeckss() && this.Multifaker_can_ss())
					{
						foreach (ClientCard set_card in base.Bot.GetSpells())
						{
							if (set_card.IsFacedown() && !set_card.IsCode(10813327))
							{
								base.AI.SelectCard(42790071);
								return true;
							}
						}
					}
					if (base.Bot.GetRemainingCount(53143898, 3) > 0)
					{
						base.AI.SelectCard(53143898);
						return true;
					}
				}
				else
				{
					if (!this.summoned && !base.Bot.HasInHand(53143898) && base.Bot.GetRemainingCount(53143898, 3) > 0)
					{
						base.AI.SelectCard(53143898);
						return true;
					}
					if (!base.Bot.HasInHandOrHasInMonstersZone(42790071) && base.Bot.GetRemainingCount(42790071, 2) > 0 && this.Multifaker_can_ss())
					{
						base.AI.SelectCard(42790071);
						return true;
					}
					if (!base.Bot.HasInHand(53143898) && base.Bot.GetRemainingCount(53143898, 3) > 0)
					{
						base.AI.SelectCard(53143898);
						return true;
					}
				}
				base.AI.SelectCard(new int[] { 52927340, 53143898, 42790071, 89538537 });
				return true;
			}
			this.attacked_Meluseek.Add(base.Card);
			ClientCard target = this.GetProblematicEnemyCard_Alter(true, true);
			if (target != null)
			{
				base.AI.SelectCard(target);
				return true;
			}
			target = base.Util.GetOneEnemyBetterThanMyBest(true, true);
			if (target != null)
			{
				base.AI.SelectCard(target);
				return true;
			}
			List<ClientCard> targets = base.Enemy.GetSpells();
			this.RandomSort(targets);
			if (targets.Count > 0)
			{
				base.AI.SelectCard(targets[0]);
				return true;
			}
			target = this.GetBestEnemyCard_random();
			if (target != null)
			{
				base.AI.SelectCard(target);
				return true;
			}
			return false;
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x0004EF48 File Offset: 0x0004D148
		public bool Multifaker_handss()
		{
			if (!this.Multifaker_candeckss() || base.Card.Location != CardLocation.Hand)
			{
				return false;
			}
			this.Multifaker_ssfromhand = true;
			if (base.Duel.Player != 0 && base.Util.GetOneEnemyBetterThanMyBest(false, false) != null)
			{
				base.AI.SelectPosition(CardPosition.FaceUpDefence);
			}
			return true;
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x0004EFA0 File Offset: 0x0004D1A0
		public bool Multifaker_deckss()
		{
			if (base.Card.Location == CardLocation.Hand)
			{
				return false;
			}
			ClientCard Silquitous_target = this.GetProblematicEnemyCard_Alter(true, true);
			if (base.Duel.Player == 1 && base.Duel.Phase >= DuelPhase.Main2 && this.GetProblematicEnemyCard_Alter(true, true) == null && base.Bot.GetRemainingCount(25533642, 3) > 0)
			{
				base.AI.SelectCard(25533642);
				this.Multifaker_ssfromdeck = true;
				return true;
			}
			if (!this.Silquitous_bounced && !base.Bot.HasInMonstersZone(89538537, false, false, false) && base.Bot.GetRemainingCount(89538537, 2) > 0 && (base.Duel.Player != 0 || Silquitous_target != null))
			{
				base.AI.SelectCard(89538537);
				this.Multifaker_ssfromdeck = true;
				return true;
			}
			if (!this.Meluseek_searched && !base.Bot.HasInMonstersZone(25533642, false, false, false) && base.Bot.GetRemainingCount(25533642, 3) > 0)
			{
				base.AI.SelectCard(25533642);
				this.Multifaker_ssfromdeck = true;
				return true;
			}
			if (base.Bot.GetRemainingCount(52927340, 1) > 0)
			{
				base.AI.SelectCard(52927340);
				this.Multifaker_ssfromdeck = true;
				return true;
			}
			base.AI.SelectCard(53143898);
			this.Multifaker_ssfromdeck = true;
			return true;
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x0004F10C File Offset: 0x0004D30C
		public bool Silquitous_eff()
		{
			if (base.ActivateDescription != base.Util.GetStringId(89538537, 0))
			{
				if (!base.Bot.HasInHandOrInSpellZone(35146019) && base.Bot.HasInGraveyard(35146019))
				{
					base.AI.SelectCard(35146019);
				}
				else
				{
					base.AI.SelectCard(27541563);
				}
				this.Silquitous_recycled = true;
				return true;
			}
			ClientCard bounce_self = null;
			int Protocol_count = 0;
			ClientCard faceup_Protocol = null;
			ClientCard faceup_Manifestation = null;
			ClientCard selected_target = null;
			foreach (ClientCard spell in base.Bot.GetSpells())
			{
				if (spell.IsCode(27541563))
				{
					if (spell.IsFaceup())
					{
						faceup_Protocol = spell;
						Protocol_count += 11;
					}
					else
					{
						Protocol_count++;
					}
				}
				if (spell.IsCode(35146019) && spell.IsFaceup())
				{
					faceup_Manifestation = spell;
				}
				if (base.Duel.LastChainPlayer != 0 && base.Util.IsChainTarget(spell) && spell.IsFaceup() && this.isAltergeist(spell))
				{
					selected_target = spell;
				}
			}
			if (Protocol_count >= 12)
			{
				bounce_self = faceup_Protocol;
			}
			else if (base.Duel.Player == 0 && faceup_Protocol != null)
			{
				bounce_self = faceup_Protocol;
			}
			else if (faceup_Manifestation != null)
			{
				bounce_self = faceup_Manifestation;
			}
			ClientCard faceup_Multifaker = null;
			ClientCard faceup_monster = null;
			List<ClientCard> monsters = base.Bot.GetMonsters();
			monsters.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			foreach (ClientCard card in monsters)
			{
				if (card.IsFaceup() && this.isAltergeist(card) && card != base.Card)
				{
					if (base.Duel.LastChainPlayer != 0 && base.Util.IsChainTarget(card) && card.IsFaceup())
					{
						selected_target = card;
					}
					if (faceup_Multifaker == null && card.IsCode(42790071))
					{
						faceup_Multifaker = card;
					}
					if (faceup_monster == null && !card.IsCode(1508649))
					{
						faceup_monster = card;
					}
				}
			}
			if (bounce_self == null)
			{
				if (selected_target != null && selected_target != base.Card)
				{
					bounce_self = selected_target;
				}
				else if (faceup_Multifaker != null)
				{
					bounce_self = faceup_Multifaker;
				}
				else
				{
					bounce_self = faceup_monster;
				}
			}
			ClientCard card_should_bounce_immediately = this.GetProblematicEnemyCard_Alter(true, true);
			if (card_should_bounce_immediately != null && base.Duel.LastChainPlayer != 0 && !this.bot_can_s_Meluseek())
			{
				Logger.DebugWriteLine("Silquitous: dangerous");
				base.AI.SelectCard(bounce_self);
				base.AI.SelectNextCard(card_should_bounce_immediately);
				return true;
			}
			if (base.Duel.Phase > DuelPhase.Main1 && base.Duel.Phase < DuelPhase.Main2)
			{
				if (base.Duel.LastChainPlayer != 0)
				{
					Logger.DebugWriteLine("Silquitous: battle");
					if (base.Util.ChainContainsCard(41999284) || base.Bot.HasInHand(52927340))
					{
						return false;
					}
					if (base.Enemy.BattlingMonster != null && base.Bot.BattlingMonster != null && base.Enemy.BattlingMonster.GetDefensePower() >= base.Bot.BattlingMonster.GetDefensePower())
					{
						if (base.Bot.HasInMonstersZone(52927340, false, false, false))
						{
							base.AI.SelectCard(52927340);
						}
						else
						{
							base.AI.SelectCard(bounce_self);
						}
						List<ClientCard> monsters2 = base.Enemy.GetMonsters();
						monsters2.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
						monsters2.Reverse();
						foreach (ClientCard target in monsters2)
						{
							if (target.IsAttack() && !target.IsShouldNotBeMonsterTarget() && !target.IsShouldNotBeTarget())
							{
								base.AI.SelectNextCard(target);
								return true;
							}
						}
						base.AI.SelectNextCard(base.Enemy.BattlingMonster);
						return true;
					}
				}
			}
			else if (base.Duel.Phase > DuelPhase.Main2)
			{
				if (base.Duel.LastChainPlayer != 0)
				{
					Logger.DebugWriteLine("Silquitous: end");
					ClientCard enemy_card = this.GetBestEnemyCard_random();
					if (enemy_card != null)
					{
						base.AI.SelectCard(bounce_self);
						base.AI.SelectNextCard(enemy_card);
						return true;
					}
				}
			}
			else if (base.Duel.Player == 0)
			{
				Logger.DebugWriteLine("Silquitous: orenoturn");
				if (base.Duel.Phase < DuelPhase.Main2 && this.summoned && bounce_self.IsMonster())
				{
					return false;
				}
				ClientCard enemy_card2 = this.GetBestEnemyCard_random();
				if (enemy_card2 != null)
				{
					Logger.DebugWriteLine("Silquitous decide:" + ((bounce_self != null) ? bounce_self.Name : null));
					base.AI.SelectCard(bounce_self);
					base.AI.SelectNextCard(enemy_card2);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x0004F5FC File Offset: 0x0004D7FC
		public bool Manifestation_eff()
		{
			if (base.Card.Location == CardLocation.Grave)
			{
				if (base.Util.ChainContainsCard(89538537))
				{
					return false;
				}
				if (!base.Bot.HasInHandOrInSpellZone(27541563) && !base.Util.ChainContainsCard(27541563))
				{
					base.AI.SelectCard(27541563);
					return true;
				}
				return false;
			}
			else
			{
				if (base.Util.ChainContainsCard(35146019) || base.Util.ChainContainsCard(53936268))
				{
					return false;
				}
				if (base.Duel.LastChainPlayer == 0 && (base.Util.GetLastChainCard() == null || !base.Util.GetLastChainCard().IsCode(1508649)))
				{
					return false;
				}
				if (base.Bot.HasInMonstersZone(1508649, false, false, false))
				{
					bool has_position = false;
					for (int i = 0; i < 7; i++)
					{
						ClientCard target = base.Bot.MonsterZone[i];
						if (target != null && target.IsCode(1508649))
						{
							int next_id = this.get_Hexstia_linkzone(i);
							if (next_id != -1 && base.Bot.MonsterZone[next_id] == null)
							{
								has_position = true;
								break;
							}
						}
					}
					if (!has_position)
					{
						return false;
					}
				}
				if (base.Enemy.HasInMonstersZone(94977269, false, false, false) && base.Bot.HasInGraveyard(89538537))
				{
					base.AI.SelectCard(89538537);
					return true;
				}
				if (!this.Multifaker_candeckss() || !base.Bot.HasInGraveyard(42790071) || !this.has_altergeist_left())
				{
					foreach (int id in new List<int> { 1508649, 89538537, 25533642, 53143898, 52927340 })
					{
						if (base.Bot.HasInGraveyard(id) && (id != 52927340 || (base.Bot.HasInHand(42790071) && this.Multifaker_candeckss())))
						{
							base.AI.SelectCard(id);
							return true;
						}
					}
					return false;
				}
				if (base.Bot.HasInHand(42790071) && base.Bot.HasInGraveyard(89538537) && base.Bot.GetRemainingCount(89538537, 2) == 0)
				{
					base.AI.SelectCard(89538537);
					return true;
				}
				base.AI.SelectCard(42790071);
				return true;
			}
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x0004F898 File Offset: 0x0004DA98
		public bool Protocol_negate_better()
		{
			return (base.ActivateDescription != base.Util.GetStringId(27541563, 1) || base.Util.GetOneEnemyBetterThanMyBest(true, false) != null) && this.Protocol_negate();
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x0004F8CC File Offset: 0x0004DACC
		public bool Protocol_negate()
		{
			if (base.ActivateDescription != base.Util.GetStringId(27541563, 1) || (base.Card.IsDisabled() && !this.Protocol_activing()))
			{
				return false;
			}
			if (!this.Should_counter())
			{
				return false;
			}
			if (this.is_should_not_negate())
			{
				return false;
			}
			if (this.Should_activate_Protocol())
			{
				return false;
			}
			foreach (ClientCard card in base.Bot.GetSpells())
			{
				if (card.IsCode(27541563) && card.IsFaceup() && card != base.Card && (base.Card.IsFacedown() || !base.Card.IsDisabled()))
				{
					base.AI.SelectCard(card);
					return true;
				}
			}
			if (base.Bot.HasInMonstersZone(1508649, false, false, false))
			{
				for (int i = 0; i < 7; i++)
				{
					ClientCard target = base.Bot.MonsterZone[i];
					if (target != null && this.isAltergeist(target) && target.IsFaceup())
					{
						if (target.IsCode(1508649))
						{
							int next_index = this.get_Hexstia_linkzone(i);
							if (next_index != -1 && base.Bot.MonsterZone[next_index] != null && base.Bot.MonsterZone[next_index].IsFaceup() && this.isAltergeist(base.Bot.MonsterZone[next_index]))
							{
								goto IL_01A8;
							}
						}
						if (!this.get_linked_by_Hexstia(i))
						{
							Logger.DebugWriteLine("negate_index: " + i.ToString());
							base.AI.SelectCard(target);
							return true;
						}
					}
					IL_01A8:;
				}
			}
			List<int> cost_list = new List<int>();
			if (base.Util.ChainContainsCard(35146019))
			{
				cost_list.Add(35146019);
			}
			if (!base.Card.IsDisabled())
			{
				cost_list.Add(27541563);
			}
			cost_list.Add(42790071);
			cost_list.Add(53143898);
			cost_list.Add(52927340);
			if (this.Meluseek_searched)
			{
				cost_list.Add(25533642);
			}
			if (this.Silquitous_bounced)
			{
				cost_list.Add(89538537);
			}
			for (int j = 0; j < 7; j++)
			{
				ClientCard card2 = base.Bot.MonsterZone[j];
				if (card2 != null && card2.IsCode(1508649))
				{
					int nextzone = this.get_Hexstia_linkzone(j);
					if (nextzone != -1)
					{
						ClientCard linkedcard = base.Bot.MonsterZone[nextzone];
						if (linkedcard == null || !this.isAltergeist(linkedcard))
						{
							cost_list.Add(1508649);
						}
					}
					else
					{
						cost_list.Add(1508649);
					}
				}
			}
			if (!this.Silquitous_bounced)
			{
				cost_list.Add(89538537);
			}
			if (!this.Meluseek_searched)
			{
				cost_list.Add(25533642);
			}
			if (!base.Util.ChainContainsCard(35146019))
			{
				cost_list.Add(35146019);
			}
			base.AI.SelectCard(cost_list);
			return true;
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x0004FBEC File Offset: 0x0004DDEC
		public bool Protocol_activate_not_use()
		{
			if (base.Util.GetLastChainCard() != null && base.Util.GetLastChainCard().Controller == 0 && base.Util.GetLastChainCard().IsTrap())
			{
				return false;
			}
			if (base.ActivateDescription != base.Util.GetStringId(27541563, 1))
			{
				if (base.Util.IsChainTarget(base.Card) && base.Card.IsFacedown())
				{
					return true;
				}
				if (this.Should_activate_Protocol())
				{
					return true;
				}
				if (!this.Multifaker_ssfromhand && this.Multifaker_candeckss() && (base.Bot.HasInHand(42790071) || base.Bot.HasInSpellZone(53936268, false, false)))
				{
					if (!base.Bot.HasInMonstersZone(1508649, false, false, false))
					{
						return true;
					}
					for (int i = 0; i < 7; i++)
					{
						if (i != 4 && base.Bot.MonsterZone[i] != null && base.Bot.MonsterZone[i].IsCode(1508649))
						{
							int next_id = this.get_Hexstia_linkzone(i);
							if (next_id != -1 && base.Bot.MonsterZone[next_id] == null)
							{
								return true;
							}
						}
					}
				}
				int can_bounce = 0;
				bool should_disnegate = false;
				foreach (ClientCard card in base.Bot.GetMonsters())
				{
					if (this.isAltergeist(card))
					{
						if (card.IsCode(89538537) && card.IsFaceup() && !this.Silquitous_bounced)
						{
							can_bounce += 10;
						}
						else if (card.IsFaceup() && !card.IsCode(1508649))
						{
							can_bounce++;
						}
						if (card.IsDisabled() && !this.Protocol_activing())
						{
							should_disnegate = true;
						}
					}
				}
				if (can_bounce == 10 || should_disnegate)
				{
					return true;
				}
				if (base.Duel.Phase > DuelPhase.Main1 && base.Duel.Phase < DuelPhase.Main2 && base.Bot.HasInHand(52927340) && base.Util.GetOneEnemyBetterThanMyBest(false, false) != null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x0004FE14 File Offset: 0x0004E014
		public void Spoofing_select(IList<int> list)
		{
			foreach (ClientCard card in base.Duel.CurrentChain)
			{
				if (card != null && card.Location == CardLocation.SpellZone && card.Controller == 0 && card.IsFaceup() && card.IsCode(35146019))
				{
					base.AI.SelectCard(card);
					return;
				}
			}
			foreach (ClientCard card2 in base.Bot.Hand)
			{
				foreach (int id in list)
				{
					if (card2.IsCode(id) && (id != 42790071 || base.Util.GetLastChainCard() != card2))
					{
						base.AI.SelectCard(card2);
						return;
					}
				}
			}
			foreach (ClientCard card3 in base.Bot.GetSpells())
			{
				foreach (int id2 in list)
				{
					if (card3.IsFaceup() && card3.IsCode(id2))
					{
						base.AI.SelectCard(card3);
						return;
					}
				}
			}
			foreach (ClientCard card4 in base.Bot.GetMonsters())
			{
				foreach (int id3 in list)
				{
					if (card4.IsFaceup() && card4.IsCode(id3))
					{
						base.AI.SelectCard(card4);
						return;
					}
				}
			}
			base.AI.SelectCard(null);
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x00050074 File Offset: 0x0004E274
		public bool Spoofing_eff()
		{
			if (base.Util.ChainContainsCard(53936268))
			{
				return false;
			}
			if (base.Card.IsDisabled())
			{
				return false;
			}
			if (!base.Util.ChainContainPlayer(0) && !this.Multifaker_ssfromhand && this.Multifaker_candeckss() && base.Bot.HasInHand(42790071) && base.Card.HasPosition(CardPosition.FaceDown))
			{
				base.AI.SelectYesNo(false);
				return true;
			}
			bool has_cost = false;
			bool can_ss_Multifaker = this.Multifaker_can_ss() || base.Card.IsFacedown();
			if (base.Card.IsFacedown())
			{
				foreach (ClientCard card in base.Bot.Hand)
				{
					if (this.isAltergeist(card))
					{
						has_cost = true;
						break;
					}
				}
				if (!has_cost)
				{
					foreach (ClientCard card2 in base.Bot.GetSpells())
					{
						if (this.isAltergeist(card2) && card2.IsFaceup())
						{
							has_cost = true;
							break;
						}
					}
				}
				if (!has_cost)
				{
					foreach (ClientCard card3 in base.Bot.GetMonsters())
					{
						if (this.isAltergeist(card3) && card3.IsFaceup())
						{
							has_cost = true;
							break;
						}
					}
				}
				if (!has_cost)
				{
					foreach (ClientCard card4 in base.Bot.GetSpells())
					{
						if (this.isAltergeist(card4) && card4.IsFaceup())
						{
							has_cost = true;
							break;
						}
					}
				}
				if (!has_cost)
				{
					return false;
				}
			}
			if (base.Duel.Player == 1)
			{
				if (!this.Multifaker_ssfromhand && this.Multifaker_candeckss() && !base.Bot.HasInHand(42790071) && can_ss_Multifaker)
				{
					if (base.Bot.HasInHand(89538537))
					{
						using (IEnumerator<ClientCard> enumerator = base.Bot.Hand.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								ClientCard card5 = enumerator.Current;
								if (card5.IsCode(89538537))
								{
									base.AI.SelectCard(card5);
									base.AI.SelectNextCard(new int[] { 42790071, 52927340 });
									return true;
								}
							}
							goto IL_0550;
						}
					}
					this.Spoofing_select(new int[] { 89538537, 35146019, 52927340, 53143898, 42790071, 27541563, 25533642 });
					base.AI.SelectNextCard(new int[] { 42790071, 53143898, 25533642, 52927340, 89538537 });
					return true;
				}
			}
			else
			{
				ClientCard self_best = base.Util.GetBestBotMonster(false);
				int best_atk = ((self_best == null) ? 0 : self_best.Attack);
				ClientCard enemy_best = base.Util.GetProblematicEnemyCard(best_atk, true);
				ClientCard enemy_target = this.GetProblematicEnemyCard_Alter(true, false);
				if (!this.Multifaker_ssfromhand && this.Multifaker_candeckss() && can_ss_Multifaker)
				{
					this.Spoofing_select(new int[] { 89538537, 35146019, 52927340, 53143898, 42790071, 27541563, 25533642 });
					base.AI.SelectNextCard(new int[] { 42790071, 53143898, 25533642, 52927340, 89538537 });
				}
				else
				{
					if (!this.summoned && !base.Bot.HasInGraveyard(25533642) && base.Bot.GetRemainingCount(25533642, 3) > 0 && !base.Bot.HasInHand(25533642) && (enemy_best != null || enemy_target != null))
					{
						if (base.Bot.HasInHand(89538537))
						{
							using (IEnumerator<ClientCard> enumerator = base.Bot.Hand.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									ClientCard card6 = enumerator.Current;
									if (card6.IsCode(89538537))
									{
										base.AI.SelectCard(card6);
										base.AI.SelectNextCard(new int[] { 25533642, 53143898 });
										return true;
									}
								}
								goto IL_0550;
							}
						}
						this.Spoofing_select(new int[] { 89538537, 35146019, 52927340, 42790071, 27541563, 25533642, 53143898 });
						base.AI.SelectNextCard(new int[] { 25533642, 53143898, 42790071, 52927340 });
						return true;
					}
					if (!this.summoned && !base.Bot.HasInHand(53143898) && base.Bot.GetRemainingCount(53143898, 3) > 0)
					{
						if (base.Bot.HasInHand(89538537))
						{
							using (IEnumerator<ClientCard> enumerator = base.Bot.Hand.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									ClientCard card7 = enumerator.Current;
									if (card7.IsCode(89538537))
									{
										base.AI.SelectCard(card7);
										base.AI.SelectNextCard(new int[] { 53143898, 25533642 });
										return true;
									}
								}
								goto IL_0550;
							}
						}
						this.Spoofing_select(new int[] { 89538537, 35146019, 52927340, 42790071, 27541563, 25533642, 53143898 });
						base.AI.SelectNextCard(new int[] { 53143898, 25533642, 42790071, 52927340 });
						return true;
					}
				}
			}
			IL_0550:
			bool go = false;
			foreach (ClientCard card8 in base.Bot.GetSpells())
			{
				if ((base.Util.ChainContainsCard(18144506) || base.Util.IsChainTarget(card8)) && card8.IsFaceup() && base.Duel.LastChainPlayer != 0 && this.isAltergeist(card8))
				{
					base.AI.SelectCard(card8);
					go = true;
					break;
				}
			}
			if (!go)
			{
				foreach (ClientCard card9 in base.Bot.GetMonsters())
				{
					if ((base.Util.IsChainTarget(card9) || base.Util.ChainContainsCard(53129443) || (!this.Protocol_activing() && card9.IsDisabled())) && card9.IsFaceup() && base.Duel.LastChainPlayer != 0 && this.isAltergeist(card9))
					{
						Logger.DebugWriteLine("Spoofing target:" + ((card9 != null) ? card9.Name : null));
						base.AI.SelectCard(card9);
						go = true;
						break;
					}
				}
			}
			if (go)
			{
				base.AI.SelectNextCard(new int[] { 53143898, 25533642, 42790071, 52927340 });
				return true;
			}
			return false;
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x000507B4 File Offset: 0x0004E9B4
		public bool OneForOne_activate()
		{
			if (!this.spell_trap_activate(false, null))
			{
				return false;
			}
			if (!base.Bot.HasInHandOrInMonstersZoneOrInGraveyard(25533642) && !base.Bot.HasInHandOrInMonstersZoneOrInGraveyard(42790071))
			{
				base.AI.SelectCard(new int[] { 62015408, 23434538, 52927340, 59438930 });
				if (base.Util.IsTurn1OrMain2())
				{
					base.AI.SelectPosition(CardPosition.FaceUpDefence);
				}
				return true;
			}
			if (!this.summoned && !this.Meluseek_searched && !base.Bot.HasInHand(53143898))
			{
				base.AI.SelectCard(new int[] { 62015408, 23434538, 52927340, 59438930 });
				return true;
			}
			return false;
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x0005086C File Offset: 0x0004EA6C
		public bool Meluseek_summon()
		{
			if (this.EvenlyMatched_ready())
			{
				return false;
			}
			if (base.Bot.HasInHand(53143898) && base.Bot.HasInGraveyard(25533642) && !this.Marionetter_reborn)
			{
				return false;
			}
			this.summoned = true;
			return true;
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x000508B9 File Offset: 0x0004EAB9
		public bool Marionetter_summon()
		{
			if (this.EvenlyMatched_ready())
			{
				return false;
			}
			this.summoned = true;
			return true;
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x000508D0 File Offset: 0x0004EAD0
		public bool Silquitous_summon()
		{
			if (this.EvenlyMatched_ready())
			{
				return false;
			}
			bool can_summon = false;
			if (base.Enemy.GetMonsterCount() == 0 && base.Enemy.LifePoints <= 800)
			{
				return true;
			}
			foreach (ClientCard card in base.Bot.Hand)
			{
				if (this.isAltergeist(card) && card.IsTrap())
				{
					can_summon = true;
					break;
				}
			}
			foreach (ClientCard card2 in base.Bot.GetMonstersInMainZone())
			{
				if (this.isAltergeist(card2))
				{
					can_summon = true;
					break;
				}
			}
			foreach (ClientCard card3 in base.Bot.GetSpells())
			{
				if (this.isAltergeist(card3))
				{
					can_summon = true;
					break;
				}
			}
			if (can_summon)
			{
				this.summoned = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x00050A08 File Offset: 0x0004EC08
		public bool Multifaker_summon()
		{
			if (this.EvenlyMatched_ready())
			{
				return false;
			}
			if (base.Enemy.GetMonsterCount() == 0 && base.Enemy.LifePoints <= 1200)
			{
				return true;
			}
			if (base.Bot.HasInMonstersZone(89538537, false, false, false) || base.Bot.HasInHandOrInSpellZone(53936268))
			{
				this.summoned = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x00050A71 File Offset: 0x0004EC71
		public bool PotofDesires_activate()
		{
			if (base.Bot.Deck.Count > 15 && this.spell_trap_activate(false, null))
			{
				base.AI.SelectPlace(this.SelectSTPlace(base.Card, true));
				return true;
			}
			return false;
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x00050AAC File Offset: 0x0004ECAC
		public bool PotofIndulgence_activate()
		{
			if (!this.spell_trap_activate(false, null))
			{
				return false;
			}
			if (base.Bot.HasInGraveyard(41999284) || base.Bot.HasInGraveyard(1508649))
			{
				base.AI.SelectPlace(this.SelectSTPlace(base.Card, true));
				base.AI.SelectOption(1);
				return true;
			}
			int important_count = 0;
			foreach (ClientCard card in base.Bot.ExtraDeck)
			{
				if (card.Id == 41999284 || card.Id == 1508649)
				{
					important_count++;
				}
			}
			if (important_count > 0)
			{
				base.AI.SelectPlace(this.SelectSTPlace(base.Card, true));
				base.AI.SelectOption(1);
				return true;
			}
			return false;
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x00050B9C File Offset: 0x0004ED9C
		public bool Anima_ss()
		{
			if (base.Duel.Phase != DuelPhase.Main2)
			{
				return false;
			}
			ClientCard card_ex_left = base.Enemy.MonsterZone[6];
			ClientCard card_ex_right = base.Enemy.MonsterZone[5];
			if (card_ex_left != null && card_ex_left.HasLinkMarker(128))
			{
				ClientCard self_card_ = base.Bot.MonsterZone[1];
				if (self_card_ == null)
				{
					base.AI.SelectMaterials(25533642, 0);
					base.AI.SelectPlace(2);
					this.ss_other_monster = true;
					return true;
				}
				if (self_card_.IsCode(25533642))
				{
					base.AI.SelectMaterials(self_card_, 0);
					base.AI.SelectPlace(2);
					this.ss_other_monster = true;
					return true;
				}
			}
			if (card_ex_right != null && card_ex_right.HasLinkMarker(128))
			{
				ClientCard self_card_2 = base.Bot.MonsterZone[3];
				if (self_card_2 == null)
				{
					base.AI.SelectMaterials(25533642, 0);
					base.AI.SelectPlace(8);
					this.ss_other_monster = true;
					return true;
				}
				if (self_card_2.IsCode(25533642))
				{
					base.AI.SelectMaterials(self_card_2, 0);
					base.AI.SelectPlace(8);
					this.ss_other_monster = true;
					return true;
				}
			}
			ClientCard card_left = base.Enemy.MonsterZone[3];
			ClientCard card_right = base.Enemy.MonsterZone[1];
			if (card_left != null && card_left.IsFacedown())
			{
				card_left = null;
			}
			if (card_right != null && card_right.IsFacedown())
			{
				card_right = null;
			}
			if (card_left != null && (card_left.IsShouldNotBeMonsterTarget() || card_left.IsShouldNotBeTarget()))
			{
				card_left = null;
			}
			if (card_right != null && (card_right.IsShouldNotBeMonsterTarget() || card_right.IsShouldNotBeTarget()))
			{
				card_right = null;
			}
			if (base.Enemy.MonsterZone[6] != null)
			{
				card_left = null;
			}
			if (base.Enemy.MonsterZone[5] != null)
			{
				card_right = null;
			}
			if (card_left == null && card_right != null && base.Bot.MonsterZone[6] == null)
			{
				base.AI.SelectMaterials(25533642, 0);
				base.AI.SelectPlace(64);
				this.ss_other_monster = true;
				return true;
			}
			if (card_left != null && card_right == null && base.Bot.MonsterZone[5] == null)
			{
				base.AI.SelectMaterials(25533642, 0);
				base.AI.SelectPlace(32);
				this.ss_other_monster = true;
				return true;
			}
			if (card_left != null && card_right != null && base.Bot.GetMonstersExtraZoneCount() == 0)
			{
				int selection;
				if (card_left.IsFloodgate() && !card_right.IsFloodgate())
				{
					selection = 32;
				}
				else if (!card_left.IsFloodgate() && card_right.IsFloodgate())
				{
					selection = 64;
				}
				else if (card_left.GetDefensePower() >= card_right.GetDefensePower())
				{
					selection = 32;
				}
				else
				{
					selection = 64;
				}
				base.AI.SelectPlace(selection);
				base.AI.SelectMaterials(25533642, 0);
				this.ss_other_monster = true;
				return true;
			}
			return false;
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x00050E49 File Offset: 0x0004F049
		public bool Linkuriboh_ss()
		{
			if (base.Bot.GetMonstersExtraZoneCount() > 0)
			{
				return false;
			}
			if (base.Util.IsTurn1OrMain2() && !this.Meluseek_searched)
			{
				base.AI.SelectPlace(32);
				this.ss_other_monster = true;
				return true;
			}
			return false;
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x00050E88 File Offset: 0x0004F088
		public bool Linkuriboh_eff()
		{
			if (base.Util.ChainContainsCard(41999284))
			{
				return false;
			}
			if (base.Util.ChainContainsCard(42790071))
			{
				return false;
			}
			if (base.Duel.Player == 1)
			{
				if (base.Card.Location == CardLocation.Grave)
				{
					base.AI.SelectCard(new int[] { 25533642 });
					this.ss_other_monster = true;
					return true;
				}
				if (base.Card.IsDisabled() && !base.Enemy.HasInSpellZone(82732705, true, false))
				{
					return false;
				}
				ClientCard enemy_card = base.Enemy.BattlingMonster;
				if (enemy_card == null)
				{
					return false;
				}
				ClientCard self_card = base.Bot.BattlingMonster;
				if (self_card == null)
				{
					return !enemy_card.IsCode(8491308);
				}
				return enemy_card.Attack > self_card.GetDefensePower();
			}
			else
			{
				if (!this.summoned && !base.Bot.HasInHand(53143898) && !this.Meluseek_searched && (base.Duel.Phase == DuelPhase.Main1 || base.Duel.Phase == DuelPhase.Main2))
				{
					base.AI.SelectCard(new int[] { 25533642 });
					this.ss_other_monster = true;
					base.AI.SelectPlace(17);
					return true;
				}
				if (base.Util.IsTurn1OrMain2())
				{
					base.AI.SelectCard(new int[] { 25533642 });
					this.ss_other_monster = true;
					base.AI.SelectPlace(17);
					return true;
				}
				if (base.Duel.Phase > DuelPhase.Main1 && base.Duel.Phase < DuelPhase.Main2)
				{
					if (base.Duel.Player != 0 || this.attacked_Meluseek.Count == 0 || base.Enemy.GetMonsterCount() > 0)
					{
						return false;
					}
					foreach (ClientCard card in this.attacked_Meluseek)
					{
						if (card != null && card.Location == CardLocation.MonsterZone)
						{
							this.ss_other_monster = true;
							base.AI.SelectPlace(17);
							return true;
						}
					}
					return false;
				}
				return false;
			}
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x000510C4 File Offset: 0x0004F2C4
		public bool Hexstia_ss()
		{
			List<ClientCard> targets = new List<ClientCard>();
			List<ClientCard> monsters = base.Bot.GetMonsters();
			monsters.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			bool Meluseek_selected = false;
			bool Silquitous_selected = false;
			bool Hexstia_selected = false;
			int altergeist_count = 0;
			foreach (ClientCard card in monsters)
			{
				if (this.isAltergeist(card))
				{
					altergeist_count++;
				}
				if (card.IsCode(25533642) && targets.Count < 2 && card.IsFaceup())
				{
					if ((!this.Meluseek_searched || !Meluseek_selected) && (!this.summoned || base.Duel.Phase == DuelPhase.Main2))
					{
						Meluseek_selected = true;
						targets.Add(card);
					}
				}
				else if (card.IsCode(89538537) && targets.Count < 2 && card.IsFaceup() && !base.Bot.HasInGraveyard(89538537))
				{
					if (!this.Silquitous_recycled || !Silquitous_selected)
					{
						Silquitous_selected = true;
						targets.Add(card);
					}
				}
				else if (card.IsCode(1508649) && targets.Count < 2 && card.IsFaceup())
				{
					if ((!this.Hexstia_searched || !Hexstia_selected) && !this.summoned && !base.Bot.HasInHand(53143898) && base.Bot.GetRemainingCount(53143898, 3) > 0)
					{
						Hexstia_selected = true;
						targets.Add(card);
					}
				}
				else if (this.isAltergeist(card) && targets.Count < 2 && card.IsFaceup())
				{
					targets.Add(card);
				}
				else if (card.IsCode(89538537) && targets.Count < 2 && card.IsFaceup() && (!this.Silquitous_recycled || !Silquitous_selected))
				{
					Silquitous_selected = true;
					targets.Add(card);
				}
			}
			if (targets.Count < 2)
			{
				return false;
			}
			if (base.Duel.Phase < DuelPhase.Main2 && this.GetTotalATK(targets) >= 1500 && (this.summoned || (!Meluseek_selected && !Hexstia_selected)))
			{
				return false;
			}
			if ((base.Bot.HasInHand(42790071) || (base.Bot.GetRemainingCount(42790071, 2) > 0 && ((Meluseek_selected && !this.Meluseek_searched) || (Hexstia_selected && !this.Hexstia_searched)))) && this.Multifaker_can_ss())
			{
				altergeist_count++;
			}
			if (base.Bot.HasInHandOrInSpellZone(35146019))
			{
				altergeist_count++;
			}
			Logger.DebugWriteLine("Multifaker_ss_check: count = " + altergeist_count.ToString());
			if (altergeist_count <= 2)
			{
				return false;
			}
			base.AI.SelectMaterials(targets, 0);
			return true;
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x000513A4 File Offset: 0x0004F5A4
		public bool TripleBurstDragon_eff()
		{
			return base.ActivateDescription == base.Util.GetStringId(49725936, 0) && base.Duel.LastChainPlayer != 0;
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x000513D0 File Offset: 0x0004F5D0
		public bool TripleBurstDragon_ss()
		{
			if (!base.Enemy.HasInGraveyard(26077387))
			{
				ClientCard self_best = base.Util.GetBestBotMonster(true);
				int self_power = ((self_best != null) ? self_best.Attack : 0);
				ClientCard enemy_best = base.Util.GetBestEnemyMonster(true, false);
				int enemy_power = ((enemy_best != null) ? enemy_best.GetDefensePower() : 0);
				if (enemy_power <= self_power)
				{
					return false;
				}
				Logger.DebugWriteLine("Three: enemy: " + enemy_power.ToString() + ", bot: " + self_power.ToString());
				if (enemy_power >= 2401)
				{
					return false;
				}
			}
			using (List<ClientCard>.Enumerator enumerator = base.Bot.GetMonstersInExtraZone().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.HasType(CardType.Link))
					{
						return false;
					}
				}
			}
			int link_count = 0;
			if (base.Enemy.HasInMonstersZone(90673288, false, false, false) && base.Enemy.GetGraveyardSpells().Count >= 9)
			{
				return false;
			}
			List<ClientCard> list = new List<ClientCard>();
			if (base.Bot.HasInMonstersZone(50588353, false, false, false))
			{
				foreach (ClientCard card in base.Bot.GetMonsters())
				{
					if (card.IsCode(50588353))
					{
						list.Add(card);
						link_count += 2;
					}
				}
			}
			List<ClientCard> monsters = base.Bot.GetMonsters();
			monsters.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			foreach (ClientCard card2 in monsters)
			{
				if (!list.Contains(card2) && card2.LinkCount < 3)
				{
					list.Add(card2);
					link_count += (card2.HasType(CardType.Link) ? card2.LinkCount : 1);
					if (link_count >= 3)
					{
						break;
					}
				}
			}
			if (link_count >= 3)
			{
				base.AI.SelectMaterials(list, 0);
				this.ss_other_monster = true;
				return true;
			}
			return false;
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x00051608 File Offset: 0x0004F808
		public bool Needlefiber_ss()
		{
			if (!base.Enemy.HasInGraveyard(26077387))
			{
				ClientCard self_best = base.Util.GetBestBotMonster(true);
				int self_power = ((self_best != null) ? self_best.Attack : 0);
				ClientCard enemy_best = base.Util.GetBestEnemyMonster(true, false);
				int enemy_power = ((enemy_best != null) ? enemy_best.GetDefensePower() : 0);
				if (enemy_power < self_power)
				{
					return false;
				}
				if (base.Bot.GetMonsterCount() <= 2 && enemy_power >= 2401)
				{
					return false;
				}
			}
			using (List<ClientCard>.Enumerator enumerator = base.Bot.GetMonstersInExtraZone().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.HasType(CardType.Link))
					{
						return false;
					}
				}
			}
			List<ClientCard> material_list = new List<ClientCard>();
			List<ClientCard> monsters = base.Bot.GetMonsters();
			monsters.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			foreach (ClientCard t in monsters)
			{
				if (t.IsTuner())
				{
					material_list.Add(t);
					break;
				}
			}
			foreach (ClientCard i in monsters)
			{
				if (!material_list.Contains(i) && i.LinkCount <= 2)
				{
					material_list.Add(i);
					if (material_list.Count >= 2)
					{
						break;
					}
				}
			}
			base.AI.SelectMaterials(material_list, 0);
			this.ss_other_monster = true;
			return true;
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x000517C0 File Offset: 0x0004F9C0
		public bool Needlefiber_eff()
		{
			base.AI.SelectCard(new int[] { 62015408, 59438930, 14558127 });
			return true;
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x000517E0 File Offset: 0x0004F9E0
		public bool Borrelsword_ss()
		{
			if (base.Duel.Phase != DuelPhase.Main1)
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
			using (List<ClientCard>.Enumerator enumerator = base.Bot.GetMonstersInExtraZone().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.HasType(CardType.Link))
					{
						return false;
					}
				}
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
					if (link_count >= 4)
					{
						break;
					}
				}
			}
			if (link_count >= 4)
			{
				base.AI.SelectMaterials(material_list, 0);
				this.ss_other_monster = true;
				return true;
			}
			return false;
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x00051954 File Offset: 0x0004FB54
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

		// Token: 0x060010A5 RID: 4261 RVA: 0x00051AA4 File Offset: 0x0004FCA4
		public bool tuner_summon()
		{
			if (this.EvenlyMatched_ready())
			{
				return false;
			}
			foreach (ClientCard card in base.Bot.GetMonstersInExtraZone())
			{
				if (card != null && !card.HasType(CardType.Link))
				{
					return false;
				}
			}
			if (!base.Enemy.HasInGraveyard(26077387))
			{
				ClientCard self_best = base.Util.GetBestBotMonster(true);
				int self_power = ((self_best != null) ? self_best.Attack : 0);
				ClientCard enemy_best = base.Util.GetBestEnemyMonster(true, false);
				int enemy_power = ((enemy_best != null) ? enemy_best.GetDefensePower() : 0);
				Logger.DebugWriteLine("Tuner: enemy: " + enemy_power.ToString() + ", bot: " + self_power.ToString());
				if (enemy_power < self_power || enemy_power == 0)
				{
					return false;
				}
				if (((base.Bot.HasInExtra(50588353) ? (base.Bot.GetMonsterCount() + 2) : (base.Bot.GetMonsterCount() + 1)) <= 3 && enemy_power >= 2400) || (!base.Bot.HasInExtra(49725936) && !base.Bot.HasInExtra(85289965)))
				{
					return false;
				}
			}
			if (this.Multifaker_ssfromdeck)
			{
				return false;
			}
			using (List<ClientCard>.Enumerator enumerator = base.Bot.GetMonsters().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsFaceup())
					{
						this.summoned = true;
						base.AI.SelectPlace(32);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x00051C60 File Offset: 0x0004FE60
		public override ClientCard OnSelectAttacker(IList<ClientCard> attackers, IList<ClientCard> defenders)
		{
			List<ClientCard> Meluseek_list = new List<ClientCard>();
			for (int i = 0; i < attackers.Count; i++)
			{
				ClientCard attacker = attackers[i];
				if (attacker.IsCode(25533642) && !attacker.IsDisabled())
				{
					if (base.Enemy.GetMonsterCount() > 0)
					{
						return attacker;
					}
					Meluseek_list.Add(attacker);
				}
				if (attacker.IsCode(85289965) && !attacker.IsDisabled())
				{
					return attacker;
				}
			}
			if (Meluseek_list.Count > 0)
			{
				foreach (ClientCard card in Meluseek_list)
				{
					attackers.Remove(card);
					attackers.Add(card);
				}
			}
			return null;
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x0000763C File Offset: 0x0000583C
		public override bool OnSelectHand()
		{
			return true;
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x00051D28 File Offset: 0x0004FF28
		public override void OnNewTurn()
		{
			this.Multifaker_ssfromhand = false;
			this.Multifaker_ssfromdeck = false;
			this.Marionetter_reborn = false;
			this.Hexstia_searched = false;
			this.Meluseek_searched = false;
			this.summoned = false;
			this.Silquitous_bounced = false;
			this.Silquitous_recycled = false;
			this.ss_other_monster = false;
			this.Impermanence_list.Clear();
			this.attacked_Meluseek.Clear();
			base.OnNewTurn();
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x00051D90 File Offset: 0x0004FF90
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

		// Token: 0x060010AA RID: 4266 RVA: 0x00051DE8 File Offset: 0x0004FFE8
		public bool MonsterRepos()
		{
			if (base.Card.Attack == 0)
			{
				return base.Card.IsAttack();
			}
			if (base.Card.IsCode(25533642) || base.Bot.HasInMonstersZone(25533642, false, false, false))
			{
				return base.Card.HasPosition(CardPosition.Defence);
			}
			if (this.isAltergeist(base.Card) && base.Bot.HasInHandOrInSpellZone(27541563) && base.Card.IsFacedown())
			{
				return true;
			}
			bool enemyBetter = base.Util.IsAllEnemyBetter(true);
			return (base.Card.IsAttack() && enemyBetter) || (base.Card.IsDefense() && !enemyBetter);
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x00051EA4 File Offset: 0x000500A4
		public bool MonsterSet()
		{
			if (base.Util.GetOneEnemyBetterThanMyBest(false, false) == null && base.Bot.GetMonsterCount() > 0)
			{
				return false;
			}
			if (base.Card.Level > 4)
			{
				return false;
			}
			int rest_lp = base.Bot.LifePoints;
			int count = base.Bot.GetMonsterCount();
			List<ClientCard> monsters = base.Enemy.GetMonsters();
			monsters.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			foreach (ClientCard card in monsters)
			{
				if (card.HasPosition(CardPosition.Attack) && count-- <= 0)
				{
					rest_lp -= card.Attack;
				}
			}
			if (rest_lp < 1700)
			{
				base.AI.SelectPosition(CardPosition.FaceDownDefence);
				return true;
			}
			return false;
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x00051F80 File Offset: 0x00050180
		public bool MonsterSummon()
		{
			return base.Enemy.GetMonsterCount() + base.Bot.GetMonsterCount() <= 0 && base.Card.Attack >= base.Enemy.LifePoints;
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x00051FBC File Offset: 0x000501BC
		public override BattlePhaseAction OnSelectAttackTarget(ClientCard attacker, IList<ClientCard> defenders)
		{
			if (this.EvenlyMatched_ready())
			{
				List<ClientCard> monsters = base.Enemy.GetMonsters();
				monsters.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				foreach (ClientCard e_card in monsters)
				{
					if (e_card.HasPosition(CardPosition.Attack))
					{
						return base.AI.Attack(attacker, e_card);
					}
				}
			}
			for (int i = 0; i < defenders.Count; i++)
			{
				ClientCard defender = defenders[i];
				attacker.RealPower = attacker.Attack;
				defender.RealPower = defender.GetDefensePower();
				if (attacker.IsCode(85289965) && !attacker.IsDisabled())
				{
					return base.AI.Attack(attacker, defender);
				}
				if (this.OnPreBattleBetween(attacker, defender) && (attacker.RealPower != defender.RealPower || base.Bot.GetMonsterCount() >= base.Enemy.GetMonsterCount()) && (attacker.RealPower > defender.RealPower || (attacker.RealPower >= defender.RealPower && attacker.IsLastAttacker && defender.IsAttack())))
				{
					return base.AI.Attack(attacker, defender);
				}
			}
			if (attacker.CanDirectAttack && (base.Enemy.GetMonsterCount() == 0 || !attacker.IsDisabled()))
			{
				return base.AI.Attack(attacker, null);
			}
			return null;
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x00052140 File Offset: 0x00050340
		public override IList<ClientCard> OnSelectCard(IList<ClientCard> cards, int min, int max, int hint, bool cancelable)
		{
			if (max == 1 && cards[0].Location == CardLocation.Deck && base.Util.GetLastChainCard() != null && base.Util.GetLastChainCard().IsCode(23002292) && base.Bot.GetRemainingCount(10813327, 1) > 0)
			{
				IList<ClientCard> result = new List<ClientCard>();
				foreach (ClientCard card in cards)
				{
					if (card.IsCode(10813327))
					{
						result.Add(card);
						base.AI.SelectPlace(this.SelectSetPlace(null));
						break;
					}
				}
				if (result.Count > 0)
				{
					return result;
				}
			}
			else if (base.Util.GetLastChainCard() != null && base.Util.GetLastChainCard().IsCode(15693423) && base.Duel.LastChainPlayer != 0)
			{
				Logger.DebugWriteLine("EvenlyMatched: min=" + min.ToString() + ", max=" + max.ToString());
			}
			else if (cards[0].Location == CardLocation.Hand && cards[cards.Count - 1].Location == CardLocation.Hand && (hint == 501 || hint == 504) && min == max)
			{
				if (base.Duel.LastChainPlayer == 0 && base.Util.GetLastChainCard().IsCode(2295440))
				{
					return null;
				}
				Logger.DebugWriteLine("Hand drop except OneForOne");
				int todrop = min;
				IList<ClientCard> result2 = new List<ClientCard>();
				IList<ClientCard> ToRemove = new List<ClientCard>(cards);
				List<int> record = new List<int>();
				foreach (ClientCard card2 in ToRemove)
				{
					if ((card2 == null || card2.Id != 0) && !record.Contains(card2.Id))
					{
						record.Add(card2.Id);
					}
					else
					{
						result2.Add(card2);
						if (--todrop <= 0)
						{
							break;
						}
					}
				}
				if (todrop <= 0)
				{
					return result2;
				}
				foreach (ClientCard card3 in result2)
				{
					ToRemove.Remove(card3);
				}
				foreach (int throw_id in this.cards_improper)
				{
					foreach (ClientCard card4 in ToRemove)
					{
						if (card4.IsCode(throw_id))
						{
							result2.Add(card4);
							if (--todrop <= 0)
							{
								return result2;
							}
						}
					}
				}
				return null;
			}
			return base.OnSelectCard(cards, min, max, hint, cancelable);
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x00052458 File Offset: 0x00050658
		public override CardPosition OnSelectPosition(int cardId, IList<CardPosition> positions)
		{
			if (base.Util.IsTurn1OrMain2() && (cardId == 25533642 || cardId == 89538537))
			{
				return CardPosition.FaceUpDefence;
			}
			return (CardPosition)0;
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x0005247C File Offset: 0x0005067C
		public override int OnSelectPlace(int cardId, int player, CardLocation location, int available)
		{
			if (player == 0 && location != CardLocation.SpellZone && location == CardLocation.MonsterZone)
			{
				if (cardId == 41999284)
				{
					if ((32 & available) > 0)
					{
						return 32;
					}
					if ((64 & available) > 0)
					{
						return 64;
					}
					for (int i = 4; i >= 0; i--)
					{
						if (base.Bot.MonsterZone[i] == null)
						{
							return (int)Math.Pow(2.0, (double)i);
						}
					}
				}
				if (this.isAltergeist(cardId))
				{
					if (base.Bot.HasInMonstersZone(1508649, false, false, false))
					{
						for (int j = 0; j < 7; j++)
						{
							if (j != 4 && base.Bot.MonsterZone[j] != null && base.Bot.MonsterZone[j].IsCode(1508649))
							{
								int next_index = this.get_Hexstia_linkzone(j);
								if (next_index != -1 && (available & (int)Math.Pow(2.0, (double)next_index)) > 0)
								{
									return (int)Math.Pow(2.0, (double)next_index);
								}
							}
						}
					}
					if (cardId == 1508649)
					{
						if ((32 & available) > 0 && base.Bot.MonsterZone[1] != null && this.isAltergeist(base.Bot.MonsterZone[1]))
						{
							return 32;
						}
						if ((64 & available) > 0 && base.Bot.MonsterZone[3] != null && this.isAltergeist(base.Bot.MonsterZone[3]))
						{
							return 64;
						}
						if (((64 & available) > 0 && base.Bot.MonsterZone[3] != null && !this.isAltergeist(base.Bot.MonsterZone[3])) || ((32 & available) > 0 && base.Bot.MonsterZone[1] == null))
						{
							return 32;
						}
						if (((32 & available) > 0 && base.Bot.MonsterZone[1] != null && !this.isAltergeist(base.Bot.MonsterZone[1])) || ((64 & available) > 0 && base.Bot.MonsterZone[3] == null))
						{
							return 64;
						}
						for (int k = 1; k < 5; k++)
						{
							if (base.Bot.MonsterZone[k] != null && this.isAltergeist(base.Bot.MonsterZone[k]) && (available & (int)Math.Pow(2.0, (double)(k - 1))) > 0)
							{
								return (int)Math.Pow(2.0, (double)(k - 1));
							}
						}
					}
					if ((2 & available) > 0)
					{
						return 2;
					}
					if ((8 & available) > 0)
					{
						return 8;
					}
				}
			}
			return base.OnSelectPlace(cardId, player, location, available);
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x000526E5 File Offset: 0x000508E5
		protected override bool DefaultSetForDiabellze()
		{
			if (base.DefaultSetForDiabellze())
			{
				base.AI.SelectPlace(this.SelectSTPlace(base.Card, true));
				return true;
			}
			return false;
		}

		// Token: 0x0400152A RID: 5418
		private List<int> Impermanence_list = new List<int>();

		// Token: 0x0400152B RID: 5419
		private bool Multifaker_ssfromhand;

		// Token: 0x0400152C RID: 5420
		private bool Multifaker_ssfromdeck;

		// Token: 0x0400152D RID: 5421
		private bool Marionetter_reborn;

		// Token: 0x0400152E RID: 5422
		private bool Hexstia_searched;

		// Token: 0x0400152F RID: 5423
		private bool Meluseek_searched;

		// Token: 0x04001530 RID: 5424
		private bool summoned;

		// Token: 0x04001531 RID: 5425
		private bool Silquitous_bounced;

		// Token: 0x04001532 RID: 5426
		private bool Silquitous_recycled;

		// Token: 0x04001533 RID: 5427
		private bool ss_other_monster;

		// Token: 0x04001534 RID: 5428
		private List<ClientCard> attacked_Meluseek = new List<ClientCard>();

		// Token: 0x04001535 RID: 5429
		private List<int> SkyStrike_list = new List<int>
		{
			26077387, 8491308, 63288573, 90673288, 21623008, 25955749, 63166095, 99550630, 25733157, 51227866,
			52340444, 98338152, 24010609, 97616504, 50005218
		};

		// Token: 0x04001536 RID: 5430
		private List<int> cards_improper = new List<int>
		{
			0, 10813327, 40605147, 53936268, 2295440, 35261759, 35146019, 68462976, 61740673, 18144506,
			62015408, 27541563, 41420027, 23924608, 59438930, 89538537, 23434538, 10045474, 25533642, 14558127,
			52927340, 53143898, 42790071
		};

		// Token: 0x04001537 RID: 5431
		private List<int> normal_counter = new List<int>
		{
			53262004, 98338152, 32617464, 45041488, 40605147, 61257789, 23440231, 27354732, 12408276, 82419869,
			10045474, 49680980, 18621798, 38814750, 17266660, 94689635, 14558127, 74762582, 75286651, 4810828,
			44665365, 21123811, 50954680, 82044279, 82044280, 79606837, 10443957, 1621413, 27541563, 90809975,
			8165596, 9753964, 53347303, 88307361, 55063751, 5818294, 2948263, 6150044, 26268488, 51447164,
			63941210, 97268402
		};

		// Token: 0x04001538 RID: 5432
		private List<int> should_not_negate = new List<int> { 81275020, 28985331 };

		// Token: 0x020002B6 RID: 694
		public class CardId
		{
			// Token: 0x04001539 RID: 5433
			public const int Kunquery = 52927340;

			// Token: 0x0400153A RID: 5434
			public const int Marionetter = 53143898;

			// Token: 0x0400153B RID: 5435
			public const int Multifaker = 42790071;

			// Token: 0x0400153C RID: 5436
			public const int AB_JS = 14558127;

			// Token: 0x0400153D RID: 5437
			public const int GO_SR = 59438930;

			// Token: 0x0400153E RID: 5438
			public const int GR_WC = 62015408;

			// Token: 0x0400153F RID: 5439
			public const int GB_HM = 73642296;

			// Token: 0x04001540 RID: 5440
			public const int Silquitous = 89538537;

			// Token: 0x04001541 RID: 5441
			public const int MaxxC = 23434538;

			// Token: 0x04001542 RID: 5442
			public const int Meluseek = 25533642;

			// Token: 0x04001543 RID: 5443
			public const int OneForOne = 2295440;

			// Token: 0x04001544 RID: 5444
			public const int PotofDesires = 35261759;

			// Token: 0x04001545 RID: 5445
			public const int PotofIndulgence = 49238328;

			// Token: 0x04001546 RID: 5446
			public const int Impermanence = 10045474;

			// Token: 0x04001547 RID: 5447
			public const int WakingtheDragon = 10813327;

			// Token: 0x04001548 RID: 5448
			public const int EvenlyMatched = 15693423;

			// Token: 0x04001549 RID: 5449
			public const int Storm = 23924608;

			// Token: 0x0400154A RID: 5450
			public const int Manifestation = 35146019;

			// Token: 0x0400154B RID: 5451
			public const int Protocol = 27541563;

			// Token: 0x0400154C RID: 5452
			public const int Spoofing = 53936268;

			// Token: 0x0400154D RID: 5453
			public const int ImperialOrder = 61740673;

			// Token: 0x0400154E RID: 5454
			public const int SolemnStrike = 40605147;

			// Token: 0x0400154F RID: 5455
			public const int SolemnJudgment = 41420027;

			// Token: 0x04001550 RID: 5456
			public const int NaturalExterio = 99916754;

			// Token: 0x04001551 RID: 5457
			public const int UltimateFalcon = 86221741;

			// Token: 0x04001552 RID: 5458
			public const int Borrelsword = 85289965;

			// Token: 0x04001553 RID: 5459
			public const int FWD = 5043010;

			// Token: 0x04001554 RID: 5460
			public const int TripleBurstDragon = 49725936;

			// Token: 0x04001555 RID: 5461
			public const int HeavymetalfoesElectrumite = 24094258;

			// Token: 0x04001556 RID: 5462
			public const int Isolde = 59934749;

			// Token: 0x04001557 RID: 5463
			public const int Hexstia = 1508649;

			// Token: 0x04001558 RID: 5464
			public const int Needlefiber = 50588353;

			// Token: 0x04001559 RID: 5465
			public const int Kagari = 63288573;

			// Token: 0x0400155A RID: 5466
			public const int Shizuku = 90673288;

			// Token: 0x0400155B RID: 5467
			public const int Linkuriboh = 41999284;

			// Token: 0x0400155C RID: 5468
			public const int Anima = 94259633;

			// Token: 0x0400155D RID: 5469
			public const int SecretVillage = 68462976;

			// Token: 0x0400155E RID: 5470
			public const int DarkHole = 53129443;

			// Token: 0x0400155F RID: 5471
			public const int NaturalBeast = 33198837;

			// Token: 0x04001560 RID: 5472
			public const int SwordsmanLV7 = 37267041;

			// Token: 0x04001561 RID: 5473
			public const int RoyalDecreel = 51452091;

			// Token: 0x04001562 RID: 5474
			public const int Anti_Spell = 58921041;

			// Token: 0x04001563 RID: 5475
			public const int Hayate = 8491308;

			// Token: 0x04001564 RID: 5476
			public const int Raye = 26077387;

			// Token: 0x04001565 RID: 5477
			public const int Drones_Token = 52340445;

			// Token: 0x04001566 RID: 5478
			public const int Iblee = 10158145;
		}
	}
}
