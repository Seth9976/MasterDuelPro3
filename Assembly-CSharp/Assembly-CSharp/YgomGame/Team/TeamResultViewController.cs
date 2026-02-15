using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YgomGame.Duel;
using YgomGame.Menu;
using YgomSystem.ElementSystem;
using YgomSystem.Network;
using YgomSystem.UI;

namespace YgomGame.Team
{
	// Token: 0x020008D2 RID: 2258
	public class TeamResultViewController : BaseMenuViewController
	{
		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06004213 RID: 16915 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06004214 RID: 16916 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool isCallingApi
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06004215 RID: 16917 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool existDialog
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004216 RID: 16918 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(ViewControllerManager manager)
		{
		}

		// Token: 0x06004217 RID: 16919 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStack(ViewControllerManager vcm, ViewController vc, bool isEntry)
		{
		}

		// Token: 0x06004218 RID: 16920 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004219 RID: 16921 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x0600421A RID: 16922 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnFocusChanged(bool setfocus)
		{
		}

		// Token: 0x0600421B RID: 16923 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x0600421C RID: 16924 RVA: 0x0000216D File Offset: 0x0000036D
		private void Init()
		{
		}

		// Token: 0x0600421D RID: 16925 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x0600421E RID: 16926 RVA: 0x0000216D File Offset: 0x0000036D
		internal void SetBGCard(int mrk, string label, bool off = false)
		{
		}

		// Token: 0x0600421F RID: 16927 RVA: 0x0000216D File Offset: 0x0000036D
		protected void OnClickExitButton()
		{
		}

		// Token: 0x06004220 RID: 16928 RVA: 0x0000216D File Offset: 0x0000036D
		protected void CreateTableTemplates()
		{
		}

		// Token: 0x06004221 RID: 16929 RVA: 0x0000216A File Offset: 0x0000036A
		protected GameObject CreateTableTemplate(GameObject template, UnityAction onClick = null)
		{
			return null;
		}

		// Token: 0x06004222 RID: 16930 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetTeamInfo()
		{
		}

		// Token: 0x06004223 RID: 16931 RVA: 0x0000216D File Offset: 0x0000036D
		internal void SetTeamTable(bool init = false)
		{
		}

		// Token: 0x06004224 RID: 16932 RVA: 0x0000216D File Offset: 0x0000036D
		internal void UpdateTeamResultView()
		{
		}

		// Token: 0x06004225 RID: 16933 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickRecreateButton()
		{
		}

		// Token: 0x06004226 RID: 16934 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool OnClickActionWithCheck(UnityAction onFinish)
		{
			return false;
		}

		// Token: 0x06004227 RID: 16935 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool OnClickActionWithCheckRecreate(UnityAction onFinish)
		{
			return false;
		}

		// Token: 0x06004228 RID: 16936 RVA: 0x0000216D File Offset: 0x0000036D
		internal void UpdateTabel()
		{
		}

		// Token: 0x06004229 RID: 16937 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickReplayButton(TeamResultViewController.ResultTableData data)
		{
		}

		// Token: 0x0600422A RID: 16938 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickProfileButton(TeamResultViewController.MemberData data)
		{
		}

		// Token: 0x0600422B RID: 16939 RVA: 0x0000216D File Offset: 0x0000036D
		private void setActiveFooter(bool active)
		{
		}

		// Token: 0x0600422C RID: 16940 RVA: 0x0000216D File Offset: 0x0000036D
		private void ShowTableResultText(ElementObjectManager eom, TeamResultViewController.ResultStatus status)
		{
		}

		// Token: 0x0600422D RID: 16941 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetComment(ElementObjectManager playerEom, TeamResultViewController.MemberData member)
		{
		}

		// Token: 0x0600422E RID: 16942 RVA: 0x0000216D File Offset: 0x0000036D
		private void ForceSetComment(ElementObjectManager playerEom, bool isSetShow)
		{
		}

		// Token: 0x0600422F RID: 16943 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator PlayResultEffect(Action onFinished = null)
		{
			return null;
		}

