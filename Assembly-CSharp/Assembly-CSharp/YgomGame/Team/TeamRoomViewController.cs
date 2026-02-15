using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using YgomGame.Menu;
using YgomGame.Menu.Common;
using YgomSystem.ElementSystem;
using YgomSystem.Network;
using YgomSystem.UI;
using YgomSystem.YGomTMPro;

namespace YgomGame.Team
{
	// Token: 0x020008D9 RID: 2265
	public class TeamRoomViewController : BaseMenuViewController, TeamLobbyPollingWatcher.ICallback, IHeaderBorderSupported
	{
		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06004248 RID: 16968 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06004249 RID: 16969 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool existDialog
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600424A RID: 16970 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator waitPolling()
		{
			return null;
		}

		// Token: 0x0600424B RID: 16971 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStack(ViewControllerManager vcm, ViewController vc, bool isEntry)
		{
		}

		// Token: 0x0600424C RID: 16972 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x0600424D RID: 16973 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x0600424E RID: 16974 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnResult(ViewController from, object value)
		{
			return false;
		}

		// Token: 0x0600424F RID: 16975 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06004250 RID: 16976 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnPollingResponse(Handle handle)
		{
		}

		// Token: 0x06004251 RID: 16977 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnApplyingStatusChanged(TeamLobbyPollingWatcher.ApplyingBattleData data)
		{
		}

		// Token: 0x06004252 RID: 16978 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnAppliedFromOtherTeam(TeamLobbyPollingWatcher.AppliedBattleData data)
		{
		}

		// Token: 0x06004253 RID: 16979 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnOpponentTeamInfoUpdated(OpponentTeamInfo data)
		{
		}

		// Token: 0x040080B9 RID: 32953
		[SerializeField]
		private ElementObjectManager _deckOverview;

		// Token: 0x040080BA RID: 32954
		private TeamRoomViewController.TeamBehaviour teamBehaviour;

		// Token: 0x040080BB RID: 32955
		private bool isBackDuelClientError;

		// Token: 0x020008DA RID: 2266
		internal abstract class TeamBehaviour
		{
			// Token: 0x17000528 RID: 1320
			// (get) Token: 0x06004255 RID: 16981 RVA: 0x000029CC File Offset: 0x00000BCC
			internal bool isCallingApi
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06004256 RID: 16982 RVA: 0x00002739 File Offset: 0x00000939
			internal TeamBehaviour(ViewControllerManager manager, TeamRoomViewController vc, ElementObjectManager viewEom)
			{
			}

			// Token: 0x06004257 RID: 16983 RVA: 0x0000216D File Offset: 0x0000036D
			internal virtual void PlayDUELBtnTextChanging(string text)
			{
			}

			// Token: 0x06004258 RID: 16984 RVA: 0x0000216D File Offset: 0x0000036D
			internal virtual void StopDUELBtnTextChanging()
			{
			}

			// Token: 0x06004259 RID: 16985 RVA: 0x0000216D File Offset: 0x0000036D
			internal virtual void OnApplyingStatusChanged(TeamLobbyPollingWatcher.ApplyingBattleData data)
			{
			}

			// Token: 0x0600425A RID: 16986 RVA: 0x0000216D File Offset: 0x0000036D
			internal virtual void OnAppliedFromOtherTeam(TeamLobbyPollingWatcher.AppliedBattleData data)
			{
			}

			// Token: 0x0600425B RID: 16987 RVA: 0x0000216D File Offset: 0x0000036D
			internal virtual void OnOpponentTeamInfoUpdated(OpponentTeamInfo data)
			{
			}

			// Token: 0x0600425C RID: 16988 RVA: 0x0000216D File Offset: 0x0000036D
			internal virtual void Onterminal()
			{
			}

			// Token: 0x0600425D RID: 16989
			internal abstract void Initialize();

			// Token: 0x0600425E RID: 16990
			protected abstract void CreateMenuButtons(Action onFinished = null);

