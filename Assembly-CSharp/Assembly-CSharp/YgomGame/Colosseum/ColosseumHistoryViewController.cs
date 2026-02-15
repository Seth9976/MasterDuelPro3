using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Duel;
using YgomGame.Menu;
using YgomSystem.ElementSystem;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.Colosseum
{
	// Token: 0x0200102B RID: 4139
	public class ColosseumHistoryViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x17000FBE RID: 4030
		// (get) Token: 0x06007C44 RID: 31812 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007C45 RID: 31813 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06007C46 RID: 31814 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x0400B420 RID: 46112
		private readonly string SCROLL_REPLAY_LABEL;

		// Token: 0x0400B421 RID: 46113
		private readonly string TMP_RANK_LABEL;

		// Token: 0x0400B422 RID: 46114
		private ColosseumUtil.PlayMode mode;

		// Token: 0x0400B423 RID: 46115
		private ColosseumHistoryViewController.ModeBehaviour modeBehaviour;

		// Token: 0x0200102C RID: 4140
		internal abstract class ModeBehaviour
		{
			// Token: 0x17000FBF RID: 4031
			// (get) Token: 0x06007C48 RID: 31816 RVA: 0x0000216A File Offset: 0x0000036A
			protected virtual string CWKEY_MYID
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000FC0 RID: 4032
			// (get) Token: 0x06007C49 RID: 31817 RVA: 0x0000216A File Offset: 0x0000036A
			protected virtual string CWKEY_DID
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000FC1 RID: 4033
			// (get) Token: 0x06007C4A RID: 31818 RVA: 0x0000216A File Offset: 0x0000036A
			protected virtual string CWKEY_ICON
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000FC2 RID: 4034
			// (get) Token: 0x06007C4B RID: 31819 RVA: 0x0000216A File Offset: 0x0000036A
			protected virtual string CWKEY_ICONFRAME
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000FC3 RID: 4035
			// (get) Token: 0x06007C4C RID: 31820 RVA: 0x0000216A File Offset: 0x0000036A
			protected virtual string CWKEY_DATE
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000FC4 RID: 4036
			// (get) Token: 0x06007C4D RID: 31821 RVA: 0x0000216A File Offset: 0x0000036A
			protected virtual string CWKEY_PLAYER
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000FC5 RID: 4037
			// (get) Token: 0x06007C4E RID: 31822 RVA: 0x0000216A File Offset: 0x0000036A
			protected virtual string CWKEY_PCODE
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000FC6 RID: 4038
			// (get) Token: 0x06007C4F RID: 31823 RVA: 0x0000216A File Offset: 0x0000036A
			protected virtual string CWKEY_NAME
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000FC7 RID: 4039
			// (get) Token: 0x06007C50 RID: 31824 RVA: 0x0000216A File Offset: 0x0000036A
			protected virtual string CWKEY_RANK
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000FC8 RID: 4040
			// (get) Token: 0x06007C51 RID: 31825 RVA: 0x0000216A File Offset: 0x0000036A
			protected virtual string CWKEY_RATE
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000FC9 RID: 4041
			// (get) Token: 0x06007C52 RID: 31826 RVA: 0x0000216A File Offset: 0x0000036A
			protected virtual string CWKEY_RES
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000FCA RID: 4042
			// (get) Token: 0x06007C53 RID: 31827 RVA: 0x0000216A File Offset: 0x0000036A
			protected virtual string CWKEY_TURN
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000FCB RID: 4043
			// (get) Token: 0x06007C54 RID: 31828 RVA: 0x0000216A File Offset: 0x0000036A
			protected virtual string CWKEY_TIME
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000FCC RID: 4044
			// (get) Token: 0x06007C55 RID: 31829 RVA: 0x0000216A File Offset: 0x0000036A
			protected virtual string CWKEY_ONLINEID
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000FCD RID: 4045
			// (get) Token: 0x06007C56 RID: 31830 RVA: 0x0000216A File Offset: 0x0000036A
			protected virtual string CWKEY_ISSAMEOS
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000FCE RID: 4046
			// (get) Token: 0x06007C57 RID: 31831 RVA: 0x0000216A File Offset: 0x0000036A
			protected virtual string CWKEY_MODE
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000FCF RID: 4047
			// (get) Token: 0x06007C58 RID: 31832 RVA: 0x0000216A File Offset: 0x0000036A
			protected virtual string CWKEY_EVENTID
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000FD0 RID: 4048
			// (get) Token: 0x06007C59 RID: 31833 RVA: 0x000029CC File Offset: 0x00000BCC
			protected virtual int ADJUST_ISV_INDEX
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x06007C5A RID: 31834 RVA: 0x00002739 File Offset: 0x00000939
			protected ModeBehaviour(ColosseumHistoryViewController vc, InfinityScrollView isv, ElementObjectManager eom, int id, Util.GameMode gameMode)
			{
			}

			// Token: 0x06007C5B RID: 31835
			internal abstract void CallAPI();

			// Token: 0x06007C5C RID: 31836
			internal abstract string GetTitle();

			// Token: 0x06007C5D RID: 31837 RVA: 0x0000216D File Offset: 0x0000036D
			internal virtual void InitializeScroll()
			{
			}

			// Token: 0x06007C5E RID: 31838 RVA: 0x0000216D File Offset: 0x0000036D
			internal virtual void UpdateView(Dictionary<string, object> dictionary)
			{
			}

			// Token: 0x06007C5F RID: 31839 RVA: 0x000029CC File Offset: 0x00000BCC
			internal virtual bool CanStartUpdateEntity(GameObject go, int dataIndex)
			{
				return false;
			}

			// Token: 0x06007C60 RID: 31840 RVA: 0x0000216D File Offset: 0x0000036D
			protected virtual void BindRankIcon(ColosseumHistoryViewController.ModeBehaviour.Data data, ElementObjectManager entityEom)
			{
			}

			// Token: 0x06007C61 RID: 31841 RVA: 0x0000216D File Offset: 0x0000036D
			protected virtual void OnClickButton(ColosseumHistoryViewController.ModeBehaviour.Data data)
			{
			}

			// Token: 0x06007C62 RID: 31842 RVA: 0x0000216D File Offset: 0x0000036D
			protected void OpenActionSheetFull(ColosseumHistoryViewController.ModeBehaviour.Data data)
			{
			}

			// Token: 0x06007C63 RID: 31843 RVA: 0x0000216D File Offset: 0x0000036D
			protected void OpenActionSheetFree(ColosseumHistoryViewController.ModeBehaviour.Data data)
			{
			}

			// Token: 0x06007C64 RID: 31844 RVA: 0x0000216D File Offset: 0x0000036D
			protected void OpenActionSheetCup(ColosseumHistoryViewController.ModeBehaviour.Data data)
			{
			}

			// Token: 0x06007C65 RID: 31845 RVA: 0x0000216D File Offset: 0x0000036D
			internal void OnSuccessAPI()
			{
			}

			// Token: 0x06007C66 RID: 31846 RVA: 0x0000216D File Offset: 0x0000036D
			internal void OnUpdateEntity(GameObject go, int dataIndex)
			{
			}

			// Token: 0x06007C67 RID: 31847 RVA: 0x0000216D File Offset: 0x0000036D
			internal virtual void CallAPISaveReplay(long did, int eid = 0)
			{
			}

			// Token: 0x06007C68 RID: 31848 RVA: 0x0000216D File Offset: 0x0000036D
			protected void UpdateScrollData(Dictionary<string, object> dictionary)
			{
			}

			// Token: 0x06007C69 RID: 31849 RVA: 0x0000216A File Offset: 0x0000036A
			protected ColosseumHistoryViewController.ModeBehaviour.Data SetData(KeyValuePair<string, object> kvp, Dictionary<string, object> dic)
			{
				return null;
			}

			// Token: 0x06007C6A RID: 31850 RVA: 0x000029CC File Offset: 0x00000BCC
			protected bool CanPlayReplay(long id, long time)
			{
				return false;
			}

			// Token: 0x06007C6B RID: 31851 RVA: 0x0000216D File Offset: 0x0000036D
			protected void OpenReportDialog(long opponentId)
			{
			}

			// Token: 0x06007C6C RID: 31852 RVA: 0x0000216D File Offset: 0x0000036D
			protected void CallAPIPvPGetHistoryDeck(long did, int mode, int exid)
			{
			}

			// Token: 0x0400B424 RID: 46116
			protected readonly string IMG_RANK_LABEL;

			// Token: 0x0400B425 RID: 46117
			protected readonly string TMP_BTN_LABEL;

			// Token: 0x0400B426 RID: 46118
			protected readonly string TMP_IMG_ICON_LABEL;

			// Token: 0x0400B427 RID: 46119
			protected readonly string TMP_TXT_DATE_LABEL;

			// Token: 0x0400B428 RID: 46120
			protected readonly string TMP_TXT_OPPONENT_LABEL;

			// Token: 0x0400B429 RID: 46121
			protected readonly string TMP_TXT_RESULT_LABEL;

			// Token: 0x0400B42A RID: 46122
			protected readonly string TMP_TXT_TURN_LABEL;

			// Token: 0x0400B42B RID: 46123
			protected readonly string TMP_TXT_TITLE_LABEL;

			// Token: 0x0400B42C RID: 46124
			protected readonly string TMP_TITLE_TEXT_LABEL;

			// Token: 0x0400B42D RID: 46125
			protected readonly string TXT_TITLE_LABEL;

			// Token: 0x0400B42E RID: 46126
			protected readonly string TXT_NOTBOOKMARK_LABEL;

			// Token: 0x0400B42F RID: 46127
			protected readonly string PLATFORM_NAME_LABEL;

			// Token: 0x0400B430 RID: 46128
			protected readonly string PLATFORM_ICON_LABEL;

			// Token: 0x0400B431 RID: 46129
			protected readonly ColosseumHistoryViewController vc;

			// Token: 0x0400B432 RID: 46130
			protected readonly InfinityScrollView isv;

			// Token: 0x0400B433 RID: 46131
			protected readonly ElementObjectManager eom;

			// Token: 0x0400B434 RID: 46132
			protected readonly int id;

			// Token: 0x0400B435 RID: 46133
			protected readonly Util.GameMode duelUtilGameMode;

			// Token: 0x0400B436 RID: 46134
			protected List<ColosseumHistoryViewController.ModeBehaviour.Data> dataList;

			// Token: 0x0400B437 RID: 46135
			protected long limitTs;

			// Token: 0x0200102D RID: 4141
			protected class Data
			{
				// Token: 0x06007C6D RID: 31853 RVA: 0x00002739 File Offset: 0x00000939
				public Data(int idx, long did, int mode, long pcode, int iconID, int frameID, int rank, int tier, string date, string name, Engine.ResultType result, int turn, int eventId, long time, bool isResistedPlatform = false, bool isSamePlatform = false, string platformName = "")
				{
				}

				// Token: 0x0400B438 RID: 46136
				public int idx;

				// Token: 0x0400B439 RID: 46137
				public long did;

				// Token: 0x0400B43A RID: 46138
				public int mode;

				// Token: 0x0400B43B RID: 46139
				public long pcode;

				// Token: 0x0400B43C RID: 46140
				public int iconID;

				// Token: 0x0400B43D RID: 46141
				public int frameID;

				// Token: 0x0400B43E RID: 46142
				public int rank;

				// Token: 0x0400B43F RID: 46143
				public int tier;

				// Token: 0x0400B440 RID: 46144
				public string date;

				// Token: 0x0400B441 RID: 46145
				public string name;

				// Token: 0x0400B442 RID: 46146
				public Engine.ResultType result;

				// Token: 0x0400B443 RID: 46147
				public int turn;

				// Token: 0x0400B444 RID: 46148
				public int eventId;

				// Token: 0x0400B445 RID: 46149
				public long time;

				// Token: 0x0400B446 RID: 46150
				public bool isResistedPlatform;

				// Token: 0x0400B447 RID: 46151
				public bool isSamePlatform;

				// Token: 0x0400B448 RID: 46152
				public string platformName;
			}
		}

		// Token: 0x0200102E RID: 4142
		internal class StandardBehaviour : ColosseumHistoryViewController.ModeBehaviour
		{
			// Token: 0x17000FD1 RID: 4049
			// (get) Token: 0x06007C6E RID: 31854 RVA: 0x0000216A File Offset: 0x0000036A
			protected override string CWKEY_EVENTID
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000FD2 RID: 4050
			// (get) Token: 0x06007C6F RID: 31855 RVA: 0x000029CC File Offset: 0x00000BCC
			protected override int ADJUST_ISV_INDEX
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x06007C70 RID: 31856 RVA: 0x000F6792 File Offset: 0x000F4992
			public StandardBehaviour(ColosseumHistoryViewController vc, InfinityScrollView isv, ElementObjectManager eom, int id, Util.GameMode gameMode)
				: base(null, null, null, 0, Util.GameMode.Normal)
			{
			}

			// Token: 0x06007C71 RID: 31857 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void CallAPI()
			{
			}

			// Token: 0x06007C72 RID: 31858 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void InitializeScroll()
			{
			}

			// Token: 0x06007C73 RID: 31859 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void UpdateView(Dictionary<string, object> dictionary)
			{
			}

			// Token: 0x06007C74 RID: 31860 RVA: 0x000029CC File Offset: 0x00000BCC
			internal override bool CanStartUpdateEntity(GameObject go, int dataIndex)
			{
				return false;
			}

			// Token: 0x06007C75 RID: 31861 RVA: 0x0000216A File Offset: 0x0000036A
			internal override string GetTitle()
			{
				return null;
			}

			// Token: 0x0400B449 RID: 46153
			protected readonly string IMG_OPEN_LABEL;

			// Token: 0x0400B44A RID: 46154
			private const int RECENT_RANK_DATA_INDEX = 0;

			// Token: 0x0400B44B RID: 46155
			private const int RECENT_RANK_TEMPLATE_INDEX = 1;

			// Token: 0x0400B44C RID: 46156
			private const int RANK_HISTORY_NUM = 6;

			// Token: 0x0400B44D RID: 46157
			private ColosseumHistoryViewController.StandardBehaviour.DataRankHistory[] dataRankHistorys;

			// Token: 0x0200102F RID: 4143
			private class DataRankHistory
			{
				// Token: 0x06007C76 RID: 31862 RVA: 0x00002739 File Offset: 0x00000939
				public DataRankHistory(int rank, int tier)
				{
				}

				// Token: 0x0400B44E RID: 46158
				public readonly int rank;

				// Token: 0x0400B44F RID: 46159
				public readonly int tier;
			}
		}

		// Token: 0x02001030 RID: 4144
		internal class TournamentBehaviour : ColosseumHistoryViewController.ModeBehaviour
		{
			// Token: 0x17000FD3 RID: 4051
			// (get) Token: 0x06007C77 RID: 31863 RVA: 0x0000216A File Offset: 0x0000036A
			protected override string CWKEY_EVENTID
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06007C78 RID: 31864 RVA: 0x000F6792 File Offset: 0x000F4992
			public TournamentBehaviour(ColosseumHistoryViewController vc, InfinityScrollView isv, ElementObjectManager eom, int id, Util.GameMode gameMode)
				: base(null, null, null, 0, Util.GameMode.Normal)
			{
			}

			// Token: 0x06007C79 RID: 31865 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void CallAPI()
			{
			}

			// Token: 0x06007C7A RID: 31866 RVA: 0x0000216A File Offset: 0x0000036A
			internal override string GetTitle()
			{
				return null;
			}
		}

		// Token: 0x02001031 RID: 4145
		internal class ExhibitionBehaviour : ColosseumHistoryViewController.ModeBehaviour
		{
			// Token: 0x17000FD4 RID: 4052
			// (get) Token: 0x06007C7B RID: 31867 RVA: 0x0000216A File Offset: 0x0000036A
			protected override string CWKEY_EVENTID
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06007C7C RID: 31868 RVA: 0x000F6792 File Offset: 0x000F4992
			public ExhibitionBehaviour(ColosseumHistoryViewController vc, InfinityScrollView isv, ElementObjectManager eom, int id, Util.GameMode gameMode)
				: base(null, null, null, 0, Util.GameMode.Normal)
			{
			}

			// Token: 0x06007C7D RID: 31869 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void CallAPI()
			{
			}

			// Token: 0x06007C7E RID: 31870 RVA: 0x0000216A File Offset: 0x0000036A
			internal override string GetTitle()
			{
				return null;
			}
		}

		// Token: 0x02001032 RID: 4146
		internal class FreeBehaviour : ColosseumHistoryViewController.ModeBehaviour
		{
			// Token: 0x06007C7F RID: 31871 RVA: 0x000F6792 File Offset: 0x000F4992
			public FreeBehaviour(ColosseumHistoryViewController vc, InfinityScrollView isv, ElementObjectManager eom, int id, Util.GameMode gameMode)
				: base(null, null, null, 0, Util.GameMode.Normal)
			{
			}

			// Token: 0x06007C80 RID: 31872 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void CallAPI()
			{
			}

			// Token: 0x06007C81 RID: 31873 RVA: 0x0000216A File Offset: 0x0000036A
			internal override string GetTitle()
			{
				return null;
			}

			// Token: 0x06007C82 RID: 31874 RVA: 0x0000216D File Offset: 0x0000036D
			protected override void OnClickButton(ColosseumHistoryViewController.ModeBehaviour.Data data)
			{
			}

			// Token: 0x06007C83 RID: 31875 RVA: 0x0000216D File Offset: 0x0000036D
			protected override void BindRankIcon(ColosseumHistoryViewController.ModeBehaviour.Data data, ElementObjectManager entityEom)
			{
			}
		}

		// Token: 0x02001033 RID: 4147
		internal class DuelistCupBehaviour : ColosseumHistoryViewController.ModeBehaviour
		{
			// Token: 0x06007C84 RID: 31876 RVA: 0x000F6792 File Offset: 0x000F4992
			public DuelistCupBehaviour(ColosseumHistoryViewController vc, InfinityScrollView isv, ElementObjectManager eom, int id, Util.GameMode gameMode)
				: base(null, null, null, 0, Util.GameMode.Normal)
			{
			}

			// Token: 0x06007C85 RID: 31877 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void CallAPI()
			{
			}

			// Token: 0x06007C86 RID: 31878 RVA: 0x0000216A File Offset: 0x0000036A
			internal override string GetTitle()
			{
				return null;
			}

			// Token: 0x06007C87 RID: 31879 RVA: 0x0000216D File Offset: 0x0000036D
			protected override void OnClickButton(ColosseumHistoryViewController.ModeBehaviour.Data data)
			{
			}

			// Token: 0x06007C88 RID: 31880 RVA: 0x0000216D File Offset: 0x0000036D
			protected override void BindRankIcon(ColosseumHistoryViewController.ModeBehaviour.Data data, ElementObjectManager entityEom)
			{
			}
		}

		// Token: 0x02001034 RID: 4148
		internal class RankEventBehaviour : ColosseumHistoryViewController.ModeBehaviour
		{
			// Token: 0x17000FD5 RID: 4053
			// (get) Token: 0x06007C89 RID: 31881 RVA: 0x0000216A File Offset: 0x0000036A
			protected override string CWKEY_EVENTID
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000FD6 RID: 4054
			// (get) Token: 0x06007C8A RID: 31882 RVA: 0x0000216A File Offset: 0x0000036A
			protected override string CWKEY_RANK
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000FD7 RID: 4055
			// (get) Token: 0x06007C8B RID: 31883 RVA: 0x0000216A File Offset: 0x0000036A
			protected override string CWKEY_RATE
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06007C8C RID: 31884 RVA: 0x000F6792 File Offset: 0x000F4992
			public RankEventBehaviour(ColosseumHistoryViewController vc, InfinityScrollView isv, ElementObjectManager eom, int id, Util.GameMode gameMode)
				: base(null, null, null, 0, Util.GameMode.Normal)
			{
			}

			// Token: 0x06007C8D RID: 31885 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void CallAPI()
			{
			}

			// Token: 0x06007C8E RID: 31886 RVA: 0x0000216A File Offset: 0x0000036A
			internal override string GetTitle()
			{
				return null;
			}

			// Token: 0x06007C8F RID: 31887 RVA: 0x0000216D File Offset: 0x0000036D
			protected override void BindRankIcon(ColosseumHistoryViewController.ModeBehaviour.Data data, ElementObjectManager entityEom)
			{
			}
		}

		// Token: 0x02001035 RID: 4149
		internal class DuelTrialBehaviour : ColosseumHistoryViewController.ModeBehaviour
		{
			// Token: 0x17000FD8 RID: 4056
			// (get) Token: 0x06007C90 RID: 31888 RVA: 0x0000216A File Offset: 0x0000036A
			protected override string CWKEY_EVENTID
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06007C91 RID: 31889 RVA: 0x000F6792 File Offset: 0x000F4992
			public DuelTrialBehaviour(ColosseumHistoryViewController vc, InfinityScrollView isv, ElementObjectManager eom, int id, Util.GameMode gameMode)
				: base(null, null, null, 0, Util.GameMode.Normal)
			{
			}

			// Token: 0x06007C92 RID: 31890 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void CallAPI()
			{
			}

			// Token: 0x06007C93 RID: 31891 RVA: 0x0000216A File Offset: 0x0000036A
			internal override string GetTitle()
			{
				return null;
			}
		}

		// Token: 0x02001036 RID: 4150
		internal class WCSBehaviour : ColosseumHistoryViewController.ModeBehaviour
		{
			// Token: 0x06007C94 RID: 31892 RVA: 0x000F6792 File Offset: 0x000F4992
			public WCSBehaviour(ColosseumHistoryViewController vc, InfinityScrollView isv, ElementObjectManager eom, int id, Util.GameMode gameMode)
				: base(null, null, null, 0, Util.GameMode.Normal)
			{
			}

			// Token: 0x06007C95 RID: 31893 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void CallAPI()
			{
			}

			// Token: 0x06007C96 RID: 31894 RVA: 0x0000216A File Offset: 0x0000036A
			internal override string GetTitle()
			{
				return null;
			}

			// Token: 0x06007C97 RID: 31895 RVA: 0x0000216D File Offset: 0x0000036D
			protected override void OnClickButton(ColosseumHistoryViewController.ModeBehaviour.Data data)
			{
			}

			// Token: 0x06007C98 RID: 31896 RVA: 0x0000216D File Offset: 0x0000036D
			protected override void BindRankIcon(ColosseumHistoryViewController.ModeBehaviour.Data data, ElementObjectManager entityEom)
			{
			}
		}

		// Token: 0x02001037 RID: 4151
		internal class VersusBehaviour : ColosseumHistoryViewController.ModeBehaviour
		{
			// Token: 0x17000FD9 RID: 4057
			// (get) Token: 0x06007C99 RID: 31897 RVA: 0x0000216A File Offset: 0x0000036A
			protected override string CWKEY_EVENTID
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06007C9A RID: 31898 RVA: 0x000F6792 File Offset: 0x000F4992
			public VersusBehaviour(ColosseumHistoryViewController vc, InfinityScrollView isv, ElementObjectManager eom, int id, Util.GameMode gameMode)
				: base(null, null, null, 0, Util.GameMode.Normal)
			{
			}

			// Token: 0x06007C9B RID: 31899 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void CallAPI()
			{
			}

			// Token: 0x06007C9C RID: 31900 RVA: 0x0000216A File Offset: 0x0000036A
			internal override string GetTitle()
			{
				return null;
			}
		}
	}
}
