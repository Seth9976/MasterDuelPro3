using System;
using System.Collections.Generic;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x02000315 RID: 789
	[Deck("Horus", "AI_Horus", "Easy")]
	public class HorusExecutor : DefaultExecutor
	{
		// Token: 0x06001470 RID: 5232 RVA: 0x00072250 File Offset: 0x00070450
		public HorusExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.SpellSet, new Func<bool>(base.DefaultSpellSet));
			base.AddExecutor(ExecutorType.Activate, 11224103);
			base.AddExecutor(ExecutorType.Activate, 81385346, new Func<bool>(base.DefaultMysticalSpaceTyphoon));
			base.AddExecutor(ExecutorType.Activate, 5318639, new Func<bool>(base.DefaultMysticalSpaceTyphoon));
			base.AddExecutor(ExecutorType.Activate, 81439173, new Func<bool>(this.FoolishBurial));
			base.AddExecutor(ExecutorType.Activate, 50913601, new Func<bool>(base.DefaultField));
			base.AddExecutor(ExecutorType.Activate, 53129443, new Func<bool>(base.DefaultDarkHole));
			base.AddExecutor(ExecutorType.SpSummon, 70095154);
			base.AddExecutor(ExecutorType.Activate, 12580477, new Func<bool>(base.DefaultRaigeki));
			base.AddExecutor(ExecutorType.Activate, 26412047, new Func<bool>(base.DefaultHammerShot));
			base.AddExecutor(ExecutorType.Activate, 66788016);
			base.AddExecutor(ExecutorType.Activate, 80600103, new Func<bool>(this.BellowOfTheSilverDragon));
			base.AddExecutor(ExecutorType.Activate, 83764718, new Func<bool>(this.MonsterReborn));
			base.AddExecutor(ExecutorType.Summon, 79473793, new Func<bool>(this.WhiteNightDragon));
			base.AddExecutor(ExecutorType.Summon, 11224103, new Func<bool>(base.DefaultMonsterSummon));
			base.AddExecutor(ExecutorType.Summon, 43096270);
			base.AddExecutor(ExecutorType.SummonOrSet, 84914462);
			base.AddExecutor(ExecutorType.SummonOrSet, 47013502);
			base.AddExecutor(ExecutorType.MonsterSet, 9666558);
			base.AddExecutor(ExecutorType.SummonOrSet, 11091375);
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(base.DefaultMonsterRepos));
			base.AddExecutor(ExecutorType.Activate, 48229808, new Func<bool>(this.HorusTheBlackFlameDragonLv8));
			base.AddExecutor(ExecutorType.Activate, 44095762, new Func<bool>(base.DefaultTrap));
			base.AddExecutor(ExecutorType.Activate, 70342110, new Func<bool>(base.DefaultTrap));
			base.AddExecutor(ExecutorType.Activate, 20638610, new Func<bool>(this.DragonsRebirth));
		}

		// Token: 0x06001471 RID: 5233 RVA: 0x00072454 File Offset: 0x00070654
		private bool FoolishBurial()
		{
			if (base.Bot.HasInGraveyard(79473793))
			{
				return false;
			}
			if (base.Bot.HasInHand(79473793))
			{
				return false;
			}
			int remaining = 2;
			using (IEnumerator<ClientCard> enumerator = base.Bot.Banished.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsCode(79473793))
					{
						remaining--;
					}
				}
			}
			if (remaining > 0)
			{
				base.AI.SelectCard(79473793);
				return true;
			}
			return false;
		}

		// Token: 0x06001472 RID: 5234 RVA: 0x000724F0 File Offset: 0x000706F0
		private bool BellowOfTheSilverDragon()
		{
			if (base.Duel.Player == 0 && (base.Duel.Phase == DuelPhase.Draw || base.Duel.Phase == DuelPhase.Standby))
			{
				return false;
			}
			if (base.Duel.Player == 1 && base.Duel.Phase == DuelPhase.End)
			{
				return false;
			}
			List<ClientCard> cards = new List<ClientCard>(base.Bot.Graveyard);
			cards.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			for (int i = cards.Count - 1; i >= 0; i--)
			{
				ClientCard card = cards[i];
				if (card.Attack < 1000)
				{
					return false;
				}
				if (card.IsMonster() && card.HasType(CardType.Normal))
				{
					base.AI.SelectCard(card);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001473 RID: 5235 RVA: 0x000725B8 File Offset: 0x000707B8
		private bool MonsterReborn()
		{
			List<ClientCard> cards = new List<ClientCard>(base.Bot.Graveyard.GetMatchingCards((ClientCard card) => card.IsCanRevive()));
			cards.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			ClientCard selectedCard = null;
			for (int i = cards.Count - 1; i >= 0; i--)
			{
				ClientCard card3 = cards[i];
				if (card3.Attack < 1000)
				{
					break;
				}
				if (card3.IsMonster())
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
				if (card2.Attack < 1000)
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

		// Token: 0x06001474 RID: 5236 RVA: 0x000726F8 File Offset: 0x000708F8
		private bool WhiteNightDragon()
		{
			if (base.Enemy.GetMonsterCount() != 0 && !base.Util.IsAllEnemyBetterThanValue(2299, false))
			{
				using (IEnumerator<ClientCard> enumerator = base.Duel.MainPhase.SummonableCards.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.IsCode(11224103))
						{
							return false;
						}
					}
				}
			}
			return base.DefaultMonsterSummon();
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x00072780 File Offset: 0x00070980
		private bool HorusTheBlackFlameDragonLv8()
		{
			return base.Duel.LastChainPlayer == 1;
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x00072790 File Offset: 0x00070990
		private bool DragonsRebirth()
		{
			List<ClientCard> cards = new List<ClientCard>(base.Bot.GetMonsters());
			if (cards.Count == 0)
			{
				return false;
			}
			cards.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			ClientCard tributeCard = null;
			foreach (ClientCard monster in cards)
			{
				if (monster.Attack > 2000)
				{
					return false;
				}
				if (!monster.IsFacedown() && monster.Race == 8192)
				{
					tributeCard = monster;
					break;
				}
			}
			if (tributeCard == null)
			{
				return false;
			}
			cards = new List<ClientCard>(base.Bot.Hand);
			cards.AddRange(base.Bot.Graveyard);
			if (cards.Count == 0)
			{
				return false;
			}
			cards.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			ClientCard summonCard = null;
			for (int i = cards.Count - 1; i >= 0; i--)
			{
				ClientCard monster2 = cards[i];
				if (monster2.Attack < 2300)
				{
					return false;
				}
				if (monster2.Race == 8192 && !monster2.IsCode(48229808))
				{
					summonCard = monster2;
					break;
				}
			}
			if (summonCard == null)
			{
				return false;
			}
			base.AI.SelectCard(tributeCard);
			base.AI.SelectNextCard(summonCard);
			return true;
		}

		// Token: 0x02000316 RID: 790
		public class CardId
		{
			// Token: 0x040018F4 RID: 6388
			public const int AlexandriteDragon = 43096270;

			// Token: 0x040018F5 RID: 6389
			public const int LusterDragon = 11091375;

			// Token: 0x040018F6 RID: 6390
			public const int WhiteNightDragon = 79473793;

			// Token: 0x040018F7 RID: 6391
			public const int HorusTheBlackFlameDragonLv8 = 48229808;

			// Token: 0x040018F8 RID: 6392
			public const int HorusTheBlackFlameDragonLv6 = 11224103;

			// Token: 0x040018F9 RID: 6393
			public const int CyberDragon = 70095154;

			// Token: 0x040018FA RID: 6394
			public const int AxeDragonute = 84914462;

			// Token: 0x040018FB RID: 6395
			public const int DodgerDragon = 47013502;

			// Token: 0x040018FC RID: 6396
			public const int GolemDragon = 9666558;

			// Token: 0x040018FD RID: 6397
			public const int Raigeki = 12580477;

			// Token: 0x040018FE RID: 6398
			public const int HammerShot = 26412047;

			// Token: 0x040018FF RID: 6399
			public const int DarkHole = 53129443;

			// Token: 0x04001900 RID: 6400
			public const int Fissure = 66788016;

			// Token: 0x04001901 RID: 6401
			public const int StampingDestruction = 81385346;

			// Token: 0x04001902 RID: 6402
			public const int FoolishBurial = 81439173;

			// Token: 0x04001903 RID: 6403
			public const int MonsterReborn = 83764718;

			// Token: 0x04001904 RID: 6404
			public const int MysticalSpaceTyphoon = 5318639;

			// Token: 0x04001905 RID: 6405
			public const int BellowOfTheSilverDragon = 80600103;

			// Token: 0x04001906 RID: 6406
			public const int Mountain = 50913601;

			// Token: 0x04001907 RID: 6407
			public const int DragonsRebirth = 20638610;

			// Token: 0x04001908 RID: 6408
			public const int MirrorForce = 44095762;

			// Token: 0x04001909 RID: 6409
			public const int DimensionalPrison = 70342110;
		}
	}
}
