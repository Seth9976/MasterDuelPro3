using System;
using System.Collections.Generic;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x02000394 RID: 916
	[Deck("Phantasm", "AI_Phantasm", "Normal")]
	public class PhantasmExecutor : DefaultExecutor
	{
		// Token: 0x06001B19 RID: 6937 RVA: 0x0009FF14 File Offset: 0x0009E114
		public PhantasmExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.GoToBattlePhase, new Func<bool>(this.GoToBattlePhase));
			base.AddExecutor(ExecutorType.Activate, 58120309, new Func<bool>(this.PreventFeatherDustereff));
			base.AddExecutor(ExecutorType.Activate, 99188141, new Func<bool>(this.PreventFeatherDustereff));
			base.AddExecutor(ExecutorType.Activate, 73642296, new Func<bool>(base.DefaultGhostBelleAndHauntedMansion));
			base.AddExecutor(ExecutorType.Activate, 24224830, new Func<bool>(base.DefaultCalledByTheGrave));
			base.AddExecutor(ExecutorType.Activate, 97268402, new Func<bool>(base.DefaultEffectVeiler));
			base.AddExecutor(ExecutorType.Activate, 10045474, new Func<bool>(base.DefaultInfiniteImpermanence));
			base.AddExecutor(ExecutorType.Activate, 14558127, new Func<bool>(base.DefaultAshBlossomAndJoyousSpring));
			base.AddExecutor(ExecutorType.Activate, 59438930, new Func<bool>(base.DefaultGhostOgreAndSnowRabbit));
			base.AddExecutor(ExecutorType.Activate, 19089195, new Func<bool>(this.SeaStealthAttackeff));
			base.AddExecutor(ExecutorType.Activate, 34302287, new Func<bool>(this.PhantasmSprialBattleeff));
			base.AddExecutor(ExecutorType.Activate, 61397885, new Func<bool>(this.PhantasmSpiralPowereff));
			base.AddExecutor(ExecutorType.Activate, 47475363, new Func<bool>(this.DrowningMirrorForceeff));
			base.AddExecutor(ExecutorType.Activate, 53334471, new Func<bool>(this.GozenMatcheff));
			base.AddExecutor(ExecutorType.Activate, 82732705, new Func<bool>(this.SkillDraineff));
			base.AddExecutor(ExecutorType.Activate, 89208725, new Func<bool>(this.Metaverseeff));
			base.AddExecutor(ExecutorType.SpSummon, 85289965, new Func<bool>(this.BorrelswordDragonsp));
			base.AddExecutor(ExecutorType.Activate, 85289965, new Func<bool>(this.BorrelswordDragoneff));
			base.AddExecutor(ExecutorType.SpSummon, 3987233, new Func<bool>(this.MissusRadiantsp));
			base.AddExecutor(ExecutorType.Activate, 3987233, new Func<bool>(this.MissusRadianteff));
			base.AddExecutor(ExecutorType.Activate, 41999284, new Func<bool>(this.Linkuriboheff));
			base.AddExecutor(ExecutorType.SpSummon, 41999284, new Func<bool>(this.Linkuribohsp));
			base.AddExecutor(ExecutorType.Activate, 18144506, new Func<bool>(base.DefaultHarpiesFeatherDusterFirst));
			base.AddExecutor(ExecutorType.Activate, 47325505, new Func<bool>(this.FossilDigeff));
			base.AddExecutor(ExecutorType.Activate, 73628505, new Func<bool>(this.Terraformingeff));
			base.AddExecutor(ExecutorType.Activate, 98645731, new Func<bool>(this.PotOfDualityeff));
			base.AddExecutor(ExecutorType.Activate, 35261759, new Func<bool>(this.PotOfDesireseff));
			base.AddExecutor(ExecutorType.Activate, 2819435, new Func<bool>(this.PacifisThePhantasmCityeff));
			base.AddExecutor(ExecutorType.Summon, 81823360, new Func<bool>(this.MegalosmasherXsummon));
			base.AddExecutor(ExecutorType.SpSummon, 63845230, new Func<bool>(this.EaterOfMillionssp));
			base.AddExecutor(ExecutorType.Activate, 63845230, new Func<bool>(this.EaterOfMillionseff));
			base.AddExecutor(ExecutorType.Activate, 73915051, new Func<bool>(base.DefaultScapegoat));
			base.AddExecutor(ExecutorType.SpellSet, 19089195, new Func<bool>(this.NoSetAlreadyDone));
			base.AddExecutor(ExecutorType.SpellSet, 58120309, new Func<bool>(this.StarlightRoadset));
			base.AddExecutor(ExecutorType.SpellSet, 99188141, new Func<bool>(this.TheHugeRevolutionIsOverset));
			base.AddExecutor(ExecutorType.SpellSet, 47475363);
			base.AddExecutor(ExecutorType.SpellSet, 10045474, new Func<bool>(this.InfiniteImpermanenceset));
			base.AddExecutor(ExecutorType.SpellSet, 73915051, new Func<bool>(this.NoSetAlreadyDone));
			base.AddExecutor(ExecutorType.SpellSet, 53334471, new Func<bool>(this.NoSetAlreadyDone));
			base.AddExecutor(ExecutorType.SpellSet, 82732705, new Func<bool>(this.NoSetAlreadyDone));
			base.AddExecutor(ExecutorType.SpellSet, 89208725);
			base.AddExecutor(ExecutorType.SpellSet, new Func<bool>(this.SpellSeteff));
			base.AddExecutor(ExecutorType.Activate, 59750328, new Func<bool>(this.CardOfDemiseeff));
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(this.MonsterRepos));
		}

		// Token: 0x06001B1A RID: 6938 RVA: 0x000A0322 File Offset: 0x0009E522
		public override void OnNewTurn()
		{
			this.summon_used = false;
			this.CardOfDemiseeff_used = false;
			this.SeaStealthAttackeff_used = false;
			base.OnNewTurn();
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x00072780 File Offset: 0x00070980
		private bool PreventFeatherDustereff()
		{
			return base.Duel.LastChainPlayer == 1;
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x000A033F File Offset: 0x0009E53F
		private bool GoToBattlePhase()
		{
			return base.Enemy.GetMonsterCount() == 0 && base.Util.GetTotalAttackingMonsterAttack(0) >= base.Enemy.LifePoints;
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x000A036C File Offset: 0x0009E56C
		private bool PhantasmSprialBattleeff()
		{
			if (base.DefaultOnBecomeTarget() && base.Card.Location == CardLocation.SpellZone)
			{
				base.AI.SelectCard(base.Util.GetBestEnemyCard(false, true));
				return true;
			}
			if (base.Enemy.HasInSpellZone(48680970, false, false))
			{
				base.AI.SelectCard(48680970);
				return base.UniqueFaceupSpell();
			}
			if (base.Bot.UnderAttack && base.Bot.BattlingMonster != null && base.Bot.BattlingMonster.IsCode(81823360))
			{
				base.AI.SelectCard(base.Enemy.BattlingMonster);
				return base.UniqueFaceupSpell();
			}
			if (base.Bot.GetMonsterCount() > 0 && !base.Bot.HasInSpellZone(19089195, false, false) && base.Util.IsOneEnemyBetterThanValue(2000, false) && base.Duel.Phase == DuelPhase.BattleStart)
			{
				base.AI.SelectCard(base.Util.GetBestEnemyMonster(true, true));
				return base.UniqueFaceupSpell();
			}
			if (base.Util.GetProblematicEnemyCard(9999, true) == null)
			{
				return false;
			}
			if (base.Util.GetProblematicEnemyCard(9999, true).IsCode(94977269) && !base.Util.GetProblematicEnemyCard(9999, true).IsDisabled())
			{
				return false;
			}
			base.AI.SelectCard(base.Util.GetProblematicEnemyCard(9999, true));
			return base.UniqueFaceupSpell();
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x000A04F4 File Offset: 0x0009E6F4
		private bool PhantasmSpiralPowereff()
		{
			if (base.DefaultOnBecomeTarget() && base.Card.Location == CardLocation.SpellZone)
			{
				return true;
			}
			if (base.Duel.Player == 0 || (base.Duel.Player == 1 && base.Bot.BattlingMonster != null))
			{
				if (base.Enemy.HasInMonstersZone(94977269, false, false, false))
				{
					base.AI.SelectCard(94977269);
					return base.UniqueFaceupSpell();
				}
				if (base.Enemy.HasInMonstersZone(15291624, false, false, false))
				{
					base.AI.SelectCard(15291624);
					return base.UniqueFaceupSpell();
				}
			}
			return base.DefaultInfiniteImpermanence() && base.UniqueFaceupSpell();
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x000A05AC File Offset: 0x0009E7AC
		private bool DrowningMirrorForceeff()
		{
			int count = 0;
			using (List<ClientCard>.Enumerator enumerator = base.Enemy.GetMonsters().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsAttack())
					{
						count++;
					}
				}
			}
			return base.Util.GetTotalAttackingMonsterAttack(1) >= base.Bot.LifePoints || count >= 2;
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x000A062C File Offset: 0x0009E82C
		private bool GozenMatcheff()
		{
			if (base.Bot.GetMonsterCount() >= 4 || base.Bot.HasInSpellZone(73915051, false, false))
			{
				return false;
			}
			if (base.DefaultOnBecomeTarget())
			{
				return true;
			}
			int dark_count = 0;
			int Divine_count = 0;
			int Earth_count = 0;
			int Fire_count = 0;
			int Light_count = 0;
			int Water_count = 0;
			int Wind_count = 0;
			foreach (ClientCard clientCard in base.Enemy.GetMonsters())
			{
				if (clientCard.HasAttribute(CardAttribute.Dark))
				{
					dark_count++;
				}
				if (clientCard.HasAttribute(CardAttribute.Divine))
				{
					Divine_count++;
				}
				if (clientCard.HasAttribute(CardAttribute.Earth))
				{
					Earth_count++;
				}
				if (clientCard.HasAttribute(CardAttribute.Fire))
				{
					Fire_count++;
				}
				if (clientCard.HasAttribute(CardAttribute.Light))
				{
					Light_count++;
				}
				if (clientCard.HasAttribute(CardAttribute.Water))
				{
					Water_count++;
				}
				if (clientCard.HasAttribute(CardAttribute.Wind))
				{
					Wind_count++;
				}
			}
			if (dark_count > 1)
			{
				dark_count = 1;
			}
			if (Divine_count > 1)
			{
				Divine_count = 1;
			}
			if (Earth_count > 1)
			{
				Earth_count = 1;
			}
			if (Fire_count > 1)
			{
				Fire_count = 1;
			}
			if (Light_count > 1)
			{
				Light_count = 1;
			}
			if (Water_count > 1)
			{
				Water_count = 1;
			}
			if (Wind_count > 1)
			{
				Wind_count = 1;
			}
			return dark_count + Divine_count + Earth_count + Fire_count + Light_count + Water_count + Wind_count >= 2 && base.UniqueFaceupSpell();
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x000A076C File Offset: 0x0009E96C
		private bool SkillDraineff()
		{
			return base.Duel.LastChainPlayer == 1 && base.Util.GetLastChainCard().Location == CardLocation.MonsterZone && base.UniqueFaceupSpell();
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x000A0798 File Offset: 0x0009E998
		private bool Metaverseeff()
		{
			if (base.Duel.LastChainPlayer == 0)
			{
				return false;
			}
			if (!base.Bot.HasInSpellZone(2819435, false, false))
			{
				base.AI.SelectOption(1);
				return base.UniqueFaceupSpell();
			}
			base.AI.SelectOption(0);
			return base.UniqueFaceupSpell();
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x000A07F0 File Offset: 0x0009E9F0
		private bool CardOfDemiseeff()
		{
			if (base.DefaultSpellWillBeNegated())
			{
				return false;
			}
			base.AI.SelectPlace(4);
			if (base.Card.Location == CardLocation.Hand)
			{
				if (base.Bot.Hand.Count <= 1 && base.Bot.GetSpellCountWithoutField() <= 3)
				{
					this.CardOfDemiseeff_used = true;
					return true;
				}
			}
			else if (base.Bot.Hand.Count <= 1 && base.Bot.GetSpellCountWithoutField() <= 4)
			{
				this.CardOfDemiseeff_used = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x000A0876 File Offset: 0x0009EA76
		private bool FossilDigeff()
		{
			return !base.DefaultSpellWillBeNegated() && (!this.CardOfDemiseeff_used || !this.summon_used);
		}

		// Token: 0x06001B25 RID: 6949 RVA: 0x000A0898 File Offset: 0x0009EA98
		private bool PotOfDualityeff()
		{
			if (!base.Bot.HasInHandOrInSpellZone(2819435) && !base.Bot.HasInHandOrInSpellZone(89208725))
			{
				if (base.Bot.HasInGraveyard(2819435) && !base.Bot.HasInHandOrInSpellZone(19089195))
				{
					base.AI.SelectCard(new int[] { 19089195, 2819435, 73628505, 89208725, 59750328, 73915051 });
				}
				else
				{
					base.AI.SelectCard(new int[] { 2819435, 73628505, 89208725, 59750328, 73915051 });
				}
			}
			else if (!base.Bot.HasInHandOrInSpellZone(19089195))
			{
				base.AI.SelectCard(new int[] { 19089195, 59750328, 35261759, 73915051 });
			}
			else
			{
				base.AI.SelectCard(new int[] { 59750328, 35261759, 73915051 });
			}
			return true;
		}

		// Token: 0x06001B26 RID: 6950 RVA: 0x000A0976 File Offset: 0x0009EB76
		private bool Terraformingeff()
		{
			return !base.DefaultSpellWillBeNegated() && (!this.CardOfDemiseeff_used || !base.Bot.HasInSpellZone(2819435, false, false));
		}

		// Token: 0x06001B27 RID: 6951 RVA: 0x000A09A4 File Offset: 0x0009EBA4
		private bool PacifisThePhantasmCityeff()
		{
			if (base.DefaultSpellWillBeNegated())
			{
				return false;
			}
			if (base.Card.Location == CardLocation.Hand)
			{
				return !base.Bot.HasInSpellZone(2819435, false, false);
			}
			ClientCard target = null;
			foreach (ClientCard s in base.Bot.GetSpells())
			{
				if (s.IsCode(19089195) && base.Card.IsFaceup())
				{
					target = s;
					break;
				}
			}
			using (List<ClientCard>.Enumerator enumerator = base.Bot.GetMonsters().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.HasAttribute(CardAttribute.Water))
					{
						if (target != null && !this.SeaStealthAttackeff_used && (base.Util.IsChainTarget(base.Card) || base.Util.IsChainTarget(target)))
						{
							return false;
						}
						break;
					}
				}
			}
			base.AI.SelectPlace(10);
			base.AI.SelectCard(34302287);
			return true;
		}

		// Token: 0x06001B28 RID: 6952 RVA: 0x000A0ADC File Offset: 0x0009ECDC
		private bool MegalosmasherXsummon()
		{
			base.AI.SelectPlace(10);
			this.summon_used = true;
			return true;
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x000A0AF4 File Offset: 0x0009ECF4
		private bool BorrelswordDragonsp()
		{
			if (!base.Bot.HasInMonstersZone(3987233, false, false, false))
			{
				return false;
			}
			IList<ClientCard> material_list = new List<ClientCard>();
			foreach (ClientCard i in base.Bot.GetMonsters())
			{
				if (i.IsCode(3987233))
				{
					material_list.Add(i);
					break;
				}
			}
			foreach (ClientCard j in base.Bot.GetMonsters())
			{
				if (j.IsCode(new int[] { 41999284, 98978921 }))
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

		// Token: 0x06001B2A RID: 6954 RVA: 0x000A0C04 File Offset: 0x0009EE04
		private bool BorrelswordDragoneff()
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
					if (check.IsAttack() && !check.HasType(CardType.Link))
					{
						base.AI.SelectCard(check);
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06001B2B RID: 6955 RVA: 0x000A0D8C File Offset: 0x0009EF8C
		private bool EaterOfMillionssp()
		{
			if (base.Bot.MonsterZone[1] == null)
			{
				base.AI.SelectPlace(2);
			}
			else
			{
				base.AI.SelectPlace(8);
			}
			if (base.Enemy.HasInMonstersZone(65330383, true, false, false))
			{
				return false;
			}
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
			IList<ClientCard> material_list = new List<ClientCard>();
			if (base.Bot.HasInExtra(31833038))
			{
				base.AI.SelectMaterials(new int[] { 5821478, 72529749, 65330383, 61665245, 31833038 }, 503);
			}
			else
			{
				foreach (ClientCard i in base.Bot.ExtraDeck)
				{
					if (material_list.Count == 5)
					{
						break;
					}
					material_list.Add(i);
				}
				base.AI.SelectMaterials(material_list, 503);
			}
			return true;
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x000A0ECC File Offset: 0x0009F0CC
		private bool EaterOfMillionseff()
		{
			return !base.Enemy.BattlingMonster.HasPosition(CardPosition.Attack) || base.Bot.BattlingMonster.Attack - base.Enemy.BattlingMonster.GetDefensePower() < base.Enemy.LifePoints;
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x000A0F20 File Offset: 0x0009F120
		private bool MissusRadiantsp()
		{
			IList<ClientCard> material_list = new List<ClientCard>();
			foreach (ClientCard monster in base.Bot.GetMonsters())
			{
				if (monster.HasAttribute(CardAttribute.Earth) && monster.Level == 1 && !monster.IsCode(63845230))
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
			if (base.Bot.HasInMonstersZone(3987233, false, false, false))
			{
				return false;
			}
			base.AI.SelectMaterials(material_list, 0);
			if ((base.Bot.MonsterZone[0] == null || base.Bot.MonsterZone[0].Level == 1) && (base.Bot.MonsterZone[2] == null || base.Bot.MonsterZone[2].Level == 1) && base.Bot.MonsterZone[5] == null)
			{
				base.AI.SelectPlace(32);
			}
			else
			{
				base.AI.SelectPlace(64);
			}
			return true;
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x000A104C File Offset: 0x0009F24C
		private bool MissusRadianteff()
		{
			base.AI.SelectCard(new int[] { 3987233 });
			return true;
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x000A1068 File Offset: 0x0009F268
		private bool Linkuribohsp()
		{
			foreach (ClientCard c in base.Bot.GetMonsters())
			{
				if (!c.IsCode(new int[] { 63845230, 41999284 }) && c.Level == 1)
				{
					base.AI.SelectMaterials(c, 0);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x0006F99C File Offset: 0x0006DB9C
		private bool Linkuriboheff()
		{
			return base.Duel.LastChainPlayer != 0 || !base.Util.GetLastChainCard().IsCode(41999284);
		}

		// Token: 0x06001B31 RID: 6961 RVA: 0x000A10F4 File Offset: 0x0009F2F4
		private bool SeaStealthAttackeff()
		{
			if (base.DefaultOnBecomeTarget())
			{
				base.AI.SelectCard(81823360);
				this.SeaStealthAttackeff_used = true;
				return true;
			}
			if (base.Card.IsFacedown() && base.Bot.HasInHandOrInSpellZoneOrInGraveyard(2819435))
			{
				if (!base.Bot.HasInSpellZone(2819435, false, false))
				{
					if (base.Bot.HasInGraveyard(2819435))
					{
						using (List<ClientCard>.Enumerator enumerator = base.Bot.GetGraveyardSpells().GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								ClientCard s = enumerator.Current;
								if (s.IsCode(2819435))
								{
									base.AI.SelectYesNo(true);
									base.AI.SelectCard(s);
									break;
								}
							}
							goto IL_012B;
						}
					}
					using (IEnumerator<ClientCard> enumerator2 = base.Bot.Hand.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							ClientCard s2 = enumerator2.Current;
							if (s2.IsCode(2819435))
							{
								base.AI.SelectYesNo(true);
								base.AI.SelectCard(s2);
								break;
							}
						}
						goto IL_012B;
					}
				}
				base.AI.SelectYesNo(false);
				IL_012B:
				return base.UniqueFaceupSpell();
			}
			if (base.Card.IsFaceup())
			{
				ClientCard target = null;
				foreach (ClientCard s3 in base.Bot.GetSpells())
				{
					if (s3.IsCode(2819435))
					{
						target = s3;
					}
				}
				if (target != null && base.Util.IsChainTarget(target))
				{
					this.SeaStealthAttackeff_used = true;
					return true;
				}
				target = base.Util.GetLastChainCard();
				if (target != null)
				{
					if (target.IsCode(99550630))
					{
						base.AI.SelectCard(81823360);
						this.SeaStealthAttackeff_used = true;
						return true;
					}
					if (base.Enemy.GetGraveyardSpells().Count >= 3 && target.IsCode(25955749))
					{
						base.AI.SelectCard(81823360);
						this.SeaStealthAttackeff_used = true;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x000A1344 File Offset: 0x0009F544
		private bool PotOfDesireseff()
		{
			return base.Bot.Deck.Count >= 18;
		}

		// Token: 0x06001B33 RID: 6963 RVA: 0x000A1360 File Offset: 0x0009F560
		private bool StarlightRoadset()
		{
			return (base.Duel.Turn <= 1 || base.Duel.Phase != DuelPhase.Main1 || !base.Bot.HasAttackingMonster()) && !base.Bot.HasInSpellZone(99188141, false, false);
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x000A13B0 File Offset: 0x0009F5B0
		private bool TheHugeRevolutionIsOverset()
		{
			return (base.Duel.Turn <= 1 || base.Duel.Phase != DuelPhase.Main1 || !base.Bot.HasAttackingMonster()) && !base.Bot.HasInSpellZone(58120309, false, false);
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x00088B99 File Offset: 0x00086D99
		private bool InfiniteImpermanenceset()
		{
			return !base.Bot.IsFieldEmpty();
		}

		// Token: 0x06001B36 RID: 6966 RVA: 0x000A1400 File Offset: 0x0009F600
		private bool NoSetAlreadyDone()
		{
			return (base.Duel.Turn <= 1 || base.Duel.Phase != DuelPhase.Main1 || !base.Bot.HasAttackingMonster()) && !base.Bot.HasInSpellZone(base.Card.Id, false, false);
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x000A1458 File Offset: 0x0009F658
		private bool SpellSeteff()
		{
			if (base.Card.HasType(CardType.Field))
			{
				return false;
			}
			if (this.CardOfDemiseeff_used)
			{
				return true;
			}
			if (base.Bot.HasInHandOrInSpellZone(59750328) && !this.CardOfDemiseeff_used)
			{
				int hand_spell_count = 0;
				foreach (ClientCard s in base.Bot.Hand)
				{
					if (s.HasType(CardType.Trap) || (s.HasType(CardType.Spell) && !s.HasType(CardType.Field)))
					{
						hand_spell_count++;
					}
				}
				return 5 - base.Bot.GetSpellCountWithoutField() - hand_spell_count >= 1;
			}
			return base.Card.IsCode(new int[] { 34302287, 61397885 }) && base.Bot.HasInMonstersZone(81823360, false, false, false) && !base.Bot.HasInHandOrInSpellZone(2819435) && !base.Bot.HasInHandOrInSpellZone(89208725);
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x000A1578 File Offset: 0x0009F778
		private bool MonsterRepos()
		{
			if (base.Card.Level >= 5)
			{
				foreach (ClientCard s in base.Bot.GetSpells())
				{
					if (s.IsFaceup() && s.IsCode(19089195) && base.Bot.HasInSpellZone(2819435, false, false) && base.Card.IsAttack())
					{
						return false;
					}
				}
			}
			return (!base.Card.IsCode(63845230) || base.Card.IsDisabled() || !base.Card.IsAttack()) && base.DefaultMonsterRepos();
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x000A1648 File Offset: 0x0009F848
		public override bool OnPreBattleBetween(ClientCard attacker, ClientCard defender)
		{
			if (attacker.IsCode(2819436) && defender.IsCode(63845230) && attacker.RealPower >= defender.RealPower)
			{
				return true;
			}
			if (attacker.Level >= 5)
			{
				foreach (ClientCard s in base.Bot.GetSpells())
				{
					if (s.IsFaceup() && s.IsCode(19089195) && base.Bot.HasInSpellZone(2819435, false, false))
					{
						attacker.RealPower = 9999;
						if (defender.IsCode(63845230))
						{
							return true;
						}
					}
				}
			}
			return base.OnPreBattleBetween(attacker, defender);
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x000A171C File Offset: 0x0009F91C
		public override ClientCard OnSelectAttacker(IList<ClientCard> attackers, IList<ClientCard> defenders)
		{
			for (int i = 0; i < attackers.Count; i++)
			{
				ClientCard attacker = attackers[i];
				if (attacker.IsCode(63845230))
				{
					return attacker;
				}
			}
			return null;
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x0000763C File Offset: 0x0000583C
		public override bool OnSelectHand()
		{
			return true;
		}

		// Token: 0x04001E59 RID: 7769
		private bool summon_used;

		// Token: 0x04001E5A RID: 7770
		private bool CardOfDemiseeff_used;

		// Token: 0x04001E5B RID: 7771
		private bool SeaStealthAttackeff_used;

		// Token: 0x02000395 RID: 917
		public class CardId
		{
			// Token: 0x04001E5C RID: 7772
			public const int MegalosmasherX = 81823360;

			// Token: 0x04001E5D RID: 7773
			public const int AshBlossom = 14558127;

			// Token: 0x04001E5E RID: 7774
			public const int EaterOfMillions = 63845230;

			// Token: 0x04001E5F RID: 7775
			public const int HarpieFeatherDuster = 18144506;

			// Token: 0x04001E60 RID: 7776
			public const int PotOfDesires = 35261759;

			// Token: 0x04001E61 RID: 7777
			public const int FossilDig = 47325505;

			// Token: 0x04001E62 RID: 7778
			public const int CardOfDemise = 59750328;

			// Token: 0x04001E63 RID: 7779
			public const int Terraforming = 73628505;

			// Token: 0x04001E64 RID: 7780
			public const int PotOfDuality = 98645731;

			// Token: 0x04001E65 RID: 7781
			public const int Scapegoat = 73915051;

			// Token: 0x04001E66 RID: 7782
			public const int PacifisThePhantasmCity = 2819435;

			// Token: 0x04001E67 RID: 7783
			public const int InfiniteImpermanence = 10045474;

			// Token: 0x04001E68 RID: 7784
			public const int PhantasmSprialBattle = 34302287;

			// Token: 0x04001E69 RID: 7785
			public const int DrowningMirrorForce = 47475363;

			// Token: 0x04001E6A RID: 7786
			public const int StarlightRoad = 58120309;

			// Token: 0x04001E6B RID: 7787
			public const int PhantasmSpiralPower = 61397885;

			// Token: 0x04001E6C RID: 7788
			public const int Metaverse = 89208725;

			// Token: 0x04001E6D RID: 7789
			public const int SeaStealthAttack = 19089195;

			// Token: 0x04001E6E RID: 7790
			public const int GozenMatch = 53334471;

			// Token: 0x04001E6F RID: 7791
			public const int SkillDrain = 82732705;

			// Token: 0x04001E70 RID: 7792
			public const int TheHugeRevolutionIsOver = 99188141;

			// Token: 0x04001E71 RID: 7793
			public const int StardustDragon = 44508094;

			// Token: 0x04001E72 RID: 7794
			public const int TopologicBomberDragon = 5821478;

			// Token: 0x04001E73 RID: 7795
			public const int BorreloadDragon = 31833038;

			// Token: 0x04001E74 RID: 7796
			public const int BorrelswordDragon = 85289965;

			// Token: 0x04001E75 RID: 7797
			public const int KnightmareGryphon = 65330383;

			// Token: 0x04001E76 RID: 7798
			public const int TopologicTrisbaena = 72529749;

			// Token: 0x04001E77 RID: 7799
			public const int SummonSorceress = 61665245;

			// Token: 0x04001E78 RID: 7800
			public const int KnightmareUnicorn = 38342335;

			// Token: 0x04001E79 RID: 7801
			public const int KnightmarePhoenix = 2857636;

			// Token: 0x04001E7A RID: 7802
			public const int KnightmareCerberus = 75452921;

			// Token: 0x04001E7B RID: 7803
			public const int CrystronNeedlefiber = 50588353;

			// Token: 0x04001E7C RID: 7804
			public const int MissusRadiant = 3987233;

			// Token: 0x04001E7D RID: 7805
			public const int LinkSpider = 98978921;

			// Token: 0x04001E7E RID: 7806
			public const int Linkuriboh = 41999284;

			// Token: 0x04001E7F RID: 7807
			public const int ElShaddollWinda = 94977269;

			// Token: 0x04001E80 RID: 7808
			public const int BrandishSkillJammingWave = 25955749;

			// Token: 0x04001E81 RID: 7809
			public const int BrandishSkillAfterburner = 99550630;

			// Token: 0x04001E82 RID: 7810
			public const int EternalSoul = 48680970;

			// Token: 0x04001E83 RID: 7811
			public const int SuperboltThunderDragon = 15291624;
		}
	}
}
