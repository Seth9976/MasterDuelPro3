using System;
using UnityEngine;

namespace YgomGame.Shop
{
	// Token: 0x0200092F RID: 2351
	public interface IProductListWidgetListener
	{
		// Token: 0x0600448D RID: 17549
		void OnProductListScrolled(Vector2 value);

		// Token: 0x0600448E RID: 17550
		void OnFocusProductLine(ProductContext product);

		// Token: 0x0600448F RID: 17551
		void OnClickProduct(ProductWidget productWidget);
	}
}
