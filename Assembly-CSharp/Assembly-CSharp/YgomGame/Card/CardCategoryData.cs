using System;
using System.Collections.Generic;

namespace YgomGame.Card
{
	// Token: 0x020010F2 RID: 4338
	public class CardCategoryData
	{
		// Token: 0x17001081 RID: 4225
		// (get) Token: 0x06008117 RID: 33047 RVA: 0x0000216A File Offset: 0x0000036A
		public IReadOnlyDictionary<int, CardCategoryData.Category> categoryMap
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06008118 RID: 33048 RVA: 0x0000216D File Offset: 0x0000036D
		public static void LoadAsync(Action<CardCategoryData> onComplete)
		{
		}

		// Token: 0x06008119 RID: 33049 RVA: 0x0000216D File Offset: 0x0000036D
		public void Import(List<object> snapshotDatas)
		{
		}

		// Token: 0x0400B994 RID: 47508
		private const string k_SnapshotPath = "External/CardCategory/CardCategory";

		// Token: 0x0400B995 RID: 47509
		private Dictionary<int, CardCategoryData.Category> m_CategoryMap;

		// Token: 0x020010F3 RID: 4339
		public class Category
		{
			// Token: 0x0600811B RID: 33051 RVA: 0x00002739 File Offset: 0x00000939
			public Category(int id, Dictionary<string, string> globalNameMap)
			{
			}

			// Token: 0x0600811C RID: 33052 RVA: 0x0000216A File Offset: 0x0000036A
			public string GetName()
			{
				return null;
			}

			// Token: 0x0400B996 RID: 47510
			public readonly int id;

			// Token: 0x0400B997 RID: 47511
			public readonly Dictionary<string, string> globalNameMap;
		}
	}
}