		// Token: 0x06004230 RID: 16944 RVA: 0x0000216A File Offset: 0x0000036A
		private Dictionary<string, object> SetProfCardArgs(TeamResultViewController.MemberData data)
		{
			return null;
		}

		// Token: 0x06004231 RID: 16945 RVA: 0x0000216A File Offset: 0x0000036A
		private static Dictionary<string, object> GetTeamResultInfo()
		{
			return null;
		}

		// Token: 0x06004232 RID: 16946 RVA: 0x0000216A File Offset: 0x0000036A
		private static List<object> GetTeamResultTableInfoList()
		{
			return null;
		}

		// Token: 0x06004233 RID: 16947 RVA: 0x0000216A File Offset: 0x0000036A
		private static List<object> GetTeamResultTableResultList()
		{
			return null;
		}

		// Token: 0x06004234 RID: 16948 RVA: 0x0000216A File Offset: 0x0000036A
		private static Dictionary<string, object> GetTeamResultDuelResult()
		{
			return null;
		}

		// Token: 0x06004235 RID: 16949 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetTeamComment(long pcode)
		{
			return 0;
		}

		// Token: 0x06004236 RID: 16950 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int GetTeamID()
		{
			return 0;
		}

		// Token: 0x06004237 RID: 16951 RVA: 0x0000216D File Offset: 0x0000036D
		protected void AddCallingCount()
		{
		}

		// Token: 0x06004238 RID: 16952 RVA: 0x0000216D File Offset: 0x0000036D
		protected void DecCallingCount()
		{
		}

		// Token: 0x06004239 RID: 16953 RVA: 0x0000216D File Offset: 0x0000036D
		internal void CallAPITeamResultTablePoling(Action onFinish = null)
		{
		}

		// Token: 0x0600423A RID: 16954 RVA: 0x0000216D File Offset: 0x0000036D
		internal virtual void CallAPIRoomSetUserComment(int commentID)
		{
		}

		// Token: 0x0600423B RID: 16955 RVA: 0x0000216D File Offset: 0x0000036D
		internal void OnErrorCallAPI(TeamCode teamCode)
		{
		}

		// Token: 0x0600423C RID: 16956 RVA: 0x0000216D File Offset: 0x0000036D
		internal void CallAPITeamCreate(Action onSuccess)
		{
		}

		// Token: 0x0600423D RID: 16957 RVA: 0x0000216D File Offset: 0x0000036D
		internal void CallAPITeamEntryNewTeam(int teamId, Action onSuccess)
		{
		}

		// Token: 0x0600423E RID: 16958 RVA: 0x0000216D File Offset: 0x0000036D
		protected void CallAPIPvPWatchDuel(long pcode)
		{
		}

		// Token: 0x0600423F RID: 16959 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CallAPIPlayReplay(Util.GameMode gameMode, long did, int idx = 0, int eid = 0, Action<PvPCode> onFailed = null)
		{
		}

		// Token: 0x06004240 RID: 16960 RVA: 0x0000216D File Offset: 0x0000036D
		internal void CallAPITeamExitRoom()
		{
		}

		// Token: 0x0400805D RID: 32861
		private static readonly string viewControllerPath;

		// Token: 0x0400805E RID: 32862
		protected readonly string OBJ_COMMENT_LABEL;

		// Token: 0x0400805F RID: 32863
		protected readonly string OBJ_TABLE_TMP_LABEL;

		// Token: 0x04008060 RID: 32864
		protected readonly string TXT_COMMENT_LABEL;

		// Token: 0x04008061 RID: 32865
		protected readonly string SCROLL_LABEL;

		// Token: 0x04008062 RID: 32866
		protected readonly string IMG_LEFT_BG_CARD_LABEL;

		// Token: 0x04008063 RID: 32867
		protected readonly string IMG_RIGHT_BG_CARD_LABEL;

		// Token: 0x04008064 RID: 32868
		protected readonly string TXT_TMP_TITLE_LABEL;

