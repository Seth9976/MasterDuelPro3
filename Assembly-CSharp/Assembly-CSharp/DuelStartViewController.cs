using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Duel;
using YgomGame.Menu;
using YgomSystem.Network;
using YgomSystem.UI;

// Token: 0x02000021 RID: 33
public class DuelStartViewController : BaseMenuViewController
{
	// Token: 0x17000007 RID: 7
	// (get) Token: 0x0600007B RID: 123 RVA: 0x0000216A File Offset: 0x0000036A
	protected override Type[] textIds
	{
		get
		{
			return null;
		}
	}

	// Token: 0x17000008 RID: 8
	// (get) Token: 0x0600007C RID: 124 RVA: 0x000029CC File Offset: 0x00000BCC
	// (set) Token: 0x0600007D RID: 125 RVA: 0x0000216D File Offset: 0x0000036D
	protected DuelStartViewController.Step step
	{
		get
		{
			return DuelStartViewController.Step.START;
		}
		set
		{
		}
	}

	// Token: 0x0600007E RID: 126 RVA: 0x0000216D File Offset: 0x0000036D
	public override void NotificationStackEntry()
	{
	}

	// Token: 0x0600007F RID: 127 RVA: 0x0000216D File Offset: 0x0000036D
	protected virtual void InitTimeLine()
	{
	}

	// Token: 0x06000080 RID: 128 RVA: 0x0000216D File Offset: 0x0000036D
	public override void NotificationStackRemove()
	{
	}

	// Token: 0x06000081 RID: 129 RVA: 0x0000216D File Offset: 0x0000036D
	protected override void OnCreatedView()
	{
	}

	// Token: 0x06000082 RID: 130 RVA: 0x0000216D File Offset: 0x0000036D
	public override void ProgressUpdate()
	{
	}

	// Token: 0x06000083 RID: 131 RVA: 0x0000216D File Offset: 0x0000036D
	public void Update()
	{
	}

	// Token: 0x06000084 RID: 132 RVA: 0x0000216D File Offset: 0x0000036D
	private void EvalEachSteps()
	{
	}

	// Token: 0x06000085 RID: 133 RVA: 0x000029CC File Offset: 0x00000BCC
	private bool SetPvPConnection()
	{
		return false;
	}

	// Token: 0x06000086 RID: 134 RVA: 0x0000216D File Offset: 0x0000036D
	protected virtual void Init()
	{
	}

	// Token: 0x06000087 RID: 135 RVA: 0x0000216D File Offset: 0x0000036D
	private void WaitDuelResourceDone()
	{
	}

	// Token: 0x06000088 RID: 136 RVA: 0x000029CC File Offset: 0x00000BCC
	private bool LoadPlayers()
	{
		return false;
	}

	// Token: 0x06000089 RID: 137 RVA: 0x0000216D File Offset: 0x0000036D
	protected virtual void WaitInit()
	{
	}

	// Token: 0x0600008A RID: 138 RVA: 0x0000216D File Offset: 0x0000036D
	private void PlayerAppear()
	{
	}

	// Token: 0x0600008B RID: 139 RVA: 0x0000216D File Offset: 0x0000036D
	protected virtual void ControllVSImage()
	{
	}

	// Token: 0x0600008C RID: 140 RVA: 0x0000216D File Offset: 0x0000036D
	protected virtual void WaitPlayerAppear()
	{
	}

	// Token: 0x0600008D RID: 141 RVA: 0x0000216D File Offset: 0x0000036D
	private void CoinToss()
	{
	}

	// Token: 0x0600008E RID: 142 RVA: 0x0000216D File Offset: 0x0000036D
	private void WaitCoinToss()
	{
	}

	// Token: 0x0600008F RID: 143 RVA: 0x0000216D File Offset: 0x0000036D
	protected virtual void SelectTurn()
	{
	}

	// Token: 0x06000090 RID: 144 RVA: 0x0000216D File Offset: 0x0000036D
	private void TimeCountNotificator(object mes)
	{
	}

	// Token: 0x06000091 RID: 145 RVA: 0x0000216D File Offset: 0x0000036D
	private void WaitSelectTurn()
	{
	}

