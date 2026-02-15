using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YgomSystem.Network;
using YgomSystem.UI;

namespace YgomGame.Team
{
	// Token: 0x020008C0 RID: 2240
	[DisallowMultipleComponent]
	public class TeamLobbyPollingWatcher : MonoBehaviour
	{
		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06004182 RID: 16770 RVA: 0x000029CC File Offset: 0x00000BCC
		public TeamLobbyPollingWatcher.ApplyingStatusOnServer applyingStatus
		{
			get
			{
				return TeamLobbyPollingWatcher.ApplyingStatusOnServer.NONE;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06004183 RID: 16771 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool isErrorDlgShowing
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06004184 RID: 16772 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool shouldPolling
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06004185 RID: 16773 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool checkTerminate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06004186 RID: 16774 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isApplyingToOppTeam
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004187 RID: 16775 RVA: 0x0000216D File Offset: 0x0000036D
		public void StartWatching()
		{
		}

		// Token: 0x06004188 RID: 16776 RVA: 0x0000216D File Offset: 0x0000036D
		public void StopWatching()
		{
		}

		// Token: 0x06004189 RID: 16777 RVA: 0x0000216D File Offset: 0x0000036D
		public void Register(TeamLobbyPollingWatcher.ICallback target)
		{
		}

		// Token: 0x0600418A RID: 16778 RVA: 0x0000216D File Offset: 0x0000036D
		public void Unregister(TeamLobbyPollingWatcher.ICallback target)
		{
		}

		// Token: 0x0600418B RID: 16779 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x0600418C RID: 16780 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnResponse(Handle res)
		{
		}

		// Token: 0x0600418D RID: 16781 RVA: 0x0000216D File Offset: 0x0000036D
		private void SendApplyStatusChanged(TeamLobbyPollingWatcher.ApplyingBattleData data)
		{
		}

		// Token: 0x0600418E RID: 16782 RVA: 0x0000216D File Offset: 0x0000036D
		private void SendApplicationFromOtherTeam(TeamLobbyPollingWatcher.AppliedBattleData data)
		{
		}

		// Token: 0x0600418F RID: 16783 RVA: 0x0000216D File Offset: 0x0000036D
		private void SendOpponentTeamInfoUpdated(OpponentTeamInfo data)
		{
		}

		// Token: 0x06004190 RID: 16784 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnApplyingBattleDataUpdated(object rootData)
		{
		}

		// Token: 0x06004191 RID: 16785 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnAppliedBattleDataUpdated(object rootData)
		{
		}

		// Token: 0x06004192 RID: 16786 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnOpponentTeamInfoUpdated(object rootData)
		{
		}

		// Token: 0x06004193 RID: 16787 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator Watching()
		{
			return null;
		}

		// Token: 0x06004194 RID: 16788 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator CallPolling()
		{
			return null;
		}

		// Token: 0x04007FD8 RID: 32728
		private const float POLLING_SPAN = 3f;

		// Token: 0x04007FD9 RID: 32729
		private const string CW_PATH_REQUEST_INFO = "$.DuelMenu.TeamMatch.team_info.request_info";

		// Token: 0x04007FDA RID: 32730
		private const string CW_PATH_NEW_REQUEST = "$.DuelMenu.TeamMatch.team_info.new_request";

		// Token: 0x04007FDB RID: 32731
		private static HashSet<ulong> s_alives;

		// Token: 0x04007FDC RID: 32732
		private static ulong s_InstanceNumber;

		// Token: 0x04007FDD RID: 32733
		private IEnumerator _routine;

		// Token: 0x04007FDE RID: 32734
		private TeamUtil.MatchType _matchType;

		// Token: 0x04007FDF RID: 32735
		private ViewControllerManager manager;

		// Token: 0x04007FE0 RID: 32736
		private ulong number;

		// Token: 0x04007FE1 RID: 32737
		private bool _isForceLeaveErrOccured;

		// Token: 0x04007FE2 RID: 32738
		private bool _isFatalErrOccured;

		// Token: 0x04007FE3 RID: 32739
		private HashSet<TeamLobbyPollingWatcher.ICallback> _callbacks;

		// Token: 0x04007FE4 RID: 32740
		private TeamLobbyPollingWatcher.ApplyingBattleData _applyingBattleData;

		// Token: 0x04007FE5 RID: 32741
		private TeamLobbyPollingWatcher.AppliedBattleData _appliedBattleData;

		// Token: 0x020008C1 RID: 2241
		public interface ICallback
		{
			// Token: 0x06004196 RID: 16790
			void OnPollingResponse(Handle handle);

			// Token: 0x06004197 RID: 16791
			void OnApplyingStatusChanged(TeamLobbyPollingWatcher.ApplyingBattleData data);

			// Token: 0x06004198 RID: 16792
			void OnAppliedFromOtherTeam(TeamLobbyPollingWatcher.AppliedBattleData data);

			// Token: 0x06004199 RID: 16793
			void OnOpponentTeamInfoUpdated(OpponentTeamInfo data);
		}

		// Token: 0x020008C2 RID: 2242
		public enum ApplyingStatusOnServer
		{
			// Token: 0x04007FE7 RID: 32743
			NONE,
			// Token: 0x04007FE8 RID: 32744
			WAITING,
			// Token: 0x04007FE9 RID: 32745
			CANCEL,
			// Token: 0x04007FEA RID: 32746
			REJECT,
			// Token: 0x04007FEB RID: 32747
			ACCEPT,
			// Token: 0x04007FEC RID: 32748
			READY
		}

		// Token: 0x020008C3 RID: 2243
		public struct ApplyingBattleData
		{
			// Token: 0x0600419A RID: 16794 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool Equals(ref TeamLobbyPollingWatcher.ApplyingBattleData other)
			{
				return false;
			}

			// Token: 0x04007FED RID: 32749
			public TeamLobbyPollingWatcher.ApplyingStatusOnServer status;

			// Token: 0x04007FEE RID: 32750
			public int mrk;

			// Token: 0x04007FEF RID: 32751
			public int teamId;
		}

		// Token: 0x020008C4 RID: 2244
		public struct AppliedBattleData
		{
			// Token: 0x1700050F RID: 1295
			// (get) Token: 0x0600419B RID: 16795 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool valid
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600419C RID: 16796 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool Equals(ref TeamLobbyPollingWatcher.AppliedBattleData other)
			{
				return false;
			}

			// Token: 0x04007FF0 RID: 32752
			public int mrk;

			// Token: 0x04007FF1 RID: 32753
			public int duelDurationId;

			// Token: 0x04007FF2 RID: 32754
			public int teamId;
		}
	}
}
