using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using YgomGame.Menu.Common;
using YgomGame.Utility;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;
using YgomSystem.UI.InfinityScroll;
using YgomSystem.YGomTMPro;

namespace YgomGame.Menu
{
	// Token: 0x02000AC3 RID: 2755
	public class ProfileEditViewController : BaseMenuViewController, IBackButtonWithoutSCSupported, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x0600502F RID: 20527 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005030 RID: 20528 RVA: 0x0000216D File Offset: 0x0000036D
		public string currentItemName
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06005031 RID: 20529 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool isMobile
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06005032 RID: 20530 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool isGamePad
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06005033 RID: 20531 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06005034 RID: 20532 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(Dictionary<string, object> args = null)
		{
		}

		// Token: 0x06005035 RID: 20533 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06005036 RID: 20534 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x06005037 RID: 20535 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06005038 RID: 20536 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnBack()
		{
			return false;
		}

		// Token: 0x06005039 RID: 20537 RVA: 0x0000216D File Offset: 0x0000036D
		private void SaveEdit(UnityAction callback = null)
		{
		}

		// Token: 0x0600503A RID: 20538 RVA: 0x0000216A File Offset: 0x0000036A
		private Dictionary<string, object> CheckUpdatedProfile()
		{
			return null;
		}

		// Token: 0x0600503B RID: 20539 RVA: 0x0000216D File Offset: 0x0000036D
		private void EnterMenu(ProfileEditViewController.ProfileEdit profileEdit)
		{
		}

		// Token: 0x0600503C RID: 20540 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetActiveMenu(ProfileEditViewController.ProfileEdit profileEdit)
		{
		}

		// Token: 0x0600503D RID: 20541 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitEdit()
		{
		}

		// Token: 0x0600503E RID: 20542 RVA: 0x0000216A File Offset: 0x0000036A
		private List<ToggleWidget> InitUserProfileEdit()
		{
			return null;
		}

		// Token: 0x0600503F RID: 20543 RVA: 0x0000216A File Offset: 0x0000036A
		private List<ToggleWidget> InitAccessoryEdit()
		{
			return null;
		}

		// Token: 0x06005040 RID: 20544 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenPickupSelectionBrowser()
		{
		}

		// Token: 0x06005041 RID: 20545 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenFieldPreview()
		{
		}

		// Token: 0x06005042 RID: 20546 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenItemPreview()
		{
		}

		// Token: 0x06005043 RID: 20547 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsEdittingAccessorry()
		{
			return false;
		}

		// Token: 0x06005044 RID: 20548 RVA: 0x0000216A File Offset: 0x0000036A
		private Dictionary<string, object> GetEditDic(ProfileEditViewController.EditType type)
		{
			return null;
		}

		// Token: 0x06005045 RID: 20549 RVA: 0x0000216A File Offset: 0x0000036A
		private Dictionary<string, object> GetEditPickCards(ProfileEditViewController.EditType type)
		{
			return null;
		}

		// Token: 0x04008E47 RID: 36423
		private const string k_ELabelTitleArea = "TitleArea";

		// Token: 0x04008E48 RID: 36424
		private const string k_ELabelFooterArea = "FooterArea";

		// Token: 0x04008E49 RID: 36425
		private const string k_ELabelRootSideMenu = "RootSideMenu";

		// Token: 0x04008E4A RID: 36426
		private ProfileEditViewController.FooterArea m_FooterArea;

		// Token: 0x04008E4B RID: 36427
		private ProfileEditViewController.TitleArea m_TitleArea;

		// Token: 0x04008E4C RID: 36428
		private ProfileEditViewController.SideMenu m_SideMenu;

		// Token: 0x04008E4D RID: 36429
		private readonly string ROOT_EDIT_LABEL;

		// Token: 0x04008E4E RID: 36430
		private readonly string ROOT_VIEW_LABEL;

		// Token: 0x04008E4F RID: 36431
		private readonly string TMP_TXT_LABEL;

		// Token: 0x04008E50 RID: 36432
		private readonly string TMP_IMG_LABEL;

		// Token: 0x04008E51 RID: 36433
		private readonly string TMP_TAG_LABEL;

		// Token: 0x04008E52 RID: 36434
		private readonly string TMP_PICK_LABEL;

		// Token: 0x04008E53 RID: 36435
		private readonly string TMP_VIEW_LABEL;

		// Token: 0x04008E54 RID: 36436
		private readonly string IMG_LABEL;

