using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using YgomGame.Card;
using YgomGame.CardPack;

namespace YgomGame.Shop
{
	// Token: 0x0200093A RID: 2362
	public class ProductContext : IComparable<ProductContext>
	{
		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x060044DA RID: 17626 RVA: 0x000029CC File Offset: 0x00000BCC
		public int shopId
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x060044DB RID: 17627 RVA: 0x000029CC File Offset: 0x00000BCC
		public int productTypeId
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x060044DC RID: 17628 RVA: 0x000029CC File Offset: 0x00000BCC
		public ShopDef.ProductType productType
		{
			get
			{
				return (ShopDef.ProductType)0;
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x060044DD RID: 17629 RVA: 0x000029CC File Offset: 0x00000BCC
		public int targetCategory
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x060044DE RID: 17630 RVA: 0x000029CC File Offset: 0x00000BCC
		public int targetId
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x060044DF RID: 17631 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool targetIsPeriod
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x060044E0 RID: 17632 RVA: 0x000029CC File Offset: 0x00000BCC
		public int categoryId
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x060044E1 RID: 17633 RVA: 0x000029CC File Offset: 0x00000BCC
		public int sectionId
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x060044E2 RID: 17634 RVA: 0x000029CC File Offset: 0x00000BCC
		public ShopDef.ShowcaseCategory category
		{
			get
			{
				return (ShopDef.ShowcaseCategory)0;
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x060044E3 RID: 17635 RVA: 0x000029CC File Offset: 0x00000BCC
		public int subCategoryId
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x060044E4 RID: 17636 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual string productName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x060044E5 RID: 17637 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual string descTextFull
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x060044E6 RID: 17638 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual string descTextShort
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x060044E7 RID: 17639 RVA: 0x0000216A File Offset: 0x0000036A
		public string listDescText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x060044E8 RID: 17640 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060044E9 RID: 17641 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isNew
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x060044EA RID: 17642 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool workIsNew
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x060044EB RID: 17643 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool sendNew
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x060044EC RID: 17644 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isLimitedDate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x060044ED RID: 17645 RVA: 0x000F1669 File Offset: 0x000EF869
		public long limitTs
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x060044EE RID: 17646 RVA: 0x0000216A File Offset: 0x0000036A
		public string limitDate
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x060044EF RID: 17647 RVA: 0x0000216A File Offset: 0x0000036A
		public string limitDateShort
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x060044F0 RID: 17648 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isLimitedBuyCount
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x060044F1 RID: 17649 RVA: 0x000029CC File Offset: 0x00000BCC
		public int limitBuyCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x060044F2 RID: 17650 RVA: 0x000029CC File Offset: 0x00000BCC
		public int nowBuyCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x060044F3 RID: 17651 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isSoldOut
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x060044F4 RID: 17652 RVA: 0x0000216A File Offset: 0x0000036A
		public List<object> confirmTextIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x060044F5 RID: 17653 RVA: 0x0000216A File Offset: 0x0000036A
		public List<object> setItems
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x060044F6 RID: 17654 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool hasSetItems
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x060044F7 RID: 17655 RVA: 0x000F1669 File Offset: 0x000EF869
		public long sort
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x060044F8 RID: 17656 RVA: 0x000029CC File Offset: 0x00000BCC
		public int packId
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x060044F9 RID: 17657 RVA: 0x000029CC File Offset: 0x00000BCC
		public int packTypeId
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x060044FA RID: 17658 RVA: 0x000029CC File Offset: 0x00000BCC
		public CardPackDef.PackType packType
		{
			get
			{
				return CardPackDef.PackType.None;
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x060044FB RID: 17659 RVA: 0x000029CC File Offset: 0x00000BCC
		public int structureId
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x060044FC RID: 17660 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isPeriod
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x060044FD RID: 17661 RVA: 0x000029CC File Offset: 0x00000BCC
		public int itemCategory
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x060044FE RID: 17662 RVA: 0x000029CC File Offset: 0x00000BCC
		public int itemId
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x060044FF RID: 17663 RVA: 0x000029CC File Offset: 0x00000BCC
		public int packNormalCardPoolId
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06004500 RID: 17664 RVA: 0x000029CC File Offset: 0x00000BCC
		public int packPickupCardPoolId
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06004501 RID: 17665 RVA: 0x000029CC File Offset: 0x00000BCC
		public int destructive
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06004502 RID: 17666 RVA: 0x000029CC File Offset: 0x00000BCC
		public int handling
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06004503 RID: 17667 RVA: 0x000029CC File Offset: 0x00000BCC
		public int difficult
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06004504 RID: 17668 RVA: 0x0000216A File Offset: 0x0000036A
		public Dictionary<string, object> duelpassreward
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06004505 RID: 17669 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingShopProductThumb.Context thumbContext
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06004506 RID: 17670 RVA: 0x000029CC File Offset: 0x00000BCC
		public ShopDef.ListButtonType listButtonType
		{
			get
			{
				return ShopDef.ListButtonType.Default;
			}
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06004507 RID: 17671 RVA: 0x0000216A File Offset: 0x0000036A
		public List<PriceContext> priceContexts
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06004508 RID: 17672 RVA: 0x0000216A File Offset: 0x0000036A
		public PriceContext listButtonPrice
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06004509 RID: 17673 RVA: 0x0000216A File Offset: 0x0000036A
		public List<HighlightContext> highlightContexts
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x0600450A RID: 17674 RVA: 0x0000216A File Offset: 0x0000036A
		public HighlightContext headHighlightContext
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x0600450B RID: 17675 RVA: 0x0000216A File Offset: 0x0000036A
		public object chartData
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x0600450C RID: 17676 RVA: 0x0000216A File Offset: 0x0000036A
		public string deckCaseImagePath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x0600450D RID: 17677 RVA: 0x0000216A File Offset: 0x0000036A
		public List<string> filterNames
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x0600450E RID: 17678 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x0600450F RID: 17679 RVA: 0x0000216D File Offset: 0x0000036D
		public ShopSettings shopSettings
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

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06004510 RID: 17680 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06004511 RID: 17681 RVA: 0x0000216D File Offset: 0x0000036D
		public CardCategoryData cardCategoryData
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

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06004512 RID: 17682 RVA: 0x000029CC File Offset: 0x00000BCC
		public int limitAlertSec
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06004513 RID: 17683 RVA: 0x000029CC File Offset: 0x00000BCC
		public int highlightStyleType
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06004514 RID: 17684 RVA: 0x0000216A File Offset: 0x0000036A
		public string productWidgetLabel
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06004515 RID: 17685 RVA: 0x0000216A File Offset: 0x0000036A
		public string headLabelText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06004516 RID: 17686 RVA: 0x000029CC File Offset: 0x00000BCC
		public int bgId
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06004517 RID: 17687 RVA: 0x0000216A File Offset: 0x0000036A
		public string informButtonLabel
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06004518 RID: 17688 RVA: 0x000029CC File Offset: 0x00000BCC
		public ShopDef.HighlightType highlightType
		{
			get
			{
				return (ShopDef.HighlightType)0;
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06004519 RID: 17689 RVA: 0x0000216A File Offset: 0x0000036A
		public string productSubLabel
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x0600451A RID: 17690 RVA: 0x000029CC File Offset: 0x00000BCC
		public ShopDef.ViewerLoopType viewerLoopType
		{
			get
			{
				return ShopDef.ViewerLoopType.Default;
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x0600451B RID: 17691 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool hideSummonPlay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x0600451C RID: 17692 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isSoldOutSort
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x0600451D RID: 17693 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isShortPayAmountSort
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600451E RID: 17694 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool FindBoolSetting(string key, bool defaultValue = false)
		{
			return false;
		}

		// Token: 0x0600451F RID: 17695 RVA: 0x000029CC File Offset: 0x00000BCC
		public int FindIntSetting(string key, int defaultValue = 0)
		{
			return 0;
		}

		// Token: 0x06004520 RID: 17696 RVA: 0x0000216A File Offset: 0x0000036A
		private string FindStringSetting(string key, string defaultValue = null)
		{
			return null;
		}

		// Token: 0x06004521 RID: 17697 RVA: 0x0000216D File Offset: 0x0000036D
		public void Import(Dictionary<string, object> productData)
		{
		}

		// Token: 0x06004522 RID: 17698 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetSectionId(int sectionId)
		{
		}

		// Token: 0x06004523 RID: 17699 RVA: 0x000029CC File Offset: 0x00000BCC
		public int Compare(ProductContext a, ProductContext b)
		{
			return 0;
		}

		// Token: 0x06004524 RID: 17700 RVA: 0x000029CC File Offset: 0x00000BCC
		public int CompareTo(ProductContext other)
		{
			return 0;
		}

		// Token: 0x06004525 RID: 17701 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsItemProduct()
		{
			return false;
		}

		// Token: 0x06004526 RID: 17702 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetPushMessage(bool isDetail = false)
		{
			return null;
		}

		// Token: 0x06004527 RID: 17703 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual string GetOwnedText()
		{
			return null;
		}

		// Token: 0x06004528 RID: 17704 RVA: 0x000029CC File Offset: 0x00000BCC
		public int SearchContainItemId(bool isPeriod, int itemCategory)
		{
			return 0;
		}

		// Token: 0x06004529 RID: 17705 RVA: 0x000F4830 File Offset: 0x000F2A30
		public ValueTuple<int, int, int, int> SearchFieldSetItems()
		{
			return default(ValueTuple<int, int, int, int>);
		}

		// Token: 0x04008333 RID: 33587
		protected Dictionary<string, object> m_ProductData;

		// Token: 0x04008334 RID: 33588
		private int m_SectionId;

		// Token: 0x04008335 RID: 33589
		private bool m_IsNew;

		// Token: 0x04008336 RID: 33590
		private readonly BindingShopProductThumb.Context m_ThumbContext;

		// Token: 0x04008337 RID: 33591
		private readonly List<PriceContext> m_PriceContexts;

		// Token: 0x04008338 RID: 33592
		private PriceContext m_ListButtonPrice;

		// Token: 0x04008339 RID: 33593
		private readonly List<HighlightContext> m_HighlightContexts;

		// Token: 0x0400833A RID: 33594
		private List<string> m_FilterNames;

		// Token: 0x0400833B RID: 33595
		private Dictionary<string, object> m_Decoration;
	}
}
