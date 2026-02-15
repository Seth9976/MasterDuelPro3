using System;
using System.Collections.Generic;
using YgomGame.Card;

namespace YgomGame.Deck
{
	// Token: 0x02000FB3 RID: 4019
	public static class CardData
	{
		// Token: 0x17000ECA RID: 3786
		// (get) Token: 0x0600778C RID: 30604 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsValid
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000ECB RID: 3787
		// (get) Token: 0x0600778D RID: 30605 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsReady
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600778E RID: 30606 RVA: 0x0000216D File Offset: 0x0000036D
		public static void AddRef()
		{
		}

		// Token: 0x0600778F RID: 30607 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Release()
		{
		}

		// Token: 0x06007790 RID: 30608 RVA: 0x000029CC File Offset: 0x00000BCC
		public static Content.Frame GetFrame(int mrk)
		{
			return Content.Frame.Normal;
		}

		// Token: 0x06007791 RID: 30609 RVA: 0x000029CC File Offset: 0x00000BCC
		public static Content.Attribute GetAttr(int mrk)
		{
			return Content.Attribute.Null;
		}

		// Token: 0x06007792 RID: 30610 RVA: 0x000029CC File Offset: 0x00000BCC
		public static Content.Type GetType(int mrk)
		{
			return Content.Type.Null;
		}

		// Token: 0x06007793 RID: 30611 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetStar(int mrk)
		{
			return 0;
		}

		// Token: 0x06007794 RID: 30612 RVA: 0x000029CC File Offset: 0x00000BCC
		public static Content.Icon GetIcon(int mrk)
		{
			return Content.Icon.Null;
		}

		// Token: 0x06007795 RID: 30613 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetOriginalID2(int mrk)
		{
			return 0;
		}

		// Token: 0x06007796 RID: 30614 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsMainDeck(int mrk)
		{
			return false;
		}

		// Token: 0x06007797 RID: 30615 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsMainDeck(Content.Frame f, int mrk = -1)
		{
			return false;
		}

		// Token: 0x06007798 RID: 30616 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsMonster(int mrk)
		{
			return false;
		}

		// Token: 0x06007799 RID: 30617 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsMonster(Content.Frame f, int mrk = -1)
		{
			return false;
		}

		// Token: 0x0600779A RID: 30618 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool HasLevel(int mrk)
		{
			return false;
		}

		// Token: 0x0600779B RID: 30619 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool HasLevel(Content.Frame f, int mrk = -1)
		{
			return false;
		}

		// Token: 0x0600779C RID: 30620 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsValidCard(int mrk)
		{
			return false;
		}

		// Token: 0x0600779D RID: 30621 RVA: 0x000029CC File Offset: 0x00000BCC
		public static CardData.INFOKIND GetInfoKind(int mrk)
		{
			return CardData.INFOKIND.Monster;
		}

		// Token: 0x0600779E RID: 30622 RVA: 0x000029CC File Offset: 0x00000BCC
		public static CardData.INFOKIND GetInfoKind(Content.Frame f, int mrk = -1)
		{
			return CardData.INFOKIND.Monster;
		}

		// Token: 0x0600779F RID: 30623 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<int> GetAllCardList()
		{
			return null;
		}

		// Token: 0x0400B014 RID: 45076
		private static int refCount;

		// Token: 0x0400B015 RID: 45077
		private static bool created;

		// Token: 0x0400B016 RID: 45078
		private static List<int> allCardList;

		// Token: 0x0400B017 RID: 45079
		private static bool allCardListReq;

		// Token: 0x02000FB4 RID: 4020
		public enum INFOKIND
		{
			// Token: 0x0400B019 RID: 45081
			Monster,
			// Token: 0x0400B01A RID: 45082
			Spell,
			// Token: 0x0400B01B RID: 45083
			Trap,
			// Token: 0x0400B01C RID: 45084
			ExMon,
			// Token: 0x0400B01D RID: 45085
			Error
		}
	}
}
