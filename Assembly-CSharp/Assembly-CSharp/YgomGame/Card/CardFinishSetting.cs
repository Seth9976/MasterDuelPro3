using System;
using UnityEngine;

namespace YgomGame.Card
{
	// Token: 0x020010F9 RID: 4345
	public class CardFinishSetting : ScriptableObject
	{
		// Token: 0x17001083 RID: 4227
		// (get) Token: 0x0600815C RID: 33116 RVA: 0x0000216A File Offset: 0x0000036A
		protected static CardFinishSetting Instance
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600815D RID: 33117 RVA: 0x000029CC File Offset: 0x00000BCC
		public static CardFinishSetting.FinishType GetCardFisnishData(int cardid, int finishid)
		{
			return CardFinishSetting.FinishType.Normal;
		}

		// Token: 0x0400B9DA RID: 47578
		private static CardFinishSetting m_Instance;

		// Token: 0x0400B9DB RID: 47579
		private const string PATH = "Duel/ScriptableObject/CardFinishSetting";

		// Token: 0x0400B9DC RID: 47580
		[SerializeField]
		private CardFinishSetting.CardFinishData[] cardFinishData;

		// Token: 0x020010FA RID: 4346
		public enum FinishType
		{
			// Token: 0x0400B9DE RID: 47582
			Normal,
			// Token: 0x0400B9DF RID: 47583
			Shine,
			// Token: 0x0400B9E0 RID: 47584
			Royal,
			// Token: 0x0400B9E1 RID: 47585
			SP1,
			// Token: 0x0400B9E2 RID: 47586
			SP2,
			// Token: 0x0400B9E3 RID: 47587
			SP3
		}

		// Token: 0x020010FB RID: 4347
		[Serializable]
		public struct CardFinishData
		{
			// Token: 0x0400B9E4 RID: 47588
			public int cardid;

			// Token: 0x0400B9E5 RID: 47589
			public int finishid;

			// Token: 0x0400B9E6 RID: 47590
			public CardFinishSetting.FinishType finsihtype;
		}
	}
}