		// Token: 0x04008E55 RID: 36437
		private readonly string TEXT_ITEM_NAME_LABEL;

		// Token: 0x04008E56 RID: 36438
		private bool isSaved;

		// Token: 0x04008E57 RID: 36439
		private bool isFixedAccsessory;

		// Token: 0x04008E58 RID: 36440
		private bool isFixedPickCards;

		// Token: 0x04008E59 RID: 36441
		private ProfileEditViewController.ProfileEdit currentEditing;

		// Token: 0x04008E5A RID: 36442
		private List<ProfileEditViewController.ProfileEdit> profileEdits;

		// Token: 0x04008E5B RID: 36443
		private ProfileEditViewController.EditType editType;

		// Token: 0x04008E5C RID: 36444
		private int deckID;

		// Token: 0x04008E5D RID: 36445
		private int identifierID;

		// Token: 0x04008E5E RID: 36446
		private int defaultTabIdx;

		// Token: 0x04008E5F RID: 36447
		private GameObject rootEdit;

		// Token: 0x04008E60 RID: 36448
		private GameObject rootView;

		// Token: 0x04008E61 RID: 36449
		private GameObject tmpEditTxt;

		// Token: 0x04008E62 RID: 36450
		private GameObject tmpEditImg;

		// Token: 0x04008E63 RID: 36451
		private GameObject tmpEditTag;

		// Token: 0x04008E64 RID: 36452
		private GameObject tmpEditPick;

		// Token: 0x04008E65 RID: 36453
		private GameObject tmpView;

		// Token: 0x04008E66 RID: 36454
		private TextMeshProUGUI m_CurrentItemNameText;

		// Token: 0x04008E67 RID: 36455
		private string m_CurrentItemName;

		// Token: 0x04008E68 RID: 36456
		private ProfileEditViewController.ProfileEditPickCards m_PEPC;

		// Token: 0x02000AC4 RID: 2756
		private class TitleArea : MonoBehaviour
		{
			// Token: 0x06005047 RID: 20551 RVA: 0x0000216D File Offset: 0x0000036D
			private void Awake()
			{
			}

			// Token: 0x06005048 RID: 20552 RVA: 0x0000216D File Offset: 0x0000036D
			public void InitializeElements()
			{
			}

			// Token: 0x06005049 RID: 20553 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetTitleByEditType(ProfileEditViewController.EditType editType)
			{
			}

			// Token: 0x0600504A RID: 20554 RVA: 0x0000216D File Offset: 0x0000036D
			private void OpenCautionDialog()
			{
			}

			// Token: 0x0600504B RID: 20555 RVA: 0x0000216D File Offset: 0x0000036D
			public void DispCautionButton(bool disp)
			{
			}

			// Token: 0x04008E69 RID: 36457
			private ElementObjectManager m_Eom;

			// Token: 0x04008E6A RID: 36458
			private bool isInitialized;

			// Token: 0x04008E6B RID: 36459
			private ExtendedTextMeshProUGUI m_TitleText;

			// Token: 0x04008E6C RID: 36460
			private SelectionButton m_CautionButton;
		}

		// Token: 0x02000AC5 RID: 2757
		private class SideMenu : MonoBehaviour
		{
			// Token: 0x1700077E RID: 1918
			// (get) Token: 0x0600504D RID: 20557 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x0600504E RID: 20558 RVA: 0x0000216D File Offset: 0x0000036D
			public GameObject sideButtonTemplate
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

			// Token: 0x1700077F RID: 1919
			// (get) Token: 0x0600504F RID: 20559 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06005050 RID: 20560 RVA: 0x0000216D File Offset: 0x0000036D
			public Selector selector
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

			// Token: 0x17000780 RID: 1920
			// (get) Token: 0x06005051 RID: 20561 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06005052 RID: 20562 RVA: 0x0000216D File Offset: 0x0000036D
			public SelectionButton m_BackButton
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

			// Token: 0x17000781 RID: 1921
			// (get) Token: 0x06005053 RID: 20563 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06005054 RID: 20564 RVA: 0x0000216D File Offset: 0x0000036D
			public Action onClickBackButtonCallback
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

			// Token: 0x06005055 RID: 20565 RVA: 0x0000216D File Offset: 0x0000036D
			private void Awake()
			{
			}

			// Token: 0x06005056 RID: 20566 RVA: 0x0000216D File Offset: 0x0000036D
			public void InitializeElements()
			{
			}

			// Token: 0x06005057 RID: 20567 RVA: 0x0000216D File Offset: 0x0000036D
			private void InitShottcutBack()
			{
			}

