using System;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace UnityEngine.ResourceManagement.ResourceProviders
{
	// Token: 0x0200005C RID: 92
	public class InstanceProvider : IInstanceProvider
	{
		// Token: 0x0600020E RID: 526 RVA: 0x00008FA0 File Offset: 0x000071A0
		public GameObject ProvideInstance(ResourceManager resourceManager, AsyncOperationHandle<GameObject> prefabHandle, InstantiationParameters instantiateParameters)
		{
			GameObject result = instantiateParameters.Instantiate<GameObject>(prefabHandle.Result);
			this.m_InstanceObjectToPrefabHandle.Add(result, prefabHandle);
			return result;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00008FCC File Offset: 0x000071CC
		public void ReleaseInstance(ResourceManager resourceManager, GameObject instance)
		{
			if (instance == null)
			{
				return;
			}
			AsyncOperationHandle<GameObject> resource;
			if (!this.m_InstanceObjectToPrefabHandle.TryGetValue(instance, out resource))
			{
				Debug.LogWarningFormat("Releasing unknown GameObject {0} to InstanceProvider.", new object[] { instance });
			}
			else
			{
				resource.Release();
				this.m_InstanceObjectToPrefabHandle.Remove(instance);
			}
			if (instance != null)
			{
				if (Application.isPlaying)
				{
					Object.Destroy(instance);
					return;
				}
				Object.DestroyImmediate(instance);
			}
		}

		// Token: 0x040000F0 RID: 240
		private Dictionary<GameObject, AsyncOperationHandle<GameObject>> m_InstanceObjectToPrefabHandle = new Dictionary<GameObject, AsyncOperationHandle<GameObject>>();
	}
}