			// Token: 0x0600425F RID: 16991 RVA: 0x0000216D File Offset: 0x0000036D
			internal void OnRemove()
			{
			}

			// Token: 0x06004260 RID: 16992 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool OnClickActionWithCheckSitting(UnityAction onFinish)
			{
				return false;
			}

			// Token: 0x06004261 RID: 16993 RVA: 0x0000216D File Offset: 0x0000036D
			public void OnClickActionStartDuel(UnityAction onFinish)
			{
			}

			// Token: 0x06004262 RID: 16994 RVA: 0x0000216D File Offset: 0x0000036D
			public void OnClickExitButton()
			{
			}

			// Token: 0x06004263 RID: 16995 RVA: 0x0000216D File Offset: 0x0000036D
			public void ShowCompleteEffect()
			{
			}

			// Token: 0x06004264 RID: 16996 RVA: 0x0000216A File Offset: 0x0000036A
			protected SelectionButton CreateMenuButton(Selector selector, GameObject template, string label, UnityAction onClick = null, bool isDefaultItem = false, bool isCheck = false)
			{
				return null;
			}

			// Token: 0x06004265 RID: 16997 RVA: 0x0000216D File Offset: 0x0000036D
			protected void ChangeButtonCallback(SelectionButton button, UnityAction newAction, bool isSittingCheck = false)
			{
			}

			// Token: 0x06004266 RID: 16998 RVA: 0x0000216D File Offset: 0x0000036D
			internal void SetCallbackInputLeft(SelectionButton btn)
			{
			}

			// Token: 0x06004267 RID: 16999 RVA: 0x0000216A File Offset: 0x0000036A
			protected GameObject CreateTableTemplate(GameObject template)
			{
				return null;
			}

			// Token: 0x06004268 RID: 17000 RVA: 0x0000216D File Offset: 0x0000036D
			internal virtual void SetTeamRoomInfo()
			{
			}

			// Token: 0x06004269 RID: 17001 RVA: 0x0000216D File Offset: 0x0000036D
			internal void SetBGCard(int mrk, bool active = true)
			{
			}

			// Token: 0x0600426A RID: 17002 RVA: 0x0000216D File Offset: 0x0000036D
			internal virtual void UpdateRoom()
			{
			}

			// Token: 0x0600426B RID: 17003 RVA: 0x0000216D File Offset: 0x0000036D
			internal virtual void UpdateTable()
			{
			}

			// Token: 0x0600426C RID: 17004 RVA: 0x0000216D File Offset: 0x0000036D
			internal virtual void SetTeamTable(bool isUpdateDataCount = false)
			{
			}

			// Token: 0x0600426D RID: 17005 RVA: 0x0000216D File Offset: 0x0000036D
			internal void UpdateDeck()
			{
			}

			// Token: 0x0600426E RID: 17006 RVA: 0x000029CC File Offset: 0x00000BCC
			internal virtual bool SetDeck(int did)
			{
				return false;
			}

			// Token: 0x0600426F RID: 17007 RVA: 0x0000216D File Offset: 0x0000036D
			protected void LoadDuelDurationConfig()
			{
			}

			// Token: 0x06004270 RID: 17008 RVA: 0x0000216A File Offset: 0x0000036A
			public static Dictionary<string, object> GetTeamInfo()
			{
				return null;
			}

			// Token: 0x06004271 RID: 17009 RVA: 0x0000216A File Offset: 0x0000036A
			public static List<object> GetTeamTable()
			{
				return null;
			}

			// Token: 0x06004272 RID: 17010 RVA: 0x0000216A File Offset: 0x0000036A
			public static Dictionary<string, object> GetTeamMemberInfo(long pcode)
			{
				return null;
			}

			// Token: 0x06004273 RID: 17011 RVA: 0x000029CC File Offset: 0x00000BCC
			public static int GetTeamComment(long pcode)
			{
				return 0;
			}

