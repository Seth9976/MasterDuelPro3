using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEngine.AddressableAssets.Initialization;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.AddressableAssets.ResourceProviders;
using UnityEngine.Networking;
using UnityEngine.ResourceManagement;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.ResourceManagement.Util;
using UnityEngine.SceneManagement;

namespace UnityEngine.AddressableAssets
{
	// Token: 0x0200000E RID: 14
	internal class AddressablesImpl : IEqualityComparer<IResourceLocation>
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000098 RID: 152 RVA: 0x0000369E File Offset: 0x0000189E
		// (set) Token: 0x06000099 RID: 153 RVA: 0x000036A8 File Offset: 0x000018A8
		public IInstanceProvider InstanceProvider
		{
			get
			{
				return this.m_InstanceProvider;
			}
			set
			{
				this.m_InstanceProvider = value;
				IUpdateReceiver rec = this.m_InstanceProvider as IUpdateReceiver;
				if (rec != null)
				{
					this.m_ResourceManager.AddUpdateReceiver(rec);
				}
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600009A RID: 154 RVA: 0x000036D7 File Offset: 0x000018D7
		public ResourceManager ResourceManager
		{
			get
			{
				return this.m_ResourceManager;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600009B RID: 155 RVA: 0x000036DF File Offset: 0x000018DF
		// (set) Token: 0x0600009C RID: 156 RVA: 0x000036E7 File Offset: 0x000018E7
		public int CatalogRequestsTimeout
		{
			get
			{
				return this.m_CatalogRequestsTimeout;
			}
			set
			{
				this.m_CatalogRequestsTimeout = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600009D RID: 157 RVA: 0x000036F0 File Offset: 0x000018F0
		internal int ActiveSceneInstances
		{
			get
			{
				return this.m_SceneInstances.Count;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600009E RID: 158 RVA: 0x000036FD File Offset: 0x000018FD
		internal int TrackedHandleCount
		{
			get
			{
				return this.m_resultToHandle.Count;
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000370C File Offset: 0x0000190C
		public AddressablesImpl(IAllocationStrategy alloc)
		{
			this.m_ResourceManager = new ResourceManager(alloc);
			SceneManager.sceneUnloaded += this.OnSceneUnloaded;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000375D File Offset: 0x0000195D
		internal void ReleaseSceneManagerOperation()
		{
			SceneManager.sceneUnloaded -= this.OnSceneUnloaded;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003770 File Offset: 0x00001970
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x0000377D File Offset: 0x0000197D
		public Func<IResourceLocation, string> InternalIdTransformFunc
		{
			get
			{
				return this.ResourceManager.InternalIdTransformFunc;
			}
			set
			{
				this.ResourceManager.InternalIdTransformFunc = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x0000378B File Offset: 0x0000198B
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00003798 File Offset: 0x00001998
		public Action<UnityWebRequest> WebRequestOverride
		{
			get
			{
				return this.ResourceManager.WebRequestOverride;
			}
			set
			{
				this.ResourceManager.WebRequestOverride = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x000037A8 File Offset: 0x000019A8
		public AsyncOperationHandle ChainOperation
		{
			get
			{
				if (!this.hasStartedInitialization)
				{
					return this.InitializeAsync();
				}
				if (this.m_InitializationOperation.IsValid() && !this.m_InitializationOperation.IsDone)
				{
					return this.m_InitializationOperation;
				}
				if (this.m_ActiveUpdateOperation.IsValid() && !this.m_ActiveUpdateOperation.IsDone)
				{
					return this.m_ActiveUpdateOperation;
				}
				Debug.LogWarning("ChainOperation property should not be accessed unless ShouldChainRequest is true.");
				return default(AsyncOperationHandle);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00003828 File Offset: 0x00001A28
		internal bool ShouldChainRequest
		{
			get
			{
				return !this.hasStartedInitialization || (this.m_InitializationOperation.IsValid() && !this.m_InitializationOperation.IsDone) || (this.m_ActiveUpdateOperation.IsValid() && !this.m_ActiveUpdateOperation.IsDone);
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003878 File Offset: 0x00001A78
		internal void OnSceneUnloaded(Scene scene)
		{
			foreach (AsyncOperationHandle s in this.m_SceneInstances)
			{
				if (!s.IsValid())
				{
					this.m_SceneInstances.Remove(s);
					break;
				}
				AsyncOperationHandle<SceneInstance> sceneHandle = s.Convert<SceneInstance>();
				if (sceneHandle.Result.Scene == scene)
				{
					this.m_SceneInstances.Remove(s);
					this.m_resultToHandle.Remove(s.Result);
					if (sceneHandle.Result.ReleaseSceneOnSceneUnloaded)
					{
						this.SceneProvider.ReleaseScene(this.m_ResourceManager, sceneHandle).ReleaseHandleOnCompletion();
						break;
					}
					break;
				}
			}
			this.m_ResourceManager.CleanupSceneInstances(scene);
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00003960 File Offset: 0x00001B60
		public string StreamingAssetsSubFolder
		{
			get
			{
				return "aa";
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00003967 File Offset: 0x00001B67
		public string BuildPath
		{
			get
			{
				return Addressables.LibraryPath + this.StreamingAssetsSubFolder + "/" + PlatformMappingService.GetPlatformPathSubFolder();
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00003983 File Offset: 0x00001B83
		public string PlayerBuildDataPath
		{
			get
			{
				return Application.streamingAssetsPath + "/" + this.StreamingAssetsSubFolder;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000AB RID: 171 RVA: 0x0000399A File Offset: 0x00001B9A
		public string RuntimePath
		{
			get
			{
				return this.PlayerBuildDataPath;
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000039A2 File Offset: 0x00001BA2
		public void Log(string msg)
		{
			Debug.Log(msg);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000039AA File Offset: 0x00001BAA
		public void LogFormat(string format, params object[] args)
		{
			Debug.LogFormat(format, args);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000039B3 File Offset: 0x00001BB3
		public void LogWarning(string msg)
		{
			Debug.LogWarning(msg);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000039BB File Offset: 0x00001BBB
		public void LogWarningFormat(string format, params object[] args)
		{
			Debug.LogWarningFormat(format, args);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000039C4 File Offset: 0x00001BC4
		public void LogError(string msg)
		{
			Debug.LogError(msg);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000039CC File Offset: 0x00001BCC
		public void LogException(AsyncOperationHandle op, Exception ex)
		{
			if (op.Status == AsyncOperationStatus.Failed)
			{
				Debug.LogError(ex.ToString());
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000039E3 File Offset: 0x00001BE3
		public void LogException(Exception ex)
		{
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000039E5 File Offset: 0x00001BE5
		public void LogErrorFormat(string format, params object[] args)
		{
			Debug.LogErrorFormat(format, args);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000039F0 File Offset: 0x00001BF0
		public string ResolveInternalId(string id)
		{
			string path = AddressablesRuntimeProperties.EvaluateString(id);
			if (path.Length >= 260 && path.StartsWith(Application.dataPath, StringComparison.Ordinal))
			{
				path = path.Substring(Application.dataPath.Length + 1);
			}
			return path;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00003A33 File Offset: 0x00001C33
		public IEnumerable<IResourceLocator> ResourceLocators
		{
			get
			{
				return this.m_ResourceLocators.Select((ResourceLocatorInfo l) => l.Locator);
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003A5F File Offset: 0x00001C5F
		public void AddResourceLocator(IResourceLocator loc, string localCatalogHash = null, IResourceLocation remoteCatalogLocation = null)
		{
			this.m_ResourceLocators.Add(new ResourceLocatorInfo(loc, localCatalogHash, remoteCatalogLocation));
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003A74 File Offset: 0x00001C74
		public void RemoveResourceLocator(IResourceLocator loc)
		{
			this.m_ResourceLocators.RemoveAll((ResourceLocatorInfo l) => l.Locator == loc);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003AA6 File Offset: 0x00001CA6
		public void ClearResourceLocators()
		{
			this.m_ResourceLocators.Clear();
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003AB4 File Offset: 0x00001CB4
		internal bool GetResourceLocations(object key, Type type, out IList<IResourceLocation> locations)
		{
			if (type == null && key is AssetReference)
			{
				type = (key as AssetReference).SubObjectType;
			}
			key = this.EvaluateKey(key);
			locations = null;
			HashSet<IResourceLocation> current = null;
			using (List<ResourceLocatorInfo>.Enumerator enumerator = this.m_ResourceLocators.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					IList<IResourceLocation> locs;
					if (enumerator.Current.Locator.Locate(key, type, out locs))
					{
						if (locations == null)
						{
							locations = locs;
						}
						else
						{
							if (current == null)
							{
								current = new HashSet<IResourceLocation>();
								foreach (IResourceLocation loc in locations)
								{
									current.Add(loc);
								}
							}
							current.UnionWith(locs);
						}
					}
				}
			}
			if (current == null)
			{
				return locations != null;
			}
			locations = new List<IResourceLocation>(current);
			return true;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003BA4 File Offset: 0x00001DA4
		internal bool GetResourceLocations(IEnumerable keys, Type type, Addressables.MergeMode merge, out IList<IResourceLocation> locations)
		{
			locations = null;
			HashSet<IResourceLocation> current = null;
			foreach (object key in keys)
			{
				IList<IResourceLocation> locs;
				if (this.GetResourceLocations(key, type, out locs))
				{
					if (locations == null)
					{
						locations = locs;
						if (merge == Addressables.MergeMode.None)
						{
							return true;
						}
					}
					else
					{
						if (current == null)
						{
							current = new HashSet<IResourceLocation>(locations, this);
						}
						if (merge == Addressables.MergeMode.Intersection)
						{
							current.IntersectWith(locs);
						}
						else if (merge == Addressables.MergeMode.Union)
						{
							current.UnionWith(locs);
						}
					}
				}
				else if (merge == Addressables.MergeMode.Intersection)
				{
					locations = null;
					return false;
				}
			}
			if (current == null)
			{
				return locations != null;
			}
			if (current.Count == 0)
			{
				locations = null;
				return false;
			}
			locations = new List<IResourceLocation>(current);
			return true;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003C6C File Offset: 0x00001E6C
		public AsyncOperationHandle<IResourceLocator> InitializeAsync(string runtimeDataPath, string providerSuffix = null, bool autoReleaseHandle = true)
		{
			if (this.hasStartedInitialization)
			{
				if (this.m_InitializationOperation.IsValid())
				{
					return this.m_InitializationOperation;
				}
				AsyncOperationHandle<IResourceLocator> completedOperation = this.ResourceManager.CreateCompletedOperation<IResourceLocator>(this.m_ResourceLocators[0].Locator, null);
				if (autoReleaseHandle)
				{
					completedOperation.ReleaseHandleOnCompletion();
				}
				return completedOperation;
			}
			else
			{
				if (ResourceManager.ExceptionHandler == null)
				{
					ResourceManager.ExceptionHandler = new Action<AsyncOperationHandle, Exception>(this.LogException);
				}
				this.hasStartedInitialization = true;
				if (this.m_InitializationOperation.IsValid())
				{
					return this.m_InitializationOperation;
				}
				GC.KeepAlive(Application.streamingAssetsPath);
				GC.KeepAlive(Application.persistentDataPath);
				if (string.IsNullOrEmpty(runtimeDataPath))
				{
					return this.ResourceManager.CreateCompletedOperation<IResourceLocator>(null, string.Format("Invalid Key: {0}", runtimeDataPath));
				}
				this.m_OnHandleCompleteAction = new Action<AsyncOperationHandle>(this.OnHandleCompleted);
				this.m_OnSceneHandleCompleteAction = new Action<AsyncOperationHandle>(this.OnSceneHandleCompleted);
				this.m_OnHandleDestroyedAction = new Action<AsyncOperationHandle>(this.OnHandleDestroyed);
				if (!this.m_InitializationOperation.IsValid())
				{
					this.m_InitializationOperation = InitializationOperation.CreateInitializationOperation(this, runtimeDataPath, providerSuffix);
				}
				if (autoReleaseHandle)
				{
					this.m_InitializationOperation.ReleaseHandleOnCompletion();
				}
				return this.m_InitializationOperation;
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003D8C File Offset: 0x00001F8C
		public AsyncOperationHandle<IResourceLocator> InitializeAsync()
		{
			string settingsPath = this.RuntimePath + "/settings.json";
			return this.InitializeAsync(this.ResolveInternalId(settingsPath), null, true);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003DBC File Offset: 0x00001FBC
		public AsyncOperationHandle<IResourceLocator> InitializeAsync(bool autoReleaseHandle)
		{
			string settingsPath = this.RuntimePath + "/settings.json";
			return this.InitializeAsync(this.ResolveInternalId(settingsPath), null, autoReleaseHandle);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003DE9 File Offset: 0x00001FE9
		public ResourceLocationBase CreateCatalogLocationWithHashDependencies<T>(IResourceLocation catalogLocation) where T : IResourceProvider
		{
			return this.CreateCatalogLocationWithHashDependencies<T>(catalogLocation.InternalId);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003DF8 File Offset: 0x00001FF8
		public ResourceLocationBase CreateCatalogLocationWithHashDependencies<T>(string catalogLocation) where T : IResourceProvider
		{
			string hashFilePath = catalogLocation.Replace(".bin", ".hash");
			return this.CreateCatalogLocationWithHashDependencies<T>(catalogLocation, hashFilePath);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003E20 File Offset: 0x00002020
		public ResourceLocationBase CreateCatalogLocationWithHashDependencies<T>(string catalogPath, string hashFilePath) where T : IResourceProvider
		{
			ResourceLocationBase catalogLoc = new ResourceLocationBase(catalogPath, catalogPath, typeof(T).FullName, typeof(IResourceLocator), Array.Empty<IResourceLocation>())
			{
				Data = new ProviderLoadRequestOptions
				{
					IgnoreFailures = false,
					WebRequestTimeout = this.CatalogRequestsTimeout
				}
			};
			if (!string.IsNullOrEmpty(hashFilePath))
			{
				ProviderLoadRequestOptions hashOptions = new ProviderLoadRequestOptions
				{
					IgnoreFailures = true,
					WebRequestTimeout = this.CatalogRequestsTimeout
				};
				string tmpPath = hashFilePath;
				if (ResourceManagerConfig.IsPathRemote(hashFilePath))
				{
					tmpPath = ResourceManagerConfig.StripQueryParameters(hashFilePath);
				}
				ResourceLocationBase hashResourceLocation = new ResourceLocationBase(hashFilePath, hashFilePath, typeof(TextDataProvider).FullName, typeof(string), Array.Empty<IResourceLocation>())
				{
					Data = hashOptions.Copy()
				};
				catalogLoc.Dependencies.Add(hashResourceLocation);
				string text = this.ResolveInternalId("{UnityEngine.Application.persistentDataPath}/com.unity.addressables/" + tmpPath.GetHashCode().ToString() + ".hash");
				ResourceLocationBase cacheResourceLocation = new ResourceLocationBase(text, text, typeof(TextDataProvider).FullName, typeof(string), Array.Empty<IResourceLocation>())
				{
					Data = hashOptions.Copy()
				};
				catalogLoc.Dependencies.Add(cacheResourceLocation);
				catalogLoc.Dependencies.Add(cacheResourceLocation);
			}
			return catalogLoc;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000039E3 File Offset: 0x00001BE3
		[Conditional("UNITY_EDITOR")]
		private void QueueEditorUpdateIfNeeded()
		{
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003F58 File Offset: 0x00002158
		public AsyncOperationHandle<IResourceLocator> LoadContentCatalogAsync(string catalogPath, bool autoReleaseHandle = true, string providerSuffix = null)
		{
			ResourceLocationBase catalogLoc = this.CreateCatalogLocationWithHashDependencies<ContentCatalogProvider>(catalogPath);
			if (this.ShouldChainRequest)
			{
				return this.ResourceManager.CreateChainOperation<IResourceLocator>(this.ChainOperation, (AsyncOperationHandle op) => this.LoadContentCatalogAsync(catalogPath, autoReleaseHandle, providerSuffix));
			}
			AsyncOperationHandle<IResourceLocator> handle = InitializationOperation.LoadContentCatalog(this, catalogLoc, providerSuffix, null);
			if (autoReleaseHandle)
			{
				handle.ReleaseHandleOnCompletion();
			}
			return handle;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003FD9 File Offset: 0x000021D9
		private AsyncOperationHandle<SceneInstance> TrackHandle(AsyncOperationHandle<SceneInstance> handle)
		{
			handle.Completed += delegate(AsyncOperationHandle<SceneInstance> sceneHandle)
			{
				this.m_OnSceneHandleCompleteAction(sceneHandle);
			};
			return handle;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003FEF File Offset: 0x000021EF
		private AsyncOperationHandle<TObject> TrackHandle<TObject>(AsyncOperationHandle<TObject> handle)
		{
			handle.CompletedTypeless += this.m_OnHandleCompleteAction;
			return handle;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003FFF File Offset: 0x000021FF
		private AsyncOperationHandle TrackHandle(AsyncOperationHandle handle)
		{
			handle.Completed += this.m_OnHandleCompleteAction;
			return handle;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000400F File Offset: 0x0000220F
		internal void ClearTrackHandles()
		{
			this.m_resultToHandle.Clear();
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000401C File Offset: 0x0000221C
		public AsyncOperationHandle<TObject> LoadAssetAsync<TObject>(IResourceLocation location)
		{
			if (this.ShouldChainRequest)
			{
				return this.TrackHandle<TObject>(this.LoadAssetWithChain<TObject>(this.ChainOperation, location));
			}
			return this.TrackHandle<TObject>(this.ResourceManager.ProvideResource<TObject>(location));
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000404C File Offset: 0x0000224C
		private AsyncOperationHandle<TObject> LoadAssetWithChain<TObject>(AsyncOperationHandle dep, IResourceLocation loc)
		{
			return this.ResourceManager.CreateChainOperation<TObject>(dep, (AsyncOperationHandle op) => this.LoadAssetAsync<TObject>(loc));
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004088 File Offset: 0x00002288
		private AsyncOperationHandle<TObject> LoadAssetWithChain<TObject>(AsyncOperationHandle dep, object key)
		{
			return this.ResourceManager.CreateChainOperation<TObject>(dep, (AsyncOperationHandle op) => this.LoadAssetAsync<TObject>(key));
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000040C4 File Offset: 0x000022C4
		public AsyncOperationHandle<TObject> LoadAssetAsync<TObject>(object key)
		{
			if (this.ShouldChainRequest)
			{
				return this.TrackHandle<TObject>(this.LoadAssetWithChain<TObject>(this.ChainOperation, key));
			}
			key = this.EvaluateKey(key);
			Type t = typeof(TObject);
			if (t.IsArray)
			{
				t = t.GetElementType();
			}
			else if (t.IsGenericType && typeof(IList<>) == t.GetGenericTypeDefinition())
			{
				t = t.GetGenericArguments()[0];
			}
			using (List<ResourceLocatorInfo>.Enumerator enumerator = this.m_ResourceLocators.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					IList<IResourceLocation> locs;
					if (enumerator.Current.Locator.Locate(key, t, out locs))
					{
						foreach (IResourceLocation loc in locs)
						{
							if (this.ResourceManager.GetResourceProvider(typeof(TObject), loc) != null)
							{
								return this.TrackHandle<TObject>(this.ResourceManager.ProvideResource<TObject>(loc));
							}
						}
					}
				}
			}
			return this.ResourceManager.CreateCompletedOperationWithException<TObject>(default(TObject), new InvalidKeyException(key, t, this));
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000420C File Offset: 0x0000240C
		public AsyncOperationHandle<IList<IResourceLocation>> LoadResourceLocationsWithChain(AsyncOperationHandle dep, IEnumerable keys, Addressables.MergeMode mode, Type type)
		{
			return this.ResourceManager.CreateChainOperation<IList<IResourceLocation>>(dep, (AsyncOperationHandle op) => this.LoadResourceLocationsAsync(keys, mode, type));
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004254 File Offset: 0x00002454
		public AsyncOperationHandle<IList<IResourceLocation>> LoadResourceLocationsAsync(IEnumerable keys, Addressables.MergeMode mode, Type type = null)
		{
			if (this.ShouldChainRequest)
			{
				return this.TrackHandle<IList<IResourceLocation>>(this.LoadResourceLocationsWithChain(this.ChainOperation, keys, mode, type));
			}
			AddressablesImpl.LoadResourceLocationKeysOp op = new AddressablesImpl.LoadResourceLocationKeysOp();
			op.Init(this, type, keys, mode);
			return this.TrackHandle<IList<IResourceLocation>>(this.ResourceManager.StartOperation<IList<IResourceLocation>>(op, default(AsyncOperationHandle)));
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000042AC File Offset: 0x000024AC
		public AsyncOperationHandle<IList<IResourceLocation>> LoadResourceLocationsWithChain(AsyncOperationHandle dep, object key, Type type)
		{
			return this.ResourceManager.CreateChainOperation<IList<IResourceLocation>>(dep, (AsyncOperationHandle op) => this.LoadResourceLocationsAsync(key, type));
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000042EC File Offset: 0x000024EC
		public AsyncOperationHandle<IList<IResourceLocation>> LoadResourceLocationsAsync(object key, Type type = null)
		{
			if (this.ShouldChainRequest)
			{
				return this.TrackHandle<IList<IResourceLocation>>(this.LoadResourceLocationsWithChain(this.ChainOperation, key, type));
			}
			AddressablesImpl.LoadResourceLocationKeyOp op = new AddressablesImpl.LoadResourceLocationKeyOp();
			op.Init(this, type, key);
			return this.TrackHandle<IList<IResourceLocation>>(this.ResourceManager.StartOperation<IList<IResourceLocation>>(op, default(AsyncOperationHandle)));
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004340 File Offset: 0x00002540
		public AsyncOperationHandle<IList<TObject>> LoadAssetsAsync<TObject>(IList<IResourceLocation> locations, Action<TObject> callback, bool releaseDependenciesOnFailure)
		{
			if (this.ShouldChainRequest)
			{
				return this.TrackHandle<IList<TObject>>(this.LoadAssetsWithChain<TObject>(this.ChainOperation, locations, callback, releaseDependenciesOnFailure));
			}
			return this.TrackHandle<IList<TObject>>(this.ResourceManager.ProvideResources<TObject>(locations, releaseDependenciesOnFailure, callback));
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004374 File Offset: 0x00002574
		private AsyncOperationHandle<IList<TObject>> LoadAssetsWithChain<TObject>(AsyncOperationHandle dep, IList<IResourceLocation> locations, Action<TObject> callback, bool releaseDependenciesOnFailure)
		{
			return this.ResourceManager.CreateChainOperation<IList<TObject>>(dep, (AsyncOperationHandle op) => this.LoadAssetsAsync<TObject>(locations, callback, releaseDependenciesOnFailure));
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000043BC File Offset: 0x000025BC
		private AsyncOperationHandle<IList<TObject>> LoadAssetsWithChain<TObject>(AsyncOperationHandle dep, IEnumerable keys, Action<TObject> callback, Addressables.MergeMode mode, bool releaseDependenciesOnFailure)
		{
			return this.ResourceManager.CreateChainOperation<IList<TObject>>(dep, (AsyncOperationHandle op) => this.LoadAssetsAsync<TObject>(keys, callback, mode, releaseDependenciesOnFailure));
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000440C File Offset: 0x0000260C
		public AsyncOperationHandle<IList<TObject>> LoadAssetsAsync<TObject>(IEnumerable keys, Action<TObject> callback, Addressables.MergeMode mode, bool releaseDependenciesOnFailure)
		{
			if (this.ShouldChainRequest)
			{
				return this.TrackHandle<IList<TObject>>(this.LoadAssetsWithChain<TObject>(this.ChainOperation, keys, callback, mode, releaseDependenciesOnFailure));
			}
			IList<IResourceLocation> locations;
			if (!this.GetResourceLocations(keys, typeof(TObject), mode, out locations))
			{
				return this.ResourceManager.CreateCompletedOperationWithException<IList<TObject>>(null, new InvalidKeyException(keys, typeof(TObject), mode, this));
			}
			return this.LoadAssetsAsync<TObject>(locations, callback, releaseDependenciesOnFailure);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004478 File Offset: 0x00002678
		private AsyncOperationHandle<IList<TObject>> LoadAssetsWithChain<TObject>(AsyncOperationHandle dep, object key, Action<TObject> callback, bool releaseDependenciesOnFailure)
		{
			return this.ResourceManager.CreateChainOperation<IList<TObject>>(dep, (AsyncOperationHandle op2) => this.LoadAssetsAsync<TObject>(key, callback, releaseDependenciesOnFailure));
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000044C0 File Offset: 0x000026C0
		public AsyncOperationHandle<IList<TObject>> LoadAssetsAsync<TObject>(object key, Action<TObject> callback, bool releaseDependenciesOnFailure)
		{
			if (this.ShouldChainRequest)
			{
				return this.TrackHandle<IList<TObject>>(this.LoadAssetsWithChain<TObject>(this.ChainOperation, key, callback, releaseDependenciesOnFailure));
			}
			IList<IResourceLocation> locations;
			if (!this.GetResourceLocations(key, typeof(TObject), out locations))
			{
				return this.ResourceManager.CreateCompletedOperationWithException<IList<TObject>>(null, new InvalidKeyException(key, typeof(TObject), this));
			}
			return this.LoadAssetsAsync<TObject>(locations, callback, releaseDependenciesOnFailure);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004527 File Offset: 0x00002727
		private void OnHandleDestroyed(AsyncOperationHandle handle)
		{
			if (handle.Status == AsyncOperationStatus.Succeeded)
			{
				this.m_resultToHandle.Remove(handle.Result);
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004546 File Offset: 0x00002746
		private void OnSceneHandleCompleted(AsyncOperationHandle handle)
		{
			if (handle.Status == AsyncOperationStatus.Succeeded)
			{
				this.m_SceneInstances.Add(handle);
				if (this.m_resultToHandle.TryAdd(handle.Result, handle))
				{
					handle.Destroyed += this.m_OnHandleDestroyedAction;
				}
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004581 File Offset: 0x00002781
		private void OnHandleCompleted(AsyncOperationHandle handle)
		{
			if (handle.Status == AsyncOperationStatus.Succeeded && this.m_resultToHandle.TryAdd(handle.Result, handle))
			{
				handle.Destroyed += this.m_OnHandleDestroyedAction;
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000045B0 File Offset: 0x000027B0
		public void Release<TObject>(TObject obj)
		{
			if (obj == null)
			{
				this.LogWarning("Addressables.Release() - trying to release null object.");
				return;
			}
			AsyncOperationHandle handle;
			if (this.m_resultToHandle.TryGetValue(obj, out handle))
			{
				handle.Release();
				return;
			}
			this.LogError("Addressables.Release was called on an object that Addressables was not previously aware of.  Thus nothing is being released");
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000045F9 File Offset: 0x000027F9
		public void Release<TObject>(AsyncOperationHandle<TObject> handle)
		{
			this.m_ResourceManager.Release(handle);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000460C File Offset: 0x0000280C
		public void Release(AsyncOperationHandle handle)
		{
			this.m_ResourceManager.Release(handle);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000461C File Offset: 0x0000281C
		private AsyncOperationHandle<long> GetDownloadSizeWithChain(AsyncOperationHandle dep, object key)
		{
			return this.ResourceManager.CreateChainOperation<long>(dep, (AsyncOperationHandle op) => this.GetDownloadSizeAsync(key));
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004658 File Offset: 0x00002858
		private AsyncOperationHandle<long> ComputeCatalogSizeWithChain(IResourceLocation catalogLoc)
		{
			if (!catalogLoc.HasDependencies)
			{
				return this.ResourceManager.CreateCompletedOperation<long>(0L, "Attempting to get the remote header size of a content catalog, but no dependencies pointing to a remote location could be found for location " + catalogLoc.InternalId + ". Catalog location dependencies can be setup using CreateCatalogLocationWithHashDependencies");
			}
			AsyncOperationHandle dep = this.ResourceManager.ProvideResource<string>(catalogLoc.Dependencies[0]);
			return this.ResourceManager.CreateChainOperation<long>(dep, delegate(AsyncOperationHandle op)
			{
				try
				{
					Hash128 remoteHash = Hash128.Parse(op.Result.ToString());
					if (!this.IsCatalogCached(catalogLoc, remoteHash))
					{
						return this.GetRemoteCatalogHeaderSize(catalogLoc);
					}
				}
				catch (Exception e)
				{
					return this.ResourceManager.CreateCompletedOperation<long>(0L, string.Format("Fetching the remote catalog size failed. {0}", e));
				}
				return this.ResourceManager.CreateCompletedOperation<long>(0L, string.Empty);
			});
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000046E8 File Offset: 0x000028E8
		internal bool IsCatalogCached(IResourceLocation catalogLoc, Hash128 remoteHash)
		{
			return catalogLoc.HasDependencies && catalogLoc.Dependencies.Count == 2 && File.Exists(catalogLoc.Dependencies[1].InternalId) && !(remoteHash != Hash128.Parse(File.ReadAllText(catalogLoc.Dependencies[1].InternalId)));
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0000474C File Offset: 0x0000294C
		internal AsyncOperationHandle<long> GetRemoteCatalogHeaderSize(IResourceLocation catalogLoc)
		{
			if (!catalogLoc.HasDependencies)
			{
				return this.ResourceManager.CreateCompletedOperation<long>(0L, "Attempting to get the remote header size of a content catalog, but no dependencies pointing to a remote location could be found for location " + catalogLoc.InternalId + ". Catalog location dependencies can be setup using CreateCatalogLocationWithHashDependencies");
			}
			AsyncOperationBase<UnityWebRequest> uwrAsyncOp = new UnityWebRequestOperation(new UnityWebRequest(catalogLoc.Dependencies[0].InternalId.Replace(".hash", ".bin"), "HEAD"));
			return this.ResourceManager.CreateChainOperation<long, UnityWebRequest>(this.ResourceManager.StartOperation<UnityWebRequest>(uwrAsyncOp, default(AsyncOperationHandle)), delegate(AsyncOperationHandle<UnityWebRequest> getOp)
			{
				UnityWebRequest result2 = getOp.Result;
				string response = ((result2 != null) ? result2.GetResponseHeader("Content-Length") : null);
				long result;
				if (response != null && long.TryParse(response, out result))
				{
					return this.ResourceManager.CreateCompletedOperation<long>(result, "");
				}
				return this.ResourceManager.CreateCompletedOperation<long>(0L, "Attempting to get the remote header of a catalog failed.");
			});
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000047E0 File Offset: 0x000029E0
		private AsyncOperationHandle<long> GetDownloadSizeWithChain(AsyncOperationHandle dep, IEnumerable keys)
		{
			return this.ResourceManager.CreateChainOperation<long>(dep, (AsyncOperationHandle op) => this.GetDownloadSizeAsync(keys));
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004819 File Offset: 0x00002A19
		public AsyncOperationHandle<long> GetDownloadSizeAsync(object key)
		{
			return this.GetDownloadSizeAsync(new object[] { key });
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000482C File Offset: 0x00002A2C
		public AsyncOperationHandle<long> GetDownloadSizeAsync(IEnumerable keys)
		{
			if (this.ShouldChainRequest)
			{
				return this.TrackHandle<long>(this.GetDownloadSizeWithChain(this.ChainOperation, keys));
			}
			List<IResourceLocation> allLocations = new List<IResourceLocation>();
			foreach (object key in keys)
			{
				IList<IResourceLocation> locations;
				if (key is IList<IResourceLocation>)
				{
					locations = key as IList<IResourceLocation>;
				}
				else if (key is IResourceLocation)
				{
					using (List<ResourceLocatorInfo>.Enumerator enumerator2 = this.m_ResourceLocators.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							if (enumerator2.Current.CatalogLocation == key as IResourceLocation)
							{
								return this.ComputeCatalogSizeWithChain(key as IResourceLocation);
							}
						}
					}
					locations = new List<IResourceLocation>(1) { key as IResourceLocation };
				}
				else if (!this.GetResourceLocations(key, typeof(object), out locations))
				{
					return this.ResourceManager.CreateCompletedOperationWithException<long>(0L, new InvalidKeyException(key, typeof(object), this));
				}
				foreach (IResourceLocation loc in locations)
				{
					if (loc.HasDependencies)
					{
						allLocations.AddRange(loc.Dependencies);
					}
				}
			}
			long size = 0L;
			foreach (IResourceLocation location in allLocations.Distinct(new ResourceLocationComparer()))
			{
				ILocationSizeData sizeData = location.Data as ILocationSizeData;
				if (sizeData != null)
				{
					size += sizeData.ComputeSize(location, this.ResourceManager);
				}
			}
			return this.ResourceManager.CreateCompletedOperation<long>(size, string.Empty);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004A60 File Offset: 0x00002C60
		private AsyncOperationHandle DownloadDependenciesAsyncWithChain(AsyncOperationHandle dep, object key, bool autoReleaseHandle)
		{
			AsyncOperationHandle<IList<IAssetBundleResource>> handle = this.ResourceManager.CreateChainOperation<IList<IAssetBundleResource>>(dep, (AsyncOperationHandle op) => this.DownloadDependenciesAsync(key, false).Convert<IList<IAssetBundleResource>>());
			if (autoReleaseHandle)
			{
				handle.ReleaseHandleOnCompletion();
			}
			return handle;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004AAC File Offset: 0x00002CAC
		internal static void WrapAsDownloadLocations(List<IResourceLocation> locations)
		{
			for (int i = 0; i < locations.Count; i++)
			{
				locations[i] = new DownloadOnlyLocation(locations[i]);
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004AE0 File Offset: 0x00002CE0
		private static List<IResourceLocation> GatherDependenciesFromLocations(IList<IResourceLocation> locations)
		{
			HashSet<IResourceLocation> locHash = new HashSet<IResourceLocation>(new ResourceLocationComparer());
			foreach (IResourceLocation loc in locations)
			{
				if (loc.ResourceType == typeof(IAssetBundleResource))
				{
					locHash.Add(loc);
				}
				if (loc.HasDependencies)
				{
					foreach (IResourceLocation dep in loc.Dependencies)
					{
						if (dep.ResourceType == typeof(IAssetBundleResource))
						{
							locHash.Add(dep);
						}
					}
				}
			}
			return new List<IResourceLocation>(locHash);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004BB4 File Offset: 0x00002DB4
		public AsyncOperationHandle DownloadDependenciesAsync(object key, bool autoReleaseHandle = false)
		{
			if (this.ShouldChainRequest)
			{
				return this.DownloadDependenciesAsyncWithChain(this.ChainOperation, key, autoReleaseHandle);
			}
			IList<IResourceLocation> locations;
			if (!this.GetResourceLocations(key, typeof(object), out locations))
			{
				AsyncOperationHandle<IList<IAssetBundleResource>> handle = this.ResourceManager.CreateCompletedOperationWithException<IList<IAssetBundleResource>>(null, new InvalidKeyException(key, typeof(object), this));
				if (autoReleaseHandle)
				{
					handle.ReleaseHandleOnCompletion();
				}
				return handle;
			}
			List<IResourceLocation> dlLocations = AddressablesImpl.GatherDependenciesFromLocations(locations);
			AddressablesImpl.WrapAsDownloadLocations(dlLocations);
			AsyncOperationHandle<IList<IAssetBundleResource>> handle2 = this.LoadAssetsAsync<IAssetBundleResource>(dlLocations, null, true);
			if (autoReleaseHandle)
			{
				handle2.ReleaseHandleOnCompletion();
			}
			return handle2;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004C44 File Offset: 0x00002E44
		private AsyncOperationHandle DownloadDependenciesAsyncWithChain(AsyncOperationHandle dep, IList<IResourceLocation> locations, bool autoReleaseHandle)
		{
			AsyncOperationHandle<IList<IAssetBundleResource>> handle = this.ResourceManager.CreateChainOperation<IList<IAssetBundleResource>>(dep, (AsyncOperationHandle op) => this.DownloadDependenciesAsync(locations, false).Convert<IList<IAssetBundleResource>>());
			if (autoReleaseHandle)
			{
				handle.ReleaseHandleOnCompletion();
			}
			return handle;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004C90 File Offset: 0x00002E90
		public AsyncOperationHandle DownloadDependenciesAsync(IList<IResourceLocation> locations, bool autoReleaseHandle = false)
		{
			if (this.ShouldChainRequest)
			{
				return this.DownloadDependenciesAsyncWithChain(this.ChainOperation, locations, autoReleaseHandle);
			}
			List<IResourceLocation> dlLocations = AddressablesImpl.GatherDependenciesFromLocations(locations);
			AddressablesImpl.WrapAsDownloadLocations(dlLocations);
			AsyncOperationHandle<IList<IAssetBundleResource>> handle = this.LoadAssetsAsync<IAssetBundleResource>(dlLocations, null, true);
			if (autoReleaseHandle)
			{
				handle.ReleaseHandleOnCompletion();
			}
			return handle;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004CDC File Offset: 0x00002EDC
		private AsyncOperationHandle DownloadDependenciesAsyncWithChain(AsyncOperationHandle dep, IEnumerable keys, Addressables.MergeMode mode, bool autoReleaseHandle)
		{
			AsyncOperationHandle<IList<IAssetBundleResource>> handle = this.ResourceManager.CreateChainOperation<IList<IAssetBundleResource>>(dep, (AsyncOperationHandle op) => this.DownloadDependenciesAsync(keys, mode, false).Convert<IList<IAssetBundleResource>>());
			if (autoReleaseHandle)
			{
				handle.ReleaseHandleOnCompletion();
			}
			return handle;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004D30 File Offset: 0x00002F30
		public AsyncOperationHandle DownloadDependenciesAsync(IEnumerable keys, Addressables.MergeMode mode, bool autoReleaseHandle = false)
		{
			if (this.ShouldChainRequest)
			{
				return this.DownloadDependenciesAsyncWithChain(this.ChainOperation, keys, mode, autoReleaseHandle);
			}
			IList<IResourceLocation> locations;
			if (!this.GetResourceLocations(keys, typeof(object), mode, out locations))
			{
				AsyncOperationHandle<IList<IAssetBundleResource>> handle = this.ResourceManager.CreateCompletedOperationWithException<IList<IAssetBundleResource>>(null, new InvalidKeyException(keys, typeof(object), mode, this));
				if (autoReleaseHandle)
				{
					handle.ReleaseHandleOnCompletion();
				}
				return handle;
			}
			List<IResourceLocation> dlLocations = AddressablesImpl.GatherDependenciesFromLocations(locations);
			AddressablesImpl.WrapAsDownloadLocations(dlLocations);
			AsyncOperationHandle<IList<IAssetBundleResource>> handle2 = this.LoadAssetsAsync<IAssetBundleResource>(dlLocations, null, true);
			if (autoReleaseHandle)
			{
				handle2.ReleaseHandleOnCompletion();
			}
			return handle2;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004DC4 File Offset: 0x00002FC4
		internal bool ClearDependencyCacheForKey(object key)
		{
			bool result = true;
			IList<IResourceLocation> bundleLocations = null;
			IList<IResourceLocation> locations;
			if (key is IResourceLocation && (key as IResourceLocation).HasDependencies)
			{
				bundleLocations = AddressablesImpl.GatherDependenciesFromLocations((key as IResourceLocation).Dependencies);
			}
			else if (this.GetResourceLocations(key, typeof(object), out locations))
			{
				bundleLocations = AddressablesImpl.GatherDependenciesFromLocations(locations);
			}
			if (bundleLocations != null)
			{
				foreach (IResourceLocation dep in bundleLocations)
				{
					AssetBundleRequestOptions bundleData = dep.Data as AssetBundleRequestOptions;
					if (bundleData != null)
					{
						string bundleName = bundleData.BundleName;
						if (this.m_ResourceManager.GetOperationFromCache(dep, typeof(IAssetBundleResource)) != null)
						{
							Debug.LogWarning(string.Concat(new string[] { "Attempting to clear cached version including ", bundleName, ", while ", bundleName, " is currently loaded." }));
							if (!string.IsNullOrEmpty(bundleData.Hash))
							{
								Hash128 currentVersion = Hash128.Parse(bundleData.Hash);
								Caching.ClearOtherCachedVersions(bundleName, currentVersion);
							}
						}
						else
						{
							result = result && Caching.ClearAllCachedVersions(bundleName);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004EFC File Offset: 0x000030FC
		internal void AutoReleaseHandleOnTypelessCompletion<TObject>(AsyncOperationHandle<TObject> handle)
		{
			handle.CompletedTypeless += delegate(AsyncOperationHandle op)
			{
				op.Release();
			};
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004F24 File Offset: 0x00003124
		public AsyncOperationHandle<bool> ClearDependencyCacheAsync(object key, bool autoReleaseHandle)
		{
			if (this.ShouldChainRequest)
			{
				AsyncOperationHandle<bool> chainOp = this.ResourceManager.CreateChainOperation<bool>(this.ChainOperation, (AsyncOperationHandle op) => this.ClearDependencyCacheAsync(key, autoReleaseHandle));
				if (autoReleaseHandle)
				{
					chainOp.ReleaseHandleOnCompletion();
				}
				return chainOp;
			}
			bool result = this.ClearDependencyCacheForKey(key);
			AsyncOperationHandle<bool> completedOp = this.ResourceManager.CreateCompletedOperation<bool>(result, result ? string.Empty : "Unable to clear the cache.  AssetBundle's may still be loaded for the given key.");
			if (autoReleaseHandle)
			{
				completedOp.ReleaseHandleOnCompletion();
			}
			return completedOp;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004FBC File Offset: 0x000031BC
		public AsyncOperationHandle<bool> ClearDependencyCacheAsync(IList<IResourceLocation> locations, bool autoReleaseHandle)
		{
			if (this.ShouldChainRequest)
			{
				AsyncOperationHandle<bool> chainOp = this.ResourceManager.CreateChainOperation<bool>(this.ChainOperation, (AsyncOperationHandle op) => this.ClearDependencyCacheAsync(locations, autoReleaseHandle));
				if (autoReleaseHandle)
				{
					chainOp.ReleaseHandleOnCompletion();
				}
				return chainOp;
			}
			bool result = true;
			foreach (IResourceLocation location in locations)
			{
				result = result && this.ClearDependencyCacheForKey(location);
			}
			AsyncOperationHandle<bool> completedOp = this.ResourceManager.CreateCompletedOperation<bool>(result, result ? string.Empty : "Unable to clear the cache.  AssetBundle's may still be loaded for the given key(s).");
			if (autoReleaseHandle)
			{
				completedOp.ReleaseHandleOnCompletion();
			}
			return completedOp;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00005098 File Offset: 0x00003298
		public AsyncOperationHandle<bool> ClearDependencyCacheAsync(IEnumerable keys, bool autoReleaseHandle)
		{
			if (this.ShouldChainRequest)
			{
				AsyncOperationHandle<bool> chainOp = this.ResourceManager.CreateChainOperation<bool>(this.ChainOperation, (AsyncOperationHandle op) => this.ClearDependencyCacheAsync(keys, autoReleaseHandle));
				if (autoReleaseHandle)
				{
					chainOp.ReleaseHandleOnCompletion();
				}
				return chainOp;
			}
			bool result = true;
			foreach (object key in keys)
			{
				result = result && this.ClearDependencyCacheForKey(key);
			}
			AsyncOperationHandle<bool> completedOp = this.ResourceManager.CreateCompletedOperation<bool>(result, result ? string.Empty : "Unable to clear the cache.  AssetBundle's may still be loaded for the given key(s).");
			if (autoReleaseHandle)
			{
				completedOp.ReleaseHandleOnCompletion();
			}
			return completedOp;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000517C File Offset: 0x0000337C
		public AsyncOperationHandle<GameObject> InstantiateAsync(IResourceLocation location, Transform parent = null, bool instantiateInWorldSpace = false, bool trackHandle = true)
		{
			return this.InstantiateAsync(location, new InstantiationParameters(parent, instantiateInWorldSpace), trackHandle);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000518E File Offset: 0x0000338E
		public AsyncOperationHandle<GameObject> InstantiateAsync(IResourceLocation location, Vector3 position, Quaternion rotation, Transform parent = null, bool trackHandle = true)
		{
			return this.InstantiateAsync(location, new InstantiationParameters(position, rotation, parent), trackHandle);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000051A2 File Offset: 0x000033A2
		public AsyncOperationHandle<GameObject> InstantiateAsync(object key, Transform parent = null, bool instantiateInWorldSpace = false, bool trackHandle = true)
		{
			return this.InstantiateAsync(key, new InstantiationParameters(parent, instantiateInWorldSpace), trackHandle);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000051B4 File Offset: 0x000033B4
		public AsyncOperationHandle<GameObject> InstantiateAsync(object key, Vector3 position, Quaternion rotation, Transform parent = null, bool trackHandle = true)
		{
			return this.InstantiateAsync(key, new InstantiationParameters(position, rotation, parent), trackHandle);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000051C8 File Offset: 0x000033C8
		private AsyncOperationHandle<GameObject> InstantiateWithChain(AsyncOperationHandle dep, object key, InstantiationParameters instantiateParameters, bool trackHandle = true)
		{
			AsyncOperationHandle<GameObject> chainOp = this.ResourceManager.CreateChainOperation<GameObject>(dep, (AsyncOperationHandle op) => this.InstantiateAsync(key, instantiateParameters, false));
			if (trackHandle)
			{
				chainOp.CompletedTypeless += this.m_OnHandleCompleteAction;
			}
			return chainOp;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000521C File Offset: 0x0000341C
		public AsyncOperationHandle<GameObject> InstantiateAsync(object key, InstantiationParameters instantiateParameters, bool trackHandle = true)
		{
			if (this.ShouldChainRequest)
			{
				return this.InstantiateWithChain(this.ChainOperation, key, instantiateParameters, trackHandle);
			}
			key = this.EvaluateKey(key);
			using (List<ResourceLocatorInfo>.Enumerator enumerator = this.m_ResourceLocators.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					IList<IResourceLocation> locs;
					if (enumerator.Current.Locator.Locate(key, typeof(GameObject), out locs))
					{
						return this.InstantiateAsync(locs[0], instantiateParameters, trackHandle);
					}
				}
			}
			return this.ResourceManager.CreateCompletedOperationWithException<GameObject>(null, new InvalidKeyException(key, typeof(GameObject), this));
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000052D4 File Offset: 0x000034D4
		private AsyncOperationHandle<GameObject> InstantiateWithChain(AsyncOperationHandle dep, IResourceLocation location, InstantiationParameters instantiateParameters, bool trackHandle = true)
		{
			AsyncOperationHandle<GameObject> chainOp = this.ResourceManager.CreateChainOperation<GameObject>(dep, (AsyncOperationHandle op) => this.InstantiateAsync(location, instantiateParameters, false));
			if (trackHandle)
			{
				chainOp.CompletedTypeless += this.m_OnHandleCompleteAction;
			}
			return chainOp;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00005328 File Offset: 0x00003528
		public AsyncOperationHandle<GameObject> InstantiateAsync(IResourceLocation location, InstantiationParameters instantiateParameters, bool trackHandle = true)
		{
			if (this.ShouldChainRequest)
			{
				return this.InstantiateWithChain(this.ChainOperation, location, instantiateParameters, trackHandle);
			}
			AsyncOperationHandle<GameObject> opHandle = this.ResourceManager.ProvideInstance(this.InstanceProvider, location, instantiateParameters);
			if (!trackHandle)
			{
				return opHandle;
			}
			opHandle.CompletedTypeless += this.m_OnHandleCompleteAction;
			return opHandle;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00005374 File Offset: 0x00003574
		public bool ReleaseInstance(GameObject instance)
		{
			if (instance == null)
			{
				this.LogWarning("Addressables.ReleaseInstance() - trying to release null object.");
				return false;
			}
			AsyncOperationHandle handle;
			if (this.m_resultToHandle.TryGetValue(instance, out handle))
			{
				handle.Release();
				return true;
			}
			return false;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000053B4 File Offset: 0x000035B4
		internal AsyncOperationHandle<SceneInstance> LoadSceneWithChain(AsyncOperationHandle dep, object key, LoadSceneParameters loadSceneParameters, SceneReleaseMode releaseMode = SceneReleaseMode.ReleaseSceneWhenSceneUnloaded, bool activateOnLoad = true, int priority = 100)
		{
			return this.TrackHandle(this.ResourceManager.CreateChainOperation<SceneInstance>(dep, (AsyncOperationHandle op) => this.LoadSceneAsync(key, loadSceneParameters, releaseMode, activateOnLoad, priority, false)));
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00005414 File Offset: 0x00003614
		internal AsyncOperationHandle<SceneInstance> LoadSceneWithChain(AsyncOperationHandle dep, IResourceLocation key, LoadSceneParameters loadSceneParameters, SceneReleaseMode releaseMode = SceneReleaseMode.ReleaseSceneWhenSceneUnloaded, bool activateOnLoad = true, int priority = 100)
		{
			return this.TrackHandle(this.ResourceManager.CreateChainOperation<SceneInstance>(dep, (AsyncOperationHandle op) => this.LoadSceneAsync(key, loadSceneParameters, releaseMode, activateOnLoad, priority, false)));
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005474 File Offset: 0x00003674
		public AsyncOperationHandle<SceneInstance> LoadSceneAsync(object key, LoadSceneParameters loadSceneParameters, SceneReleaseMode releaseMode = SceneReleaseMode.ReleaseSceneWhenSceneUnloaded, bool activateOnLoad = true, int priority = 100, bool trackHandle = true)
		{
			if (this.ShouldChainRequest)
			{
				return this.LoadSceneWithChain(this.ChainOperation, key, loadSceneParameters, releaseMode, activateOnLoad, priority);
			}
			IList<IResourceLocation> locations;
			if (!this.GetResourceLocations(key, typeof(SceneInstance), out locations))
			{
				return this.ResourceManager.CreateCompletedOperationWithException<SceneInstance>(default(SceneInstance), new InvalidKeyException(key, typeof(SceneInstance), this));
			}
			return this.LoadSceneAsync(locations[0], loadSceneParameters, releaseMode, activateOnLoad, priority, trackHandle);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000054F0 File Offset: 0x000036F0
		public AsyncOperationHandle<SceneInstance> LoadSceneAsync(IResourceLocation location, LoadSceneParameters loadSceneParameters, SceneReleaseMode releaseMode = SceneReleaseMode.ReleaseSceneWhenSceneUnloaded, bool activateOnLoad = true, int priority = 100, bool trackHandle = true)
		{
			if (this.ShouldChainRequest)
			{
				return this.LoadSceneWithChain(this.ChainOperation, location, loadSceneParameters, releaseMode, activateOnLoad, priority);
			}
			AsyncOperationHandle<SceneInstance> handle = this.ResourceManager.ProvideScene(this.SceneProvider, location, loadSceneParameters, releaseMode, activateOnLoad, priority);
			if (trackHandle)
			{
				return this.TrackHandle(handle);
			}
			return handle;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005540 File Offset: 0x00003740
		public AsyncOperationHandle<SceneInstance> UnloadSceneAsync(SceneInstance scene, UnloadSceneOptions unloadOptions = UnloadSceneOptions.None, bool autoReleaseHandle = true)
		{
			AsyncOperationHandle handle;
			if (!this.m_resultToHandle.TryGetValue(scene, out handle))
			{
				string msg = string.Format("Addressables.UnloadSceneAsync() - Cannot find handle for scene {0}", scene);
				this.LogWarning(msg);
				return this.ResourceManager.CreateCompletedOperation<SceneInstance>(scene, msg);
			}
			if (handle.m_InternalOp.IsRunning)
			{
				return this.CreateUnloadSceneWithChain(handle, unloadOptions, autoReleaseHandle);
			}
			return this.UnloadSceneAsync(handle, unloadOptions, autoReleaseHandle);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000055A8 File Offset: 0x000037A8
		public AsyncOperationHandle<SceneInstance> UnloadSceneAsync(AsyncOperationHandle handle, UnloadSceneOptions unloadOptions = UnloadSceneOptions.None, bool autoReleaseHandle = true)
		{
			if (handle.m_InternalOp.IsRunning)
			{
				return this.CreateUnloadSceneWithChain(handle, unloadOptions, autoReleaseHandle);
			}
			return this.UnloadSceneAsync(handle.Convert<SceneInstance>(), unloadOptions, autoReleaseHandle);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000055D0 File Offset: 0x000037D0
		public AsyncOperationHandle<SceneInstance> UnloadSceneAsync(AsyncOperationHandle<SceneInstance> handle, UnloadSceneOptions unloadOptions = UnloadSceneOptions.None, bool autoReleaseHandle = true)
		{
			if (handle.m_InternalOp.IsRunning)
			{
				return this.CreateUnloadSceneWithChain(handle, unloadOptions, autoReleaseHandle);
			}
			return this.InternalUnloadScene(handle, unloadOptions, autoReleaseHandle);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000055F4 File Offset: 0x000037F4
		internal AsyncOperationHandle<SceneInstance> CreateUnloadSceneWithChain(AsyncOperationHandle handle, UnloadSceneOptions unloadOptions, bool autoReleaseHandle)
		{
			return this.m_ResourceManager.CreateChainOperation<SceneInstance>(handle, (AsyncOperationHandle completedHandle) => this.InternalUnloadScene(completedHandle.Convert<SceneInstance>(), unloadOptions, autoReleaseHandle));
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005634 File Offset: 0x00003834
		internal AsyncOperationHandle<SceneInstance> CreateUnloadSceneWithChain(AsyncOperationHandle<SceneInstance> handle, UnloadSceneOptions unloadOptions, bool autoReleaseHandle)
		{
			return this.m_ResourceManager.CreateChainOperation<SceneInstance, SceneInstance>(handle, (AsyncOperationHandle<SceneInstance> completedHandle) => this.InternalUnloadScene(completedHandle, unloadOptions, autoReleaseHandle));
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005674 File Offset: 0x00003874
		internal AsyncOperationHandle<SceneInstance> InternalUnloadScene(AsyncOperationHandle<SceneInstance> handle, UnloadSceneOptions unloadOptions, bool autoReleaseHandle)
		{
			AsyncOperationHandle<SceneInstance> relOp = this.SceneProvider.ReleaseScene(this.ResourceManager, handle, unloadOptions);
			if (autoReleaseHandle)
			{
				relOp.ReleaseHandleOnCompletion();
			}
			return relOp;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000056A0 File Offset: 0x000038A0
		private object EvaluateKey(object obj)
		{
			if (obj is IKeyEvaluator)
			{
				return (obj as IKeyEvaluator).RuntimeKey;
			}
			return obj;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000056B8 File Offset: 0x000038B8
		internal AsyncOperationHandle<List<string>> CheckForCatalogUpdates(bool autoReleaseHandle = true)
		{
			if (this.ShouldChainRequest)
			{
				return this.CheckForCatalogUpdatesWithChain(autoReleaseHandle);
			}
			if (this.m_ActiveCheckUpdateOperation.IsValid())
			{
				this.m_ActiveCheckUpdateOperation.Release();
			}
			this.m_ActiveCheckUpdateOperation = new CheckCatalogsOperation(this).Start(this.m_ResourceLocators);
			if (autoReleaseHandle)
			{
				this.AutoReleaseHandleOnTypelessCompletion<List<string>>(this.m_ActiveCheckUpdateOperation);
			}
			return this.m_ActiveCheckUpdateOperation;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000571C File Offset: 0x0000391C
		internal AsyncOperationHandle<List<string>> CheckForCatalogUpdatesWithChain(bool autoReleaseHandle)
		{
			return this.ResourceManager.CreateChainOperation<List<string>>(this.ChainOperation, (AsyncOperationHandle op) => this.CheckForCatalogUpdates(autoReleaseHandle));
		}

		// Token: 0x06000105 RID: 261 RVA: 0x0000575C File Offset: 0x0000395C
		public ResourceLocatorInfo GetLocatorInfo(string c)
		{
			foreach (ResourceLocatorInfo i in this.m_ResourceLocators)
			{
				if (i.Locator.LocatorId == c)
				{
					return i;
				}
			}
			return null;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000106 RID: 262 RVA: 0x000057C4 File Offset: 0x000039C4
		internal IEnumerable<string> CatalogsWithAvailableUpdates
		{
			get
			{
				return from s in this.m_ResourceLocators
					where s.ContentUpdateAvailable
					select s.Locator.LocatorId;
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00005820 File Offset: 0x00003A20
		internal AsyncOperationHandle<List<IResourceLocator>> UpdateCatalogs(IEnumerable<string> catalogIds = null, bool autoReleaseHandle = true, bool autoCleanBundleCache = false)
		{
			if (this.m_ActiveUpdateOperation.IsValid())
			{
				return this.m_ActiveUpdateOperation;
			}
			if (catalogIds == null && !this.CatalogsWithAvailableUpdates.Any<string>())
			{
				return this.m_ResourceManager.CreateChainOperation<List<IResourceLocator>, List<string>>(this.CheckForCatalogUpdates(true), (AsyncOperationHandle<List<string>> depOp) => this.UpdateCatalogs(this.CatalogsWithAvailableUpdates, autoReleaseHandle, autoCleanBundleCache));
			}
			AsyncOperationHandle<List<IResourceLocator>> op = new UpdateCatalogsOperation(this).Start((catalogIds == null) ? this.CatalogsWithAvailableUpdates : catalogIds, autoCleanBundleCache);
			if (autoReleaseHandle)
			{
				this.AutoReleaseHandleOnTypelessCompletion<List<IResourceLocator>>(op);
			}
			return op;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000058B9 File Offset: 0x00003AB9
		public bool Equals(IResourceLocation x, IResourceLocation y)
		{
			return x.PrimaryKey.Equals(y.PrimaryKey) && x.ResourceType.Equals(y.ResourceType) && x.InternalId.Equals(y.InternalId);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000058F4 File Offset: 0x00003AF4
		public int GetHashCode(IResourceLocation loc)
		{
			return loc.PrimaryKey.GetHashCode() * 31 + loc.ResourceType.GetHashCode();
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00005910 File Offset: 0x00003B10
		internal AsyncOperationHandle<bool> CleanBundleCache(IEnumerable<string> catalogIds, bool forceSingleThreading)
		{
			if (this.ShouldChainRequest)
			{
				return this.CleanBundleCacheWithChain(catalogIds, forceSingleThreading);
			}
			if (catalogIds == null)
			{
				catalogIds = this.m_ResourceLocators.Select((ResourceLocatorInfo s) => s.Locator.LocatorId);
			}
			List<IResourceLocation> locations = new List<IResourceLocation>();
			foreach (string c in catalogIds)
			{
				if (c != null)
				{
					ResourceLocatorInfo loc = this.GetLocatorInfo(c);
					if (loc != null && loc.CatalogLocation != null)
					{
						locations.Add(loc.CatalogLocation);
					}
				}
			}
			if (locations.Count == 0)
			{
				return this.ResourceManager.CreateCompletedOperation<bool>(false, "Provided catalogs do not load data from a catalog file. This can occur when using the \"Use Asset Database (fastest)\" playmode script. Bundle cache was not modified.");
			}
			return this.CleanBundleCache(this.ResourceManager.CreateGroupOperation<object>(locations), forceSingleThreading);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000059E8 File Offset: 0x00003BE8
		internal AsyncOperationHandle<bool> CleanBundleCache(AsyncOperationHandle<IList<AsyncOperationHandle>> depOp, bool forceSingleThreading)
		{
			if (this.ShouldChainRequest)
			{
				return this.CleanBundleCacheWithChain(depOp, forceSingleThreading);
			}
			if (this.m_ActiveCleanBundleCacheOperation.IsValid() && !this.m_ActiveCleanBundleCacheOperation.IsDone)
			{
				return this.ResourceManager.CreateCompletedOperation<bool>(false, "Bundle cache is already being cleaned.");
			}
			this.m_ActiveCleanBundleCacheOperation = new CleanBundleCacheOperation(this, forceSingleThreading).Start(depOp);
			return this.m_ActiveCleanBundleCacheOperation;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00005A4C File Offset: 0x00003C4C
		internal AsyncOperationHandle<bool> CleanBundleCacheWithChain(AsyncOperationHandle<IList<AsyncOperationHandle>> depOp, bool forceSingleThreading)
		{
			return this.ResourceManager.CreateChainOperation<bool>(this.ChainOperation, (AsyncOperationHandle op) => this.CleanBundleCache(depOp, forceSingleThreading));
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00005A94 File Offset: 0x00003C94
		internal AsyncOperationHandle<bool> CleanBundleCacheWithChain(IEnumerable<string> catalogIds, bool forceSingleThreading)
		{
			return this.ResourceManager.CreateChainOperation<bool>(this.ChainOperation, (AsyncOperationHandle op) => this.CleanBundleCache(catalogIds, forceSingleThreading));
		}

		// Token: 0x0400003A RID: 58
		private ResourceManager m_ResourceManager;

		// Token: 0x0400003B RID: 59
		private IInstanceProvider m_InstanceProvider;

		// Token: 0x0400003C RID: 60
		private int m_CatalogRequestsTimeout;

		// Token: 0x0400003D RID: 61
		internal const string kCacheDataFolder = "{UnityEngine.Application.persistentDataPath}/com.unity.addressables/";

		// Token: 0x0400003E RID: 62
		public ISceneProvider SceneProvider;

		// Token: 0x0400003F RID: 63
		internal List<ResourceLocatorInfo> m_ResourceLocators = new List<ResourceLocatorInfo>();

		// Token: 0x04000040 RID: 64
		private AsyncOperationHandle<IResourceLocator> m_InitializationOperation;

		// Token: 0x04000041 RID: 65
		private AsyncOperationHandle<List<string>> m_ActiveCheckUpdateOperation;

		// Token: 0x04000042 RID: 66
		internal AsyncOperationHandle<List<IResourceLocator>> m_ActiveUpdateOperation;

		// Token: 0x04000043 RID: 67
		private Action<AsyncOperationHandle> m_OnHandleCompleteAction;

		// Token: 0x04000044 RID: 68
		private Action<AsyncOperationHandle> m_OnSceneHandleCompleteAction;

		// Token: 0x04000045 RID: 69
		private Action<AsyncOperationHandle> m_OnHandleDestroyedAction;

		// Token: 0x04000046 RID: 70
		private Dictionary<object, AsyncOperationHandle> m_resultToHandle = new Dictionary<object, AsyncOperationHandle>();

		// Token: 0x04000047 RID: 71
		internal HashSet<AsyncOperationHandle> m_SceneInstances = new HashSet<AsyncOperationHandle>();

		// Token: 0x04000048 RID: 72
		private AsyncOperationHandle<bool> m_ActiveCleanBundleCacheOperation;

		// Token: 0x04000049 RID: 73
		internal bool hasStartedInitialization;

		// Token: 0x0200000F RID: 15
		private class LoadResourceLocationKeyOp : AsyncOperationBase<IList<IResourceLocation>>
		{
			// Token: 0x17000028 RID: 40
			// (get) Token: 0x06000110 RID: 272 RVA: 0x00005B43 File Offset: 0x00003D43
			protected override string DebugName
			{
				get
				{
					return this.m_Keys.ToString();
				}
			}

			// Token: 0x06000111 RID: 273 RVA: 0x00005B50 File Offset: 0x00003D50
			public void Init(AddressablesImpl aa, Type t, object keys)
			{
				this.m_Keys = keys;
				this.m_ResourceType = t;
				this.m_Addressables = aa;
			}

			// Token: 0x06000112 RID: 274 RVA: 0x00005B67 File Offset: 0x00003D67
			protected override bool InvokeWaitForCompletion()
			{
				ResourceManager rm = this.m_RM;
				if (rm != null)
				{
					rm.Update(Time.unscaledDeltaTime);
				}
				if (!this.HasExecuted)
				{
					base.InvokeExecute();
				}
				return true;
			}

			// Token: 0x06000113 RID: 275 RVA: 0x00005B90 File Offset: 0x00003D90
			protected override void Execute()
			{
				this.m_Addressables.GetResourceLocations(this.m_Keys, this.m_ResourceType, out this.m_locations);
				if (this.m_locations == null)
				{
					this.m_locations = new List<IResourceLocation>();
				}
				base.Complete(this.m_locations, true, string.Empty);
			}

			// Token: 0x0400004A RID: 74
			private object m_Keys;

			// Token: 0x0400004B RID: 75
			private IList<IResourceLocation> m_locations;

			// Token: 0x0400004C RID: 76
			private AddressablesImpl m_Addressables;

			// Token: 0x0400004D RID: 77
			private Type m_ResourceType;
		}

		// Token: 0x02000010 RID: 16
		private class LoadResourceLocationKeysOp : AsyncOperationBase<IList<IResourceLocation>>
		{
			// Token: 0x17000029 RID: 41
			// (get) Token: 0x06000115 RID: 277 RVA: 0x00005BE8 File Offset: 0x00003DE8
			protected override string DebugName
			{
				get
				{
					return "LoadResourceLocationKeysOp";
				}
			}

			// Token: 0x06000116 RID: 278 RVA: 0x00005BEF File Offset: 0x00003DEF
			public void Init(AddressablesImpl aa, Type t, IEnumerable key, Addressables.MergeMode mergeMode)
			{
				this.m_Key = key;
				this.m_ResourceType = t;
				this.m_MergeMode = mergeMode;
				this.m_Addressables = aa;
			}

			// Token: 0x06000117 RID: 279 RVA: 0x00005C10 File Offset: 0x00003E10
			protected override void Execute()
			{
				this.m_Addressables.GetResourceLocations(this.m_Key, this.m_ResourceType, this.m_MergeMode, out this.m_locations);
				if (this.m_locations == null)
				{
					this.m_locations = new List<IResourceLocation>();
				}
				base.Complete(this.m_locations, true, string.Empty);
			}

			// Token: 0x06000118 RID: 280 RVA: 0x00005B67 File Offset: 0x00003D67
			protected override bool InvokeWaitForCompletion()
			{
				ResourceManager rm = this.m_RM;
				if (rm != null)
				{
					rm.Update(Time.unscaledDeltaTime);
				}
				if (!this.HasExecuted)
				{
					base.InvokeExecute();
				}
				return true;
			}

			// Token: 0x0400004E RID: 78
			private IEnumerable m_Key;

			// Token: 0x0400004F RID: 79
			private Addressables.MergeMode m_MergeMode;

			// Token: 0x04000050 RID: 80
			private IList<IResourceLocation> m_locations;

			// Token: 0x04000051 RID: 81
			private AddressablesImpl m_Addressables;

			// Token: 0x04000052 RID: 82
			private Type m_ResourceType;
		}
	}
}
