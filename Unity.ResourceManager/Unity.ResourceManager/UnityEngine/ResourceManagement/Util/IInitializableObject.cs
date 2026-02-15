using System;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace UnityEngine.ResourceManagement.Util
{
	// Token: 0x02000033 RID: 51
	public interface IInitializableObject
	{
		// Token: 0x06000133 RID: 307
		bool Initialize(string id, string data);

		// Token: 0x06000134 RID: 308
		AsyncOperationHandle<bool> InitializeAsync(ResourceManager rm, string id, string data);
	}
}
