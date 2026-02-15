using System;
using System.Collections.Generic;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.YGomTMPro;

namespace YgomGame.Duel
{
	// Token: 0x02000D8A RID: 3466
	public class DuelLP : MonoBehaviour
	{
		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x0600656B RID: 25963 RVA: 0x000029CC File Offset: 0x00000BCC
		public int currentLP
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x0600656C RID: 25964 RVA: 0x0000216A File Offset: 0x0000036A
		protected ExtendedTextMeshProUGUI m_RVS
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600656D RID: 25965 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create(Transform parent, int player, DuelClient host, Action<DuelLP> onFinished)
		{
		}

		// Token: 0x0600656E RID: 25966 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(int playerid, DuelClient host)
		{
		}

		// Token: 0x0600656F RID: 25967 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetLP(int lp)
		{
		}

		// Token: 0x06006570 RID: 25968 RVA: 0x0000216D File Offset: 0x0000036D
		public void ChangeLP(int afterLP, int damage, Engine.DamageType type, int player, int position, Action onFinished = null)
		{
		}

		// Token: 0x06006571 RID: 25969 RVA: 0x0000216D File Offset: 0x0000036D
		public void ChangeMode(DuelLP.DispMode mode)
		{
		}

		// Token: 0x06006572 RID: 25970 RVA: 0x0000216A File Offset: 0x0000036A
		protected LPCounterSub AppendLPCounterSub()
		{
			return null;
		}

		// Token: 0x06006573 RID: 25971 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool ApplyCounterSub(int targetlp, int changevalue, Engine.DamageType type, int player, int position)
		{
			return false;
		}

		// Token: 0x06006574 RID: 25972 RVA: 0x000F5C70 File Offset: 0x000F3E70
		protected Vector2 GetRectPosBy3DPos(RectTransform rect, Vector3 worldpos)
		{
			return default(Vector2);
		}

		// Token: 0x06006575 RID: 25973 RVA: 0x0000216D File Offset: 0x0000036D
		protected void ApplyTransLP(Color lpcol, int targetlife)
		{
		}

		// Token: 0x06006576 RID: 25974 RVA: 0x0000216D File Offset: 0x0000036D
		protected void IdleStep()
		{
		}

		// Token: 0x06006577 RID: 25975 RVA: 0x0000216D File Offset: 0x0000036D
		protected void WaitSubEffectStep()
		{
		}

		// Token: 0x06006578 RID: 25976 RVA: 0x0000216D File Offset: 0x0000036D
		protected void TransitionLPStep()
		{
		}

		// Token: 0x06006579 RID: 25977 RVA: 0x0000216D File Offset: 0x0000036D
		protected void WaitToIdleStep()
		{
		}

		// Token: 0x0600657A RID: 25978 RVA: 0x0000216D File Offset: 0x0000036D
		protected void ToIdleStep()
		{
		}

		// Token: 0x0600657B RID: 25979 RVA: 0x0000216A File Offset: 0x0000036A
		protected LPCounterSub GetAvailableLPCounterSub()
		{
			return null;
		}

		// Token: 0x0600657C RID: 25980 RVA: 0x0000216D File Offset: 0x0000036D
		protected void EmergencyEffect()
		{
		}

		// Token: 0x0600657D RID: 25981 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x0600657E RID: 25982 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x0600657F RID: 25983 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateLayout()
		{
		}

		// Token: 0x06006580 RID: 25984 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnDuelEnd()
		{
		}

		// Token: 0x06006581 RID: 25985 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionButton GetButton()
		{
			return null;
		}

		// Token: 0x06006582 RID: 25986 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDispNetworkError(bool disp)
		{
		}

		// Token: 0x06006583 RID: 25987 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetName(int playerid)
		{
		}

		// Token: 0x04009FB0 RID: 40880
		public float countTime;

		// Token: 0x04009FB1 RID: 40881
		public float EmergencyPeriod;

		// Token: 0x04009FB2 RID: 40882
		public Color lpColorDamage;

		// Token: 0x04009FB3 RID: 40883
		public Color lpColorRecover;

		// Token: 0x04009FB4 RID: 40884
		public Color lpColorNormal;

		// Token: 0x04009FB5 RID: 40885
		protected const string LAEBL_EO_RVS = "RestValueShow";

		// Token: 0x04009FB6 RID: 40886
		protected const string LAEBL_EO_CONTENT = "Content";

		// Token: 0x04009FB7 RID: 40887
		protected const string LAEBL_EO_LIFEPOINTROOT = "LifePointRoot";

		// Token: 0x04009FB8 RID: 40888
		protected const string LAEBL_EO_CHANGEVALUEROOT = "ChangeValueRoot";

		// Token: 0x04009FB9 RID: 40889
		protected const string LAEBL_EO_LPLABEL = "LPLabel";

		// Token: 0x04009FBA RID: 40890
		protected const string LAEBL_EO_RECTVALUEBG = "RectValueBg";