			// Token: 0x06004274 RID: 17012 RVA: 0x0000216A File Offset: 0x0000036A
			public static Dictionary<string, object> GetTeamRegulation()
			{
				return null;
			}

			// Token: 0x06004275 RID: 17013 RVA: 0x0000216A File Offset: 0x0000036A
			public static Dictionary<string, object> GetDeckInfo()
			{
				return null;
			}

			// Token: 0x06004276 RID: 17014 RVA: 0x0000216D File Offset: 0x0000036D
			protected void AddCallingCount()
			{
			}

			// Token: 0x06004277 RID: 17015 RVA: 0x0000216D File Offset: 0x0000036D
			protected void DecCallingCount()
			{
			}

			// Token: 0x06004278 RID: 17016 RVA: 0x0000216D File Offset: 0x0000036D
			internal virtual void CallAPIDeckCheck(Action onFinish = null)
			{
			}

			// Token: 0x06004279 RID: 17017 RVA: 0x0000216D File Offset: 0x0000036D
			internal void CallAPITeamExitRoom()
			{
			}

			// Token: 0x0600427A RID: 17018 RVA: 0x0000216D File Offset: 0x0000036D
			internal void CallAPITeamRoomTablePoling(Action onFinish = null, bool init = false)
			{
			}

			// Token: 0x0600427B RID: 17019 RVA: 0x0000216D File Offset: 0x0000036D
			internal void OnTablePollingResponsed(Handle handle, Action onFinish, bool init)
			{
			}

			// Token: 0x0600427C RID: 17020 RVA: 0x0000216D File Offset: 0x0000036D
			internal virtual void CallAPITeamTableArrive(int tableNo)
			{
			}

			// Token: 0x0600427D RID: 17021 RVA: 0x0000216D File Offset: 0x0000036D
			internal virtual void CallAPITeamTableLeave(UnityAction onSuccess = null)
			{
			}

			// Token: 0x0600427E RID: 17022 RVA: 0x0000216D File Offset: 0x0000036D
			internal virtual void CallAPIRoomSetUserComment(int commentID)
			{
			}

			// Token: 0x0600427F RID: 17023 RVA: 0x0000216D File Offset: 0x0000036D
			internal void OnErrorCallAPI(TeamCode teamCode)
			{
			}

			// Token: 0x040080BC RID: 32956
			protected readonly string BTN_LABEL;

			// Token: 0x040080BD RID: 32957
			protected readonly string TXT_LABEL;

			// Token: 0x040080BE RID: 32958
			protected readonly string ROOT_MENU_LABEL;

			// Token: 0x040080BF RID: 32959
			protected readonly string TMP_BTN_MENU_LABEL;

			// Token: 0x040080C0 RID: 32960
			protected readonly string BTN_EXIT_LABEL;

			// Token: 0x040080C1 RID: 32961
			protected readonly string BTN_DECK_LABEL;

			// Token: 0x040080C2 RID: 32962
			protected readonly string BTN_DECK_READONLY_LABEL;

			// Token: 0x040080C3 RID: 32963
			protected readonly string CARD_AREA_LABEL;

			// Token: 0x040080C4 RID: 32964
			protected readonly string IMG_ICON_LABEL;

			// Token: 0x040080C5 RID: 32965
			protected readonly string PLATFORM_NAME_LABEL;

			// Token: 0x040080C6 RID: 32966
			protected readonly string PLATFORM_ICON_LABEL;

			// Token: 0x040080C7 RID: 32967
			protected readonly string TXT_TITLE_LABEL;

			// Token: 0x040080C8 RID: 32968
			protected readonly string TXT_ROOM_MEMBER_LABEL;

			// Token: 0x040080C9 RID: 32969
			protected readonly string TXT_TEAM_NAME_LABEL;

			// Token: 0x040080CA RID: 32970
			protected readonly string BTN_COMMENT_LEFT_LABEL;

			// Token: 0x040080CB RID: 32971
			protected readonly string BTN_ENTRY_LABEL;

