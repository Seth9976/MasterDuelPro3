using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YgomGame.ActionSheet;
using YgomGame.Card;
using YgomGame.Deck;
using YgomGame.Duel;
using YgomGame.Menu;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.Utility;

namespace YgomGame.DeckBrowser
{
	// Token: 0x02000F8D RID: 3981
	public class DeckBrowserViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported, IBokeSupported
	{
		// Token: 0x17000E0D RID: 3597
		// (get) Token: 0x060074DF RID: 29919 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060074E0 RID: 29920 RVA: 0x0000216D File Offset: 0x0000036D
		public bool monochromeEnable
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000E0E RID: 3598
		// (get) Token: 0x060074E1 RID: 29921 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060074E2 RID: 29922 RVA: 0x0000216D File Offset: 0x0000036D
		public bool premiumCheckEnable
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000E0F RID: 3599
		// (get) Token: 0x060074E3 RID: 29923 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isMobile
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E10 RID: 3600
		// (get) Token: 0x060074E4 RID: 29924 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isGamePad
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E11 RID: 3601
		// (get) Token: 0x060074E5 RID: 29925 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060074E6 RID: 29926 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isDialog
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000E12 RID: 3602
		// (get) Token: 0x060074E7 RID: 29927 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060074E8 RID: 29928 RVA: 0x0000216D File Offset: 0x0000036D
		private DeckBrowserViewController.BrowserType browserType
		{
			get
			{
				return DeckBrowserViewController.BrowserType.Solo;
			}
			set
			{
			}
		}

		// Token: 0x17000E13 RID: 3603
		// (get) Token: 0x060074E9 RID: 29929 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060074EA RID: 29930 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenAsOpponentDeckCheck(string deckName, object mainCards, object extraCards, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x060074EB RID: 29931 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenAsConfirmation(DeckSelectViewController2.DeckEventType deckType, int deckID, int eventID, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x060074EC RID: 29932 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenAsPickupCardSelection(string name, object mainCards, object extraCards, int id, int eventId, int deckcaseId, ProfileEditViewController.EditType editType, Action<DeckBrowserViewController> onStackEntryCallback = null, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x060074ED RID: 29933 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator InitPickupCards()
		{
			return null;
		}

		// Token: 0x060074EE RID: 29934 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitPickupCardsMobile(PickupCardSelectionWidget pickupCardSelectionWidget)
		{
		}

		// Token: 0x060074EF RID: 29935 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClickDetailViewCard(int cardId, int styleId)
		{
		}

		// Token: 0x060074F0 RID: 29936 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenAsPublicDeck(string name, int pickCardId, object mainCards, object extraCards, object categories, object tags, Transform transform = null, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x060074F1 RID: 29937 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenAsSelect(DeckSelectViewController2.SelectMode mode, DeckSelectViewController2.DeckEventType deckType, int deckID, int eventID, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x060074F2 RID: 29938 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickSelectButton(DeckSelectViewController2.SelectMode mode, int deckID, int eventID)
		{
		}

		// Token: 0x060074F3 RID: 29939 RVA: 0x0000216D File Offset: 0x0000036D
		private void DeckSetByGameMode(Util.GameMode gameMode, int deckID)
		{
		}

		// Token: 0x060074F4 RID: 29940 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenDeckSelectDialog(DeckSelectViewController2.SelectMode mode)
		{
		}

		// Token: 0x060074F5 RID: 29941 RVA: 0x0000216A File Offset: 0x0000036A
		private Dictionary<string, object> CopyDeckData()
		{
			return null;
		}

		// Token: 0x060074F6 RID: 29942 RVA: 0x0000216D File Offset: 0x0000036D
		private void SelectDeck(DeckSelectViewController2.SelectMode mode, int deckID, int eventID)
		{
		}

		// Token: 0x060074F7 RID: 29943 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenOutOfTermDialog()
		{
		}

		// Token: 0x060074F8 RID: 29944 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenAsSolo(int chapterId, SoloDeckUtil.SoloDeckType soloDeckType, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x060074F9 RID: 29945 RVA: 0x0000216D File Offset: 0x0000036D
		private static void OpenStructure(int structureId, Action<DeckBrowserViewController> onStackCallback, Dictionary<string, object> args)
		{
		}