		// Token: 0x04009FBB RID: 40891
		protected const string LAEBL_EO_PLAYERICON = "PlayerIcon";

		// Token: 0x04009FBC RID: 40892
		protected const string LAEBL_EO_PLAYERICONFRAME = "PlayerIconBorder";

		// Token: 0x04009FBD RID: 40893
		protected const string LAEBL_EO_PLAYERNAME = "PlayerName";

		// Token: 0x04009FBE RID: 40894
		protected const string LAEBL_EO_EXTRAIDROOT = "ExtraIdRoot";

		// Token: 0x04009FBF RID: 40895
		protected const string LAEBL_EO_EXTRAIDNAME = "ExtraIdName";

		// Token: 0x04009FC0 RID: 40896
		protected const string LAEBL_EO_EXTRAIDICON = "ExtraIdIcon";

		// Token: 0x04009FC1 RID: 40897
		protected const string LAEBL_EO_SPECIALACCOUNTICONS = "SpecialAccountIcons";

		// Token: 0x04009FC2 RID: 40898
		protected const string LAEBL_EO_VALPOS = "ValueBirthPos";

		// Token: 0x04009FC3 RID: 40899
		protected const string LAEBL_EO_NETWORKERROR = "NetworkErrorRoot";

		// Token: 0x04009FC4 RID: 40900
		protected const string LAEBL_TP_CSUB = "ChangeValue";

		// Token: 0x04009FC5 RID: 40901
		protected const string LABEL_SE_LP_COUNT = "SE_LP_COUNT";

		// Token: 0x04009FC6 RID: 40902
		protected const string LABEL_SE_LP_ZERO = "SE_LP_ZERO";

		// Token: 0x04009FC7 RID: 40903
		protected const string LABEL_TW_CMAINZOOMIN = "CMainZoomIn";

		// Token: 0x04009FC8 RID: 40904
		protected const string LABEL_TW_CSUBZOOMIN = "CSubZoomIn";

		// Token: 0x04009FC9 RID: 40905
		protected const string LABEL_TW_CSUBZOOMOUT = "CSubZoomOut";

		// Token: 0x04009FCA RID: 40906
		protected const string CURRENTPLATFORMICONPATH = "Images/PlatformIcon/<_PLATFORM_>/CurrentPlatformS";

		// Token: 0x04009FCB RID: 40907
		protected const int NUM_DEFAULTCSUBNUM = 2;

		// Token: 0x04009FCC RID: 40908
		protected int m_LPBeforeTrans;

		// Token: 0x04009FCD RID: 40909
		protected int m_LPTarget;

		// Token: 0x04009FCE RID: 40910
		protected int m_LPCurrent;

		// Token: 0x04009FCF RID: 40911
		protected int m_LPForDisp;

		// Token: 0x04009FD0 RID: 40912
		protected float m_CurrentTime;

		// Token: 0x04009FD1 RID: 40913
		protected float m_WaitTime;

		// Token: 0x04009FD2 RID: 40914
		protected DuelClient m_Host;

		// Token: 0x04009FD3 RID: 40915
		protected Action m_OnFinishedCallBack;

		// Token: 0x04009FD4 RID: 40916
		protected List<LPCounterSub> m_CounterSubs;

		// Token: 0x04009FD5 RID: 40917
		protected DuelLP.Step m_Step;

		// Token: 0x04009FD6 RID: 40918
		protected Dictionary<DuelLP.Step, Action> m_StepActTable;

		// Token: 0x04009FD7 RID: 40919
		protected ElementObjectManager m_EOManager;

		// Token: 0x04009FD8 RID: 40920
		protected DuelLP.DispMode m_DispMode;

		// Token: 0x04009FD9 RID: 40921
		protected DuelLP.DispMode m_TargetDispMode;

		// Token: 0x04009FDA RID: 40922
		protected string m_CurrentSeLabel;

		// Token: 0x04009FDB RID: 40923
		protected bool m_IsSimpleViewMode;

		// Token: 0x04009FDC RID: 40924
		protected bool m_DuelEnd;

		// Token: 0x04009FDD RID: 40925
		private GameObject m_networkErrorRoot;

		// Token: 0x04009FDE RID: 40926
		private ExtendedTextMeshProUGUI m_CounterMainTxtShow_Origin;

		// Token: 0x04009FDF RID: 40927
		private bool m_IsInitialized;

		// Token: 0x02000D8B RID: 3467
		public enum Step
		{
			// Token: 0x04009FE1 RID: 40929
			Idle,
			// Token: 0x04009FE2 RID: 40930
			WaitSubEffect,
			// Token: 0x04009FE3 RID: 40931
			TransitionLP,
			// Token: 0x04009FE4 RID: 40932
			WaitToIdle,
			// Token: 0x04009FE5 RID: 40933
			ToIdle
		}

		// Token: 0x02000D8C RID: 3468
		public enum DispMode
		{
			// Token: 0x04009FE7 RID: 40935
			NormalMode,
			// Token: 0x04009FE8 RID: 40936
			SimpleMode
		}
	}
}
