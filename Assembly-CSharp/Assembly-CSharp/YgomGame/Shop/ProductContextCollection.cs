using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using YgomGame.Card;

namespace YgomGame.Shop
{
	// Token: 0x0200093B RID: 2363
	public class ProductContextCollection : List<ProductContext>
	{
		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x0600452B RID: 17707 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x0600452C RID: 17708 RVA: 0x0000216D File Offset: 0x0000036D
		public string filterProductName
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x0600452D RID: 17709 RVA: 0x0000216A File Offset: 0x0000036A
		public List<int> subCategories
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x0600452E RID: 17710 RVA: 0x0000216A File Offset: 0x0000036A
		public IReadOnlyList<ProductContext> importedContexts
		{
			get
			{
				return null;
			}
		}

		// Token: 0x14000055 RID: 85
		// (add) Token: 0x0600452F RID: 17711 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06004530 RID: 17712 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action onUpdatedEvent
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x06004531 RID: 17713 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsEmpty()
		{
			return false;
		}

		// Token: 0x06004532 RID: 17714 RVA: 0x000F4846 File Offset: 0x000F2A46
		public ProductContextCollection(ShopDef.ShowcaseCategory category, ShopSettings shopSettings, CardCategoryData cardCategoryData)
		{
		}

		// Token: 0x06004533 RID: 17715 RVA: 0x0000216D File Offset: 0x0000036D
		public void Import(Dictionary<string, object> productDatas)
		{
		}

		// Token: 0x06004534 RID: 17716 RVA: 0x0000216D File Offset: 0x0000036D
		public void Filter()
		{
		}

		// Token: 0x06004535 RID: 17717 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int SortAsPack(ProductContext a, ProductContext b)
		{
			return 0;
		}

		// Token: 0x06004536 RID: 17718 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int SortAsStructure(ProductContext a, ProductContext b)
		{
			return 0;
		}

		// Token: 0x06004537 RID: 17719 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int SortAsAccessory(ProductContext a, ProductContext b)
		{
			return 0;
		}

		// Token: 0x06004538 RID: 17720 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int SortAsSpecial(ProductContext a, ProductContext b)
		{
			return 0;
		}

		// Token: 0x0400833C RID: 33596
		public readonly ShopSettings m_ShopSettings;

		// Token: 0x0400833D RID: 33597
		public readonly CardCategoryData m_CardCategoryData;

		// Token: 0x0400833E RID: 33598
		public readonly int categoryId;

		// Token: 0x0400833F RID: 33599
		public readonly ShopDef.ShowcaseCategory category;

		// Token: 0x04008340 RID: 33600
		public readonly List<ProductContext> m_ImportedContexts;

		// Token: 0x04008341 RID: 33601
		private readonly List<int> m_ImportedSubCategories;
	}
}