		// Token: 0x060074FA RID: 29946 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenAsStructure(int structureId, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x060074FB RID: 29947 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenAsFirstStructure(int structureId, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x060074FC RID: 29948 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenAsStructureDeckCopy(int structureId, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x060074FD RID: 29949 RVA: 0x0000216D File Offset: 0x0000036D
		private static void GetFirstStructure(int structureId, DeckBrowserViewController vc, Action completeCallback)
		{
		}

		// Token: 0x060074FE RID: 29950 RVA: 0x0000216A File Offset: 0x0000036A
		private static Dictionary<string, object> GetPickupCardsByStructureMaster(object structureMaster)
		{
			return null;
		}

		// Token: 0x060074FF RID: 29951 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenAsTrialDraw(string deckName, object mainCards, object extraCards, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x06007500 RID: 29952 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenAsTrialDraw(DeckSelectViewController2.DeckEventType deckType, int deckID, int eventID, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x06007501 RID: 29953 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenTrialDrawView(string name, List<CardBaseData> main, List<CardBaseData> extra)
		{
		}

		// Token: 0x06007502 RID: 29954 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenTrialDrawView()
		{
		}

		// Token: 0x06007503 RID: 29955 RVA: 0x0000216D File Offset: 0x0000036D
		private void DispLoadingMobile(bool isLoading)
		{
		}

		// Token: 0x06007504 RID: 29956 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(string name, object mainCards, object extraCards, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x06007505 RID: 29957 RVA: 0x0000216A File Offset: 0x0000036A
		private static List<CardBaseData> GetRentalCards(int rentalPoolID)
		{
			return null;
		}

		// Token: 0x06007506 RID: 29958 RVA: 0x0000216A File Offset: 0x0000036A
		private static Dictionary<string, object> ConvertCBDtoDict(List<CardBaseData> cbd)
		{
			return null;
		}

		// Token: 0x06007507 RID: 29959 RVA: 0x0000216A File Offset: 0x0000036A
		private static Dictionary<string, object> ConvertPickupCardsListToDict(List<object> ids, List<object> r)
		{
			return null;
		}

		// Token: 0x06007508 RID: 29960 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializeView()
		{
		}

		// Token: 0x06007509 RID: 29961 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x0600750A RID: 29962 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x0600750B RID: 29963 RVA: 0x0000216A File Offset: 0x0000036A
		private string GetTitle()
		{
			return null;
		}

		// Token: 0x0600750C RID: 29964 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x0600750D RID: 29965 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator InitDeckCards(List<object> mainCardMrks, List<object> mainCardPremiums, List<object> extraCardMrks, List<object> extraCardPremiums, Action onFinish)
		{
			return null;
		}

		// Token: 0x0600750E RID: 29966 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator InitDeckCardsMobile(List<object> mainCardMrks, List<object> mainCardPremiums, List<object> extraCardMrks, List<object> extraCardPremiums, Action onFinish)
		{
			return null;
		}

		// Token: 0x0600750F RID: 29967 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCreatedCardCallbackDefault(DeckCard deckCard, int idx)
		{
		}

		// Token: 0x06007510 RID: 29968 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetRegulation(int eventID, int deckId, DeckSelectViewController2.DeckEventType regType = DeckSelectViewController2.DeckEventType.ExhibitionDeck)
		{
		}

		// Token: 0x06007511 RID: 29969 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetRegulation(int eventID, DeckSelectViewController2.SelectMode mode = DeckSelectViewController2.SelectMode.Exhibition)
		{
		}

		// Token: 0x06007512 RID: 29970 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetRegulation(int regulationId)
		{
		}

		// Token: 0x06007513 RID: 29971 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDetailViewCard(CardBaseData cbd)
		{
		}

		// Token: 0x06007514 RID: 29972 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDetailViewCard(int mrk, int premiumId, bool isRental = false)
		{
		}

		// Token: 0x06007515 RID: 29973 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClickDetailViewCard(int idx)
		{
		}

		// Token: 0x06007516 RID: 29974 RVA: 0x0000216D File Offset: 0x0000036D
		public void RefreshRegulation()
		{
		}

		// Token: 0x06007517 RID: 29975 RVA: 0x0000216D File Offset: 0x0000036D
		private void RefreshRegulationIcon()
		{
		}

