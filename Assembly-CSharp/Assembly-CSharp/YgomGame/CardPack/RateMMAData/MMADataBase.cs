using System;
using System.Collections.Generic;
using YgomGame.MDMarkup;

namespace YgomGame.CardPack.RateMMAData
{
	// Token: 0x020010AE RID: 4270
	public abstract class MMADataBase : IMMAData
	{
		// Token: 0x06007EFD RID: 32509 RVA: 0x00002739 File Offset: 0x00000939
		public MMADataBase()
		{
		}

		// Token: 0x06007EFE RID: 32510 RVA: 0x00002739 File Offset: 0x00000939
		public MMADataBase(Dictionary<string, object> sourceDic)
		{
		}

		// Token: 0x06007EFF RID: 32511
		public abstract IMDMarkupContent OutputContent();

		// Token: 0x0400B799 RID: 47001
		public int indent;
	}
}
