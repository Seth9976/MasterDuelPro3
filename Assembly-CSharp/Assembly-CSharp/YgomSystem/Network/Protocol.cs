using System;
using System.Collections;

namespace YgomSystem.Network
{
	// Token: 0x0200071C RID: 1820
	public abstract class Protocol
	{
		// Token: 0x06003907 RID: 14599
		public abstract IEnumerator Exec(NetworkMain.RequestStructure data);

		// Token: 0x06003908 RID: 14600
		public abstract void ApplicationQuitAbort();

		// Token: 0x06003909 RID: 14601
		public abstract int GetJobCount();
	}
}
