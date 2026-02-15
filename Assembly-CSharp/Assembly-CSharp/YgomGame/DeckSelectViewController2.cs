using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YgomGame.Deck;
using YgomGame.Menu;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.InfinityScroll;
using YgomSystem.Utility;

namespace YgomGame
{
	// Token: 0x020007BE RID: 1982
	public class DeckSelectViewController2 : BaseMenuViewController, IDynamicHeaderCustomSupported, IDynamicChangeDispHeaderSupported
	{
		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06003DE4 RID: 15844 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06003DE5 RID: 15845 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override int selectorPriorityAddRange
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06003DE6 RID: 15846 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06003DE7 RID: 15847 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06003DE8 RID: 15848 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x06003DE9 RID: 15849 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(Dictionary<string, object> args = null)
		{
		}

		// Token: 0x06003DEA RID: 15850 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06003DEB RID: 15851 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnResult(ViewController from, object value)
		{
			return false;
		}

		// Token: 0x06003DEC RID: 15852 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnFocusChanged(bool setfocus)
		{
		}

		// Token: 0x06003DED RID: 15853 RVA: 0x0000216D File Offset: 0x0000036D
		private void enterChildMenu(DeckSelectViewController2.ChildMenuAction type)
		{
		}

		// Token: 0x06003DEE RID: 15854 RVA: 0x0000216D File Offset: 0x0000036D
		private void ReturnFromChildMenu()
		{
		}

		// Token: 0x06003DEF RID: 15855 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator DelayResetList(int deckId)
		{
			return null;
		}

		// Token: 0x06003DF0 RID: 15856 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateStructureBadge()
		{
		}

		// Token: 0x06003DF1 RID: 15857 RVA: 0x0000216D File Offset: 0x0000036D
		private void ResetDeckList(int skipIndex)
		{
		}

		// Token: 0x06003DF2 RID: 15858 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateDeckList()
		{
		}

		// Token: 0x06003DF3 RID: 15859 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenPublicDeckSearch()
		{
		}

		// Token: 0x06003DF4 RID: 15860 RVA: 0x0000216D File Offset: 0x0000036D
		private void GetNeuronToken()
		{
		}

		// Token: 0x06003DF5 RID: 15861 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenNeuronDeckSearch(bool isFirst = false)
		{
		}

		// Token: 0x06003DF6 RID: 15862 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenStructureDeckCopy()
		{
		}

		// Token: 0x06003DF7 RID: 15863 RVA: 0x0000216D File Offset: 0x0000036D
		private void CreateNewDeck()
		{
		}

		// Token: 0x06003DF8 RID: 15864 RVA: 0x0000216D File Offset: 0x0000036D
		private void GetDeckList(DeckSelectViewController2.DeckReference deckRef, Action<DeckSelectViewController2.DeckReference> onCompleteAction)
		{
		}

		// Token: 0x06003DF9 RID: 15865 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenOutOfTermDialog()
		{
		}

		// Token: 0x06003DFA RID: 15866 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenEditDeck(DeckSelectViewController2.DeckReference deckRef)
		{
		}

		// Token: 0x06003DFB RID: 15867 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenDeckEditView(DeckSelectViewController2.DeckReference deckRef)
		{
		}

		// Token: 0x06003DFC RID: 15868 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenConfirmDeckBrowser(DeckSelectViewController2.DeckReference deckRef)
		{
		}

		// Token: 0x06003DFD RID: 15869 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenConfirmBrowser(DeckSelectViewController2.DeckReference deckRef)
		{
		}

		// Token: 0x06003DFE RID: 15870 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenSelectDeckBrowser(DeckSelectViewController2.DeckReference deckRef)
		{
		}

		// Token: 0x06003DFF RID: 15871 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenSelectBrowser(DeckSelectViewController2.DeckReference deckRef)
		{
		}

		// Token: 0x06003E00 RID: 15872 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenAccessoryEdit(DeckSelectViewController2.DeckReference deckRef)
		{
		}

		// Token: 0x06003E01 RID: 15873 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializeDeckData()
		{
		}

		// Token: 0x06003E02 RID: 15874 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializeExhibitionInfo()
		{
		}

