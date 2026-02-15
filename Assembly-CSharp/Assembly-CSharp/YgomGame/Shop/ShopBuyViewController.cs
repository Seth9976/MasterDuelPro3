using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Menu;
using YgomSystem.UI;

namespace YgomGame.Shop
{
	// Token: 0x02000945 RID: 2373
	public class ShopBuyViewController : BaseMenuViewController, IDynamicChangeDispHeaderSupported
	{
		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x060045B0 RID: 17840 RVA: 0x0000216A File Offset: 0x0000036A
		private Dictionary<string, object> sendResultDic
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x060045B1 RID: 17841 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool setSurfaceActiveOnInitialize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x060045B2 RID: 17842 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060045B3 RID: 17843 RVA: 0x000029CC File Offset: 0x00000BCC
		public HeaderViewController.IsDispHeader IsDispContents()
		{
			return (HeaderViewController.IsDispHeader)0;
		}

		// Token: 0x060045B4 RID: 17844 RVA: 0x0000216A File Offset: 0x0000036A
		private string GetTLabelPagingOut(int direction)
		{
			return null;
		}

		// Token: 0x060045B5 RID: 17845 RVA: 0x0000216A File Offset: 0x0000036A
		private string GetTLabelPagingIn(int direction)
		{
			return null;
		}

