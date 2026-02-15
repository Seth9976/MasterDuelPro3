using System;
using System.Collections.Generic;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x02000350 RID: 848
	[Deck("Lightsworn", "AI_Lightsworn", "NotFinished")]
	public class LightswornExecutor : DefaultExecutor
	{
		// Token: 0x060016D0 RID: 5840 RVA: 0x00087384 File Offset: 0x00085584
		public LightswornExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.Activate, 18144506, new Func<bool>(base.DefaultHarpiesFeatherDusterFirst));
			base.AddExecutor(ExecutorType.Activate, 5133471, new Func<bool>(base.DefaultGalaxyCyclone));
			base.AddExecutor(ExecutorType.Activate, 18144506);
			base.AddExecutor(ExecutorType.Activate, 73594093);
			base.AddExecutor(ExecutorType.Activate, 67441435);
			base.AddExecutor(ExecutorType.Activate, 57774843, new Func<bool>(base.DefaultDarkHole));
			base.AddExecutor(ExecutorType.SpSummon, 57774843);
			base.AddExecutor(ExecutorType.Activate, 32807846, new Func<bool>(this.ReinforcementOfTheArmyEffect));
			base.AddExecutor(ExecutorType.Activate, 94886282, new Func<bool>(this.ChargeOfTheLightBrigadeEffect));
			base.AddExecutor(ExecutorType.Activate, 691925, new Func<bool>(this.SolarRechargeEffect));
			base.AddExecutor(ExecutorType.Summon, 25259669, new Func<bool>(this.GoblindberghSummon));
			base.AddExecutor(ExecutorType.Activate, 25259669, new Func<bool>(this.GoblindberghEffect));
			base.AddExecutor(ExecutorType.SpSummon, 46772449, new Func<bool>(base.DefaultEvilswarmExcitonKnightSummon));
			base.AddExecutor(ExecutorType.Activate, 46772449, new Func<bool>(base.DefaultEvilswarmExcitonKnightEffect));
			base.AddExecutor(ExecutorType.SpSummon, 82633039, new Func<bool>(base.DefaultCastelTheSkyblasterMusketeerSummon));
			base.AddExecutor(ExecutorType.Activate, 82633039, new Func<bool>(base.DefaultCastelTheSkyblasterMusketeerEffect));
			base.AddExecutor(ExecutorType.SpSummon, 80666118, new Func<bool>(base.DefaultScarlightRedDragonArchfiendSummon));
			base.AddExecutor(ExecutorType.Activate, 80666118, new Func<bool>(base.DefaultScarlightRedDragonArchfiendEffect));
			base.AddExecutor(ExecutorType.SpSummon, 84013237, new Func<bool>(base.DefaultNumberS39UtopiaTheLightningSummon));
			base.AddExecutor(ExecutorType.SpSummon, 56832966);
			base.AddExecutor(ExecutorType.Activate, 56832966, new Func<bool>(base.DefaultNumberS39UtopiaTheLightningEffect));
			base.AddExecutor(ExecutorType.Activate, 67696066, new Func<bool>(this.PerformageTrickClownEffect));
			base.AddExecutor(ExecutorType.Activate, 1833916);
			base.AddExecutor(ExecutorType.Activate, 37742478, new Func<bool>(base.DefaultHonestEffect));
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(base.DefaultMonsterRepos));
		}

		// Token: 0x060016D1 RID: 5841 RVA: 0x000875A4 File Offset: 0x000857A4
		public override void OnNewTurn()
		{
			this.ClownUsed = false;
			base.OnNewTurn();
		}

		// Token: 0x060016D2 RID: 5842 RVA: 0x000875B4 File Offset: 0x000857B4
		public override bool OnPreBattleBetween(ClientCard attacker, ClientCard defender)
		{
			if (!defender.IsMonsterHasPreventActivationEffectInBattle() && attacker.Attribute == 16 && base.Bot.HasInHand(37742478))
			{
				attacker.RealPower += defender.Attack;
			}
			return base.OnPreBattleBetween(attacker, defender);
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x00087600 File Offset: 0x00085800
		public override IList<ClientCard> OnSelectXyzMaterial(IList<ClientCard> cards, int min, int max)
		{
			Logger.DebugWriteLine(string.Concat(new string[]
			{
				"OnSelectXyzMaterial ",
				cards.Count.ToString(),
				" ",
				min.ToString(),
				" ",
				max.ToString()
			}));
			IList<ClientCard> result = new List<ClientCard>();
			foreach (ClientCard card in cards)
			{
				if (!result.Contains(card) && (!this.ClownUsed || !card.IsCode(67696066)))
				{
					result.Add(card);
				}
				if (result.Count >= max)
				{
					break;
				}
			}
			return base.Util.CheckSelectCount(result, cards, min, max);
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x000876D4 File Offset: 0x000858D4
		private bool ReinforcementOfTheArmyEffect()
		{
			if (!base.Bot.HasInHand(77558536))
			{
				base.AI.SelectCard(77558536);
				return true;
			}
			if (!base.Bot.HasInHand(25259669))
			{
				base.AI.SelectCard(25259669);
				return true;
			}
			return false;
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x0008772C File Offset: 0x0008592C
		private bool ChargeOfTheLightBrigadeEffect()
		{
			if (!base.Bot.HasInHand(95503687))
			{
				base.AI.SelectCard(95503687);
			}
			else
			{
				base.AI.SelectCard(new int[] { 77558536, 95503687, 40164421, 22624373 });
			}
			return true;
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x0008777A File Offset: 0x0008597A
		private bool SolarRechargeEffect()
		{
			base.AI.SelectCard(new int[] { 58996430, 73176465, 40164421, 22624373, 77558536 });
			return true;
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x0008779C File Offset: 0x0008599C
		private bool GoblindberghSummon()
		{
			foreach (ClientCard card in base.Bot.Hand.GetMonsters())
			{
				if (!card.Equals(base.Card) && card.Level == 4)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x00087810 File Offset: 0x00085A10
		private bool GoblindberghEffect()
		{
			base.AI.SelectCard(new int[] { 73176465, 58996430, 77558536, 67696066, 1833916 });
			return true;
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x00087830 File Offset: 0x00085A30
		private bool LuminaEffect()
		{
			if (!base.Bot.HasInGraveyard(77558536) && base.Bot.HasInHand(77558536))
			{
				base.AI.SelectCard(77558536);
			}
			else if (!this.ClownUsed && base.Bot.HasInHand(67696066))
			{
				base.AI.SelectCard(67696066);
			}
			else
			{
				base.AI.SelectCard(new int[] { 58996430, 73176465, 40164421, 1833916 });
			}
			base.AI.SelectNextCard(new int[] { 77558536, 73176465 });
			return true;
		}

		// Token: 0x060016DA RID: 5850 RVA: 0x000878DD File Offset: 0x00085ADD
		private bool PerformageTrickClownEffect()
		{
			this.ClownUsed = true;
			base.AI.SelectPosition(CardPosition.FaceUpDefence);
			return true;
		}

		// Token: 0x060016DB RID: 5851 RVA: 0x000878F4 File Offset: 0x00085AF4
		private bool MinervaTheExaltedEffect()
		{
			if (base.Card.Location == CardLocation.MonsterZone)
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
			base.AI.SelectNextCard(targets);
			return true;
		}

		// Token: 0x04001AE2 RID: 6882
		private bool ClownUsed;

		// Token: 0x02000351 RID: 849
		public class CardId
		{
			// Token: 0x04001AE3 RID: 6883
			public const int JudgmentDragon = 57774843;

			// Token: 0x04001AE4 RID: 6884
			public const int Wulf = 58996430;

			// Token: 0x04001AE5 RID: 6885
			public const int Garoth = 59019082;

			// Token: 0x04001AE6 RID: 6886
			public const int Raiden = 77558536;

			// Token: 0x04001AE7 RID: 6887
			public const int Lyla = 22624373;

			// Token: 0x04001AE8 RID: 6888
			public const int Felis = 73176465;

			// Token: 0x04001AE9 RID: 6889
			public const int Lumina = 95503687;

			// Token: 0x04001AEA RID: 6890
			public const int Minerva = 40164421;

			// Token: 0x04001AEB RID: 6891
			public const int Ryko = 21502796;

			// Token: 0x04001AEC RID: 6892
			public const int PerformageTrickClown = 67696066;

			// Token: 0x04001AED RID: 6893
			public const int Goblindbergh = 25259669;

			// Token: 0x04001AEE RID: 6894
			public const int ThousandBlades = 1833916;

			// Token: 0x04001AEF RID: 6895
			public const int Honest = 37742478;

			// Token: 0x04001AF0 RID: 6896
			public const int GlowUpBulb = 67441435;

			// Token: 0x04001AF1 RID: 6897
			public const int SolarRecharge = 691925;

			// Token: 0x04001AF2 RID: 6898
			public const int GalaxyCyclone = 5133471;

			// Token: 0x04001AF3 RID: 6899
			public const int HarpiesFeatherDuster = 18144506;

			// Token: 0x04001AF4 RID: 6900
			public const int ReinforcementOfTheArmy = 32807846;

			// Token: 0x04001AF5 RID: 6901
			public const int MetalfoesFusion = 73594093;

			// Token: 0x04001AF6 RID: 6902
			public const int ChargeOfTheLightBrigade = 94886282;

			// Token: 0x04001AF7 RID: 6903
			public const int Michael = 4779823;

			// Token: 0x04001AF8 RID: 6904
			public const int MinervaTheExalted = 30100551;

			// Token: 0x04001AF9 RID: 6905
			public const int TrishulaDragonOfTheIceBarrier = 52687916;

			// Token: 0x04001AFA RID: 6906
			public const int ScarlightRedDragonArchfiend = 80666118;

			// Token: 0x04001AFB RID: 6907
			public const int PSYFramelordOmega = 74586817;

			// Token: 0x04001AFC RID: 6908
			public const int PSYFramelordZeta = 37192109;

			// Token: 0x04001AFD RID: 6909
			public const int NumberS39UtopiatheLightning = 56832966;

			// Token: 0x04001AFE RID: 6910
			public const int Number39Utopia = 84013237;

			// Token: 0x04001AFF RID: 6911
			public const int CastelTheSkyblasterMusketeer = 82633039;

			// Token: 0x04001B00 RID: 6912
			public const int EvilswarmExcitonKnight = 46772449;

			// Token: 0x04001B01 RID: 6913
			public const int DanteTravelerOfTheBurningAbyss = 83531441;

			// Token: 0x04001B02 RID: 6914
			public const int DecodeTalker = 1861629;

			// Token: 0x04001B03 RID: 6915
			public const int MissusRadiant = 3987233;
		}
	}
}