	// Token: 0x06000092 RID: 146 RVA: 0x0000216D File Offset: 0x0000036D
	private void DispSelected()
	{
	}

	// Token: 0x06000093 RID: 147 RVA: 0x0000216D File Offset: 0x0000036D
	protected virtual void PlayTweenHide()
	{
	}

	// Token: 0x06000094 RID: 148 RVA: 0x0000216D File Offset: 0x0000036D
	public override void TransitionStart(ViewController.TransitionType type)
	{
	}

	// Token: 0x06000095 RID: 149 RVA: 0x0000216D File Offset: 0x0000036D
	protected virtual void DuelBegin()
	{
	}

	// Token: 0x06000096 RID: 150 RVA: 0x0000216D File Offset: 0x0000036D
	private void WaitFinal()
	{
	}

	// Token: 0x06000097 RID: 151 RVA: 0x0000216D File Offset: 0x0000036D
	protected virtual void StartDuel()
	{
	}

	// Token: 0x06000098 RID: 152 RVA: 0x0000216D File Offset: 0x0000036D
	protected virtual void CauseError()
	{
	}

	// Token: 0x06000099 RID: 153 RVA: 0x000029D8 File Offset: 0x00000BD8
	public Color FadeColor(ViewController.TransitionType type)
	{
		return default(Color);
	}

	// Token: 0x0600009A RID: 154 RVA: 0x000029CC File Offset: 0x00000BCC
	public SystemProgress.ProgressType FadeType(ViewController.TransitionType type)
	{
		return SystemProgress.ProgressType.None;
	}

	// Token: 0x0600009B RID: 155 RVA: 0x0000216A File Offset: 0x0000036A
	private IEnumerator yInit()
	{
		return null;
	}

	// Token: 0x0600009C RID: 156 RVA: 0x0000216A File Offset: 0x0000036A
	private IEnumerator yWait()
	{
		return null;
	}

	// Token: 0x0600009D RID: 157 RVA: 0x0000216A File Offset: 0x0000036A
	private IEnumerator Start()
	{
		return null;
	}

	// Token: 0x04000088 RID: 136
	public static readonly string PrefabPath;

	// Token: 0x04000089 RID: 137
	private readonly string ROOT_PLAYER_LABEL;

	// Token: 0x0400008A RID: 138
	private readonly string ROOT_RIVAL_LABEL;

	// Token: 0x0400008B RID: 139
	private readonly string ROOT_CAN_CHOICE_LABEL;

	// Token: 0x0400008C RID: 140
	private readonly string ROOT_CANT_CHOICE_LABEL;

	// Token: 0x0400008D RID: 141
	private readonly string ROOT_VS_LABEL;

	// Token: 0x0400008E RID: 142
	private readonly string ROOT_RESULT_COINTOSS_LABEL;

	// Token: 0x0400008F RID: 143
	private readonly string TXT_TIME_LABEL;

	// Token: 0x04000090 RID: 144
	private readonly string TXT_LABEL;

	// Token: 0x04000091 RID: 145
	private readonly string SC_SKIP_LABEL;

	// Token: 0x04000092 RID: 146
	private readonly string BTN_FIRST_LABEL;

	// Token: 0x04000093 RID: 147
	private readonly string BTN_SECOND_LABEL;

	// Token: 0x04000094 RID: 148
	private readonly string BTN_BACKKEYSHORTCUT_LABEL;

	// Token: 0x04000095 RID: 149
	private readonly string TIME_COUNT_PATH;

	// Token: 0x04000096 RID: 150
	private bool m_bInit;

	// Token: 0x04000097 RID: 151
	private bool m_bStartDuel;

	// Token: 0x04000098 RID: 152
	private Dictionary<string, object> m_DuelParam;

	// Token: 0x04000099 RID: 153
	private DuelStartWaitingBase m_DSWaitingComponent;

	// Token: 0x0400009A RID: 154
	private Handle m_Handle;

	// Token: 0x0400009B RID: 155
	private IEnumerator m_SelectWaitCoroutine;

	// Token: 0x0400009C RID: 156
	private bool canChoice;

	// Token: 0x0400009D RID: 157
	private GameObject rootPlayer;