			// Token: 0x040080CC RID: 32972
			protected readonly string TXT_ENTRY_BUTTON_STATUS_LABEL;

			// Token: 0x040080CD RID: 32973
			protected readonly string BTN_LEAVE_LABEL;

			// Token: 0x040080CE RID: 32974
			protected readonly string OBJ_COMMENT_LABEL;

			// Token: 0x040080CF RID: 32975
			protected readonly string TXT_COMMENT_LABEL;

			// Token: 0x040080D0 RID: 32976
			protected readonly string OBJ_CARDIMAGE_LABEL;

			// Token: 0x040080D1 RID: 32977
			protected readonly string TMP_SMALLBUTTON_LABEL;

			// Token: 0x040080D2 RID: 32978
			protected readonly string OBJ_SMALLBUTTON_GROUP_LABEL;

			// Token: 0x040080D3 RID: 32979
			protected readonly string BTN_REGULATION_LABEL;

			// Token: 0x040080D4 RID: 32980
			protected readonly string BTN_DECIDE_LABEL;

			// Token: 0x040080D5 RID: 32981
			protected readonly string BTN_DECIDE_DESIGNATION_LABEL;

			// Token: 0x040080D6 RID: 32982
			internal readonly ViewControllerManager manager;

			// Token: 0x040080D7 RID: 32983
			internal readonly TeamRoomViewController vc;

			// Token: 0x040080D8 RID: 32984
			internal readonly ElementObjectManager viewEom;

			// Token: 0x040080D9 RID: 32985
			protected long myPcode;

			// Token: 0x040080DA RID: 32986
			protected bool isSitting;

			// Token: 0x040080DB RID: 32987
			protected string beforeRoomName;

			// Token: 0x040080DC RID: 32988
			protected readonly string[] tableComments;

			// Token: 0x040080DD RID: 32989
			internal Dictionary<int, string> regulationList;

			// Token: 0x040080DE RID: 32990
			internal int mySelectRegulationId;

			// Token: 0x040080DF RID: 32991
			private int callingApiCount;

			// Token: 0x040080E0 RID: 32992
			internal int currentDeckId;

			// Token: 0x040080E1 RID: 32993
			internal TeamRoomViewController.TeamBehaviour.TeamRoomInfo teamRoomInfo;

			// Token: 0x040080E2 RID: 32994
			internal List<TeamRoomViewController.TeamBehaviour.TableData> tableDataList;

			// Token: 0x040080E3 RID: 32995
			protected DeckCaseWidget deckCase;

			// Token: 0x040080E4 RID: 32996
			protected StringBuilder deckNameBuf;

			// Token: 0x040080E5 RID: 32997
			internal int dataCount;

			// Token: 0x040080E6 RID: 32998
			internal int teamCardId;

			// Token: 0x040080E7 RID: 32999
			internal bool isRoomInfoExist;

			// Token: 0x040080E8 RID: 33000
			internal bool isStartTeamDuelMatching;

			// Token: 0x040080E9 RID: 33001
			internal bool isStartTeamMateMatching;

			// Token: 0x040080EA RID: 33002
			internal bool isOpenWatingWindow;

			// Token: 0x040080EB RID: 33003
			internal bool isTeamMemberRecruted;

			// Token: 0x040080EC RID: 33004
			internal bool isSearchedMember;

			// Token: 0x040080ED RID: 33005
			internal bool isClickedTeamMemberMatching;

			// Token: 0x040080EE RID: 33006
			protected bool _isLeader;

			// Token: 0x040080EF RID: 33007
			internal Dictionary<int, GameObject> _tableTemplates;

			// Token: 0x040080F0 RID: 33008
			protected internal TeamLobbyPollingWatcher watchDog;

			// Token: 0x040080F1 RID: 33009
			protected List<ValueTuple<int, string, int, int>> _duelDurationConfigItems;

