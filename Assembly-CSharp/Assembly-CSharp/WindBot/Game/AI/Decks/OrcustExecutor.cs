using System;
using System.Collections.Generic;
using System.Linq;
using YGOSharp.OCGWrapper;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x0200038D RID: 909
	[Deck("Orcust", "AI_Orcust", "Normal")]
	internal class OrcustExecutor : DefaultExecutor
	{
		// Token: 0x06001AB0 RID: 6832 RVA: 0x0009DB58 File Offset: 0x0009BD58
		public OrcustExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.Activate, 25733157, new Func<bool>(this.EagleBoosterEffect));
			base.AddExecutor(ExecutorType.Activate, 703897, new Func<bool>(this.ClimaxEffect));
			base.AddExecutor(ExecutorType.Activate, 23434538, new Func<bool>(base.DefaultMaxxC));
			base.AddExecutor(ExecutorType.Activate, 14558127, new Func<bool>(base.DefaultAshBlossomAndJoyousSpring));
			base.AddExecutor(ExecutorType.Activate, 73642296, new Func<bool>(base.DefaultGhostBelleAndHauntedMansion));
			base.AddExecutor(ExecutorType.Activate, 24224830, new Func<bool>(base.DefaultCalledByTheGrave));
			base.AddExecutor(ExecutorType.Activate, 73628505, new Func<bool>(this.TerraformingEffect));
			base.AddExecutor(ExecutorType.Activate, 32807846, new Func<bool>(this.ReinforcementofTheArmyEffect));
			base.AddExecutor(ExecutorType.Activate, 81439173, new Func<bool>(this.FoolishBurialEffect));
			base.AddExecutor(ExecutorType.Activate, 35371948, new Func<bool>(this.LightStageEffect));
			base.AddExecutor(ExecutorType.Activate, 63166095, new Func<bool>(this.EngageEffect));
			base.AddExecutor(ExecutorType.Activate, 52340444, new Func<bool>(this.DronesEffectFirst));
			base.AddExecutor(ExecutorType.SpSummon, 63288573);
			base.AddExecutor(ExecutorType.Activate, 63288573);
			base.AddExecutor(ExecutorType.SpSummon, 3679218, new Func<bool>(this.KnightmareMermaidSummon));
			base.AddExecutor(ExecutorType.Activate, 3679218, new Func<bool>(this.KnightmareMermaidEffect));
			base.AddExecutor(ExecutorType.SpSummon, 98169343, new Func<bool>(this.CarobeinSummon));
			base.AddExecutor(ExecutorType.Activate, 98169343);
			base.AddExecutor(ExecutorType.SpellSet, 98827725);
			base.AddExecutor(ExecutorType.Summon, 28985331, new Func<bool>(this.ArmageddonKnightSummon));
			base.AddExecutor(ExecutorType.Activate, 28985331, new Func<bool>(this.ArmageddonKnightEffect));
			base.AddExecutor(ExecutorType.Summon, 4334811, new Func<bool>(this.ScrapRecyclerSummon));
			base.AddExecutor(ExecutorType.Activate, 4334811, new Func<bool>(this.ScrapRecyclerEffect));
			base.AddExecutor(ExecutorType.Activate, 52340444, new Func<bool>(this.DronesEffect));
			base.AddExecutor(ExecutorType.Summon, 9742784, new Func<bool>(this.JetSynchronSummon));
			base.AddExecutor(ExecutorType.Activate, 5560911, new Func<bool>(this.DestrudoSummon));
			base.AddExecutor(ExecutorType.SpSummon, 50588353, new Func<bool>(this.NeedlefiberSummonFirst));
			base.AddExecutor(ExecutorType.Activate, 50588353, new Func<bool>(this.NeedlefiberEffect));
			base.AddExecutor(ExecutorType.Activate, 68431965, new Func<bool>(this.ShootingRiserDragonEffect));
			base.AddExecutor(ExecutorType.Summon, 61283655, new Func<bool>(this.CandinaSummon));
			base.AddExecutor(ExecutorType.Activate, 61283655, new Func<bool>(this.CandinaEffect));
			base.AddExecutor(ExecutorType.Summon, 9742784, new Func<bool>(this.OneCardComboSummon));
			base.AddExecutor(ExecutorType.Summon, 90432163, new Func<bool>(this.OneCardComboSummon));
			base.AddExecutor(ExecutorType.Summon, 36426778, new Func<bool>(this.OneCardComboSummon));
			base.AddExecutor(ExecutorType.SpSummon, 60303245, new Func<bool>(this.AlmirajSummon));
			base.AddExecutor(ExecutorType.Activate, 98827725, new Func<bool>(this.ShadeBrigandineSummonFirst));
			base.AddExecutor(ExecutorType.SpSummon, 2857636, new Func<bool>(this.KnightmarePhoenixSummon));
			base.AddExecutor(ExecutorType.Activate, 2857636, new Func<bool>(this.KnightmarePhoenixEffect));
			base.AddExecutor(ExecutorType.SpSummon, 30741503, new Func<bool>(this.GalateaSummonFirst));
			base.AddExecutor(ExecutorType.Activate, 9742784, new Func<bool>(this.JetSynchronEffect));
			base.AddExecutor(ExecutorType.Activate, 4055337, new Func<bool>(this.OrcustKnightmareEffect));
			base.AddExecutor(ExecutorType.Activate, 57835716, new Func<bool>(this.HarpHorrorEffect));
			base.AddExecutor(ExecutorType.Activate, 93920420, new Func<bool>(this.WorldWandEffect));
			base.AddExecutor(ExecutorType.Activate, 90432163, new Func<bool>(this.AncientCloakEffect));
			base.AddExecutor(ExecutorType.SpSummon, 26692769, new Func<bool>(this.RustyBardicheSummon));
			base.AddExecutor(ExecutorType.Activate, 26692769, new Func<bool>(this.RustyBardicheEffect));
			base.AddExecutor(ExecutorType.Activate, 21441617, new Func<bool>(this.CymbalSkeletonEffect));
			base.AddExecutor(ExecutorType.Activate, 30741503, new Func<bool>(this.GalateaEffect));
			base.AddExecutor(ExecutorType.SpSummon, 93854893, new Func<bool>(this.SheorcustDingirsuSummon));
			base.AddExecutor(ExecutorType.Activate, 93854893, new Func<bool>(this.SheorcustDingirsuEffect));
			base.AddExecutor(ExecutorType.SpSummon, 36426778, new Func<bool>(this.SilentBootsSummon));
			base.AddExecutor(ExecutorType.Activate, 98827725, new Func<bool>(this.ShadeBrigandineSummonSecond));
			base.AddExecutor(ExecutorType.SpSummon, 27548199);
			base.AddExecutor(ExecutorType.Activate, 27548199, new Func<bool>(this.BorreloadSavageDragonEffect));
			base.AddExecutor(ExecutorType.SpSummon, 30741503, new Func<bool>(this.GalateaSummonSecond));
			base.AddExecutor(ExecutorType.Activate, 36426778, new Func<bool>(this.SilentBootsEffect));
			base.AddExecutor(ExecutorType.Summon, 73642296, new Func<bool>(this.TunerSummon));
			base.AddExecutor(ExecutorType.Summon, 14558127, new Func<bool>(this.TunerSummon));
			base.AddExecutor(ExecutorType.Summon, 21441617, new Func<bool>(this.OtherSummon));
			base.AddExecutor(ExecutorType.Summon, 57835716, new Func<bool>(this.OtherSummon));
			base.AddExecutor(ExecutorType.Summon, 90432163, new Func<bool>(this.LinkMaterialSummon));
			base.AddExecutor(ExecutorType.Summon, 23434538, new Func<bool>(this.LinkMaterialSummon));
			base.AddExecutor(ExecutorType.Summon, 36426778, new Func<bool>(this.LinkMaterialSummon));
			base.AddExecutor(ExecutorType.SpSummon, 50588353, new Func<bool>(this.NeedlefiberSummonSecond));
			base.AddExecutor(ExecutorType.SpSummon, 85289965, new Func<bool>(this.BorrelswordDragonSummon));
			base.AddExecutor(ExecutorType.Activate, 85289965, new Func<bool>(this.BorrelswordDragonEffect));
			base.AddExecutor(ExecutorType.SpellSet, 25542642);
			base.AddExecutor(ExecutorType.Activate, 25542642, new Func<bool>(this.FogBladeEffect));
			base.AddExecutor(ExecutorType.SpellSet, 703897);
			base.AddExecutor(ExecutorType.Activate, 90351981, new Func<bool>(this.BabelEffect));
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(this.MonsterRepos));
		}

		// Token: 0x06001AB1 RID: 6833 RVA: 0x0000763C File Offset: 0x0000583C
		public override bool OnSelectHand()
		{
			return true;
		}

		// Token: 0x06001AB2 RID: 6834 RVA: 0x0009E1D4 File Offset: 0x0009C3D4
		public override void OnNewTurn()
		{
			this.NormalSummoned = false;
			this.SheorcustDingirsuSummoned = false;
			this.HarpHorrorUsed = false;
			this.CymbalSkeletonUsed = false;
			this.BorrelswordDragonUsed = false;
			this.RustyBardicheTarget = null;
			base.OnNewTurn();
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x0009E206 File Offset: 0x0009C406
		public override void OnChainEnd()
		{
			this.RustyBardicheTarget = null;
			base.OnChainEnd();
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x0009E218 File Offset: 0x0009C418
		public override CardPosition OnSelectPosition(int cardId, IList<CardPosition> positions)
		{
			NamedCard cardData = NamedCard.Get(cardId);
			if (cardData != null && cardData.Attack <= 1000)
			{
				return CardPosition.FaceUpDefence;
			}
			return (CardPosition)0;
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x0009E240 File Offset: 0x0009C440
		public override int OnSelectPlace(int cardId, int player, CardLocation location, int available)
		{
			if (location == CardLocation.SpellZone)
			{
				if (cardId == 2857636 || cardId == 50588353)
				{
					ClientCard b = base.Bot.MonsterZone.GetFirstMatchingCard((ClientCard card) => card.Id == 27548199);
					int zone = (1 << ((b != null) ? b.Sequence : 0)) & available;
					if (zone > 0)
					{
						return zone;
					}
				}
				if ((available & 1) > 0)
				{
					return 1;
				}
				if ((available & 2) > 0)
				{
					return 2;
				}
				if ((available & 4) > 0)
				{
					return 4;
				}
				if ((available & 8) > 0)
				{
					return 8;
				}
				if ((available & 16) > 0)
				{
					return 16;
				}
			}
			if (location == CardLocation.MonsterZone)
			{
				if (cardId == 93854893)
				{
					ClientCard firstMatchingCard = base.Bot.MonsterZone.GetFirstMatchingCard((ClientCard card) => card.Id == 26692769);
					int zones = ((firstMatchingCard != null) ? firstMatchingCard.GetLinkedZones() : 0) & available;
					if ((zones & 16) > 0)
					{
						return 16;
					}
					if ((zones & 8) > 0)
					{
						return 8;
					}
					if ((zones & 4) > 0)
					{
						return 4;
					}
					if ((zones & 2) > 0)
					{
						return 2;
					}
					if ((zones & 1) > 0)
					{
						return 1;
					}
				}
				if (cardId == 30741503)
				{
					int zones2 = base.Bot.GetLinkedZones() & available;
					if ((zones2 & 1) > 0)
					{
						return 1;
					}
					if ((zones2 & 4) > 0)
					{
						return 4;
					}
					if ((zones2 & 2) > 0)
					{
						return 2;
					}
					if ((zones2 & 8) > 0)
					{
						return 8;
					}
					if ((zones2 & 16) > 0)
					{
						return 16;
					}
				}
				if (cardId == 2857636)
				{
					ClientCard clientCard = base.Enemy.MonsterZone[5];
					if (clientCard != null && clientCard.HasLinkMarker(CardLinkMarker.Top) && (available & 8) > 0)
					{
						return 8;
					}
					ClientCard clientCard2 = base.Enemy.MonsterZone[6];
					if (clientCard2 != null && clientCard2.HasLinkMarker(CardLinkMarker.Top) && (available & 2) > 0)
					{
						return 2;
					}
				}
				if ((available & 64) > 0)
				{
					return 64;
				}
				if ((available & 32) > 0)
				{
					return 32;
				}
				if ((available & 2) > 0)
				{
					return 2;
				}
				if ((available & 8) > 0)
				{
					return 8;
				}
				if ((available & 1) > 0)
				{
					return 1;
				}
				if ((available & 16) > 0)
				{
					return 16;
				}
				if ((available & 4) > 0)
				{
					return 4;
				}
			}
			return 0;
		}

		// Token: 0x06001AB6 RID: 6838 RVA: 0x0009E438 File Offset: 0x0009C638
		public override bool OnPreBattleBetween(ClientCard attacker, ClientCard defender)
		{
			if (!defender.IsMonsterHasPreventActivationEffectInBattle())
			{
				if (attacker.IsCode(61283655) && base.Bot.HasInHand(98169343))
				{
					attacker.RealPower += 1800;
				}
				if (attacker.IsCode(85289965) && !attacker.IsDisabled() && !this.BorrelswordDragonUsed)
				{
					attacker.RealPower += defender.GetDefensePower() / 2;
					defender.RealPower -= defender.GetDefensePower() / 2;
				}
			}
			return base.OnPreBattleBetween(attacker, defender);
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x0009E4CD File Offset: 0x0009C6CD
		private bool TerraformingEffect()
		{
			base.AI.SelectCard(35371948);
			return true;
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x0009E4E0 File Offset: 0x0009C6E0
		private bool ReinforcementofTheArmyEffect()
		{
			base.AI.SelectCard(28985331);
			return true;
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x0009E4F3 File Offset: 0x0009C6F3
		private bool FoolishBurialEffect()
		{
			base.AI.SelectCard(new int[] { 5560911, 9742784, 57835716, 21441617 });
			return true;
		}

		// Token: 0x06001ABA RID: 6842 RVA: 0x0009E514 File Offset: 0x0009C714
		private bool LightStageEffect()
		{
			if (base.Card.Location != CardLocation.Hand && !base.Card.IsFacedown())
			{
				ClientCard target = base.Enemy.SpellZone.GetFirstMatchingCard((ClientCard card) => card.IsFacedown());
				base.AI.SelectCard(target);
				return true;
			}
			ClientCard field = base.Bot.GetFieldSpellCard();
			if (field != null && field.IsCode(90351981) && base.Bot.GetMonsterCount() > 1)
			{
				return false;
			}
			if (field != null && field.IsCode(35371948) && base.Bot.HasInHandOrInMonstersZoneOrInGraveyard(61283655) && base.Bot.HasInHandOrInMonstersZoneOrInGraveyard(98169343))
			{
				return false;
			}
			base.AI.SelectYesNo(true);
			if (base.Bot.HasInHandOrHasInMonstersZone(61283655))
			{
				base.AI.SelectCard(98169343);
			}
			else
			{
				base.AI.SelectCard(61283655);
			}
			return true;
		}

		// Token: 0x06001ABB RID: 6843 RVA: 0x0009E620 File Offset: 0x0009C820
		private bool CarobeinSummon()
		{
			if (base.Bot.HasInMonstersZone(61283655, false, false, false))
			{
				return base.Bot.HasInExtra(2857636);
			}
			if (!this.NormalSummoned)
			{
				return base.Bot.Hand.IsExistingMatchingCard((ClientCard card) => card.Level <= 4, 1);
			}
			return false;
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x0009E690 File Offset: 0x0009C890
		private bool EngageEffect()
		{
			bool needProtect = false;
			if (base.Bot.HasInHand(28985331))
			{
				needProtect = true;
			}
			else
			{
				if (base.Bot.HasInHandOrInGraveyard(5560911))
				{
					if (base.Bot.Hand.IsExistingMatchingCard((ClientCard card) => card.Level <= 4, 1))
					{
						needProtect = true;
						goto IL_0074;
					}
				}
				if (base.Bot.HasInHand(61283655))
				{
					needProtect = true;
				}
			}
			IL_0074:
			if (needProtect)
			{
				base.AI.SelectCard(25733157);
			}
			else
			{
				base.AI.SelectCard(52340444);
			}
			base.AI.SelectYesNo(true);
			return true;
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x00035AA0 File Offset: 0x00033CA0
		private bool DronesEffectFirst()
		{
			return base.Bot.GetMonsterCount() == 0;
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x0009E743 File Offset: 0x0009C943
		private bool DronesEffect()
		{
			return !base.Bot.HasInHand(28985331) && !base.Bot.HasInHand(61283655);
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x0009E76C File Offset: 0x0009C96C
		private bool CandinaSummon()
		{
			this.NormalSummoned = true;
			return true;
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x0009E4CD File Offset: 0x0009C6CD
		private bool CandinaEffect()
		{
			base.AI.SelectCard(35371948);
			return true;
		}

		// Token: 0x06001AC1 RID: 6849 RVA: 0x0009E76C File Offset: 0x0009C96C
		private bool ArmageddonKnightSummon()
		{
			this.NormalSummoned = true;
			return true;
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x0009E776 File Offset: 0x0009C976
		private bool ArmageddonKnightEffect()
		{
			base.AI.SelectCard(new int[] { 5560911, 57835716 });
			return true;
		}

		// Token: 0x06001AC3 RID: 6851 RVA: 0x0009E76C File Offset: 0x0009C96C
		private bool ScrapRecyclerSummon()
		{
			this.NormalSummoned = true;
			return true;
		}

		// Token: 0x06001AC4 RID: 6852 RVA: 0x0009E79A File Offset: 0x0009C99A
		private bool ScrapRecyclerEffect()
		{
			base.AI.SelectCard(new int[] { 9742784, 57835716 });
			return true;
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x0009E7BE File Offset: 0x0009C9BE
		private bool JetSynchronSummon()
		{
			if (base.Bot.GetMonsterCount() > 0)
			{
				this.NormalSummoned = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x0009E7D8 File Offset: 0x0009C9D8
		private bool JetSynchronEffect()
		{
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			base.AI.SelectCard(this.HandCosts);
			return true;
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x0009E7FC File Offset: 0x0009C9FC
		private bool AlmirajSummon()
		{
			if (base.Bot.GetMonsterCount() > 1)
			{
				return false;
			}
			ClientCard mat = base.Bot.GetMonsters().First<ClientCard>();
			if (mat.IsCode(new int[] { 9742784, 90432163, 36426778 }))
			{
				base.AI.SelectMaterials(mat, 0);
				return true;
			}
			return false;
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x0009E854 File Offset: 0x0009CA54
		private bool DestrudoSummon()
		{
			return !base.DefaultCheckWhetherCardIsNegated(base.Card) && base.Bot.GetMonsterCount() < 3 && base.Bot.HasInExtra(new int[] { 50588353, 2857636 });
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x0009E8A4 File Offset: 0x0009CAA4
		private bool NeedlefiberSummonFirst()
		{
			if (!base.Bot.HasInExtra(27548199))
			{
				return false;
			}
			if (!base.Bot.HasInHand(9742784) && base.Bot.GetRemainingCount(9742784, 1) == 0)
			{
				return false;
			}
			int[] matids = new int[]
			{
				5560911, 14558127, 73642296, 52340445, 98169343, 63288573, 4334811, 28985331, 61283655, 57835716,
				21441617, 90432163, 36426778
			};
			if (base.Bot.MonsterZone.GetMatchingCardsCount((ClientCard card) => card.IsCode(matids)) >= 2)
			{
				base.AI.SelectMaterials(matids, 0);
				return true;
			}
			return false;
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x0009E940 File Offset: 0x0009CB40
		private bool NeedlefiberSummonSecond()
		{
			IList<ClientCard> selected = new List<ClientCard>();
			ClientCard tuner = base.Bot.MonsterZone.GetFirstMatchingFaceupCard((ClientCard card) => card.IsCode(new int[] { 5560911, 14558127, 73642296, 9742784 }));
			if (tuner != null)
			{
				selected.Add(tuner);
			}
			int[] matids = new int[] { 52340445, 98827725, 63288573, 4334811, 28985331, 57835716, 21441617, 90432163, 36426778 };
			IList<ClientCard> mats = base.Bot.MonsterZone.GetMatchingCards((ClientCard card) => card.Attack <= 1700);
			int i = 0;
			Func<ClientCard, bool> <>9__2;
			while (i < matids.Length && selected.Count < 2)
			{
				IEnumerable<ClientCard> enumerable = mats;
				Func<ClientCard, bool> func;
				if ((func = <>9__2) == null)
				{
					func = (<>9__2 = (ClientCard card) => card.IsCode(matids[i]));
				}
				ClientCard c = enumerable.GetFirstMatchingFaceupCard(func);
				if (c != null)
				{
					selected.Add(c);
					if (selected.Count == 2 && base.Util.GetBotAvailZonesFromExtraDeck(selected) == 0)
					{
						selected.Remove(c);
					}
				}
				int j = i;
				i = j + 1;
			}
			if (selected.Count == 2)
			{
				base.AI.SelectMaterials(selected, 0);
				return true;
			}
			return false;
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x0009EA7D File Offset: 0x0009CC7D
		private bool NeedlefiberEffect()
		{
			base.AI.SelectCard(9742784);
			return true;
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x0009EA90 File Offset: 0x0009CC90
		private bool ShootingRiserDragonEffect()
		{
			if (base.ActivateDescription == -1 || base.ActivateDescription == base.Util.GetStringId(68431965, 0))
			{
				if (base.Bot.MonsterZone.IsExistingMatchingCard((ClientCard card) => card.Level == 3 && card.IsFaceup() && !card.IsTuner(), 1) && base.Bot.GetRemainingCount(23434538, 3) > 0)
				{
					base.AI.SelectCard(23434538);
				}
				else if (base.Bot.MonsterZone.IsExistingMatchingCard((ClientCard card) => card.Level == 4 && card.IsFaceup() && !card.IsTuner(), 1))
				{
					base.AI.SelectCard(new int[] { 90432163, 36426778, 4334811, 21441617, 14558127, 73642296 });
				}
				else if (base.Bot.MonsterZone.IsExistingMatchingCard((ClientCard card) => card.Level == 5 && card.IsFaceup() && !card.IsTuner(), 1))
				{
					base.AI.SelectCard(new int[] { 57835716, 28985331, 61283655 });
				}
				else
				{
					this.FoolishBurialEffect();
				}
				return true;
			}
			return base.Duel.LastChainPlayer != 0;
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x0009EBD4 File Offset: 0x0009CDD4
		private bool KnightmarePhoenixSummon()
		{
			if (!this.KnightmareMermaidSummon())
			{
				return false;
			}
			if (!base.Bot.HasInExtra(3679218))
			{
				return false;
			}
			int[] firstMats = new int[] { 9742784, 50588353, 52340445, 98827725, 4334811, 63288573, 28985331, 61283655, 98169343 };
			if (base.Bot.MonsterZone.GetMatchingCardsCount((ClientCard card) => card.IsCode(firstMats)) >= 2)
			{
				base.AI.SelectMaterials(firstMats, 0);
				return true;
			}
			int[] secondMats = new int[] { 21441617, 57835716, 5560911, 9742784, 14558127, 73642296, 36426778, 90432163, 23434538, 60303245 };
			int[] mats = firstMats.Concat(secondMats).ToArray<int>();
			if (base.Bot.MonsterZone.GetMatchingCardsCount((ClientCard card) => card.IsCode(mats)) >= 2)
			{
				base.AI.SelectMaterials(mats, 0);
				return true;
			}
			return false;
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x0009ECB0 File Offset: 0x0009CEB0
		private bool KnightmarePhoenixEffect()
		{
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			int matchingCardsCount = base.Bot.Hand.GetMatchingCardsCount((ClientCard card) => card.IsCode(this.HandCosts));
			ClientCard target = base.Enemy.SpellZone.GetFloodgate(false);
			ClientCard anytarget = base.Enemy.SpellZone.GetFirstMatchingCard((ClientCard card) => !card.OwnTargets.Any((ClientCard cont) => cont.IsCode(35371948)));
			if ((matchingCardsCount > 1 && anytarget != null) || (base.Bot.GetHandCount() > 1 && target != null))
			{
				base.AI.SelectCard(this.HandCosts);
				if (target == null)
				{
					target = anytarget;
				}
				base.AI.SelectNextCard(target);
				return true;
			}
			return false;
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x0009ED67 File Offset: 0x0009CF67
		private bool KnightmareMermaidSummon()
		{
			if (base.Bot.GetHandCount() == 0)
			{
				return false;
			}
			if (base.Bot.GetRemainingCount(4055337, 2) == 0)
			{
				return false;
			}
			base.AI.SelectPlace(96);
			return true;
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x0009E7D8 File Offset: 0x0009C9D8
		private bool KnightmareMermaidEffect()
		{
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			base.AI.SelectCard(this.HandCosts);
			return true;
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x0009ED9C File Offset: 0x0009CF9C
		private bool GalateaSummonFirst()
		{
			IList<ClientCard> mats = base.Bot.MonsterZone.GetMatchingCards((ClientCard card) => card.IsCode(new int[] { 3679218, 4055337 }));
			if (mats.Count >= 2)
			{
				base.AI.SelectMaterials(mats, 0);
				return true;
			}
			return false;
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x0009EDF4 File Offset: 0x0009CFF4
		private bool OrcustKnightmareEffect()
		{
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			if (!base.Bot.HasInGraveyard(57835716))
			{
				base.AI.SelectCard(base.Util.GetBestBotMonster(false));
				base.AI.SelectNextCard(57835716);
				return true;
			}
			if (!base.Bot.HasInGraveyard(93920420) && base.Bot.GetRemainingCount(93920420, 1) > 0)
			{
				base.AI.SelectCard(30741503);
				base.AI.SelectNextCard(93920420);
				return true;
			}
			if (!base.Bot.HasInGraveyard(21441617) && base.Bot.GetRemainingCount(21441617, 1) > 0 && base.Bot.HasInGraveyard(93854893) && !this.SheorcustDingirsuSummoned)
			{
				base.AI.SelectCard(30741503);
				base.AI.SelectNextCard(21441617);
				return true;
			}
			return false;
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x0009EEF7 File Offset: 0x0009D0F7
		private bool HarpHorrorEffect()
		{
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			this.HarpHorrorUsed = true;
			base.AI.SelectCard(21441617);
			return true;
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x0009EF21 File Offset: 0x0009D121
		private bool WorldWandEffect()
		{
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			base.AI.SelectCard(21441617);
			return true;
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x0009EF44 File Offset: 0x0009D144
		private bool RustyBardicheSummon()
		{
			IList<ClientCard> mats = base.Bot.MonsterZone.GetMatchingCards((ClientCard card) => card.IsCode(30741503));
			ClientCard mat2 = base.Bot.MonsterZone.GetMatchingCards((ClientCard card) => card.IsCode(21441617)).FirstOrDefault<ClientCard>();
			if (mat2 != null)
			{
				mats.Add(mat2);
			}
			base.AI.SelectMaterials(mats, 0);
			base.AI.SelectPlace(96);
			return true;
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x0009EFDC File Offset: 0x0009D1DC
		private bool RustyBardicheEffect()
		{
			if (base.ActivateDescription != -1 && base.ActivateDescription != base.Util.GetStringId(26692769, 0))
			{
				base.AI.SelectCard(90432163);
				if (base.Bot.HasInMonstersZone(9742784, false, false, false))
				{
					if (!base.Bot.MonsterZone.IsExistingMatchingCard((ClientCard card) => card.Level == 4, 1))
					{
						base.AI.SelectNextCard(98827725);
						return true;
					}
				}
				base.AI.SelectNextCard(25542642);
				return true;
			}
			ClientCard target = this.GetFogBladeTarget();
			if (target == null)
			{
				target = base.Util.GetBestEnemyCard(false, true);
			}
			if (target == null)
			{
				return false;
			}
			this.RustyBardicheTarget = target;
			base.AI.SelectCard(target);
			return true;
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x0009F0B7 File Offset: 0x0009D2B7
		private ClientCard GetFogBladeTarget()
		{
			return base.Enemy.MonsterZone.GetFirstMatchingCard((ClientCard card) => card.OwnTargets.Any((ClientCard cont) => cont.IsCode(25542642)));
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x0009F0E8 File Offset: 0x0009D2E8
		private bool CymbalSkeletonEffect()
		{
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			int[] botTurnTargets = new int[] { 30741503, 93854893 };
			int[] emenyTurnTargets = new int[] { 93854893, 30741503 };
			if (base.Duel.Player == 0 && base.Bot.HasInGraveyard(30741503) && !base.Bot.HasInMonstersZone(30741503, false, false, false) && base.Bot.HasInExtra(93854893) && !this.SheorcustDingirsuSummoned)
			{
				base.AI.SelectCard(botTurnTargets);
				this.CymbalSkeletonUsed = true;
				return true;
			}
			if (base.Duel.Player == 0 && base.Bot.HasInGraveyard(93854893) && !this.SheorcustDingirsuSummoned)
			{
				base.AI.SelectCard(emenyTurnTargets);
				this.SheorcustDingirsuSummoned = true;
				this.CymbalSkeletonUsed = true;
				return true;
			}
			if (base.Duel.Player == 1 && base.Bot.HasInGraveyard(93854893) && !this.SheorcustDingirsuSummoned && (base.Util.GetProblematicEnemyCard(0, false) != null || base.Duel.Phase == DuelPhase.End))
			{
				base.AI.SelectCard(emenyTurnTargets);
				this.CymbalSkeletonUsed = true;
				this.SheorcustDingirsuSummoned = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x0009F23F File Offset: 0x0009D43F
		private bool SheorcustDingirsuSummon()
		{
			this.SheorcustDingirsuSummoned = true;
			return true;
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x0009F24C File Offset: 0x0009D44C
		private bool SheorcustDingirsuEffect()
		{
			if (base.ActivateDescription == 96)
			{
				if ((base.Duel.Phase == DuelPhase.Main1 || base.Duel.Phase == DuelPhase.Main2) && base.Duel.CurrentChain.Count == 0)
				{
					return false;
				}
				base.AI.SelectCard(21441617);
				return true;
			}
			else
			{
				ClientCard target = this.GetFogBladeTarget();
				if (target != null && target != this.RustyBardicheTarget)
				{
					base.AI.SelectOption(0);
					base.AI.SelectCard(target);
					return true;
				}
				target = base.Util.GetProblematicEnemyMonster(0, false);
				if (target != null && target != this.RustyBardicheTarget)
				{
					base.AI.SelectOption(0);
					base.AI.SelectCard(target);
					return true;
				}
				target = base.Util.GetProblematicEnemySpell();
				if (target != null && target != this.RustyBardicheTarget)
				{
					base.AI.SelectOption(0);
					base.AI.SelectCard(target);
					return true;
				}
				if (base.Bot.HasInBanished(21441617))
				{
					base.AI.SelectOption(1);
					base.AI.SelectCard(21441617);
					return true;
				}
				target = base.Enemy.MonsterZone.GetFirstMatchingCard((ClientCard card) => card != this.RustyBardicheTarget) ?? base.Enemy.SpellZone.GetFirstMatchingCard((ClientCard card) => card != this.RustyBardicheTarget);
				if (target != null)
				{
					base.AI.SelectOption(0);
					base.AI.SelectCard(target);
					return true;
				}
				base.AI.SelectOption(1);
				return true;
			}
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x0009F3D4 File Offset: 0x0009D5D4
		private bool AncientCloakEffect()
		{
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			if (base.Bot.HasInMonstersZone(60303245, false, false, false) && base.Bot.HasInExtra(2857636))
			{
				base.AI.SelectCard(98827725);
			}
			else
			{
				base.AI.SelectCard(36426778);
			}
			return true;
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x0000763C File Offset: 0x0000583C
		private bool SilentBootsSummon()
		{
			return true;
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x0009F43C File Offset: 0x0009D63C
		private bool SilentBootsEffect()
		{
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			if (base.Bot.HasInMonstersZone(60303245, false, false, false) && base.Bot.HasInExtra(2857636))
			{
				base.AI.SelectCard(98827725);
			}
			else
			{
				base.AI.SelectCard(25542642);
			}
			return true;
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x0009F4A4 File Offset: 0x0009D6A4
		private bool ShadeBrigandineSummonSecond()
		{
			return base.DefaultOnBecomeTarget() || (base.Bot.HasInMonstersZone(60303245, false, false, false) && base.Bot.HasInExtra(2857636)) || (base.Bot.HasInMonstersZone(9742784, false, false, false) && base.Bot.HasInMonstersZone(36426778, false, false, false));
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x0009F510 File Offset: 0x0009D710
		private bool GalateaSummonSecond()
		{
			if (!base.Util.IsTurn1OrMain2())
			{
				return false;
			}
			if (base.Bot.HasInMonstersZone(30741503, false, false, false))
			{
				return false;
			}
			IList<ClientCard> selected = new List<ClientCard>();
			if (!base.Bot.HasInGraveyard(93854893))
			{
				ClientCard sheorcustDingirsu = base.Bot.MonsterZone.GetFirstMatchingFaceupCard((ClientCard card) => card.IsCode(93854893));
				if (sheorcustDingirsu != null)
				{
					selected.Add(sheorcustDingirsu);
				}
			}
			int[] matids = new int[] { 4055337, 36426778, 90432163, 21441617, 57835716, 4334811, 50588353, 63288573, 3679218, 28985331 };
			IList<ClientCard> mats = base.Bot.MonsterZone.GetMatchingCards((ClientCard card) => card.Level > 0 && card.Level <= 7);
			int i = 0;
			Func<ClientCard, bool> <>9__2;
			while (i < matids.Length && selected.Count < 2)
			{
				IEnumerable<ClientCard> enumerable = mats;
				Func<ClientCard, bool> func;
				if ((func = <>9__2) == null)
				{
					func = (<>9__2 = (ClientCard card) => card.IsCode(matids[i]));
				}
				ClientCard c = enumerable.GetFirstMatchingFaceupCard(func);
				if (c != null)
				{
					selected.Add(c);
					if (selected.Count == 2 && base.Util.GetBotAvailZonesFromExtraDeck(selected) == 0)
					{
						selected.Remove(c);
					}
				}
				int j = i;
				i = j + 1;
			}
			if (selected.Count == 2)
			{
				base.AI.SelectMaterials(selected, 0);
				return true;
			}
			return false;
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x0009F688 File Offset: 0x0009D888
		private bool GalateaEffect()
		{
			if (base.Duel.Player == 0)
			{
				base.AI.SelectCard(4055337);
				base.AI.SelectNextCard(90351981);
			}
			if (base.Duel.Player == 1)
			{
				base.AI.SelectCard(4055337);
				base.AI.SelectNextCard(703897);
			}
			return true;
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x0009F6F4 File Offset: 0x0009D8F4
		private bool BorrelswordDragonSummon()
		{
			if (base.Util.IsTurn1OrMain2())
			{
				return false;
			}
			List<ClientCard> list = base.Bot.MonsterZone.GetMatchingCards((ClientCard card) => card.IsFaceup() && card.HasType(CardType.Effect) && card.Attack <= 2000).ToList<ClientCard>();
			list.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			list.Reverse();
			int link = 0;
			bool doubleused = false;
			IList<ClientCard> selected = new List<ClientCard>();
			foreach (ClientCard card2 in list)
			{
				selected.Add(card2);
				if (!doubleused && card2.LinkCount == 2)
				{
					doubleused = true;
					link += 2;
				}
				else
				{
					link++;
				}
				if (link >= 4)
				{
					break;
				}
			}
			if (link >= 4 && base.Util.GetBotAvailZonesFromExtraDeck(selected) > 0)
			{
				base.AI.SelectMaterials(selected, 0);
				return true;
			}
			return false;
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x0009F7EC File Offset: 0x0009D9EC
		private bool BorrelswordDragonEffect()
		{
			if (base.ActivateDescription == -1 || base.ActivateDescription == base.Util.GetStringId(85289965, 1))
			{
				this.BorrelswordDragonUsed = true;
				return true;
			}
			if (base.Duel.Player == 0 && (base.Duel.Turn == 1 || base.Duel.Phase >= DuelPhase.Main2))
			{
				return false;
			}
			ClientCard target = base.Bot.MonsterZone.GetFirstMatchingCard((ClientCard card) => card.IsAttack() && !card.HasType(CardType.Link) && card.Attacked && !card.IsShouldNotBeTarget());
			if (target != null)
			{
				base.AI.SelectCard(target);
				return true;
			}
			if (!base.Bot.MonsterZone.IsExistingMatchingCard((ClientCard card) => card.IsAttack() && !card.HasType(CardType.Link), 1))
			{
				target = base.Enemy.MonsterZone.GetFirstMatchingCard((ClientCard card) => card.IsAttack() && !card.HasType(CardType.Link) && !card.IsShouldNotBeTarget());
				if (target != null)
				{
					base.AI.SelectCard(target);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001AE3 RID: 6883 RVA: 0x0009F908 File Offset: 0x0009DB08
		private bool BabelEffect()
		{
			if (base.Card.Location != CardLocation.Grave)
			{
				return base.Bot.HasInMonstersZoneOrInGraveyard(new int[] { 21441617, 57835716, 4055337, 30741503, 76145142, 93854893 });
			}
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			if (base.Bot.Hand.GetMatchingCards((ClientCard card) => card.IsCode(this.HandCosts)).Count > 0)
			{
				base.AI.SelectCard(this.HandCosts);
				return true;
			}
			return false;
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x0009F989 File Offset: 0x0009DB89
		private bool ShadeBrigandineSummonFirst()
		{
			return base.Bot.GetMonsterCount() < 2;
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x0009F99C File Offset: 0x0009DB9C
		private bool OneCardComboSummon()
		{
			if (base.Bot.HasInExtra(60303245) && base.Bot.HasInExtra(new int[] { 50588353, 2857636 }) && base.Bot.GetMonsterCount() < 3)
			{
				this.NormalSummoned = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x0009F9F6 File Offset: 0x0009DBF6
		private bool LinkMaterialSummon()
		{
			if (base.Bot.HasInExtra(2857636) && base.Bot.GetMonsterCount() > 0 && base.Bot.GetMonsterCount() < 3)
			{
				this.NormalSummoned = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x0009FA30 File Offset: 0x0009DC30
		private bool TunerSummon()
		{
			if (base.Bot.HasInExtra(new int[] { 50588353, 2857636 }) && base.Bot.GetMonsterCount() > 0 && base.Bot.GetMonsterCount() < 3)
			{
				this.NormalSummoned = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x0009E76C File Offset: 0x0009C96C
		private bool OtherSummon()
		{
			this.NormalSummoned = true;
			return true;
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x0009FA86 File Offset: 0x0009DC86
		private bool BorreloadSavageDragonEffect()
		{
			if (base.Duel.CurrentChain.Count == 0)
			{
				base.AI.SelectCard(new int[] { 2857636, 50588353 });
				return true;
			}
			return true;
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x0009FAC0 File Offset: 0x0009DCC0
		private bool FogBladeEffect()
		{
			if (base.Card.Location == CardLocation.SpellZone)
			{
				return !base.Util.HasChainedTrap(0) && base.DefaultDisableMonster();
			}
			if (!base.Bot.HasInGraveyard(26692769) && base.Bot.GetMonsterCount() >= 2)
			{
				return false;
			}
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			base.AI.SelectCard(26692769);
			return true;
		}

		// Token: 0x06001AEB RID: 6891 RVA: 0x0009FB38 File Offset: 0x0009DD38
		private bool ClimaxEffect()
		{
			if (base.Card.Location == CardLocation.SpellZone)
			{
				return base.Duel.LastChainPlayer == 1;
			}
			if (base.Duel.Phase == DuelPhase.End)
			{
				ClientCard target = base.Bot.Banished.GetFirstMatchingFaceupCard((ClientCard card) => card.IsCode(21441617));
				if (target == null)
				{
					target = base.Bot.Banished.GetFirstMatchingFaceupCard((ClientCard card) => card.IsCode(57835716));
				}
				if (target != null)
				{
					base.AI.SelectCard(target);
					return true;
				}
				if (!base.Bot.HasInHand(57835716) && base.Bot.GetRemainingCount(57835716, 2) > 1)
				{
					base.AI.SelectCard(57835716);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001AEC RID: 6892 RVA: 0x0009FC28 File Offset: 0x0009DE28
		private bool EagleBoosterEffect()
		{
			if (base.Duel.LastChainPlayer != 1)
			{
				return false;
			}
			ClientCard target = base.Bot.GetMonstersInExtraZone().GetFirstMatchingCard((ClientCard card) => base.Duel.CurrentChain.Contains(card) || card.IsCode(3679218));
			if (target != null)
			{
				base.AI.SelectCard(target);
				return true;
			}
			return false;
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x00057C2C File Offset: 0x00055E2C
		private bool MonsterRepos()
		{
			return base.Card.IsFacedown() || base.DefaultMonsterRepos();
		}

		// Token: 0x04001E06 RID: 7686
		private bool NormalSummoned;

		// Token: 0x04001E07 RID: 7687
		private bool SheorcustDingirsuSummoned;

		// Token: 0x04001E08 RID: 7688
		private bool HarpHorrorUsed;

		// Token: 0x04001E09 RID: 7689
		private bool CymbalSkeletonUsed;

		// Token: 0x04001E0A RID: 7690
		private bool BorrelswordDragonUsed;

		// Token: 0x04001E0B RID: 7691
		private ClientCard RustyBardicheTarget;

		// Token: 0x04001E0C RID: 7692
		private int[] HandCosts = new int[]
		{
			21441617, 4055337, 5560911, 93920420, 57835716, 90432163, 36426778, 9742784, 35371948, 63166095,
			73628505, 32807846, 23434538, 73642296
		};

		// Token: 0x0200038E RID: 910
		public class CardId
		{
			// Token: 0x04001E0D RID: 7693
			public const int OrcustKnightmare = 4055337;

			// Token: 0x04001E0E RID: 7694
			public const int OrcustHarpHorror = 57835716;

			// Token: 0x04001E0F RID: 7695
			public const int OrcustCymbalSkeleton = 21441617;

			// Token: 0x04001E10 RID: 7696
			public const int WorldLegacyWorldWand = 93920420;

			// Token: 0x04001E11 RID: 7697
			public const int ThePhantomKnightsofAncientCloak = 90432163;

			// Token: 0x04001E12 RID: 7698
			public const int ThePhantomKnightsofSilentBoots = 36426778;

			// Token: 0x04001E13 RID: 7699
			public const int TrickstarCarobein = 98169343;

			// Token: 0x04001E14 RID: 7700
			public const int TrickstarCandina = 61283655;

			// Token: 0x04001E15 RID: 7701
			public const int ArmageddonKnight = 28985331;

			// Token: 0x04001E16 RID: 7702
			public const int ScrapRecycler = 4334811;

			// Token: 0x04001E17 RID: 7703
			public const int DestrudoTheLostDragonsFrisson = 5560911;

			// Token: 0x04001E18 RID: 7704
			public const int JetSynchron = 9742784;

			// Token: 0x04001E19 RID: 7705
			public const int AshBlossomJoyousSpring = 14558127;

			// Token: 0x04001E1A RID: 7706
			public const int GhostBelleHauntedMansion = 73642296;

			// Token: 0x04001E1B RID: 7707
			public const int MaxxC = 23434538;

			// Token: 0x04001E1C RID: 7708
			public const int SkyStrikerMobilizeEngage = 63166095;

			// Token: 0x04001E1D RID: 7709
			public const int SkyStrikerMechaEagleBooster = 25733157;

			// Token: 0x04001E1E RID: 7710
			public const int SkyStrikerMechaHornetDrones = 52340444;

			// Token: 0x04001E1F RID: 7711
			public const int SkyStrikerMechaHornetDronesToken = 52340445;

			// Token: 0x04001E20 RID: 7712
			public const int TrickstarLightStage = 35371948;

			// Token: 0x04001E21 RID: 7713
			public const int OrcustratedBabel = 90351981;

			// Token: 0x04001E22 RID: 7714
			public const int ReinforcementofTheArmy = 32807846;

			// Token: 0x04001E23 RID: 7715
			public const int Terraforming = 73628505;

			// Token: 0x04001E24 RID: 7716
			public const int FoolishBurial = 81439173;

			// Token: 0x04001E25 RID: 7717
			public const int CalledbyTheGrave = 24224830;

			// Token: 0x04001E26 RID: 7718
			public const int ThePhantomKnightsofShadeBrigandine = 98827725;

			// Token: 0x04001E27 RID: 7719
			public const int PhantomKnightsFogBlade = 25542642;

			// Token: 0x04001E28 RID: 7720
			public const int OrcustratedClimax = 703897;

			// Token: 0x04001E29 RID: 7721
			public const int BorreloadSavageDragon = 27548199;

			// Token: 0x04001E2A RID: 7722
			public const int ShootingRiserDragon = 68431965;

			// Token: 0x04001E2B RID: 7723
			public const int SheorcustDingirsu = 93854893;

			// Token: 0x04001E2C RID: 7724
			public const int BorrelswordDragon = 85289965;

			// Token: 0x04001E2D RID: 7725
			public const int LongirsuTheOrcustOrchestrator = 76145142;

			// Token: 0x04001E2E RID: 7726
			public const int ThePhantomKnightsofRustyBardiche = 26692769;

			// Token: 0x04001E2F RID: 7727
			public const int KnightmarePhoenix = 2857636;

			// Token: 0x04001E30 RID: 7728
			public const int GalateaTheOrcustAutomaton = 30741503;

			// Token: 0x04001E31 RID: 7729
			public const int CrystronNeedlefiber = 50588353;

			// Token: 0x04001E32 RID: 7730
			public const int SkyStrikerAceKagari = 63288573;

			// Token: 0x04001E33 RID: 7731
			public const int KnightmareMermaid = 3679218;

			// Token: 0x04001E34 RID: 7732
			public const int SalamangreatAlmiraj = 60303245;
		}
	}
}
