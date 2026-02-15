using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200008C RID: 140
	public interface IBindable
	{
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600055E RID: 1374
		IBinding binding { get; }

		// Token: 0x170000DA RID: 218
		// (set) Token: 0x0600055F RID: 1375
		string bindingPath { set; }
	}
}
