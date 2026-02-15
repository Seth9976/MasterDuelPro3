using System;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.SceneManagement;

namespace UnityEngine.ResourceManagement.ResourceProviders
{
	// Token: 0x02000059 RID: 89
	public interface ISceneProvider
	{
		// Token: 0x06000208 RID: 520
		AsyncOperationHandle<SceneInstance> ProvideScene(ResourceManager resourceManager, IResourceLocation location, LoadSceneMode loadMode, bool activateOnLoad, int priority);

		// Token: 0x06000209 RID: 521
		AsyncOperationHandle<SceneInstance> ProvideScene(ResourceManager resourceManager, IResourceLocation location, LoadSceneParameters loadSceneParameters, bool activateOnLoad, int priority);

		// Token: 0x0600020A RID: 522
		AsyncOperationHandle<SceneInstance> ProvideScene(ResourceManager resourceManager, IResourceLocation location, LoadSceneParameters loadSceneParameters, SceneReleaseMode releaseMode, bool activateOnLoad, int priority);

		// Token: 0x0600020B RID: 523
		AsyncOperationHandle<SceneInstance> ReleaseScene(ResourceManager resourceManager, AsyncOperationHandle<SceneInstance> sceneLoadHandle);
	}
}
