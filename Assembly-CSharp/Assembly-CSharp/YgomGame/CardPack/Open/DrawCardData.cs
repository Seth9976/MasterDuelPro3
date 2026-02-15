using System;
using YgomGame.Card;
using YgomSystem.ElementSystem;

namespace YgomGame.CardPack.Open
{
	// Token: 0x020010BF RID: 4287
	public class DrawCardData
	{
		// Token: 0x1700100B RID: 4107
		// (get) Token: 0x06007F67 RID: 32615 RVA: 0x000029CC File Offset: 0x00000BCC
		public CardCollectionInfo.Rarity rarity
		{
			get
			{
				return (CardCollectionInfo.Rarity)0;
			}
		}

		// Token: 0x0400B802 RID: 47106
		public int mrk;

		// Token: 0x0400B803 RID: 47107
		public int premium;

		// Token: 0x0400B804 RID: 47108
		public int locatePos;

		// Token: 0x0400B805 RID: 47109
		public bool isNew;

		// Token: 0x0400B806 RID: 47110
		public int foundSecretNum;

		// Token: 0x0400B807 RID: 47111
		public ElementObjectManager cardPref;

		// Token: 0x0400B808 RID: 47112
		public bool isVisibleRarityFrame;

		// Token: 0x0400B809 RID: 47113
		public CardCollectionInfo.Rarity backSideRarity;
	}
}
