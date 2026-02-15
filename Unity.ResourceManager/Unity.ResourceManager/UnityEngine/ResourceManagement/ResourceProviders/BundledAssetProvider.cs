using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine.Profiling;
using UnityEngine.ResourceManagement.Profiling;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.Util;

namespace UnityEngine.ResourceManagement.ResourceProviders
{
	// Token: 0x02000050 RID: 80
	[DisplayName("Assets from Bundles Provider")]
	public class BundledAssetProvider : ResourceProviderBase
	{
		// Token: 0x060001D4 RID: 468 RVA: 0x000086EB File Offset: 0x000068EB
		public override void Provide(ProvideHandle provideHandle)
		{
			new BundledAssetProvider.InternalOp().Start(provideHandle);
		}

		// Token: 0x02000051 RID: 81
		internal class InternalOp
		{
			// Token: 0x060001D6 RID: 470 RVA: 0x000086F8 File Offset: 0x000068F8
			internal static T LoadBundleFromDependecies<T>(IList<object> results) where T : class, IAssetBundleResource
			{
				if (results == null || results.Count == 0)
				{
					return default(T);
				}
				IAssetBundleResource bundle = null;
				bool firstBundleWrapper = true;
				for (int i = 0; i < results.Count; i++)
				{
					IAssetBundleResource abWrapper = results[i] as IAssetBundleResource;
					if (abWrapper != null)
					{
						abWrapper.GetAssetBundle();
						if (firstBundleWrapper)
						{
							bundle = abWrapper;
						}
						firstBundleWrapper = false;
					}
				}
				return bundle as T;
			}

