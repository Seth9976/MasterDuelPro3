using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YgomGame.Help;
using YgomGame.Menu;
using YgomSystem.Network;
using YgomSystem.UI;
using YgomSystem.Utility;

namespace YgomGame.Mission
{
	// Token: 0x02000A3E RID: 2622
	public class MissionViewController : BaseMenuViewController, IBackButtonWithoutSCSupported, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06004C1B RID: 19483 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool setSurfaceActiveOnInitialize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06004C1C RID: 19484 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool setProgressOnInitialize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06004C1D RID: 19485 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override int selectorPriorityAddRange
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06004C1E RID: 19486 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004C1F RID: 19487 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnUpdatedAll()
		{
		}

		// Token: 0x06004C20 RID: 19488 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnUpdatedContainMissions()
		{
		}

		// Token: 0x06004C21 RID: 19489 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnPrevChangeTabIndex(int oldIdx)
		{
		}

		// Token: 0x06004C22 RID: 19490 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnChangeTabIndex(int newIdx)
		{
		}

		// Token: 0x06004C23 RID: 19491 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnUpdatedTabNewEvent(int tabIdx)
		{
		}

		// Token: 0x06004C24 RID: 19492 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCreatedGoalHolder(MissionGoalHolderWidget goalHolderWidget)
		{
		}

		// Token: 0x06004C25 RID: 19493 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnUpdateGoalHolder(MissionGoalHolderWidget goalHolderWidget, int idx)
		{
		}

		// Token: 0x06004C26 RID: 19494 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnSelectedGoalHolder(MissionGoalHolderWidget goalHolder)
		{
		}

		// Token: 0x06004C27 RID: 19495 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDeselectedGoalHolder(MissionGoalHolderWidget goalHolder)
		{
		}

		// Token: 0x06004C28 RID: 19496 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickGoalHolder(MissionGoalHolderWidget goalHolder)
		{
		}

		// Token: 0x06004C29 RID: 19497 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCreatedGoalPage(GameObject goalPageEntity, MissionPanelWidget ownerPanel)
		{
		}

		// Token: 0x06004C2A RID: 19498 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnFocusGoalPage(GameObject goalPageEntity, int dataidx, bool isselect, bool initialize)
		{
		}

		// Token: 0x06004C2B RID: 19499 RVA: 0x0000216A File Offset: 0x0000036A
		private IReadOnlyList<ValueTuple<SelectionItem, int, int>> OnGoalCollectSelectionItems(GameObject goalPageEntity)
		{
			return null;
		}

		// Token: 0x06004C2C RID: 19500 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnUpdateGoalPage(GameObject goalPageEntity, int idx)
		{
		}

		// Token: 0x06004C2D RID: 19501 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDeactivateGoalPage(GameObject goalPageEntity)
		{
		}

		// Token: 0x06004C2E RID: 19502 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool OnGoalEdgeTransition(SelectionItem selectionItem, PadInputDirection direction)
		{
			return false;
		}

		// Token: 0x06004C2F RID: 19503 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCreatedPanel(GameObject panelEntity)
		{
		}

		// Token: 0x06004C30 RID: 19504 RVA: 0x0000216A File Offset: 0x0000036A
		private IReadOnlyList<ValueTuple<SelectionItem, int, int>> OnPanelCollectSelectionItems(GameObject panelEntity)
		{
			return null;
		}

		// Token: 0x06004C31 RID: 19505 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnActivatePanel(GameObject panelEntity)
		{
		}

		// Token: 0x06004C32 RID: 19506 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDeactivatePanel(GameObject panelEntity)
		{
		}

		// Token: 0x06004C33 RID: 19507 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnUpdatePanel(GameObject panelEntity, int missionIdx)
		{
		}

		// Token: 0x06004C34 RID: 19508 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnReadyUpdateGoalsPager(MissionGoalsPagerWidget goalsPager, int pageCount, int pageIdx)
		{
		}