		// Token: 0x06007518 RID: 29976 RVA: 0x0000216D File Offset: 0x0000036D
		public void RefreshRarity()
		{
		}

		// Token: 0x06007519 RID: 29977 RVA: 0x0000216D File Offset: 0x0000036D
		private void ApplyHasCardDisplay()
		{
		}

		// Token: 0x0600751A RID: 29978 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitMonochromeList()
		{
		}

		// Token: 0x0600751B RID: 29979 RVA: 0x000029CC File Offset: 0x00000BCC
		private int InitNumMainCol(int numMain)
		{
			return 0;
		}

		// Token: 0x0600751C RID: 29980 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsBottomMainCard(int index)
		{
			return false;
		}

		// Token: 0x0600751D RID: 29981 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetMainBottomKeyDownCallback(int mainIndex)
		{
		}

		// Token: 0x0600751E RID: 29982 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator DelayedInvokeCallback(Action action)
		{
			return null;
		}

		// Token: 0x0600751F RID: 29983 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool RegulationCheck()
		{
			return false;
		}

		// Token: 0x06007520 RID: 29984 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool PossetionCheck(bool distinctPrem)
		{
			return false;
		}

		// Token: 0x06007521 RID: 29985 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitCommonSettings(DeckBrowserOptionWidget optionWidget, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x06007522 RID: 29986 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitEnableDeckBrowserOptions(DeckBrowserOptionWidget optionWidget, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x06007523 RID: 29987 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitDeckBrowserCallBacks(DeckBrowserOptionWidget optionWidget, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x06007524 RID: 29988 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickCopyButton()
		{
		}

		// Token: 0x06007525 RID: 29989 RVA: 0x0000216D File Offset: 0x0000036D
		private void SaveDeck()
		{
		}

		// Token: 0x06007526 RID: 29990 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnBack()
		{
			return false;
		}

		// Token: 0x0400ADD8 RID: 44504
		private const string k_ArgKeyName = "name";

		// Token: 0x0400ADD9 RID: 44505
		private const string k_ArgKeyMainCards = "mcards";

		// Token: 0x0400ADDA RID: 44506
		private const string k_ArgKeyExtraCards = "ecards";

		// Token: 0x0400ADDB RID: 44507
		public const string k_ArgKeyRegulationVisible = "regulationVisible";

		// Token: 0x0400ADDC RID: 44508
		public const string k_ArgKeyRarityVisible = "rarityVisible";

		// Token: 0x0400ADDD RID: 44509
		public const string k_ArgKeyMonochromeEnable = "regulationMonochromeEnable";

		// Token: 0x0400ADDE RID: 44510
		public const string k_ArgKeyPremiumCheckEnable = "premiumCheckEnable";

		// Token: 0x0400ADDF RID: 44511
		public const string k_ArgKeyOnStackEntryCallback = "onStackEntryCallback";

		// Token: 0x0400ADE0 RID: 44512
		public const string k_ArgKeyShortcutSettings = "shortcutSettings";

		// Token: 0x0400ADE1 RID: 44513
		public const string k_ArgKeyAccessories = "accessories";

		// Token: 0x0400ADE2 RID: 44514
		public const string k_ArgKeyPickCards = "pickCards";

		// Token: 0x0400ADE3 RID: 44515
		public const string k_ArgKeyNumMainCards = "numMainCards";

		// Token: 0x0400ADE4 RID: 44516
		public const string k_ArgKeyNumExtraCards = "numExtraCards";

		// Token: 0x0400ADE5 RID: 44517
		public const string k_ArgKeyIconDeckId = "iconDeckId";

		// Token: 0x0400ADE6 RID: 44518
		public const string k_ArgKeyPopViewEvent = "popViewEvent";

		// Token: 0x0400ADE7 RID: 44519
		public const string k_ArgKeyOnClickCopyCallback = "onClickCopyCallback";

		// Token: 0x0400ADE8 RID: 44520
		public const string k_ArgKeyOnClickSelectCallback = "onClickSelectCallback";

		// Token: 0x0400ADE9 RID: 44521
		public const string k_ArgKeyOnCompleteSelectDeckCallback = "onCompleteSelectDeckCallback";

		// Token: 0x0400ADEA RID: 44522
		public const string k_ArgKeySortEnable = "sortEnable";