		// Token: 0x06003E03 RID: 15875 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializeExhibitionDeckData()
		{
		}

		// Token: 0x06003E04 RID: 15876 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializeCupInfo()
		{
		}

		// Token: 0x06003E05 RID: 15877 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializeCupDeckData()
		{
		}

		// Token: 0x06003E06 RID: 15878 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializeWcsInfo()
		{
		}

		// Token: 0x06003E07 RID: 15879 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializeWcsDeckData()
		{
		}

		// Token: 0x06003E08 RID: 15880 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializeRankEventInfo()
		{
		}

		// Token: 0x06003E09 RID: 15881 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializeRankEventDeckData()
		{
		}

		// Token: 0x06003E0A RID: 15882 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializeDuelTrialInfo()
		{
		}

		// Token: 0x06003E0B RID: 15883 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializeDuelTrialDeckData()
		{
		}

		// Token: 0x06003E0C RID: 15884 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializeVersusInfo()
		{
		}

		// Token: 0x06003E0D RID: 15885 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializeVersusDeckData()
		{
		}

		// Token: 0x06003E0E RID: 15886 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializeMyDeckData()
		{
		}

		// Token: 0x06003E0F RID: 15887 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializeRentalDeckData()
		{
		}

		// Token: 0x06003E10 RID: 15888 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializeDuelTrialRentalDeckData()
		{
		}

		// Token: 0x06003E11 RID: 15889 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializeVersusRentalDeckData()
		{
		}

		// Token: 0x06003E12 RID: 15890 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateDeckNum()
		{
		}

		// Token: 0x06003E13 RID: 15891 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateEventDeckNum()
		{
		}

		// Token: 0x06003E14 RID: 15892 RVA: 0x0000216D File Offset: 0x0000036D
		private void DispPickupCards()
		{
		}

		// Token: 0x06003E15 RID: 15893 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetActionLabels(DeckSelectViewController2.DeckReference deckRef)
		{
		}

		// Token: 0x06003E16 RID: 15894 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetActionCallBacks()
		{
		}

		// Token: 0x06003E17 RID: 15895 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateTemplateList()
		{
		}

		// Token: 0x06003E18 RID: 15896 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnItemInitialize(GameObject gob)
		{
		}

		// Token: 0x06003E19 RID: 15897 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnGsvStanby()
		{
		}

		// Token: 0x06003E1A RID: 15898 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnItemSetData(GameObject gob, int dataindex)
		{
		}

		// Token: 0x06003E1B RID: 15899 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClick(DeckSelectViewController2.DeckReference deckRef)
		{
		}

		// Token: 0x06003E1C RID: 15900 RVA: 0x0000216D File Offset: 0x0000036D
		private void BulkDecksDeletion()
		{
		}

		// Token: 0x06003E1D RID: 15901 RVA: 0x0000216A File Offset: 0x0000036A
		private UnityAction GetDeckActionCallBack(int i)
		{
			return null;
		}

		// Token: 0x06003E1E RID: 15902 RVA: 0x0000216A File Offset: 0x0000036A
		private GameObject CreateEmbedObj(DeckSelectViewController2.DeckReference deckRef)
		{
			return null;
		}

		// Token: 0x06003E1F RID: 15903 RVA: 0x0000216D File Offset: 0x0000036D
		private void DeleteDecks()
		{
		}

		// Token: 0x06003E20 RID: 15904 RVA: 0x000029CC File Offset: 0x00000BCC
		public HeaderViewController.IsDispHeader IsDispContents()
		{
			return (HeaderViewController.IsDispHeader)0;
		}

		// Token: 0x04003674 RID: 13940
		protected DeckSelectViewController2.SelectMode m_SelectMode;

		// Token: 0x04003675 RID: 13941
		private int m_ExhibitionID;

		// Token: 0x04003676 RID: 13942
		private int m_ExhibitionRentalID;

		// Token: 0x04003677 RID: 13943
		private int m_CupID;

		// Token: 0x04003678 RID: 13944
		private int m_WcsID;

		// Token: 0x04003679 RID: 13945
		private int m_WcsFinalID;

		// Token: 0x0400367A RID: 13946
		private int m_RankEventID;

