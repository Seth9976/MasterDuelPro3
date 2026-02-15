using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace YgomGame.Duel
{
	// Token: 0x02000EEB RID: 3819
	public class RunCoin
	{
		// Token: 0x06006F6D RID: 28525 RVA: 0x00002739 File Offset: 0x00000939
		public RunCoin(int numThrows, int faceBits, int shineBits, bool sameTime)
		{
		}

		// Token: 0x06006F6E RID: 28526 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool Update()
		{
			return false;
		}

		// Token: 0x06006F6F RID: 28527 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool UpdateCoin(RunCoin.Coin coin)
		{
			return false;
		}

		// Token: 0x06006F70 RID: 28528 RVA: 0x0000216D File Offset: 0x0000036D
		public void Skip()
		{
		}

		// Token: 0x06006F71 RID: 28529 RVA: 0x0000216D File Offset: 0x0000036D
		public void Terminate()
		{
		}

		// Token: 0x0400AA50 RID: 43600
		private float timer;

		// Token: 0x0400AA51 RID: 43601
		private bool isSkipped;

		// Token: 0x0400AA52 RID: 43602
		private List<RunCoin.Coin> coins;

		// Token: 0x0400AA53 RID: 43603
		private RunCoin.STEP step;

		// Token: 0x02000EEC RID: 3820
		private class Coin
		{
			// Token: 0x0400AA54 RID: 43604
			public PlayableDirector timeline;

			// Token: 0x0400AA55 RID: 43605
			public GameObject obj;

			// Token: 0x0400AA56 RID: 43606
			public float timer;

			// Token: 0x0400AA57 RID: 43607
			public RunCoin.Coin.State state;

			// Token: 0x0400AA58 RID: 43608
			public bool isFace;

			// Token: 0x0400AA59 RID: 43609
			public bool turned;

			// Token: 0x0400AA5A RID: 43610
			public bool decideSePlayed;

			// Token: 0x02000EED RID: 3821
			public enum State
			{
				// Token: 0x0400AA5C RID: 43612
				Wait,
				// Token: 0x0400AA5D RID: 43613
				Playing,
				// Token: 0x0400AA5E RID: 43614
				Played
			}
		}

		// Token: 0x02000EEE RID: 3822
		private enum STEP
		{
			// Token: 0x0400AA60 RID: 43616
			MAIN,
			// Token: 0x0400AA61 RID: 43617
			PRE_END,
			// Token: 0x0400AA62 RID: 43618
			END
		}
	}
}