		// Token: 0x0400ADEB RID: 44523
		public const string k_ArgKeyDeckNameInit = "deckNameInit";

		// Token: 0x0400ADEC RID: 44524
		public const string k_ArgKeyRegulation = "regulationId";

		// Token: 0x0400ADED RID: 44525
		public const string k_ArgKeyOpenAsDialog = "openAsDialog";

		// Token: 0x0400ADEE RID: 44526
		public const string k_ArgKeyRarityToggleEnable = "rarityToggleEnable";

		// Token: 0x0400ADEF RID: 44527
		public const string k_ArgKeyRentalCardPool = "rentalCardPool";

		// Token: 0x0400ADF0 RID: 44528
		public const string k_ArgKeyEventDeckID = "EventDeckID";

		// Token: 0x0400ADF1 RID: 44529
		public const string k_ArgKeyNeuronMyDeck = "NeuronMyDeck";

		// Token: 0x0400ADF2 RID: 44530
		private readonly string k_ELabelTitle;

		// Token: 0x0400ADF3 RID: 44531
		private readonly string k_ELabelDeckView;

		// Token: 0x0400ADF4 RID: 44532
		private readonly string k_ELabelDetailView;

		// Token: 0x0400ADF5 RID: 44533
		private readonly string k_ELabelDetailViewMenuRoot;

		// Token: 0x0400ADF6 RID: 44534
		private readonly string k_ELabelOptionalAreaLocator;

		// Token: 0x0400ADF7 RID: 44535
		private readonly string k_ELabelIconDeck;

		// Token: 0x0400ADF8 RID: 44536
		private readonly string k_ELabelNoItemButton;

		// Token: 0x0400ADF9 RID: 44537
		private const string k_ELabelGroupCardNum = "GroupCardNum";

		// Token: 0x0400ADFA RID: 44538
		private const string k_ELabelTextCardNum = "TextCardNum";

		// Token: 0x0400ADFB RID: 44539
		private const string k_ELabelDialogBG = "DialogBG";

		// Token: 0x0400ADFC RID: 44540
		private const string k_ELabelDialogCloseButton = "DialogCloseButton";

		// Token: 0x0400ADFD RID: 44541
		private const string k_ELabelRarityToggleButton = "RarityToggleButton";

		// Token: 0x0400ADFE RID: 44542
		private const string k_ELabelRegulationIcon = "RegulationIcon";

		// Token: 0x0400ADFF RID: 44543
		private const string k_ELabelMobileLoadingIcon = "Loading";

		// Token: 0x0400AE00 RID: 44544
		private const string k_ELabelMobileScroll = "Scroll";

		// Token: 0x0400AE01 RID: 44545
		private GameObject m_MobileLoading;

		// Token: 0x0400AE02 RID: 44546
		private GameObject m_MobileScroll;

		// Token: 0x0400AE03 RID: 44547
		public const string COPY_ENABLE = "copyEnable";

		// Token: 0x0400AE04 RID: 44548
		public const string DELETE_ENABLE = "deleteEnable";

		// Token: 0x0400AE05 RID: 44549
		public const string REGULATION_ENABLE = "regulationEnable";

		// Token: 0x0400AE06 RID: 44550
		public const string REGULATION_ICON_ENABLE = "regulationIconEnable";

		// Token: 0x0400AE07 RID: 44551
		public const string TRIAL_DRAW_ENABLE = "trialDrawEnable";

		// Token: 0x0400AE08 RID: 44552
		public const string HAS_CARD_ENABLE = "hasCardEnable";

		// Token: 0x0400AE09 RID: 44553
		public const string HAS_CARD_IS_ON = "hasCardIsOn";

		// Token: 0x0400AE0A RID: 44554
		public const string FOOTER_MENU_ENABLE = "FooterMenuEnable";

		// Token: 0x0400AE0B RID: 44555
		[SerializeField]
		private ElementObjectManager m_UIPrefab;

		// Token: 0x0400AE0C RID: 44556
		[SerializeField]
		private ElementObjectManager m_UIPrefabMobile;

		// Token: 0x0400AE0D RID: 44557
		private ElementObjectManager m_UI;

		// Token: 0x0400AE0E RID: 44558
		private TextMeshProUGUI m_TitleText;

		// Token: 0x0400AE0F RID: 44559
		private DeckViewWidget m_DeckViewWidget;

