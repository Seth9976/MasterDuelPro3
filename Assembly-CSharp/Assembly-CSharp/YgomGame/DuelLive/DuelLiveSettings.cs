using System;
using UnityEngine;

namespace YgomGame.DuelLive
{
	// Token: 0x02000C58 RID: 3160
	public class DuelLiveSettings
	{
		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06005A39 RID: 23097 RVA: 0x0000216A File Offset: 0x0000036A
		public DuelLiveCategoryShowcaseData categoryData
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06005A3A RID: 23098 RVA: 0x0000216A File Offset: 0x0000036A
		public DuelLiveShowcaseImportData replayData
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06005A3B RID: 23099 RVA: 0x0000216A File Offset: 0x0000036A
		public string[] bgMonsterImgPaths
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06005A3C RID: 23100 RVA: 0x0000216D File Offset: 0x0000036D
		public void InitDuelLiveSetting()
		{
		}

		// Token: 0x06005A3D RID: 23101 RVA: 0x0000216A File Offset: 0x0000036A
		public IDuelLiveProductGruopData GetProductGroupData(IProductContext product)
		{
			return null;
		}

		// Token: 0x06005A3E RID: 23102 RVA: 0x0000216A File Offset: 0x0000036A
		public DuelLiveCategoryData GetCategoryData(int categoryId)
		{
			return null;
		}

		// Token: 0x06005A3F RID: 23103 RVA: 0x0000216A File Offset: 0x0000036A
		public DuelLiveSubCategoryData GetSubCategoryData(IProductContext product)
		{
			return null;
		}

		// Token: 0x06005A40 RID: 23104 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetCategoryLabel(int categoryId)
		{
			return null;
		}

		// Token: 0x040095BC RID: 38332
		internal const string k_Path = "Definition/DuelLive/DuelLiveSettings";

		// Token: 0x040095BD RID: 38333
		[SerializeField]
		private DuelLiveCategorySetting m_CategorySetting;

		// Token: 0x040095BE RID: 38334
		[SerializeField]
		private string[] m_BGMonsterImgPaths;
	}
}
