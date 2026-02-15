using System;
using System.Collections.Generic;

namespace MDPro3
{
	// Token: 0x02001236 RID: 4662
	[Serializable]
	public class RarityCards
	{
		// Token: 0x060089D0 RID: 35280 RVA: 0x0010D899 File Offset: 0x0010BA99
		public RarityCards()
		{
			this.ShineCards = new List<int>();
			this.RoyalCards = new List<int>();
			this.GoldCards = new List<int>();
			this.MillenniumCards = new List<int>();
			this.BookCards = new List<int>();
		}

		// Token: 0x0400C4F7 RID: 50423
		public List<int> ShineCards;

		// Token: 0x0400C4F8 RID: 50424
		public List<int> RoyalCards;

		// Token: 0x0400C4F9 RID: 50425
		public List<int> GoldCards;

		// Token: 0x0400C4FA RID: 50426
		public List<int> MillenniumCards;

		// Token: 0x0400C4FB RID: 50427
		public List<int> BookCards;
	}
}
