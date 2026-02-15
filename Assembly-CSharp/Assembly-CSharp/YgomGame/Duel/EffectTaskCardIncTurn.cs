using System;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000DC7 RID: 3527
	public class EffectTaskCardIncTurn : EffectTask
	{
		// Token: 0x0600673C RID: 26428 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker workerHUD, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x0600673D RID: 26429 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskCardIncTurn(RunEffectWorker workerHUD, int param1, int param2, int param3)
			: base(null)
		{
		}

		// Token: 0x0600673E RID: 26430 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x0600673F RID: 26431 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnDestroy()
		{
		}

		// Token: 0x06006740 RID: 26432 RVA: 0x0000216D File Offset: 0x0000036D
		private void DestroyTurnCount()
		{
		}

		// Token: 0x0400A1C7 RID: 41415
		private bool finished;

		// Token: 0x0400A1C8 RID: 41416
		private GameObject obj;

		// Token: 0x0400A1C9 RID: 41417
		private DuelTurnCount turnCount;

		// Token: 0x0400A1CA RID: 41418
		private const string prefabPath = "Prefabs/Duel/DuelTurnCount";

		// Token: 0x0400A1CB RID: 41419
		private float waitTime;
	}
}