			// Token: 0x020008DB RID: 2267
			internal class TableData
			{
				// Token: 0x17000529 RID: 1321
				// (get) Token: 0x06004280 RID: 17024 RVA: 0x000029CC File Offset: 0x00000BCC
				// (set) Token: 0x06004281 RID: 17025 RVA: 0x0000216D File Offset: 0x0000036D
				internal bool entry
				{
					[CompilerGenerated]
					get
					{
						return false;
					}
					[CompilerGenerated]
					set
					{
					}
				}

				// Token: 0x06004282 RID: 17026 RVA: 0x00002739 File Offset: 0x00000939
				public TableData(TeamRoomViewController.TeamBehaviour.MemberData member, int regulationID, string regulation, int index, bool entry)
				{
				}

				// Token: 0x040080F2 RID: 33010
				internal TeamRoomViewController.TeamBehaviour.MemberData member;

				// Token: 0x040080F3 RID: 33011
				internal string regulation;

				// Token: 0x040080F4 RID: 33012
				internal int regulationID;

				// Token: 0x040080F5 RID: 33013
				internal int index;
			}

			// Token: 0x020008DC RID: 2268
			internal class MemberData
			{
				// Token: 0x040080F6 RID: 33014
				internal long pcode;

				// Token: 0x040080F7 RID: 33015
				internal string name;

				// Token: 0x040080F8 RID: 33016
				internal int iconID;

				// Token: 0x040080F9 RID: 33017
				internal int iconFrameID;

				// Token: 0x040080FA RID: 33018
				internal int commentID;

				// Token: 0x040080FB RID: 33019
				internal bool isResistedPlatform;

				// Token: 0x040080FC RID: 33020
				internal bool isSamePlatform;

				// Token: 0x040080FD RID: 33021
				internal string platformName;

				// Token: 0x040080FE RID: 33022
				internal string regulation;

				// Token: 0x040080FF RID: 33023
				internal bool isMyAccount;
			}

			// Token: 0x020008DD RID: 2269
			internal class TeamRoomInfo
			{
				// Token: 0x04008100 RID: 33024
				internal int teamID;

				// Token: 0x04008101 RID: 33025
				internal string roomName;

				// Token: 0x04008102 RID: 33026
				internal long roomMasterID;

				// Token: 0x04008103 RID: 33027
				internal int memberNum;

				// Token: 0x04008104 RID: 33028
				internal int roomSpecterID;

				// Token: 0x04008105 RID: 33029
				internal int specterNum;

				// Token: 0x04008106 RID: 33030
				internal int memberMax;

				// Token: 0x04008107 RID: 33031
				internal int regID;

				// Token: 0x04008108 RID: 33032
				internal string regulation;

				// Token: 0x04008109 RID: 33033
				internal int roomComment;

				// Token: 0x0400810A RID: 33034
				internal int cardMrk;
			}
		}

		// Token: 0x020008DE RID: 2270
		internal class TeamBehaviourNormal : TeamRoomViewController.TeamBehaviour
		{
			// Token: 0x06004285 RID: 17029 RVA: 0x000F477A File Offset: 0x000F297A
			public TeamBehaviourNormal(ViewControllerManager manager, TeamRoomViewController vc, ElementObjectManager viewEom)
				: base(null, null, null)
			{
			}

			// Token: 0x06004286 RID: 17030 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void Initialize()
			{
			}

			// Token: 0x06004287 RID: 17031 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void SetTeamRoomInfo()
			{
			}

			// Token: 0x06004288 RID: 17032 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void OnApplyingStatusChanged(TeamLobbyPollingWatcher.ApplyingBattleData data)
			{
			}

			// Token: 0x06004289 RID: 17033 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void OnAppliedFromOtherTeam(TeamLobbyPollingWatcher.AppliedBattleData data)
			{
			}

			// Token: 0x0600428A RID: 17034 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void OnOpponentTeamInfoUpdated(OpponentTeamInfo data)
			{
			}

			// Token: 0x0600428B RID: 17035 RVA: 0x0000216D File Offset: 0x0000036D
			protected void CreateTableTemplates()
			{
			}

