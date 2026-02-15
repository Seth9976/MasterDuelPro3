using System;
using System.Collections;
using System.Collections.Generic;

namespace YgomGame.Menu
{
	// Token: 0x02000ADF RID: 2783
	public class PvpMenuMatchingRank : PvpMenuMatchingBase
	{
		// Token: 0x06005129 RID: 20777 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int ProgressCount()
		{
			return 0;
		}

		// Token: 0x0600512A RID: 20778 RVA: 0x0000216D File Offset: 0x0000036D
		public override void StartMatching(Dictionary<string, object> param)
		{
		}

		// Token: 0x0600512B RID: 20779 RVA: 0x0000216A File Offset: 0x0000036A
		public override IEnumerator yWaitMatching(Action callback)
		{
			return null;
		}

		// Token: 0x0600512C RID: 20780 RVA: 0x0000216D File Offset: 0x0000036D
		public override void SetBootDuelParam(ref Dictionary<string, object> param)
		{
		}

		// Token: 0x04008F93 RID: 36755
		private int seasonId;

		// Token: 0x04008F94 RID: 36756
		private float m_ProgressCount;
	}
}
