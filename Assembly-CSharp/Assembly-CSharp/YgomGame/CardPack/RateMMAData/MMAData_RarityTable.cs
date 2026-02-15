using System;
using System.Collections.Generic;
using YgomGame.MDMarkup;

namespace YgomGame.CardPack.RateMMAData
{
	// Token: 0x020010B2 RID: 4274
	public class MMAData_RarityTable : MMADataBase
	{
		// Token: 0x06007F07 RID: 32519 RVA: 0x000F688C File Offset: 0x000F4A8C
		public MMAData_RarityTable()
		{
		}

		// Token: 0x06007F08 RID: 32520 RVA: 0x000F688C File Offset: 0x000F4A8C
		public MMAData_RarityTable(Dictionary<string, object> sourceDic)
		{
		}

		// Token: 0x06007F09 RID: 32521 RVA: 0x0000216A File Offset: 0x0000036A
		public override IMDMarkupContent OutputContent()
		{
			return null;
		}

		// Token: 0x0400B79F RID: 47007
		internal const string k_TP = "rarityTable";

		// Token: 0x0400B7A0 RID: 47008
		public MMAData_RarityTable.Data[] datas;

		// Token: 0x020010B3 RID: 4275
		[Serializable]
		public class Data
		{
			// Token: 0x0400B7A1 RID: 47009
			public int rare;

			// Token: 0x0400B7A2 RID: 47010
			public string rate;

			// Token: 0x0400B7A3 RID: 47011
			public int num;
		}
	}
}
