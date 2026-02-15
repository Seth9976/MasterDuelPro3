using System;
using System.Collections.Generic;
using System.Linq;
using YGOSharp.OCGWrapper;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x020002FA RID: 762
	[Deck("Exosister", "AI_Exosister", "Normal")]
	internal class ExosisterExecutor : DefaultExecutor
	{
		// Token: 0x06001331 RID: 4913 RVA: 0x00067928 File Offset: 0x00065B28
		public ExosisterExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.Activate, 59242457, new Func<bool>(this.ExosistersMagnificaActivateTrigger));
			base.AddExecutor(ExecutorType.Activate, 42741437, new Func<bool>(this.ExosisterMikailisActivate));
			base.AddExecutor(ExecutorType.Activate, 59242457, new Func<bool>(this.ExosistersMagnificaActivateBanish));
			base.AddExecutor(ExecutorType.Activate, 197042, new Func<bool>(this.ExosisterReturniaActivate));
			base.AddExecutor(ExecutorType.Activate, 77891946, new Func<bool>(this.ExosisterVadisActivate));
			base.AddExecutor(ExecutorType.Activate, 10045474, new Func<bool>(this.InfiniteImpermanenceActivate));
			base.AddExecutor(ExecutorType.Activate, 9272381);
			base.AddExecutor(ExecutorType.Activate, 14558127, new Func<bool>(this.AshBlossomActivate));
			base.AddExecutor(ExecutorType.Activate, 24224830, new Func<bool>(this.CalledbytheGraveActivate));
			base.AddExecutor(ExecutorType.Activate, new Func<bool>(this.DefaultExosisterTransform));
			base.AddExecutor(ExecutorType.Activate, 4408198, new Func<bool>(this.ExosisterArmentActivate));
			base.AddExecutor(ExecutorType.Activate, 23434538, new Func<bool>(this.MaxxCActivate));
			base.AddExecutor(ExecutorType.Activate, 84211599, new Func<bool>(this.PotofExtravaganceActivate));
			base.AddExecutor(ExecutorType.Activate, 16889337);
			base.AddExecutor(ExecutorType.Activate, 8728498, new Func<bool>(this.DonnerDaggerFurHireActivate));
			base.AddExecutor(ExecutorType.Activate, 78135071, new Func<bool>(this.ExosisterKaspitellActivate));
			base.AddExecutor(ExecutorType.Activate, 5530780, new Func<bool>(this.ExosisterGibrineActivate));
			base.AddExecutor(ExecutorType.Activate, 41524885, new Func<bool>(this.ExosisterAsophielActivate));
			base.AddExecutor(ExecutorType.Activate, 5352328, new Func<bool>(this.ExosisterSophiaActivate));
			base.AddExecutor(ExecutorType.Activate, 79858629, new Func<bool>(this.ExosisterIreneActivate));
			base.AddExecutor(ExecutorType.Activate, 43863925, new Func<bool>(this.ExosisterStellaActivate));
			base.AddExecutor(ExecutorType.Activate, 16474916, new Func<bool>(this.ExosisterElisActivate));
			base.AddExecutor(ExecutorType.Activate, 67972302, new Func<bool>(this.SakitamaActivate));
			base.AddExecutor(ExecutorType.Activate, 77913594, new Func<bool>(this.ExosisterPaxActivate));
			base.AddExecutor(ExecutorType.Activate, 43863925, new Func<bool>(this.ExosisterStellaSecondActivate));
			base.AddExecutor(ExecutorType.SpSummon, 9272381);
			base.AddExecutor(ExecutorType.SpSummon, 8728498, new Func<bool>(this.DonnerDaggerFurHireSpSummonCheck));
			base.AddExecutor(ExecutorType.SpSummon, 42741437, new Func<bool>(this.ExosisterMikailisAdvancedSpSummonCheck));
			base.AddExecutor(ExecutorType.SpSummon, 78135071, new Func<bool>(this.ExosisterKaspitellAdvancedSpSummonCheck));
			base.AddExecutor(ExecutorType.SpSummon, 78135071, new Func<bool>(this.ExosisterKaspitellSpSummonCheck));
			base.AddExecutor(ExecutorType.SpSummon, 42741437, new Func<bool>(this.ExosisterMikailisSpSummonCheck));
			base.AddExecutor(ExecutorType.SpSummon, 58858807, new Func<bool>(this.TellarknightConstellarCaduceusSpSummonCheck));
			base.AddExecutor(ExecutorType.SpSummon, 59242457, new Func<bool>(this.ExosistersMagnificaSpSummonCheck));
			base.AddExecutor(ExecutorType.SpSummon, 46772449, new Func<bool>(base.DefaultEvilswarmExcitonKnightSummon));
			base.AddExecutor(ExecutorType.Activate, 46772449, new Func<bool>(base.DefaultEvilswarmExcitonKnightEffect));
			base.AddExecutor(ExecutorType.Summon, 43863925, new Func<bool>(this.ExosisterAvoidMaxxCSummonCheck));
			base.AddExecutor(ExecutorType.Summon, 5352328, new Func<bool>(this.ExosisterAvoidMaxxCSummonCheck));
			base.AddExecutor(ExecutorType.Summon, 79858629, new Func<bool>(this.ExosisterAvoidMaxxCSummonCheck));
			base.AddExecutor(ExecutorType.Summon, 16474916, new Func<bool>(this.ExosisterAvoidMaxxCSummonCheck));
			base.AddExecutor(ExecutorType.Activate, 37343995, new Func<bool>(this.ExosisterMarthaActivate));
			base.AddExecutor(ExecutorType.Summon, 43863925, new Func<bool>(this.ExosisterStellaSummonCheck));
			base.AddExecutor(ExecutorType.Summon, 16889337, new Func<bool>(this.AratamaSummonCheck));
			base.AddExecutor(ExecutorType.Summon, new Func<bool>(this.ExosisterForElisSummonCheck));
			base.AddExecutor(ExecutorType.Summon, new Func<bool>(this.ForSakitamaSummonCheck));
			base.AddExecutor(ExecutorType.Summon, 79858629, new Func<bool>(this.ExosisterIreneSummonCheck));
			base.AddExecutor(ExecutorType.Summon, new Func<bool>(this.Level4SummonCheck));
			base.AddExecutor(ExecutorType.Summon, new Func<bool>(this.ExosisterForArmentSummonCheck));
			base.AddExecutor(ExecutorType.Summon, new Func<bool>(this.ForDonnerSummonCheck));
			base.AddExecutor(ExecutorType.Activate, 77913594, new Func<bool>(this.ExosisterPaxActivateForEndSearch));
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(base.DefaultMonsterRepos));
			base.AddExecutor(ExecutorType.SpellSet, new Func<bool>(this.SpellSetCheck));
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x0006808C File Offset: 0x0006628C
		public List<ClientCard> ShuffleCardList(List<ClientCard> list)
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

		// Token: 0x06001333 RID: 4915 RVA: 0x000680DC File Offset: 0x000662DC
		public ClientCard GetProblematicEnemyMonster(int attack = 0, bool canBeTarget = false)
		{
			List<ClientCard> floodagateList = (from c in base.Enemy.GetMonsters()
				where ((c != null) ? c.Data : null) != null && c.IsFloodgate() && c.IsFaceup() && (!canBeTarget || !c.IsShouldNotBeTarget())
				select c).ToList<ClientCard>();
			if (floodagateList.Count > 0)
			{
				floodagateList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				floodagateList.Reverse();
				return floodagateList[0];
			}
			List<ClientCard> dangerList = base.Enemy.MonsterZone.Where((ClientCard c) => ((c != null) ? c.Data : null) != null && c.IsMonsterDangerous() && c.IsFaceup() && (!canBeTarget || !c.IsShouldNotBeTarget())).ToList<ClientCard>();
			if (dangerList.Count > 0)
			{
				dangerList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				dangerList.Reverse();
				return dangerList[0];
			}
			List<ClientCard> invincibleList = base.Enemy.MonsterZone.Where((ClientCard c) => ((c != null) ? c.Data : null) != null && c.IsMonsterInvincible() && c.IsFaceup() && (!canBeTarget || !c.IsShouldNotBeTarget())).ToList<ClientCard>();
			if (invincibleList.Count > 0)
			{
				invincibleList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				invincibleList.Reverse();
				return invincibleList[0];
			}
			if (attack == 0)
			{
				attack = base.Util.GetBestAttack(base.Bot);
			}
			List<ClientCard> betterList = (from card in base.Enemy.MonsterZone.GetMonsters()
				where card.GetDefensePower() >= attack && card.IsAttack() && (!canBeTarget || !card.IsShouldNotBeTarget())
				select card).ToList<ClientCard>();
			if (betterList.Count > 0)
			{
				betterList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				betterList.Reverse();
				return betterList[0];
			}
			return null;
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x00068254 File Offset: 0x00066454
		public ClientCard GetProblematicEnemyCard(bool canBeTarget = false)
		{
			List<ClientCard> floodagateList = base.Enemy.MonsterZone.Where((ClientCard c) => ((c != null) ? c.Data : null) != null && !this.removeChosenList.Contains(c) && c.IsFloodgate() && c.IsFaceup() && (!canBeTarget || !c.IsShouldNotBeTarget())).ToList<ClientCard>();
			if (floodagateList.Count > 0)
			{
				floodagateList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				floodagateList.Reverse();
				return floodagateList[0];
			}
			List<ClientCard> problemEnemySpellList = base.Enemy.SpellZone.Where((ClientCard c) => ((c != null) ? c.Data : null) != null && !this.removeChosenList.Contains(c) && c.IsFloodgate() && c.IsFaceup() && (!canBeTarget || !c.IsShouldNotBeTarget())).ToList<ClientCard>();
			if (problemEnemySpellList.Count > 0)
			{
				return this.ShuffleCardList(problemEnemySpellList)[0];
			}
			List<ClientCard> dangerList = base.Enemy.MonsterZone.Where((ClientCard c) => ((c != null) ? c.Data : null) != null && !this.removeChosenList.Contains(c) && c.IsMonsterDangerous() && c.IsFaceup() && (!canBeTarget || !c.IsShouldNotBeTarget())).ToList<ClientCard>();
			if (dangerList.Count > 0 && (base.Duel.Player == 0 || (base.Duel.Phase > DuelPhase.Main1 && base.Duel.Phase < DuelPhase.Main2)))
			{
				dangerList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				dangerList.Reverse();
				return dangerList[0];
			}
			List<ClientCard> invincibleList = base.Enemy.MonsterZone.Where((ClientCard c) => ((c != null) ? c.Data : null) != null && !this.removeChosenList.Contains(c) && c.IsMonsterInvincible() && c.IsFaceup() && (!canBeTarget || !c.IsShouldNotBeTarget())).ToList<ClientCard>();
			if (invincibleList.Count > 0)
			{
				invincibleList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				invincibleList.Reverse();
				return invincibleList[0];
			}
			List<ClientCard> enemyMonsters = (from c in base.Enemy.GetMonsters()
				where !this.removeChosenList.Contains(c)
				select c).ToList<ClientCard>();
			if (enemyMonsters.Count > 0)
			{
				enemyMonsters.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				enemyMonsters.Reverse();
				foreach (ClientCard target in enemyMonsters)
				{
					if ((target.HasType(CardType.Fusion) || target.HasType(CardType.Ritual) || target.HasType(CardType.Synchro) || target.HasType(CardType.Xyz) || (target.HasType(CardType.Link) && target.LinkCount >= 2)) && (!canBeTarget || (!target.IsShouldNotBeTarget() && !target.IsShouldNotBeMonsterTarget())))
					{
						return target;
					}
				}
			}
			List<ClientCard> spells = (from c in base.Enemy.GetSpells()
				where c.IsFaceup() && !this.removeChosenList.Contains(c) && (c.HasType(CardType.Equip) || c.HasType(CardType.Pendulum) || c.HasType(CardType.Field) || c.HasType(CardType.Continuous))
				select c).ToList<ClientCard>();
			if (spells.Count > 0)
			{
				return this.ShuffleCardList(spells)[0];
			}
			return null;
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x000684F8 File Offset: 0x000666F8
		public ClientCard GetBestEnemyMonster(bool onlyFaceup = false, bool canBeTarget = false)
		{
			ClientCard card = this.GetProblematicEnemyMonster(0, canBeTarget);
			if (card != null)
			{
				return card;
			}
			card = base.Enemy.MonsterZone.GetHighestAttackMonster(canBeTarget);
			if (card != null)
			{
				return card;
			}
			List<ClientCard> monsters = base.Enemy.GetMonsters();
			if (monsters.Count > 0 && !onlyFaceup)
			{
				return this.ShuffleCardList(monsters)[0];
			}
			return null;
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x00068554 File Offset: 0x00066754
		public ClientCard GetBestEnemySpell(bool onlyFaceup = false, bool canBeTarget = false)
		{
			List<ClientCard> problemEnemySpellList = base.Enemy.SpellZone.Where((ClientCard c) => ((c != null) ? c.Data : null) != null && c.IsFloodgate() && c.IsFaceup() && (!canBeTarget || !c.IsShouldNotBeTarget())).ToList<ClientCard>();
			if (problemEnemySpellList.Count > 0)
			{
				return this.ShuffleCardList(problemEnemySpellList)[0];
			}
			List<ClientCard> spells = (from card in base.Enemy.GetSpells()
				where !card.IsFaceup() || !card.IsCode(15693423)
				select card).ToList<ClientCard>();
			List<ClientCard> faceUpList = spells.Where((ClientCard ecard) => ecard.IsFaceup() && (ecard.HasType(CardType.Continuous) || ecard.HasType(CardType.Field) || ecard.HasType(CardType.Pendulum))).ToList<ClientCard>();
			if (faceUpList.Count > 0)
			{
				return this.ShuffleCardList(faceUpList)[0];
			}
			if (spells.Count > 0 && !onlyFaceup)
			{
				return this.ShuffleCardList(spells)[0];
			}
			return null;
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x0006863C File Offset: 0x0006683C
		public ClientCard GetBestEnemyCard(bool onlyFaceup = false, bool canBeTarget = false, bool checkGrave = false)
		{
			ClientCard card = this.GetBestEnemyMonster(onlyFaceup, canBeTarget);
			if (card != null)
			{
				return card;
			}
			card = this.GetBestEnemySpell(onlyFaceup, canBeTarget);
			if (card != null)
			{
				return card;
			}
			if (!checkGrave || base.Enemy.Graveyard.Count <= 0)
			{
				return null;
			}
			List<ClientCard> graveMonsterList = base.Enemy.Graveyard.GetMatchingCards((ClientCard c) => c.IsMonster()).ToList<ClientCard>();
			if (graveMonsterList.Count > 0)
			{
				graveMonsterList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				graveMonsterList.Reverse();
				return graveMonsterList[0];
			}
			return this.ShuffleCardList(base.Enemy.Graveyard.ToList<ClientCard>())[0];
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x000686FC File Offset: 0x000668FC
		public int CheckRemainInDeck(int id)
		{
			for (int count = 1; count < 4; count++)
			{
				if (this.DeckCountTable[count].Contains(id))
				{
					return base.Bot.GetRemainingCount(id, count);
				}
			}
			return 0;
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x00068738 File Offset: 0x00066938
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

		// Token: 0x0600133A RID: 4922 RVA: 0x00068758 File Offset: 0x00066958
		public void CheckEnemyMoveGrave()
		{
			if (base.Duel.LastChainPlayer == 1)
			{
				ClientCard card = base.Util.GetLastChainCard();
				if (base.Duel.LastChainLocation == CardLocation.Grave && card.Location == CardLocation.Grave)
				{
					Logger.DebugWriteLine("===Exosister: enemy activate effect from GY.");
					this.enemyMoveGrave = true;
					return;
				}
				if (this.affectGraveCardIdList.Contains(card.Id))
				{
					Logger.DebugWriteLine("===Exosister: enemy activate effect that affect GY.");
					this.enemyMoveGrave = true;
					return;
				}
				using (IEnumerator<ClientCard> enumerator = base.Duel.LastChainTargets.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.Location == CardLocation.Grave)
						{
							Logger.DebugWriteLine("===Exosister: enemy target cards of GY.");
							this.enemyMoveGrave = true;
							break;
						}
					}
				}
			}
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x0006882C File Offset: 0x00066A2C
		public int CheckExosisterMentionCard(int id)
		{
			if (!this.ExosisterMentionTable.ContainsKey(id))
			{
				return 0;
			}
			return this.ExosisterMentionTable[id];
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x0006884C File Offset: 0x00066A4C
		public bool CheckLastChainShouldNegated()
		{
			ClientCard lastcard = base.Util.GetLastChainCard();
			return lastcard != null && lastcard.Controller == 1 && (!lastcard.IsMonster() || !lastcard.HasSetcode(74) || base.Duel.Phase != DuelPhase.Standby);
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x00068896 File Offset: 0x00066A96
		public bool CheckLessOperation()
		{
			return this.enemyActivateMaxxC && this.CheckAtAdvantage();
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x000688A8 File Offset: 0x00066AA8
		public bool CheckAtAdvantage()
		{
			if (this.GetProblematicEnemyMonster(0, false) == null)
			{
				if (base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup()))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x000688E8 File Offset: 0x00066AE8
		public bool CheckInDanger()
		{
			if (base.Duel.Phase > DuelPhase.Main1 && base.Duel.Phase < DuelPhase.Main2)
			{
				int totalAtk = 0;
				foreach (ClientCard i in base.Enemy.GetMonsters())
				{
					if (i.IsAttack() && !i.Attacked)
					{
						totalAtk += i.Attack;
					}
				}
				if (totalAtk >= base.Bot.LifePoints)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x00068988 File Offset: 0x00066B88
		public bool CheckAbleForXyz(ClientCard card)
		{
			return card.IsFaceup() && !card.HasType(CardType.Xyz) && !card.HasType(CardType.Link) && !card.HasType(CardType.Token) && card.Level == 4;
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x000689C4 File Offset: 0x00066BC4
		public bool CheckMarthaActivatable()
		{
			if (!this.marthaEffect1Activated && this.CheckCalledbytheGrave(37343995) == 0 && this.CheckRemainInDeck(16474916) > 0)
			{
				return !base.Bot.GetMonsters().Any((ClientCard card) => card.IsFacedown() || !card.HasType(CardType.Xyz));
			}
			return false;
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x00068A28 File Offset: 0x00066C28
		public List<ClientCard> CheckDangerousCardinEnemyGrave(bool onlyMonster = false)
		{
			return base.Enemy.Graveyard.GetMatchingCards((ClientCard card) => (!onlyMonster || card.IsMonster()) && card.HasSetcode(283)).ToList<ClientCard>();
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x00068A64 File Offset: 0x00066C64
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
			if (target.IsTrap() && (base.Enemy.HasInSpellZone(51452091, true, false) || base.Bot.HasInSpellZone(51452091, true, false)))
			{
				return true;
			}
			if (target.Location == CardLocation.SpellZone && (target.IsSpell() || target.IsTrap()))
			{
				int selfSeq = -1;
				for (int i = 0; i < 5; i++)
				{
					if (base.Bot.SpellZone[i] == base.Card)
					{
						selfSeq = i;
					}
				}
				if (this.infiniteImpermanenceList.Contains(selfSeq))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x00068BA0 File Offset: 0x00066DA0
		public bool CheckWhetherNegated(bool disablecheck = true)
		{
			if ((base.Card.IsSpell() || base.Card.IsTrap()) && this.SpellNegatable(false, null))
			{
				return true;
			}
			if (this.CheckCalledbytheGrave(base.Card.Id) > 0)
			{
				return true;
			}
			if (base.Card.IsMonster() && base.Card.Location == CardLocation.MonsterZone && base.Card.IsDefense())
			{
				if (base.Enemy.MonsterZone.GetFirstMatchingFaceupCard((ClientCard card) => card.IsCode(90590303) && card.IsDefense() && !card.IsDisabled()) == null)
				{
					if (base.Bot.MonsterZone.GetFirstMatchingFaceupCard((ClientCard card) => card.IsCode(90590303) && card.IsDefense() && !card.IsDisabled()) == null)
					{
						goto IL_00C8;
					}
				}
				return true;
			}
			IL_00C8:
			return disablecheck && base.Card.IsDisabled();
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x00068C88 File Offset: 0x00066E88
		public void SelectSTPlace(ClientCard card = null, bool avoidImpermanence = false, List<int> avoidList = null)
		{
			if (card == null)
			{
				card = base.Card;
			}
			List<int> list = new List<int>();
			for (int seq = 0; seq < 5; seq++)
			{
				if (base.Bot.SpellZone[seq] == null && (card == null || card.Location != CardLocation.Hand || !avoidImpermanence || !this.infiniteImpermanenceList.Contains(seq)) && (avoidList == null || !avoidList.Contains(seq)))
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
			if (avoidImpermanence)
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

		// Token: 0x06001346 RID: 4934 RVA: 0x00068E74 File Offset: 0x00067074
		public void SelectXyzMaterial(int num = 2, bool needExosister = false)
		{
			List<ClientCard> materialList = (from card in base.Bot.GetMonsters()
				where this.CheckAbleForXyz(card)
				select card).ToList<ClientCard>();
			if (materialList != null && materialList.Count<ClientCard>() < num)
			{
				return;
			}
			if (needExosister)
			{
				if (!materialList.Any((ClientCard card) => card.HasSetcode(370)))
				{
					return;
				}
			}
			List<ClientCard> selectedList = new List<ClientCard>();
			if (needExosister)
			{
				List<ClientCard> list = materialList.Where((ClientCard card) => card.HasSetcode(370)).ToList<ClientCard>();
				list.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				ClientCard firstSelect = list[0];
				selectedList.Add(firstSelect);
				materialList.Remove(firstSelect);
			}
			List<ClientCard> list2 = materialList.Where((ClientCard card) => (((card != null) ? card.Data : null) != null && !card.HasSetcode(370)) || (this.exosisterTransformEffectList.Contains(card.Id) && card.Id != 37343995)).ToList<ClientCard>();
			list2.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			foreach (ClientCard card3 in list2)
			{
				selectedList.Add(card3);
				if (selectedList.Count<ClientCard>() >= num)
				{
					base.AI.SelectMaterials(selectedList, 0);
					return;
				}
			}
			List<ClientCard> list3 = materialList.Where((ClientCard card) => card.Id == 37343995 || !this.exosisterTransformEffectList.Contains(card.Id)).ToList<ClientCard>();
			list3.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			foreach (ClientCard card2 in list3)
			{
				selectedList.Add(card2);
				if (selectedList.Count<ClientCard>() >= num)
				{
					base.AI.SelectMaterials(selectedList, 0);
					break;
				}
			}
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x0006903C File Offset: 0x0006723C
		public void SelectDetachMaterial(ClientCard activateCard)
		{
			base.AI.SelectCard(0);
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x0000763C File Offset: 0x0000583C
		public override bool OnSelectHand()
		{
			return true;
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x0006904C File Offset: 0x0006724C
		public override void OnChaining(int player, ClientCard card)
		{
			if (card == null)
			{
				return;
			}
			if (player == 1)
			{
				if (card.IsCode(23434538) && this.CheckCalledbytheGrave(23434538) == 0)
				{
					this.enemyActivateMaxxC = true;
				}
				if (card.IsCode(94145021) && this.CheckCalledbytheGrave(94145021) == 0)
				{
					this.enemyActivateLockBird = true;
				}
				if (card.IsCode(10045474))
				{
					for (int i = 0; i < 5; i++)
					{
						if (base.Enemy.SpellZone[i] == card)
						{
							this.infiniteImpermanenceList.Add(4 - i);
							break;
						}
					}
				}
				if (base.Duel.LastChainLocation == CardLocation.Grave && card.Location == CardLocation.Grave)
				{
					Logger.DebugWriteLine("===Exosister: enemy activate effect from GY.");
					this.enemyMoveGrave = true;
				}
			}
			base.OnChaining(player, card);
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x00069114 File Offset: 0x00067314
		public override void OnSelectChain(IList<ClientCard> cards)
		{
			int lastChainPlayer = base.Duel.LastChainPlayer;
			base.Util.GetLastChainCard();
			if (lastChainPlayer == 1)
			{
				using (IEnumerator<ClientCard> enumerator = base.Duel.LastChainTargets.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.Location == CardLocation.Grave)
						{
							Logger.DebugWriteLine("===Exosister: enemy target cards of GY.");
							this.enemyMoveGrave = true;
							break;
						}
					}
				}
			}
			base.OnSelectChain(cards);
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x0006919C File Offset: 0x0006739C
		public override void OnChainSolved(int chainIndex)
		{
			ChainInfo currentCard = base.Duel.GetCurrentSolvingChainInfo();
			if (currentCard != null && !base.Duel.IsCurrentSolvingChainNegated() && currentCard.ActivatePlayer == 1)
			{
				if (currentCard.IsCode(23434538))
				{
					this.enemyActivateMaxxC = true;
				}
				if (currentCard.IsCode(94145021))
				{
					this.enemyActivateLockBird = true;
				}
				if (currentCard.IsCode(10045474))
				{
					for (int i = 0; i < 5; i++)
					{
						if (base.Enemy.SpellZone[i] == currentCard.RelatedCard)
						{
							this.infiniteImpermanenceList.Add(4 - i);
							return;
						}
					}
				}
			}
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x00069234 File Offset: 0x00067434
		public override void OnChainEnd()
		{
			this.enemyMoveGrave = false;
			this.paxCallToField = false;
			this.potActivate = false;
			this.transformDestList.Clear();
			this.targetedMagnificaList.Clear();
			if (this.activatedMagnificaList.Count<ClientCard>() > 0)
			{
				for (int idx = this.activatedMagnificaList.Count<ClientCard>() - 1; idx >= 0; idx--)
				{
					ClientCard checkTarget = this.activatedMagnificaList[idx];
					if (checkTarget == null || checkTarget.IsFacedown() || checkTarget.Location != CardLocation.MonsterZone)
					{
						this.activatedMagnificaList.RemoveAt(idx);
					}
				}
			}
			if (this.spSummonThisTurn.Count<ClientCard>() > 0)
			{
				for (int idx2 = this.spSummonThisTurn.Count<ClientCard>() - 1; idx2 >= 0; idx2--)
				{
					ClientCard checkTarget2 = this.spSummonThisTurn[idx2];
					if (checkTarget2 == null || checkTarget2.IsFacedown() || checkTarget2.Location != CardLocation.MonsterZone)
					{
						this.spSummonThisTurn.RemoveAt(idx2);
					}
				}
			}
			base.OnChainEnd();
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x00069318 File Offset: 0x00067518
		public override void OnNewTurn()
		{
			this.enemyActivateMaxxC = false;
			this.enemyActivateLockBird = false;
			this.infiniteImpermanenceList.Clear();
			this.currentNegatingIdList.Clear();
			this.summoned = false;
			this.elisEffect1Activated = false;
			this.stellaEffect1Activated = false;
			this.irenaEffect1Activated = false;
			this.sophiaEffect1Activated = false;
			this.marthaEffect1Activated = false;
			this.mikailisEffect1Activated = false;
			this.mikailisEffect3Activated = false;
			this.kaspitellEffect1Activated = false;
			this.kaspitellEffect3Activated = false;
			this.gibrineEffect1Activated = false;
			this.gibrineEffect3Activated = false;
			this.asophielEffect1Activated = false;
			this.asophielEffect3Activated = false;
			this.sakitamaEffect1Activated = false;
			this.exosisterTransformEffectList.Clear();
			this.oncePerTurnEffectActivatedList.Clear();
			this.activatedMagnificaList.Clear();
			this.spSummonThisTurn.Clear();
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x000693E0 File Offset: 0x000675E0
		public override IList<ClientCard> OnSelectCard(IList<ClientCard> cards, int min, int max, int hint, bool cancelable)
		{
			bool beginTransformCheck = false;
			if (hint == 509 && min == 1 && max == 1 && this.transformDestList.Count<int>() > 0)
			{
				if (cards.All((ClientCard card) => card.Location == CardLocation.Extra && card.Rank == 4 && card.HasSetcode(370)))
				{
					beginTransformCheck = true;
				}
			}
			if (hint == 507 && min == 1 && max == 1 && this.transformDestList.Count<int>() > 0)
			{
				if (cards.All((ClientCard card) => card.Location == CardLocation.Overlay))
				{
					beginTransformCheck = true;
				}
			}
			if (beginTransformCheck)
			{
				for (int idx = 0; idx < this.transformDestList.Count<int>(); idx++)
				{
					int targetId = this.transformDestList[idx];
					ClientCard targetCard = cards.FirstOrDefault((ClientCard card) => card.IsCode(targetId));
					if (targetCard != null)
					{
						List<ClientCard> result = new List<ClientCard>();
						result.Add(targetCard);
						this.transformDestList.RemoveAt(idx);
						this.spSummonThisTurn.AddRange(result);
						return base.Util.CheckSelectCount(result, cards, min, max);
					}
				}
			}
			if (base.Util.ChainContainsCard(15693423) && base.Util.ChainContainPlayer(1) && hint == 503)
			{
				int num = base.Bot.GetMonsterCount() + base.Bot.GetSpellCount();
				int oppositeCount = base.Enemy.GetMonsterCount() + base.Enemy.GetSpellCount();
				if (num - oppositeCount == min && min == max)
				{
					Logger.DebugWriteLine("===Exosister: Evenly Matched activated.");
					List<ClientCard> allBotCards = new List<ClientCard>();
					allBotCards.AddRange(base.Bot.GetMonsters());
					allBotCards.AddRange(base.Bot.GetSpells());
					List<ClientCard> importantList = new List<ClientCard>();
					List<ClientCard> magnificaList = allBotCards.Where((ClientCard card) => card.IsCode(59242457)).ToList<ClientCard>();
					if (magnificaList.Count > 0)
					{
						allBotCards.RemoveAll((ClientCard c) => magnificaList.Contains(c));
						importantList.AddRange(magnificaList);
					}
					if (!this.mikailisEffect1Activated)
					{
						List<ClientCard> mikailisList = allBotCards.Where((ClientCard card) => this.spSummonThisTurn.Contains(card) && card.IsCode(42741437) && card.IsFaceup()).ToList<ClientCard>();
						if (mikailisList.Count > 0)
						{
							allBotCards.RemoveAll((ClientCard c) => mikailisList.Contains(c));
							importantList.AddRange(mikailisList);
						}
					}
					if (!this.gibrineEffect1Activated)
					{
						List<ClientCard> gibrineList = allBotCards.Where((ClientCard card) => this.spSummonThisTurn.Contains(card) && card.IsCode(5530780) && card.IsFaceup()).ToList<ClientCard>();
						if (gibrineList.Count > 0)
						{
							allBotCards.RemoveAll((ClientCard c) => gibrineList.Contains(c));
							importantList.AddRange(gibrineList);
						}
					}
					if (!this.oncePerTurnEffectActivatedList.Contains(77891946))
					{
						List<ClientCard> vadisList = allBotCards.Where((ClientCard card) => card.IsCode(77891946) && card.IsFacedown()).ToList<ClientCard>();
						if (vadisList.Count > 0)
						{
							allBotCards.RemoveAll((ClientCard c) => vadisList.Contains(c));
							importantList.AddRange(vadisList);
						}
					}
					List<ClientCard> xyzList = allBotCards.Where((ClientCard card) => card.IsMonster() && card.HasType(CardType.Xyz)).ToList<ClientCard>();
					if (xyzList.Count > 0)
					{
						xyzList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
						xyzList.Reverse();
						allBotCards.RemoveAll((ClientCard c) => xyzList.Contains(c));
						importantList.AddRange(xyzList);
					}
					List<ClientCard> monsterList = allBotCards.Where((ClientCard card) => card.IsMonster()).ToList<ClientCard>();
					if (monsterList.Count > 0)
					{
						monsterList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
						monsterList.Reverse();
						allBotCards.RemoveAll((ClientCard c) => monsterList.Contains(c));
						importantList.AddRange(monsterList);
					}
					List<ClientCard> faceDownList = allBotCards.Where((ClientCard card) => card.IsFacedown()).ToList<ClientCard>();
					if (faceDownList.Count > 0)
					{
						allBotCards.RemoveAll((ClientCard c) => faceDownList.Contains(c));
						importantList.AddRange(this.ShuffleCardList(faceDownList));
					}
					importantList.Reverse();
					return base.Util.CheckSelectCount(importantList, cards, min, max);
				}
			}
			return base.OnSelectCard(cards, min, max, hint, cancelable);
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x000698F4 File Offset: 0x00067AF4
		public override CardPosition OnSelectPosition(int cardId, IList<CardPosition> positions)
		{
			NamedCard cardData = NamedCard.Get(cardId);
			if (cardData != null)
			{
				if (base.Util.IsTurn1OrMain2())
				{
					bool turnDefense = false;
					if (cardId == 90448279 || cardId == 59242457)
					{
						turnDefense = true;
					}
					if (!cardData.HasType(CardType.Xyz))
					{
						turnDefense = true;
					}
					if (turnDefense)
					{
						return CardPosition.FaceUpDefence;
					}
				}
				if (base.Duel.Player == 1 && (!cardData.HasType(CardType.Xyz) || cardData.Defense >= cardData.Attack || base.Util.IsOneEnemyBetterThanValue(cardData.Attack, true)))
				{
					return CardPosition.FaceUpDefence;
				}
				int bestBotAttack = Math.Max(base.Util.GetBestAttack(base.Bot), cardData.Attack);
				if (base.Util.IsAllEnemyBetterThanValue(bestBotAttack, true))
				{
					return CardPosition.FaceUpDefence;
				}
			}
			return base.OnSelectPosition(cardId, positions);
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x000699B8 File Offset: 0x00067BB8
		public override bool OnSelectYesNo(int desc)
		{
			if (desc == base.Util.GetStringId(59242457, 2))
			{
				return true;
			}
			if (desc == base.Util.GetStringId(77913594, 1))
			{
				return this.paxCallToField;
			}
			return base.OnSelectYesNo(desc);
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x000699F4 File Offset: 0x00067BF4
		public override int OnSelectOption(IList<int> options)
		{
			int spSummonOption = -1;
			int banishOption = -1;
			int doNothingOption = -1;
			for (int idx = 0; idx < options.Count<int>(); idx++)
			{
				int option = options[idx];
				if (option == base.Util.GetStringId(197042, 0))
				{
					spSummonOption = idx;
				}
				else if (option == base.Util.GetStringId(197042, 1))
				{
					banishOption = idx;
				}
				else if (option == base.Util.GetStringId(197042, 2))
				{
					doNothingOption = idx;
				}
			}
			if (spSummonOption >= 0 || banishOption >= 0 || doNothingOption >= 0)
			{
				if (spSummonOption < 0 && banishOption < 0)
				{
					return doNothingOption;
				}
				if (banishOption >= 0)
				{
					ClientCard target = this.GetProblematicEnemyCard(true);
					if (target != null)
					{
						base.AI.SelectCard(target);
						return banishOption;
					}
					target = this.GetBestEnemyCard(false, false, false);
					if (target != null)
					{
						base.AI.SelectCard(target);
						return banishOption;
					}
				}
			}
			int potBanish6Option = -1;
			int potBanish3Option = -1;
			for (int idx2 = 0; idx2 < options.Count<int>(); idx2++)
			{
				int option2 = options[idx2];
				if (option2 == base.Util.GetStringId(84211599, 0))
				{
					potBanish3Option = idx2;
				}
				else if (option2 == base.Util.GetStringId(84211599, 1))
				{
					potBanish6Option = idx2;
				}
			}
			if (potBanish3Option < 0 && potBanish6Option < 0)
			{
				return base.OnSelectOption(options);
			}
			if (base.Bot.ExtraDeck.Count<ClientCard>() > 9 && potBanish6Option >= 0)
			{
				return potBanish6Option;
			}
			return potBanish3Option;
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x00069B50 File Offset: 0x00067D50
		public bool AshBlossomActivate()
		{
			return !this.CheckWhetherNegated(true) && this.CheckLastChainShouldNegated() && (base.Duel.LastChainPlayer != 1 || !base.Util.GetLastChainCard().IsCode(23434538) || !this.CheckAtAdvantage()) && base.DefaultAshBlossomAndJoyousSpring();
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x00069BA5 File Offset: 0x00067DA5
		public bool MaxxCActivate()
		{
			return !this.CheckWhetherNegated(true) && base.Duel.LastChainPlayer != 0 && base.DefaultMaxxC();
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x00069BC8 File Offset: 0x00067DC8
		public bool InfiniteImpermanenceActivate()
		{
			if (this.CheckWhetherNegated(true))
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
								this.infiniteImpermanenceList.Add(j);
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
					ClientCard target = this.GetProblematicEnemyMonster(0, true);
					base.Enemy.GetMonsters();
					base.AI.SelectCard(target);
					this.infiniteImpermanenceList.Add(this_seq);
					return true;
				}
			}
			if (LastChainCard == null || LastChainCard.Controller != 1 || LastChainCard.Location != CardLocation.MonsterZone || LastChainCard.IsDisabled() || LastChainCard.IsShouldNotBeTarget() || LastChainCard.IsShouldNotBeSpellTrapTarget())
			{
				return false;
			}
			if (base.Card.Location == CardLocation.SpellZone)
			{
				for (int l = 0; l < 5; l++)
				{
					if (base.Bot.SpellZone[l] == base.Card)
					{
						this.infiniteImpermanenceList.Add(l);
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
				List<ClientCard> monsters = base.Enemy.GetMonsters();
				monsters.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				monsters.Reverse();
				foreach (ClientCard card in monsters)
				{
					if (card.IsFaceup() && !card.IsShouldNotBeTarget() && !card.IsShouldNotBeSpellTrapTarget())
					{
						base.AI.SelectCard(card);
						return true;
					}
				}
			}
			return true;
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x00069F10 File Offset: 0x00068110
		public bool CalledbytheGraveActivate()
		{
			if (this.CheckWhetherNegated(true))
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
					if (base.Util.GetLastChainCard().IsCode(23434538) && this.CheckAtAdvantage())
					{
						return false;
					}
					if (code == 91800273)
					{
						return false;
					}
					ClientCard targetCard = base.Enemy.Graveyard.GetFirstMatchingCard((ClientCard card) => card.IsMonster() && card.IsOriginalCode(code));
					if (targetCard != null)
					{
						if (base.Card.Location != CardLocation.SpellZone)
						{
							this.SelectSTPlace(null, true, null);
						}
						base.AI.SelectCard(targetCard);
						this.currentNegatingIdList.Add(code);
						return true;
					}
				}
				foreach (ClientCard cards in base.Enemy.Graveyard)
				{
					if (base.Duel.ChainTargets.Contains(cards))
					{
						int code4 = cards.Id;
						base.AI.SelectCard(cards);
						this.currentNegatingIdList.Add(code4);
						return true;
					}
				}
				if (!base.Duel.ChainTargets.Contains(base.Card))
				{
					goto IL_01FC;
				}
				List<ClientCard> enemyMonsters = base.Enemy.Graveyard.GetMatchingCards((ClientCard card) => card.IsMonster()).ToList<ClientCard>();
				if (enemyMonsters.Count > 0)
				{
					enemyMonsters.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
					enemyMonsters.Reverse();
					int code2 = enemyMonsters[0].Id;
					base.AI.SelectCard(code2);
					this.currentNegatingIdList.Add(code2);
					return true;
				}
			}
			IL_01FC:
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

		// Token: 0x06001356 RID: 4950 RVA: 0x0006A190 File Offset: 0x00068390
		public List<ClientCard> GetPotofExtravaganceBanish()
		{
			List<ClientCard> banishList = new List<ClientCard>();
			ClientCard aaZeus = base.Bot.ExtraDeck.FirstOrDefault((ClientCard card) => card.IsCode(90448279));
			if (aaZeus != null)
			{
				banishList.Add(aaZeus);
			}
			ClientCard diamond = base.Bot.ExtraDeck.FirstOrDefault((ClientCard card) => card.IsCode(9272381));
			if (diamond != null)
			{
				banishList.Add(diamond);
			}
			ClientCard caduceus = base.Bot.ExtraDeck.FirstOrDefault((ClientCard card) => card.IsCode(58858807));
			if (caduceus != null)
			{
				banishList.Add(caduceus);
			}
			ClientCard evilswarm = base.Bot.ExtraDeck.FirstOrDefault((ClientCard card) => card.IsCode(46772449));
			if (evilswarm != null)
			{
				banishList.Add(evilswarm);
			}
			if (base.Bot.ExtraDeck.Count((ClientCard card) => card.IsCode(41524885)) > 1)
			{
				ClientCard asophiel2 = base.Bot.ExtraDeck.FirstOrDefault((ClientCard card) => card.IsCode(41524885));
				banishList.Add(asophiel2);
			}
			ClientCard gibrine = base.Bot.ExtraDeck.FirstOrDefault((ClientCard card) => card.IsCode(5530780));
			if (gibrine != null)
			{
				banishList.Add(gibrine);
			}
			if (base.Bot.ExtraDeck.Count((ClientCard card) => card.IsCode(42741437)) > 2)
			{
				ClientCard mikailis3 = base.Bot.ExtraDeck.FirstOrDefault((ClientCard card) => card.IsCode(42741437));
				banishList.Add(mikailis3);
			}
			ClientCard asophiel3 = base.Bot.ExtraDeck.FirstOrDefault((ClientCard card) => card.IsCode(41524885) && !banishList.Contains(card));
			if (asophiel3 != null)
			{
				banishList.Add(asophiel3);
			}
			ClientCard donner = base.Bot.ExtraDeck.FirstOrDefault((ClientCard card) => card.IsCode(8728498));
			if (donner != null)
			{
				banishList.Add(donner);
			}
			if (base.Bot.ExtraDeck.Count((ClientCard card) => card.IsCode(78135071)) > 1)
			{
				ClientCard kaspitell = base.Bot.ExtraDeck.FirstOrDefault((ClientCard card) => card.IsCode(78135071));
				banishList.Add(kaspitell);
			}
			if (base.Bot.ExtraDeck.Count((ClientCard card) => card.IsCode(59242457)) > 1)
			{
				ClientCard magnifica2 = base.Bot.ExtraDeck.FirstOrDefault((ClientCard card) => card.IsCode(59242457));
				banishList.Add(magnifica2);
			}
			if (base.Bot.ExtraDeck.Count((ClientCard card) => card.IsCode(42741437)) > 1)
			{
				ClientCard mikailis4 = base.Bot.ExtraDeck.FirstOrDefault((ClientCard card) => card.IsCode(42741437) && !banishList.Contains(card));
				banishList.Add(mikailis4);
			}
			ClientCard magnifica3 = base.Bot.ExtraDeck.FirstOrDefault((ClientCard card) => card.IsCode(59242457) && !banishList.Contains(card));
			if (magnifica3 != null)
			{
				banishList.Add(magnifica3);
			}
			ClientCard kaspitell2 = base.Bot.ExtraDeck.FirstOrDefault((ClientCard card) => card.IsCode(78135071) && !banishList.Contains(card));
			if (kaspitell2 != null)
			{
				banishList.Add(kaspitell2);
			}
			ClientCard mikailis5 = base.Bot.ExtraDeck.FirstOrDefault((ClientCard card) => card.IsCode(42741437) && !banishList.Contains(card));
			if (mikailis5 != null)
			{
				banishList.Add(mikailis5);
			}
			return banishList;
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x0006A610 File Offset: 0x00068810
		public bool PotofExtravaganceActivate()
		{
			if (this.CheckWhetherNegated(true))
			{
				return false;
			}
			List<ClientCard> banishList = this.GetPotofExtravaganceBanish();
			List<int> addToHandOrderList = new List<int>();
			if (this.CheckMarthaActivatable())
			{
				if (!base.Bot.HasInHand(37343995))
				{
					addToHandOrderList.Add(37343995);
				}
				if (base.Bot.HasInHand(37343995) && !base.Bot.HasInHandOrInSpellZone(24224830))
				{
					addToHandOrderList.Add(24224830);
				}
			}
			int exosisterCount = base.Bot.Hand.Count((ClientCard card) => ((card != null) ? card.Data : null) != null && card.HasSetcode(370));
			if (!this.stellaEffect1Activated && this.CheckCalledbytheGrave(43863925) == 0)
			{
				if (!base.Bot.HasInHand(43863925) && exosisterCount > 0)
				{
					addToHandOrderList.Add(43863925);
				}
				if (base.Bot.HasInHand(43863925) && exosisterCount == 0)
				{
					addToHandOrderList.AddRange(new List<int> { 5352328, 79858629, 43863925, 37343995, 16474916 });
				}
			}
			if (exosisterCount >= 0 && !base.Bot.HasInHandOrInSpellZone(197042))
			{
				addToHandOrderList.Add(197042);
			}
			List<int> remainOrderList = new List<int> { 16889337, 67972302, 23434538, 14558127, 10045474, 24224830, 77891946, 197042, 77913594 };
			addToHandOrderList.AddRange(remainOrderList);
			base.AI.SelectCard(banishList);
			base.AI.SelectNextCard(addToHandOrderList);
			if (base.Card.Location != CardLocation.SpellZone)
			{
				this.SelectSTPlace(null, true, null);
			}
			this.potActivate = true;
			return true;
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x0006A814 File Offset: 0x00068A14
		public bool SakitamaActivate()
		{
			if (base.Card.Location == CardLocation.Hand)
			{
				if (base.Bot.GetMonsters().Count((ClientCard card) => this.CheckAbleForXyz(card)) == 1)
				{
					base.AI.SelectCard(new int[] { 16889337, 67972302 });
					this.sakitamaEffect1Activated = true;
					return true;
				}
				if ((!this.CheckLessOperation() && base.Bot.HasInExtra(8728498) && !base.Bot.HasInHand(37343995)) || base.Bot.HasInHandOrInSpellZone(197042))
				{
					List<ClientCard> illegalList = (from card in base.Bot.GetMonsters()
						where card.IsFaceup() && !card.HasType(CardType.Xyz) && card.Level != 4 && (card.Data == null || !card.HasSetcode(370))
						select card).ToList<ClientCard>();
					if (illegalList.Count<ClientCard>() > 0)
					{
						if (illegalList.Count<ClientCard>() == 1)
						{
							List<ClientCard> otherMaterialList = (from card in base.Bot.GetMonsters()
								where card.IsFaceup() && !card.HasType(CardType.Xyz)
								select card).ToList<ClientCard>();
							otherMaterialList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
							illegalList.AddRange(otherMaterialList);
						}
						if (illegalList.Count<ClientCard>() == 1)
						{
							Logger.DebugWriteLine("===Exosister: activate sakitama for donner");
							base.AI.SelectCard(new int[] { 16889337, 67972302 });
							this.sakitamaEffect1Activated = true;
							return true;
						}
					}
				}
				return false;
			}
			else
			{
				if (base.Card.Location == CardLocation.Grave)
				{
					base.AI.SelectCard(new int[] { 67972302, 16889337 });
					return true;
				}
				return true;
			}
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x0006A9C8 File Offset: 0x00068BC8
		public bool DonnerDaggerFurHireActivate()
		{
			if (this.CheckAtAdvantage() && !base.Bot.HasInHand(37343995) && !base.Bot.HasInHandOrInSpellZone(197042))
			{
				return false;
			}
			ClientCard targetCard = base.Util.GetProblematicEnemyMonster(0, true);
			if (targetCard == null)
			{
				List<ClientCard> enemyMonsters = base.Enemy.GetMonsters();
				if (enemyMonsters.Count<ClientCard>() > 0)
				{
					enemyMonsters.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
					enemyMonsters.Reverse();
					targetCard = enemyMonsters[0];
				}
			}
			if (targetCard != null)
			{
				base.AI.SelectCard(base.Card);
				base.AI.SelectNextCard(targetCard);
				return true;
			}
			return false;
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x0006AA6C File Offset: 0x00068C6C
		public bool ExosisterElisActivate()
		{
			if (base.ActivateDescription != base.Util.GetStringId(16474916, 0))
			{
				return false;
			}
			if (base.Bot.GetMonsters().Count((ClientCard card) => this.CheckAbleForXyz(card)) == 1)
			{
				this.elisEffect1Activated = true;
				return true;
			}
			return false;
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x0006AABD File Offset: 0x00068CBD
		public bool ExosisterStellaActivate()
		{
			return this.ExosisterStellaActivateInner(true);
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x0006AAC6 File Offset: 0x00068CC6
		public bool ExosisterStellaSecondActivate()
		{
			return this.ExosisterStellaActivateInner(false);
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x0006AAD0 File Offset: 0x00068CD0
		public bool ExosisterStellaActivateInner(bool checkMartha = false)
		{
			if (base.ActivateDescription != base.Util.GetStringId(43863925, 0) || this.CheckWhetherNegated(true))
			{
				return false;
			}
			bool ableToXyz = base.Bot.GetMonsters().Count((ClientCard card) => this.CheckAbleForXyz(card)) >= 2;
			if (this.CheckLessOperation() && ableToXyz)
			{
				return false;
			}
			if (checkMartha && base.Bot.HasInHand(37343995) && ableToXyz)
			{
				if (base.Bot.Hand.Count((ClientCard card) => card.IsMonster() && card.HasSetcode(37343995)) == 1)
				{
					return false;
				}
			}
			base.AI.SelectCard(new int[] { 5352328, 79858629, 43863925, 16474916 });
			this.stellaEffect1Activated = true;
			return true;
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x0006ABA4 File Offset: 0x00068DA4
		public bool ExosisterIreneActivate()
		{
			if (base.ActivateDescription != base.Util.GetStringId(79858629, 0) || this.CheckWhetherNegated(true))
			{
				return false;
			}
			List<int> shuffleList = new List<int>();
			foreach (int cardId2 in new List<int> { 79858629, 5352328, 4408198 })
			{
				if (base.Bot.HasInHand(cardId2))
				{
					shuffleList.Add(cardId2);
				}
			}
			if (!this.elisEffect1Activated)
			{
				if (base.Bot.Hand.Count((ClientCard card) => card.IsCode(16474916)) <= 1)
				{
					goto IL_00D3;
				}
			}
			shuffleList.Add(16474916);
			IL_00D3:
			using (List<int>.Enumerator enumerator = new List<int> { 77913594, 197042, 77891946 }.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int cardId = enumerator.Current;
					if ((this.oncePerTurnEffectActivatedList.Contains(cardId) && base.Bot.HasInHand(cardId)) || base.Bot.Hand.Count((ClientCard card) => card.IsCode(cardId)) > 1)
					{
						shuffleList.Add(cardId);
					}
				}
			}
			if (shuffleList.Count<int>() > 0)
			{
				Logger.DebugWriteLine("===Exosister: irene return " + shuffleList[0].ToString());
				base.AI.SelectCard(shuffleList);
				return true;
			}
			return false;
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x0006AD84 File Offset: 0x00068F84
		public bool ExosisterSophiaActivate()
		{
			if (base.ActivateDescription == base.Util.GetStringId(5352328, 0) && !this.CheckWhetherNegated(true))
			{
				this.sophiaEffect1Activated = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x0006ADB2 File Offset: 0x00068FB2
		public bool ExosisterMarthaActivate()
		{
			if (base.ActivateDescription != base.Util.GetStringId(37343995, 0))
			{
				return false;
			}
			if (this.CheckLessOperation() && base.Bot.GetMonsterCount() > 0)
			{
				return false;
			}
			this.marthaEffect1Activated = true;
			return true;
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x0006ADF0 File Offset: 0x00068FF0
		public bool DefaultExosisterTransform()
		{
			List<int> canTransformList = new List<int> { 16474916, 43863925, 79858629, 5352328, 37343995 };
			if (base.Card.IsDisabled() || !canTransformList.Contains(base.Card.Id))
			{
				return false;
			}
			if (!new List<int>
			{
				base.Util.GetStringId(16474916, 1),
				base.Util.GetStringId(43863925, 1),
				base.Util.GetStringId(79858629, 1),
				base.Util.GetStringId(5352328, 1),
				base.Util.GetStringId(37343995, 1)
			}.Contains(base.ActivateDescription) && base.ActivateDescription != -1)
			{
				return false;
			}
			if (!base.Bot.HasInMonstersZone(42741437, false, false, false) && !this.mikailisEffect1Activated && (base.Duel.Player == 1 || !this.mikailisEffect3Activated) && !this.transformDestList.Contains(42741437) && base.Bot.HasInExtra(42741437))
			{
				this.exosisterTransformEffectList.Add(base.Card.Id);
				this.transformDestList.Add(42741437);
				return true;
			}
			if (!base.Bot.HasInMonstersZone(78135071, false, false, false) && !this.kaspitellEffect3Activated && base.Duel.Player == 0 && !this.transformDestList.Contains(78135071) && base.Bot.HasInExtra(78135071))
			{
				this.exosisterTransformEffectList.Add(base.Card.Id);
				this.transformDestList.Add(78135071);
				return true;
			}
			if (!base.Bot.HasInMonstersZone(5530780, false, false, false) && !this.gibrineEffect1Activated && !this.transformDestList.Contains(5530780) && base.Bot.HasInExtra(5530780))
			{
				this.exosisterTransformEffectList.Add(base.Card.Id);
				this.transformDestList.Add(5530780);
				return true;
			}
			if (!base.Bot.HasInMonstersZone(41524885, false, false, false) && !this.asophielEffect1Activated && !this.transformDestList.Contains(41524885) && base.Bot.HasInExtra(41524885))
			{
				this.exosisterTransformEffectList.Add(base.Card.Id);
				this.transformDestList.Add(41524885);
				return true;
			}
			if (!base.Bot.HasInMonstersZone(78135071, false, false, false) && !this.kaspitellEffect1Activated && !this.transformDestList.Contains(78135071) && base.Bot.HasInExtra(78135071))
			{
				this.exosisterTransformEffectList.Add(base.Card.Id);
				this.transformDestList.Add(78135071);
				return true;
			}
			return false;
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x0006B120 File Offset: 0x00069320
		public bool ExosisterMikailisActivate()
		{
			if (base.ActivateDescription == base.Util.GetStringId(42741437, 0))
			{
				if (base.Duel.Player == 0 && !this.mikailisEffect3Activated && base.Duel.Phase < DuelPhase.End && !base.DefaultOnBecomeTarget())
				{
					return false;
				}
				ClientCard target = this.GetProblematicEnemyCard(true);
				if (target != null && base.Duel.LastChainPlayer != 0)
				{
					this.removeChosenList.Add(target);
					this.mikailisEffect1Activated = true;
					base.AI.SelectCard(target);
					return true;
				}
				if (base.Duel.LastChainPlayer == 1)
				{
					List<ClientCard> targetList = base.Duel.LastChainTargets.Where((ClientCard card) => card.Controller == 1 && (card.Location == CardLocation.Grave || card.Location == CardLocation.MonsterZone || card.Location == CardLocation.SpellZone || card.Location == CardLocation.FieldZone)).ToList<ClientCard>();
					if (targetList.Count<ClientCard>() > 0)
					{
						this.mikailisEffect1Activated = true;
						base.AI.SelectCard(this.ShuffleCardList(targetList));
						return true;
					}
				}
				target = this.GetBestEnemyCard(false, true, true);
				if ((!base.DefaultOnBecomeTarget() || base.Util.ChainContainsCard(15693423)) && !base.Bot.UnderAttack && (base.Duel.Phase != DuelPhase.End || base.Duel.LastChainPlayer == 0))
				{
					if (base.Duel.Player == 0)
					{
						if (base.Bot.GetMonsters().Count((ClientCard card) => card.HasType(CardType.Xyz) && card.Rank == 4 && card.HasSetcode(370)) == 2 && base.Duel.LastChainPlayer != 0)
						{
							goto IL_01AB;
						}
					}
					if (base.Duel.Player != 1 || base.Enemy.GetMonsterCount() < 2)
					{
						return false;
					}
				}
				IL_01AB:
				this.mikailisEffect1Activated = true;
				base.AI.SelectCard(target);
				return true;
			}
			else
			{
				if (this.CheckWhetherNegated(true))
				{
					return false;
				}
				List<int> list = new List<int>();
				list.Add(197042);
				list.Add(77891946);
				list.Add(77913594);
				list.Add(4408198);
				List<int> firstSearchList = new List<int>();
				List<int> lastSearchList = new List<int>();
				foreach (int cardId in list)
				{
					if (base.Bot.HasInHandOrInSpellZone(cardId) || this.CheckRemainInDeck(cardId) == 0)
					{
						lastSearchList.Add(cardId);
					}
					else
					{
						if (cardId == 197042)
						{
							if (base.Bot.GetMonsters().Any((ClientCard card) => card.IsFacedown() || !card.HasSetcode(370)))
							{
								lastSearchList.Add(cardId);
								continue;
							}
						}
						firstSearchList.Add(cardId);
					}
				}
				firstSearchList.AddRange(lastSearchList);
				this.mikailisEffect3Activated = true;
				this.SelectDetachMaterial(base.Card);
				base.AI.SelectNextCard(firstSearchList);
				return true;
			}
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x0006B40C File Offset: 0x0006960C
		public bool ExosisterKaspitellActivate()
		{
			if (base.ActivateDescription == base.Util.GetStringId(78135071, 0) || base.ActivateDescription == -1)
			{
				if (base.Enemy.HasInMonstersZone(15397015, true, false, false))
				{
					return false;
				}
				this.kaspitellEffect1Activated = true;
				return true;
			}
			else
			{
				if (this.CheckWhetherNegated(true))
				{
					return false;
				}
				if (this.CheckMarthaActivatable() && this.CheckRemainInDeck(37343995) > 0 && !base.Bot.HasInHand(37343995))
				{
					this.kaspitellEffect3Activated = true;
					this.SelectDetachMaterial(base.Card);
					base.AI.SelectNextCard(37343995);
					return true;
				}
				if (!this.summoned && !this.sophiaEffect1Activated && this.CheckCalledbytheGrave(5352328) == 0 && !base.Bot.HasInHand(5352328) && (base.Bot.GetMonsters().Count((ClientCard card) => this.CheckAbleForXyz(card)) == 1 || (base.Bot.HasInHand(16474916) && !this.elisEffect1Activated)))
				{
					this.kaspitellEffect3Activated = true;
					this.SelectDetachMaterial(base.Card);
					base.AI.SelectNextCard(5352328);
					return true;
				}
				if (!this.summoned && !base.Bot.HasInHand(43863925) && !this.stellaEffect1Activated && this.CheckCalledbytheGrave(43863925) == 0 && this.CheckRemainInDeck(43863925) > 0)
				{
					if (base.Bot.Hand.Any((ClientCard card) => ((card != null) ? card.Data : null) != null && card.IsMonster() && card.HasSetcode(370)))
					{
						this.kaspitellEffect3Activated = true;
						this.SelectDetachMaterial(base.Card);
						base.AI.SelectNextCard(43863925);
						return true;
					}
				}
				this.kaspitellEffect3Activated = true;
				this.SelectDetachMaterial(base.Card);
				base.AI.SelectNextCard(new int[] { 37343995, 43863925, 16474916, 5352328, 79858629 });
				return true;
			}
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x0006B60C File Offset: 0x0006980C
		public bool ExosisterGibrineActivate()
		{
			if (base.ActivateDescription == base.Util.GetStringId(5530780, 0))
			{
				if (base.Duel.Player == 1)
				{
					ClientCard target = base.Enemy.MonsterZone.GetShouldBeDisabledBeforeItUseEffectMonster(true);
					if (target != null)
					{
						this.gibrineEffect1Activated = true;
						base.AI.SelectCard(target);
						return true;
					}
				}
				ClientCard LastChainCard = base.Util.GetLastChainCard();
				if (LastChainCard != null && LastChainCard.Controller == 1 && LastChainCard.Location == CardLocation.MonsterZone && !LastChainCard.IsDisabled() && !LastChainCard.IsShouldNotBeTarget() && !LastChainCard.IsShouldNotBeMonsterTarget())
				{
					this.gibrineEffect1Activated = true;
					base.AI.SelectCard(LastChainCard);
					return true;
				}
				return false;
			}
			else
			{
				if (this.CheckWhetherNegated(true))
				{
					return false;
				}
				this.gibrineEffect3Activated = true;
				this.SelectDetachMaterial(base.Card);
				return true;
			}
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x0006B6DC File Offset: 0x000698DC
		public bool ExosisterAsophielActivate()
		{
			if (base.ActivateDescription == base.Util.GetStringId(41524885, 0) || base.ActivateDescription == -1)
			{
				if (base.Enemy.HasInMonstersZone(15397015, true, false, false))
				{
					return false;
				}
				this.asophielEffect1Activated = true;
				return true;
			}
			else
			{
				if (this.CheckWhetherNegated(true))
				{
					return false;
				}
				ClientCard targetCard = base.Util.GetProblematicEnemyMonster(0, true);
				if (targetCard != null)
				{
					this.asophielEffect3Activated = true;
					this.SelectDetachMaterial(base.Card);
					base.AI.SelectNextCard(targetCard);
					return true;
				}
				return false;
			}
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x0006B76C File Offset: 0x0006996C
		public bool ExosistersMagnificaActivateTrigger()
		{
			if (base.ActivateDescription == base.Util.GetStringId(59242457, 1))
			{
				if (this.activatedMagnificaList.Contains(base.Card))
				{
					if (base.Card.Overlays.Contains(42741437) && !this.mikailisEffect1Activated && this.GetProblematicEnemyCard(true) != null && !base.Duel.CurrentChain.Any((ClientCard card) => card == base.Card))
					{
						this.transformDestList.Add(42741437);
						return true;
					}
					if (base.Card.Overlays.Contains(5530780) && !this.gibrineEffect1Activated && base.Enemy.MonsterZone.GetShouldBeDisabledBeforeItUseEffectMonster(true) != null)
					{
						this.transformDestList.Add(5530780);
						return true;
					}
				}
				if ((base.DefaultOnBecomeTarget() && !base.Util.ChainContainsCard(15693423)) || (base.Duel.CurrentChain.Any((ClientCard c) => c == base.Card) && base.Duel.LastChainPlayer != 0))
				{
					this.targetedMagnificaList.Add(base.Card);
					this.transformDestList.AddRange(new List<int> { 59242457, 42741437, 5530780, 78135071, 41524885 });
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x0006B8E8 File Offset: 0x00069AE8
		public bool ExosistersMagnificaActivateBanish()
		{
			if (base.ActivateDescription != base.Util.GetStringId(59242457, 0))
			{
				return false;
			}
			if (this.CheckWhetherNegated(true))
			{
				return false;
			}
			ClientCard target = this.GetProblematicEnemyCard(false);
			bool isProblemCard = false;
			if (target != null)
			{
				isProblemCard = true;
				Logger.DebugWriteLine("===Exosister: magnifica target 1: " + ((target != null) ? target.Name : null));
			}
			if (base.Duel.LastChainPlayer == 1 && target == null)
			{
				List<ClientCard> currentTargetList = base.Duel.LastChainTargets.Where((ClientCard card) => card.Controller == 1 && (card.Location == CardLocation.MonsterZone || card.Location == CardLocation.SpellZone || card.Location == CardLocation.FieldZone)).ToList<ClientCard>();
				if (currentTargetList.Count<ClientCard>() > 0)
				{
					target = this.ShuffleCardList(currentTargetList)[0];
					Logger.DebugWriteLine("===Exosister: magnifica target 2: " + ((target != null) ? target.Name : null));
				}
			}
			if (target == null)
			{
				target = this.GetBestEnemyCard(false, false, false);
				bool check = !base.DefaultOnBecomeTarget() || base.Util.ChainContainsCard(15693423);
				bool check2 = !this.targetedMagnificaList.Contains(base.Card);
				bool check3 = !base.Bot.UnderAttack;
				bool check4 = base.Duel.Phase != DuelPhase.End;
				bool check5 = base.Duel.Player == 0 || base.Enemy.GetMonsterCount() < 2;
				Logger.DebugWriteLine(string.Concat(new string[]
				{
					"===Exosister: magnifica check flag: ",
					check.ToString(),
					" ",
					check2.ToString(),
					" ",
					check3.ToString(),
					" ",
					check4.ToString(),
					" ",
					check5.ToString()
				}));
				if (check && check2 && check3 && check4 && check5)
				{
					target = null;
				}
			}
			if (target != null && (base.Duel.LastChainPlayer != 0 || base.Util.GetLastChainCard() == base.Card))
			{
				if (isProblemCard)
				{
					this.removeChosenList.Add(target);
				}
				Logger.DebugWriteLine("===Exosister: magnifica target final: " + ((target != null) ? target.Name : null));
				this.activatedMagnificaList.Add(base.Card);
				base.AI.SelectCard(new int[] { 5530780, 41524885, 78135071, 42741437 });
				base.AI.SelectNextCard(target);
				return true;
			}
			return false;
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x0006BB54 File Offset: 0x00069D54
		public bool ExosisterPaxActivate()
		{
			if (this.potActivate || base.Bot.LifePoints <= 800)
			{
				return false;
			}
			List<int> checkListForSpSummon = new List<int> { 5352328, 79858629, 43863925, 37343995, 16474916 };
			List<int> checkListForSearch = new List<int> { 37343995, 43863925, 77891946, 197042, 5352328, 79858629, 4408198, 16474916 };
			if (base.Duel.Player == 0 && base.Duel.LastChainPlayer != 0)
			{
				if (this.CheckAtAdvantage() && this.GetProblematicEnemyCard(true) != null && this.CheckRemainInDeck(197042) > 0 && !base.Bot.HasInHandOrInSpellZone(197042) && base.Bot.GetMonsterCount() > 0)
				{
					if (base.Bot.GetMonsters().All((ClientCard card) => card.IsFaceup() && card.HasSetcode(370)))
					{
						if (base.Card.Location != CardLocation.SpellZone)
						{
							this.SelectSTPlace(null, true, null);
						}
						this.oncePerTurnEffectActivatedList.Add(base.Card.Id);
						base.AI.SelectCard(197042);
						this.paxCallToField = false;
						return true;
					}
				}
				if (this.CheckMarthaActivatable() && this.CheckRemainInDeck(37343995) > 0 && !base.Bot.HasInHand(37343995))
				{
					if (base.Card.Location != CardLocation.SpellZone)
					{
						this.SelectSTPlace(null, true, null);
					}
					this.oncePerTurnEffectActivatedList.Add(base.Card.Id);
					base.AI.SelectCard(37343995);
					this.paxCallToField = false;
					return true;
				}
				if (!this.stellaEffect1Activated && this.CheckCalledbytheGrave(43863925) == 0)
				{
					if (base.Bot.Hand.Count((ClientCard card) => card.IsCode(43863925)) == 0 && this.CheckRemainInDeck(43863925) > 0)
					{
						bool shouldSpSummon = !this.CheckLessOperation() && this.summoned && base.Bot.HasInMonstersZoneOrInGraveyard(16474916);
						if (base.Bot.Hand.Any((ClientCard card) => ((card != null) ? card.Data : null) != null && card.IsMonster() && card.HasSetcode(370)))
						{
							if (base.Card.Location != CardLocation.SpellZone)
							{
								this.SelectSTPlace(null, true, null);
							}
							this.oncePerTurnEffectActivatedList.Add(base.Card.Id);
							base.AI.SelectCard(43863925);
							this.paxCallToField = shouldSpSummon;
							return true;
						}
					}
					bool searchExosisterMonster = false;
					if (base.Bot.HasInHand(43863925))
					{
						if (base.Bot.Hand.Count((ClientCard card) => ((card != null) ? card.Data : null) != null && card.IsMonster() && card.HasSetcode(370)) == 1)
						{
							searchExosisterMonster = true;
						}
					}
					if (base.Bot.HasInMonstersZone(43863925, false, false, false))
					{
						if (base.Bot.Hand.Count((ClientCard card) => ((card != null) ? card.Data : null) != null && card.IsMonster() && card.HasSetcode(370)) == 0)
						{
							searchExosisterMonster = true;
						}
					}
					if (searchExosisterMonster)
					{
						if (base.Card.Location != CardLocation.SpellZone)
						{
							this.SelectSTPlace(null, true, null);
						}
						this.oncePerTurnEffectActivatedList.Add(base.Card.Id);
						base.AI.SelectCard(new int[] { 5352328, 79858629, 37343995, 43863925, 16474916 });
						this.paxCallToField = false;
						return true;
					}
				}
				if (base.Bot.GetMonsters().Count((ClientCard card) => this.CheckAbleForXyz(card)) == 1 && this.summoned && !this.CheckLessOperation() && (this.sakitamaEffect1Activated || !base.Bot.HasInHand(67972302)) && (this.stellaEffect1Activated || !base.Bot.HasInMonstersZone(43863925, false, false, false)) && (this.elisEffect1Activated || !base.Bot.HasInHand(16474916)))
				{
					foreach (int checkId in checkListForSpSummon)
					{
						int checkTarget = this.CheckExosisterMentionCard(checkId);
						if (checkTarget > 0 && base.Bot.HasInMonstersZoneOrInGraveyard(checkId) && this.CheckRemainInDeck(checkTarget) > 0)
						{
							if (base.Card.Location != CardLocation.SpellZone)
							{
								this.SelectSTPlace(null, true, null);
							}
							this.oncePerTurnEffectActivatedList.Add(base.Card.Id);
							base.AI.SelectCard(checkId);
							this.paxCallToField = true;
							return true;
						}
					}
				}
			}
			bool inDanger = this.CheckInDanger();
			this.CheckEnemyMoveGrave();
			bool forReturnia = false;
			if (!this.oncePerTurnEffectActivatedList.Contains(197042) && base.Bot.HasInSpellZone(197042, false, false) && base.Bot.GetMonsters().Count<ClientCard>() == 0)
			{
				forReturnia = true;
			}
			if (this.enemyMoveGrave || base.DefaultOnBecomeTarget() || inDanger || forReturnia)
			{
				List<int> checkList = checkListForSpSummon;
				bool shouldSpSummon2 = this.enemyMoveGrave || inDanger || forReturnia;
				if (!shouldSpSummon2 && !base.Bot.HasInMonstersZone(new List<int> { 16474916, 43863925, 79858629, 5352328, 37343995 }, false, false, false))
				{
					shouldSpSummon2 = true;
				}
				if (this.CheckAtAdvantage() && !this.enemyMoveGrave)
				{
					shouldSpSummon2 = false;
					checkList = checkListForSearch;
				}
				foreach (int checkId2 in checkList)
				{
					bool checkSuccessFlag;
					if (shouldSpSummon2)
					{
						int checkTarget2 = this.CheckExosisterMentionCard(checkId2);
						checkSuccessFlag = checkTarget2 > 0 && base.Bot.HasInMonstersZoneOrInGraveyard(checkTarget2) && this.CheckRemainInDeck(checkId2) > 0 && !this.exosisterTransformEffectList.Contains(checkId2) && !base.Bot.HasInMonstersZone(checkId2, false, false, false);
					}
					else
					{
						checkSuccessFlag = !base.Bot.HasInHandOrHasInMonstersZone(checkId2) && !base.Bot.HasInSpellZone(checkId2, false, false) && this.CheckRemainInDeck(checkId2) > 0;
					}
					if (checkSuccessFlag)
					{
						if (base.Card.Location != CardLocation.SpellZone)
						{
							this.SelectSTPlace(null, true, null);
						}
						this.oncePerTurnEffectActivatedList.Add(base.Card.Id);
						base.AI.SelectCard(checkId2);
						this.paxCallToField = shouldSpSummon2;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x0006C284 File Offset: 0x0006A484
		public bool ExosisterPaxActivateForEndSearch()
		{
			if (this.potActivate || base.Bot.LifePoints <= 800)
			{
				return false;
			}
			if (base.Duel.Player == 0 || base.Duel.Phase >= DuelPhase.End)
			{
				foreach (int checkId in new List<int> { 77891946, 37343995, 197042, 43863925, 5352328, 79858629, 4408198, 16474916 })
				{
					if (!base.Bot.HasInHandOrHasInMonstersZone(checkId) && !base.Bot.HasInSpellZone(checkId, false, false) && this.CheckRemainInDeck(checkId) > 0)
					{
						if (base.Card.Location != CardLocation.SpellZone)
						{
							this.SelectSTPlace(null, true, null);
						}
						this.oncePerTurnEffectActivatedList.Add(base.Card.Id);
						base.AI.SelectCard(new int[] { 5352328, 79858629, 37343995, 43863925, 16474916 });
						this.paxCallToField = false;
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x0006C3EC File Offset: 0x0006A5EC
		public bool ExosisterArmentActivate()
		{
			if (base.Bot.LifePoints <= 800)
			{
				return false;
			}
			ClientCard activateTarget = null;
			if (base.Duel.Player == 0)
			{
				bool decided = false;
				if (base.Bot.GetMonsters().Count((ClientCard card) => this.CheckAbleForXyz(card)) == 1 && this.summoned && !this.CheckLessOperation() && (this.sakitamaEffect1Activated || !base.Bot.HasInHand(67972302)) && (this.stellaEffect1Activated || !base.Bot.HasInMonstersZone(43863925, false, false, false)) && (this.elisEffect1Activated || !base.Bot.HasInHand(16474916)))
				{
					decided = true;
				}
				if (base.Duel.LastChainPlayer == 1)
				{
					foreach (ClientCard target in base.Duel.LastChainTargets)
					{
						if (target.Controller == 0 && target.Location == CardLocation.MonsterZone && target.IsFaceup() && target.HasSetcode(370))
						{
							activateTarget = target;
							decided = true;
							break;
						}
					}
				}
				if (!decided)
				{
					return false;
				}
			}
			if (activateTarget == null && base.Duel.LastChainPlayer == 1)
			{
				foreach (ClientCard target2 in base.Duel.LastChainTargets)
				{
					if (target2.Controller == 0 && target2.Location == CardLocation.MonsterZone && target2.IsFaceup() && target2.HasSetcode(370))
					{
						activateTarget = target2;
						break;
					}
				}
			}
			if (activateTarget == null)
			{
				List<ClientCard> targetList = (from card in base.Bot.GetMonsters()
					where card.IsFaceup() && card.HasSetcode(370) && !card.HasType(CardType.Xyz)
					select card).ToList<ClientCard>();
				if (targetList.Count<ClientCard>() > 0)
				{
					targetList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
					activateTarget = targetList[0];
				}
			}
			if (activateTarget == null)
			{
				return false;
			}
			if (!base.Bot.HasInMonstersZone(42741437, false, false, false) && !this.mikailisEffect1Activated && (base.Duel.Player == 1 || !this.mikailisEffect3Activated) && !this.transformDestList.Contains(42741437) && base.Bot.HasInExtra(42741437))
			{
				if (base.Card.Location != CardLocation.SpellZone)
				{
					this.SelectSTPlace(null, true, null);
				}
				base.AI.SelectCard(activateTarget);
				this.transformDestList.Add(42741437);
				return true;
			}
			if (!base.Bot.HasInMonstersZone(78135071, false, false, false) && !this.kaspitellEffect3Activated && base.Duel.Player == 0 && !this.transformDestList.Contains(78135071) && base.Bot.HasInExtra(78135071))
			{
				if (base.Card.Location != CardLocation.SpellZone)
				{
					this.SelectSTPlace(null, true, null);
				}
				base.AI.SelectCard(activateTarget);
				this.transformDestList.Add(78135071);
				return true;
			}
			if (!base.Bot.HasInMonstersZone(5530780, false, false, false) && !this.gibrineEffect1Activated && !this.transformDestList.Contains(5530780) && base.Bot.HasInExtra(5530780))
			{
				if (base.Card.Location != CardLocation.SpellZone)
				{
					this.SelectSTPlace(null, true, null);
				}
				base.AI.SelectCard(activateTarget);
				this.transformDestList.Add(5530780);
				return true;
			}
			if (!base.Bot.HasInMonstersZone(41524885, false, false, false) && !this.asophielEffect1Activated && !this.transformDestList.Contains(41524885) && base.Bot.HasInExtra(41524885))
			{
				if (base.Card.Location != CardLocation.SpellZone)
				{
					this.SelectSTPlace(null, true, null);
				}
				base.AI.SelectCard(activateTarget);
				this.transformDestList.Add(41524885);
				return true;
			}
			if (!base.Bot.HasInMonstersZone(78135071, false, false, false) && !this.kaspitellEffect1Activated && !this.transformDestList.Contains(78135071) && base.Bot.HasInExtra(78135071))
			{
				if (base.Card.Location != CardLocation.SpellZone)
				{
					this.SelectSTPlace(null, true, null);
				}
				base.AI.SelectCard(activateTarget);
				this.transformDestList.Add(78135071);
				return true;
			}
			return false;
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x0006C874 File Offset: 0x0006AA74
		public bool ExosisterVadisActivate()
		{
			if (base.Bot.LifePoints <= 800)
			{
				return false;
			}
			List<int> checkListForSpSummon = new List<int> { 5352328, 79858629, 43863925, 37343995, 16474916 };
			bool decideToActivate = false;
			bool checkTransform = false;
			if (base.Duel.Player == 0 && base.Duel.Phase > DuelPhase.Draw && !this.CheckLessOperation())
			{
				decideToActivate = true;
			}
			this.CheckEnemyMoveGrave();
			if (this.enemyMoveGrave)
			{
				decideToActivate = true;
				checkTransform = true;
			}
			if (!this.oncePerTurnEffectActivatedList.Contains(197042) && base.Bot.HasInSpellZone(197042, false, false) && base.Bot.GetMonsters().Count<ClientCard>() == 0)
			{
				decideToActivate = true;
			}
			if (this.CheckInDanger() || (base.DefaultOnBecomeTarget() && !base.Util.ChainContainsCard(15693423)))
			{
				decideToActivate = true;
			}
			if (decideToActivate)
			{
				foreach (int checkId in checkListForSpSummon)
				{
					int checkTarget = this.CheckExosisterMentionCard(checkId);
					if (checkTarget > 0 && this.CheckRemainInDeck(checkId) > 0 && this.CheckRemainInDeck(checkTarget) > 0)
					{
						if (checkTransform)
						{
							int canTransformCount = 0;
							foreach (int num in new List<int> { checkId, checkTarget })
							{
								if (!base.Bot.HasInMonstersZone(checkId, false, false, false) && !this.exosisterTransformEffectList.Contains(checkId))
								{
									canTransformCount++;
								}
							}
							if (canTransformCount == 0)
							{
								continue;
							}
						}
						this.oncePerTurnEffectActivatedList.Add(base.Card.Id);
						Logger.DebugWriteLine("Exosiseter Vadis decide: " + checkId.ToString());
						base.AI.SelectCard(checkId);
						base.AI.SelectNextCard(checkTarget);
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x0006CAC4 File Offset: 0x0006ACC4
		public bool ExosisterReturniaActivate()
		{
			if (base.Bot.LifePoints <= 800)
			{
				return false;
			}
			ClientCard target = this.GetProblematicEnemyCard(true);
			if (target != null && base.Duel.LastChainPlayer != 0)
			{
				Logger.DebugWriteLine("===Exosister: returnia target 1: " + ((target != null) ? target.Name : null));
				this.removeChosenList.Add(target);
				this.oncePerTurnEffectActivatedList.Add(base.Card.Id);
				base.AI.SelectCard(target);
				return true;
			}
			if (base.Duel.LastChainPlayer == 1)
			{
				List<ClientCard> targetList = base.Duel.LastChainTargets.Where((ClientCard card) => card.Controller == 1 && (card.Location == CardLocation.Grave || card.Location == CardLocation.MonsterZone || card.Location == CardLocation.SpellZone || card.Location == CardLocation.FieldZone)).ToList<ClientCard>();
				if (targetList.Count<ClientCard>() > 0)
				{
					this.oncePerTurnEffectActivatedList.Add(base.Card.Id);
					List<ClientCard> shuffleTargetList = this.ShuffleCardList(targetList);
					string text = "===Exosister: returnia target 2: ";
					ClientCard clientCard = shuffleTargetList[0];
					Logger.DebugWriteLine(text + ((clientCard != null) ? clientCard.Name : null));
					base.AI.SelectCard(shuffleTargetList);
					return true;
				}
			}
			target = this.GetBestEnemyCard(false, true, true);
			bool check = base.DefaultOnBecomeTarget() && target != null && (target.Location != CardLocation.Onfield || target.Id != 15693423);
			bool check2 = base.Bot.UnderAttack;
			bool check3 = base.Duel.Player == 1 && base.Duel.Phase == DuelPhase.End && base.Duel.LastChainPlayer != 0 && target != null && target.Location != CardLocation.Grave;
			bool check4 = base.Duel.Player == 1 && base.Enemy.GetMonsterCount() >= 2 && base.Duel.LastChainPlayer != 0;
			Logger.DebugWriteLine(string.Concat(new string[]
			{
				"===Exosister: returnia check flag: ",
				check.ToString(),
				" ",
				check2.ToString(),
				" ",
				check3.ToString(),
				" ",
				check4.ToString()
			}));
			if (check || check2 || check3 || check4)
			{
				this.oncePerTurnEffectActivatedList.Add(base.Card.Id);
				Logger.DebugWriteLine("===Exosister: returnia target 3: " + ((target != null) ? target.Name : null));
				base.AI.SelectCard(target);
				return true;
			}
			return false;
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x0006CD40 File Offset: 0x0006AF40
		public bool ExosisterAvoidMaxxCSummonCheck()
		{
			if (!base.Bot.HasInHand(37343995) || !base.Bot.HasInHand(16474916) || this.elisEffect1Activated || this.marthaEffect1Activated)
			{
				return false;
			}
			if (this.enemyActivateLockBird && this.CheckAtAdvantage())
			{
				return false;
			}
			if (base.Card.Id != 16474916 && base.Card.Id != 37343995)
			{
				this.summoned = true;
				return true;
			}
			if (base.Card.IsCode(16474916))
			{
				if (base.Bot.Hand.Count((ClientCard card) => ((card != null) ? card.Data : null) != null && !card.IsCode(16474916) && !card.IsCode(37343995) && card.IsMonster() && card.HasSetcode(370)) > 0)
				{
					return false;
				}
				if (base.Bot.Hand.Count((ClientCard card) => ((card != null) ? card.Data : null) != null && card.IsCode(16474916)) > 1)
				{
					this.summoned = true;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x0006CE44 File Offset: 0x0006B044
		public bool ExosisterStellaSummonCheck()
		{
			if (this.stellaEffect1Activated || base.Bot.HasInMonstersZone(43863925, true, false, false) || this.CheckWhetherNegated(true) || this.CheckLessOperation())
			{
				return false;
			}
			if (this.enemyActivateLockBird && this.CheckAtAdvantage())
			{
				return false;
			}
			if (base.Bot.Hand.Count((ClientCard card) => card != base.Card && ((card != null) ? card.Data : null) != null && card.IsMonster() && card.HasSetcode(370)) > 0)
			{
				this.summoned = true;
				return true;
			}
			return false;
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x0006CEBC File Offset: 0x0006B0BC
		public bool ExosisterIreneSummonCheck()
		{
			if (this.irenaEffect1Activated || this.CheckLessOperation() || this.CheckWhetherNegated(true) || this.CheckCalledbytheGrave(16474916) > 0 || this.CheckCalledbytheGrave(79858629) > 0)
			{
				return false;
			}
			if (this.enemyActivateLockBird && this.CheckAtAdvantage())
			{
				return false;
			}
			if (this.CheckRemainInDeck(16474916) > 0)
			{
				this.summoned = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x0006CF2C File Offset: 0x0006B12C
		public bool ExosisterForElisSummonCheck()
		{
			if (this.elisEffect1Activated || this.CheckCalledbytheGrave(16474916) > 0 || this.CheckLessOperation())
			{
				return false;
			}
			ClientCard card2 = base.Card;
			if (((card2 != null) ? card2.Data : null) == null)
			{
				return false;
			}
			if (!base.Card.HasSetcode(370) || (base.Card.IsCode(37343995) && this.CheckRemainInDeck(16474916) > 0))
			{
				return false;
			}
			if (this.enemyActivateLockBird && this.CheckAtAdvantage())
			{
				return false;
			}
			if (base.Bot.Hand.Count((ClientCard card) => card != base.Card && ((card != null) ? card.Data : null) != null && card.IsCode(16474916)) > 0)
			{
				this.summoned = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x0006CFE0 File Offset: 0x0006B1E0
		public bool AratamaSummonCheck()
		{
			if (this.sakitamaEffect1Activated || this.CheckCalledbytheGrave(16889337) > 0 || this.CheckCalledbytheGrave(67972302) > 0)
			{
				return false;
			}
			if (this.enemyActivateLockBird && this.CheckAtAdvantage())
			{
				return false;
			}
			if (this.CheckRemainInDeck(67972302) > 0)
			{
				this.summoned = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x0006D040 File Offset: 0x0006B240
		public bool ForSakitamaSummonCheck()
		{
			if (this.sakitamaEffect1Activated || this.CheckCalledbytheGrave(67972302) > 0 || this.CheckLessOperation())
			{
				return false;
			}
			if (base.Bot.Hand.Count((ClientCard card) => ((card != null) ? card.Data : null) != null && base.Card != card && card.IsCode(67972302)) == 0)
			{
				return false;
			}
			if (this.enemyActivateLockBird && this.CheckAtAdvantage())
			{
				return false;
			}
			ClientCard card2 = base.Card;
			if (((card2 != null) ? card2.Data : null) != null && !base.Card.IsCode(37343995) && base.Card.Level == 4)
			{
				this.summoned = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x0006D0DD File Offset: 0x0006B2DD
		public bool Level4SummonCheck()
		{
			if (base.Card.Id == 37343995)
			{
				return false;
			}
			if (base.Bot.GetMonsters().Count((ClientCard card) => this.CheckAbleForXyz(card)) == 1)
			{
				this.summoned = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x0006D11C File Offset: 0x0006B31C
		public bool ForDonnerSummonCheck()
		{
			if (!base.Bot.HasInExtra(8728498) || (!base.Bot.HasInHand(37343995) && !base.Bot.HasInHandOrInSpellZone(197042)))
			{
				return false;
			}
			if (this.CheckLessOperation())
			{
				return false;
			}
			List<ClientCard> illegalList = (from card in base.Bot.GetMonsters()
				where card.IsFaceup() && !card.HasType(CardType.Xyz) && card.Level != 4 && (card.Data == null || !card.HasSetcode(370))
				select card).ToList<ClientCard>();
			if (illegalList.Count<ClientCard>() == 0)
			{
				return false;
			}
			if (illegalList.Count<ClientCard>() == 1)
			{
				List<ClientCard> otherMaterialList = (from card in base.Bot.GetMonsters()
					where card.IsFaceup() && !card.HasType(CardType.Xyz)
					select card).ToList<ClientCard>();
				otherMaterialList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				illegalList.AddRange(otherMaterialList);
			}
			if (illegalList.Count<ClientCard>() == 1)
			{
				List<ClientCard> hands = base.Bot.Hand.Where((ClientCard card) => ((card != null) ? card.Data : null) != null && card.IsMonster()).ToList<ClientCard>();
				if (hands.Count<ClientCard>() > 0)
				{
					hands.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
					if (base.Card != hands[0])
					{
						return false;
					}
				}
				Logger.DebugWriteLine("===Exosister: summon for donner");
				this.summoned = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x0006D280 File Offset: 0x0006B480
		public bool ExosisterForArmentSummonCheck()
		{
			if (!base.Bot.HasInHandOrInSpellZone(4408198))
			{
				return false;
			}
			ClientCard card2 = base.Card;
			if (((card2 != null) ? card2.Data : null) == null)
			{
				return false;
			}
			if (!base.Card.HasSetcode(370))
			{
				return false;
			}
			if (!base.Bot.GetMonsters().Any((ClientCard card) => ((card != null) ? card.Data : null) != null && card.IsFaceup() && card.HasSetcode(370)))
			{
				this.summoned = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x0006D306 File Offset: 0x0006B506
		public bool ExosisterMikailisSpSummonCheck()
		{
			return this.ExosisterMikailisSpSummonCheckInner(true);
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x0006D30F File Offset: 0x0006B50F
		public bool ExosisterMikailisAdvancedSpSummonCheck()
		{
			return this.CheckLessOperation() && !this.enemyActivateLockBird && this.ExosisterMikailisSpSummonCheckInner(false);
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x0006D32C File Offset: 0x0006B52C
		public bool ExosisterMikailisSpSummonCheckInner(bool shouldCheckLessOperation = true)
		{
			if (base.Bot.HasInMonstersZone(42741437, false, false, false) || this.mikailisEffect3Activated || (this.CheckLessOperation() && shouldCheckLessOperation))
			{
				return false;
			}
			if (!this.enemyActivateLockBird)
			{
				foreach (int cardId in this.ExosisterSpellTrapList)
				{
					if (!base.Bot.HasInHandOrInSpellZone(cardId))
					{
						this.SelectXyzMaterial(2, false);
						return true;
					}
				}
			}
			if (!this.mikailisEffect1Activated && !base.Bot.HasInMonstersZone(42741437, false, false, false) && this.GetProblematicEnemyCard(true) != null)
			{
				List<ClientCard> list = (from card in base.Bot.GetMonsters()
					where this.CheckAbleForXyz(card) && card.HasSetcode(370)
					select card).ToList<ClientCard>();
				if (list != null && list.Count<ClientCard>() > 0)
				{
					this.SelectXyzMaterial(2, true);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x0006D428 File Offset: 0x0006B628
		public bool ExosisterKaspitellSpSummonCheck()
		{
			return this.ExosisterKaspitellSpSummonCheckInner(true);
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x0006D431 File Offset: 0x0006B631
		public bool ExosisterKaspitellAdvancedSpSummonCheck()
		{
			return this.CheckLessOperation() && !this.enemyActivateLockBird && this.ExosisterKaspitellSpSummonCheckInner(false);
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x0006D44C File Offset: 0x0006B64C
		public bool ExosisterKaspitellSpSummonCheckInner(bool shouldCheckLessOperation = true)
		{
			if (base.Bot.HasInMonstersZone(78135071, false, false, false) || this.kaspitellEffect3Activated || (shouldCheckLessOperation && this.CheckLessOperation()))
			{
				return false;
			}
			bool searchMartha = true;
			bool searchStella = true;
			bool forMagnifica = false;
			if (this.marthaEffect1Activated || this.CheckCalledbytheGrave(37343995) > 0 || this.CheckRemainInDeck(37343995) == 0 || this.CheckRemainInDeck(16474916) == 0)
			{
				searchMartha = false;
			}
			if (base.Bot.GetMonsters().Any((ClientCard card) => card.HasType(CardType.Link) || card.HasType(CardType.Token)))
			{
				searchMartha = false;
			}
			if (!this.stellaEffect1Activated && this.CheckCalledbytheGrave(43863925) <= 0 && this.CheckRemainInDeck(43863925) != 0)
			{
				if (base.Bot.Hand.Any((ClientCard card) => ((card != null) ? card.Data : null) != null && card.IsMonster() && card.HasSetcode(370)))
				{
					goto IL_00EB;
				}
			}
			searchStella = false;
			IL_00EB:
			if (base.Bot.GetMonsters().Count((ClientCard card) => ((card != null) ? card.Data : null) != null && card.HasType(CardType.Xyz) && card.HasType(CardType.Xyz) && !card.IsCode(59242457)) == 1)
			{
				forMagnifica = true;
			}
			if (this.enemyActivateLockBird)
			{
				searchMartha = false;
				searchStella = false;
			}
			if (!searchMartha && !searchStella && !forMagnifica)
			{
				return false;
			}
			List<ClientCard> materialCheckList = (from card in base.Bot.GetMonsters()
				where !card.HasType(CardType.Xyz) && !card.HasType(CardType.Token) && !card.HasType(CardType.Link)
				select card).ToList<ClientCard>();
			if (materialCheckList.Count<ClientCard>() == 2)
			{
				if (materialCheckList.All((ClientCard card) => card.Level == 4))
				{
					this.SelectXyzMaterial(2, false);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x0006D600 File Offset: 0x0006B800
		public bool ExosistersMagnificaSpSummonCheck()
		{
			if (this.CheckLessOperation())
			{
				return false;
			}
			List<ClientCard> materialList = (from card in base.Bot.GetMonsters()
				where card.IsFaceup() && card.HasType(CardType.Xyz) && card.Rank == 4 && card.HasSetcode(370)
				select card).ToList<ClientCard>();
			materialList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			base.AI.SelectMaterials(materialList, 0);
			return true;
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x0006D66C File Offset: 0x0006B86C
		public bool CheckCaduceusInner(ClientCard card)
		{
			if (((card != null) ? card.Data : null) == null)
			{
				return false;
			}
			foreach (int setcode in this.SetcodeForDiamond)
			{
				if (card.HasSetcode(setcode))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x0006D6D8 File Offset: 0x0006B8D8
		public bool TellarknightConstellarCaduceusSpSummonCheck()
		{
			if (base.Duel.Turn == 1 || !base.Bot.HasInExtra(9272381))
			{
				return false;
			}
			if (base.Enemy.Graveyard.Any((ClientCard card) => this.CheckCaduceusInner(card)))
			{
				this.SelectXyzMaterial(2, false);
				return true;
			}
			return false;
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x0006D730 File Offset: 0x0006B930
		public bool DonnerDaggerFurHireSpSummonCheck()
		{
			if (!base.Bot.HasInHand(37343995) && !base.Bot.HasInHandOrInSpellZone(197042))
			{
				return false;
			}
			if (this.CheckLessOperation())
			{
				return false;
			}
			List<ClientCard> illegalList = (from card in base.Bot.GetMonsters()
				where card.IsFaceup() && !card.HasType(CardType.Xyz) && card.Level != 4 && (card.Data == null || !card.HasSetcode(370))
				select card).ToList<ClientCard>();
			if (illegalList.Count<ClientCard>() == 1)
			{
				List<ClientCard> otherMaterialList = (from card in base.Bot.GetMonsters()
					where card.IsFaceup() && !card.HasType(CardType.Xyz)
					select card).ToList<ClientCard>();
				otherMaterialList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				illegalList.AddRange(otherMaterialList);
			}
			if (illegalList.Count<ClientCard>() > 1)
			{
				base.AI.SelectMaterials(illegalList, 0);
				return true;
			}
			return false;
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x0006D814 File Offset: 0x0006BA14
		public bool SpellSetCheck()
		{
			if (base.Duel.Phase == DuelPhase.Main1 && base.Bot.HasAttackingMonster() && base.Duel.Turn > 1)
			{
				return false;
			}
			if (new List<int> { 77913594, 4408198, 77891946, 197042 }.Contains(base.Card.Id) && base.Bot.HasInSpellZone(base.Card.Id, false, false))
			{
				return false;
			}
			if (!base.Card.IsTrap() && !base.Card.HasType(CardType.QuickPlay))
			{
				return false;
			}
			List<int> avoid_list = new List<int>();
			int setFornfiniteImpermanence = 0;
			for (int i = 0; i < 5; i++)
			{
				if (base.Enemy.SpellZone[i] != null && base.Enemy.SpellZone[i].IsFaceup() && base.Bot.SpellZone[4 - i] == null)
				{
					avoid_list.Add(4 - i);
					setFornfiniteImpermanence += (int)Math.Pow(2.0, (double)(4 - i));
				}
			}
			if (!base.Bot.HasInHand(10045474))
			{
				this.SelectSTPlace(null, false, null);
				return true;
			}
			if (base.Card.IsCode(10045474))
			{
				base.AI.SelectPlace(setFornfiniteImpermanence);
				return true;
			}
			this.SelectSTPlace(base.Card, false, avoid_list);
			return true;
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x0006D982 File Offset: 0x0006BB82
		protected override bool DefaultSetForDiabellze()
		{
			if (base.DefaultSetForDiabellze())
			{
				this.SelectSTPlace(null, true, null);
				return true;
			}
			return false;
		}

		// Token: 0x040017CF RID: 6095
		private const int SetcodeTimeLord = 74;

		// Token: 0x040017D0 RID: 6096
		private const int SetcodeShadoll = 157;

		// Token: 0x040017D1 RID: 6097
		private const int SetcodeInferoid = 187;

		// Token: 0x040017D2 RID: 6098
		private const int SetcodeOrcust = 283;

		// Token: 0x040017D3 RID: 6099
		private const int SetcodeExosister = 370;

		// Token: 0x040017D4 RID: 6100
		private const int SetcodeTearlaments = 385;

		// Token: 0x040017D5 RID: 6101
		private List<int> SetcodeForDiamond = new List<int> { 157, 187, 385 };

		// Token: 0x040017D6 RID: 6102
		private List<int> affectGraveCardIdList = new List<int>
		{
			71344451, 40975243, 87746184, 70534340, 45906428, 71490127, 3659803, 12071500, 6077601, 11827244,
			95238394, 81223446, 40003819, 72490637, 21011044, 59419719, 14735698, 45410988
		};

		// Token: 0x040017D7 RID: 6103
		private Dictionary<int, List<int>> DeckCountTable = new Dictionary<int, List<int>>
		{
			{
				3,
				new List<int> { 16474916, 43863925, 37343995, 16889337, 67972302, 23434538, 14558127, 77913594, 77891946 }
			},
			{
				2,
				new List<int> { 79858629, 5352328, 84211599, 24224830, 197042, 10045474 }
			},
			{
				1,
				new List<int> { 4408198 }
			}
		};

		// Token: 0x040017D8 RID: 6104
		private Dictionary<int, int> ExosisterMentionTable = new Dictionary<int, int>
		{
			{ 16474916, 43863925 },
			{ 43863925, 16474916 },
			{ 79858629, 5352328 },
			{ 5352328, 79858629 },
			{ 37343995, 16474916 }
		};

		// Token: 0x040017D9 RID: 6105
		private List<int> ExosisterSpellTrapList = new List<int> { 77913594, 4408198, 77891946, 197042 };

		// Token: 0x040017DA RID: 6106
		private List<int> currentNegatingIdList = new List<int>();

		// Token: 0x040017DB RID: 6107
		private bool enemyActivateMaxxC;

		// Token: 0x040017DC RID: 6108
		private bool enemyActivateLockBird;

		// Token: 0x040017DD RID: 6109
		private bool enemyMoveGrave;

		// Token: 0x040017DE RID: 6110
		private bool paxCallToField;

		// Token: 0x040017DF RID: 6111
		private List<int> infiniteImpermanenceList = new List<int>();

		// Token: 0x040017E0 RID: 6112
		private bool summoned;

		// Token: 0x040017E1 RID: 6113
		private bool elisEffect1Activated;

		// Token: 0x040017E2 RID: 6114
		private bool stellaEffect1Activated;

		// Token: 0x040017E3 RID: 6115
		private bool irenaEffect1Activated;

		// Token: 0x040017E4 RID: 6116
		private bool sophiaEffect1Activated;

		// Token: 0x040017E5 RID: 6117
		private bool marthaEffect1Activated;

		// Token: 0x040017E6 RID: 6118
		private bool mikailisEffect1Activated;

		// Token: 0x040017E7 RID: 6119
		private bool mikailisEffect3Activated;

		// Token: 0x040017E8 RID: 6120
		private bool kaspitellEffect1Activated;

		// Token: 0x040017E9 RID: 6121
		private bool kaspitellEffect3Activated;

		// Token: 0x040017EA RID: 6122
		private bool gibrineEffect1Activated;

		// Token: 0x040017EB RID: 6123
		private bool gibrineEffect3Activated;

		// Token: 0x040017EC RID: 6124
		private bool asophielEffect1Activated;

		// Token: 0x040017ED RID: 6125
		private bool asophielEffect3Activated;

		// Token: 0x040017EE RID: 6126
		private bool sakitamaEffect1Activated;

		// Token: 0x040017EF RID: 6127
		private List<int> exosisterTransformEffectList = new List<int>();

		// Token: 0x040017F0 RID: 6128
		private List<int> oncePerTurnEffectActivatedList = new List<int>();

		// Token: 0x040017F1 RID: 6129
		private List<ClientCard> activatedMagnificaList = new List<ClientCard>();

		// Token: 0x040017F2 RID: 6130
		private List<ClientCard> targetedMagnificaList = new List<ClientCard>();

		// Token: 0x040017F3 RID: 6131
		private List<int> transformDestList = new List<int>();

		// Token: 0x040017F4 RID: 6132
		private List<ClientCard> spSummonThisTurn = new List<ClientCard>();

		// Token: 0x040017F5 RID: 6133
		private bool potActivate;

		// Token: 0x040017F6 RID: 6134
		private List<ClientCard> removeChosenList = new List<ClientCard>();

		// Token: 0x020002FB RID: 763
		public class CardId
		{
			// Token: 0x040017F7 RID: 6135
			public const int ExosisterElis = 16474916;

			// Token: 0x040017F8 RID: 6136
			public const int ExosisterStella = 43863925;

			// Token: 0x040017F9 RID: 6137
			public const int ExosisterIrene = 79858629;

			// Token: 0x040017FA RID: 6138
			public const int ExosisterSophia = 5352328;

			// Token: 0x040017FB RID: 6139
			public const int ExosisterMartha = 37343995;

			// Token: 0x040017FC RID: 6140
			public const int Aratama = 16889337;

			// Token: 0x040017FD RID: 6141
			public const int Sakitama = 67972302;

			// Token: 0x040017FE RID: 6142
			public const int ExosisterPax = 77913594;

			// Token: 0x040017FF RID: 6143
			public const int ExosisterArment = 4408198;

			// Token: 0x04001800 RID: 6144
			public const int PotofExtravagance = 84211599;

			// Token: 0x04001801 RID: 6145
			public const int ExosisterVadis = 77891946;

			// Token: 0x04001802 RID: 6146
			public const int ExosisterReturnia = 197042;

			// Token: 0x04001803 RID: 6147
			public const int ExosisterMikailis = 42741437;

			// Token: 0x04001804 RID: 6148
			public const int ExosisterKaspitell = 78135071;

			// Token: 0x04001805 RID: 6149
			public const int ExosisterGibrine = 5530780;

			// Token: 0x04001806 RID: 6150
			public const int ExosisterAsophiel = 41524885;

			// Token: 0x04001807 RID: 6151
			public const int ExosistersMagnifica = 59242457;

			// Token: 0x04001808 RID: 6152
			public const int TellarknightConstellarCaduceus = 58858807;

			// Token: 0x04001809 RID: 6153
			public const int StellarknightConstellarDiamond = 9272381;

			// Token: 0x0400180A RID: 6154
			public const int DivineArsenalAAZEUS_SkyThunder = 90448279;

			// Token: 0x0400180B RID: 6155
			public const int DonnerDaggerFurHire = 8728498;

			// Token: 0x0400180C RID: 6156
			public const int NaturalExterio = 99916754;

			// Token: 0x0400180D RID: 6157
			public const int NaturalBeast = 33198837;

			// Token: 0x0400180E RID: 6158
			public const int ImperialOrder = 61740673;

			// Token: 0x0400180F RID: 6159
			public const int SwordsmanLV7 = 37267041;

			// Token: 0x04001810 RID: 6160
			public const int RoyalDecree = 51452091;

			// Token: 0x04001811 RID: 6161
			public const int Number41BagooskatheTerriblyTiredTapir = 90590303;

			// Token: 0x04001812 RID: 6162
			public const int InspectorBoarder = 15397015;

			// Token: 0x04001813 RID: 6163
			public const int DimensionShifter = 91800273;
		}
	}
}
