using System;
using System.Collections.Generic;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x020002F5 RID: 757
	[Deck("Dragunity", "AI_Dragunity", "Normal")]
	public class DragunityExecutor : DefaultExecutor
	{
		// Token: 0x06001315 RID: 4885 RVA: 0x000669D4 File Offset: 0x00064BD4
		public DragunityExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.SpellSet, new Func<bool>(base.DefaultSpellSet));
			base.AddExecutor(ExecutorType.Activate, 5318639, new Func<bool>(base.DefaultMysticalSpaceTyphoon));
			base.AddExecutor(ExecutorType.Activate, 29863101);
			base.AddExecutor(ExecutorType.Activate, 70368879);
			base.AddExecutor(ExecutorType.Activate, 62265044, new Func<bool>(this.DragonRavineField));
			base.AddExecutor(ExecutorType.Activate, 73628505, new Func<bool>(this.Terraforming));
			base.AddExecutor(ExecutorType.Activate, 81439173, new Func<bool>(this.FoolishBurial));
			base.AddExecutor(ExecutorType.Activate, 83764718, new Func<bool>(this.MonsterReborn));
			base.AddExecutor(ExecutorType.Activate, 76774528, new Func<bool>(this.ScrapDragonEffect));
			base.AddExecutor(ExecutorType.Activate, 50954680, new Func<bool>(this.CrystalWingSynchroDragonEffect));
			base.AddExecutor(ExecutorType.Activate, 59755122);
			base.AddExecutor(ExecutorType.Activate, 21249921);
			base.AddExecutor(ExecutorType.Activate, 876330, new Func<bool>(this.DragunityArmaMysletainnEffect));
			base.AddExecutor(ExecutorType.Activate, 28183605);
			base.AddExecutor(ExecutorType.Activate, 71490127, new Func<bool>(this.DragonsMirror));
			base.AddExecutor(ExecutorType.SpSummon, 76774528, new Func<bool>(this.ScrapDragonSummon));
			base.AddExecutor(ExecutorType.SpSummon, 50954680, new Func<bool>(this.CrystalWingSynchroDragonSummon));
			base.AddExecutor(ExecutorType.SpSummon, 44508094);
			base.AddExecutor(ExecutorType.SpSummon, 21249921);
			base.AddExecutor(ExecutorType.SpSummon, 34116027);
			base.AddExecutor(ExecutorType.Summon, 59755122, new Func<bool>(this.DragunityPhalanxSummon));
			base.AddExecutor(ExecutorType.SpSummon, 876330, new Func<bool>(this.DragunityArmaMysletainn));
			base.AddExecutor(ExecutorType.Summon, 876330, new Func<bool>(this.DragunityArmaMysletainnTribute));
			base.AddExecutor(ExecutorType.Activate, 39701395);
			base.AddExecutor(ExecutorType.Activate, 62265044, new Func<bool>(this.DragonRavineEffect));
			base.AddExecutor(ExecutorType.Activate, 57103969, new Func<bool>(this.FireFormationTenki));
			base.AddExecutor(ExecutorType.Activate, 60004971);
			base.AddExecutor(ExecutorType.Summon, 28183605, new Func<bool>(this.DragunityDux));
			base.AddExecutor(ExecutorType.MonsterSet, 59755122, new Func<bool>(this.DragunityPhalanxSet));
			base.AddExecutor(ExecutorType.SummonOrSet, 3431737);
			base.AddExecutor(ExecutorType.Activate, 3431737, new Func<bool>(this.AssaultBeast));
			base.AddExecutor(ExecutorType.SpellSet, 71490127, new Func<bool>(this.SetUselessCards));
			base.AddExecutor(ExecutorType.SpellSet, 73628505, new Func<bool>(this.SetUselessCards));
			base.AddExecutor(ExecutorType.SpellSet, 29863101, new Func<bool>(this.SetUselessCards));
			base.AddExecutor(ExecutorType.SpellSet, 39701395, new Func<bool>(this.SetUselessCards));
			base.AddExecutor(ExecutorType.Activate, 61257789, new Func<bool>(base.DefaultStardustDragonEffect));
			base.AddExecutor(ExecutorType.Activate, 44508094, new Func<bool>(base.DefaultStardustDragonEffect));
			base.AddExecutor(ExecutorType.Activate, 58120309, new Func<bool>(base.DefaultTrap));
			base.AddExecutor(ExecutorType.Activate, 44095762, new Func<bool>(base.DefaultTrap));
			base.AddExecutor(ExecutorType.Activate, 70342110, new Func<bool>(base.DefaultTrap));
			base.AddExecutor(ExecutorType.Activate, 80280737, new Func<bool>(this.AssaultModeActivate));
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(base.DefaultMonsterRepos));
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x00066D4B File Offset: 0x00064F4B
		private bool DragonRavineField()
		{
			return base.Card.Location == CardLocation.Hand && base.DefaultField();
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x00066D64 File Offset: 0x00064F64
		private bool DragonRavineEffect()
		{
			if (base.Card.Location != CardLocation.SpellZone)
			{
				return false;
			}
			int tributeId = -1;
			if (base.Bot.HasInHand(59755122))
			{
				tributeId = 59755122;
			}
			else if (base.Bot.HasInHand(57103969))
			{
				tributeId = 57103969;
			}
			else if (base.Bot.HasInHand(73628505))
			{
				tributeId = 73628505;
			}
			else if (base.Bot.HasInHand(62265044))
			{
				tributeId = 62265044;
			}
			else if (base.Bot.HasInHand(29863101))
			{
				tributeId = 29863101;
			}
			else if (base.Bot.HasInHand(3431737))
			{
				tributeId = 3431737;
			}
			else if (base.Bot.HasInHand(876330))
			{
				tributeId = 876330;
			}
			else
			{
				int count = 0;
				using (IEnumerator<ClientCard> enumerator = base.Bot.Hand.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.IsCode(28183605))
						{
							count++;
						}
					}
				}
				if (count >= 2)
				{
					tributeId = 28183605;
				}
			}
			if (tributeId == -1 && base.Bot.HasInHand(61257789))
			{
				tributeId = 61257789;
			}
			if (tributeId == -1 && base.Bot.HasInHand(60004971))
			{
				tributeId = 61257789;
			}
			if (tributeId == -1 && base.Bot.HasInHand(71490127) && base.Bot.GetMonsterCount() == 0)
			{
				tributeId = 61257789;
			}
			if (tributeId == -1)
			{
				return false;
			}
			int needId = -1;
			if (!base.Bot.HasInMonstersZone(59755122, false, false, false) && !base.Bot.HasInGraveyard(59755122))
			{
				needId = 59755122;
			}
			else if (base.Bot.GetMonsterCount() == 0)
			{
				needId = 28183605;
			}
			else
			{
				needId = 28183605;
			}
			if (needId == -1)
			{
				return false;
			}
			if (tributeId == 59755122)
			{
				needId = 28183605;
			}
			int remaining = 3;
			using (IEnumerator<ClientCard> enumerator = base.Bot.Hand.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsCode(needId))
					{
						remaining--;
					}
				}
			}
			using (IEnumerator<ClientCard> enumerator = base.Bot.Graveyard.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsCode(needId))
					{
						remaining--;
					}
				}
			}
			using (IEnumerator<ClientCard> enumerator = base.Bot.Banished.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsCode(needId))
					{
						remaining--;
					}
				}
			}
			if (remaining <= 0)
			{
				return false;
			}
			int option;
			if (needId == 59755122)
			{
				option = 2;
			}
			else
			{
				option = 1;
			}
			if (base.ActivateDescription != base.Util.GetStringId(62265044, option))
			{
				return false;
			}
			base.AI.SelectCard(tributeId);
			base.AI.SelectNextCard(needId);
			return true;
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x0006709C File Offset: 0x0006529C
		private bool Terraforming()
		{
			return !base.Bot.HasInHand(62265044) && base.Bot.SpellZone[5] == null;
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x000670C4 File Offset: 0x000652C4
		private bool SetUselessCards()
		{
			ClientField field = base.Bot;
			return !field.HasInSpellZone(57103969, false, false) && !field.HasInSpellZone(29863101, false, false) && !field.HasInSpellZone(39701395, false, false) && !field.HasInSpellZone(71490127, false, false) && base.Bot.GetSpellCountWithoutField() < 4;
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x0006712A File Offset: 0x0006532A
		private bool FireFormationTenki()
		{
			return base.Card.Location != CardLocation.Hand || base.Bot.GetSpellCountWithoutField() < 4;
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x0006714A File Offset: 0x0006534A
		private bool FoolishBurial()
		{
			base.AI.SelectCard(new int[] { 59755122, 3431737, 61257789 });
			return true;
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x0006716C File Offset: 0x0006536C
		private bool MonsterReborn()
		{
			List<ClientCard> cards = new List<ClientCard>(base.Bot.Graveyard.GetMatchingCards((ClientCard card) => card.IsCanRevive()));
			cards.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			ClientCard selectedCard = null;
			for (int i = cards.Count - 1; i >= 0; i--)
			{
				ClientCard card3 = cards[i];
				if (card3.Attack < 2000)
				{
					break;
				}
				if (!card3.IsCode(new int[] { 61257789, 99267150 }) && card3.IsMonster())
				{
					selectedCard = card3;
					break;
				}
			}
			cards = new List<ClientCard>(base.Enemy.Graveyard.GetMatchingCards((ClientCard card) => card.IsCanRevive()));
			cards.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			for (int j = cards.Count - 1; j >= 0; j--)
			{
				ClientCard card2 = cards[j];
				if (card2.Attack < 2000)
				{
					break;
				}
				if (card2.IsMonster() && card2.HasType(CardType.Normal) && (selectedCard == null || card2.Attack > selectedCard.Attack))
				{
					selectedCard = card2;
					break;
				}
			}
			if (selectedCard != null)
			{
				base.AI.SelectCard(selectedCard);
				return true;
			}
			return false;
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x000672C8 File Offset: 0x000654C8
		private bool DragonsMirror()
		{
			IList<ClientCard> tributes = new List<ClientCard>();
			int phalanxCount = 0;
			foreach (ClientCard card in base.Bot.Graveyard)
			{
				if (card.IsCode(59755122))
				{
					phalanxCount++;
					break;
				}
				if (card.Race == 8192)
				{
					tributes.Add(card);
				}
				if (tributes.Count == 5)
				{
					break;
				}
			}
			if (tributes.Count < 5 && phalanxCount > 1)
			{
				foreach (ClientCard card2 in base.Bot.Graveyard)
				{
					if (card2.IsCode(59755122))
					{
						phalanxCount--;
						tributes.Add(card2);
						if (phalanxCount <= 1)
						{
							break;
						}
					}
				}
			}
			if (tributes.Count < 5)
			{
				return false;
			}
			base.AI.SelectCard(99267150);
			base.AI.SelectNextCard(tributes);
			return true;
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x000673E0 File Offset: 0x000655E0
		private bool ScrapDragonSummon()
		{
			return base.Util.GetProblematicEnemyCard(3000, false) != null;
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x000673F8 File Offset: 0x000655F8
		private bool ScrapDragonEffect()
		{
			ClientCard invincible = base.Util.GetProblematicEnemyCard(3000, false);
			if (invincible == null && !base.Util.IsOneEnemyBetterThanValue(2799, false))
			{
				return false;
			}
			int tributeId = -1;
			if (base.Bot.HasInSpellZone(57103969, false, false))
			{
				tributeId = 57103969;
			}
			else if (base.Bot.HasInSpellZone(73628505, false, false))
			{
				tributeId = 73628505;
			}
			else if (base.Bot.HasInSpellZone(71490127, false, false))
			{
				tributeId = 71490127;
			}
			else if (base.Bot.HasInSpellZone(39701395, false, false))
			{
				tributeId = 39701395;
			}
			else if (base.Bot.HasInSpellZone(29863101, false, false))
			{
				tributeId = 29863101;
			}
			else if (base.Bot.HasInSpellZone(80280737, false, false))
			{
				tributeId = 80280737;
			}
			else if (base.Bot.HasInSpellZone(62265044, false, false))
			{
				tributeId = 62265044;
			}
			List<ClientCard> monsters = base.Enemy.GetMonsters();
			monsters.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			ClientCard destroyCard = invincible;
			if (destroyCard == null)
			{
				for (int i = monsters.Count - 1; i >= 0; i--)
				{
					if (monsters[i].IsAttack())
					{
						destroyCard = monsters[i];
						break;
					}
				}
			}
			if (destroyCard == null)
			{
				return false;
			}
			base.AI.SelectCard(tributeId);
			base.AI.SelectNextCard(destroyCard);
			return true;
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x0006756B File Offset: 0x0006576B
		private bool CrystalWingSynchroDragonSummon()
		{
			return !base.Bot.HasInHand(80280737) && !base.Bot.HasInHand(3431737) && !base.Bot.HasInSpellZone(80280737, false, false);
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x000675A8 File Offset: 0x000657A8
		private bool CrystalWingSynchroDragonEffect()
		{
			return base.Duel.LastChainPlayer != 0;
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x000675B8 File Offset: 0x000657B8
		private bool DragunityPhalanxSummon()
		{
			return base.Bot.HasInHand(876330);
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x000675CC File Offset: 0x000657CC
		private bool DragunityArmaMysletainn()
		{
			if (base.Bot.HasInMonstersZone(59755122, false, false, false))
			{
				base.AI.SelectCard(59755122);
				return true;
			}
			if (base.Bot.HasInMonstersZone(28183605, false, false, false))
			{
				base.AI.SelectCard(28183605);
				return true;
			}
			return false;
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x00067628 File Offset: 0x00065828
		private bool DragunityArmaMysletainnEffect()
		{
			base.AI.SelectCard(59755122);
			return true;
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x0006763C File Offset: 0x0006583C
		private bool DragunityArmaMysletainnTribute()
		{
			if ((base.Bot.HasInMonstersZone(3431737, false, false, false) && base.Bot.HasInGraveyard(59755122)) || base.Bot.HasInMonstersZone(59755122, false, false, false) || base.Bot.HasInHand(60004971))
			{
				List<ClientCard> monsters = base.Bot.GetMonsters();
				monsters.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				using (List<ClientCard>.Enumerator enumerator = monsters.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						ClientCard monster = enumerator.Current;
						base.AI.SelectMaterials(monster, 0);
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x00067704 File Offset: 0x00065904
		private bool DragunityDux()
		{
			return base.Bot.HasInGraveyard(59755122) || (base.Bot.GetMonsterCount() == 0 && base.Bot.HasInHand(876330)) || base.Bot.HasInHand(60004971);
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x00067756 File Offset: 0x00065956
		private bool DragunityPhalanxSet()
		{
			return base.Bot.GetMonsterCount() == 0 || !base.Bot.HasInGraveyard(59755122);
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x0006777A File Offset: 0x0006597A
		private bool AssaultBeast()
		{
			return !base.DefaultCheckWhetherCardIsNegated(base.Card) && !base.Bot.HasInSpellZone(80280737, false, false);
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x000677A4 File Offset: 0x000659A4
		private bool AssaultModeActivate()
		{
			if (base.Duel.Player == 0 && base.Duel.Phase == DuelPhase.BattleStart)
			{
				foreach (ClientCard monster in base.Bot.GetMonsters())
				{
					if (monster.IsCode(44508094) && monster.Attacked)
					{
						base.AI.SelectCard(monster);
						return true;
					}
				}
			}
			return base.Duel.Player == 1;
		}

		// Token: 0x020002F6 RID: 758
		public class CardId
		{
			// Token: 0x040017AC RID: 6060
			public const int StardustDragonAssaultMode = 61257789;

			// Token: 0x040017AD RID: 6061
			public const int DragunityArmaMysletainn = 876330;

			// Token: 0x040017AE RID: 6062
			public const int AssaultBeast = 3431737;

			// Token: 0x040017AF RID: 6063
			public const int DragunityDux = 28183605;

			// Token: 0x040017B0 RID: 6064
			public const int DragunityPhalanx = 59755122;

			// Token: 0x040017B1 RID: 6065
			public const int AssaultTeleport = 29863101;

			// Token: 0x040017B2 RID: 6066
			public const int CardsOfConsonance = 39701395;

			// Token: 0x040017B3 RID: 6067
			public const int UpstartGoblin = 70368879;

			// Token: 0x040017B4 RID: 6068
			public const int DragonsMirror = 71490127;

			// Token: 0x040017B5 RID: 6069
			public const int Terraforming = 73628505;

			// Token: 0x040017B6 RID: 6070
			public const int FoolishBurial = 81439173;

			// Token: 0x040017B7 RID: 6071
			public const int MonsterReborn = 83764718;

			// Token: 0x040017B8 RID: 6072
			public const int MysticalSpaceTyphoon = 5318639;

			// Token: 0x040017B9 RID: 6073
			public const int FireFormationTenki = 57103969;

			// Token: 0x040017BA RID: 6074
			public const int DragunitySpearOfDestiny = 60004971;

			// Token: 0x040017BB RID: 6075
			public const int DragonRavine = 62265044;

			// Token: 0x040017BC RID: 6076
			public const int MirrorForce = 44095762;

			// Token: 0x040017BD RID: 6077
			public const int StarlightRoad = 58120309;

			// Token: 0x040017BE RID: 6078
			public const int DimensionalPrison = 70342110;

			// Token: 0x040017BF RID: 6079
			public const int AssaultModeActivate = 80280737;

			// Token: 0x040017C0 RID: 6080
			public const int FiveHeadedDragon = 99267150;

			// Token: 0x040017C1 RID: 6081
			public const int CrystalWingSynchroDragon = 50954680;

			// Token: 0x040017C2 RID: 6082
			public const int ScrapDragon = 76774528;

			// Token: 0x040017C3 RID: 6083
			public const int StardustDragon = 44508094;

			// Token: 0x040017C4 RID: 6084
			public const int DragunityKnightGaeDearg = 34116027;

			// Token: 0x040017C5 RID: 6085
			public const int DragunityKnightVajrayana = 21249921;
		}
	}
}
