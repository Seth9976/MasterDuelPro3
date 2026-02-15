using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace YgomGame.Duel
{
	// Token: 0x02000EEF RID: 3823
	public class RunDice
	{
		// Token: 0x06006F73 RID: 28531 RVA: 0x00002739 File Offset: 0x00000939
		public RunDice(int numThrows, int number, bool myself)
		{
		}

		// Token: 0x06006F74 RID: 28532 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool Update(bool isSkip)
		{
			return false;
		}

		// Token: 0x06006F75 RID: 28533 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool UpdateDice(RunDice.Dice dice, bool isSkip)
		{
			return false;
		}

		// Token: 0x06006F76 RID: 28534 RVA: 0x0000216D File Offset: 0x0000036D
		public void Terminate()
		{
		}

		// Token: 0x0400AA63 RID: 43619
		private bool myself;

		// Token: 0x0400AA64 RID: 43620
		private static Vector3[] rotsMyself;

		// Token: 0x0400AA65 RID: 43621
		private static Vector3[] rotsRival;

		// Token: 0x0400AA66 RID: 43622
		public RunDice.STEP step;

		// Token: 0x0400AA67 RID: 43623
		private List<RunDice.Dice> dices;

		// Token: 0x0400AA68 RID: 43624
		private const float TOTAL_FRAME = 80f;

		// Token: 0x0400AA69 RID: 43625
		private const float N_STABLE_FRAME = 0.675f;

		// Token: 0x02000EF0 RID: 3824
		public enum STEP
		{
			// Token: 0x0400AA6B RID: 43627
			MAIN,
			// Token: 0x0400AA6C RID: 43628
			END
		}

		// Token: 0x02000EF1 RID: 3825
		private class Dice
		{
			// Token: 0x0400AA6D RID: 43629
			public GameObject obj;

			// Token: 0x0400AA6E RID: 43630
			public PlayableDirector timeline;

			// Token: 0x0400AA6F RID: 43631
			public float timer;

			// Token: 0x0400AA70 RID: 43632
			public int number;

			// Token: 0x0400AA71 RID: 43633
			public bool turned;

			// Token: 0x0400AA72 RID: 43634
			public int seid1;

			// Token: 0x0400AA73 RID: 43635
			public int seid2;

			// Token: 0x0400AA74 RID: 43636
			public bool playing;
		}
	}
}
