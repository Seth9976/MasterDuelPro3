using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YgomSystem.ElementSystem;
using YgomSystem.Timeline;
using YgomSystem.UI;
using YgomSystem.YGomTMPro;

namespace YgomGame.Duel
{
	// Token: 0x02000EDC RID: 3804
	public class PhaseSelect3D : MonoBehaviour
	{
		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x06006ED8 RID: 28376 RVA: 0x000029CC File Offset: 0x00000BCC
		private int m_TurnPlayer
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06006ED9 RID: 28377 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create(DuelClient host, Transform parent, UnityAction<PhaseSelect3D> onFinish, bool isEsportsVer)
		{
		}

		// Token: 0x06006EDA RID: 28378 RVA: 0x0000216D File Offset: 0x0000036D
		public void PhaseChange(Engine.Phase nextphase)
		{
		}

		// Token: 0x06006EDB RID: 28379 RVA: 0x0000216D File Offset: 0x0000036D
		public void PhaseChangeSE()
		{
		}

		// Token: 0x06006EDC RID: 28380 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnDuelStart()
		{
		}

		// Token: 0x06006EDD RID: 28381 RVA: 0x0000216D File Offset: 0x0000036D
		public void PhaseChangeProcess()
		{
		}

		// Token: 0x06006EDE RID: 28382 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateTurnText()
		{
		}

		// Token: 0x06006EDF RID: 28383 RVA: 0x0000216D File Offset: 0x0000036D
		public void TurnChange(int team, Action onFinished)
		{
		}

		// Token: 0x06006EE0 RID: 28384 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateStatus(int turnplayer, Engine.Phase nextphase)
		{
		}

		// Token: 0x06006EE1 RID: 28385 RVA: 0x0000216D File Offset: 0x0000036D
		public void SkipTimeline()
		{
		}

		// Token: 0x06006EE2 RID: 28386 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateStepIcon(Engine.StepType battlestep, Engine.DmgStepType damagestep)
		{
		}

		// Token: 0x06006EE3 RID: 28387 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsPhaseMovable(Engine.Phase phase)
		{
			return false;
		}

		// Token: 0x06006EE4 RID: 28388 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnClickButtonPhase()
		{
		}

		// Token: 0x06006EE5 RID: 28389 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionButton GetButton()
		{
			return null;
		}

		// Token: 0x06006EE6 RID: 28390 RVA: 0x000F6188 File Offset: 0x000F4388
		public Vector3 GetPadIconPosition()
		{
			return default(Vector3);
		}

		// Token: 0x06006EE7 RID: 28391 RVA: 0x000F61A0 File Offset: 0x000F43A0
		public Vector3 GetPosition()
		{
			return default(Vector3);
		}

		// Token: 0x06006EE8 RID: 28392 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetHintEffectVisible(bool visible)
		{
		}

		// Token: 0x06006EE9 RID: 28393 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06006EEA RID: 28394 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitTables()
		{
		}

		// Token: 0x06006EEB RID: 28395 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitComponent()
		{
		}

		// Token: 0x06006EEC RID: 28396 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitSelectorBotton()
		{
		}

		// Token: 0x06006EED RID: 28397 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupSelectorPriority()
		{
		}

		// Token: 0x06006EEE RID: 28398 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnSelected()
		{
		}

		// Token: 0x06006EEF RID: 28399 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDeselect()
		{
		}

		// Token: 0x06006EF0 RID: 28400 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnPointerEnter()
		{
		}

		// Token: 0x06006EF1 RID: 28401 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnPointerExit()
		{
		}

		// Token: 0x06006EF2 RID: 28402 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnPointerDown()
		{
		}

		// Token: 0x06006EF3 RID: 28403 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnPointerUp()
		{
		}

		// Token: 0x06006EF4 RID: 28404 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetPhaseButton()
		{
		}

		// Token: 0x06006EF5 RID: 28405 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetActive(bool active)
		{
		}

		// Token: 0x06006EF6 RID: 28406 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetMaterialParam(string paramname, float value)
		{
		}

		// Token: 0x06006EF7 RID: 28407 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateMovablePhaseIcon()
		{
		}

		// Token: 0x06006EF8 RID: 28408 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize(DuelClient host)
		{
		}

		// Token: 0x06006EF9 RID: 28409 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x06006EFA RID: 28410 RVA: 0x0000216D File Offset: 0x0000036D
		private void Terminate()
		{
		}

		// Token: 0x0400A9B7 RID: 43447
		private const string PATH_BUTTON = "Duel/BG/Timer/Timer_c001/PhaseButton_c001";

		// Token: 0x0400A9B8 RID: 43448
		private const string PATH_BUTTON_SP = "Duel/BG/Timer/Timer_013/PhaseButton_013";

		// Token: 0x0400A9B9 RID: 43449
		private const string LABEL_EO_TEXTPHASE = "Text";

		// Token: 0x0400A9BA RID: 43450
		private const string LABEL_EO_TEXTSTEP = "Text02";

		// Token: 0x0400A9BB RID: 43451
		private const string LABEL_EO_TEXTTURN = "Text03";

