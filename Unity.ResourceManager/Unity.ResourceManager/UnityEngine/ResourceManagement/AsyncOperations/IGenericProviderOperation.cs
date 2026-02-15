using System;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace UnityEngine.ResourceManagement.AsyncOperations
{
	// Token: 0x02000083 RID: 131
	internal interface IGenericProviderOperation
	{
		// Token: 0x06000365 RID: 869
		void Init(ResourceManager rm, IResourceProvider provider, IResourceLocation location, AsyncOperationHandle<IList<AsyncOperationHandle>> depOp);

		// Token: 0x06000366 RID: 870
		void Init(ResourceManager rm, IResourceProvider provider, IResourceLocation location, AsyncOperationHandle<IList<AsyncOperationHandle>> depOp, bool releaseDependenciesOnFailure);

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000367 RID: 871
		int ProvideHandleVersion { get; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000368 RID: 872
		IResourceLocation Location { get; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000369 RID: 873
		int DependencyCount { get; }

		// Token: 0x0600036A RID: 874
		void GetDependencies(IList<object> dstList);

		// Token: 0x0600036B RID: 875
		TDepObject GetDependency<TDepObject>(int index);

		// Token: 0x0600036C RID: 876
		void SetProgressCallback(Func<float> callback);

		// Token: 0x0600036D RID: 877
		void ProviderCompleted<T>(T result, bool status, Exception e);

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600036E RID: 878
		Type RequestedType { get; }

		// Token: 0x0600036F RID: 879
		void SetDownloadProgressCallback(Func<DownloadStatus> callback);

		// Token: 0x06000370 RID: 880
		void SetWaitForCompletionCallback(Func<bool> callback);
	}
}
