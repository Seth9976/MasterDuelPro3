using System;
using UnityEngine;

namespace YgomGame.Shop
{
	// Token: 0x02000950 RID: 2384
	public class ShopCategorySetting : ScriptableObject
	{
		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x0600460B RID: 17931 RVA: 0x0000216A File Offset: 0x0000036A
		public ShopCategoryShowcaseData showcaseData
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600460C RID: 17932 RVA: 0x0000216A File Offset: 0x0000036A
		public ShopCategoryData GetCategoryData(int categoryId)
		{
			return null;
		}

		// Token: 0x0400843C RID: 33852
		[SerializeField]
		private ShopCategoryShowcaseData m_ShowcaseData;
	}
}
