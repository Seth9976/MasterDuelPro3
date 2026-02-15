using System;
using System.Collections;

namespace YgomGame.Menu
{
	// Token: 0x02000A6C RID: 2668
	public class DuelStartWaiting : DuelStartWaitingBase
	{
		// Token: 0x06004DD7 RID: 19927 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int ProgressCount()
		{
			return 0;
		}

		// Token: 0x06004DD8 RID: 19928 RVA: 0x0000216D File Offset: 0x0000036D
		public override void StartWaiting()
		{
		}

		// Token: 0x06004DD9 RID: 19929 RVA: 0x0000216A File Offset: 0x0000036A
		public override IEnumerator yWaitWaiting(Action callback)
		{
			return null;
		}

		// Token: 0x04008BB0 RID: 35760
		private float m_ProgressCount;
	}
}