			// Token: 0x060001D7 RID: 471 RVA: 0x0000875C File Offset: 0x0000695C
			internal static bool IsDownloadOnly(IList<object> results)
			{
				foreach (object obj in results)
				{
					AssetBundleResource assetBundleResource = obj as AssetBundleResource;
					if (assetBundleResource != null && assetBundleResource.m_DownloadOnly)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x060001D8 RID: 472 RVA: 0x000087B4 File Offset: 0x000069B4
			public void Start(ProvideHandle provideHandle)
			{
				provideHandle.SetProgressCallback(new Func<float>(this.ProgressCallback));
				provideHandle.SetWaitForCompletionCallback(new Func<bool>(this.WaitForCompletionHandler));
				this.subObjectName = null;
				this.m_ProvideHandle = provideHandle;
				this.m_RequestOperation = null;
				List<object> deps = new List<object>();
				this.m_ProvideHandle.GetDependencies(deps);
				IAssetBundleResource bundleResource = BundledAssetProvider.InternalOp.LoadBundleFromDependecies<IAssetBundleResource>(deps);
				if (bundleResource == null)
				{
					AssetBundle assetBundle = null;
					bool flag = false;
					string text = "Unable to load dependent bundle from location ";
					IResourceLocation location = this.m_ProvideHandle.Location;
					this.m_ProvideHandle.Complete<AssetBundle>(assetBundle, flag, new Exception(text + ((location != null) ? location.ToString() : null)));
					return;
				}
				this.m_AssetBundle = bundleResource.GetAssetBundle();
				if (this.m_AssetBundle == null)
				{
					AssetBundle assetBundle2 = null;
					bool flag2 = false;
					string text2 = "Unable to load dependent bundle from location ";
					IResourceLocation location2 = this.m_ProvideHandle.Location;
					this.m_ProvideHandle.Complete<AssetBundle>(assetBundle2, flag2, new Exception(text2 + ((location2 != null) ? location2.ToString() : null)));
					return;
				}
				AssetBundleResource assetBundleResource = bundleResource as AssetBundleResource;
				if (assetBundleResource != null)
				{
					this.m_PreloadRequest = assetBundleResource.GetAssetPreloadRequest();
				}
				if (this.m_PreloadRequest == null || this.m_PreloadRequest.isDone)
				{
					this.BeginAssetLoad();
					return;
				}
				this.m_PreloadRequest.completed += delegate(AsyncOperation operation)
				{
					this.BeginAssetLoad();
				};
			}

			// Token: 0x060001D9 RID: 473 RVA: 0x000088E4 File Offset: 0x00006AE4
			private void BeginAssetLoad()
			{
				if (this.m_AssetBundle == null)
				{
					AssetBundle assetBundle = null;
					bool flag = false;
					string text = "Unable to load dependent bundle from location ";
					IResourceLocation location = this.m_ProvideHandle.Location;
					this.m_ProvideHandle.Complete<AssetBundle>(assetBundle, flag, new Exception(text + ((location != null) ? location.ToString() : null)));
					return;
				}
				string assetPath = this.m_ProvideHandle.ResourceManager.TransformInternalId(this.m_ProvideHandle.Location);
				string mainPath;
				string subKey;
				if (this.m_ProvideHandle.Type.IsArray)
				{
					this.m_RequestOperation = this.m_AssetBundle.LoadAssetWithSubAssetsAsync(assetPath, this.m_ProvideHandle.Type.GetElementType());
				}
				else if (this.m_ProvideHandle.Type.IsGenericType && typeof(IList<>) == this.m_ProvideHandle.Type.GetGenericTypeDefinition())
				{
					this.m_RequestOperation = this.m_AssetBundle.LoadAssetWithSubAssetsAsync(assetPath, this.m_ProvideHandle.Type.GetGenericArguments()[0]);
				}
				else if (ResourceManagerConfig.ExtractKeyAndSubKey(assetPath, out mainPath, out subKey))
				{
					this.subObjectName = subKey;
					this.m_RequestOperation = this.m_AssetBundle.LoadAssetWithSubAssetsAsync(mainPath, this.m_ProvideHandle.Type);
				}
				else
				{
					this.m_RequestOperation = this.m_AssetBundle.LoadAssetAsync(assetPath, this.m_ProvideHandle.Type);
				}
				if (this.m_RequestOperation != null)
				{
					if (this.m_RequestOperation.isDone)
					{
						this.ActionComplete(this.m_RequestOperation);
						return;
					}
					if (Profiler.enabled && this.m_ProvideHandle.IsValid)
					{
						ProfilerRuntime.AddAssetOperation(this.m_ProvideHandle, ContentStatus.Loading);
					}
					this.m_RequestOperation.completed += this.ActionComplete;
				}
			}

			// Token: 0x060001DA RID: 474 RVA: 0x00008A8C File Offset: 0x00006C8C
			private bool WaitForCompletionHandler()
			{
				if (this.m_PreloadRequest != null && !this.m_PreloadRequest.isDone)
				{
					return this.m_PreloadRequest.asset == null;
				}
				return this.m_Result != null || (this.m_RequestOperation != null && (this.m_RequestOperation.isDone || this.m_RequestOperation.asset != null));
			}

			// Token: 0x060001DB RID: 475 RVA: 0x00008AF4 File Offset: 0x00006CF4
			private void ActionComplete(AsyncOperation obj)
			{
				if (this.m_RequestOperation != null)
				{
					if (this.m_ProvideHandle.Type.IsArray)
					{
						this.GetArrayResult(this.m_RequestOperation.allAssets);
					}
					else if (this.m_ProvideHandle.Type.IsGenericType && typeof(IList<>) == this.m_ProvideHandle.Type.GetGenericTypeDefinition())
					{
						this.GetListResult(this.m_RequestOperation.allAssets);
					}
					else if (string.IsNullOrEmpty(this.subObjectName))
					{
						this.GetAssetResult(this.m_RequestOperation.asset);
					}
					else
					{
						this.GetAssetSubObjectResult(this.m_RequestOperation.allAssets);
					}
				}
				this.CompleteOperation();
			}

			// Token: 0x060001DC RID: 476 RVA: 0x00008BAE File Offset: 0x00006DAE
			private void GetArrayResult(Object[] allAssets)
			{
				this.m_Result = ResourceManagerConfig.CreateArrayResult(this.m_ProvideHandle.Type, allAssets);
			}

			// Token: 0x060001DD RID: 477 RVA: 0x00008BC7 File Offset: 0x00006DC7
			private void GetListResult(Object[] allAssets)
			{
				this.m_Result = ResourceManagerConfig.CreateListResult(this.m_ProvideHandle.Type, allAssets);
			}

			// Token: 0x060001DE RID: 478 RVA: 0x00008BE0 File Offset: 0x00006DE0
			private void GetAssetResult(Object asset)
			{
				this.m_Result = ((asset != null && this.m_ProvideHandle.Type.IsAssignableFrom(asset.GetType())) ? asset : null);
			}

			// Token: 0x060001DF RID: 479 RVA: 0x00008C10 File Offset: 0x00006E10
			private void GetAssetSubObjectResult(Object[] allAssets)
			{
				foreach (Object o in allAssets)
				{
					if (o.name == this.subObjectName && this.m_ProvideHandle.Type.IsAssignableFrom(o.GetType()))
					{
						this.m_Result = o;
						return;
					}
				}
			}

			// Token: 0x060001E0 RID: 480 RVA: 0x00008C64 File Offset: 0x00006E64
			private void CompleteOperation()
			{
				if (Profiler.enabled && this.m_Result != null && this.m_ProvideHandle.IsValid)
				{
					ProfilerRuntime.AddAssetOperation(this.m_ProvideHandle, ContentStatus.Active);
				}
				Exception e = ((this.m_Result == null) ? new Exception(string.Format("Unable to load asset of type {0} from location {1}.", this.m_ProvideHandle.Type, this.m_ProvideHandle.Location)) : null);
				this.m_ProvideHandle.Complete<object>(this.m_Result, this.m_Result != null, e);
			}

			// Token: 0x060001E1 RID: 481 RVA: 0x00008CE9 File Offset: 0x00006EE9
			public float ProgressCallback()
			{
				if (this.m_RequestOperation == null)
				{
					return 0f;
				}
				return this.m_RequestOperation.progress;
			}

			// Token: 0x040000D9 RID: 217
			private AssetBundle m_AssetBundle;

			// Token: 0x040000DA RID: 218
			private AssetBundleRequest m_PreloadRequest;

			// Token: 0x040000DB RID: 219
			private AssetBundleRequest m_RequestOperation;

			// Token: 0x040000DC RID: 220
			private object m_Result;

			// Token: 0x040000DD RID: 221
			private ProvideHandle m_ProvideHandle;

			// Token: 0x040000DE RID: 222
			private string subObjectName;
		}
	}
}
