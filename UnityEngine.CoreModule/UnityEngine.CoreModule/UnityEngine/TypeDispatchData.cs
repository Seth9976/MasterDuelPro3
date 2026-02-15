using System;
using Unity.Collections;

namespace UnityEngine
{
	// Token: 0x02000153 RID: 339
	internal struct TypeDispatchData : IDisposable
	{
		// Token: 0x06000EE2 RID: 3810 RVA: 0x0001F5C5 File Offset: 0x0001D7C5
		public void Dispose()
		{
			this.changed = null;
			this.changedID.Dispose();
			this.destroyedID.Dispose();
		}

		// Token: 0x040005D6 RID: 1494
		public Object[] changed;

		// Token: 0x040005D7 RID: 1495
		public NativeArray<int> changedID;

		// Token: 0x040005D8 RID: 1496
		public NativeArray<int> destroyedID;
	}
}