			// Token: 0x04008E6D RID: 36461
			private ElementObjectManager m_Eom;

			// Token: 0x04008E6E RID: 36462
			private bool isInitialized;
		}

		// Token: 0x02000AC6 RID: 2758
		private class FooterArea : MonoBehaviour
		{
			// Token: 0x17000782 RID: 1922
			// (get) Token: 0x06005059 RID: 20569 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x0600505A RID: 20570 RVA: 0x0000216D File Offset: 0x0000036D
			public Action onClickPreviewButtonCallback
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

			// Token: 0x17000783 RID: 1923
			// (set) Token: 0x0600505B RID: 20571 RVA: 0x0000216D File Offset: 0x0000036D
			public string buttonText
			{
				set
				{
				}
			}

			// Token: 0x0600505C RID: 20572 RVA: 0x0000216D File Offset: 0x0000036D
			private void Awake()
			{
			}

			// Token: 0x0600505D RID: 20573 RVA: 0x0000216D File Offset: 0x0000036D
			public void InitializeElements()
			{
			}

			// Token: 0x04008E6F RID: 36463
			private ElementObjectManager m_Eom;

			// Token: 0x04008E70 RID: 36464
			private bool isInitialized;

			// Token: 0x04008E71 RID: 36465
			private SelectionButton m_PreviewButton;

			// Token: 0x04008E72 RID: 36466
			private ExtendedTextMeshProUGUI m_PreviewButtonText;
		}

		// Token: 0x02000AC7 RID: 2759
		public enum EditType
		{
			// Token: 0x04008E74 RID: 36468
			USER_PROFILE,
			// Token: 0x04008E75 RID: 36469
			ACCESSORY,
			// Token: 0x04008E76 RID: 36470
			ACCESSORY_TOURNAMENT,
			// Token: 0x04008E77 RID: 36471
			ACCESSORY_EXHIBITION,
			// Token: 0x04008E78 RID: 36472
			ACCESSORY_CUP,
			// Token: 0x04008E79 RID: 36473
			ACCESSORY_WCS,
			// Token: 0x04008E7A RID: 36474
			ACCESSORY_RANKEVENT,
			// Token: 0x04008E7B RID: 36475
			ACCESSORY_DUELTRIAL,
			// Token: 0x04008E7C RID: 36476
			ACCESSORY_VERSUS
		}

		// Token: 0x02000AC8 RID: 2760
		internal abstract class ProfileEdit
		{
			// Token: 0x17000784 RID: 1924
			// (get) Token: 0x0600505F RID: 20575 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool isActive
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000785 RID: 1925
			// (get) Token: 0x06005060 RID: 20576 RVA: 0x0000216A File Offset: 0x0000036A
			public ToggleWidget menuToggle
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000786 RID: 1926
			// (get) Token: 0x06005061 RID: 20577 RVA: 0x0000216A File Offset: 0x0000036A
			public GameObject view
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06005062 RID: 20578 RVA: 0x00002739 File Offset: 0x00000939
			protected ProfileEdit(string clientWorkKeyName, string saveKeyName, GameObject template, Transform parent)
			{
			}

			// Token: 0x06005063 RID: 20579 RVA: 0x0000216D File Offset: 0x0000036D
			internal virtual void Init()
			{
			}

			// Token: 0x06005064 RID: 20580 RVA: 0x0000216A File Offset: 0x0000036A
			internal virtual object GetCurrent()
			{
				return null;
			}

			// Token: 0x06005065 RID: 20581 RVA: 0x0000216D File Offset: 0x0000036D
			internal virtual void SetCurrent(object current)
			{
			}

			// Token: 0x06005066 RID: 20582
			internal abstract void EnterFromMenu();

			// Token: 0x06005067 RID: 20583 RVA: 0x0000216D File Offset: 0x0000036D
			internal void SetActiveRoot(bool rootActive)
			{
			}

			// Token: 0x06005068 RID: 20584 RVA: 0x0000216D File Offset: 0x0000036D
			internal virtual void UpdateView()
			{
			}

			// Token: 0x06005069 RID: 20585 RVA: 0x0000216D File Offset: 0x0000036D
			internal virtual void SetView(GameObject view, Action callback)
			{
			}

			// Token: 0x0600506A RID: 20586 RVA: 0x0000216A File Offset: 0x0000036A
			internal virtual ToggleWidget CreateSideToggleWidget(GameObject template, Transform parent, string label, bool defaultBtn = false)
			{
				return null;
			}

