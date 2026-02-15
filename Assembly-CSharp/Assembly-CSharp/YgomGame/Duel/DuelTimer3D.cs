using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.YGomTMPro;

namespace YgomGame.Duel
{
	// Token: 0x02000D9F RID: 3487
	public class DuelTimer3D : MonoBehaviour
	{
		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x06006687 RID: 26247 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006688 RID: 26248 RVA: 0x0000216D File Offset: 0x0000036D
		public bool Active
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x06006689 RID: 26249 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600668A RID: 26250 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isInitialized
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x0600668B RID: 26251 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x0600668C RID: 26252 RVA: 0x0000216D File Offset: 0x0000036D
		public DuelGameObjectManager goManager
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x0600668D RID: 26253 RVA: 0x000029C5 File Offset: 0x00000BC5
		private float MAXTIME
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x0600668E RID: 26254 RVA: 0x000029C5 File Offset: 0x00000BC5
		private float m_RemainTotal
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x0600668F RID: 26255 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006690 RID: 26256 RVA: 0x0000216D File Offset: 0x0000036D
		private int m_RemainTimeInt
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x06006691 RID: 26257 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsPlayerTimeOver
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x06006692 RID: 26258 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006693 RID: 26259 RVA: 0x0000216D File Offset: 0x0000036D
		private Material m_TimerMat
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x06006694 RID: 26260 RVA: 0x0000216A File Offset: 0x0000036A
		public static DuelTimer3D Create(DuelGameObjectManager goManager, string name, bool isEsportsVer)
		{
			return null;
		}

		// Token: 0x06006695 RID: 26261 RVA: 0x0000216D File Offset: 0x0000036D
		public void PrepareToDuel()
		{
		}

		// Token: 0x06006696 RID: 26262 RVA: 0x0000216D File Offset: 0x0000036D
		private void ResetTimer()
		{
		}

		// Token: 0x06006697 RID: 26263 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetRemainTime(float dueltime, float turntime)
		{
		}

		// Token: 0x06006698 RID: 26264 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnDuelStart()
		{
		}

		// Token: 0x06006699 RID: 26265 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnDuelEnd()
		{
		}

		// Token: 0x0600669A RID: 26266 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetPlayerInput(bool value)
		{
		}

		// Token: 0x0600669B RID: 26267 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitInitializeStep()
		{
		}

		// Token: 0x0600669C RID: 26268 RVA: 0x0000216D File Offset: 0x0000036D
		private void DuelStep()
		{
		}

		// Token: 0x0600669D RID: 26269 RVA: 0x0000216D File Offset: 0x0000036D
		private void TerminatingStep()
		{
		}

		// Token: 0x0600669E RID: 26270 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x0600669F RID: 26271 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateTimerCage()
		{
		}

		// Token: 0x060066A0 RID: 26272 RVA: 0x0000216D File Offset: 0x0000036D
		protected void EmergencyEffect()
		{
		}

		// Token: 0x060066A1 RID: 26273 RVA: 0x0000216D File Offset: 0x0000036D
		protected void Initialize(bool isEsportsVer)
		{
		}

		// Token: 0x060066A2 RID: 26274 RVA: 0x0000216D File Offset: 0x0000036D
		protected void Terminate()
		{
		}

		// Token: 0x0400A08E RID: 41102
		private static bool ActivePre;

		// Token: 0x0400A08F RID: 41103
		private const int EMERTGENCYTIME = 30;

		// Token: 0x0400A090 RID: 41104
		private const string LABEL_TWEEN_ACTIVE = "Active";

		// Token: 0x0400A091 RID: 41105
		private const string LABEL_TWEEN_INACTIVE = "Inactive";

		// Token: 0x0400A092 RID: 41106
		private const string LABEL_TWEEN_ALERT = "Alert";

		// Token: 0x0400A093 RID: 41107
		private const string LABEL_TWEEN_NORMAL = "Normal";

		// Token: 0x0400A094 RID: 41108
		private const string LABEL_TWEEN_COUNTDOWN = "CountDown";

