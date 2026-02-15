using System;
using System.Collections.Generic;
using YgomGame.MDMarkup;

namespace YgomGame.CardPack.RateMMAData
{
	// Token: 0x020010B4 RID: 4276
	public class MMAData_Text : MMADataBase
	{
		// Token: 0x06007F0B RID: 32523 RVA: 0x000F688C File Offset: 0x000F4A8C
		public MMAData_Text()
		{
		}

		// Token: 0x06007F0C RID: 32524 RVA: 0x000F688C File Offset: 0x000F4A8C
		public MMAData_Text(Dictionary<string, object> sourceDic)
		{
		}

		// Token: 0x06007F0D RID: 32525 RVA: 0x0000216A File Offset: 0x0000036A
		public override IMDMarkupContent OutputContent()
		{
			return null;
		}

		// Token: 0x06007F0E RID: 32526 RVA: 0x0000216A File Offset: 0x0000036A
		protected string OutputText()
		{
			return null;
		}

		// Token: 0x0400B7A4 RID: 47012
		internal const string k_TP = "text";

		// Token: 0x0400B7A5 RID: 47013
		public MMAData_Text.Data[] datas;

		// Token: 0x020010B5 RID: 4277
		public class Data
		{
			// Token: 0x06007F0F RID: 32527 RVA: 0x00002739 File Offset: 0x00000939
			public Data()
			{
			}

			// Token: 0x06007F10 RID: 32528 RVA: 0x00002739 File Offset: 0x00000939
			public Data(Dictionary<string, object> sourceDic)
			{
			}

			// Token: 0x06007F11 RID: 32529 RVA: 0x0000216A File Offset: 0x0000036A
			public string OutputText()
			{
				return null;
			}

			// Token: 0x0400B7A6 RID: 47014
			public string textId;

			// Token: 0x0400B7A7 RID: 47015
			public object[] args;
		}
	}
}
