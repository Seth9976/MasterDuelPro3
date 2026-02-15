using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Unity.Profiling;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.ResourceManagement.Util;

namespace UnityEngine.ResourceManagement.Profiling
{
	// Token: 0x02000075 RID: 117
	internal static class ProfilerRuntime
	{
		// Token: 0x06000292 RID: 658 RVA: 0x0000A32C File Offset: 0x0000852C
		public static void Initialise()
		{
			ProfilerRuntime.CatalogLoadCounter.Value = 0;
			ProfilerRuntime.AssetBundleLoadCounter.Value = 0;
			ProfilerRuntime.AssetLoadCounter.Value = 0;
			ProfilerRuntime.SceneLoadCounter.Value = 0;
			ProfilerRuntime.m_CatalogData.Data.Clear();
			ProfilerRuntime.m_BundleData.Data.Clear();
			ProfilerRuntime.m_AssetData.Data.Clear();
			ProfilerRuntime.m_SceneData.Data.Clear();
			ProfilerRuntime.m_profilerEmitter.InitialiseCallbacks(new Action<float>(ProfilerRuntime.InstanceOnOnLateUpdateDelegate));
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000A3B7 File Offset: 0x000085B7
		private static void InstanceOnOnLateUpdateDelegate(float deltaTime)
		{
			ProfilerRuntime.PushToProfilerStream();
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000A3C0 File Offset: 0x000085C0
		public static void AddCatalog(Hash128 buildHash)
		{
			if (!buildHash.isValid)
			{
				return;
			}
			ProfilerRuntime.m_CatalogData.Add(buildHash, new CatalogFrameData
			{
				BuildResultHash = buildHash
			});
			int value = ProfilerRuntime.CatalogLoadCounter.Value;
			ProfilerRuntime.CatalogLoadCounter.Value = value + 1;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000A408 File Offset: 0x00008608
		public static void AddBundleOperation(ProvideHandle handle, [NotNull] AssetBundleRequestOptions requestOptions, ContentStatus status, BundleSource source)
		{
			IAsyncOperation op = handle.InternalOp as IAsyncOperation;
			if (op == null)
			{
				throw new NullReferenceException("Could not get Bundle operation for handle loaded for Key " + handle.Location.PrimaryKey);
			}
			string bundleName = requestOptions.BundleName;
			BundleOptions loadingOptions = BundleOptions.None;
			bool doCRC = requestOptions.Crc > 0U;
			if (doCRC && source == BundleSource.Cache)
			{
				doCRC = requestOptions.UseCrcForCachedBundle;
			}
			if (doCRC)
			{
				loadingOptions |= BundleOptions.CheckSumEnabled;
			}
			if (!string.IsNullOrEmpty(requestOptions.Hash))
			{
				loadingOptions |= BundleOptions.CachingEnabled;
			}
			BundleFrameData data = new BundleFrameData
			{
				ReferenceCount = op.ReferenceCount,
				BundleCode = bundleName.GetHashCode(),
				Status = status,
				LoadingOptions = loadingOptions,
				Source = source
			};
			ProfilerRuntime.m_BundleData.Add(op, data);
			if (!ProfilerRuntime.m_BundleNameToOperation.ContainsKey(bundleName))
			{
				ProfilerRuntime.AssetBundleLoadCounter.Value = ProfilerRuntime.AssetBundleLoadCounter.Value + 1;
			}
			ProfilerRuntime.m_BundleNameToOperation[bundleName] = op;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000A4F4 File Offset: 0x000086F4
		public static void BundleReleased(string bundleName)
		{
			IAsyncOperation op;
			if (string.IsNullOrEmpty(bundleName) || !ProfilerRuntime.m_BundleNameToOperation.TryGetValue(bundleName, out op))
			{
				return;
			}
			ProfilerRuntime.m_BundleData.Remove(op);
			ProfilerRuntime.m_BundleNameToOperation.Remove(bundleName);
			ProfilerRuntime.AssetBundleLoadCounter.Value = ProfilerRuntime.AssetBundleLoadCounter.Value - 1;
			List<IAsyncOperation> assetOps;
			if (ProfilerRuntime.m_BundleNameToAssetOperations.TryGetValue(bundleName, out assetOps))
			{
				ProfilerRuntime.m_BundleNameToAssetOperations.Remove(bundleName);
				foreach (IAsyncOperation assetOp in assetOps)
				{
					ProfilerRuntime.AssetLoadCounter.Value = ProfilerRuntime.AssetLoadCounter.Value - 1;
					ProfilerRuntime.m_AssetData.Remove(assetOp);
				}
			}
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000A5B8 File Offset: 0x000087B8
		public static void AddAssetOperation(ProvideHandle handle, ContentStatus status)
		{
			if (!handle.IsValid)
			{
				throw new ArgumentException("Attempting to add a Asset handle to profiler that is not valid");
			}
			IAsyncOperation assetLoadOperation = handle.InternalOp as IAsyncOperation;
			if (assetLoadOperation == null)
			{
				throw new NullReferenceException("Could not get operation for InternalOp of handle loaded with primary key: " + handle.Location.PrimaryKey);
			}
			string containingBundleName = ProfilerRuntime.GetContainingBundleNameForLocation(handle.Location);
			string assetId;
			if (handle.Location.InternalId.EndsWith(']'))
			{
				int start = handle.Location.InternalId.IndexOf('[');
				assetId = handle.Location.InternalId.Remove(start);
			}
			else
			{
				assetId = handle.Location.InternalId;
			}
			AssetFrameData profileObject = default(AssetFrameData);
			profileObject.AssetCode = assetId.GetHashCode();
			profileObject.ReferenceCount = assetLoadOperation.ReferenceCount;
			profileObject.BundleCode = containingBundleName.GetHashCode();
			profileObject.Status = status;
			List<IAsyncOperation> assetOperations;
			if (ProfilerRuntime.m_BundleNameToAssetOperations.TryGetValue(containingBundleName, out assetOperations))
			{
				if (!assetOperations.Contains(assetLoadOperation))
				{
					assetOperations.Add(assetLoadOperation);
				}
			}
			else
			{
				ProfilerRuntime.m_BundleNameToAssetOperations.Add(containingBundleName, new List<IAsyncOperation> { assetLoadOperation });
			}
			if (ProfilerRuntime.m_AssetData.Add(assetLoadOperation, profileObject))
			{
				ProfilerRuntime.AssetLoadCounter.Value = ProfilerRuntime.AssetLoadCounter.Value + 1;
			}
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000A6F0 File Offset: 0x000088F0
		private static string GetContainingBundleNameForLocation(IResourceLocation location)
		{
			if (location == null || location.Dependencies == null || location.Dependencies.Count == 0)
			{
				return "";
			}
			AssetBundleRequestOptions options = location.Dependencies[0].Data as AssetBundleRequestOptions;
			if (options == null)
			{
				Debug.LogError("Dependency bundle location does not have AssetBundleRequestOptions");
				return "";
			}
			return options.BundleName;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000A74C File Offset: 0x0000894C
		public static void AddSceneOperation(AsyncOperationHandle<SceneInstance> handle, IResourceLocation location, ContentStatus status)
		{
			IAsyncOperation sceneLoadOperation = handle.InternalOp;
			string containingBundleName = ProfilerRuntime.GetContainingBundleNameForLocation(location);
			AssetFrameData profileObject = default(AssetFrameData);
			profileObject.AssetCode = location.InternalId.GetHashCode();
			profileObject.ReferenceCount = sceneLoadOperation.ReferenceCount;
			profileObject.BundleCode = containingBundleName.GetHashCode();
			profileObject.Status = status;
			if (ProfilerRuntime.m_SceneData.Add(sceneLoadOperation, profileObject))
			{
				ProfilerRuntime.SceneLoadCounter.Value = ProfilerRuntime.SceneLoadCounter.Value + 1;
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000A7C4 File Offset: 0x000089C4
		public static void SceneReleased(AsyncOperationHandle<SceneInstance> handle)
		{
			ChainOperationTypelessDepedency<SceneInstance> chainOp = handle.InternalOp as ChainOperationTypelessDepedency<SceneInstance>;
			if (chainOp != null)
			{
				if (ProfilerRuntime.m_SceneData.Remove(chainOp.WrappedOp.InternalOp))
				{
					ProfilerRuntime.SceneLoadCounter.Value = ProfilerRuntime.SceneLoadCounter.Value - 1;
					return;
				}
				Debug.LogWarning("Failed to remove scene from Addressables profiler for " + chainOp.WrappedOp.DebugName);
				return;
			}
			else
			{
				if (ProfilerRuntime.m_SceneData.Remove(handle.InternalOp))
				{
					ProfilerRuntime.SceneLoadCounter.Value = ProfilerRuntime.SceneLoadCounter.Value - 1;
					return;
				}
				Debug.LogWarning("Failed to remove scene from Addressables profiler for " + handle.DebugName);
				return;
			}
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000A868 File Offset: 0x00008A68
		internal static int GetSceneLoadCounterValue()
		{
			return ProfilerRuntime.SceneLoadCounter.Value;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000A874 File Offset: 0x00008A74
		internal static void PushToProfilerStream()
		{
			if (!ProfilerRuntime.m_profilerEmitter.IsEnabled)
			{
				return;
			}
			ProfilerRuntime.RefreshChangedReferenceCounts();
			ProfilerRuntime.m_profilerEmitter.EmitFrameMetaData(ProfilerRuntime.kResourceManagerProfilerGuid, 0, ProfilerRuntime.m_CatalogData.Values);
			ProfilerRuntime.m_profilerEmitter.EmitFrameMetaData(ProfilerRuntime.kResourceManagerProfilerGuid, 1, ProfilerRuntime.m_BundleData.Values);
			ProfilerRuntime.m_profilerEmitter.EmitFrameMetaData(ProfilerRuntime.kResourceManagerProfilerGuid, 2, ProfilerRuntime.m_AssetData.Values);
			ProfilerRuntime.m_profilerEmitter.EmitFrameMetaData(ProfilerRuntime.kResourceManagerProfilerGuid, 3, ProfilerRuntime.m_SceneData.Values);
			ProfilerRuntime.m_CatalogData.Data.Clear();
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000A90C File Offset: 0x00008B0C
		private static void RefreshChangedReferenceCounts()
		{
			ProfilerRuntime.m_DataChange.Clear();
			foreach (KeyValuePair<IAsyncOperation, BundleFrameData> pair in ProfilerRuntime.m_BundleData.Data)
			{
				ValueTuple<int, float> newValues;
				if (ProfilerRuntime.ShouldUpdateFrameDataWithOperationData(pair.Key, pair.Value.ReferenceCount, pair.Value.PercentComplete, out newValues))
				{
					ProfilerRuntime.m_DataChange.Add(pair.Key, newValues);
				}
			}
			foreach (KeyValuePair<IAsyncOperation, ValueTuple<int, float>> pair2 in ProfilerRuntime.m_DataChange)
			{
				BundleFrameData temp = ProfilerRuntime.m_BundleData[pair2.Key];
				temp.ReferenceCount = pair2.Value.Item1;
				temp.PercentComplete = pair2.Value.Item2;
				ProfilerRuntime.m_BundleData[pair2.Key] = temp;
			}
			ProfilerRuntime.m_DataChange.Clear();
			foreach (KeyValuePair<IAsyncOperation, AssetFrameData> pair3 in ProfilerRuntime.m_AssetData.Data)
			{
				ValueTuple<int, float> newValues2;
				if (ProfilerRuntime.ShouldUpdateFrameDataWithOperationData(pair3.Key, pair3.Value.ReferenceCount, pair3.Value.PercentComplete, out newValues2))
				{
					ProfilerRuntime.m_DataChange.Add(pair3.Key, newValues2);
				}
			}
			foreach (KeyValuePair<IAsyncOperation, ValueTuple<int, float>> pair4 in ProfilerRuntime.m_DataChange)
			{
				AssetFrameData temp2 = ProfilerRuntime.m_AssetData[pair4.Key];
				temp2.ReferenceCount = pair4.Value.Item1;
				temp2.PercentComplete = pair4.Value.Item2;
				ProfilerRuntime.m_AssetData[pair4.Key] = temp2;
			}
			ProfilerRuntime.m_DataChange.Clear();
			foreach (KeyValuePair<IAsyncOperation, AssetFrameData> pair5 in ProfilerRuntime.m_SceneData.Data)
			{
				ValueTuple<int, float> newValues3;
				if (ProfilerRuntime.ShouldUpdateFrameDataWithOperationData(pair5.Key, pair5.Value.ReferenceCount, pair5.Value.PercentComplete, out newValues3))
				{
					ProfilerRuntime.m_DataChange.Add(pair5.Key, newValues3);
				}
			}
			foreach (KeyValuePair<IAsyncOperation, ValueTuple<int, float>> pair6 in ProfilerRuntime.m_DataChange)
			{
				AssetFrameData temp3 = ProfilerRuntime.m_SceneData[pair6.Key];
				temp3.ReferenceCount = pair6.Value.Item1;
				temp3.PercentComplete = pair6.Value.Item2;
				ProfilerRuntime.m_SceneData[pair6.Key] = temp3;
			}
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000AC44 File Offset: 0x00008E44
		private static bool ShouldUpdateFrameDataWithOperationData(IAsyncOperation activeOperation, int frameReferenceCount, float framePercentComplete, out ValueTuple<int, float> newDataOut)
		{
			int currentReferenceCount = activeOperation.ReferenceCount;
			switch (activeOperation.Status)
			{
			case AsyncOperationStatus.None:
				if (activeOperation.IsDone || !activeOperation.IsRunning)
				{
					currentReferenceCount = 0;
				}
				break;
			case AsyncOperationStatus.Failed:
				currentReferenceCount = 0;
				break;
			}
			float currentPercentComplete = activeOperation.PercentComplete;
			newDataOut = new ValueTuple<int, float>(currentReferenceCount, currentPercentComplete);
			return currentReferenceCount != frameReferenceCount || !Mathf.Approximately(currentPercentComplete, framePercentComplete);
		}

		// Token: 0x04000137 RID: 311
		internal static IProfilerEmitter m_profilerEmitter = new EngineEmitter();

		// Token: 0x04000138 RID: 312
		public static readonly Guid kResourceManagerProfilerGuid = new Guid("4f8a8c93-7634-4ef7-bbbc-6c9928567fa4");

		// Token: 0x04000139 RID: 313
		public const int kCatalogTag = 0;

		// Token: 0x0400013A RID: 314
		public const int kBundleDataTag = 1;

		// Token: 0x0400013B RID: 315
		public const int kAssetDataTag = 2;

		// Token: 0x0400013C RID: 316
		public const int kSceneDataTag = 3;

		// Token: 0x0400013D RID: 317
		private static ProfilerCounterValue<int> CatalogLoadCounter = new ProfilerCounterValue<int>(ProfilerCategory.Loading, "Catalogs", ProfilerMarkerDataUnit.Count);

		// Token: 0x0400013E RID: 318
		private static ProfilerCounterValue<int> AssetBundleLoadCounter = new ProfilerCounterValue<int>(ProfilerCategory.Loading, "Asset Bundles", ProfilerMarkerDataUnit.Count);

		// Token: 0x0400013F RID: 319
		private static ProfilerCounterValue<int> AssetLoadCounter = new ProfilerCounterValue<int>(ProfilerCategory.Loading, "Assets", ProfilerMarkerDataUnit.Count);

		// Token: 0x04000140 RID: 320
		private static ProfilerCounterValue<int> SceneLoadCounter = new ProfilerCounterValue<int>(ProfilerCategory.Loading, "Scenes", ProfilerMarkerDataUnit.Count);

		// Token: 0x04000141 RID: 321
		private static ProfilerFrameData<Hash128, CatalogFrameData> m_CatalogData = new ProfilerFrameData<Hash128, CatalogFrameData>(4);

		// Token: 0x04000142 RID: 322
		private static ProfilerFrameData<IAsyncOperation, BundleFrameData> m_BundleData = new ProfilerFrameData<IAsyncOperation, BundleFrameData>(64);

		// Token: 0x04000143 RID: 323
		private static ProfilerFrameData<IAsyncOperation, AssetFrameData> m_AssetData = new ProfilerFrameData<IAsyncOperation, AssetFrameData>(512);

		// Token: 0x04000144 RID: 324
		private static ProfilerFrameData<IAsyncOperation, AssetFrameData> m_SceneData = new ProfilerFrameData<IAsyncOperation, AssetFrameData>(16);

		// Token: 0x04000145 RID: 325
		private static Dictionary<string, IAsyncOperation> m_BundleNameToOperation = new Dictionary<string, IAsyncOperation>(64);

		// Token: 0x04000146 RID: 326
		private static Dictionary<string, List<IAsyncOperation>> m_BundleNameToAssetOperations = new Dictionary<string, List<IAsyncOperation>>(512);

		// Token: 0x04000147 RID: 327
		private static Dictionary<IAsyncOperation, ValueTuple<int, float>> m_DataChange = new Dictionary<IAsyncOperation, ValueTuple<int, float>>(64);
	}
}
