using System;

namespace System.Threading
{
	// Token: 0x02000247 RID: 583
	public class Lock
	{
		// Token: 0x0600155C RID: 5468 RVA: 0x00055D8C File Offset: 0x00053F8C
		public void Acquire()
		{
			Monitor.Enter(this._lock);
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x00055D99 File Offset: 0x00053F99
		public void Release()
		{
			Monitor.Exit(this._lock);
		}

		// Token: 0x04000A82 RID: 2690
		private object _lock = new object();
	}
}
