using System;
using YgomGame.Utility;

namespace YgomGame.Shop
{
	// Token: 0x02000969 RID: 2409
	[Serializable]
	public class ShopSubCategorySectionData : ShopProductGroupData
	{
		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06004655 RID: 18005 RVA: 0x0000216A File Offset: 0x0000036A
		public override string labelText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004656 RID: 18006 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool IsMatchProduct(ProductContext product)
		{
			return false;
		}

		// Token: 0x040084B9 RID: 33977
		public ItemUtil.Category itemCategory;
	}
}
