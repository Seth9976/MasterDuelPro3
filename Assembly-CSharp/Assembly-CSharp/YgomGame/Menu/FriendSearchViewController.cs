using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using TMPro;
using UnityEngine;
using YgomGame.Friend;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.Menu
{
	// Token: 0x02000A73 RID: 2675
	public class FriendSearchViewController : BaseMenuViewController, IBackButtonWithoutSCSupported, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06004E09 RID: 19977 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004E0A RID: 19978 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004E0B RID: 19979 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06004E0C RID: 19980 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06004E0D RID: 19981 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnFocusChanged(bool setfocus)
		{
		}

		// Token: 0x06004E0E RID: 19982 RVA: 0x0000216D File Offset: 0x0000036D
		private void LateUpdate()
		{
		}

		// Token: 0x06004E0F RID: 19983 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnPadBack()
		{
		}

		// Token: 0x06004E10 RID: 19984 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool TagListEdgeTransition(SelectionItem selectionItem, PadInputDirection direction)
		{
			return false;
		}

		// Token: 0x06004E11 RID: 19985 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnBack()
		{
			return false;
		}

		// Token: 0x06004E12 RID: 19986 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenPlayerProfile(long pcode)
		{
		}

		// Token: 0x06004E13 RID: 19987 RVA: 0x0000216D File Offset: 0x0000036D
		private void RequestSearchId()
		{
		}

		// Token: 0x06004E14 RID: 19988 RVA: 0x0000216D File Offset: 0x0000036D
		private void RequestSearchIdExecute(long searchPcode)
		{
		}

		// Token: 0x06004E15 RID: 19989 RVA: 0x0000216D File Offset: 0x0000036D
		private void RequestSearchTag(bool resetPosition = true)
		{
		}

		// Token: 0x06004E16 RID: 19990 RVA: 0x0000216D File Offset: 0x0000036D
		private void RequestSearchTag(Action callback, bool resetPosition = true)
		{
		}

		// Token: 0x04008BED RID: 35821
		public const string PREFAB_NAME = "Friend/FriendSearch";

		// Token: 0x04008BEE RID: 35822
		private readonly string BTN_BACK_PAD_BUTTON_LABEL;

		// Token: 0x04008BEF RID: 35823
		private readonly string k_ELabelTitleBg_Result;

		// Token: 0x04008BF0 RID: 35824
		private readonly string k_ELabelGuideMessage_Default;

		// Token: 0x04008BF1 RID: 35825
		private readonly string k_ELabelGuideMessage_Result;

		// Token: 0x04008BF2 RID: 35826
		private readonly string IDSEARCH_TOGGLE_LABEL;

		// Token: 0x04008BF3 RID: 35827
		private readonly string TAGSEARCH_TOGGLE_LABEL;

		// Token: 0x04008BF4 RID: 35828
		private readonly string SEARCHFORM_ID_LABEL;

		// Token: 0x04008BF5 RID: 35829
		private readonly string SEARCHFORM_TAG_LABEL;

		// Token: 0x04008BF6 RID: 35830
		private readonly string SHORTCUT_BUTTON_L1;

		// Token: 0x04008BF7 RID: 35831
		private readonly string SHORTCUT_BUTTON_R1;

		// Token: 0x04008BF8 RID: 35832
		private const int k_ID_SEARCH_FORM = 0;

		// Token: 0x04008BF9 RID: 35833
		private const int k_TAG_SEARCH_FORM = 1;

		// Token: 0x04008BFA RID: 35834
		private long m_ReserveSearchPcode;

		// Token: 0x04008BFB RID: 35835
		private FriendDefinitionSetting m_FriendDefinitionSetting;

		// Token: 0x04008BFC RID: 35836
		private ToggleGroupWidget m_SearchFormTab;

		// Token: 0x04008BFD RID: 35837
		private FriendSearchViewController.ISearchForm[] m_SearchForms;

		// Token: 0x04008BFE RID: 35838
		private bool m_IsReady;

		// Token: 0x02000A74 RID: 2676
		private interface ISearchForm
		{
			// Token: 0x17000743 RID: 1859
			// (get) Token: 0x06004E18 RID: 19992
			GameObject gameObject { get; }

			// Token: 0x17000744 RID: 1860
			// (get) Token: 0x06004E19 RID: 19993
			long currentPcode { get; }

			// Token: 0x06004E1A RID: 19994
			void Clear();

			// Token: 0x06004E1B RID: 19995
			bool TrySelectChild(bool initializeSelection = false);

			// Token: 0x06004E1C RID: 19996
			bool OnPadBack();
		}

		// Token: 0x02000A75 RID: 2677
		private class PlayerListView
		{
			// Token: 0x17000745 RID: 1861
			// (get) Token: 0x06004E1D RID: 19997 RVA: 0x0000216A File Offset: 0x0000036A
			public GameObject gameObject
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000746 RID: 1862
			// (get) Token: 0x06004E1E RID: 19998 RVA: 0x0000216A File Offset: 0x0000036A
			public IReadOnlyList<string> playerList
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000747 RID: 1863
			// (get) Token: 0x06004E1F RID: 19999 RVA: 0x000F1669 File Offset: 0x000EF869
			public long currentPcode
			{
				get
				{
					return 0L;
				}
			}

			// Token: 0x14000066 RID: 102
			// (add) Token: 0x06004E20 RID: 20000 RVA: 0x0000216D File Offset: 0x0000036D
			// (remove) Token: 0x06004E21 RID: 20001 RVA: 0x0000216D File Offset: 0x0000036D
			public event Action<long> onClickPlayerEvent
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

			// Token: 0x06004E22 RID: 20002 RVA: 0x00002739 File Offset: 0x00000939
			public PlayerListView(ElementObjectManager eom)
			{
			}

			// Token: 0x06004E23 RID: 20003 RVA: 0x0000216D File Offset: 0x0000036D
			public void Initialize(Action onComplete = null)
			{
			}

			// Token: 0x06004E24 RID: 20004 RVA: 0x0000216D File Offset: 0x0000036D
			private void OnCreatedEntity(GameObject gob)
			{
			}

			// Token: 0x06004E25 RID: 20005 RVA: 0x0000216D File Offset: 0x0000036D
			private void OnUpdateEntity(GameObject gob, int dataindex)
			{
			}

			// Token: 0x06004E26 RID: 20006 RVA: 0x0000216D File Offset: 0x0000036D
			public void ApplyResult(List<object> hitPlayerList, bool isExcess, bool resetPosition = true)
			{
			}

			// Token: 0x06004E27 RID: 20007 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool TrySelectIdx(int idx)
			{
				return false;
			}

			// Token: 0x06004E28 RID: 20008 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool IsContainSelectedItem()
			{
				return false;
			}

			// Token: 0x04008BFF RID: 35839
			private readonly string EMPTY_ROOT_LABEL;

			// Token: 0x04008C00 RID: 35840
			private readonly string LIST_ROOT_LABEL;

			// Token: 0x04008C01 RID: 35841
			private readonly string LIST_TITLE_LABEL;

			// Token: 0x04008C02 RID: 35842
			private readonly string LIST_SCROLL_VIEW_LABEL;

			// Token: 0x04008C03 RID: 35843
			private ElementObjectManager m_Eom;

			// Token: 0x04008C04 RID: 35844
			private GameObject m_EmptyRoot;

			// Token: 0x04008C05 RID: 35845
			private GameObject m_ListRoot;

			// Token: 0x04008C06 RID: 35846
			private TMP_Text m_ListTitleText;

			// Token: 0x04008C07 RID: 35847
			private InfinityScrollView m_ListScrollView;

			// Token: 0x04008C08 RID: 35848
			private Dictionary<string, object> m_PlayerDataMap;

			// Token: 0x04008C09 RID: 35849
			private Dictionary<GameObject, FriendWidget> m_FriendWidgetMap;

			// Token: 0x04008C0A RID: 35850
			private List<string> m_PlayerList;
		}

		// Token: 0x02000A76 RID: 2678
		private class SearchFormId : FriendSearchViewController.ISearchForm
		{
			// Token: 0x17000748 RID: 1864
			// (get) Token: 0x06004E29 RID: 20009 RVA: 0x0000216A File Offset: 0x0000036A
			public GameObject gameObject
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000749 RID: 1865
			// (get) Token: 0x06004E2A RID: 20010 RVA: 0x000F1669 File Offset: 0x000EF869
			// (set) Token: 0x06004E2B RID: 20011 RVA: 0x0000216D File Offset: 0x0000036D
			public long currentPcode
			{
				[CompilerGenerated]
				get
				{
					return 0L;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x14000067 RID: 103
			// (add) Token: 0x06004E2C RID: 20012 RVA: 0x0000216D File Offset: 0x0000036D
			// (remove) Token: 0x06004E2D RID: 20013 RVA: 0x0000216D File Offset: 0x0000036D
			public event Action<long> onDecideEvent
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

			// Token: 0x14000068 RID: 104
			// (add) Token: 0x06004E2E RID: 20014 RVA: 0x0000216D File Offset: 0x0000036D
			// (remove) Token: 0x06004E2F RID: 20015 RVA: 0x0000216D File Offset: 0x0000036D
			public event Action<long> onClickPlayerEvent
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

			// Token: 0x06004E30 RID: 20016 RVA: 0x00002739 File Offset: 0x00000939
			public SearchFormId(ElementObjectManager eom)
			{
			}

			// Token: 0x06004E31 RID: 20017 RVA: 0x0000216D File Offset: 0x0000036D
			public void Initialize(int pcodeLength)
			{
			}

			// Token: 0x06004E32 RID: 20018 RVA: 0x0000216D File Offset: 0x0000036D
			public void Clear()
			{
			}

			// Token: 0x06004E33 RID: 20019 RVA: 0x0000216D File Offset: 0x0000036D
			public void CheckCaretPosition()
			{
			}

			// Token: 0x06004E34 RID: 20020 RVA: 0x0000216D File Offset: 0x0000036D
			private void OnValueChanged(string input)
			{
			}

			// Token: 0x06004E35 RID: 20021 RVA: 0x0000216D File Offset: 0x0000036D
			private void OnSubmitInputField(string input)
			{
			}

			// Token: 0x06004E36 RID: 20022 RVA: 0x0000216D File Offset: 0x0000036D
			public void ApplySearchResult(long searchPcode, List<object> hitPlayerList, string failedMessage = null)
			{
			}

			// Token: 0x06004E37 RID: 20023 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool TrySelectChild(bool initializeSelection = false)
			{
				return false;
			}

			// Token: 0x06004E38 RID: 20024 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool OnPadBack()
			{
				return false;
			}

			// Token: 0x04008C0B RID: 35851
			private readonly string k_ELabelResultMessage;

			// Token: 0x04008C0C RID: 35852
			private readonly string ID_INPUT_LABEL;

			// Token: 0x04008C0D RID: 35853
			private readonly string ID_INPUTOVERRIDE_TEXT_LABEL;

			// Token: 0x04008C0E RID: 35854
			private readonly string PLAYER_BOARD_LABEL;

			// Token: 0x04008C0F RID: 35855
			private ElementObjectManager m_Eom;

			// Token: 0x04008C10 RID: 35856
			private readonly InputFieldWidget m_InputFieldWidget;

			// Token: 0x04008C11 RID: 35857
			private readonly TMP_Text m_InputFieldOverrideText;

			// Token: 0x04008C12 RID: 35858
			private readonly TMP_Text m_ResultMessage;

			// Token: 0x04008C13 RID: 35859
			private readonly FriendWidget m_FriendWidget;

			// Token: 0x04008C14 RID: 35860
			private int m_LastCaretPos;

			// Token: 0x04008C15 RID: 35861
			private int pcodeLength;

			// Token: 0x04008C16 RID: 35862
			private StringBuilder m_Sb;

			// Token: 0x04008C17 RID: 35863
			private bool m_InputGuard;
		}

		// Token: 0x02000A77 RID: 2679
		private class SearchFormTag : FriendSearchViewController.ISearchForm
		{
			// Token: 0x1700074A RID: 1866
			// (get) Token: 0x06004E39 RID: 20025 RVA: 0x0000216A File Offset: 0x0000036A
			public GameObject gameObject
			{
				get
				{
					return null;
				}
			}

			// Token: 0x1700074B RID: 1867
			// (get) Token: 0x06004E3A RID: 20026 RVA: 0x0000216A File Offset: 0x0000036A
			public FriendSearchViewController.TagSelector tagForm
			{
				get
				{
					return null;
				}
			}

			// Token: 0x1700074C RID: 1868
			// (get) Token: 0x06004E3B RID: 20027 RVA: 0x0000216A File Offset: 0x0000036A
			public FriendSearchViewController.TagListView tagListView
			{
				get
				{
					return null;
				}
			}

			// Token: 0x1700074D RID: 1869
			// (get) Token: 0x06004E3C RID: 20028 RVA: 0x0000216A File Offset: 0x0000036A
			public IReadOnlyList<int> searchTagIdList
			{
				get
				{
					return null;
				}
			}

			// Token: 0x1700074E RID: 1870
			// (get) Token: 0x06004E3D RID: 20029 RVA: 0x000F1669 File Offset: 0x000EF869
			public long currentPcode
			{
				get
				{
					return 0L;
				}
			}

			// Token: 0x14000069 RID: 105
			// (add) Token: 0x06004E3E RID: 20030 RVA: 0x0000216D File Offset: 0x0000036D
			// (remove) Token: 0x06004E3F RID: 20031 RVA: 0x0000216D File Offset: 0x0000036D
			public event Action onSearchDecideEvent
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

			// Token: 0x1400006A RID: 106
			// (add) Token: 0x06004E40 RID: 20032 RVA: 0x0000216D File Offset: 0x0000036D
			// (remove) Token: 0x06004E41 RID: 20033 RVA: 0x0000216D File Offset: 0x0000036D
			public event Action<long> onClickPlayerEvent
			{
				add
				{
				}
				remove
				{
				}
			}

			// Token: 0x06004E42 RID: 20034 RVA: 0x00002739 File Offset: 0x00000939
			public SearchFormTag(ElementObjectManager eom, SelectionButton tagSearchToggle)
			{
			}

			// Token: 0x06004E43 RID: 20035 RVA: 0x0000216D File Offset: 0x0000036D
			public void Initialize(Action onComplete = null)
			{
			}

			// Token: 0x06004E44 RID: 20036 RVA: 0x0000216D File Offset: 0x0000036D
			private void CheckInitializeCallback()
			{
			}

			// Token: 0x06004E45 RID: 20037 RVA: 0x0000216D File Offset: 0x0000036D
			public void ApplySearchResult(List<object> hitPlayerList, bool isExcess, bool resetPosition = true)
			{
			}

			// Token: 0x06004E46 RID: 20038 RVA: 0x0000216D File Offset: 0x0000036D
			public void Clear()
			{
			}

			// Token: 0x06004E47 RID: 20039 RVA: 0x0000216D File Offset: 0x0000036D
			private void ToEntrySelectForm()
			{
			}

			// Token: 0x06004E48 RID: 20040 RVA: 0x0000216D File Offset: 0x0000036D
			private void ToSelectForm()
			{
			}

			// Token: 0x06004E49 RID: 20041 RVA: 0x0000216D File Offset: 0x0000036D
			private void ToResultForm(List<object> hitPlayerList, bool isExcess, bool resetPosition = true)
			{
			}

			// Token: 0x06004E4A RID: 20042 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool TrySelectDefault(bool initializeSelection = false)
			{
				return false;
			}

			// Token: 0x06004E4B RID: 20043 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool TrySelectChild(bool initializeSelection = false)
			{
				return false;
			}

			// Token: 0x06004E4C RID: 20044 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool OnPadBack()
			{
				return false;
			}

			// Token: 0x04008C18 RID: 35864
			private readonly string TAG_FORM_LABEL;

			// Token: 0x04008C19 RID: 35865
			private readonly string TAG_RESULT_FORM_LABEL;

			// Token: 0x04008C1A RID: 35866
			private readonly string TAG_SELECT_FORM_LABEL;

			// Token: 0x04008C1B RID: 35867
			private ElementObjectManager m_Eom;

			// Token: 0x04008C1C RID: 35868
			private readonly FriendSearchViewController.TagSelector m_TagForm;

			// Token: 0x04008C1D RID: 35869
			private readonly FriendSearchViewController.TagListView m_TagListView;

			// Token: 0x04008C1E RID: 35870
			private readonly FriendSearchViewController.PlayerListView m_ResultPlayerList;

			// Token: 0x04008C1F RID: 35871
			private readonly SelectionButton m_TagSearchToggle;

			// Token: 0x04008C20 RID: 35872
			private bool m_IsSelectMode;

			// Token: 0x04008C21 RID: 35873
			private Action m_OnComplete;

			// Token: 0x04008C22 RID: 35874
			private int m_LoadincCnt;

			// Token: 0x04008C23 RID: 35875
			private bool m_IsEntryMode;
		}

		// Token: 0x02000A78 RID: 2680
		private class TagListView
		{
			// Token: 0x1700074F RID: 1871
			// (get) Token: 0x06004E4D RID: 20045 RVA: 0x0000216A File Offset: 0x0000036A
			public GameObject gameObject
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000750 RID: 1872
			// (get) Token: 0x06004E4E RID: 20046 RVA: 0x0000216A File Offset: 0x0000036A
			public InfinityScrollView scrollView
			{
				get
				{
					return null;
				}
			}

			// Token: 0x1400006B RID: 107
			// (add) Token: 0x06004E4F RID: 20047 RVA: 0x0000216D File Offset: 0x0000036D
			// (remove) Token: 0x06004E50 RID: 20048 RVA: 0x0000216D File Offset: 0x0000036D
			public event Action<int> onClickTagEvent
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

			// Token: 0x06004E51 RID: 20049 RVA: 0x00002739 File Offset: 0x00000939
			public TagListView(ElementObjectManager eom, IReadOnlyList<object> searchTagIds, IReadOnlyList<int> selectedList, SelectionItem upperSelectTarget = null)
			{
			}

			// Token: 0x06004E52 RID: 20050 RVA: 0x0000216D File Offset: 0x0000036D
			public void Initialize(Action complete = null)
			{
			}

			// Token: 0x06004E53 RID: 20051 RVA: 0x0000216D File Offset: 0x0000036D
			private void OnUpdateEntity(GameObject gob, int dataindex)
			{
			}

			// Token: 0x06004E54 RID: 20052 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool SelectItemByIdx(int idx)
			{
				return false;
			}

			// Token: 0x06004E55 RID: 20053 RVA: 0x0000216D File Offset: 0x0000036D
			public void Refresh(bool selectDefault = false)
			{
			}

			// Token: 0x06004E56 RID: 20054 RVA: 0x0000216D File Offset: 0x0000036D
			public void Clear()
			{
			}

			// Token: 0x06004E57 RID: 20055 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool IsContainSelectedItem()
			{
				return false;
			}

			// Token: 0x04008C24 RID: 35876
			private readonly string SCROLL_VIEW_LABEL;

			// Token: 0x04008C25 RID: 35877
			private readonly string ITEM_ON;

			// Token: 0x04008C26 RID: 35878
			private readonly string ITEM_OFF;

			// Token: 0x04008C27 RID: 35879
			private readonly string ITEM_TAG_LABEL_ON;

			// Token: 0x04008C28 RID: 35880
			private readonly string ITEM_TAG_LABEL_OFF;

			// Token: 0x04008C29 RID: 35881
			private ElementObjectManager m_Eom;

			// Token: 0x04008C2A RID: 35882
			private readonly InfinityScrollView m_ScrollView;

			// Token: 0x04008C2B RID: 35883
			private readonly SelectionItem m_UpperSelectTarget;

			// Token: 0x04008C2C RID: 35884
			public readonly List<int> searchTagIds;

			// Token: 0x04008C2D RID: 35885
			public readonly IReadOnlyList<int> selectedList;

			// Token: 0x04008C2E RID: 35886
			public bool removeInteractable;
		}

		// Token: 0x02000A79 RID: 2681
		private class TagSelector
		{
			// Token: 0x17000751 RID: 1873
			// (get) Token: 0x06004E58 RID: 20056 RVA: 0x000029CC File Offset: 0x00000BCC
			private int k_AddIdx
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x17000752 RID: 1874
			// (get) Token: 0x06004E59 RID: 20057 RVA: 0x0000216A File Offset: 0x0000036A
			public GameObject gameObject
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000753 RID: 1875
			// (get) Token: 0x06004E5A RID: 20058 RVA: 0x0000216A File Offset: 0x0000036A
			public IReadOnlyList<int> tagList
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000754 RID: 1876
			// (get) Token: 0x06004E5B RID: 20059 RVA: 0x000029CC File Offset: 0x00000BCC
			public int currentIdx
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x17000755 RID: 1877
			// (get) Token: 0x06004E5C RID: 20060 RVA: 0x000029CC File Offset: 0x00000BCC
			public int focusTagId
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x1400006C RID: 108
			// (add) Token: 0x06004E5D RID: 20061 RVA: 0x0000216D File Offset: 0x0000036D
			// (remove) Token: 0x06004E5E RID: 20062 RVA: 0x0000216D File Offset: 0x0000036D
			public event Action<int> onClickTagEvent
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

			// Token: 0x1400006D RID: 109
			// (add) Token: 0x06004E5F RID: 20063 RVA: 0x0000216D File Offset: 0x0000036D
			// (remove) Token: 0x06004E60 RID: 20064 RVA: 0x0000216D File Offset: 0x0000036D
			public event Action onClickSearchEvent
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

			// Token: 0x06004E61 RID: 20065 RVA: 0x00002739 File Offset: 0x00000939
			public TagSelector(ElementObjectManager eom)
			{
			}

			// Token: 0x06004E62 RID: 20066 RVA: 0x0000216D File Offset: 0x0000036D
			public void Initialize()
			{
			}

			// Token: 0x06004E63 RID: 20067 RVA: 0x0000216D File Offset: 0x0000036D
			public void Clear()
			{
			}

			// Token: 0x06004E64 RID: 20068 RVA: 0x0000216D File Offset: 0x0000036D
			public void ToAllOff()
			{
			}

			// Token: 0x06004E65 RID: 20069 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool SelectDefaultCursor()
			{
				return false;
			}

			// Token: 0x06004E66 RID: 20070 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool IsContainSelectedItem(Selector selector)
			{
				return false;
			}

			// Token: 0x06004E67 RID: 20071 RVA: 0x0000216D File Offset: 0x0000036D
			public void Refresh()
			{
			}

			// Token: 0x06004E68 RID: 20072 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetTag(int tagId, bool isInvokeChangeEvent = true)
			{
			}

			// Token: 0x06004E69 RID: 20073 RVA: 0x0000216D File Offset: 0x0000036D
			public void AddTag(int tagId)
			{
			}

			// Token: 0x06004E6A RID: 20074 RVA: 0x0000216D File Offset: 0x0000036D
			public void RemoveTag(int tagId)
			{
			}

			// Token: 0x06004E6B RID: 20075 RVA: 0x0000216D File Offset: 0x0000036D
			public void ReplaceTag(int fromTagId, int toTagId)
			{
			}

			// Token: 0x04008C2F RID: 35887
			private readonly int k_TagMax;

			// Token: 0x04008C30 RID: 35888
			private readonly string TAG_ITEM_ROOT_LABEL;

			// Token: 0x04008C31 RID: 35889
			private readonly string TAG_ITEM_LABEL_OFF_LABEL;

			// Token: 0x04008C32 RID: 35890
			private readonly string TAG_ITEM_LABEL_ON_LABEL;

			// Token: 0x04008C33 RID: 35891
			private readonly string TAG_ITEM_ADD_LABEL;

			// Token: 0x04008C34 RID: 35892
			private readonly string SEARCH_BUTTON_LABEL;

			// Token: 0x04008C35 RID: 35893
			private ElementObjectManager m_Eom;

			// Token: 0x04008C36 RID: 35894
			private readonly ToggleGroupWidget m_ToggleGroupWidget;

			// Token: 0x04008C37 RID: 35895
			public readonly SelectionButton searchButton;

			// Token: 0x04008C38 RID: 35896
			private readonly List<int> m_TagList;
		}
	}
}
