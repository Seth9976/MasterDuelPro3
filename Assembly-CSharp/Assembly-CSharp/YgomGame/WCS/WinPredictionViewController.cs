using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YgomGame.Menu;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.YGomTMPro;

namespace YgomGame.WCS
{
	// Token: 0x02000801 RID: 2049
	public class WinPredictionViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06003F88 RID: 16264 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003F89 RID: 16265 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06003F8A RID: 16266 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x06003F8B RID: 16267 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator rewardDisp()
		{
			return null;
		}

		// Token: 0x06003F8C RID: 16268 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetViewElements()
		{
		}

		// Token: 0x06003F8D RID: 16269 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateButtonView()
		{
		}

		// Token: 0x06003F8E RID: 16270 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize()
		{
		}

		// Token: 0x06003F8F RID: 16271 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06003F90 RID: 16272 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickTeamSelect()
		{
		}

		// Token: 0x06003F91 RID: 16273 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickRewardButton()
		{
		}

		// Token: 0x06003F92 RID: 16274 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnResult(ViewController from, object value)
		{
			return false;
		}

		// Token: 0x06003F93 RID: 16275 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStack(ViewControllerManager vcm, ViewController vc, bool isEntry)
		{
		}

		// Token: 0x06003F94 RID: 16276 RVA: 0x0000216D File Offset: 0x0000036D
		private void Import(Action OnCompleted = null)
		{
		}

		// Token: 0x06003F95 RID: 16277 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateTemplateView(Action OnCompleted = null)
		{
		}

		// Token: 0x06003F96 RID: 16278 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateTeamSelectText()
		{
		}

		// Token: 0x06003F97 RID: 16279 RVA: 0x0000216D File Offset: 0x0000036D
		private void ActiveTweenIcon(bool active)
		{
		}

		// Token: 0x06003F98 RID: 16280 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator DelaySE()
		{
			return null;
		}

		// Token: 0x06003F99 RID: 16281 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickTemplate(int idx)
		{
		}

		// Token: 0x06003F9A RID: 16282 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetCallbackInputDown(int order)
		{
		}

		// Token: 0x06003F9B RID: 16283 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetCallBackInputUp()
		{
		}

		// Token: 0x06003F9C RID: 16284 RVA: 0x0000216D File Offset: 0x0000036D
		private void CallApi(Action onComplited = null)
		{
		}

		// Token: 0x06003F9D RID: 16285 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenErrDialog(string title, string text)
		{
		}

		// Token: 0x06003F9E RID: 16286 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool CheckStatus()
		{
			return false;
		}

		// Token: 0x06003F9F RID: 16287 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetResultBgView(WinPredictionViewController.BgType bgType)
		{
		}

		// Token: 0x06003FA0 RID: 16288 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator TweenWait(string label)
		{
			return null;
		}

		// Token: 0x06003FA1 RID: 16289 RVA: 0x0000216A File Offset: 0x0000036A
		private string GetResultText(int orderIndex)
		{
			return null;
		}

		// Token: 0x06003FA2 RID: 16290 RVA: 0x0000216A File Offset: 0x0000036A
		private string GetProgressResultText(int orderIndex)
		{
			return null;
		}

		// Token: 0x06003FA3 RID: 16291 RVA: 0x0000216A File Offset: 0x0000036A
		private Dictionary<string, object> GetCWSupport()
		{
			return null;
		}

		// Token: 0x06003FA4 RID: 16292 RVA: 0x0000216A File Offset: 0x0000036A
		private Dictionary<string, object> GetCWTeam()
		{
			return null;
		}

		// Token: 0x0400388F RID: 14479
		private string TEAMSELECT_GROUP_LABEL;

		// Token: 0x04003890 RID: 14480
		private string TEAMSELECT_BTN_UNSELECT_LABEL;

		// Token: 0x04003891 RID: 14481
		private string TEAMSELECT_BTN_LABEL;

		// Token: 0x04003892 RID: 14482
		private string TEMPLATE_LABEL;

		// Token: 0x04003893 RID: 14483
		private string REWARD_BTN_LABEL;

		// Token: 0x04003894 RID: 14484
		private string TEXT_TEAM_SELECT_GROUP_LABEL;

		// Token: 0x04003895 RID: 14485
		private string TEXT_TEAM_SELECT_NAME_LABEL;

		// Token: 0x04003896 RID: 14486
		private string ICON_TEAM_LABEL;

		// Token: 0x04003897 RID: 14487
		private string TEAM_SELECT_CHEER_ICON_LABEL;

		// Token: 0x04003898 RID: 14488
		private string TEAM_SELECT_CHEER_ICON_RESULT_LABEL;

		// Token: 0x04003899 RID: 14489
		private string TEXT_TEAM_SELECT_LABEL;

