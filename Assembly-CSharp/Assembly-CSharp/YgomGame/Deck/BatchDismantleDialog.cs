using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Card;
using YgomGame.Menu;
using YgomSystem.UI;
using YgomSystem.YGomTMPro;

namespace YgomGame.Deck
{
	// Token: 0x02000F9A RID: 3994
	public class BatchDismantleDialog : SelectDialogViewControllerBase<bool>, IBokeSupported
	{
		// Token: 0x06007593 RID: 30099 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializeElements()
		{
		}

		// Token: 0x06007594 RID: 30100 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(Action<bool> callback = null)
		{
		}

		// Token: 0x06007595 RID: 30101 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(Dictionary<string, object> args = null)
		{
		}

		// Token: 0x06007596 RID: 30102 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06007597 RID: 30103 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitCompensation()
		{
		}

		// Token: 0x06007598 RID: 30104 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06007599 RID: 30105 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitilizeCreateMode()
		{
		}

		// Token: 0x0600759A RID: 30106 RVA: 0x0000216D File Offset: 0x0000036D
		private void Dismauntle()
		{
		}

		// Token: 0x0600759B RID: 30107 RVA: 0x0000216D File Offset: 0x0000036D
		private void Create()
		{
		}

		// Token: 0x0600759C RID: 30108 RVA: 0x0000216A File Offset: 0x0000036A
		private List<CardBaseData> FormatCreateCards(Dictionary<int, int> lackCards)
		{
			return null;
		}

		// Token: 0x0600759D RID: 30109 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool CompensationCheck(Dictionary<string, object> data)
		{
			return false;
		}

		// Token: 0x0400AEE2 RID: 44770
		private const string PREFAB_PATH_BATCHDISMANTLEDIALOG = "DeckEdit/BatchDismantleDialog";

		// Token: 0x0400AEE3 RID: 44771
		private const string LABEL_SBN_CANCELBUTTON = "ButtonFooter0";

		// Token: 0x0400AEE4 RID: 44772
		private const string LABEL_SBN_DISMANTLEBUTTON = "ButtonFooter1";

		// Token: 0x0400AEE5 RID: 44773
		private const string LABEL_EOM_TEMPLATE = "Template";

		// Token: 0x0400AEE6 RID: 44774
		private const string LABEL_TXT_CANCELBUTTONLABEL = "TextButtonFooter0";

		// Token: 0x0400AEE7 RID: 44775
		private const string LABEL_TXT_DISMANTLEBUTTONLABEL = "TextButtonFooter1";

		// Token: 0x0400AEE8 RID: 44776
		private const string LABEL_TXT_POINTSGAINEDHEADER = "TextGetCP";

		// Token: 0x0400AEE9 RID: 44777
		private const string LABEL_TXT_NPOINTSGAINED = "TextGetCPNum0";

		// Token: 0x0400AEEA RID: 44778
		private const string LABEL_TXT_RPOINTSGAINED = "TextGetCPNum1";

		// Token: 0x0400AEEB RID: 44779
		private const string LABEL_TXT_SRPOINTSGAINED = "TextGetCPNum2";

		// Token: 0x0400AEEC RID: 44780
		private const string LABEL_TXT_URPOINTSGAINED = "TextGetCPNum3";

		// Token: 0x0400AEED RID: 44781
		private const string LABEL_TXT_TOTALPOINTSHEADER = "TextCP";

		// Token: 0x0400AEEE RID: 44782
		private const string LABEL_TXT_NTOTALPOINTS = "TextCPNum0";

		// Token: 0x0400AEEF RID: 44783
		private const string LABEL_TXT_RTOTALPOINTS = "TextCPNum1";

		// Token: 0x0400AEF0 RID: 44784
		private const string LABEL_TXT_SRTOTALPOINTS = "TextCPNum2";

		// Token: 0x0400AEF1 RID: 44785
		private const string LABEL_TXT_URTOTALPOINTS = "TextCPNum3";

		// Token: 0x0400AEF2 RID: 44786
		private const string LABEL_TXT_DESC0 = "TextDescription0";

		// Token: 0x0400AEF3 RID: 44787
		private const string LABEL_TXT_DESC1 = "TextDescription1";

		// Token: 0x0400AEF4 RID: 44788
		private const string LABEL_TXT_DESC2 = "TextDescription2";

		// Token: 0x0400AEF5 RID: 44789
		private const string LABEL_TXT_DIALOGTITLE = "TextTitle";

		// Token: 0x0400AEF6 RID: 44790
		private const string LABEL_RT_LISTAREA = "ListArea";

