using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.Networking;
using UnityEngine.ResourceManagement;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.Profiling;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.ResourceManagement.Util;

namespace UnityEngine.AddressableAssets.ResourceProviders
{
	// Token: 0x02000045 RID: 69
	[DisplayName("Content Catalog Provider")]
	public class ContentCatalogProvider : ResourceProviderBase
	{
		// Token: 0x060001BC RID: 444 RVA: 0x000076D4 File Offset: 0x000058D4
		public ContentCatalogProvider(ResourceManager resourceManagerInstance)
		{
			this.m_BehaviourFlags = ProviderBehaviourFlags.CanProvideWithFailedDependencies;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x000076EE File Offset: 0x000058EE
		public override void Release(IResourceLocation location, object obj)
		{
			if (this.m_LocationToCatalogLoadOpMap.ContainsKey(location))
			{
				this.m_LocationToCatalogLoadOpMap[location].Release();
				this.m_LocationToCatalogLoadOpMap.Remove(location);
			}
			base.Release(location, obj);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00007724 File Offset: 0x00005924
		public override void Provide(ProvideHandle providerInterface)
		{
			if (!this.m_LocationToCatalogLoadOpMap.ContainsKey(providerInterface.Location))
			{
				this.m_LocationToCatalogLoadOpMap.Add(providerInterface.Location, new ContentCatalogProvider.InternalOp());
			}
			this.m_LocationToCatalogLoadOpMap[providerInterface.Location].Start(providerInterface, this.DisableCatalogUpdateOnStart, this.IsLocalCatalogInBundle);
		}

		// Token: 0x040000E2 RID: 226
		public bool DisableCatalogUpdateOnStart;

		// Token: 0x040000E3 RID: 227
		public bool IsLocalCatalogInBundle;

		// Token: 0x040000E4 RID: 228
		internal Dictionary<IResourceLocation, ContentCatalogProvider.InternalOp> m_LocationToCatalogLoadOpMap = new Dictionary<IResourceLocation, ContentCatalogProvider.InternalOp>();

		// Token: 0x02000046 RID: 70
		public enum DependencyHashIndex
		{
			// Token: 0x040000E6 RID: 230
			Remote,
			// Token: 0x040000E7 RID: 231
			Cache,
			// Token: 0x040000E8 RID: 232
			Local,
			// Token: 0x040000E9 RID: 233
			Count
		}

		// Token: 0x02000047 RID: 71
		internal class InternalOp
		{
			// Token: 0x060001BF RID: 447 RVA: 0x00007780 File Offset: 0x00005980
			public void Start(ProvideHandle providerInterface, bool disableCatalogUpdateOnStart, bool isLocalCatalogInBundle)
			{
				this.m_ProviderInterface = providerInterface;
				this.m_DisableCatalogUpdateOnStart = disableCatalogUpdateOnStart;
				this.m_IsLocalCatalogInBundle = isLocalCatalogInBundle;
				this.m_ProviderInterface.SetWaitForCompletionCallback(new Func<bool>(this.WaitForCompletionCallback));
				this.m_LocalDataPath = null;
				this.m_RemoteHashValue = null;
				List<object> deps = new List<object>();
				this.m_ProviderInterface.GetDependencies(deps);
				string idToLoad = this.DetermineIdToLoad(this.m_ProviderInterface.Location, deps, disableCatalogUpdateOnStart);
				bool loadCatalogFromLocalBundle = isLocalCatalogInBundle && this.CanLoadCatalogFromBundle(idToLoad, this.m_ProviderInterface.Location);
				this.LoadCatalog(idToLoad, loadCatalogFromLocalBundle);
			}

			// Token: 0x060001C0 RID: 448 RVA: 0x00007810 File Offset: 0x00005A10
			private bool WaitForCompletionCallback()
			{
				if (this.m_ContentCatalogData != null)
				{
					return true;
				}
				bool ccComplete;
				if (this.m_BundledCatalog != null)
				{
					ccComplete = this.m_BundledCatalog.WaitForCompletion();
				}
				else
				{
					ccComplete = this.m_ContentCatalogDataLoadOp.IsDone;
					if (!ccComplete)
					{
						this.m_ContentCatalogDataLoadOp.WaitForCompletion();
					}
				}
				if (ccComplete && this.m_ContentCatalogData == null)
				{
					this.m_ProviderInterface.ResourceManager.Update(Time.unscaledDeltaTime);
				}
				return ccComplete;
			}

			// Token: 0x060001C1 RID: 449 RVA: 0x00007879 File Offset: 0x00005A79
			public void Release()
			{
				ContentCatalogData contentCatalogData = this.m_ContentCatalogData;
				if (contentCatalogData == null)
				{
					return;
				}
				contentCatalogData.CleanData();
			}

			// Token: 0x060001C2 RID: 450 RVA: 0x0000788B File Offset: 0x00005A8B
			internal bool CanLoadCatalogFromBundle(string idToLoad, IResourceLocation location)
			{
				return Path.GetExtension(idToLoad) == ".bundle" && idToLoad.Equals(this.GetTransformedInternalId(location));
			}

			// Token: 0x060001C3 RID: 451 RVA: 0x000078B0 File Offset: 0x00005AB0
			internal void LoadCatalog(string idToLoad, bool loadCatalogFromLocalBundle)
			{
				try
				{
					ProviderLoadRequestOptions providerLoadRequestOptions = null;
					ProviderLoadRequestOptions providerData = this.m_ProviderInterface.Location.Data as ProviderLoadRequestOptions;
					if (providerData != null)
					{
						providerLoadRequestOptions = providerData.Copy();
					}
					if (loadCatalogFromLocalBundle)
					{
						int webRequestTimeout = ((providerLoadRequestOptions != null) ? providerLoadRequestOptions.WebRequestTimeout : 0);
						this.m_BundledCatalog = new ContentCatalogProvider.InternalOp.BundledCatalog(idToLoad, webRequestTimeout);
						this.m_BundledCatalog.OnLoaded += delegate(ContentCatalogData ccd)
						{
							this.m_ContentCatalogData = ccd;
							this.OnCatalogLoaded(ccd);
						};
						this.m_BundledCatalog.LoadCatalogFromBundleAsync();
					}
					else if (Path.GetExtension(idToLoad) == ".json")
					{
						this.m_ProviderInterface.Complete<ContentCatalogData>(null, false, new Exception("Expecting to load catalogs in binary format but the catalog provided is in .json format. To load it enable Addressable Asset Settings > Catalog > Enable Json Catalog."));
					}
					else
					{
						ResourceLocationBase location = new ResourceLocationBase(idToLoad, idToLoad, typeof(BinaryAssetProvider<ContentCatalogData.Serializer>).FullName, typeof(ContentCatalogData), Array.Empty<IResourceLocation>());
						location.Data = providerLoadRequestOptions;
						this.m_ProviderInterface.ResourceManager.ResourceProviders.Add(new BinaryAssetProvider<ContentCatalogData.Serializer>());
						this.m_ContentCatalogDataLoadOp = this.m_ProviderInterface.ResourceManager.ProvideResource<ContentCatalogData>(location);
						this.m_ContentCatalogDataLoadOp.Completed += this.CatalogLoadOpCompleteCallback;
					}
				}
				catch (Exception ex)
				{
					this.m_ProviderInterface.Complete<ContentCatalogData>(null, false, ex);
				}
			}

			// Token: 0x060001C4 RID: 452 RVA: 0x000079F8 File Offset: 0x00005BF8
			private void CatalogLoadOpCompleteCallback(AsyncOperationHandle<ContentCatalogData> op)
			{
				this.m_ContentCatalogData = op.Result;
				op.Release();
				this.OnCatalogLoaded(this.m_ContentCatalogData);
			}

			// Token: 0x060001C5 RID: 453 RVA: 0x00007A1A File Offset: 0x00005C1A
			private string GetTransformedInternalId(IResourceLocation loc)
			{
				if (this.m_ProviderInterface.ResourceManager == null)
				{
					return loc.InternalId;
				}
				return this.m_ProviderInterface.ResourceManager.TransformInternalId(loc);
			}

			// Token: 0x060001C6 RID: 454 RVA: 0x00007A44 File Offset: 0x00005C44
			internal string DetermineIdToLoad(IResourceLocation location, IList<object> dependencyObjects, bool disableCatalogUpdateOnStart = false)
			{
				string idToLoad = this.GetTransformedInternalId(location);
				if (dependencyObjects != null && location.Dependencies != null && dependencyObjects.Count == 3 && location.Dependencies.Count == 3)
				{
					string remoteHash = dependencyObjects[0] as string;
					this.m_LocalHashValue = dependencyObjects[1] as string;
					if (string.IsNullOrEmpty(this.m_LocalHashValue))
					{
						this.m_LocalHashValue = dependencyObjects[2] as string;
					}
					if (string.IsNullOrEmpty(remoteHash) || disableCatalogUpdateOnStart)
					{
						if (!string.IsNullOrEmpty(this.m_LocalHashValue) && !this.m_Retried && !string.IsNullOrEmpty(Application.persistentDataPath))
						{
							if (string.IsNullOrEmpty(dependencyObjects[1] as string))
							{
								idToLoad = this.GetTransformedInternalId(location.Dependencies[2]).Replace(".hash", ".bin");
							}
							else
							{
								idToLoad = this.GetTransformedInternalId(location.Dependencies[1]).Replace(".hash", ".bin");
							}
						}
					}
					else if (remoteHash == this.m_LocalHashValue && !this.m_Retried)
					{
						if (string.IsNullOrEmpty(dependencyObjects[1] as string))
						{
							idToLoad = this.GetTransformedInternalId(location.Dependencies[2]).Replace(".hash", ".bin");
						}
						else
						{
							idToLoad = this.GetTransformedInternalId(location.Dependencies[1]).Replace(".hash", ".bin");
						}
					}
					else
					{
						idToLoad = this.GetTransformedInternalId(location.Dependencies[0]).Replace(".hash", ".bin");
						this.m_RemoteHashValue = remoteHash;
						if (!string.IsNullOrEmpty(Application.persistentDataPath))
						{
							this.m_LocalDataPath = this.GetTransformedInternalId(location.Dependencies[1]).Replace(".hash", ".bin");
						}
					}
				}
				return idToLoad;
			}

			// Token: 0x060001C7 RID: 455 RVA: 0x00007C30 File Offset: 0x00005E30
			private void OnCatalogLoaded(ContentCatalogData ccd)
			{
				if (ccd != null)
				{
					ProfilerRuntime.AddCatalog(Hash128.Parse(ccd.BuildResultHash));
					ccd.location = this.m_ProviderInterface.Location;
					ccd.LocalHash = this.m_LocalHashValue;
					if (!string.IsNullOrEmpty(this.m_RemoteHashValue) && !string.IsNullOrEmpty(this.m_LocalDataPath))
					{
						string dir = Path.GetDirectoryName(this.m_LocalDataPath);
						string localCachePath = this.m_LocalDataPath;
						try
						{
							if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
							{
								Directory.CreateDirectory(dir);
							}
							File.WriteAllBytes(localCachePath, ccd.GetBytes());
							File.WriteAllText(localCachePath.Replace(".bin", ".hash"), this.m_RemoteHashValue);
						}
						catch (UnauthorizedAccessException uae)
						{
							Addressables.LogWarning("Did not save cached content catalog. Missing access permissions for location " + localCachePath + " : " + uae.Message);
							this.m_ProviderInterface.Complete<ContentCatalogData>(ccd, true, null);
							return;
						}
						catch (Exception e)
						{
							string remoteInternalId = this.GetTransformedInternalId(this.m_ProviderInterface.Location.Dependencies[0]);
							string errorMessage = string.Concat(new string[] { "Unable to load ContentCatalogData from location ", remoteInternalId, ". Failed to cache catalog to location ", localCachePath, "." });
							ccd = null;
							this.m_ProviderInterface.Complete<ContentCatalogData>(ccd, false, new Exception(errorMessage, e));
							return;
						}
						ccd.LocalHash = this.m_RemoteHashValue;
					}
					else if (string.IsNullOrEmpty(this.m_LocalDataPath) && string.IsNullOrEmpty(Application.persistentDataPath))
					{
						Addressables.LogWarning("Did not save cached content catalog because Application.persistentDataPath is an empty path.");
					}
					this.m_ProviderInterface.Complete<ContentCatalogData>(ccd, true, null);
					return;
				}
				string errorMessage2 = string.Format("Unable to load ContentCatalogData from location {0}", this.m_ProviderInterface.Location);
				if (!this.m_Retried)
				{
					this.m_Retried = true;
					string cachePath = this.GetTransformedInternalId(this.m_ProviderInterface.Location.Dependencies[1]);
					if (this.m_ContentCatalogDataLoadOp.LocationName == cachePath.Replace(".hash", ".bin"))
					{
						try
						{
							File.Delete(cachePath);
						}
						catch (Exception)
						{
							errorMessage2 = errorMessage2 + ". Unable to delete cache data from location " + cachePath;
							this.m_ProviderInterface.Complete<ContentCatalogData>(ccd, false, new Exception(errorMessage2));
							return;
						}
					}
					Addressables.LogWarning(errorMessage2 + ". Attempting to retry...");
					this.Start(this.m_ProviderInterface, this.m_DisableCatalogUpdateOnStart, this.m_IsLocalCatalogInBundle);
					return;
				}
				this.m_ProviderInterface.Complete<ContentCatalogData>(ccd, false, new Exception(errorMessage2 + " on second attempt."));
			}

			// Token: 0x040000EA RID: 234
			private string m_LocalDataPath;

			// Token: 0x040000EB RID: 235
			private string m_RemoteHashValue;

			// Token: 0x040000EC RID: 236
			internal string m_LocalHashValue;

			// Token: 0x040000ED RID: 237
			private ProvideHandle m_ProviderInterface;

			// Token: 0x040000EE RID: 238
			internal ContentCatalogData m_ContentCatalogData;

			// Token: 0x040000EF RID: 239
			private AsyncOperationHandle<ContentCatalogData> m_ContentCatalogDataLoadOp;

			// Token: 0x040000F0 RID: 240
			private ContentCatalogProvider.InternalOp.BundledCatalog m_BundledCatalog;

			// Token: 0x040000F1 RID: 241
			private bool m_Retried;

			// Token: 0x040000F2 RID: 242
			private bool m_DisableCatalogUpdateOnStart;

			// Token: 0x040000F3 RID: 243
			private bool m_IsLocalCatalogInBundle;

			// Token: 0x040000F4 RID: 244
			private const string kCatalogExt = ".bin";

			// Token: 0x02000048 RID: 72
			internal class BundledCatalog
			{
				// Token: 0x14000001 RID: 1
				// (add) Token: 0x060001CA RID: 458 RVA: 0x00007EDC File Offset: 0x000060DC
				// (remove) Token: 0x060001CB RID: 459 RVA: 0x00007F14 File Offset: 0x00006114
				public event Action<ContentCatalogData> OnLoaded;

				// Token: 0x17000038 RID: 56
				// (get) Token: 0x060001CC RID: 460 RVA: 0x00007F49 File Offset: 0x00006149
				public bool OpInProgress
				{
					get
					{
						return this.m_OpInProgress;
					}
				}

				// Token: 0x17000039 RID: 57
				// (get) Token: 0x060001CD RID: 461 RVA: 0x00007F51 File Offset: 0x00006151
				public bool OpIsSuccess
				{
					get
					{
						return !this.m_OpInProgress && this.m_CatalogData != null;
					}
				}

				// Token: 0x060001CE RID: 462 RVA: 0x00007F68 File Offset: 0x00006168
				public BundledCatalog(string bundlePath, int webRequestTimeout = 0)
				{
					if (string.IsNullOrEmpty(bundlePath))
					{
						throw new ArgumentNullException("bundlePath", "Catalog bundle path is null.");
					}
					if (!bundlePath.EndsWith(".bundle", StringComparison.OrdinalIgnoreCase))
					{
						throw new ArgumentException("You must supply a valid bundle file path.");
					}
					this.m_BundlePath = bundlePath;
					this.m_WebRequestTimeout = webRequestTimeout;
				}

				// Token: 0x060001CF RID: 463 RVA: 0x00007FBC File Offset: 0x000061BC
				~BundledCatalog()
				{
					this.Unload();
				}

				// Token: 0x060001D0 RID: 464 RVA: 0x00007FE8 File Offset: 0x000061E8
				private void Unload()
				{
					AssetBundle catalogAssetBundle = this.m_CatalogAssetBundle;
					if (catalogAssetBundle != null)
					{
						catalogAssetBundle.Unload(true);
					}
					this.m_CatalogAssetBundle = null;
				}

				// Token: 0x060001D1 RID: 465 RVA: 0x00008004 File Offset: 0x00006204
				public void LoadCatalogFromBundleAsync()
				{
					if (this.m_OpInProgress)
					{
						Addressables.LogError("Operation in progress : A catalog is already being loaded. Please wait for the operation to complete.");
						return;
					}
					this.m_OpInProgress = true;
					if (!ResourceManagerConfig.ShouldPathUseWebRequest(this.m_BundlePath))
					{
						this.m_LoadBundleRequest = AssetBundle.LoadFromFileAsync(this.m_BundlePath);
						this.m_LoadBundleRequest.completed += delegate(AsyncOperation loadOp)
						{
							AssetBundleCreateRequest createRequest = loadOp as AssetBundleCreateRequest;
							if (createRequest != null && createRequest.assetBundle != null)
							{
								this.m_CatalogAssetBundle = createRequest.assetBundle;
								this.m_LoadTextAssetRequest = this.m_CatalogAssetBundle.LoadAllAssetsAsync<TextAsset>();
								if (this.m_LoadTextAssetRequest.isDone)
								{
									this.LoadTextAssetRequestComplete(this.m_LoadTextAssetRequest);
								}
								this.m_LoadTextAssetRequest.completed += this.LoadTextAssetRequestComplete;
								return;
							}
							Addressables.LogError("Unable to load dependent bundle from file location : " + this.m_BundlePath);
							this.m_OpInProgress = false;
						};
						return;
					}
					UnityWebRequest req = UnityWebRequestAssetBundle.GetAssetBundle(this.m_BundlePath);
					if (this.m_WebRequestTimeout > 0)
					{
						req.timeout = this.m_WebRequestTimeout;
					}
					this.m_WebRequestQueueOperation = WebRequestQueue.QueueRequest(req);
					if (!this.m_WebRequestQueueOperation.IsDone)
					{
						WebRequestQueueOperation webRequestQueueOperation = this.m_WebRequestQueueOperation;
						webRequestQueueOperation.OnComplete = (Action<UnityWebRequestAsyncOperation>)Delegate.Combine(webRequestQueueOperation.OnComplete, new Action<UnityWebRequestAsyncOperation>(delegate(UnityWebRequestAsyncOperation asyncOp)
						{
							this.m_RequestOperation = asyncOp;
							this.m_RequestOperation.completed += this.WebRequestOperationCompleted;
						}));
						return;
					}
					this.m_RequestOperation = this.m_WebRequestQueueOperation.Result;
					if (this.m_RequestOperation.isDone)
					{
						this.WebRequestOperationCompleted(this.m_RequestOperation);
						return;
					}
					this.m_RequestOperation.completed += this.WebRequestOperationCompleted;
				}

				// Token: 0x060001D2 RID: 466 RVA: 0x00008108 File Offset: 0x00006308
				private void WebRequestOperationCompleted(AsyncOperation op)
				{
					UnityWebRequestUtilities.LogOperationResult(op);
					UnityWebRequest webRequest = (op as UnityWebRequestAsyncOperation).webRequest;
					DownloadHandlerAssetBundle downloadHandler = webRequest.downloadHandler as DownloadHandlerAssetBundle;
					UnityWebRequestResult uwrResult;
					if (!UnityWebRequestUtilities.RequestHasErrors(webRequest, out uwrResult))
					{
						this.m_CatalogAssetBundle = downloadHandler.assetBundle;
						this.m_LoadTextAssetRequest = this.m_CatalogAssetBundle.LoadAllAssetsAsync<TextAsset>();
						if (this.m_LoadTextAssetRequest.isDone)
						{
							this.LoadTextAssetRequestComplete(this.m_LoadTextAssetRequest);
						}
						this.m_LoadTextAssetRequest.completed += this.LoadTextAssetRequestComplete;
					}
					else
					{
						Addressables.LogError("Unable to load dependent bundle from remote location : " + this.m_BundlePath);
						this.m_OpInProgress = false;
					}
					webRequest.Dispose();
				}

				// Token: 0x060001D3 RID: 467 RVA: 0x000081AC File Offset: 0x000063AC
				private void LoadTextAssetRequestComplete(AsyncOperation op)
				{
					AssetBundleRequest loadRequest = op as AssetBundleRequest;
					if (loadRequest != null)
					{
						TextAsset textAsset = loadRequest.asset as TextAsset;
						if (textAsset != null && textAsset.text != null)
						{
							this.m_CatalogData = JsonUtility.FromJson<ContentCatalogData>(textAsset.text);
							Action<ContentCatalogData> onLoaded = this.OnLoaded;
							if (onLoaded == null)
							{
								goto IL_0060;
							}
							onLoaded(this.m_CatalogData);
							goto IL_0060;
						}
					}
					Addressables.LogError("No catalog text assets where found in bundle " + this.m_BundlePath);
					IL_0060:
					this.Unload();
					this.m_OpInProgress = false;
				}

				// Token: 0x060001D4 RID: 468 RVA: 0x00008226 File Offset: 0x00006426
				public bool WaitForCompletion()
				{
					return !(this.m_LoadBundleRequest.assetBundle == null) && (this.m_LoadTextAssetRequest.asset != null || this.m_LoadTextAssetRequest.allAssets != null);
				}

				// Token: 0x040000F5 RID: 245
				private readonly string m_BundlePath;

				// Token: 0x040000F6 RID: 246
				private bool m_OpInProgress;

				// Token: 0x040000F7 RID: 247
				private AssetBundleCreateRequest m_LoadBundleRequest;

				// Token: 0x040000F8 RID: 248
				internal AssetBundle m_CatalogAssetBundle;

				// Token: 0x040000F9 RID: 249
				private AssetBundleRequest m_LoadTextAssetRequest;

				// Token: 0x040000FA RID: 250
				private ContentCatalogData m_CatalogData;

				// Token: 0x040000FB RID: 251
				private WebRequestQueueOperation m_WebRequestQueueOperation;

				// Token: 0x040000FC RID: 252
				private AsyncOperation m_RequestOperation;

				// Token: 0x040000FD RID: 253
				private int m_WebRequestTimeout;
			}
		}
	}
}