		// Token: 0x0400367B RID: 13947
		private int m_DuelTrialID;

		// Token: 0x0400367C RID: 13948
		private int m_DuelTrialRentalID;

		// Token: 0x0400367D RID: 13949
		private int m_VersusID;

		// Token: 0x0400367E RID: 13950
		private int m_VersusRentalID;

		// Token: 0x0400367F RID: 13951
		private int m_RegulationID;

		// Token: 0x04003680 RID: 13952
		private int m_EventDeckID;

		// Token: 0x04003681 RID: 13953
		private InfinityScrollView m_ScrollView;

		// Token: 0x04003682 RID: 13954
		private readonly string k_ELabelHederAreaMenu;

		// Token: 0x04003683 RID: 13955
		private readonly string k_ELabelTextHeadline;

		// Token: 0x04003684 RID: 13956
		private readonly string k_ELabelDeckNum;

		// Token: 0x04003685 RID: 13957
		private readonly string k_ELabelTextDeckNum;

		// Token: 0x04003686 RID: 13958
		private readonly string k_ELabelTounamentDeckNum;

		// Token: 0x04003687 RID: 13959
		private readonly string k_ELabelTextTounamentDeckNum;

		// Token: 0x04003688 RID: 13960
		private readonly string k_ELabelBulkDecksDeletionButton;

		// Token: 0x04003689 RID: 13961
		private readonly string k_ELabelBulkDecksDeletionTmp;

		// Token: 0x0400368A RID: 13962
		private readonly string k_ELabelBulkDecksDeletionShortcut;

		// Token: 0x0400368B RID: 13963
		private readonly string k_ELabelDeleteExecutionButton;

		// Token: 0x0400368C RID: 13964
		private readonly string k_ELabelOpenNeuronDecksButton;

		// Token: 0x0400368D RID: 13965
		private readonly string k_ELabelOpenNeuronDecksButtonIcon;

		// Token: 0x0400368E RID: 13966
		private readonly string k_LabelJpLogoButton;

		// Token: 0x0400368F RID: 13967
		private readonly string k_LabelUniversalLogoButton;

		// Token: 0x04003690 RID: 13968
		private readonly string k_LabelKoLogoButton;

		// Token: 0x04003691 RID: 13969
		protected Transform m_HeaderArea;

		// Token: 0x04003692 RID: 13970
		protected TextMeshProUGUI m_TextHeadline;

		// Token: 0x04003693 RID: 13971
		private Transform m_DeckNum;

		// Token: 0x04003694 RID: 13972
		private TextMeshProUGUI m_TextDeckNum;

		// Token: 0x04003695 RID: 13973
		private Transform m_TounamentDeckNum;

		// Token: 0x04003696 RID: 13974
		private TextMeshProUGUI m_TextTounamentDeckNum;

		// Token: 0x04003697 RID: 13975
		private bool dispPickCards;

		// Token: 0x04003698 RID: 13976
		protected ElementObjectManager m_BulkDecksDeletionEom;

		// Token: 0x04003699 RID: 13977
		protected SelectionButton m_BulkDecksDeletionButton;

		// Token: 0x0400369A RID: 13978
		private SelectionButton m_DeleteExecutionButton;

		// Token: 0x0400369B RID: 13979
		protected TextMeshProUGUI m_BulkDecksDeletionTmp;

		// Token: 0x0400369C RID: 13980
		[SerializeField]
		private SpriteContainer m_ButtonIconContainer;

		// Token: 0x0400369D RID: 13981
		private Image m_NeuronLogoIconButton;

		// Token: 0x0400369E RID: 13982
		private readonly string k_ELabelFooterMenu;

		// Token: 0x0400369F RID: 13983
		private readonly string k_ELabelPublicDeckButton;

		// Token: 0x040036A0 RID: 13984
		private readonly string k_ELabelStructureDeckCopyButton;

		// Token: 0x040036A1 RID: 13985
		public const string k_ArgKeyGameMode = "GameMode";

		// Token: 0x040036A2 RID: 13986
		public const string k_ArgKeyExhibitionID = "ExhibitionID";

		// Token: 0x040036A3 RID: 13987
		public const string k_ArgKeyRentalID = "RentalID";

