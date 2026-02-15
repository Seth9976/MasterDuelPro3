using System;

namespace UnityEngine.ResourceManagement.Util
{
	// Token: 0x02000034 RID: 52
	public interface IObjectInitializationDataProvider
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000135 RID: 309
		string Name { get; }

		// Token: 0x06000136 RID: 310
		ObjectInitializationData CreateObjectInitializationData();
	}
}
