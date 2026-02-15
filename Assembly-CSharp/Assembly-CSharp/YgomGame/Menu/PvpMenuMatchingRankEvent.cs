using System;
using System.Collections;
using System.Collections.Generic;

namespace YgomGame.Menu
{
	// Token: 0x02000AE0 RID: 2784
	public class PvpMenuMatchingRankEvent : PvpMenuMatchingBase
	{
		// Token: 0x0600512E RID: 20782 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int ProgressCount()
		{
			return 0;
		}

		// Token: 0x0600512F RID: 20783 RVA: 0x0000216D File Offset: 0x0000036D
		public override void StartMatching(Dictionary<string, object> param)
		{
		}

		// Token: 0x06005130 RID: 20784 RVA: 0x0000216A File Offset: 0x0000036A
		public override IEnumerator yWaitMatching(Action callback)
		{
			return null;
		}

		// Token: 0x06005131 RID: 20785 RVA: 0x0000216D File Offset: 0x0000036D
		public override void SetBootDuelParam(ref Dictionary<string, object> param)
		{
		}

		// Token: 0x04008F95 RID: 36757
		private int rank_event_id;

		// Token: 0x04008F96 RID: 36758
		private float m_ProgressCount;
	}
}
