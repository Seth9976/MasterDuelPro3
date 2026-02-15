using System;

namespace UnityEngine.AddressableAssets
{
	// Token: 0x02000038 RID: 56
	public interface IKeyEvaluator
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000186 RID: 390
		object RuntimeKey { get; }

		// Token: 0x06000187 RID: 391
		bool RuntimeKeyIsValid();
	}
}