		// Token: 0x06004C35 RID: 19509 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnUpdateLimitTextCallback(TMP_Text text, long remainSec)
		{
		}

		// Token: 0x06004C36 RID: 19510 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnUpdateRecieveLimitTextCallback(TMP_Text text, long remainSec)
		{
		}

		// Token: 0x06004C37 RID: 19511 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool OnSelectorSelectedPanel()
		{
			return false;
		}

		// Token: 0x06004C38 RID: 19512 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnFocusPanel(GameObject entity, int idx, bool isSelect, bool isInitializeSelect)
		{
		}

		// Token: 0x06004C39 RID: 19513 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnPageChanged(MissionGoalsPagerWidget changedPager)
		{
		}

		// Token: 0x06004C3A RID: 19514 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnPlayGoalPagingBegin()
		{
		}

		// Token: 0x06004C3B RID: 19515 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnPlayGoalPagingEnd()
		{
		}

		// Token: 0x06004C3C RID: 19516 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenRecieveResultSingleDialog(Action callback)
		{
		}

		// Token: 0x06004C3D RID: 19517 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenRecieveResultBulkDialog(Action callback)
		{
		}

		// Token: 0x06004C3E RID: 19518 RVA: 0x0000216D File Offset: 0x0000036D
		private void InsertBulkRecievedMissionContext(MissionBulkRecieveDialogWidget widget, TabContext tabCtx)
		{
		}

		// Token: 0x06004C3F RID: 19519 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitSelectorHistoryHandlers()
		{
		}

		// Token: 0x06004C40 RID: 19520 RVA: 0x0000216A File Offset: 0x0000036A
		private MissionSelectorHistoryHandler CreateTabSelectorHistoryHandler()
		{
			return null;
		}

		// Token: 0x06004C41 RID: 19521 RVA: 0x0000216A File Offset: 0x0000036A
		private MissionSelectorHistoryHandler CreateMissionSelectorHistoryHandler()
		{
			return null;
		}

		// Token: 0x06004C42 RID: 19522 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCreatedTab(GameObject entity)
		{
		}

		// Token: 0x06004C43 RID: 19523 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickTab(MissionTabWidget tabWidget)
		{
		}

		// Token: 0x06004C44 RID: 19524 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnUpdateTab(GameObject entity, int idx)
		{
		}

		// Token: 0x06004C45 RID: 19525 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnFocusTab(GameObject entity, int idx, bool isselect, bool isinitialselect)
		{
		}

		// Token: 0x06004C46 RID: 19526 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(int tabId = 0, int poolId = 0)
		{
		}

		// Token: 0x06004C47 RID: 19527 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenOnHome(int tabId = 0, int poolId = 0)
		{
		}

		// Token: 0x06004C48 RID: 19528 RVA: 0x0000216D File Offset: 0x0000036D
		private static void CheckLaunch(int tabId = 0, int poolId = 0, Action onSuccess = null, Action onFailed = null)
		{
		}

		// Token: 0x06004C49 RID: 19529 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004C4A RID: 19530 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yInitialize()
		{
			return null;
		}

		// Token: 0x06004C4B RID: 19531 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yPreReserveRoutine()
		{
			return null;
		}

		// Token: 0x06004C4C RID: 19532 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06004C4D RID: 19533 RVA: 0x0000216D File Offset: 0x0000036D
		public override void TransitionStart(ViewController.TransitionType type)
		{
		}

		// Token: 0x06004C4E RID: 19534 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool TransitionUpdate(ViewController.TransitionType type)
		{
			return false;
		}

		// Token: 0x06004C4F RID: 19535 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x06004C50 RID: 19536 RVA: 0x0000216D File Offset: 0x0000036D
		private void RefreshTabList(bool changedCount)
		{
		}

		// Token: 0x06004C51 RID: 19537 RVA: 0x0000216D File Offset: 0x0000036D
		private void RefreshMissionLabel()
		{
		}