		// Token: 0x040036A4 RID: 13988
		public const string k_ArgKeyCupID = "CupID";

		// Token: 0x040036A5 RID: 13989
		public const string k_ArgKeyWcsID = "WcsID";

		// Token: 0x040036A6 RID: 13990
		public const string k_ArgKeyWcsFinalID = "WcsFinalID";

		// Token: 0x040036A7 RID: 13991
		public const string k_ArgKeyRankEventID = "RankEventID";

		// Token: 0x040036A8 RID: 13992
		public const string k_ArgKeyDuelTrialID = "DuelTrialID";

		// Token: 0x040036A9 RID: 13993
		public const string k_ArgKeyDuelTrialRentalID = "DuelTrialRentalID";

		// Token: 0x040036AA RID: 13994
		public const string k_ArgKeyVersusID = "VersusID";

		// Token: 0x040036AB RID: 13995
		public const string k_ArgKeyVersusRentalID = "VersusRentalID";

		// Token: 0x040036AC RID: 13996
		public const string k_ArgKeyEventDeckID = "EventDeckID";

		// Token: 0x040036AD RID: 13997
		public const string k_ArgKeyRegulationID = "RegulationID";

		// Token: 0x040036AE RID: 13998
		private readonly string k_ELabelPickupCardButton;

		// Token: 0x040036AF RID: 13999
		protected Transform m_FooterArea;

		// Token: 0x040036B0 RID: 14000
		private SelectionButton m_PublicDeckButton;

		// Token: 0x040036B1 RID: 14001
		private SelectionButton m_StructureDeckCopyButton;

		// Token: 0x040036B2 RID: 14002
		private readonly string k_ELabelNewStructureBadge;

		// Token: 0x040036B3 RID: 14003
		private GameObject m_StructureBadge;

		// Token: 0x040036B4 RID: 14004
		private SelectionButton m_OpenNeuronDecksButton;

		// Token: 0x040036B5 RID: 14005
		private SelectionButton m_PickupCardButton;

		// Token: 0x040036B6 RID: 14006
		private ElementObjectManager m_PickupCardButtonEom;

		// Token: 0x040036B7 RID: 14007
		private Transform m_PickupCardButtonOn;

		// Token: 0x040036B8 RID: 14008
		private Transform m_PickupCardButtonOff;

		// Token: 0x040036B9 RID: 14009
		public const string PREFAB_PATH_DECKEDIT_VC = "DeckEdit/DeckEdit";

		// Token: 0x040036BA RID: 14010
		public const string PREFAB_PATH_CARDDIRECTORY_VC = "DeckEdit/CardDirectory";

		// Token: 0x040036BB RID: 14011
		private const string Label_BGM = "BGM_MENU_01";

		// Token: 0x040036BC RID: 14012
		private readonly string k_ALabelOverview;

		// Token: 0x040036BD RID: 14013
		private ElementObjectManager DeckOverviewPrefab;

		// Token: 0x040036BE RID: 14014
		private List<string> m_DeckActionDialogButtonLabels;

		// Token: 0x040036BF RID: 14015
		private Dictionary<string, UnityAction> m_DeckActionDialogCallBacks;

		// Token: 0x040036C0 RID: 14016
		private Dictionary<int, DeckSelectViewController2.TournamentReference> m_Exhibitions;

		// Token: 0x040036C1 RID: 14017
		private DeckSelectViewController2.TournamentReference m_Cup;

		// Token: 0x040036C2 RID: 14018
		private DeckSelectViewController2.TournamentReference m_Wcs;

		// Token: 0x040036C3 RID: 14019
		private Dictionary<int, DeckSelectViewController2.TournamentReference> m_RankEvents;

		// Token: 0x040036C4 RID: 14020
		private Dictionary<int, DeckSelectViewController2.TournamentReference> m_DuelTrials;

		// Token: 0x040036C5 RID: 14021
		private Dictionary<KeyValuePair<int, int>, DeckSelectViewController2.TournamentReference> m_VSs;

		// Token: 0x040036C6 RID: 14022
		protected List<DeckSelectViewController2.DeckReference> m_Decks;

		// Token: 0x040036C7 RID: 14023
		private List<int> m_TemplateList;

