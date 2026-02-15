using System;

namespace YgomGame.Shop
{
	// Token: 0x0200094F RID: 2383
	[Serializable]
	public class ShopCategoryData : ShopProductGroupTreeData<ShopSubCategoryData>
	{
		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06004608 RID: 17928 RVA: 0x000029CC File Offset: 0x00000BCC
		public ShopDef.ShowcaseCategory category
		{
			get
			{
				return (ShopDef.ShowcaseCategory)0;
			}
		}

		// Token: 0x06004609 RID: 17929 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool IsMatchProduct(ProductContext product)
		{
			return false;
		}
	}
}