		// Token: 0x06004C52 RID: 19538 RVA: 0x0000216D File Offset: 0x0000036D
		private void RefreshMissionList(bool changedCount)
		{
		}

		// Token: 0x06004C53 RID: 19539 RVA: 0x0000216D File Offset: 0x0000036D
		private void RefreshMissionListMessage()
		{
		}

		// Token: 0x06004C54 RID: 19540 RVA: 0x0000216D File Offset: 0x0000036D
		private void RefreshBulkRecieveButton()
		{
		}

		// Token: 0x06004C55 RID: 19541 RVA: 0x0000216D File Offset: 0x0000036D
		private void RequestBadgeDelete(Action onComplete = null)
		{
		}

		// Token: 0x06004C56 RID: 19542 RVA: 0x0000216D File Offset: 0x0000036D
		private void RequestGetMissionListApi(Action omComplete = null)
		{
		}

		// Token: 0x06004C57 RID: 19543 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCompleteRecieveReward(Handle h, bool isBulk)
		{
		}

		// Token: 0x06004C58 RID: 19544 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnRecieveResult(bool isBulk)
		{
		}

		// Token: 0x06004C59 RID: 19545 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnInputAnalogDirection(SelectorManager.AnalogType analogType, PadInputDirection dir)
		{
		}

		// Token: 0x06004C5A RID: 19546 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickBulkRecieveButton()
		{
		}

		// Token: 0x06004C5B RID: 19547 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yBulkRecieveRoutine()
		{
			return null;
		}

		// Token: 0x04008A13 RID: 35347
		private const string k_RLabelTMPanelCompleteMission = "TMPanelCompleteMission";

		// Token: 0x04008A14 RID: 35348
		private const string k_RLabelTMPanelHideMission = "TMPanelHideMission";

		// Token: 0x04008A15 RID: 35349
		private const string k_RLabelTMPanelNewMission = "TMPanelNewMission";

		// Token: 0x04008A16 RID: 35350
		private const string k_RLabelTMPanelFocusMission = "TMPanelFocusMission";

		// Token: 0x04008A17 RID: 35351
		public const string k_PrefabPath = "Mission/Mission";

		// Token: 0x04008A18 RID: 35352
		private const string k_ArgKeyTabId = "tabId";

		// Token: 0x04008A19 RID: 35353
		private const string k_ArgKeyPoolId = "poolId";

		// Token: 0x04008A1A RID: 35354
		private readonly string k_ELabelFooterOverSelector;

		// Token: 0x04008A1B RID: 35355
		private readonly string k_ELabelTabList;

		// Token: 0x04008A1C RID: 35356
		private readonly string k_ELabelMissionList;

		// Token: 0x04008A1D RID: 35357
		private readonly string k_ELabelEmptyGroup;

		// Token: 0x04008A1E RID: 35358
		private readonly string k_ELabelEmptyText;

		// Token: 0x04008A1F RID: 35359
		private readonly string k_ELabelCancelButton;

		// Token: 0x04008A20 RID: 35360
		private readonly string k_ELabelBackShortcut;

		// Token: 0x04008A21 RID: 35361
		private readonly string k_ELabelAnalogDirectionItem;

		// Token: 0x04008A22 RID: 35362
		private readonly string k_ELabelCautionButton;

		// Token: 0x04008A23 RID: 35363
		private readonly string k_ELabelBulkRecieveButton;

		// Token: 0x04008A24 RID: 35364
		private readonly string k_ELabelInEffectCover;

		// Token: 0x04008A25 RID: 35365
		private readonly string k_ELabelInEffectSkipButton;

		// Token: 0x04008A26 RID: 35366
		private readonly string k_RLabelRecieveDialogListPref;

		// Token: 0x04008A27 RID: 35367
		[SerializeField]
		private string m_CautionHelpPath;

		// Token: 0x04008A28 RID: 35368
		private bool m_IsHighEnd;

