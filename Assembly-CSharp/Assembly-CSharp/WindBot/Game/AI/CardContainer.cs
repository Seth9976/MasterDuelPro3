using System;
using System.Collections.Generic;
using System.Linq;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI
{
	// Token: 0x02000214 RID: 532
	public static class CardContainer
	{
		// Token: 0x06000B38 RID: 2872 RVA: 0x00031E29 File Offset: 0x00030029
		public static int CompareCardAttack(ClientCard cardA, ClientCard cardB)
		{
			if (cardA.Attack < cardB.Attack)
			{
				return -1;
			}
			if (cardA.Attack == cardB.Attack)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x00031E4C File Offset: 0x0003004C
		public static int CompareCardLevel(ClientCard cardA, ClientCard cardB)
		{
			if (cardA.Level < cardB.Level)
			{
				return -1;
			}
			if (cardA.Level == cardB.Level)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x00031E6F File Offset: 0x0003006F
		public static int CompareCardLink(ClientCard cardA, ClientCard cardB)
		{
			if (cardA.LinkCount < cardB.LinkCount)
			{
				return -1;
			}
			if (cardA.LinkCount == cardB.LinkCount)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x00031E94 File Offset: 0x00030094
		public static int CompareDefensePower(ClientCard cardA, ClientCard cardB)
		{
			if (cardA == null && cardB == null)
			{
				return 0;
			}
			if (cardA == null)
			{
				return -1;
			}
			if (cardB == null)
			{
				return 1;
			}
			int powerA = cardA.GetDefensePower();
			int powerB = cardB.GetDefensePower();
			if (powerA < powerB)
			{
				return -1;
			}
			if (powerA == powerB)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x00031ED0 File Offset: 0x000300D0
		public static ClientCard GetHighestAttackMonster(this IEnumerable<ClientCard> cards, bool canBeTarget = false)
		{
			return (from card in cards
				where ((card != null) ? card.Data : null) != null && card.HasType(CardType.Monster) && card.IsFaceup() && (!canBeTarget || !card.IsShouldNotBeTarget())
				orderby card.Attack descending
				select card).FirstOrDefault<ClientCard>();
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x00031F28 File Offset: 0x00030128
		public static ClientCard GetHighestDefenseMonster(this IEnumerable<ClientCard> cards, bool canBeTarget = false)
		{
			return (from card in cards
				where ((card != null) ? card.Data : null) != null && card.HasType(CardType.Monster) && card.IsFaceup() && (!canBeTarget || !card.IsShouldNotBeTarget())
				orderby card.Defense descending
				select card).FirstOrDefault<ClientCard>();
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x00031F80 File Offset: 0x00030180
		public static ClientCard GetLowestAttackMonster(this IEnumerable<ClientCard> cards, bool canBeTarget = false)
		{
			return (from card in cards
				where ((card != null) ? card.Data : null) != null && card.HasType(CardType.Monster) && card.IsFaceup() && (!canBeTarget || !card.IsShouldNotBeTarget())
				orderby card.Attack
				select card).FirstOrDefault<ClientCard>();
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x00031FD8 File Offset: 0x000301D8
		public static ClientCard GetLowestDefenseMonster(this IEnumerable<ClientCard> cards, bool canBeTarget = false)
		{
			return (from card in cards
				where ((card != null) ? card.Data : null) != null && card.HasType(CardType.Monster) && card.IsFaceup() && (!canBeTarget || !card.IsShouldNotBeTarget())
				orderby card.Defense
				select card).FirstOrDefault<ClientCard>();
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x00032030 File Offset: 0x00030230
		public static bool ContainsMonsterWithLevel(this IEnumerable<ClientCard> cards, int level)
		{
			return cards.Where((ClientCard card) => ((card != null) ? card.Data : null) != null).Any((ClientCard card) => !card.HasType(CardType.Xyz) && card.Level == level);
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x00032080 File Offset: 0x00030280
		public static bool ContainsMonsterWithRank(this IEnumerable<ClientCard> cards, int rank)
		{
			return cards.Where((ClientCard card) => ((card != null) ? card.Data : null) != null).Any((ClientCard card) => card.HasType(CardType.Xyz) && card.Rank == rank);
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x000320D0 File Offset: 0x000302D0
		public static bool ContainsCardWithId(this IEnumerable<ClientCard> cards, int id)
		{
			return cards.Where((ClientCard card) => ((card != null) ? card.Data : null) != null).Any((ClientCard card) => card.IsCode(id));
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x00032120 File Offset: 0x00030320
		public static int GetCardCount(this IEnumerable<ClientCard> cards, int id)
		{
			return cards.Where((ClientCard card) => ((card != null) ? card.Data : null) != null).Count((ClientCard card) => card.IsCode(id));
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x00032170 File Offset: 0x00030370
		public static List<ClientCard> GetMonsters(this IEnumerable<ClientCard> cards)
		{
			return cards.Where((ClientCard card) => ((card != null) ? card.Data : null) != null && card.HasType(CardType.Monster)).ToList<ClientCard>();
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x0003219C File Offset: 0x0003039C
		public static List<ClientCard> GetFaceupPendulumMonsters(this IEnumerable<ClientCard> cards)
		{
			return cards.Where((ClientCard card) => ((card != null) ? card.Data : null) != null && card.HasType(CardType.Monster) && card.IsFaceup() && card.HasType(CardType.Pendulum)).ToList<ClientCard>();
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x000321C8 File Offset: 0x000303C8
		public static ClientCard GetInvincibleMonster(this IEnumerable<ClientCard> cards, bool canBeTarget = false)
		{
			return cards.FirstOrDefault((ClientCard card) => ((card != null) ? card.Data : null) != null && card.IsMonsterInvincible() && card.IsFaceup() && (!canBeTarget || !card.IsShouldNotBeTarget()));
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x000321F4 File Offset: 0x000303F4
		public static ClientCard GetDangerousMonster(this IEnumerable<ClientCard> cards, bool canBeTarget = false)
		{
			return cards.FirstOrDefault((ClientCard card) => ((card != null) ? card.Data : null) != null && card.IsMonsterDangerous() && card.IsFaceup() && (!canBeTarget || !card.IsShouldNotBeTarget()));
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x00032220 File Offset: 0x00030420
		public static ClientCard GetFloodgate(this IEnumerable<ClientCard> cards, bool canBeTarget = false)
		{
			return cards.FirstOrDefault((ClientCard card) => ((card != null) ? card.Data : null) != null && card.IsFloodgate() && card.IsFaceup() && (!canBeTarget || !card.IsShouldNotBeTarget()));
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x0003224C File Offset: 0x0003044C
		public static ClientCard GetFirstMatchingCard(this IEnumerable<ClientCard> cards, Func<ClientCard, bool> filter)
		{
			return cards.FirstOrDefault((ClientCard card) => ((card != null) ? card.Data : null) != null && filter(card));
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x00032278 File Offset: 0x00030478
		public static ClientCard GetFirstMatchingFaceupCard(this IEnumerable<ClientCard> cards, Func<ClientCard, bool> filter)
		{
			return cards.FirstOrDefault((ClientCard card) => ((card != null) ? card.Data : null) != null && card.IsFaceup() && filter(card));
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x000322A4 File Offset: 0x000304A4
		public static IList<ClientCard> GetMatchingCards(this IEnumerable<ClientCard> cards, Func<ClientCard, bool> filter)
		{
			return cards.Where((ClientCard card) => ((card != null) ? card.Data : null) != null && filter(card)).ToList<ClientCard>();
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x000322D8 File Offset: 0x000304D8
		public static int GetMatchingCardsCount(this IEnumerable<ClientCard> cards, Func<ClientCard, bool> filter)
		{
			return cards.Count((ClientCard card) => ((card != null) ? card.Data : null) != null && filter(card));
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x00032304 File Offset: 0x00030504
		public static bool IsExistingMatchingCard(this IEnumerable<ClientCard> cards, Func<ClientCard, bool> filter, int count = 1)
		{
			return cards.GetMatchingCardsCount(filter) >= count;
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x00032314 File Offset: 0x00030514
		public static ClientCard GetShouldBeDisabledBeforeItUseEffectMonster(this IEnumerable<ClientCard> cards, bool canBeTarget = true)
		{
			return cards.FirstOrDefault((ClientCard card) => ((card != null) ? card.Data : null) != null && card.IsMonsterShouldBeDisabledBeforeItUseEffect() && card.IsFaceup() && (!canBeTarget || !card.IsShouldNotBeTarget()));
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x00032340 File Offset: 0x00030540
		public static IEnumerable<IEnumerable<T>> GetCombinations<T>(this IEnumerable<T> elements, int k)
		{
			if (k != 0)
			{
				return elements.SelectMany((T e, int i) => from c in elements.Skip(i + 1).GetCombinations(k - 1)
					select new T[] { e }.Concat(c));
			}
			return new T[][] { new T[0] };
		}
	}
}
