using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.AddressableAssets.ResourceProviders;
using UnityEngine.Networking;
using UnityEngine.ResourceManagement;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.ResourceManagement.Util;

namespace UnityEngine.AddressableAssets.Initialization
{
	// Token: 0x02000061 RID: 97
	internal class InitializationOperation : AsyncOperationBase<IResourceLocator>
	{
		// Token: 0x06000260 RID: 608 RVA: 0x00009C66 File Offset: 0x00007E66
		public InitializationOperation(AddressablesImpl aa)
		{
			this.m_Addressables = aa;
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000261 RID: 609 RVA: 0x00009C75 File Offset: 0x00007E75
		protected override float Progress
		{
			get
			{
				if (this.m_rtdOp.IsValid())
				{
					return this.m_rtdOp.PercentComplete;
				}
				return 0f;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000262 RID: 610 RVA: 0x00009C95 File Offset: 0x00007E95
		protected override string DebugName
		{
			get
			{
				return "InitializationOperation";
			}
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00009C9C File Offset: 0x00007E9C
		internal static AsyncOperationHandle<IResourceLocator> CreateInitializationOperation(AddressablesImpl aa, string playerSettingsLocation, string providerSuffix)
		{
			JsonAssetProvider jp = new JsonAssetProvider();
			aa.ResourceManager.ResourceProviders.Add(jp);
			TextDataProvider tdp = new TextDataProvider();
			aa.ResourceManager.ResourceProviders.Add(tdp);
			aa.ResourceManager.ResourceProviders.Add(new ContentCatalogProvider(aa.ResourceManager));
			ResourceLocationBase runtimeDataLocation = new ResourceLocationBase("RuntimeData", playerSettingsLocation, typeof(JsonAssetProvider).FullName, typeof(ResourceManagerRuntimeData), Array.Empty<IResourceLocation>());
			InitializationOperation initOp = new InitializationOperation(aa)
			{
				m_rtdOp = aa.ResourceManager.ProvideResource<ResourceManagerRuntimeData>(runtimeDataLocation),
				m_ProviderSuffix = providerSuffix,
				m_InitGroupOps = new InitalizationObjectsOperation()
			};
			initOp.m_InitGroupOps.Init(initOp.m_rtdOp, aa);
			AsyncOperationHandle<bool> groupOpHandle = aa.ResourceManager.StartOperation<bool>(initOp.m_InitGroupOps, initOp.m_rtdOp);
			return aa.ResourceManager.StartOperation<IResourceLocator>(initOp, groupOpHandle);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00009D8C File Offset: 0x00007F8C
		protected override bool InvokeWaitForCompletion()
		{
			if (base.IsDone)
			{
				return true;
			}
			if (this.m_rtdOp.IsValid() && !this.m_rtdOp.IsDone)
			{
				this.m_rtdOp.WaitForCompletion();
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
			if (this.m_loadCatalogOp.IsValid() && !this.m_loadCatalogOp.IsDone)
			{
				this.m_loadCatalogOp.WaitForCompletion();
				ResourceManager rm2 = this.m_RM;
				if (rm2 != null)
				{
					rm2.Update(Time.unscaledDeltaTime);
				}
			}
			return this.m_rtdOp.IsDone && this.m_loadCatalogOp.IsDone;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00009E44 File Offset: 0x00008044
		protected override void Execute()
		{
			if (this.m_rtdOp.Result == null)
			{
				Addressables.LogWarningFormat("Addressables - Unable to load runtime data at location {0}.", new object[] { this.m_rtdOp });
				base.Complete(base.Result, false, string.Format("Addressables - Unable to load runtime data at location {0}.", this.m_rtdOp));
				return;
			}
			ResourceManagerRuntimeData rtd = this.m_rtdOp.Result;
			WebRequestQueue.SetMaxConcurrentRequests(rtd.MaxConcurrentWebRequests);
			this.m_Addressables.CatalogRequestsTimeout = rtd.CatalogRequestsTimeout;
			foreach (ResourceLocationData catalogLocation in rtd.CatalogLocations)
			{
				if (catalogLocation.Data != null)
				{
					ProviderLoadRequestOptions loadData = catalogLocation.Data as ProviderLoadRequestOptions;
					if (loadData != null)
					{
						loadData.WebRequestTimeout = rtd.CatalogRequestsTimeout;
					}
				}
			}
			this.m_rtdOp.Release();
			if (rtd.CertificateHandlerType != null)
			{
				this.m_Addressables.ResourceManager.CertificateHandlerInstance = Activator.CreateInstance(rtd.CertificateHandlerType) as CertificateHandler;
			}
			if (!rtd.LogResourceManagerExceptions)
			{
				ResourceManager.ExceptionHandler = null;
			}
			ContentCatalogProvider ccp = this.m_Addressables.ResourceManager.ResourceProviders.FirstOrDefault((IResourceProvider rp) => rp.GetType() == typeof(ContentCatalogProvider)) as ContentCatalogProvider;
			if (ccp != null)
			{
				ccp.DisableCatalogUpdateOnStart = rtd.DisableCatalogUpdateOnStartup;
				ccp.IsLocalCatalogInBundle = rtd.IsLocalCatalogInBundle;
			}
			ResourceLocationMap locMap = new ResourceLocationMap("CatalogLocator", rtd.CatalogLocations);
			this.m_Addressables.AddResourceLocator(locMap, null, null);
			IList<IResourceLocation> catalogs;
			if (!locMap.Locate("AddressablesMainContentCatalog", typeof(ContentCatalogData), out catalogs))
			{
				Addressables.LogWarningFormat("Addressables - Unable to find any catalog locations in the runtime data.", Array.Empty<object>());
				this.m_Addressables.RemoveResourceLocator(locMap);
				base.Complete(base.Result, false, "Addressables - Unable to find any catalog locations in the runtime data.");
				return;
			}
			IResourceLocation remoteHashLocation = null;
			if (catalogs[0].Dependencies.Count == 2 && rtd.DisableCatalogUpdateOnStartup)
			{
				remoteHashLocation = catalogs[0].Dependencies[0];
				catalogs[0].Dependencies[0] = catalogs[0].Dependencies[1];
			}
			this.m_loadCatalogOp = this.LoadContentCatalogInternal(catalogs, 0, locMap, remoteHashLocation);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000A09C File Offset: 0x0000829C
		private static void LoadProvider(AddressablesImpl addressables, ObjectInitializationData providerData, string providerSuffix)
		{
			int indexOfExistingProvider = -1;
			string newProviderId = (string.IsNullOrEmpty(providerSuffix) ? providerData.Id : (providerData.Id + providerSuffix));
			for (int i = 0; i < addressables.ResourceManager.ResourceProviders.Count; i++)
			{
				if (addressables.ResourceManager.ResourceProviders[i].ProviderId == newProviderId)
				{
					indexOfExistingProvider = i;
					break;
				}
			}
			if (indexOfExistingProvider >= 0 && string.IsNullOrEmpty(providerSuffix))
			{
				return;
			}
			IResourceProvider provider = providerData.CreateInstance<IResourceProvider>(newProviderId);
			if (provider == null)
			{
				Addressables.LogWarningFormat("Addressables - Unable to load resource provider from {0}.", new object[] { providerData });
				return;
			}
			if (indexOfExistingProvider < 0 || !string.IsNullOrEmpty(providerSuffix))
			{
				addressables.ResourceManager.ResourceProviders.Add(provider);
				return;
			}
			addressables.ResourceManager.ResourceProviders[indexOfExistingProvider] = provider;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000A16C File Offset: 0x0000836C
		private static AsyncOperationHandle<IResourceLocator> OnCatalogDataLoaded(AddressablesImpl addressables, AsyncOperationHandle<ContentCatalogData> op, string providerSuffix, IResourceLocation remoteHashLocation)
		{
			ContentCatalogData data = op.Result;
			if (data == null)
			{
				Exception opException = ((op.OperationException != null) ? new Exception("Failed to load content catalog.", op.OperationException) : new Exception("Failed to load content catalog."));
				op.Release();
				return addressables.ResourceManager.CreateCompletedOperationWithException<IResourceLocator>(null, opException);
			}
			op.Release();
			if (data.ResourceProviderData != null)
			{
				foreach (ObjectInitializationData providerData in data.ResourceProviderData)
				{
					InitializationOperation.LoadProvider(addressables, providerData, providerSuffix);
				}
			}
			if (addressables.InstanceProvider == null)
			{
				IInstanceProvider prov = data.InstanceProviderData.CreateInstance<IInstanceProvider>(null);
				if (prov != null)
				{
					addressables.InstanceProvider = prov;
				}
			}
			if (addressables.SceneProvider == null)
			{
				ISceneProvider prov2 = data.SceneProviderData.CreateInstance<ISceneProvider>(null);
				if (prov2 != null)
				{
					addressables.SceneProvider = prov2;
				}
			}
			if (remoteHashLocation != null)
			{
				data.location.Dependencies[0] = remoteHashLocation;
			}
			IResourceLocator locMap = data.CreateCustomLocator(data.location.PrimaryKey, providerSuffix, 100);
			addressables.AddResourceLocator(locMap, data.LocalHash, data.location);
			addressables.AddResourceLocator(new DynamicResourceLocator(addressables), null, null);
			return addressables.ResourceManager.CreateCompletedOperation<IResourceLocator>(locMap, string.Empty);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000A2C0 File Offset: 0x000084C0
		public static AsyncOperationHandle<IResourceLocator> LoadContentCatalog(AddressablesImpl addressables, IResourceLocation loc, string providerSuffix, IResourceLocation remoteHashLocation = null)
		{
			Type provType = typeof(ProviderOperation<ContentCatalogData>);
			ProviderOperation<ContentCatalogData> catalogOp = addressables.ResourceManager.CreateOperation<ProviderOperation<ContentCatalogData>>(provType, provType.GetHashCode(), null, null);
			IResourceProvider catalogProvider = null;
			foreach (IResourceProvider provider in addressables.ResourceManager.ResourceProviders)
			{
				if (provider is ContentCatalogProvider)
				{
					catalogProvider = provider;
					break;
				}
			}
			AsyncOperationHandle<IList<AsyncOperationHandle>> dependencies = addressables.ResourceManager.CreateGroupOperation<string>(loc.Dependencies, true);
			catalogOp.Init(addressables.ResourceManager, catalogProvider, loc, dependencies, true);
			AsyncOperationHandle<ContentCatalogData> catalogHandle = addressables.ResourceManager.StartOperation<ContentCatalogData>(catalogOp, dependencies);
			dependencies.Release();
			return addressables.ResourceManager.CreateChainOperation<IResourceLocator, ContentCatalogData>(catalogHandle, (AsyncOperationHandle<ContentCatalogData> res) => InitializationOperation.OnCatalogDataLoaded(addressables, res, providerSuffix, remoteHashLocation));
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000A3D4 File Offset: 0x000085D4
		public AsyncOperationHandle<IResourceLocator> LoadContentCatalog(IResourceLocation loc, string providerSuffix, IResourceLocation remoteHashLocation)
		{
			return InitializationOperation.LoadContentCatalog(this.m_Addressables, loc, providerSuffix, remoteHashLocation);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000A3E4 File Offset: 0x000085E4
		internal AsyncOperationHandle<IResourceLocator> LoadContentCatalogInternal(IList<IResourceLocation> catalogs, int index, ResourceLocationMap locMap, IResourceLocation remoteHashLocation)
		{
			AsyncOperationHandle<IResourceLocator> loadOp = this.LoadContentCatalog(catalogs[index], this.m_ProviderSuffix, remoteHashLocation);
			if (loadOp.IsDone)
			{
				this.LoadOpComplete(loadOp, catalogs, locMap, index, remoteHashLocation);
			}
			else
			{
				loadOp.Completed += delegate(AsyncOperationHandle<IResourceLocator> op)
				{
					this.LoadOpComplete(op, catalogs, locMap, index, remoteHashLocation);
				};
			}
			return loadOp;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000A480 File Offset: 0x00008680
		private void LoadOpComplete(AsyncOperationHandle<IResourceLocator> op, IList<IResourceLocation> catalogs, ResourceLocationMap locMap, int index, IResourceLocation remoteHashLocation)
		{
			if (op.Result != null)
			{
				this.m_Addressables.RemoveResourceLocator(locMap);
				base.Result = op.Result;
				base.Complete(base.Result, true, string.Empty);
				op.Release();
				return;
			}
			if (index + 1 >= catalogs.Count)
			{
				Addressables.LogWarningFormat("Addressables - initialization failed.", new object[] { op });
				this.m_Addressables.RemoveResourceLocator(locMap);
				if (op.OperationException != null)
				{
					base.Complete(base.Result, false, op.OperationException, true);
				}
				else
				{
					base.Complete(base.Result, false, "LoadContentCatalogInternal");
				}
				op.Release();
				return;
			}
			this.m_loadCatalogOp = this.LoadContentCatalogInternal(catalogs, index + 1, locMap, remoteHashLocation);
			op.Release();
		}

		// Token: 0x04000156 RID: 342
		private AsyncOperationHandle<ResourceManagerRuntimeData> m_rtdOp;

		// Token: 0x04000157 RID: 343
		private AsyncOperationHandle<IResourceLocator> m_loadCatalogOp;

		// Token: 0x04000158 RID: 344
		private string m_ProviderSuffix;

		// Token: 0x04000159 RID: 345
		private AddressablesImpl m_Addressables;

		// Token: 0x0400015A RID: 346
		private InitalizationObjectsOperation m_InitGroupOps;
	}
}
