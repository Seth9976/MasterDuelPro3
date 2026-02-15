using System;
using System.Collections.Generic;
using YgomGame.Card;

namespace YgomGame.CardPack.Open
{
	// Token: 0x020010C0 RID: 4288
	public class DrawPackData
	{
		// Token: 0x0400B80A RID: 47114
		public readonly List<DrawCardData> drawCardDatas;

		// Token: 0x0400B80B RID: 47115
		public string packImagePath;

		// Token: 0x0400B80C RID: 47116
		public CardCollectionInfo.Rarity packType;

		// Token: 0x0400B80D RID: 47117
		public CardCollectionInfo.Rarity packTypeUpgrade1;

		// Token: 0x0400B80E RID: 47118
		public CardCollectionInfo.Rarity packTypeUpgrade2;

		// Token: 0x0400B80F RID: 47119
		public int thunderType;

		// Token: 0x0400B810 RID: 47120
		public int cutType;
	}
}
