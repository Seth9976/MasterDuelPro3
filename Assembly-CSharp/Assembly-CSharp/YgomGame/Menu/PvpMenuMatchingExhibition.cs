using System;
using System.Collections;
using System.Collections.Generic;

namespace YgomGame.Menu
{
	// Token: 0x02000ADD RID: 2781
	public class PvpMenuMatchingExhibition : PvpMenuMatchingBase
	{
		// Token: 0x0600511F RID: 20767 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int ProgressCount()
		{
			return 0;
		}

		// Token: 0x06005120 RID: 20768 RVA: 0x0000216D File Offset: 0x0000036D
		public override void StartMatching(Dictionary<string, object> param)
		{
		}

		// Token: 0x06005121 RID: 20769 RVA: 0x0000216A File Offset: 0x0000036A
		public override IEnumerator yWaitMatching(Action callback)
		{
			return null;
		}

		// Token: 0x06005122 RID: 20770 RVA: 0x0000216D File Offset: 0x0000036D
		public override void SetBootDuelParam(ref Dictionary<string, object> param)
		{
		}

		// Token: 0x04008F8F RID: 36751
		private int exhid;

		// Token: 0x04008F90 RID: 36752
		private int rentalState;

		// Token: 0x04008F91 RID: 36753
		private float m_ProgressCount;
	}
}
