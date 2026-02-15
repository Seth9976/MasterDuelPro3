using System;
using System.Collections.Generic;

namespace YgomGame.DuelLive
{
	// Token: 0x02000C5F RID: 3167
	public interface IProductContext
	{
		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06005A71 RID: 23153
		int menuId { get; }

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06005A72 RID: 23154
		int replayIdx { get; }

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06005A73 RID: 23155
		long duelLiveId { get; }

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x06005A74 RID: 23156
		int categoryId { get; }

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x06005A75 RID: 23157
		int categoryIdx { get; }

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x06005A76 RID: 23158
		int subCategoryId { get; }

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06005A77 RID: 23159
		int subCategoryIdx { get; }

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06005A78 RID: 23160
		int sectionId { get; }

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06005A79 RID: 23161
		int widgetType { get; }

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06005A7A RID: 23162
		List<object> mrk { get; }

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06005A7B RID: 23163
		string imagePath { get; }

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06005A7C RID: 23164
		string name1 { get; }

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06005A7D RID: 23165
		string name2 { get; }

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06005A7E RID: 23166
		int sort { get; }

		// Token: 0x06005A7F RID: 23167
		void Import(Dictionary<string, object> productData);
	}
}
