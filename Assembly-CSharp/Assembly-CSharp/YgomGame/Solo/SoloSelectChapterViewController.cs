using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YgomGame.Menu;
using YgomGame.Utility;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Solo
{
	// Token: 0x02000906 RID: 2310
	public class SoloSelectChapterViewController : BaseMenuViewController, IDynamicChangeDispHeaderSupported
	{
		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06004364 RID: 17252 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override int selectorPriorityAddRange
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06004365 RID: 17253 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06004366 RID: 17254 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool setProgressOnInitialize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06004367 RID: 17255 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06004368 RID: 17256 RVA: 0x0000216D File Offset: 0x0000036D
		public string SoloBGMLabel
		{
			get
			{
				return null;
			}
			private set
			{
			}
		}

		// Token: 0x06004369 RID: 17257 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x0600436A RID: 17258 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStack(ViewControllerManager vcm, ViewController vc, bool isEntry)
		{
		}

		// Token: 0x0600436B RID: 17259 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x0600436C RID: 17260 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x0600436D RID: 17261 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnFocusChanged(bool setfocus)
		{
		}

		// Token: 0x0600436E RID: 17262 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x0600436F RID: 17263 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnTransitionEnd(ViewController.TransitionType type)
		{
		}

		// Token: 0x06004370 RID: 17264 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitDefine()
		{
		}

		// Token: 0x06004371 RID: 17265 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnBack()
		{
			return false;
		}

		// Token: 0x06004372 RID: 17266 RVA: 0x0000216D File Offset: 0x0000036D
		private void CallAPISoloGateEntry(int gateID)
		{
		}

		// Token: 0x06004373 RID: 17267 RVA: 0x000029CC File Offset: 0x00000BCC
		public HeaderViewController.IsDispHeader IsDispContents()
		{
			return (HeaderViewController.IsDispHeader)0;
		}

		// Token: 0x06004374 RID: 17268 RVA: 0x000F47E0 File Offset: 0x000F29E0
		public Color FadeColor(ViewController.TransitionType type)
		{
			return default(Color);
		}

		// Token: 0x06004375 RID: 17269 RVA: 0x000029CC File Offset: 0x00000BCC
		public SystemProgress.ProgressType FadeType(ViewController.TransitionType type)
		{
			return SystemProgress.ProgressType.None;
		}

		// Token: 0x06004376 RID: 17270 RVA: 0x0000216D File Offset: 0x0000036D
		public static void RetryDuel(ViewControllerManager manager, ViewController swapTarget, int chapterId, bool isRental)
		{
		}

		// Token: 0x0400820E RID: 33294
		[SerializeField]
		private float chapterSpaceX;

		// Token: 0x0400820F RID: 33295
		[SerializeField]
		private float chapterSpaceY;

		// Token: 0x04008210 RID: 33296
		[SerializeField]
		private float chapterSpaceMobileX;

		// Token: 0x04008211 RID: 33297
		[SerializeField]
		private float chapterSpaceMobileY;

		// Token: 0x04008212 RID: 33298
		private readonly string TXT_TITLE_LABEL;

		// Token: 0x04008213 RID: 33299
		private readonly string OBJ_ORB_PLATE_LABEL;

		// Token: 0x04008214 RID: 33300
		private readonly string ROOT_GATE_LABEL;

		// Token: 0x04008215 RID: 33301
		private DefinitionSetting soloDefine;

		// Token: 0x04008216 RID: 33302
		private GameObject rootGate;

		// Token: 0x04008217 RID: 33303
		private SoloSelectChapterViewController.AccessDialogManager adManager;

		// Token: 0x04008218 RID: 33304
		private SoloSelectChapterViewController.ChapterMap chapterMap;

		// Token: 0x04008219 RID: 33305
		private OrbPlateWidget orbPlate;

		// Token: 0x0400821A RID: 33306
		private bool isWhileTutorial;

		// Token: 0x0400821B RID: 33307
		private string soloBGMLabel;

		// Token: 0x02000907 RID: 2311
		internal abstract class AccessDialog
		{
			// Token: 0x06004378 RID: 17272 RVA: 0x00002739 File Offset: 0x00000939
			protected AccessDialog(ElementObjectManager eom, ViewControllerManager manager, SoloSelectChapterViewController.AccessDialogManager adManager, int selectorPriority)
			{
			}

			// Token: 0x06004379 RID: 17273
			internal abstract void UpdateDisp(SoloSelectChapterViewController.Chapter data);

			// Token: 0x0600437A RID: 17274 RVA: 0x0000216D File Offset: 0x0000036D
			protected internal virtual void Open(SoloSelectChapterViewController.Chapter data)
			{
			}

			// Token: 0x0600437B RID: 17275
			internal abstract void Play(SoloSelectChapterViewController.Chapter data);

			// Token: 0x0600437C RID: 17276 RVA: 0x0000216D File Offset: 0x0000036D
			internal virtual void Close()
			{
			}

			// Token: 0x0600437D RID: 17277 RVA: 0x0000216D File Offset: 0x0000036D
			protected void CallApiSoloStart(SoloSelectChapterViewController.Chapter data)
			{
			}

			// Token: 0x0600437E RID: 17278 RVA: 0x0000216D File Offset: 0x0000036D
			protected void OnClickRewardButton(int itemID, string numText = "1")
			{
			}

			// Token: 0x0400821C RID: 33308
			protected readonly string BTN_PLAY_LABEL;

			// Token: 0x0400821D RID: 33309
			protected readonly string BTN_DECK_LABEL;

			// Token: 0x0400821E RID: 33310
			protected readonly string BTN_ENEMY_DECK_LABEL;

			// Token: 0x0400821F RID: 33311
			protected readonly string BTN_STORY_DECK_LABEL;

			// Token: 0x04008220 RID: 33312
			protected readonly string BTN_STORY_CARD_LABEL;

			// Token: 0x04008221 RID: 33313
			protected readonly string BTN_SKIP_LABEL;

			// Token: 0x04008222 RID: 33314
			protected readonly string IMG_CHAPTER_LABEL;

			// Token: 0x04008223 RID: 33315
			protected readonly string IMG_REWARD_LABEL;

			// Token: 0x04008224 RID: 33316
			protected readonly string IMG_REWARD_GET_LABEL;

			// Token: 0x04008225 RID: 33317
			protected readonly string IMG_DECK_LABEL;

			// Token: 0x04008226 RID: 33318
			protected readonly string IMG_DECK_EMPTY_LABEL;

			// Token: 0x04008227 RID: 33319
			protected readonly string IMG_DECK_DISABLED_LABEL;

			// Token: 0x04008228 RID: 33320
			protected readonly string TXT_CHAPTER_NAME_LABEL;

			// Token: 0x04008229 RID: 33321
			protected readonly string TXT_DECK_LABEL;

			// Token: 0x0400822A RID: 33322
			protected readonly string TXT_OVERVIEW_LABEL;

			// Token: 0x0400822B RID: 33323
			protected readonly string TXT_QUANTITY_LABEL;

			// Token: 0x0400822C RID: 33324
			protected readonly string TXT_CLEAR_LABEL;

			// Token: 0x0400822D RID: 33325
			protected readonly string TXT_COMPLETE_LABEL;

			// Token: 0x0400822E RID: 33326
			protected readonly string OBJ_REWARD_CLEAR_LABEL;

			// Token: 0x0400822F RID: 33327
			protected readonly string OBJ_REWARD_COMPLETE_LABEL;

			// Token: 0x04008230 RID: 33328
			protected readonly string BTN_LABEL;

			// Token: 0x04008231 RID: 33329
			internal ElementObjectManager eom;

			// Token: 0x04008232 RID: 33330
			protected readonly ViewControllerManager manager;

			// Token: 0x04008233 RID: 33331
			protected readonly SoloSelectChapterViewController.AccessDialogManager adManager;

			// Token: 0x04008234 RID: 33332
			protected readonly int selectorPriority;

			// Token: 0x04008235 RID: 33333
			protected readonly DefinitionSetting soloDefine;
		}

		// Token: 0x02000908 RID: 2312
		internal class ScenarioDialog : SoloSelectChapterViewController.AccessDialog
		{
			// Token: 0x0600437F RID: 17279 RVA: 0x000F47F6 File Offset: 0x000F29F6
			public ScenarioDialog(ElementObjectManager eom, ViewControllerManager manager, SoloSelectChapterViewController.AccessDialogManager adManager, int selectorPriority)
				: base(null, null, null, 0)
			{
			}

			// Token: 0x06004380 RID: 17280 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void UpdateDisp(SoloSelectChapterViewController.Chapter data)
			{
			}

			// Token: 0x06004381 RID: 17281 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void Play(SoloSelectChapterViewController.Chapter data)
			{
			}
		}

		// Token: 0x02000909 RID: 2313
		internal class DuelDialog : SoloSelectChapterViewController.AccessDialog
		{
			// Token: 0x06004382 RID: 17282 RVA: 0x000F47F6 File Offset: 0x000F29F6
			public DuelDialog(ElementObjectManager eom, ViewControllerManager manager, SoloSelectChapterViewController.AccessDialogManager adManager, int selectorPriority)
				: base(null, null, null, 0)
			{
			}

			// Token: 0x06004383 RID: 17283 RVA: 0x0000216D File Offset: 0x0000036D
			protected internal override void Open(SoloSelectChapterViewController.Chapter data)
			{
			}

			// Token: 0x06004384 RID: 17284 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void UpdateDisp(SoloSelectChapterViewController.Chapter data)
			{
			}

			// Token: 0x06004385 RID: 17285 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void Play(SoloSelectChapterViewController.Chapter data)
			{
			}

			// Token: 0x06004386 RID: 17286 RVA: 0x0000216D File Offset: 0x0000036D
			protected void CallApiSoloSetUseDeckType(SoloSelectChapterViewController.Chapter data, SoloModeUtil.DeckType deckType, UnityAction onSuccess = null)
			{
			}

			// Token: 0x06004387 RID: 17287 RVA: 0x0000216D File Offset: 0x0000036D
			internal virtual void UpdateDeck(SoloSelectChapterViewController.Chapter data, SoloModeUtil.DeckType deckType = SoloModeUtil.DeckType.POSSESSION)
			{
			}

			// Token: 0x04008236 RID: 33334
			protected readonly string TAB_GROUP_LABEL;

			// Token: 0x04008237 RID: 33335
			protected readonly string TAB_RENTAL_LABEL;

			// Token: 0x04008238 RID: 33336
			protected readonly string TAB_MYDECK_LABEL;

			// Token: 0x04008239 RID: 33337
			protected readonly string ROOT_RENTAL_LABEL;

			// Token: 0x0400823A RID: 33338
			protected readonly string ROOT_MYDECK_LABEL;

			// Token: 0x0400823B RID: 33339
			protected readonly string ROOT_LEVEL_LABEL;

			// Token: 0x0400823C RID: 33340
			protected DirectionalToggleGroupWidget toggleGroup;

			// Token: 0x0400823D RID: 33341
			protected ElementObjectManager rootRentalEom;

			// Token: 0x0400823E RID: 33342
			protected ElementObjectManager rootMydeckEom;

			// Token: 0x0400823F RID: 33343
			protected ElementObjectManager tabGroupEom;

			// Token: 0x04008240 RID: 33344
			protected ElementObjectManager tabRentalEom;

			// Token: 0x04008241 RID: 33345
			protected ElementObjectManager tabMydeckEom;
		}

		// Token: 0x0200090A RID: 2314
		internal class TutorialDialog : SoloSelectChapterViewController.AccessDialog
		{
			// Token: 0x06004388 RID: 17288 RVA: 0x000F47F6 File Offset: 0x000F29F6
			public TutorialDialog(ElementObjectManager eom, ViewControllerManager manager, SoloSelectChapterViewController.AccessDialogManager adManager, int selectorPriority)
				: base(null, null, null, 0)
			{
			}

			// Token: 0x06004389 RID: 17289 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void UpdateDisp(SoloSelectChapterViewController.Chapter data)
			{
			}

			// Token: 0x0600438A RID: 17290 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void Play(SoloSelectChapterViewController.Chapter data)
			{
			}

			// Token: 0x0600438B RID: 17291 RVA: 0x0000216D File Offset: 0x0000036D
			internal void CallApiSoloSkip(SoloSelectChapterViewController.Chapter data)
			{
			}

			// Token: 0x0600438C RID: 17292 RVA: 0x0000216D File Offset: 0x0000036D
			protected void CallApiSoloSetUseDeckType(SoloSelectChapterViewController.Chapter data, SoloModeUtil.DeckType deckType, UnityAction onSuccess = null)
			{
			}

			// Token: 0x04008242 RID: 33346
			private bool isWhileTutorial;

			// Token: 0x04008243 RID: 33347
			protected readonly string ROOT_LEVEL_LABEL;
		}

		// Token: 0x0200090B RID: 2315
		internal class RewardDialog : SoloSelectChapterViewController.AccessDialog
		{
			// Token: 0x0600438D RID: 17293 RVA: 0x000F47F6 File Offset: 0x000F29F6
			public RewardDialog(ElementObjectManager eom, ViewControllerManager manager, SoloSelectChapterViewController.AccessDialogManager adManager, int selectorPriority)
				: base(null, null, null, 0)
			{
			}

			// Token: 0x0600438E RID: 17294 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void UpdateDisp(SoloSelectChapterViewController.Chapter data)
			{
			}

			// Token: 0x0600438F RID: 17295 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void Play(SoloSelectChapterViewController.Chapter data)
			{
			}

			// Token: 0x04008244 RID: 33348
			private readonly string TXT_PLAY_LABEL;
		}

		// Token: 0x0200090C RID: 2316
		internal class LockDialog : SoloSelectChapterViewController.AccessDialog
		{
			// Token: 0x06004390 RID: 17296 RVA: 0x000F47F6 File Offset: 0x000F29F6
			public LockDialog(ElementObjectManager eom, ViewControllerManager manager, SoloSelectChapterViewController.AccessDialogManager adManager, int selectorPriority)
				: base(null, null, null, 0)
			{
			}

			// Token: 0x06004391 RID: 17297 RVA: 0x0000216D File Offset: 0x0000036D
			protected internal override void Open(SoloSelectChapterViewController.Chapter data)
			{
			}

			// Token: 0x06004392 RID: 17298 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void UpdateDisp(SoloSelectChapterViewController.Chapter data)
			{
			}

			// Token: 0x06004393 RID: 17299 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void Play(SoloSelectChapterViewController.Chapter data)
			{
			}

			// Token: 0x04008245 RID: 33349
			private readonly string ROOT_UNLOCK_ITEM_LABEL;

			// Token: 0x04008246 RID: 33350
			private readonly string ROOT_UNLOCK_HASITEM_LABEL;

			// Token: 0x04008247 RID: 33351
			private readonly string TXT_PLAY_LABEL;

			// Token: 0x04008248 RID: 33352
			private readonly string TXT_COST_LABEL;

			// Token: 0x04008249 RID: 33353
			private readonly string TXT_HAVE_LABEL;

			// Token: 0x0400824A RID: 33354
			private readonly string TXT_CATEGORY_LABEL;

			// Token: 0x0400824B RID: 33355
			private readonly string TXT_NAME_LABEL;

			// Token: 0x0400824C RID: 33356
			private readonly string OBJ_TEXTLABEL_LABEL;

			// Token: 0x0400824D RID: 33357
			private readonly string OBJ_NOT_ENOUGH_LABEL;

			// Token: 0x0400824E RID: 33358
			private readonly string IMG_LABEL;

			// Token: 0x0400824F RID: 33359
			private readonly string IMG_LOCK_LABEL;

			// Token: 0x04008250 RID: 33360
			private readonly string IMG_UNLOCK_LABEL;

			// Token: 0x04008251 RID: 33361
			private List<GameObject> lockItems;

			// Token: 0x04008252 RID: 33362
			private List<GameObject> lockHasItems;
		}

		// Token: 0x0200090D RID: 2317
		internal class AccessDialogManager
		{
			// Token: 0x06004394 RID: 17300 RVA: 0x00002739 File Offset: 0x00000939
			internal AccessDialogManager(ElementObjectManager eom, int selectorPriority, ViewControllerManager manager, SoloSelectChapterViewController soloVC)
			{
			}

			// Token: 0x06004395 RID: 17301 RVA: 0x0000216D File Offset: 0x0000036D
			private void DispCanvas(bool isDisp)
			{
			}

			// Token: 0x06004396 RID: 17302 RVA: 0x0000216D File Offset: 0x0000036D
			internal void UpdateAccessDialog(SoloSelectChapterViewController.Chapter chapter = null)
			{
			}

			// Token: 0x06004397 RID: 17303 RVA: 0x0000216D File Offset: 0x0000036D
			internal void OpenAccessDialog(SoloSelectChapterViewController.Chapter chapter = null, List<SoloSelectChapterViewController.Chapter> childs = null)
			{
			}

			// Token: 0x06004398 RID: 17304 RVA: 0x0000216D File Offset: 0x0000036D
			internal void CloseAccessDialog()
			{
			}

			// Token: 0x06004399 RID: 17305 RVA: 0x0000216D File Offset: 0x0000036D
			internal void RefleshDialog()
			{
			}

			// Token: 0x0600439A RID: 17306 RVA: 0x0000216D File Offset: 0x0000036D
			internal void InvokeRefleshCallback()
			{
			}

			// Token: 0x0600439B RID: 17307 RVA: 0x0000216D File Offset: 0x0000036D
			internal void SetOpenedCallback(UnityAction<SoloSelectChapterViewController.Chapter> callback)
			{
			}

			// Token: 0x0600439C RID: 17308 RVA: 0x0000216D File Offset: 0x0000036D
			internal void SetClosedCallback(UnityAction<SoloSelectChapterViewController.Chapter> callback)
			{
			}

			// Token: 0x0600439D RID: 17309 RVA: 0x0000216D File Offset: 0x0000036D
			internal void SetRefleshCallback(UnityAction callback)
			{
			}

			// Token: 0x0600439E RID: 17310 RVA: 0x0000216D File Offset: 0x0000036D
			internal void StartChapter()
			{
			}

			// Token: 0x0600439F RID: 17311 RVA: 0x0000216D File Offset: 0x0000036D
			internal void SkipChapter()
			{
			}

			// Token: 0x060043A0 RID: 17312 RVA: 0x000029CC File Offset: 0x00000BCC
			internal bool isOpen()
			{
				return false;
			}

			// Token: 0x060043A1 RID: 17313 RVA: 0x0000216A File Offset: 0x0000036A
			private SoloSelectChapterViewController.AccessDialog GetAccessDialog(SoloModeUtil.DialogType type)
			{
				return null;
			}

			// Token: 0x060043A2 RID: 17314 RVA: 0x0000216A File Offset: 0x0000036A
			internal IEnumerator OpenRewardDialog(Action onComplete = null)
			{
				return null;
			}

			// Token: 0x04008253 RID: 33363
			private readonly string BTN_BACK_LABEL;

			// Token: 0x04008254 RID: 33364
			private readonly string OBJ_ACCESS_LABEL;

			// Token: 0x04008255 RID: 33365
			private readonly string OBJ_DIALOG_SCENARIO_LABEL;

			// Token: 0x04008256 RID: 33366
			private readonly string OBJ_DIALOG_DUEL_LABEL;

			// Token: 0x04008257 RID: 33367
			private readonly string OBJ_DIALOG_REWARD_LABEL;

			// Token: 0x04008258 RID: 33368
			private readonly string OBJ_DIALOG_LOCK_LABEL;

			// Token: 0x04008259 RID: 33369
			private readonly string OBJ_DIALOG_TUTORIAL_LABEL;

			// Token: 0x0400825A RID: 33370
			private readonly string OBJ_CLEAR_LABEL;

			// Token: 0x0400825B RID: 33371
			public SoloSelectChapterViewController soloVC;

			// Token: 0x0400825C RID: 33372
			private GameObject rootAccessDialog;

			// Token: 0x0400825D RID: 33373
			private ElementObjectManager parentViewEom;

			// Token: 0x0400825E RID: 33374
			private SoloSelectChapterViewController.ScenarioDialog scenarioDialog;

			// Token: 0x0400825F RID: 33375
			private SoloSelectChapterViewController.DuelDialog duelDialog;

			// Token: 0x04008260 RID: 33376
			private SoloSelectChapterViewController.LockDialog lockDialog;

			// Token: 0x04008261 RID: 33377
			private SoloSelectChapterViewController.RewardDialog rewardDialog;

			// Token: 0x04008262 RID: 33378
			private SoloSelectChapterViewController.TutorialDialog tutorialDialog;

			// Token: 0x04008263 RID: 33379
			private SoloSelectChapterViewController.Chapter targetChapter;

			// Token: 0x04008264 RID: 33380
			private List<SoloSelectChapterViewController.Chapter> childChapters;

			// Token: 0x04008265 RID: 33381
			private UnityAction<SoloSelectChapterViewController.Chapter> openedCallback;

			// Token: 0x04008266 RID: 33382
			private UnityAction<SoloSelectChapterViewController.Chapter> closedCallback;

			// Token: 0x04008267 RID: 33383
			private UnityAction refleshCallback;

			// Token: 0x04008268 RID: 33384
			private int selectorPriority;

			// Token: 0x04008269 RID: 33385
			private ValueTuple<int, SoloModeUtil.ChapterStatus> beforeIdStatus;

			// Token: 0x0400826A RID: 33386
			internal bool isPlayingPerformance;
		}

		// Token: 0x0200090E RID: 2318
		internal class ChapterMap
		{
			// Token: 0x060043A3 RID: 17315 RVA: 0x00002739 File Offset: 0x00000939
			internal ChapterMap(SoloSelectChapterViewController soloVC, ElementObjectManager parentViewEom, SoloSelectChapterViewController.AccessDialogManager adManager, int gateID, float padingX = 300f, float padingY = 300f)
			{
			}

			// Token: 0x060043A4 RID: 17316 RVA: 0x0000216D File Offset: 0x0000036D
			internal void Create()
			{
			}

			// Token: 0x060043A5 RID: 17317 RVA: 0x0000216D File Offset: 0x0000036D
			internal void SetChaptersData()
			{
			}

			// Token: 0x060043A6 RID: 17318 RVA: 0x0000216D File Offset: 0x0000036D
			private void InitChapter(int chapterID, Dictionary<string, object> chapterData)
			{
			}

			// Token: 0x060043A7 RID: 17319 RVA: 0x0000216D File Offset: 0x0000036D
			internal void OnClickChapter(SelectionButton sb, SoloSelectChapterViewController.Chapter data)
			{
			}

			// Token: 0x060043A8 RID: 17320 RVA: 0x0000216D File Offset: 0x0000036D
			internal void UpdateMap()
			{
			}

			// Token: 0x060043A9 RID: 17321 RVA: 0x0000216D File Offset: 0x0000036D
			internal void SelectedChapter(int x)
			{
			}

			// Token: 0x060043AA RID: 17322 RVA: 0x0000216D File Offset: 0x0000036D
			internal void ZoomChapter(SelectionItem si)
			{
			}

			// Token: 0x060043AB RID: 17323 RVA: 0x0000216D File Offset: 0x0000036D
			internal void ResetZoom()
			{
			}

			// Token: 0x060043AC RID: 17324 RVA: 0x0000216D File Offset: 0x0000036D
			private void SetTransition(SoloSelectChapterViewController.Chapter chapter, PadInputDirection direction, SelectionButton settingBtn)
			{
			}

			// Token: 0x060043AD RID: 17325 RVA: 0x0000216A File Offset: 0x0000036A
			private SoloSelectChapterViewController.NodeMapCreater CreateNodeMap()
			{
				return null;
			}

			// Token: 0x060043AE RID: 17326 RVA: 0x000029CC File Offset: 0x00000BCC
			private int TransYtoMapY(int y)
			{
				return 0;
			}

			// Token: 0x0400826B RID: 33387
			protected readonly string BTN_LABEL;

			// Token: 0x0400826C RID: 33388
			protected readonly string IMG_ACCESS_LABEL;

			// Token: 0x0400826D RID: 33389
			protected readonly string SCROLL_LABEL;

			// Token: 0x0400826E RID: 33390
			protected readonly string OBJ_CHAPTER_MAP_LABEL;

			// Token: 0x0400826F RID: 33391
			protected readonly string TMP_GATE_LABEL;

			// Token: 0x04008270 RID: 33392
			protected readonly string ROOT_GATE_LABEL;

			// Token: 0x04008271 RID: 33393
			protected readonly string TXT_NAME_LABEL;

			// Token: 0x04008272 RID: 33394
			private readonly string IMG_ICON_LABEL;

			// Token: 0x04008273 RID: 33395
			private readonly string IMG_EDGE_LABEL;

			// Token: 0x04008274 RID: 33396
			private readonly string ROOT_EDGES_LABEL;

			// Token: 0x04008275 RID: 33397
			protected int currentChapterID;

			// Token: 0x04008276 RID: 33398
			protected ElementObjectManager parentViewEom;

			// Token: 0x04008277 RID: 33399
			protected Dictionary<int, SoloSelectChapterViewController.Chapter> chapterDataDic;

			// Token: 0x04008278 RID: 33400
			protected SoloSelectChapterViewController.AccessDialogManager adManager;

			// Token: 0x04008279 RID: 33401
			protected int gateID;

			// Token: 0x0400827A RID: 33402
			protected GameObject gateGO;

			// Token: 0x0400827B RID: 33403
			private readonly float PADING_X;

			// Token: 0x0400827C RID: 33404
			private readonly float PADING_Y;

			// Token: 0x0400827D RID: 33405
			protected Dictionary<SoloSelectChapterViewController.Chapter, SoloSelectChapterViewController.Node> chapterNodeMap;

			// Token: 0x0400827E RID: 33406
			protected SoloSelectChapterViewController.NodeMapCreater nodeMapCreater;

			// Token: 0x0400827F RID: 33407
			private float defaultPositionX;

			// Token: 0x04008280 RID: 33408
			private GameObject playerIconGo;

			// Token: 0x04008281 RID: 33409
			private SoloSelectChapterViewController soloVC;
		}

		// Token: 0x0200090F RID: 2319
		internal abstract class Chapter
		{
			// Token: 0x060043AF RID: 17327 RVA: 0x00002739 File Offset: 0x00000939
			protected Chapter()
			{
			}

			// Token: 0x060043B0 RID: 17328 RVA: 0x00002739 File Offset: 0x00000939
			public Chapter(int chapterID, SoloSelectChapterViewController soloVC)
			{
			}

			// Token: 0x060043B1 RID: 17329
			internal abstract string GetElementLabel();

			// Token: 0x060043B2 RID: 17330
			internal abstract void Update(SoloSelectChapterViewController.Chapter parent);

			// Token: 0x060043B3 RID: 17331 RVA: 0x000029CC File Offset: 0x00000BCC
			internal virtual bool IsCleared()
			{
				return false;
			}

			// Token: 0x060043B4 RID: 17332 RVA: 0x000029CC File Offset: 0x00000BCC
			internal virtual bool IsCompleted()
			{
				return false;
			}

			// Token: 0x060043B5 RID: 17333 RVA: 0x0000216D File Offset: 0x0000036D
			internal virtual void SetData(Dictionary<string, object> chapterData)
			{
			}

			// Token: 0x060043B6 RID: 17334 RVA: 0x0000216D File Offset: 0x0000036D
			internal void SetIcon(GameObject gameObject)
			{
			}

			// Token: 0x04008282 RID: 33410
			protected readonly string TXT_CLEAR_LABEL;

			// Token: 0x04008283 RID: 33411
			protected readonly string TXT_COMPLETE_LABEL;

			// Token: 0x04008284 RID: 33412
			protected readonly string IMG_LOCK_LABEL;

			// Token: 0x04008285 RID: 33413
			protected readonly string IMG_UNLOCK_LABEL;

			// Token: 0x04008286 RID: 33414
			protected readonly string IMG_UNOPEN_LABEL;

			// Token: 0x04008287 RID: 33415
			protected readonly string IMG_ICON_LABEL;

			// Token: 0x04008288 RID: 33416
			internal int id;

			// Token: 0x04008289 RID: 33417
			internal string strChapter;

			// Token: 0x0400828A RID: 33418
			internal string strExplanation;

			// Token: 0x0400828B RID: 33419
			internal int parentID;

			// Token: 0x0400828C RID: 33420
			internal SoloModeUtil.ChapterStatus status;

			// Token: 0x0400828D RID: 33421
			internal SoloModeUtil.DialogType dType;

			// Token: 0x0400828E RID: 33422
			internal GameObject go;

			// Token: 0x0400828F RID: 33423
			internal int myDeckSetID;

			// Token: 0x04008290 RID: 33424
			internal int setID;

			// Token: 0x04008291 RID: 33425
			internal int unlockID;

			// Token: 0x04008292 RID: 33426
			internal int npcID;

			// Token: 0x04008293 RID: 33427
			internal string scenarioName;
		}

		// Token: 0x02000910 RID: 2320
		internal class ChapterDuel : SoloSelectChapterViewController.Chapter, SoloSelectChapterViewController.IChapterLevel
		{
			// Token: 0x060043B7 RID: 17335 RVA: 0x0000216A File Offset: 0x0000036A
			internal override string GetElementLabel()
			{
				return null;
			}

			// Token: 0x060043B8 RID: 17336 RVA: 0x000F4802 File Offset: 0x000F2A02
			public ChapterDuel(int chapterID, SoloSelectChapterViewController soloVC)
			{
			}

			// Token: 0x060043B9 RID: 17337 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void Update(SoloSelectChapterViewController.Chapter parent)
			{
			}

			// Token: 0x060043BA RID: 17338 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void SetData(Dictionary<string, object> chapterData)
			{
			}

			// Token: 0x060043BB RID: 17339 RVA: 0x000029CC File Offset: 0x00000BCC
			public int GetChapterLevel()
			{
				return 0;
			}

			// Token: 0x04008294 RID: 33428
			private int level;
		}

		// Token: 0x02000911 RID: 2321
		internal class ChapterPractice : SoloSelectChapterViewController.ChapterDuel
		{
			// Token: 0x060043BC RID: 17340 RVA: 0x0000216A File Offset: 0x0000036A
			internal override string GetElementLabel()
			{
				return null;
			}

			// Token: 0x060043BD RID: 17341 RVA: 0x000F480A File Offset: 0x000F2A0A
			public ChapterPractice(int chapterID, SoloSelectChapterViewController soloVC)
				: base(0, null)
			{
			}
		}

		// Token: 0x02000912 RID: 2322
		internal class ChapterScenario : SoloSelectChapterViewController.Chapter
		{
			// Token: 0x060043BE RID: 17342 RVA: 0x0000216A File Offset: 0x0000036A
			internal override string GetElementLabel()
			{
				return null;
			}

			// Token: 0x060043BF RID: 17343 RVA: 0x000F4802 File Offset: 0x000F2A02
			public ChapterScenario(int chapterID, SoloSelectChapterViewController soloVC)
			{
			}

			// Token: 0x060043C0 RID: 17344 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void Update(SoloSelectChapterViewController.Chapter parent)
			{
			}

			// Token: 0x060043C1 RID: 17345 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void SetData(Dictionary<string, object> chapterData)
			{
			}
		}

		// Token: 0x02000913 RID: 2323
		internal class ChapterReward : SoloSelectChapterViewController.Chapter
		{
			// Token: 0x060043C2 RID: 17346 RVA: 0x0000216A File Offset: 0x0000036A
			internal override string GetElementLabel()
			{
				return null;
			}

			// Token: 0x060043C3 RID: 17347 RVA: 0x000F4802 File Offset: 0x000F2A02
			public ChapterReward(int chapterID, SoloSelectChapterViewController soloVC)
			{
			}

			// Token: 0x060043C4 RID: 17348 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void Update(SoloSelectChapterViewController.Chapter parent)
			{
			}

			// Token: 0x060043C5 RID: 17349 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void SetData(Dictionary<string, object> chapterData)
			{
			}
		}

		// Token: 0x02000914 RID: 2324
		internal class ChapterLock : SoloSelectChapterViewController.Chapter
		{
			// Token: 0x060043C6 RID: 17350 RVA: 0x0000216A File Offset: 0x0000036A
			internal override string GetElementLabel()
			{
				return null;
			}

			// Token: 0x060043C7 RID: 17351 RVA: 0x000F4802 File Offset: 0x000F2A02
			public ChapterLock(int chapterID)
			{
			}

			// Token: 0x060043C8 RID: 17352 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void Update(SoloSelectChapterViewController.Chapter parent)
			{
			}

			// Token: 0x060043C9 RID: 17353 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void SetData(Dictionary<string, object> chapterData)
			{
			}
		}

		// Token: 0x02000915 RID: 2325
		internal class ChapterGoal : SoloSelectChapterViewController.Chapter, SoloSelectChapterViewController.IChapterLevel
		{
			// Token: 0x060043CA RID: 17354 RVA: 0x0000216A File Offset: 0x0000036A
			internal override string GetElementLabel()
			{
				return null;
			}

			// Token: 0x060043CB RID: 17355 RVA: 0x000F4802 File Offset: 0x000F2A02
			public ChapterGoal(int chapterID, SoloSelectChapterViewController soloVC)
			{
			}

			// Token: 0x060043CC RID: 17356 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void Update(SoloSelectChapterViewController.Chapter parent)
			{
			}

			// Token: 0x060043CD RID: 17357 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void SetData(Dictionary<string, object> chapterData)
			{
			}

			// Token: 0x060043CE RID: 17358 RVA: 0x000029CC File Offset: 0x00000BCC
			public int GetChapterLevel()
			{
				return 0;
			}

			// Token: 0x04008295 RID: 33429
			private int level;
		}

		// Token: 0x02000916 RID: 2326
		internal interface IChapterLevel
		{
			// Token: 0x060043CF RID: 17359
			int GetChapterLevel();
		}

		// Token: 0x02000917 RID: 2327
		internal class NodeMapCreater
		{
			// Token: 0x060043D0 RID: 17360 RVA: 0x0000216D File Offset: 0x0000036D
			internal void SortPositionPostOrder(SoloSelectChapterViewController.Node node, int x = 0, bool isFirstChild = false)
			{
			}

			// Token: 0x04008296 RID: 33430
			private Dictionary<int, int> map;

			// Token: 0x04008297 RID: 33431
			internal int maxX;

			// Token: 0x04008298 RID: 33432
			internal int maxY;
		}

		// Token: 0x02000918 RID: 2328
		internal class Node
		{
			// Token: 0x060043D2 RID: 17362 RVA: 0x00002739 File Offset: 0x00000939
			public Node(int id)
			{
			}

			// Token: 0x060043D3 RID: 17363 RVA: 0x0000216A File Offset: 0x0000036A
			internal SoloSelectChapterViewController.Node GetParent()
			{
				return null;
			}

			// Token: 0x060043D4 RID: 17364 RVA: 0x0000216D File Offset: 0x0000036D
			internal void SetParent(SoloSelectChapterViewController.Node parent)
			{
			}

			// Token: 0x060043D5 RID: 17365 RVA: 0x0000216D File Offset: 0x0000036D
			internal void SetChild(SoloSelectChapterViewController.Node child)
			{
			}

			// Token: 0x060043D6 RID: 17366 RVA: 0x0000216D File Offset: 0x0000036D
			internal void SetIsRelatedGoal()
			{
			}

			// Token: 0x060043D7 RID: 17367 RVA: 0x0000216D File Offset: 0x0000036D
			internal void SlideY(int y)
			{
			}

			// Token: 0x04008299 RID: 33433
			private SoloSelectChapterViewController.Node parent;

			// Token: 0x0400829A RID: 33434
			public int id;

			// Token: 0x0400829B RID: 33435
			public List<SoloSelectChapterViewController.Node> childs;

			// Token: 0x0400829C RID: 33436
			public int x;

			// Token: 0x0400829D RID: 33437
			public int y;

			// Token: 0x0400829E RID: 33438
			public bool isRelatedGoal;
		}
	}
}
