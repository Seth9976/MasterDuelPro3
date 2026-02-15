using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YgomGame.Menu.Common;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.GemShop
{
	// Token: 0x02000C04 RID: 3076
	public class ProductWidget : ElementWidgetBase
	{
		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x0600574C RID: 22348 RVA: 0x0000216A File Offset: 0x0000036A
		public TMP_Text productName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x0600574D RID: 22349 RVA: 0x0000216A File Offset: 0x0000036A
		public TMP_Text priceLabel
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x0600574E RID: 22350 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject doubleNotationPriceGroup
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x0600574F RID: 22351 RVA: 0x0000216A File Offset: 0x0000036A
		public TMP_Text doubleNotationPriceLabel
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06005750 RID: 22352 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingGemShopIcon gemShopIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06005751 RID: 22353 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject limitCountRoot
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06005752 RID: 22354 RVA: 0x0000216A File Offset: 0x0000036A
		public TMP_Text limitCountText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06005753 RID: 22355 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject limitDateRoot
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06005754 RID: 22356 RVA: 0x0000216A File Offset: 0x0000036A
		public TMP_Text limitDateText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06005755 RID: 22357 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionButton button
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06005756 RID: 22358 RVA: 0x0000216A File Offset: 0x0000036A
		public IReadOnlyDictionary<int, ProductWidget.ItemWidget> itemWidgetMap
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06005757 RID: 22359 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public ProductWidget(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x06005758 RID: 22360 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetStyle(ProductStyle productStyle)
		{
		}

		// Token: 0x06005759 RID: 22361 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClick()
		{
		}

		// Token: 0x0400940F RID: 37903
		private readonly string k_ELabelProductName;

		// Token: 0x04009410 RID: 37904
		private readonly string k_ELabelPriceLabel;

		// Token: 0x04009411 RID: 37905
		private readonly string k_ELabelDoubleNotationPriceGroup;

		// Token: 0x04009412 RID: 37906
		private readonly string k_ELabelDoubleNotationPriceLabel;

		// Token: 0x04009413 RID: 37907
		private readonly string k_ELabelGemShopIcon;

		// Token: 0x04009414 RID: 37908
		private readonly string k_ELabelPaidGemItem;

		// Token: 0x04009415 RID: 37909
		private readonly string k_ELabelFreeGemItem;

		// Token: 0x04009416 RID: 37910
		private readonly string k_ELabelLimitRoot;

		// Token: 0x04009417 RID: 37911
		private readonly string k_ELabelLimitText;

		// Token: 0x04009418 RID: 37912
		private readonly string k_ELabelLimitDateRoot;

		// Token: 0x04009419 RID: 37913
		private readonly string k_ELabelLimitDateText;

		// Token: 0x0400941A RID: 37914
		private readonly string k_ELabelButton;

		// Token: 0x0400941B RID: 37915
		private readonly string k_ELabelTweenDefault;

		// Token: 0x0400941C RID: 37916
		private readonly string k_ELabelTweenHighlight;

		// Token: 0x0400941D RID: 37917
		private Dictionary<int, ProductWidget.ItemWidget> m_ItemWidgetMap;

		// Token: 0x0400941E RID: 37918
		public int idx;

		// Token: 0x0400941F RID: 37919
		public Action<ProductWidget> onClickCallback;

		// Token: 0x02000C05 RID: 3077
		public class ItemWidget : ElementWidgetBase
		{
			// Token: 0x17000890 RID: 2192
			// (get) Token: 0x0600575A RID: 22362 RVA: 0x0000216A File Offset: 0x0000036A
			public TMP_Text itemNumText
			{
				get
				{
					return null;
				}
			}

			// Token: 0x0600575B RID: 22363 RVA: 0x000F2C76 File Offset: 0x000F0E76
			public ItemWidget(ElementObjectManager eom)
				: base(null)
			{
			}

			// Token: 0x04009420 RID: 37920
			private readonly string k_ELabelItemNumText;
		}
	}
}
