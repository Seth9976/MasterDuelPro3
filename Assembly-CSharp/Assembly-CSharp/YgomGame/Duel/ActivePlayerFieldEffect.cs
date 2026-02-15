using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using YgomSystem.ElementSystem;

namespace YgomGame.Duel
{
	// Token: 0x02000C73 RID: 3187
	public class ActivePlayerFieldEffect : MonoBehaviour
	{
		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x06005B57 RID: 23383 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005B58 RID: 23384 RVA: 0x0000216D File Offset: 0x0000036D
		public int currentActivePlayer
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x06005B59 RID: 23385 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isInitialized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x06005B5A RID: 23386 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005B5B RID: 23387 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x06005B5C RID: 23388 RVA: 0x0000216A File Offset: 0x0000036A
		public static ActivePlayerFieldEffect Create(DuelGameObjectManager goManager, GameObject root, string name)
		{
			return null;
		}

		// Token: 0x06005B5D RID: 23389 RVA: 0x0000216D File Offset: 0x0000036D
		public void InitGuide(int firstPlayer)
		{
		}

		// Token: 0x06005B5E RID: 23390 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnDuelStart()
		{
		}

		// Token: 0x06005B5F RID: 23391 RVA: 0x0000216D File Offset: 0x0000036D
		public void SwitchGuide(int team, bool forceswitch = false)
		{
		}

		// Token: 0x06005B60 RID: 23392 RVA: 0x0000216D File Offset: 0x0000036D
		public void TurnPhaseChange()
		{
		}

		// Token: 0x06005B61 RID: 23393 RVA: 0x0000216D File Offset: 0x0000036D
		public void FinishGuide()
		{
		}

		// Token: 0x06005B62 RID: 23394 RVA: 0x0000216D File Offset: 0x0000036D
		protected void Initialize()
		{
		}

		// Token: 0x06005B63 RID: 23395 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetGuideNotice()
		{
		}

		// Token: 0x06005B64 RID: 23396 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitInitializeStep()
		{
		}

		// Token: 0x06005B65 RID: 23397 RVA: 0x0000216D File Offset: 0x0000036D
		private void DuelStep()
		{
		}

		// Token: 0x06005B66 RID: 23398 RVA: 0x0000216D File Offset: 0x0000036D
		private void TerminatingStep()
		{
		}

		// Token: 0x06005B67 RID: 23399 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06005B68 RID: 23400 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateHintEffect()
		{
		}

		// Token: 0x06005B69 RID: 23401 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool CheckIsShow()
		{
			return false;
		}

		// Token: 0x06005B6A RID: 23402 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetGuideEnable(bool isnear, bool enable, bool turnchange = false)
		{
		}

		// Token: 0x06005B6B RID: 23403 RVA: 0x0000216D File Offset: 0x0000036D
		public void Terminate()
		{
		}

		// Token: 0x0400967F RID: 38527
		private const string PATH_GUIDE_NEAR = "Duel/BG/Timer/PlayableGuide_c001/PlayableGuide_c001_near";

		// Token: 0x04009680 RID: 38528
		private const string PATH_GUIDE_FAR = "Duel/BG/Timer/PlayableGuide_c001/PlayableGuide_c001_far";

		// Token: 0x04009681 RID: 38529
		private const string LABEL_TRIGGER_APPEAR = "Apper";

		// Token: 0x04009682 RID: 38530
		private const string LABEL_TRIGGER_NOTICE = "Notice";

		// Token: 0x04009683 RID: 38531
		private const string LABEL_TRIGGER_CHANGE = "Change";

		// Token: 0x04009684 RID: 38532
		private const string LABEL_TRIGGER_OUT = "Out";

		// Token: 0x04009685 RID: 38533
		private const string LABEL_TRIGGER_END = "End";

		// Token: 0x04009686 RID: 38534
		private const float WAIT_INPUT_NOTICE_INTERVAL = 15f;

		// Token: 0x04009687 RID: 38535
		private const int LATENCY_THRESHOLD = 127;

		// Token: 0x04009688 RID: 38536
		private Animator m_GuideNear;

		// Token: 0x04009689 RID: 38537
		private Animator m_GuideFar;

		// Token: 0x0400968A RID: 38538
		private ElementObjectManager m_EOManager;

		// Token: 0x0400968B RID: 38539
		private bool m_IsNearReady;

		// Token: 0x0400968C RID: 38540
		private bool m_IsFarReady;

		// Token: 0x0400968D RID: 38541
		private ActivePlayerFieldEffect.DelayAction m_DelayAction;

		// Token: 0x0400968E RID: 38542
		private ActivePlayerFieldEffect.Step m_Step;

		// Token: 0x0400968F RID: 38543
		private float m_WaitInputTime;

		// Token: 0x04009690 RID: 38544
		public bool IsShow;

		// Token: 0x02000C74 RID: 3188
		private enum PlauableGuideTrigger
		{
			// Token: 0x04009692 RID: 38546
			CHANGE,
			// Token: 0x04009693 RID: 38547
			APPEAR,
			// Token: 0x04009694 RID: 38548
			OUT
		}

		// Token: 0x02000C75 RID: 3189
		private enum Step
		{
			// Token: 0x04009696 RID: 38550
			WaitInitialize,
			// Token: 0x04009697 RID: 38551
			Idle,
			// Token: 0x04009698 RID: 38552
			Duel,
			// Token: 0x04009699 RID: 38553
			Terminating
		}

		// Token: 0x02000C76 RID: 3190
		private class DelayAction
		{
			// Token: 0x1700099F RID: 2463
			// (get) Token: 0x06005B6D RID: 23405 RVA: 0x000029CC File Offset: 0x00000BCC
			private bool isOnTime
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06005B6E RID: 23406 RVA: 0x000029CC File Offset: 0x00000BCC
			private bool IsPlayerActive(bool isinterrupt)
			{
				return false;
			}

			// Token: 0x06005B6F RID: 23407 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetAction(UnityAction<int, bool> action)
			{
			}

			// Token: 0x06005B70 RID: 23408 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetTeamInstant(int turnplayer)
			{
			}

			// Token: 0x06005B71 RID: 23409 RVA: 0x0000216D File Offset: 0x0000036D
			public void ToInterrupt(bool isLongDelay)
			{
			}

			// Token: 0x06005B72 RID: 23410 RVA: 0x0000216D File Offset: 0x0000036D
			public void ToTurnPlayer(bool forceswitch, bool isLongDelay)
			{
			}

			// Token: 0x06005B73 RID: 23411 RVA: 0x0000216D File Offset: 0x0000036D
			public void Update()
			{
			}

			// Token: 0x0400969A RID: 38554
			private const float DELAYTIME_S = 1f;

			// Token: 0x0400969B RID: 38555
			private const float DELAYTIME_L = 2f;

			// Token: 0x0400969C RID: 38556
			private UnityAction<int, bool> action;

			// Token: 0x0400969D RID: 38557
			private bool changeflag;

			// Token: 0x0400969E RID: 38558
			private float delaytime;

			// Token: 0x0400969F RID: 38559
			private bool isInterrupted;
		}
	}
}
