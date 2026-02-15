using System;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

namespace UnityEngine.ResourceManagement.ResourceProviders
{
	// Token: 0x0200005B RID: 91
	internal static class SceneProviderExtensions
	{
		// Token: 0x0600020D RID: 525 RVA: 0x00008F7E File Offset: 0x0000717E
		public static AsyncOperationHandle<SceneInstance> ReleaseScene(this ISceneProvider provider, ResourceManager resourceManager, AsyncOperationHandle<SceneInstance> sceneLoadHandle, UnloadSceneOptions unloadOptions)
		{
			if (provider is ISceneProvider2)
			{
				return ((ISceneProvider2)provider).ReleaseScene(resourceManager, sceneLoadHandle, unloadOptions);
			}
			return provider.ReleaseScene(resourceManager, sceneLoadHandle);
		}
	}
}