		// Token: 0x0400A9BC RID: 43452
		private const string LABEL_EO_BUTTON = "Button";

		// Token: 0x0400A9BD RID: 43453
		private const string LABEL_EO_PLAYERBASE = "PlayerBase";

		// Token: 0x0400A9BE RID: 43454
		private const string LABEL_EO_PLAYERSURFACE = "PlayerSurface";

		// Token: 0x0400A9BF RID: 43455
		private const string LABEL_EO_OPPONENTBASE = "OpponentBase";

		// Token: 0x0400A9C0 RID: 43456
		private const string LABEL_EO_OPPONENTSURFACE = "OpponentSurface";

		// Token: 0x0400A9C1 RID: 43457
		private const string LABEL_EO_GUIDEFAR = "PlayableGuide_far";

		// Token: 0x0400A9C2 RID: 43458
		private const string LABEL_EO_GUIDENEAR = "PlayableGuide_near";

		// Token: 0x0400A9C3 RID: 43459
		private const string LABEL_EO_PADICON = "Position2";

		// Token: 0x0400A9C4 RID: 43460
		private const string LABEL_EO_POSITION = "Position";

		// Token: 0x0400A9C5 RID: 43461
		private const string LABEL_EO_HINTEFFECT = "HintEffect";

		// Token: 0x0400A9C6 RID: 43462
		private const string LABEL_TWEEN_TEXTZOOMOUT = "TextZoomOut";

		// Token: 0x0400A9C7 RID: 43463
		private const string LABEL_TWEEN_SWITCHTURN0 = "SwitchTurn0";

		// Token: 0x0400A9C8 RID: 43464
		private const string LABEL_TWEEN_SWITCHTURN1 = "SwitchTurn1";

		// Token: 0x0400A9C9 RID: 43465
		private const string LABEL_TWEEN_MOUSEOVERIN = "MouseOverIn";

		// Token: 0x0400A9CA RID: 43466
		private const string LABEL_TWEEN_MOUSEOVEROUT = "MouseOverOut";

		// Token: 0x0400A9CB RID: 43467
		private const string LABEL_TWEEN_PRESSBUTTONIN = "PressButtonIn";

		// Token: 0x0400A9CC RID: 43468
		private const string LABEL_TWEEN_PRESSBUTTONOUT = "PressButtonOut";

		// Token: 0x0400A9CD RID: 43469
		private const string LABEL_TWEEN_ACTIVEON = "ActiveOn";

		// Token: 0x0400A9CE RID: 43470
		private const string LABEL_TWEEN_ACTIVEOFF = "ActiveOff";

		// Token: 0x0400A9CF RID: 43471
		private const string LABEL_SHADER_SWITCHTURN = "_SwitchTurn";

		// Token: 0x0400A9D0 RID: 43472
		private const string LABEL_SHADER_ACTIVE = "_Active";

		// Token: 0x0400A9D1 RID: 43473
		private const string LABEL_TEXT_START = "S";

		// Token: 0x0400A9D2 RID: 43474
		private const string LABEL_TEXT_DAMAGE1 = "D1";

		// Token: 0x0400A9D3 RID: 43475
		private const string LABEL_TEXT_DAMAGE2 = "D2";

		// Token: 0x0400A9D4 RID: 43476
		private const string LABEL_TEXT_DAMAGE3 = "D3";

		// Token: 0x0400A9D5 RID: 43477
		private const string LABEL_TEXT_DAMAGE4 = "D4";

		// Token: 0x0400A9D6 RID: 43478
		private const string LABEL_TEXT_DAMAGE5 = "D5";

		// Token: 0x0400A9D7 RID: 43479
		private const string LABEL_TEXT_END = "E";

		// Token: 0x0400A9D8 RID: 43480
		private const string PATH_PHASECHANGE_DP0 = "Duel/Timeline/Duel/Universal/DuelPhase/ACDuelDrawPhase_near";

		// Token: 0x0400A9D9 RID: 43481
		private const string PATH_PHASECHANGE_DP1 = "Duel/Timeline/Duel/Universal/DuelPhase/ACDuelDrawPhase_far";

		// Token: 0x0400A9DA RID: 43482
		private const string PATH_PHASECHANGE_SP0 = "Duel/Timeline/Duel/Universal/DuelPhase/ACDuelStanbyPhase_near";

		// Token: 0x0400A9DB RID: 43483
		private const string PATH_PHASECHANGE_SP1 = "Duel/Timeline/Duel/Universal/DuelPhase/ACDuelStanbyPhase_far";

		// Token: 0x0400A9DC RID: 43484
		private const string PATH_PHASECHANGE_MP0 = "Duel/Timeline/Duel/Universal/DuelPhase/ACDuelMain01Phase_near";

		// Token: 0x0400A9DD RID: 43485
		private const string PATH_PHASECHANGE_MP1 = "Duel/Timeline/Duel/Universal/DuelPhase/ACDuelMain01Phase_far";

		// Token: 0x0400A9DE RID: 43486
		private const string PATH_PHASECHANGE_BP0 = "Duel/Timeline/Duel/Universal/DuelPhase/ACDuelBattlePhase_near";

