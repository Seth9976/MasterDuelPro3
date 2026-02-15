using System;
using System.Collections;
using System.Collections.Generic;

namespace YgomGame.Menu
{
	// Token: 0x02000AE7 RID: 2791
	public class PvpMenuMatchingWcs : PvpMenuMatchingBase
	{
		// Token: 0x0600514B RID: 20811 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int ProgressCount()
		{
			return 0;
		}

		// Token: 0x0600514C RID: 20812 RVA: 0x0000216D File Offset: 0x0000036D
		public override void StartMatching(Dictionary<string, object> param)
		{
		}

		// Token: 0x0600514D RID: 20813 RVA: 0x0000216A File Offset: 0x0000036A
		public override IEnumerator yWaitMatching(Action callback)
		{
			return null;
		}

		// Token: 0x0600514E RID: 20814 RVA: 0x0000216D File Offset: 0x0000036D
		public override void SetBootDuelParam(ref Dictionary<string, object> param)
		{
		}

		// Token: 0x04008F9E RID: 36766
		private int wcs_id;

		// Token: 0x04008F9F RID: 36767
		private float m_ProgressCount;
	}
}
