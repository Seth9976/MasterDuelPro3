using System;
using UnityEngine;

namespace YgomGame.Card
{
	// Token: 0x0200110C RID: 4364
	public class CardPictureFontSetting : ScriptableObject
	{
		// Token: 0x0400BA4C RID: 47692
		public string language;

		// Token: 0x0400BA4D RID: 47693
		public CardPictureFontSetting.CardidFontSizePairData[] m_CardidNormalFontSizePairDatas;

		// Token: 0x0400BA4E RID: 47694
		public CardPictureFontSetting.CardidFontSizePairData[] m_CardidPendulumFontSizePairDatas;

		// Token: 0x0200110D RID: 4365
		[Serializable]
		public struct CardidFontSizePairData
		{
			// Token: 0x0400BA4F RID: 47695
			public short cardid;

			// Token: 0x0400BA50 RID: 47696
			public float fontsize;
		}
	}
}
