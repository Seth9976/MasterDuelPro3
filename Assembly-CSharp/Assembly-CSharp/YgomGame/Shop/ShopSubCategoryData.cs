using System;
using YgomGame.Utility;

namespace YgomGame.Shop
{
	// Token: 0x02000968 RID: 2408
	[Serializable]
	public class ShopSubCategoryData : ShopProductGroupTreeData<ShopSubCategorySectionData>
	{
		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06004651 RID: 18001 RVA: 0x0000216A File Offset: 0x0000036A
		public override string labelText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004652 RID: 18002 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetProductLabel(ProductContext product)
		{
			return null;
		}

		// Token: 0x06004653 RID: 18003 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool IsMatchProduct(ProductContext product)
		{
			return false;
		}

		// Token: 0x040084B8 RID: 33976
		public ItemUtil.Category itemCategory;
	}
}
