using System;

namespace YgomGame.Shop
{
	// Token: 0x02000931 RID: 2353
	public interface IShopProductGruopData
	{
		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06004492 RID: 17554
		int groupId { get; }

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06004493 RID: 17555
		string labelTextId { get; }

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06004494 RID: 17556
		string labelText { get; }

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06004495 RID: 17557
		bool constant { get; }

		// Token: 0x06004496 RID: 17558
		bool IsMatchProduct(ProductContext product);

		// Token: 0x06004497 RID: 17559
		int FindIntData(string key, int defaultValue = 0);

		// Token: 0x06004498 RID: 17560
		string FindStringData(string key, string defaultValue = null);
	}
}
