using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x02000352 RID: 850
	[Deck("LightswornShaddoldinosour", "AI_LightswornShaddoldinosour", "Normal")]
	public class LightswornShaddoldinosour : DefaultExecutor
	{
		// Token: 0x060016DD RID: 5853 RVA: 0x00087A20 File Offset: 0x00085C20
		public LightswornShaddoldinosour(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.Activate, 59438930, new Func<bool>(this.Hand_act_eff));
			base.AddExecutor(ExecutorType.Activate, 14558127, new Func<bool>(this.Hand_act_eff));
			base.AddExecutor(ExecutorType.Activate, 23434538, new Func<bool>(this.MaxxC));
			base.AddExecutor(ExecutorType.Activate, 18144506, new Func<bool>(base.DefaultHarpiesFeatherDusterFirst));
			base.AddExecutor(ExecutorType.Activate, 10045474, new Func<bool>(base.DefaultBreakthroughSkill));
			base.AddExecutor(ExecutorType.Activate, 11110587);
			base.AddExecutor(ExecutorType.Summon, 44335251);
			base.AddExecutor(ExecutorType.Activate, 44335251, new Func<bool>(this.SouleatingOviraptoreff));
			base.AddExecutor(ExecutorType.Activate, 1475311, new Func<bool>(base.DefaultAllureofDarkness));
			base.AddExecutor(ExecutorType.Activate, 67169062, new Func<bool>(this.PotofAvariceeff));
			base.AddExecutor(ExecutorType.Activate, 94886282, new Func<bool>(this.ChargeOfTheLightBrigadeEffect));
			base.AddExecutor(ExecutorType.Activate, 81439173, new Func<bool>(this.FoolishBurialEffect));
			base.AddExecutor(ExecutorType.Activate, 99330325, new Func<bool>(this.InterruptedKaijuSlumbereff));
			base.AddExecutor(ExecutorType.Activate, 44394295, new Func<bool>(this.ShaddollFusioneff));
			base.AddExecutor(ExecutorType.Summon, 77558536);
			base.AddExecutor(ExecutorType.Activate, 77558536);
			base.AddExecutor(ExecutorType.Summon, 48048590);
			base.AddExecutor(ExecutorType.Activate, 48048590, new Func<bool>(this.KeeperOfDragonicMagiceff));
			base.AddExecutor(ExecutorType.MonsterSet, 30328508);
			base.AddExecutor(ExecutorType.MonsterSet, 67441435);
			base.AddExecutor(ExecutorType.Summon, 95503687, new Func<bool>(this.Luminasummon));
			base.AddExecutor(ExecutorType.MonsterSet, 4939890);
			base.AddExecutor(ExecutorType.MonsterSet, 77723643);
			base.AddExecutor(ExecutorType.Summon, 55623480, new Func<bool>(this.FairyTailSnowsummon));
			base.AddExecutor(ExecutorType.Activate, 55623480, new Func<bool>(this.FairyTailSnoweff));
			base.AddExecutor(ExecutorType.Activate, 95503687, new Func<bool>(this.Luminaeff));
			base.AddExecutor(ExecutorType.Activate, 67441435, new Func<bool>(this.GlowUpBulbeff));
			base.AddExecutor(ExecutorType.Activate, 98558751, new Func<bool>(this.TG_WonderMagicianeff));
			base.AddExecutor(ExecutorType.Activate, 42566602, new Func<bool>(this.CoralDragoneff));
			base.AddExecutor(ExecutorType.Activate, 76547525, new Func<bool>(this.RedWyverneff));
			base.AddExecutor(ExecutorType.Activate, 50954680, new Func<bool>(this.CrystalWingSynchroDragoneff));
			base.AddExecutor(ExecutorType.Activate, 33698022, new Func<bool>(this.BlackRoseMoonlightDragoneff));
			base.AddExecutor(ExecutorType.Activate, 74997493, new Func<bool>(this.Sdulldeateff));
			base.AddExecutor(ExecutorType.Activate, 4779823, new Func<bool>(this.Michaeleff));
			base.AddExecutor(ExecutorType.Activate, 80666118, new Func<bool>(this.ScarlightRedDragoneff));
			base.AddExecutor(ExecutorType.Activate, 50588353, new Func<bool>(this.CrystronNeedlefibereff));
			base.AddExecutor(ExecutorType.SpSummon, 18940556, new Func<bool>(this.UltimateConductorTytannosp));
			base.AddExecutor(ExecutorType.Activate, 18940556, new Func<bool>(this.UltimateConductorTytannoeff));
			base.AddExecutor(ExecutorType.Activate, 38179121, new Func<bool>(this.DoubleEvolutionPilleff));
			base.AddExecutor(ExecutorType.SpSummon, 50954680);
			base.AddExecutor(ExecutorType.Activate, 50954680, new Func<bool>(this.CrystalWingSynchroDragoneff));
			base.AddExecutor(ExecutorType.SpSummon, 80666118, new Func<bool>(this.ScarlightRedDragonsp));
			base.AddExecutor(ExecutorType.Activate, 80666118, new Func<bool>(this.ScarlightRedDragoneff));
			base.AddExecutor(ExecutorType.SpSummon, 4779823, new Func<bool>(this.Michaelsp));
			base.AddExecutor(ExecutorType.Activate, 4779823, new Func<bool>(this.Michaeleff));
			base.AddExecutor(ExecutorType.SpSummon, 76547525, new Func<bool>(this.RedWyvernsp));
			base.AddExecutor(ExecutorType.Activate, 76547525, new Func<bool>(this.RedWyverneff));
			base.AddExecutor(ExecutorType.SpSummon, 30100551);
			base.AddExecutor(ExecutorType.Activate, 30100551, new Func<bool>(this.MinervaTheExaltedEffect));
			base.AddExecutor(ExecutorType.SpSummon, 50588353, new Func<bool>(this.CrystronNeedlefibersp));
			base.AddExecutor(ExecutorType.SpSummon, 55063751, new Func<bool>(this.GamecieltheSeaTurtleKaijusp));
			base.AddExecutor(ExecutorType.SpSummon, 28674152, new Func<bool>(this.RadiantheMultidimensionalKaijusp));
			base.AddExecutor(ExecutorType.SpSummon, 93332803, new Func<bool>(this.DogorantheMadFlameKaijusp));
			base.AddExecutor(ExecutorType.Activate, 83764718, new Func<bool>(this.Reborneff));
			base.AddExecutor(ExecutorType.Activate, 41782653, new Func<bool>(this.OvertexCoatlseff));
			base.AddExecutor(ExecutorType.Activate, 4904633, new Func<bool>(this.ShaddollCoreeff));
			base.AddExecutor(ExecutorType.Activate, 3717252, new Func<bool>(this.ShaddollBeasteff));
			base.AddExecutor(ExecutorType.Activate, 37445295, new Func<bool>(this.ShaddollFalcoeff));
			base.AddExecutor(ExecutorType.Activate, 77723643, new Func<bool>(this.ShaddollDragoneff));
			base.AddExecutor(ExecutorType.Activate, 4939890, new Func<bool>(this.ShaddollHedgehogeff));
			base.AddExecutor(ExecutorType.Activate, 30328508, new Func<bool>(this.ShaddollSquamataeff));
			base.AddExecutor(ExecutorType.Activate, 80280944);
			base.AddExecutor(ExecutorType.Activate, 20366274, new Func<bool>(this.ElShaddollConstructeff));
			base.AddExecutor(ExecutorType.Activate, 48424886, new Func<bool>(this.ElShaddollGrysraeff));
			base.AddExecutor(ExecutorType.Activate, 74822425, new Func<bool>(this.ElShaddollShekhinagaeff));
			base.AddExecutor(ExecutorType.Activate, 94977269);
			base.AddExecutor(ExecutorType.SpellSet, 11110587, new Func<bool>(this.SpellSetZone));
			base.AddExecutor(ExecutorType.SpellSet, new Func<bool>(this.SpellSetZone));
			base.AddExecutor(ExecutorType.SpellSet, 74003290);
			base.AddExecutor(ExecutorType.SpellSet, 77505534);
			base.AddExecutor(ExecutorType.SpellSet, 4904633);
			base.AddExecutor(ExecutorType.SpellSet, 10045474, new Func<bool>(this.SetIsFieldEmpty));
			base.AddExecutor(ExecutorType.Activate, 74003290, new Func<bool>(this.LostWindeff));
			base.AddExecutor(ExecutorType.Activate, 77505534, new Func<bool>(this.SinisterShadowGameseff));
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(this.MonsterRepos));
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x00088073 File Offset: 0x00086273
		public int[] all_List()
		{
			return new int[]
			{
				18940556, 93332803, 55063751, 28674152, 41782653, 3717252, 80280944, 77723643, 55623480, 48048590,
				30328508, 44335251, 77558536, 95503687, 4939890, 14558127, 59438930, 37445295, 23434538, 33420078,
				67441435, 1475311, 11110587, 18144506, 38179121, 44394295, 67169062, 81439173, 83764718, 94886282,
				99330325, 10045474, 74003290, 77505534, 4904633
			};
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x00088087 File Offset: 0x00086287
		public int[] Useless_List()
		{
			return new int[]
			{
				67441435, 33420078, 94886282, 11110587, 18144506, 55623480, 80280944, 95503687, 41782653, 99330325,
				81439173
			};
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x0008809C File Offset: 0x0008629C
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

		// Token: 0x060016E1 RID: 5857 RVA: 0x000880EC File Offset: 0x000862EC
		public override void OnNewPhase()
		{
			this.Enemy_atk = 0;
			IList<ClientCard> list = new List<ClientCard>();
			foreach (ClientCard monster in base.Enemy.GetMonsters())
			{
				if (monster.IsAttack())
				{
					list.Add(monster);
				}
			}
			this.Enemy_atk = this.GetTotalATK(list);
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x00088168 File Offset: 0x00086368
		public override void OnNewTurn()
		{
			this.Pillused = false;
			this.OvertexCoatlseff_used = false;
			this.CrystronNeedlefibereff_used = false;
			this.ShaddollBeast_used = false;
			this.ShaddollFalco_used = false;
			this.ShaddollSquamata_used = false;
			this.ShaddollDragon_used = false;
			this.ShaddollHedgehog_used = false;
			base.OnNewTurn();
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x000881A8 File Offset: 0x000863A8
		private bool Luminasummon()
		{
			if (base.Bot.Deck.Count >= 20)
			{
				return true;
			}
			IList<ClientCard> extra = base.Bot.GetMonstersInExtraZone();
			if (extra != null)
			{
				using (IEnumerator<ClientCard> enumerator = extra.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!enumerator.Current.HasType(CardType.Link))
						{
							return false;
						}
					}
				}
			}
			return base.Bot.LifePoints <= 3000 || base.Bot.HasInGraveyard(77558536);
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x00088248 File Offset: 0x00086448
		private bool Luminaeff()
		{
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			if (base.Bot.HasInGraveyard(77558536))
			{
				base.AI.SelectCard(this.Useless_List());
				base.AI.SelectNextCard(77558536);
				return true;
			}
			return false;
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x0008829C File Offset: 0x0008649C
		private bool UltimateConductorTytannoeff()
		{
			IList<int> targets = new int[]
			{
				41782653, 3717252, 30328508, 4939890, 77723643, 37445295, 67441435, 33420078, 55623480, 48048590,
				93332803, 55063751, 28674152, 80280944, 4904633, 44335251, 77558536, 95503687, 14558127, 59438930,
				23434538
			};
			if (base.Duel.Phase == DuelPhase.Main1)
			{
				if (base.Duel.Player == 0)
				{
					int count = 0;
					foreach (ClientCard monster in ((IEnumerable<ClientCard>)base.Enemy.GetMonsters()))
					{
						if (monster.Attack > 2500 || monster == base.Enemy.MonsterZone.GetDangerousMonster(false))
						{
							count++;
						}
					}
					if (count == 0)
					{
						return false;
					}
				}
				if (!base.Bot.HasInHand(targets) && !base.Bot.HasInMonstersZone(targets, false, false, false))
				{
					return false;
				}
				base.AI.SelectCard(targets);
				return true;
			}
			else
			{
				if (base.Duel.Phase == DuelPhase.BattleStart)
				{
					base.AI.SelectYesNo(true);
					return true;
				}
				return false;
			}
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x00088390 File Offset: 0x00086590
		private bool GamecieltheSeaTurtleKaijusp()
		{
			return !base.Bot.HasInMonstersZone(18940556, false, false, false) && base.DefaultKaijuSpsummon();
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x000883B0 File Offset: 0x000865B0
		private bool RadiantheMultidimensionalKaijusp()
		{
			return base.Enemy.HasInMonstersZone(55063751, false, false, false) || (base.Bot.HasInHand(93332803) && !base.Bot.HasInMonstersZone(18940556, false, false, false) && base.DefaultKaijuSpsummon());
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x00088403 File Offset: 0x00086603
		private bool DogorantheMadFlameKaijusp()
		{
			return base.Enemy.HasInMonstersZone(55063751, false, false, false) || base.Enemy.HasInMonstersZone(28674152, false, false, false);
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x00088434 File Offset: 0x00086634
		private bool InterruptedKaijuSlumbereff()
		{
			return base.Enemy.GetMonsterCount() - base.Bot.GetMonsterCount() >= 2 && base.DefaultInterruptedKaijuSlumber();
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x00088458 File Offset: 0x00086658
		private bool UltimateConductorTytannosp()
		{
			this.Pillused = true;
			foreach (ClientCard card in base.Bot.GetMonsters())
			{
				if (card.IsCode(18940556) && card.IsFaceup())
				{
					return false;
				}
			}
			this.Ultimate_ss++;
			return true;
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x000884DC File Offset: 0x000866DC
		private bool KeeperOfDragonicMagiceff()
		{
			if (base.ActivateDescription == -1)
			{
				base.AI.SelectCard(this.Useless_List());
				return true;
			}
			return true;
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x000884FC File Offset: 0x000866FC
		private bool MonsterRepos()
		{
			return (base.Card.IsCode(18940556) && base.Card.IsFacedown()) || (base.Card.IsCode(20366274) && base.Card.IsFacedown()) || ((!base.Card.IsCode(20366274) || !base.Card.IsAttack()) && (!base.Card.IsCode(67441435) || !base.Card.IsDefense()) && ((base.Card.IsCode(77723643) && base.Card.IsFacedown() && base.Enemy.GetMonsterCount() >= 0) || (base.Card.IsCode(30328508) && base.Card.IsFacedown() && base.Enemy.GetMonsterCount() >= 0) || base.DefaultMonsterRepos()));
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x000885F1 File Offset: 0x000867F1
		private bool OvertexCoatlseff()
		{
			if (base.Card.Location == CardLocation.MonsterZone)
			{
				return false;
			}
			this.OvertexCoatlseff_used = true;
			return true;
		}

		// Token: 0x060016EE RID: 5870 RVA: 0x0008860C File Offset: 0x0008680C
		private bool DoubleEvolutionPilleff()
		{
			foreach (ClientCard card in base.Bot.GetMonsters())
			{
				if (card.IsCode(18940556) && card.IsFaceup())
				{
					return false;
				}
			}
			if (this.Pillused)
			{
				return false;
			}
			this.Pillused = true;
			IList<int> targets = new int[] { 80280944, 93332803, 55063751, 28674152, 41782653, 44335251, 18940556 };
			if (base.Bot.HasInGraveyard(targets))
			{
				base.AI.SelectCard(new int[] { 80280944, 93332803, 41782653, 55063751, 28674152, 44335251, 18940556 });
			}
			else
			{
				base.AI.SelectCard(new int[] { 80280944, 93332803, 55063751, 28674152, 41782653, 44335251, 18940556 });
			}
			RuntimeHelpers.InitializeArray(new int[7], fieldof(<PrivateImplementationDetails>.DD7477A8700AFDB6AF625224701A94969BBF0CCA848ADB3EC213712984D3EE12).FieldHandle);
			if (base.Bot.HasInGraveyard(targets))
			{
				base.AI.SelectNextCard(new int[]
				{
					3717252, 77723643, 48048590, 30328508, 44335251, 77558536, 95503687, 4939890, 14558127, 59438930,
					37445295, 23434538, 33420078, 67441435, 55623480
				});
			}
			else
			{
				base.AI.SelectNextCard(new int[]
				{
					3717252, 77723643, 48048590, 30328508, 44335251, 77558536, 95503687, 4939890, 14558127, 59438930,
					37445295, 23434538, 33420078, 67441435, 55623480
				});
			}
			base.AI.SelectThirdCard(new int[] { 18940556 });
			return base.Enemy.GetMonsterCount() >= 1;
		}

		// Token: 0x060016EF RID: 5871 RVA: 0x0008876C File Offset: 0x0008696C
		private bool FairyTailSnowsummon()
		{
			return base.Util.GetBestEnemyMonster(true, true) != null;
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x00088780 File Offset: 0x00086980
		private bool FairyTailSnoweff()
		{
			if (base.Card.Location == CardLocation.MonsterZone)
			{
				base.AI.SelectCard(base.Util.GetBestEnemyMonster(true, true));
				return true;
			}
			int spell_count = 0;
			IList<ClientCard> grave = base.Bot.Graveyard;
			IList<ClientCard> all = new List<ClientCard>();
			foreach (ClientCard check in grave)
			{
				if (check.IsCode(80280944))
				{
					all.Add(check);
				}
			}
			foreach (ClientCard check2 in grave)
			{
				if (check2.HasType(CardType.Spell) || check2.HasType(CardType.Trap))
				{
					spell_count++;
					all.Add(check2);
				}
			}
			foreach (ClientCard check3 in grave)
			{
				if (check3.HasType(CardType.Monster))
				{
					all.Add(check3);
				}
			}
			if (base.Util.ChainContainsCard(55623480))
			{
				return false;
			}
			if ((base.Duel.Player == 1 && base.Duel.Phase == DuelPhase.BattleStart && base.Bot.BattlingMonster == null && this.Enemy_atk >= base.Bot.LifePoints) || (base.Duel.Player == 0 && base.Duel.Phase == DuelPhase.BattleStart && base.Enemy.BattlingMonster == null && base.Enemy.LifePoints <= 1850))
			{
				base.AI.SelectCard(all);
				base.AI.SelectNextCard(base.Util.GetBestEnemyMonster(false, false));
				return true;
			}
			return false;
		}

		// Token: 0x060016F1 RID: 5873 RVA: 0x0008895C File Offset: 0x00086B5C
		private bool SouleatingOviraptoreff()
		{
			if (!this.OvertexCoatlseff_used && base.Bot.GetRemainingCount(41782653, 3) > 0)
			{
				base.AI.SelectCard(41782653);
				base.AI.SelectOption(0);
			}
			else
			{
				base.AI.SelectCard(18940556);
				base.AI.SelectOption(1);
			}
			return true;
		}

		// Token: 0x060016F2 RID: 5874 RVA: 0x000889C0 File Offset: 0x00086BC0
		private bool GlowUpBulbeff()
		{
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			using (IEnumerator<ClientCard> enumerator = ((IEnumerable<ClientCard>)base.Bot.GetMonstersInExtraZone()).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.HasType(CardType.Fusion))
					{
						return false;
					}
				}
			}
			if (base.Bot.HasInMonstersZone(95503687, false, false, false) || base.Bot.HasInMonstersZone(55623480, false, false, false) || base.Bot.HasInMonstersZone(48048590, false, false, false) || base.Bot.HasInMonstersZone(44335251, false, false, false) || base.Bot.HasInMonstersZone(80280944, false, false, false) || base.Bot.HasInMonstersZone(77558536, false, false, false))
			{
				base.AI.SelectPosition(CardPosition.FaceUpDefence);
				return true;
			}
			return false;
		}

		// Token: 0x060016F3 RID: 5875 RVA: 0x0000763C File Offset: 0x0000583C
		private bool TG_WonderMagicianeff()
		{
			return true;
		}

		// Token: 0x060016F4 RID: 5876 RVA: 0x00088ABC File Offset: 0x00086CBC
		private bool AllureofDarkness()
		{
			IEnumerable<ClientCard> hand = base.Bot.Hand;
			ClientCard mat = null;
			foreach (ClientCard card in hand)
			{
				if (card.HasAttribute(CardAttribute.Dark))
				{
					mat = card;
					break;
				}
			}
			return mat != null;
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x00088B20 File Offset: 0x00086D20
		private bool Reborneff()
		{
			if (base.Bot.HasInGraveyard(18940556) && this.Ultimate_ss > 0)
			{
				base.AI.SelectCard(18940556);
				return true;
			}
			if (!base.Util.IsOneEnemyBetter(true))
			{
				return false;
			}
			IList<int> targets = new int[] { 20366274, 93332803, 55063751, 44335251 };
			if (!base.Bot.HasInGraveyard(targets))
			{
				return false;
			}
			base.AI.SelectCard(targets);
			return true;
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x0000763C File Offset: 0x0000583C
		private bool PotofAvariceeff()
		{
			return true;
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x000348E3 File Offset: 0x00032AE3
		private bool MaxxC()
		{
			return !base.DefaultCheckWhetherCardIsNegated(base.Card) && base.Duel.Player == 1;
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x00088B99 File Offset: 0x00086D99
		private bool SetIsFieldEmpty()
		{
			return !base.Bot.IsFieldEmpty();
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x00088BA9 File Offset: 0x00086DA9
		private bool SpellSetZone()
		{
			return base.Bot.GetHandCount() > 6 && base.Duel.Phase == DuelPhase.Main2;
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x00088BD0 File Offset: 0x00086DD0
		private bool ChargeOfTheLightBrigadeEffect()
		{
			if (base.Bot.HasInGraveyard(77558536) || base.Bot.HasInHand(77558536))
			{
				base.AI.SelectCard(95503687);
			}
			else
			{
				base.AI.SelectCard(77558536);
			}
			return true;
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x00088C24 File Offset: 0x00086E24
		private bool SinisterShadowGameseff()
		{
			if (base.Bot.HasInGraveyard(44394295))
			{
				base.AI.SelectCard(4904633);
			}
			else
			{
				base.AI.SelectCard(new int[] { 3717252 });
			}
			return true;
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x00088C64 File Offset: 0x00086E64
		private bool ShaddollCoreeff()
		{
			if (base.Card.Location != CardLocation.SpellZone)
			{
				return true;
			}
			if ((base.Duel.Player == 1 && base.Bot.BattlingMonster == null && base.Duel.Phase == DuelPhase.BattleStart) || base.DefaultOnBecomeTarget())
			{
				Logger.DebugWriteLine("+++++++++++ShaddollCoreeffdododoo++++++++++");
				base.AI.SelectPosition(CardPosition.FaceUpDefence);
				return true;
			}
			return false;
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x00088CCC File Offset: 0x00086ECC
		private bool ShaddollFusioneff()
		{
			foreach (ClientCard extra_monster in base.Bot.GetMonstersInExtraZone())
			{
				if (extra_monster.HasType(CardType.Xyz) || extra_monster.HasType(CardType.Fusion) || extra_monster.HasType(CardType.Synchro))
				{
					return false;
				}
			}
			bool deck_check = false;
			foreach (ClientCard monster in base.Enemy.GetMonsters())
			{
				if (monster.HasType(CardType.Synchro) || monster.HasType(CardType.Fusion) || monster.HasType(CardType.Xyz) || monster.HasType(CardType.Link))
				{
					deck_check = true;
				}
			}
			if (deck_check)
			{
				base.AI.SelectCard(new int[] { 20366274, 74822425, 48424886, 94977269 });
				base.AI.SelectNextCard(new int[] { 30328508, 3717252, 4939890, 77723643, 37445295, 55623480 });
				base.AI.SelectPosition(CardPosition.FaceUpAttack);
				return true;
			}
			if (base.Enemy.GetMonsterCount() == 0)
			{
				int dark_count = 0;
				IEnumerable<ClientCard> hand = base.Bot.Hand;
				IList<ClientCard> m = base.Bot.MonsterZone;
				IList<ClientCard> all = new List<ClientCard>();
				foreach (ClientCard monster2 in hand)
				{
					if (dark_count == 2)
					{
						break;
					}
					if (monster2.HasAttribute(CardAttribute.Dark))
					{
						dark_count++;
						all.Add(monster2);
					}
				}
				foreach (ClientCard monster3 in m)
				{
					if (dark_count == 2)
					{
						break;
					}
					if (monster3 != null && monster3.HasAttribute(CardAttribute.Dark))
					{
						dark_count++;
						all.Add(monster3);
					}
				}
				if (dark_count == 2)
				{
					base.AI.SelectCard(94977269);
					base.AI.SelectMaterials(all, 0);
					base.AI.SelectPosition(CardPosition.FaceUpAttack);
					return true;
				}
			}
			if (!base.Util.IsOneEnemyBetter(false))
			{
				return false;
			}
			using (IEnumerator<ClientCard> enumerator2 = base.Bot.Hand.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Current.HasAttribute(CardAttribute.Light))
					{
						base.AI.SelectCard(20366274);
						base.AI.SelectPosition(CardPosition.FaceUpAttack);
						return true;
					}
				}
			}
			foreach (ClientCard monster4 in base.Bot.GetMonsters())
			{
				if (monster4 == null)
				{
					break;
				}
				if (monster4.HasAttribute(CardAttribute.Light))
				{
					base.AI.SelectCard(20366274);
					base.AI.SelectPosition(CardPosition.FaceUpAttack);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x0008900C File Offset: 0x0008720C
		private bool ElShaddollShekhinagaeff()
		{
			if (base.Card.Location != CardLocation.MonsterZone)
			{
				return true;
			}
			if (base.DefaultBreakthroughSkill())
			{
				base.AI.SelectCard(new int[] { 3717252, 30328508, 4939890, 77723643, 37445295 });
				return true;
			}
			return false;
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x00089047 File Offset: 0x00087247
		private bool ElShaddollGrysraeff()
		{
			CardLocation location = base.Card.Location;
			return true;
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x00089058 File Offset: 0x00087258
		private bool ElShaddollConstructeff()
		{
			if (!this.ShaddollBeast_used)
			{
				base.AI.SelectCard(3717252);
			}
			else
			{
				base.AI.SelectCard(37445295);
			}
			return true;
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x00089088 File Offset: 0x00087288
		private bool ShaddollSquamataeff()
		{
			this.ShaddollSquamata_used = true;
			if (base.Card.Location != CardLocation.MonsterZone)
			{
				if (base.Util.ChainContainsCard(20366274))
				{
					if (!base.Bot.HasInHand(44394295) && base.Bot.HasInGraveyard(44394295))
					{
						base.AI.SelectNextCard(4904633);
					}
					if (!this.ShaddollBeast_used)
					{
						base.AI.SelectNextCard(3717252);
					}
					else if (!this.ShaddollFalco_used)
					{
						base.AI.SelectNextCard(37445295);
					}
					else if (!this.ShaddollHedgehog_used)
					{
						base.AI.SelectNextCard(4939890);
					}
				}
				else
				{
					if (!base.Bot.HasInHand(44394295) && base.Bot.HasInGraveyard(44394295))
					{
						base.AI.SelectCard(4904633);
					}
					if (!this.ShaddollBeast_used)
					{
						base.AI.SelectCard(3717252);
					}
					else if (!this.ShaddollFalco_used)
					{
						base.AI.SelectCard(37445295);
					}
					else if (!this.ShaddollHedgehog_used)
					{
						base.AI.SelectCard(4939890);
					}
				}
			}
			else
			{
				if (base.Enemy.GetMonsterCount() == 0)
				{
					return false;
				}
				ClientCard target = base.Util.GetBestEnemyMonster(false, false);
				base.AI.SelectCard(target);
			}
			return true;
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x000891FC File Offset: 0x000873FC
		private bool ShaddollBeasteff()
		{
			this.ShaddollBeast_used = true;
			return true;
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x00089206 File Offset: 0x00087406
		private bool ShaddollFalcoeff()
		{
			this.ShaddollFalco_used = true;
			if (base.Card.Location != CardLocation.MonsterZone)
			{
				return true;
			}
			base.AI.SelectCard(new int[] { 20366274, 74822425, 48424886, 94977269, 30328508 });
			return true;
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x0008923C File Offset: 0x0008743C
		private bool ShaddollHedgehogeff()
		{
			this.ShaddollHedgehog_used = true;
			if (base.Card.Location != CardLocation.MonsterZone)
			{
				if (base.Util.ChainContainsCard(20366274))
				{
					base.AI.SelectNextCard(new int[] { 37445295, 30328508, 77723643 });
				}
				else
				{
					base.AI.SelectCard(new int[] { 30328508, 77723643 });
				}
			}
			else
			{
				base.AI.SelectCard(new int[] { 44394295, 77505534 });
			}
			return true;
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x000892D4 File Offset: 0x000874D4
		private bool ShaddollDragoneff()
		{
			this.ShaddollDragon_used = true;
			if (base.Card.Location == CardLocation.MonsterZone)
			{
				ClientCard target = base.Util.GetBestEnemyCard(false, false);
				base.AI.SelectCard(target);
				return true;
			}
			if (base.Enemy.GetSpellCount() == 0)
			{
				return false;
			}
			ClientCard target2 = base.Util.GetBestEnemySpell(false);
			base.AI.SelectCard(target2);
			return true;
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x0008933C File Offset: 0x0008753C
		private bool LostWindeff()
		{
			if (base.Card.Location == CardLocation.Grave)
			{
				return true;
			}
			using (List<ClientCard>.Enumerator enumerator = base.Enemy.GetMonsters().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Attack >= 2000)
					{
						return base.DefaultBreakthroughSkill();
					}
				}
			}
			return false;
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x000893B8 File Offset: 0x000875B8
		private bool FoolishBurialEffect()
		{
			if (base.Bot.GetRemainingCount(38179121, 3) <= 0)
			{
				base.AI.SelectCard(new int[] { 30328508, 55623480 });
				return true;
			}
			if (!this.OvertexCoatlseff_used)
			{
				base.AI.SelectCard(new int[] { 41782653 });
				return true;
			}
			return false;
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x00089420 File Offset: 0x00087620
		public bool Hand_act_eff()
		{
			return !base.DefaultCheckWhetherCardIsNegated(base.Card) && (!base.Card.IsCode(59438930) || base.Card.Location != CardLocation.Hand || !base.Bot.HasInMonstersZone(59438930, false, false, false)) && base.Duel.LastChainPlayer == 1;
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x00089484 File Offset: 0x00087684
		private bool Michaelsp()
		{
			IList<int> targets = new int[] { 77558536, 95503687 };
			if (!base.Bot.HasInMonstersZone(targets, false, false, false))
			{
				return false;
			}
			base.AI.SelectCard(targets);
			return true;
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x000894C8 File Offset: 0x000876C8
		private bool Michaeleff()
		{
			if (base.Card.Location == CardLocation.Grave)
			{
				return true;
			}
			if (base.Bot.LifePoints <= 1000)
			{
				return false;
			}
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			ClientCard select = base.Util.GetBestEnemyCard(false, false);
			if (select == null)
			{
				return false;
			}
			if (select != null)
			{
				base.AI.SelectCard(select);
				return true;
			}
			return false;
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x00089530 File Offset: 0x00087730
		private bool MinervaTheExaltedEffect()
		{
			if (base.Card.Location == CardLocation.MonsterZone)
			{
				return base.Bot.Deck.Count > 10;
			}
			IList<ClientCard> targets = new List<ClientCard>();
			ClientCard target = base.Util.GetBestEnemyMonster(false, false);
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
				if (targets.Count >= 3)
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
				if (targets.Count >= 3)
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
			base.AI.SelectCard(0);
			base.AI.SelectNextCard(targets);
			return true;
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x0008967C File Offset: 0x0008787C
		public bool CrystronNeedlefibersp()
		{
			if (base.Bot.HasInMonstersZone(20366274, false, false, false) || base.Bot.HasInMonstersZone(48424886, false, false, false) || base.Bot.HasInMonstersZone(74822425, false, false, false) || base.Bot.HasInMonstersZone(94977269, false, false, false))
			{
				return false;
			}
			if (this.CrystronNeedlefibereff_used)
			{
				return false;
			}
			if (base.Bot.HasInMonstersZone(50588353, false, false, false))
			{
				return false;
			}
			IList<int> check = new int[] { 67441435, 55623480, 48048590, 44335251, 80280944, 95503687, 77558536 };
			int count = 0;
			using (List<ClientCard>.Enumerator enumerator = base.Bot.GetMonsters().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsCode(new int[] { 67441435, 55623480, 48048590, 44335251, 80280944, 95503687, 77558536 }))
					{
						count++;
					}
				}
			}
			if (!base.Bot.HasInMonstersZone(67441435, false, false, false) || count < 2)
			{
				return false;
			}
			base.AI.SelectCard(check);
			base.AI.SelectNextCard(check);
			return true;
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x000897A8 File Offset: 0x000879A8
		public bool CrystronNeedlefibereff()
		{
			bool DarkHole = false;
			foreach (ClientCard card in base.Enemy.GetSpells())
			{
				if (card.IsCode(53129443) && card.IsFaceup())
				{
					DarkHole = true;
				}
			}
			if (base.Duel.Player == 0)
			{
				this.CrystronNeedlefibereff_used = true;
				base.AI.SelectCard(new int[] { 59438930, 67441435, 33420078, 37445295 });
				return true;
			}
			if (DarkHole || base.Util.IsChainTarget(base.Card) || base.Util.GetProblematicEnemySpell() != null)
			{
				base.AI.SelectCard(98558751);
				return true;
			}
			if (base.Duel.Player == 1 && base.Duel.Phase == DuelPhase.BattleStart && base.Util.IsOneEnemyBetterThanValue(1500, true))
			{
				base.AI.SelectCard(98558751);
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

		// Token: 0x0600170E RID: 5902 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool ScarlightRedDragonsp()
		{
			return false;
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x000898E8 File Offset: 0x00087AE8
		private bool ScarlightRedDragoneff()
		{
			IList<ClientCard> targets = new List<ClientCard>();
			ClientCard target = base.Util.GetBestEnemyMonster(false, false);
			if (target != null)
			{
				targets.Add(target);
				base.AI.SelectCard(targets);
				return true;
			}
			return false;
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x000675A8 File Offset: 0x000657A8
		private bool CrystalWingSynchroDragoneff()
		{
			return base.Duel.LastChainPlayer != 0;
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool Sdulldeateff()
		{
			return false;
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x00089924 File Offset: 0x00087B24
		private bool BlackRoseMoonlightDragoneff()
		{
			IList<ClientCard> targets = new List<ClientCard>();
			ClientCard target = base.Util.GetBestEnemyMonster(false, false);
			if (target != null)
			{
				targets.Add(target);
				base.AI.SelectCard(targets);
				return true;
			}
			return false;
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool RedWyvernsp()
		{
			return false;
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x00089960 File Offset: 0x00087B60
		private bool RedWyverneff()
		{
			IEnumerable<ClientCard> monsters = base.Enemy.GetMonsters();
			ClientCard best = null;
			foreach (ClientCard monster in monsters)
			{
				if (monster.Attack >= 2400)
				{
					best = monster;
				}
			}
			if (best != null)
			{
				base.AI.SelectCard(best);
				return true;
			}
			return false;
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x000899D0 File Offset: 0x00087BD0
		private bool CoralDragoneff()
		{
			if (base.Card.Location != CardLocation.MonsterZone)
			{
				return true;
			}
			IList<ClientCard> targets = new List<ClientCard>();
			ClientCard target = base.Util.GetBestEnemyMonster(false, false);
			if (target != null)
			{
				targets.Add(target);
			}
			ClientCard target2 = base.Util.GetBestEnemySpell(false);
			if (target2 != null)
			{
				targets.Add(target2);
			}
			else
			{
				if (base.Util.IsChainTarget(base.Card) || base.Util.GetProblematicEnemySpell() != null)
				{
					base.AI.SelectCard(targets);
					return true;
				}
				if (base.Duel.Player == 1 && base.Duel.Phase == DuelPhase.BattleStart && base.Util.IsOneEnemyBetterThanValue(2400, true))
				{
					base.AI.SelectCard(targets);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x00089A90 File Offset: 0x00087C90
		public override bool OnPreBattleBetween(ClientCard attacker, ClientCard defender)
		{
			if (!defender.IsMonsterHasPreventActivationEffectInBattle())
			{
				if (attacker.IsCode(20366274) && !attacker.IsDisabled())
				{
					attacker.RealPower = 9999;
				}
				if (attacker.IsCode(18940556) && !attacker.IsDisabled() && defender.IsDefense())
				{
					attacker.RealPower = 9999;
				}
			}
			return base.OnPreBattleBetween(attacker, defender);
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x0000763C File Offset: 0x0000583C
		public override bool OnSelectHand()
		{
			return true;
		}

		// Token: 0x04001B04 RID: 6916
		private int Ultimate_ss;

		// Token: 0x04001B05 RID: 6917
		private int Enemy_atk;

		// Token: 0x04001B06 RID: 6918
		private bool Pillused;

		// Token: 0x04001B07 RID: 6919
		private bool CrystronNeedlefibereff_used;

		// Token: 0x04001B08 RID: 6920
		private bool OvertexCoatlseff_used;

		// Token: 0x04001B09 RID: 6921
		private bool ShaddollBeast_used;

		// Token: 0x04001B0A RID: 6922
		private bool ShaddollFalco_used;

		// Token: 0x04001B0B RID: 6923
		private bool ShaddollSquamata_used;

		// Token: 0x04001B0C RID: 6924
		private bool ShaddollDragon_used;

		// Token: 0x04001B0D RID: 6925
		private bool ShaddollHedgehog_used;

		// Token: 0x02000353 RID: 851
		public class CardId
		{
			// Token: 0x04001B0E RID: 6926
			public const int UltimateConductorTytanno = 18940556;

			// Token: 0x04001B0F RID: 6927
			public const int DogorantheMadFlameKaiju = 93332803;

			// Token: 0x04001B10 RID: 6928
			public const int GamecieltheSeaTurtleKaiju = 55063751;

			// Token: 0x04001B11 RID: 6929
			public const int RadiantheMultidimensionalKaiju = 28674152;

			// Token: 0x04001B12 RID: 6930
			public const int OvertexCoatls = 41782653;

			// Token: 0x04001B13 RID: 6931
			public const int ShaddollBeast = 3717252;

			// Token: 0x04001B14 RID: 6932
			public const int GiantRex = 80280944;

			// Token: 0x04001B15 RID: 6933
			public const int ShaddollDragon = 77723643;

			// Token: 0x04001B16 RID: 6934
			public const int FairyTailSnow = 55623480;

			// Token: 0x04001B17 RID: 6935
			public const int KeeperOfDragonicMagic = 48048590;

			// Token: 0x04001B18 RID: 6936
			public const int ShaddollSquamata = 30328508;

			// Token: 0x04001B19 RID: 6937
			public const int SouleatingOviraptor = 44335251;

			// Token: 0x04001B1A RID: 6938
			public const int Raiden = 77558536;

			// Token: 0x04001B1B RID: 6939
			public const int Lumina = 95503687;

			// Token: 0x04001B1C RID: 6940
			public const int ShaddollHedgehog = 4939890;

			// Token: 0x04001B1D RID: 6941
			public const int AshBlossom = 14558127;

			// Token: 0x04001B1E RID: 6942
			public const int GhostOgre = 59438930;

			// Token: 0x04001B1F RID: 6943
			public const int ShaddollFalco = 37445295;

			// Token: 0x04001B20 RID: 6944
			public const int MaxxC = 23434538;

			// Token: 0x04001B21 RID: 6945
			public const int PlaguespreaderZombie = 33420078;

			// Token: 0x04001B22 RID: 6946
			public const int GlowUpBulb = 67441435;

			// Token: 0x04001B23 RID: 6947
			public const int AllureofDarkness = 1475311;

			// Token: 0x04001B24 RID: 6948
			public const int ThatGrassLooksgreener = 11110587;

			// Token: 0x04001B25 RID: 6949
			public const int HarpiesFeatherDuster = 18144506;

			// Token: 0x04001B26 RID: 6950
			public const int DoubleEvolutionPill = 38179121;

			// Token: 0x04001B27 RID: 6951
			public const int ShaddollFusion = 44394295;

			// Token: 0x04001B28 RID: 6952
			public const int PotOfAvarice = 67169062;

			// Token: 0x04001B29 RID: 6953
			public const int FoolishBurial = 81439173;

			// Token: 0x04001B2A RID: 6954
			public const int MonsterReborn = 83764718;

			// Token: 0x04001B2B RID: 6955
			public const int ChargeOfTheLightBrigade = 94886282;

			// Token: 0x04001B2C RID: 6956
			public const int InterruptedKaijuSlumber = 99330325;

			// Token: 0x04001B2D RID: 6957
			public const int infiniteTransience = 10045474;

			// Token: 0x04001B2E RID: 6958
			public const int LostWind = 74003290;

			// Token: 0x04001B2F RID: 6959
			public const int SinisterShadowGames = 77505534;

			// Token: 0x04001B30 RID: 6960
			public const int ShaddollCore = 4904633;

			// Token: 0x04001B31 RID: 6961
			public const int ElShaddollShekhinaga = 74822425;

			// Token: 0x04001B32 RID: 6962
			public const int ElShaddollConstruct = 20366274;

			// Token: 0x04001B33 RID: 6963
			public const int ElShaddollGrysra = 48424886;

			// Token: 0x04001B34 RID: 6964
			public const int ElShaddollWinda = 94977269;

			// Token: 0x04001B35 RID: 6965
			public const int CrystalWingSynchroDragon = 50954680;

			// Token: 0x04001B36 RID: 6966
			public const int ScarlightRedDragon = 80666118;

			// Token: 0x04001B37 RID: 6967
			public const int Michael = 4779823;

			// Token: 0x04001B38 RID: 6968
			public const int BlackRoseMoonlightDragon = 33698022;

			// Token: 0x04001B39 RID: 6969
			public const int RedWyvern = 76547525;

			// Token: 0x04001B3A RID: 6970
			public const int CoralDragon = 42566602;

			// Token: 0x04001B3B RID: 6971
			public const int TG_WonderMagician = 98558751;

			// Token: 0x04001B3C RID: 6972
			public const int MinervaTheExalte = 30100551;

			// Token: 0x04001B3D RID: 6973
			public const int Sdulldeat = 74997493;

			// Token: 0x04001B3E RID: 6974
			public const int CrystronNeedlefiber = 50588353;
		}
	}
}
