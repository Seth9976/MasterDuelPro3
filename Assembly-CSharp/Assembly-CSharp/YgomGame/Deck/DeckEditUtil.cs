using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace YgomGame.Deck
{
	// Token: 0x02000FCC RID: 4044
	public static class DeckEditUtil
	{
		// Token: 0x17000F5D RID: 3933
		// (get) Token: 0x06007901 RID: 30977 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06007902 RID: 30978 RVA: 0x0000216D File Offset: 0x0000036D
		public static int selectorPriorityBase
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x06007903 RID: 30979 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetSelectorPriority(int priority)
		{
			return 0;
		}

		// Token: 0x06007904 RID: 30980 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetSelectorPriority(DeckEditUtil.SelectorPriority priority)
		{
			return 0;
		}

		// Token: 0x06007905 RID: 30981 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SortCardDataList(List<CardBaseData> list)
		{
		}

		// Token: 0x06007906 RID: 30982 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SortCardDataList(List<CardBaseData> list, SortComparer.Sorter sorter)
		{
		}

		// Token: 0x06007907 RID: 30983 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsDifferentCardList(List<CardBaseData> deckA, List<CardBaseData> deckB)
		{
			return false;
		}

		// Token: 0x0400B0D1 RID: 45265
		private static SortComparer.Sorter deckSorter;

		// Token: 0x02000FCD RID: 4045
		public enum SelectorPriority
		{
			// Token: 0x0400B0D3 RID: 45267
			DeckEditor,
			// Token: 0x0400B0D4 RID: 45268
			ActionMenu,
			// Token: 0x0400B0D5 RID: 45269
			CardDetail
		}
	}
}
