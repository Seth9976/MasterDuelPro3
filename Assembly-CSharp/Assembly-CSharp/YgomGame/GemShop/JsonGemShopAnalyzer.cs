using System;
using System.Collections.Generic;

namespace YgomGame.GemShop
{
	// Token: 0x02000C00 RID: 3072
	public class JsonGemShopAnalyzer
	{
		// Token: 0x040093FF RID: 37887
		public JsonGemShopAnalyzer.ProductAnalyzer product;

		// Token: 0x02000C01 RID: 3073
		public class ProductAnalyzer
		{
			// Token: 0x06005735 RID: 22325 RVA: 0x0000216A File Offset: 0x0000036A
			public string GetProductNameId(object productData)
			{
				return null;
			}

			// Token: 0x06005736 RID: 22326 RVA: 0x0000216A File Offset: 0x0000036A
			public string CreateProductName(object productData, int gemCount)
			{
				return null;
			}

			// Token: 0x06005737 RID: 22327 RVA: 0x000F1669 File Offset: 0x000EF869
			public long GetShopPaidId(object productData)
			{
				return 0L;
			}

			// Token: 0x06005738 RID: 22328 RVA: 0x0000216A File Offset: 0x0000036A
			public string GetProductId(object productData)
			{
				return null;
			}

			// Token: 0x06005739 RID: 22329 RVA: 0x000029CC File Offset: 0x00000BCC
			public int GetConfirmRegId(object productData)
			{
				return 0;
			}

			// Token: 0x0600573A RID: 22330 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool GetEnabled(object productData)
			{
				return false;
			}

			// Token: 0x0600573B RID: 22331 RVA: 0x0000216A File Offset: 0x0000036A
			public IReadOnlyDictionary<string, object> GetItems(object productData)
			{
				return null;
			}

			// Token: 0x0600573C RID: 22332 RVA: 0x0000216A File Offset: 0x0000036A
			public string GetPriceLabel(object productData)
			{
				return null;
			}

			// Token: 0x0600573D RID: 22333 RVA: 0x000029CC File Offset: 0x00000BCC
			public int GetOrder(object productData)
			{
				return 0;
			}

			// Token: 0x0600573E RID: 22334 RVA: 0x000029CC File Offset: 0x00000BCC
			public int GetLimitBuyCount(object productData)
			{
				return 0;
			}

			// Token: 0x0600573F RID: 22335 RVA: 0x000F1669 File Offset: 0x000EF869
			public long GetEndDateTs(object productData)
			{
				return 0L;
			}

			// Token: 0x06005740 RID: 22336 RVA: 0x0000216A File Offset: 0x0000036A
			public string GetLimitdate(object productData)
			{
				return null;
			}

			// Token: 0x06005741 RID: 22337 RVA: 0x000029CC File Offset: 0x00000BCC
			public int GetBuyCount(object productData)
			{
				return 0;
			}

			// Token: 0x06005742 RID: 22338 RVA: 0x000029CC File Offset: 0x00000BCC
			public int GetThumbId(object productData)
			{
				return 0;
			}

			// Token: 0x06005743 RID: 22339 RVA: 0x000029CC File Offset: 0x00000BCC
			public ProductStyle GetProductStyle(object productData)
			{
				return ProductStyle.Default;
			}
		}
	}
}
