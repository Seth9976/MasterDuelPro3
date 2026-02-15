using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000DC8 RID: 3528
	public class EffectTaskCardLockon : EffectTask
	{
		// Token: 0x06006741 RID: 26433 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> PreCreate(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x06006742 RID: 26434 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
		{
			return null;
		}

		// Token: 0x06006743 RID: 26435 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskCardLockon(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
			: base(null)
		{
		}

		// Token: 0x06006744 RID: 26436 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x06006745 RID: 26437 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitCardMoveStep()
		{
		}

		// Token: 0x06006746 RID: 26438 RVA: 0x0000216D File Offset: 0x0000036D
		private void MoveFrontStep()
		{
		}

		// Token: 0x06006747 RID: 26439 RVA: 0x0000216D File Offset: 0x0000036D
		private void CardShowStep()
		{
		}

		// Token: 0x06006748 RID: 26440 RVA: 0x0000216D File Offset: 0x0000036D
		private void MoveBackStep()
		{
		}

		// Token: 0x06006749 RID: 26441 RVA: 0x0000216D File Offset: 0x0000036D
		private void FinishStep()
		{
		}

		// Token: 0x0400A1CC RID: 41420
		private bool finished;

		// Token: 0x0400A1CD RID: 41421
		private EffectTaskCardLockon.Step step;

		// Token: 0x0400A1CE RID: 41422
		private CardPlace cardPlace;

		// Token: 0x0400A1CF RID: 41423
		private CardRoot cardRoot;

		// Token: 0x0400A1D0 RID: 41424
		private bool tempCard;

		// Token: 0x0400A1D1 RID: 41425
		private int team;

		// Token: 0x0400A1D2 RID: 41426
		private int position;

		// Token: 0x0400A1D3 RID: 41427
		private int index;

		// Token: 0x0400A1D4 RID: 41428
		private int uniqueID;

		// Token: 0x0400A1D5 RID: 41429
		private bool exist;

		// Token: 0x0400A1D6 RID: 41430
		private EffectTaskCardLockon.Type type;

		// Token: 0x0400A1D7 RID: 41431
		private float time;

		// Token: 0x0400A1D8 RID: 41432
		private const float MOVE_TIME = 0.375f;

		// Token: 0x0400A1D9 RID: 41433
		private const float WAIT_TIME = 0.5f;

		// Token: 0x0400A1DA RID: 41434
		private Vector3 defaultPos;

		// Token: 0x0400A1DB RID: 41435
		private Vector3 dstPos;

		// Token: 0x0400A1DC RID: 41436
		private Quaternion defaultQuat;

		// Token: 0x0400A1DD RID: 41437
		private Quaternion dstQuat;

		// Token: 0x02000DC9 RID: 3529
		private enum Step
		{
			// Token: 0x0400A1DF RID: 41439
			WaitCardLoad,
			// Token: 0x0400A1E0 RID: 41440
			WaitCardMove,
			// Token: 0x0400A1E1 RID: 41441
			WaitEffect,
			// Token: 0x0400A1E2 RID: 41442
			MoveFront,
			// Token: 0x0400A1E3 RID: 41443
			CardShow,
			// Token: 0x0400A1E4 RID: 41444
			MoveBack,
			// Token: 0x0400A1E5 RID: 41445
			Finish
		}

		// Token: 0x02000DCA RID: 3530
		private enum Type
		{
			// Token: 0x0400A1E7 RID: 41447
			Target,
			// Token: 0x0400A1E8 RID: 41448
			NotTarget,
			// Token: 0x0400A1E9 RID: 41449
			Show
		}
	}
}
