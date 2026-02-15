using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YgomGame.ActionSheet;
using YgomGame.Menu;
using YgomGame.Menu.Common;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.CardBrowser
{
	// Token: 0x020010E8 RID: 4328
	public class CardListBrowserViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x17001070 RID: 4208
		// (get) Token: 0x060080CB RID: 32971 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060080CC RID: 32972 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenByConfig(string configPath, IReadOnlyList<int> cardMrks, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x060080CD RID: 32973 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(string title, IReadOnlyList<int> cardMrks, ViewControllerManager manager = null, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x060080CE RID: 32974 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(ViewControllerManager manager = null, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x060080CF RID: 32975 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x060080D0 RID: 32976 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x060080D1 RID: 32977 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x060080D2 RID: 32978 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateListDisplay()
		{
		}

		// Token: 0x060080D3 RID: 32979 RVA: 0x0000216D File Offset: 0x0000036D
		private void ApplyCurrentDisplay()
		{
		}

		// Token: 0x060080D4 RID: 32980 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateRegulationIcon()
		{
		}

		// Token: 0x060080D5 RID: 32981 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnEntityCreated(GameObject gob)
		{
		}

		// Token: 0x060080D6 RID: 32982 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnEntityUpdate(GameObject gob, int dataindex)
		{
		}

		// Token: 0x060080D7 RID: 32983 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickEntity(CardListBrowserViewController.CardWidget clickedWidget)
		{
		}

		// Token: 0x060080D8 RID: 32984 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnInputAnalogDirection(SelectorManager.AnalogType analogType, PadInputDirection dir)
		{
		}

		// Token: 0x060080D9 RID: 32985 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenRegurationSelectSheet()
		{
		}

		// Token: 0x0400B927 RID: 47399
		public const string k_ArgOpenOnHome = "onHome";

		// Token: 0x0400B928 RID: 47400
		private const string k_ArgTitle = "title";

		// Token: 0x0400B929 RID: 47401
		private const string k_ArgCardMrks = "cardMrks";

		// Token: 0x0400B92A RID: 47402
		public const string k_ArgRegulationVisible = "regulationVisible";

		// Token: 0x0400B92B RID: 47403
		public const string k_ArgRegulationId = "regulationId";

		// Token: 0x0400B92C RID: 47404
		public const string k_ArgRegulationSelectorVisible = "regulationSelectorVisible";

		// Token: 0x0400B92D RID: 47405
		public const string k_ArgShowOwnedNumToggleVisible = "showOwnedNumToggleVisible";

		// Token: 0x0400B92E RID: 47406
		public const string k_ArgRequestCardTermData = "requestCardInfo";

		// Token: 0x0400B92F RID: 47407
		public const string k_ArgShowHighlightReleasedCardsToggleVisible = "showHighlightReleasedCardsToggleVisible";

		// Token: 0x0400B930 RID: 47408
		public const string k_ArgHasNumVisible = "hasNumVisible";

		// Token: 0x0400B931 RID: 47409
		public const string k_ArgHasNumMonochrome = "hasNumMonochrome";

		// Token: 0x0400B932 RID: 47410
		public const string k_ArgHighlightReleasedCards = "HighlightReleasedCards";

		// Token: 0x0400B933 RID: 47411
		public const string k_ArgEnableFilterForm = "EnablefilterForm";

		// Token: 0x0400B934 RID: 47412
		public const string k_ArgEnableDisplayFilterNum = "DisplayFilterNum";

		// Token: 0x0400B935 RID: 47413
		private const int k_DisplayIdx_All = 0;

		// Token: 0x0400B936 RID: 47414
		private const int k_DisplayIdx_HasNum = 1;

		// Token: 0x0400B937 RID: 47415
		private const int k_DisplayIdx_HighlightReleasedCards = 2;

		// Token: 0x0400B938 RID: 47416
		[SerializeField]
		private string m_ELabelRegulationButton;

		// Token: 0x0400B939 RID: 47417
		private const string k_ELabelRegulationIcon = "RegulationIcon";

		// Token: 0x0400B93A RID: 47418
		private readonly string k_VLabelDefault;

		// Token: 0x0400B93B RID: 47419
		private readonly string k_VLabelFiltered;

		// Token: 0x0400B93C RID: 47420
		private readonly string k_ELabelAnalogDirectionItem;

		// Token: 0x0400B93D RID: 47421
		private readonly string k_ELabelTitleText;

		// Token: 0x0400B93E RID: 47422
		private readonly string k_ELabelCardList;

		// Token: 0x0400B93F RID: 47423
		private readonly string k_ELabelEmptyText;

		// Token: 0x0400B940 RID: 47424
		private readonly string k_ELabelRegulationButtonRoot;

		// Token: 0x0400B941 RID: 47425
		private readonly string k_ELabelShowOwnedNumToggleRoot;

		// Token: 0x0400B942 RID: 47426
		private readonly string k_ELabelShowOwnedNumToggle;

		// Token: 0x0400B943 RID: 47427
		private readonly string k_ELabelDisplaySelectButtonRoot;

		// Token: 0x0400B944 RID: 47428
		private readonly string k_ELabelDisplaySelectButton;

		// Token: 0x0400B945 RID: 47429
		private readonly string k_ELabelDisplaySelectText;

		// Token: 0x0400B946 RID: 47430
		private readonly string k_ELabelFilterInputFieldRoot;

		// Token: 0x0400B947 RID: 47431
		private readonly string k_ELabelFilterForm;

		// Token: 0x0400B948 RID: 47432
		private readonly string k_ELabelFilterInputField;

		// Token: 0x0400B949 RID: 47433
		private readonly string k_ELabelShortcutButtonRarityBack;

		// Token: 0x0400B94A RID: 47434
		private readonly string k_ELabelShortcutButtonRarityNext;

		// Token: 0x0400B94B RID: 47435
		private List<int> m_CardMrks;

		// Token: 0x0400B94C RID: 47436
		private List<int> m_DisplayCardMrks;

		// Token: 0x0400B94D RID: 47437
		private bool m_RegulationVisible;

		// Token: 0x0400B94E RID: 47438
		private bool m_HasNumVisible;

		// Token: 0x0400B94F RID: 47439
		private bool m_HasNumMonochrome;

		// Token: 0x0400B950 RID: 47440
		private bool m_HighlightReleasedCards;

		// Token: 0x0400B951 RID: 47441
		private int m_RegulationId;

		// Token: 0x0400B952 RID: 47442
		private int m_CurrentDisplayIdx;

		// Token: 0x0400B953 RID: 47443
		private ActionSheetViewController.EntryData[] m_DisplaySheetEntries;

		// Token: 0x0400B954 RID: 47444
		private ToggleWidget m_ShowOwnedNumToggle;

		// Token: 0x0400B955 RID: 47445
		private SelectionButton m_DisplaySelectButton;

		// Token: 0x0400B956 RID: 47446
		private TMP_Text m_DisplaySelectText;

		// Token: 0x0400B957 RID: 47447
		private InfinityScrollView m_ScrollView;

		// Token: 0x0400B958 RID: 47448
		private TMP_Text m_EmptyText;

		// Token: 0x0400B959 RID: 47449
		private RegulationSelectSheet m_RegulationSelectSheet;

		// Token: 0x0400B95A RID: 47450
		private CardListBrowserViewController.FilterFormWidget m_FilterFormWidget;

		// Token: 0x0400B95B RID: 47451
		private Dictionary<GameObject, CardListBrowserViewController.CardWidget> m_CardWidgetDic;

		// Token: 0x0400B95C RID: 47452
		private List<int> m_TmpSearchMrks;

		// Token: 0x020010E9 RID: 4329
		private class CardWidget : ElementWidgetBase
		{
			// Token: 0x17001071 RID: 4209
			// (get) Token: 0x060080DB RID: 32987 RVA: 0x000029CC File Offset: 0x00000BCC
			public int idx
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x17001072 RID: 4210
			// (get) Token: 0x060080DC RID: 32988 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x060080DD RID: 32989 RVA: 0x0000216D File Offset: 0x0000036D
			public bool highlightVisible
			{
				get
				{
					return false;
				}
				set
				{
				}
			}

			// Token: 0x17001073 RID: 4211
			// (get) Token: 0x060080DE RID: 32990 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x060080DF RID: 32991 RVA: 0x0000216D File Offset: 0x0000036D
			public bool newIconVisible
			{
				get
				{
					return false;
				}
				set
				{
				}
			}

			// Token: 0x17001074 RID: 4212
			// (get) Token: 0x060080E0 RID: 32992 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x060080E1 RID: 32993 RVA: 0x0000216D File Offset: 0x0000036D
			public bool limitIconVisible
			{
				get
				{
					return false;
				}
				set
				{
				}
			}

			// Token: 0x17001075 RID: 4213
			// (get) Token: 0x060080E2 RID: 32994 RVA: 0x0000216A File Offset: 0x0000036A
			public Image limitIconImage
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17001076 RID: 4214
			// (get) Token: 0x060080E3 RID: 32995 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x060080E4 RID: 32996 RVA: 0x0000216D File Offset: 0x0000036D
			public bool innerTextVisible
			{
				get
				{
					return false;
				}
				set
				{
				}
			}

			// Token: 0x17001077 RID: 4215
			// (get) Token: 0x060080E5 RID: 32997 RVA: 0x0000216A File Offset: 0x0000036A
			public TMP_Text innerText
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17001078 RID: 4216
			// (get) Token: 0x060080E6 RID: 32998 RVA: 0x0000216A File Offset: 0x0000036A
			public SelectionButton button
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17001079 RID: 4217
			// (get) Token: 0x060080E7 RID: 32999 RVA: 0x0000216A File Offset: 0x0000036A
			public GameObject noCard
			{
				get
				{
					return null;
				}
			}

			// Token: 0x1700107A RID: 4218
			// (get) Token: 0x060080E8 RID: 33000 RVA: 0x0000216A File Offset: 0x0000036A
			public BindingCardMaterial bindingCardMaterial
			{
				get
				{
					return null;
				}
			}

			// Token: 0x140000CC RID: 204
			// (add) Token: 0x060080E9 RID: 33001 RVA: 0x0000216D File Offset: 0x0000036D
			// (remove) Token: 0x060080EA RID: 33002 RVA: 0x0000216D File Offset: 0x0000036D
			public event Action<CardListBrowserViewController.CardWidget> onClickEvent
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

			// Token: 0x060080EB RID: 33003 RVA: 0x0000216A File Offset: 0x0000036A
			public static CardListBrowserViewController.CardWidget Create(ElementObjectManager eom)
			{
				return null;
			}

			// Token: 0x060080EC RID: 33004 RVA: 0x000F2C76 File Offset: 0x000F0E76
			public CardWidget(ElementObjectManager eom)
				: base(null)
			{
			}

			// Token: 0x060080ED RID: 33005 RVA: 0x0000216D File Offset: 0x0000036D
			public virtual void Binding(int idx, int mrk, int styleId = 1)
			{
			}

			// Token: 0x060080EE RID: 33006 RVA: 0x0000216D File Offset: 0x0000036D
			protected virtual void OnClick()
			{
			}

			// Token: 0x0400B95D RID: 47453
			private readonly string k_ECardLabelButton;

			// Token: 0x0400B95E RID: 47454
			private readonly string k_ECardLabelNoCard;

			// Token: 0x0400B95F RID: 47455
			private readonly string k_ECardLabelHighlight;

			// Token: 0x0400B960 RID: 47456
			private readonly string k_ECardLabelIconRarity;

			// Token: 0x0400B961 RID: 47457
			private readonly string k_ECardLabelLimitIcon;

			// Token: 0x0400B962 RID: 47458
			private readonly string k_ECardLabelNumTextArea;

			// Token: 0x0400B963 RID: 47459
			private readonly string k_ECardLabelNumText;

			// Token: 0x0400B964 RID: 47460
			private readonly string k_ECardLabelNewIcon;

			// Token: 0x0400B965 RID: 47461
			private int m_Idx;

			// Token: 0x0400B966 RID: 47462
			private int m_Mrk;

			// Token: 0x0400B967 RID: 47463
			private int m_StyleId;

			// Token: 0x0400B968 RID: 47464
			private readonly RawImage m_CardRawImage;

			// Token: 0x0400B969 RID: 47465
			private BindingCardMaterial m_BindingCardMaterial;

			// Token: 0x0400B96A RID: 47466
			public int regurationId;
		}

		// Token: 0x020010EA RID: 4330
		private class FilterFormWidget : ElementWidgetBase
		{
			// Token: 0x1700107B RID: 4219
			// (get) Token: 0x060080EF RID: 33007 RVA: 0x0000216A File Offset: 0x0000036A
			public GameObject numGroup
			{
				get
				{
					return null;
				}
			}

			// Token: 0x1700107C RID: 4220
			// (get) Token: 0x060080F0 RID: 33008 RVA: 0x0000216A File Offset: 0x0000036A
			public TMP_Text numText
			{
				get
				{
					return null;
				}
			}

			// Token: 0x060080F1 RID: 33009 RVA: 0x000F2C76 File Offset: 0x000F0E76
			public FilterFormWidget(ElementObjectManager filterFormEom, InputFieldWidget filterInputField)
				: base(null)
			{
			}

			// Token: 0x0400B96B RID: 47467
			private readonly string k_ELabelDisplayInFilterText;

			// Token: 0x0400B96C RID: 47468
			private readonly string k_ELabelFilterRarityTabs;

			// Token: 0x0400B96D RID: 47469
			private readonly string k_ELabelNumGroup;

			// Token: 0x0400B96E RID: 47470
			private readonly string k_ELabelNumText;

			// Token: 0x0400B96F RID: 47471
			public readonly DirectionalToggleGroupWidget rarityToggleWidget;

			// Token: 0x0400B970 RID: 47472
			public InputFieldWidget nameInputWidget;

			// Token: 0x0400B971 RID: 47473
			public TMP_Text displayInFilterText;
		}
	}
}
