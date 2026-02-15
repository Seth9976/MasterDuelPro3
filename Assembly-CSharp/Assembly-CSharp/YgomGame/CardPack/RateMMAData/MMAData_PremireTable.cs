using System;
using System.Collections.Generic;
using YgomGame.MDMarkup;

namespace YgomGame.CardPack.RateMMAData
{
	// Token: 0x020010B0 RID: 4272
	public class MMAData_PremireTable : MMADataBase
	{
		// Token: 0x06007F03 RID: 32515 RVA: 0x000F688C File Offset: 0x000F4A8C
		public MMAData_PremireTable()
		{
		}

		// Token: 0x06007F04 RID: 32516 RVA: 0x000F688C File Offset: 0x000F4A8C
		public MMAData_PremireTable(Dictionary<string, object> sourceDic)
		{
		}

		// Token: 0x06007F05 RID: 32517 RVA: 0x0000216A File Offset: 0x0000036A
		public override IMDMarkupContent OutputContent()
		{
			return null;
		}

		// Token: 0x0400B79B RID: 47003
		internal const string k_TP = "premiereTable";

		// Token: 0x0400B79C RID: 47004
		public MMAData_PremireTable.Data[] datas;

		// Token: 0x020010B1 RID: 4273
		public class Data
		{
			// Token: 0x0400B79D RID: 47005
			public int premire;

			// Token: 0x0400B79E RID: 47006
			public string rate;
		}
	}
}
