using System;
using System.Collections.Generic;

namespace YgomGame.Duel
{
	// Token: 0x02000DCB RID: 3531
	public class EffectTaskCardMove : EffectTask
	{
		// Token: 0x0600674A RID: 26442 RVA: 0x0000216D File Offset: 0x0000036D
		public static void MinimumEffect(RunEffectWorker worker, int param1, int param2, int param3)
		{
		}

		// Token: 0x0600674B RID: 26443 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> PreCreate(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x0600674C RID: 26444 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
		{
			return null;
		}

		// Token: 0x0600674D RID: 26445 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskCardMove(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
			: base(null)
		{
		}

		// Token: 0x0600674E RID: 26446 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x0600674F RID: 26447 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool WaitCardMoveStep()
		{
			return false;
		}

		// Token: 0x06006750 RID: 26448 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool WaitLoadCardStep()
		{
			return false;
		}

		// Token: 0x06006751 RID: 26449 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool StartMoveStep()
		{
			return false;
		}

		// Token: 0x06006752 RID: 26450 RVA: 0x0000216D File Offset: 0x0000036D
		private void CommonMove()
		{
		}

		// Token: 0x06006753 RID: 26451 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnFinished()
		{
		}

		// Token: 0x06006754 RID: 26452 RVA: 0x0000216D File Offset: 0x0000036D
		private void FinishStep()
		{
		}

		// Token: 0x06006755 RID: 26453 RVA: 0x000029CC File Offset: 0x00000BCC
		private EffectTaskCardMove.LandingType GetLandingType()
		{
			return EffectTaskCardMove.LandingType.NORMAL;
		}

		// Token: 0x06006756 RID: 26454 RVA: 0x000F5D94 File Offset: 0x000F3F94
		public static EffectTaskCardMove.LandingEffectInfo GetLandingEffectInfo(EffectTaskCardMove.LandingType landingType, EffectTaskCardMove.SummonEffectType summonEffectType, Engine.CardMoveType moveType)
		{
			return default(EffectTaskCardMove.LandingEffectInfo);
		}

		// Token: 0x06006757 RID: 26455 RVA: 0x0000216D File Offset: 0x0000036D
		private void ReqLandingEffect()
		{
		}

		// Token: 0x06006758 RID: 26456 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsSamePosition(int positionA, int positionB)
		{
			return false;
		}

		// Token: 0x0400A1EA RID: 41450
		private bool finished;

		// Token: 0x0400A1EB RID: 41451
		private EffectTaskCardMove.Step step;

		// Token: 0x0400A1EC RID: 41452
		private Engine.CardStatus from;

		// Token: 0x0400A1ED RID: 41453
		private Engine.CardStatus to;

		// Token: 0x0400A1EE RID: 41454
		private Engine.CardMoveType moveType;

		// Token: 0x0400A1EF RID: 41455
		private int uniqueId;

		// Token: 0x0400A1F0 RID: 41456
		private int cardId;

		// Token: 0x0400A1F1 RID: 41457
		private int toTopUniqueID;

		// Token: 0x0400A1F2 RID: 41458
		private int fromTopUniqueID;

		// Token: 0x0400A1F3 RID: 41459
		private int directFlag;

		// Token: 0x0400A1F4 RID: 41460
		private CardRoot cardRoot;

		// Token: 0x0400A1F5 RID: 41461
		private bool camMoved;

		// Token: 0x0400A1F6 RID: 41462
		private CardPlace fromPlace;

		// Token: 0x0400A1F7 RID: 41463
		private CardPlace toPlace;

		// Token: 0x0400A1F8 RID: 41464
		private BezierMotionSetting[] motionList;

		// Token: 0x0400A1F9 RID: 41465
		private string motionSeCode;

		// Token: 0x0400A1FA RID: 41466
		private bool toFace;

		// Token: 0x0400A1FB RID: 41467
		private CardLocator fromLocator;

