using System;
using System.Collections;
using System.Collections.Generic;

namespace YgomGame.Menu
{
	// Token: 0x02000AE1 RID: 2785
	public class PvpMenuMatchingRoom : PvpMenuMatchingBase
	{
		// Token: 0x06005133 RID: 20787 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int ProgressCount()
		{
			return 0;
		}

		// Token: 0x06005134 RID: 20788 RVA: 0x0000216D File Offset: 0x0000036D
		public override void StartMatching(Dictionary<string, object> param)
		{
		}

		// Token: 0x06005135 RID: 20789 RVA: 0x0000216A File Offset: 0x0000036A
		public override IEnumerator yWaitMatching(Action callback)
		{
			return null;
		}

		// Token: 0x06005136 RID: 20790 RVA: 0x0000216D File Offset: 0x0000036D
		public override void SetBootDuelParam(ref Dictionary<string, object> param)
		{
		}

		// Token: 0x04008F97 RID: 36759
		private float m_ProgressCount;
	}
}