		// Token: 0x040036C8 RID: 14024
		protected Dictionary<KeyValuePair<DeckSelectViewController2.DeckEventType, int>, DeckBox> m_DeckUIs;

		// Token: 0x040036C9 RID: 14025
		private DeckSelectViewController2.ChildMenuAction m_currentMenu;

		// Token: 0x040036CA RID: 14026
		private bool m_firstFocusPassed;

		// Token: 0x040036CB RID: 14027
		private int lastSet;

		// Token: 0x040036CC RID: 14028
		private DeckSelectViewController2.DeckEventType m_backupDeckType;

		// Token: 0x040036CD RID: 14029
		private bool m_isLinkageCgdbDeck;

		// Token: 0x040036CE RID: 14030
		private const int BulkDecksLimit = 10;

		// Token: 0x040036CF RID: 14031
		private List<int> m_SelectedDecks;

		// Token: 0x040036D0 RID: 14032
		private Dictionary<string, List<Tween>> m_HeaderTweens;

		// Token: 0x040036D1 RID: 14033
		private Dictionary<string, List<Tween>> m_FooterTweens;

		// Token: 0x040036D2 RID: 14034
		private readonly string k_LabelHideTween;

		// Token: 0x040036D3 RID: 14035
		private readonly string k_LabelShowTween;

		// Token: 0x020007BF RID: 1983
		public enum DeckCondition
		{
			// Token: 0x040036D5 RID: 14037
			New,
			// Token: 0x040036D6 RID: 14038
			Existing
		}

		// Token: 0x020007C0 RID: 1984
		public enum SelectMode
		{
			// Token: 0x040036D8 RID: 14040
			Default,
			// Token: 0x040036D9 RID: 14041
			Ranked,
			// Token: 0x040036DA RID: 14042
			PVE,
			// Token: 0x040036DB RID: 14043
			Tournament,
			// Token: 0x040036DC RID: 14044
			Solo,
			// Token: 0x040036DD RID: 14045
			Room,
			// Token: 0x040036DE RID: 14046
			Exhibition,
			// Token: 0x040036DF RID: 14047
			Rental,
			// Token: 0x040036E0 RID: 14048
			Free,
			// Token: 0x040036E1 RID: 14049
			Cup,
			// Token: 0x040036E2 RID: 14050
			Wcs,
			// Token: 0x040036E3 RID: 14051
			WcsFinal,
			// Token: 0x040036E4 RID: 14052
			RankEvent,
			// Token: 0x040036E5 RID: 14053
			TeamMatch,
			// Token: 0x040036E6 RID: 14054
			BulkDecksDeletion,
			// Token: 0x040036E7 RID: 14055
			DuelTrial,
			// Token: 0x040036E8 RID: 14056
			DuelTrialRental,
			// Token: 0x040036E9 RID: 14057
			Versus,
			// Token: 0x040036EA RID: 14058
			VersusRental
		}

		// Token: 0x020007C1 RID: 1985
		public enum DeckEventType
		{
			// Token: 0x040036EC RID: 14060
			NewDeck,
			// Token: 0x040036ED RID: 14061
			MyDeck,
			// Token: 0x040036EE RID: 14062
			Neuron,
			// Token: 0x040036EF RID: 14063
			TournamentDeck,
			// Token: 0x040036F0 RID: 14064
			ExhibitionDeck,
			// Token: 0x040036F1 RID: 14065
			ExhibitionRentalDeck,
			// Token: 0x040036F2 RID: 14066
			CupDeck,
			// Token: 0x040036F3 RID: 14067
			WcsDeck,
			// Token: 0x040036F4 RID: 14068
			WcsFinalDeck,
			// Token: 0x040036F5 RID: 14069
			RankEventDeck,
			// Token: 0x040036F6 RID: 14070
			DuelTrialDeck,
			// Token: 0x040036F7 RID: 14071
			DuelTrialRentalDeck,
			// Token: 0x040036F8 RID: 14072
			VersusDeck,
			// Token: 0x040036F9 RID: 14073
			VersusRentalDeck
		}

		// Token: 0x020007C2 RID: 1986
		private class TournamentReference
		{
			// Token: 0x040036FA RID: 14074
			public int id;

			// Token: 0x040036FB RID: 14075
			public int logoId;

