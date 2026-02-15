using System;
using System.Collections.Generic;
using YgomGame.MDMarkup;

namespace YgomGame.CardPack.RateMMAData
{
	// Token: 0x020010AF RID: 4271
	public class MMAData_Header : MMAData_Text
	{
		// Token: 0x06007F00 RID: 32512 RVA: 0x000F6884 File Offset: 0x000F4A84
		public MMAData_Header()
		{
		}

		// Token: 0x06007F01 RID: 32513 RVA: 0x000F6884 File Offset: 0x000F4A84
		public MMAData_Header(Dictionary<string, object> sourceDic)
		{
		}

		// Token: 0x06007F02 RID: 32514 RVA: 0x0000216A File Offset: 0x0000036A
		public override IMDMarkupContent OutputContent()
		{
			return null;
		}

		// Token: 0x0400B79A RID: 47002
		internal new const string k_TP = "header";
	}
}
