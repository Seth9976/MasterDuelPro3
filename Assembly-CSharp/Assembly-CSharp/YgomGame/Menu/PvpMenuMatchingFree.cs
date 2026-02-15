using System;
using System.Collections;
using System.Collections.Generic;

namespace YgomGame.Menu
{
	// Token: 0x02000ADE RID: 2782
	public class PvpMenuMatchingFree : PvpMenuMatchingBase
	{
		// Token: 0x06005124 RID: 20772 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int ProgressCount()
		{
			return 0;
		}

		// Token: 0x06005125 RID: 20773 RVA: 0x0000216D File Offset: 0x0000036D
		public override void StartMatching(Dictionary<string, object> param)
		{
		}

		// Token: 0x06005126 RID: 20774 RVA: 0x0000216A File Offset: 0x0000036A
		public override IEnumerator yWaitMatching(Action callback)
		{
			return null;
		}

		// Token: 0x06005127 RID: 20775 RVA: 0x0000216D File Offset: 0x0000036D
		public override void SetBootDuelParam(ref Dictionary<string, object> param)
		{
		}

		// Token: 0x04008F92 RID: 36754
		private float m_ProgressCount;
	}
}
