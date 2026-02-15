using System;
using System.Collections;
using System.Collections.Generic;

namespace YgomGame.Menu
{
	// Token: 0x02000AE5 RID: 2789
	public class PvpMenuMatchingTournament : PvpMenuMatchingBase
	{
		// Token: 0x06005143 RID: 20803 RVA: 0x0000216D File Offset: 0x0000036D
		public override void StartMatching(Dictionary<string, object> param)
		{
		}

		// Token: 0x06005144 RID: 20804 RVA: 0x0000216A File Offset: 0x0000036A
		public override IEnumerator yWaitMatching(Action callback)
		{
			return null;
		}

		// Token: 0x06005145 RID: 20805 RVA: 0x0000216D File Offset: 0x0000036D
		public override void SetBootDuelParam(ref Dictionary<string, object> param)
		{
		}

		// Token: 0x04008F99 RID: 36761
		public bool goto_cpu;

		// Token: 0x04008F9A RID: 36762
		private int tid;
	}
}