		// Token: 0x0400A9DF RID: 43487
		private const string PATH_PHASECHANGE_BP1 = "Duel/Timeline/Duel/Universal/DuelPhase/ACDuelBattlePhase_far";

		// Token: 0x0400A9E0 RID: 43488
		private const string PATH_PHASECHANGE_M2P0 = "Duel/Timeline/Duel/Universal/DuelPhase/ACDuelMain02Phase_near";

		// Token: 0x0400A9E1 RID: 43489
		private const string PATH_PHASECHANGE_M2P1 = "Duel/Timeline/Duel/Universal/DuelPhase/ACDuelMain02Phase_far";

		// Token: 0x0400A9E2 RID: 43490
		private const string PATH_PHASECHANGE_EP0 = "Duel/Timeline/Duel/Universal/DuelPhase/ACDuelEndPhase_near";

		// Token: 0x0400A9E3 RID: 43491
		private const string PATH_PHASECHANGE_EP1 = "Duel/Timeline/Duel/Universal/DuelPhase/ACDuelEndPhase_far";

		// Token: 0x0400A9E4 RID: 43492
		private const string TAG_TURN = "Turn";

		// Token: 0x0400A9E5 RID: 43493
		private const string TAG_PHASE = "Phase";

		// Token: 0x0400A9E6 RID: 43494
		private const string TAG_BATTLE_NEAR = "BattlePhase_near";

		// Token: 0x0400A9E7 RID: 43495
		private const string TAG_BATTLE_FAR = "BattlePhase_far";

		// Token: 0x0400A9E8 RID: 43496
		public bool duelStart;

		// Token: 0x0400A9E9 RID: 43497
		public bool duelEnd;

		// Token: 0x0400A9EA RID: 43498
		public UnityAction onPhaseChangedCallBack;

		// Token: 0x0400A9EB RID: 43499
		public UnityAction<bool> onChangeActive;

		// Token: 0x0400A9EC RID: 43500
		private ElementObjectManager m_RootManager;

		// Token: 0x0400A9ED RID: 43501
		private SelectionButton m_Button;

		// Token: 0x0400A9EE RID: 43502
		private Tween m_TweenSurface0;

		// Token: 0x0400A9EF RID: 43503
		private Tween m_TweenBase0;

		// Token: 0x0400A9F0 RID: 43504
		private Tween m_TweenSurface1;

		// Token: 0x0400A9F1 RID: 43505
		private Tween m_TweenBase1;

		// Token: 0x0400A9F2 RID: 43506
		private Tween m_TweenText;

		// Token: 0x0400A9F3 RID: 43507
		private MeshRenderer m_RendererSurface0;

		// Token: 0x0400A9F4 RID: 43508
		private MeshRenderer m_RendererSurface1;

		// Token: 0x0400A9F5 RID: 43509
		private MeshRenderer m_RendererBase0;

		// Token: 0x0400A9F6 RID: 43510
		private MeshRenderer m_RendererBase1;

		// Token: 0x0400A9F7 RID: 43511
		private ExtendedTextMeshPro m_PhaseName;

		// Token: 0x0400A9F8 RID: 43512
		private ExtendedTextMeshPro m_StepName;

		// Token: 0x0400A9F9 RID: 43513
		private ExtendedTextMeshPro m_TurnNum;

		// Token: 0x0400A9FA RID: 43514
		private Transform m_PadIcon;

		// Token: 0x0400A9FB RID: 43515
		private Transform m_Position;

		// Token: 0x0400A9FC RID: 43516
		private Transform m_HintEffect;

		// Token: 0x0400A9FD RID: 43517
		private Engine.Phase m_NextPhase;

		// Token: 0x0400A9FE RID: 43518
		private bool m_Pressing;

		// Token: 0x0400A9FF RID: 43519
		private bool m_Entering;

		// Token: 0x0400AA00 RID: 43520
		private Dictionary<Engine.StepType, string> m_BStepTextDict;

		// Token: 0x0400AA01 RID: 43521
		private Dictionary<string, Color> m_StepColorDict;

		// Token: 0x0400AA02 RID: 43522
		private Dictionary<Engine.DmgStepType, string> m_DStepTextDict;

		// Token: 0x0400AA03 RID: 43523
		private Dictionary<Engine.Phase, string> m_PhaseTimelineDict0;

		// Token: 0x0400AA04 RID: 43524
		private Dictionary<Engine.Phase, string> m_PhaseTImelineDict1;

		// Token: 0x0400AA05 RID: 43525
		private Dictionary<Engine.Phase, string> m_PhaseSoundDict0;

		// Token: 0x0400AA06 RID: 43526
		private Dictionary<Engine.Phase, string> m_PhaseSoundDict1;

		// Token: 0x0400AA07 RID: 43527
		private DuelClient m_Host;

		// Token: 0x0400AA08 RID: 43528
		private Queue<UnityAction> m_TaskQueue;

		// Token: 0x0400AA09 RID: 43529
		private TimelineObject m_CurrentTimeline;
	}
}