		// Token: 0x0400389A RID: 14490
		private string TEXT_DATE_TEXT_LABEL;

		// Token: 0x0400389B RID: 14491
		private string TEXT_HOLDING_TEXT_LABEL;

		// Token: 0x0400389C RID: 14492
		private string TEXT_DESC_LABEL;

		// Token: 0x0400389D RID: 14493
		private string TEXT_RESULT_LABEL;

		// Token: 0x0400389E RID: 14494
		private string IMG_RESULT_BG_LABEL;

		// Token: 0x0400389F RID: 14495
		private string TEAM_NAME_LABEL;

		// Token: 0x040038A0 RID: 14496
		private string TEAM_GROUP_LABEL;

		// Token: 0x040038A1 RID: 14497
		private string CW_TEAM_NAME;

		// Token: 0x040038A2 RID: 14498
		private string CW_TEAM_AREA;

		// Token: 0x040038A3 RID: 14499
		private string CW_TEAM_ORDER;

		// Token: 0x040038A4 RID: 14500
		private int m_SelectedTeamIndex;

		// Token: 0x040038A5 RID: 14501
		private Dictionary<int, ElementObjectManager> templatesMap;

		// Token: 0x040038A6 RID: 14502
		private List<WinPredictionViewController.TeamData> teamDatas;

		// Token: 0x040038A7 RID: 14503
		private List<int> orderList;

		// Token: 0x040038A8 RID: 14504
		private Dictionary<string, object> teamDicData;

		// Token: 0x040038A9 RID: 14505
		private SelectionButton rewardButton;

		// Token: 0x040038AA RID: 14506
		private SelectionButton teamSelectButton;

		// Token: 0x040038AB RID: 14507
		private SelectionButton teamSelectUnselectButton;

		// Token: 0x040038AC RID: 14508
		private ElementObjectManager teamSelectGroup;

		// Token: 0x040038AD RID: 14509
		private ExtendedTextMeshProUGUI resultTextEo;

		// Token: 0x040038AE RID: 14510
		private GameObject resultBg;

		// Token: 0x040038AF RID: 14511
		private ExtendedTextMeshProUGUI m_TeamSelectAreaText;

		// Token: 0x040038B0 RID: 14512
		private ExtendedTextMeshProUGUI m_TeamSelectNameText;

		// Token: 0x040038B1 RID: 14513
		private ExtendedTextMeshProUGUI m_TeamSelectText;

		// Token: 0x040038B2 RID: 14514
		private Image m_SelectTeamIcon;

		// Token: 0x040038B3 RID: 14515
		private GameObject m_CheerIcon;

		// Token: 0x040038B4 RID: 14516
		private GameObject m_CheerIconResult;

		// Token: 0x040038B5 RID: 14517
		private Queue<Action> actionsQueue;

		// Token: 0x040038B6 RID: 14518
		private bool OpenningFrag;

		// Token: 0x040038B7 RID: 14519
		private bool isOutOfVote;

		// Token: 0x040038B8 RID: 14520
		private bool isDecidedTeamResult;

		// Token: 0x040038B9 RID: 14521
		private IEnumerator rewardCoroutine;

		// Token: 0x040038BA RID: 14522
		private Dictionary<string, object> supportDictionary;

		// Token: 0x040038BB RID: 14523
		private string infoText;

		// Token: 0x040038BC RID: 14524
		private WinPredictionViewController.isPresentSentFrag m_InitFrag;

		// Token: 0x02000802 RID: 2050
		public enum WinPredictionHomeCode
		{
			// Token: 0x040038BE RID: 14526
			NONE,
			// Token: 0x040038BF RID: 14527
			ERR_OUT_OF_TERM = 4301
		}

		// Token: 0x02000803 RID: 2051
		public enum BgType
		{
			// Token: 0x040038C1 RID: 14529
			NONE,
			// Token: 0x040038C2 RID: 14530
			ADVANCED,
			// Token: 0x040038C3 RID: 14531
			BEST8,
			// Token: 0x040038C4 RID: 14532
			CHAMPION
		}

		// Token: 0x02000804 RID: 2052
		public class TeamData
		{
			// Token: 0x040038C5 RID: 14533
			public int order;

			// Token: 0x040038C6 RID: 14534
			public int Id;

			// Token: 0x040038C7 RID: 14535
			public string name;

			// Token: 0x040038C8 RID: 14536
			public string areaName;

			// Token: 0x040038C9 RID: 14537
			public int areaId;
		}

		// Token: 0x02000805 RID: 2053
		public enum isPresentSentFrag
		{
			// Token: 0x040038CB RID: 14539
			NONE,
			// Token: 0x040038CC RID: 14540
			NORMAL,
			// Token: 0x040038CD RID: 14541
			SPECIAL,
			// Token: 0x040038CE RID: 14542
			OTHERS = 4
		}
	}
}
