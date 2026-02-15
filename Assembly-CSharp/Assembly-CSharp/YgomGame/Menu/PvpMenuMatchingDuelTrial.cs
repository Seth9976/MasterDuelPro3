using System;
using System.Collections;
using System.Collections.Generic;

namespace YgomGame.Menu
{
	// Token: 0x02000ADB RID: 2779
	public class PvpMenuMatchingDuelTrial : PvpMenuMatchingBase
	{
		// Token: 0x06005116 RID: 20758 RVA: 0x0000216D File Offset: 0x0000036D
		public override void StartMatching(Dictionary<string, object> param)
		{
		}

		// Token: 0x06005117 RID: 20759 RVA: 0x0000216A File Offset: 0x0000036A
		public override IEnumerator yWaitMatching(Action callback)
		{
			return null;
		}

		// Token: 0x06005118 RID: 20760 RVA: 0x0000216D File Offset: 0x0000036D
		public override void SetBootDuelParam(ref Dictionary<string, object> param)
		{
		}

		// Token: 0x04008F8B RID: 36747
		public bool goto_cpu;

		// Token: 0x04008F8C RID: 36748
		private int tid;
	}
}
