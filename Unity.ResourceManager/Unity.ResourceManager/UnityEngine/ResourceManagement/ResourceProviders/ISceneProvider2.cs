using System;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

namespace UnityEngine.ResourceManagement.ResourceProviders
{
	// Token: 0x0200005A RID: 90
	internal interface ISceneProvider2 : ISceneProvider
	{
		// Token: 0x0600020C RID: 524
		AsyncOperationHandle<SceneInstance> ReleaseScene(ResourceManager resourceManager, AsyncOperationHandle<SceneInstance> sceneLoadHandle, UnloadSceneOptions unloadOptions);
	}
}
