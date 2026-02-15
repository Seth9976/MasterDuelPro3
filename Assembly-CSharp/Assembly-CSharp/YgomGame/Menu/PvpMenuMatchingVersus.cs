using System;
using System.Collections;
using System.Collections.Generic;

namespace YgomGame.Menu
{
	// Token: 0x02000AE6 RID: 2790
	public class PvpMenuMatchingVersus : PvpMenuMatchingBase
	{
		// Token: 0x06005147 RID: 20807 RVA: 0x0000216D File Offset: 0x0000036D
		public override void StartMatching(Dictionary<string, object> param)
		{
		}

		// Token: 0x06005148 RID: 20808 RVA: 0x0000216A File Offset: 0x0000036A
		public override IEnumerator yWaitMatching(Action callback)
		{
			return null;
		}

		// Token: 0x06005149 RID: 20809 RVA: 0x0000216D File Offset: 0x0000036D
		public override void SetBootDuelParam(ref Dictionary<string, object> param)
		{
		}

		// Token: 0x04008F9B RID: 36763
		public bool goto_cpu;

		// Token: 0x04008F9C RID: 36764
		private int tid;

		// Token: 0x04008F9D RID: 36765
		private int rentalState;
	}
}