			// Token: 0x040036FC RID: 14076
			public long end_ts;

			// Token: 0x040036FD RID: 14077
			public long res_ts;

			// Token: 0x040036FE RID: 14078
			public bool is_fixed_accessory;

			// Token: 0x040036FF RID: 14079
			public bool is_fixed_pick_cards;

			// Token: 0x04003700 RID: 14080
			public int rentalPoolId;
		}

		// Token: 0x020007C3 RID: 1987
		private enum ChildMenuAction
		{
			// Token: 0x04003702 RID: 14082
			None,
			// Token: 0x04003703 RID: 14083
			CreateDeck,
			// Token: 0x04003704 RID: 14084
			EditDeck,
			// Token: 0x04003705 RID: 14085
			DeckBrowser,
			// Token: 0x04003706 RID: 14086
			ModifyDeck,
			// Token: 0x04003707 RID: 14087
			RemoveDeck,
			// Token: 0x04003708 RID: 14088
			PublicDeckSearch,
			// Token: 0x04003709 RID: 14089
			StructureDeckCopy,
			// Token: 0x0400370A RID: 14090
			EditCopyDeck,
			// Token: 0x0400370B RID: 14091
			CardList,
			// Token: 0x0400370C RID: 14092
			NeuronMyDecks,
			// Token: 0x0400370D RID: 14093
			BulkDecksDeletion
		}

		// Token: 0x020007C4 RID: 1988
		protected internal class DeckReference
		{
			// Token: 0x06003E23 RID: 15907 RVA: 0x000F4660 File Offset: 0x000F2860
			public KeyValuePair<DeckSelectViewController2.DeckEventType, int> GetUIKey()
			{
				return default(KeyValuePair<DeckSelectViewController2.DeckEventType, int>);
			}

			// Token: 0x06003E24 RID: 15908 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetAccessory(Dictionary<string, object> accessory)
			{
			}

			// Token: 0x06003E25 RID: 15909 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetPickUp(int[] pickId, int[] pickDeco)
			{
			}

			// Token: 0x06003E26 RID: 15910 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetPickUp(Dictionary<string, object> pickupDict)
			{
			}

			// Token: 0x06003E27 RID: 15911 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetTimes(long editTime, long createTime)
			{
			}

			// Token: 0x06003E28 RID: 15912 RVA: 0x0000216A File Offset: 0x0000036A
			public Dictionary<string, object> GetAccessory()
			{
				return null;
			}

			// Token: 0x06003E29 RID: 15913 RVA: 0x0000216A File Offset: 0x0000036A
			public Dictionary<string, object> GetPickCards()
			{
				return null;
			}

			// Token: 0x0400370E RID: 14094
			public int deckID;

			// Token: 0x0400370F RID: 14095
			public string name;

			// Token: 0x04003710 RID: 14096
			public DeckSelectViewController2.DeckEventType deckType;

			// Token: 0x04003711 RID: 14097
			public int caseID;

			// Token: 0x04003712 RID: 14098
			public int protectorID;

			// Token: 0x04003713 RID: 14099
			public int fieldID;

			// Token: 0x04003714 RID: 14100
			public int objectID;

			// Token: 0x04003715 RID: 14101
			public int mateBaseID;

			// Token: 0x04003716 RID: 14102
			public int[] pickUpIDs;

			// Token: 0x04003717 RID: 14103
			public int[] pickUpDecos;

			// Token: 0x04003718 RID: 14104
			public long et;

			// Token: 0x04003719 RID: 14105
			public long ct;

			// Token: 0x0400371A RID: 14106
			public long endTime;

			// Token: 0x0400371B RID: 14107
			public long resTime;

			// Token: 0x0400371C RID: 14108
			public bool isFixedAccessories;

			// Token: 0x0400371D RID: 14109
			public bool isFixedPickCards;

			// Token: 0x0400371E RID: 14110
			public int logoID;

			// Token: 0x0400371F RID: 14111
			public int regID;

			// Token: 0x04003720 RID: 14112
			public int stage;

			// Token: 0x04003721 RID: 14113
			public int eventID;

			// Token: 0x04003722 RID: 14114
			public int rentalPoolID;
		}
	}
}