			// Token: 0x0600428C RID: 17036 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void SetTeamTable(bool isEntry = false)
			{
			}

			// Token: 0x0600428D RID: 17037 RVA: 0x0000216D File Offset: 0x0000036D
			protected override void CreateMenuButtons(Action onFinished = null)
			{
			}

			// Token: 0x0600428E RID: 17038 RVA: 0x0000216D File Offset: 0x0000036D
			private void setRegualtionIntList(Dictionary<string, object> value)
			{
			}

			// Token: 0x0600428F RID: 17039 RVA: 0x0000216D File Offset: 0x0000036D
			private void RestrictMenuButtons(bool on)
			{
			}

			// Token: 0x06004290 RID: 17040 RVA: 0x0000216D File Offset: 0x0000036D
			private void RestrictEachMenuButton(TeamRoomViewController.TeamBehaviourNormal.MenuBtn kind, SelectionButton button, bool on)
			{
			}

			// Token: 0x06004291 RID: 17041 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void UpdateTable()
			{
			}

			// Token: 0x06004292 RID: 17042 RVA: 0x0000216D File Offset: 0x0000036D
			private void SetComment(ElementObjectManager playerEom, TeamRoomViewController.TeamBehaviour.MemberData member)
			{
			}

			// Token: 0x06004293 RID: 17043 RVA: 0x0000216D File Offset: 0x0000036D
			private void ForceSetComment(ElementObjectManager playerEom, bool isSetShow)
			{
			}

			// Token: 0x06004294 RID: 17044 RVA: 0x0000216D File Offset: 0x0000036D
			private void SetDUELButtonPushable(bool pushable)
			{
			}

			// Token: 0x06004295 RID: 17045 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void PlayDUELBtnTextChanging(string text)
			{
			}

			// Token: 0x06004296 RID: 17046 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void StopDUELBtnTextChanging()
			{
			}

			// Token: 0x06004297 RID: 17047 RVA: 0x0000216D File Offset: 0x0000036D
			private void ChangeTeamIDDesignateButton(bool designatable)
			{
			}

			// Token: 0x06004298 RID: 17048 RVA: 0x0000216D File Offset: 0x0000036D
			private void OnClickDeck()
			{
			}

			// Token: 0x06004299 RID: 17049 RVA: 0x0000216D File Offset: 0x0000036D
			private void OnGoingToDesignation()
			{
			}

			// Token: 0x0600429A RID: 17050 RVA: 0x0000216D File Offset: 0x0000036D
			private void OnCancelingDesignation()
			{
			}

			// Token: 0x0400810B RID: 33035
			private Dictionary<int, SelectionButton> _menuButtonmap;

			// Token: 0x0400810C RID: 33036
			private SelectionButton _duelButton_random;

			// Token: 0x0400810D RID: 33037
			private SelectionButton _duelButton_designation;

			// Token: 0x0400810E RID: 33038
			private List<string> regulationStringList;

			// Token: 0x0400810F RID: 33039
			private int[] regulationIntList;

			// Token: 0x04008110 RID: 33040
			private bool NotAllMR;

			// Token: 0x04008111 RID: 33041
			private ExtendedTextMeshProUGUI _teamIdApplyText;

			// Token: 0x020008DF RID: 2271
			private enum MenuBtn
			{
				// Token: 0x04008113 RID: 33043
				NONE,
				// Token: 0x04008114 RID: 33044
				INFO,
				// Token: 0x04008115 RID: 33045
				MEMBER,
				// Token: 0x04008116 RID: 33046
				INVITE,
				// Token: 0x04008117 RID: 33047
				RECRUIT,
				// Token: 0x04008118 RID: 33048
				REGULATION,
				// Token: 0x04008119 RID: 33049
				DECK,
				// Token: 0x0400811A RID: 33050
				DECK_READONLY,
				// Token: 0x0400811B RID: 33051
				APPLY_TEAM_ID
			}
		}
	}
}