		// Token: 0x060045B6 RID: 17846 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenOnHome(int shopId, int[] pageShopIds = null, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x060045B7 RID: 17847 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(int shopId, bool skipRequest = false, int[] pageShopIds = null, ShopBuyViewController.OpenMode openMode = ShopBuyViewController.OpenMode.Push, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x060045B8 RID: 17848 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenProducts(int idx, ProductContextCollection pageProductCollection, ShopBuyViewController.OpenMode openMode = ShopBuyViewController.OpenMode.Push, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x060045B9 RID: 17849 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenProducts(int idx, IReadOnlyList<ProductContext> pageProducts, ShopBuyViewController.OpenMode openMode = ShopBuyViewController.OpenMode.Push, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x060045BA RID: 17850 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenProduct(int shopId, ShopBuyViewController.OpenMode openMode = ShopBuyViewController.OpenMode.Push, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x060045BB RID: 17851 RVA: 0x0000216D File Offset: 0x0000036D
		private static void InnerOpen(ShopBuyViewController.OpenMode openMode = ShopBuyViewController.OpenMode.Push, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x060045BC RID: 17852 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CheckLaunch(int shopId, bool skipRequest, Action onSuccess, Action onFailed = null)
		{
		}

		// Token: 0x060045BD RID: 17853 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x060045BE RID: 17854 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x060045BF RID: 17855 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEnable()
		{
		}

		// Token: 0x060045C0 RID: 17856 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator Start()
		{
			return null;
		}

		// Token: 0x060045C1 RID: 17857 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnTransitionEnd(ViewController.TransitionType type)
		{
		}

		// Token: 0x060045C2 RID: 17858 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnFocusChanged(bool setfocus)
		{
		}

		// Token: 0x060045C3 RID: 17859 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnBack()
		{
			return false;
		}

		// Token: 0x060045C4 RID: 17860 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x060045C5 RID: 17861 RVA: 0x0000216D File Offset: 0x0000036D
		private void ToNextPage()
		{
		}

		// Token: 0x060045C6 RID: 17862 RVA: 0x0000216D File Offset: 0x0000036D
		private void ToPrevPage()
		{
		}

		// Token: 0x060045C7 RID: 17863 RVA: 0x0000216D File Offset: 0x0000036D
		private void ChangePage(int dstIdx, int direction = 0)
		{
		}

		// Token: 0x060045C8 RID: 17864 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yPlayPaging(int direction = 0)
		{
			return null;
		}

		// Token: 0x060045C9 RID: 17865 RVA: 0x0000216D File Offset: 0x0000036D
		private void CheckTutorialMonsterCutin()
		{
		}

		// Token: 0x060045CA RID: 17866 RVA: 0x0000216D File Offset: 0x0000036D
		private void RefreshPageButtons()
		{
		}

		// Token: 0x060045CB RID: 17867 RVA: 0x0000216D File Offset: 0x0000036D
		private void RefreshProduct()
		{
		}

		// Token: 0x060045CC RID: 17868 RVA: 0x0000216D File Offset: 0x0000036D
		private void RefreshProductStatus()
		{
		}

		// Token: 0x060045CD RID: 17869 RVA: 0x0000216D File Offset: 0x0000036D
		private void RefreshTicketGroup()
		{
		}

		// Token: 0x060045CE RID: 17870 RVA: 0x0000216D File Offset: 0x0000036D
		private void RefreshTicketAmount()
		{
		}

		// Token: 0x060045CF RID: 17871 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetLimitAlertStyle(bool isAlertOn)
		{
		}

		// Token: 0x060045D0 RID: 17872 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickNextButton()
		{
		}

		// Token: 0x060045D1 RID: 17873 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickPrevButton()
		{
		}

		// Token: 0x060045D2 RID: 17874 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnInputAnalogDirection(SelectorManager.AnalogType analogType, PadInputDirection dir)
		{
		}

		// Token: 0x060045D3 RID: 17875 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickShortcutL1()
		{
		}

		// Token: 0x060045D4 RID: 17876 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickShortcutR1()
		{
		}

		// Token: 0x060045D5 RID: 17877 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickTicketHeader()
		{
		}

		// Token: 0x060045D6 RID: 17878 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickMate()
		{
		}

		// Token: 0x060045D7 RID: 17879 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickPlayShortcut()
		{
		}

		// Token: 0x060045D8 RID: 17880 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateHighlightWidgetGamePad()
		{
		}

		// Token: 0x060045D9 RID: 17881 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateShortcutIcon()
		{
		}

		// Token: 0x060045DA RID: 17882 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateFooter()
		{
		}

		// Token: 0x060045DB RID: 17883 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnUpInputHilightThumb()
		{
		}

		// Token: 0x060045DC RID: 17884 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickHilightThumb(HighlightWidget.IThumbWidget thumbWidget)
		{
		}

		// Token: 0x060045DD RID: 17885 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickHilightPlay(HighlightWidget.IThumbWidget thumbWidget)
		{
		}

		// Token: 0x060045DE RID: 17886 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDeviceChange(SelectorManager.InputDevice inputDevice)
		{
		}

		// Token: 0x060045DF RID: 17887 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnChangedSelectionItem(SelectionItem prevItem, SelectionItem currentItem)
		{
		}

		// Token: 0x060045E0 RID: 17888 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickProductBuy(PriceContext priceContext, bool bySubPricesSheet = false)
		{
		}

		// Token: 0x060045E1 RID: 17889 RVA: 0x0000216D File Offset: 0x0000036D
		private void ConfirmPurchase(PriceContext priceContext)
		{
		}

		// Token: 0x060045E2 RID: 17890 RVA: 0x0000216D File Offset: 0x0000036D
		private void RequestBuyProduct(PriceContext priceContext, Dictionary<string, object> purchaseArgs = null, Action onComplete = null)
		{
		}

		// Token: 0x060045E3 RID: 17891 RVA: 0x0000216D File Offset: 0x0000036D
		private void BuyAfterRefresh(bool isSuccess)
		{
		}

		// Token: 0x060045E4 RID: 17892 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool ExistsAllProducts()
		{
			return false;
		}

		// Token: 0x060045E5 RID: 17893 RVA: 0x0000216D File Offset: 0x0000036D
		private void ReimportExistsProducts()
		{
		}

		// Token: 0x060045E6 RID: 17894 RVA: 0x0000216D File Offset: 0x0000036D
		private void PostProcessBuySuccessResult()
		{
		}

		// Token: 0x040083BA RID: 33722
		private const string k_PrefPath = "Shop/ShopBuy";

		// Token: 0x040083BB RID: 33723
		internal const string k_PurchaseArgs_Slot = "slot";

		// Token: 0x040083BC RID: 33724
		internal const string k_RouteKey_PurchaseHandler = "purchaseHandler";

		// Token: 0x040083BD RID: 33725
		private const string K_ArgKeyShopId = "productId";

		// Token: 0x040083BE RID: 33726
		private const string K_ArgKeyPageIdx = "pageIdx";

		// Token: 0x040083BF RID: 33727
		private const string K_ArgKeySendChangedShopIdOnLaunch = "sendChangedShopId";

		// Token: 0x040083C0 RID: 33728
		private const string K_ArgKeyPageProductCollection = "pageProductCollection";

		// Token: 0x040083C1 RID: 33729
		private const string K_ArgKeyConfirmSkipped = "confirmSkipped";

		// Token: 0x040083C2 RID: 33730
		internal const string K_ArgKeyBlockPurchase = "blockPurchase";

		// Token: 0x040083C3 RID: 33731
		internal const string K_ResultKey_ChangedShopId = "ShopBuy_ChangedShopId";

		// Token: 0x040083C4 RID: 33732
		internal const string K_ResultKey_RequestedWebApi = "ShopBuy_RequestedWebApi";

		// Token: 0x040083C5 RID: 33733
		internal const string K_ResultKey_RequestDialogTitle = "ShopBuy_RequestDialogTitle";

		// Token: 0x040083C6 RID: 33734
		internal const string K_ResultKey_RequestDialogMessage = "ShopBuy_RequestDialogMessage";

		// Token: 0x040083C7 RID: 33735
		private readonly string k_ELabelAnalogDirectionItem;

		// Token: 0x040083C8 RID: 33736
		private readonly string k_ELabelShortcutButtonL1;

		// Token: 0x040083C9 RID: 33737
		private readonly string k_ELabelShortcutButtonR1;

		// Token: 0x040083CA RID: 33738
		private readonly string k_ELabelShortcutIconL1;

		// Token: 0x040083CB RID: 33739
		private readonly string k_ELabelShortcutIconR1;

		// Token: 0x040083CC RID: 33740
		private readonly string k_ELabelTicketGroup;

		// Token: 0x040083CD RID: 33741
		private readonly string k_ELabelTicketIcon;

		// Token: 0x040083CE RID: 33742
		private readonly string k_ELabelTicketNumText;

		// Token: 0x040083CF RID: 33743
		private readonly string k_ELabelPrevButton;

		// Token: 0x040083D0 RID: 33744
		private readonly string k_ELabelNextButton;

		// Token: 0x040083D1 RID: 33745
		private readonly string k_ELabelPreviewContainer;

		// Token: 0x040083D2 RID: 33746
		private readonly string k_ELabelBadge;

		// Token: 0x040083D3 RID: 33747
		private readonly string k_ELabelCategoryNameText;

		// Token: 0x040083D4 RID: 33748
		private readonly string k_ELabelCategoryNameBorder;

		// Token: 0x040083D5 RID: 33749
		private readonly string k_ELabelProductNameText;

		// Token: 0x040083D6 RID: 33750
		private readonly string k_ELabelDescText;

		// Token: 0x040083D7 RID: 33751
		private readonly string k_ELabelDescScrollRect;

		// Token: 0x040083D8 RID: 33752
		private readonly string k_ELabelProductViewer;

		// Token: 0x040083D9 RID: 33753
		private readonly string k_ELabelHighlightList;

		// Token: 0x040083DA RID: 33754
		private readonly string k_ELabelNewGroup;

		// Token: 0x040083DB RID: 33755
		private readonly string k_ELabelNewText;

		// Token: 0x040083DC RID: 33756
		private readonly string k_ELabelInformButtonGroup;

		// Token: 0x040083DD RID: 33757
		private readonly string k_ELabelBuyButtonGroup;

		// Token: 0x040083DE RID: 33758
		private readonly string k_ELabelShortcutKeyFooterRoot;

		// Token: 0x040083DF RID: 33759
		private readonly string k_ALabelConfirmRegDialogProductWidget;

		// Token: 0x040083E0 RID: 33760
		private readonly string k_ALabelConfirmRegDialogTextWidget;

		// Token: 0x040083E1 RID: 33761
		private readonly string k_ALabelBuyActionSheetPositive;

		// Token: 0x040083E2 RID: 33762
		private const string k_TLabelStyleNormal = "Style_Normal";

		// Token: 0x040083E3 RID: 33763
		private const string k_TLabelStyleHighlight = "Style_Highlight";

		// Token: 0x040083E4 RID: 33764
		private const string k_TLabelLimitAlert_OFF = "LimitAlert_OFF";

		// Token: 0x040083E5 RID: 33765
		private const string k_TLabelLimitAlert_ON = "LimitAlert_ON";

		// Token: 0x040083E6 RID: 33766
		private readonly string k_TLabelPagingNextOut;

		// Token: 0x040083E7 RID: 33767
		private readonly string k_TLabelPagingBackOut;

		// Token: 0x040083E8 RID: 33768
		private readonly string k_TLabelPagingNextIn;

		// Token: 0x040083E9 RID: 33769
		private readonly string k_TLabelPagingBackIn;

		// Token: 0x040083EA RID: 33770
		private ShopSettings m_ShopSettings;

		// Token: 0x040083EB RID: 33771
		private ShopPreviewContainer m_ShopPreviewContainer;

		// Token: 0x040083EC RID: 33772
		private ShopShortcutKeyFooter m_ShortcutKeyFooter;

		// Token: 0x040083ED RID: 33773
		private SelectionButton m_PrevButton;

		// Token: 0x040083EE RID: 33774
		private SelectionButton m_NextButton;

		// Token: 0x040083EF RID: 33775
		private ProductContext m_ProductContext;

		// Token: 0x040083F0 RID: 33776
		private int m_PageIdx;

		// Token: 0x040083F1 RID: 33777
		private IReadOnlyList<ProductContext> m_PageProducts;

		// Token: 0x040083F2 RID: 33778
		private PriceContext m_HeaderTicketPriceCtx;

		// Token: 0x040083F3 RID: 33779
		private ExtendedScrollRect m_DescScrollRect;

		// Token: 0x040083F4 RID: 33780
		private ProductViewerWidget m_ProductViewerWidget;

		// Token: 0x040083F5 RID: 33781
		private InformButtonGroupWidget m_InformButtonGroupWidget;

		// Token: 0x040083F6 RID: 33782
		private HighlightWidget m_HighlightWidget;

		// Token: 0x040083F7 RID: 33783
		private BuyButtonGroupWidget m_BuyButtonGroupWidget;

		// Token: 0x040083F8 RID: 33784
		private bool m_RefreshTrigger;

		// Token: 0x040083F9 RID: 33785
		private int m_PreviewMateItemId;

		// Token: 0x040083FA RID: 33786
		private int m_PreviewMateHighlightIdx;

		// Token: 0x040083FB RID: 33787
		private bool m_ExistsMonsterCutin;

		// Token: 0x040083FC RID: 33788
		private List<HighlightContext> m_HighlightContexts;

		// Token: 0x040083FD RID: 33789
		private List<ProductViewerWidget.IThumbPlayer> m_ViewerPlayers;

		// Token: 0x040083FE RID: 33790
		private List<int> m_CardBrowseMrks;

		// Token: 0x040083FF RID: 33791
		private List<int> m_CardBrowseRareList;

		// Token: 0x04008400 RID: 33792
		private List<HighlightContext> m_CardBrowseContexts;

		// Token: 0x04008401 RID: 33793
		private List<Tween> m_SpecialTimeTweens;

		// Token: 0x04008402 RID: 33794
		private GameObject m_ActionSheetTemplate;

		// Token: 0x04008403 RID: 33795
		private bool m_ProductClosed;

		// Token: 0x04008404 RID: 33796
		private Dictionary<string, object> m_SendResultDicCache;

		// Token: 0x04008405 RID: 33797
		private bool m_IsReady;

		// Token: 0x04008406 RID: 33798
		private bool m_OnLaunchCheckTutorialReserve;

		// Token: 0x02000946 RID: 2374
		public class PurchaseHandler
		{
			// Token: 0x060045E8 RID: 17896 RVA: 0x00002739 File Offset: 0x00000939
			public PurchaseHandler(Action<Action, Action> onOpenConfirmCallback, Action<Dictionary<string, object>, Action> onRequestPurchaseCallback, Action onPostProcessPurchaseCallback)
			{
			}

			// Token: 0x060045E9 RID: 17897 RVA: 0x0000216D File Offset: 0x0000036D
			public void OpenConfirm(Action onDecided, Action onCanceled)
			{
			}

			// Token: 0x060045EA RID: 17898 RVA: 0x0000216D File Offset: 0x0000036D
			public void RequestPurchase(Dictionary<string, object> purchaseArgs, Action onComplete)
			{
			}

			// Token: 0x060045EB RID: 17899 RVA: 0x0000216D File Offset: 0x0000036D
			public void PostProcessPurchase()
			{
			}

			// Token: 0x04008407 RID: 33799
			private readonly Action<Action, Action> m_OnOpenConfirmCallback;

			// Token: 0x04008408 RID: 33800
			private readonly Action<Dictionary<string, object>, Action> m_OnRequestPurchaseCallback;

			// Token: 0x04008409 RID: 33801
			private readonly Action m_OnPostProcessPurchaseCallback;
		}

		// Token: 0x02000947 RID: 2375
		public enum OpenMode
		{
			// Token: 0x0400840B RID: 33803
			Push,
			// Token: 0x0400840C RID: 33804
			PushOnHome,
			// Token: 0x0400840D RID: 33805
			ReplaceOpen
		}
	}
}
