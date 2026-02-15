using System;
using System.Collections.Generic;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x02000309 RID: 777
	[Deck("FamiliarPossessed", "AI_FamiliarPossessed", "Normal")]
	public class FamiliarPossessedExecutor : DefaultExecutor
	{
		// Token: 0x060013FF RID: 5119 RVA: 0x0006E36C File Offset: 0x0006C56C
		public FamiliarPossessedExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.Activate, 49238328, new Func<bool>(this.PotofExtravaganceActivate));
			base.AddExecutor(ExecutorType.SpSummon, 12014404, new Func<bool>(this.GagagaCowboySummon));
			base.AddExecutor(ExecutorType.Activate, 12014404);
			base.AddExecutor(ExecutorType.Activate, 30241314, new Func<bool>(this.MacroCosmoseff));
			base.AddExecutor(ExecutorType.Activate, 14558127, new Func<bool>(base.DefaultAshBlossomAndJoyousSpring));
			base.AddExecutor(ExecutorType.Activate, 23434538, new Func<bool>(base.DefaultMaxxC));
			base.AddExecutor(ExecutorType.Activate, 84749824, new Func<bool>(base.DefaultSolemnWarning));
			base.AddExecutor(ExecutorType.Activate, 40605147, new Func<bool>(base.DefaultSolemnStrike));
			base.AddExecutor(ExecutorType.Activate, 61740673, new Func<bool>(this.ImperialOrderfirst));
			base.AddExecutor(ExecutorType.Activate, 61740673, new Func<bool>(this.ImperialOrdereff));
			base.AddExecutor(ExecutorType.Activate, 41420027, new Func<bool>(base.DefaultSolemnJudgment));
			base.AddExecutor(ExecutorType.Activate, 82732705, new Func<bool>(this.SkillDrainEffect));
			base.AddExecutor(ExecutorType.Activate, 59305593, new Func<bool>(base.DefaultUniqueTrap));
			base.AddExecutor(ExecutorType.Activate, 62256492);
			base.AddExecutor(ExecutorType.Activate, 25704359, new Func<bool>(this.UnpossessedEffect));
			base.AddExecutor(ExecutorType.Activate, 18144506, new Func<bool>(base.DefaultHarpiesFeatherDusterFirst));
			base.AddExecutor(ExecutorType.Activate, 35261759, new Func<bool>(this.PotOfDesireseff));
			base.AddExecutor(ExecutorType.Activate, 41999284, new Func<bool>(this.Linkuriboheff));
			base.AddExecutor(ExecutorType.SpSummon, 41999284, new Func<bool>(this.Linkuribohsp));
			base.AddExecutor(ExecutorType.SpSummon, 75452921, new Func<bool>(this.Knightmaresp));
			base.AddExecutor(ExecutorType.SpSummon, 2857636, new Func<bool>(this.Knightmaresp));
			base.AddExecutor(ExecutorType.SpSummon, 97661969, new Func<bool>(this.AussaPsp));
			base.AddExecutor(ExecutorType.Activate, 97661969, new Func<bool>(this.AussaPeff));
			base.AddExecutor(ExecutorType.SpSummon, 73309655, new Func<bool>(this.EriaPsp));
			base.AddExecutor(ExecutorType.Activate, 73309655, new Func<bool>(this.EriaPeff));
			base.AddExecutor(ExecutorType.SpSummon, 30674956, new Func<bool>(this.WynnPsp));
			base.AddExecutor(ExecutorType.Activate, 30674956, new Func<bool>(this.WynnPeff));
			base.AddExecutor(ExecutorType.SpSummon, 48815792, new Func<bool>(this.HiitaPsp));
			base.AddExecutor(ExecutorType.Activate, 48815792, new Func<bool>(this.HiitaPeff));
			base.AddExecutor(ExecutorType.SpSummon, 9839945, new Func<bool>(this.LynaPsp));
			base.AddExecutor(ExecutorType.Activate, 9839945, new Func<bool>(this.LynaPeff));
			base.AddExecutor(ExecutorType.SpSummon, 41999284, new Func<bool>(this.Linkuribohsp));
			base.AddExecutor(ExecutorType.SpSummon, 98978921);
			base.AddExecutor(ExecutorType.SpSummon, 31833038, new Func<bool>(this.BorreloadDragonsp));
			base.AddExecutor(ExecutorType.Activate, 31833038, new Func<bool>(this.BorreloadDragoneff));
			base.AddExecutor(ExecutorType.SpSummon, 85289965, new Func<bool>(this.BirrelswordDragonsp));
			base.AddExecutor(ExecutorType.Activate, 85289965, new Func<bool>(this.BirrelswordDragoneff));
			base.AddExecutor(ExecutorType.Summon, 15397015, new Func<bool>(this.InspectBoardersummon));
			base.AddExecutor(ExecutorType.Summon, 36584821, new Func<bool>(this.GrenMajuDaEizosummon));
			base.AddExecutor(ExecutorType.SpSummon, 31833038, new Func<bool>(this.BorreloadDragonspsecond));
			base.AddExecutor(ExecutorType.Summon, 31887906, new Func<bool>(this.FamiliarPossessedsummon));
			base.AddExecutor(ExecutorType.Summon, 68881650, new Func<bool>(this.FamiliarPossessedsummon));
			base.AddExecutor(ExecutorType.Summon, 31764354, new Func<bool>(this.FamiliarPossessedsummon));
			base.AddExecutor(ExecutorType.Summon, 4376659, new Func<bool>(this.FamiliarPossessedsummon));
			base.AddExecutor(ExecutorType.Summon, 40542825, new Func<bool>(this.FamiliarPossessedsummon));
			base.AddExecutor(ExecutorType.Activate, 71197066, new Func<bool>(this.MetalSnakesp));
			base.AddExecutor(ExecutorType.Activate, 71197066, new Func<bool>(this.MetalSnakeeff));
			base.AddExecutor(ExecutorType.Activate, 36975314, new Func<bool>(this.Crackdowneff));
			base.AddExecutor(ExecutorType.Activate, 73915051, new Func<bool>(base.DefaultScapegoat));
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(base.DefaultMonsterRepos));
			base.AddExecutor(ExecutorType.SpellSet, new Func<bool>(this.SpellSet));
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x0006E81C File Offset: 0x0006CA1C
		public void SelectSTPlace(ClientCard card = null, bool avoid_Impermanence = false, List<int> avoid_list = null)
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
				if (base.Bot.SpellZone[seq] == null && (card == null || card.Location != CardLocation.Hand || !avoid_Impermanence) && (avoid_list == null || !avoid_list.Contains(seq)))
				{
					base.AI.SelectPlace(zone);
					return;
				}
			}
			base.AI.SelectPlace(0);
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x0006E92C File Offset: 0x0006CB2C
		public bool SpellNegatable(bool isCounter = false, ClientCard target = null)
		{
			if (target == null)
			{
				target = base.Card;
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

		// Token: 0x06001402 RID: 5122 RVA: 0x0006EA1A File Offset: 0x0006CC1A
		private bool MacroCosmoseff()
		{
			return (base.Duel.LastChainPlayer == 1 || base.Duel.LastSummonPlayer == 1 || base.Duel.Player == 0) && base.UniqueFaceupSpell();
		}

		// Token: 0x06001403 RID: 5123 RVA: 0x0006EA50 File Offset: 0x0006CC50
		private bool ImperialOrderfirst()
		{
			return (base.Util.GetLastChainCard() == null || !base.Util.GetLastChainCard().IsCode(35261759)) && base.DefaultOnBecomeTarget() && base.Util.GetLastChainCard().HasType(CardType.Spell);
		}

		// Token: 0x06001404 RID: 5124 RVA: 0x0006EAA0 File Offset: 0x0006CCA0
		private bool ImperialOrdereff()
		{
			if (base.Util.GetLastChainCard() != null && base.Util.GetLastChainCard().IsCode(35261759))
			{
				return false;
			}
			if (base.Duel.LastChainPlayer == 1)
			{
				foreach (ClientCard check in base.Enemy.GetSpells())
				{
					if (base.Util.GetLastChainCard() == check)
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x0006EB3C File Offset: 0x0006CD3C
		private bool PotOfDesireseff()
		{
			return base.Bot.Deck.Count > 14 && !base.DefaultSpellWillBeNegated();
		}

		// Token: 0x06001406 RID: 5126 RVA: 0x0006EB5D File Offset: 0x0006CD5D
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

		// Token: 0x06001407 RID: 5127 RVA: 0x0006EB88 File Offset: 0x0006CD88
		private bool Crackdowneff()
		{
			if (base.Util.GetOneEnemyBetterThanMyBest(true, true) != null && base.Bot.UnderAttack)
			{
				base.AI.SelectCard(base.Util.GetOneEnemyBetterThanMyBest(true, true));
			}
			return base.Util.GetOneEnemyBetterThanMyBest(true, true) != null && base.Bot.UnderAttack;
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x0006EBE5 File Offset: 0x0006CDE5
		private bool SkillDrainEffect()
		{
			return base.Bot.LifePoints > 1000 && base.DefaultUniqueTrap();
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x0006EC04 File Offset: 0x0006CE04
		private bool UnpossessedEffect()
		{
			base.AI.SelectCard(new List<int> { 40542825, 4376659, 31764354, 68881650, 31887906 });
			return true;
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x0006EC59 File Offset: 0x0006CE59
		private bool InspectBoardersummon()
		{
			if (base.Bot.MonsterZone[0] == null)
			{
				base.AI.SelectPlace(1);
			}
			else
			{
				base.AI.SelectPlace(16);
			}
			return true;
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x0006EC88 File Offset: 0x0006CE88
		private bool GrenMajuDaEizosummon()
		{
			if (base.Duel.Turn == 1)
			{
				return false;
			}
			if (base.Bot.HasInSpellZone(82732705, false, false) || base.Enemy.HasInSpellZone(82732705, false, false))
			{
				return false;
			}
			if (base.Bot.MonsterZone[0] == null)
			{
				base.AI.SelectPlace(1);
			}
			else
			{
				base.AI.SelectPlace(16);
			}
			return base.Bot.Banished.Count >= 6;
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x0006EC59 File Offset: 0x0006CE59
		private bool FamiliarPossessedsummon()
		{
			if (base.Bot.MonsterZone[0] == null)
			{
				base.AI.SelectPlace(1);
			}
			else
			{
				base.AI.SelectPlace(16);
			}
			return true;
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x0006ED10 File Offset: 0x0006CF10
		private bool BorreloadDragonsp()
		{
			if (!base.Bot.HasInMonstersZone(new int[] { 75452921, 2857636, 9839945, 48815792, 30674956, 73309655, 97661969 }, false, false, false))
			{
				return false;
			}
			IList<ClientCard> material_list = new List<ClientCard>();
			foreach (ClientCard monster in base.Bot.GetMonsters())
			{
				if (monster.IsCode(new int[] { 9839945, 48815792, 30674956, 73309655, 97661969, 75452921, 2857636, 98978921, 41999284 }))
				{
					material_list.Add(monster);
				}
				if (material_list.Count == 3)
				{
					break;
				}
			}
			if (material_list.Count >= 3)
			{
				base.AI.SelectMaterials(material_list, 0);
				return true;
			}
			return false;
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x0006EDD0 File Offset: 0x0006CFD0
		private bool BorreloadDragonspsecond()
		{
			if (!base.Bot.HasInMonstersZone(new int[] { 75452921, 2857636, 9839945, 48815792, 30674956, 73309655, 97661969 }, false, false, false))
			{
				return false;
			}
			IList<ClientCard> material_list = new List<ClientCard>();
			foreach (ClientCard monster in base.Bot.GetMonsters())
			{
				if (monster.IsCode(new int[] { 9839945, 48815792, 30674956, 73309655, 97661969, 75452921, 2857636, 98978921, 41999284 }))
				{
					material_list.Add(monster);
				}
				if (material_list.Count == 3)
				{
					break;
				}
			}
			if (material_list.Count >= 3)
			{
				base.AI.SelectMaterials(material_list, 0);
				return true;
			}
			return false;
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x0006EE90 File Offset: 0x0006D090
		public bool BorreloadDragoneff()
		{
			if (base.ActivateDescription == -1 && (base.Duel.Phase == DuelPhase.BattleStart || base.Duel.Phase == DuelPhase.End))
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

		// Token: 0x06001410 RID: 5136 RVA: 0x0006EF5C File Offset: 0x0006D15C
		private bool BirrelswordDragonsp()
		{
			IList<ClientCard> material_list = new List<ClientCard>();
			foreach (ClientCard i in base.Bot.GetMonsters())
			{
				if (i.IsCode(new int[] { 75452921, 2857636, 9839945, 48815792, 30674956, 73309655, 97661969 }))
				{
					material_list.Add(i);
					break;
				}
			}
			foreach (ClientCard j in base.Bot.GetMonsters())
			{
				if (j.IsCode(41999284) || j.Level == 1)
				{
					material_list.Add(j);
					if (material_list.Count == 3)
					{
						break;
					}
				}
			}
			if (material_list.Count == 3)
			{
				base.AI.SelectMaterials(material_list, 0);
				return true;
			}
			return false;
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x0006F058 File Offset: 0x0006D258
		private bool BirrelswordDragoneff()
		{
			if (base.ActivateDescription != base.Util.GetStringId(85289965, 0))
			{
				return true;
			}
			if (base.Util.IsChainTarget(base.Card) && base.Util.GetBestEnemyMonster(true, true) != null)
			{
				base.AI.SelectCard(base.Util.GetBestEnemyMonster(true, true));
				return true;
			}
			if (base.Duel.Player == 1 && base.Bot.BattlingMonster == base.Card)
			{
				base.AI.SelectCard(base.Enemy.BattlingMonster);
				return true;
			}
			if (base.Duel.Player == 1 && base.Bot.BattlingMonster != null && base.Enemy.BattlingMonster.Attack - base.Bot.BattlingMonster.Attack >= base.Bot.LifePoints)
			{
				base.AI.SelectCard(base.Enemy.BattlingMonster);
				return true;
			}
			if (base.Duel.Player == 0 && base.Duel.Phase == DuelPhase.BattleStart)
			{
				foreach (ClientCard check in base.Enemy.GetMonsters())
				{
					if (check.IsAttack())
					{
						base.AI.SelectCard(check);
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x0006F1D4 File Offset: 0x0006D3D4
		private bool MetalSnakesp()
		{
			if (base.ActivateDescription == base.Util.GetStringId(71197066, 0) && !base.Bot.HasInMonstersZone(71197066, false, false, false))
			{
				if (base.Duel.Player == 1 && base.Duel.Phase >= DuelPhase.BattleStart)
				{
					return base.Bot.Deck.Count >= 12;
				}
				if (base.Duel.Player == 0 && base.Duel.Phase >= DuelPhase.Main1)
				{
					return base.Bot.Deck.Count >= 12;
				}
			}
			return false;
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x0006F278 File Offset: 0x0006D478
		private bool MetalSnakeeff()
		{
			ClientCard target = base.Util.GetOneEnemyBetterThanMyBest(true, true);
			if (base.ActivateDescription == base.Util.GetStringId(71197066, 1) && target != null)
			{
				base.AI.SelectCard(new int[] { 9839945, 48815792, 30674956, 73309655, 65330383 });
				base.AI.SelectNextCard(target);
				return true;
			}
			return false;
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x0006F2DC File Offset: 0x0006D4DC
		private bool AussaPsp()
		{
			IList<ClientCard> material_list = new List<ClientCard>();
			foreach (ClientCard monster in base.Bot.GetMonsters())
			{
				if (monster.HasAttribute(CardAttribute.Earth) && !monster.IsCode(new int[]
				{
					75452921, 15397015, 36584821, 2857636, 9839945, 48815792, 30674956, 73309655, 97661969, 38342335,
					65330383, 31833038, 85289965
				}))
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
			if (base.Bot.HasInMonstersZone(97661969, false, false, false))
			{
				return false;
			}
			base.AI.SelectMaterials(material_list, 0);
			if (base.Bot.MonsterZone[0] == null && base.Bot.MonsterZone[2] == null && base.Bot.MonsterZone[5] == null)
			{
				base.AI.SelectPlace(32);
			}
			else
			{
				base.AI.SelectPlace(64);
			}
			return true;
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x0006F3E4 File Offset: 0x0006D5E4
		private bool AussaPeff()
		{
			base.AI.SelectCard(new int[] { 23434538, 31887906 });
			return true;
		}

		// Token: 0x06001416 RID: 5142 RVA: 0x0006F408 File Offset: 0x0006D608
		private bool EriaPsp()
		{
			IList<ClientCard> material_list = new List<ClientCard>();
			foreach (ClientCard monster in base.Bot.GetMonsters())
			{
				if (monster.HasAttribute(CardAttribute.Water) && !monster.IsCode(new int[]
				{
					75452921, 15397015, 36584821, 2857636, 9839945, 48815792, 30674956, 73309655, 97661969, 38342335,
					65330383, 31833038, 85289965
				}))
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
			if (base.Bot.HasInMonstersZone(73309655, false, false, false))
			{
				return false;
			}
			base.AI.SelectMaterials(material_list, 0);
			if (base.Bot.MonsterZone[0] == null && base.Bot.MonsterZone[2] == null && base.Bot.MonsterZone[5] == null)
			{
				base.AI.SelectPlace(32);
			}
			else
			{
				base.AI.SelectPlace(64);
			}
			return true;
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x0006F510 File Offset: 0x0006D710
		private bool EriaPeff()
		{
			base.AI.SelectCard(68881650);
			return true;
		}

		// Token: 0x06001418 RID: 5144 RVA: 0x0006F524 File Offset: 0x0006D724
		private bool WynnPsp()
		{
			IList<ClientCard> material_list = new List<ClientCard>();
			foreach (ClientCard monster in base.Bot.GetMonsters())
			{
				if (monster.HasAttribute(CardAttribute.Wind) && !monster.IsCode(new int[]
				{
					75452921, 15397015, 36584821, 2857636, 9839945, 48815792, 30674956, 73309655, 97661969, 38342335,
					65330383, 31833038, 85289965
				}))
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
			if (base.Bot.HasInMonstersZone(30674956, false, false, false))
			{
				return false;
			}
			base.AI.SelectMaterials(material_list, 0);
			if (base.Bot.MonsterZone[0] == null && base.Bot.MonsterZone[2] == null && base.Bot.MonsterZone[5] == null)
			{
				base.AI.SelectPlace(32);
			}
			else
			{
				base.AI.SelectPlace(64);
			}
			return true;
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x0006F62C File Offset: 0x0006D82C
		private bool WynnPeff()
		{
			base.AI.SelectCard(31764354);
			return true;
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x0006F640 File Offset: 0x0006D840
		private bool HiitaPsp()
		{
			IList<ClientCard> material_list = new List<ClientCard>();
			foreach (ClientCard monster in base.Bot.GetMonsters())
			{
				if (monster.HasAttribute(CardAttribute.Fire) && !monster.IsCode(new int[]
				{
					75452921, 15397015, 36584821, 2857636, 9839945, 48815792, 30674956, 73309655, 97661969, 38342335,
					65330383, 31833038, 85289965
				}))
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
			if (base.Bot.HasInMonstersZone(48815792, false, false, false))
			{
				return false;
			}
			base.AI.SelectMaterials(material_list, 0);
			if (base.Bot.MonsterZone[0] == null && base.Bot.MonsterZone[2] == null && base.Bot.MonsterZone[5] == null)
			{
				base.AI.SelectPlace(32);
			}
			else
			{
				base.AI.SelectPlace(64);
			}
			return true;
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x0006F748 File Offset: 0x0006D948
		private bool HiitaPeff()
		{
			base.AI.SelectCard(4376659);
			return true;
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x0006F75C File Offset: 0x0006D95C
		private bool LynaPsp()
		{
			IList<ClientCard> material_list = new List<ClientCard>();
			foreach (ClientCard monster in base.Bot.GetMonsters())
			{
				if (monster.HasAttribute(CardAttribute.Light) && !monster.IsCode(new int[]
				{
					75452921, 15397015, 36584821, 2857636, 9839945, 48815792, 30674956, 73309655, 97661969, 38342335,
					65330383, 31833038, 85289965
				}))
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
			if (base.Bot.HasInMonstersZone(9839945, false, false, false))
			{
				return false;
			}
			base.AI.SelectMaterials(material_list, 0);
			if (base.Bot.MonsterZone[0] == null && base.Bot.MonsterZone[2] == null && base.Bot.MonsterZone[5] == null)
			{
				base.AI.SelectPlace(32);
			}
			else
			{
				base.AI.SelectPlace(64);
			}
			return true;
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x0006F864 File Offset: 0x0006DA64
		private bool LynaPeff()
		{
			base.AI.SelectCard(40542825);
			return true;
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x0006F878 File Offset: 0x0006DA78
		private bool Linkuribohsp()
		{
			foreach (ClientCard c in base.Bot.GetMonsters())
			{
				if (c.Level == 1)
				{
					base.AI.SelectMaterials(c, 0);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x0006F8E8 File Offset: 0x0006DAE8
		private bool Knightmaresp()
		{
			int[] firstMats = new int[] { 75452921, 2857636 };
			if (base.Bot.MonsterZone.GetMatchingCardsCount((ClientCard card) => card.IsCode(firstMats)) >= 1)
			{
				return false;
			}
			foreach (ClientCard c in base.Bot.GetMonsters())
			{
				if (c.Level == 1)
				{
					base.AI.SelectMaterials(c, 0);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x0006F99C File Offset: 0x0006DB9C
		private bool Linkuriboheff()
		{
			return base.Duel.LastChainPlayer != 0 || !base.Util.GetLastChainCard().IsCode(41999284);
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x0006F9C8 File Offset: 0x0006DBC8
		private bool GagagaCowboySummon()
		{
			if (base.Enemy.LifePoints <= 800 || (base.Bot.GetMonsterCount() >= 4 && base.Enemy.LifePoints <= 1600))
			{
				base.AI.SelectPosition(CardPosition.FaceUpDefence);
				return true;
			}
			return false;
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x0006FA18 File Offset: 0x0006DC18
		private bool SpellSet()
		{
			return (!base.Card.IsCode(30241314) || !base.Bot.HasInSpellZone(30241314, false, false)) && (!base.Card.IsCode(25704359) || !base.Bot.HasInSpellZone(25704359, false, false)) && (!base.Card.IsCode(36975314) || !base.Bot.HasInSpellZone(36975314, false, false)) && (!base.Card.IsCode(82732705) || !base.Bot.HasInSpellZone(82732705, false, false)) && (!base.Card.IsCode(59305593) || !base.Bot.HasInSpellZone(59305593, false, false)) && (base.Card.IsCode(73915051) || (base.Card.HasType(CardType.Trap) && base.Bot.GetSpellCountWithoutField() < 4));
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x0006FB20 File Offset: 0x0006DD20
		public override ClientCard OnSelectAttacker(IList<ClientCard> attackers, IList<ClientCard> defenders)
		{
			for (int i = 0; i < attackers.Count; i++)
			{
				ClientCard attacker = attackers[i];
				if (attacker.IsCode(new int[] { 85289965, 31833038 }))
				{
					return attacker;
				}
			}
			return null;
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x0000763C File Offset: 0x0000583C
		public override bool OnSelectHand()
		{
			return true;
		}

		// Token: 0x0200030A RID: 778
		public class CardId
		{
			// Token: 0x04001866 RID: 6246
			public const int MetalSnake = 71197066;

			// Token: 0x04001867 RID: 6247
			public const int InspectBoarder = 15397015;

			// Token: 0x04001868 RID: 6248
			public const int AshBlossomAndJoyousSpring = 14558127;

			// Token: 0x04001869 RID: 6249
			public const int GrenMajuDaEizo = 36584821;

			// Token: 0x0400186A RID: 6250
			public const int MaxxC = 23434538;

			// Token: 0x0400186B RID: 6251
			public const int Aussa = 31887906;

			// Token: 0x0400186C RID: 6252
			public const int Eria = 68881650;

			// Token: 0x0400186D RID: 6253
			public const int Wynn = 31764354;

			// Token: 0x0400186E RID: 6254
			public const int Hiita = 4376659;

			// Token: 0x0400186F RID: 6255
			public const int Lyna = 40542825;

			// Token: 0x04001870 RID: 6256
			public const int Awakening = 62256492;

			// Token: 0x04001871 RID: 6257
			public const int Unpossessed = 25704359;

			// Token: 0x04001872 RID: 6258
			public const int NaturalExterio = 99916754;

			// Token: 0x04001873 RID: 6259
			public const int NaturalBeast = 33198837;

			// Token: 0x04001874 RID: 6260
			public const int SwordsmanLV7 = 37267041;

			// Token: 0x04001875 RID: 6261
			public const int RoyalDecreel = 51452091;

			// Token: 0x04001876 RID: 6262
			public const int HarpieFeatherDuster = 18144506;

			// Token: 0x04001877 RID: 6263
			public const int PotOfDesires = 35261759;

			// Token: 0x04001878 RID: 6264
			public const int PotofExtravagance = 49238328;

			// Token: 0x04001879 RID: 6265
			public const int Scapegoat = 73915051;

			// Token: 0x0400187A RID: 6266
			public const int MacroCosmos = 30241314;

			// Token: 0x0400187B RID: 6267
			public const int Crackdown = 36975314;

			// Token: 0x0400187C RID: 6268
			public const int ImperialOrder = 61740673;

			// Token: 0x0400187D RID: 6269
			public const int SolemnWarning = 84749824;

			// Token: 0x0400187E RID: 6270
			public const int SolemStrike = 40605147;

			// Token: 0x0400187F RID: 6271
			public const int SolemnJudgment = 41420027;

			// Token: 0x04001880 RID: 6272
			public const int SkillDrain = 82732705;

			// Token: 0x04001881 RID: 6273
			public const int Mistake = 59305593;

			// Token: 0x04001882 RID: 6274
			public const int BorreloadDragon = 31833038;

			// Token: 0x04001883 RID: 6275
			public const int BirrelswordDragon = 85289965;

			// Token: 0x04001884 RID: 6276
			public const int KnightmareGryphon = 65330383;

			// Token: 0x04001885 RID: 6277
			public const int KnightmareUnicorn = 38342335;

			// Token: 0x04001886 RID: 6278
			public const int KnightmarePhoenix = 2857636;

			// Token: 0x04001887 RID: 6279
			public const int KnightmareCerberus = 75452921;

			// Token: 0x04001888 RID: 6280
			public const int LinkSpider = 98978921;

			// Token: 0x04001889 RID: 6281
			public const int Linkuriboh = 41999284;

			// Token: 0x0400188A RID: 6282
			public const int GagagaCowboy = 12014404;

			// Token: 0x0400188B RID: 6283
			public const int AussaP = 97661969;

			// Token: 0x0400188C RID: 6284
			public const int EriaP = 73309655;

			// Token: 0x0400188D RID: 6285
			public const int WynnP = 30674956;

			// Token: 0x0400188E RID: 6286
			public const int HiitaP = 48815792;

			// Token: 0x0400188F RID: 6287
			public const int LynaP = 9839945;
		}
	}
}
