using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000D67 RID: 3431
	public class DuelEndOperation
	{
		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x060063F5 RID: 25589 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060063F6 RID: 25590 RVA: 0x0000216D File Offset: 0x0000036D
		public bool finished
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

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x060063F7 RID: 25591 RVA: 0x000029CC File Offset: 0x00000BCC
		private int winner
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x060063F8 RID: 25592 RVA: 0x000029CC File Offset: 0x00000BCC
		private int loser
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060063F9 RID: 25593 RVA: 0x0000216A File Offset: 0x0000036A
		public static DuelEndOperation Create()
		{
			return null;
		}

		// Token: 0x060063FA RID: 25594 RVA: 0x0000216D File Offset: 0x0000036D
		public void Setup(RunEffectWorker worker, Engine.ResultType resultType, Engine.FinishType finishType)
		{
		}

		// Token: 0x060063FB RID: 25595 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetupError(RunEffectWorker worker)
		{
		}

		// Token: 0x060063FC RID: 25596 RVA: 0x0000216D File Offset: 0x0000036D
		public void Update()
		{
		}

		// Token: 0x060063FD RID: 25597 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateEffect()
		{
		}

		// Token: 0x060063FE RID: 25598 RVA: 0x0000216D File Offset: 0x0000036D
		private void RequestPreLoad()
		{
		}

		// Token: 0x060063FF RID: 25599 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool WaitCardEffectStep()
		{
			return false;
		}

		// Token: 0x06006400 RID: 25600 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool InitWinLoseStep()
		{
			return false;
		}

		// Token: 0x06006401 RID: 25601 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool WaitSpecialWin()
		{
			return false;
		}

		// Token: 0x06006402 RID: 25602 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool InitDeckOutStep()
		{
			return false;
		}

		// Token: 0x06006403 RID: 25603 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitDeckOutStep()
		{
		}

		// Token: 0x06006404 RID: 25604 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool InitMateMotionStep()
		{
			return false;
		}

		// Token: 0x06006405 RID: 25605 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayWinLose()
		{
		}

		// Token: 0x06006406 RID: 25606 RVA: 0x0000216D File Offset: 0x0000036D
		private void StartEndMessage()
		{
		}

		// Token: 0x06006407 RID: 25607 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitWinLoseStep()
		{
		}

		// Token: 0x06006408 RID: 25608 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitWinLoseStep2()
		{
		}

		// Token: 0x06006409 RID: 25609 RVA: 0x0000216D File Offset: 0x0000036D
		private void FadeOutStep()
		{
		}

		// Token: 0x0600640A RID: 25610 RVA: 0x0000216D File Offset: 0x0000036D
		private void FinishStep()
		{
		}

		// Token: 0x0600640B RID: 25611 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateError()
		{
		}

		// Token: 0x0600640C RID: 25612 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitCardEffectErrorStep()
		{
		}

		// Token: 0x0600640D RID: 25613 RVA: 0x0000216D File Offset: 0x0000036D
		private void FinishErrorStep()
		{
		}

		// Token: 0x0600640E RID: 25614 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsSpecialWin(Engine.FinishType finishType)
		{
			return false;
		}

		// Token: 0x0600640F RID: 25615 RVA: 0x0000216A File Offset: 0x0000036A
		private string GetWinLoseTimelinePath(Engine.FinishType finishType, Engine.ResultType resultType)
		{
			return null;
		}

		// Token: 0x06006410 RID: 25616 RVA: 0x0000216D File Offset: 0x0000036D
		public void SelectEndMessageBtn()
		{
		}

		// Token: 0x06006411 RID: 25617 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnDestroy()
		{
		}

		// Token: 0x04009EB0 RID: 40624
		private RunEffectWorker worker;

		// Token: 0x04009EB1 RID: 40625
		private DuelEndOperation.Mode mode;

		// Token: 0x04009EB2 RID: 40626
		private DuelEndOperation.EffectStep step;

		// Token: 0x04009EB3 RID: 40627
		private DuelEndOperation.ErrorStep errorStep;

		// Token: 0x04009EB4 RID: 40628
		private Engine.ResultType resultType;

		// Token: 0x04009EB5 RID: 40629
		private Engine.FinishType finishType;

		// Token: 0x04009EB6 RID: 40630
		private DuelEndMessage msgObj;

		// Token: 0x04009EB7 RID: 40631
		private float autoFinishTimer;

		// Token: 0x04009EB8 RID: 40632
		private string userNameMyself;

		// Token: 0x04009EB9 RID: 40633
		private string userNameRival;

		// Token: 0x04009EBA RID: 40634
		private int iconIDMyself;

		// Token: 0x04009EBB RID: 40635
		private int iconIDRival;

		// Token: 0x04009EBC RID: 40636
		private int frameIDMyself;

		// Token: 0x04009EBD RID: 40637
		private int frameIDRival;

		// Token: 0x04009EBE RID: 40638
		private int myselfid;

		// Token: 0x04009EBF RID: 40639
		private int rivalid;

		// Token: 0x04009EC0 RID: 40640
		private string onlineIDMyself;

		// Token: 0x04009EC1 RID: 40641
		private string onlineIDRival;

		// Token: 0x04009EC2 RID: 40642
		private bool sameOSMyself;

		// Token: 0x04009EC3 RID: 40643
		private bool sameOSRival;

		// Token: 0x04009EC4 RID: 40644
		private int prepareCounter;

		// Token: 0x04009EC5 RID: 40645
		private GameObject animation;

		// Token: 0x04009EC6 RID: 40646
		private string endReasonFormat;

		// Token: 0x04009EC7 RID: 40647
		private bool isOnlineMode;

		// Token: 0x04009EC8 RID: 40648
		private bool isReplayMode;

		// Token: 0x04009EC9 RID: 40649
		private bool isAudienceMode;

		// Token: 0x04009ECA RID: 40650
		private bool isShowRetry;

		// Token: 0x04009ECB RID: 40651
		private bool winMyself;

		// Token: 0x04009ECC RID: 40652
		private bool winRival;

		// Token: 0x04009ECD RID: 40653
		private bool isDuelLiveContinuous;

		// Token: 0x04009ECE RID: 40654
		private bool isForceNoProfileCard;

		// Token: 0x04009ECF RID: 40655
		private bool endMessageStarted;

		// Token: 0x04009ED0 RID: 40656
		private bool hidePlayerID;

		// Token: 0x04009ED1 RID: 40657
		private Dictionary<string, object> profileDataMyself;

		// Token: 0x04009ED2 RID: 40658
		private Dictionary<string, object> profileDataRival;

		// Token: 0x04009ED3 RID: 40659
		private const float duelLiveAutoLeaveTime = 3f;

		// Token: 0x02000D68 RID: 3432
		private enum Mode
		{
			// Token: 0x04009ED5 RID: 40661
			DuelEndEffect,
			// Token: 0x04009ED6 RID: 40662
			Error
		}

		// Token: 0x02000D69 RID: 3433
		private enum EffectStep
		{
			// Token: 0x04009ED8 RID: 40664
			WaitCardEffect,
			// Token: 0x04009ED9 RID: 40665
			InitWinLose,
			// Token: 0x04009EDA RID: 40666
			WaitSpecialWin,
			// Token: 0x04009EDB RID: 40667
			InitDeckOut,
			// Token: 0x04009EDC RID: 40668
			WaitDeckOut,
			// Token: 0x04009EDD RID: 40669
			InitMateMotion,
			// Token: 0x04009EDE RID: 40670
			WaitMateMotion,
			// Token: 0x04009EDF RID: 40671
			WaitWinLose,
			// Token: 0x04009EE0 RID: 40672
			WaitWinLose2,
			// Token: 0x04009EE1 RID: 40673
			FadeOut,
			// Token: 0x04009EE2 RID: 40674
			Finish
		}

		// Token: 0x02000D6A RID: 3434
		private enum ErrorStep
		{
			// Token: 0x04009EE4 RID: 40676
			WaitCardEffect,
			// Token: 0x04009EE5 RID: 40677
			WaitErrorDialog,
			// Token: 0x04009EE6 RID: 40678
			Finish
		}
	}
}
