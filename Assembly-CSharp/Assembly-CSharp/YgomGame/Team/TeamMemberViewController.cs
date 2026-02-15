using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomGame.Menu;
using YgomSystem.Network;
using YgomSystem.UI;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.Team
{
	// Token: 0x020008C8 RID: 2248
	public class TeamMemberViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported, TeamLobbyPollingWatcher.ICallback
	{
		// Token: 0x060041B6 RID: 16822 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(ViewControllerManager manager, TeamMemberViewController.Param param)
		{
		}

		// Token: 0x060041B7 RID: 16823 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool GetBool(Dictionary<string, object> data, TeamMemberViewController.ArgKeyName key)
		{
			return false;
		}

		// Token: 0x060041B8 RID: 16824 RVA: 0x000029CC File Offset: 0x00000BCC
		private int GetInt(Dictionary<string, object> data, TeamMemberViewController.ArgKeyName key)
		{
			return 0;
		}

		// Token: 0x060041B9 RID: 16825 RVA: 0x000F1669 File Offset: 0x000EF869
		private long GetLong(Dictionary<string, object> data, TeamMemberViewController.ArgKeyName key)
		{
			return 0L;
		}

		// Token: 0x060041BA RID: 16826 RVA: 0x0000216A File Offset: 0x0000036A
		private string GetString(Dictionary<string, object> data, TeamMemberViewController.ArgKeyName key)
		{
			return null;
		}

		// Token: 0x060041BB RID: 16827 RVA: 0x0000216D File Offset: 0x0000036D
		private void Register(Dictionary<string, object> data, TeamMemberViewController.ArgKeyName key, object value)
		{
		}

		// Token: 0x060041BC RID: 16828 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x060041BD RID: 16829 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x060041BE RID: 16830 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x060041BF RID: 16831 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x060041C0 RID: 16832 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEntityUpdate(GameObject obj, int index)
		{
		}

		// Token: 0x060041C1 RID: 16833 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnPollingResponse(Handle handle)
		{
		}

		// Token: 0x060041C2 RID: 16834 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnProfileCardOpening(Dictionary<string, object> member)
		{
		}

		// Token: 0x060041C3 RID: 16835 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateMembers()
		{
		}

		// Token: 0x060041C4 RID: 16836 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnApplyingStatusChanged(TeamLobbyPollingWatcher.ApplyingBattleData data)
		{
		}

		// Token: 0x060041C5 RID: 16837 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnAppliedFromOtherTeam(TeamLobbyPollingWatcher.AppliedBattleData data)
		{
		}

		// Token: 0x060041C6 RID: 16838 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnOpponentTeamInfoUpdated(OpponentTeamInfo data)
		{
		}

		// Token: 0x04008010 RID: 32784
		private readonly string LBL_PLATFORMPLAYERICON;

		// Token: 0x04008011 RID: 32785
		private readonly string LBL_PLATFORMPLAYERNAMEGROUP;

		// Token: 0x04008012 RID: 32786
		private readonly string LBL_PROFILEICON;

		// Token: 0x04008013 RID: 32787
		private readonly string LBL_BUTTON;

		// Token: 0x04008014 RID: 32788
		private InfinityScrollView _scrollView;

		// Token: 0x04008015 RID: 32789
		private TeamMemberViewController.Param _param;

		// Token: 0x04008016 RID: 32790
		private Dictionary<long, Dictionary<string, object>> members;

		// Token: 0x04008017 RID: 32791
		private Dictionary<int, string> s_KeyNameCaches;

		// Token: 0x04008018 RID: 32792
		private List<long> _orderedPcodes;

		// Token: 0x020008C9 RID: 2249
		public class PcodeData : MonoBehaviour
		{
			// Token: 0x17000512 RID: 1298
			// (get) Token: 0x060041C8 RID: 16840 RVA: 0x000F1669 File Offset: 0x000EF869
			// (set) Token: 0x060041C9 RID: 16841 RVA: 0x0000216D File Offset: 0x0000036D
			public long value
			{
				[CompilerGenerated]
				get
				{
					return 0L;
				}
				[CompilerGenerated]
				set
				{
				}
			}
		}

		// Token: 0x020008CA RID: 2250
		public class Param
		{
			// Token: 0x17000513 RID: 1299
			// (get) Token: 0x060041CB RID: 16843 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x060041CC RID: 16844 RVA: 0x0000216D File Offset: 0x0000036D
			public TeamLobbyPollingWatcher watchDog
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

			// Token: 0x17000514 RID: 1300
			// (get) Token: 0x060041CD RID: 16845 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x060041CE RID: 16846 RVA: 0x0000216D File Offset: 0x0000036D
			public int teamNumMax
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
		}

		// Token: 0x020008CB RID: 2251
		private enum ArgKeyName
		{
			// Token: 0x0400801A RID: 32794
			name,
			// Token: 0x0400801B RID: 32795
			pcode,
			// Token: 0x0400801C RID: 32796
			follow_num,
			// Token: 0x0400801D RID: 32797
			follower_num,
			// Token: 0x0400801E RID: 32798
			level,
			// Token: 0x0400801F RID: 32799
			rank,
			// Token: 0x04008020 RID: 32800
			rate,
			// Token: 0x04008021 RID: 32801
			icon_id,
			// Token: 0x04008022 RID: 32802
			icon_frame_id,
			// Token: 0x04008023 RID: 32803
			tag,
			// Token: 0x04008024 RID: 32804
			avatar_id,
			// Token: 0x04008025 RID: 32805
			wallpaper,
			// Token: 0x04008026 RID: 32806
			exp,
			// Token: 0x04008027 RID: 32807
			need_exp,
			// Token: 0x04008028 RID: 32808
			online_id,
			// Token: 0x04008029 RID: 32809
			is_same_os,
			// Token: 0x0400802A RID: 32810
			xuid,
			// Token: 0x0400802B RID: 32811
			edit,
			// Token: 0x0400802C RID: 32812
			rank_event,
			// Token: 0x0400802D RID: 32813
			official,
			// Token: 0x0400802E RID: 32814
			MAX
		}
	}
}
