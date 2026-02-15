using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using YgomGame.Menu;
using YgomGame.Utility;
using YgomSystem.Timeline;
using YgomSystem.UI;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.Solo
{
	// Token: 0x02000903 RID: 2307
	public class SoloModeViewController : BaseMenuViewController, IDynamicChangeDispHeaderSupported, IFadeSupported
	{
		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06004325 RID: 17189 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsDispSubScroll
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06004326 RID: 17190 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06004327 RID: 17191 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override int selectorPriorityAddRange
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06004328 RID: 17192 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool setProgressOnInitialize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06004329 RID: 17193 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x0600432A RID: 17194 RVA: 0x0000216D File Offset: 0x0000036D
		public string LoadedBgmLabel
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x0600432B RID: 17195 RVA: 0x0000216D File Offset: 0x0000036D
		private void CallAPISoloInfo(bool back = false, Action onSuccess = null)
		{
		}

		// Token: 0x0600432C RID: 17196 RVA: 0x0000216D File Offset: 0x0000036D
		private void CallAPISoloGateEntry(int gateID, Action OnSuccess = null)
		{
		}

		// Token: 0x0600432D RID: 17197 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x0600432E RID: 17198 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x0600432F RID: 17199 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06004330 RID: 17200 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool TransitionUpdate(ViewController.TransitionType type)
		{
			return false;
		}

		// Token: 0x06004331 RID: 17201 RVA: 0x0000216D File Offset: 0x0000036D
		public override void TransitionStart(ViewController.TransitionType type)
		{
		}

		// Token: 0x06004332 RID: 17202 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnTransitionStart(ViewController.TransitionType type)
		{
		}

		// Token: 0x06004333 RID: 17203 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnTransitionEnd(ViewController.TransitionType type)
		{
		}

		// Token: 0x06004334 RID: 17204 RVA: 0x0000216D File Offset: 0x0000036D
		private void StartTransitionCoverRoutine(Action onComplete = null)
		{
		}

		// Token: 0x06004335 RID: 17205 RVA: 0x0000216A File Offset: 0x0000036A
		public IEnumerator yTransitionCoverRoutine(Action onComplete = null)
		{
			return null;
		}

		// Token: 0x06004336 RID: 17206 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnBack()
		{
			return false;
		}

		// Token: 0x06004337 RID: 17207 RVA: 0x000F47C8 File Offset: 0x000F29C8
		public Color FadeColor(ViewController.TransitionType type)
		{
			return default(Color);
		}

		// Token: 0x06004338 RID: 17208 RVA: 0x000029CC File Offset: 0x00000BCC
		public SystemProgress.ProgressType FadeType(ViewController.TransitionType type)
		{
			return SystemProgress.ProgressType.None;
		}

		// Token: 0x06004339 RID: 17209 RVA: 0x0000216D File Offset: 0x0000036D
		private void StartTimeLine()
		{
		}

		// Token: 0x0600433A RID: 17210 RVA: 0x0000216A File Offset: 0x0000036A
		private EventPlayableAsset GetEventPlayableAsset(PlayableDirector timeline)
		{
			return null;
		}

		// Token: 0x0600433B RID: 17211 RVA: 0x0000216D File Offset: 0x0000036D
		private void EndTimeLine(int id, bool isBadge)
		{
		}

		// Token: 0x0600433C RID: 17212 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnBackTimeLine()
		{
		}

		// Token: 0x0600433D RID: 17213 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetFilterAndSortBtnStatus()
		{
		}

		// Token: 0x0600433E RID: 17214 RVA: 0x0000216A File Offset: 0x0000036A
		private string GetSortName(SoloFilterSortUtil.GateSort gateSort)
		{
			return null;
		}

		// Token: 0x0600433F RID: 17215 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetFilterCluster(bool isSet)
		{
		}

		// Token: 0x06004340 RID: 17216 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateFilteringContent(bool isReturnChapterMap = false)
		{
		}

		// Token: 0x06004341 RID: 17217 RVA: 0x0000216D File Offset: 0x0000036D
		private void CheckFilterResult(List<int> dataList, ValueTuple<int, int> currentGate, InfinityScrollView scrollView, bool isReturnChapterMap)
		{
		}

		// Token: 0x06004342 RID: 17218 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yMovePage(InfinityScrollView isv, int targetIndex, Action onComplete = null)
		{
			return null;
		}

		// Token: 0x06004343 RID: 17219 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetDirtyHeader(bool isDisp)
		{
		}

		// Token: 0x06004344 RID: 17220 RVA: 0x000029CC File Offset: 0x00000BCC
		public HeaderViewController.IsDispHeader IsDispContents()
		{
			return (HeaderViewController.IsDispHeader)0;
		}

		// Token: 0x06004345 RID: 17221 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateData()
		{
		}

		// Token: 0x06004346 RID: 17222 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool CheckHaveCanUnlockChapter(int gateID)
		{
			return false;
		}

		// Token: 0x06004347 RID: 17223 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateBadge()
		{
		}

		// Token: 0x06004348 RID: 17224 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializeScrollDataList()
		{
		}

		// Token: 0x06004349 RID: 17225 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateScrollDataList()
		{
		}

		// Token: 0x0600434A RID: 17226 RVA: 0x0000216A File Offset: 0x0000036A
		private List<int> FilteringGate(List<int> dataList)
		{
			return null;
		}

		// Token: 0x0600434B RID: 17227 RVA: 0x0000216A File Offset: 0x0000036A
		private List<int> SortingGate(List<int> dataList)
		{
			return null;
		}

		// Token: 0x0600434C RID: 17228 RVA: 0x0000216D File Offset: 0x0000036D
		private void DispUnlockGate()
		{
		}

		// Token: 0x0600434D RID: 17229 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateView(SoloModeViewController.Data data)
		{
		}

		// Token: 0x0600434E RID: 17230 RVA: 0x0000216D File Offset: 0x0000036D
		private void HideView(bool isHide = true)
		{
		}

		// Token: 0x0600434F RID: 17231 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCreatedEntity(GameObject go)
		{
		}

		// Token: 0x06004350 RID: 17232 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnItemSetDataMain(GameObject gob, int dataindex)
		{
		}

		// Token: 0x06004351 RID: 17233 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnItemSetDataSub(GameObject gob, int dataindex)
		{
		}

		// Token: 0x06004352 RID: 17234 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenSubScroll(List<int> dataList)
		{
		}

		// Token: 0x06004353 RID: 17235 RVA: 0x0000216D File Offset: 0x0000036D
		private void CloseSubScroll()
		{
		}

		// Token: 0x040081C8 RID: 33224
		private readonly string SCROLL_LABEL;

		// Token: 0x040081C9 RID: 33225
		private readonly string SCROLL_SUB_LABEL;

		// Token: 0x040081CA RID: 33226
		private readonly string OBJ_TUTORIAL_LABEL;

		// Token: 0x040081CB RID: 33227
		private readonly string OBJ_BLACKOUT_LABEL;

		// Token: 0x040081CC RID: 33228
		private readonly string OBJ_ORB_PLATE_LABEL;

		// Token: 0x040081CD RID: 33229
		private readonly string ROOT_LABEL;

		// Token: 0x040081CE RID: 33230
		private readonly string ROOT_VIEW_LABEL;

		// Token: 0x040081CF RID: 33231
		private readonly string ROOT_SUBGATE_LABEL;

		// Token: 0x040081D0 RID: 33232
		private readonly string TXT_GATENAME_LABEL;

		// Token: 0x040081D1 RID: 33233
		private readonly string TXT_CLEAR_LABEL;

		// Token: 0x040081D2 RID: 33234
		private readonly string TXT_CONDITIONS_LABEL;

		// Token: 0x040081D3 RID: 33235
		private readonly string TXT_COMPLETE_LABEL;

		// Token: 0x040081D4 RID: 33236
		private readonly string TXT_OVERVIEW_LABEL;

		// Token: 0x040081D5 RID: 33237
		private readonly string IMG_LOCK_LABEL;

		// Token: 0x040081D6 RID: 33238
		private readonly string IMG_GATE_LABEL;

		// Token: 0x040081D7 RID: 33239
		private readonly string IMG_GATE_LOCK_LABEL;

		// Token: 0x040081D8 RID: 33240
		private readonly string IMG_ARROW_LABEL;

		// Token: 0x040081D9 RID: 33241
		private readonly string IMG_BADGE_LABEL;

		// Token: 0x040081DA RID: 33242
		private readonly string IMG_CAN_UNLOCK_LABEL;

		// Token: 0x040081DB RID: 33243
		private readonly string BTN_LABEL;

		// Token: 0x040081DC RID: 33244
		private readonly string SELECTOR_SOLO_TRANSITION;

		// Token: 0x040081DD RID: 33245
		private readonly string E_FilterAndSortArea;

		// Token: 0x040081DE RID: 33246
		private readonly string E_ClearButton;

		// Token: 0x040081DF RID: 33247
		private readonly string E_FilterButton;

		// Token: 0x040081E0 RID: 33248
		private readonly string E_SortButton;

		// Token: 0x040081E1 RID: 33249
		private readonly string E_TextEmpty;

		// Token: 0x040081E2 RID: 33250
		public const string BGM_TUTORIAL = "BGM_TUTORIAL_01";

		// Token: 0x040081E3 RID: 33251
		public const string BGM_SOLO = "BGM_SOLO_GATE";

		// Token: 0x040081E4 RID: 33252
		private DefinitionSetting soloDefine;

		// Token: 0x040081E5 RID: 33253
		private DefinitionSetting soloTransDefine;

		// Token: 0x040081E6 RID: 33254
		private SoloFlyingCardSettings soloFlyingCardSettings;

		// Token: 0x040081E7 RID: 33255
		private InfinityScrollView mainScroll;

		// Token: 0x040081E8 RID: 33256
		private GameObject subScrollRoot;

		// Token: 0x040081E9 RID: 33257
		private InfinityScrollView subScroll;

		// Token: 0x040081EA RID: 33258
		private bool isWhileTutorial;

		// Token: 0x040081EB RID: 33259
		private int filterClusterGoThroughPriority;

		// Token: 0x040081EC RID: 33260
		private string loadedBgmLabel;

		// Token: 0x040081ED RID: 33261
		private bool isReady;

		// Token: 0x040081EE RID: 33262
		private bool isReadyTutorial;

		// Token: 0x040081EF RID: 33263
		private bool isDispHeader;

		// Token: 0x040081F0 RID: 33264
		private ValueTuple<bool, bool> isCalledDispUnlock;

		// Token: 0x040081F1 RID: 33265
		private ValueTuple<int, int> currentMainGate;

		// Token: 0x040081F2 RID: 33266
		private ValueTuple<int, int> currentSubGate;

		// Token: 0x040081F3 RID: 33267
		private PlayableDirector soloTransition;

		// Token: 0x040081F4 RID: 33268
		private SoloModeViewController.GateManager gateManager;

		// Token: 0x040081F5 RID: 33269
		private List<int> masterMainDataList;

		// Token: 0x040081F6 RID: 33270
		private List<int> mainDataList;

		// Token: 0x040081F7 RID: 33271
		private List<int> masterSubDataList;

		// Token: 0x040081F8 RID: 33272
		private List<int> subDataList;

		// Token: 0x040081F9 RID: 33273
		private OrbPlateWidget orbPlate;

		// Token: 0x040081FA RID: 33274
		private SoloFilterSortUtil.GateFilter currentGateFilter;

		// Token: 0x040081FB RID: 33275
		private SoloFilterSortUtil.GateSort currentGateSort;

		// Token: 0x02000904 RID: 2308
		internal class GateManager
		{
			// Token: 0x040081FC RID: 33276
			internal Dictionary<int, SoloModeViewController.Data> masterDataDic;
		}

		// Token: 0x02000905 RID: 2309
		internal class Data
		{
			// Token: 0x1700053B RID: 1339
			// (get) Token: 0x06004356 RID: 17238 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06004357 RID: 17239 RVA: 0x0000216D File Offset: 0x0000036D
			internal string StrName
			{
				get
				{
					return null;
				}
				private set
				{
				}
			}

			// Token: 0x1700053C RID: 1340
			// (get) Token: 0x06004358 RID: 17240 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06004359 RID: 17241 RVA: 0x0000216D File Offset: 0x0000036D
			internal bool IsComplete
			{
				get
				{
					return false;
				}
				private set
				{
				}
			}

			// Token: 0x1700053D RID: 1341
			// (get) Token: 0x0600435A RID: 17242 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x0600435B RID: 17243 RVA: 0x0000216D File Offset: 0x0000036D
			internal bool IsClear
			{
				get
				{
					return false;
				}
				private set
				{
				}
			}

			// Token: 0x1700053E RID: 1342
			// (get) Token: 0x0600435C RID: 17244 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x0600435D RID: 17245 RVA: 0x0000216D File Offset: 0x0000036D
			internal bool IsBadge
			{
				get
				{
					return false;
				}
				set
				{
				}
			}

			// Token: 0x1700053F RID: 1343
			// (get) Token: 0x0600435E RID: 17246 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x0600435F RID: 17247 RVA: 0x0000216D File Offset: 0x0000036D
			internal bool HaveCanUnlockChapter
			{
				get
				{
					return false;
				}
				set
				{
				}
			}

			// Token: 0x06004360 RID: 17248 RVA: 0x00002739 File Offset: 0x00000939
			internal Data(int gateID, string strName, string strOverview, string strUnlocks, bool isUnlocked, bool haveUnlockChapter, int priority, long openDate, long lastPlayDate, bool isActive, int parentID, SoloModeViewController.GateManager gateManager)
			{
			}

			// Token: 0x06004361 RID: 17249 RVA: 0x0000216D File Offset: 0x0000036D
			internal void AddChild(int child)
			{
			}

			// Token: 0x06004362 RID: 17250 RVA: 0x0000216D File Offset: 0x0000036D
			internal void CheckCompleteClearFlag()
			{
			}

			// Token: 0x06004363 RID: 17251 RVA: 0x0000216D File Offset: 0x0000036D
			internal void UpdateDateInSubGate()
			{
			}

			// Token: 0x040081FD RID: 33277
			internal int gateID;

			// Token: 0x040081FE RID: 33278
			private string gateName;

			// Token: 0x040081FF RID: 33279
			internal string strOverview;

			// Token: 0x04008200 RID: 33280
			internal string strUnlocks;

			// Token: 0x04008201 RID: 33281
			private bool isClear;

			// Token: 0x04008202 RID: 33282
			private bool isComplete;

			// Token: 0x04008203 RID: 33283
			private bool isBadge;

			// Token: 0x04008204 RID: 33284
			private bool haveCanUnlockChapter;

			// Token: 0x04008205 RID: 33285
			internal bool isUnlocked;

			// Token: 0x04008206 RID: 33286
			internal int priority;

			// Token: 0x04008207 RID: 33287
			internal long openDate;

			// Token: 0x04008208 RID: 33288
			internal long lastPlayDate;

			// Token: 0x04008209 RID: 33289
			internal bool isActive;

			// Token: 0x0400820A RID: 33290
			internal int parentID;

			// Token: 0x0400820B RID: 33291
			internal bool isSelected;

			// Token: 0x0400820C RID: 33292
			internal List<int> childs;

			// Token: 0x0400820D RID: 33293
			internal SoloModeViewController.GateManager gateManager;
		}
	}
}