			// Token: 0x0600506B RID: 20587 RVA: 0x0000216A File Offset: 0x0000036A
			internal virtual List<ValueTuple<string, object>> GetSaveData(Dictionary<string, object> dic)
			{
				return null;
			}

			// Token: 0x04008E7D RID: 36477
			private const string k_ELabelTextOn = "TextOn";

			// Token: 0x04008E7E RID: 36478
			private const string k_ELabelTextOff = "TextOff";

			// Token: 0x04008E7F RID: 36479
			protected ElementObjectManager m_Eom;

			// Token: 0x04008E80 RID: 36480
			protected GameObject m_Edit;

			// Token: 0x04008E81 RID: 36481
			protected GameObject m_View;

			// Token: 0x04008E82 RID: 36482
			protected ToggleWidget m_MenuToggle;

			// Token: 0x04008E83 RID: 36483
			protected GameObject m_Loading;

			// Token: 0x04008E84 RID: 36484
			protected Selector m_Selector;

			// Token: 0x04008E85 RID: 36485
			protected Selector m_ParentSelector;

			// Token: 0x04008E86 RID: 36486
			protected object current;

			// Token: 0x04008E87 RID: 36487
			protected bool isInitialized;

			// Token: 0x04008E88 RID: 36488
			internal readonly string clientWorkKeyName;

			// Token: 0x04008E89 RID: 36489
			internal readonly string saveKeyName;

			// Token: 0x04008E8A RID: 36490
			protected Action onUpdateViewCallback;

			// Token: 0x04008E8B RID: 36491
			public Action onClickPreviewCallback;
		}

		// Token: 0x02000AC9 RID: 2761
		internal class ProfileEditText : ProfileEditViewController.ProfileEdit
		{
			// Token: 0x0600506C RID: 20588 RVA: 0x000F4BA6 File Offset: 0x000F2DA6
			internal ProfileEditText(string clientWorkKeyName, string saveKeyName, GameObject tmpEdit, Transform parentEdit, int groupPriority)
				: base(null, null, null, null)
			{
			}

			// Token: 0x0600506D RID: 20589 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void SetCurrent(object name)
			{
			}

			// Token: 0x0600506E RID: 20590 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void Init()
			{
			}

			// Token: 0x0600506F RID: 20591 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void EnterFromMenu()
			{
			}

			// Token: 0x04008E8C RID: 36492
			private InputFieldWidget m_InputFieldWidget;

			// Token: 0x04008E8D RID: 36493
			private SelectionButton m_InputButton;

			// Token: 0x04008E8E RID: 36494
			private readonly string INPUT_NAME_LABEL;

			// Token: 0x04008E8F RID: 36495
			private readonly string INPUT_NAME_BTN_LABEL;
		}

		// Token: 0x02000ACA RID: 2762
		internal class ProfileEditImage : ProfileEditViewController.ProfileEdit
		{
			// Token: 0x17000787 RID: 1927
			// (get) Token: 0x06005070 RID: 20592 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06005071 RID: 20593 RVA: 0x0000216D File Offset: 0x0000036D
			public string itemName
			{
				get
				{
					return null;
				}
				set
				{
				}
			}

			// Token: 0x17000788 RID: 1928
			// (get) Token: 0x06005072 RID: 20594 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06005073 RID: 20595 RVA: 0x0000216D File Offset: 0x0000036D
			public int selectedItemId
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

			// Token: 0x17000789 RID: 1929
			// (get) Token: 0x06005074 RID: 20596 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06005075 RID: 20597 RVA: 0x0000216D File Offset: 0x0000036D
			public Action onCompleteInitiazeCallback
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

			// Token: 0x06005076 RID: 20598 RVA: 0x000F4BA6 File Offset: 0x000F2DA6
			internal ProfileEditImage(string clientWorkKeyName, string saveKeyName, GameObject template, Transform parent, List<object> itemList, TextGroupLoadHolder textGroupLoadHolder)
				: base(null, null, null, null)
			{
			}

			// Token: 0x06005077 RID: 20599 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void Init()
			{
			}

			// Token: 0x06005078 RID: 20600 RVA: 0x0000216D File Offset: 0x0000036D
			public void FocusImmidiate(int dataindex, bool selectItem = false, bool isIni = true)
			{
			}

			// Token: 0x06005079 RID: 20601 RVA: 0x0000216A File Offset: 0x0000036A
			private IEnumerator DelaySelect(int dataindex, bool selectItem = false, bool isIni = true)
			{
				return null;
			}