		// Token: 0x04008A29 RID: 35369
		private int m_RegistedGoThroughPriority;

		// Token: 0x04008A2A RID: 35370
		private int m_GaugeStepLimit;

		// Token: 0x04008A2B RID: 35371
		private AssetReferer m_AssetReferer;

		// Token: 0x04008A2C RID: 35372
		private PropertyContainer m_PropertyContainer;

		// Token: 0x04008A2D RID: 35373
		private MissionViewController.WidgetFactory m_WidgetFactory;

		// Token: 0x04008A2E RID: 35374
		private readonly MissionRootContext m_RootContext;

		// Token: 0x04008A2F RID: 35375
		private MissionViewController.MissionInOutPlayer m_InOutPlayer;

		// Token: 0x04008A30 RID: 35376
		private MissionTabListWidget m_TabListWidget;

		// Token: 0x04008A31 RID: 35377
		private MissionListWidget m_MissionListWidget;

		// Token: 0x04008A32 RID: 35378
		private readonly Dictionary<GameObject, MissionTabWidget> m_TabWidgetsMap;

		// Token: 0x04008A33 RID: 35379
		private readonly Dictionary<GameObject, MissionPanelWidget> m_PanelWidgetsMap;

		// Token: 0x04008A34 RID: 35380
		private readonly Dictionary<GameObject, MissionGoalsWidget> m_GoalsWidgetsMap;

		// Token: 0x04008A35 RID: 35381
		private MissionBulkRecieveButtonWidget m_BulkRecieveButtonWidget;

		// Token: 0x04008A36 RID: 35382
		private readonly MissionSelectorHistory m_SelectorHistory;

		// Token: 0x04008A37 RID: 35383
		private MissionSelectorHistoryHandler m_MissionSelectorHistoryHandler;

		// Token: 0x04008A38 RID: 35384
		private HelpMappingData m_HelpMappingData;

		// Token: 0x04008A39 RID: 35385
		private List<int> m_DeletedBadgeIds;

		// Token: 0x02000A3F RID: 2623
		public class MissionInOutPlayer
		{
			// Token: 0x17000710 RID: 1808
			// (get) Token: 0x06004C5D RID: 19549 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool isPlaying
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000711 RID: 1809
			// (get) Token: 0x06004C5E RID: 19550 RVA: 0x0000216A File Offset: 0x0000036A
			public Selector coverSelector
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000712 RID: 1810
			// (get) Token: 0x06004C5F RID: 19551 RVA: 0x0000216A File Offset: 0x0000036A
			public SelectionButton skipButton
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06004C60 RID: 19552 RVA: 0x0000216D File Offset: 0x0000036D
			public void Init(MissionViewController vc, Selector coverSelector, SelectionButton skipButton)
			{
			}

			// Token: 0x06004C61 RID: 19553 RVA: 0x0000216D File Offset: 0x0000036D
			public void ClearEntryMissions()
			{
			}

			// Token: 0x06004C62 RID: 19554 RVA: 0x0000216D File Offset: 0x0000036D
			public void ClearRemoveMissions()
			{
			}

			// Token: 0x06004C63 RID: 19555 RVA: 0x0000216D File Offset: 0x0000036D
			public void ClearRecievedMissions()
			{
			}

			// Token: 0x06004C64 RID: 19556 RVA: 0x0000216D File Offset: 0x0000036D
			public void ClearHidedMissions()
			{
			}

			// Token: 0x06004C65 RID: 19557 RVA: 0x0000216D File Offset: 0x0000036D
			public void AddEntryMission(int missionId)
			{
			}

			// Token: 0x06004C66 RID: 19558 RVA: 0x0000216D File Offset: 0x0000036D
			public void AddRemoveMission(int missionId)
			{
			}

			// Token: 0x06004C67 RID: 19559 RVA: 0x0000216D File Offset: 0x0000036D
			public void AddRecievedMissions(List<int> missionIds, List<int> goalposs)
			{
			}

