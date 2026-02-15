using System;
using UnityEngine.ResourceManagement.Util;

namespace UnityEngine.ResourceManagement.AsyncOperations
{
	// Token: 0x02000076 RID: 118
	internal interface ICachable
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002A0 RID: 672
		// (set) Token: 0x060002A1 RID: 673
		IOperationCacheKey Key { get; set; }
	}
}
