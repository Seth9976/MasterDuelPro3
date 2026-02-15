using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using YgomGame.Card;
using YgomGame.Menu;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.Deck
{
	// Token: 0x02000FB0 RID: 4016
	public class CardCraftDialog : SelectDialogViewControllerBase<CardCraftDialog.CraftMode, int, CardCollectionInfo.Premium, int>, IBokeSupported
	{
		// Token: 0x17000EBF RID: 3775
		// (get) Token: 0x0600776A RID: 30570 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600776B RID: 30571 RVA: 0x0000216D File Offset: 0x0000036D
		private int m_CurrentCardID
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000EC0 RID: 3776
		// (get) Token: 0x0600776C RID: 30572 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600776D RID: 30573 RVA: 0x0000216D File Offset: 0x0000036D
		private CardCollectionInfo.Premium m_CurrentPremium
		{
			[CompilerGenerated]
			get
			{
				return (CardCollectionInfo.Premium)0;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x0600776E RID: 30574 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializeElements()
		{
		}

		// Token: 0x0600776F RID: 30575 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(CardCraftDialog.CraftMode mode, int cardID, CardCollectionInfo.Premium prem, Action<int, int> callback = null)
		{
		}

		// Token: 0x06007770 RID: 30576 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06007771 RID: 30577 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06007772 RID: 30578 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetCraftNum(int craftNum)
		{
		}

		// Token: 0x06007773 RID: 30579 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetNumPremiums()
		{
		}

		// Token: 0x06007774 RID: 30580 RVA: 0x0000216D File Offset: 0x0000036D
		private void CompensationCheck()
		{
		}

		// Token: 0x0400AFEB RID: 45035
		private const string PREFAB_PATH_CARDCRAFTDIALOG = "DeckEdit/CraftDialog";

		// Token: 0x0400AFEC RID: 45036
		private CardCraftDialog.CraftMode mode;

		// Token: 0x0400AFED RID: 45037
		private int currentCardNum;

		// Token: 0x0400AFEE RID: 45038
		private int currentCraftPoint;

		// Token: 0x0400AFEF RID: 45039
		private int createPoint;

		// Token: 0x0400AFF0 RID: 45040
		private int dismantlePoint;

		// Token: 0x0400AFF1 RID: 45041
		private int craftNum;

		// Token: 0x0400AFF2 RID: 45042
		private bool isCompensation;

		// Token: 0x0400AFF3 RID: 45043
		private int compensationId;

		// Token: 0x0400AFF4 RID: 45044
		private int compensationPoint;

		// Token: 0x0400AFF5 RID: 45045
		private int compensationNumMax;

		// Token: 0x0400AFF6 RID: 45046
		private string strEndTime;

		// Token: 0x0400AFF7 RID: 45047
		private Dictionary<int, CraftCompensation> m_CraftCompensations;

		// Token: 0x0400AFF8 RID: 45048
		private Dictionary<string, object> m_Compensations;

		// Token: 0x0400AFF9 RID: 45049
		private const string LABEL_SBN_CANCELBUTTON = "ButtonFooter0";

		// Token: 0x0400AFFA RID: 45050
		private const string LABEL_SBN_CRAFTBUTTON = "ButtonFooter1";

		// Token: 0x0400AFFB RID: 45051
		private const string LABEL_TXT_CRAFTBUTTONLABEL = "TextButtonFooter1";

		// Token: 0x0400AFFC RID: 45052
		private const string LABEL_TXT_CANCELBUTTONLABEL = "TextButtonFooter0";

		// Token: 0x0400AFFD RID: 45053
		private const string LABEL_TXT_CONFIRMATIONPROMPT = "TextDescription";

		// Token: 0x0400AFFE RID: 45054
		private const string LABEL_TXT_CONFIRMATIONMESSAGE1 = "TextMessage1";

		// Token: 0x0400AFFF RID: 45055
		private const string LABEL_TXT_CONFIRMATIONMESSAGE2 = "TextMessage2";

		// Token: 0x0400B000 RID: 45056
		private const string LABEL_TXT_DIALOGTITLE = "TextTitle";

		// Token: 0x0400B001 RID: 45057
		private TMP_Text m_Title;

		// Token: 0x0400B002 RID: 45058
		private TMP_Text m_CraftPrompt;

		// Token: 0x0400B003 RID: 45059
		private TMP_Text m_CraftMessage1;

		// Token: 0x0400B004 RID: 45060
		private TMP_Text m_CraftMessage2;

		// Token: 0x0400B005 RID: 45061
		private TMP_Text m_CraftButtonLabel;

		// Token: 0x0400B006 RID: 45062
		private TMP_Text m_CancelButtonLabel;

		// Token: 0x0400B007 RID: 45063
		private SelectionButton m_CraftButton;

		// Token: 0x0400B008 RID: 45064
		private SelectionButton m_CancelButton;

		// Token: 0x0400B009 RID: 45065
		private const int CREATE_NUM_MIN = 1;

		// Token: 0x0400B00A RID: 45066
		private const int CREATE_NUM_MAX = 3;

		// Token: 0x0400B00B RID: 45067
		private const int DISMANTLE_NUM_MIN = 1;

		// Token: 0x0400B00C RID: 45068
		private const int DISMANTLE_NUM_MAX = 99;

		// Token: 0x0400B00D RID: 45069
		private Action<int, int> OnDicidedCallback;

		// Token: 0x0400B00E RID: 45070
		private CardCraftDialog.CraftGroup m_CraftGroup;

		// Token: 0x02000FB1 RID: 4017
		public enum CraftMode
		{
			// Token: 0x0400B010 RID: 45072
			Create,
			// Token: 0x0400B011 RID: 45073
			Dismantle
		}

		// Token: 0x02000FB2 RID: 4018
		private class CraftGroup : ElementWidget
		{
			// Token: 0x17000EC1 RID: 3777
			// (get) Token: 0x06007776 RID: 30582 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007777 RID: 30583 RVA: 0x0000216D File Offset: 0x0000036D
			public SelectionButton m_IncrementButton
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x17000EC2 RID: 3778
			// (get) Token: 0x06007778 RID: 30584 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007779 RID: 30585 RVA: 0x0000216D File Offset: 0x0000036D
			public SelectionButton m_DecrementButton
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x17000EC3 RID: 3779
			// (get) Token: 0x0600777A RID: 30586 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x0600777B RID: 30587 RVA: 0x0000216D File Offset: 0x0000036D
			public TMP_Text m_TextNum
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x17000EC4 RID: 3780
			// (get) Token: 0x0600777C RID: 30588 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x0600777D RID: 30589 RVA: 0x0000216D File Offset: 0x0000036D
			public TMP_Text m_TextBeforeNum
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x17000EC5 RID: 3781
			// (get) Token: 0x0600777E RID: 30590 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x0600777F RID: 30591 RVA: 0x0000216D File Offset: 0x0000036D
			public TMP_Text m_TextAfterNum
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x17000EC6 RID: 3782
			// (get) Token: 0x06007780 RID: 30592 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007781 RID: 30593 RVA: 0x0000216D File Offset: 0x0000036D
			public TMP_Text m_TextPremiumNum
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x17000EC7 RID: 3783
			// (get) Token: 0x06007782 RID: 30594 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007783 RID: 30595 RVA: 0x0000216D File Offset: 0x0000036D
			public TMP_Text m_TextBeforeCP
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x17000EC8 RID: 3784
			// (get) Token: 0x06007784 RID: 30596 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007785 RID: 30597 RVA: 0x0000216D File Offset: 0x0000036D
			public TMP_Text m_TextAfterCP
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x17000EC9 RID: 3785
			// (get) Token: 0x06007786 RID: 30598 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007787 RID: 30599 RVA: 0x0000216D File Offset: 0x0000036D
			public TMP_Text m_TextCompensation
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x06007788 RID: 30600 RVA: 0x0000216D File Offset: 0x0000036D
			private void InitializeOperationUI()
			{
			}

			// Token: 0x06007789 RID: 30601 RVA: 0x0000216D File Offset: 0x0000036D
			private void InitializeResultUI()
			{
			}

			// Token: 0x0600778A RID: 30602 RVA: 0x0000216D File Offset: 0x0000036D
			protected override void InitializeElements()
			{
			}

			// Token: 0x0400B012 RID: 45074
			private ElementObjectManager m_OperationEom;

			// Token: 0x0400B013 RID: 45075
			private ElementObjectManager m_ResultEom;
		}
	}
}
