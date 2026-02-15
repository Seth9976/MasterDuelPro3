using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomGame.Card;
using YgomGame.Menu;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Shop
{
	// Token: 0x0200096C RID: 2412
	public class ShopViewController : BaseMenuViewController, IMainTabListWidgetListener, IProductListWidgetListener, ISubTabListWidgetListener, IBackButtonWithoutSCSupported, IBackButtonSupported, IHeaderBorderSupported, IGemSupported
	{
		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06004665 RID: 18021 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool setProgressOnInitialize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06004666 RID: 18022 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool setSurfaceActiveOnInitialize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06004667 RID: 18023 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004668 RID: 18024 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnInputUpMainTab()
		{
		}

		// Token: 0x06004669 RID: 18025 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnInputLeftMainTab()
		{
		}

		// Token: 0x0600466A RID: 18026 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnInputRightMainTab()
		{
		}

		// Token: 0x0600466B RID: 18027 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnInputDownMainTab()
		{
		}

		// Token: 0x0600466C RID: 18028 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnClickMainTab(int idx)
		{
		}

		// Token: 0x0600466D RID: 18029 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnActivate()
		{
		}

		// Token: 0x0600466E RID: 18030 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnActivateInit()
		{
		}

		// Token: 0x0600466F RID: 18031 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnActivateResume()
		{
		}

		// Token: 0x06004670 RID: 18032 RVA: 0x0000216D File Offset: 0x0000036D
		private void ResumeShowcase()
		{
		}

		// Token: 0x06004671 RID: 18033 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool TryResumeFocusProduct()
		{
			return false;
		}

		// Token: 0x06004672 RID: 18034 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool TryResumeFocusHeadProduct()
		{
			return false;
		}

		// Token: 0x06004673 RID: 18035 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool TryResumeFocusHeader()
		{
			return false;
		}

		// Token: 0x06004674 RID: 18036 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool TryResumeFocusSubTab()
		{
			return false;
		}

		// Token: 0x06004675 RID: 18037 RVA: 0x0000216D File Offset: 0x0000036D
		private void ResumeFocusMainTab()
		{
		}

		// Token: 0x06004676 RID: 18038 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnPreDeactivate()
		{
		}

		// Token: 0x06004677 RID: 18039 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDeactivate()
		{
		}

		// Token: 0x06004678 RID: 18040 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnProductListScrolled(Vector2 value)
		{
		}

		// Token: 0x06004679 RID: 18041 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnFocusProductLine(ProductContext product)
		{
		}

		// Token: 0x0600467A RID: 18042 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnClickProduct(ProductWidget productWidget)
		{
		}

		// Token: 0x0600467B RID: 18043 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool OnInputDirectionSubCategory(PadInputDirection direction)
		{
			return false;
		}

		// Token: 0x0600467C RID: 18044 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool TryTransitionSection(int dir, bool isInitializeSelect = false)
		{
			return false;
		}

		// Token: 0x0600467D RID: 18045 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool TryTransitionSubCategory(int dir, bool isInitializeSelect = false)
		{
			return false;
		}

		// Token: 0x0600467E RID: 18046 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnClickSubCategory(int dataIdx)
		{
		}

		// Token: 0x0600467F RID: 18047 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnClickSubCategoryGroup(int dataIdx)
		{
		}

		// Token: 0x06004680 RID: 18048 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnClickSubCategorySection(int dataIdx, int sectionIdx)
		{
		}

		// Token: 0x06004681 RID: 18049 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenOnHome(Dictionary<string, object> args = null)
		{
		}

		// Token: 0x06004682 RID: 18050 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004683 RID: 18051 RVA: 0x000029C5 File Offset: 0x00000BC5
		public override float Progress()
		{
			return 0f;
		}

		// Token: 0x06004684 RID: 18052 RVA: 0x0000216D File Offset: 0x0000036D
		public override void ProgressUpdate()
		{
		}

		// Token: 0x06004685 RID: 18053 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yInitialize(Action onComplete)
		{
			return null;
		}

		// Token: 0x06004686 RID: 18054 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializeData()
		{
		}

		// Token: 0x06004687 RID: 18055 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnTransitionStart(ViewController.TransitionType type)
		{
		}

		// Token: 0x06004688 RID: 18056 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnTransitionEnd(ViewController.TransitionType type)
		{
		}

		// Token: 0x06004689 RID: 18057 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnResult(ViewController from, object value)
		{
			return false;
		}

		// Token: 0x0600468A RID: 18058 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnFocusChanged(bool setfocus)
		{
		}

		// Token: 0x0600468B RID: 18059 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x0600468C RID: 18060 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool TryOpenShopBuy(int shopId)
		{
			return false;
		}

		// Token: 0x0600468D RID: 18061 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnInputAnalogDirection(SelectorManager.AnalogType analogType, PadInputDirection dir)
		{
		}

		// Token: 0x0600468E RID: 18062 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnInputShortcutL1()
		{
		}

		// Token: 0x0600468F RID: 18063 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnInputShortcutR1()
		{
		}

		// Token: 0x040084C9 RID: 33993
		private bool m_IsStarted;

		// Token: 0x040084CA RID: 33994
		private bool m_ReimportDirty;

		// Token: 0x040084CB RID: 33995
		private Selector m_ResumeSelector;

		// Token: 0x040084CC RID: 33996
		private int m_ResumeCategoryId;

		// Token: 0x040084CD RID: 33997
		private int m_ResumeSubcategoryId;

		// Token: 0x040084CE RID: 33998
		private int m_ResumeSectionId;

		// Token: 0x040084CF RID: 33999
		private int m_ResumeProductId;

		// Token: 0x040084D0 RID: 34000
		private string m_ResumeDialogTitle;

		// Token: 0x040084D1 RID: 34001
		private string m_ResumeDialogMessage;

		// Token: 0x040084D2 RID: 34002
		public const string k_ArgsLaunchCategory = "category";

		// Token: 0x040084D3 RID: 34003
		public const string k_ArgsLaunchSubCategory = "subcategory";

		// Token: 0x040084D4 RID: 34004
		public const string k_ArgsLaunchSection = "section";

		// Token: 0x040084D5 RID: 34005
		private const string k_ALinkerLabel_BuyView = "BuyView";

		// Token: 0x040084D6 RID: 34006
		private const string k_ALabelProductWidgetMap = "ProductWidgetMap";

		// Token: 0x040084D7 RID: 34007
		private const string k_ELabelMainTabs = "CategoryTabs";

		// Token: 0x040084D8 RID: 34008
		private const string k_ELabelSubTabs = "ShowcaseWidget/SubTabs";

		// Token: 0x040084D9 RID: 34009
		private const string k_ELabelProductList = "ShowcaseWidget";

		// Token: 0x040084DA RID: 34010
		private const string k_ELabelAnalogDirectionItem = "AnalogDirectionItem";

		// Token: 0x040084DB RID: 34011
		private const string k_ELabelShortcutButtonBack = "ShortcutButtonBack";

		// Token: 0x040084DC RID: 34012
		private const string k_ELabelShortcutButtonCancel = "ShortcutButtonCancel";

		// Token: 0x040084DD RID: 34013
		private const string k_ELabelShortcutButtonL1 = "ShortcutButtonL1";

		// Token: 0x040084DE RID: 34014
		private const string k_ELabelShortcutButtonR1 = "ShortcutButtonR1";

		// Token: 0x040084DF RID: 34015
		private ShopSettings m_ShopSettings;

		// Token: 0x040084E0 RID: 34016
		private ProductWidgetController m_ProductWidgetController;

		// Token: 0x040084E1 RID: 34017
		private ShowcaseWidgetsController m_ShowcaseWidgetsController;

		// Token: 0x040084E2 RID: 34018
		private MainTabListWidget m_MainTabList;

		// Token: 0x040084E3 RID: 34019
		private SubTabListWidget m_SubTabList;

		// Token: 0x040084E4 RID: 34020
		private ProductListWidget m_ProductList;

		// Token: 0x040084E5 RID: 34021
		private CardCategoryData m_CardCategoryData;

		// Token: 0x040084E6 RID: 34022
		private ShopViewController.ShowcaseData m_ShowcaseData;

		// Token: 0x040084E7 RID: 34023
		private IEnumerator m_InitRoutine;

		// Token: 0x040084E8 RID: 34024
		private bool m_IsHighEnd;

		// Token: 0x040084E9 RID: 34025
		private string m_CachedShopBuyUIPath;

		// Token: 0x040084EA RID: 34026
		private List<ProductContext> m_BuyProductContexts;

		// Token: 0x0200096D RID: 2413
		private class MainTabListWidgetHandler : IMainTabListWidgetHandler
		{
			// Token: 0x1700061D RID: 1565
			// (get) Token: 0x06004691 RID: 18065 RVA: 0x000029CC File Offset: 0x00000BCC
			public int currentIdx
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x06004692 RID: 18066 RVA: 0x00002739 File Offset: 0x00000939
			public MainTabListWidgetHandler(ShopViewController.ShowcaseData showcaseData)
			{
			}

			// Token: 0x06004693 RID: 18067 RVA: 0x0000216D File Offset: 0x0000036D
			public void OnUpdateMainTabDataCount(IReadOnlyList<ShopTabWidget> sourceTabWidgets, List<ShopTabWidget> activeTabWidets)
			{
			}

			// Token: 0x06004694 RID: 18068 RVA: 0x0000216D File Offset: 0x0000036D
			public void OnUpdateMainTabData(IReadOnlyList<ShopTabWidget> activeTabWidets)
			{
			}

			// Token: 0x040084EB RID: 34027
			private readonly ShopViewController.ShowcaseData m_ShowcaseData;
		}

		// Token: 0x0200096E RID: 2414
		private class ProductListWidgetHandler : IProductListWidgetHandler, IProductContainerWidgetHandler
		{
			// Token: 0x1700061E RID: 1566
			// (get) Token: 0x06004695 RID: 18069 RVA: 0x0000216A File Offset: 0x0000036A
			public ProductWidgetController productWidgetController
			{
				get
				{
					return null;
				}
			}

			// Token: 0x1700061F RID: 1567
			// (get) Token: 0x06004696 RID: 18070 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06004697 RID: 18071 RVA: 0x0000216D File Offset: 0x0000036D
			public int showcaseUnloadUnusedCnt
			{
				[CompilerGenerated]
				get
				{
					return 0;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x06004698 RID: 18072 RVA: 0x00002739 File Offset: 0x00000939
			public ProductListWidgetHandler(ProductListWidget productList, ProductWidgetController productWidgetController, ShopViewController.ShowcaseData showcaseData)
			{
			}

			// Token: 0x06004699 RID: 18073 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetShowcaseUnloadUnusedCnt(int cnt)
			{
			}

			// Token: 0x0600469A RID: 18074 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool EqualCurrentCategoryId(int chkCategoryId, int chkSubCategoryId, int chkSectionId)
			{
				return false;
			}

			// Token: 0x0600469B RID: 18075 RVA: 0x0000216D File Offset: 0x0000036D
			public void OnUpdateDataCount(List<int> templateIdxList, List<string> headerLabels, Dictionary<int, Dictionary<int, Dictionary<int, int>>> headerDataIdxMap, List<ProductContainerWidget.Context> productContainerCtxs)
			{
			}

			// Token: 0x0600469C RID: 18076 RVA: 0x0000216D File Offset: 0x0000036D
			private void InsertListLabel(string label, int categoryId, int subCategoryId, int sectionId, List<int> templateIdxList, List<string> headerLabels, Dictionary<int, Dictionary<int, Dictionary<int, int>>> headerDataIdxMap, List<ProductContainerWidget.Context> productContainerCtxs)
			{
			}

			// Token: 0x0600469D RID: 18077 RVA: 0x0000216D File Offset: 0x0000036D
			private void InsertListEmpty(List<int> templateIdxList, List<string> headerLabels, List<ProductContainerWidget.Context> productContainerCtxs)
			{
			}

			// Token: 0x0600469E RID: 18078 RVA: 0x0000216D File Offset: 0x0000036D
			private void InsertProducts(List<ProductContext> products, List<int> templateIdxList, List<string> headerLabels, List<ProductContainerWidget.Context> productContainerCtxs, bool wrapAround = false)
			{
			}

			// Token: 0x0600469F RID: 18079 RVA: 0x0000216D File Offset: 0x0000036D
			public void UpdateProductWidget(ProductWidget productWidget, ProductContext productCtx)
			{
			}

			// Token: 0x040084EC RID: 34028
			private readonly ProductListWidget m_ProductList;

			// Token: 0x040084ED RID: 34029
			private readonly ProductWidgetController m_ProductWidgetController;

			// Token: 0x040084EE RID: 34030
			private readonly ShopViewController.ShowcaseData m_ShowcaseData;

			// Token: 0x040084EF RID: 34031
			private readonly Dictionary<int, Dictionary<int, List<ProductContext>>> m_SubProductsTmpList;

			// Token: 0x040084F0 RID: 34032
			private List<Vector2> m_SizeListCache;
		}

		// Token: 0x0200096F RID: 2415
		public class ShowcaseData
		{
			// Token: 0x17000620 RID: 1568
			// (get) Token: 0x060046A0 RID: 18080 RVA: 0x0000216A File Offset: 0x0000036A
			public List<int> categoryIds
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000621 RID: 1569
			// (get) Token: 0x060046A1 RID: 18081 RVA: 0x000029CC File Offset: 0x00000BCC
			public int currentCategoryId
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x17000622 RID: 1570
			// (get) Token: 0x060046A2 RID: 18082 RVA: 0x000029CC File Offset: 0x00000BCC
			public int currentSubCategoryId
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x17000623 RID: 1571
			// (get) Token: 0x060046A3 RID: 18083 RVA: 0x000029CC File Offset: 0x00000BCC
			public int currentSectionId
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x17000624 RID: 1572
			// (get) Token: 0x060046A4 RID: 18084 RVA: 0x000029CC File Offset: 0x00000BCC
			public int currentCategoryIdx
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x17000625 RID: 1573
			// (get) Token: 0x060046A5 RID: 18085 RVA: 0x000029CC File Offset: 0x00000BCC
			public int currentSubCategoryIdx
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x17000626 RID: 1574
			// (get) Token: 0x060046A6 RID: 18086 RVA: 0x000029CC File Offset: 0x00000BCC
			public int currentSectionIdx
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x060046A7 RID: 18087 RVA: 0x0000216A File Offset: 0x0000036A
			public ProductContext GetProductContext(int shopId)
			{
				return null;
			}

			// Token: 0x060046A8 RID: 18088 RVA: 0x0000216A File Offset: 0x0000036A
			public ProductContextCollection GetProductCollection(int categoryId)
			{
				return null;
			}

			// Token: 0x060046A9 RID: 18089 RVA: 0x0000216A File Offset: 0x0000036A
			public ProductContextCollection GetProductCollection(ShopDef.ShowcaseCategory productCategory)
			{
				return null;
			}

			// Token: 0x060046AA RID: 18090 RVA: 0x000029CC File Offset: 0x00000BCC
			public ShopViewController.ShowcaseData.IdFlag DiffCurrentIds(int categoryId, int subCategoryId, int sectionId)
			{
				return ShopViewController.ShowcaseData.IdFlag.None;
			}

			// Token: 0x060046AB RID: 18091 RVA: 0x000F48E8 File Offset: 0x000F2AE8
			public ValueTuple<int, int, int> GetCurrentIds()
			{
				return default(ValueTuple<int, int, int>);
			}

			// Token: 0x060046AC RID: 18092 RVA: 0x000029CC File Offset: 0x00000BCC
			public int GetCategoriesLength()
			{
				return 0;
			}

			// Token: 0x060046AD RID: 18093 RVA: 0x0000216A File Offset: 0x0000036A
			public IShopProductGruopData GetCategoryData(int categoryId)
			{
				return null;
			}

			// Token: 0x060046AE RID: 18094 RVA: 0x0000216A File Offset: 0x0000036A
			public List<int> GetSubcategories(int categoryId)
			{
				return null;
			}

			// Token: 0x060046AF RID: 18095 RVA: 0x000029CC File Offset: 0x00000BCC
			public int GetSubcategoriesLength(int categoryId)
			{
				return 0;
			}

			// Token: 0x060046B0 RID: 18096 RVA: 0x0000216A File Offset: 0x0000036A
			public List<int> GetSections(int categoryId, int subCategoryId)
			{
				return null;
			}

			// Token: 0x060046B1 RID: 18097 RVA: 0x000029CC File Offset: 0x00000BCC
			public int GetSectionsLength(int categoryId, int subCategoryId)
			{
				return 0;
			}

			// Token: 0x060046B2 RID: 18098 RVA: 0x0000216A File Offset: 0x0000036A
			public IShopProductGruopData GetSubCategoryData(int categoryId, int subCategoryId)
			{
				return null;
			}

			// Token: 0x060046B3 RID: 18099 RVA: 0x0000216A File Offset: 0x0000036A
			public IShopProductGruopData GetSectionData(int categoryId, int subCategoryId, int sectionId)
			{
				return null;
			}

			// Token: 0x060046B4 RID: 18100 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool ExistsCategories(int categoryId, int subCategoryId = 0, int sectionId = 0)
			{
				return false;
			}

			// Token: 0x060046B5 RID: 18101 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool ExistsCategory(int categoryId)
			{
				return false;
			}

			// Token: 0x060046B6 RID: 18102 RVA: 0x000029CC File Offset: 0x00000BCC
			public int IndexOfCategory(int categoryId)
			{
				return 0;
			}

			// Token: 0x060046B7 RID: 18103 RVA: 0x000029CC File Offset: 0x00000BCC
			public int CategoryOfIndex(int categoryIdx)
			{
				return 0;
			}

			// Token: 0x060046B8 RID: 18104 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool ExistsSubCategory(int categoryId, int subCategoryId)
			{
				return false;
			}

			// Token: 0x060046B9 RID: 18105 RVA: 0x000029CC File Offset: 0x00000BCC
			public int IndexOfSubCategory(int categoryId, int subCategoryId)
			{
				return 0;
			}

			// Token: 0x060046BA RID: 18106 RVA: 0x000029CC File Offset: 0x00000BCC
			public int SubCategoryOfIndex(int categoryIdx, int subCategoryIdx)
			{
				return 0;
			}

			// Token: 0x060046BB RID: 18107 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool ExistsSection(int categoryId, int subCategoryId, int sectionId)
			{
				return false;
			}

			// Token: 0x060046BC RID: 18108 RVA: 0x000029CC File Offset: 0x00000BCC
			public int IndexOfSection(int categoryId, int subCategoryId, int sectionId)
			{
				return 0;
			}

			// Token: 0x060046BD RID: 18109 RVA: 0x000029CC File Offset: 0x00000BCC
			public int SectionOfIndex(int categoryIdx, int subCategoryIdx, int sectionIdx)
			{
				return 0;
			}

			// Token: 0x060046BE RID: 18110 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool HasSection(int categoryId, int subCategoryId)
			{
				return false;
			}

			// Token: 0x060046BF RID: 18111 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool ExistsCategoryBadge(int categoryId, int subCategoryId = 0, int sectionId = 0)
			{
				return false;
			}

			// Token: 0x060046C0 RID: 18112 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool IsBadgeProduct(ProductContext productContext)
			{
				return false;
			}

			// Token: 0x060046C1 RID: 18113 RVA: 0x000029CC File Offset: 0x00000BCC
			private bool IsTrunoff(int shopId)
			{
				return false;
			}

			// Token: 0x060046C2 RID: 18114 RVA: 0x0000216A File Offset: 0x0000036A
			public List<int> CreateVisitBadgeList()
			{
				return null;
			}

			// Token: 0x060046C3 RID: 18115 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetCurrentId(int categoryId, int subCategoryId, int sectionId)
			{
			}

			// Token: 0x060046C4 RID: 18116 RVA: 0x0000216D File Offset: 0x0000036D
			public void ImportAll(ShopSettings shopSettings, CardCategoryData cardCategoryData)
			{
			}

			// Token: 0x060046C5 RID: 18117 RVA: 0x0000216D File Offset: 0x0000036D
			private void ClearCategory()
			{
			}

			// Token: 0x060046C6 RID: 18118 RVA: 0x0000216D File Offset: 0x0000036D
			private void AddCategory(IShopProductGruopData categoryData)
			{
			}

			// Token: 0x060046C7 RID: 18119 RVA: 0x0000216D File Offset: 0x0000036D
			private void ClearSubCategory(int categoryId)
			{
			}

			// Token: 0x060046C8 RID: 18120 RVA: 0x0000216D File Offset: 0x0000036D
			private void AddSubCategory(int categoryId, IShopProductGruopData subCategoryData)
			{
			}

			// Token: 0x060046C9 RID: 18121 RVA: 0x0000216D File Offset: 0x0000036D
			private void ClearSection(int categoryId, int subCategoryId)
			{
			}

			// Token: 0x060046CA RID: 18122 RVA: 0x0000216D File Offset: 0x0000036D
			private void AddSection(int categoryId, int subCategoryId, IShopProductGruopData sectionData)
			{
			}

			// Token: 0x060046CB RID: 18123 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool TurnoffCategoryBadges(ShopSettings shopSettings, int categoryId, int subCategoryId = 0, int sectionId = 0)
			{
				return false;
			}

			// Token: 0x040084F1 RID: 34033
			private readonly Dictionary<ShopDef.ShowcaseCategory, ProductContextCollection> m_ProductCollectionMap;

			// Token: 0x040084F2 RID: 34034
			private readonly Dictionary<int, ProductContext> m_ProductContextMap;

			// Token: 0x040084F3 RID: 34035
			private readonly List<int> m_CategoryIds;

			// Token: 0x040084F4 RID: 34036
			private readonly Dictionary<int, IShopProductGruopData> m_CategoryDatas;

			// Token: 0x040084F5 RID: 34037
			private readonly Dictionary<int, List<int>> m_SubCategoryIds;

			// Token: 0x040084F6 RID: 34038
			private readonly Dictionary<int, Dictionary<int, IShopProductGruopData>> m_SubCategoryDatas;

			// Token: 0x040084F7 RID: 34039
			private readonly Dictionary<int, Dictionary<int, List<int>>> m_SectionIds;

			// Token: 0x040084F8 RID: 34040
			private readonly Dictionary<int, Dictionary<int, Dictionary<int, IShopProductGruopData>>> m_SectionDatas;

			// Token: 0x040084F9 RID: 34041
			private Dictionary<int, bool> m_TurnoffBadgeMap;

			// Token: 0x040084FA RID: 34042
			private int m_CurrentCategoryId;

			// Token: 0x040084FB RID: 34043
			private int m_CurrentSubCategoryId;

			// Token: 0x040084FC RID: 34044
			private int m_CurrentSectionId;

			// Token: 0x040084FD RID: 34045
			private int m_CurrentCategoryIdx;

			// Token: 0x040084FE RID: 34046
			private int m_CurrentSubCategoryIdx;

			// Token: 0x040084FF RID: 34047
			private int m_CurrentSectionIdx;

			// Token: 0x02000970 RID: 2416
			public enum IdType
			{
				// Token: 0x04008501 RID: 34049
				None,
				// Token: 0x04008502 RID: 34050
				CategoryId,
				// Token: 0x04008503 RID: 34051
				SubCategoryId,
				// Token: 0x04008504 RID: 34052
				SectionId
			}

			// Token: 0x02000971 RID: 2417
			public enum IdFlag
			{
				// Token: 0x04008506 RID: 34054
				None,
				// Token: 0x04008507 RID: 34055
				CategoryId = 2,
				// Token: 0x04008508 RID: 34056
				SubCategoryId = 4,
				// Token: 0x04008509 RID: 34057
				SectionId = 8
			}
		}

		// Token: 0x02000972 RID: 2418
		private class SubTabListWidgetHandler : ISubTabListWidgetHandler
		{
			// Token: 0x17000627 RID: 1575
			// (get) Token: 0x060046CD RID: 18125 RVA: 0x000029CC File Offset: 0x00000BCC
			public int currentIdx
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x17000628 RID: 1576
			// (get) Token: 0x060046CE RID: 18126 RVA: 0x000029CC File Offset: 0x00000BCC
			public int currentSectionIdx
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x060046CF RID: 18127 RVA: 0x00002739 File Offset: 0x00000939
			public SubTabListWidgetHandler(ShopViewController.ShowcaseData showcaseData)
			{
			}

			// Token: 0x060046D0 RID: 18128 RVA: 0x000F4900 File Offset: 0x000F2B00
			public ValueTuple<int, int> CategoryIdOfIndex(int dataIdx)
			{
				return default(ValueTuple<int, int>);
			}

			// Token: 0x060046D1 RID: 18129 RVA: 0x0000216D File Offset: 0x0000036D
			public void OnUpdateSubTabWidget(List<int> templateIds)
			{
			}

			// Token: 0x060046D2 RID: 18130 RVA: 0x0000216D File Offset: 0x0000036D
			public void OnUpdateTabWidget(ShopTabWidget widget, int dataIdx)
			{
			}

			// Token: 0x060046D3 RID: 18131 RVA: 0x0000216D File Offset: 0x0000036D
			public void OnUpdateSectionFactory(ElementEntityFactory entityFactory, int dataIdx)
			{
			}

			// Token: 0x060046D4 RID: 18132 RVA: 0x0000216D File Offset: 0x0000036D
			public void OnUpdateSectionTabWidget(ShopTabWidget widget, int dataIdx, int sectionIdx)
			{
			}

			// Token: 0x0400850A RID: 34058
			private readonly ShopViewController.ShowcaseData m_ShowcaseData;
		}
	}
}
