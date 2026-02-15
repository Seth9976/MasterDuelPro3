using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Menu;
using YgomSystem.ElementSystem;

// Token: 0x0200001F RID: 31
public class DuelStartOverlayViewController_Team : BaseMenuViewController
{
	// Token: 0x06000070 RID: 112 RVA: 0x0000216D File Offset: 0x0000036D
	public override void NotificationStackEntry()
	{
	}

	// Token: 0x06000071 RID: 113 RVA: 0x0000216D File Offset: 0x0000036D
	protected override void OnCreatedView()
	{
	}

	// Token: 0x06000072 RID: 114 RVA: 0x0000216D File Offset: 0x0000036D
	private void DispTeam(int myid, ElementObjectManager teamEom, bool isPlayerTeam)
	{
	}

	// Token: 0x06000073 RID: 115 RVA: 0x0000216D File Offset: 0x0000036D
	private void InitTeam()
	{
	}

	// Token: 0x06000074 RID: 116 RVA: 0x0000216A File Offset: 0x0000036A
	private DuelStartOverlayViewController_Team.TeamInfo SetTeamInfo(object teamInfo)
	{
		return null;
	}

	// Token: 0x06000075 RID: 117 RVA: 0x0000216D File Offset: 0x0000036D
	private void SetTeam(ElementObjectManager eom, DuelStartOverlayViewController_Team.TeamInfo teamInfo, bool isPlayerTeam)
	{
	}

	// Token: 0x06000076 RID: 118 RVA: 0x0000216A File Offset: 0x0000036A
	public GameObject GetHideObject()
	{
		return null;
	}

	// Token: 0x06000077 RID: 119 RVA: 0x0000216D File Offset: 0x0000036D
	public void Start()
	{
	}

	// Token: 0x06000078 RID: 120 RVA: 0x0000216D File Offset: 0x0000036D
	public void Update()
	{
	}

	// Token: 0x04000076 RID: 118
	public static readonly string PREFAB_PATH;

	// Token: 0x04000077 RID: 119
	private bool isFinish;

	// Token: 0x04000078 RID: 120
	private readonly string E_TweenFinish;

	// Token: 0x04000079 RID: 121
	private readonly string E_RootPlayer;

	// Token: 0x0400007A RID: 122
	private readonly string E_RootRival;

	// Token: 0x0400007B RID: 123
	private readonly string E_TextTeamName;

	// Token: 0x0400007C RID: 124
	private readonly string E_RootProfile;

	// Token: 0x0400007D RID: 125
	private readonly string E_ImageIcon;

	// Token: 0x0400007E RID: 126
	private readonly string E_PlatformPlayerIcon;

	// Token: 0x0400007F RID: 127
	private readonly string E_PlatformPlayerNameGroup;

	// Token: 0x04000080 RID: 128
	private ElementObjectManager playerEom;

	// Token: 0x04000081 RID: 129
	private ElementObjectManager rivalEom;

	// Token: 0x04000082 RID: 130
	private GameObject tweenFinish;

	// Token: 0x04000083 RID: 131
	[SerializeField]
	private int MAX_MEMBERS;

	// Token: 0x02000020 RID: 32
	private class TeamInfo
	{
		// Token: 0x0600007A RID: 122 RVA: 0x00002739 File Offset: 0x00000939
		public TeamInfo(object info, int teamNameMrk, int userNum, Dictionary<string, object> membersInfo)
		{
		}

		// Token: 0x04000084 RID: 132
		public readonly object info;

		// Token: 0x04000085 RID: 133
		public readonly int teamNameMrk;

		// Token: 0x04000086 RID: 134
		public readonly int userNum;

		// Token: 0x04000087 RID: 135
		public readonly Dictionary<string, object> membersInfo;
	}
}