		// Token: 0x0400A1FC RID: 41468
		private CardLocator toLocator;

		// Token: 0x0400A1FD RID: 41469
		private CardLocator summonLocator;

		// Token: 0x0400A1FE RID: 41470
		private CardRoot.ModelType toModelType;

		// Token: 0x0400A1FF RID: 41471
		private CardRoot.ModelType fromModelType;

		// Token: 0x0400A200 RID: 41472
		private bool isSoul;

		// Token: 0x0400A201 RID: 41473
		private EffectTaskCardMove.LandingType landingType;

		// Token: 0x0400A202 RID: 41474
		private bool moveFromScreenCenter;

		// Token: 0x0400A203 RID: 41475
		private bool isAttacker;

		// Token: 0x0400A204 RID: 41476
		private bool isAttackTarget;

		// Token: 0x0400A205 RID: 41477
		private int attackerPlayer;

		// Token: 0x0400A206 RID: 41478
		private int attackerPosition;

		// Token: 0x0400A207 RID: 41479
		private int attackTargetPlayer;

		// Token: 0x0400A208 RID: 41480
		private int attackTargetPosition;

		// Token: 0x0400A209 RID: 41481
		private SummonEffectBase summonEffect;

		// Token: 0x0400A20A RID: 41482
		private bool waitZoneEffect;

		// Token: 0x02000DCC RID: 3532
		private enum Step
		{
			// Token: 0x0400A20C RID: 41484
			WaitCardMove,
			// Token: 0x0400A20D RID: 41485
			WaitLoadCard,
			// Token: 0x0400A20E RID: 41486
			WaitSetCard,
			// Token: 0x0400A20F RID: 41487
			StartMove,
			// Token: 0x0400A210 RID: 41488
			WaitMove,
			// Token: 0x0400A211 RID: 41489
			Finish
		}

		// Token: 0x02000DCD RID: 3533
		public enum LandingType
		{
			// Token: 0x0400A213 RID: 41491
			NORMAL,
			// Token: 0x0400A214 RID: 41492
			SUMMON_LOW,
			// Token: 0x0400A215 RID: 41493
			SUMMON_MIDDLE,
			// Token: 0x0400A216 RID: 41494
			SUMMON_HIGH,
			// Token: 0x0400A217 RID: 41495
			MAGIC,
			// Token: 0x0400A218 RID: 41496
			MAGIC_SET
		}

		// Token: 0x02000DCE RID: 3534
		public enum SummonEffectType
		{
			// Token: 0x0400A21A RID: 41498
			None,
			// Token: 0x0400A21B RID: 41499
			Advance,
			// Token: 0x0400A21C RID: 41500
			Fusion,
			// Token: 0x0400A21D RID: 41501
			Ritual,
			// Token: 0x0400A21E RID: 41502
			Synchro,
			// Token: 0x0400A21F RID: 41503
			Xyz,
			// Token: 0x0400A220 RID: 41504
			Pendulum,
			// Token: 0x0400A221 RID: 41505
			Link
		}

		// Token: 0x02000DCF RID: 3535
		public struct LandingEffectInfo
		{
			// Token: 0x0400A222 RID: 41506
			public bool useEffect;

			// Token: 0x0400A223 RID: 41507
			public DuelEffectPool.Type effectType;

			// Token: 0x0400A224 RID: 41508
			public bool useEffectImpact;

			// Token: 0x0400A225 RID: 41509
			public DuelEffectPool.Type effectTypeImpact;

			// Token: 0x0400A226 RID: 41510
			public bool useEffectSummon;

			// Token: 0x0400A227 RID: 41511
			public DuelEffectPool.Type effectTypeSummon;

			// Token: 0x0400A228 RID: 41512
			public string shakeLabel;

			// Token: 0x0400A229 RID: 41513
			public string seLabel;

			// Token: 0x0400A22A RID: 41514
			public float effectTime;
		}
	}
}
