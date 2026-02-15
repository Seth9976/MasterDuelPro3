using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YgomGame.Menu;
using YgomGame.Menu.Common;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.Colosseum
{
	// Token: 0x02001039 RID: 4153
	public class ColosseumInfoViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x17000FDA RID: 4058
		// (get) Token: 0x06007CA0 RID: 31904 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000FDB RID: 4059
		// (get) Token: 0x06007CA1 RID: 31905 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06007CA2 RID: 31906 RVA: 0x0000216D File Offset: 0x0000036D
		public string ColosseumBGMLabel
		{
			get
			{
				return null;
			}
			private set
			{
			}
		}

		// Token: 0x06007CA3 RID: 31907 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06007CA4 RID: 31908 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStack(ViewControllerManager vcm, ViewController vc, bool isEntry)
		{
		}

		// Token: 0x06007CA5 RID: 31909 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06007CA6 RID: 31910 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06007CA7 RID: 31911 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x06007CA8 RID: 31912 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnFocusChanged(bool setfocus)
		{
		}

		// Token: 0x06007CA9 RID: 31913 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnTransitionStart(ViewController.TransitionType type)
		{
		}

		// Token: 0x06007CAA RID: 31914 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnTransitionEnd(ViewController.TransitionType type)
		{
		}

		// Token: 0x0400B450 RID: 46160
		protected readonly string ROOT_MENU_LABEL;

		// Token: 0x0400B451 RID: 46161
		protected readonly string VIEW_STANDARD_LABEL;

		// Token: 0x0400B452 RID: 46162
		protected readonly string VIEW_TOURNAMENT_LABEL;

		// Token: 0x0400B453 RID: 46163
		protected readonly string VIEW_EXHIBITION_LABEL;

		// Token: 0x0400B454 RID: 46164
		protected readonly string VIEW_FREE_LABEL;

		// Token: 0x0400B455 RID: 46165
		protected readonly string VIEW_DUELISTCUP_LABEL;

		// Token: 0x0400B456 RID: 46166
		protected readonly string VIEW_RANKEVENT_LABEL;

		// Token: 0x0400B457 RID: 46167
		protected readonly string VIEW_LABEL;

		// Token: 0x0400B458 RID: 46168
		protected readonly string BTN_DECIDE_LABEL;

		// Token: 0x0400B459 RID: 46169
		protected ColosseumUtil.PlayMode mode;

		// Token: 0x0400B45A RID: 46170
		protected ColosseumInfoViewController.ModeBehaviour modeBehaviour;

		// Token: 0x0400B45B RID: 46171
		[SerializeField]
		protected ElementObjectManager DeckOverviewPrefab;

		// Token: 0x0400B45C RID: 46172
		private string colosseumBGMLabel;

		// Token: 0x0200103A RID: 4154
		protected class DuelTrialBehaviour : ColosseumInfoViewController.ModeBehaviour
		{
			// Token: 0x06007CAC RID: 31916 RVA: 0x000F67A7 File Offset: 0x000F49A7
			public DuelTrialBehaviour(ViewControllerManager manager, ColosseumInfoViewController vc, ElementObjectManager parentEOM, ElementObjectManager viewEOM, ElementObjectManager menuEOM, ElementObjectManager overviewEOM, int duel_trial_id)
				: base(null, null, null, null, null, null)
			{
			}

			// Token: 0x06007CAD RID: 31917 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void CallAPI()
			{
			}

			// Token: 0x06007CAE RID: 31918 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void SetDuelButton(ElementObjectManager menuEOM)
			{
			}

			// Token: 0x06007CAF RID: 31919 RVA: 0x000029CC File Offset: 0x00000BCC
			protected override bool IsDispPerformance()
			{
				return false;
			}

			// Token: 0x06007CB0 RID: 31920 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void SetMenu()
			{
			}

			// Token: 0x06007CB1 RID: 31921 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void InitDisp()
			{
			}

			// Token: 0x06007CB2 RID: 31922 RVA: 0x0000216D File Offset: 0x0000036D
			private void SetShortcutLRReward(bool isSet)
			{
			}

			// Token: 0x06007CB3 RID: 31923 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void UpdateDisp()
			{
			}

			// Token: 0x06007CB4 RID: 31924 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void BackAction(ViewControllerManager vcm, ViewController vc, bool isEntry)
			{
			}

			// Token: 0x06007CB5 RID: 31925 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void OnTransitionEnd(ViewController.TransitionType type)
			{
			}

			// Token: 0x06007CB6 RID: 31926 RVA: 0x0000216D File Offset: 0x0000036D
			private void MovePageNextReward()
			{
			}

			// Token: 0x06007CB7 RID: 31927 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void OnClickDuel()
			{
			}

			// Token: 0x06007CB8 RID: 31928 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void CallAPIGetDeckList(int id, Action onSuccess)
			{
			}

			// Token: 0x06007CB9 RID: 31929 RVA: 0x0000216A File Offset: 0x0000036A
			internal override Dictionary<string, object> GetDeckArgs(int identifer, bool isScratch)
			{
				return null;
			}

			// Token: 0x06007CBA RID: 31930 RVA: 0x0000216D File Offset: 0x0000036D
			private void UpdateReward(int currentWin)
			{
			}

			// Token: 0x06007CBB RID: 31931 RVA: 0x0000216D File Offset: 0x0000036D
			private void OnUpdateEntity(GameObject go, int index)
			{
			}

			// Token: 0x06007CBC RID: 31932 RVA: 0x0000216A File Offset: 0x0000036A
			private IReadOnlyList<ValueTuple<SelectionItem, int, int>> OnCustomCollectionSelectionItems(GameObject entity)
			{
				return null;
			}

			// Token: 0x06007CBD RID: 31933 RVA: 0x000029CC File Offset: 0x00000BCC
			private bool OnCustomInnerTransition(SelectionItem selectionItem, PadInputDirection direction)
			{
				return false;
			}

			// Token: 0x06007CBE RID: 31934 RVA: 0x0000216D File Offset: 0x0000036D
			private void OnInputAnalogDirection(SelectorManager.AnalogType analogType, PadInputDirection dir)
			{
			}

			// Token: 0x06007CBF RID: 31935 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void SetDeck(int tid)
			{
			}

			// Token: 0x06007CC0 RID: 31936 RVA: 0x0000216A File Offset: 0x0000036A
			private List<ColosseumDeckWidget.ButtonInfo> SetRentalDeck(Dictionary<string, object> tInfoDic)
			{
				return null;
			}

			// Token: 0x06007CC1 RID: 31937 RVA: 0x0000216A File Offset: 0x0000036A
			private List<ColosseumDeckWidget.ButtonInfo> SetMyDeck(Dictionary<string, object> MTdic, Dictionary<string, object> tInfoDic)
			{
				return null;
			}

			// Token: 0x06007CC2 RID: 31938 RVA: 0x0000216D File Offset: 0x0000036D
			private void DispReward()
			{
			}

			// Token: 0x06007CC3 RID: 31939 RVA: 0x0000216D File Offset: 0x0000036D
			private void CheckPack()
			{
			}

			// Token: 0x06007CC4 RID: 31940 RVA: 0x0000216D File Offset: 0x0000036D
			private void CallAPIOpenCampaignPack(int item_id, int num, Action onSuccess = null)
			{
			}

			// Token: 0x0400B45D RID: 46173
			private readonly string BTN_PREV;

			// Token: 0x0400B45E RID: 46174
			private readonly string BTN_NEXT;

			// Token: 0x0400B45F RID: 46175
			private readonly string REWARD_ROOT;

			// Token: 0x0400B460 RID: 46176
			private readonly string REWARD_NONE;

			// Token: 0x0400B461 RID: 46177
			private readonly string REWARD_NORMAL;

			// Token: 0x0400B462 RID: 46178
			private readonly string REWARD_PICKUP;

			// Token: 0x0400B463 RID: 46179
			private readonly string REWARD_DEFAULT;

			// Token: 0x0400B464 RID: 46180
			private readonly string REWARD_IMAGE;

			// Token: 0x0400B465 RID: 46181
			private readonly string REWARD_RECIEVED_FRAME;

			// Token: 0x0400B466 RID: 46182
			private readonly string REWARD_RECIEVED_ICON;

			// Token: 0x0400B467 RID: 46183
			private readonly string REWARD_WIN;

			// Token: 0x0400B468 RID: 46184
			private readonly string REWARD_NUM;

			// Token: 0x0400B469 RID: 46185
			private readonly string E_Gauge;

			// Token: 0x0400B46A RID: 46186
			private readonly string REWARD_GAUGE_EXTENDED_HEAD;

			// Token: 0x0400B46B RID: 46187
			private readonly string REWARD_GAUGE_EXTENDED_TAIL;

			// Token: 0x0400B46C RID: 46188
			private readonly string E_GaugeExtendHeadFill;

			// Token: 0x0400B46D RID: 46189
			private readonly string E_GaugeExtendTailFill;

			// Token: 0x0400B46E RID: 46190
			private readonly string SCROLL_LABEL;

			// Token: 0x0400B46F RID: 46191
			private readonly string ANALOG_DIRECTION_ITEM;

			// Token: 0x0400B470 RID: 46192
			private readonly string ICON_L;

			// Token: 0x0400B471 RID: 46193
			private readonly string ICON_R;

			// Token: 0x0400B472 RID: 46194
			private const int REWARD_NUM_PER_PAGE = 3;

			// Token: 0x0400B473 RID: 46195
			private int duel_trial_id;

			// Token: 0x0400B474 RID: 46196
			private int nameRegId;

			// Token: 0x0400B475 RID: 46197
			private int logoId;

			// Token: 0x0400B476 RID: 46198
			private string titleStr;

			// Token: 0x0400B477 RID: 46199
			private ColosseumUtil.StatusDuelTrial status;

			// Token: 0x0400B478 RID: 46200
			private ColosseumDeckWidget colosseumDeckWidget;

			// Token: 0x0400B479 RID: 46201
			private InfinityScrollView isv;

			// Token: 0x0400B47A RID: 46202
			private ElementObjectManager scrollEom;

			// Token: 0x0400B47B RID: 46203
			private SlidePagerWidget slidePagerWidget;

			// Token: 0x0400B47C RID: 46204
			private List<ColosseumInfoViewController.DuelTrialBehaviour.RewardData> rewardList;

			// Token: 0x0400B47D RID: 46205
			private int rewardPageNum;

			// Token: 0x0400B47E RID: 46206
			private int rewardSpaceNum;

			// Token: 0x0200103B RID: 4155
			private class RewardData
			{
				// Token: 0x06007CC5 RID: 31941 RVA: 0x00002739 File Offset: 0x00000939
				public RewardData(int win, int itemCategory, int itemId, int num, bool isPeriod, bool existsItem, bool received, int shopId)
				{
				}

				// Token: 0x06007CC6 RID: 31942 RVA: 0x00002739 File Offset: 0x00000939
				public RewardData(int win, bool existsItem, bool received)
				{
				}

				// Token: 0x0400B47F RID: 46207
				public int win;

				// Token: 0x0400B480 RID: 46208
				public int itemCategory;

				// Token: 0x0400B481 RID: 46209
				public int itemId;

				// Token: 0x0400B482 RID: 46210
				public int num;

				// Token: 0x0400B483 RID: 46211
				public bool isPeriod;

				// Token: 0x0400B484 RID: 46212
				public bool existsItem;

				// Token: 0x0400B485 RID: 46213
				public bool received;

				// Token: 0x0400B486 RID: 46214
				public int shopId;
			}
		}

		// Token: 0x0200103C RID: 4156
		protected class DuelistCupBehaviour : ColosseumInfoViewController.ModeBehaviour
		{
			// Token: 0x06007CC7 RID: 31943 RVA: 0x000F67A7 File Offset: 0x000F49A7
			public DuelistCupBehaviour(ViewControllerManager manager, ColosseumInfoViewController vc, ElementObjectManager parentEOM, ElementObjectManager viewEOM, ElementObjectManager menuEOM, ElementObjectManager overviewEOM, int cid)
				: base(null, null, null, null, null, null)
			{
			}

			// Token: 0x06007CC8 RID: 31944 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void CallAPI()
			{
			}

			// Token: 0x06007CC9 RID: 31945 RVA: 0x000029CC File Offset: 0x00000BCC
			protected override bool IsDispPerformance()
			{
				return false;
			}

			// Token: 0x06007CCA RID: 31946 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void SetMenu()
			{
			}

			// Token: 0x06007CCB RID: 31947 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void InitDisp()
			{
			}

			// Token: 0x06007CCC RID: 31948 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void UpdateDisp()
			{
			}

			// Token: 0x06007CCD RID: 31949 RVA: 0x0000216D File Offset: 0x0000036D
			protected virtual void ChangeDispDecideBtn(bool isSetDeck, bool checkedValid, bool checkedPossession)
			{
			}

			// Token: 0x06007CCE RID: 31950 RVA: 0x0000216D File Offset: 0x0000036D
			private void UpdateDisp1stStage(Dictionary<string, object> masterDic, Dictionary<string, object> duelMenuDic)
			{
			}

			// Token: 0x06007CCF RID: 31951 RVA: 0x0000216A File Offset: 0x0000036A
			private string GetDlvString(int dlv)
			{
				return null;
			}

			// Token: 0x06007CD0 RID: 31952 RVA: 0x0000216D File Offset: 0x0000036D
			protected virtual void UpdateDisp2ndStage(Dictionary<string, object> masterDic, Dictionary<string, object> duelMenuDic)
			{
			}

			// Token: 0x06007CD1 RID: 31953 RVA: 0x0000216D File Offset: 0x0000036D
			private void UpdateRankingButton()
			{
			}

			// Token: 0x06007CD2 RID: 31954 RVA: 0x0000216A File Offset: 0x0000036A
			protected virtual string GetNorankingText()
			{
				return null;
			}

			// Token: 0x06007CD3 RID: 31955 RVA: 0x0000216A File Offset: 0x0000036A
			protected virtual Dictionary<string, object> GetRankingViewArgs()
			{
				return null;
			}

			// Token: 0x06007CD4 RID: 31956 RVA: 0x000029CC File Offset: 0x00000BCC
			protected virtual int GetBGId()
			{
				return 0;
			}

			// Token: 0x06007CD5 RID: 31957 RVA: 0x0000216A File Offset: 0x0000036A
			protected virtual string GetLogoPath()
			{
				return null;
			}

			// Token: 0x06007CD6 RID: 31958 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void BackAction(ViewControllerManager vcm, ViewController vc, bool isEntry)
			{
			}

			// Token: 0x06007CD7 RID: 31959 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void PushAction(ViewControllerManager vcm, ViewController vc, bool isEntry)
			{
			}

			// Token: 0x06007CD8 RID: 31960 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void OnTransitionStart(ViewController.TransitionType type)
			{
			}

			// Token: 0x06007CD9 RID: 31961 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void OnTransitionEnd(ViewController.TransitionType type)
			{
			}

			// Token: 0x06007CDA RID: 31962 RVA: 0x0000216D File Offset: 0x0000036D
			protected void MovePageNextReward()
			{
			}

			// Token: 0x06007CDB RID: 31963 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void OnClickDuel()
			{
			}

			// Token: 0x06007CDC RID: 31964 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void CallAPIGetDeckList(int id, Action onSuccess)
			{
			}

			// Token: 0x06007CDD RID: 31965 RVA: 0x0000216A File Offset: 0x0000036A
			internal override Dictionary<string, object> GetDeckArgs(int identifer, bool isScratch)
			{
				return null;
			}

			// Token: 0x06007CDE RID: 31966 RVA: 0x0000216D File Offset: 0x0000036D
			protected void DispReward()
			{
			}

			// Token: 0x06007CDF RID: 31967 RVA: 0x0000216D File Offset: 0x0000036D
			protected void DispAward()
			{
			}

			// Token: 0x06007CE0 RID: 31968 RVA: 0x0000216A File Offset: 0x0000036A
			protected virtual Dictionary<string, object> GetResultArgs(int ranking, ColosseumResultViewController.AwardType awardType, Action dispRewardCallback)
			{
				return null;
			}

			// Token: 0x06007CE1 RID: 31969 RVA: 0x0000216A File Offset: 0x0000036A
			protected virtual string GetResultPrefPath()
			{
				return null;
			}

			// Token: 0x06007CE2 RID: 31970 RVA: 0x0000216D File Offset: 0x0000036D
			protected virtual void OnClickInfo()
			{
			}

			// Token: 0x06007CE3 RID: 31971 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void SetDeck(int cid)
			{
			}

			// Token: 0x06007CE4 RID: 31972 RVA: 0x0000216D File Offset: 0x0000036D
			private void OnClickDeck(ViewControllerManager manager)
			{
			}

			// Token: 0x06007CE5 RID: 31973 RVA: 0x0000216D File Offset: 0x0000036D
			private void OnUpdateEntity(GameObject go, int index)
			{
			}

			// Token: 0x06007CE6 RID: 31974 RVA: 0x0000216A File Offset: 0x0000036A
			private IReadOnlyList<ValueTuple<SelectionItem, int, int>> OnCustomCollectionSelectionItems(GameObject entity)
			{
				return null;
			}

			// Token: 0x06007CE7 RID: 31975 RVA: 0x000029CC File Offset: 0x00000BCC
			private bool OnCustomInnerTransition(SelectionItem selectionItem, PadInputDirection direction)
			{
				return false;
			}

			// Token: 0x06007CE8 RID: 31976 RVA: 0x0000216D File Offset: 0x0000036D
			private void OnInputAnalogDirection(SelectorManager.AnalogType analogType, PadInputDirection dir)
			{
			}

			// Token: 0x0400B487 RID: 46215
			private readonly string SCROLL_LABEL;

			// Token: 0x0400B488 RID: 46216
			private readonly string VIEW_1ST_LABEL;

			// Token: 0x0400B489 RID: 46217
			private readonly string VIEW_2ND_LABEL;

			// Token: 0x0400B48A RID: 46218
			private readonly string ROOT_VIEW_1ST_LABEL;

			// Token: 0x0400B48B RID: 46219
			private readonly string ROOT_VIEW_2ND_LABEL;

			// Token: 0x0400B48C RID: 46220
			private readonly string TXT_DLV_LABEL;

			// Token: 0x0400B48D RID: 46221
			private readonly string ANALOG_DIRECTION_ITEM;

			// Token: 0x0400B48E RID: 46222
			private readonly string REWARD_ROOT;

			// Token: 0x0400B48F RID: 46223
			private readonly string REWARD_NORMAL;

			// Token: 0x0400B490 RID: 46224
			private readonly string REWARD_PICKUP;

			// Token: 0x0400B491 RID: 46225
			private readonly string REWARD_DEFAULT;

			// Token: 0x0400B492 RID: 46226
			private readonly string REWARD_IMAGE;

			// Token: 0x0400B493 RID: 46227
			private readonly string REWARD_RECIEVED_FRAME;

			// Token: 0x0400B494 RID: 46228
			private readonly string REWARD_RECIEVED_ICON;

			// Token: 0x0400B495 RID: 46229
			private readonly string REWARD_NUM;

			// Token: 0x0400B496 RID: 46230
			private readonly string REWARD_DLV;

			// Token: 0x0400B497 RID: 46231
			private readonly string REWARD_GAUGE_EXTENDED_HEAD;

			// Token: 0x0400B498 RID: 46232
			private readonly string REWARD_GAUGE_EXTENDED_TAIL;

			// Token: 0x0400B499 RID: 46233
			private readonly string BTN_PREV;

			// Token: 0x0400B49A RID: 46234
			private readonly string BTN_NEXT;

			// Token: 0x0400B49B RID: 46235
			private readonly string BTN_PICKUP;

			// Token: 0x0400B49C RID: 46236
			private readonly string PICKUP_ICON;

			// Token: 0x0400B49D RID: 46237
			private readonly string PICKUP_IMAGE;

			// Token: 0x0400B49E RID: 46238
			private readonly string PICKUP_DLV;

			// Token: 0x0400B49F RID: 46239
			private readonly string PICKUP_NUM;

			// Token: 0x0400B4A0 RID: 46240
			private const int REWARD_NUM_PER_PAGE = 5;

			// Token: 0x0400B4A1 RID: 46241
			protected string titleStr;

			// Token: 0x0400B4A2 RID: 46242
			protected int cid;

			// Token: 0x0400B4A3 RID: 46243
			protected ColosseumUtil.PlayMode playMode;

			// Token: 0x0400B4A4 RID: 46244
			protected PvpMenuDefine.MatchingType matchingType;

			// Token: 0x0400B4A5 RID: 46245
			private int nameRegId;

			// Token: 0x0400B4A6 RID: 46246
			protected int logoId;

			// Token: 0x0400B4A7 RID: 46247
			private int matchingTime2nd;

			// Token: 0x0400B4A8 RID: 46248
			private int stage;

			// Token: 0x0400B4A9 RID: 46249
			protected int dispStage;

			// Token: 0x0400B4AA RID: 46250
			private int maxDlv;

			// Token: 0x0400B4AB RID: 46251
			private int rewardPageNum;

			// Token: 0x0400B4AC RID: 46252
			private int rewardSpaceNum;

			// Token: 0x0400B4AD RID: 46253
			protected ColosseumUtil.StatusDuelistCup status;

			// Token: 0x0400B4AE RID: 46254
			protected ColosseumDeckManager deckManager;

			// Token: 0x0400B4AF RID: 46255
			private ElementObjectManager rankingBtnEom;

			// Token: 0x0400B4B0 RID: 46256
			private InfinityScrollView isv;

			// Token: 0x0400B4B1 RID: 46257
			private ElementObjectManager scrollEom;

			// Token: 0x0400B4B2 RID: 46258
			private ElementObjectManager pickupEom;

			// Token: 0x0400B4B3 RID: 46259
			private SlidePagerWidget slidePagerWidget;

			// Token: 0x0400B4B4 RID: 46260
			private int popWallPaperCount;

			// Token: 0x0400B4B5 RID: 46261
			private List<ColosseumInfoViewController.DuelistCupBehaviour.Data1stReward> rewardList;

			// Token: 0x0200103D RID: 4157
			private class Data1stReward
			{
				// Token: 0x06007CE9 RID: 31977 RVA: 0x00002739 File Offset: 0x00000939
				public Data1stReward(int itemCategory, int itemId, int num, bool isPeriod, int needDlv, bool focus, bool received)
				{
				}

				// Token: 0x0400B4B6 RID: 46262
				public int itemCategory;

				// Token: 0x0400B4B7 RID: 46263
				public int itemId;

				// Token: 0x0400B4B8 RID: 46264
				public int num;

				// Token: 0x0400B4B9 RID: 46265
				public bool isPeriod;

				// Token: 0x0400B4BA RID: 46266
				public int needDlv;

				// Token: 0x0400B4BB RID: 46267
				public bool focus;

				// Token: 0x0400B4BC RID: 46268
				public bool received;
			}
		}

		// Token: 0x0200103E RID: 4158
		protected class ExhibitionBehaviour : ColosseumInfoViewController.ModeBehaviour
		{
			// Token: 0x06007CEA RID: 31978 RVA: 0x000F67A7 File Offset: 0x000F49A7
			public ExhibitionBehaviour(ViewControllerManager manager, ColosseumInfoViewController vc, ElementObjectManager parentEOM, ElementObjectManager viewEOM, ElementObjectManager menuEOM, ElementObjectManager overviewEOM, int exhid)
				: base(null, null, null, null, null, null)
			{
			}

			// Token: 0x06007CEB RID: 31979 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void CallAPI()
			{
			}

			// Token: 0x06007CEC RID: 31980 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void SetDuelButton(ElementObjectManager menuEOM)
			{
			}

			// Token: 0x06007CED RID: 31981 RVA: 0x000029CC File Offset: 0x00000BCC
			protected override bool IsDispPerformance()
			{
				return false;
			}

			// Token: 0x06007CEE RID: 31982 RVA: 0x0000216D File Offset: 0x0000036D
			private void DispReward()
			{
			}

			// Token: 0x06007CEF RID: 31983 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void SetMenu()
			{
			}

			// Token: 0x06007CF0 RID: 31984 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void InitDisp()
			{
			}

			// Token: 0x06007CF1 RID: 31985 RVA: 0x0000216D File Offset: 0x0000036D
			private void SetShortcutLRReward(bool isSet)
			{
			}

			// Token: 0x06007CF2 RID: 31986 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void UpdateDisp()
			{
			}

			// Token: 0x06007CF3 RID: 31987 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void BackAction(ViewControllerManager vcm, ViewController vc, bool isEntry)
			{
			}

			// Token: 0x06007CF4 RID: 31988 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void OnTransitionEnd(ViewController.TransitionType type)
			{
			}

			// Token: 0x06007CF5 RID: 31989 RVA: 0x0000216D File Offset: 0x0000036D
			private void MovePageNextReward()
			{
			}

			// Token: 0x06007CF6 RID: 31990 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void OnClickDuel()
			{
			}

			// Token: 0x06007CF7 RID: 31991 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void CallAPIGetDeckList(int id, Action onSuccess)
			{
			}

			// Token: 0x06007CF8 RID: 31992 RVA: 0x0000216A File Offset: 0x0000036A
			internal override Dictionary<string, object> GetDeckArgs(int identifer, bool isScratch)
			{
				return null;
			}

			// Token: 0x06007CF9 RID: 31993 RVA: 0x0000216D File Offset: 0x0000036D
			private void UpdateReward(int token)
			{
			}

			// Token: 0x06007CFA RID: 31994 RVA: 0x0000216D File Offset: 0x0000036D
			private void OnUpdateEntity(GameObject go, int index)
			{
			}

			// Token: 0x06007CFB RID: 31995 RVA: 0x0000216A File Offset: 0x0000036A
			private IReadOnlyList<ValueTuple<SelectionItem, int, int>> OnCustomCollectionSelectionItems(GameObject entity)
			{
				return null;
			}

			// Token: 0x06007CFC RID: 31996 RVA: 0x000029CC File Offset: 0x00000BCC
			private bool OnCustomInnerTransition(SelectionItem selectionItem, PadInputDirection direction)
			{
				return false;
			}

			// Token: 0x06007CFD RID: 31997 RVA: 0x0000216D File Offset: 0x0000036D
			private void OnInputAnalogDirection(SelectorManager.AnalogType analogType, PadInputDirection dir)
			{
			}

			// Token: 0x06007CFE RID: 31998 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void SetDeck(int tid)
			{
			}

			// Token: 0x06007CFF RID: 31999 RVA: 0x0000216A File Offset: 0x0000036A
			private List<ColosseumDeckWidget.ButtonInfo> SetRentalDeck(Dictionary<string, object> tInfoDic)
			{
				return null;
			}

			// Token: 0x06007D00 RID: 32000 RVA: 0x0000216A File Offset: 0x0000036A
			private List<ColosseumDeckWidget.ButtonInfo> SetMyDeck(Dictionary<string, object> MTdic, Dictionary<string, object> tInfoDic)
			{
				return null;
			}

			// Token: 0x0400B4BD RID: 46269
			protected readonly string OBJ_REWARD_LABEL;

			// Token: 0x0400B4BE RID: 46270
			protected readonly string IMG_GAUGE_LABEL;

			// Token: 0x0400B4BF RID: 46271
			protected readonly string IMG_RECEIVED_LABEL;

			// Token: 0x0400B4C0 RID: 46272
			protected readonly string IMG_DEFAULT_FRAME_LABEL;

			// Token: 0x0400B4C1 RID: 46273
			protected readonly string IMG_RECEIVED_FRAME_LABEL;

			// Token: 0x0400B4C2 RID: 46274
			protected readonly string IMG_EVENT_CATEGORY;

			// Token: 0x0400B4C3 RID: 46275
			protected readonly string TXT_NEEDTOKEN_LABEL;

			// Token: 0x0400B4C4 RID: 46276
			protected readonly string TXT_DECK_LABEL;

			// Token: 0x0400B4C5 RID: 46277
			protected readonly string TXT_PLAY_LABEL;

			// Token: 0x0400B4C6 RID: 46278
			protected readonly string TXT_ITEM_NUM_LABEL;

			// Token: 0x0400B4C7 RID: 46279
			protected readonly string TAB_GROUP_LABEL;

			// Token: 0x0400B4C8 RID: 46280
			protected readonly string TAB_RENTAL_LABEL;

			// Token: 0x0400B4C9 RID: 46281
			protected readonly string TAB_MYDECK_LABEL;

			// Token: 0x0400B4CA RID: 46282
			protected readonly string ROOT_RENTAL_LABEL;

			// Token: 0x0400B4CB RID: 46283
			protected readonly string ROOT_MYDECK_LABEL;

			// Token: 0x0400B4CC RID: 46284
			protected readonly string TMP_DECK_RENTAL_LABEL;

			// Token: 0x0400B4CD RID: 46285
			protected readonly string BTN_PLAY_LABEL;

			// Token: 0x0400B4CE RID: 46286
			protected readonly string BTN_DECK_LABEL;

			// Token: 0x0400B4CF RID: 46287
			private readonly string SCROLL_LABEL;

			// Token: 0x0400B4D0 RID: 46288
			private readonly string ANALOG_DIRECTION_ITEM;

			// Token: 0x0400B4D1 RID: 46289
			private readonly string REWARD_ROOT;

			// Token: 0x0400B4D2 RID: 46290
			private readonly string REWARD_NORMAL;

			// Token: 0x0400B4D3 RID: 46291
			private readonly string REWARD_PICKUP;

			// Token: 0x0400B4D4 RID: 46292
			private readonly string REWARD_DEFAULT;

			// Token: 0x0400B4D5 RID: 46293
			private readonly string REWARD_IMAGE;

			// Token: 0x0400B4D6 RID: 46294
			private readonly string REWARD_RECIEVED_FRAME;

			// Token: 0x0400B4D7 RID: 46295
			private readonly string REWARD_RECIEVED_ICON;

			// Token: 0x0400B4D8 RID: 46296
			private readonly string REWARD_NUM;

			// Token: 0x0400B4D9 RID: 46297
			private readonly string REWARD_DLV;

			// Token: 0x0400B4DA RID: 46298
			private readonly string REWARD_GAUGE_EXTENDED_HEAD;

			// Token: 0x0400B4DB RID: 46299
			private readonly string REWARD_GAUGE_EXTENDED_TAIL;

			// Token: 0x0400B4DC RID: 46300
			private readonly string BTN_PREV;

			// Token: 0x0400B4DD RID: 46301
			private readonly string BTN_NEXT;

			// Token: 0x0400B4DE RID: 46302
			private readonly string ICON_L;

			// Token: 0x0400B4DF RID: 46303
			private readonly string ICON_R;

			// Token: 0x0400B4E0 RID: 46304
			private readonly string BTN_PICKUP;

			// Token: 0x0400B4E1 RID: 46305
			private readonly string PICKUP_ICON;

			// Token: 0x0400B4E2 RID: 46306
			private readonly string PICKUP_IMAGE;

			// Token: 0x0400B4E3 RID: 46307
			private readonly string PICKUP_DLV;

			// Token: 0x0400B4E4 RID: 46308
			private readonly string PICKUP_NUM;

			// Token: 0x0400B4E5 RID: 46309
			private const int REWARD_NUM_PER_PAGE = 5;

			// Token: 0x0400B4E6 RID: 46310
			private int exhid;

			// Token: 0x0400B4E7 RID: 46311
			private int nameRegId;

			// Token: 0x0400B4E8 RID: 46312
			private int logoId;

			// Token: 0x0400B4E9 RID: 46313
			private ColosseumUtil.StatusExhibition status;

			// Token: 0x0400B4EA RID: 46314
			private ColosseumDeckWidget colosseumDeckWidget;

			// Token: 0x0400B4EB RID: 46315
			private InfinityScrollView isv;

			// Token: 0x0400B4EC RID: 46316
			private ElementObjectManager scrollEom;

			// Token: 0x0400B4ED RID: 46317
			private ElementObjectManager pickupEom;

			// Token: 0x0400B4EE RID: 46318
			private SlidePagerWidget slidePagerWidget;

			// Token: 0x0400B4EF RID: 46319
			private List<ColosseumInfoViewController.ExhibitionBehaviour.RewardData> rewardList;

			// Token: 0x0400B4F0 RID: 46320
			private int rewardPageNum;

			// Token: 0x0400B4F1 RID: 46321
			private int rewardSpaceNum;

			// Token: 0x0200103F RID: 4159
			private class RewardData
			{
				// Token: 0x06007D01 RID: 32001 RVA: 0x00002739 File Offset: 0x00000939
				public RewardData(int itemCategory, int itemId, int num, bool isPeriod, int needToken, bool focus, bool received)
				{
				}

				// Token: 0x0400B4F2 RID: 46322
				public int itemCategory;

				// Token: 0x0400B4F3 RID: 46323
				public int itemId;

				// Token: 0x0400B4F4 RID: 46324
				public int num;

				// Token: 0x0400B4F5 RID: 46325
				public bool isPeriod;

				// Token: 0x0400B4F6 RID: 46326
				public int needToken;

				// Token: 0x0400B4F7 RID: 46327
				public bool focus;

				// Token: 0x0400B4F8 RID: 46328
				public bool received;
			}
		}

		// Token: 0x02001040 RID: 4160
		protected class FreeBehaviour : ColosseumInfoViewController.ModeBehaviour
		{
			// Token: 0x06007D02 RID: 32002 RVA: 0x000F67A7 File Offset: 0x000F49A7
			public FreeBehaviour(ViewControllerManager manager, ColosseumInfoViewController vc, ElementObjectManager parentEOM, ElementObjectManager viewEOM, ElementObjectManager menuEOM, ElementObjectManager overviewEOM)
				: base(null, null, null, null, null, null)
			{
			}

			// Token: 0x06007D03 RID: 32003 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void CallAPI()
			{
			}

			// Token: 0x06007D04 RID: 32004 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void OnClickDuel()
			{
			}

			// Token: 0x06007D05 RID: 32005 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void SetMenu()
			{
			}

			// Token: 0x06007D06 RID: 32006 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void InitDisp()
			{
			}

			// Token: 0x06007D07 RID: 32007 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void UpdateDisp()
			{
			}

			// Token: 0x06007D08 RID: 32008 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void SetDeck(int did)
			{
			}

			// Token: 0x06007D09 RID: 32009 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void BackAction(ViewControllerManager vcm, ViewController vc, bool isEntry)
			{
			}

			// Token: 0x06007D0A RID: 32010 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void CallAPIGetDeckList(int id, Action onSuccess)
			{
			}

			// Token: 0x06007D0B RID: 32011 RVA: 0x0000216A File Offset: 0x0000036A
			internal override Dictionary<string, object> GetDeckArgs(int identifer, bool isScratch)
			{
				return null;
			}

			// Token: 0x06007D0C RID: 32012 RVA: 0x0000216D File Offset: 0x0000036D
			private void OnClickDeck(ViewControllerManager manager)
			{
			}

			// Token: 0x06007D0D RID: 32013 RVA: 0x0000216A File Offset: 0x0000036A
			protected GameObject CreateEmbedObj(int deckId)
			{
				return null;
			}

			// Token: 0x0400B4F9 RID: 46329
			protected readonly string E_LogoFree;

			// Token: 0x0400B4FA RID: 46330
			private int deckId;

			// Token: 0x0400B4FB RID: 46331
			private bool checkedValid;

			// Token: 0x0400B4FC RID: 46332
			private bool checkedPossession;
		}

		// Token: 0x02001041 RID: 4161
		protected abstract class ModeBehaviour
		{
			// Token: 0x06007D0E RID: 32014 RVA: 0x00002739 File Offset: 0x00000939
			protected ModeBehaviour(ViewControllerManager manager, ColosseumInfoViewController vc, ElementObjectManager parentEOM, ElementObjectManager viewEOM, ElementObjectManager menuEOM, ElementObjectManager overviewEOM)
			{
			}

			// Token: 0x06007D0F RID: 32015
			internal abstract void OnClickDuel();

			// Token: 0x06007D10 RID: 32016
			internal abstract void CallAPI();

			// Token: 0x06007D11 RID: 32017
			internal abstract void SetMenu();

			// Token: 0x06007D12 RID: 32018
			internal abstract void InitDisp();

			// Token: 0x06007D13 RID: 32019
			internal abstract void UpdateDisp();

			// Token: 0x06007D14 RID: 32020
			internal abstract void BackAction(ViewControllerManager vcm, ViewController vc, bool isEntry);

			// Token: 0x06007D15 RID: 32021
			internal abstract void SetDeck(int did);

			// Token: 0x06007D16 RID: 32022
			internal abstract void CallAPIGetDeckList(int id, Action onSuccess);

			// Token: 0x06007D17 RID: 32023
			internal abstract Dictionary<string, object> GetDeckArgs(int identifier, bool isScratch);

			// Token: 0x06007D18 RID: 32024 RVA: 0x0000216D File Offset: 0x0000036D
			internal virtual void PushAction(ViewControllerManager vcm, ViewController vc, bool isEntry)
			{
			}

			// Token: 0x06007D19 RID: 32025 RVA: 0x0000216D File Offset: 0x0000036D
			internal virtual void OnTransitionStart(ViewController.TransitionType type)
			{
			}

			// Token: 0x06007D1A RID: 32026 RVA: 0x0000216D File Offset: 0x0000036D
			internal virtual void OnTransitionEnd(ViewController.TransitionType type)
			{
			}

			// Token: 0x06007D1B RID: 32027 RVA: 0x0000216A File Offset: 0x0000036A
			internal virtual ElementObjectManager CreateTmpBtnDeck(UnityAction clickAction = null)
			{
				return null;
			}

			// Token: 0x06007D1C RID: 32028 RVA: 0x0000216A File Offset: 0x0000036A
			internal virtual ElementObjectManager CreateTmpBtnNormal(string name, UnityAction clickAction = null)
			{
				return null;
			}

			// Token: 0x06007D1D RID: 32029 RVA: 0x0000216D File Offset: 0x0000036D
			internal virtual void SetDeckDisabled(bool activeImage)
			{
			}

			// Token: 0x06007D1E RID: 32030 RVA: 0x0000216D File Offset: 0x0000036D
			internal virtual void SetDuelButton(ElementObjectManager menuEOM)
			{
			}

			// Token: 0x06007D1F RID: 32031 RVA: 0x000029CC File Offset: 0x00000BCC
			protected virtual bool IsDispPerformance()
			{
				return false;
			}

			// Token: 0x06007D20 RID: 32032 RVA: 0x0000216D File Offset: 0x0000036D
			protected void OpenEditDeck(int id, bool isScratch = false)
			{
			}

			// Token: 0x06007D21 RID: 32033 RVA: 0x0000216D File Offset: 0x0000036D
			protected void StartPerformance(ColosseumStartViewController.PrefabType prefabType, string tournamentName = "", int logoId = 0, int identifier = 0, Action onFinish = null)
			{
			}

			// Token: 0x06007D22 RID: 32034 RVA: 0x000029CC File Offset: 0x00000BCC
			private bool CheckStartPerformance()
			{
				return false;
			}

			// Token: 0x0400B4FD RID: 46333
			protected readonly string IMG_LABEL;

			// Token: 0x0400B4FE RID: 46334
			protected readonly string IMG_BG_LABEL;

			// Token: 0x0400B4FF RID: 46335
			protected readonly string IMG_RANK_LABEL;

			// Token: 0x0400B500 RID: 46336
			protected readonly string TXT_CAUTION_LABEL;

			// Token: 0x0400B501 RID: 46337
			protected readonly string TXT_DATE_LABEL;

			// Token: 0x0400B502 RID: 46338
			protected readonly string TXT_STATUS_LABEL;

			// Token: 0x0400B503 RID: 46339
			protected readonly string TXT_SUB1_LABEL;

			// Token: 0x0400B504 RID: 46340
			protected readonly string TXT_SUB1_TITLE_LABEL;

			// Token: 0x0400B505 RID: 46341
			protected readonly string TXT_SUB2_LABEL;

			// Token: 0x0400B506 RID: 46342
			protected readonly string TXT_SUB2_TITLE_LABEL;

			// Token: 0x0400B507 RID: 46343
			protected readonly string TXT_OVERVIEW_LABEL;

			// Token: 0x0400B508 RID: 46344
			protected readonly string TXT_TITLE_LABEL;

			// Token: 0x0400B509 RID: 46345
			protected readonly string TXT_DUEL_NAME_LABEL;

			// Token: 0x0400B50A RID: 46346
			protected readonly string TXT_NAME_LABEL;

			// Token: 0x0400B50B RID: 46347
			protected readonly string IMG_DECK_LABEL;

			// Token: 0x0400B50C RID: 46348
			protected readonly string IMG_DECK_EMPTY_LABEL;

			// Token: 0x0400B50D RID: 46349
			protected readonly string IMG_DECK_DISABLED_LABEL;

			// Token: 0x0400B50E RID: 46350
			protected readonly string TXT_LABEL;

			// Token: 0x0400B50F RID: 46351
			protected readonly string BTN_LABEL;

			// Token: 0x0400B510 RID: 46352
			protected readonly string TMP_BTN_DECK_LABEL;

			// Token: 0x0400B511 RID: 46353
			protected readonly string TMP_BTN_NORMAL_LABEL;

			// Token: 0x0400B512 RID: 46354
			protected readonly string TXT_DECIDE_LABEL;

			// Token: 0x0400B513 RID: 46355
			protected readonly string BTN_DECIDE_LABEL;

			// Token: 0x0400B514 RID: 46356
			internal readonly ViewControllerManager manager;

			// Token: 0x0400B515 RID: 46357
			internal readonly ColosseumInfoViewController vc;

			// Token: 0x0400B516 RID: 46358
			internal readonly ElementObjectManager parentEOM;

			// Token: 0x0400B517 RID: 46359
			internal readonly ElementObjectManager menuEOM;

			// Token: 0x0400B518 RID: 46360
			internal readonly ElementObjectManager overviewEOM;

			// Token: 0x0400B519 RID: 46361
			protected ElementObjectManager deckBtnEOM;

			// Token: 0x0400B51A RID: 46362
			protected bool isSetDeck;

			// Token: 0x0400B51B RID: 46363
			protected DeckCaseWidget deckCase;

			// Token: 0x0400B51C RID: 46364
			protected ElementObjectManager viewEOM;

			// Token: 0x0400B51D RID: 46365
			protected bool isSetRentalDeck;

			// Token: 0x0400B51E RID: 46366
			protected string startDate;

			// Token: 0x0400B51F RID: 46367
			protected string endDate;

			// Token: 0x0400B520 RID: 46368
			protected string startDateReward;

			// Token: 0x0400B521 RID: 46369
			protected string endDateReward;

			// Token: 0x0400B522 RID: 46370
			protected int matchingTime;
		}

		// Token: 0x02001042 RID: 4162
		protected class RankEventBehaviour : ColosseumInfoViewController.ModeBehaviour
		{
			// Token: 0x06007D23 RID: 32035 RVA: 0x000F67A7 File Offset: 0x000F49A7
			public RankEventBehaviour(ViewControllerManager manager, ColosseumInfoViewController vc, ElementObjectManager parentEOM, ElementObjectManager viewEOM, ElementObjectManager menuEOM, ElementObjectManager overviewEOM, int rank_event_id)
				: base(null, null, null, null, null, null)
			{
			}

			// Token: 0x06007D24 RID: 32036 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void CallAPI()
			{
			}

			// Token: 0x06007D25 RID: 32037 RVA: 0x000029CC File Offset: 0x00000BCC
			protected override bool IsDispPerformance()
			{
				return false;
			}

			// Token: 0x06007D26 RID: 32038 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void SetMenu()
			{
			}

			// Token: 0x06007D27 RID: 32039 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void InitDisp()
			{
			}

			// Token: 0x06007D28 RID: 32040 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void UpdateDisp()
			{
			}

			// Token: 0x06007D29 RID: 32041 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void BackAction(ViewControllerManager vcm, ViewController vc, bool isEntry)
			{
			}

			// Token: 0x06007D2A RID: 32042 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void OnClickDuel()
			{
			}

			// Token: 0x06007D2B RID: 32043 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void CallAPIGetDeckList(int id, Action onSuccess)
			{
			}

			// Token: 0x06007D2C RID: 32044 RVA: 0x0000216A File Offset: 0x0000036A
			internal override Dictionary<string, object> GetDeckArgs(int identifer, bool isScratch)
			{
				return null;
			}

			// Token: 0x06007D2D RID: 32045 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void SetDeck(int tid)
			{
			}

			// Token: 0x06007D2E RID: 32046 RVA: 0x0000216D File Offset: 0x0000036D
			private void OnClickDeck(ViewControllerManager manager)
			{
			}

			// Token: 0x06007D2F RID: 32047 RVA: 0x000F67B8 File Offset: 0x000F49B8
			private ValueTuple<int, int> GetRank(string label)
			{
				return default(ValueTuple<int, int>);
			}

			// Token: 0x06007D30 RID: 32048 RVA: 0x0000216A File Offset: 0x0000036A
			private string GetNextRankText(int rank, int tier)
			{
				return null;
			}

			// Token: 0x06007D31 RID: 32049 RVA: 0x0000216D File Offset: 0x0000036D
			private void DispReward()
			{
			}

			// Token: 0x0400B523 RID: 46371
			private readonly string TXT_CON_WIN;

			// Token: 0x0400B524 RID: 46372
			private readonly string IMG_EVENT_RANK;

			// Token: 0x0400B525 RID: 46373
			private int rank_event_id;

			// Token: 0x0400B526 RID: 46374
			private int nameRegId;

			// Token: 0x0400B527 RID: 46375
			private int logoId;

			// Token: 0x0400B528 RID: 46376
			private ColosseumUtil.StatusRankEvent status;

			// Token: 0x0400B529 RID: 46377
			private ColosseumDeckManager deckManager;
		}

		// Token: 0x02001043 RID: 4163
		protected class StandardBehaviour : ColosseumInfoViewController.ModeBehaviour
		{
			// Token: 0x06007D32 RID: 32050 RVA: 0x000F67A7 File Offset: 0x000F49A7
			public StandardBehaviour(ViewControllerManager manager, ColosseumInfoViewController vc, ElementObjectManager parentEOM, ElementObjectManager viewEOM, ElementObjectManager menuEOM, ElementObjectManager overviewEOM)
				: base(null, null, null, null, null, null)
			{
			}

			// Token: 0x06007D33 RID: 32051 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void CallAPI()
			{
			}

			// Token: 0x06007D34 RID: 32052 RVA: 0x000029CC File Offset: 0x00000BCC
			protected override bool IsDispPerformance()
			{
				return false;
			}

			// Token: 0x06007D35 RID: 32053 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void OnClickDuel()
			{
			}

			// Token: 0x06007D36 RID: 32054 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void SetMenu()
			{
			}

			// Token: 0x06007D37 RID: 32055 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void InitDisp()
			{
			}

			// Token: 0x06007D38 RID: 32056 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void UpdateDisp()
			{
			}

			// Token: 0x06007D39 RID: 32057 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void SetDeck(int did)
			{
			}

			// Token: 0x06007D3A RID: 32058 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void BackAction(ViewControllerManager vcm, ViewController vc, bool isEntry)
			{
			}

			// Token: 0x06007D3B RID: 32059 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void CallAPIGetDeckList(int id, Action onSuccess)
			{
			}

			// Token: 0x06007D3C RID: 32060 RVA: 0x0000216A File Offset: 0x0000036A
			internal override Dictionary<string, object> GetDeckArgs(int identifer, bool isScratch)
			{
				return null;
			}

			// Token: 0x06007D3D RID: 32061 RVA: 0x000F67D0 File Offset: 0x000F49D0
			private ValueTuple<int, int> GetRank(string label)
			{
				return default(ValueTuple<int, int>);
			}

			// Token: 0x06007D3E RID: 32062 RVA: 0x0000216A File Offset: 0x0000036A
			private string GetNextRankText(int rank, int tier)
			{
				return null;
			}

			// Token: 0x06007D3F RID: 32063 RVA: 0x0000216D File Offset: 0x0000036D
			private void OnClickDeck(ViewControllerManager manager)
			{
			}

			// Token: 0x06007D40 RID: 32064 RVA: 0x0000216A File Offset: 0x0000036A
			protected GameObject CreateEmbedObj(int deckId)
			{
				return null;
			}

			// Token: 0x0400B52A RID: 46378
			private int seasonId;

			// Token: 0x0400B52B RID: 46379
			private int deckId;

			// Token: 0x0400B52C RID: 46380
			private bool checkedValid;

			// Token: 0x0400B52D RID: 46381
			private bool checkedPossession;

			// Token: 0x0400B52E RID: 46382
			private readonly string TXT_CON_WIN;

			// Token: 0x0400B52F RID: 46383
			private readonly string E_RootRankIconEf;

			// Token: 0x0400B530 RID: 46384
			private readonly string E_RankIconEf;

			// Token: 0x0400B531 RID: 46385
			private readonly string E_RootUpRankIconEf;

			// Token: 0x0400B532 RID: 46386
			private readonly string E_UpRankIconEf;

			// Token: 0x0400B533 RID: 46387
			private string mmaPath;
		}

		// Token: 0x02001044 RID: 4164
		protected class TournamentBehaviour : ColosseumInfoViewController.ModeBehaviour
		{
			// Token: 0x06007D41 RID: 32065 RVA: 0x000F67A7 File Offset: 0x000F49A7
			public TournamentBehaviour(ViewControllerManager manager, ColosseumInfoViewController vc, ElementObjectManager parentEOM, ElementObjectManager viewEOM, ElementObjectManager menuEOM, ElementObjectManager overviewEOM, int tid)
				: base(null, null, null, null, null, null)
			{
			}

			// Token: 0x06007D42 RID: 32066 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void CallAPI()
			{
			}

			// Token: 0x06007D43 RID: 32067 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void SetMenu()
			{
			}

			// Token: 0x06007D44 RID: 32068 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void InitDisp()
			{
			}

			// Token: 0x06007D45 RID: 32069 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void UpdateDisp()
			{
			}

			// Token: 0x06007D46 RID: 32070 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void BackAction(ViewControllerManager vcm, ViewController vc, bool isEntry)
			{
			}

			// Token: 0x06007D47 RID: 32071 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void OnClickDuel()
			{
			}

			// Token: 0x06007D48 RID: 32072 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void CallAPIGetDeckList(int id, Action onSuccess)
			{
			}

			// Token: 0x06007D49 RID: 32073 RVA: 0x0000216A File Offset: 0x0000036A
			internal override Dictionary<string, object> GetDeckArgs(int identifer, bool isScratch)
			{
				return null;
			}

			// Token: 0x06007D4A RID: 32074 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void SetDeck(int tid)
			{
			}

			// Token: 0x06007D4B RID: 32075 RVA: 0x0000216D File Offset: 0x0000036D
			private void OnClickDeck(ViewControllerManager manager)
			{
			}

			// Token: 0x06007D4C RID: 32076 RVA: 0x0000216D File Offset: 0x0000036D
			private void DispReward()
			{
			}

			// Token: 0x0400B534 RID: 46388
			protected readonly string IMG_EVENT_CATEGORY;

			// Token: 0x0400B535 RID: 46389
			private int tid;

			// Token: 0x0400B536 RID: 46390
			private int nameRegId;

			// Token: 0x0400B537 RID: 46391
			private int logoId;

			// Token: 0x0400B538 RID: 46392
			private string titleStr;

			// Token: 0x0400B539 RID: 46393
			private bool checkedValid;

			// Token: 0x0400B53A RID: 46394
			private bool checkedPossession;

			// Token: 0x0400B53B RID: 46395
			private ColosseumUtil.StatusTournament status;

			// Token: 0x0400B53C RID: 46396
			private ColosseumDeckManager deckManager;
		}

		// Token: 0x02001045 RID: 4165
		protected class VersusBehaviour : ColosseumInfoViewController.ModeBehaviour
		{
			// Token: 0x06007D4D RID: 32077 RVA: 0x000F67A7 File Offset: 0x000F49A7
			public VersusBehaviour(ViewControllerManager manager, ColosseumInfoViewController vc, ElementObjectManager parentEOM, ElementObjectManager viewEOM, ElementObjectManager menuEOM, ElementObjectManager overviewEOM, int versus_id, int group_id, bool isCalledDetailAPI)
				: base(null, null, null, null, null, null)
			{
			}

			// Token: 0x06007D4E RID: 32078 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void CallAPI()
			{
			}

			// Token: 0x06007D4F RID: 32079 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void SetDuelButton(ElementObjectManager menuEOM)
			{
			}

			// Token: 0x06007D50 RID: 32080 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void SetMenu()
			{
			}

			// Token: 0x06007D51 RID: 32081 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void InitDisp()
			{
			}

			// Token: 0x06007D52 RID: 32082 RVA: 0x0000216D File Offset: 0x0000036D
			private void SetShortcutLRReward(bool isSet)
			{
			}

			// Token: 0x06007D53 RID: 32083 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void UpdateDisp()
			{
			}

			// Token: 0x06007D54 RID: 32084 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void BackAction(ViewControllerManager vcm, ViewController vc, bool isEntry)
			{
			}

			// Token: 0x06007D55 RID: 32085 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void OnTransitionEnd(ViewController.TransitionType type)
			{
			}

			// Token: 0x06007D56 RID: 32086 RVA: 0x0000216D File Offset: 0x0000036D
			private void MovePageNextReward()
			{
			}

			// Token: 0x06007D57 RID: 32087 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void OnClickDuel()
			{
			}

			// Token: 0x06007D58 RID: 32088 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void CallAPIGetDeckList(int id, Action onSuccess)
			{
			}

			// Token: 0x06007D59 RID: 32089 RVA: 0x0000216A File Offset: 0x0000036A
			internal override Dictionary<string, object> GetDeckArgs(int identifer, bool isScratch)
			{
				return null;
			}

			// Token: 0x06007D5A RID: 32090 RVA: 0x0000216D File Offset: 0x0000036D
			private void UpdateReward(int token)
			{
			}

			// Token: 0x06007D5B RID: 32091 RVA: 0x0000216D File Offset: 0x0000036D
			private void OnUpdateEntity(GameObject go, int index)
			{
			}

			// Token: 0x06007D5C RID: 32092 RVA: 0x0000216A File Offset: 0x0000036A
			private IReadOnlyList<ValueTuple<SelectionItem, int, int>> OnCustomCollectionSelectionItems(GameObject entity)
			{
				return null;
			}

			// Token: 0x06007D5D RID: 32093 RVA: 0x000029CC File Offset: 0x00000BCC
			private bool OnCustomInnerTransition(SelectionItem selectionItem, PadInputDirection direction)
			{
				return false;
			}

			// Token: 0x06007D5E RID: 32094 RVA: 0x0000216D File Offset: 0x0000036D
			private void OnInputAnalogDirection(SelectorManager.AnalogType analogType, PadInputDirection dir)
			{
			}

			// Token: 0x06007D5F RID: 32095 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void SetDeck(int tid)
			{
			}

			// Token: 0x06007D60 RID: 32096 RVA: 0x0000216A File Offset: 0x0000036A
			private List<ColosseumDeckWidget.ButtonInfo> SetRentalDeck(Dictionary<string, object> tInfoDic)
			{
				return null;
			}

			// Token: 0x06007D61 RID: 32097 RVA: 0x0000216A File Offset: 0x0000036A
			private List<ColosseumDeckWidget.ButtonInfo> SetMyDeck(Dictionary<string, object> MTdic, Dictionary<string, object> tInfoDic)
			{
				return null;
			}

			// Token: 0x06007D62 RID: 32098 RVA: 0x0000216D File Offset: 0x0000036D
			private void DispReward()
			{
			}

			// Token: 0x06007D63 RID: 32099 RVA: 0x0000216D File Offset: 0x0000036D
			private void CallAPIOpenCampaignPack(int item_id, int num, Action onSuccess = null)
			{
			}

			// Token: 0x06007D64 RID: 32100 RVA: 0x0000216D File Offset: 0x0000036D
			private void SetGroup(int groupNo)
			{
			}

			// Token: 0x06007D65 RID: 32101 RVA: 0x0000216D File Offset: 0x0000036D
			private void DispResult(UnityAction OnFinished = null)
			{
			}

			// Token: 0x0400B53D RID: 46397
			private readonly string BTN_PREV;

			// Token: 0x0400B53E RID: 46398
			private readonly string BTN_NEXT;

			// Token: 0x0400B53F RID: 46399
			private readonly string REWARD_ROOT;

			// Token: 0x0400B540 RID: 46400
			private readonly string REWARD_NORMAL;

			// Token: 0x0400B541 RID: 46401
			private readonly string REWARD_PICKUP;

			// Token: 0x0400B542 RID: 46402
			private readonly string REWARD_DEFAULT;

			// Token: 0x0400B543 RID: 46403
			private readonly string REWARD_IMAGE;

			// Token: 0x0400B544 RID: 46404
			private readonly string REWARD_RECIEVED_FRAME;

			// Token: 0x0400B545 RID: 46405
			private readonly string REWARD_RECIEVED_ICON;

			// Token: 0x0400B546 RID: 46406
			private readonly string REWARD_DLV;

			// Token: 0x0400B547 RID: 46407
			private readonly string REWARD_NUM;

			// Token: 0x0400B548 RID: 46408
			private readonly string REWARD_GAUGE_EXTENDED_HEAD;

			// Token: 0x0400B549 RID: 46409
			private readonly string REWARD_GAUGE_EXTENDED_TAIL;

			// Token: 0x0400B54A RID: 46410
			private readonly string SCROLL_LABEL;

			// Token: 0x0400B54B RID: 46411
			private readonly string ANALOG_DIRECTION_ITEM;

			// Token: 0x0400B54C RID: 46412
			private readonly string ICON_L;

			// Token: 0x0400B54D RID: 46413
			private readonly string ICON_R;

			// Token: 0x0400B54E RID: 46414
			private readonly string BTN_PICKUP;

			// Token: 0x0400B54F RID: 46415
			private readonly string PICKUP_ICON;

			// Token: 0x0400B550 RID: 46416
			private readonly string PICKUP_IMAGE;

			// Token: 0x0400B551 RID: 46417
			private readonly string PICKUP_DLV;

			// Token: 0x0400B552 RID: 46418
			private readonly string PICKUP_NUM;

			// Token: 0x0400B553 RID: 46419
			private readonly string E_RootGroup;

			// Token: 0x0400B554 RID: 46420
			private readonly string E_ImageParticipate;

			// Token: 0x0400B555 RID: 46421
			private readonly string E_Text;

			// Token: 0x0400B556 RID: 46422
			private readonly string E_TextParticipate;

			// Token: 0x0400B557 RID: 46423
			private readonly string E_InformButtonGroup;

			// Token: 0x0400B558 RID: 46424
			private readonly string E_Template;

			// Token: 0x0400B559 RID: 46425
			private readonly string E_TemplateSmall;

			// Token: 0x0400B55A RID: 46426
			private readonly string E_TemplateSmallMB;

			// Token: 0x0400B55B RID: 46427
			private readonly string E_TextMyPoint;

			// Token: 0x0400B55C RID: 46428
			private readonly string E_TextMyPointLabel;

			// Token: 0x0400B55D RID: 46429
			private readonly string E_TextTotalPoint;

			// Token: 0x0400B55E RID: 46430
			private readonly string E_TextTotalPointLabel;

			// Token: 0x0400B55F RID: 46431
			private const int REWARD_NUM_PER_PAGE = 5;

			// Token: 0x0400B560 RID: 46432
			private int versus_id;

			// Token: 0x0400B561 RID: 46433
			private int nameRegId;

			// Token: 0x0400B562 RID: 46434
			private int logoId;

			// Token: 0x0400B563 RID: 46435
			private string titleStr;

			// Token: 0x0400B564 RID: 46436
			private ColosseumUtil.StatusVersus status;

			// Token: 0x0400B565 RID: 46437
			private ColosseumDeckWidget colosseumDeckWidget;

			// Token: 0x0400B566 RID: 46438
			private InfinityScrollView isv;

			// Token: 0x0400B567 RID: 46439
			private ElementObjectManager scrollEom;

			// Token: 0x0400B568 RID: 46440
			private ElementObjectManager pickupEom;

			// Token: 0x0400B569 RID: 46441
			private SlidePagerWidget slidePagerWidget;

			// Token: 0x0400B56A RID: 46442
			private List<ColosseumInfoViewController.VersusBehaviour.RewardData> rewardList;

			// Token: 0x0400B56B RID: 46443
			private int rewardPageNum;

			// Token: 0x0400B56C RID: 46444
			private int rewardSpaceNum;

			// Token: 0x0400B56D RID: 46445
			private ValueTuple<bool, int> currentGroup;

			// Token: 0x0400B56E RID: 46446
			private bool isCalledDetailAPI;

			// Token: 0x0400B56F RID: 46447
			private string mmaPath;

			// Token: 0x02001046 RID: 4166
			private class RewardData
			{
				// Token: 0x06007D66 RID: 32102 RVA: 0x00002739 File Offset: 0x00000939
				public RewardData(int itemCategory, int itemId, int num, bool isPeriod, int needToken, bool focus, bool received)
				{
				}

				// Token: 0x0400B570 RID: 46448
				public int itemCategory;

				// Token: 0x0400B571 RID: 46449
				public int itemId;

				// Token: 0x0400B572 RID: 46450
				public int num;

				// Token: 0x0400B573 RID: 46451
				public bool isPeriod;

				// Token: 0x0400B574 RID: 46452
				public int needToken;

				// Token: 0x0400B575 RID: 46453
				public bool focus;

				// Token: 0x0400B576 RID: 46454
				public bool received;
			}
		}

		// Token: 0x02001047 RID: 4167
		protected class WCSBehaviour : ColosseumInfoViewController.DuelistCupBehaviour
		{
			// Token: 0x06007D67 RID: 32103 RVA: 0x000F67E6 File Offset: 0x000F49E6
			public WCSBehaviour(ViewControllerManager manager, ColosseumInfoViewController vc, ElementObjectManager parentEOM, ElementObjectManager viewEOM, ElementObjectManager menuEOM, ElementObjectManager overviewEOM, int cid)
				: base(null, null, null, null, null, null, 0)
			{
			}

			// Token: 0x06007D68 RID: 32104 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void CallAPI()
			{
			}

			// Token: 0x06007D69 RID: 32105 RVA: 0x0000216A File Offset: 0x0000036A
			protected override string GetNorankingText()
			{
				return null;
			}

			// Token: 0x06007D6A RID: 32106 RVA: 0x0000216A File Offset: 0x0000036A
			protected override Dictionary<string, object> GetRankingViewArgs()
			{
				return null;
			}

			// Token: 0x06007D6B RID: 32107 RVA: 0x0000216A File Offset: 0x0000036A
			protected override Dictionary<string, object> GetResultArgs(int ranking, ColosseumResultViewController.AwardType awardType, Action dispRewardCallback)
			{
				return null;
			}

			// Token: 0x06007D6C RID: 32108 RVA: 0x0000216D File Offset: 0x0000036D
			protected override void OnClickInfo()
			{
			}

			// Token: 0x06007D6D RID: 32109 RVA: 0x0000216A File Offset: 0x0000036A
			protected override string GetResultPrefPath()
			{
				return null;
			}

			// Token: 0x06007D6E RID: 32110 RVA: 0x0000216A File Offset: 0x0000036A
			protected override string GetLogoPath()
			{
				return null;
			}

			// Token: 0x06007D6F RID: 32111 RVA: 0x000029CC File Offset: 0x00000BCC
			protected override int GetBGId()
			{
				return 0;
			}

			// Token: 0x06007D70 RID: 32112 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void BackAction(ViewControllerManager vcm, ViewController vc, bool isEntry)
			{
			}

			// Token: 0x06007D71 RID: 32113 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void CallAPIGetDeckList(int id, Action onSuccess)
			{
			}

			// Token: 0x06007D72 RID: 32114 RVA: 0x0000216D File Offset: 0x0000036D
			protected override void UpdateDisp2ndStage(Dictionary<string, object> masterDic, Dictionary<string, object> duelMenuDic)
			{
			}

			// Token: 0x06007D73 RID: 32115 RVA: 0x0000216D File Offset: 0x0000036D
			protected override void ChangeDispDecideBtn(bool isSetDeck, bool checkedValid, bool checkedPossession)
			{
			}

			// Token: 0x06007D74 RID: 32116 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void SetDuelButton(ElementObjectManager menuEOM)
			{
			}

			// Token: 0x06007D75 RID: 32117 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void OnClickDuel()
			{
			}

			// Token: 0x0400B577 RID: 46455
			private readonly string E_TextGroupName;

			// Token: 0x0400B578 RID: 46456
			private readonly string E_ButtonEntry;

			// Token: 0x0400B579 RID: 46457
			private readonly string E_TextEntry;
		}
	}
}
