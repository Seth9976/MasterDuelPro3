using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.AddressableAssets.ResourceProviders;
using UnityEngine.ResourceManagement;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace UnityEngine.AddressableAssets
{
	// Token: 0x0200003C RID: 60
	internal class UpdateCatalogsOperation : AsyncOperationBase<List<IResourceLocator>>
	{
		// Token: 0x0600019E RID: 414 RVA: 0x00006A84 File Offset: 0x00004C84
		public UpdateCatalogsOperation(AddressablesImpl aa)
		{
			this.m_Addressables = aa;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00006A94 File Offset: 0x00004C94
		public AsyncOperationHandle<List<IResourceLocator>> Start(IEnumerable<string> catalogIds, bool autoCleanBundleCache)
		{
			this.m_LocatorInfos = new List<ResourceLocatorInfo>();
			List<IResourceLocation> locations = new List<IResourceLocation>();
			foreach (string c in catalogIds)
			{
				if (c != null)
				{
					ResourceLocatorInfo loc = this.m_Addressables.GetLocatorInfo(c);
					locations.Add(loc.CatalogLocation);
					this.m_LocatorInfos.Add(loc);
				}
			}
			if (locations.Count == 0)
			{
				return this.m_Addressables.ResourceManager.CreateCompletedOperation<List<IResourceLocator>>(null, "Content update not available.");
			}
			ContentCatalogProvider ccp = this.m_Addressables.ResourceManager.ResourceProviders.FirstOrDefault((IResourceProvider rp) => rp.GetType() == typeof(ContentCatalogProvider)) as ContentCatalogProvider;
			if (ccp != null)
			{
				ccp.DisableCatalogUpdateOnStart = false;
			}
			this.m_DepOp = this.m_Addressables.ResourceManager.CreateGroupOperation<object>(locations);
			this.m_AutoCleanBundleCache = autoCleanBundleCache;
			return this.m_Addressables.ResourceManager.StartOperation<List<IResourceLocator>>(this, this.m_DepOp);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00006BB0 File Offset: 0x00004DB0
		protected override bool InvokeWaitForCompletion()
		{
			if (base.IsDone)
			{
				return true;
			}
			if (this.m_DepOp.IsValid() && !this.m_DepOp.IsDone)
			{
				this.m_DepOp.WaitForCompletion();
			}
			ResourceManager rm = this.m_RM;
			if (rm != null)
			{
				rm.Update(Time.unscaledDeltaTime);
			}
			if (!this.HasExecuted)
			{
				base.InvokeExecute();
			}
			if (this.m_CleanCacheOp.IsValid() && !this.m_CleanCacheOp.IsDone)
			{
				this.m_CleanCacheOp.WaitForCompletion();
			}
			this.m_Addressables.ResourceManager.Update(Time.unscaledDeltaTime);
			return base.IsDone;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00006C52 File Offset: 0x00004E52
		protected override void Destroy()
		{
			this.m_DepOp.Release();
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00006C5F File Offset: 0x00004E5F
		public override void GetDependencies(List<AsyncOperationHandle> dependencies)
		{
			dependencies.Add(this.m_DepOp);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00006C74 File Offset: 0x00004E74
		protected override void Execute()
		{
			List<IResourceLocator> catalogs = new List<IResourceLocator>(this.m_DepOp.Result.Count);
			for (int i = 0; i < this.m_DepOp.Result.Count; i++)
			{
				IResourceLocator locator = this.m_DepOp.Result[i].Result as IResourceLocator;
				string localHash = null;
				IResourceLocation remoteLocation = null;
				if (locator == null)
				{
					ContentCatalogData contentCatalogData = this.m_DepOp.Result[i].Result as ContentCatalogData;
					locator = contentCatalogData.CreateCustomLocator(contentCatalogData.location.PrimaryKey, null, 100);
					localHash = contentCatalogData.LocalHash;
					remoteLocation = contentCatalogData.location;
				}
				this.m_LocatorInfos[i].UpdateContent(locator, localHash, remoteLocation);
				catalogs.Add(this.m_LocatorInfos[i].Locator);
			}
			if (this.m_AutoCleanBundleCache)
			{
				this.m_CleanCacheOp = this.m_Addressables.CleanBundleCache(this.m_DepOp, false);
				this.OnCleanCacheCompleted(this.m_CleanCacheOp, catalogs);
				return;
			}
			if (this.m_DepOp.Status == AsyncOperationStatus.Succeeded)
			{
				base.Complete(catalogs, true, null);
				return;
			}
			if (this.m_DepOp.Status == AsyncOperationStatus.Failed)
			{
				base.Complete(catalogs, false, "Cannot update catalogs. Failed to load catalog: " + this.m_DepOp.OperationException.Message);
				return;
			}
			string errorMessage = "Cannot update catalogs. Catalog loading operation is still in progress when it should already be completed. ";
			errorMessage += ((this.m_DepOp.OperationException != null) ? this.m_DepOp.OperationException.Message : "");
			base.Complete(catalogs, false, errorMessage);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00006E08 File Offset: 0x00005008
		private void OnCleanCacheCompleted(AsyncOperationHandle<bool> handle, List<IResourceLocator> catalogs)
		{
			handle.Completed += delegate(AsyncOperationHandle<bool> obj)
			{
				bool success = obj.Status == AsyncOperationStatus.Succeeded;
				this.Complete(catalogs, success, success ? null : string.Format("{0}, status={1}, result={2} catalogs updated, but failed to clean bundle cache.", obj.DebugName, obj.Status, obj.Result));
			};
		}

		// Token: 0x040000C1 RID: 193
		private AddressablesImpl m_Addressables;

		// Token: 0x040000C2 RID: 194
		private List<ResourceLocatorInfo> m_LocatorInfos;

		// Token: 0x040000C3 RID: 195
		internal AsyncOperationHandle<IList<AsyncOperationHandle>> m_DepOp;

		// Token: 0x040000C4 RID: 196
		private AsyncOperationHandle<bool> m_CleanCacheOp;

		// Token: 0x040000C5 RID: 197
		private bool m_AutoCleanBundleCache;
	}
}
