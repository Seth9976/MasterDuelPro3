using System;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace UnityEngine.ResourceManagement.ResourceProviders
{
	// Token: 0x02000053 RID: 83
	public interface IInstanceProvider
	{
		// Token: 0x060001EC RID: 492
		GameObject ProvideInstance(ResourceManager resourceManager, AsyncOperationHandle<GameObject> prefabHandle, InstantiationParameters instantiateParameters);

		// Token: 0x060001ED RID: 493
		void ReleaseInstance(ResourceManager resourceManager, GameObject instance);
	}
}
