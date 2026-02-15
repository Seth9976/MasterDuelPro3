using System;
using System.Collections.Generic;

namespace YgomGame.DuelLive
{
	// Token: 0x02000C60 RID: 3168
	public interface IProductContextCollection
	{
		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06005A80 RID: 23168
		// (set) Token: 0x06005A81 RID: 23169
		int filterSubId { get; set; }

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06005A82 RID: 23170
		List<IDuelLiveProductGruopData> lockedGroups { get; }

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x06005A83 RID: 23171
		bool isDisplayEmpty { get; }

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06005A84 RID: 23172
		List<int> subCategories { get; }

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06005A85 RID: 23173
		IProductContext Item { get; }

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06005A86 RID: 23174
		int Count { get; }

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06005A87 RID: 23175
		IReadOnlyList<IProductContext> importedContexts { get; }

		// Token: 0x14000096 RID: 150
		// (add) Token: 0x06005A88 RID: 23176
		// (remove) Token: 0x06005A89 RID: 23177
		event Action onUpdatedEvent;

		// Token: 0x06005A8A RID: 23178
		bool IsEmpty();

		// Token: 0x06005A8B RID: 23179
		void Import(Dictionary<string, object> productDatas);

		// Token: 0x06005A8C RID: 23180
		void Filter();
	}
}
