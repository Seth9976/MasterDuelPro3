using System;

namespace System.Threading
{
	// Token: 0x02000211 RID: 529
	internal interface IAsyncLocalValueMap
	{
		// Token: 0x06001449 RID: 5193
		bool TryGetValue(IAsyncLocal key, out object value);

		// Token: 0x0600144A RID: 5194
		IAsyncLocalValueMap Set(IAsyncLocal key, object value);
	}
}
