using System;
using System.Collections.Generic;
using System.Linq;
using YGOSharp.OCGWrapper;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x02000354 RID: 852
	[Deck("Lucky", "AI_Test", "Test")]
	public class LuckyExecutor : DefaultExecutor
	{
		// Token: 0x06001719 RID: 5913 RVA: 0x00089AF8 File Offset: 0x00087CF8
		public LuckyExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.Activate, new Func<bool>(this.ImFeelingLucky));
			base.AddExecutor(ExecutorType.SpSummon, new Func<bool>(this.ImFeelingLucky));
			base.AddExecutor(ExecutorType.SpSummon, new Func<bool>(this.ImFeelingUnlucky));
			base.AddExecutor(ExecutorType.Activate, new Func<bool>(this.ImFeelingUnlucky));
			base.AddExecutor(ExecutorType.SummonOrSet, new Func<bool>(this.ImFeelingLazy));
			base.AddExecutor(ExecutorType.SpellSet, new Func<bool>(base.DefaultSpellSet));
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(base.DefaultMonsterRepos));
			base.AddExecutor(ExecutorType.Activate, 5318639, new Func<bool>(base.DefaultMysticalSpaceTyphoon));
			base.AddExecutor(ExecutorType.Activate, 8267140, new Func<bool>(base.DefaultCosmicCyclone));
			base.AddExecutor(ExecutorType.Activate, 5133471, new Func<bool>(base.DefaultGalaxyCyclone));
			base.AddExecutor(ExecutorType.Activate, 14087893, new Func<bool>(base.DefaultBookOfMoon));
			base.AddExecutor(ExecutorType.Activate, 94192409, new Func<bool>(base.DefaultCompulsoryEvacuationDevice));
			base.AddExecutor(ExecutorType.Activate, 97077563, new Func<bool>(base.DefaultCallOfTheHaunted));
			base.AddExecutor(ExecutorType.Activate, 73915051, new Func<bool>(base.DefaultScapegoat));
			base.AddExecutor(ExecutorType.Activate, 23434538, new Func<bool>(base.DefaultMaxxC));
			base.AddExecutor(ExecutorType.Activate, 14558127, new Func<bool>(base.DefaultAshBlossomAndJoyousSpring));
			base.AddExecutor(ExecutorType.Activate, 59438930, new Func<bool>(base.DefaultGhostOgreAndSnowRabbit));
			base.AddExecutor(ExecutorType.Activate, 73642296, new Func<bool>(base.DefaultGhostBelleAndHauntedMansion));
			base.AddExecutor(ExecutorType.Activate, 97268402, new Func<bool>(base.DefaultEffectVeiler));
			base.AddExecutor(ExecutorType.Activate, 24224830, new Func<bool>(base.DefaultCalledByTheGrave));
			base.AddExecutor(ExecutorType.Activate, 10045474, new Func<bool>(base.DefaultInfiniteImpermanence));
			base.AddExecutor(ExecutorType.Activate, 78474168, new Func<bool>(base.DefaultBreakthroughSkill));
			base.AddExecutor(ExecutorType.Activate, 41420027, new Func<bool>(base.DefaultSolemnJudgment));
			base.AddExecutor(ExecutorType.Activate, 84749824, new Func<bool>(base.DefaultSolemnWarning));
			base.AddExecutor(ExecutorType.Activate, 40605147, new Func<bool>(base.DefaultSolemnStrike));
			base.AddExecutor(ExecutorType.Activate, 53582587, new Func<bool>(base.DefaultTorrentialTribute));
			base.AddExecutor(ExecutorType.Activate, 19613556, new Func<bool>(base.DefaultHeavyStorm));
			base.AddExecutor(ExecutorType.Activate, 18144506, new Func<bool>(base.DefaultHarpiesFeatherDusterFirst));
			base.AddExecutor(ExecutorType.Activate, 26412047, new Func<bool>(base.DefaultHammerShot));
			base.AddExecutor(ExecutorType.Activate, 53129443, new Func<bool>(base.DefaultDarkHole));
			base.AddExecutor(ExecutorType.Activate, 12580477, new Func<bool>(base.DefaultRaigeki));
			base.AddExecutor(ExecutorType.Activate, 97169186, new Func<bool>(base.DefaultSmashingGround));
			base.AddExecutor(ExecutorType.Activate, 35261759, new Func<bool>(base.DefaultPotOfDesires));
			base.AddExecutor(ExecutorType.Activate, 1475311, new Func<bool>(base.DefaultAllureofDarkness));
			base.AddExecutor(ExecutorType.Activate, 83326048, new Func<bool>(base.DefaultDimensionalBarrier));
			base.AddExecutor(ExecutorType.Activate, 99330325, new Func<bool>(base.DefaultInterruptedKaijuSlumber));
			base.AddExecutor(ExecutorType.SpSummon, 63941210, new Func<bool>(base.DefaultKaijuSpsummon));
			base.AddExecutor(ExecutorType.SpSummon, 36956512, new Func<bool>(base.DefaultKaijuSpsummon));
			base.AddExecutor(ExecutorType.SpSummon, 55063751, new Func<bool>(base.DefaultKaijuSpsummon));
			base.AddExecutor(ExecutorType.SpSummon, 28674152, new Func<bool>(base.DefaultKaijuSpsummon));
			base.AddExecutor(ExecutorType.SpSummon, 29726552, new Func<bool>(base.DefaultKaijuSpsummon));
			base.AddExecutor(ExecutorType.SpSummon, 48770333, new Func<bool>(base.DefaultKaijuSpsummon));
			base.AddExecutor(ExecutorType.SpSummon, 93332803, new Func<bool>(base.DefaultKaijuSpsummon));
			base.AddExecutor(ExecutorType.SpSummon, 84769941, new Func<bool>(base.DefaultKaijuSpsummon));
			base.AddExecutor(ExecutorType.SpSummon, 46772449, new Func<bool>(base.DefaultEvilswarmExcitonKnightSummon));
			base.AddExecutor(ExecutorType.Activate, 46772449, new Func<bool>(base.DefaultEvilswarmExcitonKnightEffect));
			base.AddExecutor(ExecutorType.Summon, 33015627, new Func<bool>(base.DefaultTimelordSummon));
			base.AddExecutor(ExecutorType.Summon, 6616912, new Func<bool>(base.DefaultTimelordSummon));
			base.AddExecutor(ExecutorType.Summon, 7733560, new Func<bool>(base.DefaultTimelordSummon));
			base.AddExecutor(ExecutorType.Summon, 28929131, new Func<bool>(base.DefaultTimelordSummon));
			base.AddExecutor(ExecutorType.Summon, 34137269, new Func<bool>(base.DefaultTimelordSummon));
			base.AddExecutor(ExecutorType.Summon, 60222213, new Func<bool>(base.DefaultTimelordSummon));
			base.AddExecutor(ExecutorType.Summon, 65314286, new Func<bool>(base.DefaultTimelordSummon));
			base.AddExecutor(ExecutorType.Summon, 74530899, new Func<bool>(base.DefaultTimelordSummon));
			base.AddExecutor(ExecutorType.Summon, 91712985, new Func<bool>(base.DefaultTimelordSummon));
			base.AddExecutor(ExecutorType.Summon, 92435533, new Func<bool>(base.DefaultTimelordSummon));
			base.AddExecutor(ExecutorType.Summon, 7902349, new Func<bool>(this.JustDontIt));
			base.AddExecutor(ExecutorType.Summon, 8124921, new Func<bool>(this.JustDontIt));
			base.AddExecutor(ExecutorType.Summon, 44519536, new Func<bool>(this.JustDontIt));
			base.AddExecutor(ExecutorType.Summon, 70903634, new Func<bool>(this.JustDontIt));
			base.AddExecutor(ExecutorType.Summon, 33396948, new Func<bool>(this.JustDontIt));
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x0008A204 File Offset: 0x00088404
		public override IList<ClientCard> OnSelectCard(IList<ClientCard> _cards, int min, int max, int hint, bool cancelable)
		{
			if (base.Duel.Phase == DuelPhase.BattleStart)
			{
				return null;
			}
			if (base.AI.HaveSelectedCards())
			{
				return null;
			}
			IList<ClientCard> selected = new List<ClientCard>();
			IList<ClientCard> cards = new List<ClientCard>(_cards);
			if (max > cards.Count)
			{
				max = cards.Count;
			}
			if (this.HintMsgForEnemy.Contains(hint))
			{
				IList<ClientCard> enemyCards = cards.Where((ClientCard card) => card.Controller == 1).ToList<ClientCard>();
				while (enemyCards.Count > 0 && selected.Count < max)
				{
					ClientCard card7 = enemyCards[Program.Rand.Next(enemyCards.Count)];
					selected.Add(card7);
					enemyCards.Remove(card7);
					cards.Remove(card7);
				}
			}
			if (this.HintMsgForDeck.Contains(hint))
			{
				IList<ClientCard> deckCards = cards.Where((ClientCard card) => card.Location == CardLocation.Deck).ToList<ClientCard>();
				while (deckCards.Count > 0 && selected.Count < max)
				{
					ClientCard card2 = deckCards[Program.Rand.Next(deckCards.Count)];
					selected.Add(card2);
					deckCards.Remove(card2);
					cards.Remove(card2);
				}
			}
			if (this.HintMsgForSelf.Contains(hint))
			{
				IList<ClientCard> botCards = cards.Where((ClientCard card) => card.Controller == 0).ToList<ClientCard>();
				while (botCards.Count > 0 && selected.Count < max)
				{
					ClientCard card3 = botCards[Program.Rand.Next(botCards.Count)];
					selected.Add(card3);
					botCards.Remove(card3);
					cards.Remove(card3);
				}
			}
			if (this.HintMsgForMaterial.Contains(hint))
			{
				IList<ClientCard> materials = cards.OrderBy((ClientCard card) => card.Attack).ToList<ClientCard>();
				while (materials.Count > 0)
				{
					if (selected.Count >= min)
					{
						break;
					}
					ClientCard card4 = materials[0];
					selected.Add(card4);
					materials.Remove(card4);
					cards.Remove(card4);
				}
			}
			while (selected.Count < min)
			{
				ClientCard card5 = cards[Program.Rand.Next(cards.Count)];
				selected.Add(card5);
				cards.Remove(card5);
			}
			if (this.HintMsgForMaxSelect.Contains(hint))
			{
				while (selected.Count < max)
				{
					ClientCard card6 = cards[Program.Rand.Next(cards.Count)];
					selected.Add(card6);
					cards.Remove(card6);
				}
			}
			return selected;
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x0005E120 File Offset: 0x0005C320
		public override int OnSelectOption(IList<int> options)
		{
			return Program.Rand.Next(options.Count);
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x0008A4C4 File Offset: 0x000886C4
		public override CardPosition OnSelectPosition(int cardId, IList<CardPosition> positions)
		{
			NamedCard cardData = NamedCard.Get(cardId);
			if (cardData != null)
			{
				if (cardData.Attack < 0)
				{
					return CardPosition.FaceUpAttack;
				}
				if (cardData.Attack <= 1000)
				{
					return CardPosition.FaceUpDefence;
				}
			}
			return (CardPosition)0;
		}

		// Token: 0x0600171D RID: 5917 RVA: 0x0008A4F6 File Offset: 0x000886F6
		private bool ImFeelingLucky()
		{
			return (base.Type != ExecutorType.Activate || !base.DefaultCheckWhetherCardIsNegated(base.Card)) && Program.Rand.Next(10) >= 5 && base.DefaultDontChainMyself();
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x0008A528 File Offset: 0x00088728
		private bool ImFeelingUnlucky()
		{
			return (base.Type != ExecutorType.Activate || !base.DefaultCheckWhetherCardIsNegated(base.Card)) && base.DefaultDontChainMyself();
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x0008A549 File Offset: 0x00088749
		private bool ImFeelingLazy()
		{
			return !base.Executors.Any((CardExecutor exec) => (exec.Type == ExecutorType.SummonOrSet || exec.Type == ExecutorType.Summon || exec.Type == ExecutorType.MonsterSet) && exec.CardId == base.Card.Id) && base.DefaultMonsterSummon();
		}

		// Token: 0x06001720 RID: 5920 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool JustDontIt()
		{
			return false;
		}

		// Token: 0x04001B3F RID: 6975
		private List<int> HintMsgForEnemy = new List<int>
		{
			500, 502, 503, 504, 505, 507, 511, 512, 513, 533,
			573
		};

		// Token: 0x04001B40 RID: 6976
		private List<int> HintMsgForDeck = new List<int> { 509, 504, 503, 506, 511 };

		// Token: 0x04001B41 RID: 6977
		private List<int> HintMsgForSelf = new List<int> { 518 };

		// Token: 0x04001B42 RID: 6978
		private List<int> HintMsgForMaterial = new List<int> { 511, 512, 513, 533, 500 };

		// Token: 0x04001B43 RID: 6979
		private List<int> HintMsgForMaxSelect = new List<int> { 509, 504, 506, 511, 502 };
	}
}
