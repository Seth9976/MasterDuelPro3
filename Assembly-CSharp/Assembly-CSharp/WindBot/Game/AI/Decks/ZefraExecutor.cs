using System;
using System.Collections.Generic;
using System.Linq;
using YGOSharp.OCGWrapper;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x02000447 RID: 1095
	[Deck("Zefra", "AI_Zefra", "Normal")]
	internal class ZefraExecutor : DefaultExecutor
	{
		// Token: 0x060023B7 RID: 9143 RVA: 0x000E9214 File Offset: 0x000E7414
		public ZefraExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.Activate, 24224830, new Func<bool>(this.CalledbytheGraveEffect));
			base.AddExecutor(ExecutorType.Activate, 27548199, new Func<bool>(this.BorreloadSavageDragonEffect_2));
			base.AddExecutor(ExecutorType.Activate, 33158448, new Func<bool>(this.ResetFlag));
			base.AddExecutor(ExecutorType.Activate, 57831349, new Func<bool>(this.NinePillarsofYangZingEffect));
			base.AddExecutor(ExecutorType.Activate, 35561352, new Func<bool>(this.ZefraDivineStrikeEffect));
			base.AddExecutor(ExecutorType.Activate, 79606837, new Func<bool>(this.HeraldoftheArcLightEffect));
			base.AddExecutor(ExecutorType.Activate, 88581108, new Func<bool>(this.TruKingofAllCalamitiesEffect));
			base.AddExecutor(ExecutorType.Activate, 38814750, new Func<bool>(this.ResetFlag));
			base.AddExecutor(ExecutorType.Activate, 23434538, new Func<bool>(base.DefaultMaxxC));
			base.AddExecutor(ExecutorType.Activate, 69610326, new Func<bool>(this.SupremeKingDragonDarkwurmEffect));
			base.AddExecutor(ExecutorType.Activate, 92559258, new Func<bool>(this.ServantofEndymionEffect));
			base.AddExecutor(ExecutorType.Activate, 73628505, new Func<bool>(this.TerraformingEffect));
			base.AddExecutor(ExecutorType.Activate, 38943357, new Func<bool>(this.ResetFlag));
			base.AddExecutor(ExecutorType.Activate, 41620959, new Func<bool>(this.DragonShrineEffect));
			base.AddExecutor(ExecutorType.Activate, 81439173, new Func<bool>(this.FoolishBurialEffect));
			base.AddExecutor(ExecutorType.Activate, 46372010, new Func<bool>(this.DarkContractwiththGateEffect));
			base.AddExecutor(ExecutorType.Activate, 73628505, new Func<bool>(this.ResetFlag));
			base.AddExecutor(ExecutorType.Activate, 32354768, new Func<bool>(this.OracleofZefraEffect));
			base.AddExecutor(ExecutorType.Activate, 74580251, new Func<bool>(this.ZefraProvidenceEffect));
			base.AddExecutor(ExecutorType.Activate, 76794549, new Func<bool>(this.AstrographSorcererEffect));
			base.AddExecutor(ExecutorType.Activate, 24094258, new Func<bool>(this.HeavymetalfoesElectrumiteEffect));
			base.AddExecutor(ExecutorType.Summon, 69610326, new Func<bool>(this.SupremeKingDragonDarkwurmSummon));
			base.AddExecutor(ExecutorType.Activate, 96227613, new Func<bool>(this.SupremeKingGateZeroEffect));
			base.AddExecutor(ExecutorType.Activate, 21495657, new Func<bool>(this.Zefraxi_TreasureoftheYangZingEffect));
			base.AddExecutor(ExecutorType.Activate, 96223501, new Func<bool>(this.SatellarknightZefrathubanEffect));
			base.AddExecutor(ExecutorType.Activate, 57777714, new Func<bool>(this.RitualBeastTamerZeframpilicaEffect));
			base.AddExecutor(ExecutorType.Activate, 58990362, new Func<bool>(this.SecretoftheYangZingEffect));
			base.AddExecutor(ExecutorType.Activate, 20773176, new Func<bool>(this.FlameBeastoftheNekrozEffect));
			base.AddExecutor(ExecutorType.Activate, 95401059, new Func<bool>(this.ShaddollZefracoreEffect));
			base.AddExecutor(ExecutorType.Activate, 22617205, new Func<bool>(this.StellarknightZefraxcitonEffect));
			base.AddExecutor(ExecutorType.Activate, 69610326, new Func<bool>(this.SupremeKingGateZeroEffect));
			base.AddExecutor(ExecutorType.Activate, 76794549, new Func<bool>(this.SupremeKingGateZeroEffect));
			base.AddExecutor(ExecutorType.Activate, 29432356, new Func<bool>(this.ZefraathEffect));
			base.AddExecutor(ExecutorType.Activate, 11609969, new Func<bool>(this.DDSavantKeplerEffect));
			base.AddExecutor(ExecutorType.Summon, 11609969, new Func<bool>(this.DDSavantKeplerSummon));
			base.AddExecutor(ExecutorType.Activate, 92559258, new Func<bool>(this.ServantofEndymionEffect_3));
			base.AddExecutor(ExecutorType.Activate, 27354732, new Func<bool>(this.MythicalBeastJackalKingEffect));
			base.AddExecutor(ExecutorType.SpSummon, new Func<bool>(this.Psummon));
			base.AddExecutor(ExecutorType.Activate, 2295440, new Func<bool>(this.OneforOneEffect));
			base.AddExecutor(ExecutorType.Activate, 92559258, new Func<bool>(this.ServantofEndymionEffect_2));
			base.AddExecutor(ExecutorType.Activate, 50588353, new Func<bool>(this.CrystronHalqifibraxEffect));
			base.AddExecutor(ExecutorType.SpSummon, 96157835, new Func<bool>(this.Raidraptor_ArsenalFalconSummon));
			base.AddExecutor(ExecutorType.Activate, 96157835, new Func<bool>(this.Raidraptor_ArsenalFalconEffect));
			base.AddExecutor(ExecutorType.SpSummon, 24094258, new Func<bool>(this.HeavymetalfoesElectrumiteSummon));
			base.AddExecutor(ExecutorType.SpSummon, 80696379, new Func<bool>(this.Odd_EyesMeteorburstDragonSummon));
			base.AddExecutor(ExecutorType.Activate, 80696379, new Func<bool>(this.Odd_EyesMeteorburstDragonEffect));
			base.AddExecutor(ExecutorType.SpSummon, 36429703, new Func<bool>(this.Raidraptor_WiseStrixSummon));
			base.AddExecutor(ExecutorType.Activate, 36429703, new Func<bool>(this.Raidraptor_WiseStrixEffect));
			base.AddExecutor(ExecutorType.Activate, 14785765, new Func<bool>(this.Blackwing_ZephyrostheEliteEffect));
			base.AddExecutor(ExecutorType.SpSummon, 73347079, new Func<bool>(this.Raidraptor_ForceStrixSummon));
			base.AddExecutor(ExecutorType.Activate, 73347079, new Func<bool>(this.Raidraptor_ForceStrixEffect));
			base.AddExecutor(ExecutorType.Activate, 23581825, new Func<bool>(this.ResetFlag));
			base.AddExecutor(ExecutorType.Activate, 52159691, new Func<bool>(this.Raider_WingEffect));
			base.AddExecutor(ExecutorType.SpSummon, 31314549);
			base.AddExecutor(ExecutorType.SpSummon, 74997493, new Func<bool>(this.SaryujaSkullDreadSummon));
			base.AddExecutor(ExecutorType.Activate, 74997493, new Func<bool>(this.SaryujaSkullDreadEffect));
			base.AddExecutor(ExecutorType.SpSummon, 65536818, new Func<bool>(this.Denglong_FirstoftheYangZingSummon_2));
			base.AddExecutor(ExecutorType.SpSummon, 27548199, new Func<bool>(this.BorreloadSavageDragonSummon_2));
			base.AddExecutor(ExecutorType.SpSummon, 50588353, new Func<bool>(this.CrystronHalqifibraxSummon));
			base.AddExecutor(ExecutorType.SpSummon, 41999284, new Func<bool>(this.LinkuribohSummon));
			base.AddExecutor(ExecutorType.Activate, 19580308, new Func<bool>(this.DDLamiaEffect));
			base.AddExecutor(ExecutorType.SpSummon, 44097050, new Func<bool>(this.MechaPhantomBeastAuroradonSummon));
			base.AddExecutor(ExecutorType.Activate, 44097050, new Func<bool>(this.MechaPhantomBeastAuroradonEffect));
			base.AddExecutor(ExecutorType.SpSummon, 65536818, new Func<bool>(this.Denglong_FirstoftheYangZingSummon));
			base.AddExecutor(ExecutorType.Activate, 65536818, new Func<bool>(this.Denglong_FirstoftheYangZingEffect));
			base.AddExecutor(ExecutorType.SpSummon, 27548199, new Func<bool>(this.BorreloadSavageDragonSummon));
			base.AddExecutor(ExecutorType.Activate, 27548199, new Func<bool>(this.BorreloadSavageDragonEffect));
			base.AddExecutor(ExecutorType.SpSummon, 79606837);
			base.AddExecutor(ExecutorType.SpSummon, 33158448, new Func<bool>(this.F_A_DawnDragsterSummon));
			base.AddExecutor(ExecutorType.SpSummon, 74586817, new Func<bool>(this.BorreloadSavageDragonSummon));
			base.AddExecutor(ExecutorType.Activate, 74586817, new Func<bool>(this.PSY_FramelordOmegaEffect));
			base.AddExecutor(ExecutorType.Activate, 41999284, new Func<bool>(this.LinkuribohEffect));
			base.AddExecutor(ExecutorType.Activate, 72291078);
			base.AddExecutor(ExecutorType.Activate, 9742784, new Func<bool>(this.JetSynchronEffect));
			base.AddExecutor(ExecutorType.Activate, 14785765, new Func<bool>(this.Blackwing_ZephyrostheEliteEffect_2));
			base.AddExecutor(ExecutorType.Summon, 9742784, new Func<bool>(this.DDLamiaSummon));
			base.AddExecutor(ExecutorType.Summon, 19580308, new Func<bool>(this.DDLamiaSummon));
			base.AddExecutor(ExecutorType.Summon, 94693857, new Func<bool>(this.DDLamiaSummon));
			base.AddExecutor(ExecutorType.Summon, 61488417, new Func<bool>(this.DDLamiaSummon));
			List<int> p_summon_ids = new List<int> { 21495657, 96223501, 92559258, 57777714, 11609969, 22617205, 95401059, 69610326 };
			for (int i = 0; i < p_summon_ids.Count; i++)
			{
				base.AddExecutor(ExecutorType.Summon, p_summon_ids[i], new Func<bool>(this.DefaultSummon));
			}
			base.AddExecutor(ExecutorType.Summon, new Func<bool>(this.DefaultSummon));
			base.AddExecutor(ExecutorType.Activate, 94693857, new Func<bool>(this.ResetFlag));
			base.AddExecutor(ExecutorType.Activate, 3611830, new Func<bool>(this.TheMightyMasterofMagicEffect));
			base.AddExecutor(ExecutorType.Activate, 5560911, new Func<bool>(this.DestrudotheLostDragon_FrissonEffect));
			base.AddExecutor(ExecutorType.Summon, 14785765, new Func<bool>(this.DefaultSummon_2));
			base.AddExecutor(ExecutorType.Summon, new Func<bool>(this.DefaultSummon_2));
			base.AddExecutor(ExecutorType.SpSummon, 41999284);
			base.AddExecutor(ExecutorType.SpellSet, new Func<bool>(this.SpellSet));
			base.AddExecutor(ExecutorType.SpellSet, new Func<bool>(this.SpellSet_2));
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(base.DefaultMonsterRepos));
			base.AddExecutor(ExecutorType.Activate, new Func<bool>(this.DefaultPActivate));
			base.AddExecutor(ExecutorType.GoToEndPhase, new Func<bool>(this.GoToEndPhase));
		}

		// Token: 0x060023B8 RID: 9144 RVA: 0x000E9B00 File Offset: 0x000E7D00
		public override void OnNewTurn()
		{
			if (this.duel_start)
			{
				this.duel_start = false;
				base.AI.SendCustomChat(0, Array.Empty<object>());
			}
			this.activate_SupremeKingDragonDarkwurm_1 = false;
			this.activate_SupremeKingDragonDarkwurm_2 = false;
			this.activate_JetSynchron = false;
			this.activate_DestrudotheLostDragon_Frisson = false;
			this.activate_ZefraProvidence = false;
			this.activate_OracleofZefra = false;
			this.activate_DragonShrine = false;
			this.activate_p_Zefraath = false;
			this.p_summoned = false;
			this.summoned = false;
			this.activate_DarkContractwiththGate = false;
			this.activate_SecretoftheYangZing = false;
			this.activate_ShaddollZefracore = false;
			this.activate_SpellPowerMastery = false;
			this.link_summoned = false;
			this.activate_DDLamia = false;
			this.xyz_mode = false;
			this.Blackwing_ZephyrostheElite_activate = false;
			this.HeavymetalfoesElectrumite_activate = false;
			this.spell_activate_count = 0;
			this.p_count = 0;
			this.activate_count = 0;
			this.summon_count = 0;
			this.enemy_activate = false;
			base.OnNewTurn();
		}

		// Token: 0x060023B9 RID: 9145 RVA: 0x000E9BDB File Offset: 0x000E7DDB
		private bool ZefraProvidenceEffect()
		{
			if (base.ActivateDescription != 96)
			{
				this.activate_ZefraProvidence = true;
				return this.BeforeResult(ExecutorType.Activate);
			}
			if (this.should_destory)
			{
				this.should_destory = false;
				return false;
			}
			return this.BeforeResult(ExecutorType.Activate);
		}

		// Token: 0x060023BA RID: 9146 RVA: 0x000E9C10 File Offset: 0x000E7E10
		private List<int> CheckShouldSpsummonExtraMonster()
		{
			List<int> extra_ids = new List<int> { 24094258, 50588353 };
			if (!base.Bot.HasInExtra(24094258))
			{
				extra_ids.Remove(24094258);
			}
			if (!base.Bot.HasInExtra(50588353))
			{
				extra_ids.Remove(50588353);
			}
			if (extra_ids.Count <= 0)
			{
				return extra_ids;
			}
			bool DD_summon_check = false;
			if (base.Bot.HasInExtra(50588353) && ((!this.summoned && this.HasInDeck(11609969) && (this.HasInDeck(46372010) || base.Bot.HasInHandOrInSpellZone(46372010)) && !this.activate_DarkContractwiththGate && this.HasInDeck(19580308)) || (this.func.CardsCheckAny(base.Bot.Hand, new ZefraExecutor.Toos.Delegate(this.func.HasType), new object[] { CardType.Tuner }) && (this.HasInDeck(76794549) || base.Bot.HasInHand(76794549)))))
			{
				DD_summon_check = true;
			}
			if (base.Bot.SpellZone[0] != null && base.Bot.SpellZone[4] != null)
			{
				List<ClientCard> spSummonMonster = this.func.GetPSpSummonMonster(base.Bot, base.Bot.SpellZone[0], base.Bot.SpellZone[4]);
				if (DD_summon_check && spSummonMonster != null)
				{
					List<ClientCard> pSpsummonMonster = this.func.CardsCheckWhere(spSummonMonster, new ZefraExecutor.Toos.Delegate(this.func.HasType), new object[] { CardType.Pendulum });
					List<ClientCard> monsterCards = this.func.CardsCheckWhere(ZefraExecutor.Func.GetZoneCards(base.Bot, CardLocation.MonsterZone, false, false), (ClientCard card) => card.IsFaceup() && card.HasType(CardType.Pendulum), Array.Empty<object>());
					if (ZefraExecutor.Func.MergeList<ClientCard>(new List<ClientCard>[] { pSpsummonMonster, monsterCards }).Count <= 0)
					{
						extra_ids.Remove(24094258);
					}
				}
				else
				{
					extra_ids.Remove(24094258);
				}
			}
			else if ((!base.Bot.HasInHand(32354768) || this.activate_OracleofZefra) && (!base.Bot.HasInHand(74580251) || this.activate_ZefraProvidence) && (!base.Bot.HasInHand(29432356) || this.activate_p_Zefraath))
			{
				extra_ids.Clear();
			}
			if (!DD_summon_check)
			{
				extra_ids.Remove(24094258);
			}
			return extra_ids;
		}

		// Token: 0x060023BB RID: 9147 RVA: 0x000E9EA8 File Offset: 0x000E80A8
		private bool DDLamiaSummon()
		{
			if (!this.IsCanSynchroSummon(base.Card.Level))
			{
				return false;
			}
			if (base.Bot.HasInExtra(41999284) || (base.Bot.HasInExtra(50588353) && ZefraExecutor.Func.GetZoneCards(base.Bot, CardLocation.MonsterZone, true, false).Count > 0))
			{
				this.summoned = true;
				return this.BeforeResult(ExecutorType.Summon);
			}
			return false;
		}

		// Token: 0x060023BC RID: 9148 RVA: 0x000E9F14 File Offset: 0x000E8114
		private bool XyzModeCheck(bool flag1 = false)
		{
			return !this.link_summoned && (base.Bot.HasInExtra(96157835) || !flag1) && this.HasInDeck(14785765) && base.Bot.HasInExtra(73347079) && base.Bot.HasInExtra(36429703) && base.Bot.HasInExtra(88581108) && (this.HasInDeck(52159691) || base.Bot.HasInHand(52159691)) && (this.HasInDeck(31314549) || base.Bot.HasInHand(31314549)) && (this.HasInDeck(23581825) || base.Bot.HasInHand(23581825));
		}

		// Token: 0x060023BD RID: 9149 RVA: 0x000E9FEE File Offset: 0x000E81EE
		private bool Raidraptor_ForceStrixEffect()
		{
			base.AI.SelectCard(52159691);
			base.AI.SelectNextCard(31314549);
			return this.BeforeResult(ExecutorType.Activate);
		}

		// Token: 0x060023BE RID: 9150 RVA: 0x000EA017 File Offset: 0x000E8217
		private bool Raidraptor_ForceStrixSummon()
		{
			return this.xyz_mode && this.BeforeResult(ExecutorType.Summon);
		}

		// Token: 0x060023BF RID: 9151 RVA: 0x000EA02C File Offset: 0x000E822C
		private bool Blackwing_ZephyrostheEliteEffect_2()
		{
			if (!this.xyz_mode && base.Bot.GetMonstersInMainZone().Count > 4)
			{
				return false;
			}
			List<ClientCard> cards = this.func.CardsCheckWhere(ZefraExecutor.Func.GetZoneCards(base.Bot, CardLocation.Onfield, true, false), (ClientCard card) => !this.IsNoLinkCards(card) && !card.IsExtraCard() && (card.Location != CardLocation.SpellZone || !ZefraExecutor.Func.IsCode(card, new int[] { 74997493, 44097050, 24094258, 50588353, 36429703, 41999284 })), Array.Empty<object>());
			if (cards.Count <= 0 || (cards.Count < 2 && this.func.CardsCheckCount(cards, new ZefraExecutor.Toos.Delegate(this.func.HasLevel), new object[] { 4 }) == cards.Count))
			{
				this.Blackwing_ZephyrostheElite_activate = true;
				return false;
			}
			cards.Sort(delegate(ClientCard cardA, ClientCard cardB)
			{
				if (cardA.Location != CardLocation.MonsterZone && cardB.Location == CardLocation.MonsterZone)
				{
					return -1;
				}
				if (cardA.Location == CardLocation.MonsterZone && cardB.Location != CardLocation.MonsterZone)
				{
					return 1;
				}
				if (cardA.Location == CardLocation.SpellZone && cardB.Location == CardLocation.SpellZone)
				{
					if (cardA.IsCode(32354768) && !cardB.IsCode(32354768))
					{
						return -1;
					}
					if (!cardA.IsCode(32354768) && cardB.IsCode(32354768))
					{
						return 1;
					}
					return 0;
				}
				else
				{
					if (!this.xyz_mode)
					{
						return CardContainer.CompareCardAttack(cardA, cardB);
					}
					if (cardA.Level == 4 && cardB.Level != 4)
					{
						return 1;
					}
					if (cardA.Level != 4 && cardB.Level == 4)
					{
						return -1;
					}
					return CardContainer.CompareCardAttack(cardA, cardB);
				}
			});
			this.Blackwing_ZephyrostheElite_activate = false;
			base.AI.SelectCard(cards);
			return this.BeforeResult(ExecutorType.Activate);
		}

		// Token: 0x060023C0 RID: 9152 RVA: 0x000EA0FD File Offset: 0x000E82FD
		public override void OnChaining(int player, ClientCard card)
		{
			if (card == null)
			{
				return;
			}
			if (player == 1 && ZefraExecutor.Func.IsCode(card, new int[] { 14558127, 59438930, 94145021, 38814750, 73642296, 97268402 }))
			{
				this.enemy_activate = true;
			}
			base.OnChaining(player, card);
		}

		// Token: 0x060023C1 RID: 9153 RVA: 0x000EA12F File Offset: 0x000E832F
		private bool BeforeResult(ExecutorType type)
		{
			if (type == ExecutorType.Activate)
			{
				this.ResetFlag();
				this.activate_count++;
			}
			if (type == ExecutorType.Summon)
			{
				this.summon_count++;
			}
			return true;
		}

		// Token: 0x060023C2 RID: 9154 RVA: 0x000EA15C File Offset: 0x000E835C
		private bool GoToEndPhase()
		{
			if (base.Duel.Player == 0 && base.Duel.Turn == 1 && this.func.CardsCheckCount(ZefraExecutor.Func.GetZoneCards(base.Bot, CardLocation.MonsterZone, true, false), new ZefraExecutor.Toos.Delegate(this.IsNoLinkCards), Array.Empty<object>()) <= 0 && this.activate_count + this.summon_count < 5 && !this.enemy_activate)
			{
				base.AI.SendCustomChat(1, Array.Empty<object>());
				return true;
			}
			return false;
		}

		// Token: 0x060023C3 RID: 9155 RVA: 0x000EA1E0 File Offset: 0x000E83E0
		private bool DefaultPActivate()
		{
			return this.PendulumActivate() && ZefraExecutor.Func.IsCode(base.Card, new int[] { 21495657, 58990362 }) && (base.Bot.HasInHandOrInSpellZone(57831349) && this.func.CardsCheckCount(ZefraExecutor.Func.GetZoneCards(base.Bot, CardLocation.SpellZone, true, false), (ClientCard card) => ZefraExecutor.Func.IsCode(base.Card, new int[] { 21495657, 58990362 }), Array.Empty<object>()) <= 0) && this.BeforeResult(ExecutorType.Activate);
		}

		// Token: 0x060023C4 RID: 9156 RVA: 0x000EA262 File Offset: 0x000E8462
		private bool Blackwing_ZephyrostheEliteEffect()
		{
			return this.xyz_mode && this.Blackwing_ZephyrostheEliteEffect_2();
		}

		// Token: 0x060023C5 RID: 9157 RVA: 0x000EA274 File Offset: 0x000E8474
		private bool Raidraptor_WiseStrixSummon()
		{
			if (!this.xyz_mode)
			{
				return false;
			}
			base.AI.SelectMaterials(96157835, 14785765);
			return this.BeforeResult(ExecutorType.Summon);
		}

		// Token: 0x060023C6 RID: 9158 RVA: 0x000EA29C File Offset: 0x000E849C
		private bool Raidraptor_WiseStrixEffect()
		{
			if (base.ActivateDescription != -1)
			{
				return this.BeforeResult(ExecutorType.Activate);
			}
			int count = 0;
			if (this.HasInDeck(31314549))
			{
				count++;
			}
			if (this.HasInDeck(14785765))
			{
				count++;
			}
			if (this.HasInDeck(52159691))
			{
				count++;
			}
			if (count <= 1)
			{
				return false;
			}
			base.AI.SelectCard(52159691);
			return this.BeforeResult(ExecutorType.Activate);
		}

		// Token: 0x060023C7 RID: 9159 RVA: 0x000EA30C File Offset: 0x000E850C
		private bool Raidraptor_ArsenalFalconEffect()
		{
			if (base.Card.Location == CardLocation.MonsterZone)
			{
				base.AI.SelectCard(96227613);
				base.AI.SelectNextCard(new int[] { 14785765, 52159691, 31314549 });
				return this.BeforeResult(ExecutorType.Activate);
			}
			return false;
		}

		// Token: 0x060023C8 RID: 9160 RVA: 0x000EA35C File Offset: 0x000E855C
		private bool Raidraptor_ArsenalFalconSummon()
		{
			if (!this.XyzModeCheck(true))
			{
				return false;
			}
			List<List<ClientCard>> materials_lists = base.Util.GetXyzMaterials(base.Bot.MonsterZone, 7, 2, false, (ClientCard card) => !card.IsCode(33158448) && !card.IsCode(3611830));
			if (materials_lists.Count <= 0)
			{
				return false;
			}
			base.AI.SelectMaterials(materials_lists[0], 0);
			this.xyz_mode = true;
			return this.BeforeResult(ExecutorType.Summon);
		}

		// Token: 0x060023C9 RID: 9161 RVA: 0x000EA3DC File Offset: 0x000E85DC
		private bool Odd_EyesMeteorburstDragonCheck()
		{
			if (!this.XyzModeCheck(false))
			{
				return false;
			}
			if (base.Util.GetXyzMaterials(ZefraExecutor.Func.MergeList<ClientCard>(new List<ClientCard>[]
			{
				new List<ClientCard> { base.Card },
				ZefraExecutor.Func.GetZoneCards(base.Bot, (CardLocation)516, false, false)
			}), 7, 2, false, (ClientCard card) => !card.IsCode(33158448) && !card.IsCode(3611830)).Count <= 0)
			{
				return false;
			}
			List<ClientCard> pre_materials = this.func.CardsCheckWhere(ZefraExecutor.Func.GetZoneCards(base.Bot, CardLocation.MonsterZone, true, false), new ZefraExecutor.Toos.Delegate(this.SecretoftheYangZingCheck), Array.Empty<object>());
			List<List<ClientCard>> materials_sy_lists = base.Util.GetSynchroMaterials(pre_materials, 7, 1, 1, false, true, null, (ClientCard card) => !card.IsCode(27354732) && !card.IsCode(79606837));
			if (materials_sy_lists.Count <= 0)
			{
				return false;
			}
			this.Odd_EyesMeteorburstDragon_materials.Clear();
			foreach (List<ClientCard> materials in materials_sy_lists)
			{
				if (this.func.CardsCheckCount(materials, new ZefraExecutor.Toos.Delegate(this.func.IsCode), new object[] { 96227613 }) > 0)
				{
					this.Odd_EyesMeteorburstDragon_materials.AddRange(materials);
					return true;
				}
			}
			this.Odd_EyesMeteorburstDragon_materials.AddRange(materials_sy_lists[0]);
			return true;
		}

		// Token: 0x060023CA RID: 9162 RVA: 0x000EA568 File Offset: 0x000E8768
		private bool Odd_EyesMeteorburstDragonSummon()
		{
			if (!this.Odd_EyesMeteorburstDragonCheck())
			{
				return false;
			}
			base.AI.SelectMaterials(this.Odd_EyesMeteorburstDragon_materials, 0);
			return this.BeforeResult(ExecutorType.Summon);
		}

		// Token: 0x060023CB RID: 9163 RVA: 0x000EA58D File Offset: 0x000E878D
		private bool Odd_EyesMeteorburstDragonEffect()
		{
			base.AI.SelectCard(96227613);
			return this.BeforeResult(ExecutorType.Activate);
		}

		// Token: 0x060023CC RID: 9164 RVA: 0x000EA5A6 File Offset: 0x000E87A6
		private bool DDSavantKeplerSummon()
		{
			if (this.HasInDeck(46372010))
			{
				this.summoned = true;
				return this.BeforeResult(ExecutorType.Summon);
			}
			return false;
		}

		// Token: 0x060023CD RID: 9165 RVA: 0x000EA5C5 File Offset: 0x000E87C5
		private bool ServantofEndymionEffect_2()
		{
			return base.Card.Location == CardLocation.SpellZone && this.BeforeResult(ExecutorType.Activate);
		}

		// Token: 0x060023CE RID: 9166 RVA: 0x000EA5DE File Offset: 0x000E87DE
		private bool IsSpsummonPMonster(ClientCard card)
		{
			return this.IsZefraScaleAbove(card) || this.IsZefraScaleBelow(card) || card.Id == 96227613 || card.Id == 92559258;
		}

		// Token: 0x060023CF RID: 9167 RVA: 0x000EA610 File Offset: 0x000E8810
		private int GetSpellActivateCount()
		{
			int count = 0;
			if (!this.activate_DragonShrine && this.func.CardsCheckAny(base.Bot.Hand, new ZefraExecutor.Toos.Delegate(this.func.IsCode), new object[] { 41620959 }) && (this.HasInDeck(20773176) || this.HasInDeck(5560911) || this.HasInDeck(69610326)))
			{
				count++;
			}
			if (!this.activate_SpellPowerMastery && this.func.CardsCheckAny(base.Bot.Hand, new ZefraExecutor.Toos.Delegate(this.func.IsCode), new object[] { 38943357 }) && (this.HasInDeck(3611830) || this.HasInDeck(92559258)))
			{
				count++;
			}
			if (this.func.CardsCheckAny(base.Bot.Hand, new ZefraExecutor.Toos.Delegate(this.func.IsCode), new object[] { 46372010 }))
			{
				count++;
			}
			if (!this.activate_ZefraProvidence && this.func.CardsCheckAny(base.Bot.Hand, new ZefraExecutor.Toos.Delegate(this.func.IsCode), new object[] { 74580251 }))
			{
				if (this.func.CardsCheckCount(base.Bot.Hand, new ZefraExecutor.Toos.Delegate(this.func.IsCode), new object[] { 32354768 }) <= 0 && !this.activate_OracleofZefra && this.HasInDeck(32354768))
				{
					count += 2;
				}
				else
				{
					count++;
				}
			}
			if (!this.activate_OracleofZefra && this.func.CardsCheckAny(base.Bot.Hand, new ZefraExecutor.Toos.Delegate(this.func.IsCode), new object[] { 32354768 }))
			{
				count++;
			}
			if (this.func.CardsCheckAny(base.Bot.Hand, new ZefraExecutor.Toos.Delegate(this.func.IsCode), new object[] { 73628505 }) && this.HasInDeck(32354768))
			{
				count++;
			}
			if (this.func.CardsCheckAny(base.Bot.Hand, new ZefraExecutor.Toos.Delegate(this.func.IsCode), new object[] { 81439173 }))
			{
				count++;
			}
			if (this.func.CardsCheckCount(base.Bot.Hand, new ZefraExecutor.Toos.Delegate(this.func.HasType), new object[] { CardType.Pendulum }) > 1 && base.Bot.SpellZone[0] == null && base.Bot.SpellZone[4] == null)
			{
				count++;
			}
			if (!this.summoned && base.Bot.HasInHand(11609969) && this.HasInDeck(46372010))
			{
				count++;
			}
			return count;
		}

		// Token: 0x060023D0 RID: 9168 RVA: 0x000EA933 File Offset: 0x000E8B33
		private bool ServantofEndymionEffect_3()
		{
			return this.PendulumActivate() && this.PendulumDefaultActivate() && this.BeforeResult(ExecutorType.Activate);
		}

		// Token: 0x060023D1 RID: 9169 RVA: 0x000EA950 File Offset: 0x000E8B50
		private bool ZefraDivineStrikeEffect()
		{
			if (base.Duel.LastChainPlayer == 0)
			{
				return false;
			}
			base.AI.SelectCard(new int[] { 57777714, 96223501, 22617205, 20773176, 95401059, 58990362, 21495657 });
			return this.BeforeResult(ExecutorType.Activate);
		}

		// Token: 0x060023D2 RID: 9170 RVA: 0x000EA984 File Offset: 0x000E8B84
		private bool NinePillarsofYangZingEffect()
		{
			if (base.Duel.LastChainPlayer == 0)
			{
				return false;
			}
			List<ClientCard> cards = this.func.CardsIdToClientCards(new List<int> { 58990362 }, base.Bot.MonsterZone, true);
			cards.AddRange(this.func.CardsIdToClientCards(new List<int> { 58990362, 21495657 }, base.Bot.SpellZone, true));
			base.AI.SelectCard(cards);
			this.should_destory = true;
			return this.BeforeResult(ExecutorType.Activate);
		}

		// Token: 0x060023D3 RID: 9171 RVA: 0x000EAA1A File Offset: 0x000E8C1A
		private bool IsActivateBlackwing_ZephyrostheElite()
		{
			return (this.Blackwing_ZephyrostheElite_activate || this.HeavymetalfoesElectrumite_activate) && ZefraExecutor.Func.GetZoneCards(base.Bot, CardLocation.PendulumZone, true, false).Count <= 0;
		}

		// Token: 0x060023D4 RID: 9172 RVA: 0x000EAA4B File Offset: 0x000E8C4B
		private bool PendulumDefaultActivate()
		{
			return this.IsActivateBlackwing_ZephyrostheElite() || (this.checkPActivate() && this.IsActivateScale());
		}

		// Token: 0x060023D5 RID: 9173 RVA: 0x000EAA68 File Offset: 0x000E8C68
		private bool ServantofEndymionEffect()
		{
			if (this.PendulumActivate())
			{
				if (this.IsActivateBlackwing_ZephyrostheElite())
				{
					return this.BeforeResult(ExecutorType.Activate);
				}
				return (this.HasInDeck(3611830) || this.HasInDeck(27354732)) && this.GetSpellActivateCount() >= 2 && this.BeforeResult(ExecutorType.Activate);
			}
			else
			{
				if (base.Card.Location != CardLocation.SpellZone)
				{
					return base.Card.Location == CardLocation.MonsterZone && this.BeforeResult(ExecutorType.Activate);
				}
				if (this.func.HasInZone(base.Bot, (CardLocation)514, 29432356, true, false))
				{
					return this.func.CardsCheckAny(base.Bot.Hand, new ZefraExecutor.Toos.Delegate(this.IsSpsummonPMonster), Array.Empty<object>()) && this.BeforeResult(ExecutorType.Activate);
				}
				return this.BeforeResult(ExecutorType.Activate);
			}
		}

		// Token: 0x060023D6 RID: 9174 RVA: 0x000EAB39 File Offset: 0x000E8D39
		private bool IsZefraScaleAbove(ClientCard card)
		{
			return ZefraExecutor.Func.IsCode(card, new int[] { 22617205, 58990362, 20773176, 95401059 });
		}

		// Token: 0x060023D7 RID: 9175 RVA: 0x000EAB52 File Offset: 0x000E8D52
		private bool IsZefraScaleBelow(ClientCard card)
		{
			return ZefraExecutor.Func.IsCode(card, new int[] { 57777714, 21495657, 96223501 });
		}

		// Token: 0x060023D8 RID: 9176 RVA: 0x000EAB6B File Offset: 0x000E8D6B
		private bool TerraformingEffect()
		{
			return base.Bot.HasInHand(32354768) && this.BeforeResult(ExecutorType.Activate);
		}

		// Token: 0x060023D9 RID: 9177 RVA: 0x000EAB88 File Offset: 0x000E8D88
		private bool DDSavantKeplerEffect()
		{
			return !this.PendulumActivate() && this.BeforeResult(ExecutorType.Activate);
		}

		// Token: 0x060023DA RID: 9178 RVA: 0x000EAB9B File Offset: 0x000E8D9B
		private bool FoolishBurialEffect()
		{
			return this.BeforeResult(ExecutorType.Activate);
		}

		// Token: 0x060023DB RID: 9179 RVA: 0x000EABA4 File Offset: 0x000E8DA4
		private List<ClientCard> GetSynchroMaterials()
		{
			return this.func.CardsCheckWhere(ZefraExecutor.Func.GetZoneCards(base.Bot, CardLocation.MonsterZone, true, false), (ClientCard card) => !this.IsNoLinkCards(card) && !card.HasType((CardType)75497472), Array.Empty<object>());
		}

		// Token: 0x060023DC RID: 9180 RVA: 0x000EABD0 File Offset: 0x000E8DD0
		private bool DestrudotheLostDragon_FrissonEffect()
		{
			if (base.Bot.HasInExtra(50588353))
			{
				return this.BeforeResult(ExecutorType.Activate);
			}
			if (!base.Bot.HasInExtra(33158448) && !base.Bot.HasInExtra(80696379))
			{
				return false;
			}
			if (this.func.CardsCheckWhere(ZefraExecutor.Func.GetZoneCards(base.Bot, CardLocation.MonsterZone, true, false), (ClientCard card) => this.SecretoftheYangZingCheck(card) && !this.IsNoLinkCards(card) && !card.HasType(CardType.Tuner) && card.Level > 0, Array.Empty<object>()).Count <= 0)
			{
				return false;
			}
			List<ClientCard> cards = this.func.CardsCheckWhere(ZefraExecutor.Func.GetZoneCards(base.Bot, CardLocation.MonsterZone, true, false), (ClientCard card) => !this.IsNoLinkCards(card) && card.Level > 0 && !card.HasType(CardType.Tuner), Array.Empty<object>());
			if (cards.Count <= 0)
			{
				return false;
			}
			base.AI.SelectCard(cards);
			return this.BeforeResult(ExecutorType.Activate);
		}

		// Token: 0x060023DD RID: 9181 RVA: 0x000EAC9C File Offset: 0x000E8E9C
		private bool IsCanSynchroSummon(int level)
		{
			return this.func.CardsCheckAny(this.GetSynchroMaterials(), delegate(ClientCard card)
			{
				if (card.Level + level == 8)
				{
					if (this.func.CardsCheckAny(this.Bot.ExtraDeck, (ClientCard synchro_card) => ZefraExecutor.Func.IsCode(synchro_card, new int[] { 27548199, 74586817 }), Array.Empty<object>()))
					{
						return true;
					}
				}
				if (card.Level + level == 7 && this.SecretoftheYangZingCheck(card))
				{
					if (this.func.CardsCheckAny(this.Bot.ExtraDeck, (ClientCard synchro_card) => ZefraExecutor.Func.IsCode(synchro_card, new int[] { 80696379, 33158448 }), Array.Empty<object>()))
					{
						return true;
					}
				}
				if (card.Level + level != 5 || !this.Bot.HasInExtra(65536818))
				{
					return card.Level + level == 4 && this.Bot.HasInExtra(79606837);
				}
				return true;
			}, Array.Empty<object>());
		}

		// Token: 0x060023DE RID: 9182 RVA: 0x000EACE0 File Offset: 0x000E8EE0
		private bool DDLamiaEffect()
		{
			if (base.Bot.HasInExtra(44097050) && base.Bot.GetMonstersInMainZone().Count >= 3)
			{
				return false;
			}
			if (!base.Bot.HasInExtra(50588353) && !this.IsCanSynchroSummon(base.Card.Level))
			{
				return false;
			}
			base.AI.SelectCard(new int[] { 46372010, 11609969 });
			this.activate_DDLamia = true;
			return true;
		}

		// Token: 0x060023DF RID: 9183 RVA: 0x000EAB9B File Offset: 0x000E8D9B
		private bool DragonShrineEffect()
		{
			return this.BeforeResult(ExecutorType.Activate);
		}

		// Token: 0x060023E0 RID: 9184 RVA: 0x000EAD64 File Offset: 0x000E8F64
		private bool ZefraathEffect()
		{
			if (this.PendulumActivate())
			{
				return !this.activate_p_Zefraath || this.IsActivateBlackwing_ZephyrostheElite();
			}
			if (base.Card.Location == CardLocation.SpellZone)
			{
				this.activate_p_Zefraath = true;
				return this.BeforeResult(ExecutorType.Activate);
			}
			return false;
		}

		// Token: 0x060023E1 RID: 9185 RVA: 0x000EAD9D File Offset: 0x000E8F9D
		private bool RitualBeastTamerZeframpilicaEffect()
		{
			if (this.PendulumActivate())
			{
				return this.PendulumDefaultActivate() && this.BeforeResult(ExecutorType.Activate);
			}
			return this.BeforeResult(ExecutorType.Activate);
		}

		// Token: 0x060023E2 RID: 9186 RVA: 0x000EADC0 File Offset: 0x000E8FC0
		private bool BorreloadSavageDragonSummon_2()
		{
			return this.xyz_mode && this.BorreloadSavageDragonSummon();
		}

		// Token: 0x060023E3 RID: 9187 RVA: 0x000EADD4 File Offset: 0x000E8FD4
		private bool BorreloadSavageDragonSummon()
		{
			List<List<ClientCard>> materials_lists = base.Util.GetSynchroMaterials(base.Bot.MonsterZone, base.Card.Level, 1, 1, false, true, null, (ClientCard card) => !card.IsCode(33158448) && !card.IsCode(3611830) && !card.IsCode(79606837));
			if (materials_lists.Count <= 0)
			{
				return false;
			}
			foreach (List<ClientCard> materials in materials_lists)
			{
				if (this.func.CardsCheckAny(materials, new ZefraExecutor.Toos.Delegate(this.func.IsCode), new object[] { 44097051 }))
				{
					base.AI.SelectMaterials(materials, 0);
					return this.BeforeResult(ExecutorType.Summon);
				}
			}
			base.AI.SelectMaterials(materials_lists[0], 0);
			return this.BeforeResult(ExecutorType.Summon);
		}

		// Token: 0x060023E4 RID: 9188 RVA: 0x000EAED4 File Offset: 0x000E90D4
		private bool BorreloadSavageDragonEffect()
		{
			base.AI.SelectCard(new int[] { 74997493, 44097050, 24094258, 50588353, 36429703 });
			return this.BeforeResult(ExecutorType.Activate);
		}

		// Token: 0x060023E5 RID: 9189 RVA: 0x000EAEFC File Offset: 0x000E90FC
		private bool TheMightyMasterofMagicEffect()
		{
			if (this.PendulumActivate())
			{
				return this.PendulumDefaultActivate() && this.BeforeResult(ExecutorType.Activate);
			}
			if (base.Card.Location != CardLocation.MonsterZone)
			{
				return this.BeforeResult(ExecutorType.Activate);
			}
			if (base.Duel.LastChainPlayer == 0)
			{
				return false;
			}
			base.AI.SelectCard(new int[] { 92559258, 3611830 });
			return this.BeforeResult(ExecutorType.Activate);
		}

		// Token: 0x060023E6 RID: 9190 RVA: 0x000EAF70 File Offset: 0x000E9170
		private bool checkPActivate()
		{
			if (this.p_summoned)
			{
				return false;
			}
			if (this.func.HasInZone(base.Bot, CardLocation.PendulumZone, 29432356, true, false))
			{
				return true;
			}
			if (base.Bot.HasInHand(29432356) && (base.Bot.SpellZone[0] != null || base.Bot.SpellZone[4] != null))
			{
				return false;
			}
			if (base.Bot.SpellZone[0] == null && base.Bot.SpellZone[4] == null)
			{
				if (!base.Bot.HasInHand(29432356) && !this.func.CardsCheckAny(base.Bot.Hand, delegate(ClientCard card)
				{
					if (this.IsSpsummonPMonster(card) && ((base.Card.LScale >= 5) ? (card.LScale < 5) : (card.LScale > 5)))
					{
						List<ClientCard> pspSummonMonster3 = this.func.GetPSpSummonMonster(base.Bot, card, base.Card);
						return pspSummonMonster3 != null && pspSummonMonster3.Count > 0;
					}
					return false;
				}, Array.Empty<object>()))
				{
					return false;
				}
			}
			else
			{
				List<ClientCard> pspSummonMonster = this.func.GetPSpSummonMonster(base.Bot, base.Bot.SpellZone[0], base.Card);
				if (pspSummonMonster != null && pspSummonMonster.Count <= 0)
				{
					List<ClientCard> pspSummonMonster2 = this.func.GetPSpSummonMonster(base.Bot, base.Bot.SpellZone[4], base.Card);
					if (pspSummonMonster2 != null && pspSummonMonster2.Count <= 0)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060023E7 RID: 9191 RVA: 0x000EB0AB File Offset: 0x000E92AB
		private bool SecretoftheYangZingEffect()
		{
			if (this.PendulumActivate())
			{
				return this.PendulumDefaultActivate() && this.BeforeResult(ExecutorType.Activate);
			}
			this.activate_SecretoftheYangZing = true;
			return this.BeforeResult(ExecutorType.Activate);
		}

		// Token: 0x060023E8 RID: 9192 RVA: 0x000EA933 File Offset: 0x000E8B33
		private bool SatellarknightZefrathubanEffect()
		{
			return this.PendulumActivate() && this.PendulumDefaultActivate() && this.BeforeResult(ExecutorType.Activate);
		}

		// Token: 0x060023E9 RID: 9193 RVA: 0x000EB0D8 File Offset: 0x000E92D8
		private bool BorreloadSavageDragonEffect_2()
		{
			if (base.Duel.LastChainPlayer == 1)
			{
				ClientCard card = base.Util.GetLastChainCard();
				return card != null && !card.HasType((CardType)655360) && card.HasType((CardType)6) && this.BeforeResult(ExecutorType.Activate);
			}
			return false;
		}

		// Token: 0x060023EA RID: 9194 RVA: 0x000EAD9D File Offset: 0x000E8F9D
		private bool Zefraxi_TreasureoftheYangZingEffect()
		{
			if (this.PendulumActivate())
			{
				return this.PendulumDefaultActivate() && this.BeforeResult(ExecutorType.Activate);
			}
			return this.BeforeResult(ExecutorType.Activate);
		}

		// Token: 0x060023EB RID: 9195 RVA: 0x000EB123 File Offset: 0x000E9323
		private bool OracleofZefraEffect()
		{
			this.activate_OracleofZefra = true;
			return this.BeforeResult(ExecutorType.Activate);
		}

		// Token: 0x060023EC RID: 9196 RVA: 0x000EAD9D File Offset: 0x000E8F9D
		private bool FlameBeastoftheNekrozEffect()
		{
			if (this.PendulumActivate())
			{
				return this.PendulumDefaultActivate() && this.BeforeResult(ExecutorType.Activate);
			}
			return this.BeforeResult(ExecutorType.Activate);
		}

		// Token: 0x060023ED RID: 9197 RVA: 0x000EAB88 File Offset: 0x000E8D88
		private bool AstrographSorcererEffect()
		{
			return !this.PendulumActivate() && this.BeforeResult(ExecutorType.Activate);
		}

		// Token: 0x060023EE RID: 9198 RVA: 0x000EAD9D File Offset: 0x000E8F9D
		private bool StellarknightZefraxcitonEffect()
		{
			if (this.PendulumActivate())
			{
				return this.PendulumDefaultActivate() && this.BeforeResult(ExecutorType.Activate);
			}
			return this.BeforeResult(ExecutorType.Activate);
		}

		// Token: 0x060023EF RID: 9199 RVA: 0x000EB134 File Offset: 0x000E9334
		private bool IsNoLinkCards(ClientCard card)
		{
			return card != null && (((card.IsCode(27354732) || card.IsCode(3611830)) && !card.IsDisabled()) || card.IsCode(27548199) || card.IsCode(74586817) || card.IsCode(33158448) || card.IsCode(88581108) || card.IsCode(79606837) || card.LinkCount >= 3);
		}

		// Token: 0x060023F0 RID: 9200 RVA: 0x000EB1B8 File Offset: 0x000E93B8
		private bool LinkuribohSummon()
		{
			List<ClientCard> materials = this.func.CardsCheckWhere(ZefraExecutor.Func.GetZoneCards(base.Bot, CardLocation.MonsterZone, true, false), new ZefraExecutor.Toos.Delegate(this.func.HasLevel), new object[] { 1 });
			if (this.func.CardsCheckCount(materials, ZefraExecutor.Func.NegateFunc(new ZefraExecutor.Toos.Delegate(this.func.HasType)), new object[] { CardType.Tuner }) <= 0 && this.func.CardsCheckCount(materials, new ZefraExecutor.Toos.Delegate(this.func.HasType), new object[] { CardType.Tuner }) <= 1)
			{
				return false;
			}
			materials.Sort(delegate(ClientCard cardA, ClientCard cardB)
			{
				if (cardA.HasType(CardType.Tuner) && !cardB.HasType(CardType.Tuner))
				{
					return 1;
				}
				if (!cardA.HasType(CardType.Tuner) && cardB.HasType(CardType.Tuner))
				{
					return -1;
				}
				return 0;
			});
			base.AI.SelectMaterials(materials, 0);
			return true;
		}

		// Token: 0x060023F1 RID: 9201 RVA: 0x000EB29F File Offset: 0x000E949F
		private bool SpellSet()
		{
			if (base.Card.HasType(CardType.Trap))
			{
				base.AI.SelectPlace(31);
				return true;
			}
			return false;
		}

		// Token: 0x060023F2 RID: 9202 RVA: 0x000EB2BF File Offset: 0x000E94BF
		private bool SpellSet_2()
		{
			if (base.Card.HasType(CardType.QuickPlay))
			{
				base.AI.SelectPlace(31);
				return true;
			}
			return false;
		}

		// Token: 0x060023F3 RID: 9203 RVA: 0x000EAD9D File Offset: 0x000E8F9D
		private bool ShaddollZefracoreEffect()
		{
			if (this.PendulumActivate())
			{
				return this.PendulumDefaultActivate() && this.BeforeResult(ExecutorType.Activate);
			}
			return this.BeforeResult(ExecutorType.Activate);
		}

		// Token: 0x060023F4 RID: 9204 RVA: 0x000EB2E4 File Offset: 0x000E94E4
		private bool PSY_FramelordOmegaEffect()
		{
			if (base.Card.Location == CardLocation.MonsterZone)
			{
				if (base.Duel.Player == 0)
				{
					return this.BeforeResult(ExecutorType.Activate);
				}
				if (base.Bot.Banished.Count <= 0)
				{
					return false;
				}
				base.AI.SelectCard(this.func.CardsIdToClientCards(new List<int> { 9742784, 19580308 }, base.Bot.Banished, true));
				return this.BeforeResult(ExecutorType.Activate);
			}
			else
			{
				if (base.Bot.Graveyard.Count <= 0)
				{
					return false;
				}
				base.AI.SelectCard(this.func.CardsIdToClientCards(new List<int> { 29432356, 50588353, 65536818, 27548199, 19580308 }, base.Bot.Graveyard, true));
				return this.BeforeResult(ExecutorType.Activate);
			}
		}

		// Token: 0x060023F5 RID: 9205 RVA: 0x000EB3E8 File Offset: 0x000E95E8
		private bool Psummon()
		{
			if (base.Card.Location == CardLocation.SpellZone)
			{
				this.p_summoning = true;
				this.p_summoned = true;
				return true;
			}
			return false;
		}

		// Token: 0x060023F6 RID: 9206 RVA: 0x000EB409 File Offset: 0x000E9609
		private bool IsExtraZoneCard(ClientCard card)
		{
			return card != null && (base.Bot.MonsterZone[5] == card || base.Bot.MonsterZone[6] == card);
		}

		// Token: 0x060023F7 RID: 9207 RVA: 0x000EB438 File Offset: 0x000E9638
		private bool HeavymetalfoesElectrumiteSummon()
		{
			if (this.Odd_EyesMeteorburstDragonCheck())
			{
				return false;
			}
			List<ClientCard> materials = this.func.CardsCheckWhere(ZefraExecutor.Func.GetZoneCards(base.Bot, CardLocation.MonsterZone, true, false), new ZefraExecutor.Toos.Delegate(this.func.HasType), new object[] { CardType.Pendulum });
			if (materials.Count <= 0)
			{
				return false;
			}
			materials.Sort(delegate(ClientCard cardA, ClientCard cardB)
			{
				if ((cardA.Level == 3 || cardA.HasType(CardType.Tuner)) && cardB.Level != 3 && !cardB.HasType(CardType.Tuner))
				{
					return -1;
				}
				if (cardA.Level != 3 && !cardA.HasType(CardType.Tuner) && (cardB.Level == 3 || cardB.HasType(CardType.Tuner)))
				{
					return 1;
				}
				return CardContainer.CompareCardLevel(cardA, cardB);
			});
			materials.Reverse();
			List<ClientCard> result = new List<ClientCard>();
			foreach (ClientCard material in materials)
			{
				if (this.IsExtraZoneCard(material))
				{
					result.Insert(0, material);
				}
				else if (!this.IsNoLinkCards(material) && (!material.HasType(CardType.Tuner) || !base.Bot.HasInExtra(50588353) || this.func.CardsCheckCount(ZefraExecutor.Func.GetZoneCards(base.Bot, CardLocation.MonsterZone, true, false), new ZefraExecutor.Toos.Delegate(this.func.HasType), new object[] { CardType.Tuner }) > 0))
				{
					result.Add(material);
				}
			}
			if (result.Count < 2)
			{
				return false;
			}
			base.AI.SelectMaterials(result, 0);
			return this.BeforeResult(ExecutorType.Summon);
		}

		// Token: 0x060023F8 RID: 9208 RVA: 0x000EB5B0 File Offset: 0x000E97B0
		private bool SecretoftheYangZingCheck(ClientCard card)
		{
			if (card.IsCode(58990362) && base.Bot.HasInHandOrInSpellZone(57831349))
			{
				return this.func.CardsCheckCount(ZefraExecutor.Func.GetZoneCards(base.Bot, (CardLocation)518, true, false), (ClientCard p_card) => p_card.HasSetcode(196) && p_card.HasType(CardType.Pendulum), Array.Empty<object>()) <= 0;
			}
			return true;
		}

		// Token: 0x060023F9 RID: 9209 RVA: 0x000EB628 File Offset: 0x000E9828
		private bool F_A_DawnDragsterSummon()
		{
			List<ClientCard> pre_materials = this.func.CardsCheckWhere(ZefraExecutor.Func.GetZoneCards(base.Bot, CardLocation.MonsterZone, true, false), new ZefraExecutor.Toos.Delegate(this.SecretoftheYangZingCheck), Array.Empty<object>());
			List<List<ClientCard>> materials_lists = base.Util.GetSynchroMaterials(pre_materials, 7, 1, 1, false, true, null, (ClientCard card) => !card.IsCode(27354732) && !card.IsCode(79606837));
			if (materials_lists.Count <= 0)
			{
				return false;
			}
			foreach (List<ClientCard> materials in materials_lists)
			{
				if (this.func.CardsCheckCount(materials, (ClientCard card) => card.HasType(CardType.Tuner) && card.HasRace(CardRace.Machine), Array.Empty<object>()) <= 0)
				{
					base.AI.SelectMaterials(materials, 0);
					return this.BeforeResult(ExecutorType.Summon);
				}
			}
			base.AI.SelectMaterials(materials_lists[0], 0);
			return this.BeforeResult(ExecutorType.Summon);
		}

		// Token: 0x060023FA RID: 9210 RVA: 0x000EAB9B File Offset: 0x000E8D9B
		private bool CrystronHalqifibraxEffect()
		{
			return this.BeforeResult(ExecutorType.Activate);
		}

		// Token: 0x060023FB RID: 9211 RVA: 0x000EB740 File Offset: 0x000E9940
		private bool MechaPhantomBeastAuroradonSummon()
		{
			if (base.Bot.GetMonstersInMainZone().Count >= 4 || (!this.HasInDeck(72291078) && !this.IsCanSPSummonTunerLevel1() && !this.func.CardsCheckAny(ZefraExecutor.Func.GetZoneCards(base.Bot, (CardLocation)20, true, false), new ZefraExecutor.Toos.Delegate(this.func.IsCode), new object[] { 94693857 })))
			{
				return false;
			}
			if (this.XyzModeCheck(false))
			{
				List<ClientCard> pre_materials = new List<ClientCard>();
				List<ClientCard> key_materials = this.func.CardsCheckWhere(ZefraExecutor.Func.GetZoneCards(base.Bot, CardLocation.Grave, false, false), delegate(ClientCard card)
				{
					if (card.IsCode(19580308) && !this.activate_DDLamia)
					{
						return this.func.CardsCheckCount(ZefraExecutor.Func.GetZoneCards(base.Bot, (CardLocation)14, true, false), (ClientCard scard) => ZefraExecutor.Func.HasSetCode(scard, new int[] { 175, 174 }) && scard.Id != 19580308, Array.Empty<object>()) > 0;
					}
					return false;
				}, Array.Empty<object>());
				List<ClientCard> key_materials_2 = this.func.CardsCheckWhere(ZefraExecutor.Func.GetZoneCards(base.Bot, CardLocation.Grave, false, false), (ClientCard card) => card.IsCode(9742784) && !this.activate_JetSynchron, Array.Empty<object>());
				pre_materials.AddRange(key_materials);
				pre_materials.AddRange(key_materials_2);
				if (!this.summoned)
				{
					pre_materials.AddRange(this.func.CardsCheckWhere(base.Bot.Hand, (ClientCard card) => !card.IsCode(5560911) && card.Level < 5, Array.Empty<object>()));
				}
				pre_materials.AddRange(base.Bot.MonsterZone);
				List<List<ClientCard>> synchroMaterials = base.Util.GetSynchroMaterials(pre_materials, 7, 1, 1, false, true, null, (ClientCard card) => !card.IsCode(27354732));
				List<List<ClientCard>> xyz_materials_lists = base.Util.GetXyzMaterials(ZefraExecutor.Func.GetZoneCards(base.Bot, (CardLocation)516, false, false), 7, 1, false, (ClientCard card) => !card.IsCode(33158448) && !card.IsCode(3611830));
				List<List<ClientCard>> xyz_materials_lists_2 = base.Util.GetXyzMaterials(ZefraExecutor.Func.GetZoneCards(base.Bot, CardLocation.MonsterZone, false, false), 7, 2, false, (ClientCard card) => !card.IsCode(33158448) && !card.IsCode(3611830));
				if ((synchroMaterials.Count > 0 && xyz_materials_lists.Count > 0) || xyz_materials_lists_2.Count > 0)
				{
					return false;
				}
			}
			List<ClientCard> i = new List<ClientCard>();
			int link_count = 0;
			List<ClientCard> monsters = base.Bot.GetMonsters();
			monsters.Sort(new Comparison<ClientCard>(CardContainer.CompareCardLink));
			monsters.Reverse();
			foreach (ClientCard card2 in base.Bot.GetMonsters())
			{
				if (card2 != null && !card2.IsFacedown() && card2.HasRace(CardRace.Machine) && !this.IsNoLinkCards(card2))
				{
					i.Add(card2);
					link_count += (card2.HasType(CardType.Link) ? card2.LinkCount : 1);
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
			base.AI.SelectMaterials(i, 0);
			return true;
		}

		// Token: 0x060023FC RID: 9212 RVA: 0x000EBA28 File Offset: 0x000E9C28
		private bool SaryujaSkullDreadEffect()
		{
			if (base.ActivateDescription == base.Util.GetStringId(74997493, 2))
			{
				base.AI.SelectCard(this.GetSendToDeckIds());
				return this.BeforeResult(ExecutorType.Activate);
			}
			return this.BeforeResult(ExecutorType.Activate);
		}

		// Token: 0x060023FD RID: 9213 RVA: 0x000EBA64 File Offset: 0x000E9C64
		private bool SaryujaSkullDreadSummon()
		{
			if (base.Bot.GetMonstersInMainZone().Count < 4 || (!base.Bot.HasInExtra(50588353) && !this.xyz_mode))
			{
				return false;
			}
			List<ClientCard> materials = new List<ClientCard>();
			int link_count = 0;
			int materials_count = 0;
			this.func.CardsCheckCount(base.Bot.MonsterZone, new ZefraExecutor.Toos.Delegate(this.func.HasType), new object[] { CardType.Tuner });
			List<ClientCard> monsters = base.Bot.GetMonsters();
			monsters.Sort(delegate(ClientCard cardA, ClientCard cardB)
			{
				if ((cardA.HasType(CardType.Tuner) && cardB.HasType(CardType.Tuner)) || (!cardA.HasType(CardType.Tuner) && !cardB.HasType(CardType.Tuner)))
				{
					return CardContainer.CompareCardLevel(cardA, cardB);
				}
				if (cardA.HasType(CardType.Tuner) && !cardB.HasType(CardType.Tuner))
				{
					return 1;
				}
				return -1;
			});
			foreach (ClientCard material in monsters)
			{
				materials_count++;
				if (this.IsExtraZoneCard(material))
				{
					materials.Insert(0, material);
				}
				else
				{
					if (this.IsNoLinkCards(material))
					{
						materials_count--;
						continue;
					}
					materials.Add(material);
				}
				link_count += (material.HasType(CardType.Link) ? material.LinkCount : 1);
				if (link_count >= 4)
				{
					if (materials_count != 3 || base.Bot.Deck.Count <= 4)
					{
						break;
					}
					if (this.func.CardsCheckCount(base.Bot.Hand, new ZefraExecutor.Toos.Delegate(this.func.HasType), new object[] { CardType.Tuner }) <= 0)
					{
						if (base.Bot.HasInMonstersZone(19580308, false, false, true) && !this.activate_DDLamia)
						{
							if (this.func.CardsCheckCount(ZefraExecutor.Func.GetZoneCards(base.Bot, (CardLocation)14, true, false), (ClientCard card) => ZefraExecutor.Func.HasSetCode(card, new int[] { 175, 174 }) && card.Id != 19580308, Array.Empty<object>()) > 0)
							{
								goto IL_01EE;
							}
						}
						if ((!base.Bot.HasInMonstersZone(9742784, false, false, true) || this.activate_JetSynchron) && !this.xyz_mode)
						{
							break;
						}
					}
					IL_01EE:
					link_count--;
				}
			}
			if (materials.Count < 3)
			{
				return false;
			}
			base.AI.SelectMaterials(materials, 0);
			return this.BeforeResult(ExecutorType.Summon);
		}

		// Token: 0x060023FE RID: 9214 RVA: 0x000EBCBC File Offset: 0x000E9EBC
		private bool CrystronHalqifibraxSummon()
		{
			List<ClientCard> materials = new List<ClientCard>();
			if (base.Bot.HasInExtra(44097050))
			{
				materials.Add(base.Bot.MonsterZone[5]);
				materials.Add(base.Bot.MonsterZone[6]);
			}
			List<ClientCard> mainMonsters = base.Bot.GetMonstersInMainZone();
			mainMonsters.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			materials.AddRange(mainMonsters);
			base.AI.SelectMaterials(materials, 0);
			if (materials.Distinct<ClientCard>().Count<ClientCard>() <= 3)
			{
				base.AI.SendCustomChat(2, Array.Empty<object>());
			}
			return true;
		}

		// Token: 0x060023FF RID: 9215 RVA: 0x000EBD59 File Offset: 0x000E9F59
		private bool PendulumActivate()
		{
			return ZefraExecutor.Func.PendulumActivate(base.ActivateDescription, base.Card);
		}

		// Token: 0x06002400 RID: 9216 RVA: 0x000EBD6C File Offset: 0x000E9F6C
		private bool IsActivateScale()
		{
			return this.func.IsActivateScale(base.Bot, base.Card);
		}

		// Token: 0x06002401 RID: 9217 RVA: 0x000EBD85 File Offset: 0x000E9F85
		private bool SpellActivate()
		{
			return ZefraExecutor.Func.SpellActivate(base.Card);
		}

		// Token: 0x06002402 RID: 9218 RVA: 0x000EA933 File Offset: 0x000E8B33
		private bool SupremeKingGateZeroEffect()
		{
			return this.PendulumActivate() && this.PendulumDefaultActivate() && this.BeforeResult(ExecutorType.Activate);
		}

		// Token: 0x06002403 RID: 9219 RVA: 0x000EAD9D File Offset: 0x000E8F9D
		private bool MythicalBeastJackalKingEffect()
		{
			if (this.PendulumActivate())
			{
				return this.PendulumDefaultActivate() && this.BeforeResult(ExecutorType.Activate);
			}
			return this.BeforeResult(ExecutorType.Activate);
		}

		// Token: 0x06002404 RID: 9220 RVA: 0x000EBD92 File Offset: 0x000E9F92
		private bool Denglong_FirstoftheYangZingSummon_2()
		{
			return this.xyz_mode && this.Denglong_FirstoftheYangZingSummon();
		}

		// Token: 0x06002405 RID: 9221 RVA: 0x000EBDA4 File Offset: 0x000E9FA4
		private bool Denglong_FirstoftheYangZingSummon()
		{
			List<List<ClientCard>> materials_lists = base.Util.GetSynchroMaterials(base.Bot.MonsterZone, 5, 1, 1, false, true, null, (ClientCard card) => !card.IsCode(79606837));
			if (materials_lists.Count <= 0)
			{
				return false;
			}
			base.AI.SelectMaterials(materials_lists[0], 0);
			return this.BeforeResult(ExecutorType.Summon);
		}

		// Token: 0x06002406 RID: 9222 RVA: 0x000EBE14 File Offset: 0x000EA014
		private bool Denglong_FirstoftheYangZingEffect()
		{
			if (base.ActivateDescription == base.Util.GetStringId(65536818, 1))
			{
				return false;
			}
			if (base.Card.Location == CardLocation.MonsterZone)
			{
				base.AI.SelectCard(new int[] { 57831349, 58990362, 21495657 });
			}
			else
			{
				base.AI.SelectCard(new int[] { 58990362, 21495657, 61488417 });
			}
			return true;
		}

		// Token: 0x06002407 RID: 9223 RVA: 0x000EBE88 File Offset: 0x000EA088
		private bool DarkContractwiththGateEffect()
		{
			if (this.SpellActivate())
			{
				return (this.HasInDeck(19580308) || this.func.HasInZone(base.Bot, CardLocation.PendulumZone, 92559258, true, true)) && this.BeforeResult(ExecutorType.Activate);
			}
			return this.BeforeResult(ExecutorType.Activate);
		}

		// Token: 0x06002408 RID: 9224 RVA: 0x000EBEDC File Offset: 0x000EA0DC
		private int DisabledSort(ClientCard cardA, ClientCard cardB)
		{
			bool RitualBeastTamerZeframpilica_flag = !this.summoned && base.Bot.HasInExtra(79606837) && this.IsCanSPSummonTunerLevel1();
			if (((cardA.IsCode(57777714) && RitualBeastTamerZeframpilica_flag) || ZefraExecutor.Func.IsCode(cardA, new int[] { 23434538, 29432356, 27354732, 3611830 }) || cardA.HasType(CardType.Trap) || cardA.HasType(CardType.Tuner)) && !cardB.IsCode(57777714) && !cardB.HasType(CardType.Trap) && !ZefraExecutor.Func.IsCode(cardB, new int[] { 23434538, 29432356, 27354732, 3611830 }) && !cardB.HasType(CardType.Tuner))
			{
				return 1;
			}
			if (!cardA.IsCode(57777714) && !cardA.HasType(CardType.Trap) && !ZefraExecutor.Func.IsCode(cardA, new int[] { 23434538, 29432356, 27354732, 3611830 }) && !cardA.HasType(CardType.Tuner) && ((cardB.IsCode(57777714) && RitualBeastTamerZeframpilica_flag) || ZefraExecutor.Func.IsCode(cardB, new int[] { 23434538, 29432356, 27354732, 3611830 }) || cardB.HasType(CardType.Trap) || cardB.HasType(CardType.Tuner)))
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06002409 RID: 9225 RVA: 0x000EC008 File Offset: 0x000EA208
		private List<int> GetDisabledIds()
		{
			List<int> ids = new List<int>();
			ids.Add(5560911);
			ids.Add(14785765);
			ids.Add(52159691);
			ids.Add(31314549);
			ids.Add(49036338);
			if (!base.Bot.HasInGraveyard(96157835) || !base.Bot.HasInExtra(88581108))
			{
				ids.Add(23581825);
			}
			if (base.Bot.HasInBanished(49036338))
			{
				ids.Add(38814750);
			}
			ids.Add(61488417);
			ids.Add(19580308);
			ids.AddRange(ZefraExecutor.Func.GetCardsRepeatCardsId(base.Bot.Hand));
			List<ClientCard> zoneCards = ZefraExecutor.Func.GetZoneCards(base.Bot, CardLocation.Hand, false, false);
			zoneCards.Sort(new Comparison<ClientCard>(this.DisabledSort));
			List<int> hand_ids = ZefraExecutor.Func.ClientCardsToCardsId(zoneCards, true, false);
			ids.AddRange(hand_ids);
			return ids;
		}

		// Token: 0x0600240A RID: 9226 RVA: 0x000EC0FC File Offset: 0x000EA2FC
		private List<int> GetSendToDeckIds()
		{
			List<int> ids = new List<int>();
			List<int> repeat_ids = ZefraExecutor.Func.GetCardsRepeatCardsId(ZefraExecutor.Func.GetZoneCards(base.Bot, CardLocation.Hand, false, false));
			ids.Add(72291078);
			ids.AddRange(repeat_ids);
			ids.Add(31314549);
			ids.Add(52159691);
			ids.Add(14785765);
			ids.Add(49036338);
			ids.Add(61488417);
			ids.Add(23581825);
			if (this.activate_ZefraProvidence)
			{
				ids.Add(74580251);
			}
			if (this.activate_OracleofZefra)
			{
				ids.Add(32354768);
			}
			if (this.activate_DragonShrine)
			{
				ids.Add(41620959);
			}
			if (this.activate_SpellPowerMastery)
			{
				ids.Add(38943357);
			}
			List<ClientCard> zoneCards = ZefraExecutor.Func.GetZoneCards(base.Bot, CardLocation.Hand, false, false);
			zoneCards.Sort(new Comparison<ClientCard>(this.DisabledSort));
			List<int> hand_ids = ZefraExecutor.Func.ClientCardsToCardsId(zoneCards, true, false);
			ids.AddRange(hand_ids);
			return ids;
		}

		// Token: 0x0600240B RID: 9227 RVA: 0x000EC1F3 File Offset: 0x000EA3F3
		private bool TruKingofAllCalamitiesEffect()
		{
			if (base.Duel.Player == 1)
			{
				base.AI.SelectAttributes(new CardAttribute[] { CardAttribute.Divine });
				return this.BeforeResult(ExecutorType.Activate);
			}
			return false;
		}

		// Token: 0x0600240C RID: 9228 RVA: 0x000EC224 File Offset: 0x000EA424
		private bool JetSynchronEffect()
		{
			if (base.Card.Location == CardLocation.Grave)
			{
				if (!this.IsCanSynchroSummon(base.Card.Level))
				{
					return false;
				}
				if (this.func.CardsCheckAny(ZefraExecutor.Func.GetZoneCards(base.Bot, CardLocation.Extra, false, false), (ClientCard card) => card.HasType(CardType.Synchro) || ZefraExecutor.Func.IsCode(card, new int[] { 50588353, 41999284 }), Array.Empty<object>()))
				{
					this.activate_JetSynchron = true;
					List<ClientCard> dcards = this.func.CardsIdToClientCards(this.GetDisabledIds(), base.Bot.Hand, true);
					if (!base.Bot.HasInExtra(50588353) && dcards.Count <= 0)
					{
						return false;
					}
					base.AI.SelectCard(dcards);
					return this.BeforeResult(ExecutorType.Activate);
				}
			}
			return false;
		}

		// Token: 0x0600240D RID: 9229 RVA: 0x000EC2F0 File Offset: 0x000EA4F0
		private bool MechaPhantomBeastAuroradonEffect()
		{
			if (base.ActivateDescription == -1)
			{
				this.link_summoned = true;
				return true;
			}
			if (!this.HasInDeck(72291078) && ZefraExecutor.Func.GetZoneCards(base.Enemy, CardLocation.Onfield, false, false).Count <= 0)
			{
				return false;
			}
			List<ClientCard> tRelease = new List<ClientCard>();
			List<ClientCard> nRelease = new List<ClientCard>();
			foreach (ClientCard card in base.Bot.GetMonsters())
			{
				if (card != null && !this.IsNoLinkCards(card))
				{
					if (card.Id == 44097051)
					{
						tRelease.Add(card);
					}
					else
					{
						nRelease.Add(card);
					}
				}
			}
			int count = tRelease.Count<ClientCard>() + nRelease.Count<ClientCard>();
			this.opt_0 = false;
			this.opt_1 = false;
			this.opt_2 = false;
			if (count >= 3 && this.func.CardsCheckCount(base.Bot.Graveyard, new ZefraExecutor.Toos.Delegate(this.func.HasType), new object[] { CardType.Trap }) > 0)
			{
				this.opt_2 = true;
			}
			if (count >= 2 && this.CheckRemainInDeck(72291078) > 0)
			{
				this.opt_1 = true;
			}
			if (count >= 1 && ZefraExecutor.Func.GetZoneCards(base.Enemy, CardLocation.Onfield, false, false).Count > 0)
			{
				this.opt_0 = true;
			}
			return this.opt_0 || this.opt_1 || this.opt_2;
		}

		// Token: 0x0600240E RID: 9230 RVA: 0x000EC474 File Offset: 0x000EA674
		private bool SupremeKingDragonDarkwurmEffect()
		{
			if (base.Card.Location == CardLocation.Grave)
			{
				this.activate_SupremeKingDragonDarkwurm_1 = true;
				return this.BeforeResult(ExecutorType.Activate);
			}
			if (base.Card.Location == CardLocation.MonsterZone)
			{
				this.activate_SupremeKingDragonDarkwurm_2 = true;
				return this.BeforeResult(ExecutorType.Activate);
			}
			return false;
		}

		// Token: 0x0600240F RID: 9231 RVA: 0x000EC4B4 File Offset: 0x000EA6B4
		private bool SupremeKingDragonDarkwurmSummon()
		{
			if (this.activate_p_Zefraath || !base.Bot.HasInHand(29432356) || this.activate_SupremeKingDragonDarkwurm_1 || !this.HasInDeck(96227613) || !this.func.CardsCheckAny(base.Bot.Hand, new ZefraExecutor.Toos.Delegate(this.func.HasType), new object[] { CardType.Tuner }))
			{
				if (!this.func.CardsCheckAny(ZefraExecutor.Func.GetZoneCards(base.Bot, CardLocation.Hand, false, false), (ClientCard card) => card.LinkCount > 5, Array.Empty<object>()) || base.Bot.HasInHand(96227613) || this.activate_SupremeKingDragonDarkwurm_2)
				{
					return false;
				}
			}
			this.summoned = true;
			return this.BeforeResult(ExecutorType.Summon);
		}

		// Token: 0x06002410 RID: 9232 RVA: 0x000EC598 File Offset: 0x000EA798
		private bool DefaultSummon_2()
		{
			if (base.Card.Location == CardLocation.Hand && base.Card.Level <= 4 && base.Bot.HasInExtra(50588353) && this.func.CardsCheckAny(ZefraExecutor.Func.GetZoneCards(base.Bot, CardLocation.MonsterZone, true, false), (ClientCard card) => base.Card.HasType(CardType.Tuner) || card.HasType(CardType.Tuner), Array.Empty<object>()))
			{
				this.summoned = true;
				return this.BeforeResult(ExecutorType.Summon);
			}
			return false;
		}

		// Token: 0x06002411 RID: 9233 RVA: 0x000EC60F File Offset: 0x000EA80F
		private bool IsCanSPSummonTunerLevel1()
		{
			return this.func.CardsCheckAny(ZefraExecutor.Func.GetZoneCards(base.Bot, (CardLocation)20, true, false), delegate(ClientCard card)
			{
				if (card.IsCode(19580308) && !this.activate_DDLamia)
				{
					if (this.func.CardsCheckCount(ZefraExecutor.Func.GetZoneCards(base.Bot, (CardLocation)14, true, false), (ClientCard scard) => ZefraExecutor.Func.HasSetCode(scard, new int[] { 175, 174 }) && scard.Id != 19580308, Array.Empty<object>()) > 0)
					{
						return true;
					}
				}
				return card.IsCode(9742784) && !this.activate_JetSynchron && base.Bot.GetMonstersInMainZone().Count <= 3;
			}, Array.Empty<object>());
		}

		// Token: 0x06002412 RID: 9234 RVA: 0x000EC63C File Offset: 0x000EA83C
		private bool DefaultSummon()
		{
			if (base.Card.Level > 4)
			{
				return false;
			}
			if ((!this.link_summoned && base.Bot.HasInExtra(24094258) && this.func.CardsCheckCount(ZefraExecutor.Func.GetZoneCards(base.Bot, CardLocation.MonsterZone, true, false), new ZefraExecutor.Toos.Delegate(this.func.HasType), new object[] { CardType.Pendulum }) > 0 && base.Card.HasType(CardType.Pendulum)) || (this.IsCanSPSummonTunerLevel1() && ((base.Card.Level == 3 && base.Bot.HasInExtra(79606837)) || (base.Card.Level == 4 && base.Bot.HasInExtra(65536818)))) || (base.Card.Id == 69610326 && !this.activate_SupremeKingDragonDarkwurm_2) || (base.Bot.HasInExtra(50588353) && base.Bot.HasInHandOrInGraveyard(5560911) && !this.activate_DestrudotheLostDragon_Frisson))
			{
				this.summoned = true;
				return this.BeforeResult(ExecutorType.Summon);
			}
			return false;
		}

		// Token: 0x06002413 RID: 9235 RVA: 0x000EC766 File Offset: 0x000EA966
		private bool OneforOneEffect()
		{
			base.AI.SelectCard(this.GetDisabledIds());
			base.AI.SelectNextCard(new int[] { 9742784, 61488417, 19580308 });
			return this.BeforeResult(ExecutorType.Activate);
		}

		// Token: 0x06002414 RID: 9236 RVA: 0x000EC79C File Offset: 0x000EA99C
		private void HeavymetalfoesElectrumiteAddIds(List<int> ids)
		{
			if (!this.summoned && this.HasInDeck(46372010) && this.HasInDeck(19580308))
			{
				if (!this.func.CardsCheckAny(ZefraExecutor.Func.GetZoneCards(base.Bot, CardLocation.MonsterZone, true, false), new ZefraExecutor.Toos.Delegate(this.func.HasType), new object[] { CardType.Tuner }))
				{
					ids.Add(11609969);
				}
				else
				{
					ids.Add(76794549);
					ids.Add(11609969);
				}
			}
			ids.Add(76794549);
			ids.Add(20773176);
			ids.Add(11609969);
		}

		// Token: 0x06002415 RID: 9237 RVA: 0x000EC84E File Offset: 0x000EAA4E
		private bool LinkuribohEffect()
		{
			if (base.Card.Location == CardLocation.Grave)
			{
				return base.Duel.Player != 0 && this.BeforeResult(ExecutorType.Activate);
			}
			return this.BeforeResult(ExecutorType.Activate);
		}

		// Token: 0x06002416 RID: 9238 RVA: 0x000EC87D File Offset: 0x000EAA7D
		private bool Raider_WingEffect()
		{
			if (!base.Bot.HasInMonstersZone(73347079, false, true, true))
			{
				return false;
			}
			base.AI.SelectCard(73347079);
			return this.BeforeResult(ExecutorType.Activate);
		}

		// Token: 0x06002417 RID: 9239 RVA: 0x000EC8B0 File Offset: 0x000EAAB0
		private bool HeavymetalfoesElectrumiteEffect()
		{
			if (base.ActivateDescription == -1)
			{
				return this.BeforeResult(ExecutorType.Activate);
			}
			if (this.func.CardsCheckWhere(ZefraExecutor.Func.GetZoneCards(base.Bot, CardLocation.SpellZone, true, false), ZefraExecutor.Func.NegateFunc(new ZefraExecutor.Toos.Delegate(this.func.IsCode)), new object[] { 46372010 }).Count <= 0)
			{
				this.HeavymetalfoesElectrumite_activate = true;
				return false;
			}
			this.HeavymetalfoesElectrumite_activate = false;
			return this.BeforeResult(ExecutorType.Activate);
		}

		// Token: 0x06002418 RID: 9240 RVA: 0x0000763C File Offset: 0x0000583C
		public override bool OnSelectHand()
		{
			return true;
		}

		// Token: 0x06002419 RID: 9241 RVA: 0x000EC92F File Offset: 0x000EAB2F
		private bool ResetFlag()
		{
			this.should_destory = false;
			return true;
		}

		// Token: 0x0600241A RID: 9242 RVA: 0x000EC939 File Offset: 0x000EAB39
		private bool HeraldoftheArcLightEffect()
		{
			return base.Card.Location == CardLocation.MonsterZone && base.Duel.LastChainPlayer != 0 && this.BeforeResult(ExecutorType.Activate);
		}

		// Token: 0x0600241B RID: 9243 RVA: 0x000EC964 File Offset: 0x000EAB64
		private bool CalledbytheGraveEffect()
		{
			if ((base.Bot.SpellZone[5] == base.Card || base.Bot.SpellZone[0] == base.Card) && base.Duel.Player == 0)
			{
				return this.BeforeResult(ExecutorType.Activate);
			}
			ClientCard card = base.Util.GetLastChainCard();
			if (card == null)
			{
				return false;
			}
			int id = card.Id;
			List<ClientCard> g_cards = this.func.CardsCheckWhere(base.Enemy.Graveyard, new ZefraExecutor.Toos.Delegate(this.func.IsCode), new object[] { id });
			if (base.Duel.LastChainPlayer != 0)
			{
				if (card.Location == CardLocation.Grave && card.HasType(CardType.Monster))
				{
					base.AI.SelectCard(card);
					return this.BeforeResult(ExecutorType.Activate);
				}
				if (g_cards.Count<ClientCard>() > 0 && card.HasType(CardType.Monster))
				{
					base.AI.SelectCard(g_cards);
					return this.BeforeResult(ExecutorType.Activate);
				}
			}
			return false;
		}

		// Token: 0x0600241C RID: 9244 RVA: 0x000ECA5C File Offset: 0x000EAC5C
		public override CardPosition OnSelectPosition(int cardId, IList<CardPosition> positions)
		{
			NamedCard card = NamedCard.Get(cardId);
			if (cardId == 33158448 && base.Duel.Turn > 1)
			{
				return CardPosition.FaceUpAttack;
			}
			if (card.Attack <= 1000)
			{
				return CardPosition.FaceUpDefence;
			}
			return base.OnSelectPosition(cardId, positions);
		}

		// Token: 0x0600241D RID: 9245 RVA: 0x000ECAA0 File Offset: 0x000EACA0
		public override int OnSelectOption(IList<int> options)
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

		// Token: 0x0600241E RID: 9246 RVA: 0x000ECB08 File Offset: 0x000EAD08
		public override int OnSelectPlace(int cardId, int player, CardLocation location, int available)
		{
			NamedCard card = NamedCard.Get(cardId);
			if (player == 0)
			{
				if (location == CardLocation.SpellZone)
				{
					if (card.HasType(CardType.Pendulum))
					{
						if ((available & 16) > 0)
						{
							return 16;
						}
						if ((available & 1) > 0)
						{
							return 1;
						}
					}
					else
					{
						List<int> keys = new List<int> { 1, 2, 3 };
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
				}
				else if (location == CardLocation.MonsterZone && card.HasType(CardType.Link))
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
			}
			return base.OnSelectPlace(cardId, player, location, available);
		}

		// Token: 0x0600241F RID: 9247 RVA: 0x000ECBDC File Offset: 0x000EADDC
		private IList<ClientCard> _OnSelectPendulumSummon(IList<ClientCard> cards, int min, int max)
		{
			List<int> ids = this.func.GetSelectCardIdList();
			List<ClientCard> result = this.func.GetSelectCardList();
			List<ClientCard> exs = this.func.CardsCheckWhere(cards, new ZefraExecutor.Toos.Delegate(this.func.IsLocation), new object[] { CardLocation.Extra });
			this.func.CardsCheckWhere(cards, ZefraExecutor.Func.NegateFunc(new ZefraExecutor.Toos.Delegate(this.func.IsLocation)), new object[] { CardLocation.Extra });
			if (this.func.CardsCheckAny(ZefraExecutor.Func.GetZoneCards(base.Bot, CardLocation.PendulumZone, true, false), (ClientCard card) => card.HasSetcode(196) && !card.IsCode(29432356), Array.Empty<object>()) && this.func.CardsCheckAny(exs, new ZefraExecutor.Toos.Delegate(this.func.IsCode), new object[] { 95401059 }))
			{
				ids.Add(95401059);
			}
			result = this.func.CardsIdToClientCards(ids, cards, true);
			List<ClientCard> temp_cards = this.func.CardsCheckWhere(cards, ZefraExecutor.Func.NegateFunc(new ZefraExecutor.Toos.Delegate(this.func.IsCode)), new object[] { 23434538 });
			result.AddRange(temp_cards);
			if (result.Count <= 0)
			{
				return ZefraExecutor.Func.CheckSelectCount(base.Util, result, cards, min, min);
			}
			if (result[0] != null && result[0].Location != CardLocation.Extra)
			{
				this.p_count++;
			}
			return ZefraExecutor.Func.CheckSelectCount(base.Util, result, cards, max, max);
		}

		// Token: 0x06002420 RID: 9248 RVA: 0x000ECD80 File Offset: 0x000EAF80
		public override IList<ClientCard> OnSelectCard(IList<ClientCard> cards, int min, int max, int hint, bool cancelable)
		{
			if (base.AI.HaveSelectedCards())
			{
				return null;
			}
			List<int> ids = this.func.GetSelectCardIdList();
			List<ClientCard> result = this.func.GetSelectCardList();
			if (hint == 506)
			{
				if (this.func.CardsCheckAny(cards, (ClientCard card) => card.Location == CardLocation.Deck && card.HasSetcode(196), Array.Empty<object>()))
				{
					if (!this.activate_ZefraProvidence)
					{
						ids.Add(74580251);
					}
					if (this.p_summoned)
					{
						if (!this.summoned && base.Bot.HasInExtra(24094258) && this.func.CardsCheckCount(ZefraExecutor.Func.GetZoneCards(base.Bot, CardLocation.MonsterZone, true, false), new ZefraExecutor.Toos.Delegate(this.func.HasType), new object[] { CardType.Pendulum }) == 1)
						{
							List<int> pre_ids = new List<int> { 21495657, 22617205, 57777714, 57831349, 22617205, 95401059 };
							ids.AddRange(pre_ids);
						}
						ids.Add(35561352);
					}
					if (!this.activate_OracleofZefra)
					{
						ids.Add(32354768);
					}
					if (!this.activate_p_Zefraath && !this.func.HasInZone(base.Bot, (CardLocation)514, 29432356, true, false))
					{
						ids.Add(29432356);
					}
					if (this.func.HasInZone(base.Bot, (CardLocation)514, 96227613, true, false) && !this.func.CardsCheckAny(base.Bot.Hand, new ZefraExecutor.Toos.Delegate(this.func.HasType), new object[] { CardType.Tuner }) && !base.Bot.HasInHand(21495657))
					{
						ids.Add(21495657);
					}
					List<ClientCard> pMonsters = this.func.CardsCheckWhere(ZefraExecutor.Func.GetZoneCards(base.Bot, CardLocation.Hand, false, false), (ClientCard card) => card.HasType(CardType.Pendulum) && !card.IsCode(29432356), Array.Empty<object>());
					if (pMonsters.Count > 0)
					{
						List<ClientCard> zefraPMonsters = this.func.CardsCheckWhere(pMonsters, new ZefraExecutor.Toos.Delegate(this.func.HasSetCode), new object[] { 196 });
						if (zefraPMonsters.Count > 0)
						{
							zefraPMonsters.Sort(new Comparison<ClientCard>(ZefraExecutor.Func.CompareCardScale));
							int minScale = zefraPMonsters[0].RScale;
							if (base.Bot.HasInHand(29432356))
							{
								if (minScale < 5)
								{
									if (this.func.CardsCheckCount(cards, new ZefraExecutor.Toos.Delegate(this.IsZefraScaleAbove), Array.Empty<object>()) > 1)
									{
										ids.Add(95401059);
										if (!base.Bot.HasInHand(21495657))
										{
											ids.Add(21495657);
										}
										ids.Add(58990362);
										ids.Add(20773176);
										ids.Add(22617205);
										ids.Add(96223501);
										ids.Add(57777714);
									}
									else
									{
										ids.Add(21495657);
										ids.Add(57777714);
										ids.Add(96223501);
									}
								}
								else if (this.func.CardsCheckCount(cards, new ZefraExecutor.Toos.Delegate(this.IsZefraScaleBelow), Array.Empty<object>()) > 1)
								{
									ids.Add(95401059);
									if (!base.Bot.HasInHand(21495657))
									{
										ids.Add(21495657);
									}
									ids.Add(58990362);
									ids.Add(20773176);
									ids.Add(22617205);
									ids.Add(96223501);
									ids.Add(57777714);
								}
								else
								{
									ids.Add(22617205);
									ids.Add(58990362);
									ids.Add(20773176);
									ids.Add(95401059);
								}
							}
							else
							{
								if (base.Bot.HasInGraveyard(20773176))
								{
									ids.Add(57777714);
								}
								ids.Add(58990362);
								ids.Add(21495657);
							}
						}
						else
						{
							ids.Add(58990362);
							ids.Add(20773176);
							ids.Add(22617205);
							ids.Add(96223501);
							ids.Add(57777714);
							ids.Add(21495657);
						}
					}
					else
					{
						if (this.func.HasInZone(base.Bot, (CardLocation)514, 29432356, true, false) && !this.activate_p_Zefraath)
						{
							ids.Add(21495657);
							ids.Add(96223501);
							ids.Add(57777714);
						}
						ids.Add(58990362);
						ids.Add(20773176);
						ids.Add(22617205);
						ids.Add(96223501);
						ids.Add(57777714);
						ids.Add(21495657);
					}
					result = this.func.CardsIdToClientCards(ids, cards, true);
				}
				else if (this.func.CardsCheckALL(cards, new ZefraExecutor.Toos.Delegate(this.func.IsLocation), true, new object[] { CardLocation.Extra }))
				{
					this.HeavymetalfoesElectrumiteAddIds(ids);
					result = this.func.CardsIdToClientCards(ids, cards, true);
				}
				else if (this.func.CardsCheckALL(cards, new ZefraExecutor.Toos.Delegate(this.func.HasSetCode), true, new object[] { 298 }))
				{
					if (!this.func.HasInZone(base.Bot, (CardLocation)514, 92559258, true, false) || (this.func.HasInZone(base.Bot, (CardLocation)514, 92559258, true, false) && (!this.HasInDeck(3611830) || !this.HasInDeck(27354732))))
					{
						ids.Add(92559258);
					}
					ids.Add(3611830);
					ids.Add(27354732);
					result = this.func.CardsIdToClientCards(ids, cards, true);
				}
				else if (this.func.CardsCheckALL(cards, new ZefraExecutor.Toos.Delegate(this.func.HasSetCode), true, new object[] { 175 }))
				{
					ids.Add(19580308);
					ids.Add(11609969);
					result = this.func.CardsIdToClientCards(ids, cards, true);
				}
			}
			else if (hint == 507 && this.func.CardsCheckALL(cards, new ZefraExecutor.Toos.Delegate(this.func.IsLocation), true, new object[] { CardLocation.Hand }) && min == 3 && max == 3)
			{
				result = this.func.CardsIdToClientCards(this.GetSendToDeckIds(), cards, true);
			}
			else if (hint == 504 && this.func.CardsCheckALL(cards, new ZefraExecutor.Toos.Delegate(this.func.IsLocation), true, new object[] { CardLocation.Deck }))
			{
				List<int> extra_ids = this.CheckShouldSpsummonExtraMonster();
				if (extra_ids.Count <= 0)
				{
					if (!this.activate_SupremeKingDragonDarkwurm_2 && base.Bot.GetMonsterCount() <= 0)
					{
						ids.Add(69610326);
					}
					if (!this.activate_DestrudotheLostDragon_Frisson)
					{
						ids.Add(5560911);
					}
					if (!this.activate_JetSynchron)
					{
						ids.Add(9742784);
					}
					ids.Add(20773176);
				}
				else if (extra_ids.Count > 1)
				{
					if (base.Bot.GetMonsterCount() <= 0 && !this.activate_SupremeKingDragonDarkwurm_2)
					{
						ids.Add(69610326);
					}
					if (this.func.CardsCheckAny(base.Bot.Hand, (ClientCard card) => card.Level < 7 && card.HasType(CardType.Monster), Array.Empty<object>()))
					{
						ids.Add(5560911);
					}
					if (base.Bot.GetHandCount() > 0)
					{
						ids.Add(9742784);
					}
					if (!this.summoned && base.Bot.HasInHand(57777714))
					{
						ids.Add(20773176);
					}
					ids.Add(5560911);
					ids.Add(9742784);
					ids.Add(69610326);
					ids.Add(20773176);
				}
				else if (extra_ids.Contains(24094258))
				{
					if (base.Bot.GetMonsterCount() <= 0 && !this.activate_SupremeKingDragonDarkwurm_2)
					{
						ids.Add(69610326);
					}
					if (!this.summoned && base.Bot.HasInHand(57777714))
					{
						ids.Add(20773176);
					}
					ids.Add(5560911);
					ids.Add(9742784);
					ids.Add(69610326);
					ids.Add(20773176);
				}
				else if (extra_ids.Contains(50588353))
				{
					if (this.func.CardsCheckAny(ZefraExecutor.Func.GetZoneCards(base.Bot, (CardLocation)6, true, false), new ZefraExecutor.Toos.Delegate(this.func.HasType), new object[] { CardType.Tuner }))
					{
						if (base.Bot.GetMonsterCount() <= 0 && !this.activate_SupremeKingDragonDarkwurm_2)
						{
							ids.Add(69610326);
						}
						ids.Add(5560911);
						ids.Add(9742784);
						ids.Add(69610326);
						ids.Add(20773176);
					}
					else
					{
						ids.Add(5560911);
						ids.Add(9742784);
						ids.Add(69610326);
						ids.Add(20773176);
					}
				}
				result = this.func.CardsIdToClientCards(ids, cards, true);
			}
			else if (hint == base.Util.GetStringId(29432356, 1))
			{
				int[] pscales = ZefraExecutor.Func.GetPScales(base.Bot);
				int rScale = pscales[0];
				int lScale = pscales[1];
				if (((rScale != 5) ? rScale : lScale) < 5)
				{
					if (!this.activate_SecretoftheYangZing && !this.func.CardsCheckAny(ZefraExecutor.Func.GetZoneCards(base.Bot, (CardLocation)66, true, false), new ZefraExecutor.Toos.Delegate(this.func.IsCode), new object[] { 58990362 }))
					{
						ids.Add(58990362);
					}
					if (!this.activate_ShaddollZefracore)
					{
						if (this.func.CardsCheckAny(ZefraExecutor.Func.GetZoneCards(base.Bot, CardLocation.PendulumZone, true, false), (ClientCard card) => !card.IsCode(29432356) && card.HasSetcode(196), Array.Empty<object>()))
						{
							ids.Add(95401059);
						}
					}
					ids.Add(22617205);
					ids.Add(58990362);
					ids.Add(95401059);
				}
				else
				{
					ids.Add(21495657);
					ids.Add(96223501);
					ids.Add(57777714);
				}
				result = this.func.CardsIdToClientCards(ids, cards, true);
			}
			else if (hint == base.Util.GetStringId(24094258, 3))
			{
				this.HeavymetalfoesElectrumiteAddIds(ids);
				result = this.func.CardsIdToClientCards(ids, cards, true);
			}
			else
			{
				if (hint == 509)
				{
					if (this.func.CardsCheckALL(cards, (ClientCard card) => card.IsCode(3611830) || card.IsCode(27354732), true, Array.Empty<object>()))
					{
						ids.Add(27354732);
						ids.Add(3611830);
						result = this.func.CardsIdToClientCards(ids, cards, true);
						goto IL_11F7;
					}
				}
				if (this.p_summoning || ((base.Card == base.Bot.SpellZone[0] || base.Card == base.Bot.SpellZone[4]) && hint == 509 && base.Card.HasType(CardType.Pendulum)))
				{
					this.p_summoning = false;
					if (this.p_count >= 3 && !base.Bot.HasInExtra(74997493) && base.Bot.HasInExtra(44097050))
					{
						return ZefraExecutor.Func.CheckSelectCount(base.Util, result, cards, min, min);
					}
					return this._OnSelectPendulumSummon(cards, min, max);
				}
				else if (hint == 502)
				{
					if (this.func.CardsCheckALL(cards, (ClientCard card) => card.Controller == 0 && card.IsFaceup(), true, Array.Empty<object>()))
					{
						this.should_destory = true;
						if (this.func.CardsCheckALL(cards, new ZefraExecutor.Toos.Delegate(this.func.HasSetCode), true, new object[] { 158 }))
						{
							if (!this.activate_SecretoftheYangZing)
							{
								result = this.func.CardsIdToClientCards(new List<int> { 58990362 }, this.func.CardsCheckWhere(cards, new ZefraExecutor.Toos.Delegate(this.func.IsLocation), new object[] { CardLocation.MonsterZone }), true);
							}
							result.AddRange(this.func.CardsIdToClientCards(new List<int> { 58990362, 21495657 }, this.func.CardsCheckWhere(cards, ZefraExecutor.Func.NegateFunc(new ZefraExecutor.Toos.Delegate(this.func.IsLocation)), new object[] { CardLocation.MonsterZone }), true));
						}
						else
						{
							List<ClientCard> scards = this.func.CardsCheckWhere(cards, (ClientCard card) => card.Location == CardLocation.SpellZone, Array.Empty<object>());
							scards.Sort(delegate(ClientCard cardA, ClientCard cardB)
							{
								if (ZefraExecutor.Func.IsCode(cardA, new int[] { 32354768, 46372010 }) && !ZefraExecutor.Func.IsCode(cardB, new int[] { 32354768, 46372010 }))
								{
									return 1;
								}
								if (!ZefraExecutor.Func.IsCode(cardA, new int[] { 32354768, 46372010 }) && ZefraExecutor.Func.IsCode(cardB, new int[] { 32354768, 46372010 }))
								{
									return -1;
								}
								return 0;
							});
							result.AddRange(scards);
						}
					}
					else if (this.func.CardsCheckAny(cards, (ClientCard card) => card.Controller == 1 && (card.Location & CardLocation.Onfield) > (CardLocation)0, Array.Empty<object>()) && min == 1 && max == 1)
					{
						ClientCard card3 = base.Util.GetBestEnemyCard(false, false);
						if (card3 != null && cards.Contains(card3))
						{
							result.Add(card3);
						}
						else
						{
							result = new List<ClientCard>(this.func.CardsCheckWhere(cards, (ClientCard ecard) => ecard.Controller == 1, Array.Empty<object>()));
							if (result.Count <= 0)
							{
								return null;
							}
							result.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
							result.Reverse();
						}
					}
				}
				else if (hint == 509)
				{
					List<int> tuner_ids = new List<int> { 5560911, 49036338, 9742784, 38814750, 61488417 };
					List<int> no_tuner_ids = new List<int> { 3611830, 27354732, 58990362 };
					if (this.func.CardsCheckALL(cards, new ZefraExecutor.Toos.Delegate(this.func.IsLocation), true, new object[] { CardLocation.Hand }))
					{
						if (this.summoned && base.Bot.HasInExtra(50588353))
						{
							if (this.func.CardsCheckCount(base.Bot.MonsterZone, (ClientCard card) => card.IsFaceup() && card.HasType(CardType.Tuner), Array.Empty<object>()) <= 0)
							{
								if (base.Bot.HasInGraveyard(19580308) && !this.activate_DDLamia)
								{
									if (this.func.CardsCheckCount(ZefraExecutor.Func.GetZoneCards(base.Bot, (CardLocation)14, true, false), (ClientCard card) => ZefraExecutor.Func.HasSetCode(card, new int[] { 175, 174 }) && card.Id != 19580308, Array.Empty<object>()) <= 0)
									{
										goto IL_1070;
									}
								}
								if ((!base.Bot.HasInGraveyard(9742784) || this.activate_JetSynchron) && (!base.Bot.HasInGraveyard(5560911) || this.activate_DestrudotheLostDragon_Frisson))
								{
									ids.AddRange(tuner_ids);
									ids.AddRange(no_tuner_ids);
									goto IL_1080;
								}
							}
						}
						IL_1070:
						ids.AddRange(no_tuner_ids);
						ids.AddRange(tuner_ids);
						IL_1080:
						result = this.func.CardsIdToClientCards(ids, cards, true);
					}
					else if (this.func.CardsCheckALL(cards, (ClientCard card) => ZefraExecutor.Func.IsCode(card, new int[] { 61488417, 38814750, 72291078, 9742784, 94693857, 19580308 }), true, Array.Empty<object>()))
					{
						if (base.Bot.GetMonstersInMainZone().Count <= 1)
						{
							ids.Add(94693857);
						}
						ids.Add(9742784);
						ids.Add(94693857);
						ids.Add(61488417);
						ids.Add(38814750);
						result = this.func.CardsIdToClientCards(ids, cards, true);
					}
				}
				else if (hint == 500 && this.func.CardsCheckAny(cards, new ZefraExecutor.Toos.Delegate(this.func.IsLocation), new object[] { CardLocation.MonsterZone }))
				{
					List<ClientCard> tRelease = new List<ClientCard>();
					List<ClientCard> nRelease = new List<ClientCard>();
					foreach (ClientCard card2 in cards)
					{
						if (card2 != null && !this.IsNoLinkCards(card2))
						{
							if (card2.Id == 44097051)
							{
								tRelease.Add(card2);
							}
							else if (card2.Id == 36429703)
							{
								tRelease.Insert(0, card2);
							}
							else
							{
								nRelease.Add(card2);
							}
						}
					}
					result.AddRange(tRelease);
					result.AddRange(nRelease);
				}
			}
			IL_11F7:
			IList<ClientCard> selectResult = ZefraExecutor.Func.CheckSelectCount(base.Util, result, cards, min, max);
			if (selectResult == null)
			{
				return base.OnSelectCard(cards, min, max, hint, cancelable);
			}
			return selectResult;
		}

		// Token: 0x06002421 RID: 9249 RVA: 0x000EDFB8 File Offset: 0x000EC1B8
		private bool HasInDeck(int id)
		{
			return this.CheckRemainInDeck(id) > 0;
		}

		// Token: 0x06002422 RID: 9250 RVA: 0x000EDFC4 File Offset: 0x000EC1C4
		private int CheckRemainInDeck(int id)
		{
			if (id <= 38943357)
			{
				if (id <= 22617205)
				{
					if (id <= 11609969)
					{
						if (id <= 5560911)
						{
							if (id == 3611830)
							{
								return base.Bot.GetRemainingCount(3611830, 1);
							}
							if (id == 5560911)
							{
								return base.Bot.GetRemainingCount(5560911, 1);
							}
						}
						else
						{
							if (id == 9742784)
							{
								return base.Bot.GetRemainingCount(9742784, 1);
							}
							if (id == 11609969)
							{
								return base.Bot.GetRemainingCount(11609969, 1);
							}
						}
					}
					else if (id <= 19580308)
					{
						if (id == 14785765)
						{
							return base.Bot.GetRemainingCount(14785765, 1);
						}
						if (id == 19580308)
						{
							return base.Bot.GetRemainingCount(19580308, 1);
						}
					}
					else
					{
						if (id == 20773176)
						{
							return base.Bot.GetRemainingCount(20773176, 1);
						}
						if (id == 21495657)
						{
							return base.Bot.GetRemainingCount(21495657, 2);
						}
						if (id == 22617205)
						{
							return base.Bot.GetRemainingCount(22617205, 1);
						}
					}
				}
				else if (id <= 29432356)
				{
					if (id <= 23581825)
					{
						if (id == 23434538)
						{
							return base.Bot.GetRemainingCount(23434538, 3);
						}
						if (id == 23581825)
						{
							return base.Bot.GetRemainingCount(23581825, 1);
						}
					}
					else
					{
						if (id == 24224830)
						{
							return base.Bot.GetRemainingCount(24224830, 2);
						}
						if (id == 27354732)
						{
							return base.Bot.GetRemainingCount(27354732, 1);
						}
						if (id == 29432356)
						{
							return base.Bot.GetRemainingCount(29432356, 3);
						}
					}
				}
				else if (id <= 32354768)
				{
					if (id == 31314549)
					{
						return base.Bot.GetRemainingCount(31314549, 1);
					}
					if (id == 32354768)
					{
						return base.Bot.GetRemainingCount(32354768, 3);
					}
				}
				else
				{
					if (id == 35561352)
					{
						return base.Bot.GetRemainingCount(35561352, 1);
					}
					if (id == 38814750)
					{
						return base.Bot.GetRemainingCount(38814750, 3);
					}
					if (id == 38943357)
					{
						return base.Bot.GetRemainingCount(38943357, 3);
					}
				}
			}
			else if (id <= 72291078)
			{
				if (id <= 57777714)
				{
					if (id <= 46372010)
					{
						if (id == 41620959)
						{
							return base.Bot.GetRemainingCount(41620959, 3);
						}
						if (id == 46372010)
						{
							return base.Bot.GetRemainingCount(46372010, 1);
						}
					}
					else
					{
						if (id == 49036338)
						{
							return base.Bot.GetRemainingCount(49036338, 1);
						}
						if (id == 52159691)
						{
							return base.Bot.GetRemainingCount(52159691, 1);
						}
						if (id == 57777714)
						{
							return base.Bot.GetRemainingCount(57777714, 1);
						}
					}
				}
				else if (id <= 58990362)
				{
					if (id == 57831349)
					{
						return base.Bot.GetRemainingCount(57831349, 1);
					}
					if (id == 58990362)
					{
						return base.Bot.GetRemainingCount(58990362, 3);
					}
				}
				else
				{
					if (id == 61488417)
					{
						return base.Bot.GetRemainingCount(61488417, 1);
					}
					if (id == 69610326)
					{
						return base.Bot.GetRemainingCount(69610326, 1);
					}
					if (id == 72291078)
					{
						return base.Bot.GetRemainingCount(72291078, 1);
					}
				}
			}
			else if (id <= 92559258)
			{
				if (id <= 74580251)
				{
					if (id == 73628505)
					{
						return base.Bot.GetRemainingCount(73628505, 1);
					}
					if (id == 74580251)
					{
						return base.Bot.GetRemainingCount(74580251, 3);
					}
				}
				else
				{
					if (id == 76794549)
					{
						return base.Bot.GetRemainingCount(76794549, 1);
					}
					if (id == 81439173)
					{
						return base.Bot.GetRemainingCount(81439173, 1);
					}
					if (id == 92559258)
					{
						return base.Bot.GetRemainingCount(92559258, 3);
					}
				}
			}
			else if (id <= 95401059)
			{
				if (id == 94693857)
				{
					return base.Bot.GetRemainingCount(94693857, 1);
				}
				if (id == 95401059)
				{
					return base.Bot.GetRemainingCount(95401059, 1);
				}
			}
			else
			{
				if (id == 96073342)
				{
					return base.Bot.GetRemainingCount(96073342, 1);
				}
				if (id == 96223501)
				{
					return base.Bot.GetRemainingCount(96223501, 1);
				}
				if (id == 96227613)
				{
					return base.Bot.GetRemainingCount(96227613, 2);
				}
			}
			return 0;
		}

		// Token: 0x040025BA RID: 9658
		private bool opt_0;

		// Token: 0x040025BB RID: 9659
		private bool opt_1;

		// Token: 0x040025BC RID: 9660
		private bool opt_2;

		// Token: 0x040025BD RID: 9661
		private const bool IS_YGOPRO = true;

		// Token: 0x040025BE RID: 9662
		private const int P_ACTIVATE_DESC = 1160;

		// Token: 0x040025BF RID: 9663
		private int p_count;

		// Token: 0x040025C0 RID: 9664
		private int spell_activate_count;

		// Token: 0x040025C1 RID: 9665
		private bool summoned;

		// Token: 0x040025C2 RID: 9666
		private bool link_summoned;

		// Token: 0x040025C3 RID: 9667
		private bool p_summoned;

		// Token: 0x040025C4 RID: 9668
		private bool p_summoning;

		// Token: 0x040025C5 RID: 9669
		private bool activate_SupremeKingDragonDarkwurm_1;

		// Token: 0x040025C6 RID: 9670
		private bool activate_p_Zefraath;

		// Token: 0x040025C7 RID: 9671
		private bool activate_OracleofZefra;

		// Token: 0x040025C8 RID: 9672
		private bool activate_ZefraProvidence;

		// Token: 0x040025C9 RID: 9673
		private bool activate_SupremeKingDragonDarkwurm_2;

		// Token: 0x040025CA RID: 9674
		private bool activate_JetSynchron;

		// Token: 0x040025CB RID: 9675
		private bool activate_Blackwing_ZephyrostheElite;

		// Token: 0x040025CC RID: 9676
		private bool activate_DragonShrine;

		// Token: 0x040025CD RID: 9677
		private bool activate_SpellPowerMastery;

		// Token: 0x040025CE RID: 9678
		private bool activate_DestrudotheLostDragon_Frisson;

		// Token: 0x040025CF RID: 9679
		private bool activate_DarkContractwiththGate;

		// Token: 0x040025D0 RID: 9680
		private bool activate_SecretoftheYangZing;

		// Token: 0x040025D1 RID: 9681
		private bool activate_ShaddollZefracore;

		// Token: 0x040025D2 RID: 9682
		private bool activate_DDLamia;

		// Token: 0x040025D3 RID: 9683
		private bool xyz_mode;

		// Token: 0x040025D4 RID: 9684
		private bool Blackwing_ZephyrostheElite_activate;

		// Token: 0x040025D5 RID: 9685
		private bool HeavymetalfoesElectrumite_activate;

		// Token: 0x040025D6 RID: 9686
		private bool should_destory;

		// Token: 0x040025D7 RID: 9687
		private List<ClientCard> Odd_EyesMeteorburstDragon_materials = new List<ClientCard>();

		// Token: 0x040025D8 RID: 9688
		private bool duel_start = true;

		// Token: 0x040025D9 RID: 9689
		private int activate_count;

		// Token: 0x040025DA RID: 9690
		private int summon_count;

		// Token: 0x040025DB RID: 9691
		private bool enemy_activate;

		// Token: 0x040025DC RID: 9692
		private ZefraExecutor.Func func = new ZefraExecutor.Func();

		// Token: 0x02000448 RID: 1096
		public class CardId
		{
			// Token: 0x040025DD RID: 9693
			public const int PSY_FrameDriver = 49036338;

			// Token: 0x040025DE RID: 9694
			public const int Zefraath = 29432356;

			// Token: 0x040025DF RID: 9695
			public const int TheMightyMasterofMagic = 3611830;

			// Token: 0x040025E0 RID: 9696
			public const int AstrographSorcerer = 76794549;

			// Token: 0x040025E1 RID: 9697
			public const int DestrudotheLostDragon_Frisson = 5560911;

			// Token: 0x040025E2 RID: 9698
			public const int SupremeKingGateZero = 96227613;

			// Token: 0x040025E3 RID: 9699
			public const int MythicalBeastJackalKing = 27354732;

			// Token: 0x040025E4 RID: 9700
			public const int SecretoftheYangZing = 58990362;

			// Token: 0x040025E5 RID: 9701
			public const int FlameBeastoftheNekroz = 20773176;

			// Token: 0x040025E6 RID: 9702
			public const int StellarknightZefraxciton = 22617205;

			// Token: 0x040025E7 RID: 9703
			public const int SupremeKingDragonDarkwurm = 69610326;

			// Token: 0x040025E8 RID: 9704
			public const int Blackwing_ZephyrostheElite = 14785765;

			// Token: 0x040025E9 RID: 9705
			public const int ShaddollZefracore = 95401059;

			// Token: 0x040025EA RID: 9706
			public const int Raidraptor_SingingLanius = 31314549;

			// Token: 0x040025EB RID: 9707
			public const int SatellarknightZefrathuban = 96223501;

			// Token: 0x040025EC RID: 9708
			public const int Raider_Wing = 52159691;

			// Token: 0x040025ED RID: 9709
			public const int Zefraxi_TreasureoftheYangZing = 21495657;

			// Token: 0x040025EE RID: 9710
			public const int RitualBeastTamerZeframpilica = 57777714;

			// Token: 0x040025EF RID: 9711
			public const int ServantofEndymion = 92559258;

			// Token: 0x040025F0 RID: 9712
			public const int PSY_FramegearGamma = 38814750;

			// Token: 0x040025F1 RID: 9713
			public const int MechaPhantomBeastO_Lion = 72291078;

			// Token: 0x040025F2 RID: 9714
			public const int MaxxC = 23434538;

			// Token: 0x040025F3 RID: 9715
			public const int Deskbot001 = 94693857;

			// Token: 0x040025F4 RID: 9716
			public const int JetSynchron = 9742784;

			// Token: 0x040025F5 RID: 9717
			public const int DDLamia = 19580308;

			// Token: 0x040025F6 RID: 9718
			public const int DDSavantKepler = 11609969;

			// Token: 0x040025F7 RID: 9719
			public const int LightoftheYangZing = 61488417;

			// Token: 0x040025F8 RID: 9720
			public const int Rank_Up_MagicSoulShaveForce = 23581825;

			// Token: 0x040025F9 RID: 9721
			public const int SpellPowerMastery = 38943357;

			// Token: 0x040025FA RID: 9722
			public const int DragonShrine = 41620959;

			// Token: 0x040025FB RID: 9723
			public const int Terraforming = 73628505;

			// Token: 0x040025FC RID: 9724
			public const int ZefraProvidence = 74580251;

			// Token: 0x040025FD RID: 9725
			public const int FoolishBurial = 81439173;

			// Token: 0x040025FE RID: 9726
			public const int CalledbytheGrave = 24224830;

			// Token: 0x040025FF RID: 9727
			public const int DarkContractwiththGate = 46372010;

			// Token: 0x04002600 RID: 9728
			public const int OracleofZefra = 32354768;

			// Token: 0x04002601 RID: 9729
			public const int ZefraWar = 96073342;

			// Token: 0x04002602 RID: 9730
			public const int ZefraDivineStrike = 35561352;

			// Token: 0x04002603 RID: 9731
			public const int NinePillarsofYangZing = 57831349;

			// Token: 0x04002604 RID: 9732
			public const int OneforOne = 2295440;

			// Token: 0x04002605 RID: 9733
			public const int BorreloadSavageDragon = 27548199;

			// Token: 0x04002606 RID: 9734
			public const int Odd_EyesMeteorburstDragon = 80696379;

			// Token: 0x04002607 RID: 9735
			public const int F_A_DawnDragster = 33158448;

			// Token: 0x04002608 RID: 9736
			public const int Denglong_FirstoftheYangZing = 65536818;

			// Token: 0x04002609 RID: 9737
			public const int HeraldoftheArcLight = 79606837;

			// Token: 0x0400260A RID: 9738
			public const int TruKingofAllCalamities = 88581108;

			// Token: 0x0400260B RID: 9739
			public const int Raidraptor_ArsenalFalcon = 96157835;

			// Token: 0x0400260C RID: 9740
			public const int Raidraptor_ForceStrix = 73347079;

			// Token: 0x0400260D RID: 9741
			public const int SaryujaSkullDread = 74997493;

			// Token: 0x0400260E RID: 9742
			public const int MechaPhantomBeastAuroradon = 44097050;

			// Token: 0x0400260F RID: 9743
			public const int HeavymetalfoesElectrumite = 24094258;

			// Token: 0x04002610 RID: 9744
			public const int CrystronHalqifibrax = 50588353;

			// Token: 0x04002611 RID: 9745
			public const int Raidraptor_WiseStrix = 36429703;

			// Token: 0x04002612 RID: 9746
			public const int Linkuriboh = 41999284;

			// Token: 0x04002613 RID: 9747
			public const int PSY_FramelordOmega = 74586817;

			// Token: 0x04002614 RID: 9748
			public const int MechaPhantomBeastToken = 44097051;
		}

		// Token: 0x02000449 RID: 1097
		private enum CustomMessage
		{
			// Token: 0x04002616 RID: 9750
			Happy,
			// Token: 0x04002617 RID: 9751
			Angry,
			// Token: 0x04002618 RID: 9752
			Surprise
		}

		// Token: 0x0200044A RID: 1098
		private static class Toos
		{
			// Token: 0x0600242F RID: 9263 RVA: 0x0000763C File Offset: 0x0000583C
			private static bool DefaultFunc(ClientCard card)
			{
				return true;
			}

			// Token: 0x06002430 RID: 9264 RVA: 0x000EE830 File Offset: 0x000ECA30
			public static bool LinqAny(IList<ClientCard> cards, ZefraExecutor.Toos.Delegate @delegate = null)
			{
				if (cards == null)
				{
					return false;
				}
				@delegate = @delegate ?? new ZefraExecutor.Toos.Delegate(ZefraExecutor.Toos.DefaultFunc);
				return cards.Any((ClientCard card) => card != null && @delegate(card));
			}

			// Token: 0x06002431 RID: 9265 RVA: 0x000EE880 File Offset: 0x000ECA80
			public static bool LinqAll(IList<ClientCard> cards, ZefraExecutor.Toos.Delegate @delegate = null, bool flag = true)
			{
				if (cards == null)
				{
					return false;
				}
				IList<ClientCard> rcards = new List<ClientCard>(cards);
				if (flag)
				{
					rcards = cards.Where((ClientCard card) => card != null).ToList<ClientCard>();
				}
				@delegate = @delegate ?? new ZefraExecutor.Toos.Delegate(ZefraExecutor.Toos.DefaultFunc);
				return rcards.All((ClientCard card) => card != null && @delegate(card));
			}

			// Token: 0x06002432 RID: 9266 RVA: 0x000EE904 File Offset: 0x000ECB04
			public static int LinqCount(IList<ClientCard> cards, ZefraExecutor.Toos.Delegate @delegate = null)
			{
				if (cards == null)
				{
					return -1;
				}
				@delegate = @delegate ?? new ZefraExecutor.Toos.Delegate(ZefraExecutor.Toos.DefaultFunc);
				return cards.Count((ClientCard card) => card != null && @delegate(card));
			}

			// Token: 0x06002433 RID: 9267 RVA: 0x000EE954 File Offset: 0x000ECB54
			public static List<ClientCard> LinqWhere(IList<ClientCard> cards, ZefraExecutor.Toos.Delegate @delegate = null)
			{
				if (cards == null)
				{
					return new List<ClientCard>();
				}
				@delegate = @delegate ?? new ZefraExecutor.Toos.Delegate(ZefraExecutor.Toos.DefaultFunc);
				return cards.Where((ClientCard card) => card != null && @delegate(card)).ToList<ClientCard>();
			}

			// Token: 0x0200044B RID: 1099
			// (Invoke) Token: 0x06002435 RID: 9269
			public delegate bool Delegate(ClientCard card);
		}

		// Token: 0x02000451 RID: 1105
		private class Func
		{
			// Token: 0x06002443 RID: 9283 RVA: 0x000EEA02 File Offset: 0x000ECC02
			public List<ClientCard> GetSelectCardList()
			{
				if (this.selectCardList == null)
				{
					this.selectCardList = new List<ClientCard>();
				}
				else
				{
					this.selectCardList.Clear();
				}
				return this.selectCardList;
			}

			// Token: 0x06002444 RID: 9284 RVA: 0x000EEA2A File Offset: 0x000ECC2A
			public List<int> GetSelectCardIdList()
			{
				if (this.selectCardIdList == null)
				{
					this.selectCardIdList = new List<int>();
				}
				else
				{
					this.selectCardIdList.Clear();
				}
				return this.selectCardIdList;
			}

			// Token: 0x06002445 RID: 9285 RVA: 0x000EEA52 File Offset: 0x000ECC52
			public bool IsLocation(ClientCard card)
			{
				return card.Location == (CardLocation)this._parameters[0];
			}

			// Token: 0x06002446 RID: 9286 RVA: 0x000EEA6D File Offset: 0x000ECC6D
			public bool IsCode(ClientCard card)
			{
				return card.IsCode((int)this._parameters[0]);
			}

			// Token: 0x06002447 RID: 9287 RVA: 0x000EEA88 File Offset: 0x000ECC88
			public static bool IsCode(ClientCard card, params int[] ids)
			{
				if (card == null)
				{
					return false;
				}
				foreach (int id in ids)
				{
					if (card.IsCode(id))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06002448 RID: 9288 RVA: 0x000EEABA File Offset: 0x000ECCBA
			public bool HasSetCode(ClientCard card)
			{
				return card.HasSetcode((int)this._parameters[0]);
			}

			// Token: 0x06002449 RID: 9289 RVA: 0x000EEAD4 File Offset: 0x000ECCD4
			public static bool HasSetCode(ClientCard card, params int[] set_codes)
			{
				if (card == null)
				{
					return false;
				}
				foreach (int set_code in set_codes)
				{
					if (card.HasSetcode(set_code))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x0600244A RID: 9290 RVA: 0x000EEB06 File Offset: 0x000ECD06
			public static bool IsFaceUp(ClientCard card)
			{
				return card.IsFaceup();
			}

			// Token: 0x0600244B RID: 9291 RVA: 0x000EEB0E File Offset: 0x000ECD0E
			public bool HasAttribute(ClientCard card)
			{
				return card.HasAttribute((CardAttribute)this._parameters[0]);
			}

			// Token: 0x0600244C RID: 9292 RVA: 0x000EEB27 File Offset: 0x000ECD27
			public bool HasRace(ClientCard card)
			{
				return card.HasRace((CardRace)this._parameters[0]);
			}

			// Token: 0x0600244D RID: 9293 RVA: 0x000EEB40 File Offset: 0x000ECD40
			public bool HasLevel(ClientCard card)
			{
				return card.Level == (int)this._parameters[0];
			}

			// Token: 0x0600244E RID: 9294 RVA: 0x000EEB5B File Offset: 0x000ECD5B
			public bool HasType(ClientCard card)
			{
				return card.HasType((CardType)this._parameters[0]);
			}

			// Token: 0x0600244F RID: 9295 RVA: 0x000EEB74 File Offset: 0x000ECD74
			public static bool IsOnfield(ClientCard card)
			{
				return (card.Location & CardLocation.MonsterZone) > (CardLocation)0 || (card.Location & CardLocation.SpellZone) > (CardLocation)0;
			}

			// Token: 0x06002450 RID: 9296 RVA: 0x000EEB8E File Offset: 0x000ECD8E
			public static ZefraExecutor.Toos.Delegate NegateFunc(ZefraExecutor.Toos.Delegate @delegate)
			{
				return (ClientCard card) => !@delegate(card);
			}

			// Token: 0x06002451 RID: 9297 RVA: 0x000EEBA8 File Offset: 0x000ECDA8
			private void SetParameters(IList<object> parameters)
			{
				this.ClearParameters();
				int i = 0;
				for (;;)
				{
					int num = i;
					int? num2 = ((parameters != null) ? new int?(parameters.Count<object>()) : null);
					if (!((num < num2.GetValueOrDefault()) & (num2 != null)))
					{
						break;
					}
					this._parameters.Add(parameters[i]);
					i++;
				}
			}

			// Token: 0x06002452 RID: 9298 RVA: 0x000EEC03 File Offset: 0x000ECE03
			private void ClearParameters()
			{
				this._parameters.Clear();
			}

			// Token: 0x06002453 RID: 9299 RVA: 0x000EEC10 File Offset: 0x000ECE10
			public bool CardsCheckAny(IList<ClientCard> cards, ZefraExecutor.Toos.Delegate @delegate = null, params object[] parameters)
			{
				this.SetParameters(parameters);
				return ZefraExecutor.Toos.LinqAny(cards, @delegate);
			}

			// Token: 0x06002454 RID: 9300 RVA: 0x000EEC20 File Offset: 0x000ECE20
			public bool CardsCheckALL(IList<ClientCard> cards, ZefraExecutor.Toos.Delegate @delegate = null, bool all = true, params object[] parameters)
			{
				this.SetParameters(parameters);
				return ZefraExecutor.Toos.LinqAll(cards, @delegate, all);
			}

			// Token: 0x06002455 RID: 9301 RVA: 0x000EEC32 File Offset: 0x000ECE32
			public int CardsCheckCount(IList<ClientCard> cards, ZefraExecutor.Toos.Delegate @delegate = null, params object[] parameters)
			{
				this.SetParameters(parameters);
				return ZefraExecutor.Toos.LinqCount(cards, @delegate);
			}

			// Token: 0x06002456 RID: 9302 RVA: 0x000EEC42 File Offset: 0x000ECE42
			public List<ClientCard> CardsCheckWhere(IList<ClientCard> cards, ZefraExecutor.Toos.Delegate @delegate = null, params object[] parameters)
			{
				this.SetParameters(parameters);
				return ZefraExecutor.Toos.LinqWhere(cards, @delegate);
			}

			// Token: 0x06002457 RID: 9303 RVA: 0x000EEC54 File Offset: 0x000ECE54
			public static List<T> MergeList<T>(params List<T>[] lists)
			{
				List<T> result = new List<T>();
				foreach (List<T> list in lists)
				{
					if (list != null)
					{
						result.AddRange(list);
					}
				}
				return result;
			}

			// Token: 0x06002458 RID: 9304 RVA: 0x000EEC88 File Offset: 0x000ECE88
			public List<ClientCard> CardsIdToClientCards(IList<int> cardsId, IList<ClientCard> cardsList, bool uniqueId = true)
			{
				if ((cardsList != null && cardsList.Count<ClientCard>() <= 0) || (cardsId != null && cardsId.Count<int>() <= 0))
				{
					return new List<ClientCard>();
				}
				List<ClientCard> result = new List<ClientCard>();
				cardsId = cardsId.Distinct<int>().ToList<int>();
				foreach (int cardid in cardsId)
				{
					List<ClientCard> cards = this.CardsCheckWhere(cardsList, new ZefraExecutor.Toos.Delegate(this.IsCode), new object[] { cardid });
					if (cards.Count > 0)
					{
						if (uniqueId)
						{
							result.Add(cards.First<ClientCard>());
						}
						else
						{
							result.AddRange(cards);
						}
					}
				}
				return result;
			}

			// Token: 0x06002459 RID: 9305 RVA: 0x000EED40 File Offset: 0x000ECF40
			public static List<int> ClientCardsToCardsId(IList<ClientCard> cardsList, bool uniqueId = false, bool alias = false)
			{
				if (cardsList != null && cardsList.Count <= 0)
				{
					return new List<int>();
				}
				List<int> res = new List<int>();
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
				return res;
			}

			// Token: 0x0600245A RID: 9306 RVA: 0x000EEDEC File Offset: 0x000ECFEC
			public static IList<ClientCard> CheckSelectCount(AIUtil util, IList<ClientCard> _selected, IList<ClientCard> cards, int min, int max)
			{
				if (_selected == null || _selected.Count<ClientCard>() > 0)
				{
					return util.CheckSelectCount(_selected, cards, min, max);
				}
				return null;
			}

			// Token: 0x0600245B RID: 9307 RVA: 0x000EEE08 File Offset: 0x000ED008
			public static List<ClientCard> GetZoneCards(ClientField player, CardLocation loc, bool feceup = false, bool disable = false)
			{
				if (!feceup)
				{
					disable = false;
				}
				List<ClientCard> result = new List<ClientCard>();
				if ((loc & CardLocation.Hand) > (CardLocation)0)
				{
					result.AddRange(ZefraExecutor.Toos.LinqWhere(player.Hand, null));
				}
				if ((loc & CardLocation.MonsterZone) > (CardLocation)0)
				{
					result.AddRange(ZefraExecutor.Toos.LinqWhere(player.MonsterZone, (ClientCard card) => !(!card.IsFaceup() & feceup) && !(!card.IsDisabled() & disable)));
				}
				if ((loc & CardLocation.SpellZone) > (CardLocation)0)
				{
					result.AddRange(ZefraExecutor.Toos.LinqWhere(player.SpellZone, (ClientCard card) => !(!card.IsFaceup() & feceup) && !(!card.IsDisabled() & disable)));
				}
				if ((loc & CardLocation.PendulumZone) > (CardLocation)0)
				{
					result.AddRange(ZefraExecutor.Toos.LinqWhere(new List<ClientCard>
					{
						player.SpellZone[0],
						player.SpellZone[4]
					}, (ClientCard card) => !(!card.IsFaceup() & feceup) && !(!card.IsDisabled() & disable)));
				}
				if ((loc & CardLocation.Grave) > (CardLocation)0)
				{
					result.AddRange(ZefraExecutor.Toos.LinqWhere(player.Graveyard, null));
				}
				if ((loc & CardLocation.Removed) > (CardLocation)0)
				{
					result.AddRange(ZefraExecutor.Toos.LinqWhere(player.Banished, (ClientCard card) => !(!card.IsFaceup() & feceup)));
				}
				if ((loc & CardLocation.Extra) > (CardLocation)0)
				{
					result.AddRange(ZefraExecutor.Toos.LinqWhere(player.ExtraDeck, (ClientCard card) => !(!card.IsFaceup() & feceup)));
				}
				return result.Distinct<ClientCard>().ToList<ClientCard>();
			}

			// Token: 0x0600245C RID: 9308 RVA: 0x000EEF4C File Offset: 0x000ED14C
			public bool HasInZone(ClientField player, CardLocation loc, int id, bool feceup = false, bool disable = false)
			{
				return this.CardsCheckAny(ZefraExecutor.Func.GetZoneCards(player, loc, feceup, disable), new ZefraExecutor.Toos.Delegate(this.IsCode), new object[] { id });
			}

			// Token: 0x0600245D RID: 9309 RVA: 0x000EEF7A File Offset: 0x000ED17A
			public static bool SpellActivate(ClientCard card)
			{
				return card.Location == CardLocation.Hand || (card.Location == CardLocation.SpellZone && card.IsFacedown());
			}

			// Token: 0x0600245E RID: 9310 RVA: 0x000EEF98 File Offset: 0x000ED198
			public static bool PendulumActivate(int desc, ClientCard card)
			{
				return desc == 1160 && card.Location == CardLocation.Hand;
			}

			// Token: 0x0600245F RID: 9311 RVA: 0x000EEFB0 File Offset: 0x000ED1B0
			private static ZefraExecutor.Toos.Delegate GetPSpSummonLimilt(ClientCard pcard)
			{
				int setcode = -1;
				int setcode2 = -1;
				int id = pcard.Id;
				if (id <= 22617205)
				{
					if (id <= 20773176)
					{
						if (id == 11609969)
						{
							setcode = 175;
							goto IL_010D;
						}
						if (id != 20773176)
						{
							goto IL_010D;
						}
						setcode = 196;
						setcode2 = 180;
						goto IL_010D;
					}
					else if (id != 21495657)
					{
						if (id != 22617205)
						{
							goto IL_010D;
						}
						goto IL_00BA;
					}
				}
				else if (id <= 58990362)
				{
					if (id == 57777714)
					{
						setcode = 196;
						setcode2 = 4277;
						goto IL_010D;
					}
					if (id != 58990362)
					{
						goto IL_010D;
					}
				}
				else
				{
					if (id == 95401059)
					{
						setcode = 196;
						setcode2 = 157;
						goto IL_010D;
					}
					if (id != 96223501)
					{
						goto IL_010D;
					}
					goto IL_00BA;
				}
				setcode = 196;
				setcode2 = 158;
				goto IL_010D;
				IL_00BA:
				setcode = 196;
				setcode2 = 4252;
				IL_010D:
				return (ClientCard card) => setcode == -1 || card.HasSetcode(setcode) || setcode2 == -1 || card.HasSetcode(setcode2);
			}

			// Token: 0x06002460 RID: 9312 RVA: 0x000EF0D8 File Offset: 0x000ED2D8
			public static int[] GetPScales(ClientField bot)
			{
				int[] array = new int[2];
				ClientCard lcard = bot.SpellZone[0];
				ClientCard rcard = bot.SpellZone[4];
				array[0] = ((lcard == null || lcard.IsFacedown() || !lcard.HasType(CardType.Pendulum)) ? (-1) : lcard.RScale);
				array[1] = ((rcard == null || rcard.IsFacedown() || !rcard.HasType(CardType.Pendulum)) ? (-1) : rcard.LScale);
				return array;
			}

			// Token: 0x06002461 RID: 9313 RVA: 0x000EF148 File Offset: 0x000ED348
			public static int GetPScale(ClientField bot, int id)
			{
				bool rscale = false;
				ClientCard pcard;
				if (bot.SpellZone[0] != null && bot.SpellZone[0].Id == id)
				{
					pcard = bot.SpellZone[4];
				}
				else
				{
					pcard = bot.SpellZone[0];
					rscale = true;
				}
				if (pcard == null || pcard.IsFacedown() || !pcard.HasType(CardType.Pendulum))
				{
					return -1;
				}
				if (!rscale)
				{
					return pcard.LScale;
				}
				return pcard.RScale;
			}

			// Token: 0x06002462 RID: 9314 RVA: 0x000EF1B4 File Offset: 0x000ED3B4
			public List<ClientCard> GetPSpSummonMonster(ClientField bot, ClientCard lcard, ClientCard rcard)
			{
				if (lcard == null || rcard == null || !lcard.HasType(CardType.Pendulum) || !rcard.HasType(CardType.Pendulum) || (ZefraExecutor.Func.IsOnfield(lcard) & lcard.IsFacedown()) || (ZefraExecutor.Func.IsOnfield(lcard) & rcard.IsFacedown()))
				{
					return null;
				}
				int MaxScale = Math.Max(lcard.RScale, rcard.LScale);
				int MinScale = Math.Min(lcard.RScale, rcard.LScale);
				ZefraExecutor.Toos.Delegate llimit = ZefraExecutor.Func.GetPSpSummonLimilt(lcard);
				ZefraExecutor.Toos.Delegate rlimit = ZefraExecutor.Func.GetPSpSummonLimilt(rcard);
				return this.CardsCheckWhere(ZefraExecutor.Func.GetZoneCards(bot, (CardLocation)66, true, false), (ClientCard card) => card != lcard && card != rcard && card.HasType(CardType.Monster) && card.Level > MinScale && card.Level < MaxScale && !this.no_p_spsummon_ids.Contains(card.Id) && llimit(card) && rlimit(card), Array.Empty<object>());
			}

			// Token: 0x06002463 RID: 9315 RVA: 0x000EF2CC File Offset: 0x000ED4CC
			public bool IsActivateScale(ClientField bot, ClientCard card)
			{
				ClientCard lcard = bot.SpellZone[0];
				ClientCard rcard = bot.SpellZone[4];
				if (lcard != null && rcard != null)
				{
					return false;
				}
				if (lcard == null && rcard == null)
				{
					return true;
				}
				List<ClientCard> list = ((lcard == null) ? this.GetPSpSummonMonster(bot, card, rcard) : this.GetPSpSummonMonster(bot, lcard, card));
				return list != null && list.Count<ClientCard>() > 0;
			}

			// Token: 0x06002464 RID: 9316 RVA: 0x000EF320 File Offset: 0x000ED520
			public static int CompareCardScale(ClientCard cardA, ClientCard cardB)
			{
				if (cardA.RScale < cardB.RScale)
				{
					return -1;
				}
				if (cardA.RScale == cardB.RScale)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06002465 RID: 9317 RVA: 0x000EF344 File Offset: 0x000ED544
			public static List<int> GetCardsRepeatCardsId(IList<ClientCard> cards)
			{
				if (cards != null && cards.Count <= 0)
				{
					return new List<int> { -1 };
				}
				IList<int> cardsid = new List<int>();
				List<int> res = new List<int>();
				foreach (ClientCard card in cards)
				{
					if (card != null)
					{
						cardsid.Add(card.Id);
					}
				}
				for (int i = 0; i < cardsid.Count; i++)
				{
					if (res.Count < 0 || !res.Contains(cardsid[i]))
					{
						int times = 0;
						for (int j = 0; j < cardsid.Count; j++)
						{
							if (times > 1)
							{
								res.Add(cardsid[i]);
								break;
							}
							if (cardsid[i] == cardsid[j])
							{
								times++;
							}
						}
					}
				}
				if (res.Count <= 0)
				{
					return new List<int> { -1 };
				}
				return res;
			}

			// Token: 0x0400261F RID: 9759
			private IList<object> _parameters = new List<object>();

			// Token: 0x04002620 RID: 9760
			private List<int> no_p_spsummon_ids = new List<int> { 29432356 };

			// Token: 0x04002621 RID: 9761
			private List<ClientCard> selectCardList;

			// Token: 0x04002622 RID: 9762
			private List<int> selectCardIdList;
		}
	}
}