			// Token: 0x06004C68 RID: 19560 RVA: 0x0000216D File Offset: 0x0000036D
			public void AddHidedMission(int missionId)
			{
			}

			// Token: 0x06004C69 RID: 19561 RVA: 0x0000216D File Offset: 0x0000036D
			public void Terminate()
			{
			}

			// Token: 0x06004C6A RID: 19562 RVA: 0x0000216D File Offset: 0x0000036D
			public void Play(bool isResult = false, bool isBulk = false, Action onComplete = null)
			{
			}

			// Token: 0x06004C6B RID: 19563 RVA: 0x0000216A File Offset: 0x0000036A
			private IEnumerator yPlay(bool isResult = false, bool isBulk = false, Action onComplete = null)
			{
				return null;
			}

			// Token: 0x06004C6C RID: 19564 RVA: 0x0000216A File Offset: 0x0000036A
			private IEnumerator yPlayRecieved(List<int> recievedMissions, List<int> recievedGoalPoss, List<int> removedMissions, bool isFocus)
			{
				return null;
			}

			// Token: 0x06004C6D RID: 19565 RVA: 0x0000216A File Offset: 0x0000036A
			private IEnumerator yPlayRecievedResult(List<int> removedMissions, List<int> hidedMissions, bool isBulk = false)
			{
				return null;
			}

			// Token: 0x06004C6E RID: 19566 RVA: 0x0000216A File Offset: 0x0000036A
			private IEnumerator yPlayEntry(List<int> entryMissions, bool isRemovedMission)
			{
				return null;
			}

			// Token: 0x06004C6F RID: 19567 RVA: 0x0000216D File Offset: 0x0000036D
			private void CleanupRecieveCtxState()
			{
			}

			// Token: 0x06004C70 RID: 19568 RVA: 0x0000216D File Offset: 0x0000036D
			private void CleanupRecieveWidgetState()
			{
			}

			// Token: 0x06004C71 RID: 19569 RVA: 0x0000216A File Offset: 0x0000036A
			private IEnumerator yPlayTweenTarget(GameObject target, string label)
			{
				return null;
			}

			// Token: 0x06004C72 RID: 19570 RVA: 0x0000216A File Offset: 0x0000036A
			private List<int> PopReservedMissionIds(List<int> source)
			{
				return null;
			}

			// Token: 0x06004C73 RID: 19571 RVA: 0x000F4A24 File Offset: 0x000F2C24
			private ValueTuple<List<int>, List<int>> PopReservedMissionPairIds(List<int> source, List<int> pairSource)
			{
				return default(ValueTuple<List<int>, List<int>>);
			}

			// Token: 0x06004C74 RID: 19572 RVA: 0x0000216D File Offset: 0x0000036D
			private void OnClickSkip()
			{
			}

			// Token: 0x04008A3A RID: 35386
			private const string k_PLabelSkipSpeed_RecieveFocusScroll = "SkipSpeed_RecieveFocusScroll";

			// Token: 0x04008A3B RID: 35387
			private const string k_PLabelSkipSpeed_PageSnap = "SkipSpeed_PageSnap";

			// Token: 0x04008A3C RID: 35388
			private const string k_PLabelSkipSpeed_TM = "SkipSpeed_TM";

			// Token: 0x04008A3D RID: 35389
			private const string k_PLabelSkipSpeed_GoalRecieveBetweenWait = "SkipSpeed_GoalRecieveBetweenWait";

			// Token: 0x04008A3E RID: 35390
			private const string k_PLabelSkipSpeed_GoalRecieved = "SkipSpeed_GoalRecieved";

			// Token: 0x04008A3F RID: 35391
			private const string k_PLabelBuilRecieveSE_Check = "BuilRecieveSE_Check";

			// Token: 0x04008A40 RID: 35392
			private const string k_PLabelBuilRecieveSE_CheckSkip = "BuilRecieveSE_CheckSkip";

