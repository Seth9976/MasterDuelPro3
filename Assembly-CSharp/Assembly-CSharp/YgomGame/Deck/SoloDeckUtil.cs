using System;
using System.Collections.Generic;

namespace YgomGame.Deck
{
	// Token: 0x02001005 RID: 4101
	public class SoloDeckUtil
	{
		// Token: 0x06007B76 RID: 31606 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetStoryDeckID(int chapterId)
		{
			return 0;
		}

		// Token: 0x06007B77 RID: 31607 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetStoryDeckName(int chapterId)
		{
			return null;
		}

		// Token: 0x06007B78 RID: 31608 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetStoryDeckDesc(int chapterId)
		{
			return null;
		}

		// Token: 0x06007B79 RID: 31609 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetStoryDeck(int chapterId, DeckInfo.DeckType deckType)
		{
			return null;
		}

		// Token: 0x06007B7A RID: 31610 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetNPCDeckID(int chapterId)
		{
			return 0;
		}

		// Token: 0x06007B7B RID: 31611 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetNPCDeckName(int chapterId)
		{
			return null;
		}

		// Token: 0x06007B7C RID: 31612 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetNPCDeckDesc(int chapterId)
		{
			return null;
		}

		// Token: 0x06007B7D RID: 31613 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetNPCDeck(int chapterId, DeckInfo.DeckType deckType)
		{
			return null;
		}

		// Token: 0x02001006 RID: 4102
		public enum SoloDeckType
		{
			// Token: 0x0400B31B RID: 45851
			Story,
			// Token: 0x0400B31C RID: 45852
			NPC
		}
	}
}
