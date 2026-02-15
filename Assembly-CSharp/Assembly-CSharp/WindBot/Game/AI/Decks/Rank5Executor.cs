using System;
using System.Collections.Generic;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x0200039E RID: 926
	[Deck("Rank V", "AI_Rank5", "Normal")]
	public class Rank5Executor : DefaultExecutor
	{
		// Token: 0x06001BAC RID: 7084 RVA: 0x000A5848 File Offset: 0x000A3A48
		public Rank5Executor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.Activate, 14087893, new Func<bool>(base.DefaultBookOfMoon));
			base.AddExecutor(ExecutorType.Activate, 5318639, new Func<bool>(base.DefaultMysticalSpaceTyphoon));
			base.AddExecutor(ExecutorType.SpSummon, 58069384, new Func<bool>(this.CyberDragonNovaSummon));
			base.AddExecutor(ExecutorType.Activate, 58069384, new Func<bool>(this.CyberDragonNovaEffect));
			base.AddExecutor(ExecutorType.SpSummon, 10443957, new Func<bool>(this.CyberDragonInfinitySummon));
			base.AddExecutor(ExecutorType.Activate, 10443957, new Func<bool>(this.CyberDragonInfinityEffect));
			base.AddExecutor(ExecutorType.SpSummon, 70095154);
			base.AddExecutor(ExecutorType.SpSummon, 29353756);
			base.AddExecutor(ExecutorType.Summon, 88552992, new Func<bool>(this.NormalSummon));
			base.AddExecutor(ExecutorType.Activate, 88552992, new Func<bool>(this.ChronomalyGoldenJetEffect));
			base.AddExecutor(ExecutorType.Summon, 24610207, new Func<bool>(this.NormalSummon));
			base.AddExecutor(ExecutorType.Summon, 12299841, new Func<bool>(this.NormalSummon));
			base.AddExecutor(ExecutorType.Activate, 12299841, new Func<bool>(this.WindUpSoldierEffect));
			base.AddExecutor(ExecutorType.SpSummon, 29669359, new Func<bool>(this.Number61VolcasaurusSummon));
			base.AddExecutor(ExecutorType.Activate, 29669359, new Func<bool>(this.Number61VolcasaurusEffect));
			base.AddExecutor(ExecutorType.SpSummon, 31386180);
			base.AddExecutor(ExecutorType.Activate, 31386180, new Func<bool>(this.TirasKeeperOfGenesisEffect));
			base.AddExecutor(ExecutorType.SpSummon, 50449881);
			base.AddExecutor(ExecutorType.Activate, 50449881);
			base.AddExecutor(ExecutorType.SpSummon, 91949988, new Func<bool>(this.GaiaDragonTheThunderChargerSummon));
			base.AddExecutor(ExecutorType.SpSummon, 33911264, new Func<bool>(this.SolarWindJammerSummon));
			base.AddExecutor(ExecutorType.SpSummon, 20932152, new Func<bool>(this.QuickdrawSynchronSummon));
			base.AddExecutor(ExecutorType.Summon, 28601770, new Func<bool>(this.MistArchfiendSummon));
			base.AddExecutor(ExecutorType.Activate, 1845204, new Func<bool>(this.InstantFusionEffect));
			base.AddExecutor(ExecutorType.Activate, 43422537, new Func<bool>(this.DoubleSummonEffect));
			base.AddExecutor(ExecutorType.Activate, 13032689, new Func<bool>(this.XyzUnitEffect));
			base.AddExecutor(ExecutorType.Activate, 26708437, new Func<bool>(this.XyzRebornEffect));
			base.AddExecutor(ExecutorType.Activate, 72959823, new Func<bool>(this.PanzerDragonEffect));
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(base.DefaultMonsterRepos));
			base.AddExecutor(ExecutorType.SpellSet, new Func<bool>(base.DefaultSpellSet));
			base.AddExecutor(ExecutorType.Activate, 96457619, new Func<bool>(this.XyzVeilEffect));
			base.AddExecutor(ExecutorType.Activate, 53582587, new Func<bool>(base.DefaultTorrentialTribute));
			base.AddExecutor(ExecutorType.Activate, 44095762, new Func<bool>(base.DefaultTrap));
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnSelectHand()
		{
			return false;
		}

		// Token: 0x06001BAE RID: 7086 RVA: 0x000A5B2F File Offset: 0x000A3D2F
		public override void OnNewTurn()
		{
			this.NormalSummoned = false;
			this.InstantFusionUsed = false;
			this.DoubleSummonUsed = false;
			this.CyberDragonInfinitySummoned = false;
			this.Number61VolcasaurusUsed = false;
			base.OnNewTurn();
		}

		// Token: 0x06001BAF RID: 7087 RVA: 0x000A5B5C File Offset: 0x000A3D5C
		public override IList<ClientCard> OnSelectXyzMaterial(IList<ClientCard> cards, int min, int max)
		{
			IList<ClientCard> result = base.Util.SelectPreferredCards(new int[] { 28601770, 72959823, 33911264, 24610207 }, cards, min, max);
			return base.Util.CheckSelectCount(result, cards, min, max);
		}

		// Token: 0x06001BB0 RID: 7088 RVA: 0x000A5B98 File Offset: 0x000A3D98
		private bool NormalSummon()
		{
			this.NormalSummoned = true;
			return true;
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x000A5BA2 File Offset: 0x000A3DA2
		private bool SolarWindJammerSummon()
		{
			if (!this.NeedLV5())
			{
				return false;
			}
			base.AI.SelectPosition(CardPosition.FaceUpDefence);
			return true;
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x000A5BBB File Offset: 0x000A3DBB
		private bool QuickdrawSynchronSummon()
		{
			if (!this.NeedLV5())
			{
				return false;
			}
			base.AI.SelectCard(new int[] { 20932152, 29353756, 33911264, 70095154, 28601770, 12299841, 24610207, 88552992 });
			return true;
		}

		// Token: 0x06001BB3 RID: 7091 RVA: 0x000A5BE4 File Offset: 0x000A3DE4
		private bool MistArchfiendSummon()
		{
			if (!this.NeedLV5())
			{
				return false;
			}
			base.AI.SelectOption(1);
			this.NormalSummoned = true;
			return true;
		}

		// Token: 0x06001BB4 RID: 7092 RVA: 0x000A5C04 File Offset: 0x000A3E04
		private bool InstantFusionEffect()
		{
			if (!this.NeedLV5())
			{
				return false;
			}
			this.InstantFusionUsed = true;
			return true;
		}

		// Token: 0x06001BB5 RID: 7093 RVA: 0x000A5C18 File Offset: 0x000A3E18
		private bool NeedLV5()
		{
			if (this.HaveOtherLV5OnField())
			{
				return true;
			}
			if (base.Util.GetBotAvailZonesFromExtraDeck() == 0)
			{
				return false;
			}
			int lv5Count = 0;
			foreach (ClientCard clientCard in base.Bot.Hand)
			{
				if (clientCard.IsCode(33911264) && base.Bot.GetMonsterCount() == 0)
				{
					lv5Count++;
				}
				if (clientCard.IsCode(1845204) && !this.InstantFusionUsed)
				{
					lv5Count++;
				}
				if (clientCard.IsCode(20932152) && base.Bot.Hand.ContainsMonsterWithLevel(4))
				{
					lv5Count++;
				}
				if (clientCard.IsCode(28601770) && !this.NormalSummoned)
				{
					lv5Count++;
				}
				if (clientCard.IsCode(43422537) && this.DoubleSummonEffect())
				{
					lv5Count++;
				}
			}
			return lv5Count >= 2;
		}

		// Token: 0x06001BB6 RID: 7094 RVA: 0x000A5D18 File Offset: 0x000A3F18
		private bool WindUpSoldierEffect()
		{
			return this.HaveOtherLV5OnField();
		}

		// Token: 0x06001BB7 RID: 7095 RVA: 0x000A5D20 File Offset: 0x000A3F20
		private bool ChronomalyGoldenJetEffect()
		{
			return base.Card.Level == 4;
		}

		// Token: 0x06001BB8 RID: 7096 RVA: 0x000A5D30 File Offset: 0x000A3F30
		private bool DoubleSummonEffect()
		{
			if (!this.NormalSummoned || this.DoubleSummonUsed)
			{
				return false;
			}
			if (base.Bot.HasInHand(new int[] { 12299841, 24610207, 88552992, 28601770 }))
			{
				this.NormalSummoned = false;
				this.DoubleSummonUsed = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001BB9 RID: 7097 RVA: 0x000A5D7E File Offset: 0x000A3F7E
		private bool CyberDragonNovaSummon()
		{
			return !this.CyberDragonInfinitySummoned;
		}

		// Token: 0x06001BBA RID: 7098 RVA: 0x000A5D89 File Offset: 0x000A3F89
		private bool CyberDragonNovaEffect()
		{
			if (base.ActivateDescription == base.Util.GetStringId(58069384, 0))
			{
				return true;
			}
			if (base.Card.Location == CardLocation.Grave)
			{
				base.AI.SelectPosition(CardPosition.FaceUpDefence);
				return true;
			}
			return false;
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x000A5DC4 File Offset: 0x000A3FC4
		private bool CyberDragonInfinitySummon()
		{
			this.CyberDragonInfinitySummoned = true;
			return true;
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x000A5DD0 File Offset: 0x000A3FD0
		private bool CyberDragonInfinityEffect()
		{
			if (base.Duel.CurrentChain.Count > 0)
			{
				return base.Duel.LastChainPlayer == 1;
			}
			ClientCard bestmonster = null;
			foreach (ClientCard monster in base.Enemy.GetMonsters())
			{
				if (monster.IsAttack() && (bestmonster == null || monster.Attack >= bestmonster.Attack))
				{
					bestmonster = monster;
				}
			}
			if (bestmonster != null)
			{
				base.AI.SelectCard(bestmonster);
				return true;
			}
			return false;
		}

		// Token: 0x06001BBD RID: 7101 RVA: 0x000A5E74 File Offset: 0x000A4074
		private bool Number61VolcasaurusSummon()
		{
			return base.Util.IsOneEnemyBetterThanValue(2000, false);
		}

		// Token: 0x06001BBE RID: 7102 RVA: 0x000A5E88 File Offset: 0x000A4088
		private bool Number61VolcasaurusEffect()
		{
			ClientCard target = base.Util.GetProblematicEnemyMonster(2000, false);
			if (target != null)
			{
				base.AI.SelectCard(70095154);
				base.AI.SelectNextCard(target);
				this.Number61VolcasaurusUsed = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001BBF RID: 7103 RVA: 0x000A5ED0 File Offset: 0x000A40D0
		private bool TirasKeeperOfGenesisEffect()
		{
			ClientCard target = base.Util.GetProblematicEnemyCard(0, false);
			if (target == null)
			{
				target = base.Util.GetBestEnemyCard(false, false);
			}
			if (target != null)
			{
				base.AI.SelectCard(target);
			}
			return true;
		}

		// Token: 0x06001BC0 RID: 7104 RVA: 0x000A5F0C File Offset: 0x000A410C
		private bool GaiaDragonTheThunderChargerSummon()
		{
			if (this.Number61VolcasaurusUsed && base.Bot.HasInMonstersZone(29669359, false, false, false))
			{
				base.AI.SelectCard(29669359);
				return true;
			}
			foreach (ClientCard monster in base.Bot.GetMonsters())
			{
				if (monster.HasType(CardType.Xyz) && !monster.HasXyzMaterial())
				{
					base.AI.SelectCard(monster);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001BC1 RID: 7105 RVA: 0x000A5FB4 File Offset: 0x000A41B4
		private bool XyzRebornEffect()
		{
			if (!base.UniqueFaceupSpell())
			{
				return false;
			}
			base.AI.SelectCard(new int[] { 10443957, 58069384, 31386180, 50449881, 29669359 });
			return true;
		}

		// Token: 0x06001BC2 RID: 7106 RVA: 0x000A5FE0 File Offset: 0x000A41E0
		private bool XyzUnitEffect()
		{
			foreach (ClientCard monster in base.Bot.GetMonsters())
			{
				if (monster.HasType(CardType.Xyz))
				{
					base.AI.SelectCard(monster);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001BC3 RID: 7107 RVA: 0x000A6054 File Offset: 0x000A4254
		private bool PanzerDragonEffect()
		{
			ClientCard target = base.Util.GetBestEnemyCard(false, false);
			if (target != null)
			{
				base.AI.SelectCard(target);
				return true;
			}
			return false;
		}

		// Token: 0x06001BC4 RID: 7108 RVA: 0x000A6084 File Offset: 0x000A4284
		private bool XyzVeilEffect()
		{
			if (!base.UniqueFaceupSpell())
			{
				return false;
			}
			using (List<ClientCard>.Enumerator enumerator = base.Bot.GetMonsters().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.HasType(CardType.Xyz))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001BC5 RID: 7109 RVA: 0x000A60F4 File Offset: 0x000A42F4
		private bool HaveOtherLV5OnField()
		{
			foreach (ClientCard monster in base.Bot.GetMonsters())
			{
				if (monster.HasType(CardType.Monster) && !monster.HasType(CardType.Xyz) && base.Util.GetBotAvailZonesFromExtraDeck(monster) > 0 && (monster.Level == 5 || monster.IsCode(24610207) || (monster.IsCode(12299841) && !monster.Equals(base.Card))))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04001F02 RID: 7938
		private bool NormalSummoned;

		// Token: 0x04001F03 RID: 7939
		private bool InstantFusionUsed;

		// Token: 0x04001F04 RID: 7940
		private bool DoubleSummonUsed;

		// Token: 0x04001F05 RID: 7941
		private bool CyberDragonInfinitySummoned;

		// Token: 0x04001F06 RID: 7942
		private bool Number61VolcasaurusUsed;

		// Token: 0x0200039F RID: 927
		public class CardId
		{
			// Token: 0x04001F07 RID: 7943
			public const int MistArchfiend = 28601770;

			// Token: 0x04001F08 RID: 7944
			public const int CyberDragon = 70095154;

			// Token: 0x04001F09 RID: 7945
			public const int ZWEagleClaw = 29353756;

			// Token: 0x04001F0A RID: 7946
			public const int SolarWindJammer = 33911264;

			// Token: 0x04001F0B RID: 7947
			public const int QuickdrawSynchron = 20932152;

			// Token: 0x04001F0C RID: 7948
			public const int WindUpSoldier = 12299841;

			// Token: 0x04001F0D RID: 7949
			public const int StarDrawing = 24610207;

			// Token: 0x04001F0E RID: 7950
			public const int ChronomalyGoldenJet = 88552992;

			// Token: 0x04001F0F RID: 7951
			public const int InstantFusion = 1845204;

			// Token: 0x04001F10 RID: 7952
			public const int DoubleSummon = 43422537;

			// Token: 0x04001F11 RID: 7953
			public const int MysticalSpaceTyphoon = 5318639;

			// Token: 0x04001F12 RID: 7954
			public const int BookOfMoon = 14087893;

			// Token: 0x04001F13 RID: 7955
			public const int XyzUnit = 13032689;

			// Token: 0x04001F14 RID: 7956
			public const int XyzReborn = 26708437;

			// Token: 0x04001F15 RID: 7957
			public const int MirrorForce = 44095762;

			// Token: 0x04001F16 RID: 7958
			public const int TorrentialTribute = 53582587;

			// Token: 0x04001F17 RID: 7959
			public const int XyzVeil = 96457619;

			// Token: 0x04001F18 RID: 7960
			public const int PanzerDragon = 72959823;

			// Token: 0x04001F19 RID: 7961
			public const int GaiaDragonTheThunderCharger = 91949988;

			// Token: 0x04001F1A RID: 7962
			public const int CyberDragonInfinity = 10443957;

			// Token: 0x04001F1B RID: 7963
			public const int TirasKeeperOfGenesis = 31386180;

			// Token: 0x04001F1C RID: 7964
			public const int Number61Volcasaurus = 29669359;

			// Token: 0x04001F1D RID: 7965
			public const int SharkFortress = 50449881;

			// Token: 0x04001F1E RID: 7966
			public const int CyberDragonNova = 58069384;
		}
	}
}