			// Token: 0x04008A41 RID: 35393
			private readonly List<int> m_EntryMissions;

			// Token: 0x04008A42 RID: 35394
			private readonly List<int> m_HidedMissions;

			// Token: 0x04008A43 RID: 35395
			private readonly List<int> m_RemovedMissions;

			// Token: 0x04008A44 RID: 35396
			private readonly List<int> m_RecievedMissions;

			// Token: 0x04008A45 RID: 35397
			private readonly List<int> m_RecievedGoalPoss;

			// Token: 0x04008A46 RID: 35398
			private MissionViewController m_VC;

			// Token: 0x04008A47 RID: 35399
			private Selector m_CoverSelector;

			// Token: 0x04008A48 RID: 35400
			private SelectionButton m_SkipButton;

			// Token: 0x04008A49 RID: 35401
			private float m_PageSnapOriginalDuration;

			// Token: 0x04008A4A RID: 35402
			private Coroutine m_SequenceRoutine;

			// Token: 0x04008A4B RID: 35403
			private Action m_OnSkipCallback;
		}

		// Token: 0x02000A40 RID: 2624
		private class WidgetFactory
		{
			// Token: 0x06004C76 RID: 19574 RVA: 0x00002739 File Offset: 0x00000939
			public WidgetFactory(Transform root, GameObject pagePref, GameObject goalInProgressPref, GameObject goalRecievablePref, GameObject goalCompletedPref)
			{
			}

			// Token: 0x06004C77 RID: 19575 RVA: 0x0000216A File Offset: 0x0000036A
			public IEnumerator yInitialReserve(int pageReq, Dictionary<MissionGoalWidget.GoalType, int> goalReq)
			{
				return null;
			}

			// Token: 0x06004C78 RID: 19576 RVA: 0x0000216A File Offset: 0x0000036A
			public MissionPanelWidget[] GetCreatedPanelWidgets()
			{
				return null;
			}

			// Token: 0x06004C79 RID: 19577 RVA: 0x0000216D File Offset: 0x0000036D
			public void ReservePage(int count = 1)
			{
			}

			// Token: 0x06004C7A RID: 19578 RVA: 0x0000216A File Offset: 0x0000036A
			public GameObject RentPage(GameObject owner)
			{
				return null;
			}

			// Token: 0x06004C7B RID: 19579 RVA: 0x0000216D File Offset: 0x0000036D
			public void ReserveGoal(MissionGoalWidget.GoalType goalType, int count = 1)
			{
			}

			// Token: 0x06004C7C RID: 19580 RVA: 0x0000216A File Offset: 0x0000036A
			public MissionGoalWidget RentGoal(MissionGoalWidget.GoalType goalType, GameObject owner)
			{
				return null;
			}

			// Token: 0x06004C7D RID: 19581 RVA: 0x0000216D File Offset: 0x0000036D
			public void ReturnGoal(MissionGoalWidget goalWidget)
			{
			}

			// Token: 0x06004C7E RID: 19582 RVA: 0x0000216D File Offset: 0x0000036D
			private void OnCreatedGoalPref(GameObject obj, MissionGoalWidget.GoalType goalType)
			{
			}

			// Token: 0x04008A4C RID: 35404
			private readonly PrefabObjectPool m_PrefabObjectPool;

			// Token: 0x04008A4D RID: 35405
			private readonly GameObject m_PagePref;

			// Token: 0x04008A4E RID: 35406
			private readonly IReadOnlyDictionary<MissionGoalWidget.GoalType, GameObject> m_GoalPrefMap;

			// Token: 0x04008A4F RID: 35407
			private readonly Dictionary<GameObject, MissionPanelWidget> m_PanelWidgetMap;

			// Token: 0x04008A50 RID: 35408
			private readonly Dictionary<GameObject, MissionGoalWidget> m_GoalWidgetMap;

			// Token: 0x04008A51 RID: 35409
			public Action<GameObject> onCreatedPageCallback;
		}
	}
}