		// Token: 0x0400A095 RID: 41109
		private const string LABEL_TWEEN_DUELSTART = "DuelStart";

		// Token: 0x0400A096 RID: 41110
		private const string LABEL_TWEEN_DUELEND = "DuelEnd";

		// Token: 0x0400A097 RID: 41111
		private const string LABEL_TIMERBODY = "Timer";

		// Token: 0x0400A098 RID: 41112
		private const string LABEL_TIMERTEXT = "Text";

		// Token: 0x0400A099 RID: 41113
		private const string LABEL_ZONEEFFECTROOT = "ZoneEffect";

		// Token: 0x0400A09A RID: 41114
		private const string LABEL_ZONEEFFECT_SEC1 = "Section01";

		// Token: 0x0400A09B RID: 41115
		private const string LABEL_ZONEEFFECT_SEC2 = "Section02";

		// Token: 0x0400A09C RID: 41116
		private const string LABEL_ZONEEFFECT_SEC2Y = "Section02Yellow";

		// Token: 0x0400A09D RID: 41117
		private const string LABEL_SHINYEFFECTROOT = "ShinyEffect";

		// Token: 0x0400A09E RID: 41118
		private const string LABEL_SHINYEFFECT_BLUE = "LeadTime";

		// Token: 0x0400A09F RID: 41119
		private const string LABEL_SHINYEFFECT_GOLD = "BaseTime";

		// Token: 0x0400A0A0 RID: 41120
		private const string LABEL_SHADER_MAXTIME = "_MaxTime";

		// Token: 0x0400A0A1 RID: 41121
		private const string LABEL_SHADER_ADDTIME = "_AddTime";

		// Token: 0x0400A0A2 RID: 41122
		private const string LABEL_SHADER_ACTIVE = "_Active";

		// Token: 0x0400A0A3 RID: 41123
		private const string PATH_PREHAB = "Duel/BG/Timer/Timer_c001/Timer_c001";

		// Token: 0x0400A0A4 RID: 41124
		private const string PATH_PREHAB_SP = "Duel/BG/Timer/Timer_013/Timer_013";

		// Token: 0x0400A0A5 RID: 41125
		private GameObject m_TimerModel;

		// Token: 0x0400A0A6 RID: 41126
		private ElementObjectManager m_EOManager;

		// Token: 0x0400A0A7 RID: 41127
		private MeshRenderer m_TimerBody;

		// Token: 0x0400A0A8 RID: 41128
		private ExtendedTextMeshPro m_TimerText;

		// Token: 0x0400A0A9 RID: 41129
		private ElementObjectManager m_ShinyEffectEom;

		// Token: 0x0400A0AA RID: 41130
		private ElementObjectManager m_ZoneEffectEom;

		// Token: 0x0400A0AB RID: 41131
		private int m_MaxDuelTime;

		// Token: 0x0400A0AC RID: 41132
		private bool m_Visilble;

		// Token: 0x0400A0AD RID: 41133
		private int m_MaxTurnTime;

		// Token: 0x0400A0AE RID: 41134
		private int m_RemainTimeIntOrg;

		// Token: 0x0400A0AF RID: 41135
		private bool m_IsPlayerInput;

		// Token: 0x0400A0B0 RID: 41136
		private float m_RemainInDuel;

		// Token: 0x0400A0B1 RID: 41137
		private float m_RemainInTurn;

		// Token: 0x0400A0B2 RID: 41138
		private float m_RemainInTurnPre;

		// Token: 0x0400A0B3 RID: 41139
		private float m_RealTimePreFrame;

		// Token: 0x0400A0B4 RID: 41140
		private DuelTimer3D.Step m_Step;

		// Token: 0x02000DA0 RID: 3488
		private enum Step
		{
			// Token: 0x0400A0B6 RID: 41142
			WaitInitialize,
			// Token: 0x0400A0B7 RID: 41143
			Idle,
			// Token: 0x0400A0B8 RID: 41144
			Duel,
			// Token: 0x0400A0B9 RID: 41145
			Terminating
		}
	}
}