		// Token: 0x04008065 RID: 32869
		protected readonly string OBJ_RESULTLIST_LABEL;

		// Token: 0x04008066 RID: 32870
		protected readonly string OBJ_WLD_AREA_LABEL;

		// Token: 0x04008067 RID: 32871
		protected readonly string TEXT_WIN_LABEL;

		// Token: 0x04008068 RID: 32872
		protected readonly string TEXT_LOSE_LABEL;

		// Token: 0x04008069 RID: 32873
		protected readonly string TEXT_DRAW_LABEL;

		// Token: 0x0400806A RID: 32874
		protected readonly string OBJ_PLAYER_LEFT_LABEL;

		// Token: 0x0400806B RID: 32875
		protected readonly string OBJ_PLAYER_RIGHT_LABEL;

		// Token: 0x0400806C RID: 32876
		protected readonly string OBJ_TEAM_LEFT_CROWN_LABEL;

		// Token: 0x0400806D RID: 32877
		protected readonly string OBJ_TEAM_RIGHT_CROWN_LABEL;

		// Token: 0x0400806E RID: 32878
		protected readonly string OBJ_TEAM_LEFT_SCORE_LABEL;

		// Token: 0x0400806F RID: 32879
		protected readonly string OBJ_TEAM_RIGHT_SCORE_LABEL;

		// Token: 0x04008070 RID: 32880
		protected readonly string TXT_TEAM_NAME_LEFT_LABEL;

		// Token: 0x04008071 RID: 32881
		protected readonly string TXT_TEAM_NAME_RIGHT_LABEL;

		// Token: 0x04008072 RID: 32882
		protected readonly string BTN_EXIT_LABEL;

		// Token: 0x04008073 RID: 32883
		protected readonly string BTN_REORGANIZE_LABEL;

		// Token: 0x04008074 RID: 32884
		protected readonly string BTN_COMMENT_LEFT_LABEL;

		// Token: 0x04008075 RID: 32885
		protected readonly string BTN_REPLAY_LABEL;

		// Token: 0x04008076 RID: 32886
		protected readonly string TXT_ROOM_MEMBER_LABEL;

		// Token: 0x04008077 RID: 32887
		protected readonly string BTN_PROFILE_LABEL;

		// Token: 0x04008078 RID: 32888
		protected readonly string IMG_ICON_LABEL;

		// Token: 0x04008079 RID: 32889
		protected readonly string PLATFORM_NAME_LABEL;

		// Token: 0x0400807A RID: 32890
		protected readonly string PLATFORM_ICON_LABEL;

		// Token: 0x0400807B RID: 32891
		protected long myPcode;

		// Token: 0x0400807C RID: 32892
		private int leftScore;

		// Token: 0x0400807D RID: 32893
		private int rightScore;

		// Token: 0x0400807E RID: 32894
		private bool isFinishedResultEffect;

		// Token: 0x0400807F RID: 32895
		private int callingApiCount;

		// Token: 0x04008080 RID: 32896
		private string[] tableComments;

		// Token: 0x04008081 RID: 32897
		internal Dictionary<int, string> regulationList;

		// Token: 0x04008082 RID: 32898
		internal List<TeamResultViewController.ResultTableData> resultTableDataList;

		// Token: 0x04008083 RID: 32899
		internal TeamResultViewController.TeamResultRoomInfo teamResultRoomInfo;

		// Token: 0x04008084 RID: 32900
		private bool isReorgaButtonActive;

		// Token: 0x04008085 RID: 32901
		private TeamResultViewController.ResultStatus teamResultStatus;

		// Token: 0x04008086 RID: 32902
		private Dictionary<int, GameObject> _tableTemplates;

		// Token: 0x04008087 RID: 32903
		private float pastSec;

		// Token: 0x04008088 RID: 32904
		private int myid;

		// Token: 0x04008089 RID: 32905
		private bool isResultError;

		// Token: 0x0400808A RID: 32906
		private GameObject profileParent;

		// Token: 0x020008D3 RID: 2259
		internal enum ResultTableStatus
		{
			// Token: 0x0400808C RID: 32908
			REPLAY = 1,
			// Token: 0x0400808D RID: 32909
			SPECTATE
		}