		// Token: 0x0400AE10 RID: 44560
		private CardDetailWidget m_DetailWidget;

		// Token: 0x0400AE11 RID: 44561
		private GameObject m_NoItemButton;

		// Token: 0x0400AE12 RID: 44562
		private GameObject m_ScrollBlocker;

		// Token: 0x0400AE13 RID: 44563
		private string m_DeckName;

		// Token: 0x0400AE14 RID: 44564
		private ElementObjectManager m_DeckViewEom;

		// Token: 0x0400AE15 RID: 44565
		private DeckView m_DeckView;

		// Token: 0x0400AE16 RID: 44566
		private TMP_Text m_DeckNameText;

		// Token: 0x0400AE17 RID: 44567
		private const int MAX_COL = 8;

		// Token: 0x0400AE18 RID: 44568
		private int m_Regulation;

		// Token: 0x0400AE19 RID: 44569
		private int m_RentalPool;

		// Token: 0x0400AE1A RID: 44570
		private RegulationSelectSheet m_RegulationSelectSheet;

		// Token: 0x0400AE1B RID: 44571
		private bool m_MonochromeEnable;

		// Token: 0x0400AE1C RID: 44572
		private bool m_PremiumCheckEnable;

		// Token: 0x0400AE1D RID: 44573
		private bool m_RegulationVisible;

		// Token: 0x0400AE1E RID: 44574
		private bool m_RarityVisible;

		// Token: 0x0400AE1F RID: 44575
		private bool m_SortEnable;

		// Token: 0x0400AE20 RID: 44576
		private ShortcutKeySetter m_ShortCutSettings;

		// Token: 0x0400AE21 RID: 44577
		private Dictionary<string, object> m_Accessories;

		// Token: 0x0400AE22 RID: 44578
		private Dictionary<string, object> m_PickCards;

		// Token: 0x0400AE23 RID: 44579
		private int m_NumMainCards;

		// Token: 0x0400AE24 RID: 44580
		private int m_NumExtraCards;

		// Token: 0x0400AE25 RID: 44581
		private int m_NumMainCol;

		// Token: 0x0400AE26 RID: 44582
		private int m_UnimplementedMainNum;

		// Token: 0x0400AE27 RID: 44583
		private bool m_IsContainsUnimplemented;

		// Token: 0x0400AE28 RID: 44584
		private List<bool> m_InitCursorFlags;

		// Token: 0x0400AE29 RID: 44585
		private List<object> m_DeckCardMrks;

		// Token: 0x0400AE2A RID: 44586
		private List<object> m_DeckCardPremiums;

		// Token: 0x0400AE2B RID: 44587
		private List<object> m_MainCardMrks;

		// Token: 0x0400AE2C RID: 44588
		private List<object> m_MainCardPremiums;

		// Token: 0x0400AE2D RID: 44589
		private List<bool> m_MonochromeList;

		// Token: 0x0400AE2E RID: 44590
		private List<CardBaseData> m_MainCardBaseData;

		// Token: 0x0400AE2F RID: 44591
		private List<CardBaseData> m_ExtraCardBaseData;

		// Token: 0x0400AE30 RID: 44592
		public SelectionItem m_ExDeckSelector;

		// Token: 0x0400AE31 RID: 44593
		[NonSerialized]
		public GameObject optionalEmbedObj;

		// Token: 0x0400AE32 RID: 44594
		public Transform optionalAreaLocator;

		// Token: 0x0400AE33 RID: 44595
		[SerializeField]
		private SpriteContainer m_IconCardDB;

		// Token: 0x0400AE34 RID: 44596
		private readonly string k_LabelIconJp;

		// Token: 0x0400AE35 RID: 44597
		private readonly string k_LabelIconUniversal;

		// Token: 0x0400AE36 RID: 44598
		private readonly string k_LabelIconKo;

		// Token: 0x0400AE37 RID: 44599
		[SerializeField]
		private SpriteContainer m_ButtonLSprite;

		// Token: 0x0400AE38 RID: 44600
		private readonly string k_LabelButtonSpriteL;

		// Token: 0x0400AE39 RID: 44601
		private readonly string k_LabelButtonSpriteMobileL;

		// Token: 0x0400AE3A RID: 44602
		private readonly string k_LabelButtonSpriteL_Over;