		// Token: 0x0400AEF7 RID: 44791
		private const string LABEL_TXT_TEXTNUM = "TextCardNum";

		// Token: 0x0400AEF8 RID: 44792
		private const string LABEL_TXT_TEXTRARITY = "TextRarity";

		// Token: 0x0400AEF9 RID: 44793
		private ExtendedTextMeshProUGUI m_CancelButtonLabel;

		// Token: 0x0400AEFA RID: 44794
		private ExtendedTextMeshProUGUI m_DismanlteButtonLabel;

		// Token: 0x0400AEFB RID: 44795
		private ExtendedTextMeshProUGUI m_PointsGainedHeader;

		// Token: 0x0400AEFC RID: 44796
		private ExtendedTextMeshProUGUI m_NPointsGained;

		// Token: 0x0400AEFD RID: 44797
		private ExtendedTextMeshProUGUI m_RPointsGained;

		// Token: 0x0400AEFE RID: 44798
		private ExtendedTextMeshProUGUI m_SRPointsGained;

		// Token: 0x0400AEFF RID: 44799
		private ExtendedTextMeshProUGUI m_URPointsGained;

		// Token: 0x0400AF00 RID: 44800
		private ExtendedTextMeshProUGUI m_TotalPointsHeader;

		// Token: 0x0400AF01 RID: 44801
		private ExtendedTextMeshProUGUI m_NTotalPoints;

		// Token: 0x0400AF02 RID: 44802
		private ExtendedTextMeshProUGUI m_RTotalPoints;

		// Token: 0x0400AF03 RID: 44803
		private ExtendedTextMeshProUGUI m_SRTotalPoints;

		// Token: 0x0400AF04 RID: 44804
		private ExtendedTextMeshProUGUI m_URTotalPoints;

		// Token: 0x0400AF05 RID: 44805
		private ExtendedTextMeshProUGUI m_Desc0;

		// Token: 0x0400AF06 RID: 44806
		private ExtendedTextMeshProUGUI m_Desc1;

		// Token: 0x0400AF07 RID: 44807
		private ExtendedTextMeshProUGUI m_Desc2;

		// Token: 0x0400AF08 RID: 44808
		private ExtendedTextMeshProUGUI m_DialogTitle;

		// Token: 0x0400AF09 RID: 44809
		private SelectionButton m_DismantleButton;

		// Token: 0x0400AF0A RID: 44810
		private SelectionButton m_CancelButton;

		// Token: 0x0400AF0B RID: 44811
		private RectTransform m_ListArea;

		// Token: 0x0400AF0C RID: 44812
		private Dictionary<int, CraftCompensation> m_CraftCompensations;

		// Token: 0x0400AF0D RID: 44813
		private Dictionary<string, object> m_Compensations;

		// Token: 0x0400AF0E RID: 44814
		private Dictionary<int, int> m_CompensationsRarities;

		// Token: 0x0400AF0F RID: 44815
		private List<int> m_CompensationIds;

		// Token: 0x0400AF10 RID: 44816
		public const string k_ArgKeyDialogTitle = "title";

		// Token: 0x0400AF11 RID: 44817
		public const string k_ArgKeyDismantleCards = "dismantleCards";

		// Token: 0x0400AF12 RID: 44818
		public const string k_ArgKeyOnCompleteCallback = "onCompleteCallback";

		// Token: 0x0400AF13 RID: 44819
		public const string k_ArgKeyCreateCards = "createCards";

		// Token: 0x0400AF14 RID: 44820
		public const string k_ArgKeyCreateCardsMessage = "createCardsMessage";

		// Token: 0x0400AF15 RID: 44821
		private const string CP_TEXT_MAX = "99999+";

		// Token: 0x0400AF16 RID: 44822
		private const string CP_TEXT_MIN = "0";

		// Token: 0x0400AF17 RID: 44823
		private string m_Title;

		// Token: 0x0400AF18 RID: 44824
		private List<CardBaseData> m_DismantleCards;

		// Token: 0x0400AF19 RID: 44825
		private Action<bool> OnCompleteCallback;

		// Token: 0x0400AF1A RID: 44826
		private Dictionary<int, int> m_CreateCards;

		// Token: 0x0400AF1B RID: 44827
		private string m_CreatedCardsMessage;

		// Token: 0x0400AF1C RID: 44828
		private Dictionary<string, object> formatedCards;

		// Token: 0x0400AF1D RID: 44829
		private bool isSelectMode;

		// Token: 0x0400AF1E RID: 44830
		private bool isCreateMode;

		// Token: 0x0400AF1F RID: 44831
		private Dictionary<CardCollectionInfo.Rarity, bool> SufficientCheck;
	}
}
