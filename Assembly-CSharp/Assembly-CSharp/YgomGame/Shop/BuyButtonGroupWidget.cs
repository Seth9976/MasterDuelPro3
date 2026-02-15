using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YgomGame.Utility;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Shop
{
	// Token: 0x0200091F RID: 2335
	public class BuyButtonGroupWidget : ElementWidgetBase
	{
		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06004413 RID: 17427 RVA: 0x0000216A File Offset: 0x0000036A
		public List<BuyButtonGroupWidget.BuyButtonWidget> buttonWidgets
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004414 RID: 17428 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public BuyButtonGroupWidget(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x06004415 RID: 17429 RVA: 0x0000216D File Offset: 0x0000036D
		public void Binding(ProductContext productData, TextGroupLoadHolder textGroupLoadHolder)
		{
		}

		// Token: 0x06004416 RID: 17430 RVA: 0x0000216A File Offset: 0x0000036A
		private BuyButtonGroupWidget.BuyButtonWidget InsertButton(int buttonType, int priceId)
		{
			return null;
		}

		// Token: 0x040082C8 RID: 33480
		private readonly string k_ELabelBuyButtonTemplate;

		// Token: 0x040082C9 RID: 33481
		private readonly string k_ELabelBuyButtonSRTemplate;

		// Token: 0x040082CA RID: 33482
		private readonly string k_ELabelBuyButtonURTemplate;

		// Token: 0x040082CB RID: 33483
		private const int k_ButtonStyleNormal = 1;

		// Token: 0x040082CC RID: 33484
		private const int k_ButtonStyleSR = 2;

		// Token: 0x040082CD RID: 33485
		private const int k_ButtonStyleUR = 3;

		// Token: 0x040082CE RID: 33486
		private readonly ElementObjectManager m_BuyButtonTemplate;

		// Token: 0x040082CF RID: 33487
		private readonly ElementObjectManager m_BuyButtonSRTemplate;

		// Token: 0x040082D0 RID: 33488
		private readonly ElementObjectManager m_BuyButtonURTemplate;

		// Token: 0x040082D1 RID: 33489
		private List<BuyButtonGroupWidget.BuyButtonWidget> m_ButtonWidgets;

		// Token: 0x040082D2 RID: 33490
		private BuyButtonGroupWidget.BuyButtonWidget.Context m_ButtonCtx;

		// Token: 0x040082D3 RID: 33491
		public Action<PriceContext> onClickedCallback;

		// Token: 0x02000920 RID: 2336
		public class BuyButtonWidget : ElementWidgetBase
		{
			// Token: 0x1700054C RID: 1356
			// (get) Token: 0x06004417 RID: 17431 RVA: 0x0000216A File Offset: 0x0000036A
			public SelectionButton button
			{
				get
				{
					return null;
				}
			}

			// Token: 0x1700054D RID: 1357
			// (get) Token: 0x06004418 RID: 17432 RVA: 0x0000216A File Offset: 0x0000036A
			public GameObject iconGrp
			{
				get
				{
					return null;
				}
			}

			// Token: 0x1700054E RID: 1358
			// (get) Token: 0x06004419 RID: 17433 RVA: 0x0000216A File Offset: 0x0000036A
			public Image priceButtonIcon
			{
				get
				{
					return null;
				}
			}

			// Token: 0x1700054F RID: 1359
			// (get) Token: 0x0600441A RID: 17434 RVA: 0x0000216A File Offset: 0x0000036A
			public GameObject numGrp
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000550 RID: 1360
			// (get) Token: 0x0600441B RID: 17435 RVA: 0x0000216A File Offset: 0x0000036A
			public GameObject limitPriceGroup
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000551 RID: 1361
			// (get) Token: 0x0600441C RID: 17436 RVA: 0x0000216A File Offset: 0x0000036A
			public TMP_Text numText
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000552 RID: 1362
			// (get) Token: 0x0600441D RID: 17437 RVA: 0x0000216A File Offset: 0x0000036A
			public GameObject priceGrp
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000553 RID: 1363
			// (get) Token: 0x0600441E RID: 17438 RVA: 0x0000216A File Offset: 0x0000036A
			public GameObject paidIcon
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000554 RID: 1364
			// (get) Token: 0x0600441F RID: 17439 RVA: 0x0000216A File Offset: 0x0000036A
			public GameObject priceIcon
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000555 RID: 1365
			// (get) Token: 0x06004420 RID: 17440 RVA: 0x0000216A File Offset: 0x0000036A
			public GameObject priceIconTicket
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000556 RID: 1366
			// (get) Token: 0x06004421 RID: 17441 RVA: 0x0000216A File Offset: 0x0000036A
			public TMP_Text priceText
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000557 RID: 1367
			// (get) Token: 0x06004422 RID: 17442 RVA: 0x0000216A File Offset: 0x0000036A
			public TMP_Text infoText
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06004423 RID: 17443 RVA: 0x000F2C76 File Offset: 0x000F0E76
			public BuyButtonWidget(ElementObjectManager eom, int priceId)
				: base(null)
			{
			}

			// Token: 0x06004424 RID: 17444 RVA: 0x0000216D File Offset: 0x0000036D
			public void RefreshView(BuyButtonGroupWidget.BuyButtonWidget.Context ctx)
			{
			}

			// Token: 0x040082D4 RID: 33492
			private readonly string k_ELabel_IconGrp;

			// Token: 0x040082D5 RID: 33493
			private readonly string k_ELabel_IconGrp_PriceButtonIcon;

			// Token: 0x040082D6 RID: 33494
			private readonly string k_ELabel_NumGrp;

			// Token: 0x040082D7 RID: 33495
			private readonly string k_ELabel_NumGrp_PriceTimeLimitIcon;

			// Token: 0x040082D8 RID: 33496
			private readonly string k_ELabel_NumGrp_NumText;

			// Token: 0x040082D9 RID: 33497
			private readonly string k_ELabel_PriceGrp;

			// Token: 0x040082DA RID: 33498
			private readonly string k_ELabel_PriceGrp_PaidIcon;

			// Token: 0x040082DB RID: 33499
			private readonly string k_ELabel_PriceGrp_PriceIcon;

			// Token: 0x040082DC RID: 33500
			private readonly string k_ELabel_PriceGrp_PriceIconTicket;

			// Token: 0x040082DD RID: 33501
			private readonly string k_ELabel_PriceGrp_PriceText;

			// Token: 0x040082DE RID: 33502
			private readonly string k_ELabelInfoText;

			// Token: 0x040082DF RID: 33503
			public readonly int priceId;

			// Token: 0x02000921 RID: 2337
			public class Context
			{
				// Token: 0x06004425 RID: 17445 RVA: 0x0000216D File Offset: 0x0000036D
				public void Clear()
				{
				}

				// Token: 0x040082E0 RID: 33504
				public string buttonIconPath;

				// Token: 0x040082E1 RID: 33505
				public bool limitedIconVisible;

				// Token: 0x040082E2 RID: 33506
				public string numText;

				// Token: 0x040082E3 RID: 33507
				public bool paidIconVisible;

				// Token: 0x040082E4 RID: 33508
				public bool payItemIsPeriod;

				// Token: 0x040082E5 RID: 33509
				public int payItemCategory;

				// Token: 0x040082E6 RID: 33510
				public int payItemId;

				// Token: 0x040082E7 RID: 33511
				public string priceText;

				// Token: 0x040082E8 RID: 33512
				public string infoText;
			}
		}
	}
}
