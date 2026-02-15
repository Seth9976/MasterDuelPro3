using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.ResourceManagement.Util;

namespace UnityEngine.AddressableAssets
{
	// Token: 0x0200003B RID: 59
	internal class CleanBundleCacheOperation : AsyncOperationBase<bool>, IUpdateReceiver
	{
		// Token: 0x06000192 RID: 402 RVA: 0x000066E6 File Offset: 0x000048E6
		public CleanBundleCacheOperation(AddressablesImpl aa, bool forceSingleThreading)
		{
			this.m_Addressables = aa;
			this.m_UseMultiThreading = !forceSingleThreading && PlatformUtilities.PlatformUsesMultiThreading(Application.platform);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000670B File Offset: 0x0000490B
		public AsyncOperationHandle<bool> Start(AsyncOperationHandle<IList<AsyncOperationHandle>> depOp)
		{
			this.m_DepOp = depOp.Acquire();
			return this.m_Addressables.ResourceManager.StartOperation<bool>(this, this.m_DepOp);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00006736 File Offset: 0x00004936
		public void CompleteInternal(bool result, bool success, string errorMsg)
		{
			this.m_DepOp.Release();
			base.Complete(result, success, errorMsg);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000674C File Offset: 0x0000494C
		protected override bool InvokeWaitForCompletion()
		{
			if (!this.m_DepOp.IsDone)
			{
				this.m_DepOp.WaitForCompletion();
			}
			if (!this.HasExecuted)
			{
				base.InvokeExecute();
			}
			if (this.m_EnumerationThread != null)
			{
				this.m_EnumerationThread.Join();
				this.RemoveCacheEntries();
			}
			return base.IsDone;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000679F File Offset: 0x0000499F
		protected override void Destroy()
		{
			if (this.m_DepOp.IsValid())
			{
				this.m_DepOp.Release();
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x000067B9 File Offset: 0x000049B9
		public override void GetDependencies(List<AsyncOperationHandle> dependencies)
		{
			dependencies.Add(this.m_DepOp);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x000067CC File Offset: 0x000049CC
		protected override void Execute()
		{
			if (this.m_DepOp.Status == AsyncOperationStatus.Failed)
			{
				this.CompleteInternal(false, false, "Could not clean cache because a dependent catalog operation failed.");
				return;
			}
			HashSet<string> cacheDirsInUse = this.GetCacheDirsInUse(this.m_DepOp.Result);
			if (!Caching.ready)
			{
				this.CompleteInternal(false, false, "Cache is not ready to be accessed.");
			}
			this.m_BaseCachePath = Caching.currentCacheForWriting.path;
			if (this.m_UseMultiThreading)
			{
				this.m_EnumerationThread = new Thread(new ParameterizedThreadStart(this.DetermineCacheDirsNotInUse));
				this.m_EnumerationThread.Start(cacheDirsInUse);
				return;
			}
			this.DetermineCacheDirsNotInUse(cacheDirsInUse);
			this.RemoveCacheEntries();
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00006867 File Offset: 0x00004A67
		void IUpdateReceiver.Update(float unscaledDeltaTime)
		{
			if (this.m_UseMultiThreading && !this.m_EnumerationThread.IsAlive)
			{
				this.m_EnumerationThread = null;
				this.RemoveCacheEntries();
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000688C File Offset: 0x00004A8C
		private void RemoveCacheEntries()
		{
			foreach (string text in this.m_CacheDirsForRemoval)
			{
				Caching.ClearAllCachedVersions(Path.GetFileName(text));
			}
			this.CompleteInternal(true, true, null);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x000068EC File Offset: 0x00004AEC
		private void DetermineCacheDirsNotInUse(object data)
		{
			this.DetermineCacheDirsNotInUse((HashSet<string>)data);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x000068FC File Offset: 0x00004AFC
		private void DetermineCacheDirsNotInUse(HashSet<string> cacheDirsInUse)
		{
			this.m_CacheDirsForRemoval = new List<string>();
			if (Directory.Exists(this.m_BaseCachePath))
			{
				foreach (string cacheDir in Directory.EnumerateDirectories(this.m_BaseCachePath, "*", SearchOption.TopDirectoryOnly))
				{
					if (!cacheDirsInUse.Contains(cacheDir))
					{
						this.m_CacheDirsForRemoval.Add(cacheDir);
					}
				}
			}
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000697C File Offset: 0x00004B7C
		private HashSet<string> GetCacheDirsInUse(IList<AsyncOperationHandle> catalogOps)
		{
			HashSet<string> cacheDirsInUse = new HashSet<string>();
			for (int i = 0; i < catalogOps.Count; i++)
			{
				IResourceLocator locator = catalogOps[i].Result as IResourceLocator;
				if (locator == null)
				{
					ContentCatalogData catData = catalogOps[i].Result as ContentCatalogData;
					if (catData == null)
					{
						return cacheDirsInUse;
					}
					locator = catData.CreateCustomLocator(catData.location.PrimaryKey, null, 100);
				}
				foreach (IResourceLocation location in locator.AllLocations)
				{
					AssetBundleRequestOptions options = location.Data as AssetBundleRequestOptions;
					if (options != null)
					{
						AssetBundleResource.LoadType loadType;
						string path;
						AssetBundleResource.GetLoadInfo(location, this.m_Addressables.ResourceManager, out loadType, out path);
						if (loadType == AssetBundleResource.LoadType.Web)
						{
							string cacheDir = Path.Combine(Caching.currentCacheForWriting.path, options.BundleName);
							cacheDirsInUse.Add(cacheDir);
						}
					}
				}
			}
			return cacheDirsInUse;
		}

		// Token: 0x040000BB RID: 187
		private AddressablesImpl m_Addressables;

		// Token: 0x040000BC RID: 188
		private AsyncOperationHandle<IList<AsyncOperationHandle>> m_DepOp;

		// Token: 0x040000BD RID: 189
		private List<string> m_CacheDirsForRemoval;

		// Token: 0x040000BE RID: 190
		private Thread m_EnumerationThread;

		// Token: 0x040000BF RID: 191
		private string m_BaseCachePath;

		// Token: 0x040000C0 RID: 192
		private bool m_UseMultiThreading;
	}
}
