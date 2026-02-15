using System;
using UnityEngine;

namespace YgomGame.Shop
{
	// Token: 0x02000966 RID: 2406
	public class ShopSettings : ScriptableObject
	{
		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06004641 RID: 17985 RVA: 0x0000216A File Offset: 0x0000036A
		public ShopProductTypeSetting productTypeSetting
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06004642 RID: 17986 RVA: 0x000029CC File Offset: 0x00000BCC
		public int showcaseUnloadUnusedCnt
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06004643 RID: 17987 RVA: 0x0000216A File Offset: 0x0000036A
		public ShopCategoryShowcaseData GetShowcaseData()
		{
			return null;
		}

		// Token: 0x06004644 RID: 17988 RVA: 0x0000216A File Offset: 0x0000036A
		public ShopCategoryData GetCategoryData(int categoryId)
		{
			return null;
		}

		// Token: 0x06004645 RID: 17989 RVA: 0x0000216A File Offset: 0x0000036A
		public ShopSubCategoryData GetSubCategoryData(int categoryId, int subCategoryId)
		{
			return null;
		}

		// Token: 0x06004646 RID: 17990 RVA: 0x0000216A File Offset: 0x0000036A
		public ShopSubCategorySectionData GetSubCategorySectionData(int categoryId, int subCategoryId, ProductContext product)
		{
			return null;
		}

		// Token: 0x06004647 RID: 17991 RVA: 0x0000216A File Offset: 0x0000036A
		public ShopSubCategorySectionData GetSubCategorySectionData(int categoryId, int subCategoryId, int sectionId)
		{
			return null;
		}

		// Token: 0x06004648 RID: 17992 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool FindBoolSetting(int categoryId, int subCategoryId, ProductContext product, string key, bool defaultValue = false)
		{
			return false;
		}

		// Token: 0x06004649 RID: 17993 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool FindBoolSetting(int categoryId, int subCategoryId, int sectionId, string key, bool defaultValue = false)
		{
			return false;
		}

		// Token: 0x0600464A RID: 17994 RVA: 0x000029CC File Offset: 0x00000BCC
		public int FindIntSetting(int categoryId, int subCategoryId, ProductContext product, string key, int defaultValue = 0)
		{
			return 0;
		}

		// Token: 0x0600464B RID: 17995 RVA: 0x000029CC File Offset: 0x00000BCC
		public int FindIntSetting(int categoryId, int subCategoryId, int sectionId, string key, int defaultValue = 0)
		{
			return 0;
		}

		// Token: 0x0600464C RID: 17996 RVA: 0x0000216A File Offset: 0x0000036A
		public string FindStringSetting(int categoryId, int subCategoryId, ProductContext product, string key, string defaultValue = null)
		{
			return null;
		}

		// Token: 0x0600464D RID: 17997 RVA: 0x0000216A File Offset: 0x0000036A
		public string FindStringSetting(int categoryId, int subCategoryId, int sectionId, string key, string defaultValue = null)
		{
			return null;
		}

		// Token: 0x0600464E RID: 17998 RVA: 0x0000216A File Offset: 0x0000036A
		public ShopInformButtonData[] GetProductInfoButtonDatas(ProductContext product)
		{
			return null;
		}

		// Token: 0x040084AD RID: 33965
		internal const string k_Path = "Definition/Shop/ShopSettings";

		// Token: 0x040084AE RID: 33966
		[SerializeField]
		private ShopCategorySetting m_CategorySetting;

		// Token: 0x040084AF RID: 33967
		[SerializeField]
		private ShopProductTypeSetting m_ProductTypeSetting;

		// Token: 0x040084B0 RID: 33968
		[SerializeField]
		private ShopInformButtonSettings m_ShopInformButtonSettings;

		// Token: 0x040084B1 RID: 33969
		[SerializeField]
		private int m_ShowcaseUnloadUnusedCnt;
	}
}
