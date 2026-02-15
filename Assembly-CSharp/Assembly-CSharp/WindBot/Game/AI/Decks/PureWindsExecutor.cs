using System;
using System.Collections.Generic;
using System.Linq;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x02000396 RID: 918
	[Deck("PureWinds", "AI_PureWinds", "Normal")]
	internal class PureWindsExecutor : DefaultExecutor
	{
		// Token: 0x06001B3D RID: 6973 RVA: 0x000A1754 File Offset: 0x0009F954
		public PureWindsExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.Activate, 84749824, new Func<bool>(base.DefaultSolemnWarning));
			base.AddExecutor(ExecutorType.Activate, 25789292, new Func<bool>(this.ForbiddenChaliceeff));
			base.AddExecutor(ExecutorType.Activate, 50954680, new Func<bool>(this.CrystalWingSynchroDragoneff));
			base.AddExecutor(ExecutorType.Activate, 40605147, new Func<bool>(base.DefaultSolemnStrike));
			base.AddExecutor(ExecutorType.Activate, 65277087, new Func<bool>(this.GustoGulldoeff));
			base.AddExecutor(ExecutorType.Activate, 91662792, new Func<bool>(this.GustoEguleff));
			base.AddExecutor(ExecutorType.Activate, 54455435, new Func<bool>(this.WindaPriestessOfGustoeff));
			base.AddExecutor(ExecutorType.Activate, 71175527, new Func<bool>(this.PilicaDescendantOfGustoeff));
			base.AddExecutor(ExecutorType.Activate, 70913714, new Func<bool>(this.OldEntityHastorreff));
			base.AddExecutor(ExecutorType.Activate, 30674956, new Func<bool>(this.WynnTheWindCharmerVerdanteff));
			base.AddExecutor(ExecutorType.Activate, 90512490, new Func<bool>(this.GreatFlyeff));
			base.AddExecutor(ExecutorType.Activate, 27980138, new Func<bool>(this.QuillPenOfGulldoseff));
			base.AddExecutor(ExecutorType.Activate, 8267140, new Func<bool>(this.CosmicCycloneeff));
			base.AddExecutor(ExecutorType.Activate, 83764719, new Func<bool>(this.Reborneff));
			base.AddExecutor(ExecutorType.Activate, 43722862, new Func<bool>(this.WindwitchIceBelleff));
			base.AddExecutor(ExecutorType.Activate, 71007216, new Func<bool>(this.WindwitchGlassBelleff));
			base.AddExecutor(ExecutorType.Activate, 70117860, new Func<bool>(this.WindwitchSnowBellsp));
			base.AddExecutor(ExecutorType.Activate, 64880894);
			base.AddExecutor(ExecutorType.Activate, 14577226, new Func<bool>(this.WindwitchWinterBelleff));
			base.AddExecutor(ExecutorType.Activate, 82044279, new Func<bool>(this.ClearWingSynchroDragoneff));
			base.AddExecutor(ExecutorType.Activate, 29552709, new Func<bool>(this.DaigustoSphreezeff));
			base.AddExecutor(ExecutorType.Activate, 81275020, new Func<bool>(this.SpeedroidTerrortopeff));
			base.AddExecutor(ExecutorType.Activate, 53932291, new Func<bool>(this.SpeedroidTaketomborgeff));
			base.AddExecutor(ExecutorType.Activate, 16725505, new Func<bool>(this.SpeedroidRedEyedDiceeff));
			base.AddExecutor(ExecutorType.Activate, 27315304, new Func<bool>(this.MistWurmeff));
			base.AddExecutor(ExecutorType.Activate, 84766279, new Func<bool>(this.DaigustoGulldoseff));
			base.AddExecutor(ExecutorType.SpSummon, 14577226, new Func<bool>(this.WindwitchWinterBellsp));
			base.AddExecutor(ExecutorType.SpSummon, 50954680, new Func<bool>(this.CrystalWingSynchroDragonsp));
			base.AddExecutor(ExecutorType.SpSummon, 82044279, new Func<bool>(this.ClearWingSynchroDragonsp));
			base.AddExecutor(ExecutorType.SpSummon, 29552709, new Func<bool>(this.DaigustoSphreezsp));
			base.AddExecutor(ExecutorType.SpSummon, 81275020);
			base.AddExecutor(ExecutorType.SpSummon, 53932291, new Func<bool>(this.SpeedroidTaketomborgsp));
			base.AddExecutor(ExecutorType.Summon, 71175527, new Func<bool>(this.PilicaDescendantOfGustosu));
			base.AddExecutor(ExecutorType.Summon, 65277087, new Func<bool>(this.GustoGulldosu));
			base.AddExecutor(ExecutorType.Summon, 91662792, new Func<bool>(this.GustoEgulsu));
			base.AddExecutor(ExecutorType.Summon, 54455435, new Func<bool>(this.WindaPriestessOfGustosu));
			base.AddExecutor(ExecutorType.Summon, 16725505, new Func<bool>(this.SpeedroidRedEyedDicesu));
			base.AddExecutor(ExecutorType.SpSummon, 27315304);
			base.AddExecutor(ExecutorType.SpSummon, 84766279);
			base.AddExecutor(ExecutorType.SpSummon, 42110604);
			base.AddExecutor(ExecutorType.SpSummon, 64880894);
			base.AddExecutor(ExecutorType.SpSummon, 70913714);
			base.AddExecutor(ExecutorType.SpSummon, 90512490, new Func<bool>(this.GreatFlysp));
			base.AddExecutor(ExecutorType.SpSummon, 30674956, new Func<bool>(this.WynnTheWindCharmerVerdantsp));
			base.AddExecutor(ExecutorType.Activate, 12580477);
			base.AddExecutor(ExecutorType.Activate, 53334471);
			base.AddExecutor(ExecutorType.Activate, 24590232, new Func<bool>(this.KingsConsonanceeff));
			base.AddExecutor(ExecutorType.SpellSet, 24590232);
			base.AddExecutor(ExecutorType.SpellSet, 40605147);
			base.AddExecutor(ExecutorType.SpellSet, 84749824);
			base.AddExecutor(ExecutorType.SpellSet, 25789292);
			base.AddExecutor(ExecutorType.SpellSet, 8608979);
			base.AddExecutor(ExecutorType.SpellSet, 53334471);
			base.AddExecutor(ExecutorType.MonsterSet, 65277087, new Func<bool>(this.gulldoset));
			base.AddExecutor(ExecutorType.MonsterSet, 91662792, new Func<bool>(this.egulset));
			base.AddExecutor(ExecutorType.MonsterSet, 54455435, new Func<bool>(this.windaset));
			base.AddExecutor(ExecutorType.Summon, 71007216, new Func<bool>(this.WindwitchGlassBellsummonfirst));
			base.AddExecutor(ExecutorType.Summon, 71007216, new Func<bool>(this.WindwitchGlassBellsummon));
			base.AddExecutor(ExecutorType.MonsterSet, 16725505, new Func<bool>(this.SpeedroidRedEyedDiceset));
			base.AddExecutor(ExecutorType.MonsterSet, 70117860, new Func<bool>(this.WindwitchSnowBellset));
			base.AddExecutor(ExecutorType.Activate, 67723438, new Func<bool>(this.EmergencyTeleporteff));
			base.AddExecutor(ExecutorType.Activate, 58577036, new Func<bool>(this.Reasoningeff));
			base.AddExecutor(ExecutorType.Activate, 8608979, new Func<bool>(this.SuperTeamBuddyForceUniteeff));
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(this.MonsterRepos));
		}

		// Token: 0x06001B3E RID: 6974 RVA: 0x000A21C2 File Offset: 0x000A03C2
		public override void OnNewTurn()
		{
			this.WindwitchGlassBelleff_used = false;
			this.Summon_used = false;
			this.Pilica_eff = false;
			this.plan_A = false;
			base.OnNewTurn();
		}

		// Token: 0x06001B3F RID: 6975 RVA: 0x000A21E6 File Offset: 0x000A03E6
		private bool windaset()
		{
			return !base.Enemy.HasInMonstersZoneOrInGraveyard(55410871);
		}

		// Token: 0x06001B40 RID: 6976 RVA: 0x000A21E6 File Offset: 0x000A03E6
		private bool egulset()
		{
			return !base.Enemy.HasInMonstersZoneOrInGraveyard(55410871);
		}

		// Token: 0x06001B41 RID: 6977 RVA: 0x000A21E6 File Offset: 0x000A03E6
		private bool gulldoset()
		{
			return !base.Enemy.HasInMonstersZoneOrInGraveyard(55410871);
		}

		// Token: 0x06001B42 RID: 6978 RVA: 0x000A2200 File Offset: 0x000A0400
		private bool Reasoningeff()
		{
			if ((base.Bot.HasInMonstersZone(50954680, false, false, false) || base.Bot.HasInMonstersZone(27315304, false, false, false)) && base.Util.GetBotAvailZonesFromExtraDeck() == 0)
			{
				return false;
			}
			if (base.Bot.HasInMonstersZone(this.level3, false, false, false) && base.Bot.HasInMonstersZone(71007216, false, false, false) && base.Bot.HasInHand(70117860))
			{
				return false;
			}
			base.AI.SelectPosition(CardPosition.FaceUpDefence);
			return true;
		}

		// Token: 0x06001B43 RID: 6979 RVA: 0x000A2292 File Offset: 0x000A0492
		private bool KingsConsonanceeff()
		{
			base.AI.SelectCard(new int[] { 50954680, 29552709, 82044279, 42110604, 70913714 });
			return true;
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x000A22B4 File Offset: 0x000A04B4
		private bool Reborneff()
		{
			if (base.Bot.HasInGraveyard(this.KeepSynchro2))
			{
				base.AI.SelectCard(this.KeepSynchro2);
				return true;
			}
			if (!base.Util.IsOneEnemyBetter(true))
			{
				return false;
			}
			if (!base.Bot.HasInGraveyard(this.reborn))
			{
				return false;
			}
			base.AI.SelectCard(this.reborn);
			return true;
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x000A231E File Offset: 0x000A051E
		private bool SpeedroidRedEyedDiceset()
		{
			return !base.Enemy.HasInMonstersZone(55410871, false, false, false) && base.Bot.GetMonstersInMainZone().Count + base.Bot.GetMonstersInExtraZone().Count == 0;
		}

		// Token: 0x06001B46 RID: 6982 RVA: 0x000A231E File Offset: 0x000A051E
		private bool WindwitchSnowBellset()
		{
			return !base.Enemy.HasInMonstersZone(55410871, false, false, false) && base.Bot.GetMonstersInMainZone().Count + base.Bot.GetMonstersInExtraZone().Count == 0;
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x000A2360 File Offset: 0x000A0560
		private bool GreatFlysp()
		{
			return !base.Bot.HasInMonstersZone(this.KeepSynchro, false, false, false) && !base.Bot.HasInMonstersZone(42110604, false, false, false) && !base.Bot.HasInMonstersZone(30674956, false, false, false);
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x000A23B4 File Offset: 0x000A05B4
		private bool WynnTheWindCharmerVerdantsp()
		{
			return !base.Bot.HasInMonstersZone(this.KeepSynchro, false, false, false) && !base.Bot.HasInMonstersZone(42110604, false, false, false) && !base.Bot.HasInMonstersZone(90512490, false, false, false);
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x000A2408 File Offset: 0x000A0608
		private bool MistWurmeff()
		{
			base.AI.SelectCard(base.Util.GetBestEnemyCard(false, true));
			if (base.Util.GetBestEnemyCard(false, true) != null)
			{
				Logger.DebugWriteLine("*************SelectCard= " + base.Util.GetBestEnemyCard(false, true).Id.ToString());
			}
			base.AI.SelectNextCard(base.Util.GetBestEnemyCard(false, true));
			if (base.Util.GetBestEnemyCard(false, true) != null)
			{
				Logger.DebugWriteLine("*************SelectCard= " + base.Util.GetBestEnemyCard(false, true).Id.ToString());
			}
			base.AI.SelectThirdCard(base.Util.GetBestEnemyCard(false, true));
			if (base.Util.GetBestEnemyCard(false, true) != null)
			{
				Logger.DebugWriteLine("*************SelectCard= " + base.Util.GetBestEnemyCard(false, true).Id.ToString());
			}
			return true;
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x000A2508 File Offset: 0x000A0708
		private bool GustoGulldosu()
		{
			if (base.Bot.HasInMonstersZone(this.Gulldosulist, false, false, false) && base.Util.GetBotAvailZonesFromExtraDeck() == 0)
			{
				return false;
			}
			if (base.Bot.HasInMonstersZone(29552709, false, false, false) || base.Bot.HasInHand(67723438))
			{
				this.Summon_used = true;
				return true;
			}
			if (base.Bot.HasInMonstersZone(this.Gulldosulist2, false, false, false) || base.Bot.HasInHand(53932291))
			{
				this.Summon_used = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x000A259C File Offset: 0x000A079C
		private bool GustoEgulsu()
		{
			if (base.Bot.HasInMonstersZone(29552709, false, false, false) && !base.Bot.HasInHand(65277087))
			{
				this.Summon_used = true;
				return true;
			}
			if ((base.Bot.HasInMonstersZone(50954680, false, false, false) || base.Bot.HasInMonstersZone(27315304, false, false, false)) && base.Util.GetBotAvailZonesFromExtraDeck() == 0)
			{
				return false;
			}
			if (base.Bot.HasInMonstersZone(this.EgulsuList, false, false, false) || base.Bot.HasInHand(53932291))
			{
				this.Summon_used = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x000A2644 File Offset: 0x000A0844
		private bool WindaPriestessOfGustosu()
		{
			if (base.Bot.HasInMonstersZone(29552709, false, false, false) && !base.Bot.HasInHand(65277087) && !base.Bot.HasInHand(91662792))
			{
				this.Summon_used = true;
				return true;
			}
			if (base.Bot.HasInMonstersZone(65277087, false, false, false) || base.Bot.HasInMonstersZone(71007216, false, false, false) || ((base.Bot.HasInMonstersZone(this.level3, false, false, false) || base.Bot.HasInMonstersZone(54455435, false, false, false)) && base.Bot.HasInMonstersZone(this.tuner, false, false, false)) || (base.Bot.HasInMonstersZone(70913714, false, false, false) && base.Bot.HasInMonstersZone(this.level1, false, false, false) && base.Util.GetBotAvailZonesFromExtraDeck() >= 1))
			{
				this.Summon_used = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x000A2744 File Offset: 0x000A0944
		private bool SpeedroidRedEyedDicesu()
		{
			if ((base.Bot.HasInMonstersZone(50954680, false, false, false) || base.Bot.HasInMonstersZone(27315304, false, false, false) || base.Bot.HasInMonstersZone(29552709, false, false, false)) && base.Util.GetBotAvailZonesFromExtraDeck() == 0)
			{
				return false;
			}
			if (base.Bot.HasInMonstersZone(this.EgulsuList, false, false, false))
			{
				this.Summon_used = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x000A27C0 File Offset: 0x000A09C0
		private bool PilicaDescendantOfGustosu()
		{
			if ((base.Bot.HasInMonstersZone(50954680, false, false, false) || base.Bot.HasInMonstersZone(27315304, false, false, false)) && base.Util.GetBotAvailZonesFromExtraDeck() == 0)
			{
				return false;
			}
			if (base.Bot.HasInMonstersZone(this.Pilica, false, false, false) && !base.Bot.HasInGraveyard(this.level1) && base.Util.GetBotAvailZonesFromExtraDeck() == 0)
			{
				return false;
			}
			if (!base.Bot.HasInMonstersZoneOrInGraveyard(this.tuner))
			{
				return false;
			}
			this.Summon_used = true;
			return true;
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x000A285C File Offset: 0x000A0A5C
		private bool EmergencyTeleporteff()
		{
			if ((base.Bot.HasInMonstersZone(50954680, false, false, false) || base.Bot.HasInMonstersZone(27315304, false, false, false)) && base.Util.GetBotAvailZonesFromExtraDeck() == 0)
			{
				return false;
			}
			if (base.Bot.HasInMonstersZone(this.level3, false, false, false) && base.Bot.HasInMonstersZone(71007216, false, false, false) && base.Bot.HasInHand(70117860))
			{
				return false;
			}
			if (base.Bot.HasInMonstersZone(this.tuner, false, false, false) && base.Bot.HasInMonstersZone(this.level3, false, false, false))
			{
				return false;
			}
			if (!base.Bot.HasInHandOrInMonstersZoneOrInGraveyard(this.tuner))
			{
				return false;
			}
			if (!base.Bot.HasInHandOrInMonstersZoneOrInGraveyard(this.level1) && base.Bot.HasInMonstersZone(this.ET, false, false, false))
			{
				return false;
			}
			if (this.Pilica_eff)
			{
				return false;
			}
			base.AI.SelectCard(71175527);
			base.AI.SelectPosition(CardPosition.FaceUpDefence);
			return true;
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x000A2978 File Offset: 0x000A0B78
		private bool SpeedroidRedEyedDiceeff()
		{
			if (base.Bot.HasInMonstersZone(81275020, false, false, false))
			{
				base.AI.SelectCard(81275020);
				base.AI.SelectNumber(6);
				return true;
			}
			if (base.Bot.HasInMonstersZone(53932291, false, false, false))
			{
				base.AI.SelectCard(53932291);
				base.AI.SelectNumber(6);
				return true;
			}
			return false;
		}

		// Token: 0x06001B51 RID: 6993 RVA: 0x000A29EC File Offset: 0x000A0BEC
		private bool DaigustoGulldoseff()
		{
			base.AI.SelectCard(Array.Empty<int>());
			base.AI.SelectNextCard(base.Util.GetBestEnemyMonster(false, false));
			return true;
		}

		// Token: 0x06001B52 RID: 6994 RVA: 0x000A2A18 File Offset: 0x000A0C18
		private bool SpeedroidTaketomborgeff()
		{
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			if (base.Bot.GetRemainingCount(16725505, 1) >= 1 && base.Bot.HasInMonstersZone(81275020, false, false, false))
			{
				base.AI.SelectCard(16725505);
				return true;
			}
			return false;
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x000A2A74 File Offset: 0x000A0C74
		private bool QuillPenOfGulldoseff()
		{
			int[] gyTargets = (from x in base.Bot.Graveyard
				where x.Attribute == 8
				select x.Id).ToArray<int>();
			if (gyTargets.Count<int>() >= 2)
			{
				base.AI.SelectCard(gyTargets);
				if (base.Bot.HasInSpellZone(70913714, false, false))
				{
					base.AI.SelectNextCard(70913714);
				}
				else
				{
					if (base.Util.GetProblematicEnemyCard(0, false) == null)
					{
						return false;
					}
					base.AI.SelectNextCard(base.Util.GetProblematicEnemyCard(0, false));
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x000A2B44 File Offset: 0x000A0D44
		private bool WindwitchIceBelleff()
		{
			if (base.Enemy.HasInMonstersZone(94977269, false, false, false))
			{
				return false;
			}
			if (this.WindwitchGlassBelleff_used && !base.Bot.HasInHand(70117860))
			{
				return false;
			}
			if (base.Bot.GetRemainingCount(71007216, 3) >= 1)
			{
				base.AI.SelectCard(71007216);
			}
			else if (base.Bot.HasInHand(71007216))
			{
				base.AI.SelectCard(70117860);
			}
			base.AI.GetSelectedPosition();
			if (base.Card.Location == CardLocation.Hand)
			{
				base.AI.SelectPosition(CardPosition.FaceUpDefence);
				base.AI.SelectPosition(CardPosition.FaceUpDefence);
			}
			return true;
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x000A2C00 File Offset: 0x000A0E00
		private bool SpeedroidTaketomborgsp()
		{
			if (base.Util.GetBotAvailZonesFromExtraDeck() == 0)
			{
				return false;
			}
			if (base.Bot.HasInMonstersZone(29552709, false, false, false))
			{
				return false;
			}
			if (base.Bot.HasInMonstersZone(this.taketomborgSpList, false, false, false))
			{
				base.AI.SelectPosition(CardPosition.FaceUpDefence);
				return true;
			}
			return false;
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x000A2C58 File Offset: 0x000A0E58
		private bool WindwitchGlassBelleff()
		{
			if ((base.Bot.HasInHandOrHasInMonstersZone(43722862) || base.Bot.HasInHandOrHasInMonstersZone(53932291) || base.Bot.HasInMonstersZone(71175527, false, false, false)) && !base.Bot.HasInHand(70117860))
			{
				base.AI.SelectCard(70117860);
				this.WindwitchGlassBelleff_used = true;
				return true;
			}
			base.AI.SelectCard(43722862);
			this.WindwitchGlassBelleff_used = true;
			return true;
		}

		// Token: 0x06001B57 RID: 6999 RVA: 0x00065FCE File Offset: 0x000641CE
		private bool OldEntityHastorreff()
		{
			base.AI.SelectCard(base.Util.GetBestEnemyMonster(false, false));
			return true;
		}

		// Token: 0x06001B58 RID: 7000 RVA: 0x000A2CE1 File Offset: 0x000A0EE1
		private bool WynnTheWindCharmerVerdanteff()
		{
			base.AI.SelectCard(new int[] { 71175527, 43722862, 81275020, 65277087, 91662792, 54455435 });
			return true;
		}

		// Token: 0x06001B59 RID: 7001 RVA: 0x000A2D00 File Offset: 0x000A0F00
		private bool SpeedroidTerrortopeff()
		{
			base.AI.SelectCard(new int[] { 53932291, 16725505 });
			return true;
		}

		// Token: 0x06001B5A RID: 7002 RVA: 0x000A2CE1 File Offset: 0x000A0EE1
		private bool GreatFlyeff()
		{
			base.AI.SelectCard(new int[] { 71175527, 43722862, 81275020, 65277087, 91662792, 54455435 });
			return true;
		}

		// Token: 0x06001B5B RID: 7003 RVA: 0x000A2D24 File Offset: 0x000A0F24
		private bool PilicaDescendantOfGustoeff()
		{
			base.AI.SelectCard(new int[] { 65277087, 71007216, 70117860, 91662792, 16725505 });
			this.Pilica_eff = true;
			return true;
		}

		// Token: 0x06001B5C RID: 7004 RVA: 0x000A2D4C File Offset: 0x000A0F4C
		private bool SuperTeamBuddyForceUniteeff()
		{
			using (IEnumerator<ClientCard> enumerator = base.Duel.CurrentChain.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsCode(8608979))
					{
						return false;
					}
				}
			}
			if (base.Bot.HasInGraveyard(71175527) && base.Bot.HasInMonstersZone(29552709, false, false, false))
			{
				base.AI.SelectCard(new int[] { 8608979, 29552709, 71175527 });
				base.AI.SelectPosition(CardPosition.Attack);
				return true;
			}
			if (base.Bot.HasInGraveyard(54455435) && base.Bot.HasInMonstersZone(29552709, false, false, false))
			{
				base.AI.SelectCard(new int[] { 8608979, 29552709, 54455435 });
				base.AI.SelectPosition(CardPosition.Attack);
				return true;
			}
			if (base.Bot.HasInGraveyard(29552709) && base.Bot.HasInMonstersZone(71175527, false, false, false))
			{
				base.AI.SelectCard(new int[] { 8608979, 71175527, 29552709 });
				base.AI.SelectPosition(CardPosition.Attack);
				return true;
			}
			if (base.Bot.HasInGraveyard(29552709) && base.Bot.HasInMonstersZone(54455435, false, false, false))
			{
				base.AI.SelectCard(new int[] { 8608979, 54455435, 29552709 });
				base.AI.SelectPosition(CardPosition.Attack);
				return true;
			}
			if (base.Bot.HasInGraveyard(84766279) && base.Bot.HasInMonstersZone(54455435, false, false, false))
			{
				base.AI.SelectCard(new int[] { 8608979, 54455435, 84766279 });
				base.AI.SelectPosition(CardPosition.Attack);
				return true;
			}
			if (base.Bot.HasInGraveyard(84766279) && base.Bot.HasInMonstersZone(71175527, false, false, false))
			{
				base.AI.SelectCard(new int[] { 8608979, 84766279, 71175527 });
				base.AI.SelectPosition(CardPosition.Attack);
				return true;
			}
			if (base.Bot.HasInGraveyard(29552709) && base.Bot.HasInMonstersZone(84766279, false, false, false))
			{
				base.AI.SelectCard(new int[] { 84766279, 29552709 });
				base.AI.SelectPosition(CardPosition.Attack);
				return true;
			}
			if (base.Bot.HasInGraveyard(50954680))
			{
				base.AI.SelectCard(50954680);
				base.AI.SelectPosition(CardPosition.Attack);
				return true;
			}
			if (base.Bot.HasInGraveyard(50954680))
			{
				base.AI.SelectCard(82044279);
				base.AI.SelectPosition(CardPosition.Attack);
				return true;
			}
			if (base.Bot.HasInGraveyard(this.SynchroList))
			{
				base.AI.SelectCard(this.SynchroList);
				base.AI.SelectPosition(CardPosition.Attack);
				return true;
			}
			if (base.Bot.HasInGraveyard(71175527) && base.Bot.HasInMonstersZone(54455435, false, false, false))
			{
				base.AI.SelectCard(new int[] { 54455435, 71175527 });
				base.AI.SelectPosition(CardPosition.FaceUpDefence);
				return true;
			}
			if (base.Util.GetBotAvailZonesFromExtraDeck() >= 1)
			{
				if ((base.Bot.HasInMonstersZone(81275020, false, false, false) || base.Bot.HasInMonstersZone(16725505, false, false, false) || base.Bot.HasInMonstersZone(42110604, false, false, false)) && !base.Bot.HasInHand(53932291))
				{
					base.AI.SelectCard(new int[] { 16725505, 81275020, 53932291 });
					return true;
				}
				if ((base.Bot.HasInMonstersZone(81275020, false, false, false) || base.Bot.HasInMonstersZone(16725505, false, false, false) || base.Bot.HasInMonstersZone(42110604, false, false, false)) && base.Bot.HasInHand(53932291))
				{
					return false;
				}
			}
			if (base.Bot.HasInGraveyard(8608979))
			{
				base.AI.SelectCard(8608979);
				return true;
			}
			return false;
		}

		// Token: 0x06001B5D RID: 7005 RVA: 0x000A31C8 File Offset: 0x000A13C8
		private bool WindwitchSnowBellsp()
		{
			if ((base.Bot.HasInMonstersZone(50954680, false, false, false) || base.Bot.HasInMonstersZone(29552709, false, false, false) || base.Bot.HasInMonstersZone(27315304, false, false, false)) && base.Util.GetBotAvailZonesFromExtraDeck() == 0)
			{
				return false;
			}
			if (base.Bot.HasInMonstersZone(this.level3, false, false, false) && base.Bot.HasInMonstersZone(71007216, false, false, false) && base.Bot.HasInMonstersZone(this.level1, false, false, false))
			{
				return false;
			}
			if ((base.Bot.HasInMonstersZone(82044279, false, false, false) || base.Bot.HasInMonstersZone(14577226, false, false, false)) && base.Bot.HasInMonstersZone(70117860, false, false, false) && base.Util.GetBotAvailZonesFromExtraDeck() == 0)
			{
				return false;
			}
			base.AI.SelectPosition(CardPosition.FaceUpDefence);
			return true;
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x000A32C4 File Offset: 0x000A14C4
		private bool DaigustoSphreezsp()
		{
			base.AI.SelectCard(new int[] { 70117860, 71175527, 54455435 });
			base.AI.SelectCard(new int[] { 16725505, 71175527, 54455435 });
			base.AI.SelectCard(new int[] { 65277087, 71175527 });
			base.AI.SelectCard(new int[] { 70117860, 84766279 });
			base.AI.SelectCard(new int[] { 16725505, 84766279 });
			base.AI.SelectPosition(CardPosition.Attack);
			return true;
		}

		// Token: 0x06001B5F RID: 7007 RVA: 0x000A337C File Offset: 0x000A157C
		private bool DaigustoSphreezeff()
		{
			if (this.Summon_used)
			{
				base.AI.SelectCard(new int[] { 71175527, 65277087, 91662792, 54455435 });
				return true;
			}
			base.AI.SelectCard(new int[] { 65277087, 71175527, 91662792, 54455435 });
			return true;
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x0005B1BE File Offset: 0x000593BE
		private bool WindwitchWinterBelleff()
		{
			base.AI.SelectCard(71007216);
			return true;
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x000A33CC File Offset: 0x000A15CC
		private bool WindwitchWinterBellsp()
		{
			if (base.Bot.HasInHandOrInSpellZone(8608979) || base.Bot.HasInHandOrInSpellZone(83764719))
			{
				return false;
			}
			if (base.Bot.HasInMonstersZone(43722862, false, false, false) && base.Bot.HasInMonstersZone(71007216, false, false, false) && base.Bot.HasInMonstersZone(70117860, false, false, false))
			{
				base.AI.GetSelectedPosition();
				base.AI.SelectPosition(CardPosition.FaceUpAttack);
				base.AI.SelectCard(new int[] { 43722862, 71007216 });
				return true;
			}
			return false;
		}

		// Token: 0x06001B62 RID: 7010 RVA: 0x000A347A File Offset: 0x000A167A
		private bool ClearWingSynchroDragonsp()
		{
			if (base.Bot.HasInMonstersZone(29552709, false, false, false))
			{
				return false;
			}
			base.AI.SelectPosition(CardPosition.Attack);
			return true;
		}

		// Token: 0x06001B63 RID: 7011 RVA: 0x000A34A0 File Offset: 0x000A16A0
		private bool ClearWingSynchroDragoneff()
		{
			return base.Duel.LastChainPlayer == 1;
		}

		// Token: 0x06001B64 RID: 7012 RVA: 0x000A34B4 File Offset: 0x000A16B4
		private bool CrystalWingSynchroDragonsp()
		{
			if (base.Bot.HasInMonstersZone(70117860, false, false, false) && base.Bot.HasInMonstersZone(14577226, false, false, false))
			{
				this.plan_A = true;
			}
			else if (base.Bot.HasInMonstersZone(70117860, false, false, false) && base.Bot.HasInMonstersZone(82044279, false, false, false))
			{
				this.plan_A = true;
			}
			return true;
		}

		// Token: 0x06001B65 RID: 7013 RVA: 0x000A3528 File Offset: 0x000A1728
		private bool ForbiddenChaliceeff()
		{
			if (base.Duel.LastChainPlayer == 1)
			{
				ClientCard target = base.Util.GetProblematicEnemyMonster(0, true);
				if (target != null && !target.IsShouldNotBeSpellTrapTarget() && base.Duel.CurrentChain.Contains(target))
				{
					base.AI.SelectCard(target);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001B66 RID: 7014 RVA: 0x000A3580 File Offset: 0x000A1780
		private bool CosmicCycloneeff()
		{
			using (IEnumerator<ClientCard> enumerator = base.Duel.CurrentChain.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsCode(8267140))
					{
						return false;
					}
				}
			}
			if ((base.Enemy.HasInSpellZone(82732705, false, false) || base.Enemy.HasInSpellZone(90846359, false, false) || base.Enemy.HasInSpellZone(24207889, false, false)) && base.Bot.LifePoints > 1000)
			{
				base.AI.SelectCard(new int[] { 82732705, 73599290, 90846359, 24207889 });
				return true;
			}
			if (base.Bot.HasInSpellZone(70913714, false, false) && base.Bot.LifePoints > 1000)
			{
				base.AI.SelectCard(70913714);
				return true;
			}
			return base.Bot.LifePoints > 1000 && base.DefaultMysticalSpaceTyphoon();
		}

		// Token: 0x06001B67 RID: 7015 RVA: 0x000A34A0 File Offset: 0x000A16A0
		private bool CrystalWingSynchroDragoneff()
		{
			return base.Duel.LastChainPlayer == 1;
		}

		// Token: 0x06001B68 RID: 7016 RVA: 0x000A36A0 File Offset: 0x000A18A0
		private bool GustoGulldoeff()
		{
			if (base.Bot.HasInMonstersZone(29552709, false, false, false))
			{
				base.AI.SelectCard(new int[] { 91662792, 54455435 });
				base.AI.SelectPosition(CardPosition.Attack);
				return true;
			}
			base.AI.SelectCard(new int[] { 91662792, 54455435 });
			base.AI.SelectPosition(CardPosition.FaceUpDefence);
			return true;
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x000A3720 File Offset: 0x000A1920
		private bool GustoEguleff()
		{
			if (base.Bot.HasInMonstersZone(29552709, false, false, false))
			{
				base.AI.SelectCard(new int[] { 54455435, 71175527 });
				base.AI.SelectPosition(CardPosition.Attack);
				return true;
			}
			base.AI.SelectCard(new int[] { 54455435, 71175527 });
			base.AI.SelectPosition(CardPosition.FaceUpDefence);
			return true;
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x000A37A0 File Offset: 0x000A19A0
		private bool WindaPriestessOfGustoeff()
		{
			if (base.Bot.HasInMonstersZone(29552709, false, false, false))
			{
				base.AI.SelectCard(new int[] { 65277087, 91662792 });
				base.AI.SelectPosition(CardPosition.Attack);
			}
			base.AI.SelectCard(new int[] { 65277087, 91662792 });
			base.AI.SelectPosition(CardPosition.FaceUpDefence);
			return true;
		}

		// Token: 0x06001B6B RID: 7019 RVA: 0x000A3820 File Offset: 0x000A1A20
		private bool WindwitchGlassBellsummonfirst()
		{
			if (base.Bot.HasInHand(71175527) && (base.Bot.HasInGraveyard(65277087) || base.Bot.HasInGraveyard(91662792) || base.Bot.HasInGraveyard(71007216) || base.Bot.HasInGraveyard(16725505)))
			{
				return false;
			}
			if (base.Bot.HasInMonstersZone(29552709, false, false, false))
			{
				return false;
			}
			if (!base.Bot.HasInHand(43722862))
			{
				this.Summon_used = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001B6C RID: 7020 RVA: 0x000A38BC File Offset: 0x000A1ABC
		private bool WindwitchGlassBellsummon()
		{
			if (!this.plan_A && (base.Bot.HasInGraveyard(71007216) || base.Bot.HasInMonstersZone(71007216, false, false, false)))
			{
				return false;
			}
			if (base.Bot.HasInMonstersZone(43722862, false, false, false) && !base.Bot.HasInMonstersZone(71007216, false, false, false))
			{
				this.Summon_used = true;
				return true;
			}
			bool windwitchGlassBelleff_used = this.WindwitchGlassBelleff_used;
			return false;
		}

		// Token: 0x06001B6D RID: 7021 RVA: 0x000A3938 File Offset: 0x000A1B38
		public bool MonsterRepos()
		{
			if (base.Card.IsCode(50954680) || base.Card.IsCode(29552709))
			{
				return !base.Card.HasPosition(CardPosition.Attack);
			}
			if (base.Card.IsCode(this.SynchroFull) && (base.Card.IsFacedown() || base.Card.IsDefense()))
			{
				return true;
			}
			if (base.Bot.HasInMonstersZone(29552709, false, false, false) && base.Card.IsCode(this.gusto))
			{
				return base.Card.IsFacedown() || base.Card.IsDefense();
			}
			if (base.Util.GetBotAvailZonesFromExtraDeck() >= 1 && base.Bot.GetMonsterCount() - base.Bot.GetMonstersInExtraZone().Count >= 2)
			{
				if (base.Bot.HasInMonstersZone(this.tuner, false, false, false) && (base.Bot.HasInMonstersZone(this.level3, false, false, false) || base.Bot.HasInMonstersZone(71007216, false, false, false)) && base.Card.IsFacedown())
				{
					return true;
				}
				if (base.Bot.HasInMonstersZone(54455435, false, false, false) && (base.Bot.HasInMonstersZone(65277087, false, false, false) || base.Bot.HasInMonstersZone(71007216, false, false, false)) && base.Card.IsFacedown())
				{
					return true;
				}
				if (base.Bot.GetMonsterCount() - base.Bot.GetMonstersInExtraZone().Count >= 3 && base.Bot.HasInMonstersZone(this.level1, false, false, false) && (base.Bot.HasInMonstersZone(54455435, false, false, false) || base.Bot.HasInMonstersZone(this.level3, false, false, false)) && base.Card.IsFacedown())
				{
					return true;
				}
				if (base.Bot.GetMonsterCount() - base.Bot.GetMonstersInExtraZone().Count >= 2 && (base.Bot.HasInMonstersZone(65277087, false, false, false) || base.Bot.HasInMonstersZone(71007216, false, false, false)) && base.Bot.HasInMonstersZone(54455435, false, false, false) && base.Card.IsFacedown())
				{
					return true;
				}
			}
			return !base.Card.IsFacedown() && base.DefaultMonsterRepos();
		}

		// Token: 0x06001B6E RID: 7022 RVA: 0x0000763C File Offset: 0x0000583C
		public override bool OnSelectHand()
		{
			return true;
		}

		// Token: 0x06001B6F RID: 7023 RVA: 0x000A3BB0 File Offset: 0x000A1DB0
		public override bool OnPreBattleBetween(ClientCard attacker, ClientCard defender)
		{
			if (attacker.IsCode(50954680))
			{
				if (defender.Level >= 5)
				{
					attacker.RealPower = attacker.Attack + defender.Attack;
				}
				return true;
			}
			if (attacker.IsCode(29552709))
			{
				attacker.RealPower = attacker.Attack + defender.Attack + defender.Defense;
				return true;
			}
			if (base.Bot.HasInMonstersZone(29552709, false, false, false) && attacker.IsCode(new int[] { 29552709, 65277087, 91662792, 54455435, 71175527, 84766279 }))
			{
				attacker.RealPower = attacker.Attack + defender.Attack + defender.Defense;
				return true;
			}
			return base.OnPreBattleBetween(attacker, defender);
		}

		// Token: 0x04001E84 RID: 7812
		private List<int> ReposTargets = new List<int> { 65277087, 54455435, 91662792, 71175527, 84766279 };

		// Token: 0x04001E85 RID: 7813
		private List<int> taketomborgSpList = new List<int> { 71007216, 65277087, 91662792, 16725505, 70117860, 81275020 };

		// Token: 0x04001E86 RID: 7814
		private List<int> level1 = new List<int> { 91662792, 16725505, 70117860 };

		// Token: 0x04001E87 RID: 7815
		private List<int> Pilica = new List<int> { 82044279, 14577226, 64880894 };

		// Token: 0x04001E88 RID: 7816
		private List<int> level3 = new List<int> { 71175527, 43722862, 53932291 };

		// Token: 0x04001E89 RID: 7817
		private List<int> KeepSynchro = new List<int> { 29552709, 50954680, 82044279, 14577226, 90512490, 30674956 };

		// Token: 0x04001E8A RID: 7818
		private List<int> KeepSynchro2 = new List<int> { 50954680, 29552709, 82044279, 14577226 };

		// Token: 0x04001E8B RID: 7819
		private List<int> reborn = new List<int> { 82044279, 29552709, 14577226, 71175527, 70913714, 42110604, 84766279 };

		// Token: 0x04001E8C RID: 7820
		private List<int> Gulldosulist = new List<int> { 50954680, 27315304, 82044279, 14577226, 82044279, 64880894 };

		// Token: 0x04001E8D RID: 7821
		private List<int> Gulldosulist2 = new List<int> { 81275020, 71175527, 54455435, 43722862, 53932291, 70913714, 42110604, 84766279, 29552709 };

		// Token: 0x04001E8E RID: 7822
		private List<int> EgulsuList = new List<int>
		{
			81275020, 71175527, 54455435, 43722862, 53932291, 70913714, 42110604, 84766279, 29552709, 64880894,
			14577226, 82044279
		};

		// Token: 0x04001E8F RID: 7823
		private List<int> SynchroList = new List<int>
		{
			81275020, 71175527, 43722862, 53932291, 70913714, 42110604, 84766279, 29552709, 64880894, 14577226,
			82044279, 50954680, 27315304
		};

		// Token: 0x04001E90 RID: 7824
		private List<int> SynchroFull = new List<int> { 70913714, 42110604, 84766279, 29552709, 64880894, 14577226, 82044279, 50954680, 27315304 };

		// Token: 0x04001E91 RID: 7825
		private List<int> LinkList = new List<int> { 30674956, 90512490 };

		// Token: 0x04001E92 RID: 7826
		private List<int> tuner = new List<int> { 65277087, 91662792, 16725505, 71007216, 70117860 };

		// Token: 0x04001E93 RID: 7827
		private List<int> gusto = new List<int> { 65277087, 91662792, 54455435, 71175527, 84766279, 29552709 };

		// Token: 0x04001E94 RID: 7828
		private List<int> ET = new List<int> { 82044279, 14577226 };

		// Token: 0x04001E95 RID: 7829
		private bool WindwitchGlassBelleff_used;

		// Token: 0x04001E96 RID: 7830
		private bool Summon_used;

		// Token: 0x04001E97 RID: 7831
		private bool Pilica_eff;

		// Token: 0x04001E98 RID: 7832
		private bool plan_A;

		// Token: 0x02000397 RID: 919
		public class CardId
		{
			// Token: 0x04001E99 RID: 7833
			public const int SpeedroidTerrortop = 81275020;

			// Token: 0x04001E9A RID: 7834
			public const int WindwitchIceBell = 43722862;

			// Token: 0x04001E9B RID: 7835
			public const int PilicaDescendantOfGusto = 71175527;

			// Token: 0x04001E9C RID: 7836
			public const int SpeedroidTaketomborg = 53932291;

			// Token: 0x04001E9D RID: 7837
			public const int WindaPriestessOfGusto = 54455435;

			// Token: 0x04001E9E RID: 7838
			public const int WindwitchGlassBell = 71007216;

			// Token: 0x04001E9F RID: 7839
			public const int GustoGulldo = 65277087;

			// Token: 0x04001EA0 RID: 7840
			public const int GustoEgul = 91662792;

			// Token: 0x04001EA1 RID: 7841
			public const int WindwitchSnowBell = 70117860;

			// Token: 0x04001EA2 RID: 7842
			public const int SpeedroidRedEyedDice = 16725505;

			// Token: 0x04001EA3 RID: 7843
			public const int Raigeki = 12580477;

			// Token: 0x04001EA4 RID: 7844
			public const int MonsterReborn = 83764719;

			// Token: 0x04001EA5 RID: 7845
			public const int Reasoning = 58577036;

			// Token: 0x04001EA6 RID: 7846
			public const int ElShaddollWinda = 94977269;

			// Token: 0x04001EA7 RID: 7847
			public const int QuillPenOfGulldos = 27980138;

			// Token: 0x04001EA8 RID: 7848
			public const int CosmicCyclone = 8267140;

			// Token: 0x04001EA9 RID: 7849
			public const int EmergencyTeleport = 67723438;

			// Token: 0x04001EAA RID: 7850
			public const int ForbiddenChalice = 25789292;

			// Token: 0x04001EAB RID: 7851
			public const int SuperTeamBuddyForceUnite = 8608979;

			// Token: 0x04001EAC RID: 7852
			public const int KingsConsonance = 24590232;

			// Token: 0x04001EAD RID: 7853
			public const int GozenMatch = 53334471;

			// Token: 0x04001EAE RID: 7854
			public const int SolemnStrike = 40605147;

			// Token: 0x04001EAF RID: 7855
			public const int SolemnWarning = 84749824;

			// Token: 0x04001EB0 RID: 7856
			public const int MistWurm = 27315304;

			// Token: 0x04001EB1 RID: 7857
			public const int CrystalWingSynchroDragon = 50954680;

			// Token: 0x04001EB2 RID: 7858
			public const int ClearWingSynchroDragon = 82044279;

			// Token: 0x04001EB3 RID: 7859
			public const int WindwitchWinterBell = 14577226;

			// Token: 0x04001EB4 RID: 7860
			public const int StardustChargeWarrior = 64880894;

			// Token: 0x04001EB5 RID: 7861
			public const int DaigustoSphreez = 29552709;

			// Token: 0x04001EB6 RID: 7862
			public const int DaigustoGulldos = 84766279;

			// Token: 0x04001EB7 RID: 7863
			public const int HiSpeedroidChanbara = 42110604;

			// Token: 0x04001EB8 RID: 7864
			public const int OldEntityHastorr = 70913714;

			// Token: 0x04001EB9 RID: 7865
			public const int WynnTheWindCharmerVerdant = 30674956;

			// Token: 0x04001EBA RID: 7866
			public const int GreatFly = 90512490;

			// Token: 0x04001EBB RID: 7867
			public const int KnightmareIblee = 10158145;

			// Token: 0x04001EBC RID: 7868
			public const int ChaosMax = 55410871;

			// Token: 0x04001EBD RID: 7869
			public const int SkillDrain = 82732705;

			// Token: 0x04001EBE RID: 7870
			public const int SoulDrain = 73599290;

			// Token: 0x04001EBF RID: 7871
			public const int Rivalry = 90846359;

			// Token: 0x04001EC0 RID: 7872
			public const int OnlyOne = 24207889;
		}
	}
}
