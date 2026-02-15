using System;
using UnityEngine;

namespace YgomGame.DuelLive
{
	// Token: 0x02000C4E RID: 3150
	public class DuelLiveCategorySetting
	{
		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06005A09 RID: 23049 RVA: 0x0000216A File Offset: 0x0000036A
		public DuelLiveCategoryShowcaseData categoryData
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06005A0A RID: 23050 RVA: 0x0000216A File Offset: 0x0000036A
		public DuelLiveShowcaseImportData replayData
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06005A0B RID: 23051 RVA: 0x0000216D File Offset: 0x0000036D
		public void InitDuelLiveCategorySetting()
		{
		}

		// Token: 0x06005A0C RID: 23052 RVA: 0x0000216A File Offset: 0x0000036A
		public DuelLiveCategoryData GetCategoryData(int categoryId)
		{
			return null;
		}

		// Token: 0x0400959B RID: 38299
		[SerializeField]
		private DuelLiveCategoryShowcaseData m_ShowcaseData;

		// Token: 0x0400959C RID: 38300
		[SerializeField]
		private DuelLiveCategoryShowcaseData m_CategoryData;

		// Token: 0x0400959D RID: 38301
		[SerializeField]
		private DuelLiveShowcaseImportData m_replayData;
	}
}