	// Token: 0x0400009E RID: 158
	private GameObject rootRival;

	// Token: 0x0400009F RID: 159
	private GameObject rootCanChoice;

	// Token: 0x040000A0 RID: 160
	private GameObject rootCantChoice;

	// Token: 0x040000A1 RID: 161
	protected GameObject rootVS;

	// Token: 0x040000A2 RID: 162
	private GameObject duelStartResultCointoss;

	// Token: 0x040000A3 RID: 163
	private DuelStartViewController.PlayerSet player;

	// Token: 0x040000A4 RID: 164
	private DuelStartViewController.PlayerSet rival;

	// Token: 0x040000A5 RID: 165
	protected DuelStartViewController.Step m_Step;

	// Token: 0x02000022 RID: 34
	protected enum Step
	{
		// Token: 0x040000A7 RID: 167
		START,
		// Token: 0x040000A8 RID: 168
		INIT,
		// Token: 0x040000A9 RID: 169
		WAIT_DUELRESOURCEDONE,
		// Token: 0x040000AA RID: 170
		WAIT_INIT,
		// Token: 0x040000AB RID: 171
		PLAYER_APPEAR,
		// Token: 0x040000AC RID: 172
		WAIT_PLAYER_APPEAR,
		// Token: 0x040000AD RID: 173
		COIN_TOSS,
		// Token: 0x040000AE RID: 174
		WAIT_COIN_TOSS,
		// Token: 0x040000AF RID: 175
		SELECT_TURN,
		// Token: 0x040000B0 RID: 176
		WAIT_SELECT_TURN,
		// Token: 0x040000B1 RID: 177
		DISP_SELECTED,
		// Token: 0x040000B2 RID: 178
		DUEL_BEGIN,
		// Token: 0x040000B3 RID: 179
		WAIT_FINAL,
		// Token: 0x040000B4 RID: 180
		END,
		// Token: 0x040000B5 RID: 181
		ERROR
	}

	// Token: 0x02000023 RID: 35
	internal class PlayerSet
	{
		// Token: 0x0600009F RID: 159 RVA: 0x00002739 File Offset: 0x00000939
		internal PlayerSet(GameObject root, bool isPlayer)
		{
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000029CC File Offset: 0x00000BCC
		internal bool Initialize()
		{
			return false;
		}

		// Token: 0x040000B6 RID: 182
		private readonly string IMG_ICON_LABEL;

		// Token: 0x040000B7 RID: 183
		private readonly string IMG_RANK_LABEL;

		// Token: 0x040000B8 RID: 184
		private readonly string IMG_LINE_LABEL;

		// Token: 0x040000B9 RID: 185
		private readonly string IMG_DLV_LABEL;

		// Token: 0x040000BA RID: 186
		private readonly string PLATFORM_NAME_LABEL;

		// Token: 0x040000BB RID: 187
		private readonly string PLATFORM_ICON_LABEL;

		// Token: 0x040000BC RID: 188
		private readonly string ROOT_RANKUP_LABEL;

		// Token: 0x040000BD RID: 189
		private readonly string TXT_RANKUP_LABEL;

		// Token: 0x040000BE RID: 190
		private readonly string ROOT_RANKDOWN_LABEL;

		// Token: 0x040000BF RID: 191
		private readonly string TXT_RANKDOWN_LABEL;

		// Token: 0x040000C0 RID: 192
		private readonly string ROOT_RANK_LABEL;

		// Token: 0x040000C1 RID: 193
		private readonly string ROOT_PROFILE_LABEL;

		// Token: 0x040000C2 RID: 194
		private readonly string DECK_CASE_LABEL;

		// Token: 0x040000C3 RID: 195
		private readonly GameObject root;

		// Token: 0x040000C4 RID: 196
		private readonly bool isPlayer;

		// Token: 0x040000C5 RID: 197
		internal bool isLoading;

		// Token: 0x040000C6 RID: 198
		internal Character2D chara;

		// Token: 0x040000C7 RID: 199
		internal bool canChoice;

		// Token: 0x040000C8 RID: 200
		internal int myid;

		// Token: 0x040000C9 RID: 201
		internal Util.PlatformID platformID;
	}
}
