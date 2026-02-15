using System;
using System.IO;
using System.Threading;
using UnityEngine.Networking;
using UnityEngine.Profiling;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.Exceptions;
using UnityEngine.ResourceManagement.Profiling;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.Util;

namespace UnityEngine.ResourceManagement.ResourceProviders
{
	// Token: 0x02000047 RID: 71
	public class AssetBundleResource : IAssetBundleResource, IUpdateReceiver
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00007183 File Offset: 0x00005383
		private bool HasTimedOut
		{
			get
			{
				return this.m_TimeoutTimer >= (float)this.m_Options.Timeout && this.m_TimeoutOverFrames > 5;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000193 RID: 403 RVA: 0x000071A4 File Offset: 0x000053A4
		internal long BytesToDownload
		{
			get
			{
				if (this.m_BytesToDownload == -1L)
				{
					if (this.m_Options != null)
					{
						this.m_BytesToDownload = this.m_Options.ComputeSize(this.m_ProvideHandle.Location, this.m_ProvideHandle.ResourceManager);
					}
					else
					{
						this.m_BytesToDownload = 0L;
					}
				}
				return this.m_BytesToDownload;
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000071FC File Offset: 0x000053FC
		internal UnityWebRequest CreateWebRequest(IResourceLocation loc)
		{
			string url = this.m_ProvideHandle.ResourceManager.TransformInternalId(loc);
			return this.CreateWebRequest(url);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00007224 File Offset: 0x00005424
		internal UnityWebRequest CreateWebRequest(string url)
		{
			Uri uri = new Uri(Uri.UnescapeDataString(url).Replace(" ", "%20"));
			if (this.m_Options == null)
			{
				this.m_Source = BundleSource.Download;
				this.AddBundleToProfiler(ContentStatus.Downloading, this.m_Source);
				return UnityWebRequestAssetBundle.GetAssetBundle(uri);
			}
			UnityWebRequest webRequest;
			if (!string.IsNullOrEmpty(this.m_Options.Hash))
			{
				CachedAssetBundle cachedBundle = new CachedAssetBundle(this.m_Options.BundleName, Hash128.Parse(this.m_Options.Hash));
				this.m_Source = (Caching.IsVersionCached(cachedBundle) ? BundleSource.Cache : BundleSource.Download);
				if (this.m_Options.UseCrcForCachedBundle || this.m_Source == BundleSource.Download)
				{
					webRequest = UnityWebRequestAssetBundle.GetAssetBundle(uri, cachedBundle, this.m_Options.Crc);
				}
				else
				{
					webRequest = UnityWebRequestAssetBundle.GetAssetBundle(uri, cachedBundle, 0U);
				}
			}
			else
			{
				this.m_Source = BundleSource.Download;
				webRequest = UnityWebRequestAssetBundle.GetAssetBundle(uri, this.m_Options.Crc);
			}
			if (this.m_Options.RedirectLimit >= 0 && this.m_Options.RedirectLimit < 129)
			{
				webRequest.redirectLimit = this.m_Options.RedirectLimit;
			}
			if (this.m_ProvideHandle.ResourceManager.CertificateHandlerInstance != null)
			{
				webRequest.certificateHandler = this.m_ProvideHandle.ResourceManager.CertificateHandlerInstance;
				webRequest.disposeCertificateHandlerOnDispose = false;
			}
			Action<UnityWebRequest> webRequestOverride = this.m_ProvideHandle.ResourceManager.WebRequestOverride;
			if (webRequestOverride != null)
			{
				webRequestOverride(webRequest);
			}
			return webRequest;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00007384 File Offset: 0x00005584
		public AssetBundleRequest GetAssetPreloadRequest()
		{
			if (this.m_PreloadCompleted || this.GetAssetBundle() == null)
			{
				return null;
			}
			if (this.m_Options.AssetLoadMode == AssetLoadMode.AllPackedAssetsAndDependencies)
			{
				if (this.m_PreloadRequest == null)
				{
					this.m_PreloadRequest = this.m_AssetBundle.LoadAllAssetsAsync();
					this.m_PreloadRequest.completed += delegate(AsyncOperation operation)
					{
						this.m_PreloadCompleted = true;
					};
				}
				return this.m_PreloadRequest;
			}
			return null;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x000073EF File Offset: 0x000055EF
		private float PercentComplete()
		{
			if (this.m_RequestOperation == null)
			{
				return 0f;
			}
			return this.m_RequestOperation.progress;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000740C File Offset: 0x0000560C
		private DownloadStatus GetDownloadStatus()
		{
			if (this.m_Options == null)
			{
				return default(DownloadStatus);
			}
			DownloadStatus status = new DownloadStatus
			{
				TotalBytes = this.BytesToDownload,
				IsDone = (this.PercentComplete() >= 1f)
			};
			if (this.BytesToDownload > 0L)
			{
				if (this.m_WebRequestQueueOperation != null && string.IsNullOrEmpty(this.m_WebRequestQueueOperation.m_WebRequest.error))
				{
					this.m_DownloadedBytes = (long)this.m_WebRequestQueueOperation.m_WebRequest.downloadedBytes;
				}
				else if (this.m_RequestOperation != null)
				{
					UnityWebRequestAsyncOperation operation = this.m_RequestOperation as UnityWebRequestAsyncOperation;
					if (operation != null && string.IsNullOrEmpty(operation.webRequest.error))
					{
						this.m_DownloadedBytes = (long)operation.webRequest.downloadedBytes;
					}
				}
			}
			status.DownloadedBytes = this.m_DownloadedBytes;
			return status;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x000074E2 File Offset: 0x000056E2
		public AssetBundle GetAssetBundle()
		{
			bool isValid = this.m_ProvideHandle.IsValid;
			return this.m_AssetBundle;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x000074F8 File Offset: 0x000056F8
		private void AddBundleToProfiler(ContentStatus status, BundleSource source)
		{
			if (!Profiler.enabled)
			{
				return;
			}
			if (!this.m_ProvideHandle.IsValid)
			{
				return;
			}
			if (status == ContentStatus.Active && this.m_AssetBundle == null)
			{
				ProfilerRuntime.BundleReleased(this.m_Options.BundleName);
				return;
			}
			ProfilerRuntime.AddBundleOperation(this.m_ProvideHandle, this.m_Options, status, source);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00007555 File Offset: 0x00005755
		private void RemoveBundleFromProfiler()
		{
			if (this.m_Options == null)
			{
				return;
			}
			ProfilerRuntime.BundleReleased(this.m_Options.BundleName);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00007570 File Offset: 0x00005770
		private void OnUnloadOperationComplete(AsyncOperation op)
		{
			this.m_UnloadOperation = null;
			this.BeginOperation();
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00007580 File Offset: 0x00005780
		public void Start(ProvideHandle provideHandle, AssetBundleUnloadOperation unloadOp, Func<UnityWebRequestResult, bool> requestRetryCallback)
		{
			this.m_Retries = 0;
			this.m_AssetBundle = null;
			this.m_RequestOperation = null;
			this.m_RequestCompletedCallbackCalled = false;
			this.m_ProvideHandle = provideHandle;
			this.m_Options = this.m_ProvideHandle.Location.Data as AssetBundleRequestOptions;
			this.m_BytesToDownload = -1L;
			this.m_DownloadOnly = this.m_ProvideHandle.Location is DownloadOnlyLocation;
			this.m_ProvideHandle.SetProgressCallback(new Func<float>(this.PercentComplete));
			this.m_ProvideHandle.SetDownloadProgressCallbacks(new Func<DownloadStatus>(this.GetDownloadStatus));
			this.m_ProvideHandle.SetWaitForCompletionCallback(new Func<bool>(this.WaitForCompletionHandler));
			this.m_RequestRetryCallback = requestRetryCallback;
			this.m_UnloadOperation = unloadOp;
			if (this.m_UnloadOperation != null && !this.m_UnloadOperation.isDone)
			{
				this.m_UnloadOperation.completed += this.OnUnloadOperationComplete;
				return;
			}
			this.BeginOperation();
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00007674 File Offset: 0x00005874
		private bool WaitForCompletionHandler()
		{
			if (this.m_UnloadOperation != null && !this.m_UnloadOperation.isDone)
			{
				this.m_UnloadOperation.completed -= this.OnUnloadOperationComplete;
				this.m_UnloadOperation.WaitForCompletion();
				this.m_UnloadOperation = null;
				this.BeginOperation();
			}
			if (this.m_RequestOperation == null)
			{
				if (this.m_WebRequestQueueOperation == null)
				{
					return false;
				}
				WebRequestQueue.WaitForRequestToBeActive(this.m_WebRequestQueueOperation, 1);
			}
			UnityWebRequestAsyncOperation op = this.m_RequestOperation as UnityWebRequestAsyncOperation;
			if (op != null)
			{
				while (!UnityWebRequestUtilities.IsAssetBundleDownloaded(op))
				{
					Thread.Sleep(1);
				}
				if (this.m_Source == BundleSource.Cache)
				{
					object obj;
					if (op == null)
					{
						obj = null;
					}
					else
					{
						UnityWebRequest webRequest = op.webRequest;
						obj = ((webRequest != null) ? webRequest.downloadHandler : null);
					}
					DownloadHandlerAssetBundle downloadHandler = (DownloadHandlerAssetBundle)obj;
					if (downloadHandler.autoLoadAssetBundle)
					{
						this.m_AssetBundle = downloadHandler.assetBundle;
					}
				}
				WebRequestQueue.DequeueRequest(op);
				if (!this.m_RequestCompletedCallbackCalled)
				{
					this.m_RequestOperation.completed -= this.WebRequestOperationCompleted;
					this.WebRequestOperationCompleted(this.m_RequestOperation);
				}
			}
			if (!this.m_Completed && this.m_Source == BundleSource.Local && !this.m_RequestCompletedCallbackCalled)
			{
				this.m_RequestOperation.completed -= this.LocalRequestOperationCompleted;
				this.LocalRequestOperationCompleted(this.m_RequestOperation);
			}
			if (!this.m_Completed && this.m_RequestOperation.isDone)
			{
				this.m_ProvideHandle.Complete<AssetBundleResource>(this, this.m_AssetBundle != null, null);
				this.m_Completed = true;
			}
			return this.m_Completed;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x000077E5 File Offset: 0x000059E5
		private void AddCallbackInvokeIfDone(AsyncOperation operation, Action<AsyncOperation> callback)
		{
			if (operation.isDone)
			{
				callback(operation);
				return;
			}
			operation.completed += callback;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x000077FE File Offset: 0x000059FE
		public static void GetLoadInfo(ProvideHandle handle, out AssetBundleResource.LoadType loadType, out string path)
		{
			AssetBundleResource.GetLoadInfo(handle.Location, handle.ResourceManager, out loadType, out path);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00007818 File Offset: 0x00005A18
		internal static void GetLoadInfo(IResourceLocation location, ResourceManager resourceManager, out AssetBundleResource.LoadType loadType, out string path)
		{
			AssetBundleRequestOptions options = ((location != null) ? location.Data : null) as AssetBundleRequestOptions;
			if (options == null)
			{
				loadType = AssetBundleResource.LoadType.None;
				path = null;
				return;
			}
			path = resourceManager.TransformInternalId(location);
			if (Application.platform == RuntimePlatform.Android && path.StartsWith("jar:", StringComparison.Ordinal))
			{
				loadType = (options.UseUnityWebRequestForLocalBundles ? AssetBundleResource.LoadType.Web : AssetBundleResource.LoadType.Local);
			}
			else if (ResourceManagerConfig.ShouldPathUseWebRequest(path))
			{
				loadType = AssetBundleResource.LoadType.Web;
			}
			else if (options.UseUnityWebRequestForLocalBundles)
			{
				path = "file:///" + Path.GetFullPath(path);
				loadType = AssetBundleResource.LoadType.Web;
			}
			else
			{
				loadType = AssetBundleResource.LoadType.Local;
			}
			if (loadType == AssetBundleResource.LoadType.Web)
			{
				path = path.Replace('\\', '/');
			}
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x000078B8 File Offset: 0x00005AB8
		private void BeginOperation()
		{
			this.m_DownloadedBytes = 0L;
			this.m_RequestCompletedCallbackCalled = false;
			AssetBundleResource.LoadType loadType;
			AssetBundleResource.GetLoadInfo(this.m_ProvideHandle, out loadType, out this.m_TransformedInternalId);
			if (loadType == AssetBundleResource.LoadType.Local)
			{
				if (this.m_ProvideHandle.Location is DownloadOnlyLocation)
				{
					this.m_Source = BundleSource.Local;
					this.m_RequestOperation = null;
					this.m_ProvideHandle.Complete<AssetBundleResource>(null, true, null);
					this.m_Completed = true;
					return;
				}
				this.LoadLocalBundle();
				return;
			}
			else
			{
				if (loadType == AssetBundleResource.LoadType.Web)
				{
					this.m_WebRequestQueueOperation = this.EnqueueWebRequest(this.m_TransformedInternalId);
					this.AddBeginWebRequestHandler(this.m_WebRequestQueueOperation);
					return;
				}
				this.m_Source = BundleSource.None;
				this.m_RequestOperation = null;
				this.m_ProvideHandle.Complete<AssetBundleResource>(null, false, new RemoteProviderException(string.Format("Invalid path in AssetBundleProvider: '{0}'.", this.m_TransformedInternalId), this.m_ProvideHandle.Location, null, null));
				this.m_Completed = true;
				return;
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00007990 File Offset: 0x00005B90
		private void LoadLocalBundle()
		{
			this.m_Source = BundleSource.Local;
			this.m_RequestOperation = AssetBundle.LoadFromFileAsync(this.m_TransformedInternalId, (this.m_Options == null) ? 0U : this.m_Options.Crc);
			this.AddBundleToProfiler(ContentStatus.Loading, this.m_Source);
			this.AddCallbackInvokeIfDone(this.m_RequestOperation, new Action<AsyncOperation>(this.LocalRequestOperationCompleted));
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000079F1 File Offset: 0x00005BF1
		internal WebRequestQueueOperation EnqueueWebRequest(string internalId)
		{
			UnityWebRequest unityWebRequest = this.CreateWebRequest(internalId);
			((DownloadHandlerAssetBundle)unityWebRequest.downloadHandler).autoLoadAssetBundle = !(this.m_ProvideHandle.Location is DownloadOnlyLocation);
			unityWebRequest.disposeDownloadHandlerOnDispose = false;
			return WebRequestQueue.QueueRequest(unityWebRequest);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00007A2C File Offset: 0x00005C2C
		internal void AddBeginWebRequestHandler(WebRequestQueueOperation webRequestQueueOperation)
		{
			if (webRequestQueueOperation.IsDone)
			{
				this.BeginWebRequestOperation(webRequestQueueOperation.Result);
				return;
			}
			this.AddBundleToProfiler(ContentStatus.Queue, this.m_Source);
			webRequestQueueOperation.OnComplete = (Action<UnityWebRequestAsyncOperation>)Delegate.Combine(webRequestQueueOperation.OnComplete, new Action<UnityWebRequestAsyncOperation>(delegate(UnityWebRequestAsyncOperation asyncOp)
			{
				this.BeginWebRequestOperation(asyncOp);
			}));
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00007A80 File Offset: 0x00005C80
		private void BeginWebRequestOperation(AsyncOperation asyncOp)
		{
			this.m_TimeoutTimer = 0f;
			this.m_TimeoutOverFrames = 0;
			this.m_LastDownloadedByteCount = 0UL;
			this.m_RequestOperation = asyncOp;
			if (this.m_RequestOperation == null || this.m_RequestOperation.isDone)
			{
				this.WebRequestOperationCompleted(this.m_RequestOperation);
				return;
			}
			if (this.m_Options.Timeout > 0)
			{
				this.m_ProvideHandle.ResourceManager.AddUpdateReceiver(this);
			}
			this.AddBundleToProfiler((this.m_Source == BundleSource.Cache) ? ContentStatus.Loading : ContentStatus.Downloading, this.m_Source);
			this.m_RequestOperation.completed += this.WebRequestOperationCompleted;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00007B20 File Offset: 0x00005D20
		public void Update(float unscaledDeltaTime)
		{
			if (this.m_RequestOperation != null)
			{
				UnityWebRequestAsyncOperation operation = this.m_RequestOperation as UnityWebRequestAsyncOperation;
				if (operation != null && !operation.isDone)
				{
					if (this.m_LastDownloadedByteCount != operation.webRequest.downloadedBytes)
					{
						this.m_TimeoutTimer = 0f;
						this.m_TimeoutOverFrames = 0;
						this.m_LastDownloadedByteCount = operation.webRequest.downloadedBytes;
						this.m_LastFrameCount = -1;
						this.m_TimeSecSinceLastUpdate = 0f;
						return;
					}
					float updateTime = unscaledDeltaTime;
					if (this.m_LastFrameCount == Time.frameCount)
					{
						updateTime = Time.realtimeSinceStartup - this.m_TimeSecSinceLastUpdate;
					}
					this.m_TimeoutTimer += updateTime;
					if (this.HasTimedOut)
					{
						operation.webRequest.Abort();
					}
					this.m_TimeoutOverFrames++;
					this.m_LastFrameCount = Time.frameCount;
					this.m_TimeSecSinceLastUpdate = Time.realtimeSinceStartup;
				}
			}
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00007BFF File Offset: 0x00005DFF
		private void LocalRequestOperationCompleted(AsyncOperation op)
		{
			if (this.m_RequestCompletedCallbackCalled)
			{
				return;
			}
			this.m_RequestCompletedCallbackCalled = true;
			UnityWebRequestUtilities.LogOperationResult(op);
			this.CompleteBundleLoad((op as AssetBundleCreateRequest).assetBundle);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00007C28 File Offset: 0x00005E28
		private void CompleteBundleLoad(AssetBundle bundle)
		{
			this.m_AssetBundle = bundle;
			this.AddBundleToProfiler(ContentStatus.Active, this.m_Source);
			if (this.m_AssetBundle != null)
			{
				this.m_ProvideHandle.Complete<AssetBundleResource>(this, true, null);
			}
			else
			{
				this.m_ProvideHandle.Complete<AssetBundleResource>(null, false, new RemoteProviderException(string.Format("Invalid path in AssetBundleProvider: '{0}'.", this.m_TransformedInternalId), this.m_ProvideHandle.Location, null, null));
			}
			this.m_Completed = true;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00007CA4 File Offset: 0x00005EA4
		private void WebRequestOperationCompleted(AsyncOperation op)
		{
			if (this.m_RequestCompletedCallbackCalled)
			{
				return;
			}
			this.m_RequestCompletedCallbackCalled = true;
			if (this.m_Options.Timeout > 0)
			{
				this.m_ProvideHandle.ResourceManager.RemoveUpdateReciever(this);
			}
			UnityWebRequestAsyncOperation unityWebRequestAsyncOperation = op as UnityWebRequestAsyncOperation;
			UnityWebRequest webReq = ((unityWebRequestAsyncOperation != null) ? unityWebRequestAsyncOperation.webRequest : null);
			DownloadHandlerAssetBundle downloadHandler = ((webReq != null) ? webReq.downloadHandler : null) as DownloadHandlerAssetBundle;
			UnityWebRequestResult uwrResult = null;
			if (webReq != null && !UnityWebRequestUtilities.RequestHasErrors(webReq, out uwrResult))
			{
				if (!this.m_Completed)
				{
					if (!(this.m_ProvideHandle.Location is DownloadOnlyLocation))
					{
						this.m_AssetBundle = downloadHandler.assetBundle;
					}
					this.AddBundleToProfiler(ContentStatus.Active, this.m_Source);
					downloadHandler.Dispose();
					this.m_ProvideHandle.Complete<AssetBundleResource>(this, true, null);
					this.m_Completed = true;
				}
				if (!string.IsNullOrEmpty(this.m_Options.Hash) && this.m_Options.ClearOtherCachedVersionsWhenLoaded)
				{
					Caching.ClearOtherCachedVersions(this.m_Options.BundleName, Hash128.Parse(this.m_Options.Hash));
				}
			}
			else
			{
				if (this.HasTimedOut)
				{
					uwrResult.Error = "Request timeout";
				}
				webReq = this.m_WebRequestQueueOperation.m_WebRequest;
				if (uwrResult == null)
				{
					uwrResult = new UnityWebRequestResult(this.m_WebRequestQueueOperation.m_WebRequest);
				}
				downloadHandler = webReq.downloadHandler as DownloadHandlerAssetBundle;
				downloadHandler.Dispose();
				bool forcedRetry = false;
				string message = string.Format("Web request failed, retrying ({0}/{1})...\n{2}", this.m_Retries, this.m_Options.RetryCount, uwrResult);
				bool canRetryRequest = this.m_RequestRetryCallback(uwrResult);
				if (!string.IsNullOrEmpty(this.m_Options.Hash) && this.m_Source == BundleSource.Cache)
				{
					message = string.Format("Web request failed to load from cache. The cached AssetBundle will be cleared from the cache and re-downloaded. Retrying...\n{0}", uwrResult);
					Caching.ClearCachedVersion(this.m_Options.BundleName, Hash128.Parse(this.m_Options.Hash));
					if (this.m_Retries == 0 && canRetryRequest)
					{
						Debug.LogFormat(message, Array.Empty<object>());
						this.BeginOperation();
						this.m_Retries++;
						forcedRetry = true;
					}
				}
				if (!forcedRetry)
				{
					if (this.m_Retries < this.m_Options.RetryCount && canRetryRequest)
					{
						this.m_Retries++;
						Debug.LogFormat(message, Array.Empty<object>());
						this.BeginOperation();
					}
					else
					{
						message = "Unable to load asset bundle from : " + webReq.url;
						if (!canRetryRequest && this.m_Options.RetryCount > 0)
						{
							message += string.Format("\nRetry count set to {0} but cannot retry request due to error {1}. To override use a custom AssetBundle provider.", this.m_Options.RetryCount, uwrResult.Error);
						}
						RemoteProviderException exception = new RemoteProviderException(message, this.m_ProvideHandle.Location, uwrResult, null);
						this.m_ProvideHandle.Complete<AssetBundleResource>(null, false, exception);
						this.m_Completed = true;
						this.RemoveBundleFromProfiler();
					}
				}
			}
			webReq.Dispose();
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00007F76 File Offset: 0x00006176
		public bool Unload(out AssetBundleUnloadOperation unloadOp)
		{
			unloadOp = null;
			if (this.m_AssetBundle != null)
			{
				unloadOp = this.m_AssetBundle.UnloadAsync(true);
				this.m_AssetBundle = null;
			}
			this.m_RequestOperation = null;
			this.RemoveBundleFromProfiler();
			return unloadOp != null;
		}

		// Token: 0x040000B2 RID: 178
		private AssetBundle m_AssetBundle;

		// Token: 0x040000B3 RID: 179
		private AsyncOperation m_RequestOperation;

		// Token: 0x040000B4 RID: 180
		internal WebRequestQueueOperation m_WebRequestQueueOperation;

		// Token: 0x040000B5 RID: 181
		internal ProvideHandle m_ProvideHandle;

		// Token: 0x040000B6 RID: 182
		internal AssetBundleRequestOptions m_Options;

		// Token: 0x040000B7 RID: 183
		[NonSerialized]
		private bool m_RequestCompletedCallbackCalled;

		// Token: 0x040000B8 RID: 184
		private int m_Retries;

		// Token: 0x040000B9 RID: 185
		private BundleSource m_Source;

		// Token: 0x040000BA RID: 186
		private long m_BytesToDownload;

		// Token: 0x040000BB RID: 187
		private long m_DownloadedBytes;

		// Token: 0x040000BC RID: 188
		private bool m_Completed;

		// Token: 0x040000BD RID: 189
		private AssetBundleUnloadOperation m_UnloadOperation;

		// Token: 0x040000BE RID: 190
		private const int k_WaitForWebRequestMainThreadSleep = 1;

		// Token: 0x040000BF RID: 191
		private string m_TransformedInternalId;

		// Token: 0x040000C0 RID: 192
		private AssetBundleRequest m_PreloadRequest;

		// Token: 0x040000C1 RID: 193
		private bool m_PreloadCompleted;

		// Token: 0x040000C2 RID: 194
		private ulong m_LastDownloadedByteCount;

		// Token: 0x040000C3 RID: 195
		private float m_TimeoutTimer;

		// Token: 0x040000C4 RID: 196
		private int m_TimeoutOverFrames;

		// Token: 0x040000C5 RID: 197
		internal bool m_DownloadOnly;

		// Token: 0x040000C6 RID: 198
		private int m_LastFrameCount = -1;

		// Token: 0x040000C7 RID: 199
		private float m_TimeSecSinceLastUpdate;

		// Token: 0x040000C8 RID: 200
		internal Func<UnityWebRequestResult, bool> m_RequestRetryCallback = (UnityWebRequestResult x) => x.ShouldRetryDownloadError();

		// Token: 0x02000048 RID: 72
		public enum LoadType
		{
			// Token: 0x040000CA RID: 202
			None,
			// Token: 0x040000CB RID: 203
			Local,
			// Token: 0x040000CC RID: 204
			Web
		}
	}
}