			// Token: 0x0600507A RID: 20602 RVA: 0x0000216D File Offset: 0x0000036D
			internal virtual void SetActiveViewItem(bool isActive)
			{
			}

			// Token: 0x0600507B RID: 20603 RVA: 0x0000216D File Offset: 0x0000036D
			public void OnItemSetData(GameObject gob, int dataindex)
			{
			}

			// Token: 0x0600507C RID: 20604 RVA: 0x0000216D File Offset: 0x0000036D
			public void OnGsvStanby()
			{
			}

			// Token: 0x0600507D RID: 20605 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void EnterFromMenu()
			{
			}

			// Token: 0x04008E90 RID: 36496
			protected readonly string IMG_SELECTED_LABEL;

			// Token: 0x04008E91 RID: 36497
			protected readonly string SCROLL_LABEL;

			// Token: 0x04008E92 RID: 36498
			protected readonly string IMG_LABEL;

			// Token: 0x04008E93 RID: 36499
			protected readonly string TXT_LABEL;

			// Token: 0x04008E94 RID: 36500
			protected readonly string TXT_LABEL_ON;

			// Token: 0x04008E95 RID: 36501
			protected readonly string TXT_LABEL_OFF;

			// Token: 0x04008E96 RID: 36502
			protected readonly string BTN_LABEL;

			// Token: 0x04008E97 RID: 36503
			protected readonly string LOADING_LABEL;

			// Token: 0x04008E98 RID: 36504
			protected TextMeshProUGUI m_ItemName;

			// Token: 0x04008E99 RID: 36505
			protected TextGroupLoadHolder textGroupLoadHolder;

			// Token: 0x04008E9A RID: 36506
			protected InfinityScrollView m_InfinityScroll;

			// Token: 0x04008E9B RID: 36507
			protected List<object> itemList;
		}

		// Token: 0x02000ACB RID: 2763
		internal class ProfileEditPickCards : ProfileEditViewController.ProfileEdit
		{
			// Token: 0x0600507E RID: 20606 RVA: 0x000F4BA6 File Offset: 0x000F2DA6
			internal ProfileEditPickCards(string clientWorkKeyName, string saveKeyName, GameObject tmpEdit, Transform parentEdit, int caseId, int sleeveId, Dictionary<string, object> pickCards)
				: base(null, null, null, null)
			{
			}

			// Token: 0x0600507F RID: 20607 RVA: 0x0000216D File Offset: 0x0000036D
			public void UpdateDeckcase(int deckcaseId)
			{
			}

			// Token: 0x06005080 RID: 20608 RVA: 0x0000216D File Offset: 0x0000036D
			public void UpdateSleeve(int sleeveId)
			{
			}

			// Token: 0x06005081 RID: 20609 RVA: 0x0000216D File Offset: 0x0000036D
			public void UpdateCards(Dictionary<string, object> pickCards)
			{
			}

			// Token: 0x06005082 RID: 20610 RVA: 0x0000216A File Offset: 0x0000036A
			internal override List<ValueTuple<string, object>> GetSaveData(Dictionary<string, object> dic)
			{
				return null;
			}

			// Token: 0x06005083 RID: 20611 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void SetCurrent(object name)
			{
			}

			// Token: 0x06005084 RID: 20612 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void EnterFromMenu()
			{
			}

			// Token: 0x06005085 RID: 20613 RVA: 0x0000216D File Offset: 0x0000036D
			internal void EnterFromMenu(Dictionary<string, object> updatedDict)
			{
			}

			// Token: 0x04008E9C RID: 36508
			internal DeckCaseWidget deckCaseWidget;

			// Token: 0x04008E9D RID: 36509
			internal bool shouldSavePicks;

			// Token: 0x04008E9E RID: 36510
			public int[] pickupCards;

			// Token: 0x04008E9F RID: 36511
			public int[] pickupDecos;

			// Token: 0x04008EA0 RID: 36512
			public int deckcaseId;

			// Token: 0x04008EA1 RID: 36513
			public int protectorId;

			// Token: 0x04008EA2 RID: 36514
			private int initialDeckcaseId;

			// Token: 0x04008EA3 RID: 36515
			private int initialProtectorId;

			// Token: 0x04008EA4 RID: 36516
			internal const int PICKUP_MAX = 3;
		}