		// Token: 0x020008D4 RID: 2260
		internal enum ResultStatus
		{
			// Token: 0x0400808F RID: 32911
			WAIT,
			// Token: 0x04008090 RID: 32912
			WIN,
			// Token: 0x04008091 RID: 32913
			LOSE,
			// Token: 0x04008092 RID: 32914
			DRAW
		}

		// Token: 0x020008D5 RID: 2261
		internal class ResultTableData
		{
			// Token: 0x06004242 RID: 16962 RVA: 0x00002739 File Offset: 0x00000939
			public ResultTableData(TeamResultViewController.MemberData[] members, int index, int regulationID, string regulation, TeamResultViewController.ResultStatus myResult, TeamResultViewController.ResultStatus enemyResult, long did, int progress)
			{
			}

			// Token: 0x04008093 RID: 32915
			internal TeamResultViewController.MemberData[] members;

			// Token: 0x04008094 RID: 32916
			internal string regulation;

			// Token: 0x04008095 RID: 32917
			internal TeamResultViewController.ResultStatus myResult;

			// Token: 0x04008096 RID: 32918
			internal TeamResultViewController.ResultStatus enemyResult;

			// Token: 0x04008097 RID: 32919
			internal int regulationID;

			// Token: 0x04008098 RID: 32920
			internal int index;

			// Token: 0x04008099 RID: 32921
			internal long did;

			// Token: 0x0400809A RID: 32922
			internal int progress;
		}

		// Token: 0x020008D6 RID: 2262
		internal class MemberData
		{
			// Token: 0x0400809B RID: 32923
			internal int pcode;

			// Token: 0x0400809C RID: 32924
			internal string name;

			// Token: 0x0400809D RID: 32925
			internal int iconID;

			// Token: 0x0400809E RID: 32926
			internal int iconFrameID;

			// Token: 0x0400809F RID: 32927
			internal int commentID;

			// Token: 0x040080A0 RID: 32928
			internal bool isResistedPlatform;

			// Token: 0x040080A1 RID: 32929
			internal bool isSamePlatform;

			// Token: 0x040080A2 RID: 32930
			internal string platformName;

			// Token: 0x040080A3 RID: 32931
			internal int platformID;

			// Token: 0x040080A4 RID: 32932
			internal int follow_num;

			// Token: 0x040080A5 RID: 32933
			internal int follower_num;

			// Token: 0x040080A6 RID: 32934
			internal int level;

			// Token: 0x040080A7 RID: 32935
			internal int rank;

			// Token: 0x040080A8 RID: 32936
			internal int rate;

			// Token: 0x040080A9 RID: 32937
			internal int exp;

			// Token: 0x040080AA RID: 32938
			internal int need_exp;

			// Token: 0x040080AB RID: 32939
			internal int wallpaper;

			// Token: 0x040080AC RID: 32940
			internal ulong xuid;

			// Token: 0x040080AD RID: 32941
			internal int avater_id;

			// Token: 0x040080AE RID: 32942
			internal int edit;

			// Token: 0x040080AF RID: 32943
			internal List<object> tag;

			// Token: 0x040080B0 RID: 32944
			internal int official;

			// Token: 0x040080B1 RID: 32945
			internal string onlineID;
		}

		// Token: 0x020008D7 RID: 2263
		internal class TeamResultRoomInfo
		{
			// Token: 0x040080B2 RID: 32946
			internal int teamID;

			// Token: 0x040080B3 RID: 32947
			internal int myCardMrk;

			// Token: 0x040080B4 RID: 32948
			internal int enemyCardMrk;

			// Token: 0x040080B5 RID: 32949
			internal int memberNum;

			// Token: 0x040080B6 RID: 32950
			internal long myTeamMasterID;

			// Token: 0x040080B7 RID: 32951
			internal long enemyTeamMasterID;

			// Token: 0x040080B8 RID: 32952
			internal int nextTeamID;
		}
	}
}
