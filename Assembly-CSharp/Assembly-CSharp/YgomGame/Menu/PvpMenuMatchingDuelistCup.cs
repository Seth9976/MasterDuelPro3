using System;
using System.Collections;
using System.Collections.Generic;

namespace YgomGame.Menu
{
	// Token: 0x02000ADC RID: 2780
	public class PvpMenuMatchingDuelistCup : PvpMenuMatchingBase
	{
		// Token: 0x0600511A RID: 20762 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int ProgressCount()
		{
			return 0;
		}

		// Token: 0x0600511B RID: 20763 RVA: 0x0000216D File Offset: 0x0000036D
		public override void StartMatching(Dictionary<string, object> param)
		{
		}

		// Token: 0x0600511C RID: 20764 RVA: 0x0000216A File Offset: 0x0000036A
		public override IEnumerator yWaitMatching(Action callback)
		{
			return null;
		}

		// Token: 0x0600511D RID: 20765 RVA: 0x0000216D File Offset: 0x0000036D
		public override void SetBootDuelParam(ref Dictionary<string, object> param)
		{
		}

		// Token: 0x04008F8D RID: 36749
		private int cid;

		// Token: 0x04008F8E RID: 36750
		private float m_ProgressCount;
	}
}