		// Token: 0x02000ACC RID: 2764
		internal class ProfileEditTag : ProfileEditViewController.ProfileEdit
		{
			// Token: 0x1700078A RID: 1930
			// (get) Token: 0x06005086 RID: 20614 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06005087 RID: 20615 RVA: 0x0000216D File Offset: 0x0000036D
			public int selectedItemId
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

			// Token: 0x1700078B RID: 1931
			// (get) Token: 0x06005088 RID: 20616 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06005089 RID: 20617 RVA: 0x0000216D File Offset: 0x0000036D
			public bool isSelectingTag
			{
				[CompilerGenerated]
				get
				{
					return false;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x1700078C RID: 1932
			// (get) Token: 0x0600508A RID: 20618 RVA: 0x000029CC File Offset: 0x00000BCC
			public int editingIdx
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x0600508B RID: 20619 RVA: 0x000F4BA6 File Offset: 0x000F2DA6
			internal ProfileEditTag(string clientWorkKeyName, string saveKeyName, GameObject tmpEdit, Transform parentEdit, List<object> tagList)
				: base(null, null, null, null)
			{
			}

			// Token: 0x0600508C RID: 20620 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void Init()
			{
			}

			// Token: 0x0600508D RID: 20621 RVA: 0x0000216D File Offset: 0x0000036D
			public void SelectEditingMyTag()
			{
			}

			// Token: 0x0600508E RID: 20622 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void SetCurrent(object dictionary)
			{
			}

			// Token: 0x0600508F RID: 20623 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void EnterFromMenu()
			{
			}

			// Token: 0x06005090 RID: 20624 RVA: 0x0000216A File Offset: 0x0000036A
			internal override List<ValueTuple<string, object>> GetSaveData(Dictionary<string, object> dic)
			{
				return null;
			}

			// Token: 0x06005091 RID: 20625 RVA: 0x0000216D File Offset: 0x0000036D
			private void OnItemSetData(GameObject gob, int dataindex)
			{
			}

			// Token: 0x06005092 RID: 20626 RVA: 0x0000216D File Offset: 0x0000036D
			private void OnGsvStanby()
			{
			}

			// Token: 0x06005093 RID: 20627 RVA: 0x0000216D File Offset: 0x0000036D
			private void UpdateMytag()
			{
			}

			// Token: 0x06005094 RID: 20628 RVA: 0x0000216D File Offset: 0x0000036D
			private void OnClickMyTag(int tagNo, bool select = true)
			{
			}

			// Token: 0x06005095 RID: 20629 RVA: 0x0000216D File Offset: 0x0000036D
			private void UpdateTagScroll()
			{
			}

			// Token: 0x06005096 RID: 20630 RVA: 0x0000216D File Offset: 0x0000036D
			private void UpdateMyTagImage()
			{
			}

			// Token: 0x04008EA5 RID: 36517
			private const string TEMPLATE_LABEL = "Template";

			// Token: 0x04008EA6 RID: 36518
			private const string OBJ_MYTAG_LABEL = "MyTagField";

			// Token: 0x04008EA7 RID: 36519
			private const string TXT_LABEL_ON = "TextOn";

			// Token: 0x04008EA8 RID: 36520
			private const string TXT_LABEL_OFF = "TextOff";

			// Token: 0x04008EA9 RID: 36521
			private const string IMG_ON_LABEL = "ImageOn";

			// Token: 0x04008EAA RID: 36522
			private const string IMG_OFF_LABEL = "ImageOff";

			// Token: 0x04008EAB RID: 36523
			internal readonly int MYTAG_EMPTY;

			// Token: 0x04008EAC RID: 36524
			private List<GameObject> myTags;

			// Token: 0x04008EAD RID: 36525
			private List<object> tagList;

			// Token: 0x04008EAE RID: 36526
			private List<ProfileEditViewController.ProfileEditTag.Data> dataList;

			// Token: 0x04008EAF RID: 36527
			private GameObject myTagTemplate;

			// Token: 0x04008EB0 RID: 36528
			private GameObject myTagField;

			// Token: 0x04008EB1 RID: 36529
			private InfinityScrollView m_InfinityScroll;

			// Token: 0x04008EB2 RID: 36530
			private int editingTagIndex;

			// Token: 0x02000ACD RID: 2765
			private class Data
			{
				// Token: 0x06005097 RID: 20631 RVA: 0x00002739 File Offset: 0x00000939
				public Data(bool interactable, object value)
				{
				}

				// Token: 0x04008EB3 RID: 36531
				public bool interactable;

				// Token: 0x04008EB4 RID: 36532
				public object value;
			}
		}
	}
}