		// Token: 0x0400AE3B RID: 44603
		private readonly string k_LabelButtonSpriteMobileL_Over;

		// Token: 0x0400AE3C RID: 44604
		private DeckSelectViewController2.DeckEventType m_CopyType;

		// Token: 0x0400AE3D RID: 44605
		private const string k_ELabelCopyType = "copyType";

		// Token: 0x0400AE3E RID: 44606
		private GameObject m_UnCraftableIcon;

		// Token: 0x0400AE3F RID: 44607
		private Content cci;

		// Token: 0x0400AE40 RID: 44608
		private bool m_IsDialog;

		// Token: 0x0400AE41 RID: 44609
		private GameObject m_DialogBG;

		// Token: 0x0400AE42 RID: 44610
		private SelectionButton m_DialogCloseButton;

		// Token: 0x0400AE43 RID: 44611
		private Image m_RegulationIcon;

		// Token: 0x0400AE44 RID: 44612
		private SelectionButton m_RarityToggleButton;

		// Token: 0x0400AE45 RID: 44613
		private GameObject m_RarityToggleOn;

		// Token: 0x0400AE46 RID: 44614
		private GameObject m_RarityToggleOff;

		// Token: 0x0400AE47 RID: 44615
		private DeckBrowserViewController.BrowserType m_BrowserType;

		// Token: 0x0400AE48 RID: 44616
		public Action onPopViewCallback;

		// Token: 0x0400AE49 RID: 44617
		public Action onInitializePickupCardCallback;

		// Token: 0x0400AE4A RID: 44618
		public Action onInitializeTrialDrawCallback;

		// Token: 0x0400AE4B RID: 44619
		public Action<DeckCard, int> onCreatedCardCallback;

		// Token: 0x0400AE4C RID: 44620
		public Func<string, GameObject> createOptionalEmbedObjFunc;

		// Token: 0x0400AE4D RID: 44621
		public Action onCopySccessedCallback;

		// Token: 0x0400AE4E RID: 44622
		public Action onCompleteSelectDeckCallback;

		// Token: 0x02000F8E RID: 3982
		private enum BrowserType
		{
			// Token: 0x0400AE50 RID: 44624
			Solo,
			// Token: 0x0400AE51 RID: 44625
			SoloNPC,
			// Token: 0x0400AE52 RID: 44626
			StructureShop,
			// Token: 0x0400AE53 RID: 44627
			StructureCopy,
			// Token: 0x0400AE54 RID: 44628
			StructureFirst,
			// Token: 0x0400AE55 RID: 44629
			PublicDeck,
			// Token: 0x0400AE56 RID: 44630
			NeuronMyDeck,
			// Token: 0x0400AE57 RID: 44631
			Confirm,
			// Token: 0x0400AE58 RID: 44632
			ConfirmEvent,
			// Token: 0x0400AE59 RID: 44633
			Select,
			// Token: 0x0400AE5A RID: 44634
			SelectRental,
			// Token: 0x0400AE5B RID: 44635
			PickUpSelection,
			// Token: 0x0400AE5C RID: 44636
			OpponentDeck,
			// Token: 0x0400AE5D RID: 44637
			TrialDraw,
			// Token: 0x0400AE5E RID: 44638
			SelectWCSFinal
		}

		// Token: 0x02000F8F RID: 3983
		private class DeckBrowserInfo
		{
			// Token: 0x17000E14 RID: 3604
			// (get) Token: 0x06007528 RID: 29992 RVA: 0x000029CC File Offset: 0x00000BCC
			public int box
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x06007529 RID: 29993 RVA: 0x0000216A File Offset: 0x0000036A
			public static DeckBrowserViewController.DeckBrowserInfo GetInfo(DeckSelectViewController2.DeckEventType deckType, int deckID, int eventID, Dictionary<string, object> args)
			{
				return null;
			}

			// Token: 0x0400AE5F RID: 44639
			public string deckName;

			// Token: 0x0400AE60 RID: 44640
			public Dictionary<string, object> mainCards;

			// Token: 0x0400AE61 RID: 44641
			public Dictionary<string, object> extraCards;

			// Token: 0x0400AE62 RID: 44642
			public Dictionary<string, object> accessory;

			// Token: 0x0400AE63 RID: 44643
			public Dictionary<string, object> pickCards;
		}
	}
}
